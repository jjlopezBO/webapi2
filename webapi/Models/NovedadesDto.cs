using System;
using System.Collections.Generic;
using System.Data;

namespace cndcAPI.Models
{
    public class NovedadesDto
    {
        // Aliases de columnas
        private static readonly string _titulo = "t";
        private static readonly string _fecha = "f";
        private static readonly string _descripcion = "d";
        private static readonly string _descripcion2 = "d2";

        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Valor { get; set; } = -1;
        public DateTime Fecha { get; set; } = DateTime.Now.Date;

        public NovedadesDto(DataRow row)
        {
            // Titulo
            if (row.Table.Columns.Contains(_titulo) && row[_titulo] != DBNull.Value)
                Titulo = Convert.ToString(row[_titulo]) ?? string.Empty;

            // Descripcion
            if (row.Table.Columns.Contains(_descripcion) && row[_descripcion] != DBNull.Value)
                Descripcion = Convert.ToString(row[_descripcion]) ?? string.Empty;

            // Fecha
            if (row.Table.Columns.Contains(_fecha) && row[_fecha] != DBNull.Value)
                Fecha = (DateTime)row[_fecha];

            // Valor (desde d2)
            if (row.Table.Columns.Contains(_descripcion2) && row[_descripcion2] != DBNull.Value)
                Valor = Convert.ToDecimal(row[_descripcion2]);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}",Titulo, Descripcion, Valor, Fecha);
        }
        public static List<NovedadesDto> FromDataTable(DataTable table)
        {
            var lista = new List<NovedadesDto>();
            foreach (DataRow row in table.Rows)
                lista.Add(new NovedadesDto(row));
            return lista;
        }
    }
}
