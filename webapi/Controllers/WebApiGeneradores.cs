using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;
using System.Text.Json;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiGeneradores : Controller
    {
        private readonly ILogger<WebApiFechas> _logger;

        public WebApiGeneradores(ILogger<WebApiFechas> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiGeneradores")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiGeneradores...");

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkgaweb2.tr_generacion");
                }

                _logger.LogInformation("Consulta de WebApiGeneradores completada con éxito.");

                // Convertir DataTable a DTO
                var result = Generador.FromDataTable(table);

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

