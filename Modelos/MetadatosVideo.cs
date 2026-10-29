namespace DVM;
using System.Text.Json.Serialization;
public class MetadatosVideo
{
    [JsonPropertyName("title")]
    public string titulo {get;set;}
    [JsonPropertyName("duration")]
    public double? duracion {get;set;}
    [JsonPropertyName("uploader")]
    public string canal {get;set;}
    [JsonPropertyName("webpage_url")]
    public string url {get;set;}
    [JsonPropertyName("formats")]
    public List<FormatoVideo> formatos { get; set; } = new();
   
}
