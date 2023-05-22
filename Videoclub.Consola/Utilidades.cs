// See https://aka.ms/new-console-template for more information

namespace Videoclub.Consola
{
    internal class Utilidades
    {
        internal static int PedirMenu(string mensaje, int min, int max)
        {
            string? input;
            while (true)
            {
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (!int.TryParse(input, out _))
                {
                    Console.WriteLine("Debe ingresar un valor numérico.");
                    continue;
                }
                if (Convert.ToInt32(input) < min || Convert.ToInt32(input) > max)
                {
                    Console.WriteLine($"Debe ingresar un valor numérico entre: {min} y {max}.");
                    continue;
                }
                break;
            }
            return Convert.ToInt32(input);
        }
    }
}