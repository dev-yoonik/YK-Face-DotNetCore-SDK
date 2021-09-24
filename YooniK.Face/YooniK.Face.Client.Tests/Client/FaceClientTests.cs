using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YooniK.Face.DataExamples;
using YooniK.Face.Client.Models.Responses;
using static YooniK.Face.Client.Models.Requests.ProcessRequest;
using System.Net.Http;
using YooniK.Services.Client.Common;
using System.Net;

namespace YooniK.Face.Client.Tests
{
    /// <summary>
    ///     Please note that these are functional tests that need a proper connection to the YooniK.FaceAPI to run.
    /// </summary>
    class FaceClientTests
    {
        private FaceClient faceClient;

        private string baseUrl = Valid.BaseUrl;
        private string subscritionKey = Valid.SubscriptionKey;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var connectionInformation = new ConnectionInformation(baseUrl, subscritionKey);
            faceClient = new FaceClient(connectionInformation);
        }


        [Test]
        [TestCase(Valid.BaseImage64, new[] { ProcessingsEnum.Templify, ProcessingsEnum.Analyze })]
        [TestCase(Valid.BaseImage64, new[] { ProcessingsEnum.Templify })]
        [TestCase(Valid.BaseImage64, new[] { ProcessingsEnum.Templify, ProcessingsEnum.Analyze, ProcessingsEnum.Detect })]
        public async Task ProcessAsync_ValidParameters_Success(string b64Image, IEnumerable<ProcessingsEnum> processings)
        {
            var response = await faceClient.ProcessAync(b64Image, new List<ProcessingsEnum>(processings));
            Assert.IsInstanceOf<List<ProcessResponse>>(response);
            foreach (var process in response)
                Assert.IsNotNull(process.Height);
        }

        [Test]
        [TestCase(null, null)]
        [TestCase(null, new[] { ProcessingsEnum.Templify, ProcessingsEnum.Analyze })]
        public void ProcessAsync_NullParameters_ExceptionThrown(string b64Image, IEnumerable<ProcessingsEnum> processings)
        {
            List<ProcessingsEnum> processingsList = processings != null ? new List<ProcessingsEnum>(processings) : null;
            Assert.ThrowsAsync<ArgumentNullException>(async () => await faceClient.ProcessAync(b64Image, processingsList));
        }

        [Test]
        [TestCase(Valid.BiometricTemplate, Valid.BiometricTemplate)]
        public async Task VerifyAsync_ValidParameters_Success(string template1, string template2)
        {
            var response = await faceClient.VerifyAsync(template1, template2);
            Assert.IsInstanceOf<VerifyResponse>(response);
            Assert.IsNotNull(response.Score);
        }

        [Test]
        [TestCase(Invalid.BiometricTemplate, Valid.BiometricTemplate)]
        public void VerifyAsync_InvalidParameters_ExceptionThrown(string template1, string template2)
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await faceClient.VerifyAsync(template1, template2));
        }

        [Test]
        [TestCase(Valid.BiometricTemplate)]
        public async Task VerifyIdAsync_ValidParameters_Success(string template)
        {
            string galleryGuid = Guid.NewGuid().ToString();
            string personId = "personId1";

            await faceClient.AddGalleryAsync(galleryGuid);
            await faceClient.AddPersonToGalleryAsync(galleryGuid, personId, template);

            // PersonId(Gallery Namespace) corresponds to TemplateId(Face Namespace)
            var response = await faceClient.VerifyIdAsync(template, personId, galleryGuid);
            Assert.IsInstanceOf<VerifyIdResponse>(response);
            Assert.IsNotNull(response.Score);
        }

        [Test]
        [TestCase(Valid.BiometricTemplate, Invalid.PersonId)]
        public async Task VerifyIdAsync_InvalidPersonIdParameter_ExceptionThrown(string validTemplate, string invalidPersonId)
        {
            string galleryGuid = Guid.NewGuid().ToString();
            string personId = Guid.NewGuid().ToString();

            await faceClient.AddGalleryAsync(galleryGuid);
            await faceClient.AddPersonToGalleryAsync(galleryGuid, personId, validTemplate);

            // PersonId(Gallery Namespace) corresponds to TemplateId(Face Namespace)
            Assert.ThrowsAsync<HttpRequestException>(async () => await faceClient.VerifyIdAsync(validTemplate, invalidPersonId, galleryGuid));
        }
        
        [Test]
        [TestCase(Invalid.BiometricTemplate)]
        public async Task AddPersonToGalleryAsync_InvalidTemplateParameter_ExceptionThrown(string invalidTemplate)
        {
            string galleryGuid = Guid.NewGuid().ToString();
            string personId = Guid.NewGuid().ToString();

            await faceClient.AddGalleryAsync(galleryGuid);
            
            Assert.ThrowsAsync<HttpRequestException>(async () =>
                await faceClient.AddPersonToGalleryAsync(galleryGuid, personId, invalidTemplate)
            );
        }

        [Test]
        [TestCase(Valid.BaseImage64, Valid.BaseImage64)]
        public async Task VerifyImagesAsync_ValidParameters_Success(string b64Image1, string b64Image2)
        {
            var response = await faceClient.VerifyImagesAsync(b64Image1, b64Image2);
            Assert.IsInstanceOf<VerifyImagesResponse>(response);
            Assert.IsNotNull(response.Score);
        }

        [Test]
        public async Task IdentifyAsync_ValidParameters_Success()
        {
            string galleryId = Guid.NewGuid().ToString();
            await faceClient.AddGalleryAsync(galleryId);
            await faceClient.AddPersonToGalleryAsync(galleryId, Valid.PersonId, Valid.BiometricTemplate);

            var response = await faceClient.IdentifyAsync(Valid.BiometricTemplate, galleryId, candidateListLength: 3, minimumScore: 0.8);
            Assert.IsInstanceOf<List<IdentifyResponse>>(response);
            Assert.IsTrue(response.Count > 0);  
        }

        [Test]
        [Order(1)]
        [TestCase(Valid.GalleryId)]
        public void AddGalleryAsync_ValidParameter_Success(string galleryId)
        {
            Assert.DoesNotThrowAsync(async () => await faceClient.AddGalleryAsync(galleryId));
        }

        [Test]
        [Order(2)]
        [TestCase(Valid.GalleryId)]
        public async Task GetEnrolledPersonsAsync_OnEmptyGallery_Success(string galleryId)
        {
            EnrolledIdsResponse enrolledIds = await faceClient.GetEnrolledPersonsAsync(galleryId);
            Assert.True(enrolledIds.Count == 0);
        }

        [Test]
        [Order(3)]
        [TestCase(Valid.GalleryId, Valid.PersonId, Valid.BiometricTemplate)]
        public void AddPersonToGalleryAsync_ValidParameters_Success(string galleryId, string personId, string template)
        {
            Assert.DoesNotThrowAsync(async () => await faceClient.AddPersonToGalleryAsync(galleryId, personId, template));
        }

        [Test]
        [Order(4)]
        [TestCase(Valid.GalleryId, Valid.PersonId)]
        public async Task GetEnrolledPersonsAsync_AfterPersonAdded_Success(string galleryId, string personId)
        {
            EnrolledIdsResponse enrolledIds = await faceClient.GetEnrolledPersonsAsync(galleryId);
            foreach (string enrolledId in enrolledIds)
                if (enrolledId.Equals(personId))
                    Assert.Pass();
            Assert.Fail();
        }

        [Test]
        [Order(5)]
        [TestCase(Valid.GalleryId, Valid.PersonId)]
        public void GetPersonTemplateFromGalleryAsync_ValidParameters_Success(string galleryId, string personId)
        {
            Assert.DoesNotThrowAsync(async () => await faceClient.GetPersonTemplateFromGalleryAsync(galleryId, personId));
        }


        [Test]
        [Order(6)]
        [TestCase(Valid.GalleryId, Valid.PersonId)]
        public void RemovePersonFromGalleryAsync_ValidParameters_Success(string galleryId, string personId)
        {
            Assert.DoesNotThrowAsync(async () => await faceClient.RemovePersonFromGalleryAsync(galleryId, personId));
        }

        [Test]
        [Order(7)]
        [TestCase(Valid.GalleryId, Valid.PersonId)]
        public async Task GetEnrolledPersonsAsync_AfterPersonRemoval_Success(string galleryId, string personId)
        {
            EnrolledIdsResponse enrolledIds = await faceClient.GetEnrolledPersonsAsync(galleryId);
            foreach (string enrolledId in enrolledIds)
                if (enrolledId.Equals(personId))
                    Assert.Fail();
            Assert.Pass();
        }

        [Test]
        [Order(8)]
        [TestCase(Valid.GalleryId)]
        public void RemoveGalleryAsync_ValidParameter_Success(string galleryId)
        {
            Assert.DoesNotThrowAsync(async () => await faceClient.RemoveGalleryAsync(galleryId));
        }


        [Test]
        public async Task AddGalleriesAsync_AddedTwoGalleriesWithSameName_ExceptionThrown()
        {
            string galleryGuid = Guid.NewGuid().ToString();
            await faceClient.AddGalleryAsync(galleryGuid);
          
            HttpRequestException exception = Assert.ThrowsAsync<HttpRequestException>(async () => await faceClient.AddGalleryAsync(galleryGuid));
            Assert.True(exception.StatusCode.Equals(HttpStatusCode.Conflict));
        }

        [Test]
        [TestCase(Valid.PersonId, Valid.BiometricTemplate)]
        public async Task AddPersonToGalleryAsync_AddedTwiceTheSamePerson_ExceptionThrown(string personId, string template)
        {
            string galleryGuid = Guid.NewGuid().ToString();

            await faceClient.AddGalleryAsync(galleryGuid);
            await faceClient.AddPersonToGalleryAsync(galleryGuid, personId, template);

            HttpRequestException exception = Assert.ThrowsAsync<HttpRequestException>(async () => await faceClient.AddPersonToGalleryAsync(galleryGuid, personId, template));
            Assert.True(exception.StatusCode.Equals(HttpStatusCode.Conflict));
        }
    }
}
