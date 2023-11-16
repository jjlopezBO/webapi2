using cndcAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace cndcAPI.Oracle
{
    public class UserOracle 
    {

        private static UserOracle instance = null;
        public static UserOracle Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserOracle();
                }
                
                return instance;
            }
        }
        public User GetUser(UserDto request)
        {
            
            User rtn = null;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            DataTable table = new DataTable();
            using (OracleCommand cmd = Oracle.Instance.GetCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pkgapiv2.validar_usuario";
                cmd.Parameters.Add("email", OracleDbType.Varchar2, request.Username, ParameterDirection.Input);

                cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                using (OracleDataAdapter ada = new OracleDataAdapter(cmd))
                {
                    try
                    {
                        ada.Fill(table);


                        if (table.Rows.Count > 0)
                        {

                              rtn = User.FromTable(table.Rows[0]);
                            if (!BCrypt.Net.BCrypt.Verify(request.Password, rtn.PasswordHash))
                            {
                                rtn = null;
                            }
                        }
                    }
                    catch (System.Exception exception)
                    {

                        Console.WriteLine(exception.ToString());
                    }

                }
            }

            return rtn;
        }

        public User RegisterUser(UserDto request)
        {
          
            User rtn = null;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            DataTable table = new DataTable();
            using (OracleCommand cmd = Oracle.Instance.GetCommand())
            {

                /*   PROCEDURE registrar_usuario(
           email IN VARCHAR2,
           pasword_h in varchar2,
           results OUT SYS_REFCURSOR
       );*/
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pkgapiv2.registrar_usuario";
                cmd.Parameters.Add("email", OracleDbType.Varchar2, request.Username, ParameterDirection.Input);
                cmd.Parameters.Add("pasword_h", OracleDbType.Varchar2, passwordHash, ParameterDirection.Input);

                cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                using (OracleDataAdapter ada = new OracleDataAdapter(cmd))
                {
                    try
                    {
                        ada.Fill(table);


                        if (table.Rows.Count > 0)
                        {

                            // rtn = User.GetUser(table.Rows[0]);
                            if (!BCrypt.Net.BCrypt.Verify(request.Password, rtn.PasswordHash))
                            {
                                rtn = null;
                            }
                        }
                    }
                    catch (System.Exception exception)
                    {

                        Console.WriteLine(exception.ToString());
                    }

                }
            }

            return rtn;
        }
    }
}
