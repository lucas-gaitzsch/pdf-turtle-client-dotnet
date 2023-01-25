using PdfTurtleClientDotnet.Models;

namespace PdfTurtleClientDotnet;

public interface IPdfTurtleClient {
    /// <summary>
    /// Returns PDF file generated from HTML of body, header and footer
    /// </summary>
    /// <param name="renderData">RenderData object</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>PDF file as Stream</returns>
    Task<Stream> RenderAsync(RenderData renderData, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns PDF file generated from HTML template plus model of body, header and footer
    /// </summary>
    /// <param name="renderTemplateData">RenderTemplateData object</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<Stream> RenderTemplateAsync(RenderTemplateData renderTemplateData, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about matching model data to template
    /// </summary>
    /// <param name="renderTemplateData">RenderTemplateData object</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<TemplateTestResultResponse> TestTemplateAsync(RenderTemplateData renderTemplateData, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns PDF file generated from bundle (Zip-File) of HTML or HTML template of body, header, footer and assets. The index.html file in the Zip-Bundle is required
    /// </summary>
    /// <param name="bundleStreams">Input of bundles</param>
    /// <param name="model">Model with your data matching to the templates</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<Stream> RenderBundleAsync(IReadOnlyCollection<Stream> bundleStreams, object? model = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns PDF file generated from bundle (Zip-File) of HTML or HTML template of body, header, footer and assets. The index.html file in the Zip-Bundle is required
    /// </summary>
    /// <param name="bundleByteArrays">Input of bundles</param>
    /// <param name="model">Model with your data matching to the templates</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<Stream> RenderBundleAsync(IReadOnlyCollection<byte[]> bundleByteArrays, object? model = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns PDF file generated from bundle (Zip-File) of HTML or HTML template of body, header, footer and assets. The index.html file in the Zip-Bundle is required
    /// </summary>
    /// <param name="bundleData">Input of bundles or files with filenames</param>
    /// <param name="model">Model with your data matching to the templates</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<Stream> RenderBundleAsync(IReadOnlyCollection<IBundleFormData> bundleData, object? model = null, CancellationToken cancellationToken = default);
}
