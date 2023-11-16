using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebNoDate : Controller
    {
        private static string[] Reportes = { "pkgapiv2.pd_novedades", "pkgapiv2.ds_capacidad_efectiva", "pkgapiv2.ds_capacidad_efectiva_resumen", "pkgapiv2.ds_lineas_trans" , "pkgapiv2.exp_transmision" , "pkgapiv2.exp_generacion" };
        private readonly ILogger<WebApi> _logger;

        public WebNoDate(ILogger<WebApi> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "WebNoDate"), Authorize(Roles = "User")]
        public IEnumerable<NovedadesDto> Get(int code )
        {
           
            Console.WriteLine(Reportes[code]);
             


           
            DataTable table = Oracle.Oracle.Instance.Execute(Reportes[code] );

           

            

                return NovedadesDto.FromDataTable(table);
        }

      
    }
}

