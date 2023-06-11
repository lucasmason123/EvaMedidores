using EvaMedidoresModel;
using Mensajero.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvaMedidores
{
     class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        static bool Menu()
        {
            bool contia = true;
            Console.WriteLine("\n ¿Que quiere hacer?");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar \n 0. Salir ");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    contia = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return contia;
        }

        static void IniciarServidor()
        {

        }
        static void Main(string[] args)
        {

            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();

            while (Menu()) ;

        }

        static void Ingresar()
        {
            Console.WriteLine("Ingrese medidor : ");
            int nroMedidor;
            if (int.TryParse(Console.ReadLine().Trim(), out nroMedidor))
            {
                Console.WriteLine("Ingrese valor de consumo : ");
                double valorConsumo;
                DateTime datetime = new DateTime();
                datetime = DateTime.Now;
                if (double.TryParse(Console.ReadLine().Trim(), out valorConsumo))
                {
                    Medidor medidor = new Medidor()
                    {
                        NroMedidor = nroMedidor,
                        ValorConsumo = valorConsumo,
                        Fecha = datetime.ToString("yyyy-MM-dd-HH-mm-ss"),
                    };
                    lock (medidorDAL)
                    {
                        medidorDAL.AgregarMedidor(medidor);
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese un valor de consumo válido");
                }
            }
            else
            {
                Console.WriteLine("Ingrese un número de medidor válido");
            }

        }

        static void Mostrar()
        {
            List<Medidor> medidor = null;
            lock (medidorDAL)
            {
                medidor = medidorDAL.ObtenerMedidor();
            }
            foreach (Medidor men in medidor)
            {
                Console.Write(men.ToString());
            }

        }
    }
}
