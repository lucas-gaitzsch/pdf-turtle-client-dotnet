namespace PdfTurtleClientDotnet.Models;

public class RenderData {
    public string Html { get; set; } = string.Empty;

    /// <summary>Optional html for header. If empty, the header html will be parsed from main html (&lt;PdfHeader&gt;&lt;/PdfHeader&gt;).</summary>
    public string HeaderHtml { get; set; } = string.Empty;

    /// <summary>Optional html for footer. If empty, the footer html will be parsed from main html (&lt;PdfFooter&gt;&lt;/PdfFooter&gt;).</summary>
    public string FooterHtml { get; set; } = string.Empty;

    public RenderOptions Options { get; set; } = new();

}
