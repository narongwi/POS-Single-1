using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace BJCBCPOS_Model
{
    public static class ProgramConfig
    {
        private static INIConfig _config = null;
        private static INIConfig _running = null;
        private static Dictionary<string, object> _posConfig = null;
        private static string _abbNo;
        private static string _cnNo;
        private static string _fftiNo;
        private static string _tempFFTINo;

        private static string _signInOut;
        private static string _openDay;
        private static string _cashInOut;
        private static string _sale;
        private static string _return;
        private static string _void;
        private static string _action;
        private static string _posrep;
        private static string _redeem;
        private static string _permit;
        private static string _holdOrder;
        private static string _pod;
        private static string _creditSale;

        private static DataTable _dtActiveLanguage = new DataTable();
        private static List<Language> _listActiveLanguage = new List<Language>();
        private static bool _IsStandAloneMode;

        // get value from computer
        public static string appName { get; set; }
        public static string version { get; set; }
        public static string computerName { get; set; }
        public static string ipAddress { get; set; }

        // get value from config file
        public static string storeCode { get; set; }
        public static string tillNo { get; set; }
        public static Language language { get; set; }

        #region Running Number
        // get value from running file
        public static string abbNo // 1
        {
            get
            {
                if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                {
                    return _tempFFTINo;
                }
                else
                {
                    return _abbNo;
                }
            }
            set
            {
                if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                {
                    if (_tempFFTINo != value)
                    {
                        _running.setValue("", "tempFFTIno", value);
                    }
                    _tempFFTINo = value;
                }
                else
                {
                    if (_abbNo != value)
                    {
                        _running.setValue("", "abbno", value);
                    }
                    _abbNo = value;
                }
            }
        }

        public static string cnNo // 2
        {
            get { return _cnNo; }
            set
            {
                if (_cnNo != value)
                {
                    _running.setValue("", "cnno", value);
                }
                _cnNo = value;
            }
        }

        public static string fftiNo // 3
        {
            get { return _fftiNo; }
            set
            {
                if (_fftiNo != value)
                {
                    _running.setValue("", "fftino", value);
                }
                _fftiNo = value;
            }
        }

        public static string tempFFTINo //16
        {
            get { return _tempFFTINo; }
            set
            {
                if (_tempFFTINo != value)
                {
                    _running.setValue("", "tempFFTIno", value);
                }
                _tempFFTINo = value;
            }
        } 

        public static string endOfShiftRefNoIni
        {
            get
            {
                if (_signInOut == null)
                {
                    _signInOut = _running.getValue("", "signinoutno");
                }
                return _signInOut;
            }
            set
            {
                if (_signInOut != value)
                {
                    _running.setValue("", "signinoutno", value);
                    running.updateValue();
                }
                _signInOut = value;
            }
        } // 4

        public static string endOfTillRefNoIni
        {
            get
            {
                if (_signInOut == null)
                {
                    _signInOut = _running.getValue("", "signinoutno");
                }
                return _signInOut;
            }
            set
            {
                if (_signInOut != value)
                {
                    _running.setValue("", "signinoutno", value);
                    running.updateValue();
                }
                _signInOut = value;
            }
        } // 4

        public static string openDayRefNoIni
        {
            get
            {
                if (_openDay == null)
                {
                    _openDay = _running.getValue("", "opendayno");
                }
                return _openDay;
            }
            set
            {
                if (_openDay != value)
                {
                    _running.setValue("", "opendayno", value);
                    running.updateValue();
                }
                _openDay = value;
            }
        } // 5

        public static string cashInRefNoIni
        {
            get
            {
                if (_cashInOut == null)
                {
                    _cashInOut = _running.getValue("", "cashinoutno");
                }
                return _cashInOut;
            }
            set
            {
                if (_cashInOut != value)
                {
                    _running.setValue("", "cashinoutno", value);
                    running.updateValue();
                }
                _cashInOut = value;
            }
        } // 6
        public static string cashOutRefNoIni
        {
            get
            {
                if (_cashInOut == null)
                {
                    _cashInOut = _running.getValue("", "cashinoutno");
                }
                return _cashInOut;
            }
            set
            {
                if (_cashInOut != value)
                {
                    _running.setValue("", "cashinoutno", value);
                    running.updateValue();
                }
                _cashInOut = value;
            }
        } // 6
        public static string saleRefNoIni
        {
            get
            {
                if (_sale == null)
                {
                    _sale = _running.getValue("", "saleno");
                }
                return _sale;
            }
            set
            {
                if (_sale != value)
                {
                    _running.setValue("", "saleno", value);
                    running.updateValue();
                }
                _sale = value;
            }
        }// 7
        public static string returnRefNoIni
        {
            get
            {
                if (_return == null)
                {
                    _return = _running.getValue("", "returnno");
                }
                return _return;
            }
            set
            {
                if (_return != value)
                {
                    _running.setValue("", "returnno", value);
                    running.updateValue();
                }
                _return = value;
            }
        }// 8
        public static string voidRefNoIni
        {
            get
            {
                if (_void == null)
                {
                    _void = _running.getValue("", "voidno");
                }
                return _void;
            }
            set
            {
                if (_void != value)
                {
                    _running.setValue("", "voidno", value);
                    running.updateValue();
                }
                _void = value;
            }
        }// 9
        public static string actionRefNoIni
        {
            get
            {
                if (_action == null)
                {
                    _action = _running.getValue("", "actionno");
                }
                return _action;
            }
            set
            {
                if (_action != value)
                {
                    _running.setValue("", "actionno", value);
                    running.updateValue();
                }
                _action = value;
            }
        }// 10
        public static string posrepRefNoIni
        {
            get
            {
                if (_posrep == null)
                {
                    _posrep = _running.getValue("", "posrepno");
                }
                return _posrep;
            }
            set
            {
                if (_posrep != value)
                {
                    _running.setValue("", "posrepno", value);
                    running.updateValue();
                }
                _posrep = value;
            }
        }// 12

        //public static string redeemRefNoIni
        //{
        //    get
        //    {
        //        if (_redeem == null)
        //        {
        //            _redeem = _running.getValue("", "redeemno");
        //        }
        //        return _redeem;
        //    }
        //    set
        //    {
        //        if (_redeem != value)
        //        {
        //            _running.setValue("", "redeemno", value);
        //            running.updateValue();
        //        }
        //        _redeem = value;
        //    }
        //} // 13

        public static string expermitRefNoIni
        {
            get
            {
                if (_permit == null)
                {
                    _permit = _running.getValue("", "expermitno");
                }
                return _permit;
            }
            set
            {
                if (_permit != value)
                {
                    _running.setValue("", "expermitno", value);
                    running.updateValue();
                }
                _permit = value;
            }
        } // 14

        public static string holdOrderRefNoIni
        {
            get
            {
                if (_holdOrder == null)
                {
                    _holdOrder = _running.getValue("", "holdsaleno");
                }
                return _holdOrder;
            }
            set
            {
                if (_holdOrder != value)
                {
                    _running.setValue("", "holdsaleno", value);
                    running.updateValue();
                }
                _holdOrder = value;
            }
        } // 15

        public static string podRefNoIni
        {
            get
            {
                if (_pod == null)
                {
                    _pod = _running.getValue("", "podno");
                }
                return _pod;
            }
            set
            {
                if (_pod != value)
                {
                    _running.setValue("", "podno", value);
                    running.updateValue();
                }
                _pod = value;
            }
        } // 18

        public static string creditSaleRefNoIni
        {
            get
            {
                if (_creditSale == null)
                {
                    _creditSale = _running.getValue("", "creditsale");
                }
                return _creditSale;
            }
            set
            {
                if (_creditSale != value)
                {
                    _running.setValue("", "creditsale", value);
                    running.updateValue();
                }
                _creditSale = value;
            }
        } // 18

        // ==============================================

        #endregion

        // cancel while sale
        public static string cancelNo { get; set; }

        // get value from login
        public static string userId { get; set; }
        public static string cashierCode { get; set; }
        public static string cashierName { get; set; }
        public static string password { get; set; }

        static string _superUserID;
        public static string superUserId
        {
            get
            {
                return (_superUserID == null || _superUserID == "" || _superUserID == string.Empty) ? "N/A" : _superUserID;
            }
            set
            {
                _superUserID = value;
            }
        }
        public static string superPassword { get; set; }
        public static string superUserName { get; set; }
        public static StoreResult cashireAuthorizeResult { get; set; }
        public static StoreResult superUserAuthorizeResult { get; set; }

        // get value from DB
        public static string app_db_user { get; set; }
        public static string app_db_pass { get; set; }
        public static ProfileCollection profile { get; set; }
        public static PaymentConfigCollections payment { get; set; }
        public static CurrencyCollections currency { get; set; }
        public static PaymentPolicyCollections paymentPolicy { get; set; }
        public static AlertMessageCollection message { get; set; }
        public static PaymentTemplateConfigCollections paymentTemplate { get; set; }
        public static PaymentMenuIconCollections paymentMenuIcon { get; set; }
        public static DataTable paymentMenuIconDT { get; set; }

        public static DataTable dtActiveLanguage
        {
            get
            {
                if (_dtActiveLanguage == null)
                {
                    return new DataTable();
                }
                else
                {
                    return _dtActiveLanguage;
                }
            }
            set
            {
                _dtActiveLanguage = value;
            }
        }
        public static List<Language> listActiveLanguage
        {
            get
            {
                if (_listActiveLanguage == null)
                {
                    return new List<Language>();
                }
                else
                {
                    return _listActiveLanguage;
                }
            }
            set
            {
                _listActiveLanguage = value;
            }
        }

        // get value from posConfig
        public static string storeName { get; set; }
        public static string storeFullname { get; set; }
        public static string taxId { get; set; }
        public static string permissionId { get; set; }
        public static string serverName { get; set; }
        public static string databaseName { get; set; }
        public static string vatRate { get; set; }
        public static string address { get; set; }
        public static SaleMode saleMode { get; set; }
        public static bool flagDrawer { get; set; }
        public static string amountFormatString { get; set; }
        public static string currencyDefault { get; set; }
        public static string changeLimit { get; set; }
        public static int connectionTimeout { get; set; }
        public static int commandTimeout { get; set; }
        public static int connectionRetry { get; set; }
        public static SaleMode saleModePopUp { get; set; }
        public static string businessLogo { get; set; }
        public static MemberFormat memberFormat { get; set; }
        public static string qrPaymentMID { get; set; }
        public static MemberProfileMMFormat memberProfileMMFormat { get; set; }
        public static string multiStore { get; set; }
        public static string buType { get; set; }

        // other global use variables
        public static string redeemRefNo { get; set; }
        public static string openDayRefNo { get; set; }
        public static string cashInRefNo { get; set; }
        public static string saleRefNo { get; set; }
        public static string returnRefNo { get; set; }
        public static string voidRefNo { get; set; }
        public static string cashOutRefNo { get; set; }
        public static string endOfShiftRefNo { get; set; }
        public static string endOfTillRefNo { get; set; }
        public static string posrepRefNo { get; set; }

        public static string expermitRefNo { get; set; }
        public static string actionRefNo { get; set; }
        public static string holdOrderRefNo { get; set; }
        public static string podRefNo { get; set; }
        public static string creditSaleNo { get; set; }

        public static int timeOutActionIdle { get; set; }
        public static string memberId { get; set; }
        public static string memberName { get; set; }
        public static string memberCardNo { get; set; }
        public static string qntValue { get; set; }
        public static string amtValue { get; set; }
        public static string disValue { get; set; }
        public static string totalValue { get; set; }
        public static string paymentType { get; set; }
        public static string paymentAmt { get; set; }
        public static string refNoOpenDay { get; set; }
        public static string recOpenDay { get; set; }
        public static string redeemLTYD { get; set; }
        public static string paymentDupType { get; set; }
        public static string paymentDupAmt { get; set; }
        public static string employeeID { get; set; }
        public static string loadHoldOrderReceipt { get; set; }
        public static string pcManID { get; set; }
        public static string printerName { get; set; }
        public static string comPort { get; set; }

        static string _podQRCode = "";
        public static string podQRCode
        {
            get
            {
                return _podQRCode;
            }
            set
            {
                _podQRCode = value;
                if (value.Trim() == "")
                {
                    podRefID = "";
                    //podRefFFTI = "";
                }
            }
        }
        public static string podRefID { get; set; }
        public static string podRefFFTI { get; set; }



        //Send to frmPayment
        public static FunctionID paymentFunction { get; set; }
        public static FunctionID paymentOpenCashDrawer { get; set; }
        public static FunctionID paymentCloseCashDrawer { get; set; }
        public static PrintInvoiceType printInvoiceType { get; set; }
        public static PageBackFormPayment pageBackFromPayment { get; set; }

        public static Panel pnLanguage { get; set; }
        public static Form formGlobal { get; set; }

        public static bool flagDiscount { get; set; }
        public static bool enableCashierMessage { get; set; }
        public static bool IsStandAloneMode
        {
            get
            {
                return _IsStandAloneMode;
            }
            set
            {
                _IsStandAloneMode = value;
                if (_IsStandAloneMode)
                {
                    saleMode = SaleMode.Standalone;
                }
                else
                {
                    saleMode = SaleMode.Server;
                }
            }
        }

        // hardware status
        public static bool hasDrawer { get; set; }
        public static bool hasPrinter { get; set; }
        public static string langaugeType { get; set; }
        public static int seqOfProcess { get; set; }

        // sale config frequency use
        public static bool saleNeedAuthorize { get; set; }
        public static bool skipNormalSale { get; set; }
        public static bool normalSaleNeedAuthorize { get; set; }
        public static bool checkSaleCashIn { get; set; }
        public static int productInputType { get; set; }
        public static int defaultCursorPosition { get; set; }
        public static PolicyStatus showPaymentAmount { get; set; }
        public static int salePageState { get; set; } // 0 = initial, 1 = current sale, 2 = after end current sale
        //public static LoadingStatus loadingStatus { get; set; }

        static ProgramConfig()
        {
            storeCode = "XXXXXXXX";
            tillNo = "XXXXXXXX";
            //language = Language.ENGLISH;

            storeName = "XXXXXXXX";
            taxId = "XXXXXXXX";
            permissionId = "XXXXXXXX";
            cashierCode = "XXXXXXXX";
            serverName = "XXXXXXXX";
            databaseName = "XXXXXXXX";
            saleMode = 0;
            version = "XXXXXXXX";
            profile = null;
            userId = "N/A";
            superUserId = "N/A";
            openDayRefNo = "";
            cashInRefNo = "";
            saleRefNo = "";
            langaugeType = "Multi";
            connectionTimeout = FixedData.db_connect_timeout;
            commandTimeout = FixedData.db_command_timeout;
            connectionRetry = 0;
            message = new AlertMessageCollection();
            memberProfileMMFormat = new MemberProfileMMFormat();
            printInvoiceType = PrintInvoiceType.ABB;
        }

        /// <summary>
        /// contain all config from Config.ini file
        /// </summary>
        public static INIConfig config
        {
            get { return _config; }
            set
            {
                _config = value;

                storeCode = _config.getValue("setting", "store");
                tillNo = _config.getValue("setting", "till");
                language = new Language(Convert.ToInt32(_config.getValue("setting", "language")));
                printerName = _config.getValue("setting", "printername");
                connectionTimeout = Convert.ToInt32(_config.getValue("connection", "connecttimeout"));
                commandTimeout = Convert.ToInt32(_config.getValue("connection", "commandtimeout"));
                comPort = _config.getValue("setting", "comport");
            }
        }

        /// <summary>
        /// contain all config from Running.ini file
        /// </summary>
        public static INIConfig running
        {
            get { return _running; }
            set
            {
                _running = value;

                abbNo = _running.getValue("", "abbno");
                cnNo = _running.getValue("", "cnno");
                fftiNo = _running.getValue("", "fftino");
                //redeemRefNoIni = _running.getValue("", "redeemno");
                openDayRefNoIni = _running.getValue("", "opendayno");
                cashInRefNoIni = _running.getValue("", "cashinoutno");
                saleRefNoIni = _running.getValue("", "saleno");
                returnRefNoIni = _running.getValue("", "returnno");
                voidRefNoIni = _running.getValue("", "voidno");
                //cashOutRefNo = _running.getValue("", "cashinoutno");
                endOfShiftRefNoIni = _running.getValue("", "signinoutno");
                //endOfTillRefNo = _running.getValue("", "fftino");
                posrepRefNoIni = _running.getValue("", "posrepno");
                expermitRefNoIni = _running.getValue("", "expermitno");
                actionRefNoIni = _running.getValue("", "actionno");
                holdOrderRefNoIni = _running.getValue("", "holdsaleno");
                tempFFTINo = _running.getValue("", "tempFFTIno");
            }
        }

        /// <summary>
        /// contain all pos config get from database (get from pos_GetConfig)
        /// </summary>
        public static Dictionary<string, object> posConfig
        {
            get { return _posConfig; }
            set
            {
                _posConfig = value;

                if (_posConfig != null && _posConfig.Count > 0)
                {
                    // update config file data with db data
                    storeCode = getPosConfig("StoreCode").ToString();
                    storeName = getPosConfig("StoreShortName").ToString();
                    storeFullname = getPosConfig("StoreFullName").ToString();
                    taxId = getPosConfig("StoreTaxID").ToString();
                    tillNo = getPosConfig("TillNo").ToString();
                    serverName = getPosConfig("TillPosServerName").ToString();
                    vatRate = getPosConfig("VatRateDefault").ToString();
                    address = getPosConfig("StoreAddressLine1").ToString() + " " + getPosConfig("StoreAddressLine2").ToString() + " " + getPosConfig("StoreAddressLine3").ToString();
                    databaseName = getPosConfig("TillDBNamePOS").ToString();
                    businessLogo = getPosConfig("BusinessLogo").ToString();
                    memberFormat = (MemberFormat)Convert.ToInt32(getPosConfig("MemberFormat"));
                    qrPaymentMID = getPosConfig("QRPAYMENT_MID").ToString();
                    multiStore = getPosConfig("MultiStore").ToString();
                    buType = getPosConfig("BusinessType").ToString();

                    //saleMode เอาไว้ใช้สำหรับส่ง parameter ไปที่ stp pos_DisplayContent
                    if (saleMode != SaleMode.Standalone)
                    {
                        saleMode = (SaleMode)Convert.ToInt32(getPosConfig("TillSaleMode").ToString());
                    }                    
                    //saleModePopUp เอาไว้ใช้สำหรับ check ว่าให้ใช้ Mode Stand Alone หรือไม่
                    saleModePopUp = (SaleMode)Convert.ToInt32(getPosConfig("TillSaleMode").ToString());

                    timeOutActionIdle = Convert.ToInt32(getPosConfig("TimeOutActionIdle").ToString());

                    connectionTimeout = Convert.ToInt32(getPosConfig("TimeOutApp").ToString());
                    commandTimeout = Convert.ToInt32(getPosConfig("TimeOutDB").ToString());
                    connectionRetry = Convert.ToInt32(getPosConfig("RetryTimes").ToString());

                    language = new Language(getPosConfig("LanguageDefault").ToString()).ID == 0 ? new Language(1) : new Language(getPosConfig("LanguageDefault").ToString());

                    config.updateValue();

                    // TODO: change get value from db pos config

                    switch (getPosConfig("AmountDisplay").ToString())
                    {
                        case "1" :
                            amountFormatString = "#,##0.00";
                            break;
                        case "2" :
                            amountFormatString = "#,##0";
                            break;
                        case "3" :
                            amountFormatString = "#.##0,00";
                            break;
                        default:
                            amountFormatString = "#,##0.00";
                            break;
                    }

                    //amountFormatString = "#,##0";

                    currencyDefault = getPosConfig("CurrencyDefault").ToString();
                    //Fix Data Changelimit is missing
                    changeLimit = getPosConfig("ChangeLimit").ToString();

                    int printer_brand_id = 0;
                    int.TryParse(getPosConfig("DevicePrinterABB").ToString(), out printer_brand_id);
                    if (FixedData.isDebug)
                    {
                        AppLog.writeLog("Store Return DevicePrinterABB = " + printer_brand_id + " (1 = HP, 2 = Toshiba)" + Environment.NewLine);
                    }
                    Hardware.printerBrand = (PrinterBrand)printer_brand_id;
                }
            }
        }

        public static string connectionStringFirst
        {
            get
            {
                string server = config.getValue("connection", "server");
                //return string.Format("Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, FixedData.db_name, FixedData.db_user, FixedData.db_pass, appName, tillNo);
                return string.Format("Provider=SQLOLEDB;Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, FixedData.db_name, FixedData.db_user, FixedData.db_pass, appName, tillNo);
            }
        }

        public static string connectionString
        {
            get
            {
                string server = config.getValue("connection", "server");
                string db_name = config.getValue("connection", "database");
                if (string.IsNullOrEmpty(appName))
                {
                    appName = FixedData.appName;
                }
                //return string.Format("Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo);
                return string.Format("Provider=SQLOLEDB;Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo);
            }
        }

        public static string connectionStringLocal
        {
            get
            {
                string server = "localhost";
                string db_name = "LOCALPOS";

                if (string.IsNullOrEmpty(appName))
                {
                    appName = FixedData.appName;
                }
                //return string.Format("Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo);
                return string.Format("Provider=SQLOLEDB;Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, FixedData.db_user_local, FixedData.db_pass_local, appName, tillNo);
            }
        }

        public static string connectionString_backup
        {
            get
            {
                string server = config.getValue("connection", "serverbackup");
                string db_name = config.getValue("connection", "databasebackup");
                if (string.IsNullOrEmpty(appName))
                {
                    appName = FixedData.appName;
                }
                //return string.Format("Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo); 
                return string.Format("Provider=SQLOLEDB;Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo);
            }
        }

        public static string connectionString_training
        {
            get
            {
                string server = config.getValue("connection", "servertraining");
                string db_name = config.getValue("connection", "databasetraining");
                if (string.IsNullOrEmpty(appName))
                {
                    appName = FixedData.appName;
                }
                //return string.Format("Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo);
                return string.Format("Provider=SQLOLEDB;Server={0};Database={1};User Id={2};Password={3};Application Name={4}_Till{5}", server, db_name, app_db_user, app_db_pass, appName, tillNo);
            }
        }

        /// <summary>
        /// get value of parameter code from pos config 
        /// </summary>
        /// <param name="parameterCode">parameter code to get value</param>
        /// <returns>parameter value</returns>
        public static object getPosConfig(string parameterCode)
        {
            if (_posConfig == null)
            {
                AppLog.writeLog(new Exception("No pos config data."));
                return null;
            }
            if (_posConfig.ContainsKey(parameterCode))
            {
                return _posConfig[parameterCode];
            }
            else
            {
                AppLog.writeLog(new Exception(string.Format("Can not find parameterCode {0} in posConfig.", parameterCode)));
                return null;
            }
        }

        public static Profile getProfile(FunctionID functionId)
        {
            if (profile == null)
            {
                AppLog.writeLog(new Exception("No policy and profile data."));
                return new Profile();
            }
            return profile.getByFunctionId(functionId.value);
        }

        public static Profile getProfile(string functionId)
        {
            if (profile == null)
            {
                AppLog.writeLog(new Exception("No policy and profile data."));
                return new Profile();
            }
            return profile.getByFunctionId(functionId);
        }

        public static string operateDate
        {
            get
            {
                if (_posConfig != null)
                {
                    return _posConfig["OperateDate"].ToString();
                }
                return DateTime.Now.Date.ToString("yyyyMMdd", new CultureInfo("en-US"));
            }
        }

        public static void updateSaleConfig()
        {
            Profile check = profile.getByFunctionId(FunctionID.Sale_SelectSaleMenu);
            saleNeedAuthorize = (check.profile == ProfileStatus.NotAuthorize);

            check = profile.getByFunctionId(FunctionID.Sale_InputSaleItem_InputProduct_NormalSale);
            skipNormalSale = (check.policy == PolicyStatus.Skip);
            normalSaleNeedAuthorize = (check.profile == ProfileStatus.NotAuthorize);

            check = profile.getByFunctionId(FunctionID.Sale_BeforeInputProductItem_CheckSaleCashIn);
            checkSaleCashIn = (check.policy == PolicyStatus.Work);

            productInputType = Convert.ToInt32(posConfig[FunctionID.Login_DataConfigStore_ProductInput_InputType.parameterCode].ToString());

            defaultCursorPosition = Convert.ToInt32(posConfig[FunctionID.Login_DataConfigStore_SaleMenu_DefualtCursorPositionatStepBeforeInputProductItem.parameterCode].ToString());

            showPaymentAmount = profile.getByFunctionId(FunctionID.Sale_ShowPaymentAmount).policy;
        }

        public static void clearSaleConfig()
        {
            saleNeedAuthorize = false;
            skipNormalSale = false;
            normalSaleNeedAuthorize = false;
            checkSaleCashIn = false;
            productInputType = 0;
            defaultCursorPosition = 0;
            showPaymentAmount = PolicyStatus.NotFound;
        }
    }
}
