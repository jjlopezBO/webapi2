using System;
using System.Collections.Generic;
using System.Data;

namespace cndcAPI.Models
{
    public class Ga_PotFirme_Unid_Hidro
    {
        private static readonly string _pkCodPeriodo = "PK_COD_PERIODO";
        private static readonly string _pkPeriodoPot = "PK_PERIODO_POT";
        private static readonly string _nodo = "NODO";
        private static readonly string _nombre = "NOMBRE";
        private static readonly string _potFirme = "POTFIRME";
        private static readonly string _potDes = "POTDES";
        private static readonly string _resFria = "RESFRIA";

        public long PK_COD_PERIODO { get; set; }
        public long PK_PERIODO_POT { get; set; }
        public string NODO { get; set; }
        public string NOMBRE { get; set; }
        public decimal POTFIRME { get; set; }
        public decimal POTDES { get; set; }
        public decimal RESFRIA { get; set; }

        public Ga_PotFirme_Unid_Hidro(DataRow row)
        {
            PK_COD_PERIODO = Convert.ToInt64(row[_pkCodPeriodo]);
            PK_PERIODO_POT = Convert.ToInt64(row[_pkPeriodoPot]);
            NODO = row[_nodo] == DBNull.Value || row[_nodo] is null ? string.Empty : row[_nodo]!.ToString()!;
            NOMBRE = row[_nombre] == DBNull.Value || row[_nombre] is null ? string.Empty : row[_nombre]!.ToString()!;
            POTFIRME = Convert.ToDecimal(row[_potFirme]);
            POTDES = Convert.ToDecimal(row[_potDes]);
            RESFRIA = Convert.ToDecimal(row[_resFria]);
        }

        public static List<Ga_PotFirme_Unid_Hidro> FromDataTable(DataTable table)
        {
            List<Ga_PotFirme_Unid_Hidro> lista = new List<Ga_PotFirme_Unid_Hidro>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new Ga_PotFirme_Unid_Hidro(row));
            }
            return lista;
        }
    }
}
