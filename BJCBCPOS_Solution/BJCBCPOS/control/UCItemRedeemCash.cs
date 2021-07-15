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
    public partial class UCItemRedeemCash : UserControl, IRedeem
    {
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
                Int32.TryParse(ucTextBoxSmall1.Text.Trim(), out num);
                return num.ToString();
            }
            set
            {
                ucTextBoxSmall1.Text = value;
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
                return  lbCash_UsePoint.Text;
            }
            set
            {

                lbCash_UsePoint.Text = Convert.ToInt32(Convert.ToDouble(value)).ToString();
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
                return lbCash_Discount.Text;
            }
            set
            {
                lbCash_Discount.Text = value;
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

        public string TypeNameTxt
        {
            get
            {
                return lbTypeName.Text;
            }
            set
            {
                lbTypeName.Text = value;
            }
        }

        public bool VisibleBtnPlusMinus
        {
            set
            {
                btnPlus.Visible = value;
                btnMinus.Visible = value;

                if (value)
                {
                    lbInputQty.SendToBack();
                    ucTextBoxSmall1.BringToFront();
                }
                else
                {
                    lbInputQty.BringToFront();
                    ucTextBoxSmall1.SendToBack();
                }
            }
        }

        public string RedeemCode { get; set; }

        public string RuleID { get; set; }

        public UCItemRedeemCash()
        {
            InitializeComponent();
        }

        private void UCItemRedeemCash_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                int num = Convert.ToInt32(QTYText);
                num++;
                ucTextBoxSmall1.Text = num.ToString();
                EnterFromButton(this, e);
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                int num = Convert.ToInt32(QTYText);
                num--;
                if (num <= 0)
                {
                    num = 0;
                }
                ucTextBoxSmall1.Text = num.ToString();
                EnterFromButton(this, e);
            }
        }

        private void ucTextBoxSmall1_EnterFromButton(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
                EnterFromButton(this, e);
        }

        private void ucTextBoxSmall1_LostFocusTextBox(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
                EnterFromButton(this, e);
        }

        private void ucTextBoxSmall1_TextBoxTextChange(object sender, EventArgs e)
        {
            lbInputQty.Text = ucTextBoxSmall1.Text;
        }


    }
}
