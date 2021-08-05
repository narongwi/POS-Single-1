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
    public partial class frmAlertMsgCustomer : Form
    {
        string msg;

        public frmAlertMsgCustomer()
        {
            InitializeComponent();
        }

        public frmAlertMsgCustomer(string msg)
        {
            InitializeComponent();
            this.msg = msg;
        }

        private void frmAlertMsgCustomer_Load(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length == 2)
            {
                Point screen_location = Screen.AllScreens[1].WorkingArea.Location;
                this.Location = new Point(screen_location.X, screen_location.Y);
            }
            lbAlertMessage.Text = msg;
        }
    }
}
