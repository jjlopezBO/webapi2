using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;



namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiNoticias : Controller
    {
        private readonly ILogger<WebApiFechas> _logger;

        public WebApiNoticias(ILogger<WebApiFechas> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiNoticias")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiNoticias...");

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkgaweb2.tr_noticas");
                }

                _logger.LogInformation("Consulta de WebApiNoticias completada con éxito.");

                // Convertir DataTable a DTO
                var result = Noticia.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al WebApiFrecuenciaInstantanea.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}