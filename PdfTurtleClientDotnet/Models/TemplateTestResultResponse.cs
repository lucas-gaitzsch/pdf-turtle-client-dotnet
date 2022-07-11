namespace PdfTurtleClientDotnet.Models;

public class TemplateTestResultResponse {
    public string BodyTemplateError { get; set; } = string.Empty;

    public string FooterTemplateError { get; set; } = string.Empty;

    public string HeaderTemplateError { get; set; } = string.Empty;

    public bool IsValid { get; set; }
}
