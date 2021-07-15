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
    public partial class UCItemRedeemProductAndCouponCust : UserControl
    {
        public UCItemRedeemProductAndCouponCust()
        {
            InitializeComponent();
        }

        [Category("Action")]
        [Description("Occurs when button plus click.")]
        [Browsable(true)]
        public event EventHandler ButtonPlusClick;

        [Category("Action")]
        [Description("Occurs when button minus click.")]
        [Browsable(true)]
        public event EventHandler ButtonMinusClick;

        public string QTY
        {
            get
            {
                int num = 0;
                Int32.TryParse(lb_QTY.Text.Trim(),out num);
                return num.ToString();
            }
            set
            {
                int num = 0;
                Int32.TryParse(value, out num);
                lb_QTY.Text = num.ToString();
            }
        }

        public int SEQInt
        {
            get
            {
                return Convert.ToInt32(String.IsNullOrEmpty(lb_Seq.Text.Trim()) ? "0" : lb_Seq.Text.Trim());
            }
            set
            {
                lb_Seq.Text = value.ToString();
            }
        }

        public string RedeemCode
        {
            get;
            set;
        }


        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (ButtonPlusClick != null)
            {
                int num = Convert.ToInt32(lb_QTY.Text.Trim() == "" ? "0" : lb_QTY.Text.Trim());
                num++;
                if (num <= Convert.ToInt32(lbLimit.Text))
                {
                    lb_QTY.Text = num.ToString();
                    ButtonPlusClick(this, e);
                }

            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (ButtonMinusClick != null)
            {
                int num = Convert.ToInt32(lb_QTY.Text.Trim() == "" ? "0" : lb_QTY.Text.Trim());
                num--;
                if (num <= 0)
                {
                    num = 0;
                }

                lb_QTY.Text = num.ToString();
                ButtonMinusClick(this, e);
            }
        }
    }
}
