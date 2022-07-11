using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using PdfTurtleClientDotnet.Models;

namespace PdfTurtleClientDotnet;

public class PdfTurtleClient : IPdfTurtleClient {
    private readonly HttpClient httpClient;
    private readonly ILogger logger;
    private readonly Lazy<JsonSerializerOptions> jsonSerializerOptions = new(() => {
        var jsonSerializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        };

        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        return jsonSerializerOptions;
    });

    public PdfTurtleClient(HttpClient httpClient, ILogger<PdfTurtleClient> logger) {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    public Task<Stream> RenderAsync(RenderData renderData, CancellationToken cancellationToken = default) 
        => this.RenderGeneric("pdf/from/html/render", renderData, cancellationToken);

    public Task<Stream> RenderTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default)
        => this.RenderGeneric("/pdf/from/html-template/render", renderData, cancellationToken);

    public async Task<TemplateTestResultResponse> TestTemplateAsync(RenderTemplateData renderData, CancellationToken cancellationToken = default) {
        using var ms = new MemoryStream();

        await JsonSerializer.SerializeAsync<RenderTemplateData>(ms, renderData, this.jsonSerializerOptions.Value, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);

        using var content = new StreamContent(ms);

        var response = await httpClient.PostAsync("/pdf/from/html-template/test", content, cancellationToken);

        if (response.IsSuccessStatusCode) {
            return JsonSerializer.Deserialize<TemplateTestResultResponse>(await response.Content.ReadAsStringAsync(cancellationToken), this.jsonSerializerOptions.Value) ?? new();
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
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        } else {
            var err = await this.DeserializeErrResponse(response, cancellationToken);
            throw new ErrorResponseException(err);
        }
    }

    private async Task<ErrorResponse?> DeserializeErrResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        => JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(cancellationToken), this.jsonSerializerOptions.Value);
}
