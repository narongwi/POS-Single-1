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
    public partial class frmEditItemReason : Form
    {
        private bool IsPaint = false; 
        private SaleProcess process = new SaleProcess();
        string productCode;
        string productEditQuant;
        string productCurrentQuant;
        string productEditPrice;
        string productCurrentPrice;
        string editType;
        string _discID;
        string _recEdit;
        string _isFFNRTC;
        string _product_type;
        string _totalPrice;
        frmNotify notify;
        private frmSale fSale = null;

        public frmEditItemReason(string code, string editQuant,string currentQuant , string editPrice ,string currentPrice, string type, string discID, string recEdit, string isFFNRTC, string ProductType, string totalPrice)
        {            
            InitializeComponent();
            productCode = code;
            productEditQuant = editQuant;
            productCurrentQuant = currentQuant;
            productEditPrice = editPrice;
            productCurrentPrice = currentPrice;
            editType = type;
            _discID = discID;
            _recEdit = recEdit;
            _isFFNRTC = isFFNRTC;
            _product_type = ProductType;
            _totalPrice = totalPrice;
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            StoreResult result = process.displayEditItemReason();
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
                AppLog.writeLog("connection to server lost at frmEditItemReason.ucDDReasonToEdit_UCDropDownListClick");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dispose();
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

        private void frmEditReason_Load(object sender, EventArgs e)
        {
            ucDDReasonToEdit.LabelText = AppMessage.getMessage(ProgramConfig.language, "frmEditItemReason", "ucDDReasonToEdit");

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
            }
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void DrawIt()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
              100, 0, 0, 0);

            //graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string reasonID = ucDDReasonToEdit.ValueText;
                if (ucDDReasonToEdit.ValueText == null)
                {
                    string responseMessage = ProgramConfig.message.get("frmEditItemReason", "NotSpecifyEditReason").message;
                    string helpMessage = ProgramConfig.message.get("frmEditItemReason", "NotSpecifyEditReason").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //frmNotify dialog = new frmNotify(ResponseCode.Information, "ไม่พบเหตุผลปรับรายการสินค้า", "โปรดเลือกเหตุผลปรับรายการสินค้า");
                    dialog.ShowDialog(this);
                    return;
                }
                else
                {
                    if (editType == "Single")
                    {
                        frmLoading.showLoading();
                        StoreResult res = process.saveEditWithReasonSingle(reasonID, productCode, productEditQuant, productCurrentPrice, productEditPrice, _recEdit, _isFFNRTC, _product_type, _totalPrice);
                        frmLoading.closeLoading();
                        notify = new frmNotify(res);
                        notify.ShowDialog(this);
                        if (!res.response.next)
                        {
                            return;
                        }
                    }
                    else if (editType == "All")
                    {
                        frmLoading.showLoading();
                        StoreResult res = process.saveEditWithReasonAll(reasonID, productCode, productCurrentQuant, productEditQuant, productCurrentPrice, productEditPrice, _discID, _recEdit, _isFFNRTC, _product_type, _totalPrice);
                        frmLoading.closeLoading();
                        notify = new frmNotify(res);
                        notify.ShowDialog(this);
                        if (!res.response.next)
                        {
                            return;
                        }
                    }
                }
                //Dispose();
                //Close();
                this.Dispose();
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

        private void ucDDReasonToEdit_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            pn_DropDown.Visible = false;
        }

        private void frmEditItemReason_Shown(object sender, EventArgs e)
        {
            Utility.CropFromScreen(this, pictureBox1);
        }
    }
}
