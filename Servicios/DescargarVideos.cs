namespace DVM;
using ManuHub.Ytdlp.NET;
public class DescargarVideos 
{
    public async Task Descargar(string URL,int calidad, string formato){
        try
        {
            BarraDeProgreso BarraDeProgreso = new BarraDeProgreso();
            GestionarDirectorios Directorio = new GestionarDirectorios();

            string ruta = Path.Combine( // Instanciando direccion de la extension 
            AppContext.BaseDirectory,
            "tools",
             "yt-dlp"
            );

            // Validando directorios 
            string desVid = Directorio.CrearDirectoriosVideos();
            if(desVid != string.Empty){
                var ytdlp = new Ytdlp(ruta, new MiLogger())
    .WithMaxHeight(calidad)
    .WithMergeOutputFormat(formato)
    .WithOutputFolder(desVid);
                
                ytdlp.ProgressDownload += (s, e) =>
                {
                    BarraDeProgreso.MostrarProgreso(e.Percent);
                };
                ytdlp.DownloadCompleted += (s, msg) => Console.WriteLine($"Finished: {msg}");

                await ytdlp.DownloadAsync(URL);

                Console.WriteLine();
                Console.WriteLine("[OOO] DESCARGA COMPLETA (Haz click en cualquier tecla)");

                Console.ReadKey();
                Console.Clear();
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (Descarga): {e.Message}");
        }
    }
}
