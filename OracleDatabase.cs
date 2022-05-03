using API.Common.Interfaces.Database;
using API.Common.Models;
using API.Common.Models.Database;
using API.Exceptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.DatabaseHandler
{
    public class OracleAccess : IDatabase
    {
        public readonly OracleConnection _connection;

        public HandlerSettings HandlerSettings { get; }

        public OracleAccess(HandlerSettings handlerSettings)
        {
            HandlerSettings = handlerSettings;
            _connection = new OracleConnection(HandlerSettings.DbConnection);
            _connection.WalletLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wallet");
        }

        public OracleAccess(string connectionString)
        {
            _connection = new OracleConnection(connectionString);
            _connection.WalletLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wallet");
        }


        public async Task<object> ReturnAsAsync(string selectStatement, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            await EnsureConnectionAsync(cancellationToken);
            await using var command = await SetupCommandAsync(selectStatement, parameters, cancellationToken);
            return await command.ExecuteScalarAsync(cancellationToken);
        }

        public object ReturnAs(string selectStatement, Dictionary<string, object> parameters = null)
        {
            EnsureConnection();
            using var command = SetupCommand(selectStatement, parameters);
            return command.ExecuteScalar();
        }

        public async Task<IList<string>> ReturnAsListAsync(string selectStatement, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            List<string> ls = new List<string>();
            await EnsureConnectionAsync(cancellationToken);
            using var command = await SetupCommandAsync(selectStatement, parameters);
            using var dr = await command.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                ls.Add(dr[0].ToString());
            }
            return ls;
        }

        public async Task<IList<object>> ReturnAsObjectListAsync(string selectStatement, string modelName, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            List<object> ls = new List<object>();
            await EnsureConnectionAsync(cancellationToken);
            using var command = await SetupCommandAsync(selectStatement, parameters);
            using var dr = await command.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                ls.Add(DatabaseModelFactory.Create(modelName, dr));
            }
            return ls;
        }

        public async Task<object> ReturnAsObjectAsync(string selectStatement, string modelName, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            await EnsureConnectionAsync(cancellationToken);
            using var command = await SetupCommandAsync(selectStatement, parameters);
            using var dr = await command.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                return DatabaseModelFactory.Create(modelName, dr);
            }
            return null;
        }

        public async Task<Dictionary<string, object>> ReturnAsDictListAsync(string selectStatement, string[] key, string modelName, Dictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> r = new Dictionary<string, object>();
            await EnsureConnectionAsync(cancellationToken);
            using var command = await SetupCommandAsync(selectStatement, parameters);
            using var dr = await command.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                string kc = "";
                foreach (string s in key)
                {
                    if (s.Equals(key[key.Length - 1]))
                    {
                        kc += dr.GetString(dr.GetOrdinal(s));
                    }
                    else
                    {
                        kc += dr.GetString(dr.GetOrdinal(s)) + "_";
                    }
                }
                r.Add(kc, DatabaseModelFactory.Create(modelName, dr));
            }
            return r;
        }

        public IList<object> ReturnAsObjectList(string selectStatement, string modelName, Dictionary<string, object> parameters = null)
        {
            List<object> ls = new List<object>();
            EnsureConnection();
            using var command = SetupCommand(selectStatement, parameters);
            using var dr = command.ExecuteReader();

            while (dr.Read())
            {
                ls.Add(DatabaseModelFactory.Create(modelName, dr));
            }
            return ls;
        }

        public object ReturnAsObject(string selectStatement, string modelName, Dictionary<string, object> parameters = null)
        {
            EnsureConnection();
            using var command = SetupCommand(selectStatement, parameters);
            using var dr = command.ExecuteReader();
            if(dr.Read())
            {
                return DatabaseModelFactory.Create(modelName, dr);
            }
            return null;
        }

        public Dictionary<string, object> ReturnAsDictList(string selectStatement, string[] key, string modelName, Dictionary<string, object> parameters = null)
        {
            Dictionary<string, object> r = new Dictionary<string, object>();
            EnsureConnection();
            using var command = SetupCommand(selectStatement, parameters);
            using var dr = command.ExecuteReader();
            while (dr.Read())
            {
                string kc = "";
                foreach (string s in key)
                {
                    if (s.Equals(key[key.Length - 1]))
                    {
                        kc += dr.GetString(dr.GetOrdinal(s));
                    }
                    else
                    {
                        kc += dr.GetString(dr.GetOrdinal(s)) + "_";
                    }
                }
                r.Add(kc, DatabaseModelFactory.Create(modelName, dr));
            }
            return r;
        }

        public IList<string> ReturnAsList(string selectStatement, Dictionary<string, object> parameters = null)
        {
            List<string> ls = new List<string>();
            EnsureConnection();
            using var command = SetupCommand(selectStatement, parameters);
            using var dr = command.ExecuteReader();
            while (dr.Read())
            {
                ls.Add(dr[0].ToString());
            }
            return ls;
        }

        public async Task<int> InsertAsync(string tableName, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            var insertStatement = $"INSERT INTO {tableName} ({string.Join(",", parameters.Keys)}) VALUES ({string.Join(",", parameters.Keys.Select(x => ":" + x))})";
            await using var command = await SetupCommandAsync(insertStatement, parameters, cancellationToken);
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }
        public int Insert(string tableName, Dictionary<string, object> parameters)
        {
            var insertStatement = $"INSERT INTO {tableName} ({string.Join(",", parameters.Keys)}) VALUES ({string.Join(",", parameters.Keys.Select(x => ":" + x))})";
            using var command = SetupCommand(insertStatement, parameters);
            return command.ExecuteNonQuery();
        }

        public int Update(string tableName, Dictionary<string, object> parameters, Dictionary<string, object> whereParameters)
        {
            var statement = $"update {tableName} set {string.Join(", ", parameters.Keys.Select(x => x + " = :" + x))} where {string.Join(" AND ", whereParameters.Keys.Select(x => x + " = :" + x))} ";
            //Console.WriteLine("UpdateStatus: " + statement);
            using var command = SetupCommand(statement, parameters, whereParameters);
            return command.ExecuteNonQuery();
        }

        public async Task<int> UpdateAsync(string tableName, Dictionary<string, object> parameters, Dictionary<string, object> whereParameters, CancellationToken cancellationToken = default)
        {
            var statement = $"update {tableName} set {string.Join(", ", parameters.Keys.Select(x => x + " = :" + x))} where {string.Join(", ", whereParameters.Keys.Select(x => x + " = :" + x))} ";
            await using var command = await SetupCommandAsync(statement, parameters, whereParameters, cancellationToken);
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(string tableName, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            var deleteStatement = $"Delete from {tableName} where {string.Join(" ", parameters.Keys.Select(x => x + "=:" + x))} ";
            await using var command = await SetupCommandAsync(deleteStatement, parameters, cancellationToken);
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        public int Delete(string tableName, Dictionary<string, object> parameters)
        {
            var deleteStatement = $"Delete from {tableName} where {string.Join(" AND ", parameters.Keys.Select(x => x + "=:" + x))} ";
            using var command = SetupCommand(deleteStatement, parameters);
            return command.ExecuteNonQuery();

        }

        public int CustomBulkUpdate(string query, Dictionary<string, object> parameters)
        {
            using var command = SetupCommand(query, parameters);
            return command.ExecuteNonQuery();
        }

        /// <remarks>https://docs.oracle.com/database/121/TTPLS/exceptions.htm#TTPLS195</remarks>

        private async Task<OracleCommand> SetupCommandAsync(string commandStatement, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            await EnsureConnectionAsync(cancellationToken);
            var command = new OracleCommand(commandStatement, _connection);
            if (parameters == null) return command;
            foreach (var (key, value) in parameters)
            {
                command.Parameters.Add(key, value);
            }
            return command;
        }
        private async Task<OracleCommand> SetupCommandAsync(string commandStatement, Dictionary<string, object> parameters, Dictionary<string, object> whereParameters, CancellationToken cancellationToken = default)
        {
            await EnsureConnectionAsync(cancellationToken);
            var command = new OracleCommand(commandStatement, _connection);
            if (parameters != null)
            {
                foreach (var (key, value) in parameters)
                {
                    command.Parameters.Add(key, value);
                }
            }
            if (whereParameters != null)
            {
                foreach (var (key, value) in whereParameters)
                {
                    command.Parameters.Add(key, value);
                }
            }
            return command;
        }
        private OracleCommand SetupCommand(string commandStatement, Dictionary<string, object> parameters)
        {
            EnsureConnection();
            var command = new OracleCommand(commandStatement, _connection);
            if (parameters == null) return command;
            foreach (var (key, value) in parameters)
            {
                command.Parameters.Add(key, value);
            }
            return command;
        }

        private OracleCommand SetupCommand(string commandStatement, Dictionary<string, object> parameters, Dictionary<string, object> whereParameters)
        {
            EnsureConnection();
            var command = new OracleCommand(commandStatement, _connection);
            if (parameters != null)
            {
                foreach (var (key, value) in parameters)
                {
                    command.Parameters.Add(key, value);
                }
            }
            if (whereParameters != null)
            {
                foreach (var (key, value) in whereParameters)
                {
                    command.Parameters.Add(key, value);
                }
            }
            return command;
        }

        private async Task EnsureConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                switch (_connection.State)
                {
                    case ConnectionState.Closed:
                        await _connection.OpenAsync(cancellationToken);
                        break;
                    case ConnectionState.Broken:
                        await _connection.CloseAsync();
                        await _connection.OpenAsync(cancellationToken);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectException($"Open connection failed: {ex.Message}", ex);
            }

        }
        public void EnsureConnection()
        {
            try
            {
                switch (_connection.State)
                {
                    case ConnectionState.Closed:
                        _connection.Open();
                        break;
                    case ConnectionState.Broken:
                        _connection.Close();
                        _connection.Open();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectException($"Open connection failed: {ex.Message}", ex);
            }

        }


        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }


        public int InsertOracleBulkArrayData(string tableName, int commitRows, Dictionary<string, OracleArrayData> bulkData)
        {
            using var command = new OracleCommand($"INSERT INTO {tableName} ({string.Join(",", bulkData.Keys)}) VALUES ({string.Join(",", bulkData.Keys.Select(x => ":" + x))})", _connection)
            {
                CommandType = CommandType.Text,

                BindByName = true,
                //  In order to use ArrayBinding, the ArrayBindCount property
                //  of OracleCommand object must be set to the number of records to be inserted
                ArrayBindCount = commitRows
            };

            foreach (var c in bulkData)
            {
                switch (c.Value.DataType)
                {
                    //case OracleDbType.BFile:
                    //    break;
                    //case OracleDbType.Blob:
                    //    break;
                    //case OracleDbType.Byte:
                    //    break;
                    case OracleDbType.Char:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Char, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.Clob:
                    //    break;
                    case OracleDbType.Date:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Date, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Decimal:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Decimal, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.Double:
                    //    break;
                    //case OracleDbType.Long:
                    //    break;
                    //case OracleDbType.LongRaw:
                    //    break;
                    case OracleDbType.Int16:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Int16, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Int32:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Int32, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Int64:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Int64, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.IntervalDS:
                    //    break;
                    //case OracleDbType.IntervalYM:
                    //    break;
                    //case OracleDbType.NClob:
                    //    break;
                    //case OracleDbType.NChar:
                    //    break;
                    //case OracleDbType.NVarchar2:
                    //    break;
                    //case OracleDbType.Raw:
                    //    break;
                    //case OracleDbType.RefCursor:
                    //    break;
                    //case OracleDbType.Single:
                    //    break;
                    case OracleDbType.TimeStamp:
                        command.Parameters.Add($":{c.Key}", OracleDbType.TimeStamp, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.TimeStampLTZ:
                        command.Parameters.Add($":{c.Key}", OracleDbType.TimeStampLTZ, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.TimeStampTZ:
                        command.Parameters.Add($":{c.Key}", OracleDbType.TimeStampTZ, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Varchar2:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Varchar2, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.XmlType:
                    //    break;
                    //case OracleDbType.BinaryDouble:
                    //    break;
                    //case OracleDbType.BinaryFloat:
                    //    break;
                    case OracleDbType.Boolean:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Boolean, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    default:
                        break;
                }

            }
            EnsureConnection();
            return command.ExecuteNonQuery();
        }

        public async Task<int> InsertOracleBulkArrayDataAsync(string tableName, int commitRows, Dictionary<string, OracleArrayData> bulkData, CancellationToken cancellationToken = default)
        {

            var insertStatement = $"INSERT INTO {tableName} ({string.Join(",", bulkData.Keys)}) VALUES ({string.Join(",", bulkData.Keys.Select(x => ":" + x))})";

            var command = new OracleCommand(insertStatement, _connection);

            command.CommandType = CommandType.Text;

            command.BindByName = true;
            //  In order to use ArrayBinding, the ArrayBindCount property
            //  of OracleCommand object must be set to the number of records to be inserted
            command.ArrayBindCount = commitRows;

            foreach (var c in bulkData)
            {
                switch (c.Value.DataType)
                {
                    //case OracleDbType.BFile:
                    //    break;
                    //case OracleDbType.Blob:
                    //    break;
                    //case OracleDbType.Byte:
                    //    break;
                    case OracleDbType.Char:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Char, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.Clob:
                    //    break;
                    case OracleDbType.Date:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Date, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Decimal:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Decimal, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.Double:
                    //    break;
                    //case OracleDbType.Long:
                    //    break;
                    //case OracleDbType.LongRaw:
                    //    break;
                    case OracleDbType.Int16:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Int16, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Int32:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Int32, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Int64:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Int64, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.IntervalDS:
                    //    break;
                    //case OracleDbType.IntervalYM:
                    //    break;
                    //case OracleDbType.NClob:
                    //    break;
                    //case OracleDbType.NChar:
                    //    break;
                    //case OracleDbType.NVarchar2:
                    //    break;
                    //case OracleDbType.Raw:
                    //    break;
                    //case OracleDbType.RefCursor:
                    //    break;
                    //case OracleDbType.Single:
                    //    break;
                    case OracleDbType.TimeStamp:
                        command.Parameters.Add($":{c.Key}", OracleDbType.TimeStamp, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.TimeStampLTZ:
                        command.Parameters.Add($":{c.Key}", OracleDbType.TimeStampLTZ, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.TimeStampTZ:
                        command.Parameters.Add($":{c.Key}", OracleDbType.TimeStampTZ, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    case OracleDbType.Varchar2:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Varchar2, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    //case OracleDbType.XmlType:
                    //    break;
                    //case OracleDbType.BinaryDouble:
                    //    break;
                    //case OracleDbType.BinaryFloat:
                    //    break;
                    case OracleDbType.Boolean:
                        command.Parameters.Add($":{c.Key}", OracleDbType.Boolean, c.Value.DataValues, ParameterDirection.Input);
                        break;
                    default:
                        break;
                }

            }
            await EnsureConnectionAsync(cancellationToken);
            return command.ExecuteNonQuery();
        }

        public Task<int> InsertIgnoringDuplicatesAsync(string tableName, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> ProcessTransitionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public OracleParameter[] SetupParameters(Dictionary<string, object> parrameters)
        {
            List<OracleParameter> Parameters = new List<OracleParameter>();
            foreach (var (key, value) in parrameters)
            {
                Parameters.Add(new OracleParameter(key, value));
            }
            return Parameters.ToArray();
        }
        public OracleParameter[] SetupParameters(Dictionary<string, OracleArrayData> bulkData)
        {
            List<OracleParameter> Parameters = new List<OracleParameter>();
            foreach (var c in bulkData)
            {
                switch (c.Value.DataType)
                {
                    //case OracleDbType.BFile:
                    //    break;
                    //case OracleDbType.Blob:
                    //    break;
                    //case OracleDbType.Byte:
                    //    break;
                    case OracleDbType.Char:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Char, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    //case OracleDbType.Clob:
                    //    break;
                    case OracleDbType.Date:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Date, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    case OracleDbType.Decimal:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Decimal, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    //case OracleDbType.Double:
                    //    break;
                    //case OracleDbType.Long:
                    //    break;
                    //case OracleDbType.LongRaw:
                    //    break;
                    case OracleDbType.Int16:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Int16, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    case OracleDbType.Int32:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Int32, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    case OracleDbType.Int64:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Int64, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    //case OracleDbType.IntervalDS:
                    //    break;
                    //case OracleDbType.IntervalYM:
                    //    break;
                    //case OracleDbType.NClob:
                    //    break;
                    //case OracleDbType.NChar:
                    //    break;
                    //case OracleDbType.NVarchar2:
                    //    break;
                    //case OracleDbType.Raw:
                    //    break;
                    //case OracleDbType.RefCursor:
                    //    break;
                    //case OracleDbType.Single:
                    //    break;
                    case OracleDbType.TimeStamp:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.TimeStamp, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    case OracleDbType.TimeStampLTZ:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.TimeStampLTZ, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    case OracleDbType.TimeStampTZ:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.TimeStampTZ, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    case OracleDbType.Varchar2:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Varchar2, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    //case OracleDbType.XmlType:
                    //    break;
                    //case OracleDbType.BinaryDouble:
                    //    break;
                    //case OracleDbType.BinaryFloat:
                    //    break;
                    case OracleDbType.Boolean:
                        Parameters.Add(new OracleParameter($":{c.Key}", OracleDbType.Boolean, c.Value.DataValues, ParameterDirection.Input));
                        break;
                    default:
                        break;
                }
            }
            return Parameters.ToArray();
        }


    }
}
