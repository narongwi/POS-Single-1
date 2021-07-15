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
    public partial class UCCoupon : UserControl
    {
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
        public string lbCouponNoText
        {
            get { return lbCouponNo.Text; }
            set
            {
                lbCouponNo.Text = value;
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
        public string lbProductCodeText
        {
            get { return lbProductCode.Text; }
            set
            {
                lbProductCode.Text = value;
            }
        }

        public UCCoupon()
        {
            InitializeComponent();
        }

        [Category("Action")]
        [Description("Occurs when delete button is clicked")]
        [Browsable(true)]
        public event EventHandler DeleteClick;

        [Category("Action")]
        [Description("Occurs when the member is clicked.")]
        [Browsable(true)]
        public event EventHandler UCGridViewItemSellClick;

        private void picBin_Click(object sender, EventArgs e)
        {
            //var parent = this.Parent;
            //parent.Controls.Remove(this);

            if (DeleteClick != null)
            {
                DeleteClick(this, e);
            }
        }

        private void UCCoupon_Load(object sender, EventArgs e)
        {
            //AppMessage.fillForm(ProgramConfig.language, this.Name, this);
            this.Dock = DockStyle.Top;
        }

        private void UCCoupon_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void lbNo_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void lbCouponNo_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void lbCouponValue_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void lbQty_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void lbProductCode_Click(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click(e);
        }

        private void UCGridViewItemSell_Click(EventArgs e)
        {
            if (UCGridViewItemSellClick != null)
            {
                //this.BackColor = Color.FromArgb(235, 248, 240);
                UCGridViewItemSellClick(this, e);
            }
        }
    }
}
