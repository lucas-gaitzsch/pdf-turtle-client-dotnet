namespace PdfTurtleClientDotnet.Models;

public enum RenderTemplateDataTemplateEngine {
    [System.Runtime.Serialization.EnumMember(Value = @"golang")]
    Golang = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"handlebars")]
    Handlebars = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"django")]
    Django = 2,

}