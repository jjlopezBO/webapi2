using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using cndcAPI.Models;

namespace cndcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApi : Controller
    {
        private static string[] Reportes = { "pkgapiv2.tr_generacion96", "pkgapiv2.tr_demanda96", "pkgapiv2.pr_generacion_prevista",
            "pkgapiv2.pr_costo_marginal", "pkgapiv2.pr_demanda", "pkgapiv2.pd_costo_marginal",
            "pkgapiv2.pd_energia",  "pkgapiv2.tr_generacion", "pkgapiv2.tr_demanda" };
        private readonly ILogger<WebApi> _logger;

        public WebApi(ILogger<WebApi> logger)
        {
            _logger = logger;
        }
        //
        [HttpGet(Name = "WebApi")]
        public IEnumerable<ApiFormatBase> Get(int code, string Fecha)
        {
            string fecha = "4.12.2020";
            bool es96 = false;
            Console.WriteLine(Reportes[code]);
            if (Fecha.Length == 0)
            { fecha = "4.12.2020"; }
            else
            { fecha = Fecha; }


           
            DateTime fechad = Oracle.Helper.Instance.formatDate(fecha.ToString());

            DataTable table = Oracle.Oracle.Instance.ExecuteFecha(Reportes[code], fechad);
            if ((code ==0)||  (code == 1))
            { 
                es96 = true;
            }
                return ApiFormatBase.FromDataTable(table, es96);
        }

     


        


    }
}
