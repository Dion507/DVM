namespace DVM;

public class GestionarDirectorios
{
    public string CrearDirectoriosMusica(){
        // Creando directorios relativas 
        string misMusicas = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        string musicasDescargados = Path.Combine(misMusicas,"YtdlpMusicas");
        // Validar si existe los directorios 
        System.Console.WriteLine("||| Validando directorios necesarios");
        try {
            if(!Directory.Exists(musicasDescargados)){
                System.Console.WriteLine($"!!! EL directorio {musicasDescargados} no existe !!!");
                System.Console.WriteLine("||| Creando directorio...");
                Directory.CreateDirectory(musicasDescargados);
                System.Console.WriteLine("[O] Directorio creado");
                System.Console.WriteLine(Directory.Exists(musicasDescargados));
                return musicasDescargados;
            } else {
                System.Console.WriteLine($"[O] Directorio {musicasDescargados} ya existe");
                return musicasDescargados;
            }
        } catch (IOException ex){
            
            System.Console.WriteLine($"Error IOExeption (CrearFolders): {ex.Message}");
        } catch (Exception e){
            System.Console.WriteLine($"Error Exception {e.Message}");
        }
        return string.Empty;
    }
    public string CrearDirectoriosVideos(){
        // Creando directorios relativas 
        string misMusicas = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        string musicasDescargados = Path.Combine(misMusicas,"YtdlpVideos");
        // Validar si existe los directorios 
        System.Console.WriteLine("||| Validando directorios necesarios");
        try {
            if(!Directory.Exists(musicasDescargados)){
                System.Console.WriteLine($"!!! EL directorio {musicasDescargados} no existe !!!");
                System.Console.WriteLine("||| Creando directorio...");
                Directory.CreateDirectory(musicasDescargados);
                System.Console.WriteLine("[O] Directorio creado");
                System.Console.WriteLine(Directory.Exists(musicasDescargados));
                return musicasDescargados;
            } else {
                System.Console.WriteLine($"[O] Directorio {musicasDescargados} ya existe");
                return musicasDescargados;
            }
        } catch (IOException ex){
            
            System.Console.WriteLine($"Error IOExeption (CrearFolders): {ex.Message}");
        } catch (Exception e){
            System.Console.WriteLine($"Error Exception {e.Message}");
        }
        return string.Empty;
    }
}
