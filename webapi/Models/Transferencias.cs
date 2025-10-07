using System.Data;

namespace cndcAPI.Models
{


    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Animation
    {
        public string Duration { get; set; }
        public bool Invert { get; set; }
        public List<string> Begin { get; set; }
    }

    public class Transferencias
    {

      
        public string Id { get; set; }
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public string Inject { get; set; }
        public string flujo_activo { get; set; }
        public string flujo_reactivo { get; set; }
        public string Info { get; set; }
        public Animation Animation { get; set; }

        public Transferencias(DataRow row)
        {
            // nombre_corto, info, i_x,column2 i_y, f_x,f_

            // y , a.v,type
            decimal v = (decimal)row["Activa"];
            bool s = true;
            if (v < 0)
            {
                s = false;
            }
            Id = (string) row["nombre_corto"];
            Start = new Coordinate { X = (float)row["I_X"], Y = (float)row["I_Y"] };
            End = new Coordinate { X = (float)row["F_X"], Y = (float)row["F_Y"] };
            Inject = string.Format ("X");
            flujo_activo = string.Format("{0} [MW]", decimal.Abs((decimal)row["Activa"]));
            flujo_reactivo = string.Format("{0} [MVar]", decimal.Abs((decimal)row["reactiva"]));
            Info = (string)row["info"];
            Animation = new Animation
            {
                Duration = "2s",
                Invert = s,
                Begin = new List<string> { "0s", "0.2s", "0.4s" }
            };
            
            // Puedes agregar más objetos según sea necesario
       

        }
        public static List<Transferencias> FromDataTable(DataTable table)
        {
            List<Transferencias> lista = new List<Transferencias>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new Transferencias(row));
            }
            return lista;
        }
    }
    
}
