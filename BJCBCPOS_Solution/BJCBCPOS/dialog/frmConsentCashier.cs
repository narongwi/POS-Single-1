using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.dialog
{
    public partial class frmConsentCashier : Form
    {
        int currentLength = 0;
        bool IsRead = false;

        public frmConsentCashier()
        {
            InitializeComponent();
        }

        private void frmConsentCashier_Shown(object sender, EventArgs e)
        {
            picLogo.Image = Utility.GetLogoImage();
            chkb_Agree.Enabled = false;
            lb_Argee1.ForeColor = Color.LightGray;
            lb_Argee2.ForeColor = Color.LightGray;
        }

        private void ButtonUp_Click(object sender, EventArgs e)
        {
            currentLength -= 500;

            if (currentLength < 0)
            {
                currentLength = 0;
            }

            VScrollBar1.Value = currentLength;
            ConsentRTB.SelectionStart = currentLength;
            ConsentRTB.ScrollToCaret();

            //If currentLength >= 0 Then
            //    currentLength -= 500
            //End If

            //If currentLength < 0 Then
            //    currentLength = 0
            //End If
            //VScrollBar1.Value = currentLength
            //ConsentRTB.SelectionStart = currentLength
            //ConsentRTB.ScrollToCaret()

        }

        private void ButtonDown_Click(object sender, EventArgs e)
        {
            currentLength += 500;
            if (currentLength >= ConsentRTB.Text.Length)
            {
                currentLength = ConsentRTB.Text.Length;
            }

            VScrollBar1.Value = currentLength;
            ConsentRTB.SelectionStart = currentLength;
            ConsentRTB.ScrollToCaret();

            //If currentLength < ConsentRTB.Text.Length Then
            //If currentLength <= ConsentRTB.Text.Length Then
            //    currentLength += 500
            //End If

            //If currentLength > ConsentRTB.Text.Length Then
            //    currentLength = ConsentRTB.Text.Length
            //End If
            //VScrollBar1.Value = currentLength
            //ConsentRTB.SelectionStart = currentLength
            //ConsentRTB.ScrollToCaret()
        }

        private void ButtonEnd_Click(object sender, EventArgs e)
        {
            if (currentLength < ConsentRTB.Text.Length)
            {
                currentLength = ConsentRTB.Text.Length;
                VScrollBar1.Value = currentLength;
                ConsentRTB.SelectionStart = currentLength;
                ConsentRTB.ScrollToCaret();
            }
        }

        private void pic_UnCheck_Click(object sender, EventArgs e)
        {
            if (IsRead)
            {
                chkb_Agree.Checked = true;
                pic_Check.BringToFront();
            }
        //            If IsRead Then
        //    chkb_Agree.Checked = True
        //    pic_Check.BringToFront()
        //End If
        }

        private void ConsentRTB_VScroll(object sender, EventArgs e)
        {
            if (!IsRead && currentLength >= ConsentRTB.Text.Length)
            {
                chkb_Agree.Enabled = true;
                IsRead = true;
                lb_Argee1.ForeColor = Color.Black;
                lb_Argee2.ForeColor = Color.Black;
            }

            //If IsRead = False And currentLength >= ConsentRTB.Text.Length Then
            //    chkb_Agree.Enabled = True
            //    IsRead = True
            //    lb_Argee1.ForeColor = Color.Black
            //    lb_Argee2.ForeColor = Color.Black
            //End If

        }

    }
}
