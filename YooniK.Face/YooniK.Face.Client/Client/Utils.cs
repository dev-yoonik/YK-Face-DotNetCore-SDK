using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YooniK.Face.Client.Models.Responses;


namespace YooniK.Face.Client.Client
{
    public class Utils
    {
        public static class FaceProcessErrors
        {
            public const string None = "None";
            public const string FaceNotDetected = "Face not detected";
            public const string MultipleFacesDetected = "Multiple faces detected";
        }

        /// <summary>
        ///  Checks if the face process returned object has indeed an <see cref="ProcessResponse"/> object (i.e it has a detected face)
        ///  and if it only has one (i.e only one face was detected)
        /// </summary>
        /// <param name="processResponse">List of <see cref="List{ProcessResponse}"/></param>
        /// <returns><see cref="FaceProcessErrors"/> corresponding message</returns>
        public static string face_process_validation(List<ProcessResponse> processResponse)
        {
            if (processResponse.Count == 0)
                return FaceProcessErrors.FaceNotDetected;
            if (processResponse.Count > 1)
                return FaceProcessErrors.MultipleFacesDetected;
            return FaceProcessErrors.None;
        }
    }
}
