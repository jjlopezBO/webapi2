using cndcAPI.Models.GA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace cndcAPI.Controllers.ga
{
    [ApiController]
    [Route("[controller]")]
    public class ga_pot_firme_unid_termo : Controller
    {
        private readonly ILogger<ga_pot_firme_unid_termo> _logger;

        public ga_pot_firme_unid_termo(ILogger<ga_pot_firme_unid_termo> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ga_pot_firme_unid_termo")]
        public async Task<IActionResult> GetAsync(string periodo, string subperiodo)
        {
            try
            {
                _logger.LogInformation("Iniciando ga_pot_firme_unid_termo...");

                DataTable table;
                long periodoLong = long.Parse(periodo);
                long subperiodoLong = long.Parse(subperiodo);


                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkg_apiga.ga_pot_firme_unid_termo",periodoLong,subperiodoLong);
                }

                _logger.LogInformation("Consulta de ga_pot_firme_unid_termo completada con éxito.");
                
                // Convertir DataTable a DTO
                var result = Ga_PotFirme_Unid_Termo.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ga_pot_firme_unid_termo.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }

        public static string GenerarClaseDesdeDataTable(DataTable tabla, string nombreClase)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {nombreClase}");
            sb.AppendLine("{");

            foreach (DataColumn col in tabla.Columns)
            {
                string tipo = MapearTipo(col.DataType);
                string nombre = col.ColumnName;
                sb.AppendLine($"    public {tipo} {nombre} {{ get; set; }}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string MapearTipo(Type tipo)
        {
            if (tipo == typeof(string)) return "string";
            if (tipo == typeof(int)) return "int";
            if (tipo == typeof(long)) return "long";
            if (tipo == typeof(decimal)) return "decimal";
            if (tipo == typeof(double)) return "double";
            if (tipo == typeof(float)) return "float";
            if (tipo == typeof(bool)) return "bool";
            if (tipo == typeof(DateTime)) return "DateTime";
            if (tipo == typeof(byte[])) return "byte[]";
            return "object"; // tipo por defecto
        }
    }
}