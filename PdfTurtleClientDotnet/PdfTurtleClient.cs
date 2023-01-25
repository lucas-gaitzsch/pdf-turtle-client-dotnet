using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using PdfTurtleClientDotnet.Models;

namespace PdfTurtleClientDotnet;

public class PdfTurtleClient : IPdfTurtleClient {
    private readonly HttpClient httpClient;
    private readonly ILogger logger;

    private readonly JsonSerializerOptions? externJsonSerializerOptions;

    private Lazy<JsonSerializerOptions> jsonSerializerOptions;

    private Lazy<JsonSerializerOptions> jsonSerializerOptionsPdfTurtleResponse = new(() => {
        var ops = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        ops.Converters.Add(new JsonStringEnumConverter());

        return ops;
    });

    public PdfTurtleClient(HttpClient httpClient, ILogger<PdfTurtleClient> logger, JsonSerializerOptions? jsonSerializerOptions = null) {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        this.externJsonSerializerOptions = jsonSerializerOptions;

        this.jsonSerializerOptions = new(() => {
        
            if (this.externJsonSerializerOptions != null) {
                return externJsonSerializerOptions;
            }

            var ops = new JsonSerializerOptions {
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            return ops;
        });
    }

    public Task<Stream> RenderAsync(RenderData renderData, CancellationToken cancellationToken = default) 
        => this.RenderGeneric("/api/pdf/from/html/render", renderData, cancellationToken);

    public Task<Stream> RenderTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default)
        => this.RenderGeneric("/api/pdf/from/html-template/render", renderData, cancellationToken);

    public async Task<TemplateTestResultResponse> TestTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default) {
        using var ms = new MemoryStream();

        await JsonSerializer.SerializeAsync<RenderTemplateData>(ms, renderData, this.jsonSerializerOptions.Value, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);

        using var content = new StreamContent(ms);

        var response = await httpClient.PostAsync("/api/pdf/from/html-template/test", content, cancellationToken);

        if (response.IsSuccessStatusCode) {
            return JsonSerializer.Deserialize<TemplateTestResultResponse>(await response.Content.ReadAsStringAsync(), this.jsonSerializerOptionsPdfTurtleResponse.Value) ?? new();
        } else {
            var err = await this.DeserializeErrResponse(response, cancellationToken);
            throw new ErrorResponseException(err);
        }
    }

    private async Task<Stream> RenderGeneric<Data>(string requestPath, Data renderData, CancellationToken cancellationToken) {
        using var ms = new MemoryStream();

        await JsonSerializer.SerializeAsync<Data>(ms, renderData, this.jsonSerializerOptions.Value, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);

        using var content = new StreamContent(ms);

        var response = await httpClient.PostAsync(requestPath, content, cancellationToken);

        if (response.IsSuccessStatusCode) {
            return await response.Content.ReadAsStreamAsync();
        } else {
            var err = await this.DeserializeErrResponse(response, cancellationToken);
            throw new ErrorResponseException(err);
        }
    }

    private async Task<ErrorResponse?> DeserializeErrResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        => JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), this.jsonSerializerOptionsPdfTurtleResponse.Value);

    public async Task<Stream> RenderBundleAsync(IReadOnlyCollection<IBundleFormData> bundleData, object? model = null, CancellationToken cancellationToken = default) {
        
        using var formData = new MultipartFormDataContent();
        
        var bundleStreamContentsToDispose = new List<StreamContent>(bundleData.Count);
        
        foreach (var b in bundleData)
        {
            var sc = new StreamContent(b.Stream);
            bundleStreamContentsToDispose.Add(sc);

            formData.Add(sc, "bundle", b.FileName);
        }

        using var msModel = new MemoryStream();
        StreamContent? modelData = null;
        if (model != null) {
            await JsonSerializer.SerializeAsync(msModel, model, this.jsonSerializerOptions.Value, cancellationToken);
            msModel.Seek(0, SeekOrigin.Begin);

            modelData = new StreamContent(msModel);

            formData.Add(modelData, "model");
        }

        var response = await httpClient.PostAsync("/api/pdf/from/html-bundle/render", formData, cancellationToken);

        // cleanup
        bundleStreamContentsToDispose.ForEach(b => b.Dispose());
        bundleData.ToList().ForEach(s => s.Dispose());
        modelData?.Dispose();

        if (response.IsSuccessStatusCode) {
            return await response.Content.ReadAsStreamAsync();
        } else {
            var err = await this.DeserializeErrResponse(response, cancellationToken);
            throw new ErrorResponseException(err);
        }
    }


    public Task<Stream> RenderBundleAsync(IReadOnlyCollection<Stream> zipBundleStreams, object? model = null, CancellationToken cancellationToken = default) {
        var bundleFormData = zipBundleStreams.Select(stream => new BundleFormDataStream(fileName: GetBundleZipFileName(), stream)).ToList();
        
        return RenderBundleAsync(bundleFormData, model);
    }

    public Task<Stream> RenderBundleAsync(IReadOnlyCollection<byte[]> zipBundleByteArray, object? model = null, CancellationToken cancellationToken = default) {
        var bundleFormData = zipBundleByteArray.Select(byteArray => new BundleFormDataByteArray(fileName: GetBundleZipFileName(), byteArray)).ToList();
        
        return RenderBundleAsync(bundleFormData, model);
    }

    private string GetBundleZipFileName() => $"bundle-{Guid.NewGuid()}.zip";
}
