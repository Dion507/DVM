using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DVM;

public class Interfaz
{
    public async Task Main()
    {
        bool flag = true;
        CentrarTexto centrarTexto = new CentrarTexto();
        Diseños cursor = new Diseños();
        string ent = string.Empty;
        Videos videos = new Videos();
        string rutaMessage = Path.Combine(AppContext.BaseDirectory,"tools","Txt","Dvm.txt");
        string [] DvmMessage = File.ReadAllLines(rutaMessage);
        string [] titulo =
         {"[Version 1.1 cffi]","[DVM]",
        " - Descargar videos y extraer musica -",
        };

        string [] opciones = {
            "[1] Descargar video",
        "[2] Extraer musica",
        "[3] Cerrar/Salir",
        "[4] Sobre DVM."
        };

        try //TRY CATCH
        {
            while (flag)
            {
                centrarTexto.CentrarVariosBloques(titulo);
                centrarTexto.CentrarVariosBloques(opciones,false);

                cursor.Divisora();
                cursor.Cursor("SELECCIONE OPCION",ConsoleColor.Green);
                ent = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(ent, out int entrada))
                {
                    switch (entrada)
                    {
                        case 1:
                            await videos.Main();
                            break;
                        case 2:
                            await new Musica().Main(); 
                            break;
                        case 3:
                            System.Console.WriteLine("Saliendo...");
                            flag = false;
                            break;
                        case 4:
                            centrarTexto.CentrarVariosBloques(DvmMessage,false);
                            break;
                        default:
                            System.Console.WriteLine("Opcion incorrecta");
                            break;
                    }
                    System.Console.WriteLine("");
                } else {
                    System.Console.WriteLine("Tipo de valor invalido");
                    System.Console.WriteLine("");
                }

            }
        }
        catch (Exception e) // FIN TRY CATCH 
        {
            System.Console.WriteLine($"{"Error: " + e.Message}");
        }

    }
}
