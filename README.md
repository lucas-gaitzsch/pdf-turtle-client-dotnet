# PdfTurtle Client .NET
.NET Standard 2.0 library to use the [PdfTurtle](https://github.com/lucas-gaitzsch/pdf-turtle) service 

**HINT:** This lib is in the beta.

## How to use - Recommended way for ASP.NET Core and bundles

See a working example in [PdfTurtleClientDotnet.WebApiExample/Program.cs](./PdfTurtleClientDotnet.WebApiExample/Program.cs).

### 1. Prepare project

Get the package from [nuget](https://www.nuget.org/packages/PdfTurtleClientDotnet).

```bash
dotnet add package PdfTurtleClientDotnet
```

```csharp
// register service
services.AddPdfTurtle("https://pdfturtle.gaitzsch.dev");
```

```csharp
// resolve service over dependency injection
app.MapGet("/", (IPdfTurtleClient pdfTurtleClient) => { ... });
```

### 2. Design your PDF in the playground
Go to [🐢PdfTurtle-Playground](https://pdfturtle.gaitzsch.dev/), put an example model as JSON and design your PDF.
Download the bundle as ZIP file and put it in your resources/assets.

### 3. Call the service with the client and your data
Call `RenderBundleAsync` to render the pdf to a `Stream`.

```csharp
var pdfStream = await pdfTurtleClient.RenderBundleAsync(BUNDLE_AS_STREAM_OR_BYTES, MODEL_AS_OBJECT);
```

**Done.**

### Hint: You can split your bundle
If you want to have the same header for all documents, you can create a ZIP file with with only the `header.html` file.
Now you can call the Service with multiple bundle files. The service will assemble the files together.

```csharp
var pdfStream = await pdfTurtleClient.RenderBundleAsync(
        new [] { BUNDLE_WITHOUT_HEADER_AS_STREAM_OR_BYTES, HEADER_BUNDLE_AS_STREAM_OR_BYTES },
        MODEL_AS_OBJECT
    );
```


## How to use - Alternative ways
### Without template (plain HTML)
If the described way does not match your expectations, you can use a template engine of your choice (for example [RazorLight](https://www.nuget.org/packages/RazorLight)) and render HTML directly with PdfTurtle.

```csharp
var pdfStream = await pdfTurtleClient.RenderAsync(new RenderData() {
    ...
});
```

### With template but no bundle
If you want to render a HTML template without any images or assets, you can use the `RenderTemplateAsync` function.

```csharp
var pdfStream = await pdfTurtleClient.RenderTemplateAsync(new RenderTemplateData() {
    ...
});
```


## Open TODOs
- [x] Working examples for all Methods
- [ ] Tests