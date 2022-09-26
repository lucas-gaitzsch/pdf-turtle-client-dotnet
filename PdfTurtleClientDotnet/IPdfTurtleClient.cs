using PdfTurtleClientDotnet.Models;

namespace PdfTurtleClientDotnet;

public interface IPdfTurtleClient {
    Task<Stream> RenderAsync(RenderData renderData, CancellationToken cancellationToken = default);
    
    Task<Stream> RenderTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default);

    Task<TemplateTestResultResponse> TestTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default);
    
    Task<Stream> RenderBundleAsync(Stream bundleStream, object? model = null, CancellationToken cancellationToken = default);
    Task<Stream> RenderBundleAsync(byte[] bundleByteArray, object? model = null, CancellationToken cancellationToken = default);
}
