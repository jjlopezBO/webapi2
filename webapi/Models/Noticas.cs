using cndcAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

public class Noticia
{
    public int id { get; set; }
    public string titulo { get; set; }
    public string contenido { get; set; }
    public DateTime fecha { get; set; }
    public string categoria { get; set; } = "general";
    public int autor { get; set; } = 1;
    public string resumen { get; set; }
    public string estado { get; set; } = "publicada";

    public static List<Noticia> ProcesarHtmlANoticias(string html, DateTime fechaFija)
    {
        var noticias = new List<Noticia>();
        var regex = new Regex(@"<u>(.*?)<\/u>\s*<br><\/br>\s*<p>(.*?)<\/p>", RegexOptions.Singleline);
        var matches = regex.Matches(html);
        int id = 1;

        foreach (Match match in matches)
        {
            string titulo = match.Groups[1].Value.Trim();
            string contenido = match.Groups[2].Value.Trim();

            noticias.Add(new Noticia
            {
                id = id++,
                titulo = titulo,
                contenido = contenido,
                fecha = fechaFija,
                resumen = contenido
            });
        }

        return noticias;
    }

    public static List<Noticia> FromDataTable(DataTable table)
    {
        string texto = string.Empty; 
        DateTime fecha = DateTime.MinValue;

        if (table.Rows.Count > 0)
        {
            texto =(string) table.Rows[0]["descripcion"];
            fecha = (DateTime)table.Rows[0]["fecha"];

        }
     
        return ProcesarHtmlANoticias(texto,fecha);
    }
}

 