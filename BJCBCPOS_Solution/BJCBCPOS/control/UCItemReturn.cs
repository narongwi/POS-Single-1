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
    public partial class UCItemReturn : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Rec
        {
            get;
            set;
        }

        public UCItemReturn()
        {
            InitializeComponent();
        }

        public UCItemReturn(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemSell_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;

            lbNo.LostFocus += lbNo_LostFocus;
            lbProductCode.LostFocus += lbProductCode_LostFocus;
            lbQty.LostFocus += lbQty_LostFocus;
            lbPrice.LostFocus += lbPrice_LostFocus;
            lbReturnPrice.LostFocus += lbDiscount_LostFocus;
            lbTotalPrice.LostFocus += lbTotalPrice_LostFocus;
            lbProductName.LostFocus += lbProductName_LostFocus;
        }

        public static void LostFocusItem(UCItemReturn ucIS)
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
                this.Refresh();
                UCGridViewItemSellClick(this, e);
            }
        }

        private void lbNo_Click(object sender, EventArgs e)
        {
            lbNo.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbProductCode_Click(object sender, EventArgs e)
        {
            lbProductCode.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbQty_Click(object sender, EventArgs e)
        {
            lbQty.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbPrice_Click(object sender, EventArgs e)
        {
            lbPrice.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbDiscount_Click(object sender, EventArgs e)
        {
            lbReturnPrice.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbTotalPrice_Click(object sender, EventArgs e)
        {
            lbTotalPrice.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbProductName_Click(object sender, EventArgs e)
        {
            lbProductName.Focus();
            UCGridViewItemSell_Click(e);
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbNoText
        {
            get { return lbNo.Text; }
            set
            {
                lbNo.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbProductCodeText
        {
            get { return lbProductCode.Text; }
            set
            {
                lbProductCode.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbQtyText
        {
            get { return lbQty.Text; }
            set
            {
                lbQty.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbPriceText
        {
            get { return lbPrice.Text; }
            set
            {
                lbPrice.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbDiscountText
        {
            get { return lbReturnPrice.Text; }
            set
            {
                lbReturnPrice.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbTotalPriceText
        {
            get { return lbTotalPrice.Text; }
            set
            {
                lbTotalPrice.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbProductNameText
        {
            get { return lbProductName.Text; }
            set
            {
                lbProductName.Text = value;
            }
        }
    }
}
