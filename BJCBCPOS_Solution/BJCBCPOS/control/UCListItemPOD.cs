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
    public partial class UCListItemPOD : UserControl
    {
        public UCListItemPOD()
        {
            InitializeComponent();
        }

        private void UCListItemPOD_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Top;
        }
    }
}
