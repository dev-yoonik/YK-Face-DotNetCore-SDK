using System;

namespace YooniK.Face.Client.Client.FaceException
{

    [Serializable]
    class FaceException : Exception
    {
        public FaceException(string message) : base(message)
        {
        }
    }
}
