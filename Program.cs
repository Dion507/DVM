using System.Threading.Tasks;

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
        }
    }   
}