using System.Runtime.Serialization;

namespace YooniK.Face.Client.Models.Responses
{
    [DataContract]
    public class MatchingResponse
    {
        [DataMember(Name = "score")]
        public double Score { get; set; }
    }
}
