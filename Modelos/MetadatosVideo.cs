namespace DVM;
using System.Text.Json.Serialization;
public class MetadatosVideo
{
    [JsonPropertyName("titulo")]
    public string titulo {get;set;}
    [JsonPropertyName("duracion")]
    public int duracion {get;set;}
    [JsonPropertyName("canal")]
    public string canal {get;set;}
    [JsonPropertyName("url")]
    public string url {get;set;}
    [JsonPropertyName("formatos")]
    public List<FormatoVideo> formatos { get; set; } = new();
    [JsonPropertyName("calidades")]
    public List<CalidadVideo> calidades { get; set; } = new();
}
