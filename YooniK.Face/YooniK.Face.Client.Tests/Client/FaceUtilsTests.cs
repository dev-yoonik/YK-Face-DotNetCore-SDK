using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YooniK.Face.Client.Client;
using YooniK.Face.Client.Models.Responses;

namespace YooniK.Face.Client.Tests.Client
{
    class FaceUtilsTests
    {
        [TestCase]
        public void face_process_validation_with_multiple_faces()
        {
            // Mock FaceClient ProcessAsync Response
            List<ProcessResponse> processResponseList = new List<ProcessResponse>();
            for (int i = 0; i < 5; i++)
            {
                processResponseList.Add(new ProcessResponse());
            }
            var error_message = Utils.face_process_validation(processResponseList);
            Assert.AreEqual(error_message, Utils.FaceProcessErrors.MultipleFacesDetected);
        }

        [TestCase]
        public void face_process_validation_with_face_not_detected()
        {
            // Mock FaceClient ProcessAsync Response
            List<ProcessResponse> processResponseList = new List<ProcessResponse>();
            var error_message = Utils.face_process_validation(processResponseList);
            Assert.AreEqual(error_message, Utils.FaceProcessErrors.FaceNotDetected);
        }

        [TestCase]
        public void face_process_validation_with_no_errors()
        {
            // Mock FaceClient ProcessAsync Response
            List<ProcessResponse> processResponseList = new List<ProcessResponse>();
            processResponseList.Add(new ProcessResponse());
            var error_message = Utils.face_process_validation(processResponseList);
            Assert.AreEqual(error_message, Utils.FaceProcessErrors.None);
        }
    }
}
