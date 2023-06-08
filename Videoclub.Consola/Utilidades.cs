// See https://aka.ms/new-console-template for more information

using System.Net.Mail;

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
        internal static int PedirPlazo(string mensaje)
        {
            string? input;
            while (true)
            {
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
                if (Convert.ToInt32(input) > 15)
                {
                    IngresePlazoValido();
                    continue;
                }
                break;
            }
            return Convert.ToInt32(input);
        }

        private static void IngresePlazoValido()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El plazo es hasta 15 días.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void TelefonoLargoDigitos()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El número de teléfono debe ser entre 10 y 11 caracteres.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void IngreseValor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un valor.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void IngreseValorNumerico()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un valor numérico.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void IngreseValorNumericoEntre(int min, int max)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Debe ingresar un valor numérico entre {min} y {max}.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void DniLargoDigitos()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El DNI debe contener 8 dígitos.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void IngreseFechaValida()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar una fecha válida en formato DD/MM/AAAA.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void IngreseCadena()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar una cadena de texto.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void IngreseMailValido()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debe ingresar un email valido.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        internal static void MensajeError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        internal static void MensajeExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}