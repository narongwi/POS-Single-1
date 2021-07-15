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
    public partial class frmConfirmIDCardPassport : Form
    {
        int keyboardType = 0;
        frmRedeem fRedeem = null;
        private Profile _pfKeyBoard;


        public frmConfirmIDCardPassport()
        {
            InitializeComponent();
        }

        public frmConfirmIDCardPassport(Profile pfKeyBoard)
        {
            InitializeComponent();
            _pfKeyBoard = pfKeyBoard;
        }

        private void frmConfirmIDCardPassport_Load(object sender, EventArgs e)
        {
            //try
            //{
                AppMessage.fillForm(ProgramConfig.language, this);

                if (_pfKeyBoard.profile == ProfileStatus.NotAuthorize)
                {
                    frmUserAuthorize auth = new frmUserAuthorize("Redeem", _pfKeyBoard.diffUserStatus);
                    auth.function = _pfKeyBoard.functionId;
                    DialogResult auth_res = auth.ShowDialog(this);
                    if (auth_res != DialogResult.Yes)
                    {
                        if (auth_res == System.Windows.Forms.DialogResult.Abort)
                        {
                            DialogResult = DialogResult.Abort;
                        }
                        else
                        {
                            DialogResult = DialogResult.No;
                        }
                        return;
                    }
                }

                Utility.SetBackGroundBrightness(this.Owner, pictureBox1);
                pictureBox1.SendToBack();

                if (this.Owner is frmRedeem)
                {
                    fRedeem = (frmRedeem)this.Owner;
                }

                int.TryParse(ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Login_KeyboradDisplay.parameterCode).ToString(), out keyboardType);

                panel1.Parent = pictureBox1;
                ucTxtIDCard.Select();
            //}
            //catch (NetworkConnectionException net)
            //{
            //    fRedeem.fSale.fSaleProcess.CheckException(net);
            //    this.Dispose();
            //    this.Close();
            //    throw;
            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Owner is frmRedeem)
            {
                StoreResult res = fRedeem.fSale.fSaleProcess.CheckCustIDCard(ucTxtIDCard.Text);
                if (res.response.next)
                {
                    res = fRedeem.fSale.fSaleProcess.SaveRedeemIDCard();
                    if (res.response.next)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.Yes;
                        Program.control.CloseForm("frmConfirmIDCardPassport");
                    }
                }
            }

            //DialogResult = System.Windows.Forms.DialogResult.Yes;
            //Program.control.CloseForm("frmConfirmIDCardPassport");
        }

        private void ucTextBoxWithIcon1_Enter(object sender, EventArgs e)
        {

            //if (_pfKeyBoard.policy == PolicyStatus.Work)
            //{
                switch (keyboardType)
                {
                    case 1:
                        this.ucKeypad1.Visible = true;
                        this.ucKeyboard1.Visible = false;
                        splitContainer1.SplitterDistance = 470;

                        splitContainer1.Panel2Collapsed = false;
                        this.ucKeypad1.Visible = true;
                        this.ucKeyboard1.Visible = false;
                        this.ucKeypad1.ucTBWI = ucTxtIDCard;
                        break;
                    case 2:
                        this.ucKeypad1.Visible = false;
                        this.ucKeyboard1.Visible = true;
                        splitContainer1.SplitterDistance = 470;

                        splitContainer1.Panel2Collapsed = false;
                        this.ucKeypad1.Visible = false;
                        this.ucKeyboard1.Visible = true;
                        this.ucKeyboard1.currentInput = ucTxtIDCard;
                        break;
                    default:
                        splitContainer1.SplitterDistance = 768;
                        splitContainer1.Panel2Collapsed = true;
                        this.ucKeypad1.Visible = false;
                        this.ucKeyboard1.Visible = false;
                        break;
                }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            this.Dispose();
            //if (this.Owner is frmRedeem)
            //{
            //    frmRedeem frmRD = (frmRedeem)this.Owner;
            //    frmRD.CloseFormRedeemAndCustRedeem();
            //}
            //Program.control.CloseForm("frmConfirmIDCardPassport");
        }

        private void ucTextBoxWithIcon1_TextBoxKeydown(object sender, EventArgs e)
        {
            this.Update();
            btnOK.PerformClick();
        }
    }
}
