using System.Text.Json.Serialization;

namespace PdfTurtleClientDotnet.Models;

public class RenderTemplateData {
    /// <summary>Optional template for footer. If empty, the footer template will be parsed from main template (&lt;PdfFooter&gt;&lt;/PdfFooter&gt;).</summary>
    public string FooterHtmlTemplate { get; set; } = string.Empty;

    /// <summary>Optional model for footer. If empty or null model was used.</summary>
    public object? FooterModel { get; set; } = null;

    /// <summary>Optional template for header. If empty, the header template will be parsed from main template (&lt;PdfHeader&gt;&lt;/PdfHeader&gt;).</summary>
    public string HeaderHtmlTemplate { get; set; } = string.Empty;

    /// <summary>Optional model for header. If empty or null model was used.</summary>
    public object? HeaderModel { get; set; } = null;

    public string HtmlTemplate { get; set; } = string.Empty;

    /// <summary>// Model with your data matching to the templates</summary>
    public object? Model { get; set; } = null;

    public RenderOptions Options { get; set; } = new();

    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public RenderTemplateDataTemplateEngine? TemplateEngine { get; set; } = RenderTemplateDataTemplateEngine.Golang;

}
