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
            );  // NO BORRAR REUTILIZABLE

            /*string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local", "share", "dvm-yt", "bin", "yt-dlp"
            ); // CODIGO DE PRUEBA */

            System.Console.WriteLine("[---] Extrayendo metadatos del video...");

            // 1. Capture Mode – Extract full metadata as JSON
            var ytdlp = new Ytdlp(ruta);
            var res = ytdlp.ExecuteRawAsync(
                $"--dump-single-json \"{URL}\""
            );

            Task animar = Barra.AnimarCarga(res); // Animacion de carga


            await Task.WhenAll(res, animar); // Esperar hasta que la tarea de extracion termine
            Console.Clear();

            // Deserializacion de los metadatos
            MetadatosVideo metadatovideo = JsonSerializer.Deserialize<MetadatosVideo>(res.Result.FullOutput,new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            } ) ?? throw new Exception("[!!!] No se pudo extraer los metadatos");
            //Extraer calidades y formatos // SOLO MOSTRAR SI DATOS CARGARON, BORRAR DESPUES
            var calidades = ExtraerCalidadesVideo(metadatovideo);
            var formato = ExtraerFormatosVideo(metadatovideo);
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
            return metadatosVideo.formatos.Where(x => x.height.HasValue).Select(x => x.height!.Value).Distinct().OrderBy(x => x).ToList();
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
