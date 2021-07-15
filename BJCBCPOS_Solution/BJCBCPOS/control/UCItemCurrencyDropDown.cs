using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS;

namespace BJCBCPOS
{
    public partial class UCItemCurrencyDropDown : UserControl
    {
        private Form frmIns;
        public UCItemCurrencyDropDown()
        {
            InitializeComponent();
        }

        public UCItemCurrencyDropDown(Form frm)
        {
            InitializeComponent();
            frmIns = frm;
        }

        private void UCItemCurrencyDropDown_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void UCItemCurrencyDropDown_Click(object sender, EventArgs e)
        {
            Clickz();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Clickz();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Clickz();
        }

        public void Clickz()
        {
            string a = this.label1.Text;
            var fm = frmIns as IBase;
            fm.CurrencyClick(a);
        }
    }
}
