using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS
{
    public partial class frmSubMenu : Form
    {
        public frmSubMenu()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReturnFromReceipt_Click(object sender, EventArgs e)
        {
            Program.control.ShowForm("frmReturnFromInvoice");
        }

        private void btnReturnFromProduct_Click(object sender, EventArgs e)
        {


            Program.control.ShowForm("frmReceivePaymentPOD");
        }
    }
}
