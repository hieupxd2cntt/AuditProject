using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text.RegularExpressions;
using Dapper;
using Npgsql;
using NpgsqlTypes;
using WebCore.Base;
using WebCore.Common;
using WebCore.Entities;
using WebCore.Utils;

namespace WebDataAccess
{
    public class SQLHelper
    {
        public static Dictionary<string, SqlParameter[]> m_CachedParameters;
        public static Dictionary<string, NpgsqlParameter[]> m_CachedNpgParameters;
        static SQLHelper()
        {
            m_CachedParameters = new Dictionary<string, SqlParameter[]>();
            m_CachedNpgParameters = new Dictionary<string, NpgsqlParameter[]>();
        }
        // Get Session
        //public static Session GetSession(string query)
        //{
        //    try {

        //        Session sess = new Session();
        //        using (var connection = new SqlConnection(Core.Common.App.Configs.LocalDB)) {
        //            connection.Open();
        //            sess = connection.Query<Session>(query).FirstOrDefault();
        //            connection.Close();
        //        }
        //        return sess;
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}
        public static Session GetSession(string userName)
        {
            try
            {

                Session sess = new Session();
                sess.Username = userName;
                //sess["UserName"] = "hieupx";

                return sess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ExecuteStoreProcedure(string connectionString, string commandText, string userName, params object[] values)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {

                }

                try
                {
                    var comm = new SqlCommand(commandText, conn) { CommandType = CommandType.StoredProcedure };
                    Session session = GetSession(userName);
                    AssignParameters(comm, session, values);

                    comm.ExecuteNonQuery();
                    if (
                        comm.Parameters.Contains(CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME) &&
                        comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value != DBNull.Value
                        )
                    {
                        var errCode = int.Parse(comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value.ToString());
                        if (errCode != 0) throw ErrorUtils.CreateError(errCode, commandText, values);
                    }
                }

                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message,
                        commandText, values);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void ExecuteStoreProcedure(string connectionString, Session session, string commandText, params object[] values)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message,
                        commandText, values);
                }

                try
                {
                    var comm = new SqlCommand(commandText, conn) { CommandType = CommandType.StoredProcedure };

                    AssignParameters(comm, session, values);

                    comm.ExecuteNonQuery();
                    if (
                        comm.Parameters.Contains(CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME) &&
                        comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value != DBNull.Value
                        )
                    {
                        var errCode = int.Parse(comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value.ToString());
                        if (errCode != 0) throw ErrorUtils.CreateError(errCode, commandText, values);
                    }
                }
                catch (SqlException ex)
                {
                    throw ThrowSqlUserException(ex, commandText);
                }
                catch (FaultException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message,
                        commandText, values);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            var retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }


        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters == null) return;
            foreach (var p in commandParameters.Where(p => p != null))
            {
                // Check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput ||
                     p.Direction == ParameterDirection.Input) &&
                    (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }

        public static void FillDataTable(string connectionString, string commandText, out DataTable resultTable, params object[] values)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                resultTable = null;
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }

                try
                {
                    if (commandText.Contains("("))
                    {
                        commandText = commandText.Replace("(", " ");
                        commandText = commandText.Replace(")", "");
                        var data = SQLHelper.Scalar(connectionString, commandText, null);
                        var ds = (DataSet)data;
                        if (ds != null)
                        {
                            resultTable = ds.Tables[0];
                        }

                    }
                    else
                    {
                        using (var comm = new SqlCommand(commandText, conn))
                        {
                            var adap = new SqlDataAdapter(comm);
                            var ds = new DataSet();
                            comm.CommandType = CommandType.StoredProcedure;
                            AssignParameters(comm, values);

                            adap.Fill(ds);
                            if (
                                comm.Parameters.Contains(CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME) &&
                                comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value != DBNull.Value
                                )
                            {
                                var errCode = int.Parse(comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value.ToString());
                                if (errCode != 0) throw ErrorUtils.CreateError(errCode, commandText, values);
                            }
                            resultTable = ds.Tables[0];
                        }
                    }

                }
                catch (SqlException ex)
                {
                    throw ThrowSqlUserException(ex, commandText);
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static void FillDataTable(string connectionString, Session session, string commandText, out DataTable resultTable, params object[] values)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }

                try
                {
                    using (var comm = new SqlCommand(commandText, conn))
                    {
                        var adap = new SqlDataAdapter(comm);
                        var ds = new DataSet();
                        comm.CommandType = CommandType.StoredProcedure;
                        AssignParameters(comm, session, values);

                        adap.Fill(ds);
                        if (
                            comm.Parameters.Contains(CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME) &&
                            comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value != DBNull.Value
                            )
                        {
                            var errCode = int.Parse(comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value.ToString());
                            if (errCode != 0) throw ErrorUtils.CreateError(errCode, commandText, values);
                        }
                        resultTable = ds.Tables[0];
                    }
                }
                catch (SqlException ex)
                {
                    throw ThrowSqlUserException(ex, commandText);
                }
                catch (FaultException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                }
                finally
                {
                    conn.Close();
                }
            }
        }



        public static DataTable ExecuteStoreProcedureDaperFillDataTable(string connectionString, CommandType commandType, string commandText, params object[] values)
        //  where T : class, new()
        {
            var listaLeiturasRomaneio = new DataTable();
            if (commandType == CommandType.Text)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var reader = connection.ExecuteReader(commandText))
                    {
                        listaLeiturasRomaneio.Load(reader);
                        return listaLeiturasRomaneio;
                    }

                }
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var reader = connection.ExecuteReader(
                       commandText, param: values[0],
                       commandType: CommandType.StoredProcedure))
                    {
                        listaLeiturasRomaneio.Load(reader);
                        return listaLeiturasRomaneio;
                    }
                }
            }

            else
            {
                return null;
            }

        }

        public static List<T> ExecuteStoreProcedure<T>(string connectionString, Session session, string commandText, params object[] values)
           where T : class, new()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                       ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }
                using (var comm = new SqlCommand(commandText, conn))
                {
                    try
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        AssignParameters(comm, session, values);

                        using (var dr = comm.ExecuteReader())
                        {
                            if (
                                comm.Parameters.Contains(CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME) &&
                                comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value != DBNull.Value
                                )
                            {
                                var errCode = int.Parse(comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value.ToString());
                                if (errCode != 0) throw ErrorUtils.CreateError(errCode, commandText, values);
                            }
                            //var dataTable = new DataTable();
                            //dataTable.Load(dr);
                            return dr.ToList<T>();
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw ThrowSqlUserException(ex, commandText);
                    }
                    catch (FaultException ex)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw ErrorUtils.CreateErrorWithSubMessage(
                            ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }




        private static void AssignParameters(SqlCommand comm, params object[] values)
        {
            try
            {
                DiscoveryParameters(comm);
                // assign value
                var index = 0;
                foreach (SqlParameter param in comm.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        if (values.Length == 0)
                        {
                            continue;
                        }
                        if (values[index] == null || (values[index] is string && (string)values[index] == string.Empty))
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            switch (param.DbType)
                            {
                                case DbType.Date:
                                    param.Value = Convert.ToDateTime(values[index]);
                                    break;
                                case DbType.Boolean:
                                    if (values[index].ToString() == "1" || values[index].ToString() == "0")
                                    {
                                        param.Value = values[index].ToString() == "1" ? true : false;
                                    }
                                    else
                                    {
                                        param.Value = Convert.ToBoolean(values[index]);
                                    }

                                    break;
                                case DbType.Byte:
                                    param.Value = Convert.ToByte(values[index]);
                                    break;
                                case DbType.Int16:
                                case DbType.Int32:
                                case DbType.Int64:
                                case DbType.Single:
                                case DbType.Double:
                                case DbType.Decimal:
                                    param.Value = Convert.ToDecimal(values[index]);
                                    break;
                                default:
                                    param.Value = values[index];
                                    break;
                            }
                        }
                        index++;
                    }
                }

            }

            catch (Exception ex)
            {
                throw ErrorUtils.CreateErrorWithSubMessage(
                    ERR_SQL.ERR_SQL_ASSIGN_PARAMS_FAIL, ex.Message,
                    comm.CommandText, values);
            }
        }

        private static void AssignParameters(SqlCommand comm, Session session, params object[] values)
        {
            try
            {

                comm.Parameters.Clear();
                DiscoveryParameters(comm);
                // assign value
                var index = 0;
                foreach (SqlParameter param in comm.Parameters)
                {
                    if (param.ParameterName.ToUpper() == "@" + CONSTANTS.ORACLE_SESSION_USER)
                    {
                        param.Value = session.Username;
                    }
                    else if (param.ParameterName.ToUpper() == "@" + CONSTANTS.SQL_WSALIAS)
                    {
                        param.Value = session.WsAlias;
                    }
                    else if (param.ParameterName.ToUpper() == "@" + CONSTANTS.ORACLE_SESSIONKEY)
                    {
                        param.Value = session.SessionKey;
                    }

                    else if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        if (values[index] == null || (values[index] is string && (string)values[index] == string.Empty))
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            switch (param.DbType)
                            {
                                case DbType.Date:
                                    param.Value = Convert.ToDateTime(values[index]);
                                    break;
                                case DbType.Boolean:
                                    if (values[index].ToString() == "")
                                    {
                                        param.Value = false;
                                    }
                                    else
                                    if (values[index].ToString() == "1" || values[index].ToString() == "0")
                                    {
                                        param.Value = values[index].ToString() == "1" ? true : false;
                                    }
                                    else if (values[index].ToString() == "Y" || values[index].ToString() == "N")
                                    {
                                        param.Value = values[index].ToString() == "Y" ? true : false;
                                    }
                                    else
                                    {
                                        param.Value = Convert.ToBoolean(values[index]);
                                    }

                                    break;
                                case System.Data.DbType.AnsiStringFixedLength:
                                    if (values[index].ToString() == "")
                                    {
                                        param.Value = 0;
                                    }
                                    else
                                    if (values[index].ToString() == "1" || values[index].ToString() == "0")
                                    {
                                        param.Value = values[index].ToString() == "1" ? 1 : 0;
                                    }
                                    else if (values[index].ToString() == "Y" || values[index].ToString() == "N")
                                    {
                                        param.Value = values[index].ToString() == "Y" ? 1 : 0;
                                    }
                                    else
                                    {
                                        param.Value = Convert.ToInt16(values[index]);
                                    }

                                    break;

                                case DbType.Byte:
                                    param.Value = Convert.ToByte(values[index]);
                                    break;
                                case DbType.Int16:
                                case DbType.Int32:
                                case DbType.Int64:
                                case DbType.Single:
                                case DbType.Double:
                                case DbType.Decimal:
                                    param.Value = Convert.ToDecimal(values[index]);
                                    break;
                                default:
                                    param.Value = values[index] == null ? "" : values[index].ToString();
                                    break;
                            }
                        }
                        index++;
                    }
                }
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateErrorWithSubMessage(
                    ERR_SQL.ERR_SQL_ASSIGN_PARAMS_FAIL, ex.Message,
                    comm.CommandText, values);
            }
        }

        private static void AssignParameters(NpgsqlCommand comm, params object[] values)
        {
            try
            {
                DiscoveryParameters(comm);
                // assign value
                var index = 0;
                for (int i = 0; i < comm.Parameters.Count(); i++)// NpgsqlParameter param in comm.Parameters)
                {
                    var param = comm.Parameters[i];
                    if (param.Direction == ParameterDirection.Input && param.NpgsqlDbType != NpgsqlTypes.NpgsqlDbType.Refcursor/* || param.Direction == ParameterDirection.InputOutput*/)
                    {
                        if (values.Length == 0)
                        {
                            continue;
                        }
                        if (values[index] == null || (values[index] is string && (string)values[index] == string.Empty))
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            switch (param.DbType)
                            {
                                case DbType.Date:
                                    param.Value = Convert.ToDateTime(values[index]);
                                    break;
                                case DbType.Boolean:
                                    if (values[index].ToString() == "1" || values[index].ToString() == "0")
                                    {
                                        param.Value = values[index].ToString() == "1" ? true : false;
                                    }
                                    else
                                    {
                                        param.Value = Convert.ToBoolean(values[index]);
                                    }

                                    break;
                                case DbType.Byte:
                                    param.Value = Convert.ToByte(values[index]);
                                    break;
                                case DbType.Int16:
                                case DbType.Int32:
                                case DbType.Int64:
                                case DbType.Single:
                                case DbType.Double:
                                case DbType.Decimal:
                                    param.Value = Convert.ToDecimal(values[index]);
                                    break;
                                default:
                                    var parameter1 = comm.CreateParameter();
                                    parameter1.ParameterName = param.ParameterName;
                                    parameter1.NpgsqlDbType = NpgsqlDbType.Text;
                                    parameter1.Value = values[index];
                                    param = parameter1;
                                    comm.Parameters[i] = parameter1;
                                    //param.Value = values[index].ToString();
                                    //param.NpgsqlDbType = NpgsqlDbType.Text;
                                    break;
                            }
                        }
                        index++;
                    }
                    else if (param.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Refcursor)
                    {
                        NpgsqlParameter rf = new NpgsqlParameter(param.ParameterName, NpgsqlTypes.NpgsqlDbType.Refcursor);
                        param = rf;
                        param.Value = param.ParameterName;//"ref1";
                        comm.Parameters[i] = param;
                        //index++;
                    }
                }

                //foreach (NpgsqlParameter param in comm.Parameters)
                //{
                //    if (param.Direction == ParameterDirection.Input && param.NpgsqlDbType != NpgsqlTypes.NpgsqlDbType.Refcursor/* || param.Direction == ParameterDirection.InputOutput*/)
                //    {
                //        if (values.Length == 0)
                //        {
                //            continue;
                //        }
                //        if (values[index] == null || (values[index] is string && (string)values[index] == string.Empty))
                //        {
                //            param.Value = DBNull.Value;
                //        }
                //        else
                //        {
                //            switch (param.DbType)
                //            {
                //                case DbType.Date:
                //                    param.Value = Convert.ToDateTime(values[index]);
                //                    break;
                //                case DbType.Boolean:
                //                    if (values[index].ToString() == "1" || values[index].ToString() == "0")
                //                    {
                //                        param.Value = values[index].ToString() == "1" ? true : false;
                //                    }
                //                    else
                //                    {
                //                        param.Value = Convert.ToBoolean(values[index]);
                //                    }

                //                    break;
                //                case DbType.Byte:
                //                    param.Value = Convert.ToByte(values[index]);
                //                    break;
                //                case DbType.Int16:
                //                case DbType.Int32:
                //                case DbType.Int64:
                //                case DbType.Single:
                //                case DbType.Double:
                //                case DbType.Decimal:
                //                    param.Value = Convert.ToDecimal(values[index]);
                //                    break;
                //                default:
                //                    param.Value = values[index];
                //                    break;
                //            }
                //        }
                //        index++;
                //    }
                //    else if (param.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Refcursor)
                //    {
                //        NpgsqlParameter rf = new NpgsqlParameter("ref1", NpgsqlTypes.NpgsqlDbType.Refcursor);
                //        comm.Parameters[index] = rf;
                //        index++;
                //    }
                //}

            }

            catch (Exception ex)
            {
                throw ErrorUtils.CreateErrorWithSubMessage(
                    ERR_SQL.ERR_SQL_ASSIGN_PARAMS_FAIL, ex.Message,
                    comm.CommandText, values);
            }
        }
        public static void DiscoveryParameters(SqlCommand comm)
        {
            try
            {
                //var cachedKey = comm.CommandText;

                //SqlCommandBuilder.DeriveParameters(comm);
                ////scomm.CommandText = cachedKey;
                //var source = new SqlParameter[comm.Parameters.Count];
                //for (var i = 0; i < comm.Parameters.Count; i++) {
                //    source[i] = (SqlParameter)comm.Parameters[i];
                //}
                //m_CachedParameters.Add(cachedKey, source);


                //discovery parameter
                var cachedKey = comm.CommandText;
                if (m_CachedParameters.ContainsKey(cachedKey))
                {
                    var source = m_CachedParameters[cachedKey];
                    foreach (var param in source)
                    {
                        comm.Parameters.Add((SqlParameter)((ICloneable)param).Clone());
                    }
                }
                else
                {
                    SqlCommandBuilder.DeriveParameters(comm);
                    comm.CommandText = cachedKey;
                    var source = new SqlParameter[comm.Parameters.Count];
                    for (var i = 0; i < comm.Parameters.Count; i++)
                    {
                        //source[i] = (SqlParameter)comm.Parameters[i];
                        source[i] = (SqlParameter)((ICloneable)comm.Parameters[i]).Clone();
                    }
                    m_CachedParameters.Add(cachedKey, source);
                }

            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateErrorWithSubMessage(
                    ERR_SQL.ERR_SQL_DISCOVERY_PARAMS_FAIL, ex.Message,
                    comm.CommandText);
            }
        }
        public static void DiscoveryParameters(NpgsqlCommand comm)
        {
            try
            {
                //var cachedKey = comm.CommandText;

                //SqlCommandBuilder.DeriveParameters(comm);
                ////scomm.CommandText = cachedKey;
                //var source = new SqlParameter[comm.Parameters.Count];
                //for (var i = 0; i < comm.Parameters.Count; i++) {
                //    source[i] = (SqlParameter)comm.Parameters[i];
                //}
                //m_CachedParameters.Add(cachedKey, source);


                //discovery parameter
                var cachedKey = comm.CommandText;
                if (m_CachedNpgParameters.ContainsKey(cachedKey))
                {
                    var source = m_CachedNpgParameters[cachedKey];
                    foreach (var param in source)
                    {
                        comm.Parameters.Add((NpgsqlParameter)((ICloneable)param).Clone());
                    }
                }
                else
                {
                    NpgsqlCommandBuilder.DeriveParameters(comm);
                    comm.CommandText = cachedKey;
                    var source = new NpgsqlParameter[comm.Parameters.Count];
                    for (var i = 0; i < comm.Parameters.Count; i++)
                    {
                        //source[i] = (SqlParameter)comm.Parameters[i];
                        source[i] = (NpgsqlParameter)((ICloneable)comm.Parameters[i]).Clone();
                    }
                    m_CachedNpgParameters.Add(cachedKey, source);
                }

            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateErrorWithSubMessage(
                    ERR_SQL.ERR_SQL_DISCOVERY_PARAMS_FAIL, ex.Message,
                    comm.CommandText);
            }
        }

        public static void DiscoveryParameters(string connectionString, string commandText, List<OracleParam> @params)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                       ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message,
                       commandText);
                }

                try
                {
                    var comm = new SqlCommand(commandText, conn) { CommandType = CommandType.StoredProcedure };
                    DiscoveryParameters(comm);

                    //foreach(SqlParameter p in comm.Parameters) {
                    //    var objparam = new OracleParam();
                    //    objparam.StoreName = commandText;
                    //    objparam.Value = p.ParameterName;
                    //    @params.Add(objparam);
                    //}

                    @params.AddRange(from SqlParameter param in comm.Parameters
                                     where param.Direction == ParameterDirection.Input
                                     select new OracleParam
                                     {
                                         StoreName = commandText,
                                         Name = param.ParameterName
                                     });
                }
                catch (FaultException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message,
                        commandText);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #region Private Methods




        public static object Scalar(string connectionString, string query, List<SqlParameter> parametros)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros.ToArray());
                    }
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public static DataSet ExcuteStore2DataSet(string connectionString, string query, object[] parametros)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parametros != null && parametros.Length > 0)
                    {
                        AssignParameters(command, null, parametros);
                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public static object ExcuteStore(string connectionString, string query, object[] parametros)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros.ToArray());
                    }
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        #endregion

        public static Exception ThrowSqlUserException(SqlException ex, string commandText)
        {
            if (ex.Number == CONSTANTS.SQL_USER_HANDLED_EXCEPTION_CODE)
            {
                var match = Regex.Match(ex.Message, "<ERROR ID=([0-9]+)>([^<]*)</ERROR>");
                if (match.Success)
                {
                    var errCode = int.Parse(match.Groups[1].Value);
                    var errMessage = match.Groups[2].Value;

                    if (!string.IsNullOrEmpty(errMessage))
                    {
                        return ErrorUtils.CreateErrorWithSubMessage(errCode, errMessage);
                    }
                    return ErrorUtils.CreateError(errCode);
                }
            }
            return ErrorUtils.CreateErrorWithSubMessage(
                ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
        }


        public static string ExecuteStoreProcedureDapperRetunString(string connectionString, Session session, string commandText, params object[] values)
        {
            string result = "";
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var comm = new SqlCommand(commandText, connection) { CommandType = CommandType.StoredProcedure };
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                       ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }
                try
                {
                    AssignParameters(comm, session, values);
                    //DynamicParameters paramvalue = AssignParametersDapper(comm, session, values);
                    var p = new Dapper.DynamicParameters();
                    string nameofParamResult = "";
                    if (comm.Parameters.Count > 0)
                    {
                        foreach (SqlParameter param in comm.Parameters)
                        {
                            if (param.Direction == ParameterDirection.ReturnValue)
                            {

                            }
                            else if (param.Direction == ParameterDirection.Input)
                            {
                                if (param.Value == DBNull.Value)
                                {
                                    p.Add(param.ParameterName, "");
                                }
                                else
                                    p.Add(param.ParameterName, param.Value);
                            }
                            else if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                            {
                                p.Add(param.ParameterName, dbType: DbType.String, direction: ParameterDirection.Output, size: param.Size);
                                nameofParamResult = param.ParameterName;
                                if (nameofParamResult.Count() > 1 && nameofParamResult[0] == '@')
                                {
                                    nameofParamResult = nameofParamResult.Remove(0, 1);
                                }
                            }
                        }
                    }
                    // var p = new Dapper.DynamicParameters();
                    //p.Add("v_varname", "m_Stock");
                    //p.Add("stockcode", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                    if (!string.IsNullOrEmpty(nameofParamResult))
                    {
                        connection.Query(commandText, p, commandType: CommandType.StoredProcedure);
                        result = p.Get<String>(nameofParamResult);
                    }
                    else result = "";

                    return result;
                }
                catch (SqlException ex)
                {
                    throw ThrowSqlUserException(ex, commandText);
                }
                catch (FaultException)
                {
                    throw;
                }
                catch (Exception ex)
                {

                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private static DynamicParameters AssignParametersDapper(SqlCommand comm, Session session, params object[] values)
        {
            try
            {

                comm.Parameters.Clear();
                DiscoveryParameters(comm);
                // assign value
                var index = 0;
                var p = new Dapper.DynamicParameters();
                foreach (SqlParameter param in comm.Parameters)
                {
                    if (param.ParameterName == "@" + CONSTANTS.ORACLE_SESSION_USER)
                    {
                        param.Value = session.Username;
                    }
                    else if (param.ParameterName == "@" + CONSTANTS.SQL_WSALIAS)
                    {
                        param.Value = session.WsAlias;
                    }
                    else if (param.ParameterName == "@" + CONSTANTS.ORACLE_SESSIONKEY)
                    {
                        param.Value = session.SessionKey;
                    }

                    else if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        if (values[index] == null || (values[index] is string && (string)values[index] == string.Empty))
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            switch (param.SqlDbType)
                            {
                                case SqlDbType.Date:
                                    param.Value = Convert.ToDateTime(values[index], App.Environment.ServerInfo.Culture);
                                    break;
                                case SqlDbType.Decimal:
                                    param.Value = Convert.ToDecimal(values[index], App.Environment.ServerInfo.Culture);
                                    break;
                                case SqlDbType.Xml:
                                    System.Xml.XmlDocument xmltest = new System.Xml.XmlDocument();
                                    xmltest.LoadXml(values[index].ToString());
                                    param.Value = xmltest;
                                    break;
                                default:
                                    param.Value = values[index];
                                    break;
                            }
                        }
                        index++;
                    }
                    if (param.ParameterName != "@RETURN_VALUE")
                    {
                        p.Add(param.ParameterName, param.Value);
                    }
                    else
                    {
                        p.Add(
                            name: "@RetVal",
                            dbType: DbType.Int32,
                            direction: ParameterDirection.ReturnValue
                            );
                    }

                }
                return p;
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateErrorWithSubMessage(
                    ERR_SQL.ERR_SQL_ASSIGN_PARAMS_FAIL, ex.Message,
                    comm.CommandText, values);
            }
        }
        public static void ExecuteNonQueryDaper(string connectionString, CommandType commandType, Session session, string commandText, params object[] values)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                       ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }
                if (commandType == CommandType.StoredProcedure)
                {
                    using (var comm = new SqlCommand(commandText, conn))
                    {
                        try
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            DynamicParameters paramvalue = AssignParametersDapper(comm, session, values);
                            conn.Query(commandText, param: paramvalue
                                                                    , commandType: CommandType.StoredProcedure).ToList();
                            var RetVal = paramvalue.Get<int>("@RetVal");
                            //var result = paramvalue.Get<String>(nameofParamResult)
                        }
                        catch (SqlException ex)
                        {
                            throw ThrowSqlUserException(ex, commandText);
                        }
                        catch (FaultException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            throw ErrorUtils.CreateErrorWithSubMessage(
                                ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                else
                { //else text lam sau
                    //return null;
                }

            }
        }

        public static List<T> ExecuteProcedureDaper<T>(string connectionString, CommandType commandType, Session session, string commandText, params object[] values)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                       ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }
                if (commandType == CommandType.StoredProcedure)
                {
                    using (var comm = new SqlCommand(commandText, conn))
                    {
                        try
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            DynamicParameters paramvalue = AssignParametersDapper(comm, session, values);
                            return conn.Query<T>(commandText, param: paramvalue
                                                                    , commandType: CommandType.StoredProcedure).ToList();
                        }
                        catch (SqlException ex)
                        {
                            throw ThrowSqlUserException(ex, commandText);
                        }
                        catch (FaultException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            throw ErrorUtils.CreateErrorWithSubMessage(
                                ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                else
                { //else text lam sau
                    return null;
                }

            }


            //try {
            //    if (commandType == CommandType.Text) {
            //        using (var connection = new SqlConnection(connectionString)) {
            //            var orderDetails = connection.Query<T>(commandText).ToList();
            //            return orderDetails;
            //        }
            //    }
            //    else if (commandType == CommandType.StoredProcedure) {
            //        using (var connection = new SqlConnection(connectionString)) {
            //            var orderDetails = connection.Query<T>(commandText, param: values[0]
            //                                                    , commandType: CommandType.StoredProcedure).ToList();
            //            return orderDetails;
            //        }
            //    }

            //    else {
            //        return null;
            //    }
            //}
            //catch {
            //    return null;
            //}

        }

        public static List<T> ExecuteStoreProcedureDaper<T>(string connectionString, CommandType commandType, string commandText, params object[] values)
        {
            try
            {
                if (commandType == CommandType.Text)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        var orderDetails = connection.Query<T>(commandText).ToList();
                        return orderDetails;
                    }
                }
                else if (commandType == CommandType.StoredProcedure)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        var orderDetails = connection.Query<T>(commandText, param: values[0]
                                                                , commandType: CommandType.StoredProcedure).ToList();
                        return orderDetails;
                    }
                }

                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// Excute store với tham số truyền vào là list SqlParrammeter. Tránh trường hợp bị lỗi do string có chưa kí tự đặc biệt
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="store"></param>
        /// <param name="sqlParameters"></param>
        public static void ExcuteStoreWithSqlParram(string connectionString, string store, List<SqlParameter> sqlParameters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(store, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ErrorUtils.CreateErrorWithSubMessage(ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, store, new string[] { "" });
            }

        }
        /// <summary>
        /// Excute store với tham số truyền vào là list SqlParrammeter. Tránh trường hợp bị lỗi do string có chưa kí tự đặc biệt
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="store"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="idOut">Id output trả ra</param>
        public static void ExcuteStoreWithSqlParram(string connectionString, string store, List<SqlParameter> sqlParameters, out int idOut)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(store, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //var parrOut = new SqlParameter("@idout", SqlDbType.Int);
                        //parrOut.Direction = ParameterDirection.Output;
                        //sqlParameters.Add(parrOut);
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        idOut = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ErrorUtils.CreateErrorWithSubMessage(ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, store, new string[] { "" });
            }

        }

        public static int ExecuteNonQueryWithOutPut(string connectionString, string procedureName, SqlParameter[] parameters)
        {
            try
            {


                SqlConnection oConnection = new SqlConnection(connectionString);
                SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.Clear();
                SqlParameter OuputParam = oCommand.Parameters.Add("@idout", SqlDbType.Int);
                OuputParam.Direction = ParameterDirection.Output;
                oConnection.Open();

                using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                {


                    try
                    {
                        if (parameters != null)
                        {
                            oCommand.Parameters.AddRange(parameters);
                        }
                        oCommand.Transaction = oTransaction;


                        oCommand.ExecuteNonQuery();
                        oTransaction.Commit();
                    }
                    catch
                    {
                        oTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (oConnection.State == ConnectionState.Open)
                        {
                            oConnection.Close();
                        }
                        oConnection.Dispose();
                        oCommand.Dispose();
                    }

                }
                return Convert.ToInt32(OuputParam.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region Connect với PosgreeSql
        public static List<T> ExecuteStoreProcedurePostgreSQL<T>(string connectionString, Session session, string commandText, params object[] values)
           where T : class, new()
        {
            commandText = "core." + commandText;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                       ERR_SQL.ERR_SQL_OPEN_CONNECTION_FAIL, ex.Message, commandText);
                }
                var comm = new NpgsqlCommand(commandText, conn);
                NpgsqlTransaction tran = conn.BeginTransaction();
                try
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    AssignParameters(comm, values);
                    if (comm.Parameters.Count() > 0)
                    {
                        var checkRefcursor = comm.Parameters.Where(x => x.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Refcursor);
                        if (checkRefcursor != null)
                        {
                            if (checkRefcursor.Count() > 0)
                            {
                                var execute = comm.ExecuteNonQuery();
                                comm.CommandText = String.Format("fetch all in \"{0}\"", checkRefcursor.First().ParameterName);
                                comm.CommandType = CommandType.Text;
                            }
                        }
                    }

                    using (var dr = comm.ExecuteReader())
                    {
                        if (comm.Parameters.Contains(CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME) && comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value != DBNull.Value)
                        {
                            var errCode = int.Parse(comm.Parameters[CONSTANTS.ORACLE_EXCEPTION_PARAMETER_NAME].Value.ToString());
                            if (errCode != 0) throw ErrorUtils.CreateError(errCode, commandText, values);
                        }
                        List<T> list = new List<T>();
                        T obj = default(T);
                        while (dr.Read())
                        {
                            try
                            {
                                obj = Activator.CreateInstance<T>();
                                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                                {
                                    try
                                    {
                                        var attributes = prop.GetCustomAttributes(false);
                                        string msg = "the {0} property maps to the {1} database column";
                                        var columnMapping = attributes
                                            .FirstOrDefault(a => a.GetType() == typeof(ColumnAttribute));
                                        var mapsto = columnMapping as ColumnAttribute;
                                        if (columnMapping != null)
                                        {
                                            Console.WriteLine(msg, prop.Name, mapsto.Name);
                                        }
                                        //if (!object.Equals(dr[prop.Name], DBNull.Value))
                                        //{
                                        //    prop.SetValue(obj, dr[prop.Name], null);
                                        //}
                                        if (!object.Equals(dr[mapsto.Name], DBNull.Value))
                                        {
                                            prop.SetValue(obj, dr[mapsto.Name], null);
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                list.Add(obj);
                            }
                            catch (Exception exe)
                            {


                            }
                        }
                        //tran.Commit();
                        return list;
                    }
                }
                catch (SqlException ex)
                {
                    throw ThrowSqlUserException(ex, commandText);
                }
                catch (FaultException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SQL.ERR_SQL_EXECUTE_COMMAND_FAIL, ex.Message, commandText);
                }
                finally
                {
                    tran.Commit();
                    conn.Close();
                }

            }
        }

        #endregion
    }
    public static class ComponentExtensions
    {
        private static readonly Dictionary<Type, object> CachedMapInfo = new Dictionary<Type, object>();
        public static List<T> ToList<T>(this SqlDataReader reader)
            where T : class, new()
        {
            var col = new List<T>();
            while (reader.Read())
            {
                var obj = new T();
                reader.MapObject(obj);
                col.Add(obj);
            }
            return col;
        }

        public static T ToObject<T>(this SqlDataReader reader)
            where T : class, new()
        {
            var obj = new T();
            reader.Read();
            MapObject(reader, obj);
            return obj;
        }

        private static void MapObject<T>(this SqlDataReader reader, T obj)
            where T : class, new()
        {
            var mapInfo = GetMapInfo<T>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                if (reader[i] != DBNull.Value && mapInfo.ContainsKey(reader.GetName(i).ToUpper()))
                {
                    var prop = mapInfo[reader.GetName(i).ToUpper()];
                    //var prop = mapInfo[reader.GetName(i)];
                    prop.SetValue(obj, Convert.ChangeType(reader[i], prop.PropertyType), null);
                }
            }
        }

        private static Dictionary<string, PropertyInfo> GetMapInfo<T>()
        {
            var type = typeof(T);
            if (CachedMapInfo.ContainsKey(type))
            {
                return (Dictionary<string, PropertyInfo>)CachedMapInfo[type];
            }

            var mapInfo = new Dictionary<string, PropertyInfo>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                var attributes = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                foreach (ColumnAttribute attr in attributes)
                {
                    mapInfo.Add(attr.Name.ToUpper(), prop);
                }
            }

            CachedMapInfo.Add(type, mapInfo);
            return mapInfo;
        }



    }

}

