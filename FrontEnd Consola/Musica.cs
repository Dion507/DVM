namespace DVM;

public class Musica
{
    private string URL = string.Empty;
    public Diseños cursor = new Diseños();
    public async Task Main()
    {
        Validar validar = new Validar();
        DescargarMusicas DescargarMusic = new DescargarMusicas();
        CentrarTexto centrarTexto = new CentrarTexto();

        try
        {
            string[] bloque = {"[!!!] ESTA OPCION AUN NO ESTA ACTUALIZADA",
                "[---] EXTRAER MUSICA [---]",
                "Youtube, Instagram, Twitter (x), otros"
            };

            centrarTexto.CentrarVariosBloques(bloque);
            cursor.Divisora();
            cursor.Cursor("INGRESE URL");
            URL = System.Console.ReadLine();
            var val = validar.url(URL);
            Console.Clear();
            if (val)
            {
                System.Console.WriteLine("[---] Iniciando procesos de extracion...");
                await DescargarMusic.Descargar(URL);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error video.urlIN: " + e.Message);
        }
    }
}
