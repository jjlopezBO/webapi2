using System;
using System.Data;

namespace cndcAPI.Models
{
	public class NovedadesDto
	{
        private static string _titulo= "t";
        private static string _fecha = "f";
        private static string _descripcion = "d";
        private static string _descripcion2 = "d2";


        public string Titulo { get; set; }
        public string Descripcion  { get; set; }
        public Decimal Valor { get; set; }
        public DateTime Fecha { get; set; }


		 

        public NovedadesDto(DataRow row)
        {
            Titulo = (string)row[NovedadesDto._titulo];
             
            
            if (row[NovedadesDto._descripcion] != System.DBNull.Value)
            {
                Descripcion = (string)row[NovedadesDto._descripcion];


            }
            else
            {
                Descripcion = string.Empty;


            }


            if (row[NovedadesDto._fecha] != System.DBNull.Value)
            {
                Fecha = (DateTime)row[NovedadesDto._fecha];
            }
            else
            {
                Fecha = DateTime.Now.Date;
            }


            if (row[NovedadesDto._descripcion2] != System.DBNull.Value)
            {
                Valor =   Convert.ToDecimal( row[NovedadesDto._descripcion2]);
            }
            else
            {
                Valor = -1;
            }

            if (row[NovedadesDto._descripcion] != System.DBNull.Value)
            {
                Descripcion = (string)row[NovedadesDto._descripcion];
            }
            else
            {
                Descripcion = string.Empty;
            }



        }
        public static List<NovedadesDto> FromDataTable(DataTable table)
        {
            List<NovedadesDto> lista = new List<NovedadesDto>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new NovedadesDto(row));
            }
            return lista;
        }

    }
}

