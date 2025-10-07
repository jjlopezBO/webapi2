using Microsoft.AspNetCore.Mvc;
using System.Data;



namespace cndcAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WebApiKpi : Controller
    {
        private readonly ILogger<WebApiKpi> _logger;
        private readonly IConfiguration _configuration;

        public WebApiKpi(ILogger<WebApiKpi> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "WebApiKpi")]
        public IActionResult Get(string kpiCode)
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiKpi...");

                if (string.IsNullOrWhiteSpace(kpiCode))
                {
                    return BadRequest("Parámetro kpiCode es requerido.");
                }

                
                var section = _configuration.GetSection($"Kpi:Valores:{kpiCode}");
                var valor = section["Valor"];
                var texto = section["Texto"];

                if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(texto))
                {
                    _logger.LogWarning("KPI {kpiCode} no encontrado en configuración.", kpiCode);
                    return NotFound($"KPI {kpiCode} no encontrado.");
                }

                _logger.LogInformation("KPI {kpiCode} encontrado con valor: {valor} y descripción: {texto}", kpiCode, valor, texto);
                return Ok(new { kpiCode, valor, descripcion = texto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en WebApiKpi.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}