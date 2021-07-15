using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmMonitor2Detail : Form
    {
        public frmMonitor2Detail()
        {
            InitializeComponent();
        }

        private void frmMonitor2Detail_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            panel_list.Controls.Clear();

            AppMessage.fillForm(ProgramConfig.language, this);
            if (Screen.AllScreens.Length == 2)
            {
                Point screen_location = Screen.AllScreens[1].WorkingArea.Location;
                this.Location = new Point(screen_location.X, screen_location.Y + 287);
            }
        }

        public void clearForm()
        {
            label1.Text = "";
            label2.Text = "";
            label1.BackColor = Color.White;
            label2.BackColor = Color.White;
            panel_list.Controls.Clear();
            panel_list.BringToFront();
            panel_message.BringToFront();
            lbTxtTotalCash.Text = "0";
            lbTxtReceive.Text = "0";
            lbTxtbalance.Text = "0";
            lbCurrencyRate1.Text = "";
            lbCurrencyRate2.Text = "";
            lbCurrencyRate3.Text = "";
            lbChangeCurrency1.Text = "";
            lbChangeCurrency2.Text = "";
            lbChangeCurrency3.Text = "";
        }
    }
}
