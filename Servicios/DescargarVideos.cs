namespace DVM;
using ManuHub.Ytdlp.NET;
public class DescargarVideos 
{
    public async Task Descargar(string URL,int calidad, string formato){
        try
        {
            BarraDeProgreso BarraDeProgreso = new BarraDeProgreso(); // Barra de progreso 
            GestionarDirectorios Directorio = new GestionarDirectorios(); // Encargado de gestionar directorios

            string ruta = Path.Combine( // Instanciando direccion de la extension 
            AppContext.BaseDirectory,
            "tools",
             "yt-dlp"
            ); // NO BORRAR (REUTILIZABLE)

            /*string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local", "share", "dvm-yt", "bin", "yt-dlp"
            ); // CODIGO DE PRUEBA */

            // Validando directorios 
            string desVid = Directorio.CrearDirectoriosVideos(); // Validando directorios 
            if(desVid != string.Empty){ // si todo es correcto, se extrae el video
                var ytdlp = new Ytdlp(ruta, new MiLogger()).WithMaxHeight(calidad).WithMergeOutputFormat(formato).WithOutputFolder(desVid);
                
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
