using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YooniK.Services.Client;
using YooniK.Face.Client.Models.Requests;
using YooniK.Face.Client.Models.Responses;
using YooniK.Services.Client.Common;
using YooniK.Face.Client.Client.FaceException;

namespace YooniK.Face.Client
{
    public static class FaceEndpoints
    {
        public const string Process = "face/process";
        public const string Verify = "face/verify";
        public const string VerifyId = "face/verify_id";
        public const string Identify = "face/identify";
    }

    public static class GalleryEndpoints
    {
        public const string Gallery = "gallery/";
    }

    public class FaceClient
    {
        private readonly string ENVIRONMENT_VARIABLE_BASE_URL = "YK_FACE_BASE_URL";
        private readonly string ENVIRONMENT_VARIABLE_X_API_KEY = "YK_FACE_X_API_KEY";

        private IServiceClient _serviceClient;

        public FaceClient()
        {
            string baseUrl = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_BASE_URL);
            string x_api_key = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_X_API_KEY);
            if (baseUrl == null)
                throw new ArgumentException($"Environment Variable '{ENVIRONMENT_VARIABLE_BASE_URL}' not found. ");
            if (x_api_key == null)
                throw new ArgumentException($"Environment Variable '{ENVIRONMENT_VARIABLE_X_API_KEY}' not found. ");
            _serviceClient = new ServiceClient(new ConnectionInformation(baseUrl, x_api_key));
        }

        public FaceClient(IConnectionInformation connectionInformation)
        {
           _serviceClient = new ServiceClient(connectionInformation);
        }

        /// <summary>
        ///     Processes all the image containing faces.
        /// </summary>
        /// <param name="image"> A base64 string representing an image.</param>
        /// <param name="processings">
        ///     List of desired processings (if None, it will perform all processings):
        ///          'detect'   - Perform face and landmarks detection.
        ///          'analyze'  - Perform quality analysis(brightness, contrast, sharpness, etc).
        ///          'templify' - Perform template extraction.
        /// </param>
        /// <returns> A list of <see cref="ProcessResponse"/> (contains the face processed information). </returns>
        public async Task<List<ProcessResponse>> ProcessAsync(string image, List<ProcessRequest.ProcessingsEnum> processings = null)
        {
            try
            {
                var process = new ProcessRequest(image, processings);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.Process,
                    request: process
                    );

                return await _serviceClient.SendRequestAsync<List<ProcessResponse>>(message);
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
        /// <param name="firstTemplate"> Biometric template of one face. </param>
        /// <param name="secondTemplate"> Biometric template of another face. </param>
        /// <returns><see cref="MatchingResponse"/></returns>
        public async Task<MatchingResponse> VerifyAsync(string firstTemplate, string secondTemplate)
        {
            try
            {
                var verify = new VerifyRequest(firstTemplate, secondTemplate);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.Verify,
                    request: verify
                    );

               return await _serviceClient.SendRequestAsync<MatchingResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Compares a template to a specific Persons template in a Gallery.
        ///     <br></br><br></br><br></br>
        ///     Note: Although different names TemplateId is the same as PersonId.
        /// </summary>
        /// <param name="template"> Biometric template to compare. </param>
        /// <param name="templateId"> Identifies the stored template which is going to be compared to. </param>
        /// <param name="galleryId"> Identifies the Gallery in which the Person is enrolled. </param>
        /// <returns><see cref="MatchingResponse"/></returns>
        public async Task<MatchingResponse> VerifyIdAsync(string template, string templateId, string galleryId)
        {
            try
            {
                var verifyId = new VerifyIdRequest(template, templateId, galleryId);

                var message = new RequestMessage(
                    httpMethod: HttpMethod.Post,
                    urlRelativePath: FaceEndpoints.VerifyId,
                    request: verifyId
                    );

                return await _serviceClient.SendRequestAsync<MatchingResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Given two images in base 64 format it calculates the score of similarity between the two faces.
        /// </summary>
        /// <param name="firstImage"> First base 64 string image. </param>
        /// <param name="secondImage"> Second base 64 string image. </param>
        /// <returns><see cref="MatchingResponse"/></returns>
        public async Task<MatchingResponse> VerifyImagesAsync(string firstImage, string secondImage)
        {
            try
            {
                var firstProcess = ProcessAsync(firstImage);
                var secondProcess = ProcessAsync(secondImage);

                await Task.WhenAll(new [] { firstProcess, secondProcess });

                if (firstProcess.Result.Count > 0 && secondProcess.Result.Count > 0)
                {
                    var verify = new VerifyRequest(
                    firstProcess.Result[0].Template,
                    secondProcess.Result[0].Template);

                    var message = new RequestMessage(
                        httpMethod: HttpMethod.Post,
                        urlRelativePath: FaceEndpoints.Verify,
                        request: verify
                        );

                    return await _serviceClient.SendRequestAsync<MatchingResponse>(message);
                }
                throw new FaceException("Both images must contain a detected face.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     A biometric template is compared with the templates stored in a specific Gallery.
        ///     A list is returned with the score of similarity and the Person id.
        ///     Search an identification template against the enrollment set of a specified gallery.
        /// </summary>
        /// <param name="template"> A biometric template to compare the others to. </param>
        /// <param name="galleryId"> The gallery that stores the templates to be examine. </param>
        /// <param name="candidateListLength"> Defines the length of the returning candidates list. </param>
        /// <param name="minimumScore"> Defines the minimum threshold in which the candidates are accepted. </param>
        /// <returns>A list of <see cref="IdentifyResponse"/></returns>
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

                return await _serviceClient.SendRequestAsync<List<IdentifyResponse>>(message);
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
                await _serviceClient.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Eliminates the gallery collection data and its instance.
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

                await _serviceClient.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Returns the list of PersonsIds contained in a Gallery.
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <returns><see cref="EnrolledIdsResponse"/></returns>
        public async Task<EnrolledIdsResponse> GetEnrolledPersonsAsync(string galleryId)
        {
            try
            {
                var message = new RequestMessage(
                   httpMethod: HttpMethod.Get,
                   urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}"
                );

                return await _serviceClient.SendRequestAsync<EnrolledIdsResponse>(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Removes the Persons data from the Gallery.
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

                await _serviceClient.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Adds the Persons data to a specific Gallery.
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
                await _serviceClient.SendRequestAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Returns the Template stored for a specific person and gallery. 
        /// </summary>
        /// <param name="galleryId"> Gallery identifier. </param>
        /// <param name="personId"> Person identifier. </param>
        /// <returns><see cref="TemplateResponse"/></returns>
        public async Task<TemplateResponse> GetPersonTemplateFromGalleryAsync(string galleryId, string personId)
        {
            try
            {
                var message = new RequestMessage(
                  httpMethod: HttpMethod.Get,
                  urlRelativePath: $"{GalleryEndpoints.Gallery}{galleryId}/{personId}"
                );

                return await _serviceClient.SendRequestAsync<TemplateResponse>(message);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
