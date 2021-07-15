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
    public partial class UCItemCurrency : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemCurrency()
        {
            InitializeComponent();
        }

        public UCItemCurrency(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

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

        private void UCItemCurrency_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }
    }
}
