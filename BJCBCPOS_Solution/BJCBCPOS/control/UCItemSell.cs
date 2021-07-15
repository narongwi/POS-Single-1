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
    public partial class UCItemSell : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemSell()
        {
            InitializeComponent();
        }

        public UCItemSell(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemSell_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;

            lbNo.LostFocus += lbNo_LostFocus;
            lbUC_ProductCode.LostFocus += lbProductCode_LostFocus;
            lbUC_Qty.LostFocus += lbQty_LostFocus;
            lbUC_Price.LostFocus += lbPrice_LostFocus;
            lbUC_Discount.LostFocus += lbDiscount_LostFocus;
            lbUC_TotalPrice.LostFocus += lbTotalPrice_LostFocus;
            lbProductName.LostFocus += lbProductName_LostFocus;

            AppMessage.fillControlsFont(ProgramConfig.language, this, GetListIgnoreFont_pn_item_sell());
        }

        private List<string> GetListIgnoreFont_pn_item_sell()
        {
            List<string> lst = new List<string>();
            lst.Add("lbNo");
            lst.Add("lbUC_ProductCode");
            lst.Add("lbUC_Qty");
            lst.Add("lbUC_Price");
            lst.Add("lbUC_Discount");
            lst.Add("lbUC_TotalPrice");
            return lst;
        }

        public static bool operator ==(UCItemSell x, UCItemSell y)
        {
            return x.lbNoText == y.lbNoText;
        }

        public static bool operator !=(UCItemSell x, UCItemSell y)
        {
            return x.lbNoText != y.lbNoText;
        }

        public static void LostFocusItem(UCItemSell ucIS)
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

        private void UCGridViewItemSell_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
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

        private void UCGridViewItemSell_LostFocus(EventArgs e)
        {
            //if (lastItemClick != null)
            //{
            //    lastItemClick.BackColor = Color.White;
            //}
            //isLostFromUC = false;
        }

        private void UCItemSell_Leave(object sender, EventArgs e)
        {
            //if (this.FindForm().ActiveControl is UCTextBoxWithIcon)
            //{
            // this.BackColor = Color.FromArgb(184, 251, 207);
            //}
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
            lbNo.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbProductCode_Click(object sender, EventArgs e)
        {
            lbUC_ProductCode.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbQty_Click(object sender, EventArgs e)
        {
            lbUC_Qty.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbPrice_Click(object sender, EventArgs e)
        {
            lbUC_Price.Focus();
            UCGridViewItemSell_Click(e);
        }

        private void lbDiscount_Click(object sender, EventArgs e)
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
            lbProductName.Focus();
            UCGridViewItemSell_Click(e);
        }


        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UPCPriceDB
        {
            get;
            set;
        }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PR_TYPE
        {
            get;
            set;
        }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IsFreshFoodNRTC
        {
            get;
            set;
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
            get { return lbProductName.Text; }
            set
            {
                lbProductName.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbSymbolText
        {
            get { return lbSymbol.Text; }
            set
            {
                lbSymbol.Text = value.Trim();
                if (lbSymbol.Text != "")
                {
                    iconSymbol.Visible = true;
                    switch (lbSymbol.Text)
                    {
                        case "+" :
                            iconSymbol.Image = Properties.Resources.icon_star_special;
                            break;
                        case "-" :
                            iconSymbol.Image = Properties.Resources.icon_CannotScan;
                            break;
                        case "/" :
                            iconSymbol.Image = Properties.Resources.icon_NoBarcode;
                            break;
                        default:
                            iconSymbol.Image = null;
                            iconSymbol.Visible = false;
                            break;
                    }
                }
                else
                {
                    iconSymbol.Visible = false;
                }
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string STV
        {
            get;
            set;
        }

    }
}
