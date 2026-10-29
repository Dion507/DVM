using ManuHub.Ytdlp.NET;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace DVM;

public class Videos
{
    private string URL = string.Empty;
    public CentrarTexto centrarTexto = new CentrarTexto();
    public Diseños cursor = new Diseños();
    public async Task Main()
    {
        
        
        Validar validar = new Validar();
        DescargarVideos descargarVideos = new DescargarVideos();
        try
        {
            /// POSIBLE //
            string[] bloque = {
                "[---] DESCARGAR VIDEO [---]",
                "YouTube, Twitter (X), Instagram, otros"
            };

            centrarTexto.CentrarVariosBloques(bloque);
            
            /// POSIBLE ///
            cursor.Divisora();
            cursor.Cursor("INGRESE URL");
            URL = System.Console.ReadLine();
            Console.Clear();
            var val = validar.url(URL);
            if (val)
            {
                System.Console.WriteLine("[---] Iniciando procesos de descarga...");
                await MostrarTodosMetadatos(URL);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error (Videos.Main): " + e.Message);
        }
    }

    public async Task MostrarTodosMetadatos(string URL)
    {
        // Areglar aqui para que se pueda evitar leer metadatos
        try
        {
            CentrarTexto centrarTexto = new CentrarTexto();
            ExtractorMetadatos extractorMetadatos = new ExtractorMetadatos();
            // Extraer metadatos
            var extra = extractorMetadatos.ObtenerAcyncVideo(URL); // Obtener  metadatos
            await Task.WhenAll(extra);
            if (extra.IsCompletedSuccessfully)
            {
                // Extraer calidades y formatos (sin repetidos)
                var calidades = extractorMetadatos.ExtraerCalidadesVideo(extra.Result);
                var formatos = extractorMetadatos.ExtraerFormatosVideo(extra.Result);

                // Convertir minutos y segundos (ahora mismo tengo una perexa organizar esto asi que lo hare aqui mismo)
                TimeSpan tiempo = TimeSpan.FromSeconds(extra.Result.duracion ?? 0);
                // Mostrar toda las webadas
                bool flag = true;
                while (flag)
                {
                    string[] bloque = { "[---] DESCARGAR VIDEO [---]", $"[Titulo: {extra.Result.titulo} |  Duracion:{tiempo.ToString(@"mm\:ss")} | Canal: {extra.Result.canal} | URL: {extra.Result.url}]" };
                    centrarTexto.CentrarVariosBloques(bloque);
                    cursor.Divisora();
                    cursor.Cursor("DESCARGAR [s/n]");
                    string op = System.Console.ReadLine();
                    Console.Clear();
                    switch (op.ToLower())
                    {
                        case "s":
                            await FormatosYCalidades(URL, calidades, formatos);

                            flag = false;
                            break;
                        case "n":
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
        // contadores 
        int xCalidad = 0; 
        int xFormato = 0;
        string texto = string.Empty;  // Almacena opciones de formato y calidades
        DescargarVideos descargarVideos = new DescargarVideos(); // Para descargar el video
        string[] lete = { string.Empty, string.Empty }; // Arreglo para mostrar calidades y formatos
        CentrarTexto centrarTexto = new CentrarTexto(); // Objeto encargado de mostrar texto centrado
        bool flag = true; // Bandera de opciones 

        while (flag) // While para seleccion de calidades
        {
            // Mostrar calidades
            texto = string.Empty;  // Vaciar lista de calidades 
            string[] bloque = { "[---] DESCARGAR VIDEO [---]" };
            centrarTexto.CentrarVariosBloques(bloque);
            System.Console.WriteLine("\n");
            int i = 0;
            foreach (var x in Calidades)
            {
                System.Console.WriteLine($"[|{i}| {x}p ]");
                i++;
            }
            
            cursor.Divisora();
            cursor.Cursor("SELECCIONE CALIDAD");
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
            texto = string.Empty;  // Vaciar lista de formatos 
            string[] bloque = {"[---] DESCARGAR VIDEO [---]" };
            centrarTexto.CentrarVariosBloques(bloque);
            System.Console.WriteLine("\n");
            int i = 0;
            foreach (var x in Formatos)
            {
                System.Console.WriteLine($"[|{i}| {x} ]");
                i++;
            }
            cursor.Divisora();
            cursor.Cursor("SELECCIONE FORMATO");
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
