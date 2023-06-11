using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaMedidoresModel
{
    public class MedidorDALArchivos : IMedidorDAL
    {
        private MedidorDALArchivos() 
        {

        }

        private static MedidorDALArchivos instancia;
         
        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALArchivos();
            }
            return instancia;

        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";

        public void AgregarMedidor(Medidor medidor)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(medidor.NroMedidor + "|" + medidor.Fecha + "|" + medidor.ValorConsumo);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Medidor> ObtenerMedidor()
        {
            List<Medidor> lista = new List<Medidor>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            Medidor medidor = new Medidor()
                            {
                                NroMedidor = int.Parse(arr[0]),
                                Fecha = arr[1],
                                ValorConsumo = double.Parse(arr[2])
                            };
                            lista.Add(medidor);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
