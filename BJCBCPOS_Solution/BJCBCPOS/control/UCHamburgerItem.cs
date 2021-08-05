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
    public partial class UCHamburgerItem : UserControl
    {
        [Category("Action")]
        [Description("Occurs when the menu item is clicked.")]
        [Browsable(true)]
        public event EventHandler HambergerItemClick;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MenuIdHamberger MenuID { get; set; }


        public UCHamburgerItem()
        {
            InitializeComponent();
        }

        private void UCHambergerItem_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void UCHambergerItem_Click(object sender, EventArgs e)
        {
            if (HambergerItemClick != null)
            {
                HambergerItemClick(this, e);
            }
        }

    }
}
