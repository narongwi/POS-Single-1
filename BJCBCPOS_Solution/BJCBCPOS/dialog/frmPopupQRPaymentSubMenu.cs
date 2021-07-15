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
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace BJCBCPOS
{
    public partial class frmPopupQRPaymentSubMenu : Form
    {
        SaleProcess saleProcess = new SaleProcess();
        frmPayment fPayment;
        string filePNG;
        bool IsPaint = false;

        double _total;

        public QRPaymentOnlineMenu menu;

        public frmPopupQRPaymentSubMenu()
        {
            InitializeComponent();
        }

        public frmPopupQRPaymentSubMenu(double total)
        {
            InitializeComponent();
            this._total = total;
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

        private void btnQR_BscanC_Click(object sender, EventArgs e)
        {
            menu = QRPaymentOnlineMenu.QR_BscanC;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnQR_CscanB_Click(object sender, EventArgs e)
        {
            menu = QRPaymentOnlineMenu.QR_CscanB;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmPopupQRPaymentSubMenu_Load(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        
    }
}
