using cndcAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;



namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiNovedades : Controller
    {
        private readonly ILogger<WebApiFechas> _logger;

        public WebApiNovedades(ILogger<WebApiFechas> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiNovedades")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiNovedades...");

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkgapiv2.pd_novedades");
                }

                _logger.LogInformation("Consulta de WebApiNoticias completada con éxito.");

                // Convertir DataTable a DTO
                var result = NovedadesDto.FromDataTable(table);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al WebApiNovedades.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }





    }
}