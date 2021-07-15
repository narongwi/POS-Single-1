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
    
    public partial class UCListPayment : UserControl
    {
        public string paymentType = "";
        public string paymentAmt = "";

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbNoText
        {
            get { return UCLP_lbNo.Text; }
            set
            {
                UCLP_lbNo.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbPaymentTypeText
        {
            get { return UCLP_lbPaymentType.Text; }
            set
            {
                UCLP_lbPaymentType.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbAmountText
        {
            get { return UCLP_lbAmount.Text; }
            set
            {
                UCLP_lbAmount.Text = value;
            }
        }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LoadFromTable loadFromTable
        {
            get;
            set;
        }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PMCode
        {
            get;
            set;
        }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PMNumber
        {
            get;
            set;
        }


        public UCListPayment()
        {
            InitializeComponent();
        }

        [Category("Action")]
        [Description("Occurs when delete button is clicked")]
        [Browsable(true)]
        public event EventHandler DeleteClick;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //var parent = this.Parent;
            ProgramConfig.paymentType = UCLP_label1.Text;
            ProgramConfig.paymentAmt = UCLP_lbAmount.Text;

            string a = lbPaymentTypeText;
            string b = lbAmountText;
            //parent.Controls.Remove(this);
            if (DeleteClick != null)
            {
                DeleteClick(this, e);
            }
        }

        private void UCListPayment_Load(object sender, EventArgs e)
        {
            //AppMessage.fillForm(ProgramConfig.language, this.Name, this);
            this.Dock = DockStyle.Top;
        }

        private void UCLP_lbNo_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(UCLP_lbNo);
        }

        private void UCLP_lbPaymentType_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(UCLP_lbPaymentType);
        }

        private void UCLP_lbAmount_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(UCLP_lbAmount);
        }


    }
}
