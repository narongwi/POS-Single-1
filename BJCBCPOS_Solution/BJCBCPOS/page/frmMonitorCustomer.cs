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
    public partial class frmMonitorCustomer : Form
    {
        public frmMonitorCustomer()
        {
            InitializeComponent();
        }

        private void frmMonitorCustomer_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this);
            if (Screen.AllScreens.Length == 2)
            {
                Screen second = Screen.AllScreens[0];
                if (second.Primary)
                {
                    second = Screen.AllScreens[1];
                }

                Point screen_location = second.WorkingArea.Location;
                this.Location = new Point(screen_location.X + 495, screen_location.Y);
            }

            this.pn_Item.AutoScroll = false;
            this.pn_Item.VerticalScroll.SmallChange = 40;
            this.pn_Item.VerticalScroll.LargeChange = 360;
            this.pn_Item.AutoScroll = true;

            update();

            double deFau = 0;
            lbTxtSubTotalCash.Text = deFau.ToString(ProgramConfig.amountFormatString);
            lbTxtDiscount.Text = deFau.ToString(ProgramConfig.amountFormatString);
            lbTxtTotalCash.Text = deFau.ToString(ProgramConfig.amountFormatString);
        }

        public void update()
        {
            if (!string.IsNullOrEmpty(ProgramConfig.memberName))
            {
                lbTxtMember.Text = ProgramConfig.memberName;
            }
            else
            {
                lbTxtMember.Text = "-";
            }

            lbCurrencyRate1.Text = "";
            lbCurrencyRate2.Text = "";
            lbAmtCurrency1.Text = "";
            lbAmtCurrency2.Text = "";
        }

        public void clearForm()
        {
            AppMessage.fillForm(ProgramConfig.language, this);
            if (Screen.AllScreens.Length == 2)
            {
                Screen second = Screen.AllScreens[0];
                if (second.Primary)
                {
                    second = Screen.AllScreens[1];
                }

                Point screen_location = second.WorkingArea.Location;
                this.Location = new Point(screen_location.X + 495, screen_location.Y);
            }

            this.pn_Item.AutoScroll = false;
            this.pn_Item.VerticalScroll.SmallChange = 40;
            this.pn_Item.VerticalScroll.LargeChange = 360;
            this.pn_Item.AutoScroll = true;

            update();

            pn_Item.Controls.Clear();

            double deFau = 0;
            lbTxtSubTotalCash.Text = deFau.ToString(ProgramConfig.amountFormatString);
            lbTxtDiscount.Text = deFau.ToString(ProgramConfig.amountFormatString);
            lbTxtTotalCash.Text = deFau.ToString(ProgramConfig.amountFormatString);
        }
    }
}
