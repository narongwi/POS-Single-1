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
    /// ReturnProcess
    /// contains all process about sale
    /// </summary>
    public class ReturnProcess
    {
        private SqlCommand command;
        private DataTable _dtReturnTEMP;

        public ReturnProcess()
        {
            command = BaseProcess.command;
            _dtReturnTEMP = BaseProcess.dtReturnTEMP;
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
                AppLog.writeLog("connection to server lost at ReturnProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
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
                AppLog.writeLog("connection to server lost at ReturnProcess.posDisplayContent");
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
                AppLog.writeLog("connection to server lost at ReturnProcess.getRunning");
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
                return command.getProductDesc(FunctionID.Return_InputReturnItem_ByProduct_InputProduct_InputReturnProductCode, productCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.getProductDesc");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayReason()
        {
            try
            {
                return command.displayReason(FunctionID.Return_InputReturnReason);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.displayReason");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult searchMember(int searchType, string data)
        {
            try
            {
                return command.searchMember(FunctionID.Return_InputCustomer_ByMember, searchType, data);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.searchMember");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult searchReceipt(int searchType, string data)
        {
            try
            {
                return command.searchReceipt(searchType, data, ProgramConfig.returnRefNo, ProgramConfig.tillNo, FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo, "N/A");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.searchReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayReceiptDetail(string receiptNo)
        {
            try
            {
                return command.displayReceiptDetail(FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo, receiptNo, ProgramConfig.tillNo, "N/A");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.displayReceiptDetail");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayReturnPayment(string returnType, string saleRef, double saleAmt, double returnAmt)
        {
            try
            {
                return command.displayReturnPayment(returnType, saleRef, saleAmt, returnAmt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.displayReturnPayment");
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
                AppLog.writeLog("connection to server lost at ReturnProcess.saveTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataTable selectMaxRecReturnTempDlyptrans()
        {
            try
            {
                return command.selectMaxRecReturnTempDlyptrans();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.selectMaxRecReturnTempDlyptrans");
                throw;
            }
        }

        public StoreResult updateNewAmtQtyTempDlyptrans(string refNo, string rec, string newAmt, string newQty)
        {
            try
            {
                return command.updateNewAmtQtyTempDlyptrans(refNo, rec, newAmt, newQty);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.updateNewAmtQtyTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataTable selectReturnItemTempDlyptrans(string refNo, string code, string quant, string price)
        {
            try
            {
                return command.selectReturnItemTempDlyptrans(refNo, code, quant, price);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.selectReturnItemTempDlyptrans");
                throw;
            }
        }

        public DataTable selectReturnVatItemTempDlyptrans()
        {
            try
            {
                return command.selectReturnVatItemTempDlyptrans();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.selectReturnVatItemTempDlyptrans");
                throw;
            }
        }

        public DataTable loadReturnTempDlyptrans(string refNo)
        {
            try
            {
                return command.loadReturnTempDlyptrans(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.loadReturnTempDlyptrans");
                throw;
            }
        }

        public StoreResult checkOpenDay()
        {
            try
            {
                return command.checkOpenDay(FunctionID.Return_CheckOpenDayofTillStatus);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveReturnTransaction(string saleRef)
        {
            try
            {
                return command.saveReturnTransaction(saleRef);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.saveReturnTransaction");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult saveCloseDrawer(string closeTime, string number)
        {
            try
            {
                return command.saveCloseDrawer(FunctionID.Return_CloseDrawerAndRecordTime,  closeTime,  number);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.saveCloseDrawer");
                throw;
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
                return command.updateReturnVatTempDlyptrans(dateTimeDaTa);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.updateReturnVatTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult concludeReturn(string cnNo)
        {
            try
            {
                return command.concludeReturn(cnNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.concludeReturn");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult syncToDataTank(string eventName, FunctionID functionId, string referenceNo, string rec, string cnNo)
        {
            try
            {
                return command.syncToDataTank(eventName, functionId, referenceNo, rec, cnNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.syncToDataTank");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printCN(string cnNo)
        {
            try
            {
                return command.printCN(cnNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.printCN");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printCopyCN(string cnNo)
        {
            try
            {
                return command.printCopyCN(cnNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.printCopyCN");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getMemberProfile(string memberID)
        {
            try
            {
                return command.getMemberProfile(FunctionID.Return_InputCustomer_Display, memberID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.getMemberProfile");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getCustomerFFTI(string taxId)
        {
            try
            {
                return command.getCustomerFFTI(taxId);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.getCustomerFFTI");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult clearPDISCTempDlyptrans()
        {
            try
            {
                return command.clearPDISCTempDlyptrans();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.clearPDISCTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getPosConfig()
        {
            try
            {
                return command.getPosConfig();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.getPosConfig");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveTempReturn(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
            , string usr, string egp, string stt, string stv, string reason, string pdisc, string discid, string discamt, string upc, string dty)
        {
            try
            {
                return command.saveTempReturn(refNo, rec, sty, vty, pcd, qnt, amt, fds, usr, egp, stt, stv, reason, pdisc, discid, discamt, upc, dty);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.saveTempReturn");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateSuperuserIdTempF()
        {
            try
            {
                return command.updateSuperuserIdTempF(ProgramConfig.returnRefNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.updateSuperuserIdTempF");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updatePrintTempDlyptrans()
        {
            try
            {
                return command.updatePrintTempDlyptrans(ProgramConfig.returnRefNo, "1");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.updatePrintTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateReturnFSlot(string refNo)
        {
            try
            {
                return command.updateReturnFSlot(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.updateReturnFSlot");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateOpenCashDrawer(string vtime)
        {
            try
            {
                return command.updateOpenCashDrawer(vtime, ProgramConfig.returnRefNo, "3");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReturnProcess.updateOpenCashDrawer");
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
                StoreResult res = command.getMessageCashierStatus(FunctionID.Return_GetMessageCashier);
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
                AppLog.writeLog("connection to server lost at ReturnProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult selectDLYPTRANS(string refNo, string sty = "", string vty = "")
        {
            try
            {
                var res = command.selectDLYPTRANS(refNo, sty, vty);
                if (res.otherData.Rows.Count > 0)
                {
                    res = command.selectEDCTrans(refNo);
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
    }
}
