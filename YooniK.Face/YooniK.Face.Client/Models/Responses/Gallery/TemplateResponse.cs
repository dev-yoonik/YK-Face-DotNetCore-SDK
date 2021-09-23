using System.Runtime.Serialization;

namespace YooniK.Face.Client.Models.Responses
{
    [DataContract]
    public class TemplateResponse
    {
        [DataMember(Name = "template")]
        public string Template { get; set; }
    }
}
