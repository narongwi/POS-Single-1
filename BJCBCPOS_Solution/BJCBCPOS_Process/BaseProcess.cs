using System;
using System.Collections.Generic;
using System.Data;
using BJCBCPOS_Model;
using BJCBCPOS_Data;

namespace BJCBCPOS_Process
{
    public static class BaseProcess
    {
        private static SqlCommand _command = null;
        private static SqlCommand _commandLocal = null;
        private static DataTable _dtSaleMain;
        private static DataTable _dtPayment;
        private static DataTable _dtReturnTEMP;
        private static API_PayInvoiceAR _ObjPayInvoiceAR;
        private static Language _currentTempLanguage = new Language (Convert.ToInt32(ProgramConfig.config.getValue("setting", "language")));
        //private static Language _currentTempLanguage = new Language(1);
        public static SqlCommand command
        {
            get
            {
                if (_command == null)
                {
                    _command = new SqlCommand(ProgramConfig.connectionStringFirst);
                }
                return _command;
            }
        }

        public static SqlCommand commandLocal
        {
            get
            {
                if (_commandLocal == null)
                {
                    _commandLocal = new SqlCommand(ProgramConfig.connectionStringLocal);
                }
                return _commandLocal;
            }
        }

        public static API_PayInvoiceAR ObjPayInvoiceAR
        {
            get { return _ObjPayInvoiceAR; }
            set { _ObjPayInvoiceAR = value; }
        }

        public static DataTable dtSaleMain
        {
            get { return _dtSaleMain; }
        }

        public static DataTable dtPayment
        {
            get { return _dtPayment; }
        }

        public static DataTable dtReturnTEMP
        {
            get { return _dtReturnTEMP; }
        }

        public static Language currentTempLanguage
        {
            get { return _currentTempLanguage; }
            set { _currentTempLanguage = value; }
        }

        static BaseProcess()
        {
            _ObjPayInvoiceAR = new API_PayInvoiceAR();
            _dtSaleMain = new DataTable();
            initdtSaleMain();
            _dtPayment = new DataTable();
            initdtPayment();
            _dtReturnTEMP = new DataTable();
            initdtReturnTEMP();
        }

        private static void initdtSaleMain()
        {
            _dtSaleMain.Columns.Add(new DataColumn("STCODE", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("REF", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("REC", typeof(Int32)));
            _dtSaleMain.Columns.Add(new DataColumn("STY", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("VTY", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("PCD", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("QNT", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("AMT", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("FDS", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("TTM", typeof(DateTime)));
            _dtSaleMain.Columns.Add(new DataColumn("USR", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("EGP", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("STT", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("STV", typeof(string)));
            _dtSaleMain.Columns.Add(new DataColumn("REASON_ID", typeof(Int32)));
            _dtSaleMain.Columns.Add(new DataColumn("PDISC", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("DISCID", typeof(Int32)));
            _dtSaleMain.Columns.Add(new DataColumn("DISCAMT", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("UPC", typeof(double)));
            _dtSaleMain.Columns.Add(new DataColumn("DTY", typeof(string)));

            //_dtSaleMain.Columns.Add(new DataColumn("RECID", typeof(Int32)));

            // add temp product data and other data, not available in dlyptrans
            _dtSaleMain.Columns.Add("DisplayRec", typeof(int));
            _dtSaleMain.Columns.Add("DisplayPrice", typeof(double));
            _dtSaleMain.Columns.Add("DisplayAmt", typeof(double));
            _dtSaleMain.Columns.Add("TotalPrice", typeof(double));
            _dtSaleMain.Columns.Add("ProductName", typeof(string));
            _dtSaleMain.Columns.Add("PromotionName", typeof(string));
            _dtSaleMain.Columns.Add("PromotionPrice", typeof(double));
            _dtSaleMain.Columns.Add("PrintExport", typeof(string));
            _dtSaleMain.Columns.Add("IsFFNRTC", typeof(string));
            _dtSaleMain.Columns.Add("PRODUCT_TYPE", typeof(string));

            // set column default value
            _dtSaleMain.Columns["REC"].DefaultValue = 0;
            _dtSaleMain.Columns["QNT"].DefaultValue = 0f;
            _dtSaleMain.Columns["AMT"].DefaultValue = 0f;
            _dtSaleMain.Columns["FDS"].DefaultValue = 0f;
            _dtSaleMain.Columns["EGP"].DefaultValue = 0f;
            _dtSaleMain.Columns["REASON_ID"].DefaultValue = 0;
            _dtSaleMain.Columns["PDISC"].DefaultValue = 0f;
            _dtSaleMain.Columns["DISCID"].DefaultValue = 0;
            _dtSaleMain.Columns["DISCAMT"].DefaultValue = 0f;
            _dtSaleMain.Columns["UPC"].DefaultValue = 0f;

            _dtSaleMain.Columns["DisplayRec"].DefaultValue = 0;
            _dtSaleMain.Columns["DisplayPrice"].DefaultValue = 0f;
            _dtSaleMain.Columns["DisplayAmt"].DefaultValue = 0f;
            _dtSaleMain.Columns["TotalPrice"].DefaultValue = 0f;
            _dtSaleMain.Columns["PromotionPrice"].DefaultValue = 0f;
            _dtSaleMain.Columns["PrintExport"].DefaultValue = "N";
            _dtSaleMain.Columns["IsFFNRTC"].DefaultValue = "N";
            _dtSaleMain.Columns["PRODUCT_TYPE"].DefaultValue = "";
        }

        private static void initdtPayment()
        {
            _dtPayment.Columns.Add(new DataColumn("REF", typeof(string)));
            _dtPayment.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtPayment.Columns.Add(new DataColumn("PAY", typeof(string)));
            _dtPayment.Columns.Add(new DataColumn("AMT", typeof(double)));
            _dtPayment.Columns.Add(new DataColumn("CHG", typeof(double)));
            _dtPayment.Columns.Add(new DataColumn("FXCUQNT", typeof(double)));
            _dtPayment.Columns.Add(new DataColumn("PDISC", typeof(double)));
            _dtPayment.Columns.Add(new DataColumn("RECID", typeof(Int32)));
            _dtPayment.Columns.Add(new DataColumn("REASON_ID", typeof(Int32)));
            _dtPayment.Columns.Add(new DataColumn("USR_AUTHOR", typeof(string)));
        }

        private static void initdtReturnTEMP()
        {
            _dtReturnTEMP.Columns.Add(new DataColumn("STCODE", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("REF", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("MonthID", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("REC", typeof(Int32)));
            _dtReturnTEMP.Columns.Add(new DataColumn("STY", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("VTY", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("PCD", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("QNT", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("AMT", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("FDS", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("TTM", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("USR", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("EGP", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("STT", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("STV", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("REASON_ID", typeof(Int32)));
            _dtReturnTEMP.Columns.Add(new DataColumn("PDISC", typeof(double)));
            _dtReturnTEMP.Columns.Add(new DataColumn("DISCID", typeof(Int32)));
            _dtReturnTEMP.Columns.Add(new DataColumn("DISCAMT", typeof(double)));
            _dtReturnTEMP.Columns.Add(new DataColumn("UPC", typeof(double)));
            _dtReturnTEMP.Columns.Add(new DataColumn("DTY", typeof(string)));
            _dtReturnTEMP.Columns.Add(new DataColumn("RECID", typeof(Int32)));
        }

        public static void clearDataTable() 
        {
            _dtSaleMain.Rows.Clear();
            _dtPayment.Rows.Clear();
            _dtReturnTEMP.Rows.Clear();
        }

        public static void changeConnectionString(string connectionString)
        {
            AppLog.writeLog("[Change connection string] Current connection string: " + connectionString + "");
            _command.changeConnectionString(connectionString);
        }
    }
}
