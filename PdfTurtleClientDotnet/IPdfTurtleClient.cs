using PdfTurtleClientDotnet.Models;

namespace PdfTurtleClientDotnet;

public interface IPdfTurtleClient {
    Task<Stream> RenderAsync(RenderData renderData, CancellationToken cancellationToken = default);
    
    Task<Stream> RenderTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default);

    Task<TemplateTestResultResponse> TestTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default);
}
