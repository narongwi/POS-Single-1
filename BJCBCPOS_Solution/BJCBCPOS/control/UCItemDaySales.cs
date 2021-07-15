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
    public partial class UCItemDaySales : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemDaySales()
        {
            InitializeComponent();
        }

        public UCItemDaySales(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemSell_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;

            lbNo.LostFocus += AllUC_LostFocus;
            lbTill.LostFocus += AllUC_LostFocus;
            lbCashier.LostFocus += AllUC_LostFocus;
            lbSaleReceiptCnt.LostFocus += AllUC_LostFocus;
            lbSaleAmt.LostFocus += AllUC_LostFocus;
            lbDiscount.LostFocus += AllUC_LostFocus;
            lbReturnAmt.LostFocus += AllUC_LostFocus;
            lbNetSale.LostFocus += AllUC_LostFocus;
            lbCashInDrawer.LostFocus += AllUC_LostFocus;
            lbStatus.LostFocus += AllUC_LostFocus;            
        }
        private void AllUC_LostFocus(object sender, EventArgs e)
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

    }
}
