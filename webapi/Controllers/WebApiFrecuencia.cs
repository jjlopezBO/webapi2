using cndcAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiFrecuencia : ControllerBase
    {
        private readonly ILogger<WebApiFrecuencia> _logger;
        private readonly HttpClient _httpClient;

        public WebApiFrecuencia(ILogger<WebApiFrecuencia> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet(Name = "WebApiFrecuencia")]
        public async Task<IActionResult> GetAsync()
        {
            var internalApiUrl = "http://192.168.5.35:8080/frequency";

            try
            {
                _logger.LogInformation("Consultando API interna en: {url}", internalApiUrl);

                var response = await _httpClient.GetAsync(internalApiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Respuesta recibida correctamente desde la API interna.");

                return Content(content, "application/json");
                // Alternativamente:
                // return Ok(JsonConvert.DeserializeObject(content)); si quieres deserializar
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al consultar la API interna");
                return StatusCode(500, $"Error al obtener datos de la API interna: {ex.Message}");
            }
        }
        [HttpGet("historial", Name = "WebApiFrecuencia2")]
        public async Task<IActionResult> GetAsync2(int registros)
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiFrecuenciaInstantanea...");

                DataTable table;

                // Usar una instancia temporal de Oracle
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync("pkgaweb2.tr_frecuencia");
                }

                _logger.LogInformation("Consulta de WebApiFrecuenciaInstantanea completada con éxito.");

                // Convertir DataTable a DTO
                var result = Frecuencia.FromDataTable(table).Take(registros).ToList(); ;

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
