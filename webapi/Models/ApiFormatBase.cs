using System.Data;

namespace cndcAPI.Models
{
    public class ApiFormatBase
    {

        private static string _codigo = "CODIGO";
        private static string _fecha = "FECHA";
        private static string[] _v = { "P1", "P2", "P3", "P4", "P5", "P6", "P7", "P8", "P9", "P10", "P11", "P12", "P13", "P14", "P15", "P16", "P17", "P18", "P19", "P20", "P21", "P22", "P23", "P24" };


        public string Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public List<double> Valores { get; set; }


        public ApiFormatBase(DataRow row)
        {
            Codigo = (string)row[ApiFormatBase._codigo];
            Fecha = (DateTime)row[ApiFormatBase._fecha];
            Valores = new List<double>();
            foreach (string v in ApiFormatBase._v)
            {
                Valores.Add((double)(decimal)row[v]);
            }
        }
        public static List<ApiFormatBase> FromDataTable(DataTable table)
        {
            List<ApiFormatBase> lista = new List<ApiFormatBase>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new ApiFormatBase(row));
            }
            return lista;
        }

    }
}
