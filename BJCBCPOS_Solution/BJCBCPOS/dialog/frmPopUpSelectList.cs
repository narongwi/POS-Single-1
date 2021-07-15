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
    public partial class frmPopUpSelectList : Form
    {
        private bool IsPaint = false;
        public List<UserControl> lstUC = new List<UserControl>();

        public frmPopUpSelectList()
        {
            InitializeComponent();
            lstUC = new List<UserControl>();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            //var IsChoice = panel2.Controls.Cast<UCItemDepositCustomerType>().Any(a => a.IsLastUC);
            //if (IsChoice)
            //{
            this.Dispose();
            //}
            //else
            //{
            //    Utility.AlertMessage(ResponseCode.Error, "กรุณาเลือกรายการ");
            //}     
        }

        private void frmPopUpSelectList_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);
                this.Location = this.Owner.Location;
            }
            else
            {
                this.Size = new System.Drawing.Size(1024, 768);
                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);
                this.Location = new Point(0, 0);
            }


            foreach (var item in lstUC)
            {
                panel2.Controls.Add(item);
            }

            btnOK.Enabled = false;
            btnOK.BackColor = Color.Gray;
        }


    }
}
