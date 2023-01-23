using System.Text.Json.Serialization;

namespace PdfTurtleClientDotnet.Models;

public class RenderOptionsMargins {
    /// <summary>margin bottom in mm</summary>
    [JsonPropertyName("bottom")]
    public int? Bottom { get; set; } = 20;

    /// <summary>margin left in mm</summary>
    [JsonPropertyName("left")]
    public int? Left { get; set; } = 25;

    /// <summary>margin right in mm</summary>
    [JsonPropertyName("right")]
    public int? Right { get; set; } = 25;

    /// <summary>margin top in mm</summary>
    [JsonPropertyName("top")]
    public int? Top { get; set; } = 25;


}
