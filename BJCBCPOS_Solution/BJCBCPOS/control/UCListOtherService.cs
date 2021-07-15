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
    public partial class UCListOtherService : UserControl
    {
        [Category("Action")]
        [Description("Occurs when the item is clicked.")]
        [Browsable(true)]
        public event EventHandler ItemClick;

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StoreCode { get; set; }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InvoiceNo { get; set; }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InvoiceDate { get; set; }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InvoiceAmount { get; set; }

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TransDate { get; set; }
     
        public UCListOtherService()
        {
            InitializeComponent();
        }

        private void UCListOtherService_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void UCListOtherService_Click(object sender, EventArgs e)
        {
            ItemsClick(sender, e);
        }

        private void ItemsClick(object sender, EventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(this, e);
            }
        }
    }
}
