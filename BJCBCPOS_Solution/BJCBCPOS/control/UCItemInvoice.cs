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
    public partial class UCItemInvoice : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemInvoice()
        {
            InitializeComponent();
        }

        public UCItemInvoice(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemSell_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;

            lbUC_No.LostFocus += lbNo_LostFocus;
            lbUC_ReceiptNo.LostFocus += lbReceiptNo_LostFocus;
            lbUC_Qty.LostFocus += lbQty_LostFocus;
            lbUC_Cashier.LostFocus += lbPrice_LostFocus;
            lbUC_ReturnPrice.LostFocus += lbDiscount_LostFocus;
            lbUC_TotalDisc.LostFocus += lbTotalDisc_LostFocus;
            lbUC_Date.LostFocus += lbProductName_LostFocus;
        }

        private void lbNo_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbReceiptNo_LostFocus(object sender, EventArgs e)
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

        private void lbTotalDisc_LostFocus(object sender, EventArgs e)
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

        private void UCItemSell_Click(object sender, EventArgs e)
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

        private void lbReceiptNo_Click(object sender, EventArgs e)
        {
            lbUC_ReceiptNo.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbQty_Click(object sender, EventArgs e)
        {
            lbUC_Qty.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbPrice_Click(object sender, EventArgs e)
        {
            lbUC_Cashier.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbDiscount_Click(object sender, EventArgs e)
        {
            lbUC_ReturnPrice.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbTotalDisc_Click(object sender, EventArgs e)
        {
            lbUC_TotalDisc.Focus();
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
        public string lbReceiptNoText
        {
            get { return lbUC_ReceiptNo.Text; }
            set
            {
                lbUC_ReceiptNo.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbQtyText
        {
            get { return lbUC_Qty.Text; }
            set
            {
                lbUC_Qty.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbPriceText
        {
            get { return lbUC_Cashier.Text; }
            set
            {
                lbUC_Cashier.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbDiscountText
        {
            get { return lbUC_ReturnPrice.Text; }
            set
            {
                lbUC_ReturnPrice.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbTotalDiscText
        {
            get { return lbUC_TotalDisc.Text; }
            set
            {
                lbUC_TotalDisc.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbDateText
        {
            get { return lbUC_Date.Text; }
            set
            {
                lbUC_Date.Text = value;
            }
        }

        private void lbDate_Click(object sender, EventArgs e)
        {
            lbUC_Date.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbTotalDisc_Click_1(object sender, EventArgs e)
        {
            lbUC_TotalDisc.Focus();
            UCGridViewItemSell_Click(e);
        }
    }
}
