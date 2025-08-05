using System.Data;

namespace cndcAPI.Models
{
    public class Frecuencia
    {// valor, hora

        private static string _valor = "VALOR";
        private static string _hora = "HORA";

        public decimal Valor { get; set; }
        public string Hora { get; set; }

        public Frecuencia(DataRow row)
        {
            Valor = (decimal) row[Frecuencia._valor];
            Hora = (string)row[Frecuencia._hora];
        }

        public static List<Frecuencia> FromDataTable(DataTable table)
        {
            List<Frecuencia> lista = new List<Frecuencia>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new Frecuencia(row));
            }
            return lista;
        }
    }
}
