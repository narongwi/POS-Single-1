using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class UCEmployeeDiscount : UserControl
    {
        frmSaleProcess saleProcess;
        frmSale _frmSale;
        string _empID = "";

        public delegate void EmployeeConnectionLostHandler(NetworkConnectionException net);

        #region Event

        [Category("Action")]
        [Description("Occurs when the network lost.")]
        [Browsable(true)]
        public event EmployeeConnectionLostHandler EmployeeCatchNetWorkConnectionException;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler EmployeeButtonBackClick;

        [Category("Action")]
        [Description("Occurs when the click button enter")]
        [Browsable(true)]
        public event EventHandler EmployeeEnterFromButton;

        #endregion

        public UCEmployeeDiscount()
        {
            InitializeComponent();
        }

        private void UCEmployeeDiscount_Load(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            if (frm is frmSale)
            {
                _frmSale = (frmSale)frm;
                saleProcess = new frmSaleProcess(_frmSale);
            }
            CheckEmployee();
        }

        private void picBtnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (EmployeeButtonBackClick != null)
            {
                EmployeeButtonBackClick(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_empID != "")
            {
                ProgramConfig.employeeID = _empID;
            }
            this.Visible = false;
        }

        private void ucTxt_EmpID_EnterFromButton(object sender, EventArgs e)
        {
            string empID = ucTxt_EmpID.InpTxt.Trim();

            StoreResult res = saleProcess.CheckEmployee(empID);
            if (res.response.next)
            {
                lbNameEmp.Text = "ชื่อ : " + res.otherData.Rows[0]["Employee_name"].ToString();
                lbNameEmp.Visible = true;
                btnDelete.Visible = true;
                btnOk.Visible = true;
                _empID = empID;      
                if (EmployeeEnterFromButton != null)
                {
                    EmployeeEnterFromButton(this, e);
                }
            }
            ucTxt_EmpID.FocusTxt();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmNotify notify = new frmNotify(ResponseCode.Warning, "ต้องการยกเลิกการระบุส่วนลดสำหรับพนักงานใช่หรือไม่?");
            var res = notify.ShowDialog(this);
            if (res == DialogResult.Yes)
            {
                ProgramConfig.employeeID = "";
                ucTxt_EmpID.InpTxt = "";
                CheckEmployee();
            }
            ucTxt_EmpID.FocusTxt();
        }

        private void CheckEmployee()
        {
            if (String.IsNullOrEmpty(ProgramConfig.employeeID))
            {
                lbNameEmp.Visible = false;
                btnDelete.Visible = false;
                btnOk.Visible = false;
            }
            else
            {
                lbNameEmp.Visible = true;
                btnDelete.Visible = true;
                btnOk.Visible = true;
            }
        }

        private void UCEmployeeDiscount_VisibleChanged(object sender, EventArgs e)
        {
            CheckEmployee();
        }


    }
}
