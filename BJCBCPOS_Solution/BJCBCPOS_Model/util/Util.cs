using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Text;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// class declare result return response code from database store procedure
    /// </summary>
    public class ResponseCode : IEquatable<ResponseCode>
    {
        public static ResponseCode Success { get { return new ResponseCode("000"); } }
        public static ResponseCode Information { get { return new ResponseCode("100"); } }
        public static ResponseCode Warning { get { return new ResponseCode("200"); } }
        public static ResponseCode Error { get { return new ResponseCode("300"); } }
        public static ResponseCode Ignore { get { return new ResponseCode("400"); } }
        public static ResponseCode PasswordExpired { get { return new ResponseCode("401"); } }
        public static ResponseCode Exit { get { return new ResponseCode("500"); } }
        public static ResponseCode CloseDrawer { get { return new ResponseCode("XXX"); } }
        public static ResponseCode ConnectionLost { get { return new ResponseCode("600"); } }

        public string value { get; set; }

        public ResponseCode(string value)
        {
            this.value = value;
        }

        public string text
        {
            get
            {
                switch (this.value)
                {
                    case "000":
                        return "Success";
                    case "100":
                        return "Information";
                    case "200":
                        return "Warning";
                    case "300":
                        return "Error";
                    case "400":
                        return "Ignore";
                    case "401":
                        return "PasswordExpired";
                    case "500":
                        return "Exit";
                    case "600":
                        return "ConnectionLost";
                    case "":
                        return "Empty";
                    default:
                        return "";
                }
            }
        }

        public bool next
        {
            get
            {
                switch (this.value)
                {
                    case "000":
                        return true;
                    case "100":
                        return true;
                    case "200":
                        return false;
                    case "300":
                        return false;
                    case "400":
                        return true;
                    case "401":
                        return true;
                    case "500":
                        return false;
                    case "600":
                        return false;
                    case "":
                        return false;
                    default:
                        return false;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ResponseCode);
        }

        public bool Equals(ResponseCode obj)
        {
            return this.value == obj.value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(ResponseCode lhs, ResponseCode rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ResponseCode lhs, ResponseCode rhs)
        {
            return !(lhs.Equals(rhs));
        }
    }

    /// <summary>
    /// serialize result from store procedure
    /// </summary>
    public class StoreResult
    {
        public readonly ResponseCode response;
        public readonly string responseMessage;
        public readonly string helpMessage;
        public readonly string nextStep;
        public readonly string applicationDesc;
        public readonly DataTable otherData;
        public readonly string functionID = "";
        public DialogResult dialogRes { get; set; }

        public StoreResult(DataTable data)
        {
            if (data != null)
            {
                AppLog.writeSqlCommand(DataTableToJSONWithStringBuilder(data));   
                try
                {
                    string missColumn = "";
                    if (!data.Columns.Contains("Response_Code"))
                    {
                        missColumn += "Response_Code, ";
                    }
                    if (!data.Columns.Contains("Response_Message"))
                    {
                        missColumn += "Response_Message, ";
                    }
                    if (!data.Columns.Contains("Help_Message"))
                    {
                        missColumn += "Help_Message, ";
                    }
                    if (!data.Columns.Contains("NextStep_Code"))
                    {
                        missColumn += "NextStep_Code, ";
                    }

                    if (data.Columns.Contains("Alert_FunctionID") && data.Rows.Count > 0)
                    {
                        this.functionID = data.Rows[0]["Alert_FunctionID"].ToString();
                    }
                    
                    if (missColumn.Length > 0)
                    {                        
                        AppLog.writeLog("data return from store procedure doesn't contains all standard columns. (missing " + missColumn.Substring(0, missColumn.Length - 2) + ")" + Environment.NewLine);
                        AppLog.writeLog(DataTableToJSONWithStringBuilder(data));
                        this.response = ResponseCode.Error;
                        this.responseMessage = "data return from store procedure doesn't contains all standard columns";
                        this.helpMessage = "data return from store procedure doesn't contains all standard columns";
                        this.nextStep = string.Empty;
                        this.applicationDesc = string.Empty;
                        this.otherData = null;
                    }
                    else
                    {
                        if (data.Rows.Count > 0)
                        {
                            if (String.IsNullOrEmpty(data.Rows[0]["Response_Code"].ToString().Trim()))
                            {
                                AppLog.writeLog("data return from store procedure column response is empty" + Environment.NewLine);
                                this.response = ResponseCode.Error;
                                this.responseMessage = "data return from store procedure column response is empty";
                                this.helpMessage = "data return from store procedure column response is empty";
                                this.nextStep = string.Empty;
                                this.applicationDesc = string.Empty;
                                this.otherData = null;
                            }
                            else
                            {                                                             
                                this.response = new ResponseCode(data.Rows[0]["Response_Code"].ToString().Trim());
                                this.responseMessage = data.Rows[0]["Response_Message"].ToString();
                                this.helpMessage = data.Rows[0]["Help_Message"].ToString();
                                this.nextStep = data.Rows[0]["NextStep_Code"].ToString();

                                data.Columns.Remove("Response_Code");
                                data.Columns.Remove("Response_Message");
                                data.Columns.Remove("Help_Message");
                                data.Columns.Remove("NextStep_Code");

                                if (data.Columns.Contains("Application_Desc"))
                                {
                                    this.applicationDesc = data.Rows[0]["Application_Desc"].ToString();
                                    data.Columns.Remove("Application_Desc");
                                }

                                this.otherData = data;



                                if (this.response == ResponseCode.Error)
                                {
                                    if (data.Columns.Count > 0)
                                    {
                                        double val = 0;
                                        bool isnumber = double.TryParse((data.Rows[0][0] + "").Trim(), out val);
                                        if (this.responseMessage == "")
                                        {
                                            this.response = ResponseCode.Ignore;
                                        }
                                    }

                                    string logMessage = "store procedure return error (ResponseCode = \"300\")" + Environment.NewLine;
                                    logMessage += "Response Message = " + this.responseMessage + Environment.NewLine;
                                    logMessage += "Help Message = " + this.helpMessage + Environment.NewLine;
                                    logMessage += "Application Desc = " + this.applicationDesc + Environment.NewLine;
                                    AppLog.writeLog(logMessage);
                                }                                
                            }
                        }
                        else
                        {
                            AppLog.writeLog("store procedure return data no record.");
                            this.response = ResponseCode.Error;
                            this.responseMessage = "store procedure return data no record";
                            this.helpMessage = "store procedure return data no record";
                            this.nextStep = string.Empty;
                            this.applicationDesc = string.Empty;
                            this.otherData = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppLog.writeLog(ex);
                    this.response = ResponseCode.Error;
                    this.responseMessage = ex.Message;
                    this.helpMessage = string.Empty;
                    this.nextStep = string.Empty;
                    this.applicationDesc = string.Empty;
                    this.otherData = null;
                }
            }
            else
            {
                this.response = ResponseCode.Error;
                this.responseMessage = "no data return from store procedure";
                this.helpMessage = "no data return from store procedure";
                this.nextStep = string.Empty;
                this.applicationDesc = string.Empty;
                this.otherData = null;
            }
        }

        public StoreResult(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                string missColumn, logMessage = "";
                bool isRemove;
                List<DataTable> removeTable = new List<DataTable>();
                foreach (DataTable data in dataSet.Tables)
                {
                    isRemove = false;
                    missColumn = "";
                    if (!data.Columns.Contains("Response_Code"))
                    {
                        missColumn += "Response_Code, ";
                        isRemove = true;
                    }
                    if (!data.Columns.Contains("Response_Message"))
                    {
                        missColumn += "Response_Message, ";
                        isRemove = true;
                    }
                    if (!data.Columns.Contains("Help_Message"))
                    {
                        missColumn += "Help_Message, ";
                        isRemove = true;
                    }
                    if (!data.Columns.Contains("NextStep_Code"))
                    {
                        missColumn += "NextStep_Code, ";
                        isRemove = true;
                    }

                    if (isRemove)
                    {
                        logMessage += data.TableName + " doesn't contains columns " + missColumn.Substring(0, missColumn.Length - 2) + "." + Environment.NewLine;
                        removeTable.Add(data);
                    }
                }

                if (removeTable.Count > 0)
                {
                    foreach (DataTable table in removeTable)
                    {
                        dataSet.Tables.Remove(table);
                    }
                }

                if (dataSet.Tables.Count > 0)
                {
                    removeTable.Clear();
                    foreach (DataTable data in dataSet.Tables)
                    {
                        if (data.Rows == null || data.Rows.Count <= 0)
                        {
                            removeTable.Add(data);
                        }
                    }

                    if (removeTable.Count > 0)
                    {
                        foreach (DataTable table in removeTable)
                        {
                            dataSet.Tables.Remove(table);
                        }
                    }

                    if (dataSet.Tables.Count == 1)
                    {
                        try
                        {
                            DataTable data = dataSet.Tables[0];
                            this.response = new ResponseCode(data.Rows[0]["Response_Code"].ToString().Trim());
                            this.responseMessage = data.Rows[0]["Response_Message"].ToString();
                            this.helpMessage = data.Rows[0]["Help_Message"].ToString();
                            this.nextStep = data.Rows[0]["NextStep_Code"].ToString();

                            data.Columns.Remove("Response_Code");
                            data.Columns.Remove("Response_Message");
                            data.Columns.Remove("Help_Message");
                            data.Columns.Remove("NextStep_Code");

                            if (data.Columns.Contains("Application_Desc"))
                            {
                                this.applicationDesc = data.Rows[0]["Application_Desc"].ToString();
                                data.Columns.Remove("Application_Desc");
                            }

                            this.otherData = data;
                            if (this.response == ResponseCode.Error)
                            {
                                logMessage = "store procedure return error (ResponseCode = \"300\")" + Environment.NewLine;
                                logMessage += "Response Message = " + this.responseMessage + Environment.NewLine;
                                logMessage += "Help Message = " + this.helpMessage + Environment.NewLine;
                                logMessage += "Application Desc = " + this.applicationDesc + Environment.NewLine;
                                AppLog.writeLog(logMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            AppLog.writeLog(ex);
                            this.response = ResponseCode.Error;
                            this.responseMessage = ex.Message;
                            this.helpMessage = string.Empty;
                            this.nextStep = string.Empty;
                            this.applicationDesc = string.Empty;
                            this.otherData = null;
                        }
                    }
                    else if (dataSet.Tables == null || dataSet.Tables.Count <= 0)
                    {
                        AppLog.writeLog("store procedure return data no record.");
                        this.response = ResponseCode.Error;
                        this.responseMessage = "store procedure return data no record";
                        this.helpMessage = string.Empty;
                        this.nextStep = string.Empty;
                        this.applicationDesc = string.Empty;
                        this.otherData = null;
                    }
                    else
                    {
                        AppLog.writeLog("store procedure return more than 1 table with all standard columns.");
                        this.response = ResponseCode.Error;
                        this.responseMessage = "store procedure return more than 1 table with all standard columns.";
                        this.helpMessage = string.Empty;
                        this.nextStep = string.Empty;
                        this.applicationDesc = string.Empty;
                        this.otherData = null;
                    }
                }
                else
                {
                    AppLog.writeLog("data return from store procedure doesn't contains all standard columns." + Environment.NewLine + logMessage);
                    this.response = ResponseCode.Error;
                    this.responseMessage = "data return from store procedure doesn't contains all standard columns";
                    this.helpMessage = string.Empty;
                    this.nextStep = string.Empty;
                    this.applicationDesc = string.Empty;
                    this.otherData = null;
                }
            }
            else
            {
                this.response = ResponseCode.Error;
                this.responseMessage = "no data return from store procedure";
                this.helpMessage = string.Empty;
                this.nextStep = string.Empty;
                this.applicationDesc = string.Empty;
                this.otherData = null;
            }
        }

        public StoreResult(ResponseCode response, string message = "", string help = "", string next = "", string application_desc = "", DataTable data = null)
        {
            this.response = response;
            this.responseMessage = message;
            this.helpMessage = help;
            this.nextStep = next;
            this.applicationDesc = application_desc;
            this.otherData = data;
        }

        public StoreResult(ResponseCode response, AlertMessage message, string next = "", string application_desc = "", DataTable data = null)
        {
            this.response = response;
            this.responseMessage = message.message;
            this.helpMessage = message.help;
            this.nextStep = next;
            this.applicationDesc = application_desc;
            this.otherData = data;
        }

        public StoreResult(LogResponse log)
        {
            this.response = log.respone;
            this.responseMessage = log.message;
            this.helpMessage = log.helpMessage;
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
    }

    public class ProcessResult
    {
        public readonly ResponseCode response;
        public readonly string responseMessage;
        public readonly string helpMessage;
        // use for show super user login or call specific process
        public readonly bool needNextProcess;
        public readonly object data;
        public readonly object data2;
        public readonly NotifyMessage[] notify = null;
        public readonly string functionID = "";

        public ProcessResult(ResponseCode reponse, string responseMessage = "", string helpMessage = "", bool needNextProcess = false, object data = null, object data2 = null, List<NotifyMessage> notify = null)
        {
            this.response = reponse;
            this.responseMessage = responseMessage;
            this.helpMessage = helpMessage;
            this.needNextProcess = needNextProcess;
            this.data = data;
            this.data2 = data2;
            if (notify != null && notify.Count > 0)
            {
                this.notify = notify.ToArray();
            }
        }

        public ProcessResult(ResponseCode reponse, AlertMessage message, bool needNextProcess = false, object data = null, object data2 = null, List<NotifyMessage> notify = null)
        {
            this.response = reponse;
            this.responseMessage = message.message;
            this.helpMessage = message.help;
            this.needNextProcess = needNextProcess;
            this.data = data;
            this.data2 = data2;
            if (notify != null && notify.Count > 0)
            {
                this.notify = notify.ToArray();
            }
        }

        public ProcessResult(StoreResult store, bool needNextProcess = false, object data = null, object data2 = null, List<NotifyMessage> notify = null)
        {
            this.response = store.response;
            this.responseMessage = store.responseMessage;
            this.helpMessage = store.helpMessage;
            this.needNextProcess = needNextProcess;
            this.data = store.otherData;
            this.functionID = store.functionID;

            this.data = data;
            this.data2 = data2;
            if (notify != null && notify.Count > 0)
            {
                this.notify = notify.ToArray();
            }
        }

        public ProcessResult(LogResponse log)
        {
            this.response = log.respone;
            this.responseMessage = log.message;
            this.helpMessage = log.helpMessage;
            this.needNextProcess = false;
            this.data = null;
        }
    }

    public struct NotifyMessage
    {
        public ResponseCode response;
        public string message;
        public string help;

        public NotifyMessage(ResponseCode response, string message, string help = "")
        {
            this.response = response;
            this.message = message;
            this.help = help;
        }
    }
}
