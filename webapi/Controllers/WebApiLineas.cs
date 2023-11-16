using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;


namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiLineas : Controller
    {
		 
        private readonly ILogger<WebApi> _logger;

        private static string Reporte = "pkgapiv2.ds_lineas_trans";

        public WebApiLineas(ILogger<WebApi> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiLineas"), Authorize(Roles = "User")]
        public IEnumerable<LineasDto> Get( )
        {
             

            

            DataTable table = Oracle.Oracle.Instance.Execute(Reporte);


            return LineasDto.FromDataTable(table);
        }
    }
}

