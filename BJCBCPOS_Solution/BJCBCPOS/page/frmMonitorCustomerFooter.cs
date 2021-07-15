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
    public partial class frmMonitorCustomerFooter : Form
    {
        public frmMonitorCustomerFooter()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(frmMonitorCustomerFooter_Disposed);
        }

        private void frmMonitorCustomerFooter_Load(object sender, EventArgs e)
        {
            lbTxtCashier.Text = "";
            AppMessage.fillForm(ProgramConfig.language, this);
            if (Screen.AllScreens.Length == 2)
            {
                Point screen_location = Screen.AllScreens[1].WorkingArea.Location;
                this.Location = new Point(screen_location.X, screen_location.Y + 570);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTxtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbTxtTime.Text = DateTime.Now.ToString("HH:mm:ss");

            if (!string.IsNullOrEmpty(ProgramConfig.cashierName))
            {
                lbTxtCashier.Text = ProgramConfig.cashierName;
            }
        }

        private void frmMonitorCustomerFooter_Disposed(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }
    }
}
