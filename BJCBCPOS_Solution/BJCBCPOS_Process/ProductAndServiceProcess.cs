using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Data;
using System.Data;
using BJCBCPOS_Model;

namespace BJCBCPOS_Process
{
    public class ProductAndServiceProcess
    {
        private SqlCommand command;
        private DataTable _dtSaleMain;
        private DataTable _dtPayment;
        private API_PayInvoiceAR _ObjPayInvoiceAR;

        public ProductAndServiceProcess()
        {
            _dtSaleMain = BaseProcess.dtSaleMain;
            _ObjPayInvoiceAR = BaseProcess.ObjPayInvoiceAR;
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

        private void removeDtSaleMain(string rec)
        {
            DataRow[] row = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + rec + "'");
            foreach (DataRow item in row)
            {
                _dtSaleMain.Rows.Remove(item);
            }
        }

        public StoreResult checkOpenDay(FunctionID functionID)
        {
            try
            {
                return command.checkOpenDay(functionID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at ProductAndServiceProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult getRunning(FunctionID functionID, RunningReceiptID receiptID)
        {
            try
            {
                StoreResult res = command.getRunning(functionID, receiptID);
                //if (res.otherData != null && res.otherData.Rows.Count > 0)
                //{
                //    ProgramConfig.saleRefNo = res.otherData.Rows[0]["ReferenceNo"].ToString();
                //    ProgramConfig.saleRefNoIni = res.otherData.Rows[0]["ReferenceNoINI"].ToString();
                //}
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getRunning");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public ProcessResult cashireMessageStatus(FunctionID function)
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.Sale_GetMessageCashier);
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
                AppLog.writeLog("connection to server lost at SaleProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult GetDepositCustomerType()
        {
            try
            {
                return command.GetDepositCustomerType();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetDepositCustomerType");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        //public StoreResult insertTempCustomerFullTax()
        //{
        //    try
        //    {
        //        return command.InsertTempCustomerFullTax();
        //    }
        //    catch (NetworkConnectionException)
        //    {
        //        AppLog.writeLog("connection to server lost at SaleProcess.GetDepositCustomerType");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog(ex);
        //        return new StoreResult(ResponseCode.Error, ex.Message, "", "");
        //    }
        //}

        public StoreResult Deposit_EditPrice(string code, string quant, string price, string rec)
        {
            try
            {
                var res = command.updateItemTempDlyptrans(code, quant, price, rec);
                if (res.response.next)
                {
                    _dtSaleMain.Clear();
                }
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetDepositCustomerType");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult PODGetOrder(string abbNo)
        {
            try
            {
                return command.PODGetOrder(abbNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetDepositCustomerType");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveCreditSaleData(CreditSaleData data)
        {
            try
            {
                command.newTransaction();

                var res = getRunning(FunctionID.CreditSale_APIAR, RunningReceiptID.APIPayInvoiceAR);
                if (!res.response.next)
                {
                    command.rollback();
                    return res;
                }

                _ObjPayInvoiceAR.TransID = res.otherData.Rows[0]["ReferenceNo"].ToString();
                _ObjPayInvoiceAR.PayAmount = Convert.ToDouble(data.Amount);
                _ObjPayInvoiceAR.PayDate = DateTime.Now.ToString();
                _ObjPayInvoiceAR.StoreCode = ProgramConfig.storeCode;
                _ObjPayInvoiceAR.CreateBy = ProgramConfig.userId;
                _ObjPayInvoiceAR.ReceiptNo = data.RefCreditPay;

                res = command.saveTempCREDPAY_TRANS(data);
                if (!res.response.next)
                {
                    command.rollback();
                    return res;
                }

                _ObjPayInvoiceAR.ListInvoice = new MMFSAPI.clsMMFSAPI.invoice_list[data.ListCreditSaleDetail.Count];
                int cnt = 0;
                foreach (var item in data.ListCreditSaleDetail)
                {
                    _ObjPayInvoiceAR.ListInvoice[cnt].invoice_no = item.Credit_InvoiceNo;
                    //_ObjPayInvoiceAR.ListInvoice[cnt].invoice_amount = Convert.ToDouble(item.Credit_Amount);
                    _ObjPayInvoiceAR.ListInvoice[cnt].invoice_amount = Convert.ToDouble(item.Credit_AmountAPI);
                   res = command.saveTempCREDPAY_TRANS_Detail(item);
                   if (!res.response.next)
                   {
                       command.rollback();
                       return res;
                   }
                }
                command.commit();
                BaseProcess.ObjPayInvoiceAR = _ObjPayInvoiceAR;
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetDepositCustomerType");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
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
                AppLog.writeLog("connection to server lost at SaleProcess.posDisplayContent");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

    }
}
