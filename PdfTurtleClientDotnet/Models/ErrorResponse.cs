namespace PdfTurtleClientDotnet.Models;

public class ErrorResponse {
    public string Msg { get; set; } = string.Empty;

    public string Err { get; set; } = string.Empty;

    public string RequestId { get; set; } = string.Empty;
}
