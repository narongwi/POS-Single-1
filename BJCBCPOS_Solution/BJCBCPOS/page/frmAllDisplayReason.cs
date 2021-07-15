using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;
using System.Reflection;

namespace BJCBCPOS
{
    public partial class frmAllDisplayReason : Form
    {
        private bool IsPaint = false; 
        private VoidProcess process = new VoidProcess();
        private int typeChange;
        //string productCode;
        string productEditQuant;
        string productCurrentQuant;
        string productEditPrice;
        string productCurrentPrice;
        //string editType;
        string stcode;
        string refNo;
        string rec;
        string sty;
        string vty;
        //string pcd;
        string qnt;
        //string amt;
        //string fds;
        string ttm;
        string usr;
        //string egp;
        string stt;
        string stv;
        string reason;
        //string pdisc;
        //string discid;
        //string discamt;
        string upc;
        //string dty;
        public string eventName;
        public string functionId;
        public StoreResult result = null;
        public string _reasonTxt;
        public string _reasonID;

        public frmAllDisplayReason(string evName)
        {            
            InitializeComponent();
            eventName = evName;

        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
                        
            if (result.otherData != null)
            {
                DataTable dt = result.otherData;
                dt = dt.AsEnumerable().OrderByDescending(o => Convert.ToInt32(o["seq"])).CopyToDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    drItem.DisplayText = dr["ReasonDesc"].ToString();
                    drItem.ValueText = dr["ReasonID"].ToString();
                    lstStr.Add(drItem);
                }
            }
            return lstStr;
        }

        private void ucDDReasonToEdit_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList2();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    IsPaint = true;
                }
            }
        }

        private void frmAllDisplayReason_Load(object sender, EventArgs e)
        {
            try
            {
                ucDDReasonToEdit.LabelText = AppMessage.getMessage(ProgramConfig.language, "frmAllDisplayReason", "ucDDReasonToEdit");
                AppMessage.fillForm(ProgramConfig.language, this);
                if (this.Owner != null)
                {
                    this.Size = this.Owner.Size;

                    int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                    int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                    panel1.Location = new Point(x, y);

                    this.Location = this.Owner.Location;
                }

                if (eventName == "ReturnScan")
                {
                    lbMenuName.Visible = true;
                    lbMenuNameVoid.Visible = false;
                    result = process.displayReason(FunctionID.Return_InputReturnReason);
                }
                else if (eventName == "ReturnInvoice")
                {
                    lbMenuName.Visible = true;
                    lbMenuNameVoid.Visible = false;
                    result = process.displayReason(FunctionID.Return_InputReturnReason);
                }
                else if (eventName == "Void")
                {
                    lbMenuName.Visible = false;
                    lbMenuNameVoid.Visible = true;
                    result = process.displayReason(FunctionID.Void_InputReason);
                }
                else if (eventName == "CreditSale")
                {
                    lbMenuName.Visible = true;
                    lbMenuNameVoid.Visible = false;
                    result = process.displayReason(FunctionID.CreditSale_CreditInputReason);
                }
                else if (eventName == "POD")
                {
                    lbMenuName.Visible = true;
                    lbMenuNameVoid.Visible = false;
                    result = process.displayReason(FunctionID.ReceivePOD_InputVoid);
                }
                else if (eventName == "Deposit")
                {
                    lbMenuName.Visible = true;
                    lbMenuNameVoid.Visible = false;
                    result = process.displayReason(FunctionID.Deposit_InputVoid);
                }
                
                System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    this.Dispose();
                    DialogResult = System.Windows.Forms.DialogResult.Retry;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SubmitReason();
        }

        private void SubmitReason()
        {
            try
            {
                if (ucDDReasonToEdit.ValueText == null)
                {
                    if (eventName == "ReturnScan")
                    {
                        string responseMessage = ProgramConfig.message.get("frmAllDisplayReason", "NotSpecifyReturnReason").message;
                        string helpMessage = ProgramConfig.message.get("frmAllDisplayReason", "NotSpecifyReturnReason").help;
                        frmNotify dialog = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);

                        //frmNotify dialog = new frmNotify(ResponseCode.Information, "โปรดเลือกเหตุผลรับคืนรายการสินค้า");
                        dialog.ShowDialog(this);
                        return;
                    }

                    else if (eventName == "ReturnInvoice")
                    {
                        string responseMessage = ProgramConfig.message.get("frmAllDisplayReason", "NotSpecifyReturnReason").message;
                        string helpMessage = ProgramConfig.message.get("frmAllDisplayReason", "NotSpecifyReturnReason").help;
                        frmNotify dialog = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);

                        //frmNotify dialog = new frmNotify(ResponseCode.Information, "โปรดเลือกเหตุผลรับคืนรายการสินค้า");
                        dialog.ShowDialog(this);
                        return;
                    }
                    else if (eventName == "Void" || eventName == "CreditSale" || eventName == "POD" || eventName == "Deposit")
                    {
                        string responseMessage = ProgramConfig.message.get("frmAllDisplayReason", "NotSpecifyVoidReason").message;
                        string helpMessage = ProgramConfig.message.get("frmAllDisplayReason", "NotSpecifyVoidReason").help;
                        frmNotify dialog = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);

                        //frmNotify dialog = new frmNotify(ResponseCode.Information, "โปรดเลือกเหตุผลยกเลิกใบเสร็จ");
                        dialog.ShowDialog(this);
                        return;
                    }
                }
                else
                {
                    string reasonID = ucDDReasonToEdit.ValueText;
                    _reasonID = ucDDReasonToEdit.ValueText;
                    _reasonTxt = ucDDReasonToEdit.DisplayText;
                    if (eventName == "ReturnScan")
                    {
                        frmReturnFromScanProduct frmReturnFromScanProduct = (frmReturnFromScanProduct)this.Owner;
                        frmReturnFromScanProduct.frmAllDisplayReasonData(reasonID);
                    }

                    else if (eventName == "ReturnInvoice")
                    {
                        frmReturnFromInvoice frmReturnFromInvoice = (frmReturnFromInvoice)this.Owner;
                        frmReturnFromInvoice.frmAllDisplayReasonData(reasonID);
                    }
                    else if (eventName == "Void" || eventName == "CreditSale" || eventName == "POD" || eventName == "Deposit")
                    {
                        frmVoid frmVoid = (frmVoid)this.Owner;
                        frmVoid.frmAllDisplayReasonData(reasonID);
                    }

                }

                this.DialogResult = DialogResult.Yes;
                this.Dispose();

            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    this.Dispose();
                    DialogResult = System.Windows.Forms.DialogResult.Retry;
                }
            }
        }

        private void frmAllDisplayReason_Shown(object sender, EventArgs e)
        {
            Utility.CropFromScreen(this, pictureBox1);
        }

        private void ucDDReasonToEdit_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            SubmitReason();
        }

    }
}
