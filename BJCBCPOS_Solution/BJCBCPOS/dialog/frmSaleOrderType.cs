using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmSaleOrderType : Form
    {
        frmSaleProcess fSaleProc;
        frmSale fSale;
        Sale_TypeCode saleTypeCode;

        string orderStep;
        private bool IsPaint = false;
        private DataTable _dtSaleOrderMenu;
        private string _relateFlag;
        private string _menuID;
        private string _tempValue;

        public frmSaleOrderType()
        {
            InitializeComponent();
        }

        public frmSaleOrderType(DataTable dtSaleOrderMenu)
        {
            InitializeComponent();
            _dtSaleOrderMenu = dtSaleOrderMenu;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), new Size(1024, 768)));
                    IsPaint = true;
                }
            }
        }

        private void frmSaleOrderType_Load(object sender, EventArgs e)
        {
            if (this.Owner is frmSale)
            {
                fSale = (frmSale)this.Owner;
                fSaleProc = new frmSaleProcess((frmSale)this.Owner);
            }

            this.Size = new Size(1024, 768);
            this.Location = new Point(0, 0);

            if (_dtSaleOrderMenu != null && _dtSaleOrderMenu.Rows.Count > 0)
            {
                _relateFlag = _dtSaleOrderMenu.Rows[0]["RELATE_FLAG"].ToString();
                _menuID = _dtSaleOrderMenu.Rows[0]["MENU_ID"].ToString();
            }

            btnSkip.Visible = _relateFlag == "N";

            orderStep = SaleOrderTypeStep.OrderType;
            GenItemList(orderStep, "N/A");
        }

        public void itm_ItemClick(object sender, EventArgs e)
        {
            ItemClick(false, sender);
        }

        private void ItemClick(bool isSkip, object sender)
        {
            UCGeneralListItem itm = (UCGeneralListItem)sender;
            if (orderStep != "")
            {
                if (!isSkip)
                {
                    SetDataSaleTypeCode(itm, orderStep);
                }
                else
                {
                    SetDataSaleTypeCode(null, orderStep);
                }

                if (orderStep == SaleOrderTypeStep.OrderType)
                {
                    orderStep = SaleOrderTypeStep.DeliveryType;
                    if (!GenItemList(orderStep, itm.ValueText.ToString()))
                    {
                        orderStep = SaleOrderTypeStep.OrderType;
                    }
                }
                else if (orderStep == SaleOrderTypeStep.DeliveryType)
                {
                    orderStep = SaleOrderTypeStep.MediaType;
                    if (!GenItemList(orderStep, itm.ValueText.ToString()))
                    {
                        orderStep = SaleOrderTypeStep.DeliveryType;
                    }
                }
                else if (orderStep == SaleOrderTypeStep.MediaType)
                {
                    AlertConfirm();
                }
                else
                {
                    orderStep = "";
                }
            }
        }

        private void AlertConfirm()
        {
            if (saleTypeCode.OrderType != 0 || saleTypeCode.DeliveryType != 0 || saleTypeCode.MediaType != 0)
            {
                frmNotify notify = new frmNotify(ResponseCode.Warning, String.Format("ยืนยันการเลือก\n{0}\n{1}\n{2}"
                    , saleTypeCode.OrderTypeDesc != "" && saleTypeCode.OrderTypeDesc != null && saleTypeCode.OrderType != 0
                                    ? String.Format("วิธีการสั่งสินค้า {0}", saleTypeCode.OrderTypeDesc) : ""
                    , saleTypeCode.DeliveryTypeDesc != "" && saleTypeCode.DeliveryTypeDesc != null && saleTypeCode.DeliveryType != 0
                                    ? String.Format("วิธีการรับสินค้า {0}", saleTypeCode.DeliveryTypeDesc) : ""
                    , saleTypeCode.MediaTypeDesc != "" && saleTypeCode.MediaTypeDesc != null && saleTypeCode.MediaType != 0
                                    ? String.Format("ช่องทาง {0}", saleTypeCode.MediaTypeDesc) : ""));
                var dialogRes = notify.ShowDialog();
                if (dialogRes == System.Windows.Forms.DialogResult.Yes)
                {
                    var result = fSaleProc.insertSALEORDERTYPE_TRANS(saleTypeCode.OrderType, saleTypeCode.DeliveryType, saleTypeCode.MediaType);
                    if (result.response.next)
                    {
                        fSale.SetSaleTypeCode(saleTypeCode);
                        //Fix language
                        Utility.AlertMessage(ResponseCode.Success, "บันทึกประเภทสินค้า เรียบร้อยแล้ว");
                        this.Dispose();
                    }
                }
            }
            else
            {
                this.Dispose();
            }

        }

        private void SetDataSaleTypeCode(UCGeneralListItem itm, string ordstp)
        {
            if (ordstp == SaleOrderTypeStep.OrderType)
            {
                saleTypeCode.OrderType = itm == null ? 0 : itm.ValueText;
                saleTypeCode.OrderTypeDesc = itm == null ? "" : itm.lbTxtDesc.Text;
            }
            else if (ordstp == SaleOrderTypeStep.DeliveryType)
            {
                saleTypeCode.DeliveryType = itm == null ? 0 : itm.ValueText;
                saleTypeCode.DeliveryTypeDesc = itm == null ? "" : itm.lbTxtDesc.Text;
            }
            else if (ordstp == SaleOrderTypeStep.MediaType)
            {
                saleTypeCode.MediaType = itm == null ? 0 : itm.ValueText;
                saleTypeCode.MediaTypeDesc = itm == null ? "" : itm.lbTxtDesc.Text;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (orderStep == SaleOrderTypeStep.MediaType)
            {
                orderStep = SaleOrderTypeStep.DeliveryType;
                if (!GenItemList(orderStep, _tempValue))
                {
                    orderStep = SaleOrderTypeStep.MediaType;
                }
                return;
            }
            if (orderStep == SaleOrderTypeStep.DeliveryType)
            {
                orderStep = SaleOrderTypeStep.OrderType;
                if (!GenItemList(orderStep, _tempValue))
                {
                    orderStep = SaleOrderTypeStep.DeliveryType;
                }
                return;
            }
            else if (orderStep == SaleOrderTypeStep.OrderType)
            {
                this.Dispose();
            }
        }

        private bool GenItemList(string type, string value)
        {
            bool IsComplete = false;

            if (fSaleProc != null)
            {
                _tempValue = value;
                StoreResult res = fSaleProc.GetSaleOrderType(type, _menuID, _relateFlag, GetLevelSaleOrderType(type), saleTypeCode.OrderType.ToString(), saleTypeCode.DeliveryType.ToString());

                if (res.response == ResponseCode.Success && res.otherData.Rows[0]["ErrorCode"].ToString() == "000")
                {
                    IsComplete = true;
                    panel1.Controls.Clear();
                    int valueIn = 0;
                    string desc = "";
                    foreach (DataRow dr in res.otherData.Rows)
                    {
                        int.TryParse(dr["TYPE_CODE"].ToString(), out valueIn);
                        desc = valueIn.ToString() + " - " + dr["TYPE_DESC"].ToString();

                        UCGeneralListItem itm = new UCGeneralListItem();
                        itm.lbTxtDesc.Font = new Font(itm.lbTxtDesc.Font.FontFamily, 16f, itm.lbTxtDesc.Font.Style, itm.lbTxtDesc.Font.Unit, itm.lbTxtDesc.Font.GdiCharSet);
                        itm.ValueText = valueIn;
                        itm.lbTxtDesc.Text = desc;
                        itm.SaleOrderType = type;
                        itm.ItemClick += itm_ItemClick;
                        panel1.Controls.Add(itm);
                        panel1.Controls.SetChildIndex(itm, 0);
                    }

                    //Fix Language
                    if (type == SaleOrderTypeStep.OrderType)
                    {
                        lbHeader.Text = "วิธีสั่งสินค้า";
                    }
                    else if (type == SaleOrderTypeStep.DeliveryType)
                    {
                        lbHeader.Text = "วิธีรับสินค้า";
                    }
                    else if (type == SaleOrderTypeStep.MediaType)
                    {
                        lbHeader.Text = "ช่องทาง";
                    }
                }
                else if (res.response == ResponseCode.Success && res.otherData.Rows[0]["ErrorCode"].ToString() == "200")
                {
                    AlertConfirm();
                }
            }

            return IsComplete;
        }

        private string GetLevelSaleOrderType(string type)
        {
            if (_relateFlag == "Y")
            {
                if (type == SaleOrderTypeStep.OrderType)
                {
                    return "1";
                }
                else if (type == SaleOrderTypeStep.DeliveryType)
                {
                    return "2";
                }
                else if (type == SaleOrderTypeStep.MediaType)
                {
                    return "3";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }

        }

        private string GetValueOrder(string type, string value)
        {
            if (type == SaleOrderTypeStep.DeliveryType)
            {
                return value;
            }
            else
            {
                if (saleTypeCode.OrderType != 0)
                {
                    return saleTypeCode.OrderType.ToString();
                }
            }

            return "";
        }

        private string GetValueDelivery(string type, string value)
        {
            if (type == SaleOrderTypeStep.MediaType)
            {
                return value;
            }
            else
            {
                if (saleTypeCode.DeliveryType != 0)
                {
                    return saleTypeCode.DeliveryType.ToString();
                }
            }

            return "";
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            ItemClick(true, null);
        }


    }
}























