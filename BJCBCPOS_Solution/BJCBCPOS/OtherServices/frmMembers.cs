using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.OtherServices
{
    public partial class frmMembers : Form
    {
        public frmMembers()
        {
            InitializeComponent();
        }

        private void frmMembers_Load(object sender, EventArgs e)
        {
            lsvService.Columns.Add("หมายเลขบัตรประชาชน", 100, HorizontalAlignment.Left);
            lsvService.Columns.Add("หมายเลขบัตรสมาชิก", 100, HorizontalAlignment.Left);
            lsvService.Columns.Add("ภาษาที่ใช้ในการสื่อสาร", 60, HorizontalAlignment.Left);
            lsvService.Columns.Add("ชื่อ", 80, HorizontalAlignment.Left);
            lsvService.Columns.Add("นามสกุล", 120, HorizontalAlignment.Left);
            lsvService.Columns.Add("ที่อยู่เดียวกันกับบัตรประชาชน", 60, HorizontalAlignment.Left);
            lsvService.Columns.Add("รหัสไปรษณีย์", 50, HorizontalAlignment.Left);
            lsvService.Columns.Add("หมายเลขโทรศัพท์มือถือ", 80, HorizontalAlignment.Left);
            lsvService.Columns.Add("หมายเลขบัตรสวัสดิการแห่งรัฐ", 80, HorizontalAlignment.Left);
            lsvService.View = View.Details;
            lsvService.GridLines = true;
        }
    }
}
