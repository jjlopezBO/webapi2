using cndcAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Threading.Tasks;

namespace cndcAPI.Oracle
{
    public class UserOracle : IDisposable
    {
        private readonly Oracle _oracle;

        // Constructor: Crea una instancia de Oracle
        public UserOracle()
        {
            _oracle = new Oracle();
        }

        // Método para obtener un usuario
        public async Task<User> GetUserAsync(UserDto request)
        {
            User rtn = null;
            DataTable table = new DataTable();

            try
            {
                // Ejecutar procedimiento almacenado
                table = await _oracle.ExecuteAsync("pkgapiv2.validar_usuario", parameters =>
                {
                    parameters.Add("email", OracleDbType.Varchar2, request.Username, ParameterDirection.Input);
                    parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                });

                // Procesar resultados
                if (table.Rows.Count > 0)
                {
                    rtn = User.FromTable(table.Rows[0]);

                    // Verificar la contraseña
                    if (!BCrypt.Net.BCrypt.Verify(request.Password, rtn.PasswordHash))
                    {
                        rtn = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes usar un logger aquí si es necesario
                throw new Exception("Error al obtener el usuario.", ex);
            }

            return rtn;
        }

        // Método para registrar un usuario
        public async Task<User> RegisterUserAsync(UserDto request)
        {
            User rtn = null;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            DataTable table = new DataTable();

            try
            {
                // Ejecutar procedimiento almacenado
                table = await _oracle.ExecuteAsync("pkgapiv2.registrar_usuario", parameters =>
                {
                    parameters.Add("email", OracleDbType.Varchar2, request.Username, ParameterDirection.Input);
                    parameters.Add("pasword_h", OracleDbType.Varchar2, passwordHash, ParameterDirection.Input);
                    parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);
                });

                // Procesar resultados
                if (table.Rows.Count > 0)
                {
                    rtn = User.FromTable(table.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                // Puedes usar un logger aquí si es necesario
                throw new Exception("Error al registrar el usuario.", ex);
            }

            return rtn;
        }

        // Implementación de IDisposable
        public void Dispose()
        {
            // Liberar recursos asociados a Oracle
            _oracle?.Dispose();
        }
    }
}
