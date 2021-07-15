using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BJCBCPOS_Model;
using System.Text.RegularExpressions;
using System.Threading;

namespace BJCBCPOS_Data
{
    public class DBConnect
    {
        int _timeout = ProgramConfig.commandTimeout;
        OleDbConnectionStringBuilder builder = null;
        OleDbConnection conn = null;
        OleDbCommand command = null;
        OleDbTransaction trans = null;
        string connStr;
        List<string> connectionLostCode = new List<string> { "08001", "08004", "08S01" };
        List<string> queryTimeoutCode = new List<string> { "HYT00", "HY018" };

        //System.Timers.Timer timer = null;
        //Thread openThread = null;

        public DBConnect(string connectionString)
        {
            connStr = connectionString;

            //timer = new System.Timers.Timer();
            //timer.AutoReset = false;
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        public DBConnect(string connectionString, int timeout)
        {
            connStr = connectionString;
            _timeout = timeout;
        }

        //private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    AppLog.writeLog("throw error from timer.");
        //    openThread.Abort();
        //}

        public void openConnection()
        {
            try
            {
                if (conn == null || conn.State == ConnectionState.Connecting)
                {
                    builder = new OleDbConnectionStringBuilder(connStr);
                    builder.Add("Connect Timeout", ProgramConfig.connectionTimeout);
                    conn = new OleDbConnection(builder.ConnectionString);
                    if (command != null)
                    {
                        command.Connection = conn;
                    }
                    if (trans != null)
                    {
                        trans = null;
                    }
                }

                if (conn.State != ConnectionState.Open)
                {
                    //timer.Interval = ProgramConfig.connectionTimeout * 1000;
                    //timer.Start();

                    //openThread = new Thread(new ParameterizedThreadStart(threadOpenConnection));
                    //openThread.Start(conn);

                    //while (conn.State != ConnectionState.Open)
                    //{
                    //    if (!timer.Enabled)
                    //    {
                    //        throw new NetworkConnectionException("open connection exceed time limit");
                    //    }
                    //    Thread.Sleep(5);
                    //}
                    conn.Open();
                }
            }
            catch (NetworkConnectionException)
            {
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        //private void threadOpenConnection(object connection)
        //{
        //    try
        //    {
        //        OleDbConnection connect = (OleDbConnection)connection;
        //        connect.Open();
        //        timer.Stop();
        //    }
        //    catch (ThreadAbortException)
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //        if (timer.Enabled)
        //        {
        //            timer.Stop();
        //        }
        //        AppLog.writeLog(ex);
        //    }
        //}

        public void closeConnection()
        {
            try
            {
                if (trans != null)
                {
                    trans.Rollback();
                    trans = null;
                }
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }
            catch (NetworkConnectionException)
            {
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public void BeginTransaction()
        {
            try
            {
                openConnection();
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                        trans = null;
                    }
                    catch { }
                }
                trans = conn.BeginTransaction();
            }
            catch (NetworkConnectionException)
            {
                trans = null;
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open && trans != null)
                {
                    trans.Commit();
                    trans = null;
                    closeConnection();
                    command = null;
                }
            }
            catch (NetworkConnectionException)
            {
                trans = null;
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open && trans != null)
                {
                    trans.Rollback();
                    trans = null;
                    closeConnection();
                    command = null;
                }
            }
            catch (NetworkConnectionException)
            {
                trans = null;
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public void SetCommandText(string commandText)
        {
            try
            {
                openConnection();
                command = new OleDbCommand();
                command.Connection = conn;
                command.CommandType = CommandType.Text;
                command.CommandText = commandText;
                command.CommandTimeout = _timeout;
                if (trans != null)
                {
                    command.Transaction = trans;
                }
            }
            catch (NetworkConnectionException)
            {
                if (trans != null)
                {
                    trans = null;
                }
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans = null;
                }
                conn = null;
                checkConnectionLostError(ex);
            }
        }

        public void SetCommandStoredProcedure(string storeProcName)
        {
            try
            {
                openConnection();
                command = new OleDbCommand();
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storeProcName;
                command.CommandTimeout = _timeout;
                if (trans != null)
                {
                    command.Transaction = trans;
                }
            }
            catch (NetworkConnectionException)
            {
                if (trans != null)
                {
                    trans = null;
                }
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public void AddInputParameter(string parameterName, SqlDbType type, object value)
        {
            try
            {
                if (command == null)
                {
                    throw new Exception("Not specify StoreProcedure. Please call SetCommandStoredProcedure before AddInputParameter.");
                }
                if (command.Parameters.Count == 0)
                {
                    OleDbCommandBuilder.DeriveParameters(command);
                }
                OleDbParameter param = command.Parameters[parameterName];
                if (param != null)
                {
                    param.Value = value;
                }
                else
                {
                    command.Parameters.AddWithValue("@" + parameterName, value);
                }
            }
            catch (NetworkConnectionException)
            {
                if (trans != null)
                {
                    trans = null;
                }
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public void ExecuteNonQuery()
        {
            try
            {
                string functionID = "";
                if (command != null)
                {
                    if (FixedData.isLogSql)
                    {
                        AppLog.writeSqlCommand(GetCurrentCommand(out functionID));
                    }
                    openConnection();

                    command.ExecuteNonQuery();
                    command = null;

                    if (trans == null)
                    {
                        closeConnection();
                    }
                }
            }
            catch (NetworkConnectionException)
            {
                if (trans != null)
                {
                    trans = null;
                }
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                checkConnectionLostError(ex);
            }
        }

        public DataTable ExecuteToDataTable()
        {
            DataTable dt = null;
            try
            {
                string functionID = "";
                if (command != null)
                {
                    if (FixedData.isLogSql)
                    {
                        AppLog.writeSqlCommand(GetCurrentCommand(out functionID));
                    }
                    openConnection();
                    OleDbDataAdapter reader = new OleDbDataAdapter(command);
                    dt = new DataTable();
                    reader.Fill(dt);

                    if (functionID != "" && !dt.Columns.Contains("Alert_FunctionID"))
                    {
                        dt.Columns.Add("Alert_FunctionID", typeof(string), "'" + functionID + "'");
                    }

                    command = null;
                    if (trans == null)
                    {
                        closeConnection();
                    }
                }
            }
            catch (NetworkConnectionException)
            {
                if (trans != null)
                {
                    trans = null;
                }
                conn = null;
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog("ExecuteToDataTable Error message : " + ex.Message);
                checkConnectionLostError(ex);
            }
            return dt;
        }

        //private DbType convertType(SqlDbType dbType)
        //{
        //    if (dbType == SqlDbType.Char)
        //    {
        //        return DbType.AnsiStringFixedLength;
        //    }
        //    else if (dbType == SqlDbType.Int)
        //    {
        //        return DbType.Int32;
        //    }
        //    else if (dbType == SqlDbType.VarChar)
        //    {
        //        return DbType.AnsiString;
        //    }
        //    else if (dbType == SqlDbType.NVarChar)
        //    {
        //        return DbType.String;
        //    }
        //    else if (dbType == SqlDbType.Money)
        //    {
        //        return DbType.Currency;
        //    }
        //    return DbType.Object;
        //}

        public string GetCurrentCommand(out string functionID)
        {
            functionID = "";
            string result = "";
            if (command.CommandType == CommandType.StoredProcedure)
            {
                result = command.CommandText;
                result = "EXEC " + result;
                foreach (OleDbParameter param in command.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input)
                    {
                        if (param.DbType == DbType.AnsiStringFixedLength || param.DbType == DbType.AnsiString || param.DbType == DbType.String || param.DbType == DbType.StringFixedLength)
                        {
                            result += " '" + param.Value + "',";
                        }
                        else
                        {
                            result += " " + param.Value + ",";
                        }

                        if (param.ParameterName == "FunctionID")
                        {
                            functionID = param.Value.ToString().Trim();
                        }
                    }
                }
                result = result.Substring(0, result.Length - 1) + ";";
            }
            else
            {
                result = command.CommandText;
            }
            return result;
        }

        private void checkConnectionLostError(Exception ex)
        {
            if (ex is OleDbException)
            {
                foreach (OleDbError item in ((OleDbException)ex).Errors)
                {
                    if (connectionLostCode.Contains(item.SQLState))
                    {
                        AppLog.writeLog("OleDbError Error: " + item.NativeError + ", " + item.Message);
                        try
                        {
                            RollbackTransaction();
                        }
                        catch { }
                        finally
                        {
                            trans = null;
                            conn = null;
                        }
                        throw new NetworkConnectionException(ex, NetworkErrorType.ConnectionTimeout);
                    }
                    else if (queryTimeoutCode.Contains(item.SQLState))
                    {
                        AppLog.writeLog("OleDbError Error: " + item.NativeError + ", " + item.Message);
                        // try rollback transaction
                        try
                        {
                            RollbackTransaction();
                        }
                        catch { }
                        throw new NetworkConnectionException(ex, NetworkErrorType.CommandTimeout);
                    }
                    else
                    {
                        AppLog.writeLog("Current SQLState not in connectionLost and queryTimeount. SQLState = " + item.SQLState);
                    }
                }
            }
            throw ex;
        }
    }

}
