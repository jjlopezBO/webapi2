using System.Data;

namespace cndcAPI.Models.GA
{
    public class Ga_Subperiodos
    {
        private static readonly string _pkCodPeriodo = "PK_COD_PERIODO";
        private static readonly string _pkPeriodoPot = "PK_PERIODO_POT";
        private static readonly string _mesInicio = "MES_INICIO";
        private static readonly string _anioInicio = "ANIO_INICIO";
        private static readonly string _deFecha = "DEFECHA";
        private static readonly string _aFecha = "AFECHA";
        private static readonly string _observaciones = "OBSERVACIONES";

        public long PK_COD_PERIODO { get; set; }
        public long PK_PERIODO_POT { get; set; }
        public object MES_INICIO { get; set; }
        public object ANIO_INICIO { get; set; }
        public DateTime DEFECHA { get; set; }
        public DateTime AFECHA { get; set; }
        public string OBSERVACIONES { get; set; }

        public Ga_Subperiodos(DataRow row)
        {
            PK_COD_PERIODO = Convert.ToInt64(row[_pkCodPeriodo]);
            PK_PERIODO_POT = Convert.ToInt64(row[_pkPeriodoPot]);
            MES_INICIO = row[_mesInicio];
            ANIO_INICIO = row[_anioInicio];
            DEFECHA = Convert.ToDateTime(row[_deFecha]);
            AFECHA = Convert.ToDateTime(row[_aFecha]);
            OBSERVACIONES = row[_observaciones] == DBNull.Value || row[_observaciones] == null ? string.Empty : row[_observaciones]!.ToString()!;
        }

        public static List<Ga_Subperiodos> FromDataTable(DataTable table)
        {
            List<Ga_Subperiodos> lista = new List<Ga_Subperiodos>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new Ga_Subperiodos(row));
            }
            return lista;
        }
    }
}
