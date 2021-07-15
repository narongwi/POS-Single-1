using System;
using BJCBCPOS_Model;
using BJCBCPOS_Data;
using System.Collections.Generic;
using System.Data;

namespace BJCBCPOS_Process
{
    public class CashOutProcess
    {
        private SqlCommand command;

        public CashOutProcess()
        {
            command = BaseProcess.command;
        }

        public void newTransaction()
        {
            command.newTransaction();
        }

        public void commit()
        {
            command.commit();
        }

        public void rollback()
        {
            command.rollback();
        }

        public StoreResult checkOpenDay()
        {
            try
            {
                return command.checkOpenDay(FunctionID.CashOut_CheckOpenDayofTillStatus);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult posDisplayContent()
        {
            try
            {
                return command.posDisplayContent();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.posDisplayContent");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public ProcessResult checkAuthorize()
        {
            try
            {
                Profile check = ProgramConfig.getProfile(FunctionID.CashOut_SelectCashOutMenu);
                if (!check.found || check.profile == ProfileStatus.NotAuthorize)
                {
                    return new ProcessResult(ResponseCode.Success, "", "", true);
                }
                else
                {
                    return new ProcessResult(ResponseCode.Success, "", "", false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.checkAuthorize");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult getRunning()
        {
            try
            {
                StoreResult res = command.getRunning(FunctionID.CashOut_GetRunningNo, RunningReceiptID.CashInOut);
                if (res.response.next)
                {
                    ProgramConfig.cashOutRefNo = res.otherData.Rows[0]["ReferenceNo"].ToString();
                    ProgramConfig.cashOutRefNoIni = res.otherData.Rows[0]["ReferenceNoINI"].ToString();

                    Profile check = ProgramConfig.getProfile(FunctionID.CashOut_CheckOpenDayofTillStatus);
                    if (!check.found || check.policy == PolicyStatus.Work)
                    {
                        res = command.checkOpenDay(FunctionID.CashOut_CheckOpenDayofTillStatus);
                    }
                    if (res.response.next)
                    {
                        check = ProgramConfig.getProfile(FunctionID.CashOut_CheckUserIDPasswordCurrentCashier);
                        if (!check.found || check.policy == PolicyStatus.Work)
                        {
                            return new ProcessResult(res, true);
                        }
                    }
                }
                return new ProcessResult(res, false);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.getRunning");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult cashireMessageStatus()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.CashOut_CashierMessage_Status);
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
                AppLog.writeLog("connection to server lost at CashOutProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult getMenu()
        {
            try
            {
                return command.generateMenu(FunctionID.CashOut_DisplayCashOutMenu);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.getMenu");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult checkLastLoginSale()
        {
            try
            {
                return command.checkLastLoginSales();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.checkLastLoginSale");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult posCheckCashIn()
        {
            try
            {
                return command.posCheckCashIn();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.posCheckCashIn");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public DataTable selectPaymentByCode(string paymentCode)
        {
            try
            {
                return command.selectPaymentByCode(paymentCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.selectPaymentByCode");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return null;
            }
        }

        public StoreResult summarySale()
        {
            try
            {
                return command.summaryCashoutAuto();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.summarySale");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public bool saveOpenDrawer(string openTime)
        {
            try
            {
                return command.saveOpenDrawerCashout(openTime);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.saveOpenDrawer");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool saveCashoutSale(DataTable data, string openTime, string closeTime, double totalAmt, int modeSave, List<banknote> banknote = null)
        {
            try
            {
                StoreResult res;
                command.newTransaction();
                // save dlyptrans
                if (command.updateDlyptrans("V", ProgramConfig.cashOutRefNo, "2500"))
                {
                    int rec = 0;
                    if (modeSave == 0)
                    {
                        rec = command.getMaxRecDlyptrans(ProgramConfig.cashOutRefNo, "I", "O");
                    }
                    else if (modeSave == 2)
                    {
                        rec = command.getMaxRecDlyptrans(ProgramConfig.cashOutRefNo, "C", "P");
                    }
                    
                    if (rec >= 0)
                    {
                        float amt;
                        foreach (DataRow row in data.Rows)
                        {
                            rec++;
                            if (float.TryParse(row["Amt"].ToString(), out amt))
                            {
                                string pcd = row["PaymentCode"] + " " + row["CurrencyCode"] + " " + row["ExchangeRate"].ToString().Replace("-", "");
                                if (pcd.Length > 20)
                                {
                                    pcd = pcd.Trim().Substring(0, 19);
                                }

                                if (modeSave == 0)
                                {
                                    res = command.insertDlyptrans(ProgramConfig.cashOutRefNo, rec, "I", "O", pcd, row["MoneyBag"].ToString(), amt.ToString(), 0f, 0f, row["InputAmt"].ToString());
                                    if (!res.response.next)
                                    {
                                        string responseMessage = ProgramConfig.message.get("CashOutProcess", "SaveDLYPTRANSIncomplete").message;
                                        string helpMessage = ProgramConfig.message.get("CashOutProcess", "SaveDLYPTRANSIncomplete").help;
                                        throw new Exception(responseMessage);

                                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน DLYPTRANS");
                                    }
                                }
                                else if (modeSave == 2)
                                {
                                    res = command.insertDlyptrans(ProgramConfig.cashOutRefNo, rec, "C", "P", pcd, row["MoneyBag"].ToString(), amt.ToString(), 0f, 0f, row["InputAmt"].ToString());
                                    if (!res.response.next)
                                    {
                                        string responseMessage = ProgramConfig.message.get("CashOutProcess", "SaveDLYPTRANSIncomplete").message;
                                        string helpMessage = ProgramConfig.message.get("CashOutProcess", "SaveDLYPTRANSIncomplete").help;
                                        throw new Exception(responseMessage);
                                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน DLYPTRANS");
                                    }
                                }
                                
                            }
                        }

                        if (modeSave == 0)
                        {
                            // save drawer log
                            if (command.saveCloseDrawerCashoutBalance(openTime, closeTime)) //เงินทอน
                            {
                                int cashireId = command.getCashierID();
                                if (cashireId > 0)
                                {
                                    if (command.insertCashireLog(cashireId, totalAmt))
                                    {
                                        if (command.deleteBanknoteCashout(cashireId.ToString()))
                                        {
                                            // insert banknote
                                            if (banknote != null && banknote.Count > 0)
                                            {
                                                foreach (banknote item in banknote)
                                                {
                                                    if (!command.insertBanknoteCashout(cashireId.ToString(), item.bankValue, item.bankCount, item.totalValue, ProgramConfig.cashOutRefNo))
                                                    {
                                                        string responseMessage = ProgramConfig.message.get("CashOutProcess", "SaveCASHCOUNTIncomplete").message;
                                                        string helpMessage = ProgramConfig.message.get("CashOutProcess", "SaveCASHCOUNTIncomplete").help;
                                                        throw new Exception(responseMessage);

                                                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน CASHCOUNT");
                                                    }
                                                }
                                            }
                                        }
                                        //command.commit();
                                        //return true;
                                    }
                                }
                            }
                        }
                        else if (modeSave == 2)
                        {
                            // save drawer log
                            if (command.saveCloseDrawerCashout(openTime, closeTime))
                            {
                                int cashireId = command.getCashierID();
                                if (cashireId > 0)
                                {
                                    if (command.insertCashireLog(cashireId, totalAmt))
                                    {
                                        if (command.deleteBanknoteCashout(cashireId.ToString()))
                                        {
                                            // insert banknote
                                            if (banknote != null && banknote.Count > 0)
                                            {
                                                foreach (banknote item in banknote)
                                                {
                                                    if (!command.insertBanknoteCashout(cashireId.ToString(), item.bankValue, item.bankCount, item.totalValue, ProgramConfig.cashOutRefNo))
                                                    {
                                                        string responseMessage = ProgramConfig.message.get("CashOutProcess", "SaveCASHCOUNTIncomplete").message;
                                                        string helpMessage = ProgramConfig.message.get("CashOutProcess", "SaveCASHCOUNTIncomplete").help;
                                                        throw new Exception(responseMessage);

                                                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน CASHCOUNT");
                                                    }
                                                }
                                            }
                                        }
                                        //command.commit();
                                        //return true;
                                    }
                                }
                            }
                        }

                        Profile check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_PrintCashoutDocument);
                        if (!check.found || check.policy == PolicyStatus.Work)
                        {
                            if (modeSave == 0)
                            {
                                res = command.printCashOutFloat();
                                if (res.response.next)
                                {
                                    Hardware.printTermal(res.otherData);
                                }
                                else
                                {
                                    //frmLoading.closeLoading();
                                    //notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                                    //notify.ShowDialog(this);
                                    string responseMessage = res.responseMessage;
                                    string helpMessage = res.helpMessage;
                                    throw new Exception(responseMessage);
                                    return false;
                                }
                            }
                            else if (modeSave == 2)
                            {
                                res = command.printCashOut();
                                if (res.response.next)
                                {
                                    Hardware.printTermal(res.otherData);
                                }
                                else
                                {
                                    //frmLoading.closeLoading();
                                    //notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                                    //notify.ShowDialog(this);
                                    string responseMessage = res.responseMessage;
                                    string helpMessage = res.helpMessage;
                                    throw new Exception(responseMessage);
                                    return false;
                                }
                            }
                        }
                        command.commit();
                        return true;
                    }
                }

                throw new Exception(ProgramConfig.message.get("CashOutProcess", "SaveDLYPTRANSIncomplete").message);

                //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน DLYPTRANS");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.saveCashoutSale");
                throw;
            }
            catch (Exception ex)
            {
                command.rollback();
                AppLog.writeLog(ex);
                return false;
            }
        }

        public ProcessResult submitCashout(int typeChange)
        {
            try
            {
                string responseMessage = ProgramConfig.message.get("CashOutProcess", "SaveComplete").message;
                string helpMessage = ProgramConfig.message.get("CashOutProcess", "SaveComplete").help;
                StoreResult res = new StoreResult(ResponseCode.Success, responseMessage, helpMessage, "", "");

                Profile check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_SaveCashOutTransaction_SynchSaleTransactiontoDataTank);
                if (!check.found || check.policy == PolicyStatus.Work)
                {
                    check = ProgramConfig.getProfile(FunctionID.CashOut_NormalChange_SaveCashOutTransaction_SynchSaleTransactiontoDataTank);
                    if (!check.found || check.policy == PolicyStatus.Work)
                    {
                        res = command.syncToDataTank("CashOut", FunctionID.CashOut_NormalChange_SaveCashOutTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.cashOutRefNo, "", "");
                        if (res.response.next)
                        {
                            return new ProcessResult(res, true);
                        }
                    }
                }
                else if (typeChange == 2) //เงินขาย
                {
                    check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_SaveCashOutTransaction_SynchSaleTransactiontoDataTank);
                    if (!check.found || check.policy == PolicyStatus.Work)
                    {
                        res = command.syncToDataTank("CashOut", FunctionID.CashOut_Sale_SaveCashOutTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.cashOutRefNo, "", "");
                        if (res.response.next)
                        {
                            return new ProcessResult(res, true);
                        }
                    }
                }

                //check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_PrintCashoutDocument);
                //if (!check.found || check.policy == PolicyStatus.Work)
                //{
                //    return new ProcessResult(res.response, res.responseMessage, res.helpMessage, true);
                //}

                return new ProcessResult(res, false);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.submitCashout");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult printCashout()
        {
            try
            {
                return command.printCashOut();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.printCashout");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult printCashOutNormalChange()
        {
            try
            {
                return command.printCashOutFloat();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.printCashOutNormalChange");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult getCashUnit(string currencyCode)
        {
            try
            {
                return command.getCashUnit(currencyCode, ProgramConfig.cashOutRefNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.getCashUnit");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMoneyBag(string bagNo)
        {
            try
            {
                return command.checkMoneyBag(bagNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.checkMoneyBag");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMinCashUnitAmount(string paymentCode, string payAmt, string currencyCode)
        {
            try
            {
                return command.checkMinCashUnitAmount(paymentCode, payAmt, ProgramConfig.cashOutRefNo, currencyCode, "1");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.checkMinCashUnitAmount");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayCashOut()
        {
            try
            {
                return command.displayCashOut();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.displayCashOut");
                throw;
            }           
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateStatusCancelCashOut(string refNo)
        {
            try
            {
                return command.UpdateStatusCancelCashOut(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.updateStatusCancelCashOut");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayCashOutDetail(string refNo, string currencyCode)
        {
            try
            {
                return command.displayCashOutDetail(refNo, currencyCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.displayCashOutDetail");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getAmountExchangeRate(string saleAmt, string mode, string pmCode, string pmMainCode)
        {
            try
            {
                return command.getAmountExchangeRate(saleAmt, mode, pmCode, pmMainCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.getAmountExchangeRate");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult generateMenu()
        {
            try
            {
                return command.generateMenu(FunctionID.CashOut_DisplayCashOutMenu);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.generateMenu");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public double CalAmount(double balance, string pmCode, string pmSubCode)
        {
            double fxRate = command.GetFXCU_RATE(pmCode, pmSubCode);
            if (fxRate <= 0)
            {
                return balance;
            }
            else
            {
                return Math.Round((balance / fxRate), 2);
            }
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
                        CashinoutInternalRef = {3}, LoginInternalRef = {4}, ExportPermitRef = {5}, OpendayInternalRef = {6}, PosRepRef = {7}, actionRefNo = {8}, HoldRef = {9}, TempFFTIRef = {10}"
                                                    , dt.Rows[0]["SaleInternalRef"].ToString()
                                                    , dt.Rows[0]["VoidInternalRef"].ToString()
                                                    , dt.Rows[0]["RetnInternalRef"].ToString()
                                                    , dt.Rows[0]["CashinoutInternalRef"].ToString()
                                                    , dt.Rows[0]["LoginInternalRef"].ToString()
                                                    , dt.Rows[0]["ExportPermitRef"].ToString()
                                                    , dt.Rows[0]["OpendayInternalRef"].ToString()
                                                    , dt.Rows[0]["PosRepRef"].ToString()
                                                    , dt.Rows[0]["DelEditItemInternalRef"].ToString()
                                                    , dt.Rows[0]["HoldRef"].ToString()
                                                    , dt.Rows[0]["TempFFTIRef"].ToString()));

                    }
                }
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashOutProcess.CheckRunningNumber");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
            
        }
    }
}
