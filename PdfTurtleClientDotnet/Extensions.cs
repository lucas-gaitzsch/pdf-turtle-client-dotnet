using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace PdfTurtleClientDotnet;

public static class Extensions {

    public static IServiceCollection AddPdfTurtle(this IServiceCollection serviceCollection, Uri pdfTurtleBaseUrl, string? token = null) {
        
        serviceCollection.AddHttpClient<IPdfTurtleClient, PdfTurtleClient>(client => {
            client.BaseAddress = new (pdfTurtleBaseUrl, "/api/");
            
            if (token != null) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        });

        return serviceCollection;
    }


    public static IServiceCollection AddPdfTurtle(this IServiceCollection serviceCollection, IConfiguration configuration, string sectionKey = "PdfTurtle") {
        
        serviceCollection.Configure<PdfTurtleConfig>(configuration.GetSection(sectionKey));

        serviceCollection.AddHttpClient<IPdfTurtleClient, PdfTurtleClient>((sp, client) => {
            var config = sp.GetRequiredService<IOptions<PdfTurtleConfig>>();

            client.BaseAddress = new (new Uri(config.Value.Endpoint), "/api/");
            
            if (config.Value.Token != null) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.Value.Token);
            }
        });

        return serviceCollection;
    }

    public static IServiceCollection AddPdfTurtle(this IServiceCollection serviceCollection, string pdfTurtleBaseUrl = "http://localhost:8000")
        => serviceCollection.AddPdfTurtle(new Uri(pdfTurtleBaseUrl));

}