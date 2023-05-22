// See https://aka.ms/new-console-template for more information

namespace Videoclub.Consola
{
    internal class Utiliddades
    {
        internal static int PedirMenu(string mensaje, int min, int max)
        {
            string input;
            while (true)
            {
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (input is null || input.Length == 0)
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (!int.TryParse(input, out int num))
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