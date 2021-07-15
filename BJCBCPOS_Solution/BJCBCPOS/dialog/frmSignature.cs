using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace BJCBCPOS.dialog
{
    public partial class frmSignature : Form
    {
        private Image signature;
        private bool clicked = false;
        private Point previousPoint;

        public frmSignature()
        {
            InitializeComponent();
        }

        private void lb_Signature_MouseDown(object sender, MouseEventArgs e)
        {
            lb_Signature.Visible = false;
        }

        private void PictureBox3_Paint(object sender, PaintEventArgs e)
        {
            if (signature != null)
            {
                e.Graphics.DrawImage(signature, 0, 0);
            }
        }

        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            previousPoint = e.Location;
        }

        private void PictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            clicked = false;
        }

        private void PictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                lb_Signature.Visible = false;
                btnConfirm.Enabled = true;
                //btn_Confirm.BackgroundImage = My.Resources.cancel_bt_1x

                if (signature == null)
                {
                    signature = new Bitmap(PictureBox3.Width, PictureBox3.Height);
                }

                using (Graphics g = Graphics.FromImage(signature))
                {
                    Brush brush = new SolidBrush(Color.Black);
                    Pen pen = new Pen(brush, 3);
                    g.DrawLine(pen, previousPoint, e.Location);
                    previousPoint = e.Location;
                    PictureBox3.Invalidate();
                //Dim myBrush As Brush = New SolidBrush(Color.Black)
                //Dim pen As Pen = New Pen(myBrush, 3)
                //g.DrawLine(pen, previousPoint, e.Location)
                //previousPoint = e.Location
                //PictureBox3.Invalidate()
                }
            }
        //If clicked Then
        //    lb_Signature.Visible = False
        //    btn_Confirm.Enabled = True
        //    btn_Confirm.BackgroundImage = My.Resources.cancel_bt_1x
        //    If signature Is Nothing Then signature = New Bitmap(PictureBox3.Width, PictureBox3.Height)

        //    Using g As Graphics = Graphics.FromImage(signature)
        //        Dim myBrush As Brush = New SolidBrush(Color.Black)
        //        Dim pen As Pen = New Pen(myBrush, 3)
        //        g.DrawLine(pen, previousPoint, e.Location)
        //        previousPoint = e.Location
        //        PictureBox3.Invalidate()
        //    End Using
        //End If
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lb_Signature.Visible = true;
            btnConfirm.Enabled = false;
            btnConfirm.BackgroundImage = Properties.Resources.consent_btn_disableconfirm;

            if (signature != null)
            {
                signature = new Bitmap(PictureBox3.Width, PictureBox3.Height);
                using (Graphics g = Graphics.FromImage(signature))
                {
                    PictureBox3.Invalidate();
                }
            }

        //lb_Signature.Visible = True
        //btn_Confirm.Enabled = False
        //'btn_Confirm.BackgroundImage = My.Resources.disable_ok_bt_1x
        //If signature IsNot Nothing Then
        //    signature = New Bitmap(PictureBox3.Width, PictureBox3.Height)
        //    Using g As Graphics = Graphics.FromImage(signature)
        //        PictureBox3.Invalidate()
        //    End Using
        //End If
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (signature != null)
            {
                decimal resize = 0.7m;
                string str64bit = "";
                byte[] imgByte;


                using (MemoryStream m = new MemoryStream())
                {
                    signature.Save(m, ImageFormat.Png);
                    imgByte = m.ToArray();
                    str64bit = Convert.ToBase64String(imgByte);
                }

            Resize:
                Image img;
                byte[] imgByte2 = Convert.FromBase64String(str64bit);
                using (MemoryStream m = new MemoryStream(imgByte2, 0, imgByte2.Length))
                {
                    img = Image.FromStream(m, true);
                    int w = Convert.ToInt32(Convert.ToDecimal(img.Width) * resize);
                    int h = Convert.ToInt32(Convert.ToDecimal(img.Height) * resize);
                    img = new Bitmap(img, w, h);
                }

                string str64bit2 = "";
                using (MemoryStream m = new MemoryStream())
                {
                    img.Save(m, ImageFormat.Png);
                    imgByte = m.ToArray();
                    str64bit2 = Convert.ToBase64String(imgByte);
                }

                if (str64bit2.Length >= 4000)
                {
                    resize = resize - 0.02m;
                    goto Resize;
                }

                //Insert Consent

                if (signature != null)
                {
                    signature = new Bitmap(PictureBox3.Width, PictureBox3.Height);
                    using (Graphics g = Graphics.FromImage(signature))
                    {
                        PictureBox3.Invalidate();
                    }
                }
                //                If signature IsNot Nothing Then
                //    signature = New Bitmap(PictureBox3.Width, PictureBox3.Height)
                //    Using g As Graphics = Graphics.FromImage(signature)
                //        PictureBox3.Invalidate()
                //    End Using
                //End If
            }

        }
    }
}
