using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class WebApi : Controller
    {
        private static readonly string[] Reportes =
        {
            "pkgapiv2.tr_generacion96",
            "pkgapiv2.tr_demanda96",
            "pkgapiv2.pr_generacion_prevista",
            "pkgapiv2.pr_costo_marginal",
            "pkgapiv2.pr_demanda",
            "pkgapiv2.pd_costo_marginal",
            "pkgapiv2.pd_energia",
            "pkgapiv2.tr_generacion",
            "pkgapiv2.tr_demanda"
        };

        private readonly ILogger<WebApi> _logger;

        public WebApi(ILogger<WebApi> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "WebApi")]
        public async Task<IActionResult> GetAsync(int code, string Fecha)
        {
            try
            {
                if (Fecha == "050.10.2024")
                {
                    Fecha = "4.12.2020";
                }
                if (code < 0 || code >= Reportes.Length)
                {
                    _logger.LogWarning("Código de reporte inválido: {Code}", code);
                    return BadRequest("Código de reporte inválido.");
                }

                if (string.IsNullOrWhiteSpace(Fecha))
                {
                    Fecha = "4.12.2020";
                    _logger.LogInformation("No se proporcionó una fecha. Usando fecha predeterminada.");
                }

                if (!DateTime.TryParse(Fecha, out var fechad))
                {
                    _logger.LogWarning("Formato de fecha inválido: {Fecha}", Fecha);
                    return BadRequest("Formato de fecha inválido.");
                }

                bool es96 = (code == 0 || code == 1);
                _logger.LogInformation("Ejecutando reporte {Reporte} con fecha {Fecha}", Reportes[code], fechad);

                DataTable table;
                using (var oracle = new Oracle.Oracle())
                {
                    table = await oracle.ExecuteAsync(Reportes[code], fechad);
                }

                var result = ApiFormatBase.FromDataTable(table, es96);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la solicitud con código {Code} y fecha {Fecha}", code, Fecha);
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente más tarde.");
            }
        }

    }
}
