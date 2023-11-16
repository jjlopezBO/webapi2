using System;
using System.Data;

namespace cndcAPI.Models
{
	public class LineasDto
	{
		public LineasDto(DataRow row)
        {
            CodigoAgente = (string)row[LineasDto._codigo_agente];
            UnLt = Convert.ToInt32 (row[LineasDto._un_lt]);
            Total = Convert.ToDouble (row[LineasDto._total]);
        }

        private static string _codigo_agente = "codigo_agente";
        private static string _un_lt = "un_lt";
        private static string _total = "total";

        public string CodigoAgente { get; set; }
        public int UnLt { get; set; }
        public double Total { get; set; }

        
        public static List<LineasDto> FromDataTable(DataTable table)
        {
            List<LineasDto> lista = new List<LineasDto>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new LineasDto(row));
            }
            return lista;
        }

      
    }
}

