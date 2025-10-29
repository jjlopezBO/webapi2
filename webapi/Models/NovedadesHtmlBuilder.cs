using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Text;
using cndcAPI.Models;


namespace cndcAPI.Models
{
    public static class NovedadesHtmlBuilder
    {

        private static string BuildDatosCorani(List<NovedadesDto> items)
        {
            
            NovedadesDto item8 = items[8];
            NovedadesDto item9 = items[9];
            string[] s8 = item8.Descripcion.Split("<br>");

            string[] s9 = item9.Descripcion.Split("<br>");

            s8 = Array.ConvertAll(s8, s => s.Trim());
            s9 = Array.ConvertAll(s9, s => s.Trim());
            var combinado = s8.Concat(s9)
    .Select(linea => $"<p><strong>{linea.Replace("=", ":</strong>")}</p>");

            // Convertir todo en una sola cadena
            string html = string.Join("", combinado);

            return html;   
        }
        public static string Build(List<NovedadesDto> items)
        {

           
            var sb = new StringBuilder();

            sb.Append(@"<div class='cndc-datos-relevantes'>");

            int i = 0;

            foreach (var it in items)
            {

                string titulo = WebUtility.HtmlEncode(it.Titulo ?? string.Empty);
                string descripcion = it.Descripcion ?? string.Empty;
                string fecha = it.Fecha.ToString("dd/MM/yyyy");

                switch (i)
                {
                    case 0 or 1:
                        if (i == 0)
                                { sb.Append(string.Format(@"  <h2>Información del día {0}</h2>", fecha));
                        }

                        sb.Append(string.Format(@"  <p><strong>{0}</strong>{1}</p>", titulo, descripcion));
             
                        break;
                    case  3 or 4 or 5 or 6 or 7 :
                             sb.Append(string.Format(@"< p >< strong > {0}</ strong >{1}) </ p > ", titulo,descripcion));
                        break;
                    default:


                        break;
                }

                 
                i++;
            }
            sb.Append(BuildDatosCorani(items));

            sb.Append("<p><em>Fuente: CNDC</em></p> </div>");
            return sb.ToString();
        }
    }
}
