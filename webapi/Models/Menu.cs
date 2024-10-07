using System.Data;

namespace cndcAPI.Models
{
    public class Menu
    {



        private static string _id = "ID";
        private static string _menu = "MENU";
        private static string _url_menu = "URL_MENU";
        private static string _submenu = "SUBMENU";
        private static string _url_submenu = "URL_SUBMENU";
        private static string _rol = "ROL";



        public decimal Id { get; set; }
        public string MMenu { get; set; }
        public string UrlMenu { get; set; }
        public string SubMenu { get; set; }
        public string UrlSubmenu { get; set; }
        public int Rol { get; set; }


        public Menu(DataRow row)
        {
            Id = (decimal)row[Menu._id];
            MMenu = (string)row[Menu._menu];
            UrlMenu = (string)row[Menu._url_menu];
            SubMenu = (string)row[Menu._submenu];
            UrlSubmenu = (string)row[Menu._url_submenu];
            Rol = (int)(short)row[Menu._rol];
        }

        public static List<Menu> FromDataTable(DataTable table)
        {
            List<Menu> lista = new List<Menu>();
            foreach (DataRow row in table.Rows)
            {
                lista.Add(new Menu(row));
            }
            return lista;
        }
    }
}
