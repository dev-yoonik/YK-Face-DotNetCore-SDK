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
    public class VerifyImagesRequest : IRequest, IEquatable<VerifyImagesRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyImages" /> class.
        /// </summary>
        /// <param name="firstImage">firstImage (required).</param>
        /// <param name="secondImage">secondImage (required).</param>
        public VerifyImagesRequest(string firstImage, string secondImage)
        {
            if (firstImage == null)
                throw new ArgumentNullException("The First Image is a required property for VerifyImagesRequest and cannot be null");
            if (secondImage == null)
                throw new ArgumentNullException("The Second Image is a required property for VerifyImagesRequest and cannot be null");
            
            this.FirstImage = firstImage;
            this.SecondImage = secondImage;
        }

        /// <summary>
        /// Gets or Sets FirstImage
        /// </summary>
        [DataMember(Name = "first_image", EmitDefaultValue = false)]
        public string FirstImage { get; set; }

        /// <summary>
        /// Gets or Sets SecondImage
        /// </summary>
        [DataMember(Name = "second_image", EmitDefaultValue = false)]
        public string SecondImage { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VerifyImagesRequest {\n");
            sb.Append("  FirstImage: ").Append(FirstImage).Append("\n");
            sb.Append("  SecondImage: ").Append(SecondImage).Append("\n");
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
            return this.Equals(input as VerifyImagesRequest);
        }

        /// <summary>
        /// Returns true if VerifyImages instances are equal
        /// </summary>
        /// <param name="input">Instance of VerifyImages to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VerifyImagesRequest input)
        {
            if (input == null)
                return false;

            return
                (
                    this.FirstImage == input.FirstImage ||
                    (this.FirstImage != null &&
                    this.FirstImage.Equals(input.FirstImage))
                ) &&
                (
                    this.SecondImage == input.SecondImage ||
                    (this.SecondImage != null &&
                    this.SecondImage.Equals(input.SecondImage))
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
                if (this.FirstImage != null)
                    hashCode = hashCode * 59 + this.FirstImage.GetHashCode();
                if (this.SecondImage != null)
                    hashCode = hashCode * 59 + this.SecondImage.GetHashCode();
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
