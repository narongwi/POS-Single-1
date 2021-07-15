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
    public partial class UCItemCurrency2 : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCItemCurrency2()
        {
            InitializeComponent();
        }

        public UCItemCurrency2(int cntCount)
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

        private void UCItemCurrency2_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }
    }
}
