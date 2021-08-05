using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.OtherServices.Forms {
    public partial class frmParcelService : Form
    {
        public frmParcelService()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new frmParcelKerry().Show();

        }
    }
}
