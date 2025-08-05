using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cndcAPI.Models.GA
{
    public class Ga_PotFirme_Unid_Termo
    {
  private static readonly string _pkCodPeriodo = "PK_COD_PERIODO";
    private static readonly string _pkPeriodoPot = "PK_PERIODO_POT";
    private static readonly string _nombre = "NOMBRE";
    private static readonly string _potFirme = "POTFIRME";
    private static readonly string _potDes = "POTDES";
    private static readonly string _resFria = "RESFRIA";
    private static readonly string _ppg = "PPG";
    private static readonly string _compensaUbicacion = "COMPENSA_UBICACION";
    private static readonly string _regimenOperacion = "REGIMEN_OPERACION";

    public long PK_COD_PERIODO { get; set; }
    public long PK_PERIODO_POT { get; set; }
    public string NOMBRE { get; set; }
    public decimal POTFIRME { get; set; }
    public decimal POTDES { get; set; }
    public decimal RESFRIA { get; set; }
    public decimal PPG { get; set; }
    public decimal COMPENSA_UBICACION { get; set; }
    public string REGIMEN_OPERACION { get; set; }


           public Ga_PotFirme_Unid_Termo(DataRow row)
        {
            PK_COD_PERIODO = Convert.ToInt64(row[_pkCodPeriodo]);
            PK_PERIODO_POT = Convert.ToInt64(row[_pkPeriodoPot]);
            NOMBRE = row[_nombre] == DBNull.Value || row[_nombre] is null ? string.Empty : row[_nombre]!.ToString()!;
            POTFIRME = Convert.ToDecimal(row[_potFirme]);
            POTDES = Convert.ToDecimal(row[_potDes]);
            RESFRIA = Convert.ToDecimal(row[_resFria]);
            PPG = Convert.ToDecimal(row[_ppg]);
            COMPENSA_UBICACION = Convert.ToDecimal(row[_compensaUbicacion]);
            REGIMEN_OPERACION = row[_regimenOperacion] == DBNull.Value || row[_regimenOperacion] is null ? string.Empty : row[_regimenOperacion]!.ToString()!;
        }

    public static List<Ga_PotFirme_Unid_Termo> FromDataTable(DataTable table)
    {
        List<Ga_PotFirme_Unid_Termo> lista = new List<Ga_PotFirme_Unid_Termo>();
        foreach (DataRow row in table.Rows)
        {
            lista.Add(new Ga_PotFirme_Unid_Termo(row));
        }
        return lista;
    }
}
}
