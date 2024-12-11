using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiLineas : Controller
    {
        private readonly ILogger<WebApiLineas> _logger;

        private static readonly string Reporte = "pkgapiv2.ds_lineas_trans";

        public WebApiLineas(ILogger<WebApiLineas> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiLineas")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Solicitando el reporte de líneas de transmisión: {Reporte}", Reporte);

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync(Reporte);
                }

                _logger.LogInformation("Reporte de líneas de transmisión ejecutado con éxito.");

                // Convertir DataTable a DTO
                var result = LineasDto.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la solicitud para el reporte de líneas de transmisión.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}
