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
    public partial class UCMonitor2Item : UserControl
    {
        private int _cntColor;
        public UCMonitor2Item()
        {
            InitializeComponent();
        }

        public UCMonitor2Item(int cntColor)
        {
            InitializeComponent();
            _cntColor = cntColor;
        }

        [Category("Custom Property")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbNoText
        {
            get { return lbNo.Text; }
            set
            {
                lbNo.Text = value;
            }
        }

        private void UCMonitor2Item_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            AppMessage.fillControlsFont(ProgramConfig.language, this, new List<string>() { lb_AMT.Name, lb_QTY.Name });
        }

        public static void LostFocusItem(UCMonitor2Item ucIS)
        {
            if (Convert.ToInt32(ucIS.lbNoText) % 2 != 0)
            {
                ucIS.BackColor = Color.FromArgb(143, 255, 182);
            }
            else
            {
                ucIS.BackColor = Color.White;
            }
        }
    }
}
