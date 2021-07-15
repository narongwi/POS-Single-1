using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Process;
using BJCBCPOS_Model;
using System.Data;
using System.Windows.Forms;

namespace BJCBCPOS
{
    public class frmSaleProcess : frmBaseProcess
    {
        SaleProcess process = new SaleProcess();
        frmSale _frmSale = null;

        public frmSaleProcess(frmSale fSale)
        {
            _frmSale = fSale;
        }

        //public StoreResult ReturnResult(Func<StoreResult> action)
        //{
        //    try
        //    {
        //        frmLoading.showLoading();
        //        return Utility.CheckNotifyNext(action());
        //    }
        //    catch (NetworkConnectionException net)
        //    {
        //        AppLog.writeLog("connection to server lost at " + action.Method.Name);
        //        if (!_frmSale.CatchNetWorkConnectionException(net))
        //        {
        //            return Utility.CheckNotifyNext(action());
        //        }

        //        if (_currentForm != null)
        //        {
        //            _currentForm.Dispose();
        //            _currentForm = null;
        //        }
        //        return new StoreResult(AppLog.writeLog(net));
        //    }
        //    finally
        //    {
        //       frmLoading.closeLoading();
        //    }
        //}

        //public ProcessResult ReturnResult(Func<ProcessResult> action)
        //{
        //    try
        //    {
        //        frmLoading.showLoading();
        //        return Utility.CheckNotifyNext(action());
        //    }
        //    catch (NetworkConnectionException net)
        //    {
        //        AppLog.writeLog("connection to server lost at " + action.Method.Name);
        //        if (!_frmSale.CatchNetWorkConnectionException(net))
        //        {
        //            return Utility.CheckNotifyNext(action());
        //        }

        //        if (_currentForm != null)
        //        {
        //            _currentForm.Dispose();
        //            _currentForm = null;
        //        }
        //        return new ProcessResult(AppLog.writeLog(net));
        //    }
        //    finally
        //    {
        //        frmLoading.closeLoading();
        //    }
        //}

        //public T ReturnResult<T>(Func<T> action)
        //{
        //    try
        //    {
        //        return action();
        //    }
        //    catch (NetworkConnectionException net)
        //    {
        //        AppLog.writeLog("connection to server lost at " + action.ToString());
        //        _frmSale.CatchNetWorkConnectionException(net);
        //        throw;
        //    }
        //}

        //public StoreResult posDisplayContent()
        //{
        //    return ReturnResult(() => command.posDisplayContent());
        //}

        //public int selectMaxRecTempDlyptrans(string refNo)
        //{
        //    return ReturnResult(() => command.selectMaxRecTempDlyptrans(refNo));
        //}

        public override bool CheckException(NetworkConnectionException net)
        {
            return !_frmSale.CatchNetWorkConnectionException(net);
        }

        public StoreResult getProductIcon()
        {
            return ReturnResult(() => process.getProductIcon());
        }

        public StoreResult InsertTempDLYPTransLocalPOS()
        {
            return ReturnResult(() => process.InsertTempDLYPTransLocalPOS());
        }

        //public ProcessResult deleteAllPayment()
        //{
        //    return ReturnResult(() => process.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h)));
        //}

        public StoreResult CheckRedeemPoint_Free_CPN(string saleAmt, string rdCode, string qty)
        {
            //return ReturnResult(() => command.CheckRedeemPoint_Free_CPN(saleAmt, rdCode, qty), true);
            return ReturnResult(() => process.CheckRedeemPoint_Free_CPN(saleAmt, rdCode, qty));
        }

        public StoreResult Update_PMS_REDEEM_POINT_Product(string cntRedeem, string rateUse, string pointUse, string ruleID, string rdCode)
        {
            return ReturnResult(() => process.Update_PMS_REDEEM_POINT_Product(cntRedeem, rateUse, pointUse, ruleID, rdCode));
        }

        public StoreResult DeleteTempRedeemFreePointCash()
        {
            return ReturnResult(() => process.DeleteTempRedeemFreePointCash());
        }

        public StoreResult UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL()
        {
            return ReturnResult(() => process.UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL());
        }

        public StoreResult CheckRedeemPointPercentDiscount(string saleAmt, string rdCode, string isRedeem)
        {
            return ReturnResult(() => process.CheckRedeemPointPercentDiscount(saleAmt, rdCode, isRedeem));
        }

        public StoreResult Delete_Temp_Redeem_Percent_Discount()
        {
            return ReturnResult(() => process.Delete_Temp_Redeem_Percent_Discount());
        }

        public StoreResult Update_PMS_REDEEM_POINT_Percent_Discount()
        {
            return ReturnResult(() => process.Update_PMS_REDEEM_POINT_Percent_Discount());
        }

        public StoreResult CheckRedeemPoint(string saleAmt)
        {
            return ReturnResult(() => process.CheckRedeemPoint(saleAmt));
        }

        public StoreResult Clear_PMS_REDEEM_POINT_Cash(string ruleID)
        {
            return ReturnResult(() => process.Clear_PMS_REDEEM_POINT_Cash(ruleID));
        }

        public StoreResult Update_PMS_REDEEM_POINT_Cash(string cntRedeem, string rateUse, string pointUse, string ruleID, string rdCode, string s_redeem)
        {
            return ReturnResult(() => process.Update_PMS_REDEEM_POINT_Cash(cntRedeem, rateUse, pointUse, ruleID, rdCode, s_redeem));
        }

        public StoreResult CheckCustIDCard(string idCard)
        {
            return ReturnResult(() => process.CheckCustIDCard(idCard));
        }

        public StoreResult SaveRedeemIDCard()
        {
            return ReturnResult(() => process.SaveRedeemIDCard());
        }

        public StoreResult RedeemPoint_sum()
        {
            return ReturnResult(() => process.RedeemPoint_sum());
        }

        public StoreResult savePaymentRedeemBalance(string amtPrice, string txtBalance, string type)
        {
            return ReturnResult(() => process.savePaymentRedeemBalance(amtPrice, txtBalance, type));
        }

        public StoreResult CheckRedeemPoint_Coupon(string saleAmt)
        {
            return ReturnResult(() => process.CheckRedeemPoint_Coupon(saleAmt));
        }

        public StoreResult GetChange(string saleAmt, string payAmt)
        {
            return ReturnResult(() => process.GetChange(saleAmt, payAmt));
        }

        //public StoreResult PaymentDiscount(string pmCode)
        //{
        //    return ReturnResult(() => command.PaymentDiscount(pmCode));
        //}

        public StoreResult saveLTYD(DataTable dt)
        {
            return ReturnResult(() => process.saveLTYD(dt));
        }

        public StoreResult CheckRunningNumber(string sRef, string vRef, string rRef, string cRef, string lRef,
                                              string eRef, string oRef, string pRef, string dRef, string hRef, string tRef)
        {
            return ReturnResult(() => process.CheckRunningNumber(sRef, vRef, rRef, cRef, lRef, eRef, oRef, pRef, dRef, hRef, tRef));
        }

        public StoreResult PrintRedeemPoint_Coupon()
        {
            return ReturnResult(() => process.PrintRedeemPoint_Coupon());
        }

        public StoreResult RedeemPoint_Coupon_Sum()
        {
            return ReturnResult(() => process.RedeemPoint_Coupon_Sum());
        }

        public StoreResult CheckEmployee(string empID)
        {
            return ReturnResult(() => process.CheckEmployee(empID));
        }

        public StoreResult CheckPCMan(string pcManID)
        {
            return ReturnResult(() => process.CheckPCMan(pcManID));
        }

        public StoreResult SavePCMan(string pcmanID)
        {
            return ReturnResult(() => process.SavePCMan(pcmanID));
        }

        public StoreResult GetSaleOrderType(string type, string menuID, string relateFlag, string level, string valueSaleOrder, string valueDelivery)
        {
            return ReturnResult(() => process.GetSaleOrderType(type, menuID, relateFlag, level, valueSaleOrder, valueDelivery));
        }

        public StoreResult insertSALEORDERTYPE_TRANS(int orderType, int deliveryType, int mediaType)
        {
            return ReturnResult(() => process.insertSALEORDERTYPE_TRANS(orderType, deliveryType, mediaType));
        }

        public StoreResult deleteSALEORDERTYPE_TRANS()
        {
            return ReturnResult(() => process.deleteSALEORDERTYPE_TRANS());
        }

        public StoreResult CheckProducRule(string projectid_productrule, string productcode, string barcodetype)
        {
            return ReturnResult(() => process.CheckProducRule(projectid_productrule, productcode, barcodetype));
        }
    }
}
