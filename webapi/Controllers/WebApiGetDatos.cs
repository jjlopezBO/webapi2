using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiGetDatos : Controller
    {
        private readonly ILogger<WebApiGetDatos> _logger;

        private static readonly string Reporte = "pkgapiv2.tr_generacion_datos";

        public WebApiGetDatos(ILogger<WebApiGetDatos> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiGeneracion")]
        public async Task<IActionResult> GetAsync(string Fecha, int intervalo)
        {
            try
            {
                // Validar parámetros
                if (string.IsNullOrWhiteSpace(Fecha))
                {
                    Fecha = "4.12.2020"; // Fecha predeterminada
                    _logger.LogInformation("No se proporcionó una fecha. Usando fecha predeterminada.");
                }

                if (intervalo < 0)
                {
                    _logger.LogWarning("Intervalo inválido: {Intervalo}", intervalo);
                    return BadRequest("Intervalo debe ser mayor a 0.");
                }

                // Parsear la fecha
                if (!DateTime.TryParse(Fecha, out var fechad))
                {
                    _logger.LogWarning("Formato de fecha inválido: {Fecha}", Fecha);
                    return BadRequest("Formato de fecha inválido.");
                }

                _logger.LogInformation("Ejecutando reporte {Reporte} con fecha {Fecha} y intervalo {Intervalo}", Reporte, fechad, intervalo);

                DataTable table;

                // Usar instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync(Reporte, fechad, intervalo);
                }

                _logger.LogInformation("Reporte {Reporte} ejecutado con éxito.", Reporte);

                // Convertir DataTable a DTO
                var result = GeneracionDatos.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar el reporte {Reporte} con fecha {Fecha} y intervalo {Intervalo}", Reporte, Fecha, intervalo);
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}
