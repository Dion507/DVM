namespace DVM;

public class Otros
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
}
