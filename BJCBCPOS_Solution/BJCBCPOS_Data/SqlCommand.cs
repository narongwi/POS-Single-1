using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BJCBCPOS_Model;

namespace BJCBCPOS_Data
{
    /// <summary>
    /// SqlCommand class
    /// store sql command of all process.
    /// </summary>
    public class SqlCommand
    {
        private DBConnect command = null;
        private int _timeout = ProgramConfig.commandTimeout;

        /// <summary>
        /// Constructor create new object with connection string with default timeout (30 seconds). 
        /// </summary>
        /// <param name="connectionString">connection string to database.</param>
        public SqlCommand(string connectionString)
        {
            try
            {
                command = new DBConnect(connectionString);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public void changeConnectionString(string connectionString)
        {
            if (command != null)
            {
                command.RollbackTransaction();
                command.closeConnection();
            }
            command = new DBConnect(connectionString);
        }

        /// <summary>
        /// create new transaction
        /// </summary>
        public void newTransaction()
        {
            try
            {
                command.BeginTransaction();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// commit current transaction
        /// </summary>
        public void commit()
        {
            try
            {
                command.CommitTransaction();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// rollback current transaction
        /// </summary>
        public void rollback()
        {
            try
            {
                command.RollbackTransaction();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool testConnection()
        {
            try
            {
                command.SetCommandText("select getdate();");
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public DataTable getApplicationDBLogin()
        {
            try
            {
                command.SetCommandText("select * from UserApplication where USERID='BIGAPP'");
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable getLogoImageFile(string fileName)
        {
            try
            {
                command.SetCommandText(string.Format(@"select * from POSIMAGE WHERE ImageName = '{0}'", fileName));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return null;
            }
        }

        public StoreResult checkAppVersion()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckApplication");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_CheckVersionofApplication.formatValue);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                command.AddInputParameter("AppVersion", SqlDbType.NVarChar, ProgramConfig.version);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getPosConfig()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetConfig");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_LoadConfigDatatoPOSClient.formatValue);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getPaymentConfig(FunctionID function, string refNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetPaymentConfig");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, function.formatValue);

                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    command.AddInputParameter("CustomerNo", SqlDbType.NVarChar, ProgramConfig.memberProfileMMFormat.Customer_No);
                }
                
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getPaymentPolicy()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetPaymentPolicy");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_LoadPaymentPolicy.formatValue);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getAlertMessage()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetAppAlertMessage");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_LoadAppAlertMessage.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getPosImage()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetPOSImage");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_LoadPosImage.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getLanguage()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetDisplayLanguage");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_LoadLanguage.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkLogin(string user, string password)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckUser");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, user);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Login_ValidUserIDPassword.formatValue);
                command.AddInputParameter("Passw", SqlDbType.VarChar, password);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkUserAuthorize(FunctionID functionId, string user, string password, string refNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckUser");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, user);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("Passw", SqlDbType.VarChar, password);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                command.AddInputParameter("CurrentUserID", SqlDbType.NVarChar, ProgramConfig.userId);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getAuthority()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetAuthority");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkTerminal()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckTerminal");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_CheckTerminal.formatValue);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                command.AddInputParameter("AppVersion", SqlDbType.NVarChar, ProgramConfig.version);
                command.AddInputParameter("IpAddress", SqlDbType.NVarChar, ProgramConfig.ipAddress);
                //Fix Data
                //command.AddInputParameter("IpAddress", SqlDbType.NVarChar, "172.16.152.109");
                command.AddInputParameter("ComputerName", SqlDbType.NVarChar, ProgramConfig.computerName);
                command.AddInputParameter("LastUser", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("AbbNo", SqlDbType.NVarChar, ProgramConfig.abbNo);
                command.AddInputParameter("CNNo", SqlDbType.NVarChar, ProgramConfig.cnNo);
                command.AddInputParameter("FFTINo", SqlDbType.NVarChar, ProgramConfig.fftiNo);
                command.AddInputParameter("SaleInternalRef", SqlDbType.NVarChar, ProgramConfig.saleRefNoIni);
                command.AddInputParameter("VoidInternalRef", SqlDbType.NVarChar, ProgramConfig.voidRefNoIni);
                command.AddInputParameter("RetnInternalRef", SqlDbType.NVarChar, ProgramConfig.returnRefNoIni);
                command.AddInputParameter("CashinoutInternalRef", SqlDbType.NVarChar, ProgramConfig.cashInRefNoIni);
                command.AddInputParameter("LoginInternalRef", SqlDbType.NVarChar, ProgramConfig.endOfShiftRefNoIni);
                command.AddInputParameter("ExportPermitRef", SqlDbType.NVarChar, ProgramConfig.expermitRefNoIni);
                command.AddInputParameter("OpendayInternalRef", SqlDbType.NVarChar, ProgramConfig.openDayRefNoIni);
                command.AddInputParameter("PosRepRef", SqlDbType.NVarChar, ProgramConfig.posrepRefNoIni);
                command.AddInputParameter("DelEditItemInternalRef", SqlDbType.NVarChar, ProgramConfig.actionRefNoIni);
                command.AddInputParameter("HoldRef", SqlDbType.NVarChar, ProgramConfig.holdOrderRefNoIni);
                command.AddInputParameter("TempFFTIRef", SqlDbType.NVarChar, ProgramConfig.tempFFTINo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public int getRecLogin(string refNo)
        {
            try
            {
                command.SetCommandText(string.Format(@"select isnull(min(rec),0)-1 from dlyptrans with(nolock) 
                    where ref={0} and rec<0 and sty='L' and (vty='I' or vty='O')", refNo));
                DataTable data = command.ExecuteToDataTable();
                if (data != null && data.Rows != null && data.Rows.Count > 0)
                {
                    return Convert.ToInt32(data.Rows[0][0].ToString());
                }
                return -1;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return 0;
            }
        }

        public StoreResult insertDlyptrans(string refNo, int rec, string sty, string vty, string pcd, string qnt, string amt, double fds, double upc, string egp)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"INSERT INTO [DLYPTRANS] with(ROWLOCK)
                        ([STCODE],  [REF],  [REC],  [STY],  [VTY],  [PCD],  [QNT],  [AMT],  [FDS],  [TTM],      [USR], [EGP], [UPC],  [REASON_ID]) 
                        VALUES
                        (N'{0}',     N'{1}',  {2},    N'{3}',  N'{4}',  N'{5}',  N'{6}',    {7},    {8},  getdate(),  N'{9}', '{10}' ,{11},   0)"
                        , ProgramConfig.storeCode
                        , refNo
                        , rec
                        , sty
                        , vty
                        , pcd
                        , qnt
                        , amt
                        , fds
                        , ProgramConfig.userId
                        , egp
                        , upc));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public int countCashierCtlLogin()
        {
            try
            {
                command.SetCommandText(string.Format(@"select count(*)  from  CASHIERCTL  with(nolock) 
                    where STCODE='{0}' and USERID='{1}'
                    and LOCKNO='{2}' and STATUS=' '"
                    , ProgramConfig.storeCode
                    , ProgramConfig.userId
                    , ProgramConfig.tillNo));

                DataTable data = command.ExecuteToDataTable();
                if (data != null && data.Rows != null && data.Rows.Count > 0)
                {
                    return Convert.ToInt32(data.Rows[0][0].ToString());
                }
                return 0;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return -1;
            }
        }

        public StoreResult insertCashierCtlLogin(string refNo, string maxID)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"Insert into CASHIERCTL(CASHID,STCODE,USERID,LOCKNO,STARTRCV,LOGINDATE)
                        values ({0}, N'{1}', N'{2}', N'{3}', {4}, getdate())"
                        , maxID
                        , ProgramConfig.storeCode
                        , ProgramConfig.userId
                        , ProgramConfig.tillNo
                        , refNo));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "");
            }
            catch (NetworkConnectionException ex)
            {

                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public string getMaxCashierID()
        {
            command.SetCommandText(String.Format(
            @"select isnull(max(CASHID),0) + 1 as CTL from CASHIERCTL where STCODE='{0}'", ProgramConfig.storeCode));

            DataTable data = command.ExecuteToDataTable();

            if (data != null && data.Rows != null && data.Rows.Count > 0)
            {
                return data.Rows[0]["CTL"].ToString();
            }

            return "1";
        }

        public StoreResult saveLoginTransaction()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveLoginTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.NVarChar, FunctionID.Login_SaveLoginTransaction.formatValue);
                command.AddInputParameter("AbbNo", SqlDbType.NVarChar, ProgramConfig.abbNo);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult syncToDataTank(string eventName, FunctionID functionId, string referenceNo, string rec, string abbNo, string superUserID = null)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_POS2DataTank");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, superUserID ?? "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, referenceNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("Event", SqlDbType.NVarChar, eventName);
                command.AddInputParameter("Database", SqlDbType.NVarChar, ProgramConfig.IsStandAloneMode ? "LOCALPOS" : "ESTORE");
                command.AddInputParameter("Rec", SqlDbType.NVarChar, rec);
                command.AddInputParameter("AbbNo", SqlDbType.NVarChar, abbNo);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getAmountExchangeRate(string SaleAmt, string mode, string pmCode, string pmMainCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetAmountExchangeRate");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.NoFunctionID.formatValue);
                command.AddInputParameter("SaleAMT", SqlDbType.NVarChar, SaleAmt);
                command.AddInputParameter("Mode", SqlDbType.Char, mode);
                command.AddInputParameter("PaymentMainCode", SqlDbType.NVarChar, pmMainCode);
                command.AddInputParameter("FXCUCode", SqlDbType.Char, pmCode);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkLastReceipt(string referenceNo, string abbNo, string abbNo_ini, FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckLastReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, referenceNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("AbbNo", SqlDbType.NVarChar, abbNo);
                command.AddInputParameter("AbbNo_INI", SqlDbType.NVarChar, abbNo_ini);
                command.AddInputParameter("SaleInternalRef", SqlDbType.NVarChar, ProgramConfig.saleRefNoIni);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult generateMenu(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GenerateMenu");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashInRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getMessageCashierStatus(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetCashierMessage");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getCashierMessage()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayCashierMessage");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Login_CashierMessage_Enabled.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getRunning(FunctionID functionId, RunningReceiptID receiptId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetRunning");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("ReceiptID", SqlDbType.Int, (int)receiptId);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkOpenDay(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckOpenDay");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult downloadHOToPOS()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DownLoadDataHO2POS");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.OpenDay_DownLoadDatafromHeadOfficetoClient.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveOpenDayTransaction()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveOpenDayTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.openDayRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.OpenDay_SaveOpenDayTransaction.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printOpenDay()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintOpenDay");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.openDayRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.OpenDay_PrintOpenDayDocument.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkChangeAmount(FunctionID functionId, double amount, Currency currency)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckChangeAmt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, ProgramConfig.superUserId);
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashInRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("ChangeAmt", SqlDbType.Money, amount);
                command.AddInputParameter("Currency", SqlDbType.Char, currency.code);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveTempCashIn(string cashType, string PM_CODE, string SUB_PM_CODE, string EXCG_RATE, string EXCG_AMT, string CASH_AMT, string ODATE)
        {
            try
            {
                command.SetCommandText(String.Format(@"insert into TempCashInTrans(STCODE, REF, TRANS_TYPE, CASH_TYPE, PM_CODE, SUB_PM_CODE, EXCG_RATE, EXCG_AMT, CASH_AMT, TTM, ODATE) 
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', getdate(), N'{9}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.cashInRefNo
                                                , "I"
                                                , cashType
                                                , PM_CODE
                                                , SUB_PM_CODE
                                                , EXCG_RATE
                                                , EXCG_AMT
                                                , CASH_AMT
                                                , ODATE));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveCashInTransaction(string openDrawerTime, string closeDrawerTime)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveCashInTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashInRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashIn_SaveCashInTransaction.formatValue);
                command.AddInputParameter("OpenDrawerTime", SqlDbType.Char, openDrawerTime);
                command.AddInputParameter("CloseDrawerTime", SqlDbType.Char, closeDrawerTime);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printCashIn()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCashIn");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashInRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashIn_PrintCashInDocument.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getCashUnit(string currencyCode, string refNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetCashUnit");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "");
                command.AddInputParameter("CurrencyCode", SqlDbType.Char, currencyCode);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        //Sale
        public StoreResult posDisplayContent()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayContent");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeCode.formatValue);
                command.AddInputParameter("SaleMode", SqlDbType.Char, (int)ProgramConfig.saleMode);
                command.AddInputParameter("Version", SqlDbType.VarChar, ProgramConfig.version);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult posCheckCashIn()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckCashIn");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashOut_NormalChange_CheckData.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult posCheckCashInSaleAmt()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckCashInSaleAmt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_BeforeInputProductItem_CheckSaleCashIn.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult searchMember(FunctionID functionId, int searchType, string data)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SearchMember");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("DataType", SqlDbType.Int, searchType);
                command.AddInputParameter("DataSearch", SqlDbType.NVarChar, data);
                if (functionId == FunctionID.Sale_Member_Search_Data || functionId == FunctionID.Deposit_SearchMember2)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputCustomer_ByMember)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getMemberProfile(FunctionID functionId, string memberID)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetMemberProfile");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("MemberID", SqlDbType.NVarChar, memberID);
                if (functionId == FunctionID.Sale_Member_Display)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputCustomer_Display)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getDisplayDiscountManual()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayDiscountManual");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                //command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_InputSaleItem_EditProduct_DiscountByProductManual.formatValue);
                command.AddInputParameter("MemberID", SqlDbType.VarChar, ProgramConfig.memberId);
                command.AddInputParameter("DiscountGroup", SqlDbType.Char, "I");

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectPaymentType(string paymentMainCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"select PaymentTypeId from payment where PaymentMaincode = '{0}'"
                                                    , paymentMainCode
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectTempDlyptrans(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC, QNT, AMT, VTY FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] >= 0"
                                                    , refNo
                                                    , ProgramConfig.storeCode
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectTypeFromMaxRec(string rec)
        {
            try
            {

                command.SetCommandText(String.Format(@"SELECT PCD , AMT FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] = {2} "
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.storeCode
                                                ,rec
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public int selectMaxRecTempDlyptrans(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT isnull(max(REC),0) AS REC FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] >= 0
                                            AND [REC] < 2000 "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return -1;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public float selectMaxRecTempDlyptransForTypeP(string refNo)
        {
            try
            {

                command.SetCommandText(String.Format(@"SELECT isnull(max(QNT),0) AS REC FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'P'"
                                                , refNo
                                                , ProgramConfig.storeCode
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return float.Parse(dt.Rows[0][0].ToString());
                }
                return -1f;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public int selectMaxRecTempDlyptransForTypeP_FormPMCODE(string pm_Code)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT isnull(max(REC),0) AS REC FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'P'
                                            AND [PCD] = '{2}'"
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.storeCode
                                                , pm_Code
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return -1;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectTempDlyptransForTypeP_FromPMCODE(string pm_Code)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'P'
                                            AND [PCD] like '{2}%'"
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.storeCode
                                                , pm_Code
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public int selectMaxRecTempDlyptransForTypeD_FormPMCODE(string pm_Code)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT isnull(max(REC),0) AS REC FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'D'
                                            AND [PCD] = '{2}'"
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.storeCode
                                                , pm_Code
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return -1;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public int selectMaxRecTempDlyptransForTypeP_FXCU_Diif()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT isnull(max(REC),0) AS REC FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'P'
                                            AND [PCD] like 'FXCU%'
                                            AND [FDS] > 0"
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.storeCode
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return -1;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }


        public DataTable selectMaxRecReturnTempDlyptrans()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT isnull(max(REC),0) AS REC FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [STY] = '3'
                                            AND [REC] >= 0
                                            AND [VTY] in ('0','1') "
                                                , ProgramConfig.returnRefNo
                                                , ProgramConfig.storeCode
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable loadTempDlyptrans(string refNo)
        {
            try
            {
                //string saleRef = refLoadTemp == "" ? refNo : refLoadTemp;
                //string whereCondition = refLoadTemp == "" ? " AND [STT] != 'V' AND [REC] < 2000 " : "";

                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [STT] != 'V' AND [REC] < 2000
                                            AND [VTY] in ('0','1')
                                            AND [REC] >= 0                                           "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable loadReturnTempDlyptrans(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [STY] = '3'
                                            AND [STT] != 'V'
                                            AND [VTY] in ('0','1')
                                            AND [REC] >= 0 "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable loadTempDlyForPayment(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [STT] != 'V'
                                            AND [VTY] in ('P')
                                            AND [REC] > 0
                                            AND [REC] < 2000
                                            AND [PCD] != 'CHGD'"
                                                , refNo
                                                , ProgramConfig.storeCode
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDeleteItemTempDlyptrans(string refNo, string code, string quant, string price)
        {
            try
            {
                    command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [PCD] = '{2}'
                                            AND [QNT] = '{3}'
                                            AND [AMT] = '{4}'
                                            AND [STT] != 'V'
                                            AND [REC] >= 0 "
                                                    , refNo
                                                    , ProgramConfig.storeCode
                                                    , code
                                                    , quant
                                                    , price
                                                    ));
                    return command.ExecuteToDataTable();                
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectReturnItemTempDlyptrans(string refNo, string code, string quant, string price)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [PCD] = '{2}'
                                            AND [QNT] = '{3}'
                                            AND [AMT] = '{4}'
                                            AND [VTY] in ('0','1')
                                            AND [REC] >= 0 "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , code
                                                , quant
                                                , price
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectReturnVatItemTempDlyptrans()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = '1'
                                            AND [REC] >= 0 "
                                                , ProgramConfig.returnRefNo
                                                , ProgramConfig.storeCode
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectAllDeleteItemTempDlyptrans(string refNo, string code, string price, string discID)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [PCD] = '{2}'
                                            AND [UPC] = '{3}'
                                            AND [DISCID] = '{4}'
                                            AND [STT] != 'V'
                                            AND [REC] >= 0 "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , code
                                                , price
                                                , discID
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool updateTempDlyptrans(string refNo, string rec)
        {
            string resRec = (Convert.ToInt32(rec) + 10000).ToString();
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                                           SET [REC] = '{0}'
                                                                ,[STT] = 'V'
                                                            WHERE [REF] = '{1}'
                                                            AND [STCODE] = '{2}'
                                                            AND [REC] = '{3}' "
                                                , resRec
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , rec
                                                ));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool updateTempDlyptransLogin (string refNo, string rec)
        {
            string resRec = (Convert.ToInt32(rec) + 10000).ToString();
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                           SET [REC] = '{0}'
                                                ,[STT] = 'V'
                                            WHERE [REF] = '{1}'
                                            AND [STCODE] = '{2}'
                                            AND [REC] = '{3}'"
                                                , resRec
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , rec
                                                ));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult updatePrintTempDlyptrans(string refNo, string discID)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [DLYPTRANS] with(ROWLOCK)
                                           SET [DISCID] = '1'
                                            WHERE VTY = 'F'
                                            AND [REF] = '{0}'"
                                                , refNo
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult updateReturnFSlot(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [DLYPTRANS] with(ROWLOCK)
                                           SET [STT] = 'T'
                                               ,[EGP] = '{1}'
                                            WHERE VTY = 'F'
                                            AND [REF] = '{0}'"
                                                , refNo
                                                , ProgramConfig.tillNo
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public bool updateDeleteItemTempDlyptrans(string refNo, string rec)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                           SET [STT] = 'V'
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] = '{2}' "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , rec
                                                ));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult updateSuperuserIdTempF(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                           SET [PDISC] = '{2}'
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'F' "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.superUserId
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult updateNewAmtQtyTempDlyptrans(string refNo, string rec, string newAmt, string newQty)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                            SET [AMT] = '{3}'
                                            ,[QNT] = '{4}'                                            
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] = '{2}' "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , rec
                                                , newAmt
                                                , newQty
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult updateReturnVatTempDlyptrans(string dateTimeDaTa)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                            SET [PDISC] = '{2}'                                           
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [VTY] = 'V'
                                            AND [REC] >= '0' "
                                                , ProgramConfig.returnRefNo
                                                , ProgramConfig.storeCode
                                                , dateTimeDaTa
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult clearPDISCTempDlyptrans()
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                            SET [PDISC] = '0'                                           
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [STY] = '3'
                                            AND [VTY] in ('0','1')
                                            AND [REC] >= '0' "
                                                , ProgramConfig.returnRefNo
                                                , ProgramConfig.storeCode
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public DataTable selectDISCSUMMARYByDiscCode(string refNo, string discode)
        {
            try
            {
                command.SetCommandText(String.Format(@"select DISCCODE , MSG , DISCAMT , DISCABLE , PDISC from DISCSUMMARY with(NOLOCK) WHERE REF = '{0}' AND DISCCODE = '{1}'"
                                                    , refNo, discode));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDISCSUMMARY(string refNo, List<string> pmType)
        {
            try
            {
                command.SetCommandText(String.Format(@"select DISCCODE, DISCAMT from DISCSUMMARY with(NOLOCK) WHERE REF = '{0}' and PMTYPE in ('{1}') "
                                                    , refNo, String.Join("','", pmType)));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getProductDesc(FunctionID functionId, string productCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetProductDesc");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("ProductCode", SqlDbType.NVarChar, productCode);
                if (functionId == FunctionID.Sale_InputSaleItem_InputProduct_NormalSale)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputReturnItem_ByProduct_InputProduct_InputReturnProductCode)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getCheckProduct(FunctionID functionId, string productCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckProductDesc");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("ProductCode", SqlDbType.NVarChar, productCode);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getPromotionProduct(FunctionID functionId, string productCode, string member)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetPromotionProduct");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("Barcode", SqlDbType.NVarChar, productCode);
                command.AddInputParameter("MemberID", SqlDbType.NVarChar, member);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getDiscount(string discountCode, double discountValue, double saleAmount)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetDiscount");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "");
                command.AddInputParameter("MemberID", SqlDbType.VarChar, ProgramConfig.memberId);
                command.AddInputParameter("DiscountCode", SqlDbType.Char, discountCode);
                command.AddInputParameter("DiscountValue", SqlDbType.Money, discountValue);
                command.AddInputParameter("SaleAmount", SqlDbType.Money, saleAmount);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public int countTempDlyptrans(string refNo, string rec)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT ISNULL(COUNT(*), 0) FROM [TEMPDLYPTRANS] WITH(NOLOCK) 
                    WHERE STCODE = '{0}' AND REF = '{1}' AND REC = {2};"
                                                , ProgramConfig.storeCode
                                                , refNo
                                                , rec));
                DataTable data = command.ExecuteToDataTable();
                if (data != null && data.Rows != null && data.Rows.Count > 0)
                {
                    return Convert.ToInt32(data.Rows[0][0].ToString());
                }
                return 0;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return -1;
            }
        }

        public bool updateTempDlyptrans(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
            , string usr, string egp, string stt, string pdisc, string discid, string upc, string dty, string discamt, string reason, string stv)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"UPDATE [tempDLYPTRANS] with(ROWLOCK)
                        SET
                            [STY] = N'{3}', 
                            [VTY] = N'{4}', 
                            [PCD] = N'{5}', 
                            [QNT] = {6}, 
                            [AMT] = {7},
                            [FDS] = {8}, 
                            [USR] = N'{9}', 
                            [EGP] = {10}, 
                            [STT] = N'{11}', 
                            [PDISC] = {12}, 
                            [DISCID] = {13}, 
                            [UPC] = {14}, 
                            [DTY] = N'{15}', 
                            [DISCAMT] = {16}, 
                            [REASON_ID] = {17}, 
                            [STV] = N'{18}'
                        WHERE [STCODE] = N'{0}' AND [REF] = N'{1}' AND [REC] = {2};", 
                    ProgramConfig.storeCode, refNo, rec, sty, vty, pcd, qnt, amt, fds, usr, 
                    egp, stt, pdisc, discid, upc, dty, discamt, reason, stv));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool updateTempDlyptransFDS(string rec, string fds, string pdisc)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"UPDATE [tempDLYPTRANS] SET FDS = {0}, PDISC = {1} WHERE REF = '{2}' AND STCODE = '{3}' AND REC = '{4}'",
                    fds, pdisc, ProgramConfig.saleRefNo, ProgramConfig.storeCode, rec));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool updateTempPay(string pay, string chg, string fxcu_qnt)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"UPDATE [TempPay] SET CHG = {0}, FXCUQNT = {1} WHERE REF = '{2}' AND pay = '{3}' and type = 'P'",
                   
                    chg, fxcu_qnt, ProgramConfig.saleRefNo, pay ));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool saveTempDlyptrans(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
            , string usr, string egp, string stt, string pdisc, string discid, string upc, string dty, string discamt, string reason, string stv)
        {
            try
            {
                    command.SetCommandText(String.Format(@"INSERT INTO [tempDLYPTRANS] with(ROWLOCK) ([STCODE], [REF], [REC], [STY], [VTY], [PCD], [QNT], [AMT],[FDS], [TTM], [USR], [EGP], [STT], [PDISC], [DISCID], [UPC], [DTY], [DISCAMT], [REASON_ID], [STV]) 
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', getdate(), N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}')"
                                                     , ProgramConfig.storeCode
                                                     , refNo
                                                     , rec
                                                     , sty
                                                     , vty
                                                     , pcd
                                                     , qnt
                                                     , amt
                                                     , fds
                                                     , usr
                                                     , egp
                                                     , stt
                                                     , pdisc
                                                     , discid
                                                     , upc
                                                     , dty
                                                     , discamt
                                                     , reason
                                                     , stv));
                    command.ExecuteNonQuery();          
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult saveTempReturn(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
            , string usr, string egp, string stt, string stv, string reason, string pdisc, string discid, string discamt, string upc, string dty)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [tempDLYPTRANS] with(ROWLOCK) ([STCODE], [REF], [REC], [STY], [VTY], [PCD], [QNT], [AMT],[FDS], [TTM], [USR], [EGP], [STT], [STV], [REASON_ID] , [PDISC], [DISCID], [DISCAMT], [UPC], [DTY]) 
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', getdate(), N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}')"
                                                , ProgramConfig.storeCode
                                                , refNo
                                                , rec
                                                , sty
                                                , vty
                                                , pcd
                                                , qnt
                                                , amt
                                                , fds
                                                , usr
                                                , egp
                                                , stt
                                                , stv
                                                , reason
                                                , pdisc
                                                , discid
                                                , discamt
                                                , upc
                                                , dty));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }
   
        public bool saveQRPayTransOffline(string refNo, string action_type, string rec, string refNoInp, string amount, string stt, string flag, string channal, string bankCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [QRPAYTRANS] with(ROWLOCK) (STORE_CODE, OPERATE_DATE, LOCKNO, REF, ACTION_TYPE, SEQ, ORG_TRANID, TRAN_ID, ACCOUNT_NAME, QR_CODE, 
                                                        PAY_AMOUNT, TXN_STATUS, TTM, STT, RESPONSE_CODE, ERRORCODE, RESDESCRIPTION, ONLINE_FLAG, AUTHORIZE, AUTHORIZE_BY, CHANNEL, BANKCODE) 
                                                VALUES(N'{0}', N'{1}', {2}, N'{3}', N'{4}', '{5}', (select dbo.GetQRPaymentUID('{0}', '{3}', '{2}', 'RQ', 'CB', '')), N'{6}', '', '', N'{7}', '', getdate(), N'{8}', '', '', '', N'{9}', '', '', N'{10}', N'{11}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , refNo
                                                , action_type 
                                                , rec
                                                , refNoInp
                                                , amount
                                                , stt
                                                , flag
                                                , channal
                                                , bankCode)
                                      );
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool saveBarcodeExtend(string refNo, string rec, string pcd, string dty)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [BarcodeExtend] with(ROWLOCK) ([STCODE], [REF], [REC], [PCD], [TTM], [PRODUCT_TYPE]) 
                                                VALUES(N'{0}', N'{1}', {2}, N'{3}', getdate(), '{4}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.saleRefNo
                                                , rec
                                                , pcd
                                                , dty));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool saveEditItemTrans(string refNo, string aid, string aty, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
            , string usr, string stt, string referRec, string reason, string upc)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [EditItemTrans] with(ROWLOCK)
                                                        ([STCODE], [REF], [ABB], [AID],[ATY], [REC], [STY], [VTY], [PCD], [QNT]
                                                        , [AMT], [FDS], [TTM], [USR], [STT], [REFER_REC], [REASON_ID], [UPC])
                                                 VALUES
                                                       (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}'
                                                       , N'{10}', N'{11}', getdate(), N'{12}', N'{13}', N'{14}', N'{15}', N'{16}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.abbNo
                                                , aid
                                                , aty
                                                , rec
                                                , sty
                                                , vty
                                                , pcd
                                                , qnt
                                                , amt
                                                , fds
                                                , ProgramConfig.userId
                                                , stt
                                                , referRec
                                                , reason
                                                , upc));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult searchItem(string productCode, SearchItemAction action)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SearchItem");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("ProductCode", SqlDbType.VarChar, productCode);
                command.AddInputParameter("Action", SqlDbType.Char, action);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getProductIcon()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetProductIcon");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_InputSaleItem_InputProduct_NormalSale.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult displayReason(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayReason");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);

                if (functionId == FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason ||
                    functionId == FunctionID.Sale_InputSaleItem_EditProduct_EditPrice_InputReason ||
                    functionId == FunctionID.Sale_CancelWhileSale_CancelOrder_InputReason)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputReturnReason)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }
                else if (functionId == FunctionID.Void_InputReason || functionId == FunctionID.Deposit_InputVoid || functionId == FunctionID.ReceivePOD_InputVoid || functionId == FunctionID.CreditSale_CreditInputReason)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.voidRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getDiscountItem(string refNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetDiscountItem");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_ProcessBeforePayment.formatValue);
                command.AddInputParameter("MemberID", SqlDbType.NVarChar, ProgramConfig.memberId);
                command.AddInputParameter("EmployeeId", SqlDbType.VarChar, ProgramConfig.employeeID);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getTotalAmtDiff(string refNo, string saleAmt, string mode, string pmCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetTotalAmtDiff");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_TotalAmtDiff.formatValue);
                command.AddInputParameter("SaleAMT", SqlDbType.Char, saleAmt);
                command.AddInputParameter("Mode", SqlDbType.Char, mode);
                command.AddInputParameter("PaymentCode", SqlDbType.Char, pmCode);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveCancelSaleTransaction(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveCancelTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveCancelTransaction(FunctionID functionId, string receiptNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveCancelTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, receiptNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool saveTempPay(string refNo, string type, string pay, string amt, string chg, string fxcuqnt, string pdisc)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO TEMPPAY with(ROWLOCK) (REF, TYPE, PAY,AMT,CHG,FXCUQNT,PDISC)
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}')"
                                                , refNo
                                                , type
                                                , pay
                                                , amt
                                                , chg
                                                , fxcuqnt
                                                , pdisc));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public DataTable selectDataSCANGFSL()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REF , GFSL_NO FROM [SCANGFSL] with(NOLOCK) WHERE REF = '{0}'"
                                                    , ProgramConfig.saleRefNo
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }      

        public StoreResult saveScanGFSL(string gfslNo, string gfslAmt)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT [SCANGFSL] with(ROWLOCK) ([REF], [GFSL_NO], [GFSL_VALUES], [STATUS], [SDATE],[USER_APPROVE],[REASON_ID],[MODE]) 
                                                            VALUES(N'{0}', N'{1}', N'{2}', '', getdate(),'',0, N'OFF')"
                                                , ProgramConfig.saleRefNo
                                                , gfslNo
                                                , gfslAmt));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public bool saveEdcTran(string cardNo, string approveCode, string edcAmount)
        {
            try
            {
                command.SetCommandText(String.Format(@"DELETE FROM [EDCTRANS] with(ROWLOCK) WHERE [STCODE]='{0}' and [REF]='{1}' and [card_no] = '{2}';
                                                    
                                                    INSERT INTO [EDCTRANS] with(ROWLOCK) ([STCODE],[REF],[CARD_NO], [TERMINAL_ID],[APPROVE_CODE],[TRACE_NO],[EDCDATE], [TDATE],[EDC_AMOUNT],[CARD_ISSUER_NAME],[EDC_VATAMOUNT]) 
                                                    VALUES(N'{0}',N'{1}',N'{2}', '',N'{3}','' ,'', getDate(),'{4}','',0);
                                                   "
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.saleRefNo
                                                , cardNo
                                                , approveCode
                                                , edcAmount));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public DataTable selectDataToDeleteCashTempDLY()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC , AMT, QNT FROM [TEMPDLYPTRANS] with(NOLOCK) WHERE REF = '{0}' AND PCD like '{1}%'"
                                                    , ProgramConfig.saleRefNo
                                                    , ProgramConfig.paymentType
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDataToDeleteTempDLY()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC FROM [TEMPDLYPTRANS] with(NOLOCK) WHERE REF = '{0}' AND PCD = '{1}' AND AMT = '{2}'"
                                                    , ProgramConfig.saleRefNo
                                                    , ProgramConfig.paymentType
                                                    , ProgramConfig.paymentAmt
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDataToDeleteTempDLY(string rec)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC FROM [TEMPDLYPTRANS] with(NOLOCK) WHERE REF = '{0}' AND PCD = '{1}' AND AMT = '{2}' AND REC = '{3}'"
                                                    , ProgramConfig.saleRefNo
                                                    , ProgramConfig.paymentType
                                                    , ProgramConfig.paymentAmt
                                                    , rec
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDataToDeleteTempDLYByPCD(string pcd)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC FROM [TEMPDLYPTRANS] with(NOLOCK) WHERE REF = '{0}' AND PCD = '{1}'"
                                                    , ProgramConfig.saleRefNo
                                                    , pcd
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDataToDeleteByVTYTempDLY(string vty)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC FROM [TEMPDLYPTRANS] with(NOLOCK) WHERE REF = '{0}' AND VTY = '{1}'"
                                                    , ProgramConfig.saleRefNo
                                                    , vty
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDataToDeleteByDiscountTempDLY(string vty, string pcd)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REC FROM [TEMPDLYPTRANS] with(NOLOCK) WHERE REF = '{0}' AND VTY = '{1}'"
                                                    , ProgramConfig.saleRefNo
                                                    , vty
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDiscountCode(string discountCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT DiscountId FROM discount with(NOLOCK) WHERE DiscountCode = '{0}'"
                                                    , discountCode
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool deleteTempDlyTransByRefRec(string recNo)
        {
            try
            {
                command.SetCommandText(String.Format(@" 
                                                    EXEC [dbo].[delRefRecDLYPTrans] @RefItem = '{0}', @RecItem = {1};
                                                    
                                                   "
                                                , ProgramConfig.saleRefNo
                                                , recNo));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult deletePaymentType(string recNo)
        {
            try
            {
                string tempDepo = "";
                if (ProgramConfig.paymentType != null &&  ProgramConfig.paymentType.StartsWith("DEPO") && ProgramConfig.paymentType.Length > 4)
                {
                    tempDepo = ProgramConfig.paymentType.Substring(4, ProgramConfig.paymentType.Length - 4);
                }

                string refNo = "";
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale || ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
                {
                    refNo = ProgramConfig.saleRefNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    refNo = ProgramConfig.creditSaleNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    refNo = ProgramConfig.podRefNo;
                }

                command.SetCommandText(String.Format(@" 
                                                    EXEC [dbo].[delRefRecDLYPTrans] @RefItem = '{0}', @RecItem = {3};

                                                    DELETE TEMPPAY with(ROWLOCK) WHERE REF = '{0}' AND TYPE = 'P' AND PAY = '{1}' AND AMT = '{2}';

                                                    DELETE Temppay_detail with(ROWLOCK) WHERE REF = '{0}' AND PAY = '{1}' 

                                                    DELETE Tempdeposit_trans_history with(ROWLOCK) WHERE REF = '{0}' AND DEPOSIT_FFTI_NO = '{4}'
                                                   "
                                                , refNo
                                                , ProgramConfig.paymentType
                                                , ProgramConfig.paymentAmt
                                                , recNo
                                                , tempDepo));

                ProgramConfig.paymentType = "";
                ProgramConfig.paymentAmt = "";

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public bool deletePaymentTypeForCoupon(string recNo, string paymentType)
        {
            try
            {
                string refNo = "";
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale || ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
                {
                    refNo = ProgramConfig.saleRefNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    refNo = ProgramConfig.creditSaleNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    refNo = ProgramConfig.podRefNo;
                }

                command.SetCommandText(String.Format(@" 
                                                    EXEC [dbo].[delRefRecDLYPTrans] @RefItem = '{0}', @RecItem = {2};

                                                    DELETE TEMPPAY with(ROWLOCK) WHERE REF = '{0}' AND TYPE = 'P' AND PAY = '{1}';
                                                    
                                                    DELETE Temppay_detail with(ROWLOCK) WHERE REF = '{3}' AND PAY = '{1}' 
                                                    
                                                   "
                                                , ProgramConfig.saleRefNo
                                                , paymentType
                                                , recNo
                                                , refNo));

                ProgramConfig.paymentType = "";
                ProgramConfig.paymentAmt = "";

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult deleteAllPayment(string recNo)
        {
            try
            {
                string refNo = "";
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale || ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
                {
                    refNo = ProgramConfig.saleRefNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    refNo = ProgramConfig.creditSaleNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    refNo = ProgramConfig.podRefNo;
                }

                command.SetCommandText(String.Format(@" 
                                                    EXEC [dbo].[delRefRecDLYPTrans] @RefItem = '{0}', @RecItem = '{1}';

                                                    DELETE TEMPPAY with(ROWLOCK) WHERE REF = '{0}' AND TYPE = 'P';
                                                    
                                                    DELETE Temppay_detail with(ROWLOCK) WHERE REF = '{2}'; 

                                                    DELETE Tempdeposit_trans_history with(ROWLOCK) WHERE REF = '{0}'
                                                   "
                                                , ProgramConfig.saleRefNo
                                                , recNo
                                                , refNo));

                ProgramConfig.paymentType = "";
                ProgramConfig.paymentAmt = "";

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult updateOpenCashDrawer(string vtime, string refNo, string sty)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                           SET [PDISC] = '{0}'
                                            WHERE [STY] = '{2}'
                                            AND [VTY] = 'V'
                                            AND [REF] = '{1}'"
                                                , vtime, refNo
                                                , sty
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult delGFSL(string voucherNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DelGFSL");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("GiftVoucherNo", SqlDbType.NVarChar, voucherNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult delQRPayTrans(string paymentCode = "")
        {
            try
            {
                string where = "";
                if (!String.IsNullOrEmpty(paymentCode.Trim()))
                {
                    //Comment ทำพื่อรองรับ QR ของลาว
                    where = " AND BANKCODE = '" + paymentCode + "'";
                }
               
                command.SetCommandText(String.Format(
                    @"DELETE FROM QRPAYTRANS WHERE OPERATE_DATE = '{0}' AND REF = '{1}' AND CHANNEL <> 'MN' " + where
                                , ProgramConfig.operateDate
                                , ProgramConfig.saleRefNo
                                ));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult printCancel(FunctionID functionId, string saleRefNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCancel");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, ProgramConfig.superUserId);
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("CancelNo", SqlDbType.NVarChar, ProgramConfig.cancelNo);

            ProgramConfig.superUserId = "N/A";
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printCancelLogin(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCancel");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cancelNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);

                ProgramConfig.superUserId = "N/A";
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getPaymentCode(string paymentNumber)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetPaymentCode");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD ? ProgramConfig.podRefNo : ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("PaymentNumber", SqlDbType.VarChar, paymentNumber);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkGiftVoucher(string giftVoucherNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckGiftVoucher");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("GiftVoucherNo", SqlDbType.VarChar, giftVoucherNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult displayCoupon()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayCoupon");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkCoupon(string couponNo, int qty, string memberId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckCoupon");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("CouponNo", SqlDbType.NVarChar, couponNo);
                command.AddInputParameter("Coupon_Quant", SqlDbType.Int, qty);
                command.AddInputParameter("MemberID", SqlDbType.NVarChar, memberId);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult delCoupon(string couponNo, int qty, int vRow)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DelCoupon");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("CouponNo", SqlDbType.NVarChar, couponNo);
                command.AddInputParameter("CouponQnt", SqlDbType.Int, qty);
                if (vRow == 0)
                {
                    command.AddInputParameter("Row", SqlDbType.Int, null);
                }
                else
                {
                    command.AddInputParameter("Row", SqlDbType.Int, vRow);
                }


                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult delCouponAll()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DelCoupon_All");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult couponUse()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CouponUse");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("MemberID", SqlDbType.NVarChar, ProgramConfig.memberId);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveSaleTransaction(string memberId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveSaleTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("ABBNo_INI", SqlDbType.VarChar, ProgramConfig.abbNo);
                command.AddInputParameter("PrintInvoiceType", SqlDbType.VarChar, ProgramConfig.printInvoiceType);
                
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveSaleTransactionUpdateABBNO(string mode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveSaleTrans_UpdateABBno");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_SaveSaleTransaction.formatValue);
                command.AddInputParameter("ABBNo", SqlDbType.VarChar, ProgramConfig.abbNo);
                command.AddInputParameter("Mode", SqlDbType.VarChar, mode);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult concludeSale(string abbNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_ConcludeSale");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_ProcessAfterSaveSaleTransaction.formatValue);
                command.AddInputParameter("ABBNo", SqlDbType.VarChar, abbNo);

                string memID = ProgramConfig.memberFormat == MemberFormat.MegaMaket
                    ? ProgramConfig.memberCardNo : ProgramConfig.memberId;
                command.AddInputParameter("MemberID", SqlDbType.VarChar, memID);


                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printReceipt(string receiptNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintABB");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, receiptNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_Document_ReceiptABB.formatValue);
                command.AddInputParameter("ABBNo", SqlDbType.VarChar, ProgramConfig.abbNo);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveCloseDrawer(FunctionID functionId, string closeTime, string number)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveCloseDrawer");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("CloseTime", SqlDbType.Char, closeTime);
                if (functionId == FunctionID.Sale_CloseDrawerAndRecordTime)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                    //command.AddInputParameter("AbbNo", SqlDbType.VarChar, number);
                }
                else if (functionId == FunctionID.Return_CloseDrawerAndRecordTime)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                    //command.AddInputParameter("CNNo", SqlDbType.VarChar, number);
                }
                else if (functionId == FunctionID.Void_CloseDrawerAndRecordTime)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.voidRefNo);
                    //command.AddInputParameter("VoidReceiptNo", SqlDbType.VarChar, number);
                }
                command.AddInputParameter("ReceiptNo", SqlDbType.VarChar, number);


                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getCustomerFFTI(string taxId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetCustomerFFTI");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Return_InputCustomer_InputCustomer.formatValue);
                command.AddInputParameter("CusTaxId", SqlDbType.VarChar, taxId);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult searchReceipt(int type, string search, string refNo, string tillNo, FunctionID functionId, string superUserId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SearchReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, superUserId);
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("DataType", SqlDbType.Int, type);
                command.AddInputParameter("DataSearch", SqlDbType.VarChar, search);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult displayReceiptDetail(FunctionID functionId, string receiptNo, string tillNo, string superUserId,string saleType = "")
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayReceiptDetail");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, superUserId);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("ReceiptNo", SqlDbType.VarChar, receiptNo);
                if (functionId == FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }
                else if (functionId == FunctionID.Void_InputReceiptNo)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.voidRefNo);
                }
                else if (functionId == FunctionID.Report_CheckCurrentDayReceipt)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                }
                command.AddInputParameter("SaleType", SqlDbType.VarChar, saleType);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult displayReturnPayment(string returnType, string saleRef, double saleAmt, double returnAmt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayReturnPayment");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Return_SuggestReturnPaymentType.formatValue);
                command.AddInputParameter("ReturnType", SqlDbType.Char, returnType);
                command.AddInputParameter("SaleRef", SqlDbType.VarChar, saleRef);
                command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                command.AddInputParameter("ReturnAmt", SqlDbType.Money, returnAmt);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveReturnTransaction(string saleRef)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveReturnTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Return_SaveReturnTransaction.formatValue);
                command.AddInputParameter("CNNo_INI", SqlDbType.NVarChar, ProgramConfig.cnNo);
                command.AddInputParameter("SaleRef", SqlDbType.NVarChar, saleRef);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult concludeReturn(string cnNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_ConcludeReturn");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Return_ProcessAfterReturnTransaction.formatValue);
                command.AddInputParameter("CNNo", SqlDbType.VarChar, cnNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printCN(string cnNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCN");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Return_Document_CNCreditNote.formatValue);
                command.AddInputParameter("CNNo", SqlDbType.VarChar, cnNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printCopyCN(string cnNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCN_copy");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Return_Document_CopyOfCNCreditNote.formatValue);
                command.AddInputParameter("CNNo", SqlDbType.VarChar, cnNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkReceipt(string receiptNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.voidRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Void_InputReceiptNo.formatValue);
                command.AddInputParameter("ReceiptNo", SqlDbType.VarChar, receiptNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveVoidTransaction(string voidReceiptNo, string openDrawer, string reason, string refNo, FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveVoidTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, ProgramConfig.superUserId);
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("VoidReceiptNo", SqlDbType.VarChar, voidReceiptNo);
                command.AddInputParameter("OpenDrawer", SqlDbType.Char, openDrawer);
                command.AddInputParameter("ReasonID", SqlDbType.Int, reason);

                ProgramConfig.superUserId = "N/A";

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult concludeVoid(string voidReceiptNo, string refNo, FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_ConcludeVoid");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("VoidReceiptNo", SqlDbType.VarChar, voidReceiptNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult PrintVoidReceipt(string voidReceiptNo, string refNo, FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintVoidReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("VoidReceiptNo", SqlDbType.VarChar, voidReceiptNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkLastLoginSales()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckLastLoginSales");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashOut_Sale_CheckData.formatValue);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectPaymentByCode(string pamentCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"select PaymentMainCode, PaymentMainName from payment where PaymentMainCode = '{0}'"
                                                , pamentCode
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult summaryCashoutAuto()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SummaryCashOutAuto");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashOut_Sale_DisplayPaymentType.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool saveOpenDrawerCashout(string openTime)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO TRANSFERLOG
                    ([TDATE], [SOURCE], [DESTINATION], [MESSAGE]) 
                    VALUES
                    (getdate(), 'OpenDrawer_Ctrl0', 'User{0}_{1}', left('{2}',300))"
                                                , ProgramConfig.userId
                                                , ProgramConfig.cashOutRefNo
                                                , openTime));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool updateDlyptrans(string stt, string refNo, string rec)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"update DLYPTRANS  with(ROWLOCK) set STT='{0}' where REF='{1}' and  stcode='{2}' and REC>={3} "
                        , stt
                        , refNo
                        , ProgramConfig.storeCode
                        , rec));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public int getMaxRecDlyptrans(string refNo, string sty, string vty)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"select ISNULL(max(REC), 0) from DLYPTRANS with(NOLOCK)  
                        where REF='{0}' and  stcode='{1}' and STY='{2}' and VTY='{3}'"
                        , refNo
                        , ProgramConfig.storeCode
                        , sty
                        , vty));

                DataTable data = command.ExecuteToDataTable();

                if (data != null && data.Rows != null && data.Rows.Count > 0)
                {
                    return Convert.ToInt32(data.Rows[0][0].ToString());
                }
                return 0;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return -1;
            }
        }

        public bool deleteBanknoteCashout(string cashireId)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"DELETE FROM [CASHCOUNT] WHERE CASHCTL = {0} AND REF = '{1}'"
                        , cashireId, ProgramConfig.cashOutRefNo));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool insertBanknoteCashout(string cashireId, double bank_value, int bank_count, double total_amount, string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"INSERT INTO [CASHCOUNT]([CASHCTL], [CASHVAL], [CASHCNT], [CASHAMT], [REF]) 
                        VALUES({0}, {1}, {2}, {3}, N'{4}')"
                        , cashireId
                        , bank_value
                        , bank_count
                        , total_amount
                        , refNo));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool saveCloseDrawerCashoutBalance(string openTime, string closeTime)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"update dlyptrans with(ROWLOCK) 
                        set DISCID=999,pdisc={0},discamt={1}  
                        where ref='{2}' and STY='I' 
                        and rec=(select max(rec) from dlyptrans with(NOLOCK) where ref='{2}')"
                                                , openTime
                                                , closeTime
                                                , ProgramConfig.cashOutRefNo));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool saveCloseDrawerCashout(string openTime, string closeTime)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"update dlyptrans with(ROWLOCK) 
                        set DISCID=999,pdisc={0},discamt={1}  
                        where ref='{2}' and STY='C' 
                        and rec=(select max(rec) from dlyptrans with(NOLOCK) where ref='{2}')"
                                                , openTime
                                                , closeTime
                                                , ProgramConfig.cashOutRefNo));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public int getCashierID()
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"select max(CASHID) as CTL from CASHIERCTL 
                        where STCODE='{0}' and LOCKNO='{1}' and USERID= '{2}' and LOGINDATE>='{3}'"
                        , ProgramConfig.storeCode
                        , ProgramConfig.tillNo
                        , ProgramConfig.userId
                        , ProgramConfig.operateDate));

                DataTable data = command.ExecuteToDataTable();

                if (data != null && data.Rows != null && data.Rows.Count > 0)
                {
                    return Convert.ToInt32(data.Rows[0]["CTL"].ToString());
                }
                return 0;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return 0;
            }
        }

        public bool insertCashireLog(int cashireId, double amt)
        {
            try
            {
                command.SetCommandText(String.Format(@"
                    Delete from CASHIERLOG where CASHCTL = {0};
                    
                    INSERT INTO [CASHIERLOG]
                    ([STCODE], [OPR_DATE], [USER_ID], [END_RCV], [PM_CODE], [POS_AMT], [CAS_AMT], [CNT_AMT], [STATUS], [CASHCTL]) 
                    VALUES(N'{1}', getdate(), N'{2}', N'{3}', N'CASH', 0, {4}, 0, ' ', {0})"
                    , cashireId
                    , ProgramConfig.storeCode
                    , ProgramConfig.userId
                    , ProgramConfig.cashOutRefNo
                    , amt));

                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult printCashOut()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCashOut");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashOutRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashOut_Sale_PrintCashoutDocument.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printCashOutFloat()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintCashOutFloat");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashOutRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashOut_NormalChange_PrintCashoutDocument.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveEndOfShift()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveEndOfShiftTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.endOfShiftRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.EndOfShift_SaveEndOfShiftTransaction.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printEndOfShift(int control_id)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintEndOfShift");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.endOfShiftRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.EndOfShift_PrintEndOfShiftDocument.formatValue);
                command.AddInputParameter("DocType", SqlDbType.Char, "C");
                command.AddInputParameter("CashierCtlID", SqlDbType.Int, control_id);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveEndOfTill()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveEndOfTillTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.endOfTillRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.EndOfTill_SaveEndOfTillTransaction.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printEndOfTill()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintEndOfTill");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.endOfTillRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.EndOfTill_PrintEndOfTillDocument.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveLogout(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveLogoutTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveChangePassword(string userId, string password, FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveChangePassword");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("Password", SqlDbType.NVarChar, password);
                command.AddInputParameter("AppName", SqlDbType.NVarChar, ProgramConfig.appName);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable checkPassword(string newpass)
        {
            try
            {
                command.SetCommandText(String.Format(@"select top 4 Passw,NPassw=[dbo].[EnCrypt]('{1}', '{0}')  from USR_HIST_PASSWORD where USERID='{1}' order by LAST_UPDATE desc "
                                                , newpass
                                                ,ProgramConfig.userId
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult getTillNo4DispReport(FunctionID functionId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetTillNo4DispReport");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printReceiptReport(string receiptNo ,string superUserId)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintReceiptReport");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, superUserId);
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Report_CheckCurrentDayReceipt_PrintReceiptCopy.formatValue);
                command.AddInputParameter("ReceiptNo", SqlDbType.VarChar, receiptNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable SumTRansHEader(string posRepNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"select tot_sales,tot_vatable,tot_vat,tot_discount, tot_discount_coupon,tot_quant,tot_rcv,cash_amt, tot_record,avg_Sales,Sales_Excl_VAT,order_desk, tot_ret,ret_amt,ret_qnt,Ret_Excl_VAT 
                                            from 	DLYREP_SumTRansHEader 
                                            where	STORE_CODE='{0}' and OPERATE_DATE='{1}' and REQUESTED_NO='{2}' "
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , posRepNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable SumTRansDetail(string posRepNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"select	seq,Till,SaleReceiptCnt,SaleAmt,Discount, ReturnAmt,NetSale,CashInDrawer,Status, cashier
                                            from	 DLYREP_SumTRansDetail 
                                            where	STORE_CODE='{0}' and OPERATE_DATE='{1}' and REQUESTED_NO='{2}' "
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , posRepNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult printSumTransReport(string refno)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintSumTransReport");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refno);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Report_CheckCurrentDaySale_PrintReportDocument.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult repSumTrans(string tillNo)
        {
            try
            {
                command.SetCommandStoredProcedure("posrep_SumTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Report_CheckCurrentDaySale.formatValue);
                command.AddInputParameter("TillRep", SqlDbType.VarChar, tillNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkMoneyBag(string bagNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckMoneyBag");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.cashOutRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CashOut_Sale_CheckInputMoneyBag.formatValue);
                command.AddInputParameter("MoneyBagNo", SqlDbType.Int, bagNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult checkMinCashUnitAmount(string paymentCode, string payAmt, string refNo, string currencyCode, string mode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckMinCashUnitAmount");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("PaymentMainCode", SqlDbType.VarChar, paymentCode);
                command.AddInputParameter("CashierPayAMT", SqlDbType.Money, payAmt);
                command.AddInputParameter("PaymentSubCode", SqlDbType.NVarChar, currencyCode);
                command.AddInputParameter("Mode", SqlDbType.NVarChar, mode);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult displayCashOut()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayCashOut");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CancelCashOut_DisplayCashOut.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }              
        }

        public StoreResult UpdateStatusCancelCashOut(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"Update DLYPTRANS set STT = 'V' where STY = 'C' and VTY = 'P' and REF = '{0}' and left(PCD, 4) = 'CASH' ", refNo));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }    
        }

        public StoreResult displayCashOutDetail(string refNo, string currencyCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayCashOut_Detail");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CancelCashOut_DisplayCashOut.formatValue);
                command.AddInputParameter("CurrencyCode", SqlDbType.NVarChar, currencyCode);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable SelectName(string userId)
        {
            try
            {
                command.SetCommandText(String.Format(@"Select UserNameLocal from pos_user where UserId = '{0}'"
                                                , userId
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult deleteTempCashIn()
        {
            try
            {
                command.SetCommandText(String.Format("delete TempCashInTrans where REF = '{0}'", ProgramConfig.cashInRefNo));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public bool saveTempDlyptransLocalPOS(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
           , string usr, string egp, string stt, string pdisc, string discid, string upc, string dty, string discamt, string reason, string stv)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [tempDLYPTRANS] with(ROWLOCK) ([STCODE], [REF], [REC], [STY], [VTY], [PCD], [QNT], [AMT],[FDS], [TTM], [USR], [EGP], [STT], [PDISC], [DISCID], [UPC], [DTY], [DISCAMT], [REASON_ID], [STV]) 
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', getdate(), N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}')"
                                                 , ProgramConfig.storeCode
                                                 , refNo
                                                 , rec
                                                 , sty
                                                 , vty
                                                 , pcd
                                                 , qnt
                                                 , amt
                                                 , fds
                                                 , usr
                                                 , egp
                                                 , stt
                                                 , pdisc
                                                 , discid
                                                 , upc
                                                 , dty
                                                 , discamt
                                                 , reason
                                                 , stv));
                command.ExecuteNonQuery();

                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult CheckRedeemPoint_Free_CPN(string saleAmt, string rdCode, string qty)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckRedeemPoint_Free_CPN");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("CardNo", SqlDbType.VarChar, ProgramConfig.memberCardNo);
                command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                command.AddInputParameter("RedeemLimit", SqlDbType.Int, null);
                command.AddInputParameter("P_RedeemCode", SqlDbType.VarChar, rdCode);
                command.AddInputParameter("P_Quant", SqlDbType.Money, qty);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Product(string cntRedeem, string rateUse, string pointUse, string ruleID, string rdCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"update PMS_REDEEM_POINT set CNTREDEEM = '{0}' ,rateuse = '{1}', pointuse = '{2}' where ref='{3}' 
                                                        and ruleid = '{4}' and redeemcode = '{5}' and  stcode ='{6}'", cntRedeem, rateUse, pointUse, ProgramConfig.saleRefNo, ruleID, rdCode, ProgramConfig.storeCode));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckRedeemPointPercentDiscount(string saleAmt, string rdCode, string isRedeem)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckRedeemPointPercentDiscount");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("CardNo", SqlDbType.VarChar, ProgramConfig.memberCardNo);
                command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                command.AddInputParameter("pointredeemcode", SqlDbType.VarChar, rdCode);
                command.AddInputParameter("isRedeem", SqlDbType.Char, isRedeem);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult Delete_Temp_Redeem_Percent_Discount()
        {
            try
            {
                command.SetCommandText(String.Format(@"delete temp_redeem_percent_discount where ref = '{0}'", ProgramConfig.saleRefNo));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Percent_Discount()
        {
            try
            {
                command.SetCommandText(String.Format(@"update PMS_REDEEM_POINT set CNTREDEEM = 0 ,rateuse = 0 where ref='{0}' 
                                                        and POINT_REDEEM_TYPE in ('PP','PD') and stcode ='{1}'", ProgramConfig.saleRefNo, ProgramConfig.storeCode));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckRedeemPoint(string saleAmt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckRedeemPoint");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("CardNo", SqlDbType.VarChar, ProgramConfig.memberCardNo);
                command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                command.AddInputParameter("RedeemLimit", SqlDbType.Int, null);
                //command.AddInputParameter("pointredeemcode", SqlDbType.VarChar, rdCode);
                //command.AddInputParameter("isRedeem", SqlDbType.Money, isRedeem);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult Clear_PMS_REDEEM_POINT_Cash(string ruleID)
        {
            try
            {
                command.SetCommandText(String.Format(@"update PMS_REDEEM_POINT set CNTREDEEM = 0 ,rateuse = 0, pointuse = 0 where ref='{0}' 
                                                        and POINT_REDEEM_TYPE = 'C' and stcode ='{1}'", ProgramConfig.saleRefNo, ProgramConfig.storeCode));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Cash(string cntRedeem, string rateUse, string pointUse, string ruleID, string rdCode, string s_redeem)
        {
            try
            {
                command.SetCommandText(String.Format(@"update PMS_REDEEM_POINT set CNTREDEEM = '{0}' ,rateuse = '{1}', pointuse = '{2}' where ref='{3}' 
                                                        and ruleid = '{4}' and redeemcode = '{5}' and  stcode ='{6}' and S_RedeemPoint = '{7}' and POINT_REDEEM_TYPE = 'C' ",
                                                        cntRedeem, rateUse, pointUse, ProgramConfig.saleRefNo, ruleID, rdCode, ProgramConfig.storeCode, s_redeem));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckCustIDCard(string idCard)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_checkCustIDCard");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("memberID", SqlDbType.VarChar, ProgramConfig.memberId);
                command.AddInputParameter("idcard", SqlDbType.VarChar, idCard);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult SaveRedeemIDCard()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveRedeemIDCard");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, ProgramConfig.superUserId);
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("Member_ID", SqlDbType.VarChar, ProgramConfig.memberId);
                command.AddInputParameter("ByEdc", SqlDbType.Char, "N");

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult RedeemPoint_sum()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_RedeemPoint_sum");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("memid", SqlDbType.VarChar, ProgramConfig.memberId);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult ClearPromotion()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_ClearPromotion");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckRedeemPoint_Coupon(string saleAmt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckRedeemPoint_Coupon");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.redeemRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("CardNo", SqlDbType.VarChar, ProgramConfig.memberCardNo);
                command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                command.AddInputParameter("RedeemLimit", SqlDbType.Int, null);
                command.AddInputParameter("RefPOS", SqlDbType.VarChar, ProgramConfig.abbNo);
                //command.AddInputParameter("isRedeem", SqlDbType.Money, isRedeem);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Coupon(string cntRedeem, string pointUse, string ruleID, string rdCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"update PMS_REDEEM_POINT set CNTREDEEM = '{0}' , pointuse = '{1}' where ref='{2}' 
                                                        and ruleid = '{3}' and redeemcode = '{4}' and  stcode ='{5}'  ",
                                                        cntRedeem, pointUse, ProgramConfig.redeemRefNo, ruleID, rdCode, ProgramConfig.storeCode));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult RedeemPoint_Coupon_Sum()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_RedeemPoint_Coupon_Sum");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.redeemRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("memid", SqlDbType.VarChar, ProgramConfig.memberId);
                //command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                //command.AddInputParameter("RedeemLimit", SqlDbType.Int, null);
                command.AddInputParameter("RefPOS", SqlDbType.VarChar, ProgramConfig.abbNo);
                //command.AddInputParameter("isRedeem", SqlDbType.Money, isRedeem);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult PrintRedeemPoint_Coupon()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintRedeemPoint_Coupon");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.redeemRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                //command.AddInputParameter("memid", SqlDbType.VarChar, ProgramConfig.memberId);
                //command.AddInputParameter("SaleAmt", SqlDbType.Money, saleAmt);
                //command.AddInputParameter("RedeemLimit", SqlDbType.Int, null);
                //command.AddInputParameter("RefPOS", SqlDbType.VarChar, ProgramConfig.abbNo);
                //command.AddInputParameter("isRedeem", SqlDbType.Money, isRedeem);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult GetChange(string saleAmt, string payAmt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetChange");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_ConfirmPaymentAmount.formatValue);
                command.AddInputParameter("SaleAMT", SqlDbType.Money, saleAmt);
                command.AddInputParameter("PayAMT", SqlDbType.Money, payAmt);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult PaymentDiscount(string pmCode, string cardNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PaymentDiscount");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_DiscountNonCash.formatValue);
                command.AddInputParameter("PaymentMainCode", SqlDbType.Char, pmCode);
                command.AddInputParameter("cardno", SqlDbType.Char, cardNo);
                command.AddInputParameter("MemberId", SqlDbType.VarChar, ProgramConfig.memberId);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult updatePCDTempDlyptrans(string refNo, string pcd, string rec, string pcdNew)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                           SET [PCD] = '{4}'
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] = '{2}' 
                                            AND [PCD] = '{3}'"
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , rec
                                                , pcd
                                                , pcdNew
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updatePCDTempDlyptransdd(string refNo, string pcd, string rec, string pcdNew)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] with(ROWLOCK)
                                           SET [PCD] = '{4}'
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [REC] = '{2}' 
                                            AND [PCD] = '{3}'"
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , rec
                                                , pcd
                                                , pcdNew
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult insertEXPORTPERMIT(string exportNo, string rec, string seq)
        {
            try
            {
                command.SetCommandText(String.Format(
                    @"INSERT INTO [EXPORTPERMIT]([EXPORTNO], [REF], [REC], [SDATE], [SEQ]) 
                        VALUES('{0}', '{1}', '{2}', dbo.OpeDate(getdate()), '{3}')"
                        , exportNo
                        , ProgramConfig.abbNo
                        , rec
                        , seq));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult PrintExportPermit()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PrintExportPermit");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_Print_ExportPermit.formatValue);
                command.AddInputParameter("AbbNo", SqlDbType.VarChar, ProgramConfig.abbNo);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectDataEXPORTPERMIT()
        {
            try
            {
                command.SetCommandText(String.Format(@"Select isnull(max(SEQ), 0) + 1 as SEQ, dbo.logindate(getdate()) as ExpPortDate from EXPORTPERMIT 
                               where left(ref,3) = '{0}' and sdate=dbo.OpeDate(getdate())", ProgramConfig.tillNo));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult DisplayDiscountReceipt()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_DisplayDiscountReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectSCANTC()
        {
            try
            {
                command.SetCommandText(String.Format(@"select REF, CPNCODE, CPNVALUE, CPNQNT, RUNNING_NO, COUPON_USED_TYPE from SCANTC WHERE REF = '{0}' and CPNQNT > 0 "
                                                    , ProgramConfig.saleRefNo
                                                    ));


                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectCheckORG_TransFromQRPAYTRANS_Manual(string org_tranid, string pcd)
        {
            try
            {
                command.SetCommandText(String.Format(@"select ORG_TRANID From QRPAYTRANS WHERE REF = '{0}' and OPERATE_DATE = '{1}' and ORG_TRANID = N'{2}' and BANKCODE = '{3}' and ONLINE_FLAG = 'M' and CHANNEL = 'MN'"
                                                    , ProgramConfig.saleRefNo
                                                    , ProgramConfig.operateDate
                                                    , org_tranid
                                                    , pcd
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectTempChange(string pm_Code)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REF , PM_CODE, FXCU_CODE, CHG, EXCG_RATE, EXCG_AMT FROM tempchange with(NOLOCK) WHERE REF = '{0}' and PM_CODE = '{1}'"
                                                    , ProgramConfig.saleRefNo
                                                    , pm_Code
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectTempChangeByRef()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT REF , PM_CODE, FXCU_CODE, CHG, EXCG_RATE, EXCG_AMT FROM tempchange with(NOLOCK) WHERE REF = '{0}'"
                                                    , ProgramConfig.saleRefNo
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult DeleteTempRedeemFreePointCash()
        {
            try
            {
                command.SetCommandText(String.Format(@"DELETE Temp_REDEEM_FREE_POINTCASH WHERE REF='{0}'"
                                                , ProgramConfig.saleRefNo));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL()
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE PMS_REDEEM_POINT SET CNTREDEEM=0 
                                WHERE POINT_REDEEM_TYPE in ('F','PC') and REF ='{0}' and STCODE='{1}'"
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.storeCode));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public double GetFXCU_RATE(string pmCode, string pmSubCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"select dbo.FXCU_RATE('{0}', '{1}')"
                                                , ProgramConfig.storeCode
                                                , pmSubCode
                                                , pmCode
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    if (dt.Rows[0][0].ToString().Trim() != "")
                    {
                        return double.Parse(dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                   
                }
                return 0;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public double GetMinCashUnit(string pmSubCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"select isnull(min(unit_amt), 0) as min_unit from CashUnit where CURRENCY_CODE = '{0}' And STATUS = 'A' "
                                                , pmSubCode
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return double.Parse(dt.Rows[0][0].ToString());
                }
                return 0;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckRunningNumber(string sRef, string vRef, string rRef, string cRef, string lRef,
                                              string eRef, string oRef, string pRef, string dRef, string hRef, string tRef)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckRunningNumber");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");

                command.AddInputParameter("SaleInternalRef", SqlDbType.NVarChar, sRef);
                command.AddInputParameter("VoidInternalRef", SqlDbType.NVarChar, vRef);
                command.AddInputParameter("RetnInternalRef", SqlDbType.NVarChar, rRef);
                command.AddInputParameter("CashinoutInternalRef", SqlDbType.NVarChar, cRef);
                command.AddInputParameter("LoginInternalRef", SqlDbType.NVarChar, lRef);
                command.AddInputParameter("ExportPermitRef", SqlDbType.NVarChar, eRef);
                command.AddInputParameter("OpendayInternalRef", SqlDbType.NVarChar, oRef);
                command.AddInputParameter("PosRepRef", SqlDbType.NVarChar, pRef);
                command.AddInputParameter("DelEditItemInternalRef", SqlDbType.NVarChar, dRef);
                command.AddInputParameter("HoldRef", SqlDbType.NVarChar, ProgramConfig.holdOrderRefNoIni);
                command.AddInputParameter("TempFFTIRef", SqlDbType.NVarChar, ProgramConfig.tempFFTINo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectPaymentMenuIcon()
        {
            try
            {
                //Fix
                command.SetCommandText(String.Format(@"select mi.*, ISNULL(mil.LanguageID, 0) as 'LanguageID', ISNULL(mil.Label, mi.Label) as 'Label_Language' from POSADMIN_MENU_ICON mi LEFT OUTER JOIN 
  POSADMIN_MENU_ICON_LANGUAGE mil 
  on mi.StoreCode = mil.StoreCode and mi.MenuID = mil.MenuID 
  where mi.StoreCode = '{0}' and mi.Status = 'A'"
                                                    , ProgramConfig.storeCode
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectGenerateTextBoxLabel()
        {
            try
            {
                //Fix
                command.SetCommandText(String.Format(@"select st.*, stl.Label as Label_Language, CASE WHEN ISNULL(stl.LabelTextInput, st.LabelTextInput) = 'N/A' THEN st.LabelTextInput ELSE ISNULL(stl.LabelTextInput, st.LabelTextInput) END as 'LabelTextInput_Language' from POSADMIN_PAYMENT_STEP_DET st LEFT OUTER JOIN 
  POSADMIN_PAYMENT_STEP_DET_LANGUAGE stl on 
  st.StoreCode = stl.StoreCode and 
  st.PaymentStepGroupID = stl.PaymentStepGroupID and
  st.Seq = stl.Seq and
  stl.LanguageID = {1}
  where st.StoreCode = '{0}' and st.status = 'A'  "
                                                    , ProgramConfig.storeCode
                                                    , ProgramConfig.language.ID
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckEmployee(string empID)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckEmployee");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_CheckEmployee.formatValue);
                command.AddInputParameter("emp_id", SqlDbType.VarChar, empID);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckIME_Serial(string serial)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckIME_Serial");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("imeSerial", SqlDbType.VarChar, serial);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckPCMan(string pcManID)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckPCman");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_CheckPCMan.formatValue);
                command.AddInputParameter("pcman", SqlDbType.VarChar, pcManID);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool insertSCANPRODUCT_SERIAL(string rec, string pcd, string serial1, string serial2)
        {
            try
            {
                command.SetCommandText(String.Format(@"
                                                delete [SCANPRODUCT_SERIAL]  where STCODE='{0}' and LOC_NO='{2}' and REF='{3}' and ROW= {4};

                                                INSERT INTO [SCANPRODUCT_SERIAL] with(ROWLOCK) (STCODE, ODATE, LOC_NO, REF, ROW, PRODUCT_CODE, SERIAL_REF1, SERIAL_REF2, CASHIER_ID, TDATE, STATUS)
                                                VALUES(N'{0}', N'{1}', N'{2}', N'{3}', {4}, N'{5}', N'{6}', N'{7}', '{8}', getdate(), N'A');"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.saleRefNo
                                                , rec
                                                , pcd
                                                , serial1
                                                , serial2
                                                , ProgramConfig.userId)
                                      );
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult searchCustomer(FunctionID functionId, int searchType, string data)
        {
            try
            {
                command.SetCommandStoredProcedure("POS_SearchCustomer");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("DataType", SqlDbType.Int, searchType);
                command.AddInputParameter("DataSearch", SqlDbType.NVarChar, data);
                if (functionId == FunctionID.Sale_Member_Search_Data || functionId == FunctionID.Deposit_SearchMember3)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputCustomer_ByMember)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult searchCustomerFullTax(FunctionID functionId, int searchType, string data)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SearchCustomerFullTax");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("DataType", SqlDbType.Int, searchType);
                command.AddInputParameter("DataSearch", SqlDbType.NVarChar, data);
                if (functionId == FunctionID.Sale_Member_Search_Data || functionId == FunctionID.Deposit_SearchMember1)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputCustomer_ByMember)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckCustomer(FunctionID functionId, string memberID)
        {
            try
            {
                command.SetCommandStoredProcedure("POS_CheckCustomer");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("CardNumber", SqlDbType.NVarChar, memberID);
                if (functionId == FunctionID.Sale_Member_Display)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                }
                else if (functionId == FunctionID.Return_InputCustomer_Display)
                {
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.returnRefNo);
                }

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool savePCMan(string rec, string pcd, string serial1, string serial2)
        {
            try
            {
                command.SetCommandText(String.Format(@"
                                                delete [SCANPRODUCT_SERIAL]  where STCODE='{0}' and LOC_NO='{2}' and REF='{3}' and ROW= {4};

                                                INSERT INTO [SCANPRODUCT_SERIAL] with(ROWLOCK) (STCODE, ODATE, LOC_NO, REF, ROW, PRODUCT_CODE, SERIAL_REF1, SERIAL_REF2, CASHIER_ID, TDATE, STATUS)
                                                VALUES(N'{0}', N'{1}', N'{2}', N'{3}', {4}, N'{5}', N'{6}', N'{7}', '{8}', getdate(), N'A');"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.saleRefNo
                                                , rec
                                                , pcd
                                                , serial1
                                                , serial2
                                                , ProgramConfig.userId)
                                      );
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult SavePCMan(string pcman)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SavePCman");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A"); //TO DO Change function
                command.AddInputParameter("pcman", SqlDbType.NVarChar, pcman);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectSALEORDER_MENU()
        {
            try
            {
                command.SetCommandText(String.Format(@"select MENU_ID,RELATE_FLAG from SALEORDER_MENU where MENU_ID=121"));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult GetSaleOrderType(string type, string menuID, string relateFlag, string level, string valueSaleOrder, string valueDelivery)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetSaleOrderType");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("Type", SqlDbType.NVarChar, relateFlag == "Y" ? "N/A" : type);
                command.AddInputParameter("Menu_ID", SqlDbType.VarChar, menuID);
                command.AddInputParameter("Relate_Flag", SqlDbType.Char, relateFlag);
                command.AddInputParameter("Level", SqlDbType.Int, level);
                command.AddInputParameter("Saleorder_Code", SqlDbType.NVarChar, relateFlag == "Y" ? valueSaleOrder : "N/A");
                command.AddInputParameter("Delivery_Code", SqlDbType.NVarChar, relateFlag == "Y" ? valueDelivery : "N/A");
                
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool insertSALEORDERTYPE_TRANS(int orderType, int deliveryType, int mediaType)
        {
            try
            {
                command.SetCommandText(String.Format(@"                                              
                        Delete [SALEORDERTYPE_TRANS] where ref = '{2}';
                        Insert [SALEORDERTYPE_TRANS]([STORE_CODE],[ODATE],[REF],[ORDER_TYPE_CODE],[DELIVERY_TYPE_CODE],[MEDIA_TYPE_CODE],[TRANSACTION_DATE]) 
                                                VALUES(N'{0}', N'{1}', N'{2}', '{3}', '{4}', '{5}', getdate());"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.saleRefNo
                                                , orderType
                                                , deliveryType
                                                , mediaType
                                                )
                                      );
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public DataTable selectSALEORDERTYPE_TRANS()
        {
            try
            {
                command.SetCommandText(String.Format(@"select [REF] from [SALEORDERTYPE_TRANS] where REF = '{0}'"
                                                    , ProgramConfig.saleRefNo
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult deleteSALEORDERTYPE_TRANS()
        {
            try
            {
                command.SetCommandText(String.Format(@"Delete [SALEORDERTYPE_TRANS] where REF = '{0}'"
                                                    , ProgramConfig.saleRefNo
                                                    ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool insertTmpTrans()
        {
            try
            {
                command.SetCommandText(String.Format(@"Insert TMPTRANS (STORE_CODE, OPERATE_DATE, LOCK, REF, Nitem, CustID, RDate, EMPID, STATUS, SALESREF, USERID) 
                                                VALUES(N'{0}', N'{1}', N'{2}', '{3}', '{4}', '{5}', getdate(), '{6}', 'A', '{7}', '{8}');"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.holdOrderRefNoIni
                                                , ProgramConfig.qntValue
                                                , ProgramConfig.memberFormat == MemberFormat.MegaMaket ? ProgramConfig.memberCardNo : ProgramConfig.memberId
                                                , ProgramConfig.employeeID
                                                , ProgramConfig.saleRefNo
                                                , ProgramConfig.userId
                                                )
                                      );
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }


        public bool insertTmpTransDetail(string rec, string pcd, string qnt, string amt, string entry, string stt)
        {
            try
            {
                command.SetCommandText(String.Format(@"                                              
                        Insert TMPTRANS_DETAIL (STORE_CODE, OPERATE_DATE, TILLNO, REF, REC, PCD, QNT, AMT, ENTRY_DATA, STT) 
                                                VALUES(N'{0}', N'{1}', N'{2}', N'{3}', '{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}');"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.holdOrderRefNoIni
                                                , rec
                                                , pcd
                                                , qnt
                                                , amt
                                                , entry
                                                , stt
                                                )
                                      );
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult CheckOnhold()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckOnhold");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_CancelWhileSale_CheckHolderOrder.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult GetOnhold(string refInp)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetOnhold");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.NVarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_CancelWhileSale_LoadHolderOrder.formatValue);
                command.AddInputParameter("HoldOrderNo", SqlDbType.NVarChar, refInp);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult SaveChangeTrans(double posChg, double cashierChg)
        {
            try
            {
                command.SetCommandStoredProcedure("POS_SaveChangeTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_Change_EditChange.formatValue);
                command.AddInputParameter("PMCODE", SqlDbType.Char, "N/A");
                command.AddInputParameter("PosChg", SqlDbType.Money, posChg);
                command.AddInputParameter("CashierChg", SqlDbType.Money, cashierChg);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult PODSaveChangeTrans(double posChg, double cashierChg)
        {
            try
            {
                command.SetCommandStoredProcedure("POS_PODSaveChangeTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.podRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_SaveChange.formatValue);
                command.AddInputParameter("PMCODE", SqlDbType.Char, "N/A");
                command.AddInputParameter("PosChg", SqlDbType.Money, posChg);
                command.AddInputParameter("CashierChg", SqlDbType.Money, cashierChg);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable functionGetPaymentCode(string paymentNumber)
        {
            try
            {
                command.SetCommandText(String.Format(@"select dbo.getpaymentcode('{0}','')"
                                                    , paymentNumber
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult updateVat2EDCTrans(string pmCode, string cardNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"Update vat2edctrans set PM_CODE = '{0}',CARD_NO = '{1}' ,status = 'A' where ref = '{2}' and seq = (select isnull(max(seq),0) from vat2edctrans where ref = '{2}')", 
                                                    pmCode,
                                                    cardNo,
                                                    ProgramConfig.saleRefNo
                                                    ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult deleteEDCTrans(string cardNo)
        {
            try
            {
                command.SetCommandText(String.Format(@" DELETE FROM EDCTRANS WHERE STCODE='{0}' and REF='{1}' and CARD_NO = '{2}' ", 
                                                    ProgramConfig.storeCode,
                                                    ProgramConfig.saleRefNo,
                                                    cardNo
                                                    ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }


        public StoreResult insertEDCTrans(string tid, string cardNo, string approveCode, string trace, string edcDate, double edcAmt, string cardIssue, string vatAmt)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [EDCTRANS]([STCODE],[REF],[CARD_NO], [TERMINAL_ID],[APPROVE_CODE]
                                                        ,[TRACE_NO],[EDCDATE], [TDATE],[EDC_AMOUNT],[CARD_ISSUER_NAME],[EDC_VATAMOUNT])
                                                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}', getdate(),'{7}','{8}','{9}')",
                                                    ProgramConfig.storeCode,
                                                    ProgramConfig.saleRefNo,
                                                    cardNo,
                                                    tid,
                                                    approveCode,
                                                    trace,
                                                    edcDate,
                                                    edcAmt,
                                                    cardIssue,
                                                    vatAmt
                                                    ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult SpecialPay(string paymentCode, string cardNo, string amt, string memberCard)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SpecialPay");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_DiscountNonCash.formatValue);
                command.AddInputParameter("AbbNo", SqlDbType.VarChar, "N/A");
                command.AddInputParameter("Pay", SqlDbType.NChar, paymentCode);
                command.AddInputParameter("Credit_CardNo", SqlDbType.NChar, cardNo);
                command.AddInputParameter("Direct", SqlDbType.NChar, "");
                command.AddInputParameter("MemberID", SqlDbType.NVarChar, memberCard);
                command.AddInputParameter("AmtPay", SqlDbType.Money, amt);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectPAYMENT_PARAMETER(string buType, string pmCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * from PAYMENT_PARAMETER where BU_TYPE = '{0}' and PM_CODE = '{1}'", 
                                                    buType,
                                                    pmCode
                                                    ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectParameterForQR(string refNo, string type, string channel, string qrRunningType)
        {
            try
            {
                command.SetCommandText(String.Format(@"select dbo.GetQRPaymentUID('{0}', '{1}', '{2}', '{3}', '{4}', '{5}') as TranID
                                                      , Convert(varchar,getdate(),121) as TranTime
                                                      , isnull((select max(seq)+1 from QRPAYTRANS where ref = '{1}'),'1') as Seq"

                                                    , ProgramConfig.storeCode
                                                    , refNo
                                                    , ProgramConfig.tillNo
                                                    , type
                                                    , channel
                                                    , qrRunningType
                                                    ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult QRRequest(string tranID, string tranTime, string merchantID, string amt)
        {
            try
            {
                    command.SetCommandStoredProcedure("pos_QR_Request");
                    command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                    command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                    command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                    command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                    command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                    command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                    command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                    command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                    command.AddInputParameter("TranID", SqlDbType.NVarChar, tranID);
                    command.AddInputParameter("TranTime", SqlDbType.Date, tranTime);
                    command.AddInputParameter("merchantId", SqlDbType.NVarChar, merchantID);
                    command.AddInputParameter("terminalId", SqlDbType.NChar, ProgramConfig.tillNo);
                    command.AddInputParameter("qrType", SqlDbType.NVarChar, "3");
                    command.AddInputParameter("PayAmount", SqlDbType.Money, amt);
                    command.AddInputParameter("PayCurrency", SqlDbType.NChar, ProgramConfig.currencyDefault);
                    command.AddInputParameter("reference1", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                    command.AddInputParameter("reference2", SqlDbType.NVarChar, "");
                    command.AddInputParameter("reference3", SqlDbType.NVarChar, "");
                    command.AddInputParameter("reference4", SqlDbType.NVarChar, "");
                    command.AddInputParameter("metadata", SqlDbType.NVarChar, "");

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveQRPayTransOnline(string refNo , string action_type, string seq, string org_tranID, string accountName, string qrCode, string amount,
            string resCode, string errorCode, string channal, string bankCode, string tokenID, string ota, string bsd, string tepa_code, string sendBank, 
            string receiveBank, string ref2, string transRef, string trn_status = "" ,string stt = "", string onlineFlag = "", string tranID = "")
        { 
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [QRPAYTRANS] with(ROWLOCK) (STORE_CODE, OPERATE_DATE, LOCKNO, REF, ACTION_TYPE, SEQ, ORG_TRANID, TRAN_ID, ACCOUNT_NAME, QR_CODE, 
                                                        PAY_AMOUNT, TXN_STATUS, TTM, STT, RESPONSE_CODE, ERRORCODE, RESDESCRIPTION, ONLINE_FLAG, AUTHORIZE, AUTHORIZE_BY, CHANNEL, BANKCODE, TOKEN_ID, OTA, BSD, 
                                                        TEPA_CODE, SENDING_BANK, RECEVING_BANK, REF2, TRANSREF) 
                                                VALUES(N'{0}', N'{1}', '{2}', N'{3}', N'{4}', '{5}', N'{6}', N'{25}', N'{7}', N'{8}', N'{9}', N'{22}', getdate(), N'{23}', N'{10}', N'{11}', '', N'{24}', '', '', 
                                                               N'{12}', N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}', N'{19}', N'{20}', N'{21}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , refNo
                                                , action_type 
                                                , seq
                                                , org_tranID
                                                , accountName 
                                                , qrCode
                                                , amount //{9}
                                                , resCode
                                                , errorCode
                                                , channal
                                                , bankCode
                                                , tokenID
                                                , ota
                                                , bsd
                                                , tepa_code
                                                , sendBank
                                                , receiveBank
                                                , ref2
                                                , transRef
                                                , trn_status
                                                , stt
                                                , onlineFlag
                                                , tranID
                                                )
                                      );
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveQRPayTransVoid(string refNo, string tranID, string amount, string resCode)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [QRPAYTRANS_VOID] with(ROWLOCK) (STORE_CODE, OPERATE_DATE, LOCKNO, REF, TRAN_ID, PAY_AMOUNT, USER_AUTHORIZE, USER_SALE, RESPONSE_CODE, ACTION_TYPE, TRANSACTION_DATE) 
                                                VALUES(N'{0}', N'{1}', '{2}', N'{3}', N'{4}', '{5}', N'{6}', N'{7}', N'{8}', N'{9}', getdate())"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , refNo
                                                , tranID
                                                , amount //{9}
                                                , ProgramConfig.superUserId
                                                , ProgramConfig.userId
                                                , resCode
                                                , "C"
                                                )
                                      );
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult QRInquirePayment(string tranID, string tranTime, string merchantID, string orgTran)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_QR_InquirePayment");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("TranID", SqlDbType.NVarChar, tranID);
                command.AddInputParameter("TranTime", SqlDbType.Date, tranTime);
                command.AddInputParameter("merchantId", SqlDbType.NVarChar, merchantID);
                command.AddInputParameter("terminalId", SqlDbType.NChar, ProgramConfig.tillNo);
                command.AddInputParameter("qrType", SqlDbType.NVarChar, "3");
                command.AddInputParameter("OrgTranID", SqlDbType.NVarChar, orgTran);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult selectAPICONF_RETRY(string apiName)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * from APICONF_RETRY where STORE_CODE = '{0}' and APINAME = '{1}'",
                                                    ProgramConfig.storeCode,
                                                    apiName
                                                    ));

                return new StoreResult(ResponseCode.Success, "Success", data: command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult updateQRPayTrans(string tranID, string resCode, string errCode, string resMsg, string onlineFlag, string auth, string authBy,
            string channel = "", string ref2 = "", string transRef = "",string stt = "", string whereCondition = "")
        {
            try
            {
                command.SetCommandText(String.Format(@" UPDATE QRPAYTRANS SET Tran_ID = '{1}', STT = 'A', Response_code = '{2}', TTM = getdate(), ErrorCode = '{3}', RESDESCRIPTION = N'{4}',
                                                        ONLINE_FLAG = '{5}', Authorize = '{6}', AUTHORIZE_BY = '{7}' , CHANNEL = '{8}', REF2 = '{9}', TRANSREF = '{10}'
                                                        WHERE REF = '{0}' and STT = '{11}' {12}",
                                                            ProgramConfig.saleRefNo,
                                                            tranID,
                                                            resCode,
                                                            errCode,
                                                            resMsg,
                                                            onlineFlag,
                                                            auth,
                                                            authBy,
                                                            channel,
                                                            ref2,
                                                            transRef,
                                                            stt,
                                                            whereCondition
                                                            ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updateQRPayTransBySet(string set, string whereCondition = "")
        {
            try
            {
                command.SetCommandText(String.Format(@" UPDATE QRPAYTRANS SET {1}
                                                        WHERE REF = '{0}' and STT = '' {2}",                                                       
                                                            ProgramConfig.saleRefNo,
                                                            set,
                                                            whereCondition
                                                            ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updateVoidQRPayTrans(string whereCondition = "")
        {
            try
            {

                command.SetCommandText(String.Format(@" UPDATE QRPAYTRANS SET STT = 'V' WHERE REF = '{0}' {1} ",
                                                            ProgramConfig.saleRefNo,
                                                            whereCondition 
                                                            ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        //public StoreResult updateVoidQRPayTransBscanC(string orgTran, string stt)
        //{
        //    try
        //    {

        //        command.SetCommandText(String.Format(@" UPDATE QRPAYTRANS SET STT = 'V', ORG_TRANID = '{1}' WHERE REF = '{0}' ",
        //                                                    ProgramConfig.saleRefNo,
        //                                                    orgTran
        //                                                    ));
        //        command.ExecuteNonQuery();
        //        return new StoreResult(ResponseCode.Success, "Success");
        //    
        //    catch (NetworkConnectionException ex)
        //    {
        //        AppLog.writeLog(ex);
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog(ex);
        //        return new StoreResult(ResponseCode.Error, ex.Message);
        //    }
        //}

        public StoreResult API_POS_BSC_PAYMENT(string tranID, string qr_code, string amt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_API_POS_BSC_PAYMENT");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("prn_TxnID", SqlDbType.NVarChar, tranID);
                command.AddInputParameter("qr_code", SqlDbType.Date, qr_code);
                command.AddInputParameter("amt", SqlDbType.Money, amt);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckCallback(string tranID)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckCallback");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("prn_TxnID", SqlDbType.VarChar, tranID);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult API_POS_BSC_INQUIRY_PAYMENT_STATUS(string tranID, string orgTran , string qrCode, string bankCode , string tokenID, string ota)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_API_POS_BSC_INQUIRY_PAYMENT_STATUS");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("prn_TxnID", SqlDbType.VarChar, tranID);
                command.AddInputParameter("org_PrtTxnID", SqlDbType.VarChar, orgTran);
                command.AddInputParameter("qr_code", SqlDbType.VarChar, qrCode);
                command.AddInputParameter("BankCode", SqlDbType.VarChar, bankCode);
                command.AddInputParameter("Token_ID", SqlDbType.NVarChar, tokenID);
                command.AddInputParameter("OTA", SqlDbType.NVarChar, ota);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult API_POS_BSC_VERIFY_SLIP(string tranID, string bankCode, string orgTran, string tranRef)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_API_POS_BSC_VERIFY_SLIP");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("prn_TxnID", SqlDbType.VarChar, tranID);
                command.AddInputParameter("sending_Bank", SqlDbType.VarChar, bankCode);
                command.AddInputParameter("TransRef", SqlDbType.VarChar, tranRef);
                command.AddInputParameter("org_PrtTxnID", SqlDbType.VarChar, orgTran);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult API_POS_BSC_VOID(string refNo, string tranID, string orgTran, string amt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_API_POS_BSC_VOID");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_AutoVoid_API_POS_BSC_VOID.formatValue);
                command.AddInputParameter("prn_TxnID", SqlDbType.VarChar, tranID);
                command.AddInputParameter("org_PrtTxnID", SqlDbType.VarChar, orgTran);
                command.AddInputParameter("amt", SqlDbType.Money, amt);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult API_POS_BSC_INQUIRY_VOID_STATUS(string tranID, string orgTran, string amt)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_API_POS_BSC_INQUIRY_VOID_STATUS");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("prn_TxnID", SqlDbType.VarChar, tranID);
                command.AddInputParameter("org_PrtTxnID", SqlDbType.VarChar, orgTran);
                command.AddInputParameter("amt", SqlDbType.Money, amt);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }


        public StoreResult updateTmpTrans()
        {
            try
            {

                command.SetCommandText(String.Format(@" UPDATE TMPTRANS SET STATUS = 'V' WHERE REF = '{0}' ",
                                                            ProgramConfig.loadHoldOrderReceipt
                                                            ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updatePOS_Running(string receiptID)
        {
            try
            {
                command.SetCommandText(String.Format(@" DECLARE @length int  
                                                        select @length = RUNNING_LENGTH from POS_RUNNING where TILL_NO = '{0}' and receipt_id = {1}
                                                        update POS_RUNNING set next_running_no = 
                                                                  case when next_running_no >= Convert(int, REPLACE(STR('9', @length), SPACE(1), '9'))  then 1 else 
                                                                  next_running_no + 1 end from pos_running  
                                                          where TILL_NO = '{0}' and receipt_id = {1} ",
                                                            ProgramConfig.tillNo,
                                                            receiptID
                                                            ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult selectTMPTRANS_DETAIL(string refInp)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * from TMPTRANS_DETAIL where STORE_CODE = '{0}' and TILLNO = '{1}' and REF = '{2}' ",
                                                    ProgramConfig.storeCode,
                                                    ProgramConfig.tillNo,
                                                    refInp
                                                    ));

                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult selectBarcodeExtend(string rec)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * from BarcodeExtend where STCODE = '{0}' and REF = '{1}' and REC = '{2}'  ",
                                                    ProgramConfig.storeCode,
                                                    ProgramConfig.saleRefNo,
                                                    rec
                                                    ));

                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult selectQRPAYTRANS(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * From QRPAYTRANS WHERE REF = '{0}' and LOCKNO = '{1}' and ACTION_TYPE = 'SA' and CHANNEL = 'BC' and STT = 'A' "
                                                    , refNo
                                                    , ProgramConfig.tillNo
                                                    ));

                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult selectPOSADMIN_PAYMENT_STEP_DET(string whereCondition)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * from POSADMIN_PAYMENT_STEP_DET where StoreCode = '{0}' {1} ",
                                                    ProgramConfig.storeCode,
                                                    whereCondition
                                                    ));

                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckValueDate(string validateType, string dataEntry)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckValueDate");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("ValidDateType", SqlDbType.Int, validateType);
                command.AddInputParameter("DataEntry", SqlDbType.NVarChar, dataEntry);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckValuePayment(string validateType, string dataEntry, string pmCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckValuePayment");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("ValidDateType", SqlDbType.Int, validateType);
                command.AddInputParameter("PaymentMainCode", SqlDbType.VarChar, pmCode);
                command.AddInputParameter("DataEntry", SqlDbType.NVarChar, dataEntry);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult selectCustomer_FFTI(string idCard)
        {
            try
            {
                command.SetCommandText(String.Format(@"select * from Customer_FFTI where IDNUMBER = '{0}' ",
                                                    idCard
                                                    ));

                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckProducRule(string projectid_productrule, string productcode, string barcodetype)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckProductRule");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_InputSaleItem_EditProduct_EditPrice_InputReason.formatValue);
                command.AddInputParameter("ProjectID_ProductRule", SqlDbType.VarChar, projectid_productrule);
                command.AddInputParameter("ProductCode", SqlDbType.VarChar, productcode);
                command.AddInputParameter("BarcodeType", SqlDbType.VarChar, barcodetype);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveTemppayDetail(string pmCode,string paymentStepGroupID, string seq, string stepID, string dataType, string data_value)
        {
            try
            {
                string refNo = "";
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale || ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
                {
                    refNo = ProgramConfig.saleRefNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    refNo = ProgramConfig.creditSaleNo;
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    refNo = ProgramConfig.podRefNo;
                }

                command.SetCommandText(String.Format(@"INSERT INTO [Temppay_detail] with(ROWLOCK) (STORECODE, OPERATEDATE, TILLNO, REF, PAY, PAYMENTSTEPGROUPID, 
                                                            SEQ, STEPID, DATATYPE, DATA_VALUE, TRANSACTIONDATE) 
                                                VALUES(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', getdate())"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , refNo
                                                , pmCode
                                                , paymentStepGroupID
                                                , seq
                                                , stepID
                                                , dataType
                                                , data_value
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult SumSalesVat2EDC(string walfareID, string payamount, string pmCode, string cardNo, string menu)
        {
            try
            {
                //TO DO WalfreID get from CheckCustomer and getprofilemember
                command.SetCommandStoredProcedure("pos_SumSalesVat2EDC");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_InputSaleItem_EditProduct_EditPrice_InputReason.formatValue);
                command.AddInputParameter("Member_ID", SqlDbType.VarChar, ProgramConfig.memberId);
                command.AddInputParameter("Welfare_ID", SqlDbType.VarChar, walfareID);
                command.AddInputParameter("PayAmount", SqlDbType.VarChar, payamount);
                command.AddInputParameter("Pm_Code", SqlDbType.VarChar, pmCode);
                command.AddInputParameter("Card_no", SqlDbType.VarChar, cardNo);
                command.AddInputParameter("Menu", SqlDbType.VarChar, menu);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult GetDepositCustomerType()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_GetDepositCustomerType");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Deposit_GetDepositCustomerType.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult insertTempCustomerFullTax(string pmCode, string paymentStepGroupID, string seq, string stepID, string dataType, string data_value)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO [TempCustomerFullTax] with(ROWLOCK) (STORECODE, OPERATEDATE, TILLNO, REF, PAY, PAYMENTSTEPGROUPID, 
                                                            SEQ, STEPID, DATATYPE, DATA_VALUE, TRANSACTIONDATE) 
                                                VALUES(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', getdate())"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.saleRefNo
                                                , pmCode
                                                , paymentStepGroupID
                                                , seq
                                                , stepID
                                                , dataType
                                                , data_value
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updateItemTempDlyptrans(string code, string quant, string price, string rec)
        {
            try
            {
                command.SetCommandText(String.Format(@"UPDATE [TEMPDLYPTRANS] SET AMT = '{5}', UPC = '{5}'
                                            WHERE [REF] = '{0}'
                                            AND [STCODE] = '{1}'
                                            AND [PCD] = '{2}'
                                            AND [QNT] = '{3}'
                                            AND [STT] != 'V'
                                            AND [REC] = '{4}'"
                                , ProgramConfig.saleRefNo
                                , ProgramConfig.storeCode
                                , code
                                , quant
                                , rec
                                , price
                                ));

//                command.SetCommandText(String.Format(@"SELECT * FROM [TEMPDLYPTRANS] with(NOLOCK)
//                                            WHERE [REF] = '{0}'
//                                            AND [STCODE] = '{1}'
//                                            AND [PCD] = '{2}'
//                                            AND [QNT] = '{3}'
//                                            AND [AMT] = '{4}'
//                                            AND [STT] != 'V'
//                                            AND [REC] >= 0 "
//                                                , refNo
//                                                , ProgramConfig.storeCode
//                                                , code
//                                                , quant
//                                                , price
//                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult rcv2fullform_ffti(string abbNo)
        {
            try
            {
                command.SetCommandStoredProcedure("FFTI_RCV2FULLFORM");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("RefernceNo", SqlDbType.NVarChar, abbNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Deposit_RCV2FULLFORM_FFTI.formatValue);
                string saleType = "1";
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
                {
                    saleType = "2";
                }
                command.AddInputParameter("Sale_type", SqlDbType.VarChar, saleType);
                command.AddInputParameter("MultiStore", SqlDbType.VarChar, ProgramConfig.multiStore);
                command.AddInputParameter("AppName", SqlDbType.VarChar, "BJCBCPOS");

                string memID = ProgramConfig.memberFormat == MemberFormat.MegaMaket
                    ? ProgramConfig.memberCardNo : ProgramConfig.memberId;
                command.AddInputParameter("Custid", SqlDbType.VarChar, memID);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveDepositTrans(string fftiNo, string abbNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveDepositTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, abbNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Sale_ProcessAfterSaveSaleTransaction.formatValue);
                command.AddInputParameter("FFTINO", SqlDbType.VarChar, fftiNo);

                string memID = ProgramConfig.memberFormat == MemberFormat.MegaMaket
                    ? ProgramConfig.memberCardNo : ProgramConfig.memberId;
                command.AddInputParameter("MemberID", SqlDbType.VarChar, memID);


                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckDEPO(string validateType, string dataEntry, string pmCode)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckDEPO");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.saleRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");

                //if (validateType != "" && validateType != "N/A")
                //{
                //    command.AddInputParameter("ValidDateType", SqlDbType.Int, validateType);
                //}
                
                //if (pmCode != "")
                //{
                //    command.AddInputParameter("PaymentMainCode", SqlDbType.VarChar, pmCode);
                //}            

                command.AddInputParameter("DocumentNo", SqlDbType.NVarChar, dataEntry);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveTempdeposit_trans_history(string refNo, string amt, string depoRef)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO Tempdeposit_trans_history with(ROWLOCK) (STORE_CODE, OPERATE_DATE, TILL_NO, REF, DEPOSIT_FFTI_NO,
                                                                TRANS_TYPE, AMOUNT, STATUS, TRANSACTION_DATE, DEPOSIT_REF, FFTI_NO, DEPOSIT_BALANCE_AMOUNT)
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', getdate(), N'{8}', '', '')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.saleRefNo
                                                , refNo
                                                , "VD"
                                                , amt
                                                , "A"
                                                , depoRef));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult PODGetOrder(string abbNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PODGetOrder");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_GetOrder.formatValue);
                command.AddInputParameter("ABBNo", SqlDbType.VarChar, abbNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveTEMP_PODTRANS_PAY(string pay, string payNum, string amt, string chg, string pdisc, string refID, string approveCode, string traceNo, 
                                          string terminalID, string merchantID, string edc_date, string invoiceNo, string QRCode)
        {
            try
            {
                string date = "null";
                if (edc_date != "")
                {
                    date = (DateTime.ParseExact(edc_date, "yyMMddHHmmss", new System.Globalization.CultureInfo("en-US"))).ToString("yyyyMMdd HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                    date = "'" + date + "'";
                }

                command.SetCommandText(String.Format(@"INSERT INTO TEMP_PODTRANS_PAY with(ROWLOCK) (STCODE, ODATE, REF_POD, TYPE, REC, PAY, PAY_NUMBER, AMT, 
                                                            CHG, PDISC, REFERENCE_ID, APPROVE_CODE, TRACE_NO, TERMINAL_ID, MERCHANT_ID, EDC_DATE, INVOICENO, QRCODE)
                                                    values (N'{0}', N'{1}', N'{2}', N'{3}', 
                                                        (select isnull(max(rec), 0) + 1 as REC from TEMP_PODTRANS_PAY where STCODE = '{0}' and REF_POD = '{2}')
                                                    , N'{4}', N'{5}', '{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', {14}, N'{15}', N'{16}') "
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.podRefNo
                                                , "P"
                                                , pay
                                                , payNum
                                                , amt
                                                , chg
                                                , pdisc
                                                , refID
                                                , approveCode
                                                , traceNo
                                                , terminalID
                                                , merchantID
                                                , date
                                                , invoiceNo
                                                , QRCode));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePODSaveSale(string refFFTI, string amt, string qrCode, string refID, string openDrawerTime)
        {
            try
            {
                DateTime date;
                if (openDrawerTime != "")
                {
                    date = DateTime.ParseExact(openDrawerTime, "HHmmss", new System.Globalization.CultureInfo("en-US")); 
                }
                else
                {
                    date = DateTime.Now; 
                }
                
                command.SetCommandStoredProcedure("pos_PODSaveSale");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.podRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("ref_FullTax", SqlDbType.VarChar, refFFTI);
                command.AddInputParameter("PODAmt", SqlDbType.VarChar, amt);
                command.AddInputParameter("QRCode_Data", SqlDbType.VarChar, qrCode);
                command.AddInputParameter("Reference_ID", SqlDbType.VarChar, refID ?? "");
                command.AddInputParameter("Cashier_ID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("CashDrawerOpenTime", SqlDbType.DateTime, date);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult podPrintReceipt()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PODPrintReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.podRefNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_PrintReceipt.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult syncPODToDataTank(string eventName, FunctionID functionId, string referenceNo, string voidReceiptNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_POS2DataTank");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, referenceNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                command.AddInputParameter("Event", SqlDbType.NVarChar, eventName);
                command.AddInputParameter("Database", SqlDbType.NVarChar, ProgramConfig.IsStandAloneMode ? "LOCALPOS" : "ESTORE");
                command.AddInputParameter("VoidReceiptNo", SqlDbType.NVarChar, voidReceiptNo);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult DeleteTEMP_PODTRANS_PAY(string rec = "", string pay = "", string payNumber = "", string approveCode = "")
        {
            try
            {
                string condition = "";
                if (rec != "")
                {
                    condition += " AND REC = '" + rec + "'";
                }

                if (pay != "")
                {
                    condition += " AND PAY = '" + pay + "'";
                }

                if (payNumber != "")
                {
                    condition += " AND PAY_NUMBER = '" + payNumber + "'";
                }

                if (approveCode != "")
                {
                    condition += " AND APPROVE_CODE = '" + approveCode + "'";
                }

                command.SetCommandText(String.Format(@"DELETE TEMP_PODTRANS_PAY WHERE STCODE = '{0}' and REF_POD= '{1}'  {2}"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.podRefNo
                                                , condition));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult VoidDeposit(string depoNo, string refNo, FunctionID function)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_VoidDeposit");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, function.formatValue);
                command.AddInputParameter("DepoNo", SqlDbType.NVarChar, depoNo);
                command.AddInputParameter("Menu", SqlDbType.Char, "V");
                command.AddInputParameter("reasonid", SqlDbType.NVarChar, 0);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable loadTEMP_PODTRANS_PAY(string refNo, string pmCode = "", string pmNumber = "", string amt = "")
        {
            try
            {
                string condition = "";
                if (pmCode != "")
                {
                    condition = " AND PAY = '" + pmCode + "'";
                }

                if (pmNumber != "")
                {
                    condition = " AND PAY_NUMBER = '" + pmNumber + "'";
                }

                if (amt != "")
                {
                    condition = " AND AMT = '" + amt + "'";
                }
                                                         
                command.SetCommandText(String.Format(@"SELECT * FROM [TEMP_PODTRANS_PAY] with(NOLOCK)
                                            WHERE [REF_POD] = '{0}'
                                            AND [STCODE] = '{1}' {2} "
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , condition
                                                ));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult SaveDepositPayTrans(string fftiNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_SaveDepositPayTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.abbNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");
                command.AddInputParameter("FFTINO", SqlDbType.VarChar, fftiNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult PODVoidReceipt(string podNo, string refNo, string openDrawer, string reason)
        {
            try
            {
                DateTime date;
                if (openDrawer != "")
                {
                    date = DateTime.ParseExact(openDrawer, "HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                }
                else
                {
                    date = DateTime.Now;
                }

                command.SetCommandStoredProcedure("pos_PODVoidReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, podNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_VoidReceipt.formatValue);
                command.AddInputParameter("usr_authorize", SqlDbType.NVarChar, ProgramConfig.superUserId);
                command.AddInputParameter("Reason_ID", SqlDbType.Char, reason);
                command.AddInputParameter("CashDrawerOpen", SqlDbType.NVarChar, date);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult PODPrintVoid(string refNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_PODPrintVoid");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_PrintVoidReceipt.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public bool saveTempCREDPAY_TRANS_PAY(string seq, string pmCode, string payNum, string amt, string chg)
        {
            try
            {
                DeleteTempCREDPAY_TRANS_PAY("", pmCode, payNum, "");

                command.SetCommandText(String.Format(@"INSERT INTO TempCREDPAY_TRANS_PAY with(ROWLOCK) (STORE_CODE, OPERATE_DATE, TILL_NO, Ref_CredPay, SEQ, PaymentMainCode,
                                                        PAYMENT_NUMBER, PAYMENT_AMOUNT, PAYMENT_CHANGE, STATUS, TRANSACTION_DATE)
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}' , 'A' , getdate())"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , ProgramConfig.creditSaleNo
                                                , seq
                                                , pmCode
                                                , payNum
                                                , amt
                                                , chg));
                command.ExecuteNonQuery();
                return true;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

     

        public int selectMaxSeqTempCREDPAY_TRANS_PAY(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT isnull(max(SEQ),0) AS SEQ FROM [TempCREDPAY_TRANS_PAY] with(NOLOCK)
                                            WHERE [STORE_CODE] = '{0}'
                                            AND [Ref_CredPay] = '{1}'"
                                                , ProgramConfig.storeCode
                                                , refNo
                                                ));

                DataTable dt = command.ExecuteToDataTable();
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    return int.Parse(dt.Rows[0][0].ToString());
                }
                return -1;
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CREDPaySaveChangeTrans(double posChg, double cashierChg)
        {
            try
            {
                command.SetCommandStoredProcedure("POS_CREDPaySaveChangeTrans");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.creditSaleNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_SaveChange.formatValue);
                command.AddInputParameter("PMCODE", SqlDbType.Char, "N/A");
                command.AddInputParameter("PosChg", SqlDbType.Money, posChg);
                command.AddInputParameter("CashierChg", SqlDbType.Money, cashierChg);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveCREDPaySaveSale()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CREDPaySaveSale");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.creditSaleNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, "N/A");

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult saveTempCREDPAY_TRANS(CreditSaleData credit)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO TempCREDPAY_TRANS with(ROWLOCK) 
                                                (STORE_CODE, OPERATE_DATE, TILL_NO, Ref_CredPay, CUSTOMER_ID, CUSTOMER_NO, CARD_HOLDER_NUMBER, AMOUNT, STATUS,
                                                CASHIER_ID, SUPER_CASHIER_ID, REASON_ID, TRANSACTION_DATE, LASTUPDATED_DATE,
                                                SynchStatus, Trans_ID, Payment_ID)
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}' , N'{9}', N'{10}', N'{11}', getdate(), getdate() ,  N'{12}', N'{13}', N'{14}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , credit.RefCreditPay
                                                , credit.CustomerID
                                                , credit.CustomerNo
                                                , credit.CustomerCardNo
                                                , credit.Amount
                                                , credit.Status
                                                , ProgramConfig.userId
                                                , ProgramConfig.superUserId
                                                , "0"
                                                , credit.SyncStatus
                                                , credit.TransID
                                                , credit.PaymentID
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveTempCREDPAY_TRANS_Detail(CreditSaleDataDetail credit)
        {
            try
            {
                command.SetCommandText(String.Format(@"INSERT INTO TempCREDPAY_TRANS_Detail with(ROWLOCK) 
                                                (STORE_CODE, OPERATE_DATE, TILL_NO, Ref_CredPay, SEQ, CRED_INVOICE_NO, CRED_INVOICE_DATE, CRED_AMOUNT, STATUS, TRANSACTION_DATE)
                                                values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}' , N'{9}')"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.operateDate
                                                , ProgramConfig.tillNo
                                                , credit.RefCreditPay
                                                , credit.Seq
                                                , credit.Credit_InvoiceNo
                                                , credit.Credit_InvoiceDate
                                                , credit.Credit_Amount
                                                , "A"
                                                , credit.TransDate
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public DataTable selectPODTRANS(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [PODTRANS] with(NOLOCK)
                                            WHERE [REF_POD] = '{0}' "
                                                , refNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }


        public DataTable selectPODTRANS_PAY()
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [PODTRANS_PAY] with(NOLOCK)
                                            WHERE [REF_POD] = '{0}' "
                                                , ProgramConfig.podRefNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CREDPayPrintReceipt()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CREDPayPrintReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, ProgramConfig.creditSaleNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CreditSale_Print.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public DataTable loadTempCREDPAY_TRANS_PAY(string refNo, string pmCode = "", string pmNumber = "", string amt = "")
        {
            try
            {
                string setCondition = "";
                if (pmCode != "")
                {
                    setCondition = " AND PaymentMainCode = '" + pmCode + "'";
                }

                if (pmNumber != "")
                {
                    setCondition = " AND Payment_Number = '" + pmNumber + "'";
                }



                command.SetCommandText(String.Format(@"SELECT * FROM [TempCREDPAY_TRANS_PAY] with(NOLOCK)
                                            WHERE [REF_CredPay] = '{0}'
                                            AND [STORE_CODE] = '{1}' {2}"
                                                , refNo
                                                , ProgramConfig.storeCode
                                                , setCondition));

                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult DeleteTempCREDPAY()
        {
            try
            {
                command.SetCommandText(String.Format(@"
                                                    DELETE TempCREDPAY_TRANS WHERE STORE_CODE = '{0}' and Ref_CredPay = '{1}';
                                                    DELETE TempCREDPAY_TRANS_DETAIL WHERE STORE_CODE = '{0}' and Ref_CredPay = '{1}';
                                                    DELETE TempCREDPAY_TRANS_PAY WHERE STORE_CODE = '{0}' and Ref_CredPay = '{1}'; "
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.creditSaleNo
                                                ));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updateCreditPayTrans(string refNo, bool isVoid, string transID = "", string paymentID = "", string transIDVoid = "", string paymentIDVoid = "")
        {
            try
            {
                string setCondition = "";

                if (!isVoid)
                {
                    setCondition += " SynchStatus = 'Y' ";
                }
                else
                {
                    setCondition += " SynchStatus_Void = 'Y' ";
                }

                if (transID != "")
                {
                    setCondition += ", Trans_ID = N'" + transID + "'";
                }

                if (paymentID != "")
                {
                    setCondition += ", Payment_ID = N'" + paymentID + "'";
                }

                if (transIDVoid != "")
                {
                    setCondition += ", Trans_ID_Void = N'" + transIDVoid + "'";
                }

                if (paymentIDVoid != "")
                {
                    setCondition += ", Payment_ID_Void = N'" + paymentIDVoid + "'";
                }

                command.SetCommandText(String.Format(@"Update [CREDPay_TRANS] SET {2} 
                                            WHERE STORE_CODE = '{0}' and [Ref_CredPay] = '{1}' "
                                                , ProgramConfig.storeCode
                                                , refNo
                                                , setCondition
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult updatePOD_TRANS(string refNo, bool isVoid, string transID = "", string paymentID = "", string transIDVoid = "", string paymentIDVoid = "")
        {
            try
            {
                string setCondition = "";

                if (!isVoid)
                {
                    setCondition += " SynchStatus = 'Y' ";
                }
                else
                {
                    setCondition += " SynchStatus_Void = 'Y' ";
                }

                if (transID != "")
                {
                    setCondition += ", Trans_ID = N'" + transID + "'";
                }

                if (paymentID != "")
                {
                    setCondition += ", Payment_ID = N'" + paymentID + "'";
                }

                if (transIDVoid != "")
                {
                    setCondition += ", Trans_ID_Void = N'" + transIDVoid + "'";
                }

                if (paymentIDVoid != "")
                {
                    setCondition += ", Payment_ID_Void = N'" + paymentIDVoid + "'";
                }

                command.SetCommandText(String.Format(@"Update [PODTRANS] SET {2} 
                                            WHERE STCODE = '{0}' AND [Ref_POD] = '{1}' "
                                                , ProgramConfig.storeCode
                                                , refNo
                                                , setCondition
                                                ));
                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult DeleteTempCREDPAY_TRANS_PAY(string seq = "", string pmCode = "", string payNumber = "", string approveCode = "")
        {
            try
            {
                string condition = "";
                if (seq != "")
                {
                    condition += " AND SEQ = '" + seq + "'";
                }

                if (pmCode != "")
                {
                    condition += " AND PaymentMainCode = '" + pmCode + "'";
                }

                if (payNumber != "")
                {
                    condition += " AND PAYMENT_NUMBER = '" + payNumber + "'";
                }

                command.SetCommandText(String.Format(@"DELETE TempCREDPAY_TRANS_PAY WHERE STORE_CODE = '{0}' and Ref_CredPay = '{1}'  {2}"
                                                , ProgramConfig.storeCode
                                                , ProgramConfig.creditSaleNo
                                                , condition));

                command.ExecuteNonQuery();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CredPayVoidReceipt(string podNo, string refNo, string openDrawer, string reason)
        {
            try
            {
                DateTime date;
                if (openDrawer != "")
                {
                    date = DateTime.ParseExact(openDrawer, "HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                }
                else
                {
                    date = DateTime.Now;
                }

                command.SetCommandStoredProcedure("pos_CredPayVoidReceipt");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, podNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ReceivePOD_VoidReceipt.formatValue);
                command.AddInputParameter("usr_authorize", SqlDbType.NVarChar, ProgramConfig.superUserId);
                command.AddInputParameter("Reason_ID", SqlDbType.Char, reason);
                command.AddInputParameter("CashDrawerOpen", SqlDbType.NVarChar, date);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectCREDPAY_TRANS(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [CREDPAY_TRANS] with(NOLOCK)
                                            WHERE [REF_CredPay] = '{0}' "
                                                , refNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CredPayPrintVoid(string refNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CredPayPrintVoid");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.CreditSale_PrintVoidReceipt.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult CheckLastReceiptCredPay()
        {
            try
            {
                command.SetCommandStoredProcedure("pos_CheckLastReceiptCredPay");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.Login_CheckLastReceiptCredPay.formatValue);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectCREDPAY_TRANS_PAY(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [CREDPAY_TRANS_PAY] with(NOLOCK)
                                            WHERE [REF_CredPay] = '{0}' "
                                                , refNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public DataTable selectCREDPAY_TRANS_DETAIL(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [CREDPAY_TRANS_DETAIL] with(NOLOCK)
                                            WHERE [REF_CredPay] = '{0}' "
                                                , refNo
                                                ));
                return command.ExecuteToDataTable();
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult clearUp(FunctionID functionId, string voidRefNo)
        {
            try
            {
                string refNo = "";
                if (functionId == FunctionID.ReceivePOD_CancelVoid || functionId == FunctionID.ReceivePOD_CancelVoid )
                {
                    refNo = voidRefNo;
                }
                else if (functionId == FunctionID.CreditSale_APIAR)
                {
                    refNo = ProgramConfig.creditSaleNo;
                }
                else if (functionId == FunctionID.ReceivePOD_RollBack)
                {
                    refNo = ProgramConfig.podRefNo;
                }

                command.SetCommandStoredProcedure("pos_CleanUp");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, refNo);
                command.AddInputParameter("FunctionID", SqlDbType.Char, functionId.formatValue);
                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult selectDLYPTRANS(string refNo, string vty = "", string dty = "")
        {
            try
            {
                string condition = "";
                if (vty != "")
                {
                    condition += " AND VTY = '" + vty + "'";
                }
                if (dty != "")
                {
                    condition += " AND DTY = '" + dty + "'";
                }

                command.SetCommandText(String.Format(@"SELECT * FROM [DLYPTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}' {1}"
                                , refNo
                                , condition
                                ));

                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }


        public StoreResult selectEDCTrans(string refNo)
        {
            try
            {
                command.SetCommandText(String.Format(@"SELECT * FROM [EDCTRANS] with(NOLOCK)
                                            WHERE [REF] = '{0}' "
                                , refNo
                                ));
                DataTable dt = command.ExecuteToDataTable();
                return new StoreResult(ResponseCode.Success, "Success", data: dt);
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }

        public StoreResult validateFFTI(string fftiNo)
        {
            try
            {
                command.SetCommandStoredProcedure("pos_ValidateFFTI");
                command.AddInputParameter("LanguageID", SqlDbType.Int, ProgramConfig.language.ID);
                command.AddInputParameter("OperateDate", SqlDbType.Char, ProgramConfig.operateDate);
                command.AddInputParameter("StoreCode", SqlDbType.VarChar, ProgramConfig.storeCode);
                command.AddInputParameter("TillNo", SqlDbType.VarChar, ProgramConfig.tillNo);
                command.AddInputParameter("UserID", SqlDbType.VarChar, ProgramConfig.userId);
                command.AddInputParameter("SuperUserID", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("ReferenceNo", SqlDbType.NVarChar, "N/A");
                command.AddInputParameter("FunctionID", SqlDbType.Char, FunctionID.ValidFFTI.formatValue);
                command.AddInputParameter("FFTI_NO", SqlDbType.VarChar, fftiNo);

                return new StoreResult(command.ExecuteToDataTable());
            }
            catch (NetworkConnectionException ex)
            {
                AppLog.writeLog(ex);
                throw ex;
            }
        }
    }
}
