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
    public partial class frmDeleteItemReason : Form
    {
        private bool IsPaint = false; 
        private SaleProcess process = new SaleProcess();
        string productCode;
        string productQuant;
        string productDeleteQuant;
        string productPrice;
        string deleteType;
        string _discID;
        frmNotify notify;
        private frmSale fSale = null;

        public frmDeleteItemReason(string code, string quant,string deleteQuant, string price, string type, string discID)
        {                    
            InitializeComponent();
            productCode = code;
            productQuant = quant;
            productDeleteQuant = deleteQuant;
            productPrice = price;
            deleteType = type;
            _discID = discID;
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            StoreResult result = process.displayDeleteItemReason();
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
                frmLoading.showLoading();
                if (sender is UCDropDownList)
                {
                    var ucDDL = (UCDropDownList)sender;
                    ucDDL.lstDDL = SetDataucDropDownList2();
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at frmDeleteItemReason.ucDDReasonToDelete_UCDropDownListClick");
                fSale.CatchNetWorkConnectionException(net);
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

        private void frmDeleteItemReason_Load(object sender, EventArgs e)
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
                this.Size = this.Owner.Size;
                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);

                this.Location = this.Owner.Location;
                //this.pictureBox1.Size = this.Owner.Size;
            }
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {             
                string reasonID = ucDDReasonToDelete.ValueText;
                if (ucDDReasonToDelete.ValueText == null)
                {
                    string responseMessage = ProgramConfig.message.get("frmDeleteItemReason", "NotSpecifyDeleteReason").message;
                    string helpMessage = ProgramConfig.message.get("frmDeleteItemReason", "NotSpecifyDeleteReason").help;
                    notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Information, "ไม่พบเหตุผลลบรายการสินค้า", "โปรดเลือกเหตุผลลบรายการสินค้า");
                    notify.ShowDialog(this);
                    return;
                }
                else
                {

                    this.Refresh();
                    if (deleteType == "Single")
                    {
                        frmLoading.showLoading();
                        StoreResult res = process.saveDeleteWithReasonSingle(reasonID, productCode, productQuant, productPrice);
                        frmLoading.closeLoading();
                        notify = new frmNotify(res);
                        notify.ShowDialog(this);
                        if (!res.response.next)
                        {
                            return;
                        }
                    }
                    else if (deleteType == "All")
                    {
                        frmLoading.showLoading();
                        StoreResult res = process.saveDeleteWithReasonAll(reasonID, productCode, productQuant, productDeleteQuant, productPrice, _discID);
                        frmLoading.closeLoading();
                        notify = new frmNotify(res);
                        notify.ShowDialog(this);
                        if (!res.response.next)
                        {
                            return;
                        }
                    }
                    Dispose();
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at frmDeleteItemReason.btnOk_Click");
                if (fSale.CatchNetWorkConnectionException(net))
                {
                    frmLoading.closeLoading();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void frmDeleteItemReason_Shown(object sender, EventArgs e)
        {
            Utility.CropFromScreen(this, pictureBox1);
        }

    }
}
