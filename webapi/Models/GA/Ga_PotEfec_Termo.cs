using System;
using System.Collections.Generic;
using System.Data;

namespace cndcAPI.Models.GA
{
    public class Ga_PotEfec_Termo
    {
        private static readonly string _pkCodPeriodo = "PK_COD_PERIODO";
        private static readonly string _pkPeriodoPot = "PK_PERIODO_POT";
        private static readonly string _nombre = "NOMBRE";
        private static readonly string _potEfectiva = "POTEFECTIVA";
        private static readonly string _perdidas = "PERDIDAS";
        private static readonly string _indo = "INDO";
        private static readonly string _potEfectivaBorne = "POTEFECTIVABORNE";
        
        public long PK_COD_PERIODO { get; set; }
        public long PK_PERIODO_POT { get; set; }
        public string NOMBRE { get; set; }
        public decimal POTEFECTIVA { get; set; }
        public decimal PERDIDAS { get; set; }
        public decimal INDO { get; set; }
        public decimal POTEFECTIVABORNE { get; set; }

        

        public Ga_PotEfec_Termo(DataRow row)
        {
            PK_COD_PERIODO = Convert.ToInt64(row[_pkCodPeriodo]);
            PK_PERIODO_POT = Convert.ToInt64(row[_pkPeriodoPot]);
            NOMBRE = row.IsNull(_nombre) ? string.Empty : row[_nombre]?.ToString() ?? string.Empty;
            POTEFECTIVA = Convert.ToDecimal(row[_potEfectiva]);
            PERDIDAS = Convert.ToDecimal(row[_perdidas]);
            INDO = Convert.ToDecimal(row[_indo]);
            POTEFECTIVABORNE = Convert.ToDecimal(row[_potEfectivaBorne]);
        }

        public static List<Ga_PotEfec_Termo> FromDataTable(DataTable table)
        {
            List<Ga_PotEfec_Termo> lista = new List<Ga_PotEfec_Termo>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new Ga_PotEfec_Termo(row));
            }
            return lista;
        }
    }
}
