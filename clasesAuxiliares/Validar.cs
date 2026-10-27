namespace DVM;
using System;
public class Validar 
{
    public bool url(string URL)
{
    try
    {
        if (URL != string.Empty)
        {
            bool valido = Uri.TryCreate(
                URL,
                UriKind.Absolute,
                out Uri? resultado
            ) && (resultado.Scheme == Uri.UriSchemeHttps ||
                  resultado.Scheme == Uri.UriSchemeHttp);

            if (valido)
            {
                Console.WriteLine($"---> Validacion {URL} correcta...");
                return true;
            }

            throw new Exception("!!! URL INVALIDA !!!");
        } else {
            throw new Exception("!!! CAMPO VACIO !!!");
        }  
    }
    catch (Exception e)
    {
        Console.Clear();
        Console.WriteLine($"Ha ocurrido un error (Validar): {e.Message}");
        return false;
    }
}
}
