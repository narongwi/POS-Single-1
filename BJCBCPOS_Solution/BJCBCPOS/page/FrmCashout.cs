using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;
using BJCBCPOS;
using System.Threading;
using System.Collections;
using System.Reflection;

namespace BJCBCPOS
{
    public partial class frmCashout : Form
    {
        private frmMainMenu frmMain;
        private frmNotify notify;
        DateTime openTime;
        DateTime closeTime;
        private CashOutProcess process = new CashOutProcess();
        private MenuProcess menuProcess = new MenuProcess();
        private int typeChange;
        private string _strNormalChange = "";
        private string _strAdjustChange = "";
        private string _strPayCash = "";
        string defaultCurrency;
        string moneyBagNo = "";
        double amount = 0;
        private List<banknote> banknoteList = null;

        private bool updatePageError = false;

        public string lbChangeText
        {
            get { return lbChange.Text; }
            set
            {
                if (typeChange == 0)
                {
                    _strNormalChange = value;
                    lbChange.Text = _strNormalChange;
                }
                else if (typeChange == 1)
                {
                    _strAdjustChange = value;
                    lbChange.Text = _strAdjustChange;
                }
                else
                {
                    _strPayCash = value;
                    lbChange.Text = _strPayCash;
                }
            }
        }
        public bool EnabledBtnBankNote
        {
            get
            {
                return btnNoteBank.Enabled;
            }
            set
            {
                btnNoteBank.Enabled = value;
                if (value)
                {
                    btnNoteBank.Image = Properties.Resources.icon_banknote_enable;
                }
                else
                {
                    btnNoteBank.Image = Properties.Resources.icon_banknote_disable;
                }
            }
        }

        //====================== For Test
        System.Globalization.CultureInfo cultureinfo =
            new System.Globalization.CultureInfo("en-US");
        //======================

        public frmCashout()
        {
            InitializeComponent();
        }

        private void frmCashout_Load(object sender, EventArgs e)
        {
            try
            {
                if (ProgramConfig.enableCashierMessage)
                {
                    ucHeader2.alertFunctionID = FunctionID.Login_CashierMessage_Status;
                    ucHeader2.alertEnabled = true;
                    ProcessResult res = process.cashireMessageStatus();
                    if (res.response.next)
                    {
                        if (res.needNextProcess)
                        {
                            ucHeader2.alertStatus = true;
                        }
                        else
                        {
                            ucHeader2.alertStatus = false;
                        }
                        updatePageError = false;
                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        updatePageError = true;
                    }
                }
                else
                {
                    ucHeader2.alertEnabled = false;
                }

                //Profile check = ProgramConfig.getProfile(FunctionID.CashOut_CheckUserIDPasswordCurrentCashier);
                //if (check.policy == PolicyStatus.Work)
                //{
                //    frmUserAuthorize auth;
                //    if (check.diffUserStatus)
                //    {
                //        auth = new frmUserAuthorize("", "");
                //    }
                //    else
                //    {
                //        auth = new frmUserAuthorize(lbSummaryCashout.Text, "1");
                //    }

                //    auth.function = FunctionID.CashOut_CheckUserIDPasswordCurrentCashier;
                //    DialogResult auth_res = auth.ShowDialog(this);
                //    if (auth_res != DialogResult.Yes)
                //    {
                //        //frmNotify dialog = new frmNotify(ResponseCode.Error, "No Authorize.");
                //        //dialog.ShowDialog(this);
                //        this.Dispose();
                //        return;
                //    }
                //}

                label4.Text = ProgramConfig.currencyDefault;
                _strNormalChange = lbNormalChange.Text;
                _strAdjustChange = lbAdjustChange.Text;
                _strPayCash = lbPayCash.Text;
                //AppMessage.getMessage(ProgramConfig.language, this.Name, "lbMoneyBag");
                //lbMoneyBag.Text = string.Format( AppMessage.getMessage(ProgramConfig.language, this.Name, "lbMoneyBag"),moneyBagNo);
                lbTxtLockNo.Text = ProgramConfig.tillNo;
                lbTxtAsOfDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm", cultureinfo);

                defaultCurrency = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefault.parameterCode).ToString();
                ucDDCurrency.LabelText = defaultCurrency;
                ucDDCurrency.ValueText = defaultCurrency;

                panel1.Controls.Clear();
                pn_MainChange.BringToFront();

                btnNormalChange.Visible = true;
                btnAdjustChange.Visible = true;
                btnPayCash.Visible = true;

                GenerateMenu();

                DisableControl();
                setVisibleButtonPayment();
                //btnAdjustChange.Tag = "disable";
                //btnNormalChange.Tag = "disable";
                //btnPayCash.Tag = "disable";

                //ปิด default
                //btnPayCash_Click(this, null);

                //this.ActiveControl = ucTBWI_Amt.TextBox;

                StoreResult result = process.posDisplayContent();
                if (result.response.next)
                {
                    if (result.otherData.Rows.Count > 0)
                    {
                        if (result.otherData.Columns.Contains("Content_Default"))
                        {
                            ucFooterTran1.mainContent = result.otherData.Rows[0]["Content_Default"].ToString();
                        }
                        if (result.otherData.Columns.Contains("Content_Detail"))
                        {
                            ucFooterTran1.fullContent = result.otherData.Rows[0]["Content_Detail"].ToString();
                        }
                        ucFooterTran1.functionId = FunctionID.Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeCode.formatValue;
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    StoreResult res = process.CheckRunningNumber(ProgramConfig.saleRefNoIni, ProgramConfig.voidRefNoIni, ProgramConfig.returnRefNoIni, ProgramConfig.cashInRefNoIni
    , ProgramConfig.endOfShiftRefNoIni, ProgramConfig.expermitRefNoIni, ProgramConfig.openDayRefNoIni, ProgramConfig.posrepRefNoIni, ProgramConfig.actionRefNoIni, ProgramConfig.holdOrderRefNoIni, ProgramConfig.tempFFTINo);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        //private void GenerateMenu()
        //{
        //    // TODO: get display menu from db
        //    //StoreResult result = process.getMenu();
        //    //if (result.response.next)
        //    //{
        //    //    if (result.otherData != null && result.otherData.Rows != null)
        //    //    {
        //    //        //DisableButton(result.otherData);
        //    //        foreach (DataRow dr in result.otherData.Rows)
        //    //        {
        //    //            Button currentBtn = null;
        //    //            //if (dr["functionid"] + "" == FunctionID.CashOut_NormalChange.formatValue)
        //    //            if (dr["functionid"] + "" == FunctionID.CashOut_Sale.formatValue)
        //    //            {
        //    //                currentBtn = btnPayCash;
        //    //            }
        //    //            else if (dr["functionid"] + "" == FunctionID.CashOut_AdditionalChange.formatValue)
        //    //            {
        //    //                currentBtn = btnAdjustChange;
        //    //            }
        //    //            else if (dr["functionid"] + "" == FunctionID.CashOut_NormalChange.formatValue)
        //    //            {
        //    //                currentBtn = btnNormalChange;
        //    //            }

        //    //            if (currentBtn != null)
        //    //            {
        //    //                switch (dr["MenuSeq"] + "")
        //    //                {
        //    //                    case "1":
        //    //                        currentBtn.Location = new Point(7, 60);
        //    //                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_left_white;
        //    //                        break;
        //    //                    case "2":
        //    //                        currentBtn.Location = new Point(165, 60);
        //    //                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_right_white;
        //    //                        break;
        //    //                    case "3":
        //    //                        currentBtn.Location = new Point(7, 131);
        //    //                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_left_white;
        //    //                        break;
        //    //                }
        //    //                currentBtn.Visible = dr["MenuStatus"] + "" == "2";
        //    //                currentBtn.Text = dr["MenuName"] + "";
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    frmNotify dialog = new frmNotify(result.response, result.responseMessage, result.helpMessage);
        //    //    dialog.ShowDialog(this);
        //    //    this.Dispose();
        //    //    return;
        //    //}
        //    // use mock up 
        //    btnNormalChange.Enabled = true;
        //    btnAdjustChange.Visible = false;
        //    btnPayCash.Enabled = true;
        //}

        private void DisableButton(DataTable dataTable)
        {
            //if (dataTable.Select("Seq = '1'").Count() == 0)
            //{

            //}
        }

        private StoreResult GenerateMenu()
        {
            btnNormalChange.Visible = false;
            btnAdjustChange.Visible = false;
            btnPayCash.Visible = false;
            button2.Visible = false;

            StoreResult result;
            result = process.generateMenu();
            if (result.response.next)
            {
                if (result.otherData != null && result.otherData.Rows != null)
                {
                    //DisableButton(result.otherData);
                    foreach (DataRow dr in result.otherData.Rows)
                    {
                        Button currentBtn = null;
                        if (dr["functionid"] + "" == FunctionID.CashOut_NormalChange.formatValue)
                        {
                            currentBtn = btnNormalChange;
                        }
                        else if (dr["functionid"] + "" == FunctionID.CashOut_AdditionalChange.formatValue)
                        {
                            currentBtn = btnAdjustChange;
                        }
                        else if (dr["functionid"] + "" == FunctionID.CashOut_Sale.formatValue)
                        {
                            currentBtn = btnPayCash;
                        }

                        if (currentBtn != null)
                        {
                            switch (dr["MenuSeq"] + "")
                            {
                                case "1":
                                    currentBtn.Location = new Point(7, 60);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Text = dr["MenuName"] + "";
                                        //currentBtn.Enabled = true;
                                        currentBtn.Visible = true;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_left_white;
                                    }
                                    else
                                    {
                                        currentBtn.Text = "";
                                        currentBtn.Enabled = false;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_btm_left_disable;
                                    }
                                    break;
                                case "2":
                                    currentBtn.Location = new Point(165, 60);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Text = dr["MenuName"] + "";
                                        //currentBtn.Enabled = true;
                                        currentBtn.Visible = true;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_right_white;
                                    }
                                    else
                                    {
                                        currentBtn.Text = "";
                                        currentBtn.Enabled = false;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_btm_right_disable;
                                    }
                                    break;
                                case "3":
                                    currentBtn.Location = new Point(7, 131);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Text = dr["MenuName"] + "";
                                        //currentBtn.Enabled = true;
                                        currentBtn.Visible = true;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_btm_left_white;
                                    }
                                    else
                                    {
                                        currentBtn.Text = "";
                                        currentBtn.Enabled = false;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_btm_left_disable;
                                    }
                                    break;
                                case "4":
                                    currentBtn.Location = new Point(165, 131);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Text = dr["MenuName"] + "";
                                        //currentBtn.Enabled = true;
                                        currentBtn.Visible = true;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_btm_right_white;
                                    }
                                    else
                                    {
                                        currentBtn.Text = "";
                                        currentBtn.Enabled = false;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_btm_right_disable;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void DrawerStatus(string status)
        {
            if (status.ToUpper() == "FALSE")
            {
                frmLoading.closeLoading();
                ConfirmChange();
                frmLoading.closeLoading();
            }
        }

        private void ConfirmChange()
        {
            try
            {
                //Save time Close Cash Drawer
                closeTime = DateTime.Now;

                Program.control.CloseForm("frmNotify");
                return;

                //var result = process.saveCashInTransaction(openTime, closeTime);
                //if (!result.response.next)
                //{
                //    frmNotify dialog = new frmNotify(result.response, result.responseMessage, result.helpMessage);
                //    dialog.ShowDialog(this);
                //    Program.control.CloseForm("frmNotify");
                //    return;
                //}
            }
            catch (NetworkConnectionException net)
            {
                throw net;
                //Program.control.RetryConnection(net.errorType);
            }
            catch (Exception ex)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
                Program.control.CloseForm("frmNotify");
            }
        }

        private void btnNormalChange_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                //StoreResult res = process.posCheckCashIn();
                if (banknoteList != null)
                {
                    banknoteList.Clear();
                }
                panel1.Controls.Clear();
                SummaryCashIn();
                btnNormalChange.Enabled = false;
                btnPayCash.Enabled = false;
                ucTBWI_Amt.EnabledUC = true;

                moneyBagNo = "";
                lbMoneyBag.Text = "";

                typeChange = 0;
                string cfAmtType = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashOut_LoanAmountType.parameterCode);
                switch (cfAmtType)
                {
                    case "1":
                        ucDDCurrency.Enabled = false;
                        string defaultAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashOut_LoanAmountDefault.parameterCode);
                        ucTBWI_Amt.Text = defaultAmt;
                        ucTBWI_Amt.label1.Visible = false;
                        break;
                    case "2":
                        ucDDCurrency.Enabled = true;
                        Type type = typeof(FunctionID);
                        var pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashOut_LoanAmountDefault", ucDDCurrency.ValueText));
                        var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
                        string defaultCurrencyAmt = ProgramConfig.getPosConfig(paraCode).ToString();
                        ucTBWI_Amt.Text = defaultCurrencyAmt;
                        lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
                        ucTBWI_Amt.label1.Visible = false;
                        string config2 = (string)ProgramConfig.getPosConfig(FunctionID.CashOut_NormalChange_InputMethod.parameterCode);
                        switch (config2)
                        {
                            case "1":
                                this.EnabledBtnBankNote = false;
                                ucTBWI_Amt.EnabledUC = true;
                                break;
                            case "2":
                                this.EnabledBtnBankNote = true;
                                ucTBWI_Amt.EnabledUC = false;
                                ucTBWI_Amt.IsTextChange = true;
                                ucKeypad.ucTBWI = ucTBWI_Amt;
                                break;
                            case "3":
                                this.EnabledBtnBankNote = true;
                                ucTBWI_Amt.EnabledUC = true;
                                break;
                        }
                        ucTBWI_Amt.label1.Visible = false;
                        break;
                }
                                                            
                Profile check = ProgramConfig.getProfile(FunctionID.CashOut_NormalChange_DefaultAmount_Editable);

                switch (check.policy)
                {
                    case PolicyStatus.NotCheck:
                        break;
                    case PolicyStatus.Skip:
                        ucTBWI_Amt.EnabledUC = false;
                        this.EnabledBtnBankNote = false;
                        ucTBWI_Amt.IsTextChange = true;
                        ucKeypad.ucTBWI = ucTBWI_Amt;
                        break;
                    case PolicyStatus.Work:
                        ucTBWI_Amt.EnabledUC = true;
                        this.EnabledBtnBankNote = true;
                        ucTBWI_Amt.label1.Visible = false;
                        break;
                }


                DisableControl();
                if (btnNormalChange.Tag == null || (string)btnNormalChange.Tag == "disable")
                {
                    lbChange.Text = _strNormalChange;
                    lbChange.Visible = true;
                    panelAmt.Visible = true;
                    btnNormalChange.Tag = "enable";
                    ChangeButtonBackGround(btnNormalChange);
                    btnNormalChange.ForeColor = Color.White;
                    ucTBWI_Amt.SetSelection = true;
                }



                //var chk = false;
                //Open Cash Drawer
                Hardware.openDrawer();
                process.SaveDrawerTrans(FunctionID.CashOut_NormalChange_OpenDrawerAndRecordTime);
                if (Hardware.isDrawerOpen)
                {
                    drawerOpenEvent();
                }
                Hardware.addDrawerListeners(DrawerStatusClose);

                //Hardware.addDrawerListeners(DrawerStatus);
                //var chk = Hardware.openDrawer();

                //openTime = DateTime.Now;
                frmLoading.closeLoading();
                if (Hardware.isDrawerOpen)
                {
                    string responseMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").message;
                    string helpMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").help;
                    frmNotify dialog = new frmNotify(ResponseCode.CloseDrawer, responseMessage, helpMessage);

                    //frmNotify dialog = new frmNotify(ResponseCode.Success, "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำรายการต่อ");
                    dialog.btnOK.Visible = false;
                    dialog.Show(this);
                    dialog.Refresh();
                }

                //if (!chk)
                //{
                //    ConfirmChange();
                //}
            }
            catch (NetworkConnectionException net)
            {
                throw net;
                //frmLoading.closeLoading();
                //btnPayCash.Enabled = true;
                //btnNormalChange.Enabled = true;
                //Program.control.RetryConnection(net.errorType);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                btnPayCash.Enabled = true;
                btnNormalChange.Enabled = true;
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
                Program.control.CloseForm("frmNotify");
            }
        }

        private string GetExchangeRate(string currency)
        {
            float ex_rate = ProgramConfig.payment.getExchangeRate(currency);
            if (currency == defaultCurrency)
            {
                return "1";
            }
            else
            {
                return ex_rate.ToString();
            }

            //StoreResult res = process.getAmountExchangeRate("0", "1", ProgramConfig.payment.getMainCurrency());

            ////double ex_rate = ProgramConfig.payment.getExchangeRate(currency);
            //if (currency == defaultCurrency)
            //{
            //    return "1";
            //}
            //else
            //{
            //    foreach (DataRow dr in res.otherData.Rows)
            //    {
            //        if (dr["PaymentSubCode"].ToString() == currency)
            //        {
            //            return dr["rate"].ToString();
            //        }                   
            //    }               
            //}
            //return "";
        }

        private void DisableControl()
        {
            btnNormalChange.Tag = "disable";
            btnAdjustChange.Tag = "disable";
            btnPayCash.Tag = "disable";

            ChangeButtonBackGround(btnAdjustChange);
            ChangeButtonBackGround(btnNormalChange);
            ChangeButtonBackGround(btnPayCash);
            panelAmt.Visible = false;
            lbChange.Visible = false;
            btnNormalChange.ForeColor
                = btnAdjustChange.ForeColor
                = btnPayCash.ForeColor
                = Color.Gray;

            //ucDDCurrency.LabelText 
            //    = ucDDCurrency.DisplayText
            //    = AppMessage.getMessage(ProgramConfig.language, "frmCashout", "UCddlLbText");
            //ucDDCurrency.ValueText = defaultCurrency;
            //lb_ExchangeRate.Text = "0.0000";
            //ucTBWI_Amt.Text = "";
        }

        private void btnAdjustChange_Click(object sender, EventArgs e)
        {
            //frmLoading.showLoading();
            //try
            //{
            //    if (banknoteList != null)
            //    {
            //        banknoteList.Clear();
            //    }
            //    panel1.Controls.Clear();
            //    SummaryCashIn();
            //    //btnNormalChange.Enabled = false;
            //    btnPayCash.Enabled = false;
            //    ucTBWI_Amt.EnabledUC = true;

            //    moneyBagNo = "";
            //    lbMoneyBag.Text = "";

            //    typeChange = 0;
            //    string cfAmtType = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashOut_FloatAmountType.parameterCode);
            //    switch (cfAmtType)
            //    {
            //        case "1":
            //            ucDDCurrency.Enabled = false;
            //            string defaultAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashOut_FloatAmountDefault.parameterCode);
            //            ucTBWI_Amt.Text = defaultAmt;
            //            ucTBWI_Amt.label1.Visible = false;
            //            break;
            //        case "2":
            //            ucDDCurrency.Enabled = true;
            //            Type type = typeof(FunctionID);
            //            var pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashOut_FloatAmountDefault", ucDDCurrency.ValueText));
            //            var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
            //            string defaultCurrencyAmt = ProgramConfig.getPosConfig(paraCode).ToString();
            //            ucTBWI_Amt.Text = defaultCurrencyAmt;
            //            lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
            //            ucTBWI_Amt.label1.Visible = false;
            //            string config2 = (string)ProgramConfig.getPosConfig(FunctionID.CashOut_NormalChange_InputMethod.parameterCode);
            //            switch (config2)
            //            {
            //                case "1":
            //                    this.EnabledBtnBankNote = false;
            //                    ucTBWI_Amt.EnabledUC = true;
            //                    break;
            //                case "2":
            //                    this.EnabledBtnBankNote = true;
            //                    ucTBWI_Amt.EnabledUC = false;
            //                    ucTBWI_Amt.IsTextChange = true;
            //                    ucKeypad.ucTBWI = ucTBWI_Amt;
            //                    break;
            //                case "3":
            //                    this.EnabledBtnBankNote = true;
            //                    ucTBWI_Amt.EnabledUC = true;
            //                    break;
            //            }
            //            ucTBWI_Amt.label1.Visible = false;
            //            break;
            //    }

            //    Profile check = ProgramConfig.getProfile(FunctionID.CashOut_NormalChange_DefaultAmount_Editable);

            //    switch (check.policy)
            //    {
            //        case PolicyStatus.NotCheck:
            //            break;
            //        case PolicyStatus.Skip:
            //            ucTBWI_Amt.EnabledUC = false;
            //            this.EnabledBtnBankNote = false;
            //            ucTBWI_Amt.IsTextChange = true;
            //            ucKeypad.ucTBWI = ucTBWI_Amt;
            //            break;
            //        case PolicyStatus.Work:
            //            ucTBWI_Amt.EnabledUC = true;
            //            this.EnabledBtnBankNote = true;
            //            ucTBWI_Amt.label1.Visible = false;
            //            break;
            //    }


            //    DisableControl();
            //    if (btnNormalChange.Tag == null || (string)btnNormalChange.Tag == "disable")
            //    {
            //        lbChange.Text = _strNormalChange;
            //        lbChange.Visible = true;
            //        panelAmt.Visible = true;
            //        btnNormalChange.Tag = "enable";
            //        ChangeButtonBackGround(btnNormalChange);
            //        btnNormalChange.ForeColor = Color.White;
            //        ucTBWI_Amt.SetSelection = true;
            //    }



            //    //var chk = false;
            //    //Open Cash Drawer
            //    Hardware.openDrawer();
            //    if (Hardware.isDrawerOpen)
            //    {
            //        drawerOpenEvent();
            //    }
            //    Hardware.addDrawerListeners(DrawerStatusClose);

            //    //Hardware.addDrawerListeners(DrawerStatus);
            //    //var chk = Hardware.openDrawer();

            //    //openTime = DateTime.Now;
            //    frmLoading.closeLoading();
            //    if (Hardware.isDrawerOpen)
            //    {
            //        string responseMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").message;
            //        string helpMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").help;
            //        frmNotify dialog = new frmNotify(ResponseCode.CloseDrawer, responseMessage, helpMessage);

            //        //frmNotify dialog = new frmNotify(ResponseCode.Success, "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำรายการต่อ");
            //        dialog.btnOK.Visible = false;
            //        dialog.Show(this);
            //        dialog.Refresh();
            //    }

            //    //if (!chk)
            //    //{
            //    //    ConfirmChange();
            //    //}
            //}
            //catch (NetworkConnectionException net)
            //{
            //    //throw;
            //    frmLoading.closeLoading();
            //    btnPayCash.Enabled = true;
            //    btnNormalChange.Enabled = true;
            //    Program.control.RetryConnection(net.errorType);
            //}
            //catch (Exception ex)
            //{
            //    frmLoading.closeLoading();
            //    btnPayCash.Enabled = true;
            //    btnNormalChange.Enabled = true;
            //    frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
            //    dialog.ShowDialog(this);
            //    Program.control.CloseForm("frmNotify");
            //}

            #region Old Code
            //typeChange = 1;
            //string cfAmtType = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountType.parameterCode);
            //switch (cfAmtType)
            //{
            //    case "1":
            //        ucDDCurrency.Enabled = false;
            //        string defaultAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountDefault.parameterCode);
            //        ucTBWI_Amt.Text = defaultAmt;
            //        ucTBWI_Amt.label1.Visible = false;
            //        break;
            //    case "2":
            //        Type type = typeof(FunctionID);
            //        var pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_LoanAmountDefault", ucDDCurrency.LabelText));
            //        var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
            //        string defaultCurrencyAmt = ProgramConfig.getPosConfig(paraCode).ToString();
            //        ucTBWI_Amt.Text = defaultCurrencyAmt;
            //        lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
            //        ucTBWI_Amt.label1.Visible = false;
            //        string config2 = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountMethod.parameterCode);
            //        switch (config2)
            //        {
            //            case "1":
            //                this.EnabledBtnBankNote = false;
            //                ucTBWI_Amt.EnabledUC = true;
            //                break;
            //            case "2":
            //                this.EnabledBtnBankNote = true;
            //                ucTBWI_Amt.EnabledUC = false;
            //                ucTBWI_Amt.IsTextChange = true;
            //                ucKeypad.ucTBWI = ucTBWI_Amt;
            //                break;
            //            case "3":
            //                this.EnabledBtnBankNote = true;
            //                ucTBWI_Amt.EnabledUC = true;
            //                break;
            //        }
            //        break;
            //}

            //Profile check = ProgramConfig.getProfile(FunctionID.CashIn_AdditionChange_DefalutAmount_Editable);

            //switch (check.policy)
            //{
            //    case PolicyStatus.NotCheck:
            //        break;
            //    case PolicyStatus.Skip:
            //        ucTBWI_Amt.EnabledUC = false;
            //        this.EnabledBtnBankNote = false;
            //        ucTBWI_Amt.IsTextChange = true;
            //        ucKeypad.ucTBWI = ucTBWI_Amt;
            //        break;
            //    case PolicyStatus.Work:
            //        ucTBWI_Amt.EnabledUC = true;
            //        this.EnabledBtnBankNote = true;
            //        break;
            //}        

            //DisableControl();
            //if (btnAdjustChange.Tag == null || (string)btnAdjustChange.Tag == "disable")
            //{
            //    lbChange.Text = _strAdjustChange;
            //    lbChange.Visible = true;
            //    panelAmt.Visible = true;
            //    btnAdjustChange.Tag = "enable";
            //    ChangeButtonBackGround(btnAdjustChange);
            //    btnAdjustChange.ForeColor = Color.White;
            //    ucTBWI_Amt.SetSelection = true;
            //}
            #endregion
        }

        private void btnPayCash_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                if (banknoteList != null)
                {
                    banknoteList.Clear();
                }
                panel1.Controls.Clear();
                SummaryCashIn();
                btnPayCash.Enabled = false;
                btnNormalChange.Enabled = false;
                EnabledBtnBankNote = true;
                ucTBWI_Amt.EnabledUC = true;

                typeChange = 2;
                StoreResult res = process.checkLastLoginSale();
                if (res.response.next)
                {
                    if (res.response == ResponseCode.Information)
                    {
                        notify = new frmNotify(res);
                        notify.ShowDialog(this);
                    }

                    res = process.summarySale();
                    if (res.response.next)
                    {
                        if (res.response == ResponseCode.Information)
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog(this);
                        }

                        frmMoneyBagInput input = new frmMoneyBagInput();
                        DialogResult inputRes = input.ShowDialog(this);
                        if (inputRes == DialogResult.OK)
                        {
                            if (res.otherData != null)
                            {
                                panel1.Controls.Clear();
                                int num = 1, status = 0;
                                double amt;
                                UCItemCashIn ucITCI;
                                foreach (DataRow row in res.otherData.Rows)
                                {
                                    int.TryParse(row["CashOutStatus"].ToString(), out status);
                                    double.TryParse(row["PaymentAmt"].ToString(), out amt);
                                    if (status == 2 && amt > 0)
                                    {
                                        // add item to display
                                        ucITCI = new UCItemCashIn();
                                        ucITCI.lbNoText = num.ToString();
                                        ucITCI.lbChangeText = _strPayCash;
                                        ucITCI.lbChangeTypeText = row["PaymentCode"].ToString();
                                        ucITCI.lbExchangeRateText = "-";
                                        ucITCI.lbCurrencyText = ucDDCurrency.LabelText;
                                        ucITCI.lbInputDisplayText = amt.ToString(ProgramConfig.amountFormatString);
                                        ucITCI.lbAmtText = amt.ToString(ProgramConfig.amountFormatString);
                                        ucITCI.lbInputAmt = amt.ToString(ProgramConfig.amountFormatString);
                                        ucITCI.lbCashType = "S";
                                        ucITCI.paymentType = row["PaymentName"].ToString();
                                        ucITCI.btnDeleteVisible = false;
                                        panel1.Controls.Add(ucITCI);
                                        num++;
                                    }
                                }
                                RefreshGrid();
                            }
                            SummaryCashIn();

                            moneyBagNo = input.moneyBag;
                            lbMoneyBag.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbMoneyBag"), moneyBagNo);
                            //lbMoneyBag.Text = "เลขที่ซองเงิน: " + moneyBagNo;
                            lbMoneyBag.Visible = true;

                            ucDDCurrency.Enabled = true;
                            ucTBWI_Amt.Text = "";
                            lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
                            ucTBWI_Amt.label1.Visible = false;
                            ucTBWI_Amt.EnabledUC = true;

                            // TODO: change parameter code to "CashOutSaleAmtMethod". currently not available
                            string config2 = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashOut_SaleAmountMethod.parameterCode);
                            switch (config2)
                            {
                                case "1":
                                    this.EnabledBtnBankNote = false;
                                    ucTBWI_Amt.EnabledUC = true;
                                    break;
                                case "2":
                                    this.EnabledBtnBankNote = true;
                                    ucTBWI_Amt.EnabledUC = false;
                                    ucTBWI_Amt.IsTextChange = true;
                                    ucKeypad.ucTBWI = ucTBWI_Amt;
                                    break;
                                case "3":
                                    this.EnabledBtnBankNote = true;
                                    ucTBWI_Amt.EnabledUC = true;
                                    break;
                            }

                            DisableControl();
                            if (btnPayCash.Tag == null || (string)btnPayCash.Tag == "disable")
                            {
                                lbChange.Text = _strPayCash;
                                lbChange.Visible = true;
                                panelAmt.Visible = true;
                                btnPayCash.Tag = "enable";
                                ChangeButtonBackGround(btnPayCash);
                                btnPayCash.ForeColor = Color.White;
                                ucTBWI_Amt.SetSelection = true;
                            }
                            //Hardware.addDrawerListeners(DrawerStatusOpen);
                            Hardware.openDrawer();
                            process.SaveDrawerTrans(FunctionID.CashOut_Sale_OpenDrawerAndRecordTime);
                            if (Hardware.isDrawerOpen)
                            {
                                drawerOpenEvent();
                            }
                            Hardware.addDrawerListeners(DrawerStatusClose);
                        }
                        else if (inputRes == DialogResult.Cancel)
                        {
                            if (res.otherData != null)
                            {
                                panel1.Controls.Clear();
                                int num = 1, status = 0;
                                double amt;
                                UCItemCashIn ucITCI;
                                foreach (DataRow row in res.otherData.Rows)
                                {
                                    int.TryParse(row["CashOutStatus"].ToString(), out status);
                                    double.TryParse(row["PaymentAmt"].ToString(), out amt);
                                    if (status == 2 && amt > 0)
                                    {
                                        // add item to display
                                        ucITCI = new UCItemCashIn();
                                        ucITCI.lbNoText = num.ToString();
                                        ucITCI.lbChangeText = _strPayCash;
                                        ucITCI.lbChangeTypeText = row["PaymentCode"].ToString();
                                        ucITCI.lbExchangeRateText = "-";
                                        ucITCI.lbCurrencyText = ucDDCurrency.LabelText;
                                        ucITCI.lbInputDisplayText = amt.ToString(ProgramConfig.amountFormatString);
                                        ucITCI.lbAmtText = amt.ToString(ProgramConfig.amountFormatString);
                                        ucITCI.lbInputAmt = amt.ToString(ProgramConfig.amountFormatString);
                                        ucITCI.lbCashType = "S";
                                        ucITCI.paymentType = row["PaymentName"].ToString();
                                        ucITCI.btnDeleteVisible = false;
                                        panel1.Controls.Add(ucITCI);
                                        num++;
                                    }
                                }
                                RefreshGrid();
                            }
                            SummaryCashIn();

                            moneyBagNo = "";
                            lbMoneyBag.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbMoneyBag"), moneyBagNo);
                            //lbMoneyBag.Text = "เลขที่ซองเงิน: " + moneyBagNo;
                            lbMoneyBag.Visible = true;

                            ucDDCurrency.Enabled = true;
                            ucTBWI_Amt.Text = "";
                            lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
                            ucTBWI_Amt.label1.Visible = false;
                            ucTBWI_Amt.EnabledUC = true;

                            // TODO: change parameter code to "CashOutSaleAmtMethod". currently not available
                            string config2 = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashOut_SaleAmountMethod.parameterCode);
                            switch (config2)
                            {
                                case "1":
                                    this.EnabledBtnBankNote = false;
                                    ucTBWI_Amt.EnabledUC = true;
                                    break;
                                case "2":
                                    this.EnabledBtnBankNote = true;
                                    ucTBWI_Amt.EnabledUC = false;
                                    ucTBWI_Amt.IsTextChange = true;
                                    ucKeypad.ucTBWI = ucTBWI_Amt;
                                    break;
                                case "3":
                                    this.EnabledBtnBankNote = true;
                                    ucTBWI_Amt.EnabledUC = true;
                                    break;
                            }

                            DisableControl();
                            if (btnPayCash.Tag == null || (string)btnPayCash.Tag == "disable")
                            {
                                lbChange.Text = _strPayCash;
                                lbChange.Visible = true;
                                panelAmt.Visible = true;
                                btnPayCash.Tag = "enable";
                                ChangeButtonBackGround(btnPayCash);
                                btnPayCash.ForeColor = Color.White;
                                ucTBWI_Amt.SetSelection = true;
                            }
                            //Hardware.addDrawerListeners(DrawerStatusOpen);
                            Hardware.openDrawer();
                            process.SaveDrawerTrans(FunctionID.CashOut_Sale_OpenDrawerAndRecordTime);
                            if (Hardware.isDrawerOpen)
                            {
                                drawerOpenEvent();
                            }
                            Hardware.addDrawerListeners(DrawerStatusClose);
                        }
                        else
                        {
                            string responseMessage = ProgramConfig.message.get("frmMoneyBagInput", "SpecifyNumber").message;
                            string helpMessage = ProgramConfig.message.get("frmMoneyBagInput", "SpecifyNumber").help;
                            notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                            //notify = new frmNotify(ResponseCode.Error, "กรุณาระบุเลขที่ซองเงิน");
                            notify.ShowDialog(this);
                        }
                    }
                    else
                    {
                        frmLoading.closeLoading();
                        notify = new frmNotify(res);
                        notify.ShowDialog(this);
                        return;
                    }
                }
                else
                {
                    frmLoading.closeLoading();
                    notify = new frmNotify(res);
                    notify.ShowDialog(this);
                    this.Dispose();
                    return;
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                throw net;
                //frmLoading.closeLoading();
                //btnPayCash.Enabled = true;
                //btnNormalChange.Enabled = true;
                //Program.control.RetryConnection(net.errorType);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                btnPayCash.Enabled = true;
                btnNormalChange.Enabled = true;
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
                Program.control.CloseForm("frmNotify");
            }
        }

        private void ucDropDownList1_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList2();
            }

            #region Old Code
            //if (sender is UCDropDownList)
            //{
            //    var ucDDL = (UCDropDownList)sender;
            //    var ucDDL_Point = ucDDL.FindForm().PointToClient(ucDDL.Parent.PointToScreen(ucDDL.Location));
            //    if (pn_DropDown.Visible && pnt == ucDDL_Point)
            //    {
            //        pn_DropDown.Visible = !pn_DropDown.Visible;
            //    }
            //    else
            //    {
            //        pnt = ucDDL_Point;
            //        currentUCDDL = ucDDL;
            //        pn_DropDown.Width = ucDDL.Width;
            //        pn_DropDown.Location = new Point(ucDDL_Point.X, ucDDL.Height + ucDDL_Point.Y + 1);

            //        pn_DropDown.Controls.Clear();
            //        pn_DropDown.Visible = true;
            //        List<string> lstStr = new List<string>();

            //        lstStr = SetDataucDropDownList2();

            //        int h = 0;
            //        int ucH = 0;
            //        int cnt = 1;
            //        int temp = 0;
            //        int widthDD = pn_DropDown.Width;
            //        int widthLine = 0;
            //        int widthLabel = 0;

            //        string maxStr = lstStr.Select(s => s).OrderByDescending(o => o.Length).FirstOrDefault();

            //        UCItemDropDownList _ucdd = new UCItemDropDownList();
            //        Font font = _ucdd.label1.Font;
            //        widthLine = _ucdd.lineShape1.X2 - _ucdd.lineShape1.X1;
            //        widthLabel = _ucdd.Width;
            //        temp = TextRenderer.MeasureText(maxStr, font).Width;

            //        if (temp + 13 >= widthDD)
            //        {
            //            widthDD = temp + 13; // 13 คือส่วนต่างของ Size form width กับ Size label width >>>>> Form UCItemDropDownList
            //            widthLabel = temp;
            //        }

            //        if (temp >= widthLine)
            //        {
            //            widthLine = temp + _ucdd.lineShape1.X1;
            //        }
            //        else
            //        {
            //            widthLine = widthDD - 13;
            //        }

            //        foreach (string str in lstStr)
            //        {
            //            ucH += 33; // ความสูงของ item >>>>> UCItemDropDownList
            //            h += 33;

            //            UCItemDropDownList ucdd = new UCItemDropDownList();
            //            ucdd.UCItemDropDownListClick += UCItemDropDownListClick;
            //            ucdd.label1.Text = str;
            //            ucdd.label1.Width = widthLabel;
            //            ucdd.lineShape1.X2 = widthLine;

            //            if (cnt == 1)
            //            {
            //                ucdd.lineShape1.Visible = false;
            //            }

            //            cnt++;
            //            pn_DropDown.Controls.Add(ucdd);
            //        }

            //        if (ucH >= 198) // check ให้ item dropdown มีได้แค่ 6 ถ้ามากกว่านั้น จะมี scroll bar 
            //        {
            //            ucH = 198; // 198 คือ ส่วนสูงของ panel เมื่อมี item 6 ชิ้น
            //            widthDD = widthDD + (widthDD == pn_DropDown.Width ? 0 : 10); // + scorll bar ที่เพิ่มเข้ามา
            //        }

            //        if (widthDD > this.pn_DropDown.Width)
            //        {
            //            // set location ไปทางซ้าย
            //            pn_DropDown.Location = new Point(pn_DropDown.Location.X - (widthDD - pn_DropDown.Width), pn_DropDown.Location.Y);
            //        }

            //        pn_DropDown.Height = ucH + 3;
            //        pn_DropDown.Width = widthDD;
            //        pn_DropDown.BringToFront();
            //        pn_DropDown.Focus();
            //    }
            //}
            #endregion
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            List<Currency> lstCur = new List<Currency>();
            lstCur = ProgramConfig.currency.list();
            lstCur.Reverse();
            foreach (Currency currency in lstCur)
            {
                drItem.DisplayText = currency.code;
                drItem.ValueText = currency.code;
                lstStr.Add(drItem);
            }
            return lstStr;
        }

        private void setVisibleButtonPayment()
        {
            btnSubmit.Enabled = false;
            btnSubmit.BackgroundImage = Properties.Resources.btn_payment_disable;
        }

        private void ucTBWI_Amt_EnterFromButton(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            if (ucTBWI_Amt.Text != "")
            {
                double amount = 0.0;
                if (!double.TryParse(ucTBWI_Amt.Text, out amount))
                {
                    frmLoading.closeLoading();
                    string responseMessage = ProgramConfig.message.get("frmCashOut", "SpecifyAmount").message;
                    string helpMessage = ProgramConfig.message.get("frmCashOut", "SpecifyAmount").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "กรุณาระบุจำนวนเงิน");
                    notify.ShowDialog(this);
                    return;
                }

                //EXEC pos_CheckMinCashUnitAmount 1, '20201215', '00501', '003', '300003', 'N/A', 'C003000366', 'N/A', 'FXCU', 0, 'THB';
                StoreResult chkMinCash = process.checkMinCashUnitAmount(ProgramConfig.payment.getPaymentCode(ucDDCurrency.ValueText), ucTBWI_Amt.Text, ucDDCurrency.ValueText);
                if (!chkMinCash.response.next)
                {
                    frmNotify dialog = new frmNotify(ResponseCode.Error, chkMinCash.responseMessage, chkMinCash.helpMessage);
                    dialog.ShowDialog();
                }
                else
                {
                    if (ValidateData())
                    {
                        var a = ucDDCurrency.ValueText;
                        Currency crcy;
                        crcy = ProgramConfig.currency.getCurrency(ucDDCurrency.LabelText);

                        string ex_rate = "";
                        double ex_amt = 0;

                        ex_amt = process.CalAmount(Convert.ToDouble(ucTBWI_Amt.Text), ProgramConfig.payment.getPaymentCode(ucDDCurrency.ValueText), ucDDCurrency.ValueText);

                        if (ucDDCurrency.ValueText == defaultCurrency)
                        {
                            ex_rate = "1";
                        }
                        else
                        {
                            ex_rate = ProgramConfig.payment.getExchangeRate(ucDDCurrency.ValueText).ToString();
                        }
 
                        frmConfirmCashout cashOut = new frmConfirmCashout(ex_amt.ToString(ProgramConfig.amountFormatString), lbChange.Text);
                        var res = cashOut.ShowDialog(this);
                        //string defaultCurrency = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefualt.parameterCode).ToString();
                        if (res == System.Windows.Forms.DialogResult.Yes)
                        {
                            btnSubmit.Enabled = true;
                            btnSubmit.BackgroundImage = Properties.Resources.payment_enable;

                            string changeTypeText = ucDDCurrency.LabelText == defaultCurrency ? "CASH" : "FXCU";
                            var item = panel1.Controls.Cast<UCItemCashIn>()
                                .Where(w => w.lbCurrencyText == ucDDCurrency.LabelText.Trim() && w.lbChangeTypeText == changeTypeText);
                            if (item.Any())
                            {
                                UCItemCashIn itm = item.Select(s => s).FirstOrDefault();
                                var amtNew = ex_amt;
                                itm.lbAmtText = amtNew.ToString(ProgramConfig.amountFormatString);
                                itm.lbInputAmt = Convert.ToDouble(ucTBWI_Amt.Text).ToString(ProgramConfig.amountFormatString);
                                itm.lbInputDisplayText = Convert.ToDouble(ucTBWI_Amt.Text).ToString(ProgramConfig.amountFormatString);
                                this.Refresh();
                            }
                            else
                            {
                                frmLoading.showLoading();
                                string payMainName = process.selectPaymentByCode(changeTypeText).Rows[0]["PaymentMainName"].ToString();

                                int num;
                                if (panel1.Controls.Count > 0)
                                {
                                    num = panel1.Controls.Cast<UCItemCashIn>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
                                }
                                else
                                {
                                    num = 1;
                                }


                                UCItemCashIn ucITCI = new UCItemCashIn();
                                ucITCI.lbNoText = num.ToString();
                                ucITCI.DeleteClick += DeleteClick;
                                ucITCI.lbChangeText = lbChange.Text;
                                ucITCI.lbChangeTypeText = changeTypeText;
                                ucITCI.lbExchangeRateText = ex_rate;
                                ucITCI.lbCurrencyText = ucDDCurrency.LabelText;
                                ucITCI.lbInputDisplayText = Convert.ToDouble(ucTBWI_Amt.Text).ToString(ProgramConfig.amountFormatString);
                                ucITCI.lbAmtText = ex_amt.ToString(ProgramConfig.amountFormatString);
                                ucITCI.lbInputAmt = Convert.ToDouble(ucTBWI_Amt.Text).ToString(ProgramConfig.amountFormatString);
                                ucITCI.lbCashType = typeChange == 0 ? "F" : "S";
                                ucITCI.paymentType = payMainName;
                                panel1.Controls.Add(ucITCI);
                            }

                            if (moneyBagNo != "" || moneyBagNo != null)
                            {
                                lbMoneyBag.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbMoneyBag"), moneyBagNo);
                            }

                            if (!ProgramConfig.hasDrawer)
                            {
                                DrawerStatusClose("close");
                            }


                            SummaryCashIn();
                            //DisableControl();
                            //btnAdjustChange.Tag = "disable";
                            //btnNormalChange.Tag = "disable";
                            //btnPayCash.Tag = "disable";

                            // only 1 transaction
                            //btnNormalChange.Enabled = false;
                            //btnAdjustChange.Enabled = false;
                            btnPayCash.Enabled = false;

                            RefreshGrid();



                            //ucKeypad.ucTBWI = null;

                            //DisableControl();
                            //if (btnPayCash.Tag == null || (string)btnPayCash.Tag == "disable")
                            //{
                            //    lbChange.Text = _strPayCash;
                            //    lbChange.Visible = true;
                            //    panelAmt.Visible = true;
                            //    btnPayCash.Tag = "enable";
                            //    ChangeButtonBackGround(btnPayCash);
                            //    btnPayCash.ForeColor = Color.White;
                            //    ucTBWI_Amt.SetSelection = true;
                            //    ucTBWI_Amt.Text = "";
                            //}

                            //Utility.SetBackGroundBrightness(panel_right, pictureBox3, pictureBox5);
                        }
                        else
                        {
                            setVisibleButtonPayment();
                            //DisableControl();
                            //if (btnPayCash.Tag == null || (string)btnPayCash.Tag == "disable")
                            //{
                            //    lbChange.Text = _strPayCash;
                            //    lbChange.Visible = true;
                            //    panelAmt.Visible = true;
                            //    btnPayCash.Tag = "enable";
                            //    ChangeButtonBackGround(btnPayCash);
                            //    btnPayCash.ForeColor = Color.White;
                            //    ucTBWI_Amt.SetSelection = true;
                            //}
                        }
                    }
                }
            }
            else
            {
                frmLoading.closeLoading();
                string responseMessage = ProgramConfig.message.get("frmCashOut", "SpecifyAmount").message;
                string helpMessage = ProgramConfig.message.get("frmCashOut", "SpecifyAmount").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Error, "กรุณาระบุจำนวนเงิน");
                notify.ShowDialog(this);
                return;
            }
            frmLoading.closeLoading();
        }

        private bool ValidateData()
        {
            if (!ValidateDropDown())
            {
                return false;
            }

            double amt;
            string amt_text = ucTBWI_Amt.Text;
            if (!double.TryParse(amt_text, out amt))
            {
                string responseMessage = ProgramConfig.message.get("frmCashOut", "SpecifyAmount").message;
                string helpMessage = ProgramConfig.message.get("frmCashOut", "SpecifyAmount").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Error, "กรุณาระบุจำนวนเงิน");
                notify.ShowDialog(this);
                return false;
            }
            return true;        
        }

        private bool ValidateDropDown()
        {
            if (ucDDCurrency.ValueText == "")
            {
                string responseMessage = ProgramConfig.message.get("frmCashOut", "ChooseCurrency").message;
                string helpMessage = ProgramConfig.message.get("frmCashOut", "ChooseCurrency").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Error, "กรุณาเลือกสกุลเงิน");
                notify.ShowDialog(this);
                return false;
            }
            return true;
        }

        private void RefreshGrid()
        {
            List<UCItemCashIn> lstCashIn = new List<UCItemCashIn>();
            lstCashIn = panel1.Controls.Cast<UCItemCashIn>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panel1.Controls.Clear();
            int num = lstCashIn.Count;

            foreach (UCItemCashIn item in lstCashIn)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(225, 225, 225);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num.ToString();
                panel1.Controls.Add(item);
                num--;
            }
            ScrollToBottom(panel1);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void SummaryCashIn()
        {
            amount = 0;
            foreach (UCItemCashIn uc in panel1.Controls)
            {
                amount += Convert.ToDouble(uc.lbAmtText);
            }
            lbTxtSubtotal.Text = amount.ToString(ProgramConfig.amountFormatString);
        }

        public void DrawerStatusOpen(string status)
        {
            if (status.ToUpper() == "TRUE")
            {
                // drawer open event
                openTime = DateTime.Now;
                if (!process.saveOpenDrawer(openTime.ToString("dd-MM-yyyy HH:mm", cultureinfo)))
                {
                    string responseMessage = ProgramConfig.message.get("frmCashOut", "SaveCloseDrawerIncomplete").message;
                    string helpMessage = ProgramConfig.message.get("frmCashOut", "SaveCloseDrawerIncomplete").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถบันทึกเวลาเปิด cash drawer ได้");
                    notify.ShowDialog(this);
                }
            }
        }

        public void drawerOpenEvent()
        {
            openTime = DateTime.Now;
            if (!process.saveOpenDrawer(openTime.ToString("dd-MM-yyyy HH:mm", cultureinfo)))
            {
                string responseMessage = ProgramConfig.message.get("frmCashOut", "SaveCloseDrawerIncomplete").message;
                string helpMessage = ProgramConfig.message.get("frmCashOut", "SaveCloseDrawerIncomplete").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถบันทึกเวลาเปิด cash drawer ได้");
                notify.ShowDialog(this);
            }
        }

        public void DrawerStatusClose(string status)
        {
            if (status.ToUpper() == "FALSE")
            {
                // drawer close event
                //Save time Close Cash Drawer
                closeTime = DateTime.Now;

                // move to update after submit (cause no data insert in dlyptrans before submit)
                //if (!process.saveCashDrawerLog(openTime.ToString("HHmmss", cultureinfo), closeTime.ToString("HHmmss", cultureinfo), amount))
                //{
                //    frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง DLYPTRANS");
                //    dialog.ShowDialog(this);
                //    return;
                //}
                Program.control.CloseForm("frmNotify");
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            RefreshGrid();
            SummaryCashIn();
            //DisableControl();
            pictureBox3.Visible = false;
            pictureBox3.Image = null;
            lbMoneyBag.Visible = false;
            //btnPayCash.Enabled = true;
            //btnNormalChange.Enabled = true;
            setVisibleButtonPayment();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                //Open Cash Drawer
                //openTime = DateTime.Now.ToString("HH:mm:ss", cultureinfo);
                //Hardware.openDrawer();
                if (double.Parse(lbTxtSubtotal.Text) == 0)
                {
                    frmLoading.closeLoading();
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    notify.ShowDialog(this);
                    return;
                }


                foreach (UCItemCashIn item in panel1.Controls.Cast<UCItemCashIn>())
                {
                    double chkMaxCashoutValue = ProgramConfig.payment.getMaxCashout(item.lbChangeTypeText);

                    if (double.Parse(item.lbAmtText) > chkMaxCashoutValue)
                    {
                        frmLoading.closeLoading();
                        string responseMessage = ProgramConfig.message.get("CashOutProcess", "CashOutExceedLimit").message;
                        string helpMessage = ProgramConfig.message.get("CashOutProcess", "CashOutExceedLimit").help;
                        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                        //notify = new frmNotify(ResponseCode.Error, "ยอดเงินที่ส่งเกินกว่าค่าส่งเงินสูงสุดที่กำหนด");
                        notify.ShowDialog(this);
                        return;
                    }
                }

                if (ProgramConfig.hasDrawer)
                {
                    #region normal
                    if (!Hardware.isDrawerOpen)
                    {
                        DataTable data = new DataTable();
                        data.Columns.Add("PaymentCode");
                        data.Columns.Add("PaymentType");
                        data.Columns.Add("CurrencyCode");
                        data.Columns.Add("ExchangeRate");
                        data.Columns.Add("InputAmt");
                        data.Columns.Add("MoneyBag");
                        data.Columns.Add("Amt");
                        data.Columns.Add("PaymentChangeType");

                        DataRow row;
                        foreach (UCItemCashIn item in panel1.Controls.Cast<UCItemCashIn>())
                        {
                            row = data.NewRow();
                            row["PaymentCode"] = item.lbChangeTypeText;
                            row["PaymentType"] = item.paymentType;
                            row["CurrencyCode"] = item.lbCurrencyText;
                            row["ExchangeRate"] = item.lbExchangeRateText;
                            row["InputAmt"] = item.lbInputAmt;
                            row["Amt"] = item.lbAmtText;
                            if (item.lbChangeTypeText == "CASH" || item.lbChangeTypeText == "FXCU")
                            {
                                row["MoneyBag"] = moneyBagNo;
                            }
                            else
                            {
                                row["MoneyBag"] = "";
                            }
                            row["PaymentChangeType"] = item.lbChangeText;

                            data.Rows.Add(row);
                        }

                        if (!process.saveCashoutSale(data, openTime.ToString("HHmmss", cultureinfo), closeTime.ToString("HHmmss", cultureinfo), amount, typeChange, banknoteList))
                        {
                            frmLoading.closeLoading();
                            string responseMessage = ProgramConfig.message.get("frmCashOut", "SaveCashOutIncomplete").message;
                            string helpMessage = ProgramConfig.message.get("frmCashOut", "SaveCashOutIncomplete").help;
                            notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                            //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลส่งเงินการขาย");
                            notify.ShowDialog(this);
                            return;
                        }

                        //Profile check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_PrintCashoutDocument);
                        //if (!check.found || check.policy == PolicyStatus.Work)
                        //{
                        //    if (typeChange == 0)
                        //    {
                        //        StoreResult res = process.printCashOutNormalChange();
                        //        if (res.response.next)
                        //        {
                        //            Hardware.printTermal(res.otherData);
                        //        }
                        //        else
                        //        {
                        //            frmLoading.closeLoading();
                        //            notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                        //            notify.ShowDialog(this);
                        //            return;
                        //        }
                        //    }
                        //    else if (typeChange == 2)
                        //    {
                        //        StoreResult res = process.printCashout();
                        //        if (res.response.next)
                        //        {
                        //            Hardware.printTermal(res.otherData);
                        //        }
                        //        else
                        //        {
                        //            frmLoading.closeLoading();
                        //            notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                        //            notify.ShowDialog(this);
                        //            return;
                        //        }
                        //    }
                        //}

                        ProcessResult result = process.submitCashout(typeChange);
                        if (result.response.next)
                        {
                            moneyBagNo = "";
                            lbMoneyBag.Text = "";
                            lbMoneyBag.Visible = false;

                            string responseMessage = ProgramConfig.message.get("frmCashOut", "SaveComplete").message;
                            string helpMessage = ProgramConfig.message.get("frmCashOut", "SaveComplete").help;
                            notify = new frmNotify(ResponseCode.Success, responseMessage, helpMessage);

                            //notify = new frmNotify(ResponseCode.Success, "บันทึกส่งเงินเรียบร้อยแล้ว");
                            notify.ShowDialog(this);

                            Profile check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_ConfirmCloseTill);
                            if (check.policy == PolicyStatus.Work)
                            {
                                foreach (Form item in Application.OpenForms)
                                {
                                    if (item is frmMainMenu)
                                    {
                                        frmMain = (frmMainMenu)item;
                                        frmMain.BringToFront();
                                        frmMain.btnCloseCashier_Click(this, null);
                                        break;
                                    }
                                }
                            }
                            this.Dispose();
                        }
                        else
                        {
                            frmLoading.closeLoading();
                            notify = new frmNotify(result);
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                    else
                    {
                        frmLoading.closeLoading();
                        string responseMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").message;
                        string helpMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").help;
                        notify = new frmNotify(ResponseCode.CloseDrawer, responseMessage, helpMessage);

                        //notify = new frmNotify(ResponseCode.Success, "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำรายการต่อ");
                        notify.btnOK.Visible = false;
                        notify.ShowDialog(this);
                        btnSubmit_Click(sender, e);
                    }
                    #endregion
                }
                else
                {
                    #region test
                    DataTable data = new DataTable();
                    data.Columns.Add("PaymentCode");
                    data.Columns.Add("PaymentType");
                    data.Columns.Add("CurrencyCode");
                    data.Columns.Add("ExchangeRate");
                    data.Columns.Add("InputAmt");
                    data.Columns.Add("MoneyBag");
                    data.Columns.Add("Amt");
                    data.Columns.Add("PaymentChangeType");

                    DataRow row;
                    foreach (UCItemCashIn item in panel1.Controls.Cast<UCItemCashIn>())
                    {
                        row = data.NewRow();
                        row["PaymentCode"] = item.lbChangeTypeText;
                        row["PaymentType"] = item.paymentType;
                        row["CurrencyCode"] = item.lbCurrencyText;
                        row["ExchangeRate"] = item.lbExchangeRateText;
                        row["InputAmt"] = item.lbInputAmt;
                        row["Amt"] = item.lbAmtText;
                        if (item.lbChangeTypeText == "CASH" || item.lbChangeTypeText == "FXCU")
                        {
                            row["MoneyBag"] = moneyBagNo;
                        }
                        else
                        {
                            row["MoneyBag"] = "";
                        }
                        row["PaymentChangeType"] = item.lbChangeText;

                        data.Rows.Add(row);
                    }

                    if (!process.saveCashoutSale(data, openTime.ToString("HHmmss", cultureinfo), closeTime.ToString("HHmmss", cultureinfo), amount, typeChange, banknoteList))
                    {
                        frmLoading.closeLoading();
                        string responseMessage = ProgramConfig.message.get("frmCashOut", "SaveCashOutIncomplete").message;
                        string helpMessage = ProgramConfig.message.get("frmCashOut", "SaveCashOutIncomplete").help;
                        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                        //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลส่งเงินการขาย");
                        notify.ShowDialog(this);
                        return;
                    }

                    //Profile check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_PrintCashoutDocument);
                    //if (!check.found || check.policy == PolicyStatus.Work)
                    //{
                    //    if (typeChange == 0)
                    //    {
                    //        StoreResult res = process.printCashOutNormalChange();
                    //        if (res.response.next)
                    //        {
                    //            Hardware.printTermal(res.otherData);
                    //        }
                    //        else
                    //        {
                    //            frmLoading.closeLoading();
                    //            notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                    //            notify.ShowDialog(this);
                    //            return;
                    //        }
                    //    }
                    //    else if (typeChange == 2)
                    //    {
                    //        StoreResult res = process.printCashout();
                    //        if (res.response.next)
                    //        {
                    //            Hardware.printTermal(res.otherData);
                    //        }
                    //        else
                    //        {
                    //            frmLoading.closeLoading();
                    //            notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                    //            notify.ShowDialog(this);
                    //            return;
                    //        }
                    //    }
                    //}

                    ProcessResult result = process.submitCashout(typeChange);
                    if (result.response.next)
                    {
                        moneyBagNo = "";
                        lbMoneyBag.Text = "";
                        lbMoneyBag.Visible = false;

                        string responseMessage = ProgramConfig.message.get("frmCashOut", "SaveComplete").message;
                        string helpMessage = ProgramConfig.message.get("frmCashOut", "SaveComplete").help;
                        notify = new frmNotify(ResponseCode.Success, responseMessage, helpMessage);

                        //notify = new frmNotify(ResponseCode.Success, "บันทึกส่งเงินเรียบร้อยแล้ว");
                        notify.ShowDialog(this);

                        Profile check = ProgramConfig.getProfile(FunctionID.CashOut_Sale_ConfirmCloseTill);
                        if (check.policy == PolicyStatus.Work)
                        {
                            foreach (Form item in Application.OpenForms)
                            {
                                if (item is frmMainMenu)
                                {
                                    frmMain = (frmMainMenu)item;
                                    frmMain.BringToFront();
                                    frmMain.btnCloseCashier_Click(this, null);
                                    break;
                                }
                            }
                        }
                        this.Dispose();
                    }
                    else
                    {
                        frmLoading.closeLoading();
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }
                    #endregion
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                throw net;
                //frmLoading.closeLoading();
                //Program.control.RetryConnection(net.errorType);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void ucAdjustCash1_EnterFromButton(object sender, EventArgs e)
        {
            UCAdjustCash txtBox = (UCAdjustCash)sender;
            foreach (Control ctrl in pn_MainBankNote.Controls)
            {
                if (ctrl is Label)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        UCTextBoxSmall tbs = txtBox.Controls.Find("ucTextBoxSmall" + i, true).FirstOrDefault() as UCTextBoxSmall;
                        Label lb = pn_MainBankNote.Controls.Find("lbQty" + i, true).FirstOrDefault() as Label; 
                        if ((ctrl.Tag + "").Trim() != "" && tbs.Tag + "" == ctrl.Tag + "")
                        {
                            lb.Text = tbs.Text.Trim();
                            ctrl.Text = String.Format("{0}", (Convert.ToDouble(tbs.Text.Trim() == "" ? "0" : tbs.Text.Trim()) * Convert.ToDouble(tbs.Tag + "")).ToString());
                            lbTxtTotalCash.Text = String.Format("{0}", (SumAmountTotal()).ToString());

                            break;
                        }
                        //total += Convert.ToDouble(txtbox.Text.Trim() == "" ? "0" : txtbox.Text.Trim());
                    }
                }
            }
        }

        private void ucAdjustCash1_EnterContinueButton(object sender, EventArgs e)
        {
            ucTBWI_Amt.Text = String.Format("{0}", SumAmountTotal().ToString(ProgramConfig.amountFormatString));
            ucKeypad.ucTBWI = ucTBWI_Amt;
            getBanknoteList();
            CloseBankNote();

            if (double.Parse(ucTBWI_Amt.Text) == 0)
            {
                //ucTBWI_Amt.Text = "0";
                ucTBWI_Amt.SetSelection = true;
                //ucTBWI_Amt.EnabledUC = true;
                ucTBWI_Amt.Focus();
            }
        }

        public void CloseBankNote()
        {
            pn_MainChange.BringToFront();
            pnNoteBank.Visible = false;
            ClearBankNote();
            pictureBox2.Visible = false;
            ucKeypad.ucTBS = null;
        }

        private double SumAmountTotal()
        {
            double total = 0.0;
            for (int i = 1; i <= 12; i++)
            {
                Label lb = pn_MainBankNote.Controls.Find("lbAmount" + i, true).FirstOrDefault() as Label;
                if (lb.Text != "")
                    total += Convert.ToDouble(lb.Text);
            }
            return total;
        }

        private void getBanknoteList()
        {
            banknoteList = new List<banknote>();
            Label node;
            string valueText, amtText;
            double value, amt;
            string lbAmountName = "lbAmount{0}";
            for (int i = 1; i <= 12; i++)
            {
                node = (Label)pn_MainBankNote.Controls.Find(string.Format(lbAmountName, i), true).FirstOrDefault();
                valueText = node.Tag.ToString();
                amtText = node.Text;
                amt = value = 0;
                if (double.TryParse(amtText, out amt) && double.TryParse(valueText, out value))
                {
                    if (value > 0 && amt > 0)
                    {
                        banknoteList.Add(new banknote(value, Convert.ToInt32(amt / value)));
                    }
                }
            }

            if (banknoteList.Count == 0)
            {
                banknoteList = null;
            }
        }

        private void btnNoteBank_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            if (ValidateDropDown())
            {
                setVisibleButtonPayment();                
                //Class1.SetFormAndArrow(pictureBox5, ucTBWI_Amt, panelMainSell, pnNoteBank);
                //pictureBox5.Parent = pictureBox2;
                //pictureBox5.BringToFront();

                ClearBankNote();
                LoadBankNote();
            }
           
            frmLoading.closeLoading();
        }

        private void LoadBankNote()
        {
            StoreResult result;

            result = process.getCashUnit(ucDDCurrency.LabelText);
            if (!result.response.next)
            {
                notify = new frmNotify(result);
                notify.ShowDialog(this);
                return;
            }
            Utility.SetBackGroundBrightness(panelMainSell, pictureBox2, pictureBox5);
            ucAdjustCash1.InitialBankNote(result.otherData);
            InitialPanelBankNote(result.otherData);
            pn_MainBankNote.BringToFront();
            pnNoteBank.Visible = true;
            pnNoteBank.BringToFront();
            ucKeypad.ucTBWI = null;
            //ucTBWI_Amt.EnabledUC = false;         
        }

        private void InitialPanelBankNote(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string amt = Convert.ToDouble(dr["UnitAmt"]).ToString();
                switch (dr["Seq"].ToString())
                {
                    case "1":
                        lbBankNote1.Text = amt;
                        lbAmount1.Tag = amt;
                        lbAmount1.Text = "0";
                        lbQty1.Text = "0";
                        break;
                    case "2":
                        lbBankNote2.Text = amt;
                        lbAmount2.Tag = amt;
                        lbAmount2.Text = "0";
                        lbQty2.Text = "0";
                        break;
                    case "3":
                        lbBankNote3.Text = amt;
                        lbAmount3.Tag = amt;
                        lbAmount3.Text = "0";
                        lbQty3.Text = "0";
                        break;
                    case "4":
                        lbBankNote4.Text = amt;
                        lbAmount4.Tag = amt;
                        lbAmount4.Text = "0";
                        lbQty4.Text = "0";
                        break;
                    case "5":
                        lbBankNote5.Text = amt;
                        lbAmount5.Tag = amt;
                        lbAmount5.Text = "0";
                        lbQty5.Text = "0";
                        break;
                    case "6":
                        lbBankNote6.Text = amt;
                        lbAmount6.Tag = amt;
                        lbAmount6.Text = "0";
                        lbQty6.Text = "0";
                        break;
                    case "7":
                        lbBankNote7.Text = amt;
                        lbAmount7.Tag = amt;
                        lbAmount7.Text = "0";
                        lbQty7.Text = "0";
                        break;
                    case "8":
                        lbBankNote8.Text = amt;
                        lbAmount8.Tag = amt;
                        lbAmount8.Text = "0";
                        lbQty8.Text = "0";
                        break;
                    case "9":
                        lbBankNote9.Text = amt;
                        lbAmount9.Tag = amt;
                        lbAmount9.Text = "0";
                        lbQty9.Text = "0";
                        break;
                    case "10":
                        lbBankNote10.Text = amt;
                        lbAmount10.Tag = amt;
                        lbAmount10.Text = "0";
                        lbQty10.Text = "0";
                        break;
                    case "11":
                        lbBankNote11.Text = amt;
                        lbAmount11.Tag = amt;
                        lbAmount11.Text = "0";
                        lbQty11.Text = "0";
                        break;
                    case "12":
                        lbBankNote12.Text = amt;
                        lbAmount12.Tag = amt;
                        lbAmount12.Text = "0";
                        lbQty12.Text = "0";
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearBankNote()
        {
            for (int i = 1; i <= 12; i++)
            {
                Label lbAmount = pn_MainBankNote.Controls.Find("lbAmount" + i, true).FirstOrDefault() as Label;
                lbAmount.Text = "";
                lbAmount.Tag = "";

                Label lbNotBank = pn_MainBankNote.Controls.Find("lbBankNote" + i, true).FirstOrDefault() as Label;
                lbNotBank.Text = "";
                lbNotBank.Tag = "";

                Label lbQty = pn_MainBankNote.Controls.Find("lbQty" + i, true).FirstOrDefault() as Label;
                lbQty.Text = "";
                lbQty.Tag = "";
            }
            lbTxtTotalCash.Text = "0.00";
        }

        private void frmCashout_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hardware.clearDrawerListeners();
        }

        private void ucDDCurrency_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            //var ddl = (UCDropDownList)sender;
            //lb_ExchangeRate.Text = GetExchangeRate(ddl.ValueText);
            //ucTBWI_Amt.Text = "";

            var ddl = (UCDropDownList)sender;
            Type type = typeof(FunctionID);
            PropertyInfo pInfo;
            lb_ExchangeRate.Text = GetExchangeRate(ddl.ValueText);
            if (typeChange == 0)
            {
                pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashOut_LoanAmountDefault", ddl.UCddlLbText.Text));
            }
            else
            {
                pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashOut_SaleAmountDefault", ddl.UCddlLbText.Text));
            }
            var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
            string defaultCurrencyAmt = ProgramConfig.getPosConfig(paraCode).ToString();
            ucTBWI_Amt.Text = defaultCurrencyAmt;
            ucTBWI_Amt.SetSelection = true;
        }

        private void ucHeader2_LanguageClick(object sender, EventArgs e)
        {
            ucAdjustCash1.SendToBackLabel();
            GenerateMenu();
            ChangeButtonBackGround(btnNormalChange);
            ChangeButtonBackGround(btnAdjustChange);
            ChangeButtonBackGround(btnPayCash);
            lbMoneyBag.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbMoneyBag"), moneyBagNo);
            //ucDDCurrency.ValueText = "";
        }

        private void lbNormalChange_TextChanged(object sender, EventArgs e)
        {
            _strNormalChange = lbNormalChange.Text;
            if (typeChange == 0)
            {
                lbChangeText = lbNormalChange.Text;
            }
            else if (typeChange == 1)
            {
                lbChangeText = lbAdjustChange.Text;
            }
            else
            {
                lbChangeText = lbPayCash.Text;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            CloseBankNote();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lbAdjustChange_TextChanged(object sender, EventArgs e)
        {
            _strAdjustChange = lbAdjustChange.Text;
            if (typeChange == 0)
            {
                lbChangeText = lbNormalChange.Text;
            }
            else if (typeChange == 1)
            {
                lbChangeText = lbAdjustChange.Text;
            }
            else
            {
                lbChangeText = lbPayCash.Text;
            }
        }

        private void ChangeButtonBackGround(Button button)
        {
            if (button.Location == new Point(7, 60))
            {
                //if (button.Enabled)
                //{
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left_white;
                    }
                //}
            }
            else if (button.Location == new Point(165, 60))
            {
                //if (button.Enabled)
                //{
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right_white;
                    }
                //}
            }
            else if (button.Location == new Point(7, 131))
            {
                //if (button.Enabled)
                //{
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left_white;
                    }
                //}
            }
            else if (button.Location == new Point(165, 131))
            {
                //if (button.Enabled)
                //{
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right_white;
                    }
                //}
            }
        }

        private void lbPayCash_TextChanged(object sender, EventArgs e)
        {
            _strPayCash = lbPayCash.Text;
            if (typeChange == 0)
            {
                lbChangeText = lbNormalChange.Text;
            }
            else if (typeChange == 1)
            {
                lbChangeText = lbAdjustChange.Text;
            }
            else
            {
                lbChangeText = lbPayCash.Text;
            }
        }

        private void ucHeader2_MainMenuClick(object sender, EventArgs e)
        {
            if (Hardware.isDrawerOpen)
            {
                string responseMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawerTomainMenu").message;
                string helpMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawerTomainMenu").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Error, "กรุณาปิดลิ้นชักให้เรียบร้อยก่อนกลับหน้าเมนูหลัก");
                notify.ShowDialog(this);
            }
            else
            {
                Program.control.ShowForm("frmMainMenu");
                Program.control.CloseForm(this.Name);
            }
        }

        private void ucTBWI_Amt_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTBWI_Amt_EnterFromButton(sender, e);
        }

        private void ucKeypad_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

    }
}
