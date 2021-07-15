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
    public partial class UCItem : UserControl
    {
        public UCItem()
        {
            InitializeComponent();
        }

        [Category("Custom Property")]
        [Description("Text display label in dropdown")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Label1Text
        {
            get { return lb_ProductCode.Text; }
            set
            {
                lb_ProductCode.Text = value;
            }
        }

        [Category("Action")]
        [Description("Occurs when the member is clicked.")]
        [Browsable(true)]
        public event EventHandler ItemClick;

        private void UCItem_Click(object sender, EventArgs e)
        {
            UCItem_Click(e);
        }

        private void UCItem_Click(EventArgs e)
        {
            if (ItemClick != null)
            {
                this.BackColor = Color.FromArgb(184, 251, 207);
                ItemClick(this, e);
            }
        }

        private void UCItem_Load(object sender, EventArgs e)
        {
            AppMessage.fillControlsFont(ProgramConfig.language, this, new List<string>() { lb_Amount.Name, lb_ProductCode.Name });
        }

        private void label1_Click(object sender, EventArgs e)
        {
            UCItem_Click(e);
        }

        private void lb_ProductName_Click(object sender, EventArgs e)
        {
            UCItem_Click(e);
        }

        private void lb_Current_Click(object sender, EventArgs e)
        {
            UCItem_Click(e);
        }
    }
}
