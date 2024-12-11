using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiFechas : Controller
    {
        private readonly ILogger<WebApiFechas> _logger;

        public WebApiFechas(ILogger<WebApiFechas> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiFechas")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de fechas...");

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkgapiv2.getfechas");
                }

                _logger.LogInformation("Consulta de fechas completada con éxito.");

                // Convertir DataTable a DTO
                var result = FechasDto.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar las fechas.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}
