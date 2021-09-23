using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YooniK.Face.Client.Models.Responses
{
    /// <summary>
    /// ProcessResponse
    /// </summary>
    [DataContract]
    public partial class ProcessResponse
    {

        [DataMember(Name = "biometric_type", EmitDefaultValue = false)]
        public string BiometricType { get; set; }

        [DataMember(Name = "x", EmitDefaultValue = false)]
        public double X { get; set; }  //  detection center coordinate x
        
        [DataMember(Name = "Y", EmitDefaultValue = false)]
        public double Y { get; set; }  //  detection center coordinate y
        
        [DataMember(Name = "width", EmitDefaultValue = false)]
        public double Width { get; set; }  //  detection bounding box width
        
        [DataMember(Name = "height", EmitDefaultValue = false)]
        public double Height { get; set; }  //  detection bounding box height
        
        [DataMember(Name = "Z", EmitDefaultValue = false)]
        public double? Z { get; set; }  //  detection center 3D coordinate Z
        
        [DataMember(Name = "matching_score", EmitDefaultValue = false)]
        public double? MatchingScore { get; set; }  //  Matching score obtained with template_id person
#nullable enable
        [DataMember(Name = "template", EmitDefaultValue = false)]
        public string? Template { get; set; }  //  Biometric template 

        [DataMember(Name = "template_version", EmitDefaultValue = false)]
        public string? TemplateVersion { get; set; }  //  Template version
        
        [DataMember(Name = "matching_image", EmitDefaultValue = false)]
        public string? MatchingImage { get; set; }  //  Thumbnail/crop used for template extraction
        
        [DataMember(Name = "tracking_id", EmitDefaultValue = false)]
        public string? TrackingId { get; set; }  //  Tracking id.Available when processing video.
        
        [DataMember(Name = "template_id", EmitDefaultValue = false)]
        public string? TemplateId { get; set; }  //  Template Id
        
        [DataMember(Name = "quality_metrics", EmitDefaultValue = false)]
        public List<QualityMetrics>? QualityMetrics { get; set; }
        
        [DataMember(Name = "biometric_points", EmitDefaultValue = false)]
        public List<BiometricPoints>? BiometricPoints { get; set; }
#nullable disable
    }

    [DataContract]
    public partial class QualityMetrics
    {
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public double value { get; set; }  //  Metric value.
        [DataMember(Name = "@enum", EmitDefaultValue = false)]
#nullable enable
        public string? Enum { get; set; }  //  String with metric value for enumerables.
        [DataMember(Name = "bottom_threshold", EmitDefaultValue = false)]
#nullable disable
        public double? BottomThreshold { get; set; }  //  Bottom threshold.
        [DataMember(Name = "top_threshold", EmitDefaultValue = false)]
        public double? TopThreshold { get; set; }  //  Top threshold.
        [DataMember(Name = "test", EmitDefaultValue = false)]
        public bool Test { get; set; }  //  Metric test according to threshold.
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }  //  Metric name.
    }

    [DataContract]
    public partial class BiometricPoints
    {
        [DataMember(Name = "x")]
        public int X { get; set; }  //  Point x coordinate.
        [DataMember(Name = "Y")]
        public int Y { get; set; }  //  Point y coordinate.
        [DataMember(Name = "name")]
        public string Name { get; set; }  //  Point coordinate name.
    }
}
