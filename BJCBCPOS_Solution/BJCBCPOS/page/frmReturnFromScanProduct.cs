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
    public partial class frmReturnFromScanProduct : Form
    {
        private bool chkOpenDrawer = false;
        private ReturnProcess process = new ReturnProcess();
        private UCItemReturn ucGV;
        private UCItemReturn lastUCIS = new UCItemReturn();

        string qty = "";
        string r = "";
        int faq = 0;
        int cashierMode = 0;
        double amtPrice = 0;
        double returnPrice = 0;
        public string lbTxtNo;
        public string code;
        public string name;
        public string price;
        public string quant;
        public string currentPrice;
        public string currentAmt;
        public string seq;
        public string memberID;
        public string memberName;
        public string reasonId;
        public string returnType = "P";
        frmPayment frmPm = null;
        Form form = null;
        string openTime = "";
        string closeTime = "";
        string taxId;
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public int cnt { get; set; }
        public frmMonitorCustomer frmMoCus;
        public UCTextBoxWithIcon ucTBWI { get; set; }
        public UCTextBoxSmall ucTBS { get; set; }

        public DataTable TEMPDLYPTRANSPARTIAL = new DataTable();

        string displayAmt = ProgramConfig.amountFormatString;
        string _reasonId;
        string _reasonTxt;

        public frmReturnFromScanProduct()
        {
            InitializeComponent();
            InitalDatatable();
        }

        private void frmReturnFromScanProduct_Load(object sender, EventArgs e)
        {
            try
            {
                Utility.InitialTextBoxIcon(ucTBScanBarcodeRet, BJCBCPOS.Properties.Resources.icon_textbox_scan, UCTextBoxIconType.ScanAndDelete, IconType.Scan, "กรุณาระบุรหัสสินค้า");
                StoreResult result = null;
                //panelScanBarcode.BringToFront();
                //ucTBScanBarcode.Focus();

                Utility.GlobalClear();
                ClearMember();
                DisableControl();

                result = process.getRunning(FunctionID.Return_GetRunningNo, RunningReceiptID.ReturnRef);
                string refNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.returnRefNo = refNo;
                ProgramConfig.returnRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                lbTxtRefNo.Text = refNo;

                Profile openDay = ProgramConfig.getProfile(FunctionID.Return_CheckOpenDayofTillStatus);
                if (openDay.policy == PolicyStatus.Work)
                {
                    StoreResult checkOpenDay = process.checkOpenDay();
                    if (!checkOpenDay.response.next)
                    {
                        frmNotify dialog = new frmNotify(checkOpenDay);
                        dialog.ShowDialog(this);
                        this.Dispose();
                        return;
                    }
                }

                //DisplayContent อย่าลืม
                result = process.posDisplayContent();
                if (result.response.next)
                {
                    if (result.otherData.Rows.Count > 0)
                    {
                        if (result.otherData.Columns.Contains("Content_Default"))
                        {
                            ucFooterTran1.mainContent = result.otherData.Rows[0]["Content_Default"].ToString();
                        }
                        if (result.otherData.Columns.Contains("Content_Detail"))
                        {
                            ucFooterTran1.fullContent = result.otherData.Rows[0]["Content_Detail"].ToString();
                        }
                        ucFooterTran1.functionId = FunctionID.Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeCode.formatValue;
                    }
                }

                Program.control.ShowForm("frmMonitorCustomer");
                Program.control.ShowForm("frmMonitorCustomerFooter");

                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmMonitorCustomer)
                    {
                        frmMoCus = (frmMonitorCustomer)item;
                        break;
                    }
                }

                CheckItemSell();

                btnReturn.Enabled = false;
                btnReturn.BackgroundImage = Properties.Resources.payment_disable;

                lbPointReceive.Visible = ProgramConfig.memberFormat != MemberFormat.MegaMaket;
                ucTxtPointReceive.Visible = ProgramConfig.memberFormat != MemberFormat.MegaMaket;

                ucHeader1.btnMember_Click(sender, e);
                ucKeypad.ucTBWI.Text = "";
                ucKeypad.ucTBWI.FocusTxt();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
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

        public void keyProduct()
        {
            if (ucTBScanBarcodeRet.Text.Length < 13 && ucTBScanBarcodeRet.Text.Length != 0)
            {
                ucTBScanBarcodeRet.Text = ucTBScanBarcodeRet.Text.PadLeft(13, '0');
            }

            if (pn_Item_Return.Controls.Count > 0 && ucTBScanBarcodeRet.Text == "")
            {
                //CalDiscount
                if (Convert.ToDouble(lbTxtTotal.Text) > 0)
                {
                    btnReturn.Enabled = true;
                    btnReturn.BackgroundImage = Properties.Resources.payment_enable;
                }
            }
            else
            {
                btnReturn.Enabled = false;
                btnReturn.BackgroundImage = Properties.Resources.payment_disable;

                string check = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Quantity_Format.parameterCode);
                if (check == "1")
                {
                    if (ucTxtQty.Text == "")
                    {
                        qty = "1";
                    }
                    else
                    {
                        qty = ucTxtQty.Text;
                    }
                }
                else if (check == "2")
                {
                    if (ucTxtQty.Text == "")
                    {
                        qty = "1.00";
                    }
                    else
                    {
                        //แปลงให้เป็น 2ตำแหน่ง
                        qty = double.Parse(ucTxtQty.Text).ToString("0.00");
                    }
                }
                else if (check == "3")
                {
                    if (ucTxtQty.Text == "")
                    {
                        qty = "1.000";
                    }
                    else
                    {
                        //แปลงให้เป็น 3 ตำแหน่ง
                        qty = double.Parse(ucTxtQty.Text).ToString("0.000");
                    }
                }

                StoreResult result = process.getProductDesc(ucTBScanBarcodeRet.Text);

                if (result.response == ResponseCode.Success)
                {
                    if (result.otherData != null)
                    {
                        for (int i = 0; i < result.otherData.Rows.Count; i++)
                        {
                            string isFFNRTC = "N";
                            string code = result.otherData.Rows[i]["PR_CODE"].ToString();
                            string name = result.otherData.Rows[i]["PR_NAME"].ToString();
                            string price = result.otherData.Rows[i]["PR_PRICE"].ToString();
                            string dbPrice = result.otherData.Rows[i]["PR_PRICE"].ToString();
                            string amt = (double.Parse(qty) * double.Parse(price)).ToString(displayAmt);
                            string vat = result.otherData.Rows[i]["PR_VAT"].ToString();
                            string vty = "";
                            if (vat == "V")
                            {
                                vty = "1";
                            }
                            else
                            {
                                vty = "0";
                            }
                            string promo = result.otherData.Rows[i]["Promotion"].ToString();
                            string promoPrice = result.otherData.Rows[i]["PricePromotion"].ToString();
                            string barcode = result.otherData.Rows[i]["BARCODE"].ToString();
                            string pdisc = result.otherData.Rows[i]["PDISC"].ToString();
                            string discid = result.otherData.Rows[i]["DISCID"].ToString();
                            string discamt = result.otherData.Rows[i]["DISCAMT"].ToString();
                            string dty = result.otherData.Rows[i]["PR_DTYPE"].ToString();
                            string stv = result.otherData.Rows[i]["PR_Type"].ToString();
                            //qty = 1; 

                            bool chkWeight = Convert.ToDouble(result.otherData.Rows[i]["WEIGHT"].ToString()) > 0;
                            if (result.otherData.Rows[i]["PRODUCT_TYPE"].ToString() == "FF")
                            {
                                isFFNRTC = chkWeight ? "Y" : "N";
                            }
                            else
                            {
                                isFFNRTC = result.otherData.Rows[i]["PRODUCT_TYPE"].ToString() == "RTC" ? "Y" : "N";
                            }

                            if (isFFNRTC == "Y")
                            {
                                qty = double.Parse(result.otherData.Rows[i]["WEIGHT"].ToString()).ToString();
                                amt = double.Parse(result.otherData.Rows[i]["AMT"].ToString()).ToString(displayAmt);
                                price = (Convert.ToDouble(amt) /  Convert.ToDouble(qty)).ToString(displayAmt);
                            }


                            //SaveTemp
                            //StoreResult res = process.saveTempDlyptrans(ProgramConfig.returnRefNo, cnt.ToString(), "3", vty, barcode, qty, amt, "0.00", ProgramConfig.userId
                            //    , "0", "", "0.00", "0", price, "1", "0.00", "0", "");
                            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, cnt.ToString(), "3", vty, barcode
                                                , qty, amt, "0", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", cultureinfo), ProgramConfig.userId, "0"
                                                , "", "", "0", "0.00", "0", "0.00", price, dty, check, name, "", "");


                            UCMonitor2Item ucitm = new UCMonitor2Item(cnt);
                            ucitm.lbNo.Text = cnt.ToString();
                            ucitm.lb_ITEM.Text = name;
                            ucitm.lb_AMT.Text = amt;
                            ucitm.lb_QTY.Text = qty.ToString();
                            frmMoCus.pn_Item.Controls.Add(ucitm);

                            UCItemReturn ucitmReturn = new UCItemReturn(cnt);
                            ucitmReturn.UCGridViewItemSellClick += UCGridViewItemSellClick;
                            ucitmReturn.Rec = cnt.ToString();
                            ucitmReturn.lbNo.Text = cnt.ToString();
                            ucitmReturn.lbProductCode.Text = code;
                            ucitmReturn.lbQty.Text = qty;
                            ucitmReturn.lbPrice.Text = Convert.ToDouble(dbPrice).ToString(displayAmt);
                            ucitmReturn.lbReturnPrice.Text = Convert.ToDouble(price).ToString(displayAmt);
                            ucitmReturn.lbTotalPrice.Text = amt;
                            ucitmReturn.lbProductName.Text = name;
                            pn_Item_Return.Controls.Add(ucitmReturn);

                            cnt++;

                            amtPrice += double.Parse(ucitmReturn.lbTotalPrice.Text);
                            lbTxtSubtotal.Text = amtPrice.ToString("0.00");
                            returnPrice += (double.Parse(ucitmReturn.lbReturnPrice.Text) * double.Parse(ucitmReturn.lbQty.Text));
                            lbTxtTotal.Text = returnPrice.ToString("0.00");

                            frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString("0.00");
                            frmMoCus.lbTxtDiscount.Text = "0.00";
                            frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString("0.00");
                            RefreshGrid();

                        }
                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                        dialog.ShowDialog(this);
                        ucTBScanBarcodeRet.Text = "";
                        ucTxtQty.Text = "";
                        ucTBScanBarcodeRet.Focus();
                        return;
                    }
                }
                else if (result.response == ResponseCode.Error)
                {
                    frmNotify dialog = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                    dialog.ShowDialog(this);
                    ucTBScanBarcodeRet.Text = "";
                    ucTxtQty.Text = "";
                    ucTBScanBarcodeRet.Focus();
                    return;
                }

                CheckItemSell();

                ucTBScanBarcodeRet.Text = "";
                ucTxtQty.Text = "";
                ucTBScanBarcodeRet.Focus();
            }
            DisableControl();
        }

        public void UCGridViewItemSellClick(object sender, EventArgs e)
        {
            btnReturn.Enabled = false;
            btnReturn.BackgroundImage = Properties.Resources.payment_disable;
            btnConfirm.BringToFront();
            panelEditPrice.BringToFront();
            ucGV = (UCItemReturn)sender;

            lbNo.Text = ucGV.lbNo.Text;
            lbTxtNo = ucGV.lbNo.Text;
            lbTxtProductCode.Text = ucGV.lbProductCode.Text;
            code = ucGV.lbProductCode.Text;
            lbTxtDesc.Text = ucGV.lbProductName.Text;
            qty = ucGV.lbQty.Text;
            name = ucGV.lbProductName.Text;
            ucReturnPrice.Text = ucGV.lbReturnPrice.Text;
            ucReturnPrice.Focus();
            lbCurrentPrice.Text = "ราคาเดิม " + ucGV.lbReturnPrice.Text + " บาท";
            currentPrice = ucGV.lbReturnPrice.Text;
            currentAmt = ucGV.lbTotalPrice.Text;

            if (lastUCIS != ucGV)
                UCItemReturn.LostFocusItem(lastUCIS);

            lastUCIS = ucGV;
        }

        private void RefreshGrid()
        {
            List<UCItemReturn> lstItemReturn = new List<UCItemReturn>();
            lstItemReturn = pn_Item_Return.Controls.Cast<UCItemReturn>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_Item_Return.Controls.Clear();
            int num = lstItemReturn.Count;
            double a = 0;

            foreach (UCItemReturn item in lstItemReturn)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = Color.White;
                }

                TEMPDLYPTRANSPARTIAL.Rows[num - 1]["REC"] = num;
                TEMPDLYPTRANSPARTIAL.AcceptChanges();

                item.Rec = num.ToString();
                item.lbNoText = num.ToString();
                a += double.Parse(item.lbQtyText);
                pn_Item_Return.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_Item_Return);
            ProgramConfig.qntValue = a.ToString();

            List<UCMonitor2Item> lstMonitor2ItemReturn = new List<UCMonitor2Item>();
            lstMonitor2ItemReturn = frmMoCus.pn_Item.Controls.Cast<UCMonitor2Item>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            frmMoCus.pn_Item.Controls.Clear();
            int number = lstMonitor2ItemReturn.Count;

            foreach (UCMonitor2Item monitor2Item in lstMonitor2ItemReturn)
            {
                if (number % 2 != 0)
                {
                    monitor2Item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    monitor2Item.BackColor = Color.White;
                }
                monitor2Item.lbNoText = number.ToString();
                frmMoCus.pn_Item.Controls.Add(monitor2Item);
                number--;
            }
            ScrollToBottom(frmMoCus.pn_Item);

            CheckItemSell();
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void ucTBScanBarcode_EnterFromButton(object sender, EventArgs e)
        {
            //DataTable selectCount = process.selectMaxRecReturnTempDlyptrans();

            if (TEMPDLYPTRANSPARTIAL.Rows.Count > 0)
            {
                r = (TEMPDLYPTRANSPARTIAL.Rows[TEMPDLYPTRANSPARTIAL.Rows.Count - 1]["REC"]).ToString();
            }
            else
            {
                r = "0";
            }
            cnt = int.Parse(r) + 1;
            keyProduct();
        }

        private void ucTBScanBarcode_TextBoxKeydown(object sender, EventArgs e)
        {
            //DataTable selectCount = process.selectMaxRecReturnTempDlyptrans();
            if (TEMPDLYPTRANSPARTIAL.Rows.Count > 0)
            {
                r = (TEMPDLYPTRANSPARTIAL.Rows[TEMPDLYPTRANSPARTIAL.Rows.Count - 1]["REC"]).ToString();
            }
            else
            {
                r = "0";
            }
            cnt = int.Parse(r) + 1;
            keyProduct();
        }

        private void ucTxtQty_EnterFromButton(object sender, EventArgs e)
        {
            string check = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Quantity_Limit.parameterCode);
            if (double.Parse(ucTxtQty.Text) > double.Parse(check))
            {
                string alertMsg = ProgramConfig.message.get("frmReturnFromScanProduct", "QuantityInvalid").message;
                string helpMsg = ProgramConfig.message.get("frmReturnFromScanProduct", "QuantityInvalid").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, alertMsg, String.Format(helpMsg, check));
                dialog.ShowDialog(this);
                ucTxtQty.Text = "";
                ucTxtQty.Focus();
                return;

                
                //if (ProgramConfig.language == Language.THAI)
                //{
                //    frmNotify dialog = new frmNotify(ResponseCode.Error, "จำนวนสินค้าไม่ถูกต้อง.", "จำกัดจำนวนขายสินค้าต่อรายการสูงสุด ไม่เกิน " + check + " หน่วย");
                //    dialog.ShowDialog(this);
                //    ucTxtQty.Text = "";
                //    ucTxtQty.Focus();
                //    return;
                //}
                //else if (ProgramConfig.language == Language.ENGLISH)
                //{
                //    frmNotify dialog = new frmNotify(ResponseCode.Error, "Invalid quantity of products.", "Limit the number of products sold per item, up to " + check + " Unit");
                //    dialog.ShowDialog(this);
                //    ucTxtQty.Text = "";
                //    ucTxtQty.Focus();
                //    return;
                //}
                //else if (ProgramConfig.language == Language.LAOS)
                //{
                //    frmNotify dialog = new frmNotify(ResponseCode.Error, "ປະລິມານສິນຄ້າບໍ່ຖືກຕ້ອງ.", "ຈຳ ກັດ ຈຳ ນວນຜະລິດຕະພັນທີ່ຂາຍຕໍ່ສິນຄ້າ, ຂື້ນໄປ " + check + " ໜ່ວຍ");
                //    dialog.ShowDialog(this);
                //    ucTxtQty.Text = "";
                //    ucTxtQty.Focus();
                //    return;
                //}
                
            }
            else
            {
                ucTBScanBarcodeRet.Focus();
            }
        }

        private void btnMultiplyItem_Click(object sender, EventArgs e)
        {
            DisableControl();
            if (btnMultiplyItem.Tag == null || (string)btnMultiplyItem.Tag == "disable")
            {
                lbQty.Visible = true;
                ucTxtQty.Visible = true;
                ucTxtQty.Focus();
                btnMultiplyItem.BackgroundImage = Properties.Resources.btn_Search_ReturnFromInvoice;
                btnMultiplyItem.ForeColor = Color.White;
                btnMultiplyItem.Tag = "enable";
            }
            else
            {
                btnMultiplyItem.Tag = "disable";
                ucTBScanBarcodeRet.Focus();
            }
        }

        private void DisableControl()
        {
            btnMultiplyItem.BackgroundImage = Properties.Resources.multi_disable;

            btnMultiplyItem.ForeColor
                = Color.Gray;
            ucTxtQty.Visible = false;
            lbQty.Visible = false;
            ucTxtQty.Text = "";
            ucReturnPrice.Text = "";
            ucTBScanBarcodeRet.Text = "";
            if (pn_Item_Return.Controls.Count == 0)
            {
                //CalDiscount
                btnReturn.Enabled = false;
                btnReturn.BackgroundImage = Properties.Resources.payment_disable;
            }

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            ucTBScanBarcodeRet.Focus();
        }

        private void ucReturnPrice_EnterFromButton(object sender, EventArgs e)
        {
            if (ucReturnPrice.Text != currentPrice)
            {
                //DataTable selectCount = process.selectMaxRecReturnTempDlyptrans();
                //r = selectCount.Rows[0]["REC"].ToString();
                //cnt = int.Parse(r) + 1;
                DataRow dr = TEMPDLYPTRANSPARTIAL.Select("REF = '" + ProgramConfig.returnRefNo + "' and STCODE = '" + ProgramConfig.storeCode
                + "' and ISNULL(STT, '') <> 'V' and VTY in ('0','1') and REC >= 0 and REC < 2000 and PCD = '" + code + "' and QNT = '" + qty + "' and AMT = '" + currentAmt+ "'").FirstOrDefault();
                //process.selectReturnItemTempDlyptrans(ProgramConfig.returnRefNo, code, qty, currentAmt);
                //string stcode = selectDeleteItem.Rows[0]["STCODE"].ToString();
                //string refNo = selectDeleteItem.Rows[0]["REF"].ToString();
                //string rec = selectDeleteItem.Rows[0]["REC"].ToString();
                //string sty = selectDeleteItem.Rows[0]["STY"].ToString();
                //string vty = selectDeleteItem.Rows[0]["VTY"].ToString();
                //string pcd = selectDeleteItem.Rows[0]["PCD"].ToString();
                //string qnt = selectDeleteItem.Rows[0]["QNT"].ToString();
                //string amt = selectDeleteItem.Rows[0]["AMT"].ToString();
                //string newAmt = (double.Parse(ucReturnPrice.Text) * double.Parse(qty)).ToString();
                //string fds = selectDeleteItem.Rows[0]["FDS"].ToString();
                //string ttm = selectDeleteItem.Rows[0]["TTM"].ToString();
                //string usr = selectDeleteItem.Rows[0]["USR"].ToString();
                //string egp = selectDeleteItem.Rows[0]["EGP"].ToString();
                //string stt = selectDeleteItem.Rows[0]["STT"].ToString();
                //string stv = selectDeleteItem.Rows[0]["STV"].ToString();
                //string reason = selectDeleteItem.Rows[0]["REASON_ID"].ToString();
                //string pdisc = selectDeleteItem.Rows[0]["PDISC"].ToString();
                //string discid = selectDeleteItem.Rows[0]["DISCID"].ToString();
                //string discamt = selectDeleteItem.Rows[0]["DISCAMT"].ToString();
                //string upc = selectDeleteItem.Rows[0]["UPC"].ToString();
                //string dty = selectDeleteItem.Rows[0]["DTY"].ToString();

                //StoreResult updateTemp = process.updateNewAmtQtyTempDlyptrans(ProgramConfig.returnRefNo, rec, newAmt, qnt);
                //if (updateTemp.response == ResponseCode.Error)
                //{
                //    frmNotify dialog = new frmNotify(ResponseCode.Error, updateTemp.responseMessage, updateTemp.helpMessage);
                //    dialog.ShowDialog(this);
                //    return;
                //}

                if (dr != null)
                {               
                    string total = (double.Parse(ucReturnPrice.Text) * double.Parse(qty)).ToString(displayAmt);
                    dr["AMT"] = total;
                    dr["UPC"] = ucReturnPrice.Text;
                    dr.AcceptChanges();

                    ucGV.lbReturnPrice.Text = double.Parse(ucReturnPrice.Text).ToString(displayAmt);
                    ucGV.lbTotalPrice.Text = total;

                    double totalOriAmt = 0.0;
                    foreach (UCItemReturn item in pn_Item_Return.Controls)
                    {
                        totalOriAmt += Convert.ToDouble(item.lbPriceText) * Convert.ToDouble(item.lbQtyText);
                    }

                    double totalRetAmt = 0.0;
                    foreach (DataRow dr2 in TEMPDLYPTRANSPARTIAL.Rows)
                    {
                        totalRetAmt += Convert.ToDouble(dr2["AMT"]);
                    }

                    lbTxtSubtotal.Text = totalOriAmt.ToString(displayAmt);
                    lbTxtTotal.Text = totalRetAmt.ToString(displayAmt);

                    frmMoCus.lbTxtSubTotalCash.Text = totalOriAmt.ToString(displayAmt);
                    frmMoCus.lbTxtDiscount.Text = "0.00";
                    frmMoCus.lbTxtTotalCash.Text = totalRetAmt.ToString(displayAmt);

     

                    this.Refresh();
                    DisableControl();
                    panelScanBarcode.BringToFront();
                    ucTBScanBarcodeRet.Focus();
                }

                

                //loadTempDLYPTRANS();


            }
        }

        public void loadTempDLYPTRANS()
        {
            //Default

            cnt = 1;
            amtPrice = 0;
            returnPrice = 0;
            lbTxtSubtotal.Text = amtPrice.ToString("0.00");
            lbTxtTotal.Text = amtPrice.ToString("0.00");
            frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString("0.00");
            frmMoCus.lbTxtDiscount.Text = "0.00";
            frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString("0.00");
            pn_Item_Return.Controls.Clear();
            frmMoCus.pn_Item.Controls.Clear();

            DataTable loadTemp = process.loadReturnTempDlyptrans(ProgramConfig.returnRefNo);
            if (loadTemp.Rows != null)
            {
                for (int i = 0; i < loadTemp.Rows.Count; i++)
                {
                    string stcode = loadTemp.Rows[i]["REF"].ToString();
                    int rec = int.Parse(loadTemp.Rows[i]["REC"].ToString());
                    string pcd = loadTemp.Rows[i]["PCD"].ToString();
                    string qnt = loadTemp.Rows[i]["QNT"].ToString();
                    string amt = loadTemp.Rows[i]["AMT"].ToString();
                    string fds = loadTemp.Rows[i]["FDS"].ToString();
                    string discamt = loadTemp.Rows[i]["DISCAMT"].ToString();
                    string upc = loadTemp.Rows[i]["UPC"].ToString();

                    StoreResult result = process.getProductDesc(pcd);
                    if (result.response == ResponseCode.Success)
                    {
                        if (result.otherData != null)
                        {
                            for (int j = 0; j < result.otherData.Rows.Count; j++)
                            {
                                string name = result.otherData.Rows[j]["PR_NAME"].ToString();
                                string price = result.otherData.Rows[j]["PR_PRICE"].ToString();

                                UCMonitor2Item ucitm = new UCMonitor2Item(cnt);
                                ucitm.lbNo.Text = cnt.ToString();
                                ucitm.lb_ITEM.Text = name;
                                ucitm.lb_AMT.Text = amt;
                                ucitm.lb_QTY.Text = qnt.ToString();
                                frmMoCus.pn_Item.Controls.Add(ucitm);

                                UCItemReturn ucitmReturn = new UCItemReturn(cnt);
                                ucitmReturn.UCGridViewItemSellClick += UCGridViewItemSellClick;
                                ucitmReturn.lbNo.Text = cnt.ToString();
                                ucitmReturn.lbProductCode.Text = code;
                                ucitmReturn.lbQty.Text = qnt;
                                ucitmReturn.lbPrice.Text = upc;
                                ucitmReturn.lbReturnPrice.Text = (double.Parse(amt) / double.Parse(qnt)).ToString("0.00");
                                ucitmReturn.lbTotalPrice.Text = (double.Parse(price) * double.Parse(qnt)).ToString("0.00");
                                ucitmReturn.lbProductName.Text = name;
                                pn_Item_Return.Controls.Add(ucitmReturn);
                                cnt++;

                                amtPrice += double.Parse(ucitmReturn.lbTotalPrice.Text);
                                lbTxtSubtotal.Text = amtPrice.ToString("0.00");
                                returnPrice += double.Parse(amt);
                                lbTxtTotal.Text = returnPrice.ToString("0.00");

                                frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString("0.00");
                                frmMoCus.lbTxtDiscount.Text = "0.00";
                                frmMoCus.lbTxtTotalCash.Text = returnPrice.ToString("0.00");

                            }
                        }
                    }
                }
            }
            RefreshGrid();

        }

        private void ClearMember()
        {
            ProgramConfig.memberId = "";
            ProgramConfig.memberName = "";
            ProgramConfig.memberCardNo = "";
            ProgramConfig.memberProfileMMFormat.Clear();
            ucHeader1.nameText = "";
            ucHeader1.nameVisible = false;
            ucHeader1.pnNameSize = new Size(50, 43);
        }

        //public void clickSearchMember()
        //{
        //    Profile check = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember);
        //    if (check.policy == PolicyStatus.Work)
        //    {
        //        panelMember.BringToFront();
        //        ucTBWI_Member.InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_search, UCTextBoxIconType.SearchAndDelete, IconType.Search, "ກະລຸນາລະບຸສະມາຊິກ");
        //        ucTBWI_Member.Focus();
        //    }
        //}

        //private void ucTBWI_Member_IconClick(object sender, EventArgs e)
        //{
        //    string eventName = "ReturnScan";
        //    Profile check = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember);
        //    if (check.policy == PolicyStatus.Work)
        //    {
        //        frmSearchMember frm = new frmSearchMember((UCTextBoxWithIcon)sender, eventName);
        //        frm.ShowDialog(this);
        //    }
        //    ucHeader1.nameText = ucTBWI_Member.Text;
        //    ucHeader1.nameVisible = true;
        //    Label newFont = new Label();
        //    newFont.Font = new Font("Microsoft Sans Serif", 14);
        //    int checkWidth = TextRenderer.MeasureText(ucTBWI_Member.Text, newFont.Font).Width;
        //    ucHeader1.pnNameSize = new Size(40 + checkWidth, 45);
        //    //DisableControl();
        //    //panelScanBarcode.BringToFront();
        //    //ucTBScanBarcode.Focus();
        //    panelMember.SendToBack();
        //    memberProcess();
        //}

        public void frmSearchMemberData(string memberData, string memberDataName)
        {
            this.memberID = memberData;
            ProgramConfig.memberId = memberID;
            this.memberName = memberDataName;
            ProgramConfig.memberName = memberName;
           
        }

        public void frmSearchMemberDataAuto(string memberDataId, string memberDataName)
        {
            this.memberID = memberDataId;
            ProgramConfig.memberId = memberID;
            this.memberName = memberDataName;
            ProgramConfig.memberName = memberDataName;
        }

        private void picBtBack6_Click(object sender, EventArgs e)
        {
            DisableControl();
            panelScanBarcode.BringToFront();
            ucTBScanBarcodeRet.Focus();
        }

        //private void btnOk_Click(object sender, EventArgs e)
        //{
        //    string eventName = "ReturnScan";
        //    Profile check = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember);
        //    if (check.policy == PolicyStatus.Work)
        //    {
        //        frmSearchMemberAuto frm = new frmSearchMemberAuto(ucTBWI_Member.Text, eventName);
        //        frm.ShowDialog(this);
        //    }

        //    ucHeader1.nameText = memberName;
        //    ucHeader1.nameVisible = true;
        //    //e.Graphics.MeasureString(ucTBWI_Member.Text, SystemFonts.DefaultFont).Width);
        //    Label newFont = new Label();
        //    newFont.Font = new Font("Microsoft Sans Serif", 14);
        //    int checkWidth = TextRenderer.MeasureText(memberName, newFont.Font).Width;
        //    //base {System.MarshalByRefObject} = {Name = "Microsoft Sans Serif" Size=8.25}
        //    ucHeader1.pnNameSize = new Size(40 + checkWidth, 45);
        //    panelMember.SendToBack();
        //    //DisableControl();
        //    //panelScanBarcode.BringToFront();
        //    //ucTBScanBarcode.Focus();
        //    //memberProcess();
        //}

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string eventName = "ReturnScan";
            Profile checkReturn = ProgramConfig.getProfile(FunctionID.Return_InputReturnReason);
            if (checkReturn.policy == PolicyStatus.Work)
            {
                frmAllDisplayReason displayReason = new frmAllDisplayReason(eventName);
                var displayReason_res = displayReason.ShowDialog(this);

                this._reasonId = displayReason._reasonID;
                this._reasonTxt = displayReason._reasonTxt;

                if (displayReason_res != DialogResult.Yes)
                {
                    if (displayReason_res != System.Windows.Forms.DialogResult.Retry)
                    {
                        return;
                    }
                    else
                    {
                        ucFooterTran1.IsStandAlone = true;
                        frmReturnFromScanProduct_Load(null, null);
                        return;
                    }
                } 
            }


            Profile chkDisplayReturnPayment = ProgramConfig.getProfile(FunctionID.Return_SuggestReturnPaymentType);
            if (chkDisplayReturnPayment.policy == PolicyStatus.Work)
            {
                StoreResult displayReturnPayment = process.displayReturnPayment(returnType, ProgramConfig.returnRefNo, amtPrice, returnPrice);
            }

            ucTxtPointReceive.Text = "0.00";
            ucTxtCashReceive.Text = lbTxtTotal.Text;
            ucTxtCashReceive.EnabledUC = false;
            lbSummaryCash.Text = "มูลค่าสินค้ารวม " + lbTxtSubtotal.Text + " บาท";

            panelConfirm.BringToFront();
            //ucTxtCashReceive.Focus();
        }

        private void picBack2_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            ucTBScanBarcodeRet.Focus();
        }

        private void btnSubmit2_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();

            string maxRec = (TEMPDLYPTRANSPARTIAL.Rows[TEMPDLYPTRANSPARTIAL.Rows.Count - 1]["REC"]).ToString();
            string qty = TEMPDLYPTRANSPARTIAL.AsEnumerable().Sum(s => Convert.ToDouble(s["QNT"])).ToString(); //(TEMPDLYPTRANSPARTIAL.Rows[TEMPDLYPTRANSPARTIAL.Rows.Count - 1]["QNT"]).ToString();
            string newInstuRec = (double.Parse(maxRec) + 10).ToString();
            string superId;
            if (ProgramConfig.superUserId == "" || ProgramConfig.superUserId == null || ProgramConfig.superUserId == "N/A")
            {
                superId = "0";
            }
            else
            {
                superId = ProgramConfig.superUserId;
            }

            if (ProgramConfig.memberId != null && ProgramConfig.memberId.Trim() != "")
            {
                bool isMMFormat = ProgramConfig.memberFormat == MemberFormat.MegaMaket;
                string memberID = isMMFormat ? ProgramConfig.memberCardNo : ProgramConfig.memberId;
                string pdisc = isMMFormat ? ProgramConfig.memberProfileMMFormat.CreditCustomerNo : ""; // CreditCustomerNo
                string discID = isMMFormat ? ProgramConfig.memberProfileMMFormat.CustomerCategory : ""; // CustomerCategory
                string discAmt = isMMFormat ? ProgramConfig.memberProfileMMFormat.Customer_No : ucTxtPointReceive.Text; // Customer_No

                pdisc = pdisc == "" ? "0" : pdisc;
                string subMemberId = memberID.Substring(0, 2);

                TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, "0", "3", "C", memberID, qty, ucTxtCashReceive.Text, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", cultureinfo), ProgramConfig.userId
                                            , subMemberId, "", "", "0", pdisc, discID, discAmt, "0.00", "1");
            }

            //Pay
            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", "RETN", "1.00", ucTxtCashReceive.Text, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", cultureinfo), ProgramConfig.userId
                            , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1", "", "");


            //Vat
            string vatPercent = ProgramConfig.vatRate;
            string vatAmt = ((Convert.ToDouble(ucTxtCashReceive.Text) * Convert.ToDouble(ProgramConfig.vatRate)) / (100 + Convert.ToDouble(ProgramConfig.vatRate))).ToString("N2");
            newInstuRec = (double.Parse(newInstuRec) + 1).ToString();

            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "V", "Vat Return", qty, vatAmt, ProgramConfig.vatRate, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", cultureinfo), ProgramConfig.userId
                                            , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");


            //Final Rec
            newInstuRec = (double.Parse(newInstuRec) + 1).ToString();
            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "F", ProgramConfig.returnRefNo + " Return", qty, ucTxtCashReceive.Text, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", cultureinfo), ProgramConfig.userId
                                , ProgramConfig.tillNo, "", "", _reasonId, superId, "", DateTime.Now.ToString("yyyyMMdd", cultureinfo), (ProgramConfig.IsStandAloneMode ? "1" : ""), "1");


            frmLoading.closeLoading();

            frmReturnSuccess frmSuccess = new frmReturnSuccess(ProgramConfig.returnRefNo, ucTxtCashReceive.Text, _reasonId, _reasonTxt, ProgramConfig.returnRefNo,
                                            returnType, ProgramConfig.tillNo, ProgramConfig.userId, ProgramConfig.memberName, "-", null, TEMPDLYPTRANSPARTIAL, ProgramConfig.printInvoiceType, false, "-");
            DialogResult frmSuccess_res = frmSuccess.ShowDialog(this);

            if (frmSuccess_res != DialogResult.Yes)
            {
                TEMPDLYPTRANSPARTIAL.AcceptChanges();
                foreach (DataRow row in TEMPDLYPTRANSPARTIAL.Rows)
                {
                    if (row["VTY"].ToString() == "V" || row["VTY"].ToString() == "P" || row["VTY"].ToString() == "F" || row["VTY"].ToString() == "C")
                    {
                        row.Delete();
                    }

                }
                TEMPDLYPTRANSPARTIAL.AcceptChanges();
            }
        }

        public void frmAllDisplayReasonData(string reasonData)
        {
            reasonId = reasonData;
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            if (chkOpenDrawer == false)
            {
                this.Dispose();
            }
            else
            {                
                string responseMessage = ProgramConfig.message.get("frmCashbalance", "CloseDrawer").message;
                string helpMessage = ProgramConfig.message.get("frmCashbalance", "CloseDrawer").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                dialog.ShowDialog(this);
                return;
            }  
        }

        public void memberProcess()
        {
            StoreResult getMemberProfile = process.getMemberProfile(memberID);
            if(getMemberProfile.response == ResponseCode.Success)
            {
                taxId = getMemberProfile.otherData.Rows[0]["IDCARD"].ToString();

            }
            else if (getMemberProfile.response == ResponseCode.Error)
            { 
                frmNotify dialog = new frmNotify(ResponseCode.Error, getMemberProfile.responseMessage, getMemberProfile.helpMessage);
                dialog.ShowDialog(this);
                return;
            
            }

            StoreResult getCustomerFFTI = process.getCustomerFFTI(taxId);
            if (getCustomerFFTI.response == ResponseCode.Success)
            {
                taxId = getMemberProfile.otherData.Rows[0]["IDCARD"].ToString();

            }
            else if (getCustomerFFTI.response == ResponseCode.Error)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, getCustomerFFTI.responseMessage, getCustomerFFTI.helpMessage);
                dialog.ShowDialog(this);
                return;

            }

        }

        public void InitalDatatable()
        {
            TEMPDLYPTRANSPARTIAL.Columns.Add("STCODE", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("REF", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("REC", typeof(int));
            TEMPDLYPTRANSPARTIAL.Columns.Add("STY", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("VTY", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("PCD", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("QNT", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("AMT", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("FDS", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("TTM", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("USR", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("EGP", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("STT", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("STV", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("REASON_ID", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("PDISC", typeof(double));
            TEMPDLYPTRANSPARTIAL.Columns.Add("DISCID", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("DISCAMT", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("UPC", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("DTY", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("QNTMAX", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("PR_NAME", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("DISPQ", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("RTCPRICE", typeof(string));
            TEMPDLYPTRANSPARTIAL.Columns.Add("ISEDC", typeof(string));
        }

        private void ucHeader1_MemberEnterFromButton(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            ucTBScanBarcodeRet.Focus();
        }

        private void lbDelete_Click(object sender, EventArgs e)
        {
            //Fix language
            if (Utility.AlertMessage(this, ResponseCode.Warning, "ต้องการลบรายการหรือไม่"))
            {
                DataRow dr = TEMPDLYPTRANSPARTIAL.AsEnumerable().Where(w => w["REC"].ToString() == ucGV.Rec).FirstOrDefault();
                if (dr != null)
                {
                    TEMPDLYPTRANSPARTIAL.Rows.Remove(dr);
                    pn_Item_Return.Controls.Remove(ucGV);
                    RefreshGrid();
                }
                panelScanBarcode.BringToFront();
            }

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            btnReturn.Enabled = true;
            btnReturn.BackgroundImage = Properties.Resources.payment_enable;
            btnReturn.BringToFront();
            panelScanBarcode.BringToFront();
        }

        private void CheckItemSell()
        {
            if (pn_Item_Return.Controls.Count > 0)
            {
                setVisibleButtonConfirm(true);
            }
            else if (pn_Item_Return.Controls.Count == 0)
            {
                setVisibleButtonConfirm(false);
            }
        }

        private void setVisibleButtonConfirm(bool val)
        {
            if (val)
            {
                if (!btnConfirm.Enabled)
                {
                    btnConfirm.Enabled = true;
                    btnConfirm.BackgroundImage = Properties.Resources.Sale_btnConfirm;
                }
            }
            else
            {
                if (btnConfirm.Enabled)
                {
                    btnConfirm.Enabled = false;
                    btnConfirm.BackgroundImage = Properties.Resources.payment_disable;
                }

            }
        }

        private void ucTBScanBarcodeRet_Enter(object sender, EventArgs e)
        {
            btnReturn.Enabled = false;
            CheckItemSell();
            btnConfirm.BringToFront();

        }
    }
}
