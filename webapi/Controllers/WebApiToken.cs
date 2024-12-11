using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;
 
using System.Threading.Tasks;
 

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiToken : Controller
    {
        private static readonly string[] Reportes =
        {
            "pkgapiv2.exp_generacion",
            "pkgapiv2.exp_transmision"
        };

        private readonly ILogger<WebApiToken> _logger;

        public WebApiToken(ILogger<WebApiToken> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiToken")]
        public async Task<IActionResult> GetAsync(int code)
        {
            try
            {
                // Validar el índice del reporte
                if (code < 0 || code >= Reportes.Length)
                {
                    _logger.LogWarning("Código de reporte inválido: {Code}", code);
                    return BadRequest("Código de reporte inválido.");
                }

                _logger.LogInformation("Solicitando reporte: {Reporte}", Reportes[code]);

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync(Reportes[code]);
                }

                _logger.LogInformation("Reporte '{Reporte}' ejecutado con éxito.", Reportes[code]);

                // Convertir DataTable a DTO
                var result = NovedadesDto.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la solicitud para el código de reporte {Code}", code);
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}
