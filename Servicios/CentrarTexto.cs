namespace DVM;

public class CentrarTexto
{
    public void CentrarUnBloque(string texto)
    {
        int posicion = (Console.WindowWidth + texto.Length) / 2;
        Console.WriteLine(texto.PadLeft(posicion));
    }

    public void CentrarVariosBloques(string[] bloque)
    {
        foreach (string linea in bloque)
        {
            int espacio = Math.Max(0,
                (Console.WindowWidth - linea.Length) / 2);

            Console.WriteLine(new string(' ', espacio) + linea);
        }
    }

    public void CentrarVariosBloques(string[] bloque, bool estado) // Metodosobrecargado para dos tareas
    {
        try
        {
            if (estado)
            { // True centrar. False no, solo impresion en la forma que sea
                foreach (string linea in bloque)
                {
                    int espacio = Math.Max(0,
                        (Console.WindowWidth - linea.Length) / 2);

                    Console.WriteLine(new string(' ', espacio) + linea);
                }
            }
            else
            {
                foreach (string linea in bloque)
                {
                    System.Console.WriteLine(linea);
                }
            }

        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error [CentrarVariosBloques]: {e.Message}");
        }

    }

}
