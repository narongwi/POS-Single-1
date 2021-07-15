using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS
{
    public partial class UCConfirmPaymentFXCU : UserControl
    {
        public UCConfirmPaymentFXCU()
        {
            InitializeComponent();
        }

        private void UCConfirmPaymentFXUC_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void Control_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont((Control)sender);
        }
    }
}
