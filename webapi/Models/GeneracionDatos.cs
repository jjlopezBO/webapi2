using System;
using System.Data;

namespace cndcAPI.Models
{
	public class GeneracionDatos
	{

        private static string _codigo= "CODIGO";
        private static string _gen = "GEN";
        private static string _fecha = "FECHA";
        private static string _valor = "VALOR";


        public string Codigo { get; set; }
        public string Gen { get; set; }
        public string Fecha { get; set; }
        public Double Valor { get; set; }


        public GeneracionDatos(DataRow row)
        {
            Codigo = (string)row[GeneracionDatos._codigo];
            Fecha = (string)row[GeneracionDatos._fecha];
            Gen = (string)row[GeneracionDatos._gen];
            Valor = Convert.ToDouble(row[GeneracionDatos._valor]);
        }

        public static List<GeneracionDatos> FromDataTable(DataTable table)
        {
            List<GeneracionDatos> lista = new List<GeneracionDatos>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new GeneracionDatos(row));
            }
            return lista;
        }
    }
}

