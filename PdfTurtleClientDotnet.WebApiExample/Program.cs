using PdfTurtleClientDotnet;

var builder = WebApplication.CreateBuilder(args);

// Register PdfTurtle Client for Dependency Injection
builder.Services.AddPdfTurtle("https://pdfturtle.gaitzsch.dev");

var app = builder.Build();

app.MapGet("/", async (IPdfTurtleClient pdfTurtleClient, CancellationToken cancellationToken) =>
{
    // Results.File(await pdfTurtleClient.RenderTemplateAsync(new(), cancellationToken));
    return Results.File(await pdfTurtleClient.RenderAsync(new PdfTurtleClientDotnet.Models.RenderData{
        Html="<h1>test</h1>"
    }, cancellationToken), "application/pdf");
})
.WithName("Sample Pdf");

app.Run();