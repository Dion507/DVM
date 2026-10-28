namespace DVM;

public class Musica
{
    private string URL = string.Empty;
    public async Task Main()
    {
        Validar validar = new Validar();
        DescargarMusicas DescargarMusic = new DescargarMusicas();

        try
        {
            System.Console.WriteLine("[---] DESCARGAR MUSICA [---]");
            System.Console.WriteLine("Ingrese URL");
            System.Console.Write(">>>> ");
            URL = System.Console.ReadLine();
            var val = validar.url(URL);
            if (val)
            {
                System.Console.WriteLine("---> Iniciando procesos de descarga...");
                //await DescargarMusic.Descargar(URL);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error video.urlIN: " + e.Message);
        }
    }
}
