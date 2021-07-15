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
    public partial class UCItemInvoiceDetail : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemInvoiceDetail()
        {
            InitializeComponent();
        }

        public UCItemInvoiceDetail(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemSell_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;

            lbUC_No.LostFocus += lbNo_LostFocus;
            lbUC_ProductCode.LostFocus += lbProductCode_LostFocus;
            lbUC_Qty.LostFocus += lbQty_LostFocus;
            lbUC_Price.LostFocus += lbPrice_LostFocus;
            lbUC_Discount.LostFocus += lbDiscount_LostFocus;
            lbUC_TotalPrice.LostFocus += lbTotalPrice_LostFocus;
            lbUC_ProductName.LostFocus += lbProductName_LostFocus;
        }

        private void lbNo_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbProductCode_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void lbDiscount_LostFocus(object sender, EventArgs e)
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
            //this.BackColor = Color.Transparent;
            //isLostFromUC = false;
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
                this.BackColor = Color.FromArgb(184, 251, 207);
                UCGridViewItemSellClick(this, e);
            }
        }

        private void lbNo_Click(object sender, EventArgs e)
        {
            lbUC_No.Focus();
            UCGridViewItemSell_Click(e);
        }

        public static void LostFocusItem(UCItemInvoiceDetail ucIS)
        {
            if (Convert.ToInt32(ucIS.lbNoText) % 2 != 0)
            {
                ucIS.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                ucIS.BackColor = Color.White;
            }
        }

        private void lbProductCode_Click(object sender, EventArgs e)
        {
            lbUC_ProductCode.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbType_Click(object sender, EventArgs e)
        {
            lbUC_Qty.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbQty_Click(object sender, EventArgs e)
        {
            lbUC_Price.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbPrice_Click(object sender, EventArgs e)
        {
            lbUC_Discount.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbTotalPrice_Click(object sender, EventArgs e)
        {
            lbUC_TotalPrice.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbProductName_Click(object sender, EventArgs e)
        {
            lbUC_ProductName.Focus();
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
            get { return lbUC_ProductCode.Text; }
            set
            {
                lbUC_ProductCode.Text = value;
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
            get { return lbUC_Price.Text; }
            set
            {
                lbUC_Price.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbDiscountText
        {
            get { return lbUC_Discount.Text; }
            set
            {
                lbUC_Discount.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbTotalPriceText
        {
            get { return lbUC_TotalPrice.Text; }
            set
            {
                lbUC_TotalPrice.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbProductNameText
        {
            get { return lbUC_ProductName.Text; }
            set
            {
                lbUC_ProductName.Text = value;
            }
        }
    }
}
