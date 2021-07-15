using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Data;
using BJCBCPOS_Model;
using System.Data;

namespace BJCBCPOS_Process
{
    /// <summary>
    /// MenuProcess class
    /// contains all process in main menu page
    /// </summary>
    public class MenuProcess
    {
        private SqlCommand command;

        public MenuProcess()
        {
            command = BaseProcess.command;
        }

        /// <summary>
        /// get menu from database
        /// </summary>
        /// <returns></returns>
        public StoreResult generateMenu()
        {
            try
            {
                return command.generateMenu(FunctionID.Login_DisplayMainMenu);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.generateMenu");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult cashireMessageStatus()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.Login_CashierMessage_Status);
                string sil = res.otherData.Rows[0]["silent"].ToString();
                if (sil.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return new ProcessResult(res, true);
                }
                else
                {
                    return new ProcessResult(res, false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult cashireMessageStatusOpenDayMenu()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.OpenDay_GetMessageCashier);
                string sil = res.otherData.Rows[0]["silent"].ToString();
                if (sil.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return new ProcessResult(res, true);
                }
                else
                {
                    return new ProcessResult(res, false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.cashireMessageStatusOpenDayMenu");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult cashireMessageStatusOpenEndOfShift()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.EndOfShift_GetMessageCashier);
                string sil = res.otherData.Rows[0]["silent"].ToString();
                if (sil.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return new ProcessResult(res, true);
                }
                else
                {
                    return new ProcessResult(res, false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.cashireMessageStatusOpenEndOfShift");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult cashireMessageStatusOpenEndOfTill()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.EndOfTill_GetMessageCashier);
                string sil = res.otherData.Rows[0]["silent"].ToString();
                if (sil.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return new ProcessResult(res, true);
                }
                else
                {
                    return new ProcessResult(res, false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.cashireMessageStatusOpenEndOfTill");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult checkOpenDayAuthorize()
        {
            try
            {
                // check profile
                Profile check = ProgramConfig.getProfile(FunctionID.OpenDay_SelectOpenDayMenu);
                if (check.profile == ProfileStatus.NotAuthorize) // profileStatus = 1
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
                AppLog.writeLog("connection to server lost at MenuProcess.checkOpenDayAuthorize");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult checkOpenDayofTill()
        {
            try
            {
                // get running
                StoreResult result = command.getRunning(FunctionID.OpenDay_GetRunningNo, RunningReceiptID.OpenDay);
                if (result.response.next)
                {
                    ProgramConfig.openDayRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    ProgramConfig.openDayRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();

                    Profile check = ProgramConfig.getProfile(FunctionID.OpenDay_CheckOpenDayofTillStatus);
                    if (check.policy == PolicyStatus.Work)
                    {
                        return command.checkOpenDay(FunctionID.OpenDay_CheckOpenDayofTillStatus);
                    }
                    return new StoreResult(ResponseCode.Success, "", "", "");
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.checkOpenDayofTill");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult openDay()
        {
            try
            {
                StoreResult result = null;
                Profile check = ProgramConfig.getProfile(FunctionID.OpenDay_DownLoadDatafromHeadOfficetoClient);
                if (check.policy == PolicyStatus.Work)
                {
                    result = command.downloadHOToPOS();
                    if (!result.response.next)
                    {
                        return new ProcessResult(result, false);
                    }
                }
                result = command.saveOpenDayTransaction();
                if (!result.response.next)
                {
                    return new ProcessResult(result, false);
                }
                else
                {
                    //check = ProgramConfig.getProfile(FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank);
                    //if (check.policy == PolicyStatus.Work)
                    //{
                    //    string ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    //    string rec = result.otherData.Rows[0]["Rec"].ToString();
                    //    result = command.syncToDataTank("OpenDay", FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank, ref_no, rec, ProgramConfig.abbNo);
                    //    if (!result.response.next)
                    //    {
                    //        return new ProcessResult(result.response, result.responseMessage, result.helpMessage, false);
                    //    }
                    //}
                    ProgramConfig.refNoOpenDay = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    ProgramConfig.recOpenDay = result.otherData.Rows[0]["Rec"].ToString();

                    check = ProgramConfig.getProfile(FunctionID.OpenDay_PrintOpenDayDocument);
                    if (check.policy == PolicyStatus.Work)
                    {
                        return new ProcessResult(result, true);
                    }
                    else
                    {
                        return new ProcessResult(result, false);
                    }
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.openDay");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult printOpenDay()
        {
            try
            {
                StoreResult result = command.printOpenDay();
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.printOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult saveToDataTankOpenDay()
        {
            try
            {
                StoreResult result = null;
                Profile check = ProgramConfig.getProfile(FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank);
                if (check.policy == PolicyStatus.Work)
                {
                    string ref_no = ProgramConfig.refNoOpenDay;
                    string rec = ProgramConfig.recOpenDay;
                    ProgramConfig.refNoOpenDay = "";
                    ProgramConfig.recOpenDay = "";
                    result = command.syncToDataTank("OpenDay", FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank, ref_no, rec, ProgramConfig.abbNo);
                    if (!result.response.next)
                    {
                        return new ProcessResult(result, false);
                    }
                    return new ProcessResult(result, true);
                }
                else
                {
                    return new ProcessResult(ResponseCode.Success, "Success", "N/A", false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.saveToDataTankOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult cashIn()
        {
            try
            {
                string responseMessage = ProgramConfig.message.get("MenuProcess", "SaveChangeComplete").message;
                string helpMessage = ProgramConfig.message.get("MenuProcess", "SaveChangeComplete").help;
                return new StoreResult(ResponseCode.Success, responseMessage, helpMessage, "");

                //return new StoreResult(ResponseCode.Success, "บันทึกรับเงินทอนเรียบร้อย", "", "");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.cashIn");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult beforeEndOfShift()
        {
            try
            {
                
                Profile check = ProgramConfig.getProfile(FunctionID.EndOfShift_CheckOpenDayofTillStatus);
                if (check.policy == PolicyStatus.Work)
                {
                    StoreResult result = command.checkOpenDay(FunctionID.EndOfShift_CheckOpenDayofTillStatus);
                    if (result.response.next)
                    {
                        result = command.getRunning(FunctionID.EndOfShift_GetRunningNo, RunningReceiptID.SignInOut);
                        if (result.response.next)
                        {
                            ProgramConfig.endOfShiftRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                            ProgramConfig.endOfShiftRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                        }
                        return result;
                    }
                    return result;
                }
                else
                {
                    StoreResult result = command.getRunning(FunctionID.EndOfShift_GetRunningNo, RunningReceiptID.SignInOut);
                    if (result.response.next)
                    {
                        ProgramConfig.endOfShiftRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                        ProgramConfig.endOfShiftRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                    }
                    return result;
                }

            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.beforeEndOfShift");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult endOfShift()
        {
            try
            {
                string rec = "";
                StoreResult result = command.saveEndOfShift();
                if (result.response.next)
                {
                    rec = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    string control = result.otherData.Rows[0]["CashierCtlID"].ToString();

                    //Profile check = ProgramConfig.getProfile(FunctionID.EndOfShift_SaveEndOfShiftTransaction_SynchSaleTransactiontoDataTank);
                    //if (check.policy == PolicyStatus.Work)
                    //{
                    //    result = command.syncToDataTank("CloseCashier", FunctionID.EndOfShift_SaveEndOfShiftTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.endOfShiftRefNo, rec, ProgramConfig.abbNo);
                    //}

                    Profile check = ProgramConfig.getProfile(FunctionID.EndOfShift_PrintEndOfShiftDocument);
                    if (check.policy == PolicyStatus.Work)
                    {
                        int control_id = 0;
                        int.TryParse(control, out control_id);
                        return new ProcessResult(result, true, control_id, rec);
                    }
                    else
                    {
                        return new ProcessResult(result, false, "", rec);
                    }
                }
                return new ProcessResult(result, false, "", rec);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.endOfShift");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public DataTable checkPassword(string newpass)
        {
            try
            {
                return command.checkPassword(newpass);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.checkPassword");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return null;
            }
        }

        public StoreResult printEndOfShift(int control_id)
        {
            try
            {
                StoreResult result = command.printEndOfShift(control_id);
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.printEndOfShift");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult endOfShiftLogout()
        {
            try
            {
                StoreResult result = command.saveLogout(FunctionID.EndOfShift_SaveLogoutTransaction);
                if (result.response.next)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.EndOfShift_SaveLogoutTransaction_SynchSaleTransactiontoDataTank);
                    if (check.policy == PolicyStatus.Work)
                    {
                        string ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                        string rec = result.otherData.Rows[0]["Rec"].ToString();
                        result = command.syncToDataTank("Logout", FunctionID.EndOfShift_SaveLogoutTransaction_SynchSaleTransactiontoDataTank, ref_no, rec, ref_no);
                    }
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.endOfShiftLogout");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult checkEndOfTillAuthorize()
        {
            try
            {
                // check profile
                Profile check = ProgramConfig.getProfile(FunctionID.EndOfTill_SelectEndOfTillMenu);
                if (check.profile == ProfileStatus.NotAuthorize) // profileStatus = 1
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
                AppLog.writeLog("connection to server lost at MenuProcess.checkEndOfTillAuthorize");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult endOfTillGetRunning()
        {
            try
            {
                // get running
                StoreResult result = command.getRunning(FunctionID.EndOfTill_GetRunningNo, RunningReceiptID.SignInOut);
                if (result.response.next)
                {
                    ProgramConfig.endOfTillRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    ProgramConfig.endOfTillRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.endOfTillCheckOpendDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult endOfTillOpenDay()
        {
            try
            {
                // get running
                StoreResult result = command.checkOpenDay(FunctionID.EndOfTill_CheckOpenDayofTillStatus);
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.endOfTillOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult endOfTill()   
        {
            try
            {
                string ref_no = "";
                StoreResult result = command.saveEndOfTill();
                if (result.response.next)
                {
                    //Profile check = ProgramConfig.getProfile(FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank);
                    //if (check.policy == PolicyStatus.Work)
                    //{
                    ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    //    result = command.syncToDataTank("CloseDay", FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.endOfTillRefNo, ref_no, ProgramConfig.abbNo);
                    //}

                    Profile check = ProgramConfig.getProfile(FunctionID.EndOfTill_PrintEndOfTillDocument);
                    if (check.policy == PolicyStatus.Work)
                    {
                        return new ProcessResult(result, true, data: ref_no);
                    }
                    else
                    {
                        return new ProcessResult(result, false, data: ref_no);
                    }
                }
                return new ProcessResult(result, false, data: ref_no);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.endOfTill");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public void syncToDataTank(string events, FunctionID functionID, string ref_no2,string rec)
        {
            Profile check = ProgramConfig.getProfile(functionID);
            if (check.policy == PolicyStatus.Work)
            {
                command.syncToDataTank(events, functionID, ref_no2, rec, ProgramConfig.abbNo);
            }
        }

        public StoreResult printEndOfTill()
        {
            try
            {
                StoreResult result = command.printEndOfTill();
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.printEndOfTill");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult endOfTillLogout()
        {
            try
            {
                StoreResult result = command.saveLogout(FunctionID.EndOfTill_SaveLogoutTransaction);
                if (result.response.next)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.EndOfTill_SaveLogoutTransaction_SynchSaleTransactiontoDataTank);
                    if (check.policy == PolicyStatus.Work)
                    {
                        string ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                        string rec = result.otherData.Rows[0]["Rec"].ToString();
                        result = command.syncToDataTank("Logout", FunctionID.EndOfTill_SaveLogoutTransaction_SynchSaleTransactiontoDataTank, ref_no, rec, ref_no);
                    }
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.endOfTillLogout");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveChangePassword(string userId, string newPassword)
        {
            try
            {
                return command.saveChangePassword(userId, newPassword, FunctionID.Login_ChangePassword_SaveTransactionChangePassword);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.saveChangePassword");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveChangePasswordAutoLogout()
        {
            try
            {
                StoreResult result = command.saveLogout(FunctionID.Login_ChangePassword_SaveLogoutTransaction);
                if (result.response.next)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.Login_ChangePassword_SaveLogoutTransaction_SynchSaleTransactiontoDataTank);
                    if (check.policy == PolicyStatus.Work)
                    {
                        string ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                        string rec = result.otherData.Rows[0]["Rec"].ToString();
                        result = command.syncToDataTank("Logout", FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank, ref_no, rec, ref_no);
                    }
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.saveChangePasswordAutoLogout");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveLogout()
        {
            try
            {
                StoreResult result = command.saveLogout(FunctionID.Logout_SaveLogoutTransaction);
                if (result.response.next)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.Logout_SaveLogoutTransaction_SynchSaleTransactiontoDataTank);
                    if (check.policy == PolicyStatus.Work)
                    {
                        string ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                        string rec = result.otherData.Rows[0]["Rec"].ToString();
                        result = command.syncToDataTank("Logout", FunctionID.Logout_SaveLogoutTransaction_SynchSaleTransactiontoDataTank, ref_no, rec, ref_no);
                    }
                }
                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at MenuProcess.saveLogout");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

    }
}
