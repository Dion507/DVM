using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DVM;

public class Interfaz
{
    public async Task Main()
    {
        bool flag = true;
        string ent = string.Empty;
        Videos videos = new Videos();

        try //TRY CATCH
        {
            while (flag)
            {
                System.Console.WriteLine("Proyecto - DVM -");
                System.Console.WriteLine("1. Descargar video");
                System.Console.WriteLine("2. Descargar musica");
                System.Console.WriteLine("3. Salir");
                System.Console.Write(">>>>> ");
                
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
