﻿// See https://aka.ms/new-console-template for more information

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
                    Console.WriteLine("Debe ingresar una fecha.");
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
    }
}