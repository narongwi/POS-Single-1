using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class UCHirePurchase : UserControl
    {
        #region Event

        #endregion

        private SaleProcess saleProcess = new SaleProcess();

        Form _frm;
        frmPayment _frmPayment;
        string _paymentCode;       
        Point LocationDefault = new Point(67, 23);
        Point LocationKB = new Point(67, -30);//new Point(67, -30);

        public UCHirePurchase()
        {
            InitializeComponent();
        }

        public UCHirePurchase(Form frm, string paymentCode)
        {
            InitializeComponent();
            _frm = frm;
            _paymentCode = paymentCode;
        }

        private void UCHirePurchase_Load(object sender, EventArgs e)
        {
            _frmPayment = _frm as frmPayment;
            this.ucInstallment.FocusTxt();
            this.ucInstallment.SetSelection = true;
        }

        private void ucInstallment_EnterFromButton(object sender, EventArgs e)
        {
            ucInstallmentProcess();
            ucContractNumber.FocusTxt();
        }

        private void ucInstallmentProcess()
        {
            double amt = 0;
            if (!double.TryParse(ucInstallment.Text, out amt))
            {
                string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                ucInstallment.Text = _frmPayment.lbTxtBalance.Text;
                ucInstallment.FocusTxt();
                ucInstallment.SetSelection = true;

                return;
            }

            double total = Convert.ToDouble(lbPrPriceAmt.Text) - amt;

            if (!ProgramConfig.payment.getExcessChange(_paymentCode))
            {
                if (total < 0)
                {
                    string responseMessage = ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help;
                    Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                    ucInstallment.Text = _frmPayment.lbTxtBalance.Text;
                    ucInstallment.FocusTxt();
                    ucInstallment.SetSelection = true;

                    return;
                }
            }

            frmLoading.showLoading();
            StoreResult chkMinCash = saleProcess.checkMinCashUnitAmount("CASH", ucInstallment.Text, ProgramConfig.currencyDefault);
            frmLoading.closeLoading();
            if (!chkMinCash.response.next)
            {
                Utility.AlertMessage(chkMinCash.response, chkMinCash.responseMessage, chkMinCash.helpMessage);
                ucInstallment.Focus();
                return;
            }

            lbDownPaymentAmt.Text = total.ToString(ProgramConfig.amountFormatString);

        }

        private void CloseFrom()
        {
            _frmPayment.pictureBox3.SendToBack();
            _frmPayment.DisablePaymentGroup();
            _frmPayment.ucKeypad.ucTBS = null;
            _frmPayment.ucKeyboard1.Visible = false;
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucInstallment.Text.Trim() == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                    Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                    ucInstallment.Text = _frmPayment.lbTxtBalance.Text;
                    ucInstallment.FocusTxt();
                    ucInstallment.SetSelection = true;

                    return;
                }

                if (ucContractNumber.Text.Trim() == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyContractNumber").message;
                    string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyContractNumber").help;
                    Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                    ucContractNumber.FocusTxt();

                    return;
                }

                _frmPayment.SaveTemp(ucInstallment, ucContractNumber, _paymentCode + ucContractNumber.Text, () => CloseFrom());
                _frmPayment.btnPayment_HirePurchase.Enabled = false;
                _frmPayment.btnPayment_HirePurchase.BackgroundImage = Properties.Resources.payment_btm_right_disable;
            }
            catch (NetworkConnectionException net)
            {
                _frmPayment.ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseFrom();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ucContractNumber.Text = "";
            lbDownPaymentAmt.Text = "0.00";
            lbPrPriceAmt.Text = _frmPayment.lbTxtBalance.Text;
            ucInstallment.Text = _frmPayment.lbTxtBalance.Text;
            ucInstallment.FocusTxt();
            ucInstallment.SetSelection = true;
            panel1.Location = LocationDefault;
            _frmPayment.ucKeyboard1.Visible = false;
        }

        private void ucContractNumber_Enter(object sender, EventArgs e)
        {
            _frmPayment.ucKeyboard1.Visible = true;
            _frmPayment.ucKeyboard1.BringToFront();
            _frmPayment.ucKeyboard1.currentInput = ucContractNumber;
            panel1.Location = LocationKB;

            //_frmPayment.SetIndexKeyBoard();
            //var a = _frmPayment.Controls.Find("ucKeyboard1", true);
            //_frmPayment.Controls.SetChildIndex(a[0], 0);
           
            //this.BringToFront();
        }

        private void ucContractNumber_TextBoxLeave(object sender, EventArgs e)
        {
            if (!(this.ActiveControl is Button))
            {
                panel1.Location = LocationDefault;
                _frmPayment.ucKeyboard1.Visible = false;
            }                
        }

        private void ucInstallment_TextBoxLeave(object sender, EventArgs e)
        {
            ucInstallmentProcess(); 
            //if (!(this.ActiveControl is Button))
            //{
                 
            //}                 
        }

        private void lbClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
