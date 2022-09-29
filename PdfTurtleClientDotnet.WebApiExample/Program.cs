using PdfTurtleClientDotnet;
using PdfTurtleClientDotnet.Models;

var builder = WebApplication.CreateBuilder(args);

// Register PdfTurtle Client for Dependency Injection
builder.Services.AddPdfTurtle("https://pdfturtle.gaitzsch.dev");

var app = builder.Build();

app.MapGet("/", 
    () => Results.Content(@"
        <!DOCTYPE html>
        <a href='/html-bundle-example'>   HTML bundle example   </a><br>
        <a href='/html-example'>          HTML example          </a><br>
        <a href='/html-template-example'> HTML template example </a><br>
        ", "text/html")
)
.WithName("Index");


app.MapGet("/html-bundle-example", async (IPdfTurtleClient pdfTurtleClient, CancellationToken cancellationToken) =>
{
    var bodyBundle = await File.ReadAllBytesAsync("pdf-turtle-bundle-body.zip");
    var headerBundle = await File.ReadAllBytesAsync("pdf-turtle-bundle-header.zip");

    var model = new {
        title = "PdfTurtle _🐢_ TestReport",
        heading = "Sales Overview",
        summery = new {
            totalSales = 32993,
            salesPerWeek = 82,
            performanceIndex = 5.132,
            salesVolume = 848932,
        }
    };

    var pdfStream = await pdfTurtleClient.RenderBundleAsync(
        new [] { bodyBundle, headerBundle },
        model,
        cancellationToken
    );

    return Results.File(pdfStream, "application/pdf");
})
.WithName("Render PDF - Bundle example (with header)");


app.MapGet("/html-example", async (IPdfTurtleClient pdfTurtleClient, CancellationToken cancellationToken) =>
{
    var renderData = new RenderData {
        Html="<h1>test</h1>",
    };

    var pdfStream = await pdfTurtleClient.RenderAsync(renderData, cancellationToken);

    return Results.File(pdfStream, "application/pdf");
})
.WithName("Render PDF - HTML example");


app.MapGet("/html-template-example", async (IPdfTurtleClient pdfTurtleClient, CancellationToken cancellationToken) =>
{
    var renderTemplateData = new RenderTemplateData {
        HeaderHtmlTemplate="<div><h1>Document with Title: {{.title}}</h1></div>",
        HtmlTemplate = "<h3>hi {{ .name }}</h3>",
        Model = new { title = "Hello World", name = "Timo" },
    };

    var pdfStream = await pdfTurtleClient.RenderTemplateAsync(renderTemplateData, cancellationToken);

    return Results.File(pdfStream, "application/pdf");
})
.WithName("Render PDF - HTML template example");

app.Run();