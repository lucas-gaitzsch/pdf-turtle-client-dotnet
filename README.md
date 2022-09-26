# PdfTurtleClientDotnet
.NET Standard 2.0 library to use the [pdf-turtle](https://github.com/lucas-gaitzsch/pdf-turtle) service 

**HINT:** This lib is in the beta.

## How to use
Get the package from [nuget](https://www.nuget.org/packages/PdfTurtleClientDotnet).

```bash
dotnet add package PdfTurtleClientDotnet
```

```csharp
...
// register service
services.AddPdfTurtle("https://pdfturtle.gaitzsch.dev");
...
```

```csharp
...
// resolve service over dependency injection
app.MapGet("/", (IPdfTurtleClient pdfTurtleClient) =>
{ ... }
...
```

For more information: see an example in [Program.cs](./PdfTurtleClientDotnet.WebApiExample/Program.cs)
