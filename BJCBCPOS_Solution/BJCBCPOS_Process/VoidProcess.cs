using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Data;
using BJCBCPOS_Model;
using System.Data;
using MMFSAPI;
using System.Threading;

namespace BJCBCPOS_Process
{
    /// <summary>
    /// VoidProcess
    /// contains all process about sale
    /// </summary>
    public class VoidProcess
    {
        private SqlCommand command;

        public VoidProcess()
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

        public StoreResult checkOpenDay(FunctionID functionId)
        {
            try
            {
                return command.checkOpenDay(functionId);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkOpenDay()
        {
            try
            {
                return command.checkOpenDay(FunctionID.Void_CheckOpenDayofTillStatus);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.checkOpenDay");
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
                AppLog.writeLog("connection to server lost at VoidProcess.posDisplayContent");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayReceiptDetail(FunctionID functionID, string receiptNo, string saleType)
        {
            try
            {
                return command.displayReceiptDetail(functionID, receiptNo, ProgramConfig.tillNo, "N/A", saleType);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.displayReceiptDetail");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getRunning(FunctionID functionID, RunningReceiptID receiptID)
        {
            try
            {
                return command.getRunning(functionID, receiptID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.getRunning");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayReason(FunctionID functionID)
        {
            try
            {
                return command.displayReason(functionID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.displayReason");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkReceipt(string receiptNo)
        {
            try
            {
                return command.checkReceipt(receiptNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.checkReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveTempDlyptrans(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
            , string usr, string egp, string stt, string pdisc, string discid, string upc, string dty, string discamt, string reason, string stv)
        {
            try
            {
                if (command.saveTempDlyptrans(refNo, rec, sty, vty, pcd, qnt, amt, fds, usr, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                {
                    return new StoreResult(ResponseCode.Success, "", "", "");
                }
                return new StoreResult(ResponseCode.Error, "", "", "");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.saveTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveVoidTransaction(string receiptNo, string openDrawer, string reason, string saleType)
        {
            try
            {
                if (saleType == VoidSaleType.Deposit)
                {
                    return command.VoidDeposit(receiptNo, ProgramConfig.voidRefNo, FunctionID.Void_SaveVoidDepositTransaction);
                }
                else if (saleType == VoidSaleType.POD)
                {
                    return command.PODVoidReceipt(receiptNo, ProgramConfig.voidRefNo, openDrawer, reason);
                }
                else if (saleType == VoidSaleType.CreditSale)
                {
                    return command.CredPayVoidReceipt(receiptNo, ProgramConfig.voidRefNo, openDrawer, reason);
                }
                else
                {
                    return command.saveVoidTransaction(receiptNo, openDrawer, reason, ProgramConfig.voidRefNo, FunctionID.Void_SaveVoidTransaction);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.saveVoidTransaction");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult concludeVoid(string receiptNo)
        {
            try
            {
                return command.concludeVoid(receiptNo, ProgramConfig.voidRefNo, FunctionID.Void_ProcessAfterVoidTransaction);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.concludeVoid");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult syncToDataTank(string eventName, FunctionID functionId, string referenceNo, string rec, string abbNo)
        {
            try
            {
                return command.syncToDataTank(eventName, functionId, referenceNo, rec, abbNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.syncToDataTank");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult PrintVoidReceipt(string voidReceiptNo)
        {
            try
            {
                return command.PrintVoidReceipt(voidReceiptNo, ProgramConfig.voidRefNo, FunctionID.Void_PrintVoidDocument);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.PrintVoidReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveCloseDrawer(FunctionID functionId, string closeTime, string number)
        {
            try
            {
                return command.saveCloseDrawer(functionId, closeTime, number);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.saveCloseDrawer");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getProductDesc(string productCode)
        {
            try
            {
                return command.getProductDesc(FunctionID.Sale_InputSaleItem_InputProduct_NormalSale, productCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.getProductDesc");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public ProcessResult cashireMessageStatus()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.Void_GetMessageCashier);
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
                AppLog.writeLog("connection to server lost at VoidProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public delegate bool AlertMessage(ResponseCode resCode, string resMsg, string resHelp = "");

        public StoreResult processCheckQRPayment(string refNo, AlertMessage AlertMessage = null)
        {
            try
            {
                StoreResult res = command.selectQRPAYTRANS(refNo);
                if (res.otherData.Rows.Count == 0)
                {
                    return res;
                }
                else
                {
                    DataRow dr = res.otherData.Rows[0];
                    string org_tranID = dr["ORG_TRANID"].ToString();
                    string pay_amount = dr["PAY_AMOUNT"].ToString();

                    var dt = command.selectParameterForQR(refNo, "RQ", "BC", "VP");
                    if (dt.Rows.Count > 0)
                    {
                        string prn_TxnID = dt.Rows[0]["TranID"].ToString();
                        string seq = dt.Rows[0]["Seq"].ToString();

                        res = command.API_POS_BSC_VOID(refNo, prn_TxnID, org_tranID, pay_amount);
                        if (res.response.next)
                        {
                            res = command.saveQRPayTransOnline(refNo, "VD", seq, prn_TxnID, dr["ACCOUNT_NAME"].ToString(), dr["QR_CODE"].ToString(), pay_amount
                                 , "", "", "BC", dr["BANKCODE"].ToString(), dr["TOKEN_ID"].ToString(), dr["OTA"].ToString(), dr["BSD"].ToString(), dr["TEPA_CODE"].ToString(),
                                 dr["Sending_Bank"].ToString(), dr["Receving_Bank"].ToString(), dr["REF2"].ToString(), dr["TRANSREF"].ToString(),
                                 trn_status: dr["TXN_Status"].ToString(),
                                 stt: "A",
                                 onlineFlag: dr["ONLINE_FLAG"].ToString());
                        }
                        else if (res.response == ResponseCode.Warning)
                        {
                            var resDialog = AlertMessage(res.response, res.responseMessage);
                            if (resDialog)
                            {
                                var dt2 = command.selectParameterForQR(refNo, "RQ", "BC", "IV");
                                if (dt2.Rows.Count > 0)
                                {
                                    string prn_TxnID2 = res.otherData.Rows[0]["TranID"].ToString();

                                    res = command.API_POS_BSC_INQUIRY_VOID_STATUS(prn_TxnID2, org_tranID, pay_amount);
                                    if (res.response.next)
                                    {
                                        res = command.saveQRPayTransOnline(refNo, "VD", seq, prn_TxnID, dr["ACCOUNT_NAME"].ToString(), dr["QR_CODE"].ToString(), pay_amount
                                                 , "", "", "BC", dr["BANKCODE"].ToString(), dr["TOKEN_ID"].ToString(), dr["OTA"].ToString(), dr["BSD"].ToString(), dr["TEPA_CODE"].ToString(),
                                                 dr["Sending_Bank"].ToString(), dr["Receving_Bank"].ToString(), dr["REF2"].ToString(), dr["TRANSREF"].ToString(),
                                                 trn_status: dr["TXN_Status"].ToString(),
                                                 stt: "A",
                                                 onlineFlag: dr["ONLINE_FLAG"].ToString(),
                                                 tranID: prn_TxnID2);
                                    }
                                }
                            }
                            else
                            {
                                return new StoreResult(ResponseCode.Ignore, "");
                            }
                        }
                    }
                }

                if (!res.response.next)
                {
                    AlertMessage(res.response, res.responseMessage);
                }

                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.processCheckQRPayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printCN(string cnno)
        {
            try
            {
                return command.printCN(cnno);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.printCN");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult PODPrintVoid(string refNo)
        {
            try
            {
                return command.PODPrintVoid(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.PrintVoidReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CallAPICancelPayInvoicePOD(string refNo)
        {
            DataTable dt = command.selectPODTRANS(refNo);

            if (dt.Rows.Count > 0)
            {
                clsMMFSAPI sv = new clsMMFSAPI();
                string rescode = "";
                string resmsg_en = "";
                string resmsg_th = "";
                string paymentID = dt.Rows[0]["Payment_ID"].ToString();
                string transID = "";
                string storeCode = dt.Rows[0]["STCODE"].ToString();
                string createby = dt.Rows[0]["CASHIER_ID"].ToString();
                string recepitNo = "";

                int defaultDelay = 3;
                int defaultRetry = 5;
                var res = selectAPICONF_RETRY("CancelPayInvoicePOD");
                if (res.response.next)
                {
                    defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                    defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
                }

                res = getRunning(FunctionID.ReceivePOD_GetRunningCallAPI, RunningReceiptID.PODAPI);
                transID = res.otherData.Rows[0]["ReferenceNo"].ToString();

                int cnt = 0;
            Retry:
                sv.CancelPayInvoicePOD(ref rescode, ref resmsg_en, ref resmsg_th, ref paymentID, ref transID, ref storeCode, ref createby);

                if (rescode == "0000")
                {
                    command.updatePOD_TRANS(refNo, true, transIDVoid: transID, paymentIDVoid: paymentID);
                    return new StoreResult(ResponseCode.Success, resmsg_th);
                }
                else if (rescode == "9999" && cnt <= defaultRetry)
                {
                    cnt++;
                    Thread.Sleep(defaultDelay);
                    goto Retry;
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, resmsg_th);
                }
            }
            else
            {
                return new StoreResult(ResponseCode.Error, "ไม่พบข้อมูลการยกเลิก POD", help: "ไม่พบข้อมูลที่ PODTRANS");
            }
        }

        public StoreResult selectAPICONF_RETRY(string apiName)
        {
            try
            {
                return command.selectAPICONF_RETRY(apiName);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectAPICONF_RETRY");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CallAPICancelPayInvoiceAR(string refNo)
        {
            DataTable dt = command.selectCREDPAY_TRANS(refNo);

            if (dt.Rows.Count > 0)
            {
                clsMMFSAPI sv = new clsMMFSAPI();
                string rescode = "";
                string resmsg_en = "";
                string resmsg_th = "";
                string paymentID = dt.Rows[0]["Payment_ID"].ToString();
                string transID = "";
                string storeCode = dt.Rows[0]["STORE_CODE"].ToString();
                string createby = dt.Rows[0]["CASHIER_ID"].ToString();
                string recepitNo = "";

                int defaultDelay = 3;
                int defaultRetry = 5;
                var res = selectAPICONF_RETRY("CancelPayInvoiceAR");
                if (res.response.next)
                {
                    defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                    defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
                }

                res = getRunning(FunctionID.CreditSale_GetRunningCallAPI, RunningReceiptID.PODAPI);
                transID = res.otherData.Rows[0]["ReferenceNo"].ToString();

                int cnt = 0;
            Retry:
                sv.CancelPayInvoiceAR(ref rescode, ref resmsg_en, ref resmsg_th, ref paymentID, ref transID, ref storeCode, ref createby);

                if (rescode == "0000")
                {
                    command.updateCreditPayTrans(refNo, true, transIDVoid: transID, paymentIDVoid: paymentID);
                    return new StoreResult(ResponseCode.Success, resmsg_th);
                }
                else if (rescode == "9999" && cnt <= defaultRetry)
                {
                    cnt++;
                    Thread.Sleep(defaultDelay);
                    goto Retry;
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, resmsg_th);
                }
            }
            else
            {
                return new StoreResult(ResponseCode.Error, "ไม่พบข้อมูลการยกเลิก POD", help: "ไม่พบข้อมูลที่ PODTRANS");
            }
        }

        public StoreResult CredPayPrintVoid(string refNo)
        {
            try
            {
                return command.CredPayPrintVoid(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.PrintVoidReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult selectDLYPTRANS(string refNo, string vty = "", string dty = "")
        {
            try
            {
                var res = command.selectDLYPTRANS(refNo, vty, dty);
                if (res.otherData.Rows.Count > 0)
                {
                    string cardNo = res.otherData.Rows[0]["PCD"].ToString();

                    res = command.selectEDCTrans(refNo, cardNo);
                    if (res.otherData.Rows.Count > 0)
                    {
                        return new StoreResult(ResponseCode.Success, data: res.otherData);
                    }
                }
                return new StoreResult(ResponseCode.Error, "");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.PrintVoidReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult clearUp(FunctionID functionId, string voidRefNo = "")
        {
            try
            {
                return command.clearUp(functionId, voidRefNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.PrintVoidReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult SaveDrawerTrans(FunctionID function)
        {
            try
            {
                return command.saveDrawerTrans(ProgramConfig.voidRefNo, function);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }
    }
}
