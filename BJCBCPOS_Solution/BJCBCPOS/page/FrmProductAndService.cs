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
using BJCBCPOS.page;

namespace BJCBCPOS
{
    public partial class frmProductAndService : Form
    {
        private bool IsPaint = false;
        ProductAndServiceProcess process = new ProductAndServiceProcess();

        public frmProductAndService()
        {
            InitializeComponent();
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmProductAndService_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel2.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel2.Size.Height / 2);
                panel2.Location = new Point(x, y);

                this.Location = this.Owner.Location;
            }
        }

        private void btnService_1_Click(object sender, EventArgs e)
        {
            SubOtherService1 fsub = new SubOtherService1();
            fsub.ShowDialog();
        }

        private void ucButtonPayment1_ButtonClick(object sender, EventArgs e)
        {
            //#79
            Profile check = ProgramConfig.getProfile(FunctionID.Deposit_CheckAuthorize);
            if (!Utility.CheckAuthPass(this, check, "รับเงินมัดจำ"))
            {
                return;
            }

            var res = process.checkOpenDay(FunctionID.Deposit_CheckOpenDayofTillStatus);
            if (res.response.next)
            {
                //res = process.getRunning(FunctionID.Deposit_GetRunning, RunningReceiptID.SaleRef);
                //if (res.response.next)
                //{
                //    ProgramConfig.saleRefNo = res.otherData.Rows[0]["ReferenceNo"].ToString();
                //    ProgramConfig.saleRefNoIni = res.otherData.Rows[0]["ReferenceNoINI"].ToString();
                ProgramConfig.salePageState = 0;
                Program.control.ShowForm("frmDeposit");
                Program.control.CloseForm("frmSale");
                this.Dispose();
                //}
            }

        }

        private void ucButtonPayment3_ButtonClick(object sender, EventArgs e)
        {
            //#83
            Profile check = ProgramConfig.getProfile(FunctionID.ReceivePOD_CheckAuthorize);
            if (!Utility.CheckAuthPass(this, check, "รับชำระ POD"))
            {
                return;
            }

            //#689
            var res = process.checkOpenDay(FunctionID.ReceivePOD_CheckOpenDay);
            if (res.response.next)
            {
                ProgramConfig.salePageState = 0;
                Program.control.ShowForm("frmReceivePaymentPOD");
                Program.control.CloseForm("frmSale");
                this.Dispose();
            }
        }

        private void ucButtonPayment4_ButtonClick(object sender, EventArgs e)
        {
            //#89
            Profile check = ProgramConfig.getProfile(FunctionID.CreditSale_CheckAuthorize);
            if (!Utility.CheckAuthPass(this, check, "รับชำระเงินขายเชื่อ"))
            {
                return;
            }

            //#715
            var res = process.checkOpenDay(FunctionID.CreditSale_CheckOpenDay);
            if (res.response.next)
            {
                ProgramConfig.salePageState = 0;
                Program.control.ShowForm("SubOtherService1");
                Program.control.CloseForm("frmSale");
                this.Dispose();
            }
        }

        private void btnService_5_Click(object sender, EventArgs e)
        {
            new frmParcelService().Show();
        }
    }
}
