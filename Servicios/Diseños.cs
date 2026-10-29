using System.Reflection.Metadata.Ecma335;

namespace DVM;

public class Diseños
{  
    public void Cursor(string op, ConsoleColor color) 
    {
        Console.ForegroundColor = color;
        Console.Write($" {op} ❯ ");
        Console.ResetColor();
    }

    public void Cursor(string op) 
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($" {op} ❯ ");
        Console.ResetColor();
    }

    public void Divisora(){
        Console.ForegroundColor = ConsoleColor.Gray;
        System.Console.WriteLine(new string('-',30));
        Console.ResetColor();
    }

    public void Divisora(ConsoleColor color){
        Console.ForegroundColor = color;
        System.Console.WriteLine(new string('.',30));
        Console.ResetColor();
    }
}


