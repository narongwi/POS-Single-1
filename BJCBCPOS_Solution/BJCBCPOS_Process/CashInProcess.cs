using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Data;
using BJCBCPOS_Model;
using System.Reflection;

namespace BJCBCPOS_Process
{
    public class CashInProcess
    {
        private SqlCommand command;

        public CashInProcess()
        {
            command = BaseProcess.command;
        }

        public StoreResult checkOpenDay()
        {
            try
            {
                return command.checkOpenDay(FunctionID.CashIn_CheckOpenDayofTillStatus);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult generateMenu()
        {
            try
            {
                return command.generateMenu(FunctionID.CashIn_DisplayCashInMenu);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.generateMenu");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMinChange(double amount, int typeChange)
        {
            try
            {
                var defaultCurrency = (Currency)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefault.parameterCode);
                return checkMinChange(amount, defaultCurrency, typeChange);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.checkOpenDay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMinChange(double amount, Currency currency, int typeChange)
        {
            try
            {
                Type type = typeof(FunctionID);
                PropertyInfo pInfo;
                if (typeChange == 0)
                {
                    pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_FloatAmountMinimum", currency.code.ToString()));
                }
                else
                {
                    pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_LoanAmountMinimum", currency.code.ToString()));
                }

                var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
                string minAmt = ProgramConfig.getPosConfig(paraCode).ToString();

                if (amount < Convert.ToDouble(minAmt))
                {
                    string responseMessage = ProgramConfig.message.get("frmCashbalance", "MinBalance").message;
                    string helpMessage = ProgramConfig.message.get("frmCashbalance", "MinBalance").help;
                    return new StoreResult(ResponseCode.Error, responseMessage + " " + Convert.ToDouble(minAmt).ToString(ProgramConfig.amountFormatString), helpMessage, "");

                    //  return new StoreResult(ResponseCode.Error, "เงินทอนขั้นต่ำ " + Convert.ToDouble(minAmt).ToString(ProgramConfig.amountFormatString), "", "");
                }
                else
                {
                    FunctionID func = typeChange == 0 ? FunctionID.CashIn_NormalChange_CheckAmount_Minimum : FunctionID.CashIn_AdditionChange_CheckAmount_Minimum;
                    return command.checkChangeAmount(func, amount, currency);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.checkMinChange");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMaxChange(double amount, int typeChange)
        {
            try
            {
                var defaultCurrency = (Currency)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefault.parameterCode);
                return checkMaxChange(amount, defaultCurrency, typeChange);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.checkMaxChange");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMaxChange(double amount, Currency currency, int typeChange)
        {
            try
            {
                Type type = typeof(FunctionID);
                PropertyInfo pInfo;
                if (typeChange == 0)
                {
                    pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_FloatAmountMaximum", currency.code.ToString()));
                }
                else
                {
                    pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_LoanAmountMaximum", currency.code.ToString()));
                }

                var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
                string maxAmt = ProgramConfig.getPosConfig(paraCode).ToString();

                if (amount > Convert.ToDouble(maxAmt))
                {
                    string responseMessage = ProgramConfig.message.get("frmCashbalance", "MaxBalance").message;
                    string helpMessage = ProgramConfig.message.get("frmCashbalance", "MaxBalance").help;
                    return new StoreResult(ResponseCode.Error, responseMessage + " " + Convert.ToDouble(maxAmt).ToString(ProgramConfig.amountFormatString), helpMessage, "");

                    //return new StoreResult(ResponseCode.Error, "เงินทอนมากสุด " + Convert.ToDouble(maxAmt).ToString(ProgramConfig.amountFormatString), "", "");
                }
                else
                {
                    FunctionID func = typeChange == 0 ? FunctionID.CashIn_NormalChange_CheckAmount_Maximum : FunctionID.CashIn_AdditionChange_CheckAmount_Maximum;
                    return command.checkChangeAmount(func, amount, currency);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.checkMaxChange");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveTempCashIn(string cashType, string PM_CODE, string SUB_PM_CODE, string EXCG_RATE, string EXCG_AMT, string CASH_AMT, string ODATE)
        {
            try
            {
                return command.saveTempCashIn(cashType, PM_CODE, SUB_PM_CODE, EXCG_RATE, EXCG_AMT, CASH_AMT, ODATE);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.saveTempCashIn");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveCashInTransaction(string openDrawerTime, string closeDrawerTime)
        {
            try
            {
                return command.saveCashInTransaction(openDrawerTime, closeDrawerTime);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.saveCashInTransaction");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult syncToDataTank()
        {
            try
            {
                return command.syncToDataTank("CashIn", FunctionID.CashIn_SaveCashInTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.cashInRefNo, "0", ProgramConfig.abbNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.syncToDataTank");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printCashIn()
        {
            try
            {
                return command.printCashIn();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.printCashIn");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getCashUnit(string currencyCode)
        {
            try
            {
                return command.getCashUnit(currencyCode, ProgramConfig.cashInRefNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.getCashUnit");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getRunning(FunctionID functionID)
        {
            try
            {
                return command.getRunning(functionID, RunningReceiptID.CashInOut);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.getRunning");
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
                AppLog.writeLog("connection to server lost at CashInProcess.posDisplayContent");
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
                return command.checkMinCashUnitAmount(paymentCode, payAmt, ProgramConfig.cashInRefNo, currencyCode, "1");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.checkMinCashUnitAmount");
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
                StoreResult res = command.getMessageCashierStatus(FunctionID.CashIn_GetMessageCashier);
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
                AppLog.writeLog("connection to server lost at CashInProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult deleteTempCashIn()
        {
            try
            {
                return command.deleteTempCashIn();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at CashInProcess.deleteTempCashIn");
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
                AppLog.writeLog("connection to server lost at SaleProcess.getAmountExchangeRate");
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

        public StoreResult SaveDrawerTrans(FunctionID function)
        {
            try
            {
                return command.saveDrawerTrans(ProgramConfig.cashInRefNo, function);
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
