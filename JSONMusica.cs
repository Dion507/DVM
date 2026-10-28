namespace DVM;

using System.Text.Json;
using ManuHub.Ytdlp.NET;
public class JSONMusica
{
    public async Task main()
    {
        try
        {
            string ruta = Path.Combine( // Instanciando direccion de la extension 
            AppContext.BaseDirectory,
            "tools",
             "yt-dlp"
            );

            System.Console.WriteLine(">>>> ");

            // 1. Capture Mode – Extract full metadata as JSON
            var ytdlp = new Ytdlp(ruta);
            var resultado = await ytdlp.ExecuteRawAsync(
    "--print \"{\\\"titulo\\\": %(title)j, " +
    "\\\"duracion\\\": %(duration)j, " +
    "\\\"canal\\\": %(channel)j, " +
    "\\\"url\\\": %(webpage_url)j, " +
    "\\\"formatos\\\": %(formats.:.{ext})j, " +
    "\\\"calidades\\\": %(formats.:.{height})j}\" " +
    "https://youtu.be/XzIfoPPwbU8"
);
            System.Console.WriteLine(resultado.ToString());

            
            string a = resultado.FullOutput;

            File.WriteAllText("jonson.json", a);
            Console.WriteLine($"Metadata JSON: {resultado.FullOutput}");
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (JSONMusica): {e.Message}");
        }
    }

    public void pruebas(){

        string [] carga = {"\rCargando[c-o--o-]","\rCargando[-co--o-]","\rCargando[--C--o-]","\rCargando[---c-o-]","\rCargando[----co-]","\rCargando[-----C-]","\rCargando[------c]"};
        System.Console.WriteLine(carga.Length);
    }
}
