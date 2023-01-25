using System;

namespace PdfTurtleClientDotnet.Models;

/// <summary>
/// Implementations: BundleFormDataByteArray & BundleFormDataStream
/// </summary>
public interface IBundleFormData : IDisposable {
    string FileName { get; }
    Stream Stream { get; } 
}

public class BundleFormDataByteArray : IBundleFormData
{
    public string FileName { get; set; }

    public byte[] ByteArray { get; }

    public Stream Stream => lazyStream.Value;

    private Lazy<Stream> lazyStream;

    public BundleFormDataByteArray(string fileName, byte[] byteArray) {
        FileName = fileName;
        ByteArray = byteArray;

        initLazyStream();
    }

    private void initLazyStream() {
        lazyStream = new Lazy<Stream>(() => new MemoryStream(ByteArray));
    }

    public void Dispose()
    {
        Stream.Dispose();
        initLazyStream();
    }
}

public class BundleFormDataStream : IBundleFormData
{
   public string FileName { get; set; }

    public Stream Stream { get; set; }

    public BundleFormDataStream(string fileName, Stream stream) {
        FileName = fileName;
        Stream = stream;
    }

    public void Dispose()
    {}
}