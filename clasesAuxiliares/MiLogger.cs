using ManuHub.Ytdlp.NET;

namespace DVM;

public class MiLogger : ILogger
{
    public void Log(LogType tipo, string mensaje)
    {
        // No mostrar los mensajes informativos.
    }
}