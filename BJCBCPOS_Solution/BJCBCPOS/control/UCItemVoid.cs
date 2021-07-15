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
    public partial class UCItemVoid : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemVoid()
        {
            InitializeComponent();
        }

        public UCItemVoid(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemVoid_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;

            lbUC_No.LostFocus += lbNo_LostFocus;
            lbUC_Payment.LostFocus += lbProductCode_LostFocus;
            lbUC_Price.LostFocus += lbPrice_LostFocus;
        }

        private void lbNo_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbProductCode_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbQty_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbPrice_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbDiscount_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbTotalPrice_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbProductName_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void UCGridViewItemSell_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void UCGridViewItemSell_LostFocus(EventArgs e)
        {
            this.BackColor = Color.Transparent;
            isLostFromUC = false;
        }

        [Category("Action")]
        [Description("Occurs when the member is clicked.")]
        [Browsable(true)]
        public event EventHandler UCGridViewItemSellClick;

        private void UCItemVoid_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void UCGridViewItemSell_Click(EventArgs e)
        {
            if (UCGridViewItemSellClick != null)
            {
                this.BackColor = Color.FromArgb(235, 248, 240);
                UCGridViewItemSellClick(this, e);
            }
        }

        private void lbNo_Click(object sender, EventArgs e)
        {
            lbUC_No.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbProductCode_Click(object sender, EventArgs e)
        {
            lbUC_Payment.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbPrice_Click(object sender, EventArgs e)
        {
            lbUC_Price.Focus();
            UCGridViewItemSell_Click(e);
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbNoText
        {
            get { return lbUC_No.Text; }
            set
            {
                lbUC_No.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbProductCodeText
        {
            get { return lbUC_Payment.Text; }
            set
            {
                lbUC_Payment.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbPriceText
        {
            get { return lbUC_Price.Text; }
            set
            {
                lbUC_Price.Text = value;
            }
        }
    }
}
