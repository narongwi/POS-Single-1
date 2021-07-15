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
    public partial class UCButtonPayment : UserControl
    {

        [Category("Action")]
        [Description("Occurs when click button.")]
        [Browsable(true)]
        public event EventHandler ButtonClick;

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Template { get; set; }

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PaymentMainCode { get; set; }

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string TextButton
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public UCButtonPayment()
        {
            InitializeComponent();
        }

        private void Item_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(this, e);
            }
        }


    }
}
