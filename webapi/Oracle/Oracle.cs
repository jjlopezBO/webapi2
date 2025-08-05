using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Serilog;

namespace cndcAPI.Oracle
{
    public class Oracle : IDisposable
    {
        private readonly string _connectionString;
        private OracleConnection _connection;
        private readonly Serilog.ILogger _logger;

        // Constructor
        public Oracle()
        {
            _connectionString = "User Id=spectrum;Password=spectrum;Data Source=192.168.2.13:1521/orcl.cndc.bo;Min Pool Size=5;Max Pool Size=50;Connection Lifetime=300;";
            _logger = Log.Logger;
            _logger.Information("Clase Oracle inicializada.");
        }

        // Método para abrir la conexión
        private async Task<OracleConnection> GetConnectionAsync()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new OracleConnection(_connectionString);
                await _connection.OpenAsync();
                _logger.Information("Conexión a la base de datos abierta.");
            }
            return _connection;
        }
        public async Task<DataTable> ExecuteAsync(string procedureName, Action<OracleParameterCollection> configureParameters)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;

                    // Configurar parámetros utilizando el delegado proporcionado
                    configureParameters?.Invoke(cmd.Parameters);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes agregar un logger aquí si lo deseas
                throw new Exception($"Error al ejecutar el procedimiento {procedureName}", ex);
            }

            return table;
        }

        public async Task<DataTable> ExecuteAsync(string procedimiento, string username)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedimiento;

                    // Agregar parámetros

                    
                    cmd.Parameters.Add("email", OracleDbType.Varchar2, username, ParameterDirection.Input);
                    cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                _logger.Information("Procedimiento '{Procedimiento}' ejecutado con éxito.", procedimiento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al ejecutar el procedimiento '{Procedimiento}'", procedimiento);
                throw;
            }

            return table;
        }

        public async Task<DataTable> ExecuteAsync(string procedimiento, long periodo , long subperiodo)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedimiento;

                    // Agregar parámetros


                    cmd.Parameters.Add("periodo_pot", OracleDbType.Long, periodo, ParameterDirection.Input);
                    cmd.Parameters.Add("subperiodo", OracleDbType.Long, subperiodo, ParameterDirection.Input);
                    cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                _logger.Information("Procedimiento '{Procedimiento}' ejecutado con éxito.", procedimiento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al ejecutar el procedimiento '{Procedimiento}'", procedimiento);
                throw;
            }

            return table;
        }


        public async Task<DataTable> ExecuteAsync(string procedimiento, DateTime fecha, int intervalo)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedimiento;

                    // Agregar parámetros
                    cmd.Parameters.Add("pfecha", OracleDbType.Date, fecha, ParameterDirection.Input);
                    cmd.Parameters.Add("pintervalo", OracleDbType.Int16, intervalo, ParameterDirection.Input);
                    cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                _logger.Information("Procedimiento '{Procedimiento}' ejecutado con éxito.", procedimiento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al ejecutar el procedimiento '{Procedimiento}'", procedimiento);
                throw;
            }

            return table;
        }

        // Método genérico para ejecutar un procedimiento almacenado
        public async Task<DataTable> ExecuteAsync(string procedimiento, DateTime fecha)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedimiento;

                    // Agregar parámetros
                    cmd.Parameters.Add("pfecha", OracleDbType.Date, fecha, ParameterDirection.Input);
                    cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                _logger.Information("Procedimiento '{Procedimiento}' ejecutado con éxito.", procedimiento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al ejecutar el procedimiento '{Procedimiento}'", procedimiento);
                throw;
            }

            return table;
        }

        // Método genérico para ejecutar un procedimiento almacenado
        public async Task<DataTable> ExecuteAsync(string procedimiento)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedimiento;

                    // Agregar parámetros
                    cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                _logger.Information("Procedimiento '{Procedimiento}' ejecutado con éxito.", procedimiento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al ejecutar el procedimiento '{Procedimiento}'", procedimiento);
                throw;
            }

            return table;
        }
        public async Task<DataTable> ExecuteAsync(string procedimiento , int n)
        {
            DataTable table = new DataTable();

            try
            {
                var connection = await GetConnectionAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedimiento;

                    // Agregar parámetros
                    // Agregar parámetros
                    cmd.Parameters.Add("n", OracleDbType.Int64,n, ParameterDirection.Output);
                    cmd.Parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                _logger.Information("Procedimiento '{Procedimiento}' ejecutado con éxito.", procedimiento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al ejecutar el procedimiento '{Procedimiento}'", procedimiento);
                throw;
            }

            return table;
        }

        // Implementación de IDisposable
        public void Dispose()
        {
            if (_connection != null)
            {
                try
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                        _logger.Information("Conexión a la base de datos cerrada.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error al cerrar la conexión a la base de datos.");
                }
                finally
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}
