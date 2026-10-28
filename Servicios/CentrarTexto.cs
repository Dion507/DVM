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
}
