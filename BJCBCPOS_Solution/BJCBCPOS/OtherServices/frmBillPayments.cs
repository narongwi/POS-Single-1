using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.OtherServices {
    public partial class frmBillPayments : Form {
        private ImageList lstImages= new ImageList();
        public frmBillPayments() {
            InitializeComponent();
        }

        private void frmBillPayments_Load(object sender,EventArgs e) {
            //var fi = new System.IO.DirectoryInfo(Application.StartupPath + "\\images\\items");
            //lstImages.ColorDepth = ColorDepth.Depth32Bit;
            //lstImages.ImageSize = new Size(62,48);

            //foreach(var item in fi.GetFiles()) {
            //    lstImages.Images.Add(Image.FromFile(item.FullName));
            //}

            //for(int i = 0 ; i < lstImages.Images.Count ; i++) {
            //    dgvService.Rows.Add("",lstImages.Images[i],"test");
            //}
        }
    }
}
