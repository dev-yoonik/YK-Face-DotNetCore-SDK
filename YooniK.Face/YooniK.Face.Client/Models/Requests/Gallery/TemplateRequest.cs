using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using YooniK.Services.Client.Common;

namespace YooniK.Face.Client.Models.Requests
{
    /// <summary>
    /// TemplateRequest
    /// </summary>
    [DataContract]
    public partial class TemplateRequest : IRequest, IEquatable<TemplateRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateRequest" /> class.
        /// </summary>
        /// <param name="template">template.</param>
        public TemplateRequest(string template = default(string))
        {
            this.Template = template;
        }

        /// <summary>
        /// Gets or Sets Template
        /// </summary>
        [DataMember(Name = "template", EmitDefaultValue = false)]
        public string Template { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TemplateRequest {\n");
            sb.Append("  Template: ").Append(Template).Append("\n");
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
            return this.Equals(input as TemplateRequest);
        }

        /// <summary>
        /// Returns true if TemplateRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of TemplateRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TemplateRequest input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Template == input.Template ||
                    (this.Template != null &&
                    this.Template.Equals(input.Template))
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
                if (this.Template != null)
                    hashCode = hashCode * 59 + this.Template.GetHashCode();
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
