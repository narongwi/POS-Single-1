using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.OtherServices {
    public partial class frmBills : Form {
        private ImageList lstImages = new ImageList();
        public frmBills() {
            InitializeComponent();
        }

        private void panel6_Paint(object sender,PaintEventArgs e) {
          
        }

        private void frmBills_Load(object sender,EventArgs e) {
            lsvService.Columns.Add("รหัสสินค้า",90,HorizontalAlignment.Left);
            lsvService.Columns.Add("ชื่อสินค้า",190,HorizontalAlignment.Left);
            lsvService.Columns.Add("ราคาขาย",60,HorizontalAlignment.Right);
            lsvService.Columns.Add("จำนวน",50,HorizontalAlignment.Right);
            lsvService.Columns.Add("ส่วนลด",60,HorizontalAlignment.Right);
            lsvService.Columns.Add("ภาษี",60,HorizontalAlignment.Right);
            lsvService.Columns.Add("รวมเป็นเงิน",75,HorizontalAlignment.Right);
            lsvService.Columns.Add("",0,HorizontalAlignment.Right);
            lsvService.Columns.Add("",0,HorizontalAlignment.Right);
            lsvService.Columns.Add("",0,HorizontalAlignment.Right);
            lsvService.View = View.Details;
            lsvService.GridLines = true;

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
