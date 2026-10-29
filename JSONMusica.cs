namespace DVM;

using System.Text.Json;
using ManuHub.Ytdlp.NET;
public class JSONMusica
{
    public async Task ObtenerAcyncVideo(string URL)
    {
        try
        {
            BarraDeProgreso Barra = new BarraDeProgreso();
            ExtractorMetadatos ex = new ExtractorMetadatos();
            string ruta = Path.Combine( // Instanciando direccion de la extension 
            AppContext.BaseDirectory,
            "tools",
             "yt-dlp"
            );

            System.Console.WriteLine("[---] Extrayendo metadatos del video...");

            // 1. Capture Mode – Extract full metadata as JSON
            var ytdlp = new Ytdlp(ruta);
            var res = ytdlp.ExecuteRawAsync(
                $"--dump-single-json \"{URL}\""
            );

            Task animar = Barra.AnimarCarga(res); // Animacion de carga


            await Task.WhenAll(res, animar); // Esperar hasta que la tarea de extracion termine
            //Console.Clear();

            // Deserializacion de los metadatos
            MetadatosVideo metadatovideo = JsonSerializer.Deserialize<MetadatosVideo>(res.Result.FullOutput,new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            } ) ?? throw new Exception("[!!!] No se pudo extraer los metadatos");
            //Extraer calidades y formatos // SOLO MOSTRAR SI DATOS CARGARON, BORRAR DESPUES
            var calidades = ex.ExtraerCalidadesVideo(metadatovideo);
            var formato = ex.ExtraerFormatosVideo(metadatovideo);
            // Contar calidades y formatos
            System.Console.WriteLine(calidades.Count); // contar calidades
            System.Console.WriteLine(formato.Count); // contar formatos
            System.Console.WriteLine("Duracion: "+metadatovideo.duracion);

            // Mostrar calidades y formatos
            foreach(var cal in calidades){
                System.Console.WriteLine(cal);
            }
            foreach(var fora in formato ){
                System.Console.WriteLine(fora);
            }

            System.Console.ReadKey();
            
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (ExtraerMetadatos): {e.Message}");
        }
        
    }
}
