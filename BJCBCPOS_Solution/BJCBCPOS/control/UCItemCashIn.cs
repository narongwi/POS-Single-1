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
    public partial class UCItemCashIn : UserControl
    {
        public UCItemCashIn()
        {
            InitializeComponent();
        }

        private string _paymentType;

        #region Property
        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool btnDeleteVisible
        {
            get { return btnDelete.Visible; }
            set
            {
                btnDelete.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbNoText
        {
            get { return lbUCNo.Text; }
            set
            {
                lbUCNo.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbChangeText
        {
            get { return lbUCChange.Text; }
            set
            {
                lbUCChange.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbChangeTypeText
        {
            get { return lbUCChangeType.Text; }
            set
            {
                lbUCChangeType.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbCurrencyText
        {
            get { return lbUCCurrency.Text; }
            set
            {
                lbUCCurrency.Text = value;
            }
        }


        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbExchangeRateText
        {
            get { return lbExchangeRate.Text; }
            set
            {
                lbExchangeRate.Text = value;
            }
        }


        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbAmtText
        {
            get { return lbValue.Text; }
            set
            {
                lbValue.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbInputAmt
        {
            get { return UCItemInputAmt.Text; }
            set
            {
                UCItemInputAmt.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbCashType
        {
            get { return UCItemCashType.Text; }
            set
            {
                UCItemCashType.Text = value;
            }
        }

        [Category("Custom Property CashOut")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string paymentType
        {
            get { return this._paymentType; }
            set
            {
                this._paymentType = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbInputDisplayText
        {
            get { return lbInputDisplay.Text; }
            set
            {
                lbInputDisplay.Text = value;
            }
        }

        #endregion

        [Category("Action")]
        [Description("Occurs when delete button is clicked")]
        [Browsable(true)]
        public event EventHandler DeleteClick;

        private void UCItemCashIn_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this.FindForm());
            this.Dock = DockStyle.Top;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var parent = this.Parent;
            parent.Controls.Remove(this);
            if (DeleteClick != null)
            {
                DeleteClick(sender, e);
            }
        }

        private void Control_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont((Control)sender);
        }
    }
}
