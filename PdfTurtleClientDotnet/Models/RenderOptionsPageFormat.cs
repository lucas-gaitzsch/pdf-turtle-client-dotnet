using System.Runtime.Serialization;

namespace PdfTurtleClientDotnet.Models;

public enum RenderOptionsPageFormat {
    [EnumMember(Value = @"A0")]
    A0 = 0,

    [EnumMember(Value = @"A1")]
    A1 = 1,

    [EnumMember(Value = @"A2")]
    A2 = 2,

    [EnumMember(Value = @"A3")]
    A3 = 3,

    [EnumMember(Value = @"A4")]
    A4 = 4,

    [EnumMember(Value = @"A5")]
    A5 = 5,

    [EnumMember(Value = @"A6")]
    A6 = 6,

    [EnumMember(Value = @"Letter")]
    Letter = 7,

    [EnumMember(Value = @"Legal")]
    Legal = 8,

}
