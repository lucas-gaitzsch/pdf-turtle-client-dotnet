using System.Text.Json.Serialization;

namespace PdfTurtleClientDotnet.Models;

public class RenderTemplateData {
    /// <summary>Optional template for footer. If empty, the footer template will be parsed from main template (&lt;PdfFooter&gt;&lt;/PdfFooter&gt;).</summary>
    [JsonPropertyName("footerHtmlTemplate")]
    public string FooterHtmlTemplate { get; set; } = string.Empty;

    /// <summary>Optional model for footer. If empty or null model was used.</summary>
    [JsonPropertyName("footerModel")]
    public object? FooterModel { get; set; } = null;

    /// <summary>Optional template for header. If empty, the header template will be parsed from main template (&lt;PdfHeader&gt;&lt;/PdfHeader&gt;).</summary>
    [JsonPropertyName("headerHtmlTemplate")]
    public string HeaderHtmlTemplate { get; set; } = string.Empty;

    /// <summary>Optional model for header. If empty or null model was used.</summary>
    [JsonPropertyName("headerModel")]
    public object? HeaderModel { get; set; } = null;

    [JsonPropertyName("htmlTemplate")]
    public string HtmlTemplate { get; set; } = string.Empty;

    /// <summary>// Model with your data matching to the templates</summary>
    [JsonPropertyName("model")]
    public object? Model { get; set; } = null;

    [JsonPropertyName("options")]
    public RenderOptions Options { get; set; } = new();

    [JsonConverter(typeof(JsonStringEnumConverter))] 
    [JsonPropertyName("templateEngine")]
    public RenderTemplateDataTemplateEngine? TemplateEngine { get; set; } = RenderTemplateDataTemplateEngine.Golang;

}
