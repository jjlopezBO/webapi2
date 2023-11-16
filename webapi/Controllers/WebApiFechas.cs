using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiFechas : Controller
    {
        private readonly ILogger<WebApi> _logger;


        public WebApiFechas(ILogger<WebApi> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "WebApiFechas"), Authorize(Roles = "User")]
        public IEnumerable<FechasDto> Get()
        {
          


       
            DataTable table = Oracle.Oracle.Instance.Execute("pkgapiv2.getfechas");


            return FechasDto.FromDataTable(table);
        }

    }
}

