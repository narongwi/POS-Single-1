using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class UCFooter : UserControl
    {
        private Timer tm = new Timer();

        private const string firstline_format = "Store Code: {0} | Store Name: {1} | Tax ID: {2} | Permission ID: {3}  | Till No: {4} ";
        private const string secondline_format = "Cashier Code and Name: {0} | Server Name: {1} | Database Name: {2} | Sale Mode: {3} | Version: {4}";

        public UCFooter()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(UCFooter_Disposed);

            lbFirstLine.ForeColor = this.ForeColor;
            lbSecondLine.ForeColor = this.ForeColor;

            tm.Tick += new EventHandler(timer1_Tick);
            tm.Interval = 1000;
            tm.Enabled = true;
        }

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStandAlone
        {
            get
            {
                return ProgramConfig.IsStandAloneMode;
            }
            set
            {
                ProgramConfig.IsStandAloneMode = value;
                CheckModeStandAlone();
                this.Refresh();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;

                lbFirstLine.ForeColor = value;
                lbSecondLine.ForeColor = value;
            }
        }

        public void CheckModeStandAlone()
        {
            if (ProgramConfig.IsStandAloneMode)
            {
                lbStatus.Text = "STAND ALONE";
                lbStatus.BackColor = Color.Red;
                //ProgramConfig.saleMode = SaleMode.Standalone;
            }
            else
            {
                lbStatus.Text = "ON SERVER";
                lbStatus.BackColor = Color.FromArgb(100, 192, 100);
                //ProgramConfig.saleMode = SaleMode.Server;
            }
            this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbFirstLine.Text = string.Format(firstline_format, ProgramConfig.storeCode, ProgramConfig.storeName, ProgramConfig.taxId, ProgramConfig.permissionId, ProgramConfig.tillNo);
            lbSecondLine.Text = string.Format(secondline_format, ProgramConfig.cashierCode, ProgramConfig.serverName, ProgramConfig.databaseName, ProgramConfig.saleMode, ProgramConfig.version);
            tm.Interval = 50000000;
        }

        private void UCFooter_Disposed(object sender, EventArgs e)
        {
            tm.Stop();
            tm.Dispose();
        }

        private void UCFooter_Load(object sender, EventArgs e)
        {
            CheckModeStandAlone();
            lbStatus.Font = new Font("Prompt", lbStatus.Font.Size, lbStatus.Font.Style, lbStatus.Font.Unit, lbStatus.Font.GdiCharSet);
            lbFirstLine.Font = new Font("Prompt", lbFirstLine.Font.Size, lbFirstLine.Font.Style, lbFirstLine.Font.Unit, lbFirstLine.Font.GdiCharSet);
            lbSecondLine.Font = new Font("Prompt", lbSecondLine.Font.Size, lbSecondLine.Font.Style, lbSecondLine.Font.Unit, lbSecondLine.Font.GdiCharSet);
        }

        private void lb_Status_FontChanged(object sender, EventArgs e)
        {
            lbStatus.Font = new Font("Prompt", lbStatus.Font.Size, lbStatus.Font.Style, lbStatus.Font.Unit, lbStatus.Font.GdiCharSet);
            lbFirstLine.Font = new Font("Prompt", lbFirstLine.Font.Size, lbFirstLine.Font.Style, lbFirstLine.Font.Unit, lbFirstLine.Font.GdiCharSet);
            lbSecondLine.Font = new Font("Prompt", lbSecondLine.Font.Size, lbSecondLine.Font.Style, lbSecondLine.Font.Unit, lbSecondLine.Font.GdiCharSet);
            lbFunction.Font = new Font("Prompt", lbFunction.Font.Size, lbFunction.Font.Style, lbFunction.Font.Unit, lbFunction.Font.GdiCharSet);
        }

    }
}
