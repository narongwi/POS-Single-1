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
    public partial class UCListItemSaveTempSale : UserControl
    {
        public Color BackGround {
            set
            {
                Color colr = value;
                lbDateTime.BackColor = colr;
                lbRefTxt.BackColor = colr;
                lbRefVal.BackColor = colr;
                this.BackColor = colr;
            }
        }

        public Color FontColor
        {
            set
            {
                Color colr = value;
                lbDateTime.ForeColor = colr;
                lbRefTxt.ForeColor = colr;
                lbRefVal.ForeColor = colr;
            }
        }

        //[Category("Custom Property")]
        //[Description("Item Chiosed")]
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool ItemChiosed
        //{
        //    get;
        //    set;
        //}

        [Category("Custom Property")]
        [Description("Sale Reference")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RefSaleTxt
        {
            get;
            set;
        }

        [Category("Action")]
        [Description("Occurs when item clicked")]
        [Browsable(true)]
        public event EventHandler ItemClick;

        public UCListItemSaveTempSale()
        {
            InitializeComponent();
        }

        private void UCListItemSaveTempSale_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            if (this.Parent is Panel)
            {
                Panel pn = this.Parent as Panel;
                var itms = pn.Controls.Cast<UCListItemSaveTempSale>();
                foreach (var itm in itms)
                {
                    itm.BackGround = Color.White;
                    itm.lbDateTime.ForeColor = Color.Gray;
                    itm.lbRefTxt.ForeColor = Color.Black;
                    itm.lbRefVal.ForeColor = Color.Black;
                    //itm.ItemChiosed = false;
                }

                this.BackGround = Color.Green;
                this.FontColor = Color.White;
                //this.ItemChiosed = true;
            }

            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }


    }
}
