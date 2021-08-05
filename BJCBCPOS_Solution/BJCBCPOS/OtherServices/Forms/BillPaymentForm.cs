using BJCBCPOS_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BJCBCPOS.OtherServices.Forms {
    public partial class BillPaymentForm : Form
    {
        bool clicked = false;
        CheckBoxState state;
        public BillPaymentForm()
        {
            InitializeComponent();

        }

        private void deftsoftPictureIcon2_Click(object sender,EventArgs e) {

        }

        private void BillPaymentForm_Load(object sender,EventArgs e) {

            List<PaymentItems> payment = new List<PaymentItems>() {
                new PaymentItems(1,"สาธารณูปโภค (ค่าน้ำ/ค่าไฟ)"),
                new PaymentItems(2,"เทเลคอม (ค่าโทรศัพท์รายเดือน/อินเตอร์เน็ต)"),
                new PaymentItems(3,"บัตรเครดิต / สินเชื่อ"),
                new PaymentItems(4,"ค่างวดรถ / ลิสซิ้ง"),
                new PaymentItems(5,"ประกันภัย / ประกันชีวิต"),
                new PaymentItems(6,"อื่น ๆ"),
                new PaymentItems(7,"บริจาค"),
                new PaymentItems(8,"กลุ่มราชการ / รัฐวิสาหกิจ"),
                new PaymentItems(9,"เติมวอลเล็ต")
            };

            PaymentGv.DataSource = payment;
            PaymenTotalLabel.Text = payment.Count().ToString();
        }

        private void deftsoftPanel3_Paint(object sender,PaintEventArgs e) {

        }

        private void selPaymen_Click(object sender,EventArgs e) {
            PaymentListPanel.Visible = !PaymentListPanel.Visible;

         // hide
            if(!PaymentListPanel.Visible) {
                PaymentListPanel.Height = 59;
                PaymenSelBtn.BorderSize = 1;
                PaymenSelBtn.BackColor = Color.WhiteSmoke;
                PaymenSelBtn.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;

            } else {
                // show 
                PaymentListPanel.Height = 568;
                PaymenSelBtn.BorderSize = 0;
                PaymenSelBtn.BackColor = Color.Transparent;
                PaymenSelBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            }
        }

        private void PaymentGv_CellClick(object sender,DataGridViewCellEventArgs e) {
            if(e.RowIndex == -1) return;
            PaymenSelBtn.Text = PaymentGv.Rows[e.RowIndex].Cells["pmColName"].Value.ToString();
            PaymentListPanel.Height = 59;
            PaymenSelBtn.BorderSize = 1;
            PaymenSelBtn.BackColor = Color.WhiteSmoke;
            PaymenSelBtn.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            PaymentListPanel.Visible = false;
        }
    }

    public class PaymentItems {
        public PaymentItems() {
        }

        public PaymentItems(int id,string name) {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
