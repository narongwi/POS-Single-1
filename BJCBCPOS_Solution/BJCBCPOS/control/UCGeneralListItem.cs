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
    public partial class UCGeneralListItem : UserControl
    {
        private int _valueItem;
        private string _saleOrderType;

        [Category("Custom Property")]
        [Description("Set value item")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ValueText
        {
            get
            {
                return _valueItem;
            }
            set
            {
                _valueItem = value;
            }
        }

        [Category("Custom Property")]
        [Description("Set sale order type")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SaleOrderType
        {
            get
            {
                return _saleOrderType;
            }
            set
            {
                _saleOrderType = value;
            }
        }
        
        [Category("Action")]
        [Description("Occurs when the item is clicked.")]
        [Browsable(true)]
        public event EventHandler ItemClick;

        public UCGeneralListItem()
        {
            InitializeComponent();
        }

        private void UCGeneralListItem_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void lbTxtDesc_Click(object sender, EventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }

        private void UCGeneralListItem_Click(object sender, EventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }
    }
}
