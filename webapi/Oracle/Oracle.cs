using cndcAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace cndcAPI.Oracle
{
    public class Oracle
    {

        protected  OracleConnection con;
        protected Oracle()
        {
            Reconnect();

        }
        protected void Reconnect()
        {
            string conString = "User Id = spectrum ; password = spectrum; Data Source = 192.168.2.13:1521 /orcl.cndc.bo;Min Pool Size=5;Connection Lifetime=100000;Connection Timeout=60;Incr Pool Size=5; Decr Pool Size=2";
            con = new OracleConnection();
            con.ConnectionString = conString;
            con.Open();
        }

        private static Oracle instance = null;
        public static Oracle Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Oracle();
                }
                else
                {
                    if (instance.con.State != System.Data.ConnectionState.Open)
                    {
                        instance.con.Close();
                        instance.con.Dispose();
                        instance.Reconnect();

                    }

                }
                return instance;
            }
        }

        protected bool ValidarConection()
        {
            bool rtn = false;

            if (con == null)
            {
                Reconnect();
            }
            else
            {
                if (con.State != ConnectionState.Open)
                {
                    try
                    {
                        con.Dispose();
                        con = null;
                        Reconnect();
                        rtn = true;
                    }
                    catch (Exception)
                    {


                    }

                }

            }

            return rtn;
        }


        public OracleCommand GetCommand()
        {
            ValidarConection();
            return con.CreateCommand();
        }
        public DataTable Execute(string procedimiento)
        {
            ValidarConection();
            DataTable table = new DataTable();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedimiento;

                cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                using (OracleDataAdapter ada = new OracleDataAdapter(cmd))
                {
                    try
                    {
                        ada.Fill(table);
                    }
                    catch (System.Exception exception)
                    {

                        Console.WriteLine(exception.ToString());
                    }

                }
            }

            return table;
        }

        public DataTable ExecuteFecha(string procedimiento, DateTime fecham)
        {
            ValidarConection();
            DataTable table = new DataTable();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedimiento;
                cmd.Parameters.Add("pfecha", OracleDbType.Date, fecham, ParameterDirection.Input);

                cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                using (OracleDataAdapter ada = new OracleDataAdapter(cmd))
                {
                    try
                    {
                        ada.Fill(table);
                    }
                    catch (System.Exception exception)
                    {

                        Console.WriteLine(exception.ToString());
                    }

                }
            }

            return table;
        }
        public DataTable ExecuteFecha(string procedimiento, DateTime fecham, int intervalom)
        {
            ValidarConection();
            DataTable table = new DataTable();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedimiento;
                cmd.Parameters.Add("pfecha", OracleDbType.Date, fecham, ParameterDirection.Input);
                cmd.Parameters.Add("pintervalo", OracleDbType.Int16, intervalom, ParameterDirection.Input);

                cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                using (OracleDataAdapter ada = new OracleDataAdapter(cmd))
                {
                    try
                    {
                        ada.Fill(table);
                    }
                    catch (System.Exception exception)
                    {

                        Console.WriteLine(exception.ToString());
                    }

                }
            }

            return table;
        }

    }
}
