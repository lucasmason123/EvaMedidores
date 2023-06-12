using EvaMedidoresModel;
using ServidorSocketUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensajero.Comunicacion
{

    public class HebraCliente
    {
        private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();

        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }
        public void Ejecutar()
        {
            clienteCom.Escribir("Ingresar medidor: ");
            int nroMedidor;
            if (int.TryParse(clienteCom.Leer().Trim(), out nroMedidor))
            {
                clienteCom.Escribir("Ingrese valor de consumo : ");
                double valorConsumo;
                DateTime datetime = new DateTime();
                datetime = DateTime.Now;
                if (double.TryParse(clienteCom.Leer().Trim(), out valorConsumo))
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
                    clienteCom.Escribir("Ingrese un valor de consumo válido");
                }
            }
            else
            {
                clienteCom.Escribir("Ingrese un número de medidor válido");
            }

            clienteCom.Desconectar();

        }
    }
}
