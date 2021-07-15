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
    public partial class UCItemCancelCashOut : UserControl
    {
        private int _cntCount;

        public UCItemCancelCashOut()
        {
            InitializeComponent();
        }

        public UCItemCancelCashOut(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCItemCancelCashOut_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this.FindForm());
            this.Dock = DockStyle.Top;
        }

        [Category("Action")]
        [Description("Occurs when click bin button")]
        [Browsable(true)]
        public event EventHandler DeleteItem;

        [Category("Action")]
        [Description("Occurs when click item")]
        [Browsable(true)]
        public event EventHandler ClickItem;

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
        public string lbEnvelopNumText
        {
            get { return lbEnvelopNum.Text; }
            set
            {
                lbEnvelopNum.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbCurrencyText
        {
            get { return lb_Currency.Text; }
            set
            {
                lb_Currency.Text = value;
            }
        }

        

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbPriceText
        {
            get { return lb_Amt.Text; }
            set
            {
                lb_Amt.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbRefNoText
        {
            get { return lbRefNo.Text; }
            set
            {
                lbRefNo.Text = value;
            }
        }

        private void picBin_Click(object sender, EventArgs e)
        {
            if (this.DeleteItem != null)
            {
                DeleteItem(this, e);
            }
        }

        private void Click_Item(object sender, EventArgs e)
        {
            if (this.ClickItem != null)
            {
                this.BackColor = Color.FromArgb(184, 251, 207);
                ClickItem(this, e);
            }
        }

        internal static void LostFocusItem(UCItemCancelCashOut ucICO)
        {
            if (Convert.ToInt32(ucICO.lbNoText) % 2 != 0)
            {
                ucICO.BackColor = Color.White;
            }
            else
            {
                ucICO.BackColor = Color.FromArgb(225, 225, 225);
            }
        }
    }
}
