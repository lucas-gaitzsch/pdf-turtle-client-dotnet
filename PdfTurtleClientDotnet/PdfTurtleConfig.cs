namespace PdfTurtleClientDotnet;

public sealed class PdfTurtleConfig {
    public string? Endpoint { get; set; }

    /// <summary>
    /// Used as bearer token (optional)
    /// </summary>
    public string? Secret { get; set; }
}
