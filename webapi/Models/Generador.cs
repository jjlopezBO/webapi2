using System.Data;
using System.Text.Json.Serialization;

namespace cndcAPI.Models
{
    public class Generador
    {
        [JsonPropertyName("cx")]
        public double Cx { get; set; }

        [JsonPropertyName("cy")]
        public double Cy { get; set; }

        [JsonPropertyName("r")]
        public int Radius { get; set; }

        [JsonPropertyName("fill")]
        public string Fill { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("dataInfo")]
        public string DataInfo { get; set; }

        [JsonPropertyName("potencia_activa")]
        public string PotenciaActiva { get; set; }

        [JsonPropertyName("potencia_reactiva")]
        public string PotenciaReactiva { get; set; }

        [JsonPropertyName("capacidad_instalada")]
        public string CapacidadInstalada { get; set; }

        public static List<Generador> FromDataTable(DataTable table)
        {
            var lista = new List<Generador>();

            foreach (DataRow row in table.Rows)
            {
                var item = new Generador
                {
                    Cx = Convert.ToDouble(row["i_x"]),
                    Cy = Convert.ToDouble(row["i_y"]),
                    Radius = Convert.ToInt32(row["r"]),
                    Fill = row["fill"]?.ToString(),
                    Class = row["class"]?.ToString(),
                    DataInfo = row["nombre_corto"]?.ToString(),

                    PotenciaActiva = $"{Convert.ToDouble(row["valor_p"]):F2} MW",
                    PotenciaReactiva = $"{Convert.ToDouble(row["valor_q"]):F2} MVAR",

                    CapacidadInstalada = $"0 MW"
                };

                lista.Add(item);
            }

            return lista;
        }
    }


}
