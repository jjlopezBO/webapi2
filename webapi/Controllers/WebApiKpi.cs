using Microsoft.AspNetCore.Mvc;
using System.Data;



namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiKpi : Controller
    {
        private readonly ILogger<WebApiKpi> _logger;
        private Dictionary<string, (string Valor, string Texto)> _kpiValores =
       new Dictionary<string, (string, string)>
   {
    { "dem_max", ("1,752.02 MW", "25-SEP-2024 Hrs. 19:30") },
    { "cap_total", ("3,573.56 MW", "May-2025") },
    { "gen_renovable", ("1,101.96 GWh", "Mar-2025") },
    { "gen_anual", ("1,101.96 GWh", "Mar-2025") }
   };
        public WebApiKpi(ILogger<WebApiKpi> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiKpi")]
        public async Task<IActionResult> GetAsync(string kpiCode)
        {
            try
            {
                _logger.LogInformation("Iniciando WebApiKpi...");
                if (_kpiValores.TryGetValue(kpiCode, out var dato))
                {
                    _logger.LogInformation($"KPI {kpiCode} encontrado con valor: {dato.Valor} y descripción: {dato.Texto}");
                    return Ok(new { kpiCode, valor = dato.Valor, descripcion = dato.Texto });
                }
                else
                {
                    _logger.LogWarning($"KPI {kpiCode} no encontrado.");
                    return NotFound($"KPI {kpiCode} no encontrado.");
                }
                //DataTable table;

                //// Usar una instancia temporal de Oracle
                //using (var oracle = new Oracle.Oracle())
                //{
                //    table = await oracle.ExecuteAsync("pkgaweb2.tr_noticas");
                //}

                //_logger.LogInformation("Consulta de WebApiNoticias completada con éxito.");

                //// Convertir DataTable a DTO
                //var result = Noticia.FromDataTable(table);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al WebApiKpi.");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }
    }
}