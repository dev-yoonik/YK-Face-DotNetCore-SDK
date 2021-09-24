using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace YooniK.Face.Client.Models.Responses
{
    /// <summary>
    /// EnrolledIds
    /// </summary>
    [DataContract]
    public partial class EnrolledIdsResponse : List<string>
    {
        [JsonConstructorAttribute]
        public EnrolledIdsResponse() : base()
        {
        }
    }
}
