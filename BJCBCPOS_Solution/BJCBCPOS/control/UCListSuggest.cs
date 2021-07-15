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
    public partial class UCListSuggest : UserControl
    {
        private int _cntCount;

        public UCListSuggest()
        {
            InitializeComponent();
        }

        public UCListSuggest(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCListSuggest_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewItemSell_LostFocus;
        }

        private void UCGridViewItemSell_LostFocus(object sender, EventArgs e)
        {
            UCGridViewItemSell_LostFocus(e);
        }

        private void UCGridViewItemSell_LostFocus(EventArgs e)
        {
            //if (lastItemClick != null)
            //{
            //    lastItemClick.BackColor = Color.White;
            //}
            //isLostFromUC = false;
        }

        public static void LostFocusItem(UCListSuggest ucIS)
        {
            if (Convert.ToInt32(ucIS.lbNoText) % 2 != 0)
            {
                ucIS.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                ucIS.BackColor = Color.White;
            }
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
    }
}
