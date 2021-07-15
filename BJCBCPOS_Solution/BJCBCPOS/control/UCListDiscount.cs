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
    public partial class UCListDiscount : UserControl
    {
        private int _cntCount;
        private bool isLostFromUC = false;

        public UCListDiscount()
        {
            InitializeComponent();
        }

        public UCListDiscount(int cntCount)
        {
            InitializeComponent();
            _cntCount = cntCount;
        }

        private void UCListDiscount_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.LostFocus += UCGridViewListDiscount_LostFocus;

            lbTxtName.LostFocus += lbTxtName_LostFocus;
            lbTxtPrice.LostFocus += lbTxtPrice_LostFocus;
            lbTxtAmt.LostFocus += lbTxtAmt_LostFocus;
        }

        private void lbTxtName_LostFocus(object sender, EventArgs e)
        {
            UCGridViewListDiscount_LostFocus(e);
        }

        private void lbTxtPrice_LostFocus(object sender, EventArgs e)
        {
            UCGridViewListDiscount_LostFocus(e);
        }

        private void lbTxtAmt_LostFocus(object sender, EventArgs e)
        {
            UCGridViewListDiscount_LostFocus(e);
        }

        private void UCGridViewListDiscount_LostFocus(object sender, EventArgs e)
        {
            UCGridViewListDiscount_LostFocus(e);
        }

        private void UCGridViewListDiscount_LostFocus(EventArgs e)
        {
            //if (lastItemClick != null)
            //{
            //    lastItemClick.BackColor = Color.White;
            //}
            //isLostFromUC = false;
        }

        public static void LostFocusItem(UCListDiscount ucIS)
        {
            if (Convert.ToInt32(ucIS.lbTxtNameText) % 2 != 0)
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
        public string lbTxtNameText
        {
            get { return lbTxtName.Text; }
            set
            {
                lbTxtName.Text = value;
            }
        }
    }
}
