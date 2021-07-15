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
    public partial class UCItemRedeemCashCust : UserControl, IRedeem
    {
        [Category("Action")]
        [Description("Occurs when button plus and minus click.")]
        [Browsable(true)]
        public event EventHandler ButtonPlusMinusClick;

        [Category("Action")]
        [Description("Occurs when enter from button.")]
        [Browsable(true)]
        public event EventHandler EnterFromButton;

        public string SEQText
        {
            get
            {
                return lb_Seq.Text;
            }
            set
            {
                lb_Seq.Text = value;
            }
        }

        public string QTYText
        {
            get
            {
                int num = 0;
                Int32.TryParse(lb_QTY.Text.Trim(), out num);
                return num.ToString();
            }
            set
            {
                lb_QTY.Text = Convert.ToInt32(Convert.ToDouble(value)).ToString();
            }
        }

        public string UsePointTxt
        {
            get;
            set;
        }

        public string UseCashTxt
        {
            get;
            set;
        }

        public string SumPointTxt
        {
            get
            {
                return lbCashCust_UsePoint.Text;
            }
            set
            {
                lbCashCust_UsePoint.Text = Convert.ToInt32(Convert.ToDouble(value)).ToString();
            }
        }

        public string SumCashTxt
        {
            get;
            set;
        }

        public string DiscountTxt
        {
            get
            {
                return lbCashCust_Discount.Text;
            }
            set
            {
                lbCashCust_Discount.Text = value;
            }
        }

        public string ItemNameTxt
        {
            get;
            set;
        }

        public string LimitTxt
        {
            get;
            set;
        }

        public bool VisibleBtnPlusMinus
        {
            set
            {
                btnPlus.Visible = value;
                btnMinus.Visible = value;
            }
        }

        public string RedeemCode { get; set; }

        public string RuleID { get; set; }

        public UCItemRedeemCashCust()
        {
            InitializeComponent();
        }

        private void UCItemRedeemCashCust_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (ButtonPlusMinusClick != null)
            {
                int num = Convert.ToInt32(QTYText);
                num++;
                lb_QTY.Text = num.ToString();
                ButtonPlusMinusClick(this, e);
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (ButtonPlusMinusClick != null)
            {
                int num = Convert.ToInt32(QTYText);
                num--;
                if (num <= 0)
                {
                    num = 0;
                }
                lb_QTY.Text = num.ToString();
                ButtonPlusMinusClick(this, e);
            }
        }


    }
}
