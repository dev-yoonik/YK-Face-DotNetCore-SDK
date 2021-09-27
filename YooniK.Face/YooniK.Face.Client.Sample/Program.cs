using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YooniK.Services.Client.Common;
using YooniK.Face.Client;
using YooniK.Face.Client.Models.Responses;

namespace YooniK.Face.Sample
{
    class Program
    {
        static string firstPhoto = "../../../first_sample.png";
        static string secondPhoto = "../../../second_sample.png";

        public static string ImageToBase64String(string filepath)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
            return Convert.ToBase64String(imageArray);
        }

        static async Task<int> Main(string[] args)
        {
            string baseUrl = "YOUR-API-ENDPOINT";
            string subscriptionKey = "YOUR-X-API-KEY-ENDPOINT";

            try
            {                
                var faceConnectionInformation = new ConnectionInformation(baseUrl, subscriptionKey);
                
                // Instantiates the FaceClient, passing its server connection information
                FaceClient faceClient = new FaceClient(faceConnectionInformation);

                // Represents the photo files in base 64 string
                string firstPhotoInBase64 = ImageToBase64String(firstPhoto);
                string secondPhotoInBase64 = ImageToBase64String(secondPhoto);

                // Verifies the faces similarity between two images in base 64
                VerifyImagesResponse verifyImages = await faceClient.VerifyImagesAsync(firstPhotoInBase64, secondPhotoInBase64);
                Console.WriteLine($"Similarity Score: { verifyImages.Score }");

                // Processes all the image containing faces, and returns its information in a list. This photos only contains one face. 
                List<ProcessResponse> process = await faceClient.ProcessAync(firstPhotoInBase64);
                string firstPhotoTemplate = process.Count == 1 ? process[0].Template : null;

                List<ProcessResponse> process2 = await faceClient.ProcessAync(secondPhotoInBase64);
                string secondPhotoTemplate = process2.Count == 1 ? process2[0].Template : null;

                // Verifies the faces similarity between the extracted biometric template from the processed images
                VerifyResponse verify = await faceClient.VerifyAsync(firstPhotoTemplate, secondPhotoTemplate);
                Console.WriteLine($"Similarity Score (w/Template): {verify.Score}");

                // Unique identifiers for the Gallery
                string galleryGuid = Guid.NewGuid().ToString();
                
                // Unique identifiers for the Persons
                string personGuid = Guid.NewGuid().ToString();
                string personGuid2 = Guid.NewGuid().ToString();

                // Creating a Gallery
                await faceClient.AddGalleryAsync(galleryGuid);
                
                // Saving a person in a specific gallery
                await faceClient.AddPersonToGalleryAsync(galleryGuid, personGuid, firstPhotoTemplate);
                await faceClient.AddPersonToGalleryAsync(galleryGuid, personGuid2, secondPhotoTemplate);

                // Gets the persons stored template given the gallery id and person id
                TemplateResponse template = await faceClient.GetPersonTemplateFromGalleryAsync(galleryGuid, personGuid);
                Console.WriteLine($"Stored template: {template.Template}");

                // Gets the list of enrolled persons in a specific gallery
                EnrolledIdsResponse enrolledIds = await faceClient.GetEnrolledPersonsAsync(galleryGuid);
                if (enrolledIds.Count == 0)
                    Console.WriteLine($"There aren't enrolled persons in {galleryGuid} at this moment.");
                else
                {
                    Console.WriteLine("Enrolled persons: ");
                    foreach (string enrolledId in enrolledIds)
                        Console.Write(enrolledId);
                }

                // Removes a specific person from a gallery
                await faceClient.RemovePersonFromGalleryAsync(galleryGuid, personGuid);
                
                // Deletes an entire gallery collection and its instance
                await faceClient.RemoveGalleryAsync(galleryGuid);
            }
            catch (Exception)
            {

                throw;
            }
            return 1;
        }
    }
}
