using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class UCItemRedeemProduct : UserControl, IRedeem
    {
        [Category("Action")]
        [Description("Occurs when enter from button.")]
        [Browsable(true)]
        public event EventHandler EnterFromButton;

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
                return lbProduct_UsePoint.Text;
            }
            set
            {
                lbProduct_UsePoint.Text = value;
            }
        }

        public string UseCashTxt
        {
            get
            {
                return lbProduct_UseCash.Text;
            }
            set
            {
                lbProduct_UseCash.Text = value;
            }
        }

        public string SumPointTxt
        {
            get
            {
                return lbProduct_SumPoint.Text;
            }
            set
            {
                lbProduct_SumPoint.Text = Convert.ToInt32(Convert.ToDouble(value)).ToString();
            }
        }

        public string SumCashTxt
        {
            get
            {
                return lbProduct_SumCash.Text;
            }
            set
            {
                lbProduct_SumCash.Text = value;
            }
        }

        public string DiscountTxt
        {
            get
            {
                return lbProduct_Discount.Text;
            }
            set
            {
                lbProduct_Discount.Text = value;
            }
        }

        public string ItemNameTxt
        {
            get
            {
                return lbItemName.Text;
            }
            set
            {
                lbItemName.Text = value;
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
                //btnPlus.Visible = value;
                //btnMinus.Visible = value;
            }
        }

        public string RedeemCode
        {
            get;
            set;
        }

        public string RuleID
        {
            get;
            set;
        }

        #endregion

        public UCItemRedeemProduct()
        {
            InitializeComponent();
        }

        private void UCItemRedeem_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void ucTextBoxSmall1_EnterFromButton(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                //SumPointTxt = (Convert.ToInt32(lbProduct_UsePoint.Text) * Convert.ToInt32(ucTextBoxSmall1.Text)).ToString("N0");
                //SumCashTxt = (Convert.ToInt32(lbProduct_UseCash.Text) * Convert.ToInt32(ucTextBoxSmall1.Text)).ToString(ProgramConfig.amountFormatString);
                EnterFromButton(this, e);
            }
        }

        private void ucTextBoxSmall1_LostFocusTextBox(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                EnterFromButton(this, e);
            }
        }

        private void ucTextBoxSmall1_TextBoxTextChange(object sender, EventArgs e)
        {
            lbInputQty.Text = ucTextBoxSmall1.Text;
        }
    }
}
