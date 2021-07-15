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
using System.Collections;

namespace BJCBCPOS
{
    public partial class frmDeleteReason : Form
    {
        private bool IsPaint = false; 
        private SaleProcess process = new SaleProcess();
        public frmMonitorCustomer frmMonitor;
        public frmMonitor2Detail frm2Detail;
        private frmNotify notify;
        private frmSale fSale = null;

        public frmDeleteReason()
        {
            InitializeComponent();
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            frmLoading.showLoading();
            StoreResult result = process.displayDeleteReason();
            frmLoading.closeLoading();
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

        private void ucDDReasonToDelete_UCDropDownListClick(object sender, EventArgs e)
        {
            try
            {
                //frmLoading.showLoading();
                if (sender is UCDropDownList)
                {
                    var ucDDL = (UCDropDownList)sender;
                    ucDDL.lstDDL = SetDataucDropDownList2();
                }
                //frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at frmDeleteReason.ucDDReasonToDelete_UCDropDownListClick");
                fSale.CatchNetWorkConnectionException(net);
                frmLoading.closeLoading();
                this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
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

        private void frmDeleteReason_Load(object sender, EventArgs e)
        {
            ucDDReasonToDelete.LabelText = AppMessage.getMessage(ProgramConfig.language, "frmDeleteReason", "ucDDReasonToDelete");
            if (fSale == null)
            {
                Form form = Application.OpenForms["frmSale"];
                fSale = form as frmSale;
            }

            AppMessage.fillForm(ProgramConfig.language, this);
            if (this.Owner != null)
            {
               // Utility.SetBackGroundBrightness(this.Owner, pictureBox1);

                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);

                this.Location = this.Owner.Location;
            }
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //string r = "";
            try
            {
                var a = ucDDReasonToDelete.ValueText;
                if (a != null)
                {
                    frmLoading.showLoading();
                    StoreResult resSaveCancelTran = process.saveCancelSaleTransaction(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction, ucDDReasonToDelete.ValueText);
                    frmLoading.closeLoading();
                    Profile checkPro = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank);

                    if (resSaveCancelTran.response.next)
                    {
                        this.DialogResult = DialogResult.Yes;
                        printCancel(FunctionID.Sale_CancelWhileSale_CancelOrder_PrintCancelDocument);
                        if (checkPro.policy == PolicyStatus.Work) //2
                        {
                            process.syncToDataTank("Cancel", FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.saleRefNo, "1");
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.No;
                        notify = new frmNotify(ResponseCode.Error, resSaveCancelTran.responseMessage, resSaveCancelTran.helpMessage);
                        notify.ShowDialog(this);
                        return;
                    }
                }
                else
                {
                    string responseMessage = ProgramConfig.message.get("frmDeleteReason", "NotSpecifyCancelReason").message;
                    string helpMessage = ProgramConfig.message.get("frmDeleteReason", "NotSpecifyCancelReason").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่พบเเหตุผลยกเลิกการขาย", "โปรดเลือกเหตุผลยกเลิกการขาย");
                    dialog.ShowDialog(this);
                    return;
                }
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at frmDeleteReason.btnOk_Click");
                fSale.CatchNetWorkConnectionException(net);
                frmLoading.closeLoading();
                this.Dispose();
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void printCancel(FunctionID function)
        {
            Profile check = ProgramConfig.getProfile(function);
            if (check.policy == PolicyStatus.Skip) //1
            {
                closeForm();
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                StoreResult result = process.printCancel(function);
                if (!result.response.next)
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                    return;
                }

                DataTable dt = result.otherData;
                Hardware.printTermal(dt);
                this.Dispose();

                //ProgramConfig.cnNo =  "CN" + (Convert.ToInt32(ProgramConfig.cnNo) + 1).ToString("D9");
                //ProgramConfig.running.updateValue();

                closeForm();
            }
        }

        private void closeForm()
        {
            ProgramConfig.salePageState = 2;

            Program.control.CloseForm("frmPayment");
            Program.control.CloseForm("frmDeleteReason");

            //Program.control.CloseForm("frmMonitorCustomer");
            //Program.control.CloseForm("frmMonitorCustomerFooter");
            //Program.control.ShowForm("frmMonitorCustomer");
            //Program.control.ShowForm("frmMonitorCustomerFooter");
            //Program.control.ShowForm("frmMonitor2Detail");

            Form form = Application.OpenForms["frmMonitorCustomer"];
            frmMonitorCustomer mon = form as frmMonitorCustomer;
            mon.clearForm();

            Form form2 = Application.OpenForms["frmMonitor2Detail"];
            frmMonitor2Detail mon2 = form2 as frmMonitor2Detail;
            mon2.clearForm();

            //Program.control.ShowForm("frmSale");
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Dispose();
        }

        private void frmDeleteReason_Shown(object sender, EventArgs e)
        {
            Utility.CropFromScreen(this, pictureBox1);            
        }
    }
}
