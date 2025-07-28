using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WebApiRTransferencias : Controller
    {
        private readonly ILogger<WebApiFechas> _logger;

        public WebApiRTransferencias(ILogger<WebApiFechas> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiRTransferencias")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiRTransferencias...");

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkgaweb2.tr_transferencia");
                }

                _logger.LogInformation("Consulta de WebApiFrecuencia completada con éxito.");

                // Convertir DataTable a DTO
                var result = Transferencias.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultarWebApiFrecuencia.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}