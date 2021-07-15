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
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BJCBCPOS
{
    public partial class frmPayment : Form
    {
        private frmNotify notify;
        public struct Dropdown
        {
            public string DisplayText;
            public string ValueText;
        }

        int[] xPoint_Credit = { 0, 3, 213, 424 };
        int[] yPoint_Credit = { 0, 3, 118 };

        Form frmOwner = null;

        public Point LocationKBCredit = new Point(15, -145);
        public Point LocationKBCredit_default = new Point(15, 83);

        [Browsable(false)]
        public List<Dropdown> lstDD { get; set; }
        public List<string> lstDDL { get; set; }

        public bool DropdownExpandRightSide { get; set; }

        public frmMonitor2Detail moni2;
        public frmMonitorCustomer monCust;

        private SaleProcess saleProcess = new SaleProcess();
        private CashInProcess processCash = new CashInProcess();

        public DialogResult dialogFromOther = DialogResult.None;
        public string _hpContract = "";
        string _defaultPayment = "";

        string lastMenuID;
        double amtPrice = 0;
        string defaultCurrency;
        string paymentNumber;

        public string paymentCode;
        public string creditCard;

        string edcStatus;
        string cardSwipe;

        Panel currentPanel;
        string currentTemplate;

        DataTable dtStepDet = new DataTable();
        
        //Para insert tempDLY
        string upc;
        string dty;

        DataTable dt2Data = new DataTable();
        public PaymentMenuIconCollections _paymentMenuIcon;

        CurrencyCollections _currency;

        DataTable dtPaymentStep = new DataTable();
        int seqPaymentStep;

        public string payCode;
        public string couNo;
        public string couAmt;
        public string proCode;
        public string vRow;
        public string couponType;
        

        string displayAmt = "";
        string mode = "";
        string lbCreditManual = "";

        //Other Payment
        public string _OPtemplate = "";
        public string _OPpaymentCode = "";
        public string _OPpaymentName = "";

        Point pnt = new Point();
        UCHeader currentUCDDL;
        List<UCListPayment> lst = new List<UCListPayment>();
        List<Button> btnPaymentGen = new List<Button>();
        List<Button> btnCreditGen = new List<Button>();
        Button LastButtonGen = null;

        private string depositRef = "";

        public frmPayment()
        {
            InitializeComponent();
            InitDataTable();
            this.ActiveControl = ucTxtAmountCash;
            ucTxtAmountCash.Select();
            ucTxtAmountCash.Focus();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            try
            {
                AppLog.writeLog("frmPayment_Load");
                Form form2 = Application.OpenForms["frmMonitor2Detail"];
                moni2 = form2 as frmMonitor2Detail;

                Form form3 = Application.OpenForms["frmMonitorCustomer"];
                monCust = form3 as frmMonitorCustomer;

                ucHeader1.btnMember.Enabled = false;
                ucHeader1.btnMember.BackgroundImage = Properties.Resources.member_non_activate;

                SetRefNo_TitleHeader();

                _currency = ProgramConfig.currency;

                frmLoading.showLoading();
                saleProcess.SaveSmsDiscount();
                frmLoading.closeLoading();
                
                displayAmt = ProgramConfig.amountFormatString;

                lbTxtSubtotal.Text = ProgramConfig.totalValue;
                lbTxtTotalCash.Text = ProgramConfig.totalValue;
                ucTxtAmountCash.Text = ProgramConfig.totalValue;
                lbTxtBalance.Text = ProgramConfig.totalValue;

                defaultCurrency = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CurrencyDefault.parameterCode).ToString();
                //ucDDCurrency.LabelText = defaultCurrency;
                //ucDDCurrency.ValueText = "1";

                lbCurrency.Text = defaultCurrency;
                lbCurrency.Tag = "1";

                ucHeader1.nameText = ProgramConfig.memberName;
                if (ProgramConfig.memberName != "")
                {
                    ucHeader1.nameVisible = true;
                    Label newFont = new Label();
                    newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                    int checkWidth = TextRenderer.MeasureText(ProgramConfig.memberName, newFont.Font).Width;
                    ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);
                }
                else
                {
                    ucHeader1.nameVisible = false;
                }

                loadTempDLYForPayment();

                List<PaymentConfig> lstPaymentConfig = new List<PaymentConfig>();
                StoreResult res = saleProcess.GetPaymentConfig(ProgramConfig.paymentFunction, ProgramConfig.saleRefNo);
                if (res.response.next)
                {
                    _defaultPayment = res.otherData.Rows[0]["Default_Payment_Code"].ToString();
                    lstPaymentConfig = new PaymentConfigCollections(res.otherData).ToList();
                }
                else
                {
                    Utility.AlertMessage(res);
                    return;
                }

                res = saleProcess.selectPOSADMIN_PAYMENT_STEP_DET();
                if (res.response.next)
                {
                    dtStepDet = res.otherData;
                }
                else
                {
                    Utility.AlertMessage(res);
                    return;
                }

                PaymentMenuIconCollections pmMenuIcon = new PaymentMenuIconCollections(ProgramConfig.paymentMenuIcon.data(), lstPaymentConfig);
                bool result = GenerateMenu3(pmMenuIcon);//GenerateMenu2();//GenerateMenu();
                if (result == false)
                {
                    string responseMessage = ProgramConfig.message.get("frmPayment", "LoadMenuIncomplete").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "LoadMenuIncomplete").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถโหลดเมนูได้");
                    notify.ShowDialog(this);
                    this.Dispose();
                    return;
                }

                DisablePaymentGroup();
                //DisplayContent อย่าลืม
                StoreResult result2 = saleProcess.posDisplayContent();
                if (result2.response.next)
                {
                    if (result2.otherData.Rows.Count > 0)
                    {
                        if (result2.otherData.Columns.Contains("Content_Default"))
                        {
                            ucFooterTran1.mainContent = result2.otherData.Rows[0]["Content_Default"].ToString();
                        }
                        if (result2.otherData.Columns.Contains("Content_Detail"))
                        {
                            ucFooterTran1.fullContent = result2.otherData.Rows[0]["Content_Detail"].ToString();
                        }
                        ucFooterTran1.functionId = FunctionID.Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeCode.formatValue;
                    }
                }

                lbTxtBalance.Text = saleProcess.CalBalanceDiff(Convert.ToDouble(lbTxtBalance.Text), "FXCU", ProgramConfig.currencyDefault).ToString(displayAmt);

                if (Convert.ToDouble(lbTxtBalance.Text) == 0)
                {
                    ShowConfirmPayment();
                }
               
                panelPayment.BringToFront();
                frmLoading.showLoading();
                SetDefaultPayment(_defaultPayment);
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
                this.Close();
                this.Dispose();
                //frmLoading.closeLoading();
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    this.Dispose();
                //}
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void SetRefNo_TitleHeader()
        {
            if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale)
            {
                lbTxtRefNo.Text = ProgramConfig.saleRefNo;
                ucHeader1.currentMenuTitle1 = "ขายสินค้า";
                ucHeader1.currentMenuTitle2 = "ขายปกติ";
            }
            else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
            {
                lbTxtRefNo.Text = ProgramConfig.saleRefNo;
                ucHeader1.currentMenuTitle1 = "ขายสินค้า";
                ucHeader1.currentMenuTitle2 = "รับเงินมัดจำ";
            }
            else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
            {
                lbTxtRefNo.Text = ProgramConfig.podRefNo;
                ucHeader1.currentMenuTitle1 = "ขายสินค้า";
                ucHeader1.currentMenuTitle2 = "รับชำระ POD";
            }
            else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
            {
                lbTxtRefNo.Text = ProgramConfig.creditSaleNo;
                ucHeader1.currentMenuTitle1 = "ชำระเงินขายเชื่อ";
                ucHeader1.currentMenuTitle2 = "รับชำระเงินขายเชื่อ";
            }
        }

        private void SetDefaultPayment(string defaultPayment)
        {
            if (ProgramConfig.pageBackFromPayment != PageBackFormPayment.ReceivePOD)
            {
                bool hasCash = false;
                foreach (Control ctrl in panelPayment.Controls)
                {
                    if (ctrl is Button)
                    {
                        Button btn = (Button)ctrl;
                        if (defaultPayment != "")
                        {
                            if (ctrl.Tag != null && ctrl.Tag.ToString() == ProgramConfig.payment.getPaymentTypeID(defaultPayment).ToString())
                            {
                                btn.PerformClick();
                                break;
                            }

                            if (ctrl.Tag != null && ctrl.Tag.ToString() == "6")
                            {

                            }
                        }
                        else
                        {
                            if (ctrl.Tag != null && ctrl.Tag.ToString() == "1")
                            {
                                btnCash_Click(null, null);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void ShowDropdownList(bool isShow)
        {
            throw new NotImplementedException();
        }

        private void InitDataTable()
        {
            dt2Data.Columns.Add("PaymentCode", typeof(string));
            dt2Data.Columns.Add("CouponNo", typeof(string));
            dt2Data.Columns.Add("CouponValue", typeof(string));
            dt2Data.Columns.Add("CouponQnt", typeof(string));
            dt2Data.Columns.Add("Barcode", typeof(string));
            dt2Data.Columns.Add("Row", typeof(string));
        }

        private void showPaymentCash()
        {
            DisablePaymentGroup();
            pn_payment_cash.Visible = true;
            if (pn_payment_cash.Visible == true)
            {
                btnPayment_Cash.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_left;
                btnPayment_Cash.Image = BJCBCPOS.Properties.Resources.payment_icon_cash_white;
                btnPayment_Cash.ForeColor = Color.White;
            }
            ClearPaymentData();
            if (double.Parse(lbTxtBalance.Text) >= 0)
            {
                ucTxtAmountCash.Text = lbTxtBalance.Text;
            }
            ucTxtAmountCash.Focus();
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    if (!Process_POD("CASH"))
                    {
                        return;
                    }                  
                }

                bool visible = pn_payment_cash.Visible;
                DisablePaymentGroup();
                pn_payment_cash.Visible = !visible;

                if (pn_payment_cash.Visible)
                {
                    frmLoading.closeLoading();

                    if (btnPayment_Cash.InvokeRequired)
                    {
                        btnPayment_Cash.BeginInvoke((MethodInvoker)delegate
                        {
                            //btnPayment_Cash.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_left;
                            InitialImageButtonFromSEQ(btnPayment_Cash, btnPayment_Cash.Tag.ToString(), false);
                            btnPayment_Cash.Image = BJCBCPOS.Properties.Resources.payment_icon_cash_white;
                            //btnPayment_Cash.ForeColor = Color.White;
                        });
                    }
                    else
                    {
                        //btnPayment_Cash.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_left;
                        InitialImageButtonFromSEQ(btnPayment_Cash, btnPayment_Cash.Tag.ToString(), false);
                        btnPayment_Cash.Image = BJCBCPOS.Properties.Resources.payment_icon_cash_white;
                        //btnPayment_Cash.ForeColor = Color.White;
                    }

                    ClearPaymentData();

                    StoreResult res = saleProcess.PaymentDiscount("CASH", "");
                    loadDiscount();
                    SummaryCashIn();

                    double balance = GetTotalAmountDiff(lbTxtBalance.Text);

                    //if (lbTxtBalance.InvokeRequired)
                    //{
                    //    lbTxtBalance.BeginInvoke((MethodInvoker)delegate
                    //    {
                    //        balance = double.Parse(lbTxtBalance.Text);
                    //    });
                    //}
                    //else
                    //{
                    //    balance = double.Parse(lbTxtBalance.Text);
                    //}

                    if (ucTxtAmountCash.InvokeRequired)
                    {
                        ucTxtAmountCash.BeginInvoke((MethodInvoker)delegate
                        {
                            AppLog.writeLog("BeginInvoke ucTxtAmountCash.Text");
                            if (balance >= 0)
                            {
                                ucTxtAmountCash.Text = balance.ToString(displayAmt);
                            }
                            AppLog.writeLog("after BeginInvoke ucTxtAmountCash.Text");

                            ucTxtAmountCash.Focus();
                            AppLog.writeLog("after BeginInvoke ucTxtAmountCash.Focus");
                        });
                    }
                    else
                    {
                        if (balance >= 0)
                        {
                            ucTxtAmountCash.Text = balance.ToString(displayAmt);
                        }
                        AppLog.writeLog("after ucTxtAmountCash.Text");

                        ucTxtAmountCash.Focus();
                        AppLog.writeLog("after ucTxtAmountCash.Focus");
                    }
                }
                else
                {
                    ucKeypad.ucTBWI = null;
                }

            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
            //catch (InvalidOperationException inv)
            //{
            //    AppLog.writeLog("InvalidOperationException [Method] btnCash_Click :" + inv.Message);
            //    pn_payment_cash.Visible = false;
            //    DisablePaymentGroup();
            //    frmNotify dialog = new frmNotify(ResponseCode.Information, "Try again.", "");
            //    AppLog.writeLog("InvalidOperationException : End");
            //}
            catch (Exception ex)
            {
                AppLog.writeLog("catch [Method] btnCash_Click :" + ex.Message);
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private double GetTotalAmountDiff(string amt)
        {
            var res = saleProcess.getTotalAmtDiff(ProgramConfig.saleRefNo, amt, "2", "CASH");
            if (res.response.next)
            {
                double saleAmt_round = 0f;
                double.TryParse(res.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round);

                return saleAmt_round;
            }
            return Convert.ToDouble(amt);
        }

        private bool Process_POD(string pmCode)
        {
            var check = ProgramConfig.paymentPolicy.GetPaymentPolicyByFunctionPaymentCode(FunctionID.ReceivePOD_ShowQR, pmCode);
            if (check.policy == PolicyStatus.Work)
            {
                //Pop up QR
                frmPOD_QR frmPOD = new frmPOD_QR(lbTxtBalance.Text, pmCode);
                var resDialog = frmPOD.ShowDialog(this);
                if (resDialog == System.Windows.Forms.DialogResult.Yes)
                {
                    ShowConfirmPaymentPOD(frmPOD.edcAmt);
                    return false;
                }
                else if (resDialog == System.Windows.Forms.DialogResult.Retry)
                {
                    loadTempDLYForPayment();
                    return false;
                }
                else if (resDialog == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
            }
            //else
            //{
            //    return false;
                //ProgramConfig.podQRCode = "";
                //ProgramConfig.podRefID = "";
                //var res = saleProcess.savePaymentPOD(pmCode, "", lbTxtBalanceDiff.Text, "", "", "", "", "", "", "", "", "", "");
                //if (res.response.next)
                //{
                //    ShowConfirmPaymentPOD();
                //    return false;
                //}
                //else
                //{
                //    Utility.AlertMessage(res);
                //}
            //}
            return true;
        }

        private void ShowConfirmPaymentPOD(string edcAmt)
        {
            var resChange = saleProcess.GetChange(lbTxtBalanceDiff.Text, edcAmt);
            if (resChange.response.next)
            {
                double balance = Convert.ToDouble(edcAmt) - Convert.ToDouble(lbTxtBalanceDiff.Text);

                ProcessResult resDiff = saleProcess.beforePaymentProcessNew(lbTxtTotalCash.Text, balance.ToString(), resChange.otherData);

                if (resDiff.response.next)
                {
                    frmConfirmPayment frmConfirmPm = new frmConfirmPayment(true);
                    frmConfirmPm.dtChange = resChange.otherData;
                    //frmConfirmPm.lbConfirmCash = lbTxtTotalCash.Text;
                    frmConfirmPm.lbConfirmCash = lbTxtBalanceDiff.Text;
                    frmConfirmPm.lbConfirmPayment = lbTxtBalanceDiff.Text;
                    ProgramConfig.totalValue = lbTxtBalanceDiff.Text;

                    if (resChange.otherData != null && resChange.otherData.Rows.Count > 0)
                    {
                        if (resChange.otherData.AsEnumerable().Any(a => a["ChangeStatus"].ToString() == "Y"))
                        {
                            frmConfirmPm.lbConfirmBalance = lbTxtBalance.Text;
                        }
                        else
                        {
                            frmConfirmPm.lbConfirmBalance = "0";
                        }
                    }
                    else
                    {
                        frmConfirmPm.lbConfirmBalance = "0";
                    }

                    frmConfirmPm.Show(this);

                    //Program.control.ShowForm("frmConfirmPayment");
                }
                else
                {
                    //saleProcess.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                    //loadTempDLYForPayment();
                    //pn_payment_cash.Visible = false;
                    //btnCash_Click(null, null);
                    //frmNotify notify = new frmNotify(resDiff);
                    //notify.ShowDialog();
                    DisablePaymentGroup();
                    Utility.AlertMessage(resDiff);
                }
            }
            else
            {
                //saleProcess.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                //loadTempDLYForPayment();
                //pn_payment_cash.Visible = false;
                //btnCash_Click(null, null);
                DisablePaymentGroup();
                Utility.AlertMessage(resChange);
            }
        }

        public void ClearPaymentData()
        {
            //pn_payment_cash
            lbAmountCash.Text = "CASH";
            lbCurrency.Text = defaultCurrency;
            ucTxtAmountCash.Text = "";
            ucTxtAmountCash.Focus();

            //pn_payment_credit
            //lbCardType.Text = "Credit";
            //ucTxtCardNo.Text = "";
            //ucTxtCardNo.EnabledUC = true;
            //ucTxtCardNo.Visible = true;
            //lbDisplayCreditCard.Text = "";
            //lbDisplayCreditCard.Visible = false;

            //ucTxtCreditAmt.Text = "";
            //ucTxtCreditAmt.Visible = false;
            //ucTxtCreditApprove.Text = "";
            //ucTxtCreditApprove.Visible = false;
            //lbApprove.Visible = false;
            pn_payment_credit.Location = LocationKBCredit_default;

            ucTxtCardNo.Text = "";
            ucTxtCardNo.EnabledUC = true;
            ucTxtCardNo.Visible = true;
            lbDisplayCreditCard_remove.Text = "";
            lbDisplayCreditCard_remove.Visible = false;

            ucTxtAmount.Text = "";
            ucTxtAmount.Visible = false;
            ucTxtApprove.Text = "";
            ucTxtApprove.Visible = false;
            lbApprove_remove.Visible = false;

            //pn_payment_voucher
            lbVoucher.Text = "GFSL";
            ucTxtVoucherNo.Text = "";
            ucTxtVoucherAmt.Text = "";

            //pn_payment_coupon
            lbCouponNo.Text = "";
            ucTxtCouponNo.Text = "";
            ucTxtCouponQnt.Text = "";

            cardSwipe = "";
        } 

        private void btnCredit_Click(object sender, EventArgs e)
        {
            if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
            {
                frmOtherPayment frmOtr = new frmOtherPayment(_paymentMenuIcon.ToList(), "Credit Card", 3);
                var res = frmOtr.ShowDialog(this);

                if (res != System.Windows.Forms.DialogResult.None)
                {
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!Process_POD(_OPpaymentCode))
                        {
                            return;
                        }   
                    }
                }
            }

            bool visible = pn_payment_credit_Mapping.Visible;
            DisablePaymentGroup();
            pn_payment_credit_Mapping.Visible = !visible;
            if (pn_payment_credit_Mapping.Visible)
            {
                btnPayment_Credit.Image = BJCBCPOS.Properties.Resources.payment_icon_credit_white;
                InitialImageButtonFromSEQ(btnPayment_Credit, btnPayment_Credit.Tag.ToString(), false);

                edcStatus = ProgramConfig.getPosConfig(FunctionID.Login_CheckHardware_EDC_Type.parameterCode).ToString();

                //if (edcStatus == "1")
                //{
                //    //offline
                //    btnEDCOnline_remove.Enabled = false;
                //    btnEDCOnline_remove.Text = "EDC Offline";
                //}
                //else if (edcStatus == "2")
                //{
                //    //online
                //    btnEDCOnline_remove.Enabled = true;
                //    btnEDCOnline_remove.Text = "EDC Onlie";
                //}

                ClearPaymentData();
                ucTxtCardNo.Focus();

            }
            else
            {
                ucKeypad.ucTBWI = null;
            }
        }

        private void SetButtonCreditCard(string search, string pageID)
        {
            if (search != lastMenuID)
            {
                SetPage(search);
                lastMenuID = search;
            }
                       
            //DataRow[] drs = dtData.Select(" ReferMenuID = '" + search + "' and PageID = " + pageID + " ");

            List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID(search, pageID: Convert.ToInt32(pageID)); //dtData.Select(" ReferMenuID = '" + search + "' and PageID = " + pageID + "");
            
            if (lstPmMenuIcon.Count > 0)
            {
                //lbCreditManual += pmCodeIn + " ";
                panel1.Controls.Clear();
                pn_ManualChoise.Visible = false;
                btnCreditGen = new List<Button>();
            }
            else
            {
                pn_ManualChoise.Visible = true;
                if (lbDisplayCreditCard_remove.Text != "")
                {
                    ucTxtCardNo_ManualChoise.Visible = true;
                    ucTxtCardNo_ManualChoise.InpTxt = cardSwipe;
                    lbDisplayCreditCard_ManualChoise.Visible = false;
                }
                lbTxtCreditBalance_ManualChoise.Text = lbTxtBalance.Text;
            }

            int i = 1;
            foreach (PaymentMenuIcon itm  in lstPmMenuIcon)
            {
                Button btn = new Button();

                int row = itm.Row;
                int col = itm.Comlumn;
                string menuID = itm.MenuID.ToString();
                string subMenuID = itm.SubMenuID;
                string pmMainCode = itm.PaymentMainCode == "N/A" ? "" : itm.PaymentMainCode;
                string pmSubCode = itm.SubPaymentCode == "N/A" ? "" : itm.SubPaymentCode;

                string pmCode = pmMainCode + " " + pmSubCode;

                btn.BackgroundImage = Properties.Resources.btn_Credit_Disable;
                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                btn.ForeColor = System.Drawing.Color.Gray;
                btn.Location = new System.Drawing.Point(xPoint_Credit[col], yPoint_Credit[row]);
                btn.Name = "btnGen" + i;
                btn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
                btn.Size = new System.Drawing.Size(183, 98);
                btn.TabIndex = 0;
                btn.Text = itm.LabelName;
                btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                btn.UseVisualStyleBackColor = true;
                btn.Click += (s, ev) => ButtonCreditItemClick(s, ev, menuID, pmCode, subMenuID);
                //string template = dr["Template"].ToString();
                //int row = Convert.ToInt32(dr["Row"]);
                //int col = Convert.ToInt32(dr["Column"]);

                //UCButtonPayment ucbpm = new UCButtonPayment();
                //ucbpm.label1.Text = dr["Label"].ToString();
                //ucbpm.ButtonClick += (s, ev) => ButtonItemClick(s, ev, template);
                ////ucbpm.pictureBox1.Image 
                //ucbpm.Location = new Point(xPoint[col], yPoint[row]);

                btnCreditGen.Add(btn);
                panel1.Controls.Add(btn);
            }
        }

        private void ButtonCreditItemClick(object sender, EventArgs e, string menuID, string pmCode, string subMenuID)
        {
            DisableButtonCreditGen();
            Button btnCurrent = (Button)sender;
            List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID(menuID, pageID: Convert.ToInt32("1"));

            if (subMenuID == "Y" && lstPmMenuIcon.Count > 0)
            {
                lb_PageNo.Text = "1";
                SetButtonCreditCard(menuID, "1");
            }
            else if (subMenuID == "N" || lstPmMenuIcon.Count == 0)
            {
                pn_ManualChoise.Visible = true;
                if (lbDisplayCreditCard_remove.Text != "")
                {
                    ucTxtCardNo_ManualChoise.Visible = true;
                    ucTxtCardNo_ManualChoise.InpTxt = cardSwipe;
                    lbDisplayCreditCard_ManualChoise.Visible = false;
                }
                lbTxtCreditBalance_ManualChoise.Text = lbTxtBalance.Text;
                ucTxtCardNo_ManualChoise.FocusTxt();
            }

            lbCardType_ManualChoise.Text = pmCode;
            btnCurrent.BackgroundImage = Properties.Resources.btn_Credit_Enable;
            btnCurrent.ForeColor = Color.Black;
        }

        private void DisableButtonCreditGen()
        {
            foreach (Button btn in btnCreditGen)
            {
                //Button btn = btnPaymentGen[i];
                if (btn != null)
                {
                    btn.ForeColor = System.Drawing.Color.Gray;
                    btn.BackgroundImage = Properties.Resources.btn_Credit_Disable;
                }
            }
        }

        private void picBtBack_Click(object sender, EventArgs e)
        {
            BackProcess();
        }

        public void BackProcess()
        {
            try
            {
                frmLoading.showLoading();
                ProcessResult check = saleProcess.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                frmLoading.closeLoading();

                if (check.response.next)
                {
                    Form form1 = Application.OpenForms["frmMonitor2Detail"];
                    frmMonitor2Detail mon1 = form1 as frmMonitor2Detail;
                    mon1.panel_payment.SendToBack();

                    this.Dispose();
                    //Form form2 = Application.OpenForms["frmSale"];
                    //frmSale mon2 = form2 as frmSale;
                    //ProgramConfig.formGlobal = mon2;
                    //mon2.BringToFront();
                    //mon2.ucTBScanBarcode.Focus();
                }
                else
                {
                    Utility.AlertMessage(check.response, check.responseMessage, check.helpMessage);
                    return;
                }
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
            finally
            {
                frmLoading.closeLoading();
            }

            #region Old Code          
            //try
            //{
            //    frmLoading.showLoading();
            //    ProcessResult check = saleProcess.deleteAllPayment();
            //    frmLoading.closeLoading();

            //    Form form1 = Application.OpenForms["frmMonitor2Detail"];
            //    frmMonitor2Detail mon1 = form1 as frmMonitor2Detail;
            //    mon1.panel_payment.SendToBack();

            //    if (check.response.next)
            //    {
            //        Form form2 = Application.OpenForms["frmSale"];
            //        frmSale mon2 = form2 as frmSale;
            //        ProgramConfig.formGlobal = mon2;
            //        mon2.BringToFront();
            //        mon2.ucTBScanBarcode.Focus();

            //        //foreach (Form item in Application.OpenForms)
            //        //{
            //        //    if (item is frmSale)
            //        //    {
            //        //        frmSale = (frmSale)item;
            //        //        //frmSale.loadTempDLYPTRANS();
            //        //        frmSale.BringToFront();
            //        //        frmSale.ucTBScanBarcode.Select();
            //        //        frmSale.ucTBScanBarcode.Focus();
            //        //        break;
            //        //    }
            //        //}
            //    }
            //    else
            //    {
            //        notify = new frmNotify(check.response, check.responseMessage);
            //        notify.ShowDialog(this);
            //        return;
            //    }
                
            //}
            //catch (NetworkConnectionException net)
            //{
            //    ProcessCheckNetWorkLost(net);
            //}
            //finally
            //{
            //    frmLoading.closeLoading();
            //}
            #endregion
        }

        public bool ProcessCheckNetWorkLost(NetworkConnectionException net)
        {
            //bool res = false;
            //if (frmOwner is frmSale)
            //{
            Form form = Application.OpenForms["frmSale"];
            frmSale frmSale = form as frmSale;
            ProgramConfig.formGlobal = frmSale;
            bool res = frmSale.CatchNetWorkConnectionException(net);
            if (res)
            {
                Program.control.CloseForm(this.Name);
                return res;
            }
            ProgramConfig.formGlobal = this;
            //}
            //else if(frmOwner is frmOtherPayment)
            //{

            //}

            return res;
        }

        public void DisablePaymentGroup()
        {
            InitialImageButtonFromSEQ(btnPayment_Cash, btnPayment_Cash.Tag + "", true);
            InitialImageButtonFromSEQ(btnPayment_Credit, btnPayment_Credit.Tag + "", true);
            InitialImageButtonFromSEQ(btnPayment_GiftVoucher, btnPayment_GiftVoucher.Tag + "", true);
            InitialImageButtonFromSEQ(btnPayment_Coupon, btnPayment_Coupon.Tag + "", true);
            InitialImageButtonFromSEQ(btnPayment_HirePurchase, btnPayment_HirePurchase.Tag + "", true);
            InitialImageButtonFromSEQ(btnPayment_Other, btnPayment_Other.Tag + "", true);
            InitialImageButtonFromSEQ(btnPayment_QRPayment, btnPayment_QRPayment.Tag + "", true);

            //btnPayment_Cash.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_left_white;
            btnPayment_Cash.Image = BJCBCPOS.Properties.Resources.payment_icon_cash;

            //btnPayment_Credit.BackgroundImage = BJCBCPOS.Properties.Resources.payment_middle_white;
            btnPayment_Credit.Image = BJCBCPOS.Properties.Resources.payment_icon_credit;

            //btnPayment_GiftVoucher.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_right_white;
            btnPayment_GiftVoucher.Image = BJCBCPOS.Properties.Resources.payment_icon_giftvoucher1; 

            //btnPayment_Coupon.BackgroundImage = BJCBCPOS.Properties.Resources.payment_btm_left_white;
            btnPayment_Coupon.Image = BJCBCPOS.Properties.Resources.payment_icon_coupon;

            //btnPayment_Other.BackgroundImage = BJCBCPOS.Properties.Resources.payment_middle_white;
            btnPayment_Other.Image = BJCBCPOS.Properties.Resources.payment_icon_other;
            
            btnPayment_Cash.ForeColor
                = btnPayment_Credit.ForeColor
                = btnPayment_GiftVoucher.ForeColor
                = btnPayment_Coupon.ForeColor
                = btnPayment_Other.ForeColor
                = btnPayment_HirePurchase.ForeColor
                = btnPayment_QRPayment.ForeColor
                //= btnPayment_6.ForeColor
                = Color.Gray;

            pn_payment_cash.Visible = false;
            pn_payment_credit.Visible = false;
            pn_payment_credit_Mapping.Visible = false;
            pictureBox2.Visible = false;
            pn_payment_voucher.Visible = false;
            pn_payment_coupon.Visible = false;
            pn_payment_QRPPOnline.Visible = false;
            //btnPayment_2.Enabled = false;
            //btnPayment_5.Enabled = false;
            DisablePaymentButtonGen();
            DisableButtonCreditGen();

            //Case Co-Brand ให้ลบตราสาร SDCB ออกถ้ามีการเปลี่ยน payment การชำระ
            //Check IsCoBrand
            //saleProcess.

            foreach (UCListPayment itm in pn_list_payment.Controls)
            {
                if (itm.UCLP_lbPaymentType.Text.Trim() == "SDCB" || itm.UCLP_lbPaymentType.Text.Trim() == "ODCB")
                {
                    DeleteTempPayment(itm.UCLP_lbPaymentType.Text);
                }                
            }
            
        }


        private bool GenerateMenu()
        {
            ClearMenuPayment();

            string paymentSeq = "";
            string paymentName = "";
            string paymentType = "";
            string paymentCode = "";
            var abc = ProgramConfig.payment.data();

            for (int i = 0; i < abc.Length; i++)
            {
                Button currentBtn = null;
                if (paymentSeq != abc[i].paymentTypeSeq.ToString())
                {
                    paymentSeq = abc[i].paymentTypeSeq.ToString();
                    paymentType = abc[i].paymentTypeId.ToString();
                    paymentCode = abc[i].paymentCode.ToString();
                    paymentName = abc[i].paymentTypeName.ToString();

                    if (paymentType == "0")
                    {
                        currentBtn = btnPayment_Cash;

                        //pn_payment_cash.Visible = true;
                        currentBtn.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_left;
                        currentBtn.Image = BJCBCPOS.Properties.Resources.payment_icon_cash_white;
                        currentBtn.ForeColor = Color.White;
                        
                        ClearPaymentData();
                        if (double.Parse(lbTxtBalance.Text) > 0)
                        {
                            ucTxtAmountCash.Text = lbTxtBalance.Text;
                        }
                    }
                    else if (paymentType == "1")
                    {
                        currentBtn = btnPayment_Credit;
                    }
                    else if (paymentType == "6")
                    {
                        currentBtn = btnPayment_GiftVoucher; 
                    }
                    else if (paymentType == "7")
                    {
                        currentBtn = btnPayment_Coupon; 
                    }
                }

                if (currentBtn != null)
                {
                    switch (paymentSeq)
                    {
                        case "1":                            
                            currentBtn.Location = new Point(7, 43);
                            currentBtn.Text = paymentName;
                            currentBtn.BackgroundImage = Properties.Resources.payment_top_left_white;
                            break;
                        case "2":
                            currentBtn.Location = new Point(113, 43);
                            currentBtn.Text = paymentName;
                            currentBtn.BackgroundImage = Properties.Resources.payment_middle_white;
                            break;
                        case "3":
                            currentBtn.Location = new Point(219, 43);
                            currentBtn.Text = paymentName;
                            currentBtn.BackgroundImage = Properties.Resources.payment_top_right_white;
                            break;
                        case "4":
                            currentBtn.Location = new Point(7, 127);
                            currentBtn.Text = paymentName;
                            currentBtn.BackgroundImage = Properties.Resources.payment_btm_left_white;
                            break;
                        case "5":
                            currentBtn.Location = new Point(113, 127);
                            currentBtn.Text = paymentName;
                            currentBtn.BackgroundImage = Properties.Resources.payment_middle_white;
                            break;
                        case "6":
                            currentBtn.Location = new Point(219, 127);
                            currentBtn.Text = paymentName;
                            currentBtn.BackgroundImage = Properties.Resources.payment_btm_right_white;
                            break;
                    }

                    currentBtn.Tag = paymentSeq;
                    currentBtn.Visible = true;
                    //btnPayment_2.Location = new Point(5, 127);
                    ////btnPayment_2.Visible = false;
                    //btnPayment_2.BackgroundImage = Properties.Resources.payment_btm_right_disable;
                    //btnPayment_2.Text = "";
                    //btnPayment_2.Image = null;

                    //btnPayment_5.Visible = false;
                    //btnPayment_5.BackgroundImage = Properties.Resources.payment_btm_right_disable;
                    //btnPayment_5.Text = "";
                    //btnPayment_5.Image = null;
                }
            } 

            return true;
        }

        private void InitialImageButtonFromSEQ(Button btn, string seq, bool isWhite)
        {
            if (seq == "1")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_left_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_left;
                    btn.ForeColor = Color.White;
                }

            }
            else if (seq == "2")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle;
                    btn.ForeColor = Color.White;
                }

            }
            else if (seq == "3")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_right_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_right;
                    btn.ForeColor = Color.White;
                }
            }
            else if (seq == "4")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_left_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_left;
                    btn.ForeColor = Color.White;
                }
            }
            else if (seq == "5")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle;
                    btn.ForeColor = Color.White;
                }
            }
            else if (seq == "6")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_right_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_right;
                    btn.ForeColor = Color.White;
                }
            }
            else
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle;
                    btn.ForeColor = Color.White;
                }
            }
        }

        private void InitialImageButtonFromRowCol(Button btn, string row, string col, bool isWhite)
        {
            if (row == "1" && col == "1")
            {
                if (isWhite)
                {                   
                    btn.BackgroundImage = Properties.Resources.payment_top_left_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_left;
                    btn.ForeColor = Color.White;
                }
                btn.Tag = "1";
                btn.Location = new System.Drawing.Point(7, 45);
            }
            else if (row == "1" && col == "2")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle;
                    btn.ForeColor = Color.White;
                }
                btn.Tag = "2";
                btn.Location = new System.Drawing.Point(113, 45);
            }
            else if (row == "1" && col == "3")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_right_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_top_right;
                    btn.ForeColor = Color.White;
                }
                btn.Tag = "3";
                btn.Location = new System.Drawing.Point(219, 45);
            }
            else if (row == "2" && col == "1")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_left_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_left;
                    btn.ForeColor = Color.White;
                }
                btn.Tag = "4";
                btn.Location = new System.Drawing.Point(7, 129);
            }
            else if (row == "2" && col == "2")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_middle;
                    btn.ForeColor = Color.White;
                }
                btn.Tag = "5";
                btn.Location = new System.Drawing.Point(113, 129);
            }
            else if (row == "2" && col == "3")
            {
                if (isWhite)
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_right_white;
                    btn.ForeColor = Color.Gray;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.payment_btm_right;
                    btn.ForeColor = Color.White;
                }
                btn.Tag = "6";
                btn.Location = new System.Drawing.Point(219, 129);
            }
            else
            {
                return;
            }

            btn.Visible = true;
        }

        //private bool GenerateMenu2()
        //{
        //    ClearMenuPayment();
        //    ClearPaymentData();
        //    dtData = saleProcess.GetPaymentMenuIcon();

        //    if (dtData != null && dtData.Rows.Count > 0)
        //    {
        //        DataTable dt = dtData.Select(" ReferMenuID = '0'").CopyToDataTable();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            //DataRow dr = dt.Rows[i];
        //            string pmCode = dr["PaymentMainCode"].ToString();
        //            string row = dr["RowIcon"].ToString();
        //            string col = dr["ColumnIcon"].ToString();
        //            string labelTxt = dr["Label"].ToString();
        //            string template = dr["PaymentStepGroupID"].ToString();
        //            string menuID = dr["MenuID"].ToString();
        //            string subMenuID = dr["SubMenuID"].ToString();

        //            Button btn = new Button();
        //            if (menuID == "2") // if payment code = CASH
        //            {
        //                btn = btnPayment_Cash;
        //                btn.Text = labelTxt;
        //                InitialImageButtonFromRowCol(btn, row, col, true);
        //                if (double.Parse(lbTxtBalance.Text) > 0)
        //                {
        //                    ucTxtAmountCash.Text = lbTxtBalance.Text;
        //                }
        //                continue;
        //            }
        //            else if (menuID == "1") // if payment code = CPN1
        //            {
        //                btn = btnPayment_Coupon;
        //                btn.Text = labelTxt;
        //                InitialImageButtonFromRowCol(btn, row, col, true);
        //                continue;
        //            }
        //            else if (menuID == "3")
        //            {
        //                btn = btnPayment_Credit;
        //                btn.Text = labelTxt;
        //                InitialImageButtonFromRowCol(btn, row, col, true);
        //                continue;
        //            }
        //            else if (menuID == "4") // if payment code = GFSL
        //            {
        //                btn = btnPayment_GiftVoucher;
        //                btn.Text = labelTxt;
        //                InitialImageButtonFromRowCol(btn, row, col, true);
        //                continue;
        //            }
        //            else if (menuID == "8") 
        //            {
        //                btn = btnPayment_HirePurchase;
        //                btn.Text = labelTxt;
        //                InitialImageButtonFromRowCol(btn, row, col, true);
        //                continue;
        //            }


        //            if (row == "1" && col == "1")
        //            {
        //                //template = "1";
        //                btn.Name = "btnGen1";
        //                //btn.Tag = template;
        //                //btn.Location = SetLocationButtonPayment("1", "1");
        //                //btn.Location = new System.Drawing.Point(7, 45);
        //                //btn.BackgroundImage = Properties.Resources.payment_top_left_white;
        //                //btn.Text = dr["Label"].ToString();

        //            }
        //            else if (row == "1" && col == "2")
        //            {
        //                //template = "1";
        //                btn.Name = "btnGen2";
        //                //btn.Tag = template;
        //                //btn.Location = SetLocationButtonPayment("1", "2");
        //                //btn.Location = new System.Drawing.Point(113, 45);
        //                //btn.BackgroundImage = Properties.Resources.payment_middle_white;
        //                //btn.Text = "Temp1";
        //                //btn.Click += GetButtonTemplate(template);                 
        //            }
        //            else if (row == "1" && col == "3")
        //            {
        //                //template = "3";
        //                btn.Name = "btnGen3";
        //                //btn.Tag = template;
        //                //btn.Location = SetLocationButtonPayment("1", "3");
        //                //btn.Location = new System.Drawing.Point(219, 45);
        //                //btn.BackgroundImage = Properties.Resources.payment_top_right_white;
        //                //btn.Text = "Temp3";
        //                //btn.Click += GetButtonTemplate(template);                  
        //            }
        //            else if (row == "2" && col == "1")
        //            {
        //                //template = "4";
        //                btn.Name = "btnGen4";
        //                btn.Tag = template;
        //                //btn.Location = SetLocationButtonPayment("2", "1");
        //                //btn.Location = new System.Drawing.Point(7, 129);
        //                //btn.BackgroundImage = Properties.Resources.payment_btm_left_white;
        //                //btn.Text = "Temp4";
        //                //btn.Click += GetButtonTemplate(template);                
        //            }
        //            else if (row == "2" && col == "2")
        //            {
        //                //template = "5";
        //                btn.Name = "btnGen5";
        //                btn.Tag = template;
        //                //btn.Location = SetLocationButtonPayment("2", "2");
        //                //btn.Location = new System.Drawing.Point(113, 129);
        //                //btn.BackgroundImage = Properties.Resources.payment_middle_white;
        //                //btn.Text = "Temp5";
        //                //btn.Click += GetButtonTemplate(template);                  
        //            }
        //            else if (row == "2" && col == "3")
        //            {
        //                //template = "6";
        //                btn.Name = "btnGen6";
        //                //btn.Tag = template;
        //                //btn.Location = SetLocationButtonPayment("2", "3");
        //                //btn.Location = new System.Drawing.Point(219, 129);
        //                //btn.BackgroundImage = Properties.Resources.payment_btm_right_white;
        //                //btn.Text = "Temp6";
        //                //btn.Click += GetButtonTemplate(template);
        //            }
        //            else
        //            {
        //                continue;
        //            }

        //            btn.Tag = template;
        //            InitialImageButtonFromRowCol(btn, row, col, true);
        //            btn.Click += GetButtonTemplate(template, menuID, pmCode, labelTxt, subMenuID);
        //            btn.Text = dr["Label"].ToString();
        //            btn.BackColor = System.Drawing.Color.White;
        //            btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        //            btn.FlatAppearance.BorderSize = 0;
        //            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        //            btn.Font = new System.Drawing.Font("Prompt", dr["Label"].ToString().Length >= 10 ? 10F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //            btn.ForeColor = System.Drawing.Color.Gray;
        //            btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
        //            btn.Margin = new System.Windows.Forms.Padding(0);
        //            btn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
        //            btn.Size = new System.Drawing.Size(108, 86);
        //            btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        //            btn.UseVisualStyleBackColor = false;
        //            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
        //            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;

        //            btnPaymentGen.Add(btn);
        //            panelPayment.Controls.Add(btn);
        //            btn.BringToFront();
        //        }

        //        DisablePaymentButtonGen();
        //        return true;
        //    }
        //    return false;
        //}
        
        public bool GenerateMenu3(PaymentMenuIconCollections paymentMenuIcon)
        {
            ClearMenuPayment();
            ClearPaymentData();
            _paymentMenuIcon = paymentMenuIcon;

            if (_paymentMenuIcon != null && _paymentMenuIcon.Count() > 0)
            {
                List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID("0");
                if (lstPmMenuIcon.Count > 0)
                {
                    foreach (PaymentMenuIcon itm in lstPmMenuIcon)
                    {
                        //DataRow dr = dt.Rows[i];
                        string pmCode = itm.PaymentMainCode;
                        string row = itm.Row.ToString();
                        string col = itm.Comlumn.ToString();
                        string labelTxt = itm.LabelName;
                        string template = itm.PaymentStepGroupID;
                        string menuID = itm.MenuID.ToString();
                        string subMenuID = itm.SubMenuID;

                        Button btn = new Button();
                        if (menuID == "2") // if payment code = CASH
                        {
                            btn = btnPayment_Cash;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            btn.Text = labelTxt;
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            if (double.Parse(lbTxtBalance.Text) > 0)
                            {
                                ucTxtAmountCash.Text = lbTxtBalance.Text;
                            }
                            continue;
                        }
                        else if (menuID == "1") // if payment code = CPN1
                        {
                            btn = btnPayment_Coupon;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            btn.Text = labelTxt;
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            continue;
                        }
                        else if (menuID == "3")
                        {
                            btn = btnPayment_Credit;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            btn.Text = labelTxt;
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            continue;
                        }
                        else if (menuID == "4") // if payment code = GFSL
                        {
                            btn = btnPayment_GiftVoucher;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            btn.Text = labelTxt;
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            continue;
                        }
                        else if (menuID == "9")
                        {
                            btn = btnPayment_HirePurchase;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            btn.Text = labelTxt;
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            btn.Image = Utility.ByteToImage(itm.Picture);
                            continue;
                        }
                        else if (menuID == "6")
                        {
                            btn = btnPayment_Other;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            btn.Text = labelTxt;
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            continue;
                        }
                        else if (menuID == "86")
                        {
                            btn = btnPayment_QRPayment;
                            btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                            InitialImageButtonFromRowCol(btn, row, col, true);
                            btn.Image = Utility.ByteToImage(itm.Picture);
                            btn.Text = labelTxt;
                            continue;
                        }

                        if (row == "1" && col == "1")
                        {
                            btn.Name = "btnGen1";
                        }
                        else if (row == "1" && col == "2")
                        {
                            btn.Name = "btnGen2";              
                        }
                        else if (row == "1" && col == "3")
                        {
                            btn.Name = "btnGen3";              
                        }
                        else if (row == "2" && col == "1")
                        {
                            btn.Name = "btnGen4";           
                        }
                        else if (row == "2" && col == "2")
                        {
                            btn.Name = "btnGen5";              
                        }
                        else if (row == "2" && col == "3")
                        {
                            btn.Name = "btnGen6";
                        }
                        else
                        {
                            continue;
                        }

                        btn.Tag = ProgramConfig.payment.getPaymentTypeID(pmCode);
                        InitialImageButtonFromRowCol(btn, row, col, true);
                        btn.Click += GetButtonTemplate(template, menuID, _paymentMenuIcon, pmCode, labelTxt, subMenuID, false);
                        btn.Text = labelTxt;
                        btn.BackColor = System.Drawing.Color.White;
                        btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        btn.Font = new System.Drawing.Font("Prompt", labelTxt.Length >= 9 ? 10F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.ForeColor = System.Drawing.Color.Gray;
                        btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
                        btn.Image = Utility.ByteToImage(itm.Picture);
                        btn.Margin = new System.Windows.Forms.Padding(0);
                        btn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
                        btn.Size = new System.Drawing.Size(108, 86);
                        btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                        btn.UseVisualStyleBackColor = false;
                        btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        btn.FlatAppearance.MouseOverBackColor = Color.Transparent;

                        btnPaymentGen.Add(btn);
                        panelPayment.Controls.Add(btn);
                        btn.BringToFront();

                        //if (menuID == "86")
                        //{
                        //    btn.Visible = false;
                        //}

                    }

                    //Fix Template QR
                    label10.Tag = "3";
                    label9.Tag = "4";
                    ucTextBoxWithIcon2.SeqTextBox = "3";
                    ucTextBoxWithIcon1.SeqTextBox = "4";

                    DisablePaymentButtonGen();


                    return true;
                }
            }
            return false;
        }

        private DataTable GenDataTest()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Type");
            dt.Columns.Add("Page_ID");
            dt.Columns.Add("Icon_ID");
            dt.Columns.Add("Row");
            dt.Columns.Add("Column");
            dt.Columns.Add("Picture_Name");
            dt.Columns.Add("Label");
            dt.Columns.Add("Payment_Code");
            dt.Columns.Add("Sub_Payment_Code");
            dt.Columns.Add("Template");

            dt.Rows.Add("Main", "0", "1", "1", "1", "", "Coupon", "CPN1", "N/A");
            dt.Rows.Add("Main", "0", "2", "1", "2", "", "Cash", "CASH", "N/A");
            dt.Rows.Add("Main", "0", "3", "1", "3", "", "Credit Card", "CRED", "N/A");
            dt.Rows.Add("Main", "0", "4", "2", "1", "", "Voucher", "GFSL", "N/A");
            dt.Rows.Add("Main", "0", "5", "2", "2", "", "QR Payment", "N/A", "N/A", "1");
            dt.Rows.Add("Main", "0", "6", "2", "3", "", "Other", "N/A", "N/A", "2");

            dt.Rows.Add("Other", "1", "1", "1", "1", "", "Alipay", "", "N/A", "1");
            dt.Rows.Add("Other", "1", "2", "1", "2", "", "We Chat", "", "N/A", "2");
            dt.Rows.Add("Other", "1", "3", "1", "3", "", "Big Wallet", "", "N/A", "3");
            dt.Rows.Add("Other", "1", "4", "2", "1", "", "Rabbit", "", "N/A", "4");
            //dt.Rows.Add("Other", "1", "5", "2", "2", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "1", "6", "2", "3", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "2", "1", "1", "1", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "2", "2", "1", "2", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "2", "3", "1", "3", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "2", "4", "2", "1", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "2", "5", "2", "2", "", "N/A", "", "N/A");
            //dt.Rows.Add("Other", "2", "6", "2", "3", "", "N/A", "", "N/A");
            dt.Rows.Add("QR Payment", "1", "1", "1", "1", "", "Manual", "QRPP", "N/A", "1");
            dt.Rows.Add("QR Payment", "1", "2", "1", "2", "", "C scan B", "QRPP", "N/A", "2");
            dt.Rows.Add("QR Payment", "1", "3", "1", "3", "", "B scan C", "QRPP", "N/A", "3");
            dt.Rows.Add("Credit Card", "1", "1", "1", "1", "", "VISA", "", "N/A", "1");
            dt.Rows.Add("Credit Card", "1", "2", "1", "2", "", "MAST", "", "N/A", "2");
            dt.Rows.Add("Credit Card", "1", "3", "1", "3", "", "Other", "", "N/A", "3");
            dt.Rows.Add("VISA", "1", "1", "1", "1", "", "BBLC", "", "");
            dt.Rows.Add("VISA", "1", "2", "1", "2", "", "CITI", "", "");
            dt.Rows.Add("VISA", "1", "3", "1", "3", "", "KBANK", "", "");
            dt.Rows.Add("VISA", "1", "4", "2", "1", "", "AEON", "", "");
            dt.Rows.Add("VISA", "1", "5", "2", "2", "", "KTC", "", "");
            dt.Rows.Add("VISA", "1", "6", "2", "3", "", "SCB", "", "");
            //dt.Rows.Add("VISA", "2", "1", "1", "1", "", "VISA", "", "");
            //dt.Rows.Add("VISA", "2", "2", "1", "2", "", "VISA", "", "");
            //dt.Rows.Add("VISA", "2", "3", "1", "3", "", "VISA", "", "");
            //dt.Rows.Add("VISA", "2", "4", "2", "1", "", "VISA", "", "");
            //dt.Rows.Add("VISA", "2", "5", "2", "2", "", "VISA", "", "");
            //dt.Rows.Add("VISA", "2", "6", "2", "3", "", "VISA", "", "");

            return dt;
        }

        //private Point SetLocationButtonPayment(string row, string col)
        //{
        //    if (row == "1" && col == "1")
        //    {
        //        return new System.Drawing.Point(7, 43);
        //    }
        //    else if (row == "1" && col == "2")
        //    {
        //        return new System.Drawing.Point(113, 43);
        //    }
        //    else if (row == "1" && col == "3")
        //    {
        //        return new System.Drawing.Point(219, 43);
        //    }
        //    else if (row == "2" && col == "1")
        //    {
        //        return new System.Drawing.Point(7, 127);
        //    }
        //    else if (row == "2" && col == "2")
        //    {
        //        return new System.Drawing.Point(113, 127);
        //    }
        //    else if (row == "2" && col == "3")
        //    {
        //        return new System.Drawing.Point(219, 127);
        //    }
        //    else
        //    {
        //        return new Point();
        //    }
        //}

        public EventHandler GetButtonTemplate(string template, string keySearch, PaymentMenuIconCollections paymentMenuIcon, string pmCode, string header, string subMenuID, bool IsFromOtherPayment)
        {
            EventHandler eventRet = delegate { };
            string temp = template;
            if (template != "1" && template != "2" && template != "3")
            {
                var dr2 = dtStepDet.Select(" PaymentStepGroupID = '" + template + "'");
                if (dr2.Length > 0)
                {
                    int cnt = dr2.AsEnumerable().Count(c => c["TextBoxType"].ToString() == PaymentStepDetail_TextBoxType.TextBox);
                    if (cnt > 2)
                    {
                        temp = "0";
                    }
                    else
                    {
                        temp = "4";
                    }
                }
            }

            switch (temp)
            {
                case "0":
                    eventRet = (s, e) => ButtonTemplate(s, e, pn_payment_temp0, template, keySearch, paymentMenuIcon, pmCode, header, subMenuID, IsFromOtherPayment); break;
                case "1":
                    eventRet = (s, e) => ButtonTemplate(s, e, pn_payment_voucher, template, keySearch, paymentMenuIcon, pmCode, header, subMenuID, IsFromOtherPayment); break;
                case "2":
                    eventRet = (s, e) => ButtonTemplate(s, e, pn_payment_temp1, template, keySearch, paymentMenuIcon, pmCode, header, subMenuID, IsFromOtherPayment); break;
                case "3":
                    eventRet = (s, e) => ButtonTemplate(s, e, pn_payment_temp1, template, keySearch, _paymentMenuIcon, pmCode, header, subMenuID, IsFromOtherPayment); break;
                case "4":
                    eventRet = (s, e) => ButtonTemplate(s, e, pn_payment_temp4, template, keySearch, _paymentMenuIcon, pmCode, header, subMenuID, IsFromOtherPayment); break;
            }

            return eventRet;
        }

        private Panel GetPanelTemplate(string template)
        {
            Panel panel = null;
            switch (template)
            {
                case "0":
                    panel = pn_payment_temp0; break;
                case "1":
                    panel = pn_payment_voucher; break;
                case "2":
                    panel = pn_payment_temp1; break;
                case "4":
                    panel = pn_payment_temp4; break;
                //case "3":
                //    panel = pn_payment_temp2; break;
                //case "4":
                //    panel = pn_payment_temp4; break;
                //case "5":
                //    panel = pn_payment_temp5; break;
                //case "6":
                //    panel = pn_payment_temp6; break;
            }

            return panel;
        }

        private void ButtonTemplate(object sender, EventArgs e, Panel pn_payment_template, string template, string menuID, PaymentMenuIconCollections paymentMenuIcon, string pmCode, string header, string subMenuID, bool IsFormOtherPayment)
        {
            paymentCode = pmCode == "N/A" ? "" : pmCode ; 
            seqPaymentStep = 1;
            bool visible = pn_payment_template.Visible;

            Button btn = null;
            if (sender is Button)
            {
                btn = (Button)sender;
            }
            else
            {
                visible = false;
            }
           
            DisablePaymentGroup();

            if (visible)
            {
                if (LastButtonGen == btn)
                {
                    pn_payment_template.Visible = !visible;
                    if (btn != null)
                    {
                        btn.ForeColor = System.Drawing.Color.Gray;
                        btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
                    }
                    ucKeypad.ucTBWI = null;
                }
                else
                {
                    //เคส กดเปิด menu สำเร็จ กรณีเป็น template เดียวกัน
                    pn_payment_template.Visible = visible;
                    if (btn != null)
                    {
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
                    }
                    if (subMenuID == "Y")
                    {
                        DialogResult res = System.Windows.Forms.DialogResult.None;
                        List<PaymentMenuIcon> lstPmMenuIcon = paymentMenuIcon.GetDataByReferMenuID(menuID);
                        if (lstPmMenuIcon.Count > 0)
                        {
                            if (lstPmMenuIcon.Count == 1)
                            {
                                _OPtemplate = lstPmMenuIcon[0].PaymentStepGroupID;
                                _OPpaymentCode = lstPmMenuIcon[0].PaymentMainCode;
                                _OPpaymentName = lstPmMenuIcon[0].LabelName;
                                res = System.Windows.Forms.DialogResult.Yes;
                            }
                            else
                            {
                                Program.control.CloseForm("frmOtherPayment");
                                frmOtherPayment fOther = new frmOtherPayment(lstPmMenuIcon, header, Convert.ToInt32(menuID));
                                res = fOther.ShowDialog(this);
                            }
                        }

                        if (res != System.Windows.Forms.DialogResult.None)
                        {
                            if (res == System.Windows.Forms.DialogResult.Yes)
                            {
                                //Panel pntemp = GetPanelTemplate(_OPtemplate);
                                Panel pntemp = GetPanelTemplate(template);
                                if (pntemp != null)
                                {
                                    pn_payment_template = pntemp;
                                    template = _OPtemplate;
                                    paymentCode = _OPpaymentCode;
                                    header = _OPpaymentName;

                                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                                    {
                                        pn_payment_template.Visible = false;
                                        if (!Process_POD(paymentCode))
                                        {
                                            return;
                                        }   
                                    }

                                    pn_payment_template.Visible = true;
                                    pn_payment_template.BringToFront();
                                    if (IsFormOtherPayment)
                                    {
                                        InitialImageButtonFromSEQ(btnPayment_Other, btnPayment_Other.Tag.ToString(), false);
                                    }
                                }
                                else
                                {
                                    DisablePaymentGroup();
                                    return;
                                }
                            }
                            else
                            {
                                DisablePaymentGroup();
                                return;
                            }
                            dialogFromOther = res;
                        }
                       
                    }                    
                    currentTemplate = template;
                    currentPanel = pn_payment_template;
                    GenerateTextBoxLabel(template, pn_payment_template, header);
                    Program.control.CloseForm("frmOtherPayment");
                }
            }
            else
            {
                pn_payment_template.Visible = !visible;
                if (pn_payment_template.Visible)
                {
                    //เคส กดเปิด menu สำเร็จ
                    if (btn != null)
                    {
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
                    }
                    if (subMenuID == "Y")
                    {
                        DialogResult res = System.Windows.Forms.DialogResult.None;
                        List<PaymentMenuIcon> lstPmMenuIcon = paymentMenuIcon.GetDataByReferMenuID(menuID);
                        if (lstPmMenuIcon.Count > 0)
                        {
                            if (lstPmMenuIcon.Count == 1)
                            {
                                _OPtemplate = lstPmMenuIcon[0].PaymentStepGroupID;
                                _OPpaymentCode = lstPmMenuIcon[0].PaymentMainCode;
                                _OPpaymentName = lstPmMenuIcon[0].LabelName;
                                res = System.Windows.Forms.DialogResult.Yes;
                            }
                            else
                            {
                                Program.control.CloseForm("frmOtherPayment");
                                frmOtherPayment fOther = new frmOtherPayment(lstPmMenuIcon, header, Convert.ToInt32(menuID));
                                res = fOther.ShowDialog(this);
                            }
                        }

                        if (res != System.Windows.Forms.DialogResult.None)
                        {
                            if (res == System.Windows.Forms.DialogResult.Yes)
                            {
                                Panel pntemp = GetPanelTemplate(template);
                                if (pntemp != null)
                                {
                                    pn_payment_template = pntemp;
                                    template = _OPtemplate;
                                    paymentCode = _OPpaymentCode;
                                    header = _OPpaymentName;

                                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                                    {
                                        pn_payment_template.Visible = false;
                                        if (!Process_POD(paymentCode))
                                        {
                                            return;
                                        }   
                                    }

                                    pn_payment_template.Visible = true;
                                    pn_payment_template.BringToFront();

                                    if (IsFormOtherPayment)
                                    {
                                        InitialImageButtonFromSEQ(btnPayment_Other, btnPayment_Other.Tag.ToString(), false);
                                    }                                   
                                }
                                else
                                {
                                    DisablePaymentGroup();
                                    return;
                                }
                            }
                            else
                            {
                                DisablePaymentGroup();
                                return;
                            }
                        }
                        dialogFromOther = res;
                    }
                    currentTemplate = template;
                    currentPanel = pn_payment_template;
                    GenerateTextBoxLabel(template, pn_payment_template, header);
                    Program.control.CloseForm("frmOtherPayment");

                    //if (template == "0")
                    //{
                    //    lbHDTemplate0.Text = keySearch;
                    //    Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2, pictureBox5);
                    //    pn_payment_template.BringToFront();
                    //}
                }
                else
                {
                    if (btn != null)
                    {
                        btn.ForeColor = System.Drawing.Color.Gray;
                        btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
                    }
                    ucKeypad.ucTBWI = null;
                }
            }

            if (btn != null)
            {
                LastButtonGen = btn;
            }

            if (pn_payment_template.Visible)
            {
                //Program.control.CloseForm("frmOtherPayment");
                RunModuleParameter pModule = new RunModuleParameter();
                pModule.pmCode = paymentCode;
                pModule.cardNo = "";
                RunModule(seqPaymentStep, pModule);
            }
        }

        //private bool SubButtonTemplate(Panel pn_payment_template, string template, string menuID, DataTable iDtData, string header)
        //{
            
        //}

        private void GenerateTextBoxLabel(string template, Panel pn_payment_template, string header = "")
        {
            var dr2 = dtStepDet.Select(" PaymentStepGroupID = '" + template + "'");
            string temp = "";
            if (dr2.Length > 0)
            {
                int cnt = dr2.AsEnumerable().Count(c => c["TextBoxType"].ToString() == PaymentStepDetail_TextBoxType.TextBox);
                if (cnt > 2)
                {
                    temp = "0";
                }
            }

            DataRow[] dr = saleProcess.GenerateTextBoxLabel().Select(" PaymentStepGroupID = '" + template + "'");

            if (temp == "0")
            {
                if (dr.Length > 0 && pn_payment_temp0.Visible)
                {
                    int cnt = 0;
                    DisablePn0();

                    dtPaymentStep = dr.CopyToDataTable();
                    dtPaymentStep.DefaultView.Sort = "Seq Asc";
                    dtPaymentStep = dtPaymentStep.DefaultView.ToTable();

                    lbHeaderPn0.Text = header;

                    UCTextBoxWithIcon uc = null;

                    foreach (DataRow drin in dtPaymentStep.Select(" TextBoxType = 'TextBox' "))
                    {
                        cnt++;
                        Label lbPn0 = pn_payment_temp0.Controls.Find("lbPn0_" + cnt, true).FirstOrDefault() as Label;
                        lbPn0.Text = drin["Label"].ToString();
                        lbPn0.Visible = true;

                        UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + cnt, true).FirstOrDefault() as UCTextBoxWithIcon;
                        ucTxtPn0.Text = "";
                        ucTxtPn0.Visible = true;
                        ucTxtPn0.PaymentStepGroupID = drin["PaymentStepGroupID"].ToString();
                        ucTxtPn0.SeqTextBox = drin["Seq"].ToString();
                        ucTxtPn0.InputType = drin["InputType"].ToString();
                        ucTxtPn0.StepID = (PaymentStepDetail_StepID)drin["StepID"];    
                        ucTxtPn0.DataType = drin["DataType"].ToString();
                        ucTxtPn0.placeHolder = drin["LabelTextInput"].ToString();// drin["LabelTextInput_Language"].ToString(); //TO DO Change
                                                                
                        ucTxtPn0.IsValidateTextEmpty = true;
                        if (drin["DataType"].ToString() == PaymentStepDetail_DataType.Money)
                        {

                            ucTxtPn0.IsValidateNumberZero = true;
                            ucTxtPn0.IsAmount = true;
                            ucTxtPn0.IsSetFormat = true;
                            ucTxtPn0.TextBoxAlign = HorizontalAlignment.Right;
                        }
                        else
                        {
                            ucTxtPn0.IsAmount = false;
                            ucTxtPn0.IsSetFormat = false;
                            ucTxtPn0.TextBoxAlign = HorizontalAlignment.Left;
                        }

                        if (drin["DataLenghtType"].ToString() == "D")
                        {
                            if (drin["InputType"].ToString() != "AE")
                            {
                                ucTxtPn0.InpTxt = lbTxtBalance.Text;
                            }                          
                        }
                        else
                        {
                            ucTxtPn0.InpTxt = "";
                        }

                        string maxLength = drin["DataLenghtMax"].ToString();
                        if (maxLength != "0")
                        {
                            int length = 0;
                            if (int.TryParse(maxLength, out length))
                            {
                                ucTxtPn0.MaxLength = length;
                            }
                        }

                        if (ucTxtPn0.StepID == PaymentStepDetail_StepID.Display_Amount)
                        {
                            ucTxtPn0.EnabledUC = false;
                        }
                        else
                        {
                            ucTxtPn0.EnabledUC = true;
                        }

                        if (cnt == 1)
                        {
                            uc = ucTxtPn0;
                        }

                    }

                    if (cnt <= 4)
                    {
                        pn_payment_temp0.Height = 312;
                    }
                    else if (cnt <= 6)
                    {
                        pn_payment_temp0.Height = 402;
                    }
                    else if (cnt <= 8)
                    {
                        pn_payment_temp0.Height = 496;
                    }
                    else if (cnt <= 10)
                    {
                        pn_payment_temp0.Height = 589;
                    }

                    Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2);
                    pictureBox2.BringToFront();
                    pn_payment_temp0.BringToFront();
                    uc.FocusTxt();
                }
            }
            else if (pn_payment_template.Name == "pn_payment_temp4")
            {
                if (dr.Length > 0 && pn_payment_temp4.Visible)
                {
                    int cnt = 0;
                    DisablePn4();

                    dtPaymentStep = dr.CopyToDataTable();
                    dtPaymentStep.DefaultView.Sort = "Seq Asc";
                    dtPaymentStep = dtPaymentStep.DefaultView.ToTable();

                    lbHeaderPn4.Text = header;

                    UCTextBoxWithIcon uc = null;
                    var aryDr = dtPaymentStep.Select(" TextBoxType = 'TextBox' ");
                    int length = aryDr.Length;

                    foreach (DataRow drin in aryDr)
                    {
                        cnt++;
                        Label lbPn4 = pn_payment_temp4.Controls.Find("lbPn4_" + cnt, true).FirstOrDefault() as Label;
                        lbPn4.Text = drin["Label"].ToString();
                        lbPn4.Visible = true;

                        UCTextBoxWithIcon ucTxtPn4 = pn_payment_temp4.Controls.Find("ucTxtPn4_" + cnt, true).FirstOrDefault() as UCTextBoxWithIcon;
                        ucTxtPn4.Text = "";
                        ucTxtPn4.Visible = true;
                        ucTxtPn4.PaymentStepGroupID = drin["PaymentStepGroupID"].ToString();
                        ucTxtPn4.SeqTextBox = drin["Seq"].ToString();
                        ucTxtPn4.DataType = drin["DataType"].ToString();
                        ucTxtPn4.InputType = drin["InputType"].ToString();
                        ucTxtPn4.placeHolder = drin["LabelTextInput"].ToString();// drin["LabelTextInput_Language"].ToString(); //TO DO Change
                        ucTxtPn4.StepID = (PaymentStepDetail_StepID)drin["StepID"];
                        ucTxtPn4.EnterFromButton -= btnOkPn0_Click;
                        if (cnt == length)
                        {
                            ucTxtPn4.EnterFromButton += btnOkPn0_Click;
                        }

                        ucTxtPn4.IsValidateTextEmpty = true;
                        if (drin["DataType"].ToString() == PaymentStepDetail_DataType.Money)
                        {

                            ucTxtPn4.IsValidateNumberZero = true;
                            ucTxtPn4.IsAmount = true;
                            ucTxtPn4.IsSetFormat = true;
                            ucTxtPn4.TextBoxAlign = HorizontalAlignment.Right;
                        }
                        else
                        {
                            ucTxtPn4.IsAmount = false;
                            ucTxtPn4.IsSetFormat = false;
                            ucTxtPn4.TextBoxAlign = HorizontalAlignment.Left;
                        }

                        if (drin["DataLenghtType"].ToString() == "D")
                        {
                            if (drin["InputType"].ToString() != "AE")
                            {
                                ucTxtPn4.InpTxt = lbTxtBalance.Text;
                            }       
                        }
                        else
                        {
                            ucTxtPn4.InpTxt = "";
                        }

                        string maxLength = drin["DataLenghtMax"].ToString();
                        if (maxLength != "0")
                        {
                            int lengthTxt = 0;
                            if (int.TryParse(maxLength, out lengthTxt))
                            {
                                ucTxtPn4.MaxLength = lengthTxt;
                            }
                        }

                        if (ucTxtPn4.StepID == PaymentStepDetail_StepID.Display_Amount)
                        {
                            ucTxtPn4.EnabledUC = false;
                        }
                        else
                        {
                            ucTxtPn4.EnabledUC = true;
                        }

                        if (cnt == 1)
                        {
                            uc = ucTxtPn4;
                        }
                    }

                    pn_payment_temp4.BringToFront();
                    uc.FocusTxt();
                }
            }
            else
            {
                if (dr.Length > 0 && pn_payment_template.Visible)
                {
                    dtPaymentStep = dr.CopyToDataTable();
                    dtPaymentStep.DefaultView.Sort = "Seq Asc";
                    dtPaymentStep = dtPaymentStep.DefaultView.ToTable();

                    List<UCTextBoxWithIcon> lst = new List<UCTextBoxWithIcon>();

                    foreach (DataRow drin in dtPaymentStep.Select(" TextBoxType = 'TextBox' "))
                    {
                        foreach (Control ctrl in pn_payment_template.Controls)
                        {
                            if (ctrl is UCTextBoxWithIcon)
                            {
                                UCTextBoxWithIcon uc = (UCTextBoxWithIcon)ctrl;
                                if (drin["SEQ"].ToString() == uc.SeqTextBox)
                                {
                                    lst.Add(uc);
                                    uc.placeHolder = drin["LabelTextInput_Language"].ToString();

                                    if (drin["DataType"].ToString() == PaymentStepDetail_DataType.Money)
                                    {
                                        uc.IsAmount = true;
                                        uc.IsSetFormat = true;
                                        uc.TextBoxAlign = HorizontalAlignment.Right;
                                    }
                                    else
                                    {
                                        uc.IsAmount = false;
                                        uc.IsSetFormat = false;
                                        uc.TextBoxAlign = HorizontalAlignment.Left;
                                    }
                                    uc.DataType = drin["DataType"].ToString();

                                    if (drin["DataLenghtType"].ToString() == "D")
                                    {
                                        uc.InpTxt = lbTxtBalance.Text;
                                    }
                                    else
                                    {
                                        uc.InpTxt = "";
                                    }

                                    string maxLength = drin["DataLenghtMax"].ToString();
                                    if (maxLength != "0")
                                    {
                                        int length = 0;
                                        if (int.TryParse(maxLength, out length))
                                        {
                                            uc.MaxLength = length;
                                        }
                                    }
                                }
                            }

                            if (ctrl is Label)
                            {
                                Label lb = (Label)ctrl;
                                if (drin["SEQ"].ToString() == (string)lb.Tag)
                                {
                                    lb.Text = drin["Label_Language"].ToString();
                                }

                                if ("1" == (string)lb.Tag && header != "")
                                {
                                    lb.Text = header;
                                }
                            }

                        }
                    }

                    if (lst.Count > 0)
                    {
                        lst[0].FocusTxt();
                    }
                }

                this.Update();
            }

        }

        private void DisablePn0()
        {
            for (int i = 1; i <= 10; i++)
            {
                Label lbPn0 = pn_payment_temp0.Controls.Find("lbPn0_" + i, true).FirstOrDefault() as Label;
                lbPn0.Text = "";
                lbPn0.Visible = false;

                UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                ucTxtPn0.Text = "";
                ucTxtPn0.Visible = false;
            }
        }

        private void DisablePn4()
        {
            for (int i = 1; i <= 2; i++)
            {
                Label lbPn4 = pn_payment_temp4.Controls.Find("lbPn4_" + i, true).FirstOrDefault() as Label;
                lbPn4.Text = "";
                lbPn4.Visible = false;

                UCTextBoxWithIcon ucTxtPn4 = pn_payment_temp4.Controls.Find("ucTxtPn4_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                ucTxtPn4.Text = "";
                ucTxtPn4.Visible = false;
            }
        }

        private void ButtonTemplate1(object sender, EventArgs e, Panel pn_payment_template, string template)
        {

        }

        private void ButtonTemplate2(object sender, EventArgs e, Panel pn_payment_template, string template)
        {

        }

        private void ButtonTemplate3(object sender, EventArgs e, Panel pn_payment_template, string template)
        {
            //Button btn = (Button)sender;
            //bool visible = pn_payment_temp3.Visible;
            //DisablePaymentGroup();
            //DisablePaymentButtonGen();
            //pn_payment_temp3.Visible = !visible;

            //if (pn_payment_temp3.Visible)
            //{
            //    btn.ForeColor = System.Drawing.Color.White;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
            //}
            //else
            //{
            //    btn.ForeColor = System.Drawing.Color.Gray;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
            //}
            //btnPayment_3_Click(sender, e);
        }

        private void ButtonTemplate4(object sender, EventArgs e, Panel pn_payment_template, string tag)
        {
            //Button btn = (Button)sender;
            //bool visible = pn_payment_temp4.Visible;
            //DisablePaymentGroup();
            //DisablePaymentButtonGen();
            //pn_payment_temp4.Visible = !visible;

            //if (pn_payment_temp4.Visible)
            //{
            //    btn.ForeColor = System.Drawing.Color.White;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
            //}
            //else
            //{
            //    btn.ForeColor = System.Drawing.Color.Gray;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
            //}
            //btnPayment_3_Click(sender, e);
        }

        private void ButtonTemplate5(object sender, EventArgs e, Panel pn_payment_template, string tag)
        {
            //Button btn = (Button)sender;
            //bool visible = pn_payment_temp5.Visible;
            //DisablePaymentGroup();
            //DisablePaymentButtonGen();
            //pn_payment_temp5.Visible = !visible;

            //if (pn_payment_temp5.Visible)
            //{
            //    btn.ForeColor = System.Drawing.Color.White;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
            //}
            //else
            //{
            //    btn.ForeColor = System.Drawing.Color.Gray;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
            //}
            //btnPayment_3_Click(sender, e);
        }

        private void ButtonTemplate6(object sender, EventArgs e, Panel pn_payment_template, string tag)
        {
            //Button btn = (Button)sender;
            //bool visible = pn_payment_temp6.Visible;
            //DisablePaymentGroup();
            //DisablePaymentButtonGen();
            //pn_payment_temp6.Visible = !visible;

            //if (pn_payment_temp6.Visible)
            //{
            //    btn.ForeColor = System.Drawing.Color.White;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
            //}
            //else
            //{
            //    btn.ForeColor = System.Drawing.Color.Gray;
            //    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
            //}
            //btnPayment_3_Click(sender, e);
        }

        private void DisablePaymentButtonGen()
        {
            foreach (Button btn in btnPaymentGen)
            {
                //Button btn = btnPaymentGen[i];
                if (btn != null)
                {
                    btn.ForeColor = System.Drawing.Color.Gray;
                    btn.BackgroundImage = GetImageButtonGenFromTag(btn.Name, true);
                }
            }

            pn_payment_temp0.Visible = false; 
            pn_payment_temp1.Visible = false;
            //pn_payment_temp2.Visible = false;
            //pn_payment_temp3.Visible = false;
            pn_payment_temp4.Visible = false;
            //pn_payment_temp5.Visible = false;
            //pn_payment_temp6.Visible = false;
            
        }

        private Image GetImageButtonGenFromTag(string name, bool isWhite)
        {
            if (name == "btnGen1")
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_top_left_white;
                }
                else
                {
                    return Properties.Resources.payment_top_left;
                }
                
            }
            else if (name == "btnGen2")
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_middle_white;
                }
                else
                {
                    return Properties.Resources.payment_middle;
                }
                
            }
            else if (name == "btnGen3")
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_top_right_white;
                }
                else
                {
                    return Properties.Resources.payment_top_right;
                }
            }
            else if (name == "btnGen4")
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_btm_left_white;
                }
                else
                {
                    return Properties.Resources.payment_btm_left;
                }
            }
            else if (name == "btnGen5")
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_middle_white;
                }
                else
                {
                    return Properties.Resources.payment_middle;
                }
            }
            else if (name == "btnGen6")
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_btm_right_white;
                }
                else
                {
                    return Properties.Resources.payment_btm_right;
                }
                
            }
            else
            {
                if (isWhite)
                {
                    return Properties.Resources.payment_middle_white;
                }
                else
                {
                    return Properties.Resources.payment_middle;
                }
            }
        }

        private void ClearMenuPayment()
        {
            btnPayment_Cash.Visible = false;
            btnPayment_Credit.Visible = false;
            btnPayment_GiftVoucher.Visible = false;
            btnPayment_Coupon.Visible = false;
            btnPayment_Other.Visible = false;
            btnPayment_6.Visible = false;
            btnPayment_HirePurchase.Visible = false;

            //btnPayment_4.BackgroundImage = Properties.Resources.payment_btm_right_disable;
            //btnPayment_4.Text = "";
            //btnPayment_4.Image = null;

            //btnPayment_5.BackgroundImage = Properties.Resources.payment_btm_right_disable;
            //btnPayment_5.Text = "";
            //btnPayment_5.Image = null;

            //btnPayment_6.BackgroundImage = Properties.Resources.payment_btm_right_disable;
            //btnPayment_6.Text = "";
            //btnPayment_6.Image = null;
            ClearMenuPaymentButtonGen();
        }

        private void ClearMenuPaymentButtonGen()
        {
            foreach (Button btn in btnPaymentGen)
            {
                //Button btn = btnPaymentGen[i];
                if (btn != null)
                {
                    btn.Dispose();
                }
            }
        }

        private void ucDDCurrency_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            var ddl = (UCDropDownList)sender;

            double exChange = ProgramConfig.payment.getExchangeRate(ddl.UCddlLbText.Text);

            if (exChange != 0)
            {
                //ucTxtAmountCash.Text = (double.Parse(lbTxtTotalCash.Text) / exChange).ToString(displayAmt);
                ucTxtAmountCash.Text = (double.Parse(lbTxtBalanceDiff.Text) / exChange).ToString(displayAmt);
            }
            else
            {
                ucTxtAmountCash.Text = "0.00000";
            }
            
            ucTxtAmountCash.Focus();
        }

        private void ucDDCurrency_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList2();
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList1()
        {
            //List<string> lstStr = new List<string>();
            //lstStr.Add("ยกเลิกระหว่างขาย");


            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            if (ProgramConfig.language.ID == 1)
            {
                drItem.DisplayText = "Cancel sales";
            }
            else if (ProgramConfig.language.ID == 2)
            {
                drItem.DisplayText = "ยกเลิกการขาย";
            }
            else if (ProgramConfig.language.ID == 3)
            {
                drItem.DisplayText = "ຍົກເລີກການຂາຍ";
            }
            drItem.ValueText = "1";
            lstStr.Add(drItem);
            return lstStr;
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            int i = 1;
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            List<Currency> lstCur = new List<Currency>();
            lstCur = _currency.list();
            lstCur.Reverse();
            foreach (Currency currency in lstCur)
            {
                drItem.DisplayText = currency.code;
                drItem.ValueText = i.ToString();
                lstStr.Add(drItem);
                i++;
            }
            return lstStr;
        }

        private void btnNoteBank_Click(object sender, EventArgs e)
        {
            panel_assist_cash.Visible = !panel_assist_cash.Visible;
            if (panel_assist_cash.Visible)
            {
                Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2, pictureBox5);
                //Class1.SetFormAndArrow(pictureBox5, ucTBWI_Amt, panelMainSell, pnNoteBank);
                //pictureBox5.Parent = pictureBox2;
                //pictureBox5.BringToFront();
                ClearBankNote();
                LoadBankNote();
                pn_MainBankNote.BringToFront();
                panel_assist_cash.Visible = true;
                panel_assist_cash.BringToFront();
                ucKeypad.ucTBWI = null;
            }
            else
            {
                CloseBankNote();
            } 
        }

        private void LoadBankNote()
        {
            StoreResult result;

            result = processCash.getCashUnit(lbCurrency.Text);
            if (!result.response.next)
            {
                notify = new frmNotify(result);
                notify.ShowDialog(this);
                return;
            }
            else if (result.response == ResponseCode.Information)
            {
                notify = new frmNotify(result);
                notify.ShowDialog(this);
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

        private void btnPayment_4_Click(object sender, EventArgs e)
        {
            bool visible = pn_payment_coupon.Visible;
            DisablePaymentGroup();
            pn_payment_coupon.Visible = !visible;
            if (pn_payment_coupon.Visible)
            {
                //btnPayment_Coupon.BackgroundImage = BJCBCPOS.Properties.Resources.payment_btm_left;
                btnPayment_Coupon.Image = BJCBCPOS.Properties.Resources.payment_icon_coupon_white;
                InitialImageButtonFromSEQ(btnPayment_Coupon, btnPayment_Coupon.Tag.ToString(), false);
                //btnPayment_Coupon.ForeColor = Color.White;
                ClearPaymentData();
                ucTxtCouponNo.Focus();
            }
            else
            {
                ucKeypad.ucTBWI = null;
            }
        }

        private void btnPayment_3_Click(object sender, EventArgs e)
        {
            bool visible = pn_payment_voucher.Visible;
            DisablePaymentGroup();
            pn_payment_voucher.Visible = !visible;
            if (pn_payment_voucher.Visible)
            {
                DataRow[] dr = saleProcess.GenerateTextBoxLabel().Select(" PaymentStepGroupID = '1' ");
                if (dr.Length > 0)
                {
                    dtPaymentStep = dr.CopyToDataTable();
                }
                //btnPayment_GiftVoucher.BackgroundImage = BJCBCPOS.Properties.Resources.payment_top_right;
                btnPayment_GiftVoucher.Image = BJCBCPOS.Properties.Resources.payment_icon_giftvoucher_white;
                InitialImageButtonFromSEQ(btnPayment_GiftVoucher, btnPayment_GiftVoucher.Tag.ToString(), false);
                //btnPayment_GiftVoucher.ForeColor = Color.White;
                ClearPaymentData();
                ucTxtVoucherAmt.EnabledUC = false;
                ucTxtVoucherNo.Focus();
            }
            else
            {
                ucKeypad.ucTBWI = null;
            }
        }

        private void ucAdjustCash1_EnterContinueButton(object sender, EventArgs e)
        {
            ucTxtAmountCash.Text = String.Format("{0}", SumAmountTotal().ToString(displayAmt));
            CloseBankNote();
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

        public void CloseBankNote()
        {
            panelPayment.BringToFront();
            panel_assist_cash.Visible = false;
            ClearBankNote();
            pictureBox2.Visible = false;
            ucKeypad.ucTBS = null;
        }

        public void closeAddcoupon()
        {
            pn_page_coupon.Visible = false;
            pn_coupon_list.Controls.Clear();
            pictureBox2.Visible = false;
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
            lbAmtCash.Text = "0";
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
                            ctrl.Text = String.Format("{0}", Convert.ToDouble(tbs.Text.Trim() == "" ? "0" : tbs.Text.Trim()) * Convert.ToDouble(tbs.Tag + ""));
                            lbAmtCash.Text = String.Format("{0}", SumAmountTotal());
                            ucKeypad.ucTBWI = null;
                            break;
                        }
                        //total += Convert.ToDouble(txtbox.Text.Trim() == "" ? "0" : txtbox.Text.Trim());
                    }

                }
            }
        }

        private void ucTxtAmountCash_EnterFromButton(object sender, EventArgs e)
        {
            try
            {
                //StoreResult res = saleProcess.couponUse(ProgramConfig.memberId);
                //if (res.response.next)
                //{
                enterChangebalance();
                //}

            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtAmountCash_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                enterChangebalance();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        public void clearAndConfirmVoucher()
        {
            ucTxtVoucherNo.Text = "";
            ucTxtVoucherAmt.Text = "";
            ucTxtVoucherNo.Focus();

            //loadTempDLYForPayment();

            ShowConfirmPayment();

        }

        public void ShowConfirmPayment(bool isShowConfirm = true)      
        {
            pn_CannotUseTicketCoupon.Visible = false;

            loadTempDLYForPayment();

            List<PaymentList> lstPM = new List<PaymentList>();
            foreach (var item in pn_list_payment.Controls.Cast<UCListPayment>().ToList())
            {
                lstPM.Add(new PaymentList() { PaymentCode = item.PMCode, Amount = item.UCLP_lbAmount.Text, PaymentNo = item.PMNumber });
            }

            saleProcess.SaveCreditSalePayment(lstPM);

            StoreResult res = saleProcess.couponUse();
            if (res.response.next)
            {
                if (res.response != ResponseCode.Ignore)
                {
                    if (!SavePaymentLast(res.otherData))
                    {
                        return;
                    }
                }

                var resChange = saleProcess.GetChange(lbTxtBalanceDiff.Text, lbTxtReceiveCash.Text);
                if (resChange.response.next)
                {
                    double balance = Convert.ToDouble(lbTxtReceiveCash.Text) - Convert.ToDouble(lbTxtBalanceDiff.Text);

                    ProcessResult resDiff = saleProcess.beforePaymentProcessNew(lbTxtTotalCash.Text, balance.ToString(), resChange.otherData);

                    if (resDiff.response.next)
                    {
                        frmConfirmPayment frmConfirmPm = new frmConfirmPayment(isShowConfirm);
                        frmConfirmPm.dtChange = resChange.otherData;
                        //frmConfirmPm.lbConfirmCash = lbTxtTotalCash.Text;
                        frmConfirmPm.lbConfirmCash = lbTxtBalanceDiff.Text;
                        frmConfirmPm.lbConfirmPayment = lbTxtReceiveCash.Text;

                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                        {
                            ProgramConfig.totalValue = lbTxtBalanceDiff.Text;
                        }

                        if (resChange.otherData != null && resChange.otherData.Rows.Count > 0)
                        {
                            if (resChange.otherData.AsEnumerable().Any(a => a["ChangeStatus"].ToString() == "Y"))
                            {
                                frmConfirmPm.lbConfirmBalance = lbTxtBalance.Text;
                            }
                            else
                            {
                                frmConfirmPm.lbConfirmBalance = "0";
                            }
                        }
                        else
                        {
                            frmConfirmPm.lbConfirmBalance = "0";
                        }

                        frmConfirmPm.Show(this);

                        if (!isShowConfirm)
                        {
                            frmConfirmPm.btnOk_Click(null, null);
                        }

                        //Program.control.ShowForm("frmConfirmPayment");
                    }
                    else
                    {
                        saleProcess.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                        loadTempDLYForPayment();
                        pn_payment_cash.Visible = false;
                        btnCash_Click(null, null);
                        frmNotify notify = new frmNotify(resDiff);
                        notify.ShowDialog();

                    }
                }
                else
                {
                    saleProcess.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                    loadTempDLYForPayment();
                    pn_payment_cash.Visible = false;
                    btnCash_Click(null, null);
                    Utility.AlertMessage(resChange);
                }
            }
            else
            {
                saleProcess.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                loadTempDLYForPayment();
                pn_payment_cash.Visible = false;
                btnCash_Click(null, null);
                frmNotify notify = new frmNotify(res);
                notify.ShowDialog();
            }
        }

        public void clearAndConfirm()
        {
            //ucTxtAmountCash.Text = "";
            ucTxtAmountCash.Focus();

            ShowConfirmPayment();
        }

        private bool SavePaymentLast(DataTable dt)
        {


            List<string> lstStr = new List<string>();
            loadTempDLYForPayment();

            double receiveCash = dt.AsEnumerable().Where(w => w["STATUS"].ToString().Trim() == "A").Sum(s => Convert.ToDouble(s["Coupon_Value"]));


            ProcessResult resProcess = saleProcess.deleteAllPaymentTempDLYPTRANS();
            //if (!resProcess.response.next)
            //{
            //    return false;
            //}

            double amtSumCPN = 0.0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PaymentCode"].ToString() == "CPN1")
                {
                    if (dr["STATUS"].ToString().Trim() == "A")
                    {
                        amtSumCPN += Convert.ToDouble(dr["Coupon_Value"]);
                    }
                }
                else
                {
                    saleProcess.savePaymentLast(dr["PaymentCode"].ToString(), dr["Coupon_Value"].ToString());
                }
            }

            if (amtSumCPN > 0)
            {
                saleProcess.savePaymentLast("CPN1", amtSumCPN.ToString());
            }


            //if (receiveCash - Convert.ToDouble(lbTxtTotalCash.Text) < 0)
            if (receiveCash - Convert.ToDouble(lbTxtBalanceDiff.Text) < 0)
            {
                lstStr = dt.AsEnumerable().Where(w => w["PaymentCode"].ToString() == "CPN1" && w["STATUS"].ToString().Trim() == "I")
                                            .Select(s => s["RUNNINGNO"].ToString())
                                            .ToList();

                if (lstStr.Count > 0)
                {
                    pn_CannotUseTicketCoupon.Visible = true;
                    pn_CannotUseTicketCoupon.BringToFront();
                    lb_CannotUseTicketCoupon.Text = String.Join(", ", lstStr);
                    pn_DisplayDiscount.Visible = false;
                }
                return false;
            }
            //else if (receiveCash - Convert.ToDouble(lbTxtTotalCash.Text) >= 0)
            //{
                
            //}

            

            return true;
        }

        private DataTable GetDataChange()
        {
            DataTable dt = new DataTable();
            Profile check = ProgramConfig.getProfile(FunctionID.Login_DataConfigStore_Change_Display);
            //if (check.policy == PolicyStatus.Work)
            //{

            //StoreResult res = saleProcess.GetChange(lbTxtTotalCash.Text, lbTxtReceiveCash.Text);
            StoreResult res = saleProcess.GetChange(lbTxtBalanceDiff.Text, lbTxtReceiveCash.Text);
            if (res.response.next)
            {
                if (res.response == ResponseCode.Ignore)
                {
                    dt = new DataTable();
                }
                else
                {
                    dt = res.otherData;
                }

            }
            else
            {
                Utility.AlertMessage(res);
            }
            return dt;
        }

        public void enterChangebalance()
        {
            double checkTextAmount = 0.0;
            string txtBalance = lbTxtBalance.Text;
            if (!double.TryParse(ucTxtAmountCash.Text, out checkTextAmount))
            {
                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                notify.ShowDialog(this);
                return;
            }

            if (ucTxtAmountCash.Text == "")
            {
                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                notify.ShowDialog(this);
                return;
            }

            frmLoading.showLoading();
            StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount(lbAmountCash.Text.Trim(), ucTxtAmountCash.Text, lbCurrency.Text, "2");
            frmLoading.closeLoading();
            if (!chkMinCash.response.next)
            {
                notify = new frmNotify(chkMinCash);
                notify.ShowDialog();
            }
            else
            {
                if (chkMinCash.response == ResponseCode.Information)
                {
                    notify = new frmNotify(chkMinCash);
                    notify.ShowDialog(this);
                }

                if (ValidateData())
                {
                    //Case นี้ดักไวข้างบนแล้ว
                    //if (ucTxtAmountCash.Text == "" && double.Parse(lbTxtReceiveCash.Text) >= double.Parse(lbTxtTotalCash.Text))
                    if (ucTxtAmountCash.Text == "" && double.Parse(lbTxtReceiveCash.Text) >= double.Parse(lbTxtBalanceDiff.Text))
                    {
                        loadTempDLYForPayment();
                        ShowConfirmPayment();
                        return;
                    }
                    //Case นี้ดักไวข้างบนแล้ว
                    //else if (ucTxtAmountCash.Text == "" && double.Parse(lbTxtReceiveCash.Text) < double.Parse(lbTxtTotalCash.Text))
                    else if (ucTxtAmountCash.Text == "" && double.Parse(lbTxtReceiveCash.Text) < double.Parse(lbTxtBalanceDiff.Text))
                    {
                        string responseMessage = ProgramConfig.message.get("frmPayment", "NotEnoughMoney").message;
                        string helpMessage = ProgramConfig.message.get("frmPayment", "NotEnoughMoney").help;
                        notify = new frmNotify(ResponseCode.Warning, string.Format(responseMessage, lbTxtBalance.Text, ProgramConfig.currencyDefault), helpMessage);

                        //notify = new frmNotify(ResponseCode.Warning, "จำนวนเงินไม่พอ. คงเหลือ " + lbTxtBalance.Text + " " + ProgramConfig.currencyDefault);
                        notify.ShowDialog(this);
                        ucTxtAmountCash.Text = "";
                        ucTxtAmountCash.Focus();
                        return;
                    }
                    else
                    {
                        ProgramConfig.paymentType = ProgramConfig.payment.getPCD(lbAmountCash.Text.Trim(), lbCurrency.Text);     
                  
                        DataTable dt = saleProcess.selectDataToDeleteCashTempDLY();
                        if (dt.Rows.Count != 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string amt = "";
                                string rec = "";
                                LoadFromTable load;
                               
                                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                                {
                                    amt = dt.Rows[i]["AMT"].ToString();
                                    rec = dt.Rows[i]["REC"].ToString();
                                    load = LoadFromTable.TEMP_PODTRANS_PAY;
                                }
                                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                                {
                                    amt = dt.Rows[i]["Payment_Amount"].ToString();
                                    rec = dt.Rows[i]["SEQ"].ToString();
                                    load = LoadFromTable.TEMPCREDPAY_TRANS_PAY;
                                }
                                else
                                {
                                    amt = dt.Rows[i]["AMT"].ToString();
                                    rec = dt.Rows[i]["REC"].ToString();
                                    load = LoadFromTable.TEMPDLYPTRANS;
                                }

                                ProgramConfig.paymentAmt = amt;
                                StoreResult resRec = saleProcess.deletePaymentType(rec, load);

                                if (resRec.response == ResponseCode.Success)
                                {
                                    loadTempDLYForPayment();
                                }
                                else if (resRec.response == ResponseCode.Error)
                                {
                                    notify = new frmNotify(ResponseCode.Error, resRec.responseMessage, resRec.helpMessage);
                                    notify.ShowDialog(this);
                                    return;
                                }
                            }
                        }

                        SummaryCashIn();
                        RefreshGrid();

                        double receiveCash = 0;
                        double balance = 0;
                        string paymentType = "";

                        receiveCash = double.Parse(ucTxtAmountCash.Text);

                        string exChangeText = ucTxtAmountCash.Text;
                        paymentType = lbAmountCash.Text;

                        string formatCash = "";
                        formatCash = receiveCash.ToString(displayAmt);

                        AppLog.writeLog("before saleProcess.getAmountExchangeRate enterChangebalance");
                        if (double.Parse(lbTxtReceiveCash.Text) != 0)
                        {
                            string strTxtBalance = lbTxtBalance.Text;
                            if (lbAmountCash.Text == "CASH")
                            {
                                amtPrice = double.Parse(formatCash);
                            }
                            else
                            {
                                StoreResult res = saleProcess.getAmountExchangeRate(formatCash.ToString(), "2", lbCurrency.Text, lbAmountCash.Text);
                                AppLog.writeLog("after saleProcess.getAmountExchangeRate enterChangebalance true");
                                if (res.response.next)
                                {
                                    foreach (DataRow dr in res.otherData.Rows)
                                    {
                                        if (dr["PaymentSubCode"].ToString() == lbCurrency.Text)
                                        {
                                            amtPrice = double.Parse(dr["Total"].ToString());
                                        }
                                    }
                                }
                                receiveCash = amtPrice;
                            }

                            if (lbTxtReceiveCash.InvokeRequired)
                            {
                                lbTxtReceiveCash.BeginInvoke((MethodInvoker)delegate
                                {
                                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                                });
                            }
                            else
                            {
                                lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                            }

                            balance = double.Parse(strTxtBalance) - receiveCash;
                        }
                        else
                        {
                            string strTxtBalanceDiff = lbTxtBalanceDiff.Text;
                            if (lbAmountCash.Text == "CASH")
                            {
                                amtPrice = double.Parse(formatCash);
                            }
                            else
                            {
                                StoreResult res = saleProcess.getAmountExchangeRate(formatCash.ToString(), "2", lbCurrency.Text, lbAmountCash.Text);
                                AppLog.writeLog("after saleProcess.getAmountExchangeRate enterChangebalance false");
                                if (res.response.next)
                                {
                                    foreach (DataRow dr in res.otherData.Rows)
                                    {
                                        if (dr["PaymentSubCode"].ToString() == lbCurrency.Text)
                                        {
                                            amtPrice = double.Parse(dr["Total"].ToString());
                                        }
                                    }
                                }
                                receiveCash = amtPrice;
                            }
                            //amtPrice = double.Parse(formatCash);
                            //lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - receiveCash).ToString(displayAmt);

                            if (lbTxtReceiveCash.InvokeRequired)
                            {
                                lbTxtReceiveCash.BeginInvoke((MethodInvoker)delegate
                                {
                                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                                });
                            }
                            else
                            {
                                lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                            }


                            balance = double.Parse(strTxtBalanceDiff) - receiveCash;
                        }

                        AppLog.writeLog("before CalBalanceDiff");
                        balance = saleProcess.CalBalanceDiff(balance, "FXCU", ProgramConfig.currencyDefault);

                        //if (double.Parse(lbTxtBalance.Text) <= 0 || double.Parse(lbTxtReceiveCash.Text) >= double.Parse(lbTxtTotalCash.Text))
                        if (balance <= 0 || double.Parse(lbTxtReceiveCash.Text) >= double.Parse(lbTxtBalanceDiff.Text))
                        {
                            double cast = balance * -1;
                            lbTxtBalance.Text = cast.ToString(displayAmt);

                            if (double.Parse(lbTxtBalance.Text) > double.Parse(ProgramConfig.changeLimit))
                            {
                                string responseMessage = ProgramConfig.message.get("frmPayment", "IrregularChange").message;
                                string helpMessage = ProgramConfig.message.get("frmPayment", "IrregularChange").help;
                                notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(ProgramConfig.changeLimit).ToString(displayAmt), ProgramConfig.currencyDefault));

                                //notify = new frmNotify(ResponseCode.Error, "ยอดเงินทอนผิดปกติ!!! กรุณาป้อนจำนวนเงินรับชำระใหม่", "ยอดเงินทอนต้องไม่เกิน " + double.Parse(ProgramConfig.changeLimit).ToString(displayAmt) + " " + ProgramConfig.currencyDefault);
                                notify.ShowDialog(this);
                                loadTempDLYForPayment();
                                return;
                            }

                            double amount = 0;
                            foreach (UCListPayment uc in pn_list_payment.Controls)
                            {
                                amount += Convert.ToDouble(uc.lbAmountText);
                            }

                            StoreResult res = saleProcess.savePaymentCashBalance(amtPrice.ToString(displayAmt), lbTxtBalance.Text, lbAmountCash.Text, txtBalance, amount.ToString(), formatCash.ToString(), lbCurrency.Text);
                            if (!res.response.next)
                            {
                                notify = new frmNotify(res);
                                notify.ShowDialog(this);
                                return;
                            }

                            paymentCode = "CASH";
                            clearAndConfirm();
                        }
                        else
                        {
                            if (ucTxtAmountCash.Text == "0" || checkTextAmount == 0.0)
                            {
                                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                                notify.ShowDialog(this);
                                return;
                            }

                            lbTxtBalance.Text = balance.ToString(displayAmt);
                            StoreResult res = saleProcess.savePaymentCashBalance(amtPrice.ToString(displayAmt), "", lbAmountCash.Text, txtBalance, "-1", formatCash.ToString(), lbCurrency.Text);
                            if (!res.response.next)
                            {
                                notify = new frmNotify(res);
                                notify.ShowDialog(this);
                                return;
                            }

                            SummaryCashIn();
                            RefreshGrid();

                            ucTxtAmountCash.Text = "";
                            ucTxtAmountCash.Focus();
                        }
                    }
                    loadTempDLYForPayment();
                }         
            }
            frmLoading.closeLoading();
        }

        public bool DeleteTempPayment(string pcd, LoadFromTable load = LoadFromTable.TEMPDLYPTRANS, string rec = "")
        {
            ProgramConfig.paymentType = pcd;

            if (load == LoadFromTable.TEMP_PODTRANS_PAY || load == LoadFromTable.TEMPCREDPAY_TRANS_PAY)
            {
                var res = saleProcess.deletePaymentType(rec);
                if (!res.response.next)
                {
                    Utility.AlertMessage(res);
                    return false;
                }
            }
            else
            {
                DataTable dt = saleProcess.selectDataToDeleteCashTempDLY();
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramConfig.paymentAmt = dt.Rows[i]["AMT"].ToString();
                        StoreResult resRec = saleProcess.deletePaymentType(dt.Rows[i]["REC"].ToString());
                        if (resRec.response == ResponseCode.Error)
                        {
                            notify = new frmNotify(ResponseCode.Error, resRec.responseMessage, resRec.helpMessage);
                            notify.ShowDialog(this);
                            return false;
                        }
                    }

                    loadTempDLYForPayment();
                }
            }
            return true;
        }

        private bool DeleteTempPayment(string pcd, out string amt)
        {
            amt = "0";
            ProgramConfig.paymentType = pcd;
            DataTable dt = saleProcess.selectDataToDeleteCashTempDLY();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rec = "";
                    LoadFromTable load;

                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                    {
                        amt = dt.Rows[i]["AMT"].ToString();
                        rec = dt.Rows[i]["REC"].ToString();
                        load = LoadFromTable.TEMP_PODTRANS_PAY;
                    }
                    else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                    {
                        amt = dt.Rows[i]["Payment_Amount"].ToString();
                        rec = dt.Rows[i]["SEQ"].ToString();
                        load = LoadFromTable.TEMPCREDPAY_TRANS_PAY;
                    }
                    else
                    {
                        amt = dt.Rows[i]["AMT"].ToString();
                        rec = dt.Rows[i]["REC"].ToString();
                        load = LoadFromTable.TEMPDLYPTRANS;
                    }

                    ProgramConfig.paymentAmt = amt;
                    StoreResult resRec = saleProcess.deletePaymentType(rec, load);
                    if (resRec.response == ResponseCode.Error)
                    {
                        notify = new frmNotify(ResponseCode.Error, resRec.responseMessage, resRec.helpMessage);
                        notify.ShowDialog(this);
                        return false;
                    }
                }

                loadTempDLYForPayment();
            }
            return true;
        }

        private void DeleteListCouponClick(object sender, EventArgs e)
        {
            try
            {
                
                if (mode == "Select")
                {
                    var ucCoupon = (UCCoupon)sender;
                    string cpNo = ucCoupon.lbCouponNo.Text;
                    string cpAmt = ucCoupon.lbCouponValue.Text;
                    string cpQnt = ucCoupon.lbQty.Text;
                    string cpRow = ucCoupon.lbRow.Text;

                    string responseMessage = ProgramConfig.message.get("frmPayment", "DeleteCoupon").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "DeleteCoupon").help;
                    notify = new frmNotify(ResponseCode.Warning, string.Format(responseMessage, cpNo), helpMessage);

                    //notify = new frmNotify(ResponseCode.Warning, "ต้องการลบคูปองเลขที่ " + cpNo + " ใช่หรือไม่");
                    DialogResult noti_res = notify.ShowDialog(this);
                    if (noti_res == DialogResult.Yes)
                    {
                        
                        frmLoading.showLoading();
                        StoreResult res = saleProcess.saveDeleteCouponFromList(cpNo, Convert.ToInt32(cpQnt), Convert.ToInt32(cpRow));
                        if (!res.response.next)
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog(this);
                            return;
                        }
                        ucCoupon.Parent.Controls.Remove(ucCoupon);
                        RefreshCouponList();
                    }
                }
                else
                {
                    var ucCoupon = (UCCoupon)sender;
                    string cpNo = ucCoupon.lbCouponNo.Text;
                    string cpQnt = ucCoupon.lbQty.Text;
                    string cpRow = ucCoupon.lbRow.Text;

                    //Delete CouponUse
                    string responseMessage = ProgramConfig.message.get("frmPayment", "DeleteCoupon").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "DeleteCoupon").help;
                    notify = new frmNotify(ResponseCode.Warning, string.Format(responseMessage, cpNo), helpMessage);

                    //notify = new frmNotify(ResponseCode.Warning, "ต้องการลบคูปองเลขที่ " + cpNo + " ใช่หรือไม่");
                    DialogResult noti_res = notify.ShowDialog(this);
                    if (noti_res == DialogResult.Yes)
                    {
                        //parent.Controls.Remove(ucCoupon);
                        
                        frmLoading.showLoading();
                        StoreResult resDelete = saleProcess.delCoupon(cpNo, Convert.ToInt32(cpQnt), Convert.ToInt32(Convert.ToDouble(cpRow)));
                        if (resDelete.response.next)
                        {
                            if (resDelete.response == ResponseCode.Information)
                            {
                                notify = new frmNotify(resDelete);
                                notify.ShowDialog(this);
                            }
                            ucCoupon.Parent.Controls.Remove(ucCoupon);
                            RefreshCouponList();
                        }
                        else
                        {
                            notify = new frmNotify(resDelete);
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            try
            {
                UCListPayment ucLP = (UCListPayment)sender;
                //Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                Regex re = new Regex(@"([a-zA-Z]{4})(.*)");
                Match result = re.Match(ProgramConfig.paymentType);
                string alphaPart = result.Groups[1].ToString(); //ProgramConfig.paymentType.ToString().Substring(0, 4);
                string numberPart = result.Groups[2].ToString(); //ProgramConfig.paymentType.ToString().Substring(4, ProgramConfig.paymentType.Length - 4);
                string payCode = alphaPart;
                string payType = "";


                    if (payCode != "" && payCode.Length == 4) //payCode.Length == 4 ดักกรณีเป็น credit
                    {
                        frmLoading.showLoading();
                        DataTable dtPayType = saleProcess.selectPaymentType(payCode);
                        frmLoading.closeLoading();
                        if (dtPayType.Rows.Count > 0)
                        {
                            payType = dtPayType.Rows[0]["PaymentTypeId"].ToString();
                        }
                        else
                        {
                            payType = "-1";
                        }
                    }

                    if (payType == "6")
                    {
                        string responseMessage = ProgramConfig.message.get("frmPayment", "DeleteGifeVoucher").message;
                        string helpMessage = ProgramConfig.message.get("frmPayment", "DeleteGifeVoucher").help;
                        notify = new frmNotify(ResponseCode.Warning, string.Format(responseMessage, numberPart), helpMessage);
                        //notify = new frmNotify(ResponseCode.Warning, "ต้องการลบบัตรของขวัญเลขที่ " + numberPart + " ?");
                        DialogResult noti_res = notify.ShowDialog(this);
                        frmLoading.showLoading();
                        if (noti_res == DialogResult.Yes)
                        {
                            StoreResult resDelGFSL = saleProcess.delGFSL(numberPart);
                            frmLoading.closeLoading();
                            if (!resDelGFSL.response.next)
                            {
                                notify = new frmNotify(resDelGFSL);
                                notify.ShowDialog(this);
                                return;
                            }
                            else
                            {
                                if (resDelGFSL.response == ResponseCode.Information)
                                {
                                    notify = new frmNotify(resDelGFSL);
                                    notify.ShowDialog(this);
                                }


                                if (ucLP.loadFromTable == LoadFromTable.TEMP_PODTRANS_PAY || ucLP.loadFromTable == LoadFromTable.TEMPCREDPAY_TRANS_PAY)
                                {
                                    var res = saleProcess.deletePaymentType(ucLP.UCLP_lbRec.Text, ucLP.loadFromTable);
                                    if (!res.response.next)
                                    {
                                        Utility.AlertMessage(res);
                                        return;
                                    }
                                    pn_list_payment.Controls.Remove((UCListPayment)sender);
                                }
                                else
                                {
                                    DataTable dt = saleProcess.selectDataToDeleteTempDLY();
                                    if (dt.Rows.Count != 0)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            StoreResult res = saleProcess.deletePaymentType(dt.Rows[i]["REC"].ToString());
                                            if (!res.response.next)
                                            {
                                                notify = new frmNotify(res);
                                                notify.ShowDialog(this);
                                                return;
                                            }
                                            pn_list_payment.Controls.Remove((UCListPayment)sender);
                                        }
                                    }
                                }
                            }
                        }
                        loadTempDLYForPayment();
                        frmLoading.closeLoading();
                    }
                    else if (payType == "5")
                    {
                        string responseMessage = ProgramConfig.message.get("frmPayment", "DeletePayment").message;
                        string helpMessage = ProgramConfig.message.get("frmPayment", "DeletePayment").help;
                        notify = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);
                        DialogResult noti_res = notify.ShowDialog(this);

                        if (noti_res == DialogResult.Yes)
                        {
                            if (!DeleteTempPayment(payCode, ucLP.loadFromTable, ucLP.UCLP_lbRec.Text))
                            {
                                return;
                            }

                            if (payCode.StartsWith("QR"))
                            {
                                StoreResult res = saleProcess.delQRPayTrans(payCode);
                                if (!res.response.next)
                                {
                                    Utility.AlertMessage(res.response, res.responseMessage, res.helpMessage);
                                    return;
                                }
                            }
                            loadTempDLYForPayment();
                        }
                    }
                    else
                    {
                        string responseMessage = ProgramConfig.message.get("frmPayment", "DeletePayment").message;
                        string helpMessage = ProgramConfig.message.get("frmPayment", "DeletePayment").help;
                        notify = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);

                        //notify = new frmNotify(ResponseCode.Warning, "คุณต้องการลบรายการ ใช่หรือไม่", "ลบรายการชำระ");
                        DialogResult noti_res = notify.ShowDialog(this);
                        frmLoading.showLoading();
                        if (noti_res == DialogResult.Yes)
                        {
                            if (ucLP.loadFromTable == LoadFromTable.TEMP_PODTRANS_PAY || ucLP.loadFromTable == LoadFromTable.TEMPCREDPAY_TRANS_PAY)
                            {
                                var res = saleProcess.deletePaymentType(ucLP.UCLP_lbRec.Text, ucLP.loadFromTable);
                                if (!res.response.next)
                                {
                                    Utility.AlertMessage(res);
                                    return;
                                }
                                pn_list_payment.Controls.Remove((UCListPayment)sender);
                            }
                            else
                            {
                                DataTable dt = saleProcess.selectDataToDeleteTempDLY();
                                frmLoading.closeLoading();
                                if (dt.Rows.Count != 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        StoreResult res = saleProcess.deletePaymentType(dt.Rows[i]["REC"].ToString());
                                        if (!res.response.next)
                                        {
                                            notify = new frmNotify(res);
                                            notify.ShowDialog(this);
                                            return;
                                        }
                                        pn_list_payment.Controls.Remove((UCListPayment)sender);
                                    }
                                }
                            }
                        }
                        loadTempDLYForPayment();
                        frmLoading.closeLoading();
                    }

                    
                showPaymentCash();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        //private void DeleteTempPaymentDeleteClick(UCListPayment ucLP)
        //{
        //    DataTable dt = new DataTable();
        //    dt = saleProcess.selectDataToDeleteTempDLY(ucLP.lbRec.Text);
        //    if (dt.Rows.Count != 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            StoreResult res = saleProcess.deletePaymentType(dt.Rows[i]["REC"].ToString());
        //            if (!res.response.next)
        //            {
        //                notify = new frmNotify(res.response, res.responseMessage, res.helpMessage);
        //                notify.ShowDialog(this);
        //                return;
        //            }
        //            pn_list_payment.Controls.Remove(ucLP);
        //        }
        //    }
        //}

        private void RefreshGrid()
        {
            List<UCListPayment> lstCashIn = new List<UCListPayment>();
            lstCashIn = pn_list_payment.Controls.Cast<UCListPayment>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_list_payment.Controls.Clear();
            int num = lstCashIn.Count;

            foreach (UCListPayment item in lstCashIn)
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
                pn_list_payment.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_list_payment);
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
            foreach (UCListPayment uc in pn_list_payment.Controls)
            {
                amount += Convert.ToDouble(uc.lbAmountText);
            }
            //lbTxtReceiveCash.Text = amount.ToString(displayAmt);
            //lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - amount).ToString(displayAmt);

            //if (amount >= double.Parse(lbTxtTotalCash.Text))
            if (amount >= double.Parse(lbTxtBalanceDiff.Text))
            {
                lbTxtReceiveCash.Text = amount.ToString(displayAmt);
                //lbTxtBalance.Text = ((double.Parse(lbTxtTotalCash.Text) - amount) * -1).ToString(displayAmt);
                lbTxtBalance.Text = ((double.Parse(lbTxtBalanceDiff.Text) - amount) * -1).ToString(displayAmt);
                //lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - amount).ToString(displayAmt);

                //moni2.lbTxtTotalCash.Text = lbTxtTotalCash.Text;
                monCust.lbTxtTotalCash.Text = lbTxtTotalCash.Text;
                moni2.lbTxtTotalCash.Text = lbTxtBalanceDiff.Text;
                moni2.lbTxtReceive.Text = lbTxtReceiveCash.Text;

                //moni2.lbCurrencyName.Visible = false;
                //moni2.lbRate.Visible = false;
                //moni2.lbRateTotal.Visible = false;
                //moni2.panel_exchange.Controls.Clear();
            }
            else
            {
                lbTxtReceiveCash.Text = amount.ToString(displayAmt);
                //lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - amount).ToString(displayAmt);
                lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - amount).ToString(displayAmt);

                //Form form2 = Application.OpenForms["frmMonitor2Detail"];
                //moni2 = form2 as frmMonitor2Detail;
                monCust.lbTxtTotalCash.Text = lbTxtTotalCash.Text;
                moni2.lbTxtTotalCash.Text = lbTxtBalanceDiff.Text;
                moni2.lbTxtReceive.Text = lbTxtReceiveCash.Text;
                //moni2.lbCurrencyName.Visible = true;
                //moni2.lbRate.Visible = true;
                //moni2.lbRateTotal.Visible = true;
                panel_exchange.Controls.Clear();
                moni2.panel_exchange.Controls.Clear();

                StoreResult res = saleProcess.getAmountExchangeRate(lbTxtBalance.Text, "1", ProgramConfig.payment.getMainCurrency(), ProgramConfig.payment.getPaymentCode(ProgramConfig.payment.getMainCurrency()));
                AppLog.writeLog("after saleProcess.getAmountExchangeRate Method SummaryCashIn");

                if (res.response.next)
                {
                    AppLog.writeLog("after saleProcess.getAmountExchangeRate Method SummaryCashIn res.response.next = true Start");
                    DataTable loadExchange = res.otherData;

                    for (int i = 0; i < loadExchange.Rows.Count; i++)
                    {
                        //AppLog.writeLog("loop Currency = " + loadExchange.Rows[i]["Currency"].ToString() + ", Rate = " + loadExchange.Rows[i]["Rate"].ToString() + "Total = " + loadExchange.Rows[i]["Total"].ToString());
                        UCItemCurrency ucCurren = new UCItemCurrency(i);
                        ucCurren.lbNoText = i.ToString();
                        ucCurren.lbCurrency.Text = loadExchange.Rows[i]["Currency"].ToString();
                        ucCurren.lbRate.Text = loadExchange.Rows[i]["Rate"].ToString();
                        ucCurren.lbTotal.Text = loadExchange.Rows[i]["Total"].ToString();
                        panel_exchange.Controls.Add(ucCurren);
                        panel_exchange.Controls.SetChildIndex(ucCurren, 0);

                        if (i == 0)
                        {
                            monCust.lbCurrencyRate1.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString();
                            monCust.lbAmtCurrency1.Text = loadExchange.Rows[i]["Total"].ToString();
                        }

                        if (i == 1)
                        {
                            monCust.lbCurrencyRate2.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString();
                            monCust.lbAmtCurrency2.Text = loadExchange.Rows[i]["Total"].ToString();
                        }
                        
                        //UCItemCurrency2 ucCurren2 = new UCItemCurrency2(i);
                        //ucCurren2.lbNoText = i.ToString();
                        //ucCurren2.lbCurrency.Text = loadExchange.Rows[i]["Currency"].ToString();
                        //ucCurren2.lbRate.Text = loadExchange.Rows[i]["Rate"].ToString();
                        //ucCurren2.lbTotal.Text = loadExchange.Rows[i]["Total"].ToString();
                        //moni2.panel_exchange.Controls.Add(ucCurren2);
                        //moni2.panel_exchange.Controls.SetChildIndex(ucCurren2, 0);
                    }
                    //RefreshCurrency();
                    Utility.SetGridColorAlternate<UCItemCurrency>
                        (panel_exchange.Controls.Cast<UCItemCurrency>().ToList(), Color.FromArgb(255, 220, 188), Color.FromArgb(255, 188, 128));
                    Utility.SetGridColorAlternate<UCItemCurrency2>
                        (moni2.panel_exchange.Controls.Cast<UCItemCurrency2>().ToList(), Color.FromArgb(255, 220, 188), Color.FromArgb(255, 188, 128));

                    AppLog.writeLog("after saleProcess.getAmoSuntExchangeRate Method SummaryCashIn res.response.next = true End");
                }
                else
                {
                    AppLog.writeLog("after saleProcess.getAmountExchangeRate Method SummaryCashIn res.response.next = false");
                    AppLog.writeLog("Response Code = " + res.response.value + " Response Message = " + res.responseMessage + "");
                    notify = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                    notify.ShowDialog(this);
                    return;
                }


            }
        }

        private bool ValidateData()
        {
            //if (!ValidateDropDown())
            //{
            //    return false;
            //}

            return true;
        }

        //private bool ValidateDropDown()
        //{
        //    if (ucDDCurrency.ValueText == "")
        //    {
        //        string responseMessage = ProgramConfig.message.get("frmPayment", "ChooseCurrency").message;
        //        string helpMessage = ProgramConfig.message.get("frmPayment", "ChooseCurrency").help;
        //        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

        //        //notify = new frmNotify(ResponseCode.Error, "กรุณาเลือกสกุลเงิน");c
        //        notify.ShowDialog(this);
        //        return false;
        //    }
        //    return true;
        //}

        private void ucHeader2_MainMenuClick(object sender, EventArgs e)
        {

            //if (sender is UCHeader)
            //{
            //    var ucDDL = (UCHeader)sender;
            //    var ucDDL_Point = ucDDL.FindForm().PointToClient(ucDDL.Parent.PointToScreen(ucDDL.Location));
            //    if (pn_drop_menu.Visible && pnt == ucDDL_Point)
            //    {
            //        pn_drop_menu.Visible = !pn_drop_menu.Visible;
            //    }
            //    else
            //    {
            //        pnt = ucDDL_Point;
            //        currentUCDDL = ucDDL;
            //        pn_drop_menu.Width = ucDDL.Width - 700;
            //        pn_drop_menu.Location = new Point(ucDDL_Point.X, ucDDL.Height + ucDDL_Point.Y + 1);

            //        pn_drop_menu.Controls.Clear();
            //        pn_drop_menu.Visible = true;
            //        List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();

            //        lstStr = SetDataucDropDownList1();

            //        int h = 0;
            //        int ucH = 0;
            //        int cnt = 1;
            //        int temp = 0;
            //        int widthDD = pn_drop_menu.Width;
            //        int widthLine = 0;
            //        int widthLabel = 0;

            //        BJCBCPOS.UCDropDownList.Dropdown maxStr = lstStr.Select(s => s).OrderByDescending(o => o.DisplayText.Length).FirstOrDefault();

            //        UCItemDropDownList _ucdd = new UCItemDropDownList();
            //        Font font = _ucdd.label1.Font;
            //        widthLine = _ucdd.lineShape1.Width - _ucdd.lineShape1.Location.X;
            //        widthLabel = _ucdd.Width;
            //        temp = TextRenderer.MeasureText(maxStr.DisplayText, font).Width;

            //        if (temp + 13 >= widthDD)
            //        {
            //            widthDD = temp + 13; // 13 คือส่วนต่างของ Size form width กับ Size label width >>>>> Form UCItemDropDownList
            //            widthLabel = temp;
            //        }

            //        if (temp >= widthLine)
            //        {
            //            widthLine = temp + _ucdd.lineShape1.Location.X;
            //        }
            //        else
            //        {
            //            widthLine = widthDD - 13;
            //        }

            //        foreach (BJCBCPOS.UCDropDownList.Dropdown str in lstStr)
            //        {
            //            ucH += 33; // ความสูงของ item >>>>> UCItemDropDownList
            //            h += 33;

            //            UCItemDropDownList ucdd = new UCItemDropDownList();
            //            ucdd.UCItemDropDownListClick += UCItemDropDownListClick;
            //            ucdd.label1.Text = str.DisplayText;
            //            ucdd.label2.Text = str.ValueText;
            //            ucdd.label1.Width = widthLabel;
            //            ucdd.lineShape1.Width = widthLine;

            //            if (cnt == 1)
            //            {
            //                ucdd.lineShape1.Visible = false;
            //            }

            //            cnt++;
            //            pn_drop_menu.Controls.Add(ucdd);
            //        }

            //        if (ucH >= 198) // check ให้ item dropdown มีได้แค่ 6 ถ้ามากกว่านั้น จะมี scroll bar 
            //        {
            //            ucH = 198; // 198 คือ ส่วนสูงของ panel เมื่อมี item 6 ชิ้น
            //            widthDD = widthDD + (widthDD == pn_drop_menu.Width ? 0 : 10); // + scorll bar ที่เพิ่มเข้ามา
            //        }

            //        if (widthDD > this.pn_drop_menu.Width)
            //        {
            //            // set location ไปทางซ้าย
            //            pn_drop_menu.Location = new Point(pn_drop_menu.Location.X - (widthDD - pn_drop_menu.Width), pn_drop_menu.Location.Y);
            //        }

            //        pn_drop_menu.Height = ucH + 3;
            //        pn_drop_menu.Width = widthDD;
            //        pn_drop_menu.BringToFront();
            //        pn_drop_menu.Focus();
            //    }
            //}

            //Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder);
            //if (check.policy == PolicyStatus.Work)
            //{
            //    pn_drop_menu.Enabled = true;
            //}
            //else if (check.policy == PolicyStatus.Skip)
            //{
            //    //ปิดยกเลิกระหว่างขาย
            //    pn_drop_menu.Enabled = false;
            //}

            ////pn_drop_menu.BringToFront();
            ////if (pn_drop_menu.Visible)
            ////{
            ////    pn_drop_menu.Visible = !pn_drop_menu.Visible;
            ////}
            ////else
            ////{
            ////    pn_drop_menu.Visible = !pn_drop_menu.Visible;
            ////    List<string> lstStr = new List<string>();
            ////    lstStr.Add("ยกเลิกระหว่างขาย");
            ////    UCCurrencyDropDown ucdd = new UCCurrencyDropDown(this);
            ////    pn_drop_menu.Height = ucdd.SetDropDown(lstStr) + 20;
            ////    pn_drop_menu.Controls.Add(ucdd);
            ////}

        }

        private void UCItemDropDownListClick(object sender, EventArgs e)
        {
            CancelSale(sender);
        }

        public void CancelSale(object sender)
        {
            try
            {
                string valueReturn = "";
                pn_drop_menu.Visible = false;
                var ucIDDL = (UCHambergerItem)sender;
                if (ucIDDL.MenuID == MenuIdHamberger.CancelReceipt)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder);
                    if (check.profile == ProfileStatus.NotAuthorize)
                    {
                        AuthResult authRes = Utility.CheckAuthPassRes(this, check, "CancelSale");
                        if (!authRes.Next)
                        {
                            showPaymentCash();
                            return;
                        }

                        valueReturn = authRes.maxCancelReceiptAmt;
                    }

                    check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_CheckLimit);
                    if (check.policy == PolicyStatus.Work) //2
                    {
                        //string valueReturn = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CancelOrder_Limit.parameterCode);
                        if (valueReturn == "" || valueReturn == null)
                        {
                            valueReturn = "0";
                        }
                        //if (double.Parse(lbTxtTotalCash.Text) > double.Parse(valueReturn))
                        if (double.Parse(lbTxtBalanceDiff.Text) > double.Parse(valueReturn))
                        {
                            //ไป Step4
                            check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_OverLimit);
                            if (check.policy == PolicyStatus.Skip) //1
                            {
                                string responseMessage = ProgramConfig.message.get("frmPayment", "NotAllowCancelSale").message;
                                string helpMessage = ProgramConfig.message.get("frmPayment", "NotAllowCancelSale").help;
                                notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(valueReturn).ToString(displayAmt)));

                                //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(valueReturn).ToString(displayAmt));
                                notify.ShowDialog(this);
                                pn_drop_menu.Visible = false;
                                return;
                            }
                            else if (check.policy == PolicyStatus.Work) //2
                            {
                                string responseMessage = ProgramConfig.message.get("frmPayment", "NotAllowCancelSale").message;
                                string helpMessage = ProgramConfig.message.get("frmPayment", "NotAllowCancelSale").help;
                                notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(valueReturn).ToString(displayAmt)));

                                //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(valueReturn).ToString(displayAmt));
                                notify.ShowDialog(this);

                                //ข้ามตรวจสอบวงเงินยกเลิกของ User Authorize มาก่อน
                                //Fix Auth (Done)
                                //frmUserAuthorize auth = new frmUserAuthorize("CancelSale", check.diffUserStatus);
                                //auth.function = FunctionID.Sale_CancelWhileSale_CancelOrder_OverLimit;
                                //DialogResult auth_res = auth.ShowDialog(this);
                                //if (auth_res != DialogResult.Yes)
                                //{
                                //    showPaymentCash();
                                //    return;
                                //}

                                AuthResult authRes = Utility.CheckAuthPassRes(this, check, "CancelSale");
                                if (!authRes.Next)
                                {
                                    showPaymentCash();
                                    return;
                                }


                                string checkAgain = authRes.maxCancelReceiptAmt;

                                //string checkAgain = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxCancelReceiptAmt"].ToString();

                                //เช็ควงเงิน User Authorize 
                                //if (double.Parse(checkAgain) >= double.Parse(lbTxtTotalCash.Text))
                                if (double.Parse(checkAgain) >= double.Parse(lbTxtBalanceDiff.Text))
                                {
                                    //Step 5
                                    reasonToCancel();
                                }
                                else
                                {
                                    string message = ProgramConfig.message.get("frmPayment", "NotAllowCancelSale").message;
                                    string help = ProgramConfig.message.get("frmPayment", "NotAllowCancelSale").help;
                                    notify = new frmNotify(ResponseCode.Error, message, string.Format(help, double.Parse(checkAgain).ToString(displayAmt)));

                                    //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(checkAgain).ToString(displayAmt));
                                    notify.ShowDialog(this);
                                    pn_drop_menu.Visible = false;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            reasonToCancel();
                        }
                    }
                    else if (check.policy == PolicyStatus.Skip) //1
                    {
                        //ข้ามไป Step 5
                        reasonToCancel();
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        public void reasonToCancel()
        {
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_CheckLimit);
            if (check.policy == PolicyStatus.Skip) //1
            {
                //ไปทำ Step 7
                string r = "";
                DataTable selectCount = saleProcess.selectTempDlyptrans(ProgramConfig.saleRefNo);
                for (int i = 0; i < selectCount.Rows.Count; i++)
                {
                    r = selectCount.Rows[i]["REC"].ToString();
                    //StoreResult res = saleProcess.updateTempDlyptrans(ProgramConfig.saleRefNo, r);
                    //if (res.response == ResponseCode.Error)
                    //{
                    //    frmNotify dialog = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                    //    dialog.ShowDialog(this);
                    //    return;
                    //}
                }
                string newRec = (Convert.ToInt32(r) + 10001).ToString();

                if (ProgramConfig.superUserId != "N/A")
                {
                    //StoreResult resSave = saleProcess.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec, "0", "H", "Cancel By Super User", "2", "119", "0", ProgramConfig.superUserId, "0"
                    //, "V", "0", "0", "0", "0", "0", "Cancel", "0");
                }
                else
                {
                    //StoreResult resSave = saleProcess.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec, "0", "H", "Cancel By User", "2", "119", "0", ProgramConfig.userId, "0"
                    //, "V", "0", "0", "0", "0", "0", "Cancel", "0");
                }

                //StoreResult resSaveCancelTran = saleProcess.saveCancelSaleTransaction(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction);
                StoreResult resSaveCancelTran = new StoreResult(ResponseCode.Success, "", "");

                if (resSaveCancelTran.response == ResponseCode.Error)
                {
                    notify = new frmNotify(ResponseCode.Error, resSaveCancelTran.responseMessage, resSaveCancelTran.helpMessage);
                    notify.ShowDialog(this);
                    return;
                }
                else if (resSaveCancelTran.response == ResponseCode.Success)
                {
                    if (resSaveCancelTran.otherData != null)
                    {
                        ProgramConfig.abbNo = resSaveCancelTran.otherData.Rows[0]["CancelNo"].ToString();
                    }

                    Profile checkPro = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank);
                    if (checkPro.policy == PolicyStatus.Skip) //1
                    {
                        //Step9
                        printCancel(FunctionID.Sale_CancelWhileSale_CancelOrder_PrintCancelDocument);
                    }
                    else if (checkPro.policy == PolicyStatus.Work) //2
                    {
                        string eventName = "Cancel";
                        string refNo = ProgramConfig.saleRefNo;
                        string rec = "1";
                        StoreResult res = saleProcess.syncToDataTank(eventName, FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank, refNo, rec);
                        if (res.response.next)
                        {
                            if (res.response == ResponseCode.Information)
                            {
                                notify = new frmNotify(res);
                                notify.ShowDialog(this);
                            }

                            printCancel(FunctionID.Sale_CancelWhileSale_CancelOrder_PrintCancelDocument);
                        }
                        else
                        {
                            notify = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                            notify.ShowDialog(this);
                            return;
                        }

                    }
                }
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                frmDeleteReason dialog = new frmDeleteReason();
                DialogResult auth_res = dialog.ShowDialog(this);
                if (auth_res != DialogResult.Yes)
                {
                    showPaymentCash();
                    return;
                }
            }
            Program.control.ShowForm("frmSale");
        }

        private void printCancel(FunctionID function)
        {
            Profile check = ProgramConfig.getProfile(function);
            if (check.policy == PolicyStatus.Skip) //1
            {
                closeForm();
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                StoreResult result = saleProcess.printCancel(function);
                if (!result.response.next)
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                    return;
                }
                else
                {
                    if (result.response == ResponseCode.Information)
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                    }

                    DataTable dt = result.otherData;
                    Hardware.printTermal(dt);
                    this.Dispose();

                    closeForm();
                }
            }
        }

        private void closeForm()
        {
            ucFooterTran1.mainContent = "";
            ucFooterTran1.fullContent = "";
            ucFooterTran1.functionId = "";

            Program.control.ShowForm("frmMainMenu");
            Program.control.CloseForm("frmPayment");
            Program.control.CloseForm("frmSale");
        }

        private void ucTxtCardNo_EnterFromButton2(object sender, EventArgs e)
        {
            try
            {
                keyCredit((UCTextBoxWithIcon)sender, lbDisplayCreditCard_remove, ucTxtAmount, ucTxtApprove);
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtCardNo_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                keyCredit((UCTextBoxWithIcon)sender, lbDisplayCreditCard_remove, ucTxtAmount, ucTxtApprove);
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        private void keyCredit(UCTextBoxWithIcon txtCardno, Label labelDisplay, UCTextBoxWithIcon txtCreditAmt, UCTextBoxWithIcon txtCreditApprove)
        {
            if (txtCardno.Text.Trim() != "")
            {
                //edcStatus = "1";
                cardSwipe = txtCardno.Text;
                string cash = "CASH";
                string cardNo = cash + txtCardno.Text;
                frmLoading.showLoading();
                StoreResult read = saleProcess.getPaymentCode(cardNo);
                frmLoading.closeLoading();

                if (read.response.next)
                {                   
                    if (read.response == ResponseCode.Information)
                    {
                        //Fix Case Co-Brand (Done)
                        ClearPaymentData();
                        notify = new frmNotify(read);
                        notify.ShowDialog(this);
                        txtCardno.Focus();
                        return;
                    }

                    txtCardno.EnabledUC = false;
                    txtCardno.Visible = false;
                    DataTable dt = read.otherData;
                    paymentCode = dt.Rows[0]["PaymentCodeDisplay"].ToString();
                    paymentCode = paymentCode.Replace(" ", "");
                    paymentNumber = dt.Rows[0]["PaymentNumberDisplay"].ToString();
                    creditCard = dt.Rows[0]["CreditCard"].ToString();
                    creditCard = creditCard.Replace(" ", "");

                    //lbCardType.Text = paymentCode;
                    //ucTxtCardNo.Text = paymentNumber;
                    labelDisplay.Visible = true;
                    labelDisplay.Text = creditCard;

                    if (edcStatus == "1")
                    {
                        //offline
                        lbApprove_remove.Visible = true;
                        txtCreditApprove.Visible = true;
                    }
                    else if (edcStatus == "2")
                    {
                        //online
                        lbApprove_remove.Visible = false;
                        txtCreditApprove.Visible = false;
                    }

                    if (paymentCode.Length > 4)
                    {
                        paymentCode = paymentCode.Substring(0, 4);
                    }

                    saleProcess.PaymentDiscount(paymentCode, labelDisplay.Text);
                    loadDiscount();

                    if (dt.Rows[0]["IsCobrand"].ToString().Trim() == "Y")
                    {
                        string memberCardNo = dt.Rows[0]["MemCardNo"].ToString().Trim();
                        //search member


                        var res = saleProcess.searchMember(1, memberCardNo);
                        if (res.response.next)
                        {
                            string memberID = res.otherData.Rows[0]["MemberID"].ToString();

                            //getmember profile
                            res = saleProcess.getMemberProfile(memberID);
                            if (res.response.next)
                            {
                                string card = creditCard.Substring(4, creditCard.Length - 4);
                                res = saleProcess.SpecialPay(paymentCode, card, lbTxtBalance.Text, res.otherData.Rows[0]["Member_ID"].ToString());
                                if (res.response.next)
                                {
                                    double disc = 0;
                                    double.TryParse(res.otherData.Rows[0]["PMDISC_AMT"].ToString(), out disc);

                                    string pmCode = res.otherData.Rows[0]["PMDISC_CODE"].ToString();
                                    if (disc > 0)
                                    {
                                        //insert SDCB
                                        res = saleProcess.savePaymentCreditBalance(pmCode, "", "O", disc.ToString(), "", false, cardNo);
                                        if (res.response.next)
                                        {
                                            loadTempDLYForPayment();
                                        }
                                    }
                                }
                                else
                                {
                                    Utility.AlertMessage(res);
                                }
                            }
                        }
                    }

                    txtCreditAmt.Visible = true;
                    txtCreditAmt.Text = lbTxtBalance.Text;
                    txtCreditAmt.Focus();
                    frmLoading.closeLoading();
                }
                else
                {
                    ClearPaymentData();
                    notify = new frmNotify(read.response, read.responseMessage, read.helpMessage);
                    notify.ShowDialog(this);
                    frmLoading.closeLoading();
                    txtCardno.Focus();
                }
            }
            else
            {
                ClearPaymentData();
                frmLoading.closeLoading();
                //notify = new frmNotify(ResponseCode.Information, "กรุณาใส่หมายเลขบัตรเครดิต", "");
                notify = new frmNotify(ResponseCode.Information
                                        , ProgramConfig.message.get("frmPayment", "PlsEnterCreditCardCode").message
                                        , ProgramConfig.message.get("frmPayment", "PlsEnterCreditCardCode").message);
                notify.ShowDialog(this);
                txtCardno.Focus();
            }
        }

        private void ucTxtAmount_EnterFromButton(object sender, EventArgs e)
        {
            try
            {
                addCreditAmount();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtAmount_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                if (ucTxtAmount.Text.Trim() != "")
                {
                    //addCreditAmount();
                    ucTxtApprove.Focus();
                }
                else
                {
                    //"กรุณาระบุจำนวนเงิน";
                    string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                    notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                    notify.ShowDialog(this);
                    ucTxtAmount.Focus();
                }
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void addCreditAmount()
        {
            dty = "";
            if (ucTxtApprove.Visible == true)
            {
                dty = "F";
                if (ValidateCredit(dty))
                {
                    addCreditTogrid();
                }
            }
            else
            {
                dty = "O";
                if (ValidateCredit(dty))
                {
                    addCreditTogrid();
                }
            }
        }

        private bool ValidateCredit(string dty)
        {
            if (ucTxtAmount.Text.Trim() != "")
            {
                if (!ProgramConfig.payment.getExcessChange(paymentCode))
                {
                    if (Convert.ToDouble(ucTxtAmount.Text.Trim()) > Convert.ToDouble(lbTxtBalance.Text))
                    {
                        //"ยอดเงินที่จ่ายมากกว่ายอดชำระ\nกรุณาตรวจสอบอีกครั้ง";
                        string responseMessage = ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message;//ProgramConfig.message.get("frmPayment", "PleaseInputApproveCode").message;
                        string helpMessage = ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help;//ProgramConfig.message.get("frmPayment", "PleaseInputApproveCode").help;
                        notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                        notify.ShowDialog(this);
                        ucTxtAmount.Focus();
                        return false;
                    }
                }

                frmLoading.showLoading();
                StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount("CASH", ucTxtAmount.Text.Trim(), ProgramConfig.currencyDefault);
                frmLoading.closeLoading();
                if (!chkMinCash.response.next)
                {
                    notify = new frmNotify(chkMinCash.response, chkMinCash.responseMessage, chkMinCash.helpMessage);
                    notify.ShowDialog();
                    ucTxtAmount.Focus();
                    return false;
                }

                if (ucTxtApprove.Text.Trim() == "" && dty == "F")
                {
                    string responseMessage = ProgramConfig.message.get("frmPayment", "PleaseInputApproveCode").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "PleaseInputApproveCode").help;
                    notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                    notify.ShowDialog(this);
                    ucTxtApprove.Focus();
                    return false;
                }
                else
                {
                    if (ucTxtApprove.Text.Trim().Length != 6 && dty == "F")
                    {
                        //"กรุณาระบุ Approve Code ให้ครบ 6 หลัก"
                        string responseMessage = ProgramConfig.message.get("frmPayment", "PlsInputApproveCode6Digit").message;
                        string helpMessage = ProgramConfig.message.get("frmPayment", "PlsInputApproveCode6Digit").help;
                        notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                        notify.ShowDialog(this);
                        ucTxtApprove.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }                   
                }
            }
            else
            {
                //"กรุณาระบุจำนวนเงิน"
                string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                notify.ShowDialog(this);
                ucTxtAmount.Focus();
                return false;
            }
        }

        private void addCreditTogrid()
        {
            double amount = 0.0;
            if (ucTxtAmount.Text == "" && double.Parse(lbTxtBalance.Text) <= 0)
            {
                frmLoading.showLoading();
                SummaryCashIn();
                frmLoading.closeLoading();
                RefreshGrid();
                ShowConfirmPayment();
            }
            else
            {
                if (!double.TryParse(ucTxtAmount.Text, out amount))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Warning, "Please Input Approve Code.");
                    notify.ShowDialog(this);
                    return;
                }


                ProgramConfig.paymentType = creditCard;
                frmLoading.showLoading();
                DataTable dt = saleProcess.selectDataToDeleteCashTempDLY();
                frmLoading.closeLoading();
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string amt = "";
                        string rec = "";
                        LoadFromTable load;

                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                        {
                            amt = dt.Rows[i]["AMT"].ToString();
                            rec = dt.Rows[i]["REC"].ToString();
                            load = LoadFromTable.TEMP_PODTRANS_PAY;
                        }
                        else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                        {
                            amt = dt.Rows[i]["Payment_Amount"].ToString();
                            rec = dt.Rows[i]["SEQ"].ToString();
                            load = LoadFromTable.TEMPCREDPAY_TRANS_PAY;
                        }
                        else
                        {
                            amt = dt.Rows[i]["AMT"].ToString();
                            rec = dt.Rows[i]["REC"].ToString();
                            load = LoadFromTable.TEMPDLYPTRANS;
                        }

                        ProgramConfig.paymentAmt = amt;
                        StoreResult resRec = saleProcess.deletePaymentType(rec, load);
                        if (resRec.response == ResponseCode.Success)
                        {
                            loadTempDLYForPayment();
                        }
                        else if (resRec.response == ResponseCode.Error)
                        {
                            notify = new frmNotify(ResponseCode.Error, resRec.responseMessage, resRec.helpMessage);
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                }

                SummaryCashIn();
                RefreshGrid();

                double receiveCash = 0;

                receiveCash = double.Parse(ucTxtAmount.Text);

                string formatCash = "";
                formatCash = receiveCash.ToString(displayAmt);

                if (double.Parse(lbTxtReceiveCash.Text) != 0)
                {
                    amtPrice = double.Parse(formatCash);
                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
                }
                else
                {
                    amtPrice = double.Parse(formatCash);
                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - receiveCash).ToString(displayAmt);
                }

                if (double.Parse(lbTxtBalance.Text) <= 0)
                {
                    double cast = double.Parse(lbTxtBalance.Text) * -1;
                    lbTxtBalance.Text = cast.ToString(displayAmt);

                    StoreResult result = saleProcess.savePaymentCreditBalance(creditCard, ucTxtApprove.Text, dty, amtPrice.ToString(displayAmt), lbTxtBalance.Text, dty == "F", cardSwipe);
                    if (result.response.next)
                    {
                        this.ucKeyboard1.Visible = false;
                        this.Refresh();
                        ShowConfirmPayment(dty == "F");
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }
                    //loadTempDLYForPayment();
                    //ClearPaymentData();
                }
                else
                {
                    StoreResult result = saleProcess.savePaymentCreditBalance(creditCard, ucTxtApprove.Text, dty, amtPrice.ToString(displayAmt), "", dty == "F", cardSwipe);
                    if (!result.response.next)
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }

                    ucTxtCardNo.Text = "";
                    ucTxtAmount.Text = "";
                    ucTxtApprove.Text = "";
                    lbApprove_remove.Visible = false;
                    ucTxtApprove.Visible = false;
                    ucTxtAmount.Focus();

                    //SummaryCashIn();
                    //RefreshGrid();
                    ClearPaymentData();
                    loadTempDLYForPayment();
                    this.ucKeyboard1.Visible = false;
                    this.Update();
                    btnCash_Click(null, null);
                }
                pn_payment_credit.Visible = false;
                pn_payment_credit_Mapping.Visible = false;
                pictureBox2.Visible = false;                
            }            
        }

        private void ucTxtApprove_EnterFromButton(object sender, EventArgs e)
        {
            try
            {
                addCreditAmount();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtApprove_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                addCreditAmount();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtVoucherNo_EnterFromButton(object sender, EventArgs e)
        {
            try
            {
                seqPaymentStep = 2;
                RunModuleParameter pModule = new RunModuleParameter();
                RunModule(seqPaymentStep, pModule);
                //keyVoucher();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }        
        }

        private void ucTxtVoucherNo_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                seqPaymentStep = 2;
                RunModuleParameter pModule = new RunModuleParameter();
                RunModule(seqPaymentStep, pModule);
                //keyVoucher();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }    
        }

        private bool keyVoucher(out string pmCode)
        {
            pmCode = "";
            string voucherNo = ucTxtVoucherNo.Text;
            string voucherAmt;

            if (voucherNo != "")
            {
                frmLoading.showLoading();
                StoreResult read = saleProcess.checkGiftVoucher(voucherNo);
                frmLoading.closeLoading();
                if (read.response == ResponseCode.Success)
                {
                    DataTable dt = read.otherData;
                    paymentCode = dt.Rows[0]["PaymentCode"].ToString().Trim();
                    //var lstPMPolicy = ProgramConfig.paymentPolicy.GetPaymentPolicyByFunction(FunctionID.PaymentForHirePurchase);
                    //if (lstPMPolicy.Any(a => a.paymentCode == paymentCode))
                    //{
                        if (dt.Rows[0]["GiftVoucherAmt"].ToString() == "")
                        {
                            voucherAmt = "";
                        }
                        else
                        {
                            double dbVoucherAmt = double.Parse(dt.Rows[0]["GiftVoucherAmt"].ToString());
                            //dbVoucherAmt = 400;
                            voucherAmt = dbVoucherAmt.ToString(displayAmt);

                            StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount(paymentCode.Replace(" ", ""), dbVoucherAmt.ToString(), defaultCurrency);
                            if (!chkMinCash.response.next)
                            {
                                notify = new frmNotify(ResponseCode.Error, chkMinCash.responseMessage, chkMinCash.helpMessage);
                                notify.ShowDialog();
                                return false;
                            }
                        }

                        paymentCode = paymentCode.Replace(" ", "");
                        lbVoucher.Text = paymentCode;
                        pmCode = paymentCode;
                        //saleProcess.PaymentDiscount(paymentCode, "");
                        //loadDiscount();

                        ucTxtVoucherAmt.Text = voucherAmt;
                        ucTxtVoucherAmt.EnabledUC = false;
                        ucTxtVoucherAmt.Focus();
                    //}
                    //else
                    //{
                    //    ClearPaymentData();
                    //    //TO DO Change Language Done
                    //    Utility.AlertMessage(ResponseCode.Error, String.Format(ProgramConfig.message.get("frmPayment", "CannotCoPayment").message, paymentCode), ProgramConfig.message.get("frmPayment", "CannotCoPayment").help);
                    //    return false;
                    //}
                }
                else
                {
                    ClearPaymentData();
                    notify = new frmNotify(ResponseCode.Error, read.responseMessage, read.helpMessage);
                    notify.ShowDialog(this);
                    return false;
                }          
            }
            else
            {
                frmLoading.closeLoading();
                string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyGifeVoucher").message;
                string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyGifeVoucher").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Information, "กรุณากรอกหมายเลขบัตรกำนัล");
                notify.ShowDialog(this);
                ucTxtVoucherNo.Focus();
                return false;
            }

            return true;
        }

        private void ucTxtAmount1_EnterFromButton(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                addVoucherAmt();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
                frmLoading.closeLoading();
            }
            frmLoading.closeLoading();
        }

        private void ucTxtAmount1_TextBoxKeydown(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                addVoucherAmt();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
                frmLoading.closeLoading();
            }
            frmLoading.closeLoading();
        }

        private void addVoucherAmt()
        {
            upc = ucTxtVoucherAmt.Text;
            double amount;
            if (ucTxtVoucherAmt.Text == "" && double.Parse(lbTxtBalance.Text) <= 0)
            {
                SummaryCashIn();
                RefreshGrid();
                ShowConfirmPayment();

                //frmConfirmPayment frmConfirmPm = new frmConfirmPayment();
                //frmConfirmPm.Show(this);
                //frmConfirmPm.lbConfirmCash = lbTxtTotalCash.Text;
                //frmConfirmPm.lbConfirmPayment = lbTxtReceiveCash.Text;
                //frmConfirmPm.lbConfirmBalance = lbTxtBalance.Text;
                //Program.control.ShowForm("frmConfirmPayment");
                //frmConfirmPm.OpenCashDrawer();
            }
            else
            {
                if (!double.TryParse(upc, out amount))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Warning, "Please Input Approve Code.");
                    notify.ShowDialog(this);
                    return;
                }

                double receiveCash = 0;
                string paymentType = "";

                receiveCash = double.Parse(ucTxtVoucherAmt.Text);

                string formatCash = "";
                formatCash = receiveCash.ToString(displayAmt);

                RefreshGrid();
                SummaryCashIn();

                if (double.Parse(lbTxtReceiveCash.Text) != 0)
                {
                    amtPrice = double.Parse(formatCash);
                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
                }
                else
                {
                    amtPrice = double.Parse(formatCash);
                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    //lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - receiveCash).ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - receiveCash).ToString(displayAmt);
                }

                //ถึงตรงนี้
                if (double.Parse(lbTxtBalance.Text) <= 0)
                {
                    double cashbalance = 0;
                    double cast = double.Parse(lbTxtBalance.Text) * -1;
                    lbTxtBalance.Text = cashbalance.ToString(displayAmt);
                    //double cast = 0;
                    //lbTxtBalance.Text = cast.ToString(displayAmt);

                    double amountPay = 0;
                    foreach (UCListPayment uc in pn_list_payment.Controls)
                    {
                        amountPay += Convert.ToDouble(uc.lbAmountText);
                    }

                    StoreResult result = saleProcess.savePaymentVoucherBalance(ucTxtVoucherNo.Text, paymentCode, amtPrice.ToString(displayAmt), cast.ToString(displayAmt), upc, lbTxtBalanceDiff.Text, amountPay.ToString());
                    if (result.response.next)
                    {
                        clearAndConfirmVoucher();
                        return;
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }
                }
                else
                {
                    StoreResult result = saleProcess.savePaymentVoucherBalance(ucTxtVoucherNo.Text, paymentCode, amtPrice.ToString(displayAmt), "", upc, lbTxtBalanceDiff.Text, "0");
                    if (!result.response.next)
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }

                    ucTxtVoucherNo.Text = "";
                    ucTxtVoucherAmt.Text = "";
                    ucTxtVoucherNo.Focus();

                    SummaryCashIn();
                    RefreshGrid();
                    loadTempDLYForPayment();
                    btnCash_Click(this, null);
                }
            }
            
            loadTempDLYForPayment();
        }

        private void ucTxtCouponNo_EnterFromButton(object sender, EventArgs e)
        {
            try
            {
                keyCoupon();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtCouponNo_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                keyCoupon();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void keyCoupon()
        {
            frmLoading.showLoading();
            try
            {
                string member;
                string couponNo = ucTxtCouponNo.Text;
                int couponQnt = 0;
                if (ucTxtCouponQnt.Text == "")
                {
                    ucTxtCouponQnt.Text = "1";
                }
                else if (!int.TryParse(ucTxtCouponQnt.Text, out couponQnt))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Information, "กรุณากรอกหมายเลขบัตรกำนัล");
                    notify.ShowDialog(this);
                    ucTxtCouponQnt.Focus();
                    return;
                }

                //if (ProgramConfig.memberId == null)
                //{
                //    ProgramConfig.memberId = "N/A";
                //}

                if (couponNo == "" && double.Parse(lbTxtBalance.Text) <= 0)
                {
                    SummaryCashIn();
                    RefreshGrid();
                    ShowConfirmPayment();
                    //frmConfirmPayment frmConfirmPm = new frmConfirmPayment();
                    //frmConfirmPm.Show(this);
                    //frmConfirmPm.lbConfirmCash = lbTxtTotalCash.Text;
                    //frmConfirmPm.lbConfirmPayment = lbTxtReceiveCash.Text;
                    //frmConfirmPm.lbConfirmBalance = lbTxtBalance.Text;
                    //Program.control.ShowForm("frmConfirmPayment");
                    //frmConfirmPm.OpenCashDrawer();
                }
                else
                { 

                    StoreResult read = saleProcess.checkCoupon(couponNo, Convert.ToInt32(ucTxtCouponQnt.Text), ProgramConfig.memberId);
                    if (read.response == ResponseCode.Success)
                    {
               
                        //Old Code
                        //using (frmAddCoupon frm = new frmAddCoupon())
                        //{
                        //    // passing this in ShowDialog will set the .Owner 
                        //    // property of the child form
                        //    frm.couponNo = ucTxtCouponNo.Text;
                        //    frm.couponQnt = ucTxtCouponQnt.Text;
                        //    frm.loadAddCoupon();
                        //    frm.ShowDialog(this);
                        //}

                        string formatCash = "";

                        pn_page_coupon.Visible = true;
                        pn_page_coupon.BringToFront();
                        btnShowListCoupon.Enabled = false;

                        DataTable dt = read.otherData;
                        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                        {
                            int num;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                payCode = dt.Rows[i]["PaymentCode"].ToString();
                                couNo = dt.Rows[i]["CouponNo"].ToString();
                                couAmt = dt.Rows[i]["CouponAmt"].ToString();
                                proCode = dt.Rows[i]["PR_CODE"].ToString();
                                vRow = dt.Rows[i]["Row"].ToString();
                                couponType = dt.Rows[i]["CouponType"].ToString();

                                payCode = payCode.Replace(" ", "");

                                dt2Data.Rows.Add(payCode, couNo, couAmt, ucTxtCouponQnt.Text, proCode, vRow);

                                double receiveCash = double.Parse(couAmt);
                                formatCash = receiveCash.ToString(displayAmt);
                                StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount(payCode, couAmt, defaultCurrency);
                                if (!chkMinCash.response.next)
                                {
                                    notify = new frmNotify(ResponseCode.Error, chkMinCash.responseMessage, chkMinCash.helpMessage);
                                    notify.ShowDialog();
                                }
                                else
                                {
                                    if (chkMinCash.response == ResponseCode.Information)
                                    {
                                        notify = new frmNotify(chkMinCash);
                                        notify.ShowDialog(this);
                                    }

                                    if (pn_coupon_list.Controls.Count > 0)
                                    {
                                        num = pn_coupon_list.Controls.Cast<UCCoupon>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
                                    }
                                    else
                                    {
                                        pictureBox2.Image = null;
                                        num = 1;
                                        Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2, pictureBox5);
                                    }

                                    UCCoupon ucList = new UCCoupon();
                                    ucList.lbNo.Text = num.ToString();
                                    //ucList.UCGridViewItemSellClick += UCGridViewItemSellClick;
                                    ucList.DeleteClick += DeleteListCouponClick;
                                    ucList.paymentCode.Text = payCode;
                                    ucList.lbCouponNo.Text = couNo;
                                    ucList.lbCouponValue.Text = formatCash;
                                    ucList.lbQty.Text = ucTxtCouponQnt.Text;
                                    ucList.lbProductCode.Text = proCode;
                                    ucList.lbRow.Text = vRow;
                                    ucList.UCC_lbcouponType.Text = couponType;
                                    pn_coupon_list.Controls.Add(ucList);

                                    ucTxtCouponNo.Text = "";
                                    ucTxtCouponQnt.Text = "";
                                    ucTxtCouponNo.Focus();
                                }
                            }
                        }

                        RefreshCouponList();
                        frmLoading.closeLoading();
                    }
                    else
                    {
                        frmLoading.closeLoading();
                        ClearPaymentData();
                        notify = new frmNotify(read);
                        notify.ShowDialog(this);
                        ucTxtCouponNo.Focus();
                        return;
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                if (!ProcessCheckNetWorkLost(net))
                {
                    frmLoading.closeLoading();
                    ClearPaymentData();
                    ucTxtCouponNo.Focus();
                }
                //Program.control.RetryConnection(net.errorType);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            
        }

        private void loadCoupon()
        {
            pn_coupon_list.Controls.Clear();
            if (dt2Data != null && dt2Data.Rows != null && dt2Data.Rows.Count > 0)
            {
                int num;
                string formatCash = "";

                for (int i = 0; i < dt2Data.Rows.Count; i++)
                {
                    payCode = dt2Data.Rows[i]["PaymentCode"].ToString();
                    couNo = dt2Data.Rows[i]["CouponNo"].ToString();
                    string value = dt2Data.Rows[i]["CouponValue"].ToString();
                    couAmt = dt2Data.Rows[i]["CouponQnt"].ToString();
                    proCode = dt2Data.Rows[i]["Barcode"].ToString();
                    vRow = dt2Data.Rows[i]["Row"].ToString();

                    double receiveCash = double.Parse(value);
                    formatCash = receiveCash.ToString(displayAmt);

                    double amt = double.Parse(couAmt);
                    couAmt = amt.ToString(displayAmt);
                    StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount(payCode, couAmt, defaultCurrency);
                    if (!chkMinCash.response.next)
                    {
                        notify = new frmNotify(chkMinCash);
                        notify.ShowDialog();
                    }
                    else
                    {
                        if (chkMinCash.response == ResponseCode.Information)
                        {
                            notify = new frmNotify(chkMinCash);
                            notify.ShowDialog(this);
                        }

                        if (pn_coupon_list.Controls.Count > 0)
                        {
                            num = pn_coupon_list.Controls.Cast<UCCoupon>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
                        }
                        else
                        {
                            pictureBox2.Image = null;
                            num = 1;
                            Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2, pictureBox5);
                        }

                        UCCoupon ucList = new UCCoupon();
                        ucList.lbNo.Text = num.ToString();
                        //ucList.UCGridViewItemSellClick += UCGridViewItemSellClick;
                        ucList.DeleteClick += DeleteListCouponClick;
                        ucList.paymentCode.Text = payCode;
                        ucList.lbCouponNo.Text = couNo;
                        ucList.lbCouponValue.Text = formatCash;
                        ucList.lbQty.Text = couAmt;
                        ucList.lbProductCode.Text = proCode;
                        ucList.lbRow.Text = vRow;
                        pn_coupon_list.Controls.Add(ucList);

                        ucTxtCouponNo.Text = "";
                        ucTxtCouponQnt.Text = "";
                        ucTxtCouponNo.Focus();
                    }
                }
            }

            RefreshCouponList();
        }

        public void RefreshCouponList()
        {

            List<UCCoupon> lstCoupon = new List<UCCoupon>();
            lstCoupon = pn_coupon_list.Controls.Cast<UCCoupon>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_coupon_list.Controls.Clear();
            int num = lstCoupon.Count;

            foreach (UCCoupon item in lstCoupon)
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
                pn_coupon_list.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_coupon_list);
        }

        public void frmAddCouponData(string payCode, string couNo, string couAmt)
        {
            //int num;
            //sty = "0";
            //vty = "P";
            //qnt = "1";
            //egp = "0";
            //stt = "";
            //pdisc = "0";
            //discid = "0";
            //upc = "";
            //dty = "";
            //discamt = "0";
            //reason = "0";
            //stv = "0";

            //string amtTye = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_AmountType.parameterCode);
            //string displayAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_AmountDisplay.parameterCode);

            //double receiveCash = 0;

            //receiveCash = double.Parse(couAmt);

            //string formatCash = "";
            //if (amtTye == "1") //ทศนิยม
            //{
            //    if (displayAmt == "1")
            //    {
            //        formatCash = receiveCash.ToString("#,###,###.00");
            //    }
            //    else if (displayAmt == "2")
            //    {
            //        formatCash = receiveCash.ToString("#,###,###");
            //    }
            //    else if (displayAmt == "3")
            //    {
            //        formatCash = receiveCash.ToString("#.###.###,00");
            //    }
            //}
            //else if (amtTye == "1") //ไม่มีทศนิยม
            //{
            //    if (displayAmt == "2")
            //    {
            //        formatCash = receiveCash.ToString("#,###,###");
            //    }
            //    else
            //    {
            //        formatCash = receiveCash.ToString("#,###,###");
            //    }
            //}

            //if (pn_list_payment.Controls.Count > 0)
            //{
            //    num = pn_list_payment.Controls.Cast<UCListPayment>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
            //}
            //else
            //{
            //    num = 1;
            //}

            //UCListPayment ucList = new UCListPayment();
            //ucList.lbNo.Text = num.ToString();
            //ucList.DeleteClick += DeleteClick;
            //ucList.lbPaymentType.Text = payCode + "     " + couNo;
            //ucList.lbAmount.Text = formatCash;
            //pn_list_payment.Controls.Add(ucList);
            //cnt++;

            //if (double.Parse(lbTxtReceiveCash.Text) != 0)
            //{
            //    amtPrice = double.Parse(formatCash);
            //    lbTxtReceiveCash.Text = amtPrice.ToString("0.00");
            //    lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString("0.00");
            //}
            //else
            //{
            //    amtPrice = double.Parse(formatCash);
            //    lbTxtReceiveCash.Text = amtPrice.ToString("0.00");
            //    lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - receiveCash).ToString("0.00");
            //}

            //if (double.Parse(lbTxtBalance.Text) < 0)
            //{
            //    double cast = double.Parse(lbTxtBalance.Text) * -1;
            //    lbTxtBalance.Text = cast.ToString("N2");

            //    //saveTempPay
            //    StoreResult res = saleProcess.saveTempPay(ProgramConfig.saleRefNo, vty, payCode + "." + couNo, amtPrice.ToString("0.00"), lbTxtBalance.Text, "0.00", "0.00");
            //    if (!res.response.next)
            //    {
            //        frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
            //        dialog.ShowDialog(this);
            //        return;
            //    }

            //    string maxRec = saleProcess.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo).Rows[0]["REC"].ToString();
            //    int maxRecInt = Convert.ToInt32(maxRec) + 1;
            //    maxRec = maxRecInt.ToString();

            //    StoreResult resSave = saleProcess.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec, sty, vty, payCode + "." + couNo, qnt
            //        , amtPrice.ToString("N2"), lbTxtBalance.Text, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);


            //    if (!resSave.response.next)
            //    {
            //        frmNotify dialog = new frmNotify(resSave.response, resSave.responseMessage, resSave.helpMessage);
            //        dialog.ShowDialog(this);
            //        return;
            //    }

            //    frmConfirmPayment frmConfirmPm = new frmConfirmPayment();
            //    frmConfirmPm.Show(this);
            //    frmConfirmPm.lbTxtCash.Text = lbTxtTotalCash.Text;
            //    frmConfirmPm.lbTxtPayment.Text = lbTxtReceiveCash.Text;
            //    frmConfirmPm.lbTxtBalance.Text = lbTxtBalance.Text;
            //    Program.control.ShowForm("frmConfirmPayment");
            //}
            //else
            //{
            //    //saveTempPay
            //    StoreResult res = saleProcess.saveTempPay(ProgramConfig.saleRefNo, vty, payCode + "." + couNo, amtPrice.ToString("0.00"), "", "0.00", "0.00");
            //    if (!res.response.next)
            //    {
            //        frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
            //        dialog.ShowDialog(this);
            //        return;
            //    }

            //    string maxRec = saleProcess.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo).Rows[0]["REC"].ToString();
            //    int maxRecInt = Convert.ToInt32(maxRec) + 1;
            //    maxRec = maxRecInt.ToString();

            //    StoreResult resSave = saleProcess.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec, sty, vty, payCode + "." + couNo, qnt
            //        , amtPrice.ToString("N2"), "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);


            //    if (!resSave.response.next)
            //    {
            //        frmNotify dialog = new frmNotify(resSave.response, resSave.responseMessage, resSave.helpMessage);
            //        dialog.ShowDialog(this);
            //        return;
            //    }
            //}
            //ucTxtCouponNo.Text = "";
            //ucTxtCouponQnt.Text = "";
            //ucTxtCouponNo.Focus();

            //SummaryCashIn();
            //RefreshGrid();

            //loadTempDLYForPayment();
        }

        public void loadDiscount()
        {
            double sumDis = 0;
            DataTable dataDissummary = saleProcess.selectDISCSUMMARY(ProgramConfig.saleRefNo, new List<string>() { "P", "L", "3", "4" });
            StoreResult res = saleProcess.DiscplayDiscountReceipt();
            if (res.response.next && res.otherData != null && res.otherData.Rows.Count > 0)
            {
                sumDis = res.otherData.AsEnumerable().Sum(s => Convert.ToDouble(s["DiscountAmt"]));
            }

            lbTxtDiscountOtherPayment.Text = sumDis.ToString(ProgramConfig.amountFormatString);
            if (monCust != null)
            {
                monCust.lbTxtDiscount.Text = (Convert.ToDouble(ProgramConfig.disValue) + sumDis).ToString(displayAmt);
                monCust.lbTxtTotalCash.Text = lbTxtTotalCash.Text;
                moni2.lbTxtTotalCash.Text = lbTxtBalanceDiff.Text;
                moni2.lbTxtReceive.Text = lbTxtReceiveCash.Text;
            }

            Font oldFont = lbDiscountOtherPayment.Font;
            lbDiscountOtherPayment.Click -= DiscountOtherPaymentClick;
            if (sumDis > 0)
            {
                lbDiscountOtherPayment.Click += DiscountOtherPaymentClick;
                lbDiscountOtherPayment.ForeColor = Color.ForestGreen;
                lbDiscountOtherPayment.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size, FontStyle.Underline, oldFont.Unit, oldFont.GdiCharSet);
            }
            else
            {
                lbDiscountOtherPayment.ForeColor = Color.Gray;
                lbDiscountOtherPayment.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size, FontStyle.Regular, oldFont.Unit, oldFont.GdiCharSet);
            }

            lbTxtTotalCash.Text = (double.Parse(lbTxtSubtotal.Text) - double.Parse(lbTxtDiscountOtherPayment.Text)).ToString(displayAmt);
        }

        public void DiscountOtherPaymentClick(object sender, EventArgs e)
        {
            pn_DisplayDiscount.Visible = !pn_DisplayDiscount.Visible;

            if (pn_DisplayDiscount.Visible)
            {
                pn_CannotUseTicketCoupon.Visible = false;
                StoreResult res = saleProcess.DiscplayDiscountReceipt();
                if (res.response.next)
                {
                    pn_ItemDisplayDiscount.Controls.Clear();
                    foreach (DataRow dr in res.otherData.Rows)
                    {
                        UCItemDisplayDiscount itm = new UCItemDisplayDiscount();
                        itm.lbItemName.Text = dr["DiscountName"].ToString();
                        itm.lbDisAmt.Text = Convert.ToDouble(dr["DiscountAmt"].ToString()).ToString(displayAmt);
                        pn_ItemDisplayDiscount.Controls.Add(itm);
                    }
                    Utility.SetGridColorAlternate<UCItemDisplayDiscount>(pn_ItemDisplayDiscount.Controls.Cast<UCItemDisplayDiscount>().ToList(), Color.FromArgb(255, 188, 128), Color.FromArgb(255, 220, 188));
                }
            }
        }

        public void loadTempDLYForPayment()
        {
            loadDiscount();

            //Default
            amtPrice = 0;
            lbTxtTotalCash.Text = (double.Parse(lbTxtSubtotal.Text) - double.Parse(lbTxtDiscountOtherPayment.Text)).ToString(displayAmt);
            lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
            lbTxtBalance.Text = amtPrice.ToString(displayAmt);
            pn_list_payment.Controls.Clear();

            int numC = GetMaxNoPaymentList();
            DataTable dtRes = saleProcess.selectSCANTC();
            if (dtRes.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRes.Rows)
                {
                    UCListPayment ucPayment = new UCListPayment();
                    ucPayment.lbNoText = numC.ToString();
                    ucPayment.UCLP_lbPaymentType.Text = "CPN1 " + dr["RUNNING_NO"].ToString();
                    ucPayment.UCLP_lbAmount.Text = (Convert.ToDouble(dr["CPNVALUE"]) * Convert.ToDouble(dr["CPNQNT"])).ToString(ProgramConfig.amountFormatString);
                    ucPayment.btnDelete.Visible = false;
                    pn_list_payment.Controls.Add(ucPayment);
                    numC++;
                }
            }

            LoadFromTable loadFromTable;
            DataTable loadTemp;



            if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
            {
                loadTemp = saleProcess.loadTempCREDPAY_TRANS_PAY(ProgramConfig.creditSaleNo);
            }
            else if (ProgramConfig.pageBackFromPayment != PageBackFormPayment.ReceivePOD)
            {
                loadTemp = saleProcess.loadTempDlyForPayment(ProgramConfig.saleRefNo);
            }         
            else
            {
                loadTemp = saleProcess.loadTEMP_PODTRANS_PAY(ProgramConfig.podRefNo);
            }

            if (loadTemp.Rows != null)
            {
                for (int i = 0; i < loadTemp.Rows.Count; i++)
                {
                    int rec;
                    string pcd = "";
                    string amt = "";
                    string paymentCode = "";
                    string paymentNum = "";

                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                    {
                        loadFromTable = LoadFromTable.TEMPCREDPAY_TRANS_PAY;
                        rec = int.Parse(loadTemp.Rows[i]["SEQ"].ToString());
                        string pay = loadTemp.Rows[i]["PaymentMainCode"].ToString();
                        string payNum = loadTemp.Rows[i]["PAYMENT_NUMBER"].ToString();
                        pcd = pay + payNum;
                        amt = loadTemp.Rows[i]["PAYMENT_AMOUNT"].ToString();

                        if (pay.Length >= 8)
                        {
                            pay = pay.Substring(0, 8);
                        }

                        if (payNum != "" && !pay.StartsWith("QR"))
                        {
                            DataTable dt = saleProcess.functionGetPaymentCode(payNum);
                            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "XXXXXXXXXXXXXXXXXXXX")
                            {
                                pcd = dt.Rows[0][0].ToString();
                            }
                            else
                            {
                                pcd = pay + payNum;
                            }
                        }
                        else
                        {
                            pcd = pay + payNum;
                        }


                        paymentCode = pay;
                        paymentNum = payNum;               
                    }
                    else if (ProgramConfig.pageBackFromPayment != PageBackFormPayment.ReceivePOD)
                    {
                        loadFromTable = LoadFromTable.TEMPDLYPTRANS;
                        
                        string refno = loadTemp.Rows[i]["REF"].ToString();
                        rec = int.Parse(loadTemp.Rows[i]["REC"].ToString());
                        pcd = loadTemp.Rows[i]["PCD"].ToString();
                        string qnt = loadTemp.Rows[i]["QNT"].ToString();
                        double dbAmt = double.Parse(loadTemp.Rows[i]["AMT"].ToString());
                        amt = dbAmt.ToString(displayAmt);
                        string fds = loadTemp.Rows[i]["FDS"].ToString();
                        string discamt = loadTemp.Rows[i]["DISCAMT"].ToString();
                        string upc = loadTemp.Rows[i]["UPC"].ToString();

                        paymentCode = pcd.Substring(0, 4);
                        paymentNum = pcd.Substring(4, pcd.Length - 4).Trim();
                    }
                    else
                    {
                        loadFromTable = LoadFromTable.TEMP_PODTRANS_PAY;
                        rec = int.Parse(loadTemp.Rows[i]["REC"].ToString());
                        string pay = loadTemp.Rows[i]["PAY"].ToString().Trim();
                        string payNum = loadTemp.Rows[i]["PAY_NUMBER"].ToString();
                        
                        if (pay.Length >= 8)
                        {
                            pay = pay.Substring(0, 8);
                        }

                        if (payNum != "" && !pay.StartsWith("QR"))
                        {
                            DataTable dt = saleProcess.functionGetPaymentCode(payNum);
                            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "XXXXXXXXXXXXXXXXXXXX")
                            {
                                pcd = dt.Rows[0][0].ToString();
                            }
                            else
                            {
                                pcd = pay + payNum;
                            }
                        }
                        else
                        {
                            pcd = pay + payNum;
                        }

                        double dbAmt = double.Parse(loadTemp.Rows[i]["AMT"].ToString());
                        amt = dbAmt.ToString(displayAmt);

                        paymentCode = pay;
                        paymentNum = payNum;
                    }

                    int num;
                    if (pn_list_payment.Controls.Count > 0)
                    {
                        num = pn_list_payment.Controls.Cast<UCListPayment>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
                    }
                    else
                    {
                        num = 1;
                    }

                    UCListPayment ucList = new UCListPayment();
                    ucList.UCLP_lbNo.Text = num.ToString();
                    string pmCode = pcd.Substring(0, 4);

                    var lstPMPolicy = ProgramConfig.paymentPolicy.GetPaymentPolicyByFunction(FunctionID.PaymentDelete);

                    if (lstPMPolicy.Any(a => a.paymentCode == pmCode))
                    {
                        ucList.btnDelete.Visible = false;
                    }
                    else
                    {
                        ucList.btnDelete.Visible = true;
                    }

                    //if (pmCode == "QRPP")
                    //{
                    //    pcd = pcd.Trim() + saleProcess.selectORG_TransFromQRPAYTRANS_Manual(rec.ToString());
                    //}

                    ucList.UCLP_lbRec.Text = rec.ToString();
                    ucList.DeleteClick += DeleteClick;
                    ucList.UCLP_lbPaymentType.Text = pcd;
                    ucList.UCLP_label1.Text = pcd;
                    ucList.UCLP_lbAmount.Text = amt;
                    ucList.PMCode = paymentCode;
                    ucList.PMNumber = paymentNum;
                    ucList.loadFromTable = loadFromTable;
                    pn_list_payment.Controls.Add(ucList);

                }
            }

            RefreshGrid();
            SummaryCashIn();
        }

        private void RefreshCurrency()
        {
            List<UCItemCurrency> lstCashIn = new List<UCItemCurrency>();
            lstCashIn = panel_exchange.Controls.Cast<UCItemCurrency>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panel_exchange.Controls.Clear();
            int num = lstCashIn.Count;

            foreach (UCItemCurrency item in lstCashIn)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(255, 220, 188);
                }
                else
                {
                    item.BackColor = Color.FromArgb(255, 188, 128);
                }
                item.lbNoText = num.ToString();
                panel_exchange.Controls.Add(item);
                num--;
            }
            ScrollToBottom(panel_exchange);

            //List<UCItemCurrency2> lstCashIn2 = new List<UCItemCurrency2>();
            //lstCashIn2 = moni2.panel_exchange.Controls.Cast<UCItemCurrency2>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            //moni2.panel_exchange.Controls.Clear();
            //int num2 = lstCashIn2.Count;

            //foreach (UCItemCurrency2 item2 in lstCashIn2)
            //{
            //    if (num2 % 2 != 0)
            //    {
            //        item2.BackColor = Color.FromArgb(255, 220, 188);
            //    }
            //    else
            //    {
            //        item2.BackColor = Color.FromArgb(255, 188, 128);
            //    }
            //    item2.lbNoText = num.ToString();
            //    moni2.panel_exchange.Controls.Add(item2);
            //    num2--;
            //}
            //ScrollToBottom(moni2.panel_exchange);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                string formatCash = "";
                string no;
                string code = "";
                string cpno = "";
                string cpValue;
                string cpQty;
                string cpProduct;
                double receiveCash = 0;
                string couponType = "";


                if (mode == "Select")
                {
                    if (pn_coupon_list.Controls.Count > 0)
                    {
                        foreach (UCCoupon uc in pn_coupon_list.Controls)
                        {
                            no = uc.lbNo.Text;
                            code = uc.paymentCode.Text;
                            cpno = uc.lbCouponNo.Text;
                            cpValue = uc.lbCouponValue.Text;
                            cpQty = uc.lbQty.Text;
                            cpProduct = uc.lbProductCode.Text;
                            couponType = uc.UCC_lbcouponType.Text;
                            code = code.Replace(" ", "");

                            receiveCash += double.Parse(cpValue);
                            formatCash = receiveCash.ToString(displayAmt);

                        }
                        StoreResult res = saleProcess.savePaymentCouponSelect();
                        if (res.response.next)
                        {
                            closeAddcoupon();
                        }
                        else
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog();
                            return;
                        }
                    }
                    else
                    {
                        closeAddcoupon();
                    }
                }
                else
                {
                    if (pn_coupon_list.Controls.Count > 0)
                    {
                        foreach (UCCoupon uc in pn_coupon_list.Controls)
                        {
                            no = uc.lbNo.Text;
                            code = uc.paymentCode.Text;
                            cpno = uc.lbCouponNo.Text;
                            cpValue = uc.lbCouponValue.Text;
                            cpQty = uc.lbQty.Text;
                            cpProduct = uc.lbProductCode.Text;
                            couponType = uc.UCC_lbcouponType.Text;
                            code = code.Replace(" ", "");

                            receiveCash += double.Parse(cpValue);
                            formatCash = receiveCash.ToString(displayAmt);

                        }

                        StoreResult res = saleProcess.savePaymentCoupon();
                        if (res.response.next)
                        {
                            //int num = GetMaxNoPaymentList();
                            //foreach (UCCoupon uc in pn_coupon_list.Controls)
                            //{
                            //    if (uc.UCC_lbcouponType.Text == CouponType.TicketCoupon)
                            //    {
                            //        string amt = (Convert.ToDouble(uc.lbQtyText) * Convert.ToDouble(uc.lbCouponValue.Text)).ToString(ProgramConfig.amountFormatString);
                            //        saleProcess.savePaymentTicketCoupon(code, amt);

                            //        //UCListPayment ucPayment = new UCListPayment();
                            //        //ucPayment.lbNoText = num.ToString();
                            //        //ucPayment.lbPaymentType.Text = uc.lbCouponNo.Text;
                            //        //ucPayment.lbAmount.Text = 
                            //        //lst.Add(ucPayment);
                            //        //num++;
                            //    }
                            //}
                            closeAddcoupon();
                        }
                        else
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog();
                            return;
                        }
                    }
                    else
                    {
                        closeAddcoupon();
                    }
                }

                //SummaryCoupon();
                loadTempDLYForPayment();
                btnShowListCoupon.Enabled = true;
                dt2Data.Rows.Clear();

                //if (Convert.ToDouble(lbTxtTotalCash.Text) - Convert.ToDouble(lbTxtReceiveCash.Text) <= 0)
                if (Convert.ToDouble(lbTxtBalanceDiff.Text) - Convert.ToDouble(lbTxtReceiveCash.Text) <= 0)
                {
                    double cashbalance = 0;
                    lbTxtBalance.Text = cashbalance.ToString(displayAmt);
                    ShowConfirmPayment();
                }
                else
                {             
                    mode = "";
                    ucTxtCouponNo.Focus();
                    btnCash_Click(this, null);
                }

                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void SummaryCoupon()
        {
            loadTempDLYForPayment();

            //if (double.Parse(lbTxtReceiveCash.Text) != 0)
            //{
            //    amtPrice = double.Parse(formatCash);
            //    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
            //    lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
            //}
            //else
            //{
            //    amtPrice = double.Parse(formatCash);
            //    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
            //    lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - receiveCash).ToString(displayAmt);
            //}
        }

        private void picCancel_Click(object sender, EventArgs e)
        {
            pn_coupon_list.Controls.Clear();
            pn_page_coupon.SendToBack();
        }

        private void ucTxtCardNo_IconClick(object sender, EventArgs e)
        {
            lbCardType_remove.Text = "Credit";
            ucTxtAmount.Text = "";
            ucTxtApprove.Text = "";
            ucTxtCardNo.Focus();
        }

        private void lbCurrency_Click(object sender, EventArgs e)
        {
            var pn_Currency = this.FindForm().Controls.Find("pn_Currency", true).FirstOrDefault() as Panel;
            pn_Currency.Leave += LeavePanel;
            if (pn_Currency.Visible)
            {
                pn_Currency.Visible = !pn_Currency.Visible;
            }
            else
            {
                int i = 1;
                List<Dropdown> lstStr = new List<Dropdown>();
                Dropdown drItem = new Dropdown();
                List<Currency> lstCur = new List<Currency>();
                lstCur = _currency.list();
                lstCur.Reverse();

                foreach (Currency currency in lstCur)
                {
                    drItem.DisplayText = currency.code;
                    drItem.ValueText = i.ToString();
                    lstStr.Add(drItem);
                    i++;
                }

                pn_Currency.Controls.Clear();
                pn_Currency.Visible = true;

                int ucH = 0;
                int cnt = 1;
                int temp = 0;
                int widthDD = pn_Currency.Width;
                int widthLine = 0;
                int widthLabel = 0;

                Dropdown maxStr = lstStr.Select(s => s).OrderByDescending(o => o.DisplayText.Length).FirstOrDefault();

                UCItemDropDownList _ucdd = new UCItemDropDownList();
                Font font = _ucdd.label1.Font;
                widthLine = _ucdd.lineShape1.Width - _ucdd.lineShape1.Location.X;
                widthLabel = _ucdd.Width;
                temp = TextRenderer.MeasureText(maxStr.DisplayText, font).Width;

                if (temp + 13 >= widthDD)
                {
                    widthDD = temp + 13; // 13 คือส่วนต่างของ Size form width กับ Size label width >>>>> Form UCItemDropDownList
                    widthLabel = temp;
                }

                if (temp >= widthLine)
                {
                    widthLine = temp + _ucdd.lineShape1.Location.X;
                }
                else
                {
                    widthLine = widthDD - 13;
                }

                foreach (Dropdown str in lstStr)
                {
                    ucH += 35; // ความสูงของ item >>>>> UCItemDropDownList

                    UCItemDropDownList ucdd = new UCItemDropDownList();

                    ucdd.UCItemDropDownListClick += (s2, e2) => UCItemDropDownListClick(s2, e2, pn_Currency);

                    ucdd.label1.Text = str.DisplayText;
                    ucdd.label2.Text = str.ValueText;

                    ucdd.label1.Width = widthLabel;
                    ucdd.lineShape1.Width = widthLine;

                    if (cnt == 1)
                    {
                        ucdd.lineShape1.Visible = false;
                    }

                    cnt++;
                    pn_Currency.Controls.Add(ucdd);
                }

                if (ucH >= 198) // check ให้ item dropdown มีได้แค่ 6 ถ้ามากกว่านั้น จะมี scroll bar 
                {
                    ucH = 198; // 198 คือ ส่วนสูงของ panel เมื่อมี item 6 ชิ้น
                    widthDD = widthDD + (widthDD == pn_Currency.Width ? 0 : 10); // + scorll bar ที่เพิ่มเข้ามา
                }

                if (widthDD > pn_Currency.Width && !DropdownExpandRightSide)
                {
                    // set location ไปทางซ้าย
                    pn_Currency.Location = new Point(pn_Currency.Location.X - (widthDD - pn_Currency.Width), pn_Currency.Location.Y);
                }

                pn_Currency.Height = ucH + 3;
                pn_Currency.Width = widthDD;
                pn_Currency.BringToFront();
                pn_Currency.Focus();
                this.BackgroundImage = Properties.Resources.txtboxWIC_enable;
            }

        }

        private void UCItemDropDownListClick(object sender, EventArgs e, Panel pn_DropDown)
        {
            try
            {
                var ucIDDL = (UCItemDropDownList)sender;
                this.lbCurrency.Text = ucIDDL.label1.Text;
                this.lbCurrency.Tag = ucIDDL.label2.Text;
                pn_DropDown.Visible = false;

                double exChange = ProgramConfig.payment.getExchangeRate(lbCurrency.Text);
                string pmCode = ProgramConfig.payment.getPaymentCode(lbCurrency.Text);
                lbAmountCash.Text = pmCode;
                if (exChange != 0)
                {
                    //ucTxtAmountCash.Text = (double.Parse(lbTxtBalance.Text) / exChange).ToString(displayAmt);
                    frmLoading.showLoading();
                    StoreResult res = saleProcess.getAmountExchangeRate(lbTxtBalance.Text, "0", lbCurrency.Text, lbAmountCash.Text);
                    frmLoading.closeLoading();
                    AppLog.writeLog("after saleProcess.getAmountExchangeRate UCItemDropDownListClick");
                    if (res.otherData != null && res.otherData.Rows.Count > 0)
                    {
                        AppLog.writeLog("after saleProcess.getAmountExchangeRate Have Data");
                        if (ucTxtAmountCash.InvokeRequired)
                        {
                            ucTxtAmountCash.BeginInvoke((MethodInvoker)delegate
                            {
                                ucTxtAmountCash.Text = res.otherData.AsEnumerable().Where(w => w["PaymentSubCode"].ToString() == lbCurrency.Text).Select(s => s["Total"].ToString()).FirstOrDefault();
                            });
                        }
                        else
                        {
                            ucTxtAmountCash.Text = res.otherData.AsEnumerable().Where(w => w["PaymentSubCode"].ToString() == lbCurrency.Text).Select(s => s["Total"].ToString()).FirstOrDefault();
                        }
                    }

                    if (ucTxtAmountCash.InvokeRequired)
                    {
                        ucTxtAmountCash.BeginInvoke((MethodInvoker)delegate
                        {
                            ucTxtAmountCash.Focus();
                        });
                    }
                    else
                    {
                        ucTxtAmountCash.Focus();
                    }
                }
                else
                {
                    if (ucTxtAmountCash.InvokeRequired)
                    {
                        ucTxtAmountCash.BeginInvoke((MethodInvoker)delegate
                        {
                            ucTxtAmountCash.Text = (double.Parse(lbTxtBalance.Text) * 1).ToString(displayAmt);
                            ucTxtAmountCash.Focus();
                        });
                    }
                    else
                    {
                        ucTxtAmountCash.Text = (double.Parse(lbTxtBalance.Text) * 1).ToString(displayAmt);
                        ucTxtAmountCash.Focus();
                    }
                }
                AppLog.writeLog("after saleProcess.getAmountExchangeRate UCItemDropDownListClick end");
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
        }

        private void LeavePanel(object sender, EventArgs e)
        {
            ((Panel)sender).Visible = false;
            this.BackgroundImage = Properties.Resources.txtboxWIC_disable;
        }

        private void btnShowListCoupon_Click(object sender, EventArgs e)
        {

        }

        private void frmPayment_Shown(object sender, EventArgs e)
        {
           // btnCash_Click(this, null);
        }

        private void ucTxtVoucherNo_IconClick(object sender, EventArgs e)
        {
            ucTxtVoucherAmt.Text = "";
            ucTxtVoucherNo.Focus();
        }

        private void pn_drop_menu_Leave(object sender, EventArgs e)
        {
            pn_drop_menu.Visible = false;
        }

        private void ucTxtCouponQnt_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                keyCoupon();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucTxtCouponQnt_EnterFromButton(object sender, EventArgs e)
        {
            try
            {
                keyCoupon();
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            try
            {
                //loadTempDLYForPayment();
                //loadCoupon();
                if (currentTemplate != null && currentPanel != null)
                {
                    GenerateTextBoxLabel(currentTemplate, currentPanel, currentTemplate);
                }
                ChangeLanguageButtonMenu();
                if (ProgramConfig.memberName != "")
                {
                    ucHeader1.nameVisible = true;
                    Label newFont = new Label();
                    newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                    int checkWidth = TextRenderer.MeasureText(ProgramConfig.memberName, newFont.Font).Width;
                    ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);
                }
                else
                {
                    ucHeader1.nameVisible = false;
                }
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private int GetMaxNoPaymentList()
        {
            int num;
            if (pn_list_payment.Controls.Count > 0)
            {
                num = pn_list_payment.Controls.Cast<UCListPayment>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
            }
            else
            {
                num = 1;
            }

            return num;
        }

        private void btnShowListCoupon_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                mode = "Select";

                pn_coupon_list.Controls.Clear();
                StoreResult read = saleProcess.displayCoupon();
                if (read.response == ResponseCode.Success)
                {

                    if (read.otherData.Rows[0]["ROW"].ToString() != "0")
                    {
                        pn_page_coupon.Visible = true;
                        pn_page_coupon.BringToFront();

                        string formatCash = "";
                        string couQnt = "";
                        string barcode = "";
                        DataTable dt = read.otherData;
                        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                        {
                            int num;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                couNo = dt.Rows[i]["CouponNo"].ToString();
                                couAmt = dt.Rows[i]["CouponValue"].ToString();
                                couQnt = dt.Rows[i]["CouponQnt"].ToString();
                                barcode = dt.Rows[i]["Barcode"].ToString();
                                vRow = dt.Rows[i]["Row"].ToString();

                                payCode = "";

                                dt2Data.Rows.Add(payCode, couNo, couAmt, couQnt, barcode, vRow);

                                double receiveCash = double.Parse(couAmt);
                                formatCash = receiveCash.ToString(displayAmt);

                                if (pn_coupon_list.Controls.Count > 0)
                                {
                                    num = pn_coupon_list.Controls.Cast<UCCoupon>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
                                }
                                else
                                {
                                    num = 1;
                                    Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2, pictureBox5);
                                }

                                UCCoupon ucList = new UCCoupon();
                                ucList.lbNo.Text = num.ToString();
                                //ucList.UCGridViewItemSellClick += UCGridViewItemSellClick;
                                ucList.DeleteClick += DeleteListCouponClick;
                                ucList.paymentCode.Text = couNo;
                                ucList.lbCouponNo.Text = couNo;
                                ucList.lbCouponValue.Text = formatCash;
                                ucList.lbQty.Text = couQnt;
                                ucList.lbProductCode.Text = barcode;
                                ucList.lbRow.Text = vRow;
                                pn_coupon_list.Controls.Add(ucList);

                            }
                        }

                        RefreshCouponList();
                    }
                }
                else
                {
                    ClearPaymentData();
                    notify = new frmNotify(read);
                    notify.ShowDialog(this);
                    return;
                }
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void lbTxtBalance_TextChanged(object sender, EventArgs e)
        {
            lbTxtCreditBalance_ManualChoise.Text = lbTxtBalance.Text;
            //StoreResult res = saleProcess.getTotalAmtDiff(ProgramConfig.saleRefNo, lbTxtBalance.Text);
            ////ProcessResult res = saleProcess.beforePaymentProcess(lbTxtBalance.Text);
            //if (res.response.next)
            //{
            //    lbTxtBalance.TextChanged -= lbTxtBalance_TextChanged; 
              
            //    double saleAmt = 0f, saleAmt_round = 0f;
            //    double.TryParse(res.otherData.Rows[0]["SaleAMT"].ToString(), out saleAmt);
            //    double.TryParse(res.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round);

            //    //double amountCash = double.Parse(res.otherData[].ToString());
            //    lbTxtBalance.Text = saleAmt_round.ToString(displayAmt);

            //    lbTxtBalance.TextChanged += lbTxtBalance_TextChanged;
            //}
        }

        private void lbTxtTotalCash_TextChanged(object sender, EventArgs e)
        { 
            //ProcessResult res = saleProcess.beforePaymentProcess(lbTxtTotalCash.Text);
            StoreResult res = saleProcess.getTotalAmtDiff(ProgramConfig.saleRefNo, lbTxtTotalCash.Text, "1", "");
            if (res != null && res.response.next && res.otherData != null)
            {
                //lbTxtBalance.TextChanged -= lbTxtBalance_TextChanged;  

                double saleAmt = 0f, saleAmt_round = 0f;
                double.TryParse(res.otherData.Rows[0]["SaleAMT"].ToString(), out saleAmt);
                double.TryParse(res.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round);
                //double amountCash = double.Parse(res.data.ToString());
                lbTxtBalanceDiff.Text = saleAmt_round.ToString(displayAmt);

                //amountCash - Convert.ToDouble(lbTxtReceiveCash.Text);

                //lbTxtBalance.TextChanged += lbTxtBalance_TextChanged;
            }
            else
            {
                lbTxtBalanceDiff.Text = lbTxtTotalCash.Text;
            }
        }

        private void lbTxtBalanceDiff_TextChanged(object sender, EventArgs e)
        {
            double amtBeforeChkDiff = Convert.ToDouble(lbTxtBalanceDiff.Text) - Convert.ToDouble(lbTxtReceiveCash.Text);
            lbTxtBalance.Text = amtBeforeChkDiff.ToString(displayAmt);
            //string amtStrBeforeChkDiff = amtBeforeChkDiff.ToString();
            //if (amtBeforeChkDiff >= 0)
            //{
            //    //ProcessResult res = saleProcess.beforePaymentProcess(amtStrBeforeChkDiff);
            //    StoreResult res = saleProcess.getTotalAmtDiff(ProgramConfig.saleRefNo, amtStrBeforeChkDiff);
            //    if (res != null && res.response.next && res.otherData != null)
            //    {
            //        //lbTxtBalance.TextChanged -= lbTxtBalance_TextChanged;    

            //        double saleAmt = 0f, saleAmt_round = 0f;
            //        double.TryParse(res.otherData.Rows[0]["SaleAMT"].ToString(), out saleAmt);
            //        double.TryParse(res.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round);

            //        //double amountCash = double.Parse(res.data.ToString());
            //        lbTxtBalance.Text = saleAmt_round.ToString(displayAmt);
            //        //lbTxtBalance.TextChanged += lbTxtBalance_TextChanged;
            //    }
            //    else
            //    {
            //        lbTxtBalance.Text = amtBeforeChkDiff.ToString(displayAmt);
            //    }
            //}
        }

        private void ucHeader1_Title2Click(object sender, EventArgs e)
        {
            BackProcess();
        }

        private void lbTxtReceiveCash_TextChanged(object sender, EventArgs e)
        {
            moni2.lbTxtReceive.Text = lbTxtReceiveCash.Text;
        }

        private void ucTxtApprove_VisibleChanged(object sender, EventArgs e)
        {
            lbApprove_remove.Visible = ucTxtApprove.Visible;
        }

        private void ucTxtApprove_Enter(object sender, EventArgs e)
        {
            int keyboradtype = 2;
            switch (keyboradtype)
            {
                case 1:
                    this.ucKeyboard1.Visible = false;
                    break;
                case 2:
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.BringToFront();
                    this.ucKeyboard1.currentInput = ucTxtApprove;
                    //this.ucKeyboard1.updateLanguage();
                    pn_payment_credit.Location = LocationKBCredit;
                    break;
                default:
                    this.ucKeyboard1.Visible = false;
                    break;
            }
        }

        private void ucKeyboard1_HideKeyboardClick(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
        }

        private void ucTxtApprove_TextBoxLeave(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
            pn_payment_credit.Location = LocationKBCredit_default;
        }

        private void lbCardType_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbCardType_remove);
        }

        private void lbAmountCash_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbAmountCash);
        }

        private void lbCurrency_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbCurrency);
        }

        private void btnPayment_Other_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID("6");

            frmOtherPayment frmOtPm = new frmOtherPayment(_paymentMenuIcon.ToList(), btn.Text, 6, true);
            frmOtPm.Show(this);
            //if (dialogFromOther == System.Windows.Forms.DialogResult.Yes)
            //{
            //    //SetVisibleByTemplate(_OPtemplate);
            //    //btnPayment_Other.ForeColor = System.Drawing.Color.White;
            //    InitialImageButtonFromSEQ(btnPayment_Other, btnPayment_Other.Tag.ToString(), false);
            //    //btnPayment_Other.BackgroundImage = GetImageButtonGenFromTag(btn.Name, false);
            //}
            //else
            //{
            //    DisablePaymentGroup();
            //    ClearPaymentData();
            //}
        }

        private void SetVisibleByTemplate(string template)
        {
            switch (template)
            {
                case "1": pn_payment_temp1.Visible = true; break;
                case "2": pn_payment_temp1.Visible = true; break;
                case "3": pn_payment_temp1.Visible = true; break;
                case "4": pn_payment_temp1.Visible = true; break;
                case "5": pn_payment_temp1.Visible = true; break;
                case "6": pn_payment_temp1.Visible = true; break;
                default:
                    break;
            }
        }

        private void TextBoxCallModuleTemp1_EnterFromButton(object sender, EventArgs e)
        {
            PaymentTemplateConfig[] pmConfig = ProgramConfig.paymentTemplate.getModuleFromLabel("", "");
            UCTextBoxWithIcon uctxt = (UCTextBoxWithIcon)sender;

            if (uctxt.InpTxt.Trim() != "")
            {
                //edcStatus = "1";    

                string cash = "CASH";
                string cardNo = cash + uctxt.InpTxt;

                frmLoading.showLoading();
                StoreResult read = saleProcess.getPaymentCode(cardNo);
                frmLoading.closeLoading();

                if (read.response.next)
                {
                    if (read.response == ResponseCode.Information)
                    {
                        //Fix Case Co-Brand (Done)
                        ClearPaymentData();
                        notify = new frmNotify(read);
                        notify.ShowDialog(this);
                        uctxt.Focus();
                        return;
                    }

                    uctxt.EnabledUC = false;
                    uctxt.Visible = false;
                    DataTable dt = read.otherData;
                    paymentCode = dt.Rows[0]["PaymentCodeDisplay"].ToString();
                    paymentCode = paymentCode.Replace(" ", "");
                    paymentNumber = dt.Rows[0]["PaymentNumberDisplay"].ToString();
                    creditCard = dt.Rows[0]["CreditCard"].ToString();
                    creditCard = creditCard.Replace(" ", "");

                    //lbCardType.Text = paymentCode;
                    //ucTxtCardNo.Text = paymentNumber;
                    //lbDisplayCreditCard.Visible = true;
                    //lbDisplayCreditCard.Text = creditCard.Substring(4, creditCard.Length - 4);

                    if (paymentCode.Length > 4)
                    {
                        paymentCode = paymentCode.Substring(0, 4);
                    }

                    //ucTxtCreditAmt.Visible = true;
                    //ucTxtCreditAmt.Focus();
                    frmLoading.closeLoading();
                }
                else
                {
                    ClearPaymentData();
                    notify = new frmNotify(ResponseCode.Error, read.responseMessage, read.helpMessage);
                    notify.ShowDialog(this);
                    frmLoading.closeLoading();
                    uctxt.Focus();
                }
            }
            else
            {
                ClearPaymentData();
                frmLoading.closeLoading();
                //notify = new frmNotify(ResponseCode.Information, "กรุณาใส่หมายเลขบัตรเครดิต", "");
                notify = new frmNotify(ResponseCode.Information
                                        , ProgramConfig.message.get("frmPayment", "PlsEnterCreditCardCode").message
                                        , ProgramConfig.message.get("frmPayment", "PlsEnterCreditCardCode").message);
                notify.ShowDialog(this);
                //ucTxtCardNo.Focus();
            }
        }

        private bool RunModule(int seq, RunModuleParameter pModule)
        {
            try
            {
                ReturnModuleParameter ret = new ReturnModuleParameter();
                int tempSeq = seq;
                string pmCode = "";

                if (dtPaymentStep.Rows.Count > 0)
                {
                    foreach (DataRow drdt in dtPaymentStep.Select(" StepID = '4' and SeqRef = " + seq + ""))
                    {
                        tempSeq = tempSeq + 1;
                        DataRow dr = dtPaymentStep.Select(" Seq = " + tempSeq + "").FirstOrDefault();

                        if (dr != null)
                        {
                            if ((PaymentStepDetail_ModuleID)dr["ModuleID"] == PaymentStepDetail_ModuleID.PaymentDiscount)
                            {
                                if (!PaymentDiscount(pModule.pmCode, pModule.cardNo))
                                {
                                    //seqPaymentStep = seqPaymentStep - 1;
                                    return false;
                                }
                            }
                            else if ((PaymentStepDetail_ModuleID)dr["ModuleID"] == PaymentStepDetail_ModuleID.Valid_GFSL)
                            {
                                if (!keyVoucher(out pmCode))
                                {
                                    //seqPaymentStep = seqPaymentStep - 1;
                                    return false;
                                }
                                else
                                {
                                    pModule.pmCode = pmCode;
                                    pModule.cardNo = ucTxtVoucherNo.Text;
                                }
                            }
                            else if ((PaymentStepDetail_ModuleID)dr["ModuleID"] == PaymentStepDetail_ModuleID.pos_CheckCustomer)
                            {
                                //if (seq.ToString() == dr["SeqRef"].ToString())
                                //{
                                    if (!CheckCustomer(pModule.inpRef1))
                                    {
                                        return false;
                                    }
                                //}
                            }
                            //else if ((PaymentStepDetail_ModuleID)dr["ModuleID"] == PaymentStepDetail_ModuleID.pos_CheckValuePayment)
                            //{
                            //    //if (seq.ToString() == dr["SeqRef"].ToString())
                            //    //{
                            //        if (!CheckValuePayment(dr["DataType"].ToString(), pModule.inpRef1, pModule.pmCode))
                            //        {
                            //            return false;
                            //        }
                            //    //}
                            //}
                            else if ((PaymentStepDetail_ModuleID)dr["ModuleID"] == PaymentStepDetail_ModuleID.pos_CheckValuePayment)
                            {
                                //if (seq.ToString() == dr["SeqRef"].ToString())
                                //{
                                    var res = CheckValuePayment(dr["DataType"].ToString(), pModule.inpRef1, pModule.pmCode, out ret);
                                    if (res.response.next)
	                                {
                                        var ary = dtPaymentStep.Select(" (StepID = '3' or StepID = '6') and ReferReturnModuleID = '" + dr["ModuleID"].ToString() + "'");
                                        if (ary.Length > 0)
                                        {
                                            string seqTxtBox = ary[0]["Seq"].ToString();
                                            string referRetCol = ary[0]["ReferReturnColModuleName"].ToString();
                                            for (int i = 1; i <= 10; i++)
                                            {
                                                UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                                                if (ucTxtPn0 != null)
                                                {
                                                    if (ucTxtPn0.SeqTextBox == seqTxtBox)
                                                    {
                                                        ucTxtPn0.Text = res.otherData.Rows[0][referRetCol].ToString();
                                                        break;
                                                    }
                                                }
                                            }

                                            for (int i = 1; i <= 2; i++)
                                            {
                                                UCTextBoxWithIcon ucTxtPn4 = pn_payment_temp4.Controls.Find("ucTxtPn4_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                                                if (ucTxtPn4 != null)
                                                {
                                                    if (ucTxtPn4.SeqTextBox == seqTxtBox)
                                                    {
                                                        double amt = 0;

                                                        if (double.TryParse(res.otherData.Rows[0][referRetCol].ToString(), out amt))
                                                        {
                                                            ucTxtPn4.Text = amt.ToString(ProgramConfig.amountFormatString);
                                                        }
                                                        else
                                                        {
                                                            ucTxtPn4.Text = res.otherData.Rows[0][referRetCol].ToString();
                                                        }
                                                        
                                                        ucTxtPn4.FocusTxt();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                //}
                            }
                            else if ((PaymentStepDetail_ModuleID)dr["ModuleID"] == PaymentStepDetail_ModuleID.pos_CheckDEPO)
                            {
                                var res = CheckDEPO(dr["DataType"].ToString(), pModule.inpRef1, pModule.pmCode, out ret);
                                if (res.response.next)
                                {
                                    var ary = dtPaymentStep.Select(" (StepID = '3' or StepID = '6') and ReferReturnModuleID = '" + dr["ModuleID"].ToString() + "'");
                                    if (ary.Length > 0)
                                    {
                                        string seqTxtBox = ary[0]["Seq"].ToString();
                                        string referRetCol = ary[0]["ReferReturnColModuleName"].ToString();
                                        for (int i = 1; i <= 10; i++)
                                        {
                                            UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                                            if (ucTxtPn0 != null)
                                            {
                                                if (ucTxtPn0.SeqTextBox == seqTxtBox)
                                                {
                                                    if (pModule.pmCode == "DEPO")
                                                    {
                                                        double pay = 0.0;
                                                        double.TryParse(res.otherData.Rows[0][referRetCol].ToString(), out pay);

                                                        double balance = 0.0;
                                                        double.TryParse(lbTxtBalance.Text, out balance);

                                                        if (pay > balance)
                                                        {
                                                            ucTxtPn0.Text = balance.ToString(displayAmt);
                                                            break;
                                                        }
                                                    }

                                                    depositRef = res.otherData.Rows[0]["DEPOSIT_REF"].ToString();
                                                    ucTxtPn0.Text = res.otherData.Rows[0][referRetCol].ToString();
                                                    break;
                                                }
                                            }
                                        }

                                        for (int i = 1; i <= 2; i++)
                                        {
                                            UCTextBoxWithIcon ucTxtPn4 = pn_payment_temp4.Controls.Find("ucTxtPn4_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                                            if (ucTxtPn4 != null)
                                            {
                                                if (ucTxtPn4.SeqTextBox == seqTxtBox)
                                                {
                                                    //double amt = 0;

                                                    //if (double.TryParse(res.otherData.Rows[0][referRetCol].ToString(), out amt))
                                                    //{
                                                    //    ucTxtPn4.Text = amt.ToString(ProgramConfig.amountFormatString);
                                                    //}
                                                    //else
                                                    //{
                                                    //    ucTxtPn4.Text = res.otherData.Rows[0][referRetCol].ToString();
                                                    //}

                                                    if (pModule.pmCode == "DEPO")
                                                    {
                                                        double pay = 0.0;
                                                        double.TryParse(res.otherData.Rows[0][referRetCol].ToString(), out pay);

                                                        double balance = 0.0;
                                                        double.TryParse(lbTxtBalance.Text, out balance);

                                                        if (pay > balance)
                                                        {
                                                            ucTxtPn4.Text = balance.ToString(displayAmt);
                                                            break;
                                                        }
                                                    }

                                                    ucTxtPn4.Text = res.otherData.Rows[0][referRetCol].ToString();
                                                    ucTxtPn4.FocusTxt();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }

                            //var ary = dtPaymentStep.Select(" (StepID = '3' or StepID = '6') and ReferReturnModuleID = " + (PaymentStepDetail_ModuleID)dr["ModuleID"]  + "");
                            //if (ary.Length > 0)
                            //{
                            //    string seqTxtBox = ary[0]["Seq"].ToString();
                            //    for (int i = 1; i <= 10; i++)
                            //    {
                            //        UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;
                            //        if (ucTxtPn0.SeqTextBox == seqTxtBox)
                            //        {
                            //            //ucTxtPn0.Text = 
                            //        }
                                    
                            //    }
                            //}
                        }
                        else
                        {
                            //seqPaymentStep = seqPaymentStep - 1;
                            return false;
                        }




                    }
                    return true;
                }
                return false;
                //seqPaymentStep = tempSeq;
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                return ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
                return false;
            }
        }
        
        #region Module 
		 
        private bool PaymentDiscount(string pmCode, string cardNo)
        {
            frmLoading.showLoading();
            DeleteTempPayment("PAYD");
            StoreResult res = saleProcess.PaymentDiscount(pmCode, cardNo);
            frmLoading.closeLoading();
            if (res.response.next)
            {
                loadDiscount();
                return true;
            }
            else
            {
                notify = new frmNotify(res);
                notify.ShowDialog();
                return false;
            }           
        }

        private bool CheckCustomer(string cardNo)
        {
            frmLoading.showLoading();
            StoreResult res = saleProcess.CheckCustomerModule(cardNo);
            frmLoading.closeLoading();
            if (res.response.next)
            {
                return true;
            }
            else
            {
                Utility.AlertMessage(res);
                return false;
            }  
        }

        private StoreResult CheckValuePayment(string validateType, string dataEntry, string pmCode, out ReturnModuleParameter ret)
        {
            ret = new ReturnModuleParameter();
            frmLoading.showLoading();
            StoreResult res = saleProcess.CheckValuePayment(validateType, dataEntry, pmCode);
            frmLoading.closeLoading();
            if (res.response.next)
            {
                return res;
            }
            else
            {
                Utility.AlertMessage(res);
                return res;
            }
        }

        private StoreResult CheckDEPO(string validateType, string dataEntry, string pmCode, out ReturnModuleParameter ret)
        {
            ret = new ReturnModuleParameter();
            frmLoading.showLoading();
            StoreResult res = saleProcess.CheckDEPO(validateType, dataEntry, pmCode);
            frmLoading.closeLoading();
            if (res.response.next)
            {
                return res;
            }
            else
            {
                Utility.AlertMessage(res);
                return res;
            }
        }

	    #endregion

        private void btn_edit_credit_Click(object sender, EventArgs e)
        {
            btnEditClick();
        }

        private void btnEditClick()
        {
            bool visible = pn_payment_credit.Visible;
            ClearPaymentCredit();
            pn_payment_credit.Visible = !visible;
            if (pn_payment_credit.Visible)
            {
                Utility.SetBackGroundBrightness(panelMainPayment, pictureBox2, pictureBox5);
                pn_payment_credit.Visible = true;
                pn_payment_credit.BringToFront();
                pn_ManualChoise.Visible = false;

                //SetPage();
                lb_PageNo.Text = "1";
                lastMenuID = "";
                SetButtonCreditCard("3", "1");
                lbCreditManual = "";
            }
            else
            {
                ucKeypad.ucTBWI = null;
            }
        }

        private void SetPage(string menuID)
        {
            List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID(menuID); //dtData.Select(" ReferMenuID = '" + menuID + "' ");
            var lst = _paymentMenuIcon.data().AsEnumerable().Where(itm => itm.ReferMenuID.ToString() == menuID).OrderByDescending(o => o.PageID).Select(s => s.PageID).ToList();

            if (lstPmMenuIcon.Count > 0)
            {
                lb_PageTotal.Text = lst[0].ToString();             
            }
        }

        private void ClearPaymentCredit()
        {
            lbTxtCreditBalance_ManualChoise.Text = "0.00";
            panel1.Controls.Clear();
            pictureBox2.Visible = false;
            ucTxtCardNo_ManualChoise.InpTxt = "";
            ucTxtCardNo_ManualChoise.Visible = true;
            //ucTxtCreditAmt_ManualChoise.InpTxt = "";
            //ucTxtCreditApprove_ManualChoise.InpTxt = "";
            lbDisplayCreditCard_ManualChoise.Text = "";
            lbDisplayCreditCard_ManualChoise.Visible = false;

            //ucTxtCreditAmt_ManualChoise.Text = "";
            //ucTxtCreditAmt_ManualChoise.Visible = false;
            //ucTxtCreditApprove_ManualChoise.Text = "";
            //ucTxtCreditApprove_ManualChoise.Visible = false;
            //label31.Visible = false;
        }

        private void lbDisplayCreditCard_remove_TextChanged(object sender, EventArgs e)
        {
            if (lbDisplayCreditCard_remove.Visible)
            {
                ucTxtCardNo_ManualChoise.InpTxt = lbDisplayCreditCard_remove.Text;
            }
            else
            {
                lbDisplayCreditCard_remove.Text = "";
            }
        }

        private void ucTxtCardNo_ManualChoise_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                string cardNo = ucTxtCardNo_ManualChoise.InpTxt;
                //int minlength = 15;
                //int maxlength = 16;

                if (cardNo.Trim() == "")
                {
                    Utility.AlertMessage(ResponseCode.Information
                                            , ProgramConfig.message.get("frmPayment", "PlsEnterCreditCardCode").message
                                            , ProgramConfig.message.get("frmPayment", "PlsEnterCreditCardCode").message);
                    ucTxtCardNo_ManualChoise.FocusTxt();
                    return;
                }
                else
                {
                    if (edcStatus == "1")
                    {
                        //offline
                        lbApprove_remove.Visible = true;
                        ucTxtApprove.Visible = true;
                    }
                    else if (edcStatus == "2")
                    {
                        //online
                        lbApprove_remove.Visible = false;
                        ucTxtApprove.Visible = false;
                    }

                    string newCardNo = cardNo.Substring(0, 6) + "XX" + cardNo.Substring(cardNo.Length - 4, 4);

                    string[] pmSplite = lbCardType_ManualChoise.Text.Split(' ');

                    string pmCode1 = "";
                    string pmCode2 = "";

                    if (pmSplite.Length >= 2)
                    {
                        if (pmSplite[0].Trim().Length >= 4)
                        {
                            pmCode1 = pmSplite[0].Trim().Substring(0, 4);
                        }
                        else
                        {
                            pmCode1 = pmSplite[0].Trim().PadRight(4, ' ');
                        }

                        if (pmSplite[1].Trim().Length >= 4)
                        {
                            pmCode2 = pmSplite[1].Trim().Substring(0, 4);
                        }
                        else
                        {
                            pmCode2 = pmSplite[1].Trim().PadRight(4, ' ');
                        }
                    }

                    PaymentDiscount(pmCode1, pmCode2 + newCardNo);
                    loadDiscount();

                    lbDisplayCreditCard_remove.Visible = true;
                    lbDisplayCreditCard_remove.Text = pmCode1 + pmCode2 + newCardNo;
                    creditCard = lbDisplayCreditCard_remove.Text;

                    ucTxtCardNo.Visible = false;
                    ucTxtAmount.Text = lbTxtCreditBalance_ManualChoise.Text;

                    ClearPaymentCredit();
                    pn_payment_credit.Visible = false;

                    ucTxtAmount.Visible = true;
                    ucTxtAmount.Focus();
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        //private void ucTxtCreditAmt_ManualChoise_EnterFromButton2(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ucTxtCreditAmt_ManualChoise.Text.Trim() != "")
        //        {
        //            //addCreditAmount();
        //            ucTxtCreditApprove_ManualChoise.Focus();
        //        }
        //        else
        //        {
        //            //"กรุณาระบุจำนวนเงิน";
        //            string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
        //            string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
        //            notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
        //            notify.ShowDialog(this);
        //            ucTxtCreditAmt_ManualChoise.Focus();
        //        }
        //    }
        //    catch (NetworkConnectionException net)
        //    {
        //        ProcessCheckNetWorkLost(net);
        //    }
        //}

        //private void ucTxtCreditApprove_ManualChoise_Enter(object sender, EventArgs e)
        //{
        //    int keyboradtype = 2;
        //    switch (keyboradtype)
        //    {
        //        case 1:
        //            this.ucKeyboard1.Visible = false;
        //            break;
        //        case 2:
        //            this.ucKeyboard1.Visible = true;
        //            this.ucKeyboard1.BringToFront();
        //            this.ucKeyboard1.currentInput = ucTxtCreditApprove_ManualChoise;
        //            pn_payment_credit.Location = LocationKBCredit;
        //            break;
        //        default:
        //            this.ucKeyboard1.Visible = false;
        //            break;
        //    }
        //}

        private void ucTxtCreditApprove_ManualChoise_TextBoxLeave(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
            pn_payment_credit.Location = LocationKBCredit_default;
        }

        private void btnOK_CreditCard_ManualChoise_Click(object sender, EventArgs e)
        {

        }

        private void ucTextBoxWithIcon2_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTextBoxWithIcon2Enter(sender);
        }

        private void ucTextBoxWithIcon2_EnterFromButton_1(object sender, EventArgs e)
        {
            ucTextBoxWithIcon2Enter(sender);
        }

        private void ucTextBoxWithIcon2Enter(object sender)
        {
            if (ucTextBoxWithIcon2.Text == "")
            {
                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                notify.ShowDialog(this);
                return;
            }

            RunModuleParameter pModule = new RunModuleParameter();
            RunModule(3, pModule);
            //seqPaymentStep++;
            ucTextBoxWithIcon1.Focus();
        }

        private void ucTextBoxWithIcon1_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTextBoxWithIcon1Enter();
        }

        private void ucTextBoxWithIcon1Enter()
        {
            try
            {
                //Insert Tempdlyptran
                double checkTextAmount = 0;
                if (!double.TryParse(ucTextBoxWithIcon1.Text, out checkTextAmount) || ucTextBoxWithIcon2.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                    notify.ShowDialog(this);

                    if (ucTextBoxWithIcon2.Text == "")
                    {
                        ucTextBoxWithIcon2.Focus();
                    }
                    else
                    {
                        ucTextBoxWithIcon1.Focus();
                    }
                    return;
                }

                if (ucTextBoxWithIcon1.Text == "0" || checkTextAmount == 0.0)
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                    notify.ShowDialog(this);
                    return;
                }

                if (saleProcess.selectCheckORG_TransFromQRPAYTRANS_Manual(ucTextBoxWithIcon2.Text, paymentCode).Rows.Count > 0)
                {
                    string responseMessage = String.Format(ProgramConfig.message.get("frmPayment", "CannotInputDuplicate").message
                                                        , "Reference No.");
                    Utility.AlertMessage(ResponseCode.Error, responseMessage, "N/A");
                    ucTextBoxWithIcon2.Focus();
                    return;
                }

                frmLoading.showLoading();
                StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount("CASH", ucTextBoxWithIcon1.Text, ProgramConfig.currencyDefault);
                frmLoading.closeLoading();
                if (!chkMinCash.response.next)
                {
                    Utility.AlertMessage(chkMinCash.response, chkMinCash.responseMessage, chkMinCash.helpMessage);
                    ucTextBoxWithIcon1.Focus();
                    return;
                }

                //int payId = ProgramConfig.payment.getPaymentTypeID(paymentCode); //saleProcess.selectPaymentID(paymentCode);

                string paymentDupAmt = "0";
                if (!DeleteTempPayment(paymentCode, out paymentDupAmt))
                {
                    return;
                }
                ProgramConfig.paymentDupAmt = paymentDupAmt;

                AppLog.writeLog("before frmPayment ucTextBoxWithIcon1Enter");
                frmLoading.showLoading();
                SummaryCashIn();
                frmLoading.closeLoading();
                RefreshGrid();

                double receiveCash = 0;
                receiveCash = double.Parse(ucTextBoxWithIcon1.Text) + (paymentCode.StartsWith("QR") ? Convert.ToDouble(paymentDupAmt) : 0.0);

                string formatCash = "";
                formatCash = receiveCash.ToString(displayAmt);

                if (double.Parse(lbTxtReceiveCash.Text) != 0)
                {
                    amtPrice = double.Parse(formatCash);
                    double balance = double.Parse(lbTxtBalance.Text) - receiveCash;
                    if (balance < 0)
                    {
                        if (!ProgramConfig.payment.getExcessChange(paymentCode))
                        {
                            Utility.AlertMessage(ResponseCode.Error, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help);
                            ucTextBoxWithIcon1.Focus();
                            return;
                        }
                    }

                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
                }
                else
                {
                    amtPrice = double.Parse(formatCash);
                    double balance = double.Parse(lbTxtBalanceDiff.Text) - receiveCash;
                    if (balance < 0)
                    {
                        if (!ProgramConfig.payment.getExcessChange(paymentCode))
                        {
                            Utility.AlertMessage(ResponseCode.Error, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help);
                            ucTextBoxWithIcon1.Focus();
                            return;
                        }
                    }

                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - receiveCash).ToString(displayAmt);
                }

                if (double.Parse(lbTxtBalance.Text) <= 0)
                {
                    double cashbalance = 0;
                    double cast = double.Parse(lbTxtBalance.Text) * -1;
                    lbTxtBalance.Text = cashbalance.ToString(displayAmt);

                    frmLoading.showLoading();
                    StoreResult result = saleProcess.savePaymentOffline(ucTextBoxWithIcon1.Text, paymentCode, new List<PaymentStepDet>(),ucTextBoxWithIcon2.Text, total: lbTxtBalance.Text);
                    frmLoading.closeLoading();

                    if (result.response.next)
                    {
                        ucTextBoxWithIcon1.Text = "";
                        ucTextBoxWithIcon2.Text = "";
                        ShowConfirmPayment();
                        return;
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        loadTempDLYForPayment();
                        return;
                    }
                    //loadTempDLYForPayment();
                }
                else
                {
                    frmLoading.showLoading();
                    StoreResult result = saleProcess.savePaymentOffline(ucTextBoxWithIcon1.Text, paymentCode, new List<PaymentStepDet>(),ucTextBoxWithIcon2.Text, total: lbTxtBalance.Text);
                    frmLoading.closeLoading();

                    if (!result.response.next)
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }

                    ucTextBoxWithIcon1.Text = "";
                    ucTextBoxWithIcon2.Text = "";

                    loadTempDLYForPayment();
                    btnCash_Click(null, null);

                    //ucTextBoxWithIcon2.Focus();
                }
                

            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void SaveTemp(UCTextBoxWithIcon ucTxt1, UCTextBoxWithIcon ucTxt2, string pcd, Action action)
        {
            double receiveCash = 0;
            receiveCash = double.Parse(ucTxt1.Text);

            string formatCash = "";
            formatCash = receiveCash.ToString(displayAmt);

            if (double.Parse(lbTxtReceiveCash.Text) != 0)
            {
                amtPrice = double.Parse(formatCash);
                lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
            }
            else
            {
                amtPrice = double.Parse(formatCash);
                lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                //lbTxtBalance.Text = (double.Parse(lbTxtTotalCash.Text) - receiveCash).ToString(displayAmt);
                lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - receiveCash).ToString(displayAmt);
            }

            //ถึงตรงนี้
            if (double.Parse(lbTxtBalance.Text) <= 0)
            {
                double cashbalance = 0;
                double cast = double.Parse(lbTxtBalance.Text) * -1;
                lbTxtBalance.Text = cashbalance.ToString(displayAmt);
            
                frmLoading.showLoading();
                StoreResult result = saleProcess.savePaymentOffline(ucTxt1.Text, pcd, new List<PaymentStepDet>(), total: lbTxtBalance.Text);
                frmLoading.closeLoading();
                if (result.response.next)
                {
                    _hpContract = ucTxt2.Text;
                    ucTxt2.Text = "";
                    ucTxt1.Text = "";
                    ucTxt1.Focus();
                    action();
                    ShowConfirmPayment();
                    return;
                }
                else
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                    return;
                }
            }
            else
            {
                frmLoading.showLoading();
                StoreResult result = saleProcess.savePaymentOffline(ucTxt1.Text, pcd, new List<PaymentStepDet>(), total: lbTxtBalance.Text);
                frmLoading.closeLoading();
                if (!result.response.next)
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                    return;
                }

                _hpContract = ucTxt2.Text;
                ucTxt2.Text = "";
                ucTxt1.Text = "";
                ucTxt1.Focus();
                action();
                loadTempDLYForPayment();

                var lstPMPolicy = ProgramConfig.paymentPolicy.GetPaymentPolicyByFunction(FunctionID.PaymentForHirePurchase);
                PaymentMenuIconCollections pmMenuIcon = new PaymentMenuIconCollections(ProgramConfig.paymentMenuIcon.data(), lstPMPolicy);

                CurrencyCollections newCurrency = new CurrencyCollections();
                foreach (var itmCur in _currency.ToList())
                {
                    foreach (var itmPo in lstPMPolicy)
                    {
                        if (itmPo.paymentCode == itmCur.pmCode)
                        {
                            newCurrency.Add(itmCur);
                        }
                    }
                }
                _currency = newCurrency;
                GenerateMenu3(pmMenuIcon);
                btnCash_Click(null, null);
            }

            SummaryCashIn();
            RefreshGrid();

        }

        public void btnPayment_HirePurchase_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                DataTable loadTemp = saleProcess.loadTempDlyForPayment(ProgramConfig.saleRefNo);
                frmLoading.closeLoading();
                if (loadTemp.Rows != null)
                {
                    for (int i = 0; i < loadTemp.Rows.Count; i++)
                    {
                        string pcd = loadTemp.Rows[i]["PCD"].ToString();
                        string pmCode = pcd.Substring(0, 4);

                        var lstPMPolicy = ProgramConfig.paymentPolicy.GetPaymentPolicyByFunction(FunctionID.PaymentCoHirePurchase);

                        if (!lstPMPolicy.Any(a => a.paymentCode == pmCode))
                        {
                            //TO DO Change language (Done)
                            Utility.AlertMessage(ResponseCode.Error, String.Format(ProgramConfig.message.get("frmPayment", "HirePurchaseCoPayment").message, pmCode), ProgramConfig.message.get("frmPayment", "HirePurchaseCoPayment").help);
                            return;
                        }
                    }
                }

                List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID("9"); //dtData.Select(" ReferMenuID = '8' ");
                DialogResult res = System.Windows.Forms.DialogResult.None;
                string header = "";

                if (lstPmMenuIcon.Count == 0)
                {
                    //TO DO Change language
                    Utility.AlertMessage(ResponseCode.Error, "No SubMenu Hire Purchase");
                    return;
                }
                else
                {
                    if (lstPmMenuIcon.Count > 0)
                    {
                        if (lstPmMenuIcon.Count == 1)
                        {
                            _OPtemplate = lstPmMenuIcon[0].PaymentStepGroupID;
                            _OPpaymentCode = lstPmMenuIcon[0].PaymentMainCode;
                            _OPpaymentName = lstPmMenuIcon[0].LabelName;
                            res = System.Windows.Forms.DialogResult.Yes;
                        }
                        else
                        {
                            Program.control.CloseForm("frmOtherPayment");
                            frmOtherPayment fOther = new frmOtherPayment(lstPmMenuIcon, "Hire Purchase", 9);
                            res = fOther.ShowDialog(this);
                        }
                    }
                }

                if (res != System.Windows.Forms.DialogResult.None)
                {
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Panel pntemp = GetPanelTemplate(_OPtemplate);
                        //if (pntemp != null)
                        //{

                        //pn_payment_template = pntemp;
                        //template = _OPtemplate;
                        paymentCode = _OPpaymentCode;
                        header = _OPpaymentName;
                        InitialImageButtonFromSEQ(btnPayment_Other, btnPayment_Other.Tag + "", false);
                        //dialogFromOther = System.Windows.Forms.DialogResult.Yes;
                        //Program.control.CloseForm("frmOtherPayment");
                        //}
                        //else
                        //{
                        //    DisablePaymentGroup();
                        //    return;
                        //}
                    }
                    else
                    {
                        DisablePaymentGroup();
                        return;
                    }
                }
                else
                {
                    DisablePaymentGroup();
                    return;
                }

                PaymentDiscount(paymentCode, "");

                SummaryCashIn();
                //RefreshGrid();

                DisablePaymentGroup();
                pictureBox3.BringToFront();

                UCHirePurchase ucHP = new UCHirePurchase(this, paymentCode);
                ucHP.lbHeader.Text = header;
                ucHP.lbPrPriceAmt.Text = lbTxtBalance.Text;
                ucHP.Location = new Point(0, 43);
                ucHP.ucInstallment.Text = lbTxtBalance.Text;

                pnMainPayment.Controls.Add(ucHP);

                Utility.SetBackGroundBrightness(panelMainPayment, ucHP);
                ucHP.BringToFront();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }      

        private void btn_Next_Click(object sender, EventArgs e)
        {
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage++;
            if (nextPage <= Convert.ToInt32(lb_PageTotal.Text))
            {
                //pageNo = nextPage.ToString();
                lb_PageNo.Text = nextPage.ToString();
                SetButtonCreditCard(lastMenuID, lb_PageNo.Text);
            }
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage--;
            if (nextPage > 0)
            {
                lb_PageNo.Text = nextPage.ToString();
                SetButtonCreditCard(lastMenuID, lb_PageNo.Text);
            }
        }

        public void SetIndexKeyBoard()
        {
            ucKeyboard1.BringToFront();
            pnMainPayment.Controls.SetChildIndex(ucKeyboard1, 0);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            CloseBankNote();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnEditClick();
        }

        private void ucTextBoxWithIcon1_EnterFromButton_1(object sender, EventArgs e)
        {
            ucTextBoxWithIcon1Enter();
        }



        private void ChangeLanguageButtonMenu()
        {
            List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID("0");
            if (lstPmMenuIcon.Count > 0)
            {
                foreach (PaymentMenuIcon itm in lstPmMenuIcon)
                {
                    //DataRow dr = dt.Rows[i];
                    string row = itm.Row.ToString();
                    string col = itm.Comlumn.ToString();
                    string labelTxt = itm.LabelName;
                    string menuID = itm.MenuID.ToString();

                    if (menuID == "2") // if payment code = CASH
                    {
                        btnPayment_Cash.Text = labelTxt;
                        btnPayment_Cash.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //InitialImageButtonFromRowCol(btnPayment_Cash, row, col, true);
                        btnPayment_Cash.Update();
                        continue;
                    }
                    else if (menuID == "1") // if payment code = CPN1
                    {
                        btnPayment_Coupon.Text = labelTxt;
                        btnPayment_Coupon.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //InitialImageButtonFromRowCol(btnPayment_Coupon, row, col, true);
                        btnPayment_Coupon.Update();
                        continue;
                    }
                    else if (menuID == "3")
                    {
                        btnPayment_Credit.Text = labelTxt;
                        btnPayment_Credit.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));                      
                        //InitialImageButtonFromRowCol(btnPayment_Credit, row, col, true);
                        btnPayment_Credit.Update();
                        continue;
                    }
                    else if (menuID == "4") // if payment code = GFSL
                    {
                        btnPayment_GiftVoucher.Text = labelTxt;
                        btnPayment_GiftVoucher.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));                                              
                        //InitialImageButtonFromRowCol(btnPayment_GiftVoucher, row, col, true);
                        btnPayment_GiftVoucher.Update();
                        continue;
                    }
                    else if (menuID == "9")
                    {
                        btnPayment_HirePurchase.Text = labelTxt;
                        btnPayment_HirePurchase.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));                                              
                        //InitialImageButtonFromRowCol(btnPayment_HirePurchase, row, col, true);
                        btnPayment_HirePurchase.Update();
                        continue;
                    }
                    else if (menuID == "6")
                    {
                        btnPayment_Other.Text = labelTxt;
                        btnPayment_Other.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));                                              
                        //InitialImageButtonFromRowCol(btnPayment_Other, row, col, true);
                        btnPayment_Other.Update();
                        continue;
                    }

                    Button btnCurrent = new Button();
                    if (row == "1" && col == "1")
                    {
                        foreach (Button btn in btnPaymentGen)
                        {
                            if (btn.Name == "btnGen1")
                            {
                                btnCurrent = btn;
                                btn.Text = labelTxt;
                            }
                        }
                    }
                    else if (row == "1" && col == "2")
                    {
                        foreach (Button btn in btnPaymentGen)
                        {
                            if (btn.Name == "btnGen2")
                            {
                                btnCurrent = btn;
                                btn.Text = labelTxt;
                            }
                        }
                    }
                    else if (row == "1" && col == "3")
                    {
                        foreach (Button btn in btnPaymentGen)
                        {
                            if (btn.Name == "btnGen3")
                            {
                                btnCurrent = btn;
                                btn.Text = labelTxt;
                            }
                        }
                    }
                    else if (row == "2" && col == "1")
                    {
                        foreach (Button btn in btnPaymentGen)
                        {
                            if (btn.Name == "btnGen4")
                            {
                                btnCurrent = btn;
                                btn.Text = labelTxt;
                            }
                        }
                    }
                    else if (row == "2" && col == "2")
                    {
                        foreach (Button btn in btnPaymentGen)
                        {
                            if (btn.Name == "btnGen5")
                            {
                                btnCurrent = btn;
                                btn.Text = labelTxt;
                            }
                        }
                    }
                    else if (row == "2" && col == "3")
                    {
                        foreach (Button btn in btnPaymentGen)
                        {
                            if (btn.Name == "btnGen6")
                            {
                                btnCurrent = btn;
                                btn.Text = labelTxt;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }

                    btnCurrent.Font = new System.Drawing.Font(ProgramConfig.language.FontName, labelTxt.Length >= 10 ? 9.7F : 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnCurrent.Update();
                }
            }
        }

        private void ucTextBoxWithIcon2_Enter(object sender, EventArgs e)
        {
                this.ucKeyboard1.Visible = true;
                this.ucKeyboard1.BringToFront();
                this.ucKeyboard1.currentInput = ucTextBoxWithIcon2;
            //this.ucKeyboard1.updateLanguage();
        }

        private void ucTextBoxWithIcon2_TextBoxLeave(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
        }

        private void lbDisplayCreditCard_remove_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbDisplayCreditCard_remove);
        }

        private void ucTxtCardNo_TextBoxFocus(object sender, EventArgs e)
        {
            ucKeyboard1.updateLanguage(new Language(1));
        }

        private void ucTxtCardNo_ManualChoise_TextBoxFocus(object sender, EventArgs e)
        {
            ucKeyboard1.updateLanguage(new Language(1));
        }

        private void ucKeyboard1_VisibleChanged(object sender, EventArgs e)
        {
            if (ucKeyboard1.Visible)
            {
                ucKeyboard1.updateLanguage();
            }
        }

        private void lbTxtRefNo_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbTxtRefNo);
        }

        private void ucHeader1_HambergerItemClick(object sender, EventArgs e)
        {
            CancelSale(sender);
        }

        private void btnEDCInterface_Click(object sender, EventArgs e)
        {
            ucTxtApprove.Visible = false;

            frmEDCProcess fEDCProcess = new frmEDCProcess(EventEDC.NormalSale, lbTxtBalance.Text
                                                        , (Convert.ToDouble(lbTxtReceiveCash.Text) + Convert.ToDouble(lbTxtBalance.Text)).ToString(displayAmt)
                                                        , lbTxtBalanceDiff.Text);
            fEDCProcess.ShowDialog(this);

            var EDCResult = fEDCProcess.edcResult;

            if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    creditCard = EDCResult.creditCard;
                    paymentCode = EDCResult.paymentCode;
                    ucTxtAmount.Text = EDCResult.edcAmount.ToString();
                    ucTxtApprove.Text = EDCResult.ApproveCode;
                    ucTxtApprove_EnterFromButton(sender, e);
                    fEDCProcess.Dispose();
                }
                catch (Exception)
                {
                    //TO DO Void EDC
                    fEDCProcess.Dispose();
                }
            }
            else if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.No)
            {
                Utility.AlertMessage(EDCResult.res);
            }

        }

        private void btn_QRPayment_Click(object sender, EventArgs e)
        {
            if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
            {
                if (!Process_POD("QRPP"))
                {
                    return;
                }   
            }

            DisablePaymentGroup();
            InitialImageButtonFromSEQ(btnPayment_QRPayment, btnPayment_QRPayment.Tag.ToString(), false);

            frmPopupQRPaymentSubMenu fQRSubMenu = new frmPopupQRPaymentSubMenu();
            var res = fQRSubMenu.ShowDialog(this);

            if (res == System.Windows.Forms.DialogResult.OK && fQRSubMenu.menu == QRPaymentOnlineMenu.QR_CscanB)
            {
                frmQRPaymentOnline fQROnline = new frmQRPaymentOnline(lbTxtBalance.Text);
                fQROnline.Show(this);
            }
            else if (res == System.Windows.Forms.DialogResult.OK && fQRSubMenu.menu == QRPaymentOnlineMenu.QR_BscanC)
            {
                pn_payment_QRPPOnline.Visible = true;
                pn_payment_QRPPOnline.BringToFront();
                pn_QRPM_ucTxt_Ref1.InpTxt = Convert.ToDouble(lbTxtBalance.Text).ToString();
                pn_QRPM_ucTxt_Ref2.FocusTxt();
            }
            else if (res == System.Windows.Forms.DialogResult.Cancel)
            {
                DisablePaymentGroup();
                pn_payment_QRPPOnline.Visible = false;
            }
        }

        private void pn_QRPM_ucTxt_Ref1_EnterFromButton(object sender, EventArgs e)
        {
            if (CheckChangeStatus(pn_QRPM_ucTxt_Ref1, pn_QRPM_ucTxt_Ref1.Text, true))
            {
                pn_QRPM_ucTxt_Ref2.FocusTxt();   
            }      
        }

        private void pn_QRPM_ucTxt_Ref2_TextBoxKeydown(object sender, EventArgs e)
        {
            ConfirmQRPM_CscanB(pn_QRPM_ucTxt_Ref2);
        }

        private void pn_QRPM_ucTxt_Ref2_EnterFromButton(object sender, EventArgs e)
        {
            ConfirmQRPM_CscanB(pn_QRPM_ucTxt_Ref2);
        }

        private void ConfirmQRPM_CscanB(UCTextBoxWithIcon ucTxt)
        {
            string total = ucTxt.InpTxt;
            saleProcess.PaymentDiscount("QRPP", "");
            loadDiscount();

            SummaryCashIn();
            RefreshGrid();

            //TO DO Call Procedure pos_CheckLastPayWithCoupon 
            //if ture Call TiketUseSum

            //res = saleProcess.selectPAYMENT_PARAMETER("", "");
            //if (res.response.next)
            //{
            //    if (_total > Convert.ToDouble(res.otherData.Rows[0]["QRPAYMENT_LIMIT_AMT"]))
            //    {
            //        //Alert Message QR Payment ไม่สามารถชำระได้มากกว่า xx,xxx บาท!!!
            //        this.Dispose();
            //        return;
            //    }
            //}



            if (!CheckChangeStatus(pn_QRPM_ucTxt_Ref1, pn_QRPM_ucTxt_Ref1.Text, true))
            {
                pn_QRPM_ucTxt_Ref1.FocusTxt();
                return;
            }


            double receiveCash = 0;

            receiveCash = double.Parse(pn_QRPM_ucTxt_Ref1.Text);

            string formatCash = "";
            formatCash = receiveCash.ToString(displayAmt);

            if (double.Parse(lbTxtReceiveCash.Text) != 0)
            {
                amtPrice = double.Parse(formatCash);
                lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
            }
            else
            {
                amtPrice = double.Parse(formatCash);
                lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - receiveCash).ToString(displayAmt);
            }

            bool IsEndReceipt = double.Parse(lbTxtBalance.Text) <= 0;

            frmQRPaymentOnlineBscanC fBscanC = new frmQRPaymentOnlineBscanC(total, pn_QRPM_ucTxt_Ref1.InpTxt, IsEndReceipt);
            var res = fBscanC.ShowDialog(this);

            if (!IsEndReceipt)
            {
                loadTempDLYForPayment();
            }

            if (res == System.Windows.Forms.DialogResult.Cancel)
            {           
                pn_QRPM_ucTxt_Ref2.InpTxt = "";
                pn_QRPM_ucTxt_Ref2.FocusTxt();
            }
            else if (res == System.Windows.Forms.DialogResult.OK)
            {
                SetDefaultPayment(_defaultPayment);
            }
            else
            {
                pn_QRPM_ucTxt_Ref1.InpTxt = "";
                pn_QRPM_ucTxt_Ref2.InpTxt = "";
                pn_QRPM_ucTxt_Ref1.FocusTxt();
            }
            
        }

        private void pn_payment_temp0_VisibleChanged(object sender, EventArgs e)
        {
            pictureBox2.Visible = pn_payment_temp0.Visible;
        }

        private void btnOkPn0_Click(object sender, EventArgs e)
        {
            string mainRefNo = "";
            List<string> subRefNo = new List<string>();
            string amt = "";
            UCTextBoxWithIcon uc1 = null;
            List<PaymentStepDet> lstPayStep = new List<PaymentStepDet>();

            if (pn_payment_temp0.Visible)
            {
                for (int i = 1; i <= 10; i++)
                {
                    UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;

                    if (ucTxtPn0.Visible)
                    {                 
                        if (!SubEnterFromButton(ucTxtPn0))
                        {
                            ucTxtPn0.FocusTxt();
                            return;
                        }


                        if (ucTxtPn0.StepID == PaymentStepDetail_StepID.Input_Main_Reference)
                        {
                            mainRefNo = ucTxtPn0.Text;
                        }

                        if (ucTxtPn0.StepID == PaymentStepDetail_StepID.Input_Sub_Reference)
                        {
                            subRefNo.Add(ucTxtPn0.Text);
                        }

                        if (ucTxtPn0.StepID == PaymentStepDetail_StepID.Input_Amount || ucTxtPn0.StepID == PaymentStepDetail_StepID.Display_Amount)
                        {
                            amt = ucTxtPn0.Text;
                            uc1 = ucTxtPn0;
                        }

                        PaymentStepDet payStepDet = new PaymentStepDet();
                        payStepDet.PaymentGroupID = ucTxtPn0.PaymentStepGroupID;
                        payStepDet.Seq = ucTxtPn0.SeqTextBox;
                        payStepDet.StepID = ((int)ucTxtPn0.StepID).ToString();
                        payStepDet.DataType = ucTxtPn0.DataType;
                        payStepDet.DataValue = ucTxtPn0.Text;
                        payStepDet.PMCode = paymentCode;

                        lstPayStep.Add(payStepDet);
                    }
                }
            }

            if (pn_payment_temp4.Visible)
            {
                for (int i = 1; i <= 2; i++)
                {
                    UCTextBoxWithIcon ucTxtPn4 = pn_payment_temp4.Controls.Find("ucTxtPn4_" + i, true).FirstOrDefault() as UCTextBoxWithIcon;

                    if (ucTxtPn4.Visible)
                    {
                        if (!SubEnterFromButton(ucTxtPn4))
                        {
                            ucTxtPn4.FocusTxt();
                            return;
                        }


                        if (ucTxtPn4.StepID == PaymentStepDetail_StepID.Input_Main_Reference)
                        {
                            mainRefNo = ucTxtPn4.Text;
                        }

                        if (ucTxtPn4.StepID == PaymentStepDetail_StepID.Input_Sub_Reference)
                        {
                            subRefNo.Add(ucTxtPn4.Text);
                        }

                        if (ucTxtPn4.StepID == PaymentStepDetail_StepID.Input_Amount || ucTxtPn4.StepID == PaymentStepDetail_StepID.Display_Amount)
                        {
                            amt = ucTxtPn4.Text;
                            uc1 = ucTxtPn4;
                        }

                        PaymentStepDet payStepDet = new PaymentStepDet();
                        payStepDet.PaymentGroupID = ucTxtPn4.PaymentStepGroupID;
                        payStepDet.Seq = ucTxtPn4.SeqTextBox;
                        payStepDet.StepID = ((int)ucTxtPn4.StepID).ToString();
                        payStepDet.DataType = ucTxtPn4.DataType;
                        payStepDet.DataValue = ucTxtPn4.Text;
                        payStepDet.PMCode = paymentCode;

                        lstPayStep.Add(payStepDet);
                    }
                }
            }


            if (mainRefNo != "" && amt != "")
            {
                Parallel.ForEach(lstPayStep, l => l.MainRef = mainRefNo);
                string pcd = paymentCode + mainRefNo;
                SaveDataPaymentOffline(uc1, amt, pcd, paymentCode, lstPayStep);
            }
            else
            {
                Utility.AlertMessage(ResponseCode.Error, "กรุณากรอกข้อมูลให้ครบ");
            }
          // pn_payment_temp0
        }

        private void ucTxtPn0_EnterFromButton(object sender, EventArgs e)
        {
            UCTextBoxWithIcon ucTxt = (UCTextBoxWithIcon)sender;
            string[] nameSplite = ucTxt.Name.Split('_');
            string panel = nameSplite[0];
            int seq = Convert.ToInt32(nameSplite[1]);
            seq++;
            if (panel == "ucTxtPn0")
            {
                UCTextBoxWithIcon ucTxtPn0 = pn_payment_temp0.Controls.Find("ucTxtPn0_" + seq, true).FirstOrDefault() as UCTextBoxWithIcon;
                if (ucTxtPn0 != null)
                {
                    ucTxtPn0.FocusTxt();
                }
            }

            if (panel == "ucTxtPn4")
            {
                UCTextBoxWithIcon ucTxtPn4 = pn_payment_temp4.Controls.Find("ucTxtPn4_" + seq, true).FirstOrDefault() as UCTextBoxWithIcon;
                if (ucTxtPn4 != null)
                {
                    ucTxtPn4.FocusTxt();
                }
            }


            SubEnterFromButton(ucTxt);
        }

        private bool SubEnterFromButton(UCTextBoxWithIcon ucTxt)
        {
            if (ucTxt.DataType == PaymentStepDetail_DataType.Money)
            {
                frmLoading.showLoading();
                StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount("CASH", ucTxt.Text, ProgramConfig.currencyDefault);
                frmLoading.closeLoading();
                if (!chkMinCash.response.next)
                {
                    Utility.AlertMessage(chkMinCash.response, chkMinCash.responseMessage, chkMinCash.helpMessage);
                    ucTxt.Focus();
                    return false;
                }
            }

            RunModuleParameter pModule = new RunModuleParameter();
            pModule.dataType = ucTxt.DataType;
            pModule.inpRef1 = ucTxt.Text;
            pModule.pmCode = paymentCode;
            return RunModule(Convert.ToInt32(ucTxt.SeqTextBox), pModule);
        }

        public void SaveDataPaymentOffline(UCTextBoxWithIcon ucTxt, string amt, string pcd, string paymentCode, List<PaymentStepDet> lstPayDet)
        {
            //Insert Tempdlyptran
            try
            {
                string balance = lbTxtBalance.Text;

                frmLoading.showLoading();
                SummaryCashIn();
                frmLoading.closeLoading();
                RefreshGrid();

                //double receiveCash = 0;
                //receiveCash = double.Parse(amt);

                //string formatCash = "";
                //formatCash = receiveCash.ToString(displayAmt);

                if (!CheckChangeStatus(ucTxt, amt))
                {
                    ucTxt.FocusTxt();
                    return;
                }

                if (double.Parse(lbTxtBalance.Text) <= 0)
                {
                    double cashbalance = 0;
                    double cast = double.Parse(lbTxtBalance.Text) * -1;
                    lbTxtBalance.Text = cashbalance.ToString(displayAmt);

                    frmLoading.showLoading();
                    string depoRef = "";
                    if (paymentCode == "DEPO")
                    {
                        depoRef = depositRef;
                        paymentCode = pcd;
                    }
                    DeleteTempPayment(paymentCode);
                    StoreResult result = saleProcess.savePaymentOffline(amt, pcd, lstPayDet, total: balance, depoRef: depoRef);
                    frmLoading.closeLoading();

                    if (result.response.next)
                    {
                        ShowConfirmPayment();
                        return;
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        loadTempDLYForPayment();
                        return;
                    }
                    //loadTempDLYForPayment();
                }
                else
                {
                    frmLoading.showLoading();
                    string depoRef = "";
                    if (paymentCode == "DEPO")
                    {
                        depoRef = depositRef;
                        paymentCode = pcd;
                    }

                    DeleteTempPayment(paymentCode);
                    StoreResult result = saleProcess.savePaymentOffline(amt, pcd, lstPayDet, total: balance, depoRef: depoRef);
                    frmLoading.closeLoading();

                    if (!result.response.next)
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }

                    loadTempDLYForPayment();
                    //ucTextBoxWithIcon2.Focus();
                    SetDefaultPayment(_defaultPayment);

                }


            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }          
        }

        public bool CheckChangeStatus(UCTextBoxWithIcon ucTxt, string amt, bool IsOnlyValidate = false)
        {
            double receiveCash = 0;
            receiveCash = double.Parse(amt);

            string formatCash = "";
            formatCash = receiveCash.ToString(displayAmt);

            if (double.Parse(lbTxtReceiveCash.Text) != 0)
            {
                amtPrice = double.Parse(formatCash);
                double balance = double.Parse(lbTxtBalance.Text) - receiveCash;
                if (balance < 0)
                {
                    if (!ProgramConfig.payment.getExcessChange(paymentCode))
                    {
                        Utility.AlertMessage(ResponseCode.Error, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help);
                        ucTxt.FocusTxt();
                        return false;
                    }
                }

                if (!IsOnlyValidate)
                {
                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalance.Text) - receiveCash).ToString(displayAmt);
                }

            }
            else
            {
                amtPrice = double.Parse(formatCash);
                double balance = double.Parse(lbTxtBalanceDiff.Text) - receiveCash;
                if (balance < 0)
                {
                    if (!ProgramConfig.payment.getExcessChange(paymentCode))
                    {
                        Utility.AlertMessage(ResponseCode.Error, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message, ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help);
                        ucTxt.FocusTxt();
                        return false;
                    }
                }

                if (!IsOnlyValidate)
                {
                    lbTxtReceiveCash.Text = amtPrice.ToString(displayAmt);
                    lbTxtBalance.Text = (double.Parse(lbTxtBalanceDiff.Text) - receiveCash).ToString(displayAmt);
                }
            }

            return true;
        }
    }
}
