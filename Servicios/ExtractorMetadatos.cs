namespace DVM;
using ManuHub.Ytdlp.NET;
using Microsoft.VisualBasic;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

public class ExtractorMetadatos
{
    public async Task<MetadatosVideo> ObtenerAcyncVideo(string URL)
    {
        try
        {
            BarraDeProgreso Barra = new BarraDeProgreso();

            string ruta = Path.Combine( // Instanciando direccion de la extension 
            AppContext.BaseDirectory,
            "tools",
             "yt-dlp"
            );

            System.Console.WriteLine("[---] Extrayendo metadatos del video...");

            // 1. Capture Mode – Extract full metadata as JSON
            var ytdlp = new Ytdlp(ruta);
            var res = ytdlp.ExecuteRawAsync(
                     "--print \"{\\\"titulo\\\": %(title)j, " +
                      "\\\"duracion\\\": %(duration)j, " +
                      "\\\"canal\\\": %(channel)j, " +
                      "\\\"url\\\": %(webpage_url)j, " +
                      "\\\"formatos\\\": %(formats.:.{ext})j, " +
                       "\\\"calidades\\\": %(formats.:.{height})j}\" " +
                      URL
            );

            Task animar = Barra.AnimarCarga(res);


            await Task.WhenAll(res, animar);
            Console.Clear();

            MetadatosVideo metadatovideo =
    JsonSerializer.Deserialize<MetadatosVideo>(
        res.Result.FullOutput,
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }
    ) ?? throw new Exception("No se pudieron obtener los metadatos.");

            Console.WriteLine($"Calidades: {metadatovideo?.calidades.Count}");
            Console.WriteLine($"Formatos: {metadatovideo?.formatos.Count}");
            Console.WriteLine(res.Result.FullOutput);
            System.Console.ReadKey();
            return metadatovideo;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (ExtraerMetadatos): {e.Message}");
        }
        return null;
    }

    public List<int> ExtraerCalidadesVideo(MetadatosVideo metadatosVideo)
    {
        try
        {
            return metadatosVideo.calidades.Select(x => x.height).Distinct().OrderBy(x => x).ToList();

        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (ExtraerCalidadesVideo: {e.Message})");
        }
        return null;
    }

    public List<string> ExtraerFormatosVideo(MetadatosVideo metadatosVideo)
    {
        try
        {
            return metadatosVideo.formatos.Select(x => x.ext).Distinct().ToList();
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (ExtraerCalidadesVideo: {e.Message})");
        }
        return null;
    }

}
