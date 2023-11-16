using System;
using System.Data;

namespace cndcAPI.Models
{
	public class FechasDto
	{
        private static string _tipo = "TIPO";
        private static string _fecha = "FECHA";

        public string Tipo { get; set; }
        public string Fecha { get; set; }

        public FechasDto(DataRow row)
        {
            Tipo = (string)row[FechasDto._tipo];
            Fecha = (string)row[FechasDto._fecha];
        }

        public static List<FechasDto> FromDataTable(DataTable table)
        {
            List<FechasDto> lista = new List<FechasDto>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new FechasDto(row));
            }
            return lista;
        }

    }
}

