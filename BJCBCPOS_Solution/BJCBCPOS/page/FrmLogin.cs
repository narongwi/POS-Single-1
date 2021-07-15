using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Process;
using BJCBCPOS_Model;
using System.Threading;

namespace BJCBCPOS
{
    public partial class frmLogin : Form
    {
        private StartProcess process = new StartProcess();
        private int keyboardType = 0;
        public UCTextBoxWithIcon ucTBWI { get; set; }
        private Point defaultLoginPanelLocation = new Point(344, 274);
        private Point newloginPanelLocation = new Point(344, 50);
        private frmNotify notify;

        public frmLogin()
        {
            InitializeComponent();
            this.ucHeader1.showMainMenu = false;
            ucTextBoxWithIcon1.EnabledUC = true;
            ucTextBoxWithIcon2.EnabledUC = true;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            picLogo.Image = Utility.GetLogoImage();
            ucFooter1.lbFunction.Text = FunctionID.Login_PopupLoginScreen.formatValue;

            lbTxtStoreId.Text = ProgramConfig.storeCode + "-" + ProgramConfig.storeName;
            lbTxtServerName.Text = ProgramConfig.serverName;
            lbTxtTillNo.Text = "No. " + ProgramConfig.tillNo;

            // get keyboard type from pos config
            int.TryParse(ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Login_KeyboradDisplay.parameterCode).ToString(), out keyboardType);
            ucTextBoxWithIcon1.Select();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ProgramConfig.printInvoiceType = PrintInvoiceType.RELATECUSTOMER;
            frmLoading.showLoading();
            ProcessResult result = process.checkLogin(ucTextBoxWithIcon1.Text, ucTextBoxWithIcon2.Text);
            frmLoading.closeLoading();
            if (result.response.next)
            {
                if (result.response == ResponseCode.Information)
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                }

                if (result.notify != null)
                {
                    foreach (NotifyMessage message in result.notify)
                    {
                        notify = new frmNotify(message.response, message.message, message.help);
                        notify.ShowDialog(this);
                    }
                }

                if (result.needNextProcess)
                {
                    // password expired show message and go to change password page
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);

                    frmChangePassword chgPass = new frmChangePassword();
                    if (chgPass.ShowDialog(this) == DialogResult.OK)
                    {
                        ucTextBoxWithIcon1.Text = "";
                        ucTextBoxWithIcon2.Text = "";
                        ucTextBoxWithIcon1.Focus();

                        ProgramConfig.userId = string.Empty;
                        ProgramConfig.cashierName = string.Empty;
                        ProgramConfig.password = string.Empty;
                        ProgramConfig.cashireAuthorizeResult = null;
                    }
                }
                else
                {
                    result = process.checkLoginPolicy();
                    if (result.needNextProcess)
                    {
                        frmUserAuthorize author = new frmUserAuthorize("Login", "2");
                        DialogResult res = author.ShowDialog(this);
                        if (res != DialogResult.Yes)
                        {
                            return;
                        }
                    }

                    //AutoVoid
                    frmLoading.showLoading();
                    result = process.autoVoid("N/A", "N/A", ProgramConfig.abbNo);
                    frmLoading.closeLoading();
                    if (result.response.next)
                    {
                        if (result.notify != null)
                        {
                            foreach (NotifyMessage message in result.notify)
                            {
                                notify = new frmNotify(message.response, message.message, message.help);
                                notify.ShowDialog(this);
                            }
                        }
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                        return;
                    }

                    StoreResult reslt = process.CheckLastReceiptCredPay();
                    if (!reslt.response.next)
                    {
                        Utility.AlertMessage(reslt);
                        return;
                    }

                    frmLoading.showLoading();
                    result = process.checkTerminal();
                    frmLoading.closeLoading();
                    if (result.response.next)
                    {
                        if (result.notify != null)
                        {
                            foreach (NotifyMessage message in result.notify)
                            {
                                notify = new frmNotify(message.response, message.message, message.help);
                                notify.ShowDialog(this);
                            }
                        }

                        if (result.response == ResponseCode.Information)
                        {
                            notify = new frmNotify(result);
                            notify.ShowDialog(this);
                        }

                        

                        // check hardware
                        List<string> lstMessageChkHW = new List<string>();
                        List<string> lstHelpMessageChkHW = new List<string>();

                        Profile profile = ProgramConfig.getProfile(FunctionID.Login_CheckHardware_Drawer);
                        if (profile.policy == PolicyStatus.Work)
                        {
                            ProgramConfig.hasDrawer = Hardware.checkCashDrawer();
                            if (!ProgramConfig.hasDrawer)
                            {
                                string responseMessage = ProgramConfig.message.get("frmLogin", "CashDrawerNotFound").message;
                                string helpMessage = ProgramConfig.message.get("frmLogin", "CashDrawerNotFound").help;

                                lstMessageChkHW.Add(responseMessage);
                                lstHelpMessageChkHW.Add(helpMessage);

                                //notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                                ////notify = new frmNotify(ResponseCode.Information, "ไม่พบ cash drawer");
                                //notify.ShowDialog(this);
                            }
                        }
                        profile = ProgramConfig.getProfile(FunctionID.Login_CheckHardware_PrinterABB);
                        if (profile.policy == PolicyStatus.Work)
                        {
                            ProgramConfig.hasPrinter = Hardware.checkPrinter();
                            if (!ProgramConfig.hasPrinter)
                            {
                                string responseMessage = ProgramConfig.message.get("frmLogin", "PrinterABBNotFound").message;
                                string helpMessage = ProgramConfig.message.get("frmLogin", "PrinterABBNotFound").help;

                                lstMessageChkHW.Add(responseMessage);
                                lstHelpMessageChkHW.Add(helpMessage);

                                //notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                                ////notify = new frmNotify(ResponseCode.Information, "ไม่พบ printer");
                                //notify.ShowDialog(this);
                            }
                        }

                        if (lstMessageChkHW.Count > 0)
                        {
                            notify = new frmNotify(ResponseCode.Information, string.Join("\n", lstMessageChkHW.ToList()), string.Join("\n", lstHelpMessageChkHW.ToList()));
                            notify.ShowDialog(this);
                        }

                        // check alert message
                        profile = ProgramConfig.getProfile(FunctionID.Login_CashierMessage_Enabled);
                        if (profile.found || profile.policy == PolicyStatus.Work)
                        {
                            ProgramConfig.enableCashierMessage = true;
                        }
                        else
                        {
                            ProgramConfig.enableCashierMessage = false;
                        }

                        Program.control.ShowForm("frmMainMenu");
                        Program.control.CloseForm("frmLogin");
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog(this);
                    }
                }
                ProgramConfig.cashierCode = ProgramConfig.userId + " - " + ProgramConfig.cashierName;
            }
            else
            {
                notify = new frmNotify(result);
                notify.ShowDialog(this);
            }

            if (this.Visible)
            {
                this.Focus();
                if (ucTextBoxWithIcon1.Focused || ucTextBoxWithIcon1.TextBox.Focused)
                {
                    ucTextBoxWithIcon1_Enter(null, null);
                }
                else if (ucTextBoxWithIcon2.Focused || ucTextBoxWithIcon2.TextBox.Focused)
                {
                    ucTextBoxWithIcon2_Enter(null, null);
                }
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void ucTextBoxWithIcon1_Enter(object sender, EventArgs e)
        {
            switch (keyboardType)
            {
                case 1:
                    picLogo.Visible = false;
                    pnfrmLogin.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 470;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad1.Visible = true;
                    this.ucKeyboard1.Visible = false;
                    this.ucKeypad1.ucTBWI = ucTextBoxWithIcon1;

                    break;
                case 2:
                    picLogo.Visible = false;
                    pnfrmLogin.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad1.Visible = false;
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.currentInput = ucTextBoxWithIcon1;
                    break;
                default:
                    picLogo.Visible = true;
                    pnfrmLogin.Location = defaultLoginPanelLocation;
                    splitContainer1.SplitterDistance = 768;

                    splitContainer1.Panel2Collapsed = true;
                    this.ucKeypad1.Visible = false;
                    this.ucKeyboard1.Visible = false;

                    break;
            }
        }

        private void ucTextBoxWithIcon2_Enter(object sender, EventArgs e)
        {
            switch (keyboardType)
            {
                case 1:
                    picLogo.Visible = false;
                    pnfrmLogin.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 470;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad1.Visible = true;
                    this.ucKeyboard1.Visible = false;
                    this.ucKeypad1.ucTBWI = ucTextBoxWithIcon2;

                    break;
                case 2:
                    picLogo.Visible = false;
                    pnfrmLogin.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad1.Visible = false;
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.currentInput = ucTextBoxWithIcon2;
                    break;
                default:
                    picLogo.Visible = true;
                    pnfrmLogin.Location = defaultLoginPanelLocation;
                    splitContainer1.SplitterDistance = 768;

                    splitContainer1.Panel2Collapsed = true;
                    this.ucKeypad1.Visible = false;
                    this.ucKeyboard1.Visible = false;

                    break;
            }
        }

        private void ucTextBoxWithIcon1_Leave(object sender, EventArgs e)
        {
            picLogo.Visible = true;
            pnfrmLogin.Location = defaultLoginPanelLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeypad1.Visible = false;
            this.ucKeyboard1.Visible = false;
        }

        private void ucTextBoxWithIcon2_Leave(object sender, EventArgs e)
        {
            picLogo.Visible = true;
            pnfrmLogin.Location = defaultLoginPanelLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeypad1.Visible = false;
            this.ucKeyboard1.Visible = false;
        }

        private void ucKeyboard1_HideKeyboardClick(object sender, EventArgs e)
        {
            picLogo.Visible = true;
            pnfrmLogin.Location = defaultLoginPanelLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeypad1.Visible = false;
            this.ucKeyboard1.Visible = false;
        }

        private void ucKeyboard1_NumPadClick(object sender, EventArgs e)
        {
            this.ucKeypad1.ucTBWI = (UCTextBoxWithIcon)this.ucKeyboard1.currentInput;
            this.ucKeypad1.ucTBWI.TextBox.Focus();
            this.ucKeyboard1.Visible = false;
            this.ucKeypad1.Visible = true;
        }

        private void ucHeader1_LogoutClick(object sender, EventArgs e)
        {
            string responseMessage = ProgramConfig.message.get("frmLogin", "ConfirmCloseProgram").message;
            string helpMessage = ProgramConfig.message.get("frmLogin", "ConfirmCloseProgram").help;
            frmNotify dialog = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);

            //frmNotify dialog = new frmNotify(ResponseCode.Warning, "ยืนยันปิดโปรแกรมใช่หรือไม่");
            DialogResult res = dialog.ShowDialog(this);
            if (res == DialogResult.Yes)
            {
                Program.control.ExitProgram();
            }
        }

        private void ucTextBoxWithIcon1_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTextBoxWithIcon1_Leave(sender, e);
            this.Update();
            this.ucTextBoxWithIcon2.Focus();
        }

        private void ucTextBoxWithIcon2_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTextBoxWithIcon2_Leave(sender, e);
            this.Update();
            this.btnLogin.PerformClick();
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            //var locat = this.ucHeader1.Location;
            //Panel pn = new Panel();
            //pn.Location = new System.Drawing.Point(locat.X, locat.Y + 100);
            //pn.Name = "panel100";
            //pn.Size = new System.Drawing.Size(800, 800);
            //pn.BackColor = Color.LightBlue;
            //pn.BringToFront();
            //splitContainer1.Panel1.Controls.Add(pn);
            //this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //test frmtest = new test();
            //frmtest.ShowDialog();
        }
    }
}
