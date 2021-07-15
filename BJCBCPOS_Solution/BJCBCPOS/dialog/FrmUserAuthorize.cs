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

namespace BJCBCPOS
{
    public partial class frmUserAuthorize : Form
    {
        private delegate DialogResult InvokeDelegate(Form parent);

        private bool IsPaint = false;
        private FunctionID _function = FunctionID.NoFunctionID;
        private bool _diffUser = false;
        public string refNo { get; set; }
        private StartProcess process;
        private int keyboardType = 0;
        private Point defaultLoginPanelLocation;
        private Point newloginPanelLocation = new Point(245, 51);
        public string maxPriceChange = "0";
        public string maxDeleteItemAmt = "0";
        public string maxCancelReceiptAmt = "0";
        public string eventName;

        public FunctionID function
        {
            get { return _function; }
            set { this._function = value; }
        }

        public bool diffUser
        {
            get { return _diffUser; }
            set { this._diffUser = value; }
        }

        public frmUserAuthorize()
        {
            InitializeComponent();
            process = new StartProcess();
            lbMainUser.Visible = true;
            lbMainUser.BringToFront();
            ucTBWIEmpUser.placeHolder = AppMessage.getMessage(ProgramConfig.language, "frmUserAuthorize", "lbMainUser"); //lbMainUser.Text;
        }

        public frmUserAuthorize(string header, string lbText)
        {
            InitializeComponent();
            eventName = header;
            process = new StartProcess();
            if (lbText == "1") //ผู้ใช้งาน
            {
                lbMainUser.Visible = false;
                lbUser.BringToFront();
                lbUserVal.Text = ProgramConfig.userId;
                lbUserVal.Visible = true;
                lbUserVal.BringToFront();
                ucTBWIEmpUser.InpTxt = lbUserVal.Text;
                ucTBWIEmpUser.placeHolder = lbUser.Text;
                ucTBWIEmpUser.Visible = false;
                ucTBWIEmpUser.placeHolder = AppMessage.getMessage(ProgramConfig.language, "frmUserAuthorize", "lbUser"); //lbUser.Text;
            }
            else
            {
                lbMainUser.Visible = true;
                lbMainUser.BringToFront();
                ucTBWIEmpUser.placeHolder = lbMainUser.Text;
                ucTBWIEmpUser.Visible = true;
                lbUserVal.Visible = true;
                ucTBWIEmpUser.placeHolder = AppMessage.getMessage(ProgramConfig.language, "frmUserAuthorize", "lbMainUser"); //lbMainUser.Text;
            }
            //lbMainUser.Text = lbText;
            //lbMessage.Text = header;
        }

        public frmUserAuthorize(string header, bool IsDiffUser, string functionId = "")
        {
            InitializeComponent();
            eventName = header;
            process = new StartProcess();
            if (!IsDiffUser)
            {
                lbMainUser.Visible = false;
                lbUser.BringToFront();
                lbUserVal.Text = ProgramConfig.userId;
                lbUserVal.Visible = true;
                lbUserVal.BringToFront();
                ucTBWIEmpUser.InpTxt = lbUserVal.Text;
                ucTBWIEmpUser.placeHolder = lbUser.Text;
                ucTBWIEmpUser.Visible = false;
                ucTBWIEmpUser.placeHolder = AppMessage.getMessage(ProgramConfig.language, "frmUserAuthorize", "lbUser"); //lbUser.Text;
            }
            else
            {
                lbMainUser.Visible = true;
                lbMainUser.BringToFront();
                ucTBWIEmpUser.placeHolder = lbMainUser.Text;
                ucTBWIEmpUser.Visible = true;
                lbUserVal.Visible = false;
                ucTBWIEmpUser.placeHolder = AppMessage.getMessage(ProgramConfig.language, "frmUserAuthorize", "lbMainUser"); //lbMainUser.Text;
            }
            //lbMainUser.Text = lbText;
            //lbMessage.Text = header;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    IsPaint = true;
                }
            }
        }

        public void btnEnable()
        {
            btnOK.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void btnDisable()
        {
            btnOK.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                //btnDisable();
                string user = ucTBWIEmpUser.Text;
                string password = ucTBWIEmpPass.Text;
                StoreResult result = process.checkAuthorize(function, user, password, refNo);
                if (result.response.next)
                {
                    maxPriceChange = result.otherData.Rows[0]["MaxPriceChange"].ToString();
                    maxDeleteItemAmt = result.otherData.Rows[0]["MaxDeleteItemAmt"].ToString();
                    maxCancelReceiptAmt = result.otherData.Rows[0]["MaxCancelReceiptAmt"].ToString();

                    // super user id and authority completly add in ProgramConfig
                    if (result.otherData.Rows[0]["Profile_Status"].ToString() == "1")
                    {
                        ucTBWIEmpUser.Text = "";
                        ucTBWIEmpPass.Text = "";
                        string responseMessage = ProgramConfig.message.get("UserAuthorize", "NoAuthorize").message;
                        string helpMessage = ProgramConfig.message.get("UserAuthorize", "NoAuthorize").help;
                        frmNotify notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                        notify.ShowDialog(this);
                        this.DialogResult = DialogResult.No;
                    }
                    else if (result.otherData.Rows[0]["Profile_Status"].ToString() == "2")
                    {
                        refNo = "";
                        if (result.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                        }
                        else if (result.response == ResponseCode.PasswordExpired)
                        {
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                            frmChangePassword chgPass = new frmChangePassword("frmUserAuthorize");
                            if (chgPass.ShowDialog(this) == DialogResult.OK)
                            {
                                ucTBWIEmpUser.Text = "";
                                ucTBWIEmpPass.Text = "";
                                ucTBWIEmpUser.Focus();

                                ProgramConfig.superUserId = string.Empty;
                                ProgramConfig.superPassword = string.Empty;
                                ProgramConfig.superUserName = string.Empty;
                                ProgramConfig.superUserAuthorizeResult = null;
                            }
                            btnEnable();
                            return;
                        }

                        this.DialogResult = DialogResult.Yes;
                        this.Dispose();
                    }
                    else
                    {
                        if (result.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                        }
                        else if (result.response == ResponseCode.PasswordExpired)
                        {
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                            frmChangePassword chgPass = new frmChangePassword("frmUserAuthorize");
                            if (chgPass.ShowDialog(this) == DialogResult.OK)
                            {
                                ucTBWIEmpUser.Text = "";
                                ucTBWIEmpPass.Text = "";
                                ucTBWIEmpUser.Focus();

                                ProgramConfig.superUserId = string.Empty;
                                ProgramConfig.superPassword = string.Empty;
                                ProgramConfig.superUserName = string.Empty;
                                ProgramConfig.superUserAuthorizeResult = null;
                            }
                            btnEnable();
                            return;
                        }

                        this.DialogResult = DialogResult.Yes;
                        this.Dispose();
                        //ucTBWIEmpUser.Text = "";
                        //ucTBWIEmpPass.Text = "";
                        //string responseMessage = ProgramConfig.message.get("UserAuthorize", "NoAuthorize").message;
                        //string helpMessage = ProgramConfig.message.get("UserAuthorize", "NoAuthorize").help;
                        //frmNotify notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                        //notify.ShowDialog(this);
                        //this.DialogResult = DialogResult.No;
                    }
                }
                else
                {
                    btnEnable();
                    frmNotify notify = new frmNotify(result);
                    notify.ShowDialog(this);
                    this.DialogResult = DialogResult.No;
                    ucTBWIEmpPass.Focus();
                }
                if (this.Visible)
                {
                    this.Focus();
                    if (ucTBWIEmpUser.Focused || ucTBWIEmpUser.TextBox.Focused)
                    {
                        ucTBWIEmp_Enter(null, null);
                    }
                    else if (ucTBWIEmpPass.Focused || ucTBWIEmpPass.TextBox.Focused)
                    {
                        ucTBWIEmpPass_Enter(null, null);
                    }
                }
                btnEnable();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException ex)
            {
                //throw ex;
                DialogResult = DialogResult.Abort;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            btnDisable();
            this.DialogResult = DialogResult.Ignore;
            this.Dispose();
            frmLoading.closeLoading();
        }

        private void frmUserAuthorize_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmUserAuthorize_Load(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            // clear old super user data
            ProgramConfig.superUserId = null;
            ProgramConfig.superUserAuthorizeResult = null;

            // change language
            AppMessage.fillForm(ProgramConfig.language, this);
            int.TryParse(ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Login_KeyboradDisplay.parameterCode).ToString(), out keyboardType);
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);
                defaultLoginPanelLocation = panel1.Location;
                this.Location = this.Owner.Location;
            }

            headerChangeText();
            if (ucTBWIEmpUser.Visible)
            {
                ucTBWIEmpUser.Select();
            }
            else
            {
                ucTBWIEmpPass.Select();
            }

            if (function.formatValue != "N/A" || function.formatValue.Trim() != "")
            {
                ucFooter1.lbFunction.Text = function.formatValue;
            }
            
            frmLoading.closeLoading();
        }

        public void headerChangeText()
        {
            if (eventName == "OpenDay")
            {
                lbMessage.Text = lbOpenDay.Text;
            }
            else if (eventName == "CashOut")
            {
                lbMessage.Text = lbCashOut.Text;
            }
            else if (eventName == "CashIn")
            {
                lbMessage.Text = lbCashIn.Text;
            }
            else if (eventName == "EndOfShift")
            {
                lbMessage.Text = lbEndofShift.Text;
            }
            else if (eventName == "EndOfTill")
            {
                lbMessage.Text = lbEndofTill.Text;
            }
            else if (eventName == "Sale")
            {
                lbMessage.Text = lbSale.Text;
            }
            else if (eventName == "ReturnReceipt")
            {
                lbMessage.Text = lbReturnReceipt.Text;
            }
            else if (eventName == "ReturnProduct")
            {
                lbMessage.Text = lbReturnProduct.Text;
            }
            else if (eventName == "Void")
            {
                lbMessage.Text = lbVoid.Text;
            }
            else if (eventName == "ChangePassword")
            {
                lbMessage.Text = lbChangePassword.Text;
            }
            else if (eventName == "CheckProduct")
            {
                lbMessage.Text = lbCheckProduct.Text;
            }
            else if (eventName == "ReportDaySale")
            {
                lbMessage.Text = lbReportDaySale.Text;
            }
            else if (eventName == "ReportReceipt")
            {
                lbMessage.Text = lbReportReceipt.Text;
            }
            else if (eventName == "CancelCashOut")
            {
                lbMessage.Text = lbCancelCashOut.Text;
            }
            else if (eventName == "ConfirmPayment")
            {
                lbMessage.Text = lbConfirmPayment.Text;
            }
            else if (eventName == "ReturnSuccess")
            {
                lbMessage.Text = lbReturnSuccess.Text;
            }
            else if (eventName == "VoidSuccess")
            {
                lbMessage.Text = lbVoidSuccess.Text;
            }
            else if (eventName == "DeleteItem")
            {
                lbMessage.Text = lbDeleteItem.Text;
            }
            else if (eventName == "EditItem")
            {
                lbMessage.Text = lbEditItem.Text;
            }
            else if (eventName == "CancelSale")
            {
                lbMessage.Text = lbCancelSale.Text;
            }
            else if (eventName == "CashOut")
            {
                lbMessage.Text = lbCashOut.Text;
            }
            else if (eventName == "Report")
            {
                lbMessage.Text = lbReport.Text;
            }
            else if (eventName == "Redeem")
            {
                lbMessage.Text = lbRedeem.Text;
            }
            else if (eventName == "PrintExport")
            {
                lbMessage.Text = lbPrintExport.Text;
            }
            else if (eventName == "SpecialProduct")
            {
                lbMessage.Text = lbSpecialProduct.Text;
            }
            else
            {
                lbMessage.Text = eventName;
            }
        }

        private void ucTBWIEmp_Enter(object sender, EventArgs e)
        {
            //ShowKeyBoard((UCTextBoxWithIcon)sender);
            switch (keyboardType)
            {
                case 1:
                    panel1.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad.Visible = true;
                    this.ucKeyboard1.Visible = false;
                    this.ucKeypad.ucTBWI = ucTBWIEmpUser;

                    break;
                case 2:
                    panel1.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad.Visible = false;
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.currentInput = ucTBWIEmpUser;
                    break;
                default:
                    panel1.Location = defaultLoginPanelLocation;
                    splitContainer1.SplitterDistance = 768;

                    splitContainer1.Panel2Collapsed = true;
                    this.ucKeypad.Visible = false;
                    this.ucKeyboard1.Visible = false;

                    break;
            }
        }

        private void ShowKeyBoard(UCTextBoxWithIcon ucTBWI)
        {
            switch (keyboardType)
            {
                case 1:
                    panel1.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad.Visible = true;
                    this.ucKeyboard1.Visible = false;
                    this.ucKeypad.ucTBWI = ucTBWI;
                    break;
                case 2:
                    panel1.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad.Visible = false;
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.currentInput = ucTBWI;
                    break;
                default:
                    panel1.Location = defaultLoginPanelLocation;
                    splitContainer1.SplitterDistance = 768;

                    splitContainer1.Panel2Collapsed = true;
                    this.ucKeypad.Visible = false;
                    this.ucKeyboard1.Visible = false;
                    break;
            }
        }

        private void ucTBWIEmp_Leave(object sender, EventArgs e)
        {
            LeaveKeyBoard();
        }

        private void LeaveKeyBoard()
        {
            panel1.Location = defaultLoginPanelLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeypad.Visible = false;
            this.ucKeyboard1.Visible = false;
        }

        private void ucTBWIEmpUser_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTBWIEmp_Leave(sender, e);
            this.Update();
            ucTBWIEmpPass.Focus();
        }

        private void ucTBWIEmpPass_TextBoxKeydown(object sender, EventArgs e)
        {
            try
            {
                ucTBWIEmp_Leave(sender, e);
                this.Update();
                this.btnOK.PerformClick();
            }
            catch (NetworkConnectionException ex)
            {
                DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
        }

        private void frmUserAuthorize_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        public DialogResult ShowDialog(Form parent)
        {
            try
            {
                frmLoading.closeLoading();
                if (parent.InvokeRequired)
                {
                    AppLog.writeLog("frmUserAuthorize > ShowDialog parent.InvokeRequired");
                    InvokeDelegate d = new InvokeDelegate(ShowDialog);
                    object[] o = new object[] { parent };
                    return (DialogResult)parent.Invoke(d, o);
                }
                else
                {
                    return base.ShowDialog(parent);
                }
            }
            catch (NetworkConnectionException)
            {
                return System.Windows.Forms.DialogResult.Abort;
            }
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            headerChangeText();
        }

        private void ucHeader1_Load(object sender, EventArgs e)
        {

        }

        private void ucTBWIEmpPass_Enter(object sender, EventArgs e)
        {
            switch (keyboardType)
            {
                case 1:
                    panel1.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 470;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad.Visible = true;
                    this.ucKeyboard1.Visible = false;
                    this.ucKeypad.ucTBWI = ucTBWIEmpPass;

                    break;
                case 2:
                    panel1.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad.Visible = false;
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.currentInput = ucTBWIEmpPass;
                    break;
                default:
                    panel1.Location = defaultLoginPanelLocation;
                    splitContainer1.SplitterDistance = 768;

                    splitContainer1.Panel2Collapsed = true;
                    this.ucKeypad.Visible = false;
                    this.ucKeyboard1.Visible = false;

                    break;
            }
        }
    }
}
