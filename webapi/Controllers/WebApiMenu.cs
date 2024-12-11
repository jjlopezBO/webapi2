using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiMenu : Controller
    {
        private readonly ILogger<WebApiMenu> _logger;

        private static readonly string Reporte = "pkgapiv2.get_menu";

        public WebApiMenu(ILogger<WebApiMenu> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiMenu")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Solicitando menú con el reporte: {Reporte}", Reporte);

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync(Reporte);
                }

                _logger.LogInformation("Reporte de menú ejecutado con éxito.");

                // Convertir DataTable a DTO
                var result = Menu.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la solicitud para el reporte de menú.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}
