using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmHirePurchase : Form
    {
        frmPayment fPayment;
        string _header;
        string _paymentCode;

        public frmHirePurchase()
        {
            InitializeComponent();
        }

        public frmHirePurchase(string header, string paymentCode)
        {
            InitializeComponent();
            _header = header;
            _paymentCode = paymentCode;
        }

        private void frmHirePurchase_Load(object sender, EventArgs e)
        {
            Point pt = this.Owner.Location;
            fPayment = this.Owner as frmPayment;
            this.Location = new Point(pt.X, pt.Y + 43);
            ucInstallment.IsTextChange = true;
            ucContractNumber.IsTextChange = true;

            Utility.SetBackGroundBrightness(fPayment.panelMainPayment, this);
            fPayment.DisablePaymentGroup();
            fPayment.pictureBox3.BringToFront();
            lbPrPriceAmt.Text = fPayment.lbTxtBalance.Text;            
            lbHeader.Text = _header;
            ucInstallment.Text = fPayment.lbTxtBalance.Text;
            ucInstallment.FocusTxt();
            ucInstallment.SetSelection = true;
        }

        private void frmHirePurchase_Leave(object sender, EventArgs e)
        {
            ucInstallment.IsTextChange = true;
            ucContractNumber.IsTextChange = true;
        }

        private void ucContractNumber_Leave(object sender, EventArgs e)
        {
            ucInstallment.IsTextChange = true;
            ucContractNumber.IsTextChange = true;
        }

        private void ucInstallment_Leave(object sender, EventArgs e)
        {
            ucInstallment.IsTextChange = true;
            ucContractNumber.IsTextChange = true;
        }

        private void ucInstallment_TextBoxLeave(object sender, EventArgs e)
        {
            ucInstallment.IsTextChange = true;
            ucContractNumber.IsTextChange = true;
        }

        private void ucContractNumber_TextBoxLeave(object sender, EventArgs e)
        {
            ucInstallment.IsTextChange = true;
            ucContractNumber.IsTextChange = true;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            
        }

        private void ucInstallment_EnterFromButton(object sender, EventArgs e)
        {
            double amt = 0;
            if (!double.TryParse(ucInstallment.Text, out amt))
            {
                string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                ucInstallment.Text = fPayment.lbTxtBalance.Text;
                ucInstallment.FocusTxt();
                ucInstallment.SetSelection = true;

                return;
            }

            double total = Convert.ToDouble(lbPrPriceAmt.Text) - amt;

            if (total < 0)
            {
                string responseMessage = ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").message;
                string helpMessage = ProgramConfig.message.get("frmPayment", "AmtPaidMoreThanPayment").help;
                Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                ucInstallment.Text = fPayment.lbTxtBalance.Text;
                ucInstallment.FocusTxt();
                ucInstallment.SetSelection = true;

                return;
            }

            lbDownPaymentAmt.Text = total.ToString(ProgramConfig.amountFormatString);
            ucContractNumber.FocusTxt();
        }

        private void ucContractNumber_EnterFromButton(object sender, EventArgs e)
        {
            btnAccept.Focus();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (ucInstallment.Text.Trim() == "")
            {           
                string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);

                ucInstallment.Text = fPayment.lbTxtBalance.Text;
                ucInstallment.FocusTxt();
                ucInstallment.SetSelection = true;

                return;
            }

            if (ucContractNumber.Text.Trim() == "")
            {
                //TO DO Set Language
                //string responseMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").message;
                //string helpMessage = ProgramConfig.message.get("frmPayment", "SpecifyAmount").help;
                Utility.AlertMessage(ResponseCode.Error, "Please spicify Contract Number.", "");

                ucContractNumber.FocusTxt();

                return;
            }


            fPayment.SaveTemp(ucInstallment, ucContractNumber, _paymentCode + ucContractNumber.Text, () => CloseFrom());
            fPayment.btnPayment_HirePurchase.Enabled = false;
            fPayment.btnPayment_HirePurchase.BackgroundImage = Properties.Resources.payment_btm_right_disable;
        }

        private void CloseFrom()
        {
            fPayment.pictureBox3.SendToBack();
            fPayment.DisablePaymentGroup();
            fPayment.ucKeypad.ucTBS = null;
            this.Close();
            this.Dispose();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CloseFrom();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }




    }
}
