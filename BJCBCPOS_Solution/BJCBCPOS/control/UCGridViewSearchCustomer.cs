using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS
{
    public partial class UCGridViewSearchCustomer : UserControl
    {
        //private bool isLostFromUC = false;
        public string placeHolderName = "UCGridViewSearchCustomer_";

        public bool IsLastUC { get; set; }
        public Color OldColor { get; set; }

        public UCGridViewSearchCustomer()
        {
            InitializeComponent();
        }

        private void UCGridViewSearchCustomer_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewSearchCustomer_LostFocus;

            lb_MemberID.LostFocus += MemberID_LostFocus;
            lb_MemberCard.LostFocus += lb_MemberCard_LostFocus;
            lb_IDCard.LostFocus += lb_IDCard_LostFocus;
            lb_Tel.LostFocus += lb_Tel_LostFocus;
            lb_Name.LostFocus += lb_Name_LostFocus;
        }

        private void lb_MemberCard_LostFocus(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_LostFocus(e);
        }

        private void lb_IDCard_LostFocus(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_LostFocus(e);
        }

        private void lb_Tel_LostFocus(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_LostFocus(e);
        }

        private void lb_Name_LostFocus(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_LostFocus(e);
        }

        private void MemberID_LostFocus(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_LostFocus(e);
        }

        private void UCGridViewSearchCustomer_LostFocus(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_LostFocus(e);
        }

        private void UCGridViewSearchCustomer_LostFocus(EventArgs e)
        {        
            if(this.Parent is Panel)
            {
                Panel pn = this.Parent as Panel;
                var itm = pn.Controls.Cast<UCGridViewSearchCustomer>().Where(w => w.Name.Contains(placeHolderName) && w.IsLastUC).FirstOrDefault();
                if (itm != null)
                {
                    itm.BackColor = itm.OldColor;
                    SetForeColor(Color.FromArgb(120, 120, 120));
                    IsLastUC = false;
                }
            }
            
            //this.BackColor = Color.FromArgb(235, 248, 240);
            //this.BackColor = Color.Transparent;
        }

        [Category("Action")]
        [Description("Occurs when the member is clicked.")]
        [Browsable(true)]
        public event EventHandler UCGridViewSearchCustomerClick;

        private void UCGridViewSearchCustomer_Click(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer_Click(e);
        }

        private void UCGridViewSearchCustomer_Click(EventArgs e)
        {
            if (UCGridViewSearchCustomerClick != null)
            {
                if (!IsLastUC)
                {
                    this.OldColor = this.BackColor;
                    this.BackColor = Color.FromArgb(63, 184, 104);
                    SetForeColor(Color.White);
                    this.IsLastUC = true;
                    UCGridViewSearchCustomerClick(this, e);
                }
            }
        }

        private void SetForeColor(Color color)
        {
            lb_MemberCard.ForeColor
                = lb_MemberID.ForeColor 
                = lb_IDCard.ForeColor 
                = lb_Name.ForeColor 
                = lb_Tel.ForeColor 
            = color;
        }

        private void lb_MemberID_Click(object sender, EventArgs e)
        {
            lb_MemberID.Focus();
            UCGridViewSearchCustomer_Click(e);
        }

        private void lb_MemberCard_Click(object sender, EventArgs e)
        {
            lb_MemberCard.Focus();
            UCGridViewSearchCustomer_Click(e);
        }

        private void lb_IDCard_Click(object sender, EventArgs e)
        {
            lb_IDCard.Focus();
            UCGridViewSearchCustomer_Click(e);
        }

        private void lb_Tel_Click(object sender, EventArgs e)
        {
            lb_Tel.Focus();
            UCGridViewSearchCustomer_Click(e);
        }

        private void lb_Name_Click(object sender, EventArgs e)
        {
            lb_Name.Focus();
            UCGridViewSearchCustomer_Click(e);
        }


    }
}
