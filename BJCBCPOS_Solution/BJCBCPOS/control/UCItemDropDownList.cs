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
    public partial class UCItemDropDownList : UserControl
    {
        public UCItemDropDownList()
        {
            InitializeComponent();
        }

        [Category("Action")]
        [Description("Occurs when the dropdown is clicked.")]
        [Browsable(true)]
        public event EventHandler UCItemDropDownListClick;

        private void UCItemDropDownList_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            AppMessage.fillForm(ProgramConfig.language, this);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        private void UCItemDropDownList_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        private void lineShape1_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        public void DropDownClick(object sender, EventArgs e)
        {
            if (UCItemDropDownListClick != null)
            {
                UCItemDropDownListClick(this, e);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }
    }
}
