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
    public partial class UCItemRedeemDiscountCust : UserControl
    {

        [Category("Action")]
        [Description("Occurs when click Y/N button")]
        [Browsable(true)]
        public event EventHandler ClickYesNoButton;

        public string RedeemCode { get; set; }

        public UCItemRedeemDiscountCust()
        {
            InitializeComponent();
        }

        private void btnYesNo_Click(object sender, EventArgs e)
        {
            if (ClickYesNoButton != null)
            {
                if (btnYesNo.Text == "Y")
                {
                    btnYesNo.Text = "N";
                    btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountDisable;
                    btnYesNo.ForeColor = Color.Black;
                }
                else
                {
                    btnYesNo.Text = "Y";
                    btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountEnable;
                    btnYesNo.ForeColor = Color.White;
                }
                ClickYesNoButton(this, e);
            }
        }

        private void UCItemRedeemDiscount_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }
    }
}
