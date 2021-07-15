using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Process;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmCashireMessage : Form
    {
        private bool IsPaint = false; 
        public FunctionID function { get; set; }
        private StartProcess process = null;

        public frmCashireMessage()
        {
            InitializeComponent();
            process = new StartProcess();
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
            this.Dispose();
        }

        private void frmCashireMessage_Load(object sender, EventArgs e)
        {
            GetCashierMessage();
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (pnMainPanel.Size.Width / 2);
                int y = (this.Size.Height / 2) - (pnMainPanel.Size.Height / 2);
                pnMainPanel.Location = new Point(x, y);

                this.Location = this.Owner.Location;
            }
            else
            {
                this.Size = new Size(1024, 768);

                int x = (this.Size.Width / 2) - (pnMainPanel.Size.Width / 2);
                int y = (this.Size.Height / 2) - (pnMainPanel.Size.Height / 2);
                pnMainPanel.Location = new Point(x, y);
            }           
        }

        private void GetCashierMessage()
        {
            frmLoading.showLoading();
            StoreResult res = process.getCashierMessage();
            if (res.response.next)
            {
                int count = res.otherData.Rows.Count;
                UCCashierMessage message;
                DataRow row;
                for (int i = 0; i < count; i++)
                {
                    row = res.otherData.Rows[i];
                    message = new UCCashierMessage();
                    message.date = row["Message_DateTime"].ToString();
                    message.text = row["Message_Text"].ToString();
                    message.Dock = DockStyle.Top;

                    //if (i % 2 == 0)
                    //{
                    //    message.background = Color.White;
                    //}
                    //else
                    //{
                    //    message.background = Color.FloralWhite;
                    //}

                    message.update();

                    pnMessagePanel.Controls.Add(message);
                    pnMessagePanel.Controls.SetChildIndex(message, 0);
                }
            }
            frmLoading.closeLoading();
        }

        private void frmCashireMessage_Shown(object sender, EventArgs e)
        {

        }
    }
}
