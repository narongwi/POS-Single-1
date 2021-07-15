using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.page
{
    public partial class frmSubMenuDeposit : Form
    {
        public frmSubMenuDeposit()
        {
            InitializeComponent();
        }

        private void btnReturnFromProduct_Click(object sender, EventArgs e)
        {
            Program.control.ShowForm("frmReceivePaymentPOD"); 
        }

        private void btnReturnFromReceipt_Click(object sender, EventArgs e)
        {

        }
    }
}
