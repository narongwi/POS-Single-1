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
    public partial class UCItemRedeemDiscount : UserControl, IRedeem
    {

        [Category("Action")]
        [Description("Occurs when click Y/N button")]
        [Browsable(true)]
        public event EventHandler ClickYesNoButton;

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
            get;
            set;
        }

        public string UsePointTxt
        {
            get
            {
                return lbDisc_PointUse.Text;
            }
            set
            {
                lbDisc_PointUse.Text = value;
            }
        }

        public string UseCashTxt
        {
            get;
            set;
        }

        public string SumPointTxt
        {
            get;
            set;
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
                return lbDisc_RateUse.Text;
            }
            set
            {
                lbDisc_RateUse.Text = value;
            }
        }

        public string ItemNameTxt
        {
            get
            {
                return lbName.Text;
            }
            set
            {
                lbName.Text = value;
            }
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
                btnYesNo.Visible = value;
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

        public UCItemRedeemDiscount()
        {
            InitializeComponent();
        }

        private void UCItemRedeemDiscount_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
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
    }
}
