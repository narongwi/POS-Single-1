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

namespace BJCBCPOS
{
    public partial class frmDeleteEditItem : Form
    {
        private bool IsPaint = false; 
        private SaleProcess process = new SaleProcess();
        public string productCode;
        public string productName;
        public string productQty;
        public string productPrice;
        public string discID;
        public string IsFFNRTC;
        public string _PrType;
        public string _UPCPriceDB;
        //private UCItemSell ucGV;
        public StoreResult result = null;
        public int cnt { get; set; }
        public string mode = "";
        string amtFormat = ProgramConfig.amountFormatString;
        private UCItemSell lastUCIS = new UCItemSell();

        public frmDeleteEditItem(StoreResult res, string modeText)
        {
            InitializeComponent();
            //actiontype = action;
            //productBarcode = barCode; 
            result = res;
            mode = modeText;
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

        private void frmDeleteItem_Load(object sender, EventArgs e)
        {
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

            //action = "D";
            //proDuctCode = "";
            //result = process.searchItem(productBarcode, actiontype);

            if (mode == "edit")
            {
                lbHeaderEdit.Visible = true;
                lbHeaderEdit.BringToFront();
                lbHeaderDelete.Visible = false;
            }
            else if (mode == "delete")
            {
                lbHeaderDelete.Visible = true;
                lbHeaderDelete.BringToFront();
                lbHeaderEdit.Visible = false;
            }

            if (result.response == ResponseCode.Success)
            {
                int rowCount = result.otherData.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    string code = result.otherData.Rows[i]["ProductCode"].ToString().Trim();
                    string name = result.otherData.Rows[i]["ProductName"].ToString();
                    string price = result.otherData.Rows[i]["Price"].ToString();
                    string quant = result.otherData.Rows[i]["Quant"].ToString();
                    string seq = result.otherData.Rows[i]["Seq"].ToString();
                    string discID = result.otherData.Rows[i]["DISCID"].ToString();
                    string product_type = result.otherData.Rows[i]["PRODUCT_TYPE"].ToString();
                    string IsFFRTC = product_type == "RTC" || product_type == "FF" ? "Y" : "N";
                    string amt = result.otherData.Rows[i]["AMT"].ToString();
               
                    

                    UCItemSell ucitmSell = new UCItemSell(cnt);
                    ucitmSell.UCGridViewItemSellClick += UCGridViewItemSellClick;
                    ucitmSell.lbNo.Text = seq.ToString();
                    ucitmSell.lbUC_ProductCode.Text = code;
                    ucitmSell.lbUC_Qty.Text = double.Parse(quant).ToString();
                    ucitmSell.lbUC_Discount.Visible = false;
                    ucitmSell.lbUC_Price.Text = double.Parse(price).ToString(amtFormat);
                    ucitmSell.lbDiscID.Text = discID;
                    ucitmSell.IsFreshFoodNRTC = IsFFRTC;
                    ucitmSell.PR_TYPE = product_type;
                    ucitmSell.UPCPriceDB = price;
                    //ucitmSell.lbDiscount.Text = "0.00";
                    ucitmSell.lbUC_TotalPrice.Location = new Point(400, 5);
                    ucitmSell.lbUC_TotalPrice.Text = double.Parse(amt).ToString(amtFormat);
                    ucitmSell.lbProductName.Text = name;
                    pn_item_sell.Controls.Add(ucitmSell);
                    RefreshGrid();

                }
            }
        }

        private void RefreshGrid()
        {
            List<UCItemSell> lstItemSell = new List<UCItemSell>();
            lstItemSell = pn_item_sell.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_item_sell.Controls.Clear();
            int num = lstItemSell.Count;

            foreach (UCItemSell item in lstItemSell)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num.ToString();
                pn_item_sell.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_item_sell);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void UCGridViewItemSellClick(object sender, EventArgs e)
        {
            UCItemSell ucGV = (UCItemSell)sender;
            productCode = ucGV.lbUC_ProductCode.Text;
            productName = ucGV.lbProductName.Text;
            productQty = ucGV.lbUC_Qty.Text;
            productPrice = ucGV.lbUC_Price.Text;
            discID = ucGV.lbDiscID.Text;
            IsFFNRTC = ucGV.IsFreshFoodNRTC;
            _PrType = ucGV.PR_TYPE;
            _UPCPriceDB = ucGV.UPCPriceDB;
            //ucGV.lbProductCode.Text = productCode;
            //ucGV.lbProductName.Text = productName;
            //ucGV.lbQty.Text = productQty;
            //ucGV.lbPrice.Text = productPrice;
            if (lastUCIS != ucGV)
                UCItemSell.LostFocusItem(lastUCIS);

            lastUCIS = ucGV;
             
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //frmSale frmSale = new frmSale(productCode, productName, productQty, productPrice);
            if (productCode != null)
            {
                frmSale frmSale = (frmSale)this.Owner;
                frmSale.frmDeleteEditData(productCode, productName, productQty, productPrice, mode, discID, IsFFNRTC, _PrType, _UPCPriceDB);

                Dispose();
            }
            else
            {
                string responseMessage = ProgramConfig.message.get("frmDeleteEditItem", "ChooseProduct").message;
                string helpMessage = ProgramConfig.message.get("frmDeleteEditItem", "ChooseProduct").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //frmNotify dialog = new frmNotify(ResponseCode.Error, "กรุณาเลือกรายการที่ต้องการ");
                dialog.ShowDialog(this);
                return;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmSale frmSale = (frmSale)this.Owner;
            frmSale.frmCancelEditData();

            Dispose();
        }
    }
}