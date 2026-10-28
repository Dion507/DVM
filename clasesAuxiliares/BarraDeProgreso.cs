namespace DVM;

public class BarraDeProgreso
{
    public void MostrarProgreso(double porcentaje)
    {
        porcentaje = Math.Clamp(porcentaje, 0, 100);

        int longitud = 30;
        int llenos = (int)(porcentaje * longitud / 100);

        string barra = new string('█', llenos);
        string vacios = new string('░', longitud - llenos);

        Console.Write($"\r[{barra}{vacios}] {porcentaje:F2}%   ");
    }

    public async Task AnimarCarga(Task tarea) // Se nececita usar whenAll y tener al menos 1 o mas tareas primarias
    {
        int i = 0;

        string [] carga = {"\rCargando[c-o--o--o--o]","\rCargando[-co--o--o--o]","\rCargando[--C--o--o--o]","\rCargando[---c-o--o--o]","\rCargando[----co--o--o]","\rCargando[-----C--o--o]","\rCargando[------c-o--o]","\rCargando[-------co--o]","\rCargando[--------C--o]","\rCargando[---------c-o]","\rCargando[----------co]","\rCargando[-----------C"};
        while(!tarea.IsCompleted){
            System.Console.Write(carga[i]);
            i = (i + 1) % carga.Length;
            await Task.Delay(400);
        }
    }
    
}


