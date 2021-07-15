using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Data;
using System.Configuration;
using BJCBCPOS_Model;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using MMFSAPI;
using System.Threading;
using ApiServices;

namespace BJCBCPOS_Process
{
    /// <summary>
    /// StartProcess class
    /// contains all process about start program and login 
    /// </summary>
    public class StartProcess
    {
        private SqlCommand command;

        /// <summary>
        /// Constructor create new StartProcess Class object
        /// </summary>
        public StartProcess()
        {
            command = BaseProcess.command;
        }

        public void getDatabaseLogin()
        {
            try
            {
                DataTable tab = command.getApplicationDBLogin();
                if (tab != null && tab.Rows.Count > 0)
                {
                    if (tab.Columns.Contains("USERID"))
                    {
                        ProgramConfig.app_db_user = tab.Rows[0]["USERID"].ToString();
                    }
                    if (tab.Columns.Contains("PASSWORD"))
                    {
                        // decrypt password
                        ProgramConfig.app_db_pass = BlowfishEncryption.decrypt(tab.Rows[0]["PASSWORD"].ToString());
                    }
                }

                // change db user and login
                BaseProcess.changeConnectionString(ProgramConfig.IsStandAloneMode ? ProgramConfig.connectionStringLocal : ProgramConfig.connectionString);
                command = BaseProcess.command;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.getDatabaseLogin");
                throw;
            }
        }

        /// <summary>
        /// start program process.
        /// check app version and get all config from DB
        /// </summary>
        /// <returns>result from database store procedure</returns>
        public StoreResult getPOSConfigFromDB()
        {
            string s1 = "", s2 = "";
            try
            {
                StoreResult result = command.checkAppVersion();
                if (result.response.next)
                {
                    // get permission
                    ProgramConfig.permissionId = result.otherData.Rows[0]["PermissionID"].ToString();
                    result = command.getPosConfig();
                    if (result.response.next)
                    {
                        // get all parameter code and value
                        Dictionary<string, object> config = new Dictionary<string, object>();
                        foreach (DataRow row in result.otherData.Rows)
                        {
                            s1 = row[0].ToString().Trim();
                            s2 = row[1].ToString().Trim();
                            if (s1.Equals("StoreCode", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!ProgramConfig.storeCode.Equals(s2))
                                {
                                    string responseMessage = ProgramConfig.message.get("StartProcess", "StoreCodeIncorrect").message;
                                    string helpMessage = ProgramConfig.message.get("StartProcess", "StoreCodeIncorrect").help;
                                    result = new StoreResult(ResponseCode.Error, responseMessage, helpMessage);

                                    //result = new StoreResult(ResponseCode.Error, "Store Code ไม่ถูกต้อง");
                                    return result;
                                }
                            }
                            if (!config.ContainsKey(s1))
                            {
                                config.Add(s1, s2);
                            }
                        }
                        ProgramConfig.posConfig = config;

                        // get payment exchange rate and currency
                        result = command.getPaymentConfig(FunctionID.Login_LoadPaymentConfig, "N/A");
                        if (result.response.next)
                        {
                            ProgramConfig.payment = new PaymentConfigCollections(result.otherData);
                            ProgramConfig.currency = new CurrencyCollections(result.otherData);

                            DataTable dtMenuIcon = command.selectPaymentMenuIcon();

                            if (dtMenuIcon.Rows.Count > 0)
                            {
                                ProgramConfig.paymentMenuIconDT = dtMenuIcon;
                                ProgramConfig.paymentMenuIcon = new PaymentMenuIconCollections(dtMenuIcon);


                                // get payment policy
                                result = command.getPaymentPolicy();
                                if (result.response.next)
                                {
                                    ProgramConfig.paymentPolicy = new PaymentPolicyCollections(result.otherData);

                                    // get alert message
                                    result = command.getAlertMessage();
                                    if (result.response.next)
                                    {
                                        ProgramConfig.message = new AlertMessageCollection(result.otherData);

                                        // get language 
                                        //StoreResult result2 = SetLanguage();

                                        // get Image
                                        result = command.getPosImage();
                                        string fileName = "";//ProgramConfig.getPosConfig("BusinessLogo").ToString();
                                        byte[] ImageData;
                                        MemoryStream imgStream;
                                        Image img;
                                        if (result.response.next)
                                        {
                                            foreach (DataRow row in result.otherData.Rows)
                                            {
                                                if (ProgramConfig.dtActiveLanguage.AsEnumerable().Any(dr => (dr["ImageName"] + "").ToUpper() == row["ImageName"].ToString().ToUpper()))
                                                {
                                                    fileName = @"iconLanguage\" + row["ImageName"].ToString();
                                                }
                                                else
                                                {
                                                    fileName = row["ImageName"].ToString();
                                                }
                                                ImageData = (byte[])row["Image"];
                                                imgStream = new MemoryStream(ImageData);
                                                img = Image.FromStream(imgStream, false, true);
                                                img.Save(fileName);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result = new StoreResult(ResponseCode.Error, "No payment menu icon.");
                            }
                        }

                        // change connection timeout and command timeout get from database
                        command.changeConnectionString(ProgramConfig.IsStandAloneMode ? ProgramConfig.connectionStringLocal : ProgramConfig.connectionString);
                    }
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.getPOSConfigFromDB");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        /// <summary>
        /// check user login
        /// </summary>
        /// <param name="user">user id to check</param>
        /// <param name="password">password to check</param>
        /// <returns>result from database store procedure</returns>
        public ProcessResult checkLogin(string user, string password)
        {
            try
            {
                List<NotifyMessage> message = null;
                ProgramConfig.superUserId = "N/A";
                ProgramConfig.superUserName = "";
                ProgramConfig.superUserAuthorizeResult = null;
                StoreResult result = command.checkLogin(user, password);
                if (!result.response.next)
                {
                    return new ProcessResult(result, false);
                }
                else
                {
                    if (result.response == ResponseCode.Information)
                    {
                        if (message == null) { message = new List<NotifyMessage>(); }
                        message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));
                    }

                    ProgramConfig.userId = user;
                    ProgramConfig.password = password;
                    ProgramConfig.cashireAuthorizeResult = result;
                    if (result.otherData != null && result.otherData.Rows != null && result.otherData.Rows.Count > 0)
                    {
                        ProgramConfig.cashierName = result.otherData.Rows[0]["UserNameLocal"].ToString();
                    }

                    if (result.response == ResponseCode.PasswordExpired)
                    {
                        string responseMessage = result.responseMessage;
                        string helpMessage = result.helpMessage;

                        result = command.getAuthority();
                        if (result.response.next)
                        {
                            if (result.response == ResponseCode.Information)
                            {
                                if (message == null) { message = new List<NotifyMessage>(); }
                                message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));
                            }

                            //ProgramConfig.profile = new ProfileCollection();
                            //foreach (DataRow row in result.otherData.Rows)
                            //{
                            //    ProgramConfig.profile.Add(new Profile(row["FunctionID"].ToString().Replace("-", ""), (int)row["PolicyStatus"], (int)row["ProfileStatus"], "N", row["DiffUserStatus"].ToString()));
                            //}
                            ProgramConfig.profile = new ProfileCollection(result.otherData);
                        }
                        //string responseMessage = ProgramConfig.message.get("StartProcess", "PasswordExpire").message;
                        //string helpMessage = ProgramConfig.message.get("StartProcess", "PasswordExpire").help;
                        return new ProcessResult(ResponseCode.PasswordExpired, responseMessage, helpMessage, true, message);

                        //return new ProcessResult(ResponseCode.PasswordExpired, "รหัสผ่านหมดอายุ กรุณาเปลี่ยนรหัสผ่านใหม่", "", true, notify: message);

                    }
                    else
                    {
                        result = command.getAuthority();
                        if (result.response.next)
                        {
                            if (result.response == ResponseCode.Information)
                            {
                                if (message == null) { message = new List<NotifyMessage>(); }
                                message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));
                            }

                            //ProgramConfig.profile = new ProfileCollection();
                            //foreach (DataRow row in result.otherData.Rows)
                            //{
                            //    ProgramConfig.profile.Add(new Profile(row["FunctionID"].ToString().Replace("-", ""), (int)row["PolicyStatus"], (int)row["ProfileStatus"], "N", row["DiffUserStatus"].ToString()));
                            //}
                            ProgramConfig.profile = new ProfileCollection(result.otherData);
                            ProgramConfig.updateSaleConfig();
                        }
                        return new ProcessResult(result, false, notify: message);
                    }
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.checkLogin");
                throw;
            }
            catch (Exception ex)
            {
                // clear login value.
                ProgramConfig.userId = "";
                ProgramConfig.password = "";
                ProgramConfig.cashierName = "";
                ProgramConfig.cashireAuthorizeResult = null;
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult checkLoginPolicy()
        {
            try
            {
                // check profile
                Profile check = ProgramConfig.getProfile(FunctionID.Login_CheckProfile);
                if (!check.found)
                {
                    return new ProcessResult(ResponseCode.Error, ProgramConfig.message.get("", "GetAuthorityNotFound"), false);
                }
                else if (check.policy == PolicyStatus.Work && check.profile == ProfileStatus.NotAuthorize) // policyStatus = 2 && profileStatus = 1
                {
                    // popup super user login
                    return new ProcessResult(ResponseCode.Success, "", "", true);
                }
                else
                {
                    return new ProcessResult(ResponseCode.Success, "", "", false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.checkLoginPolicy");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult checkTerminal()
        {
            try
            {
                List<NotifyMessage> message = null;
                // check terminal
                StoreResult result = command.checkTerminal();
                if (!result.response.next)
                {
                    return new ProcessResult(result);
                }
                else
                {
                    if (result.response == ResponseCode.Information)
                    {
                        if (message == null) { message = new List<NotifyMessage>(); }
                        message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));
                    }

                    //************ check terminal not return column ***************
                    ProgramConfig.abbNo = result.otherData.Rows[0]["AbbNo"].ToString();
                    ProgramConfig.cnNo = result.otherData.Rows[0]["CNNo"].ToString();
                    ProgramConfig.fftiNo = result.otherData.Rows[0]["FFTINo"].ToString();

                    ProgramConfig.saleRefNoIni = result.otherData.Rows[0]["SaleInternalRef"].ToString();
                    ProgramConfig.voidRefNoIni = result.otherData.Rows[0]["VoidInternalRef"].ToString();
                    ProgramConfig.returnRefNoIni = result.otherData.Rows[0]["RetnInternalRef"].ToString();
                    ProgramConfig.cashInRefNoIni = result.otherData.Rows[0]["CashinoutInternalRef"].ToString();
                    ProgramConfig.endOfShiftRefNoIni = result.otherData.Rows[0]["LoginInternalRef"].ToString();
                    ProgramConfig.expermitRefNoIni = result.otherData.Rows[0]["ExportPermitRef"].ToString();
                    ProgramConfig.openDayRefNoIni = result.otherData.Rows[0]["OpendayInternalRef"].ToString();
                    ProgramConfig.posrepRefNoIni = result.otherData.Rows[0]["PosRepRef"].ToString();
                    ProgramConfig.actionRefNoIni = result.otherData.Rows[0]["DelEditItemInternalRef"].ToString();
                    ProgramConfig.holdOrderRefNoIni = result.otherData.Rows[0]["HoldRef"].ToString();
                    ProgramConfig.tempFFTINo = result.otherData.Rows[0]["TempFFTIRef"].ToString();

                    ProgramConfig.running.updateValue();

                    result = command.saveLoginTransaction();
                    if (result.response.next)
                    {
                        ProgramConfig.endOfShiftRefNoIni = result.otherData.Rows[0]["ReferenceNo"].ToString();
                        if (result.response == ResponseCode.Information)
                        {
                            if (message == null) { message = new List<NotifyMessage>(); }
                            message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));
                        }

                        // check policy
                        Profile check = ProgramConfig.getProfile(FunctionID.Login_SaveLoginTransaction_SynchSaleTransactiontoDataTank);
                        if (check.policy == PolicyStatus.Work)
                        {
                            string refNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                            string recNo = result.otherData.Rows[0]["rec"].ToString();
                            string cashierID = result.otherData.Rows[0]["CashID"].ToString();
                            // sync to HO
                            result = command.syncToDataTank("Login", FunctionID.Login_SaveLoginTransaction_SynchSaleTransactiontoDataTank, refNo, recNo, ProgramConfig.abbNo, cashierID);
                        }
                    }
                    return new ProcessResult(result, notify: message);
                }
            }
            catch (NetworkConnectionException)
            {
                command.rollback();
                AppLog.writeLog("connection to server lost at StartProcess.checkTerminal");
                throw;
            }
            catch (Exception ex)
            {
                command.rollback();
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public ProcessResult autoVoid(string refNo, string abb, string abb_ini)
        {
            try
            {
                string receiptNo = "";
                string voidReceiptNo = "";
                string saleRefNo = "";
                string abbNo = "";
                //Hardware.checkPrinter();

                List<NotifyMessage> message = null;
                //AutoVoid
                //ProgramConfig.running = new INIConfig(FixedData.running_name);
                StoreResult result = command.checkLastReceipt(refNo, abb, abb_ini, FunctionID.Login_AutoVoid);
                if (result.response == ResponseCode.Success)
                {
                    return new ProcessResult(result);
                }
                else
                {                    
                    if (result.otherData != null)
                    {
                        receiptNo = result.otherData.Rows[0]["ABB_NO"].ToString().Trim();
                        //abbNo = result.otherData.Rows[0]["ABB_NO_INI"].ToString().Trim();
                        saleRefNo = result.otherData.Rows[0]["Internal_SaleREF"].ToString().Trim();

                        if (result.response == ResponseCode.Information)
                        {
                            if (message == null) { message = new List<NotifyMessage>(); }
                            message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));

                            //if (abbNo != null && abbNo != "")
                            //{
                            //    ProgramConfig.abbNo = abbNo;
                            //    ProgramConfig.running.updateValue();
                            //}
                        }

                        if (result.otherData.Rows[0]["Action_Type"].ToString().Trim() == "C")
                        {
                            command.newTransaction();
                            int rec = 0;
                            DataTable data = command.selectTempDlyptrans(saleRefNo);
                            foreach (DataRow row in data.Rows)
                            {
                                rec = (int)row["REC"];
                                //command.updateTempDlyptrans(saleRefNo, rec.ToString());
                                if (!command.updateTempDlyptransLogin(saleRefNo, rec.ToString()))
                                {
                                    command.rollback();
                                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                                    throw new Exception(responseMessage);
                                    throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                                }
                            }

                            string newRec = (rec + 10001).ToString();
                            //command.saveTempDlyptrans(saleRefNo, newRec.ToString(), "0", "H", "Auto Cancel", "0", "0", "0", ProgramConfig.userId, "0"
                            //, "V", "0", "0", "0", "0", "0", "", "0");
                            if (!command.saveTempDlyptrans(saleRefNo, newRec.ToString(), "0", "H", "Auto Cancel", "0", "0", "0", ProgramConfig.userId, "0"
                            , "V", "0", "0", "0", "0", "0", "", "0"))
                            {
                                command.rollback();
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(responseMessage);
                                throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                            }

                            command.commit();

                            result = command.saveCancelTransaction(FunctionID.Login_AutoVoid_SaveCancelTrans, saleRefNo);

                            if (result.response.next)
                            {
                                if (result.otherData != null)
                                {
                                    ProgramConfig.cancelNo = result.otherData.Rows[0]["CancelNo"].ToString();
                                }

                                Profile chkPrintCancel = ProgramConfig.getProfile(FunctionID.Login_AutoVoid_PrintCancel);
                                if (chkPrintCancel.policy == PolicyStatus.Work)
                                {
                                    result = command.printCancel(FunctionID.Login_AutoVoid_PrintCancel, saleRefNo);
                                    if (!result.response.next)
                                    {
                                        return new ProcessResult(result, notify: message);
                                    }
                                    else if (result.response == ResponseCode.Information)
                                    {
                                        if (message == null) { message = new List<NotifyMessage>(); }
                                        message.Add(new NotifyMessage(result.response, result.responseMessage, result.helpMessage));
                                    }
                                    DataTable dt = result.otherData;
                                    Hardware.printTermal(dt);
                                }

                                Profile chkCancelDataTank = ProgramConfig.getProfile(FunctionID.Login_AutoVoid_SyncCancelToDataTank);
                                if (chkCancelDataTank.policy == PolicyStatus.Work)
                                {
                                    result = command.syncToDataTank("Cancel", FunctionID.Login_AutoVoid_SyncCancelToDataTank, saleRefNo, "1", ProgramConfig.cancelNo);
                                    if(!result.response.next)
                                    {
                                        return new ProcessResult(result, notify: message);
                                    }
                                }

                            }
                            else
                            {
                                return new ProcessResult(result, notify: message);
                            }
                        }
                        else if (result.otherData.Rows[0]["Action_Type"].ToString().Trim() == "V")
                        {                      
                            result = command.saveVoidTransaction(receiptNo, "0", "0", "N/A", FunctionID.Login_AutoVoid_SaveVoidTrans); 
                            if (result.response.next)
                            {
                                voidReceiptNo = result.otherData.Rows[0]["VoidReceiptNo"].ToString(); 
                                Profile chkConcludVoid = ProgramConfig.getProfile(FunctionID.Login_AutoVoid_ConcludeVoid);
                                if (chkConcludVoid.policy == PolicyStatus.Work)
                                {
                                    result = command.concludeVoid(voidReceiptNo, "", FunctionID.Login_AutoVoid_ConcludeVoid);
                                    if (!result.response.next)
                                    {
                                        return new ProcessResult(result, notify: message);
                                    }
                                }

                                Profile chkPrintVoid = ProgramConfig.getProfile(FunctionID.Login_AutoVoid_PrintVoidReceipt);
                                if (chkPrintVoid.policy == PolicyStatus.Work)
                                {
                                    result = command.PrintVoidReceipt(voidReceiptNo, "", FunctionID.Login_AutoVoid_PrintCancel);
                                    if (!result.response.next)
                                    {
                                        return new ProcessResult(result, notify: message);
                                    }
                                    DataTable dt = result.otherData;
                                    Hardware.printTermal(dt);
                                }

                                Profile chkVoidDataTank = ProgramConfig.getProfile(FunctionID.Login_AutoVoid_SyncVoidToDataTank);
                                if (chkVoidDataTank.policy == PolicyStatus.Work)
                                {
                                    result = command.syncToDataTank("Void", FunctionID.Login_AutoVoid_SyncCancelToDataTank, "N/A", "1", voidReceiptNo);
                                    if (!result.response.next)
                                    {
                                        return new ProcessResult(result, notify: message);
                                    }
                                }
                            }
                            else
                            {
                                return new ProcessResult(result, notify: message);
                            }
                        }
                    }                   
                
                }

                return new ProcessResult(result, notify: message);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.autoVoid");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult checkAuthorize(FunctionID functionId, string user, string password, string refNo)
        {
            try
            {
                ProgramConfig.superUserId = "";
                ProgramConfig.superUserName = "";
                ProgramConfig.superUserAuthorizeResult = null;
                StoreResult result = command.checkUserAuthorize(functionId, user, password, refNo);
                if (result.response.next)
                {
                    ProgramConfig.superUserId = user;
                    ProgramConfig.superPassword = password;
                    if (result.otherData != null && result.otherData.Rows != null && result.otherData.Rows.Count > 0)
                    {
                        ProgramConfig.superUserName = result.otherData.Rows[0]["UserNameLocal"].ToString();
                    }
                    ProgramConfig.superUserAuthorizeResult = result;
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.checkAuthorize");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult getCashierMessage()
        {
            try
            {
                return command.getCashierMessage();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.getCashierMessage");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }      


        public bool retryConnection(int retryTime)
        {
            try
            {
                AppLog.writeLog("server connection lost. try reconnecting " + retryTime + " times.");
                command = BaseProcess.command;
                return command.testConnection();
            }
            catch (NetworkConnectionException)
            {
                retryTime++;
                if (retryTime > ProgramConfig.connectionRetry)
                //if (retryTime > 1)
                {
                    return false;
                }
                System.Threading.Thread.Sleep(1000);
                return retryConnection(retryTime);
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult SetLanguage()
        {
            StoreResult res = command.getLanguage();

            if (res.response.next)
            {
                ProgramConfig.dtActiveLanguage = res.otherData.AsDataView().ToTable(false, "LANGUAGE_ID", "LANGUAGE_DESC", "COUNTRY_CODE", "LANGUAGE_FONT", "LANGUAGE_CULTURE", "LANGUAGE_FILE", "ImageName");
                ProgramConfig.listActiveLanguage = new List<Language>();
                foreach (DataRow item in ProgramConfig.dtActiveLanguage.Rows)
                {
                    ProgramConfig.listActiveLanguage.Add(new Language((int)item["LANGUAGE_ID"]));

                    if (!Directory.Exists("iconLanguage"))
                    {
                        Directory.CreateDirectory("iconLanguage");
                    }

                    string filePath = string.Format(@"{0}\{1}", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), item["LANGUAGE_FILE"]);

                    if (!File.Exists(filePath))
                    {
                        File.WriteAllText(filePath, "", Encoding.Unicode);            
                    }
                }
            }

            return res;
        }

        public bool CheckLocalPOS()
        {
            try
            {
                if (ProgramConfig.saleMode == SaleMode.Standalone)
                {
                    command = BaseProcess.commandLocal;
                    return command.testConnection();
                }
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("CheckLocalPOS NetworkConnectionException : " + net.Message);
                return false;
            }
            catch (Exception ex)
            {
                AppLog.writeLog("CheckLocalPOS Exception : " + ex.Message);
                return false;
            }
            return true;
        }

        public StoreResult CheckRunningNumber(string sRef, string vRef, string rRef, string cRef, string lRef, 
                                              string eRef, string oRef, string pRef, string dRef, string hRef, string tRef)
        {
            try
            {
                StoreResult res = command.CheckRunningNumber(sRef, vRef, rRef, cRef, lRef, eRef, oRef, pRef, dRef, hRef, tRef);
                if (res.response.next && res.otherData != null && res.otherData.Rows.Count > 0)
                {
                    if (res.response == ResponseCode.Success)
                    {
                        DataTable dt = res.otherData;
                        ProgramConfig.saleRefNoIni = dt.Rows[0]["SaleInternalRef"].ToString();
                        ProgramConfig.voidRefNoIni = dt.Rows[0]["VoidInternalRef"].ToString();
                        ProgramConfig.returnRefNoIni = dt.Rows[0]["RetnInternalRef"].ToString();
                        ProgramConfig.cashInRefNoIni = dt.Rows[0]["CashinoutInternalRef"].ToString();
                        ProgramConfig.endOfShiftRefNoIni = dt.Rows[0]["LoginInternalRef"].ToString();
                        ProgramConfig.expermitRefNoIni = dt.Rows[0]["ExportPermitRef"].ToString();
                        ProgramConfig.openDayRefNoIni = dt.Rows[0]["OpendayInternalRef"].ToString();
                        ProgramConfig.posrepRefNoIni = dt.Rows[0]["PosRepRef"].ToString();
                        ProgramConfig.actionRefNoIni = dt.Rows[0]["DelEditItemInternalRef"].ToString();
                        ProgramConfig.holdOrderRefNoIni = dt.Rows[0]["HoldRef"].ToString();
                        ProgramConfig.tempFFTINo = dt.Rows[0]["TempFFTIRef"].ToString();

                        ProgramConfig.running.updateValue();

                        AppLog.writeLog(String.Format(@"[CheckRunningNumber] SaleInternalRef = {0}, VoidInternalRef = {1}, RetnInternalRef = {2}, 
                        CashinoutInternalRef = {3}, LoginInternalRef = {4}, ExportPermitRef = {5}, OpendayInternalRef = {6}, PosRepRef = {7}, actionRefNo = {8}"
                                                       ,dt.Rows[0]["SaleInternalRef"].ToString()
                                                       , dt.Rows[0]["VoidInternalRef"].ToString()
                                                       , dt.Rows[0]["RetnInternalRef"].ToString()
                                                       , dt.Rows[0]["CashinoutInternalRef"].ToString()
                                                       , dt.Rows[0]["LoginInternalRef"].ToString()
                                                       , dt.Rows[0]["ExportPermitRef"].ToString()
                                                       , dt.Rows[0]["OpendayInternalRef"].ToString()
                                                       , dt.Rows[0]["PosRepRef"].ToString()
                                                       , dt.Rows[0]["DelEditItemInternalRef"].ToString()));
                    }
                }
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at StartProcess.CheckRunningNumber");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckUser(string user, string password)
        {
            return command.checkLogin(user, password);
        }

        public StoreResult CheckLastReceiptCredPay()
        {
            var res = command.CheckLastReceiptCredPay();
            if (res.response.next && res.otherData.Rows[0][0].ToString() != "")
            {
                int defaultDelay = 3;
                int defaultRetry = 5;
                var res2 = command.selectAPICONF_RETRY("CancelPayInvoiceAR");
                if (res2.response.next)
                {
                    defaultDelay = Convert.ToInt32(res2.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                    defaultRetry = Convert.ToInt32(res2.otherData.Rows[0]["NUMBER_RETRY"]);
                }

                clsMMFSAPI sv = new clsMMFSAPI();
                foreach (DataRow item in res.otherData.Rows)
                {
                    string rescode = "";
                    string resmsg_en = "";
                    string resmsg_th = "";
                    string paymentID = "";
                    string transID = res.otherData.Rows[0]["trans_id"].ToString();
                    string storeCode = res.otherData.Rows[0]["Store_Code"].ToString();
                    string createby = res.otherData.Rows[0]["CASHIER_ID"].ToString();
                    string invoiceNo = "";
                    double paymentAmount = 0;
                    string paydate = "";
                    string rep = res.otherData.Rows[0]["Ref_CredPay"].ToString();
                    string recepitNo = "";
                    string status = "";
                    clsMMFSAPI.invoice_list[] invList = null;
                    clsMMFSAPI.payment_list[] pmList = null;

                    int cnt2 = 0;
                Retry:
                    sv.CheckPayInvoiceAR(ref rescode, ref resmsg_en, ref resmsg_th, ref transID, ref storeCode, ref createby,
                        ref paymentID, ref invoiceNo, ref paymentAmount, ref paydate, ref recepitNo, ref status, ref pmList, ref invList);

                    if (rescode == "0000")
                    {
                        command.updateCreditPayTrans(recepitNo, false);
                        return new StoreResult(ResponseCode.Success, resmsg_th);
                    }
                    else if (rescode == "9999" && cnt2 <= defaultRetry)
                    {
                        cnt2++;
                        Thread.Sleep(defaultDelay);
                        goto Retry;
                    }
                    else if (rescode != "9999" && rescode != "0000")
                    {
                        DataTable dtCred = command.selectCREDPAY_TRANS(rep);
                        DataTable dtCredDetial = command.selectCREDPAY_TRANS_DETAIL(rep);
                        DataTable dtCredPay = command.selectCREDPAY_TRANS_PAY(rep);

                        string rescode2 = "";
                        string resmsg_en2 = "";
                        string resmsg_th2 = "";
                        string paymentID2 = "";
                        string invoiceNo2 = "";
                        string transID2 = dtCred.Rows[0]["Ref_CredPay"].ToString();
                        double paymentAmount2 = Convert.ToDouble(dtCred.Rows[0]["AMOUNT"]);
                        string paydate2 = dtCred.Rows[0]["TRANSACTION_DATE"].ToString();
                        string storeCode2 = dtCred.Rows[0]["STORE_CODE"].ToString();
                        string creditby2 = dtCred.Rows[0]["CASHIER_ID"].ToString();
                        string recepitNo2 = dtCred.Rows[0]["Ref_CredPay"].ToString();

                        int cnt = 0;
                        clsMMFSAPI.invoice_list[] invList2 = new clsMMFSAPI.invoice_list[dtCredDetial.Rows.Count];
                        foreach (DataRow dr in dtCredDetial.Rows)
                        {
                            invList2[cnt].invoice_no = dr["CRED_INVOICE_NO"].ToString();
                            invList2[cnt].invoice_amount = Convert.ToDouble(dr["CRED_AMOUNT"]);
                        }

                        clsMMFSAPI.payment_list[] pmList2 = new clsMMFSAPI.payment_list[dtCredPay.Rows.Count];
                        cnt = 0;
                        foreach (DataRow dr in dtCredPay.Rows)
                        {
                            pmList2[cnt].seq_no = (int)dr["SEQ"];
                            pmList2[cnt].payment_method = dr["PaymentMainCode"].ToString().Trim();
                            pmList2[cnt].apply_amount = Convert.ToDouble(dr["PAYMENT_AMOUNT"]) - Convert.ToDouble(dr["PAYMENT_CHANGE"]);
                            pmList2[cnt].payment_no = dr["PAYMENT_NUMBER"].ToString().Trim();
                            cnt++;
                        }

                        clsMMFSAPI sv2 = new clsMMFSAPI();

                        defaultDelay = 3;
                        defaultRetry = 5;
                        res = command.selectAPICONF_RETRY("payInvoiceAR");
                        if (res.response.next)
                        {
                            defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                            defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
                        }

                        int cnt3 = 0;
                    Retry2:
                        sv2.payInvoiceAR(ref rescode2, ref  resmsg_en2, ref  resmsg_th2, ref  transID2, ref paymentAmount2, ref  paydate2, ref  storeCode2, ref  creditby2, ref  recepitNo2, ref  pmList2, ref  invList2, ref  paymentID2);
                        if (rescode == "0000")
                        {
                            command.updateCreditPayTrans(recepitNo, false);
                            return new StoreResult(ResponseCode.Success, resmsg_th);
                        }
                        else if (rescode == "9999" && cnt3 <= defaultRetry)
                        {
                            cnt++;
                            Thread.Sleep(defaultDelay);
                            goto Retry2;
                        }
                        else
                        {
                            return new StoreResult(ResponseCode.Error, resmsg_th2);
                        }
                    }
                    else
                    {
                        return new StoreResult(ResponseCode.Error, resmsg_th);
                    }
                }
            }

            return new StoreResult(res.response, res.responseMessage);
        }
    }
}
