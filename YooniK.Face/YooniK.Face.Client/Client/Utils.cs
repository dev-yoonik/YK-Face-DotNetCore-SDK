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
