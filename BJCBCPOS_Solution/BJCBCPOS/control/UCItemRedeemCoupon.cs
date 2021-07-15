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
    public partial class UCItemRedeemCoupon : UserControl, IRedeem
    {
        [Category("Action")]
        [Description("Occurs when enter from button.")]
        [Browsable(true)]
        public event EventHandler EnterFromButton;

        public UCItemRedeemCoupon()
        {
            InitializeComponent();
        }

        #region Properties
        
        

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
            get
            {
               return lbCoupon_UsePoint.Text; 
            }
            set
            {
                lbCoupon_UsePoint.Text = value;
            }
        }

        public string UseCashTxt
        {
            get
            {
                return lbCoupon_CouponVal.Text;
            }
            set
            {
                lbCoupon_CouponVal.Text = value;
            }
        }

        public string SumPointTxt
        {
            get
            {
                return lbCoupon_SumPoint.Text;
            }
            set
            {
                lbCoupon_SumPoint.Text = value;
            }
        }

        public string SumCashTxt
        {
            get
            {
                return lbCoupon_SumCouponVal.Text;
            }
            set
            {
                lbCoupon_SumCouponVal.Text = value;
            }
        }

        public string DiscountTxt
        {
            get;
            set;
        }

        public string ItemNameTxt
        {
            get
            {
                return lbCoupon_ItemName.Text;
            }
            set
            {
                lbCoupon_ItemName.Text = value;
            }
        }

        public string LimitTxt
        {
            get
            {
                return lbLimit.Text;
            }
            set
            {
                lbLimit.Text = value;
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

        public string RedeemCode
        {
            get;
            set;
        }

        public string RuleID { get; set; }

        #endregion

        private void UCItemRedeemCoupon_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void ucTextBoxSmall1_EnterFromButton(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
                EnterFromButton(this, e);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                int num = Convert.ToInt32(ucTextBoxSmall1.Text);
                num++;
                if (num <= Convert.ToInt32(lbLimit.Text))
                {
                    ucTextBoxSmall1.Text = num.ToString();
                    EnterFromButton(this, e);
                }
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                int num = Convert.ToInt32(ucTextBoxSmall1.Text);
                num--;
                if (num <= 0)
                {
                    num = 0;
                }
                ucTextBoxSmall1.Text = num.ToString();
                EnterFromButton(this, e);
            }
        }

        private void ucTextBoxSmall1_TextBoxTextChange(object sender, EventArgs e)
        {
            lbInputQty.Text = ucTextBoxSmall1.Text;
        }



    }
}
