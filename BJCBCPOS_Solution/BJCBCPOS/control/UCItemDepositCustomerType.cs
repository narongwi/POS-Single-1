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
    public partial class UCItemDepositCustomerType : UserControl
    {
        public string placeHolderName = "UCItemDepositCustomerType_";
        public bool IsLastUC = false;
        public Color OldColor;

        [Category("Action")]
        [Description("Occurs when the member is clicked.")]
        [Browsable(true)]
        public event EventHandler UCItemDepositCustomerTypeClick;

        public string Seq { get; set; }

        public string FunctionID { get; set; }

        public UCItemDepositCustomerType()
        {
            InitializeComponent();
        }

        private void UCItemDepositCustomerType_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCItemDepositCustomerType_LostFocus;

            lbName.LostFocus += lbName_LostFocus;
        }

        private void UCItemDepositCustomerType_Click(object sender, EventArgs e)
        {
            if (UCItemDepositCustomerTypeClick != null)
            {
                ((Control)sender).Focus();
                if (!IsLastUC)
                {
                    this.OldColor = this.BackColor;
                    this.BackColor = Color.FromArgb(63, 184, 104);
                    SetForeColor(Color.White);
                    this.IsLastUC = true;
                    UCItemDepositCustomerTypeClick(this, e);
                }
            }
        }

        private void lbName_LostFocus(object sender, EventArgs e)
        {
            UCItem_LostFocus(sender, e);
        }

        private void UCItemDepositCustomerType_LostFocus(object sender, EventArgs e)
        {
            UCItem_LostFocus(sender, e);

            //this.BackColor = Color.FromArgb(235, 248, 240);
            //this.BackColor = Color.Transparent;
        }

        private void UCItem_LostFocus(object sender, EventArgs e)
        {
            if (this.Parent is Panel)
            {
                Panel pn = this.Parent as Panel;
                var itm = pn.Controls.Cast<UCItemDepositCustomerType>().Where(w => w.Name.Contains(placeHolderName) && w.IsLastUC).FirstOrDefault();
                if (itm != null)
                {
                    itm.BackColor = itm.OldColor;
                    SetForeColor(Color.FromArgb(120, 120, 120));
                    IsLastUC = false;
                }
            }
        }

        private void SetForeColor(Color color)
        {
            lbName.ForeColor = color;
        }
    }
}
