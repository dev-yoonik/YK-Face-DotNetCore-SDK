using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YooniK.Services.Client.Common;

namespace YooniK.Face.Client.Models.Requests
{
    [DataContract]
    public class ProcessRequest : IRequest, IEquatable<ProcessRequest>
    {

        /// <summary>
        /// Defines Processings
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ProcessingsEnum
        {
            /// <summary>
            /// Enum Detect for value: detect
            /// </summary>
            [EnumMember(Value = "detect")]
            Detect = 1,
            /// <summary>
            /// Enum Analyze for value: analyze
            /// </summary>
            [EnumMember(Value = "analyze")]
            Analyze = 2,

            /// <summary>
            /// Enum Templify for value: templify
            /// </summary>
            [EnumMember(Value = "templify")]
            Templify = 3,
        }

        /// <summary>
        /// Requested biometric processings.
        /// </summary>
        /// <value>Requested biometric processings.</value>
        [DataMember(Name = "processings", EmitDefaultValue = false)]
        public List<ProcessingsEnum> Processings { get; set; } = new List<ProcessingsEnum>();


        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        [DataMember(Name = "image", EmitDefaultValue = false)]
        public string Image { get; set; }

        public ProcessRequest(string image = null, List<ProcessingsEnum> processings = null, List<Object> configuration = null)
        {
            if (image == null)
                throw new ArgumentNullException("An image is a required property for ProcessRequest and cannot be null");
            if (processings == null || processings.Count == 0)
                processings = new List<ProcessingsEnum> { ProcessingsEnum.Analyze, ProcessingsEnum.Detect, ProcessingsEnum.Templify };

            this.Image = image;
            this.Processings = processings;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Extensible configurations for biometric processing.
        /// </summary>
        /// <value>Extensible configurations for biometric processing.</value>
        [DataMember(Name = "configuration", EmitDefaultValue = false)]
        public List<Object> Configuration { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProcessRequest {\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  Processings: ").Append(Processings).Append("\n");
            sb.Append("  Configuration: ").Append(Configuration).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ProcessRequest);
        }

        /// <summary>
        /// Returns true if ProcessRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of ProcessRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProcessRequest input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Image == input.Image ||
                    (this.Image != null &&
                    this.Image.Equals(input.Image))
                ) &&
                (
                    this.Processings == input.Processings ||
                    this.Processings != null &&
                    this.Processings.SequenceEqual(input.Processings)
                ) &&
                (
                    this.Configuration == input.Configuration ||
                    this.Configuration != null &&
                    this.Configuration.SequenceEqual(input.Configuration)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Image != null)
                    hashCode = hashCode * 59 + this.Image.GetHashCode();
                if (this.Processings != null)
                    hashCode = hashCode * 59 + this.Processings.GetHashCode();
                if (this.Configuration != null)
                    hashCode = hashCode * 59 + this.Configuration.GetHashCode();
                return hashCode;
            }
        }
    }
}
