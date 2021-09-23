using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using YooniK.Services.Client.Common;

namespace YooniK.Face.Client.Models.Requests
{
    [DataContract]
    public class VerifyRequest : IRequest, IEquatable<VerifyRequest>, IValidatableObject
    {
        [DataMember(Name = "first_template")]
        public string FirstTemplate { get; set; } = String.Empty;
        [DataMember(Name = "second_template")]
        public string SecondTemplate { get; set; } = String.Empty;

        /// <summary>
        /// Instanciates a VerifyRequest Class
        /// </summary>
        /// <param name="first">First Template</param>
        /// <param name="second">Second Template</param>
        public VerifyRequest(string first, string second)
        {
            if (first == null)
                throw new ArgumentNullException("The first template is a required property for VerifyRequest and cannot be null");
            if (second == null)
                throw new ArgumentNullException("The first template is a required property for VerifyRequest and cannot be null");
            
            FirstTemplate = first;
            SecondTemplate = second;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VerifyRequest {\n");
            sb.Append("  FirstTemplate: ").Append(FirstTemplate).Append("\n");
            sb.Append("  SecondTemplate: ").Append(SecondTemplate).Append("\n");
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
            return this.Equals(input as VerifyRequest);
        }

        /// <summary>
        /// Returns true if VerifyRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of VerifyRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VerifyRequest input)
        {
            if (input == null)
                return false;

            return
                (
                    this.FirstTemplate == input.FirstTemplate ||
                    (this.FirstTemplate != null &&
                    this.FirstTemplate.Equals(input.FirstTemplate))
                ) &&
                (
                    this.SecondTemplate == input.SecondTemplate ||
                    (this.SecondTemplate != null &&
                    this.SecondTemplate.Equals(input.SecondTemplate))
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
                if (this.FirstTemplate != null)
                    hashCode = hashCode * 59 + this.FirstTemplate.GetHashCode();
                if (this.SecondTemplate != null)
                    hashCode = hashCode * 59 + this.SecondTemplate.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
