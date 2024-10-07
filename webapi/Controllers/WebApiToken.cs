using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;


namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiToken : Controller
    {
        private static string[] Reportes = {  "pkgapiv2.exp_generacion", "pkgapiv2.exp_transmision" };
        private readonly ILogger<WebApi> _logger;

        public WebApiToken(ILogger<WebApi> logger)
        {
            _logger = logger;
        }

          [HttpGet(Name = "WebApiToken"), Authorize(Roles = "User")]         
        public IEnumerable<NovedadesDto> Get(int code)
        {

            Console.WriteLine(Reportes[code]);




            DataTable table = Oracle.Oracle.Instance.Execute(Reportes[code]);





            return NovedadesDto.FromDataTable(table);
        }


    }
}

