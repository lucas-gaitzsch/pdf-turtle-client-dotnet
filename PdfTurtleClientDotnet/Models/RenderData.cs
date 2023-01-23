using System.Text.Json.Serialization;

namespace PdfTurtleClientDotnet.Models;

public class RenderData {
    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    /// <summary>Optional html for header. If empty, the header html will be parsed from main html (&lt;PdfHeader&gt;&lt;/PdfHeader&gt;).</summary>
    [JsonPropertyName("headerHtml")]
    public string HeaderHtml { get; set; } = string.Empty;

    /// <summary>Optional html for footer. If empty, the footer html will be parsed from main html (&lt;PdfFooter&gt;&lt;/PdfFooter&gt;).</summary>
    [JsonPropertyName("footerHtml")]
    public string FooterHtml { get; set; } = string.Empty;

    [JsonPropertyName("options")]
    public RenderOptions Options { get; set; } = new();

}
