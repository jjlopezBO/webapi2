using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;


namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiGetDatos : Controller
    {
        private readonly ILogger<WebApi> _logger;

        private static string Reporte = "pkgapiv2.tr_generacion_datos" ;

        public WebApiGetDatos(ILogger<WebApi> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiGeneracion"), Authorize(Roles = "User")]
        public IEnumerable<GeneracionDatos> Get(  string Fecha, int intervalo)
        {
            string fecha = "4.12.2020";
            Console.WriteLine(Reporte);
            if (Fecha.Length == 0)
            { fecha = "4.12.2020"; }
            else
            { fecha = Fecha; }



            DateTime fechad = Oracle.Helper.Instance.formatDate(fecha.ToString());

            DataTable table = Oracle.Oracle.Instance.ExecuteFecha(Reporte, fechad, intervalo);


            return GeneracionDatos.FromDataTable(table);
        }
    }
}

