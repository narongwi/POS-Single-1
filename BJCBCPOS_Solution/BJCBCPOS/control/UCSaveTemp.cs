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
    public partial class UCSaveTemp : UserControl
    {
        DataTable _dtData = new DataTable();
        UCListItemSaveTempSale itemChoosed = null;

        [Category("Action")]
        [Description("Occurs when item clicked")]
        [Browsable(true)]
        public event EventHandler ItemClick;

        [Category("Action")]
        [Description("Occurs when button confirm clicked")]
        [Browsable(true)]
        public event EventHandler ConfirmClick;

        [Category("Action")]
        [Description("Occurs when button confirm clicked")]
        [Browsable(true)]
        public event EventHandler BackClick;

        public UCSaveTemp()
        {
            InitializeComponent();
        }

        public UCSaveTemp(DataTable dtData)
        {
            _dtData = dtData;
            InitializeComponent();
        }

        private void UCSaveTemp_Load(object sender, EventArgs e)
        {
            pn_Item.Controls.Clear();
            foreach (DataRow dr in _dtData.Rows)
            {
                UCListItemSaveTempSale itm = new UCListItemSaveTempSale();
                itm.lbRefVal.Text = dr["REF"].ToString();
                itm.lbDateTime.Text = dr["Rdate"].ToString();
                itm.RefSaleTxt = dr["SALESREF"].ToString();
                itm.ItemClick += (se, ev) =>
                {
                    if (ItemClick != null)
                    {
                        itemChoosed = (UCListItemSaveTempSale)se;
                        ItemClick(se, ev);
                    }
                };

                pn_Item.Controls.Add(itm);
                pn_Item.Controls.SetChildIndex(itm, 0);
            }
        }

        private void lbListSave_Click(object sender, EventArgs e)
        {

        }

        private void picBtBack3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            if (BackClick != null)
            {
                BackClick(this, e);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ConfirmClick != null)
            {
                if (itemChoosed != null)
                {
                    ConfirmClick(itemChoosed, e);
                    this.Dispose();                  
                }

            }
        }
    }
}
