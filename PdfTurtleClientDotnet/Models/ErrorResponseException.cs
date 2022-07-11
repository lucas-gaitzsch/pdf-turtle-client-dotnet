namespace PdfTurtleClientDotnet.Models;

public class ErrorResponseException : Exception {

    public ErrorResponseException(ErrorResponse? errorResponse)
        : base($"{errorResponse?.Msg ?? "-"}: {errorResponse?.Err ?? "-"}") {
        ErrorResponse = errorResponse;
    }

    public ErrorResponseException(ErrorResponse? errorResponse, Exception inner)
        : base($"{errorResponse?.Msg ?? "-"}: {errorResponse?.Err ?? "-"}", inner) {
        ErrorResponse = errorResponse;
    }

    public ErrorResponse? ErrorResponse { get; }
}
