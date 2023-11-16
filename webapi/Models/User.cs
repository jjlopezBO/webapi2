using System.Data;

namespace cndcAPI.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public short Rol { get; set; } = -1; //0 -> USER ; 1-> Admin
        public static User FromTable(DataRow row)
        {
            User user = new User(); 
            user.Username = (string)row["login"];
            user.PasswordHash = (string)row["password_hash"];
            user.Rol = (short)row["rol"];
            return user;
        }
    }
}
