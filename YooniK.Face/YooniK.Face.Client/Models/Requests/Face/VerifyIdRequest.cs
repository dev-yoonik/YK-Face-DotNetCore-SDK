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
    public class VerifyIdRequest : IRequest, IEquatable<VerifyIdRequest>, IValidatableObject
    {
        [DataMember(Name = "template")]
        public string Template { get; set; } = string.Empty;
        [DataMember(Name = "template_id")]
        public string TemplateId { get; set; } = string.Empty;
        [DataMember(Name = "gallery_id")]
        public string GalleryId { get; set; } = string.Empty;

        public VerifyIdRequest(string template, string templateId, string galleryId)
        {
            if (template == null)
                throw new ArgumentNullException("Template is a required property for VerifyIdRequest and cannot be null");
            if (templateId == null)
                throw new ArgumentNullException("TemplateId is a required property for VerifyIdRequest and cannot be null");

            Template = template;
            TemplateId = templateId;
            GalleryId = galleryId;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VerifyIdRequest {\n");
            sb.Append("  Template: ").Append(Template).Append("\n");
            sb.Append("  TemplateId: ").Append(TemplateId).Append("\n");
            sb.Append("  GalleryId: ").Append(GalleryId).Append("\n");
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
            return this.Equals(input as VerifyIdRequest);
        }

        /// <summary>
        /// Returns true if VerifyIdRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of VerifyIdRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VerifyIdRequest input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Template == input.Template ||
                    (this.Template != null &&
                    this.Template.Equals(input.Template))
                ) &&
                (
                    this.TemplateId == input.TemplateId ||
                    (this.TemplateId != null &&
                    this.TemplateId.Equals(input.TemplateId))
                ) &&
                (
                    this.GalleryId == input.GalleryId ||
                    (this.GalleryId != null &&
                    this.GalleryId.Equals(input.GalleryId))
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
                if (this.TemplateId != null)
                    hashCode = hashCode * 59 + this.TemplateId.GetHashCode();
                if (this.GalleryId != null)
                    hashCode = hashCode * 59 + this.GalleryId.GetHashCode();
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
