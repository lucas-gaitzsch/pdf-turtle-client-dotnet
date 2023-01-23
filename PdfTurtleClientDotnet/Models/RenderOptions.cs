using System.Text.Json.Serialization;

namespace PdfTurtleClientDotnet.Models;

public class RenderOptions {
    
    [JsonPropertyName("excludeBuiltinStyles")]
    public bool? ExcludeBuiltinStyles { get; set; } = false;

    [JsonPropertyName("landscape")]
    public bool? Landscape { get; set; } = false;

    /// <summary>margins in mm; fallback to default if null</summary>
    public RenderOptionsMargins Margins { get; set; } = new();

    [JsonPropertyName("pageFormat")]
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public RenderOptionsPageFormat? PageFormat { get; set; } = RenderOptionsPageFormat.A4;

    [JsonPropertyName("pageSize")]
    /// <summary>page size in mm; overrides page format</summary>
    public PageSize? PageSize { get; set; } = null;


}
