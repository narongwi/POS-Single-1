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
    /// ReportProcess
    /// contains all process about sale
    /// </summary>
    public class ReportProcess
    {
        private SqlCommand command;

        public ReportProcess()
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
                AppLog.writeLog("connection to server lost at ReportProcess.checkOpenDay");
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
                AppLog.writeLog("connection to server lost at ReportProcess.checkOpenDay");
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
                AppLog.writeLog("connection to server lost at ReportProcess.posDisplayContent");
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
                AppLog.writeLog("connection to server lost at ReportProcess.getRunning");
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
                AppLog.writeLog("connection to server lost at ReportProcess.displayReason");
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
                AppLog.writeLog("connection to server lost at ReportProcess.checkReceipt");
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
                AppLog.writeLog("connection to server lost at ReportProcess.saveTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult syncToDataTank(string eventName, FunctionID functionId, string referenceNo, string rec, string abbNo)
        {
            try
            {
                return command.syncToDataTank(eventName, functionId,referenceNo,rec,abbNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.syncToDataTank");
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
                AppLog.writeLog("connection to server lost at ReportProcess.saveCloseDrawer");
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
                AppLog.writeLog("connection to server lost at ReportProcess.getProductDesc");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }
        public StoreResult searchReceipt(int searchType, string data, string tillNo, bool getAutherize)
        {
            try
            {
                if (getAutherize == true)
                {
                    if (ProgramConfig.superUserId != null && ProgramConfig.superUserId != "" && ProgramConfig.superUserId != "N/A" && ProgramConfig.superUserId != string.Empty)
                    {
                        return command.searchReceipt(searchType, data, "N/A", tillNo, FunctionID.Report_CheckCurrentDayReceipt_SearchReceipt, ProgramConfig.superUserId);
                    }
                    else
                    {
                        return command.searchReceipt(searchType, data, "N/A", tillNo, FunctionID.Report_CheckCurrentDayReceipt_SearchReceipt, ProgramConfig.userId);
                    }
                }
                else
                {
                    return command.searchReceipt(searchType, data, "N/A", tillNo, FunctionID.Report_CheckCurrentDayReceipt_SearchReceipt, "N/A");
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.searchReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayReceiptDetail(string receiptNo, string tillNo, bool getAutherize)
        {
            try
            {
                if (getAutherize == true)
                {
                    if (ProgramConfig.superUserId != null && ProgramConfig.superUserId != "" && ProgramConfig.superUserId != "N/A" && ProgramConfig.superUserId != string.Empty)
                    {
                        return command.displayReceiptDetail(FunctionID.Report_CheckCurrentDayReceipt, receiptNo, tillNo, ProgramConfig.superUserId);
                    }
                    else
                    {
                        return command.displayReceiptDetail(FunctionID.Report_CheckCurrentDayReceipt, receiptNo, tillNo, ProgramConfig.userId);
                    }
                }
                else
                {
                    return command.displayReceiptDetail(FunctionID.Report_CheckCurrentDayReceipt, receiptNo, tillNo, "N/A");
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.displayReceiptDetail");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getTillNo4DispReport(FunctionID functionId)
        {
            try
            {
                return command.getTillNo4DispReport(functionId);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.getTillNo4DispReport");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printReceiptReport(string receiptNo, bool needPrintAutherize)
        {
            try
            {
                if (needPrintAutherize == true)
                {
                    return command.printReceiptReport(receiptNo, ProgramConfig.superUserId);
                }
                else
                {
                    return command.printReceiptReport(receiptNo, ProgramConfig.userId);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.printReceiptReport");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printSumTransReport(string refNo)
        {
            try
            {
                return command.printSumTransReport(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.printSumTransReport");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataTable SumTRansHEader(string posRepNo)
        {
            try
            {
                return command.SumTRansHEader(posRepNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.SumTRansHEader");
                throw;
            }
        }

        public DataTable SumTRansDetail(string posRepNo)
        {
            try
            {
                return command.SumTRansDetail(posRepNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.SumTRansDetail");
                throw;
            }
        }

        public DataTable SelectName(string cashier)
        {
            try
            {
                return command.SelectName(cashier);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.SumTRansDetail");
                throw;
            }
        }

        public StoreResult repSumTrans(String tillNo)
        {
            try
            {
                return command.repSumTrans(tillNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ReportProcess.repSumTrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }
    }
}
