using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PdfTurtleClientDotnet;

public static class Extensions {

    public static IServiceCollection AddPdfTurtle(this IServiceCollection serviceCollection, Uri pdfTurtleBaseUrl) {
        
        serviceCollection.AddHttpClient<IPdfTurtleClient, PdfTurtleClient>(client => {
            client.BaseAddress = new (pdfTurtleBaseUrl, "/api/");
        });

        return serviceCollection;
    }

    public static IServiceCollection AddPdfTurtle(this IServiceCollection serviceCollection, IConfiguration configuration) {
        
        var config = new Config();
        configuration.Bind("PdfTurtle", config);

        return serviceCollection.AddPdfTurtle(new Uri(config.BaseUrl));
    }

    public static IServiceCollection AddPdfTurtle(this IServiceCollection serviceCollection, string pdfTurtleBaseUrl = "http://localhost:8000")
        => serviceCollection.AddPdfTurtle(new Uri(pdfTurtleBaseUrl));

}