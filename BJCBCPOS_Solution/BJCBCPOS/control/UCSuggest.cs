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
    public partial class UCSuggest : UserControl
    {
        private int _cntCount;

        public UCSuggest()
        {
            InitializeComponent();
        }

        public UCSuggest(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCSuggest_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
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

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string label1Text
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string label2Text
        {
            get { return label2.Text; }
            set
            {
                label2.Text = value;
            }
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string label3Text
        {
            get { return label3.Text; }
            set
            {
                label3.Text = value;
            }
        }
    }
}
