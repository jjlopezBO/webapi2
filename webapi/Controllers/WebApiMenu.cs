using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;


namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiMenu : Controller
    {
        private readonly ILogger<WebApi> _logger;

        private static string Reporte = "pkgapiv2.get_menu";

        public WebApiMenu(ILogger<WebApi> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiMenu") ]
        public IEnumerable<Menu> Get()
        {




            DataTable table = Oracle.Oracle.Instance.Execute(Reporte);


            return Menu.FromDataTable(table);
        }
    }
}
