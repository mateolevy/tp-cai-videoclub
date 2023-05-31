// See https://aka.ms/new-console-template for more information

using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Videoclub.Consola
{
    internal class Utilidades
    {
        internal static DateTime PedirFecha(string mensaje)
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
                if (!DateTime.TryParse(input, out _))
                {
                    Console.WriteLine("Debe ingresar una fecha válida en formato DD/MM/AAAA.");
                    continue;
                }
                break;
            }
            return Convert.ToDateTime(input);
        }

        internal static int PedirInt(string mensaje)
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
                break;
            }
            return Convert.ToInt32(input);
        }
        internal static int PedirDNI(string mensaje)
        {
            string? input;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    IngreseValor();
                    continue;
                }
                if (!int.TryParse(input, out _))
                {
                    Console.WriteLine("Debe ingresar un valor numérico.");
                    continue;
                }
                if (input.Length != 8)
                {
                    Console.WriteLine("El DNI debe contener 8 dígitos.");
                    continue;
                }
                break;
            }
            return Convert.ToInt32(input);
        }

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

        internal static string PedirString(string mensaje)
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
                if (int.TryParse(input, out _))
                {
                    Console.WriteLine("Debe ingresar una cadena de texto.");
                    continue;
                }
                break;
            }
            return input;
        }
        
        internal static string PedirEmail(string mensaje)
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

                bool esMailValido;
                try
                {
                    MailAddress address = new MailAddress(input);
                    esMailValido = (address.Address == input);
                }
                catch (FormatException)
                {
                    esMailValido = false;
                }
                
                if (!esMailValido)
                {
                    Console.WriteLine("Debe ingresar un email valido.");
                    continue;
                }
                break;
            }
            return input;
        }

        internal static string PedirTelefono(string mensaje)
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
                if(input.Length < 10 || input.Length > 11)
                {
                    Console.WriteLine("El número de teléfono debe ser entre 10 y 11 caracteres.");
                    continue;
                }
                break;
            }
            return input;
        }

        private static void IngreseValor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un valor.");
        }
    }
}