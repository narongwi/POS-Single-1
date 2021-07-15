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
    public partial class frmShowInfoCustFullTax : Form
    {
        private bool IsPaint = false;
        string _name = "";

        public frmShowInfoCustFullTax(string name)
        {
            InitializeComponent();
            _name = name;
        }

        private void frmShowInfoCustFullTax_Load(object sender, EventArgs e)
        {
            label5.Text = _name;
            label7.Text = "หมายเลขประจำตัวผู้เสียภาษี " + ProgramConfig.memberProfileMMFormat.Customer_IDCard;
            label6.Text = ProgramConfig.memberProfileMMFormat.Address;

            AppMessage.fillForm(ProgramConfig.language, this);
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);

                this.Location = this.Owner.Location;
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
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



    }
}
