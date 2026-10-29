using System.Threading.Tasks;
using System.Text.Json;
using ManuHub.Ytdlp.NET;
using System.Globalization;

namespace DVM
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Interfaz HUD = new Interfaz();
            await HUD.Main();
            
            /*IntVideos videos = new IntVideos();
            videos.prueba();*/

            //Musica m = new Musica();
            //m.crearFolders();

            //ExtractorMetadatos ex = new ExtractorMetadatos();
            //await ex.ObtenerAcyncVideo();

            //JSONMusica a = new JSONMusica();
            //await a.ObtenerAcyncVideo("https://es.pornhub.com/view_video.php?viewkey=6ad09538f3934");

        }
    }
}