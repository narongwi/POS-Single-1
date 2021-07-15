using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class frmMoneyBagInput : Form
    {
        private bool IsPaint = false; 
        private string _type = "";
        Point defaultLocation = new Point(247, 254);
        Point newLocation = new Point(247, 100);
        public string moneyBag = "";
        private CashOutProcess process = new CashOutProcess();
        
        public frmMoneyBagInput()
        {
            InitializeComponent();
        }

        //public frmMoneyBagInput(string type)
        //{
        //    InitializeComponent();
        //    _type = type;
        //}

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
        //    {
        //        e.Graphics.FillRectangle(brush, e.ClipRectangle);
        //    }
        //}

        private void frmMoneyBagInput_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this);
            //label1.Text = _type;

            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);

                this.Location = this.Owner.Location;
            }
            ucTextBoxWithIcon1.Select();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Profile chkMoneyBag = ProgramConfig.getProfile(FunctionID.CashOut_Sale_CheckInputMoneyBag);
            if (string.IsNullOrEmpty(ucTextBoxWithIcon1.Text))
            {
                string responseMessage = ProgramConfig.message.get("frmMoneyBagInput", "SpecifyNumber").message;
                string helpMessage = ProgramConfig.message.get("frmMoneyBagInput", "SpecifyNumber").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //frmNotify dialog = new frmNotify(ResponseCode.Error, "กรุณาระบุเลขที่ซองเงิน");
                dialog.ShowDialog();
                clearAndFocus();
            }
            else
            {
                StoreResult chkMoneyBag = process.checkMoneyBag(ucTextBoxWithIcon1.Text);
                if (chkMoneyBag.response.next)
                {
                    if (chkMoneyBag.response == ResponseCode.Information)
                    {
                        frmNotify dialog = new frmNotify(chkMoneyBag);
                        dialog.ShowDialog();
                    }


                    if (ucTextBoxWithIcon1.Text.Length > 4)
                    {
                        string responseMessage = ProgramConfig.message.get("frmMoneyBagInput", "NumberNotExceed4Digit").message;
                        string helpMessage = ProgramConfig.message.get("frmMoneyBagInput", "NumberNotExceed4Digit").help;
                        frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                        //frmNotify dialog = new frmNotify(ResponseCode.Error, "กรุณาระบุเลขที่ซองเงินไม่เกิน 4 หลัก");
                        dialog.ShowDialog();
                        clearAndFocus();
                    }
                    else
                    {
                        if (ucTextBoxWithIcon1.Text.Length < 4 && ucTextBoxWithIcon1.Text.Length != 0)
                        {
                            ucTextBoxWithIcon1.Text = ucTextBoxWithIcon1.Text.PadLeft(4, '0');
                        }

                        int num;
                        if (int.TryParse(ucTextBoxWithIcon1.Text, out num))
                        {
                            moneyBag = ucTextBoxWithIcon1.Text;
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                        }
                        else
                        {
                            string responseMessage = ProgramConfig.message.get("frmMoneyBagInput", "AllowOnlyNumber").message;
                            string helpMessage = ProgramConfig.message.get("frmMoneyBagInput", "AllowOnlyNumber").help;
                            frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                            //frmNotify dialog = new frmNotify(ResponseCode.Error, "กรุณาระบุเลขที่ซองเงินเฉพาะตัวเลขเท่านั้น");
                            dialog.ShowDialog();
                            clearAndFocus();
                        }
                    }
                }
                else
                {
                    frmNotify dialog = new frmNotify(chkMoneyBag);
                    dialog.ShowDialog();
                    clearAndFocus();
                }
            }
        }

        public void clearAndFocus()
        {
            ucTextBoxWithIcon1.Text = "";
            ucTextBoxWithIcon1.Focus();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            panel1.Location = newLocation;
            splitContainer1.SplitterDistance = 500;

            splitContainer1.Panel2Collapsed = false;
            ucKeyboard1.currentInput = ucTextBoxWithIcon1;
            ucKeyboard1.Visible = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ucKeyboard1.Visible = false;
            panel1.Location = defaultLocation;
        }

        private void ucTextBoxWithIcon1_TextBoxKeydown(object sender, EventArgs e)
        {
            btnOk.PerformClick();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            //frmNotify dialog = new frmNotify(ResponseCode.Warning, "ต้องการยกเลิกส่งเงินและกลับไปหน้าเมนูหลักใช่หรือไม่");
            //if (dialog.ShowDialog(this) == DialogResult.Yes)
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //    this.Dispose();
            //}
        }
    }
}
