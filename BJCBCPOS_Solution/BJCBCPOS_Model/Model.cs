using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using MMFSAPI;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// store all program hard code data.
    /// </summary>
    public static class FixedData
    {
        public const string config_name = "BJCBCPOS_Config.ini";
        public const string running_name = "BJCBCPOS_Running.ini";
        public const string db_name = "STDBENGINE";
        public const string db_user = "LOWAPP";
        public const string db_pass = "W0L@Pp";
        //public const string db_user_local = "LTRANS";
        //public const string db_pass_local = "lp#2020J@NCNBCbJC";
        public const string db_user_local = "XTRANS";
        public const string db_pass_local = "x1#2020J@NCNBCbJC";

        public const string appName = "BJCBCPOS_Till000";
        public const int db_command_timeout = 10;
        public const int db_connect_timeout = 15;
        public const int connect_retry_time = 3;
        public const bool isLogSql = true;
        public const bool isDebug = true;
        public const string defaultMessageKey = "ApplicationError";
        public const string defaultMessageNotFound = "NoAlertMessage";
        //public static string[] app_default_message = { "", "Application Error.", "ระบบผิดพลาด", "ລະບົບຜິດພາດ" };
        //public static string[] app_default_help_message = { "", "Please Contract IT Service Desk Tel.xxx-xxxx", "กรุณาติดต่อ IT Service Desk Tel.xxx-xxxx", "ກະລຸນາຕິດຕໍ່  IT Service Desk Tel.xxx-xxxx" };
        public const bool member_function_allow = true;
    }

    #region Enum

    public enum RunningReceiptID
    {
        ABB = 1,
        CN = 2,
        FullTax = 3,
        SignInOut = 4,
        OpenDay = 5,
        CashInOut = 6,
        SaleRef = 7,
        ReturnRef = 8,
        VoidRef = 9,
        ActionID = 10,
        Redeem = 13,
        ExportPermit = 14,
        HoldOrder = 15,
        POD = 18,
        CreditSale = 19,
        PODAPI = 20,
        APIPayInvoiceAR = 23
    }

    public enum SearchItemAction
    {
        Delete = 'D',
        PriceAdjust = 'P'
    }

    public enum UCTextBoxIconType
    {
        None,
        ScanAndDelete,
        SearchAndDelete,
        NoneAndDelete,
    }

    public enum IconType
    {
        Search,
        Delete,
        Scan,
        None
    }

    public enum PrinterBrand
    {
        HP = 1,
        Toshiba = 2,
        Epson = 3,
        Window = 4
    }

    public enum SaleMode
    {
        Server = 1,
        Standalone = 2
    }

    public enum RedeemPage
    {
        Product = 1,
        Discount = 2,
        Cash = 3,
        Coupon = 4,
        Earn = 5,
        PWP = 6
    }

    public enum PaymentStepDetail_ModuleID
    {
        None = 0,
        PaymentDiscount = 1,
        Valid_GFSL = 2,
        GetQRCode_QRPayment = 3,
        SaveDataPaymentRefer = 4,
        SaveEDCTRANS = 5,
        pos_CheckCustomer = 6,
        pos_CheckValuePayment = 7,
        pos_CheckDEPO = 8,
        pos_GetRefQRPayment_Offline = 9

        //InterfaceEDC = "Interface_EDC";
        //GenQRCode = "Gen_QR_Code";
        //CheckPayStatus = "Check_Pay_Status";
        //BigCCoBrandDiscount = "BigC_CoBrand_Discount";
    }

    public enum PaymentStepDetail_StepID
    {
        None = 0,
        SelectPayment = 1,
        Input_Main_Reference = 2,
        Input_Amount = 3,
        Call_Module = 4,
        Check_Authorize = 5,
        Display_Amount = 6,
        Input_Sub_Reference = 7
    }

    public enum NetworkErrorType
    {
        NotSpecify = 0,
        ConnectionTimeout = 1,
        CommandTimeout = 2
    }

    public enum MenuIdHamberger
    {
        DisCountProductManual = 1,
        CancelReceipt = 2,
        Member = 3,
        PC_Man = 4,
        Employee = 5,
        OrderType = 6,
        HolderOrder = 7,
        LoadHolder = 8
    }

    public enum LoadingStatus
    {
        None = 0,
        Close = 1,
        Show = 2
    }

    public enum MemberFormat
    {
        BigC = 1,
        MegaMaket = 2
    }

    public enum QRPaymentOnlineMenu
    {
        QR_CscanB = 1,
        QR_BscanC = 2
    }

    public enum PrintInvoiceType
    {
        ABB = 1,
        FULLTAX = 2,
        RELATECUSTOMER = 3
    }

    public enum SelectPrintInvoiceType
    {
        PrintABB = 1,
        PrintFullTax = 2,
    }

    public enum SearchTypeCustomer
    {
        SearchMember = 1,
        SearchCustomer = 2,
        SearchCustomerFullTax = 3
    }

    public enum PageBackFormPayment
    {
        NormalSale = 1,
        Deposit = 2,
        ReceivePOD = 3,
        CreditSale = 4
    }

    public enum LoadFromTable
    {
        TEMPDLYPTRANS = 1,
        TEMP_PODTRANS_PAY = 2,
        TEMPCREDPAY_TRANS_PAY = 3
    }

    public enum EventEDC
    {
        NormalSale = 1,
        Void = 2,
        Return = 3
    }

    #endregion

    public struct banknote
    {
        public double bankValue;
        public int bankCount;

        public banknote(double value, int count)
        {
            this.bankValue = value;
            this.bankCount = count;
        }

        public double totalValue
        {
            get
            {
                return bankValue * bankCount;
            }
        }
    }

    #region Struct
    

    /// <summary>
    /// structer of return data after write error log
    /// </summary>
    /// 
    public struct LogResponse
    {
        public ResponseCode respone;
        public string message;
        public string helpMessage;
    }

    public struct CouponType
    {
        public const string TicketCoupon = "TicketCoupon";
        public const string ProductCoupon = "ProductCoupon";
    }

    public struct CurrentPanelSale
    {
        public const string PanelScanBarcode = "panelScanBarcode";
        public const string PanelEditItem = "panelEditItem";
        public const string PanelDeleteItem = "panelDeleteItem";
        public const string PaneltemDetail = "paneltemDetail";
        public const string PanelPrintExport = "panelPrintExport";
        public const string PanelAddProductSpecial = "panelAddProductSpecial";
        public const string PanelSaveTempSale = "panelSaveTempSale";
        public const string PanelMember = "panelMember";
    }

    //public struct PrintInvoiceType
    //{
    //    public const string ABB = "ABB";
    //    public const string FullTax = "FULLTAX";
    //    public const string CheckMember = "CHECKMEMBER";   
    //}

    public struct Result_frmPopupInput
    {
        public StoreResult resultAction;
        public string input1;
        public string input2;
        public bool IsSuccess;
    }

    public struct Sale_TypeCode
    {
        public int OrderType { get; set; }
        public string OrderTypeDesc { get; set; }
        public int DeliveryType { get; set; }
        public string DeliveryTypeDesc { get; set; }
        public int MediaType { get; set; }
        public string MediaTypeDesc { get; set; }
    }

    public struct SaleOrderTypeStep
    {
        public const string OrderType = "ORDERTYPE";
        public const string DeliveryType = "DELIVERYTYPE";
        public const string MediaType = "MEDIATYPE";
    }

    public struct PaymentStepDetail_TextBoxType
    {
        public const string TextBox = "TextBox";
    }

    public struct PaymentStepDetail_DataType  
    {
        public const string Money = "MONEY";
        public const string NVarchar = "NVARCHAR";
    }

    public struct VoidSaleType
    {
        public const string NormalSale = "NS";
        public const string Deposit = "DP";
        public const string POD = "POD";
        public const string CreditSale = "CRED";
    }

    public struct PODData
    {
        public string InvoiceNo { get; set; }
        public string TransID { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentDate { get; set; }
        public string CreateBy { get; set; }
        public string ReceiptNo { get; set; }
        public string SyncStatus { get; set; }
        public List<CreditSaleDataDetail> ListCreditSaleDetail { get; set; }
    }

    public struct CreditSaleData
    {
        public string RefCreditPay { get; set; }
        public string CustomerID { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerCardNo { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string SyncStatus { get; set; }
        public string TransID { get; set; }
        public string PaymentID { get; set; }
        public List<CreditSaleDataDetail> ListCreditSaleDetail { get; set; }
    }

    public struct CreditSaleDataDetail
    {
        public string StoreCode { get; set; }
        public string RefCreditPay { get; set; }
        public string Seq { get; set; }
        public string Credit_InvoiceNo { get; set; }
        public string Credit_InvoiceDate { get; set; }
        public string Credit_Amount { get; set; }
        public string Credit_AmountAPI { get; set; }
        public string Status { get; set; }
        public string TransDate { get; set; }
    }

    #endregion

    public class NetworkConnectionException : Exception
    {
        public NetworkErrorType errorType { get; set; }

        public NetworkConnectionException()
        {
            errorType = 0;
        }

        public NetworkConnectionException(string message)
            : base(message)
        {
            this.errorType = 0;
        }

        public NetworkConnectionException(string message, NetworkErrorType errorType)
            : base(message)
        {
            this.errorType = errorType;
        }

        public NetworkConnectionException(string message, Exception inner)
            : base(message, inner)
        {
            this.errorType = 0;
        }

        public NetworkConnectionException(string message, Exception inner, NetworkErrorType errorType)
            : base(message, inner)
        {
            this.errorType = errorType;
        }

        public NetworkConnectionException(Exception inner)
            : this("Can't connect to database server. More detail in innerException.", inner)
        {
            errorType = 0;
        }

        public NetworkConnectionException(Exception inner, NetworkErrorType errorType)
            : this("Can't connect to database server. More detail in innerException.", inner, errorType)
        {
        }
    }

    public class TabColor
    {
        public int Index { get; set; }
        public string ColorCode { get; set; }
    }

    public class AuthResult
    {
        public bool Next { get; set; }
        public string maxPriceChange { get; set; }
        public string maxDeleteItemAmt { get; set; }
        public string maxCancelReceiptAmt { get; set; }
        public bool Skip { get; set; }
    }

    public class RunModuleParameter
    {
        public string pmCode = ""; 
        public string cardNo = "";
        public string inpRef1 = "";
        public string dataType = "";
    }

    public class ReturnModuleParameter
    {
        public string inpRef1 = "";
    }

    public class MemberProfileMMFormat
    {
        public string CreditCustomerNo { get; set; }
        public string CustomerCategory { get; set; }
        public string Customer_No { get; set; }
        public string Customer_IDCard { get; set; }
        public string CustomerIDFFTI { get; set; }
        public string CustomerID { get; set; }
        public string Address { get; set; }

        public void Clear()
        {
            CreditCustomerNo = "";
            CustomerCategory = "";
            Customer_No = "";
            Customer_IDCard = "";
            CustomerID = "";
            Address = "";
        }
    }

    public class PaymentStepDet
    {
        public string PMCode { get; set; }
        public string PaymentGroupID { get; set; }
        public string Seq { get; set; }
        public string StepID { get; set; }
        public string DataType { get; set; }
        public string DataValue { get; set; }
        public string MainRef { get; set; }

        public PaymentStepDet()
        {
            PMCode = "";
            PaymentGroupID = "";
            Seq = "";
            StepID = "";
            DataType = "";
            DataValue = "";
            MainRef = "";
        }
    }

    public class API_PayInvoiceAR
    {
        public string TransID { get; set; }
        public double PayAmount { get; set; }
        public string PayDate { get; set; }
        public string StoreCode { get; set; }
        public string CreateBy { get; set; }
        public string ReceiptNo { get; set; }
        public clsMMFSAPI.invoice_list[] ListInvoice { get; set; }
        public clsMMFSAPI.payment_list[] ListPaymnet { get; set; }
        public List<PaymentList> ListPaymnetTemp { get; set; }
    }

    public struct PaymentList
    {
        public string PaymentCode { get; set; }
        public string Amount { get; set; }
        public string PaymentNo { get; set; }
    }
}
