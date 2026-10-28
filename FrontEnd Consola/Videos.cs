using ManuHub.Ytdlp.NET;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace DVM;

public class Videos
{
    private string URL = string.Empty;

    public async Task Main()
    {
        Validar validar = new Validar();
        DescargarVideos descargarVideos = new DescargarVideos();
        try
        {
            /// POSIBLE //
            string[] bloque = {
                "[---] DESCARGAR VIDEO [---]",
                "[>>>Ingrese URL<<<]",
            };
            foreach (string linea in bloque)
            {
                int espacio = Math.Max(0,
                    (Console.WindowWidth - linea.Length) / 2);

                Console.WriteLine(new string(' ', espacio) + linea);
            }
            /// POSIBLE ///
            System.Console.Write(">>>> ");
            URL = System.Console.ReadLine();
            var val = validar.url(URL);
            if (val)
            {
                System.Console.WriteLine("[---] Iniciando procesos de descarga...");
                await MostrarTodosMetadatos(URL);

                //await descargarVideos.Descargar(URL);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error (Videos.Main): " + e.Message);
        }
    }

    public async Task MostrarTodosMetadatos(string URL)
    {
        try
        {
            CentrarTexto centrarTexto = new CentrarTexto();
            ExtractorMetadatos extractorMetadatos = new ExtractorMetadatos();
            // Extraer metadatos
            var extra = extractorMetadatos.ObtenerAcyncVideo(URL);
            await Task.WhenAll(extra);
            if (extra.IsCompletedSuccessfully)
            {
                // Extraer calidades y formatos (sin repetidos)
                var calidades = extractorMetadatos.ExtraerCalidadesVideo(extra.Result);
                var formatos = extractorMetadatos.ExtraerFormatosVideo(extra.Result);

                // Convertir minutos y segundos (ahora mismo tengo una perexa organizar esto asi que lo hare aqui mismo)
                TimeSpan tiempo = TimeSpan.FromSeconds(extra.Result.duracion);
                // Mostrar toda las webadas
                bool flag = true;
                while (flag)
                {
                    string[] bloque = { "[---] DESCARGAR VIDEO [---]", $"[Titulo: {extra.Result.titulo} |  Duracion: {tiempo.ToString(@"mm\:ss")} | Canal: {extra.Result.canal} | URL: {extra.Result.url}]", "[???] Descargar [S/s][N/n]" };
                    centrarTexto.CentrarVariosBloques(bloque);

                    string op = System.Console.ReadLine();
                    Console.Clear();
                    switch (op)
                    {
                        case "S":
                            await FormatosYCalidades(URL, calidades, formatos);

                            flag = false;
                            break;

                        case "N":
                            System.Console.WriteLine("REGRESANDO");
                            flag = false;
                            break;
                        default:
                            centrarTexto.CentrarUnBloque("[!!! Opcion invalida]");
                            break;
                    }
                }
            }
            else
            {
                Console.Clear();
                throw new Exception($"[!!!] No se pudo extraer correctamente los meta datos del video: {URL}");
            }

        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error (Videos.MostrarTodosMetadatos): {e.StackTrace}");
        }
    }

    public async Task FormatosYCalidades(string URL, List<int> Calidades, List<string> Formatos)
    {
        int xCalidad = 0;
        int xFormato = 0;
        DescargarVideos descargarVideos = new DescargarVideos();
        string[] lete = { string.Empty, string.Empty };
        CentrarTexto centrarTexto = new CentrarTexto();
        bool flag = true;

        while (flag) // While para seleccion de calidades
        {
            
            string texto = string.Empty;
            string[] bloque = { "[---] DESCARGAR VIDEO [---]", "[ SELECCIONE CALIDAD ]" };
            centrarTexto.CentrarVariosBloques(bloque);
            System.Console.WriteLine("\n");
            int i = 0;
            foreach (var x in Calidades)
            {
                texto = $"[|{i}| {x}p ] - {texto}";
                i++;
            }
            centrarTexto.CentrarUnBloque(texto);
            string op = System.Console.ReadLine();
            Console.Clear();
            if(int.TryParse(op,out int nigga)){
                if(!(nigga > Calidades.Count-1 || nigga < 0)){
                    xCalidad = nigga;
                    flag = false;
                } else {
                    
                    centrarTexto.CentrarUnBloque("[!!!] Calidad no valida");
                }
            } else {
                centrarTexto.CentrarUnBloque("[!!!] Calidad incorrecta");
            }
        }
        string p = $"Calidad: [{Calidades[xCalidad]}]";
        lete[0] = p;
        centrarTexto.CentrarVariosBloques(lete);

        flag = true;
        while(flag){ // Flag para seleccion de formato
            string texto = string.Empty;
            string[] bloque = {"[---] DESCARGAR VIDEO [---]", "[ SELECCIONE FORMATO ]" };
            centrarTexto.CentrarVariosBloques(bloque);
            System.Console.WriteLine("\n");
            int i = 0;
            foreach (var x in Formatos)
            {
                texto = $"[|{i}| {x} ] - {texto}";
                i++;
            }
            centrarTexto.CentrarUnBloque(texto);
            string op = System.Console.ReadLine();
            Console.Clear();
            if(int.TryParse(op,out int nigga)){
                if(!(nigga > Formatos.Count-1 || nigga < 0)){
                    xFormato = nigga;
                    flag = false;
                } else {
                    centrarTexto.CentrarUnBloque("[!!!] Calidad no valida");
                }
            } else {
                centrarTexto.CentrarUnBloque("[!!!] Calidad incorrecta");
            }
        }
        p = $"Formato: [{Formatos[xFormato]}]";
        lete[1] = p;
        centrarTexto.CentrarVariosBloques(lete);

        await descargarVideos.Descargar(URL, Calidades[xCalidad], Formatos[xFormato]);
    }
}
