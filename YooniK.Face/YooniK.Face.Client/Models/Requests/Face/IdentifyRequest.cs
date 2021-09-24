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
    public class IdentifyRequest : IRequest, IEquatable<IdentifyRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifyRequest" /> class.
        /// </summary>
        /// <param name="template">template (required).</param>
        /// <param name="candidateListLength">candidateListLength.</param>
        /// <param name="minimumScore">minimumScore (default to -1.0).</param>
        /// <param name="galleryId">galleryId.</param>
        public IdentifyRequest(string template, string galleryId, int candidateListLength = 1, double minimumScore = -1.0)
        {
            if (template == null)
                throw new ArgumentNullException("Template is a required property for IdentifyRequest and cannot be null");
            if (candidateListLength < 1)
                throw new ArgumentOutOfRangeException("CandidateListLength in IdentifyRequest must be higher than 1");
            if (minimumScore < -1.0)
                throw new ArgumentOutOfRangeException("MinimumScore in IdentifyRequest must be higher than 1");

            Template = template;
            CandidateListLength = candidateListLength;
            MinimumScore = minimumScore;
            GalleryId = galleryId;
        }

        /// <summary>
        /// Gets or Sets Template
        /// </summary>
        [DataMember(Name = "template")]
        public string Template { get; set; }

        /// <summary>
        /// Gets or Sets CandidateListLength
        /// </summary>
        [DataMember(Name = "candidate_list_length")]
        public int CandidateListLength { get; set; }

        /// <summary>
        /// Gets or Sets MinimumScore
        /// </summary>
        [DataMember(Name = "minimum_score")]
        public double MinimumScore { get; set; }

        /// <summary>
        /// Gets or Sets GalleryId
        /// </summary>
        [DataMember(Name = "gallery_id")]
        public string GalleryId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class IdentifyRequest {\n");
            sb.Append("  Template: ").Append(Template).Append("\n");
            sb.Append("  CandidateListLength: ").Append(CandidateListLength).Append("\n");
            sb.Append("  MinimumScore: ").Append(MinimumScore).Append("\n");
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
            return this.Equals(input as IdentifyRequest);
        }

        /// <summary>
        /// Returns true if IdentifyRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of IdentifyRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(IdentifyRequest input)
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
                    this.CandidateListLength == input.CandidateListLength ||
                    (this.CandidateListLength.Equals(input.CandidateListLength))
                ) &&
                (
                    this.MinimumScore == input.MinimumScore ||
                    (this.MinimumScore.Equals(input.MinimumScore))
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
                hashCode = hashCode * 59 + this.CandidateListLength.GetHashCode();
                hashCode = hashCode * 59 + this.MinimumScore.GetHashCode();
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
            // CandidateListLength (int?) minimum
            if (this.CandidateListLength < (int?)1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for CandidateListLength, must be a value greater than or equal to 1.", new[] { "CandidateListLength" });
            }

            // MinimumScore (double?) minimum
            if (this.MinimumScore < (double?)-1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for MinimumScore, must be a value greater than or equal to -1.", new[] { "MinimumScore" });
            }

            yield break;
        }
    }
}
