using ManuHub.Ytdlp.NET;
using System.Reflection;

namespace DVM;

public class IntVideos
{
    private string URL = string.Empty;

    public async Task Main()
    {
        string ent;
        bool flag = true;
        try
        {
            while (flag)
            {
                System.Console.WriteLine("--- DESCARGAR VIDEO ---");
                System.Console.WriteLine("1. Insertar URL del Video");
                System.Console.WriteLine("2. Regresar");
                System.Console.WriteLine("3. Salir");
                System.Console.Write(">>>> ");
                ent = System.Console.ReadLine();
                Console.Clear();
                if (int.TryParse(ent, out int entrada))
                {
                    switch (entrada)
                    {
                        case 1:
                            await urlIN();
                            break;
                        case 2:
                            flag = false;
                            Console.Clear();
                            break;
                        case 3:
                            System.Console.WriteLine("Saliendo...");
                            Environment.Exit(593);
                            break;
                        default:
                            System.Console.WriteLine("Opcion invalida");
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("VALOR INVALIDO");
                }
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error in videos: " + e.Message);
        }
    }

    async Task urlIN()
    {
        Validar validar = new Validar();

        try
        {
            System.Console.WriteLine("|_____ DESCARGAR VIDEO ");
            System.Console.WriteLine("Ingrese URL");
            System.Console.Write(">>>> ");
            URL = System.Console.ReadLine();
            var val = validar.url(URL);
            if (val)
            {
                System.Console.WriteLine("---> Iniciando procesos de descarga...");
                await descarga(URL);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error video.urlIN: " + e.Message);
        }
    }

    async Task descarga(string URL)
    {
        try
        {
            Otros other = new Otros();

            string ruta = Path.Combine( // Instanciando direccion de la extension 
            AppContext.BaseDirectory,
            "tools",
             "yt-dlp"
            );

            // Validando directorios 
            string desVid = crearFolders();
            if(desVid != string.Empty){
                var ytdlp = new Ytdlp(ruta, new MiLogger())
                .WithMaxHeight(1080)
                .WithOutputFolder(desVid);
                
                ytdlp.ProgressDownload += (s, e) =>
                {
                    other.MostrarProgreso(e.Percent);
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

    public string crearFolders(){
        // Creando directorios relativas 
        string misVIdeos = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        string videoDescargados = Path.Combine(misVIdeos,"YtdlpVideos");
        // Validar si existe los directorios 
        System.Console.WriteLine("||| Validando directorios necesarios");
        try {
            if(!Directory.Exists(videoDescargados)){
                System.Console.WriteLine($"!!! EL directorio {videoDescargados} no existe !!!");
                System.Console.WriteLine("||| Creando directorio...");
                Directory.CreateDirectory(videoDescargados);
                System.Console.WriteLine("[O] Directorio creado");
                System.Console.WriteLine(Directory.Exists(videoDescargados));
                return videoDescargados;
            } else {
                System.Console.WriteLine($"[O] Directorio {videoDescargados} ya existe");
                return videoDescargados;
            }
        } catch (IOException ex){
            
            System.Console.WriteLine($"Error IOExeption (CrearFolders): {ex.Message}");
        } catch (Exception e){
            System.Console.WriteLine($"Error Exception {e.Message}");
        }
        return string.Empty;
    }

    public void prueba()
    {

        Type tipo = typeof(ManuHub.Ytdlp.NET.ILogger);

        foreach (MethodInfo metodo in tipo.GetMethods())
        {
            Console.WriteLine(metodo);
        }

        foreach (PropertyInfo propiedad in tipo.GetProperties())
        {
            Console.WriteLine(propiedad);
        }
    }
}
