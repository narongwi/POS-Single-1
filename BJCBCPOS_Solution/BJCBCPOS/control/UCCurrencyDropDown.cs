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
    public partial class UCCurrencyDropDown : UserControl
    {
        private Form parent;
        public UCCurrencyDropDown()
        {
            InitializeComponent();
        }

        public UCCurrencyDropDown(Form pr)
        {
            InitializeComponent();
            parent = pr;
        }

        public int SetDropDown(List<string> lst)
        {
            int h = 0;
            int cnt = 1;
            this.panel1.Controls.Clear();
            foreach (string str in lst)
            {
                //ucH += 30;
                h += 30;
                UCItemCurrencyDropDown UCitm = new UCItemCurrencyDropDown(parent);
                UCitm.label1.Text = str;
                if (str == "Yuan")
                {
                    UCitm.pictureBox1.BackgroundImage = BJCBCPOS.Properties.Resources.china;
                }
                else if (str == "Dollar")
                {
                    UCitm.pictureBox1.BackgroundImage = BJCBCPOS.Properties.Resources.usa;
                }
                else if (str == "Baht")
                {
                    UCitm.pictureBox1.BackgroundImage = BJCBCPOS.Properties.Resources.Mask_Group_6;
                }

                if (cnt == 1) UCitm.lineShape1.Visible = false;
                cnt++;
                this.panel1.Controls.Add(UCitm);
            }
            //h = h + 10;
            this.panel1.Height = h;
            this.Height = h + 10;
            return h;
        }
    }
}
