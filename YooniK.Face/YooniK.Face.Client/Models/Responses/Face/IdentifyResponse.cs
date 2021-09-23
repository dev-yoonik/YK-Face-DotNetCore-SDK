using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YooniK.Face.Client.Models.Responses
{
    [DataContract]
    public class IdentifyResponse
    {
        [DataMember(Name = "score")]
        public double Score { get; set; }
        
        [DataMember(Name = "template_id")]
        public string TemplateId { get; set; }

        [JsonConstructorAttribute]
        public IdentifyResponse() : base()
        {
        }
    }
}
