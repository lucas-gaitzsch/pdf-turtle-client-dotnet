using System.Text.Json.Serialization;

namespace PdfTurtleClientDotnet.Models;

public class PageSize {
    /// <summary>in mm</summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; }

    /// <summary>in mm</summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }
}
