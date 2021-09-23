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
        static string Marcelo1stPhotoPath = "../../../Marcelo_Rebelo_de_Sousa1.jpg";
        static string Marcelo2ndPhotoPath = "../../../Marcelo_Rebelo_de_Sousa2.png";

        public static string ImageToBase64String(string filepath)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
            return Convert.ToBase64String(imageArray);
        }

        static async Task<int> Main(string[] args)
        {
            string baseUrl = "INSERT_HERE";
            string subscriptionKey = "INSERT_HERE";
            try
            {

                var serverInfo = new ConnectionInformation(baseUrl, subscriptionKey);

                FaceClient faceClient = new FaceClient(serverInfo);

                string base64Marcelo1 = ImageToBase64String(Marcelo1stPhotoPath);
                string base64Marcelo2 = ImageToBase64String(Marcelo2ndPhotoPath);

                VerifyImagesResponse verifyImages = await faceClient.VerifyImagesAsync(base64Marcelo1, base64Marcelo2);
                Console.WriteLine($"Similarity Score: { verifyImages.Score }");

                List<ProcessResponse> process = await faceClient.ProcessAync(base64Marcelo1);
                string Marcelo1Template = process.Count == 1 ? process[0].Template : null;

                List<ProcessResponse> process2 = await faceClient.ProcessAync(base64Marcelo2);
                string Marcelo2Template = process2.Count == 1 ? process2[0].Template : null;

                VerifyResponse verify = await faceClient.VerifyAsync(Marcelo1Template, Marcelo2Template);
                Console.WriteLine($"Similarity Score (w/Template): {verify.Score}");

                string galleryGuid = Guid.NewGuid().ToString();
                string personGuid = Guid.NewGuid().ToString();
                string personGuid2 = Guid.NewGuid().ToString();

                await faceClient.AddGalleryAsync(galleryGuid);
                await faceClient.AddPersonToGalleryAsync(galleryGuid, personGuid, Marcelo1Template);
                await faceClient.AddPersonToGalleryAsync(galleryGuid, personGuid2, Marcelo2Template);

                TemplateResponse template = await faceClient.GetPersonTemplateFromGalleryAsync(galleryGuid, personGuid);
                Console.WriteLine(template.Template);

                EnrolledIdsResponse enrolledIds = await faceClient.GetEnrolledPersonsAsync(galleryGuid);
                foreach (string enrolledId in enrolledIds)
                    Console.Write(enrolledId);

                await faceClient.RemovePersonFromGalleryAsync(galleryGuid, personGuid);
                await faceClient.RemoveGalleryAsync(galleryGuid);
            }
            catch (HttpRequestException)
            {

                throw;
            }
            return 1;
        }
    }
}
