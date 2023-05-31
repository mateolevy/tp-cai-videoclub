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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    IngreseValor();
                    continue;
                }
                if (!DateTime.TryParse(input, out _))
                {
                    IngreseFechaValida();
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
                    IngreseValorNumerico();
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
                    IngreseValorNumerico();
                    continue;
                }
                if (input.Length != 8)
                {
                    DniLargoDigitos();
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
                    IngreseValorNumerico();
                    continue;
                }
                if (Convert.ToInt32(input) < min || Convert.ToInt32(input) > max)
                {
                    IngreseValorNumericoEntre(min, max);
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    IngreseValor();
                    continue;
                }
                if (int.TryParse(input, out _))
                {
                    IngreseCadena();
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    IngreseValor();
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
                    IngreseMailValido();
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mensaje);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    IngreseValor();
                    continue;
                }
                if(input.Length < 10 || input.Length > 11)
                {
                    TelefonoLargoDigitos();
                    continue;
                }
                break;
            }
            return input;
        }

        private static void TelefonoLargoDigitos()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El número de teléfono debe ser entre 10 y 11 caracteres.");
        }
        private static void IngreseValor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un valor.");
        }
        private static void IngreseValorNumerico()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un valor numérico.");
        }
        private static void IngreseValorNumericoEntre(int min, int max)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Debe ingresar un valor numérico entre {min} y {max}.");
        }
        private static void DniLargoDigitos()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El DNI debe contener 8 dígitos.");
        }
        private static void IngreseFechaValida()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar una fecha válida en formato DD/MM/AAAA.");
        }
        private static void IngreseCadena()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar una cadena de texto.");
        }
        private static void IngreseMailValido()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un email valido.");
        }
        
    }
}