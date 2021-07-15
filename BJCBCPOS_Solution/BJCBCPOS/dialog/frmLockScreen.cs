using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class frmLockScreen : Form
    {
        private StartProcess process = new StartProcess();
        private bool IsPaint = false; 

        public frmLockScreen()
        {
            InitializeComponent();
        }

        private void frmLockScreen_Load(object sender, EventArgs e)
        {           
            lbUser.Text = ProgramConfig.userId;
            this.ucKeyboard1.currentInput = ucTxtPassword;
            ucTxtPassword.FocusTxt();
            //Utility.SetBackGroundBrightness(this.Owner, this);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    IsPaint = true;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginClick();
        }

        private void ucTxtPassword_TextBoxKeydown(object sender, EventArgs e)
        {
            LoginClick();
        }

        private void LoginClick()
        {
            StoreResult result = process.CheckUser(lbUser.Text, ucTxtPassword.InpTxt);
            if (!result.response.next)
            {
                ucTxtPassword.InpTxt = "";
                Utility.AlertMessage(result);
                this.Invalidate();
                return;
            }
            else
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}
