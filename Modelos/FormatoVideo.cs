namespace DVM;
using System.Text.Json.Serialization;

public class FormatoVideo
{
    [JsonPropertyName("ext")]
    public string ext {get;set;} = string.Empty;
    [JsonPropertyName("height")]
    public int? height {get;set;}
}
