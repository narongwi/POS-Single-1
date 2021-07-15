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
    public partial class frmConfirmCancelCashOut : Form
    {
        public string envelopNumber = "";
        public string cashAmt = "";
        public bool IsPaint = false;

        public frmConfirmCancelCashOut()
        {       
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private void frmConfirmCancelCashOut_Load(object sender, EventArgs e)
        {       
            lbEnvelop.Text = envelopNumber;
            lbTxtAmount.Text = cashAmt;
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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    IsPaint = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}
