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
    public partial class frmCashbalance : Form
    {
        DateTime openTime;
        DateTime closeTime;
        private CashInProcess process = new CashInProcess();
        private int typeChange;
        private string _strNormalChange = "";
        private string _strAdjustChange = "";
        string defaultCurrency;

        public string lbChangeText
        {
            get{ return lbChange.Text; }
            set
            {
                if (typeChange == 0)
                {
                    _strNormalChange = value;
                    lbChange.Text = _strNormalChange;
                }
                else
                {
                    _strAdjustChange = value;
                    lbChange.Text = _strAdjustChange;
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
     
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public frmCashbalance()
        {
            InitializeComponent();
        }

        private void frmCashbalance_Load(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                label4.Text = ProgramConfig.currencyDefault;
                CheckIsEnablePaymentBtn();
                _strNormalChange = lbNormalChange.Text;
                _strAdjustChange = lbAdjustChange.Text;
                lbTxtLockNo.Text = ProgramConfig.tillNo;
                lbTxtAsOfDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm", cultureinfo);

                defaultCurrency = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefault.parameterCode).ToString();
                ucDDCurrency.LabelText = defaultCurrency;
                ucDDCurrency.ValueText = defaultCurrency;

                ProgramConfig.flagDrawer = true;
                StoreResult result = null;
                panel1.Controls.Clear();
                pn_MainChange.BringToFront();

                Profile chkMCashier = ProgramConfig.getProfile(FunctionID.CashIn_GetMessageCashier);
                if (chkMCashier.policy == PolicyStatus.Work)
                {
                    ProcessResult res = process.cashireMessageStatus();
                    if (res.response.next)
                    {
                        if (res.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                        }

                        if (res.needNextProcess)
                        {
                            ucHeader2.alertStatus = true;
                        }
                        else
                        {
                            ucHeader2.alertStatus = false;
                        }
                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        this.Dispose();
                        return;
                    }
                }

                result = GenerateMenu();
                if (!result.response.next)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    this.Dispose();
                    return;
                }

                result = process.posDisplayContent();
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

                result = process.getRunning(FunctionID.CashIn_GetRunningNo);
                if (!result.response.next)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    return;
                }

                ProgramConfig.cashInRefNo = result.otherData.Rows[0]["ReferenceNo"] + "";
                ProgramConfig.cashInRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"] + "";

                DisableControl();
                btnAdjustChange.Tag = "disable";
                btnNormalChange.Tag = "disable";
                btnNormalChange_Click(sender, e);
                this.ActiveControl = ucTBWI_Amt.TextBox;
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
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

        private void CheckIsEnablePaymentBtn()
        {
            if (panel1.Controls.Count > 0)
            {
                btnPayment.Enabled = true;
                btnPayment.BackgroundImage = Properties.Resources.confirm_enable;
            }
            else
            {
                btnPayment.Enabled = false;
                btnPayment.BackgroundImage = Properties.Resources.confirm_disable;
            }
        }

        private StoreResult GenerateMenu()
        {
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
                        if (dr["functionid"] + "" == FunctionID.CashIn_NormalChange.formatValue)
                        {
                            currentBtn = btnNormalChange;
                        }
                        else if (dr["functionid"] + "" == FunctionID.CashIn_AdditionChange.formatValue)
                        {
                            currentBtn = btnAdjustChange;
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
                                        currentBtn.Enabled = true;
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
                                        currentBtn.Enabled = true;
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
                                        currentBtn.Enabled = true;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_left_white;
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
                                        currentBtn.Enabled = true;
                                        currentBtn.BackgroundImage = Properties.Resources.cashin_top_right_white;
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

        private void DisableButton(DataTable dataTable)
        {
            if (dataTable.Select("Seq = '1'").Count() == 0)
            {

            }
        }

        private void btnNormalChange_Click(object sender, EventArgs e)
        {
            typeChange = 0;
            string cfAmtType = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_FloatAmountType.parameterCode);
            switch (cfAmtType)
            {
                case "1":
                    ucDDCurrency.Enabled = false;
                    string defaultAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_FloatAmountDefault.parameterCode);
                    ucTBWI_Amt.Text = defaultAmt;
                    ucTBWI_Amt.label1.Visible = false;                
                    break;
                case "2":
                    ucDDCurrency.Enabled = true;
                    Type type = typeof(FunctionID);
                    var pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_FloatAmountDefault", ucDDCurrency.ValueText));
                    var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
                    string defaultCurrencyAmt = ProgramConfig.getPosConfig(paraCode).ToString();
                    ucTBWI_Amt.Text = defaultCurrencyAmt;
                    lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
                    ucTBWI_Amt.label1.Visible = false;
                    string config2 = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_FloatAmountMethod.parameterCode);
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
       
            Profile check = ProgramConfig.getProfile(FunctionID.CashIn_NormalChange_DefalutAmount_Editable);

            switch (check.policy)
            {
                case PolicyStatus.NotCheck :
                    break;
                case PolicyStatus.Skip :
                    ucTBWI_Amt.EnabledUC = false;
                    this.EnabledBtnBankNote = false;
                    ucTBWI_Amt.IsTextChange = true;
                    ucTBWI_Amt.FocusWithDisable = true;
                    ucKeypad.ucTBWI = ucTBWI_Amt;
                    break;
                case PolicyStatus.Work :
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
        }

        private void DisableControl()
        {
            btnNormalChange.Tag = "disable";
            btnAdjustChange.Tag = "disable";

            ChangeButtonBackGround(btnAdjustChange);
            ChangeButtonBackGround(btnNormalChange);
            panelAmt.Visible = false;
            lbChange.Visible = false;
            btnNormalChange.ForeColor
                = btnAdjustChange.ForeColor
                = Color.Gray;

            //ucDDCurrency.LabelText 
            //    = ucDDCurrency.DisplayText
            //    = AppMessage.getMessage(ProgramConfig.language, "frmCashbalance", "UCddlLbText");
            //ucDDCurrency.ValueText = defaultCurrency;
            //lb_ExchangeRate.Text = "0.0000";
            //ucTBWI_Amt.Text = "";
        }

        private void btnAdjustChange_Click(object sender, EventArgs e)
        {
            typeChange = 1;
            string cfAmtType = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountType.parameterCode);
            switch (cfAmtType)
            {
                case "1":
                    ucDDCurrency.Enabled = false;
                    string defaultAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountDefault.parameterCode);
                    ucTBWI_Amt.Text = defaultAmt;
                    ucTBWI_Amt.label1.Visible = false;
                    break;
                case "2":
                    Type type = typeof(FunctionID);
                    var pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_LoanAmountDefault", ucDDCurrency.LabelText));
                    var paraCode = ((FunctionID)pInfo.GetValue(pInfo, null)).parameterCode;
                    string defaultCurrencyAmt = ProgramConfig.getPosConfig(paraCode).ToString();
                    ucTBWI_Amt.Text = defaultCurrencyAmt;
                    lb_ExchangeRate.Text = GetExchangeRate(ucDDCurrency.ValueText);
                    ucTBWI_Amt.label1.Visible = false;
                    string config2 = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountMethod.parameterCode);
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
                    break;
            }

            Profile check = ProgramConfig.getProfile(FunctionID.CashIn_AdditionChange_DefalutAmount_Editable);

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
                    break;
            }        

            DisableControl();
            if (btnAdjustChange.Tag == null || (string)btnAdjustChange.Tag == "disable")
            {
                lbChange.Text = _strAdjustChange;
                lbChange.Visible = true;
                panelAmt.Visible = true;
                btnAdjustChange.Tag = "enable";
                ChangeButtonBackGround(btnAdjustChange);
                btnAdjustChange.ForeColor = Color.White;
                ucTBWI_Amt.SetSelection = true;
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

        private void ucTBWI_Amt_EnterFromButton(object sender, EventArgs e)
        {
            
            frmLoading.showLoading();
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
                    Profile checkMin = typeChange == 0 ? ProgramConfig.getProfile(FunctionID.CashIn_NormalChange_CheckAmount_Minimum) : ProgramConfig.getProfile(FunctionID.CashIn_AdditionChange_CheckAmount_Minimum);
                    Profile checkMax = typeChange == 0 ? ProgramConfig.getProfile(FunctionID.CashIn_NormalChange_CheckAmount_Maximum) : ProgramConfig.getProfile(FunctionID.CashIn_AdditionChange_CheckAmount_Maximum);
                    Currency crcy;
                    double amt;
                    StoreResult result = null;
                    StoreResult result2 = null;
                    string cfAmtType = typeChange == 0 ? (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_FloatAmountType.parameterCode) : (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CashIn_LoanAmountType.parameterCode);

                    //Enum.TryParse<Currency>(ucDDCurrency.LabelText, out crcy);
                    crcy = ProgramConfig.currency.getCurrency(ucDDCurrency.LabelText);
                    double.TryParse(((UCTextBoxWithIcon)sender).InpTxt, out amt);

                    switch (cfAmtType)
                    {
                        case "1":
                            switch (checkMin.policy)
                            {
                                case PolicyStatus.NotCheck:
                                    break;
                                case PolicyStatus.Skip:
                                    break;
                                case PolicyStatus.Work:
                                    result = process.checkMinChange(amt, typeChange);
                                    break;
                            }

                            switch (checkMax.policy)
                            {
                                case PolicyStatus.NotCheck:
                                    break;
                                case PolicyStatus.Skip:
                                    break;
                                case PolicyStatus.Work:
                                    result2 = process.checkMaxChange(amt, typeChange);
                                    break;
                            }
                            break;

                        case "2":
                            switch (checkMin.policy)
                            {
                                case PolicyStatus.NotCheck:
                                    break;
                                case PolicyStatus.Skip:
                                    break;
                                case PolicyStatus.Work:

                                    result = process.checkMinChange(amt, crcy, typeChange);
                                    break;
                            }

                            switch (checkMax.policy)
                            {
                                case PolicyStatus.NotCheck:
                                    break;
                                case PolicyStatus.Skip:
                                    break;
                                case PolicyStatus.Work:
                                    result2 = process.checkMaxChange(amt, crcy, typeChange);
                                    break;
                            }
                            break;
                    }

                    if (result != null)
                    {
                        if (!result.response.next)
                        {
                            frmLoading.closeLoading();
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                            return;
                        }
                    }

                    if (result2 != null)
                    {
                        if (!result2.response.next)
                        {
                            frmLoading.closeLoading();
                            frmNotify dialog = new frmNotify(result2);
                            dialog.ShowDialog(this);
                            return;
                        }
                    }

                    //double ex_rate = Convert.ToDouble(lb_ExchangeRate.Text);
                    //double ex_amt = Convert.ToDouble(ucTBWI_Amt.Text) * ex_rate;

                    string ex_rate = "";
                    double ex_amt = 0;// Convert.ToDouble(ucTBWI_Amt.Text) * ex_rate;

                    //if (ucDDCurrency.ValueText != defaultCurrency)
                    //{
                    //    //StoreResult resExRatet = process.getAmountExchangeRate(ucTBWI_Amt.Text, "2", defaultCurrency);
                    //    StoreResult resExRate = process.getAmountExchangeRate(ucTBWI_Amt.Text, "2", ucDDCurrency.ValueText, ProgramConfig.payment.getPaymentCode(ucDDCurrency.ValueText));

                    //    if (resExRate.otherData != null && resExRate.otherData.Rows.Count > 0)
                    //    {
                    //        ex_amt = Convert.ToDouble(resExRate.otherData.AsEnumerable().Where(w => w["PaymentSubCode"].ToString() == ucDDCurrency.ValueText).Select(s => s["Total"].ToString()).FirstOrDefault());
                    //        ex_rate = resExRate.otherData.AsEnumerable().Where(w => w["PaymentSubCode"].ToString() == ucDDCurrency.ValueText).Select(s => s["RATE"].ToString()).FirstOrDefault();
                    //    }
                    //}
                    //else
                    //{
                    //    ex_rate = "1";
                    //    ex_amt = Convert.ToDouble(ucTBWI_Amt.Text);
                    //}

                    ex_amt = process.CalAmount(Convert.ToDouble(ucTBWI_Amt.Text), ProgramConfig.payment.getPaymentCode(ucDDCurrency.ValueText), ucDDCurrency.ValueText);

                    if (ucDDCurrency.ValueText == defaultCurrency)
                    {
                        ex_rate = "1";
                    }
                    else
                    {
                        ex_rate = ProgramConfig.payment.getExchangeRate(ucDDCurrency.ValueText).ToString();
                    }

                    ucFooterTran1.lbFunction.Text = typeChange == 0 ? FunctionID.CashIn_NormalChange_PopupSummaryPage.formatValue : FunctionID.CashIn_AdditionChange_PopupSummaryPage.formatValue;
                    frmConfirmCashIn cashIn = new frmConfirmCashIn(ex_amt.ToString(ProgramConfig.amountFormatString), lbChange.Text);
                    var res = cashIn.ShowDialog(this);
                    //string defaultCurrency = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefualt.parameterCode).ToString();
                    if (res == DialogResult.Yes)
                    {
                        string cashType = typeChange == 0 ? "F" : "L";
                        var item = panel1.Controls.Cast<UCItemCashIn>()
                            .Where(w => w.lbCashType == cashType && w.lbCurrencyText == ucDDCurrency.LabelText.Trim());
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
                            ucITCI.lbChangeTypeText = ucDDCurrency.LabelText == defaultCurrency ? "CASH" : "FXCU";
                            ucITCI.lbExchangeRateText = ex_rate.ToString();
                            ucITCI.lbCurrencyText = ucDDCurrency.LabelText;
                            ucITCI.lbInputDisplayText = Convert.ToDouble(ucTBWI_Amt.Text).ToString(ProgramConfig.amountFormatString);
                            ucITCI.lbAmtText = ex_amt.ToString(ProgramConfig.amountFormatString);
                            ucITCI.lbInputAmt = Convert.ToDouble(ucTBWI_Amt.Text).ToString(ProgramConfig.amountFormatString);
                            ucITCI.lbCashType = cashType;
                            panel1.Controls.Add(ucITCI);
                        }
                    }
                    SummaryCashIn();
                    RefreshGrid();
                    //ucKeypad.ucTBWI = null;
                    ucFooterTran1.lbFunction.Text = typeChange == 0 ? FunctionID.CashIn_NormalChange_ShowChangeDetail.formatValue : FunctionID.CashIn_AdditionChange_ShowChangeDetail.formatValue;
                    //102-030-007-000-000
                }
                CheckIsEnablePaymentBtn();
            }
            frmLoading.closeLoading();
        }

        private bool ValidateData()
        {
            if (!ValidateDropDown())
            {
                return false;
            }

            return true;
        }

        private bool ValidateDropDown()
        {
            if (ucDDCurrency.ValueText == "")
            {
                string responseMessage = ProgramConfig.message.get("frmCashbalance", "ChooseCurrency").message;
                string helpMessage = ProgramConfig.message.get("frmCashbalance", "ChooseCurrency").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //frmNotify dialog = new frmNotify(ResponseCode.Error, "กรุณาเลือกสกุลเงิน");
                dialog.ShowDialog(this);
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
            double amount = 0;
            foreach (UCItemCashIn uc in panel1.Controls)
            {
               amount += Convert.ToDouble(uc.lbAmtText);
            }
            lbTxtSubtotal.Text = amount.ToString(ProgramConfig.amountFormatString);
        }

        public void DrawerStatus(string status)
        {
            try
            {
                if (status.ToUpper() == "FALSE")
                {
                    AppLog.writeLog("frmCashIn DrawerStatus = " + status.ToUpper());
                    //Save time Close Cash Drawer
                    closeTime = DateTime.Now;
                    process.SaveDrawerTrans(FunctionID.CashIn_SaveCloseCashDrawer);

                    ConfirmChange();
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                AppLog.writeLog("DrawerStatus NetWorkLost CashIn");
                if (Program.control.RetryConnection(net.errorType))
                {
                    this.Dispose();
                    Program.control.ShowForm(this.Name);
                } 
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            RefreshGrid();
            SummaryCashIn();
            CheckIsEnablePaymentBtn();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            //var chk = false;
            ucFooterTran1.lbFunction.Text = FunctionID.CashIn_ConfirmCashIn.formatValue;
            Hardware.addDrawerListeners(DrawerStatus);    
            var chk = Hardware.openDrawer();
            process.SaveDrawerTrans(FunctionID.CashIn_SaveOpenCashDrawer);


            //Open Cash Drawer
            AppLog.writeLog("frmCashIn Hardware.isDrawerOpen = " + Hardware.isDrawerOpen);
            if (Hardware.isDrawerOpen)
            {
                openTime = DateTime.Now;
            }            

            if (Hardware.isDrawerOpen)
            {
                string responseMessage = ProgramConfig.message.get("frmCashbalance", "CloseDrawer").message;
                string helpMessage = ProgramConfig.message.get("frmCashbalance", "CloseDrawer").help;
                frmNotify dialog = new frmNotify(ResponseCode.CloseDrawer, responseMessage, helpMessage);

                //frmNotify dialog = new frmNotify(ResponseCode.Success, "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำรายการต่อ");
                dialog.btnOK.Visible = false;
                dialog.Show(this);
                dialog.Refresh();
            }

            #region For Test

            AppLog.writeLog("frmCashIn btnPayment_Click chk = " + chk);
            if (!chk)
            {
                ConfirmChange();
            }
            #endregion
        }

        private void ConfirmChange()
        {
            Program.control.CloseForm("frmNotify");
            //try
            //{
                frmLoading.showLoading();
                StoreResult result;

                result = process.deleteTempCashIn();
                foreach (UCItemCashIn uc in panel1.Controls)
                {
                    string cashType = uc.lbCashType;
                    string pm_Code = uc.lbCurrencyText.Trim() == defaultCurrency.Trim() ? "CASH" : "FXCU";
                    string sub_Pm_Code = pm_Code == "CASH" ? "" : uc.lbCurrencyText;
                    string amount = uc.lbAmtText;
                    // Hard Code ExchangeRate, Amount
                    double exchange = Convert.ToDouble(uc.lbExchangeRateText);
                    double ex_amt = Convert.ToDouble(amount);
                    result = process.saveTempCashIn(cashType, pm_Code, sub_Pm_Code, exchange.ToString(), uc.lbInputAmt, ex_amt.ToString(), ProgramConfig.operateDate);
                }

                result = process.saveCashInTransaction(openTime.ToString("HH:mm:ss", cultureinfo), closeTime.ToString("HH:mm:ss", cultureinfo));
                if (!result.response.next)
                {
                    frmLoading.closeLoading();
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    return;
                }

                Profile checkPrint = ProgramConfig.getProfile(FunctionID.CashIn_PrintCashInDocument);
                if (checkPrint.policy == PolicyStatus.Work)
                {
                    result = process.printCashIn();
                    if (!result.response.next)
                    {
                        frmLoading.closeLoading();
                        frmNotify dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                        return;
                    }

                    DataTable dt = result.otherData;
                    Hardware.printTermal(dt);
                    //this.Dispose();
                }

                Profile checkSync = ProgramConfig.getProfile(FunctionID.CashIn_SaveCashInTransaction_SynchSaleTransactiontoDataTank);
                if (checkSync.policy == PolicyStatus.Work)
                {
                    result = process.syncToDataTank();
                    if (!result.response.next)
                    {
                        frmLoading.closeLoading();
                        frmNotify dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                        //return;
                    }
                }
                frmLoading.closeLoading();
                this.Dispose();
            //}
            //catch (NetworkConnectionException net)
            //{
            //    //throw;
            //    Program.control.RetryConnection(net.errorType);
            //}
            //catch (Exception ex)
            //{
            //    frmLoading.closeLoading();
            //    frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
            //    dialog.ShowDialog(this);
            //}
        }

        private void ucAdjustCash1_EnterFromButton(object sender, EventArgs e)
        {
            UCAdjustCash txtBox = (UCAdjustCash)sender;
            foreach(Control ctrl in pn_MainBankNote.Controls)
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
                            ucKeypad.ucTBS = null;
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
            CloseBankNote();
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
                if(lb.Text != "")
                total += Convert.ToDouble(lb.Text);
            }
            return total;
        }

        private void btnNoteBank_Click(object sender, EventArgs e)
        {
            if (ValidateDropDown())
            {
                Utility.SetBackGroundBrightness(panelMainSell, pictureBox2, pictureBox5);
                //Class1.SetFormAndArrow(pictureBox5, ucTBWI_Amt, panelMainSell, pnNoteBank);
                //pictureBox5.Parent = pictureBox2;
                //pictureBox5.BringToFront();
                ClearBankNote();
                LoadBankNote();
                pn_MainBankNote.BringToFront();
                pnNoteBank.Visible = true;
                pnNoteBank.BringToFront();
                ucKeypad.ucTBWI = null;
            }
        }

        private void LoadBankNote()
        {
            StoreResult result;

            result = process.getCashUnit(ucDDCurrency.LabelText);
            if (!result.response.next)
            {
                frmNotify dialog = new frmNotify(result);
                dialog.ShowDialog(this);
                return;
            }
            ucAdjustCash1.InitialBankNote(result.otherData);
            InitialPanelBankNote(result.otherData);
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
            lbTxtTotalCash.Text = "0";
        }

        private void frmCashbalance_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hardware.clearDrawerListeners();
        }

        private void ucDDCurrency_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            var ddl = (UCDropDownList)sender;
            Type type = typeof(FunctionID);
            PropertyInfo pInfo;
            lb_ExchangeRate.Text = GetExchangeRate(ddl.ValueText);
            if (typeChange == 0)
            {
                pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_FloatAmountDefault", ddl.UCddlLbText.Text));
            }
            else
            {
                pInfo = type.GetProperty(String.Format("{0}_{1}", "Login_DataConfigStore_CashIn_LoanAmountDefault", ddl.UCddlLbText.Text));
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
            //ucDDCurrency.ValueText = "";
        }

        private void lbNormalChange_TextChanged(object sender, EventArgs e)
        {
            _strNormalChange = lbNormalChange.Text;
            if (typeChange == 0)
            {
                lbChangeText = lbNormalChange.Text;
            }
            else
            {
                lbChangeText = lbAdjustChange.Text;
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
            else
            {
                lbChangeText = lbAdjustChange.Text;
            }
        }

        private void ChangeButtonBackGround(Button button)
        {
            if (button.Location == new Point(7, 60))
            {
                if (button.Enabled)
                {
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left_white;
                    }
                }
            }
            else if (button.Location == new Point(165, 60))
            {
                if (button.Enabled)
                {
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right_white;
                    }
                }
            }
            else if (button.Location == new Point(7, 131))
            {
                if (button.Enabled)
                {
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_left_white;
                    }
                }
            }
            else if (button.Location == new Point(165, 131))
            {
                if (button.Enabled)
                {
                    if (button.Tag.ToString() == "enable")
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right;
                    }
                    else
                    {
                        button.BackgroundImage = Properties.Resources.cashin_top_right_white;
                    }
                }
            }
        }

        private void ucHeader2_MainMenuClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            this.Dispose();
            frmLoading.closeLoading();
        }

        private void ucKeypad_Leave(object sender, EventArgs e)
        {
            ucKeypad.Enabled = true;
        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            CloseBankNote();
        }
    }
}
