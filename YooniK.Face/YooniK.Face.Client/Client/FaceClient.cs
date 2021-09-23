using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YooniK.Services.Client;
using YooniK.Face.Client.Models.Requests;
using YooniK.Face.Client.Models.Responses;
using YooniK.Services.Client.Common;

namespace YooniK.Face.Client
{
    public static class FaceEndpoints
    {
        public const string Process = "face/process";
        public const string Verify = "face/verify";
        public const string VerifyId = "face/verify_id";
        public const string VerifyImages = "face/verify_images";
        public const string Identify = "face/identify";
    }

    public static class GalleryEndpoints
    {
        public const string Gallery = "gallery/";
    }

    public class FaceClient
    {
        private IServiceClient _clientService;

        public FaceClient(ConnectionInformation connectionInformation)
        {
           _clientService = new ServiceClient(connectionInformation);
        }

        /// <summary>
        /// Process human faces in an image.
        /// Given an image and a set of configuration parameters and minimal set of parameters return an array of YooniK biometric object
        /// </summary>
        /// <param name="image"> A base64 string representing an image.</param>
        /// <param name="processings">
        ///     List of desired processings (if None, it will perform all processings):
        ///          'detect'   - Perform face and landmarks detection.
        ///          'analyze'  - Perform quality analysis(brightness, contrast, sharpness, etc).
        ///          'templify' - Perform template extraction.</param>
        /// <returns>List of face entries in format.</returns>
        public async Task<List<ProcessResponse>> ProcessAync(string image, List<ProcessRequest.ProcessingsEnum> processings = null)
        {
            try
            {
                var process = new ProcessRequest(image, processings);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.Process,
                    request: process
                    );

                return await _clientService.SendRequestAsync<List<ProcessResponse>>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Verify whether two faces belong to the same person.
        ///     Compares two descriptors and outputs a similarity score
        /// </summary>
        /// <param name="firstTemplate"> Biometric template of one face(obtained from `face.process`).</param>
        /// <param name="secondTemplate"> Biometric template of another face(obtained from `face.process`).</param>
        /// <returns>VerifyResponse</returns>
        public async Task<VerifyResponse> VerifyAsync(string firstTemplate, string secondTemplate)
        {
            try
            {
                var verify = new VerifyRequest(firstTemplate, secondTemplate);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.Verify,
                    request: verify
                    );

               return await _clientService.SendRequestAsync<VerifyResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Validates if the sent template belongs to a specific Person in a Gallery.
        ///     Match template with a specific id in a gallery.
        /// </summary>
        /// <param name="template"> New biometric template to validate. </param>
        /// <param name="templateId"> Identifier of the Original Template stored in the gallery. </param>
        /// <param name="galleryId"> The gallery identifier in which the Original Template is stored. </param>
        /// <returns></returns>
        public async Task<VerifyIdResponse> VerifyIdAsync(string template, string templateId, string galleryId)
        {
            try
            {
                var verifyId = new VerifyIdRequest(template, templateId, galleryId);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.VerifyId,
                    request: verifyId
                    );

                return await _clientService.SendRequestAsync<VerifyIdResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Takes two images in base 64 format and outputs the score of similarity between the two faces.
        /// </summary>
        /// <param name="b64FirstImage"> First base 64 string image. </param>
        /// <param name="b64SecondImage"> Second base 64 string image. </param>
        /// <returns></returns>
        public async Task<VerifyImagesResponse> VerifyImagesAsync(string b64FirstImage, string b64SecondImage)
        {
            try
            {
                var verifyImages = new VerifyImagesRequest(b64FirstImage, b64SecondImage);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.VerifyImages,
                    request: verifyImages
                    );

                return await _clientService.SendRequestAsync<VerifyImagesResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     The given biometric template is compared with the templates stored in the specified Gallery.
        ///     A list is returned with the score of similarity and the template id.
        ///     Search an identification template against the enrollment set of a specified gallery.
        /// </summary>
        /// <param name="template"> A new biometric template to compare the others to. </param>
        /// <param name="galleryId"> The gallery to examine. </param>
        /// <param name="candidateListLength"> Defines the length of the returning candidates list. </param>
        /// <param name="minimumScore"> Defines the minimum threshold in which the candidates are accepted. </param>
        /// <returns></returns>
        public async Task<List<IdentifyResponse>> IdentifyAsync(string template, string galleryId, int candidateListLength = 1, double minimumScore = -1.0)
        {
            try
            {
                var identify = new IdentifyRequest(template, galleryId, candidateListLength, minimumScore);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.Identify,
                    request: identify
                    );

                return await _clientService.SendRequestAsync<List<IdentifyResponse>>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     A new gallery is created with the provided name (must be unique).
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <returns></returns>
        public async Task AddGalleryAsync(string galleryId)
        {
            try
            {
                if (String.IsNullOrEmpty(galleryId))
                    throw new ArgumentNullException("GalleryId is a required property for GalleryClientNs cannot be null nor empty");

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}"
                    );
                await _clientService.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Eliminates the Gallery with the given identifier.
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <returns></returns>
        public async Task RemoveGalleryAsync(string galleryId)
        {
            try
            {
                var message = new RequestMessage(
                    httpMethod: HttpMethod.Delete,
                    urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}"
                );

                await _clientService.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Returns the list of PersonsIds contained in the Gallery provided.
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <returns></returns>
        public async Task<EnrolledIdsResponse> GetEnrolledPersonsAsync(string galleryId)
        {
            try
            {
                var message = new RequestMessage(
                   httpMethod: HttpMethod.Get,
                   urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}"
                );

                return await _clientService.SendRequestAsync<EnrolledIdsResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Removes the Person information from the Gallery.
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <param name="personId"> Person identifier. </param>
        /// <returns></returns>
        public async Task RemovePersonFromGalleryAsync(string galleryId, string personId)
        {
            try
            {
                var message = new RequestMessage(
                   httpMethod: HttpMethod.Delete,
                   urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}/{personId}"
                );

                await _clientService.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Adds the Persons information to the identified Gallery.
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <param name="personId"> Person identifier. </param>
        /// <param name="template"> Person biometric template. </param>
        /// <returns></returns>
        public async Task AddPersonToGalleryAsync(string galleryId, string personId, string template)
        {
            try
            {
                var request = new TemplateRequest(template);

                var message = new RequestMessage(
                  httpMethod: HttpMethod.Post,
                  request: request,
                  urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}/{personId}"
                );
                await _clientService.SendRequestAsync<string>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Returns the Template stored for the specified person and gallery. 
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <param name="personId"> Person identifier. </param>
        /// <returns></returns>
        public async Task<TemplateResponse> GetPersonTemplateFromGalleryAsync(string galleryId, string personId)
        {
            try
            {
                var message = new RequestMessage(
                  httpMethod: HttpMethod.Get,
                  urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}/{personId}"
                );

                return await _clientService.SendRequestAsync<TemplateResponse>(message);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
