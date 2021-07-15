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
    public partial class frmReturnFromInvoice : Form
    {
        string searchType;
        public UCTextBoxWithIcon ucTBWI { get; set; }
        private ReturnProcess process = new ReturnProcess();
        public UCTextBoxSmall ucTBS { get; set; }
        public int cnt { get; set; }
        public UCItemInvoice ucGV;
        public UCItemInvoiceDetail ucGV2;
        public UCItemInvoice lastUCIS;
        public UCItemInvoiceDetail lastUCIS2;
        double amtPriceF = 0;
        double disPriceF = 0;
        double amtPriceP = 0;
        double disPriceP = 0;
        double changeValue = 0;
        double totalTax = 0;
        double totalTaxDis = 0;
        double vatValue = 0;
        double discountPQ = 0;
        double a = 0;
        double totalQnt;
        double currentAmt = 0;
        double rtcPrice = 0;
        string productDisPQ;
        string qtyRes;
        string receiptNo;
        string saleDate;
        string cashier;
        string totalQty ;
        string totalAmt;
        string totalDisc;
        string vat;
        string dty;
        string value1 = "";
        string value2 = "";
        double upPrice = 0;
        double downPrice = 0;
        string newAmt;
        string newQty;
        string newDis;
        int productRec;
        string productPrice;
        string productRTCPrice;
        string productPriceDis;
        string returnType;
        string paymentType;
        string saleAmt;
        string _reasonId;
        string _reasonTxt;
        string memberID;
        string memberName = "";
        string taxId;
        string currentQty;
        StoreResult displayReturnPayment = null;
        string amtFormat = ProgramConfig.amountFormatString;
        string totalResult;
        string vty;
        string maxRec;
        string newVatRec;
        string chkPartial;
        public DataTable TEMPDLYPTRANSFULL = new DataTable();
        public DataTable TEMPDLYPTRANSPARTIAL = new DataTable();
        private DataTable _dtDisplayPayment = new DataTable();
        private PrintInvoiceType _printType;

        public frmReturnFromInvoice()
        {
            InitializeComponent();
            InitalDatatable();
        }

        private void frmReturnFromInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                cnt = 1;
                //btnSearch.BackgroundImage = Properties.Resources.change_enable;
                //btnSearch.ForeColor = Color.White;
                ucHeader1.alertFunctionID = FunctionID.Return_GetMessageCashier;
                //Class1.InitialTextBoxIcon(ucTxtSearch, BJCBCPOS.Properties.Resources.icon_textbox_scan, Class1.UCTextBoxIconType.ScanAndDelete, Class1.IconType.Scan);
                StoreResult result = null;

                Profile check = ProgramConfig.getProfile(FunctionID.Return_SelectReturnTypeMenu_ByReceipt);
                if (check.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("ReturnReceipt", check.diffUserStatus);
                    //auth.function = FunctionID.Return_SelectReturnTypeMenu_ByReceipt;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    this.Dispose();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, check, "ReturnReceipt"))
                    {
                        this.Dispose();
                        return;
                    }
                }

                result = process.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ReturnRef);
                string refNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.returnRefNo = refNo;
                ProgramConfig.returnRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
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

                Profile chkMCashier = ProgramConfig.getProfile(FunctionID.Return_GetMessageCashier);
                if (chkMCashier.policy == PolicyStatus.Work)
                {
                    ProcessResult res = process.cashireMessageStatus();
                    if (res.response.next)
                    {
                        if (res.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                        }

                        if (res.needNextProcess)
                        {
                            ucHeader1.alertStatus = true;
                        }
                        else
                        {
                            ucHeader1.alertStatus = false;
                        }
                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        this.Dispose();
                        return;
                    }
                }

                defaultPage();
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

        public void defaultPage()
        {
            string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ReceiptNo").message;
            ucSearchType.LabelText = responseMessage;
            ucSearchType.ValueText = "1";
            ucDropDownList.LabelText = "RETN";
            panelDetailInvoice.BringToFront();
            panelSearchReceipt.BringToFront();
            panelKeyNumber.BringToFront();
            panelSearchReceipt.BringToFront();
            panelSearchInvoice.Controls.Clear();
            ucTxtSearch.Text = "";
            ucTxtSearch.Select();
            ucTxtSearch.Focus();
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            Profile check1 = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo_ReceiptNo);
            Profile check2 = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo_ProductCode);
            Profile check3 = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo_MemberID);
            Profile check4 = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo_PhoneNO);
            Profile check5 = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_SearchByReceiptNo_CitizenID);

            if (check5.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "IDCard").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "5" });
            }
            if (check4.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "Telephone").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "4" });
            }
            if (check3.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "MemberID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "3" });
            }
            if (check2.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ProductCode").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "2" });
            }
            if (check1.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ReceiptNo").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "1" });
            }
            return lstStr;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            searchReceiptProcess();
            frmLoading.closeLoading();
        }

        public void defaultValue()
        {
            amtPriceF = 0;
            disPriceF = 0;
            amtPriceP = 0;
            disPriceP = 0;
            changeValue = 0;
            totalTax = 0;
            totalTaxDis = 0;
            vatValue = 0;
            a = 0;
            upPrice = 0;
            downPrice = 0;
        }

        public void searchReceiptProcess()
        {
            try
            {
                defaultValue();
                panelSearchInvoice.Controls.Clear();
                searchType = ucSearchType.ValueText;
                StoreResult searchReceipt = process.searchReceipt(Convert.ToInt32(searchType), ucTxtSearch.Text.Trim());
                if (searchReceipt.response.next)
                {
                    _printType = (PrintInvoiceType)Enum.Parse(typeof(PrintInvoiceType), searchReceipt.otherData.Rows[0]["PrintInvoiceType"].ToString(), true);
                    if (searchReceipt.response == ResponseCode.Information)
                    {
                        frmNotify dialog = new frmNotify(searchReceipt);
                        dialog.ShowDialog(this);
                    }

                    if (searchReceipt.otherData != null)
                    {
                        if (searchReceipt.otherData.Rows.Count == 1)
                        {
                            receiptNo = searchReceipt.otherData.Rows[0]["ReceiptNo"].ToString();
                            saleDate = searchReceipt.otherData.Rows[0]["SaleDate"].ToString();
                            cashier = searchReceipt.otherData.Rows[0]["Cashier"].ToString();
                            totalQty = searchReceipt.otherData.Rows[0]["TotalQty"].ToString();
                            totalAmt = searchReceipt.otherData.Rows[0]["TotalAmt"].ToString();
                            totalDisc = searchReceipt.otherData.Rows[0]["TotalDisc"].ToString();
                            showDetailInvoice();
                        }
                        else
                        {
                            for (int i = 0; i < searchReceipt.otherData.Rows.Count; i++)
                            {
                                receiptNo = searchReceipt.otherData.Rows[i]["ReceiptNo"].ToString();
                                saleDate = searchReceipt.otherData.Rows[i]["SaleDate"].ToString();
                                cashier = searchReceipt.otherData.Rows[i]["Cashier"].ToString();
                                totalQty = searchReceipt.otherData.Rows[i]["TotalQty"].ToString();
                                totalAmt = searchReceipt.otherData.Rows[i]["TotalAmt"].ToString();
                                totalDisc = searchReceipt.otherData.Rows[i]["TotalDisc"].ToString();

                                //double a = double.Parse(price) * double.Parse(qty);
                                //UCMonitor2Item ucitm = new UCMonitor2Item(cnt);
                                //ucitm.lbNo.Text = cnt.ToString();
                                //ucitm.lb_ITEM.Text = name;
                                //ucitm.lb_AMT.Text = a.ToString("N2");
                                //ucitm.lb_QTY.Text = qty;
                                //frmMoCus.pn_Item.Controls.Add(ucitm);

                                UCItemInvoice ucitmInvoice = new UCItemInvoice(cnt);
                                ucitmInvoice.UCGridViewItemSellClick += UCGridViewItemSellClick;
                                ucitmInvoice.lbUC_No.Text = cnt.ToString();
                                ucitmInvoice.lbUC_ReceiptNo.Text = receiptNo;
                                ucitmInvoice.lbUC_Date.Text = saleDate;
                                ucitmInvoice.lbUC_Cashier.Text = cashier;
                                ucitmInvoice.lbUC_Qty.Text = double.Parse(totalQty).ToString();
                                ucitmInvoice.lbUC_ReturnPrice.Text = double.Parse(totalAmt).ToString(amtFormat);
                                ucitmInvoice.lbUC_TotalDisc.Text = double.Parse(totalDisc).ToString(amtFormat);
                                panelSearchInvoice.Controls.Add(ucitmInvoice);
                                cnt++;

                            }
                            RefreshGrid();
                        }
                    }
                }
                else
                {
                    frmNotify dialog = new frmNotify(searchReceipt);
                    dialog.ShowDialog(this);
                    return;
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    Utility.CheckRunningNumber();
                    defaultPage();
                    ucFooterTran1.IsStandAlone = true;
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            //defaultPage();
        }

        public void InitalDatatable()
        {
            TEMPDLYPTRANSFULL.Columns.Add("STCODE", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("REF", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("REC", typeof(int));
            TEMPDLYPTRANSFULL.Columns.Add("STY", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("VTY", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("PCD", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("QNT", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("AMT", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("FDS", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("TTM", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("USR", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("EGP", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("STT", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("STV", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("REASON_ID", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("PDISC", typeof(float));
            TEMPDLYPTRANSFULL.Columns.Add("DISCID", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("DISCAMT", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("UPC", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("DTY", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("PR_NAME", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("DISPQ", typeof(string));
            TEMPDLYPTRANSFULL.Columns.Add("ISEDC", typeof(string));

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
            TEMPDLYPTRANSPARTIAL.Columns.Add("PDISC", typeof(float));
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

        private void RefreshGrid()
        {
            List<UCItemInvoice> lstItemReturn = new List<UCItemInvoice>();
            lstItemReturn = panelSearchInvoice.Controls.Cast<UCItemInvoice>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panelSearchInvoice.Controls.Clear();
            int num = lstItemReturn.Count;
            //double a = 0;

            foreach (UCItemInvoice item in lstItemReturn)
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
                //a += double.Parse(item.lbQtyText);
                panelSearchInvoice.Controls.Add(item);
                num--;
            }
            ScrollToBottom(panelSearchInvoice);
            //ProgramConfig.qntValue = a.ToString();

            //List<UCMonitor2Item> lstMonitor2ItemReturn = new List<UCMonitor2Item>();
            //lstMonitor2ItemReturn = frmMoCus.pn_Item.Controls.Cast<UCMonitor2Item>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            //frmMoCus.pn_Item.Controls.Clear();
            //int number = lstMonitor2ItemReturn.Count;

            //foreach (UCMonitor2Item monitor2Item in lstMonitor2ItemReturn)
            //{
            //    if (number % 2 != 0)
            //    {
            //        monitor2Item.BackColor = Color.FromArgb(240, 240, 240);
            //    }
            //    else
            //    {
            //        monitor2Item.BackColor = Color.White;
            //    }
            //    monitor2Item.lbNoText = number.ToString();
            //    frmMoCus.pn_Item.Controls.Add(monitor2Item);
            //    number--;
            //}
            //ScrollToBottom(frmMoCus.pn_Item);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void RefreshGridInfo()
        {
            List<UCItemInvoiceDetail> lstItemReturn = new List<UCItemInvoiceDetail>();
            lstItemReturn = pn_Item.Controls.Cast<UCItemInvoiceDetail>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_Item.Controls.Clear();
            int num = lstItemReturn.Count;
            //double a = 0;

            foreach (UCItemInvoiceDetail item in lstItemReturn)
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
                //a += double.Parse(item.lbQtyText);
                pn_Item.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_Item);            
        }

        private void RefreshGridInfo2()
        {
            List<UCItemInvoiceDetail> lstItemReturn = new List<UCItemInvoiceDetail>();
            lstItemReturn = pn_ItemPartial.Controls.Cast<UCItemInvoiceDetail>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_ItemPartial.Controls.Clear();
            int num = lstItemReturn.Count;
            a = 0;
            
            foreach (UCItemInvoiceDetail item in lstItemReturn)
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
                a += double.Parse(item.lbQtyText);
                pn_ItemPartial.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_ItemPartial);

        }

        public void showDetailInvoice()
        {
            //pn_receript_information.BringToFront();
            //pn_input_type.BringToFront();
            //pn_Item.BringToFront();
            //pn_Detail.BringToFront();
            pn_Item.Controls.Clear();
            TEMPDLYPTRANSFULL.Rows.Clear();
            TEMPDLYPTRANSPARTIAL.Rows.Clear();
                      
            
            StoreResult displayReceiptDetail = process.displayReceiptDetail(receiptNo);
            
            if (displayReceiptDetail.response.next)
            {
                if (displayReceiptDetail.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(displayReceiptDetail);
                    dialog.ShowDialog(this);
                }

                if (displayReceiptDetail.otherData != null)
                {
                    for (int i = 0; i < displayReceiptDetail.otherData.Rows.Count; )
                    {
                        string codeOrName = displayReceiptDetail.otherData.Rows[i]["ProductDesc"].ToString();
                        string qty = displayReceiptDetail.otherData.Rows[i]["QuantSale"].ToString();
                        string qtyReturn = displayReceiptDetail.otherData.Rows[i]["QuantReturn"].ToString();
                        string price = displayReceiptDetail.otherData.Rows[i]["Price"].ToString();
                        string dis = displayReceiptDetail.otherData.Rows[i]["Discount"].ToString();
                        if (dis == "" || dis == null)
                        {
                            dis = "0";
                        }
                        string amt = displayReceiptDetail.otherData.Rows[i]["Amount"].ToString();
                        string line = displayReceiptDetail.otherData.Rows[i]["LineNo"].ToString();
                        int rec = Convert.ToInt32(displayReceiptDetail.otherData.Rows[i]["rec"].ToString());
                        string dataType = displayReceiptDetail.otherData.Rows[i]["DataType"].ToString();
                        string vatType = displayReceiptDetail.otherData.Rows[i]["Vat_Status"].ToString();
                        if (dataType == "I")
                        {
                            if (line == "1")
                            {
                                discountPQ = double.Parse(dis) / double.Parse(qty);
              
                                UCItemInvoiceDetail ucitmDetail = new UCItemInvoiceDetail();
                                ucitmDetail.lbUC_No.Text = rec.ToString();
                                ucitmDetail.lbUC_ProductRec.Text = rec.ToString();
                                ucitmDetail.lbUC_ProductCode.Text = codeOrName;
                                qtyRes = (double.Parse(qty) - double.Parse(qtyReturn)).ToString();
                                ucitmDetail.lbUC_Qty.Text = double.Parse(qty).ToString();
                                ucitmDetail.lbUC_Price.Text = double.Parse(price).ToString(amtFormat);
                                ucitmDetail.lbUC_QtyMax.Text = double.Parse(qtyRes).ToString(amtFormat);
                                ucitmDetail.lbUC_Discount.Text = double.Parse(dis).ToString(amtFormat);
                                ucitmDetail.lbUC_DisPQ.Text = discountPQ.ToString(amtFormat);
                                ucitmDetail.lbUC_TotalPrice.Text = (double.Parse(amt)).ToString(amtFormat);
                                if (vatType == "1")
                                {
                                    vty = "1";
                                    totalTax += double.Parse(amt);
                                    totalTaxDis += double.Parse(dis);
                                }
                                else
                                {
                                    vty = "0";
                                }
                                disPriceF += double.Parse(dis);
                                amtPriceF += double.Parse(amt);
                                codeOrName = displayReceiptDetail.otherData.Rows[i + 1]["ProductDesc"].ToString();
                                ucitmDetail.lbUC_ProductName.Text = codeOrName;
                                double currentDis = double.Parse(qtyRes) * discountPQ;

                                if (double.Parse(qty) == 1 && double.Parse(price) != double.Parse(amt) && double.Parse(dis) == 0)
                                {
                                    currentAmt = double.Parse(qtyRes) * (double.Parse(amt));
                                    rtcPrice = double.Parse(amt);
                                }
                                else
                                {
                                    currentAmt = double.Parse(qtyRes) * (double.Parse(price));
                                    rtcPrice = 0;
                                }
                                
                                double currentAmtDis = double.Parse(qtyRes) * (double.Parse(price) - discountPQ);
                                                                
                                TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode,ProgramConfig.returnRefNo, rec, "3", vty, ucitmDetail.lbUC_ProductCode.Text
                                    , ucitmDetail.lbUC_QtyMax.Text, currentAmt.ToString(amtFormat), currentDis.ToString(amtFormat), DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId, vatValue
                                                , "", "", "0", "0.00", "0", "0.00", ucitmDetail.lbUC_Price.Text, dty, ucitmDetail.lbUC_ProductName.Text, discountPQ.ToString());
                                TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, rec, "3", vty, ucitmDetail.lbUC_ProductCode.Text
                                                , "0", "0", "0", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId, vatValue
                                                , "", "", "0", "0.00", "0", "0.00", ucitmDetail.lbUC_Price.Text, dty, ucitmDetail.lbUC_QtyMax.Text, ucitmDetail.lbUC_ProductName.Text, discountPQ.ToString(), rtcPrice.ToString());
                                
                                pn_Item.Controls.Add(ucitmDetail);
                                i += 2;
                            }

                        }
                        else if (dataType == "P")
                        {
                            paymentType = displayReceiptDetail.otherData.Rows[i]["ProductDesc"].ToString().Replace(" ", "");
                            value1 = displayReceiptDetail.otherData.Rows[i]["Discount"].ToString();
                            value2 = displayReceiptDetail.otherData.Rows[i]["Amount"].ToString();
                            if (paymentType == "CHGD")
                            {
                                upPrice = double.Parse(value1);
                                downPrice = double.Parse(value2);
                                i++;
                            }
                            else if (paymentType.Trim() == "CASH")
                            {
                                changeValue += double.Parse(value1);                                
                                i++;
                            }
                            else
                            {
                                i++;
                            }

                        }
                        else if (dataType == "V")
                        {
                            totalTax = double.Parse(displayReceiptDetail.otherData.Rows[i]["Discount"].ToString());
                            vat = displayReceiptDetail.otherData.Rows[i]["Amount"].ToString();
                            i++;

                        }
                        else if (dataType == "C")
                        {
                            StoreResult result = process.getMemberProfile(displayReceiptDetail.otherData.Rows[i]["ProductDesc"].ToString().Replace(" ", ""));
                            if (result.response.next)
                            {
                                string fName = "";
                                string lName = "";

                                memberID = result.otherData.Rows[0]["MEMBER_ID"].ToString();
                                if (result.otherData.Rows[0]["TNAME"].ToString().Trim() == "")
                                {
                                    fName = result.otherData.Rows[0]["ENAME"].ToString();
                                    lName = result.otherData.Rows[0]["ELNAME"].ToString();
                                }
                                else
                                {
                                    fName = result.otherData.Rows[0]["TNAME"].ToString();
                                    lName = result.otherData.Rows[0]["TLNAME"].ToString();
                                }

                                memberName = String.Format("{0} {1}", fName, lName);
                                VisibleMember(true);
                            }
                            else
                            {
                                VisibleMember(false);
                            }
                            i++;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    for (int j = 0; j < displayReceiptDetail.otherData.Rows.Count; j++ )
                    {
                        chkPartial = displayReceiptDetail.otherData.Rows[j]["ReturnPartial"].ToString();
                        if (chkPartial == "N")
                        {
                            break;
                        }
                    }

                    RefreshGridInfo();
                    lbTxtNo.Text = ProgramConfig.returnRefNo;
                    lbTxtParitalNo.Text = ProgramConfig.returnRefNo;
                    lbBalanceResult.Text = changeValue.ToString(amtFormat);
                    lbTaxAmountResult.Text = (totalTax).ToString(amtFormat);
                    lbVatAmountResult.Text = double.Parse(vat).ToString(amtFormat);
                    lbDiscountAmountResult.Text = disPriceF.ToString(amtFormat);
                    lbTotalAmountResult.Text = (amtPriceF + upPrice - downPrice).ToString(amtFormat);
                    saleAmt = amtPriceF.ToString(amtFormat);
                    
                }
            }
            else
            {
                defaultPage();
                frmNotify dialog = new frmNotify(displayReceiptDetail);
                dialog.ShowDialog(this);
                return;
                
            }

            Profile chkReturnFull = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_InputAmountReturn_ReturnAllItem);
            if (chkReturnFull.policy == PolicyStatus.Skip)
            {
                btnReturnAll.Enabled = false;
                btnReturnAll.BackgroundImage = BJCBCPOS.Properties.Resources.payment_btm_right_disable;
                btnReturnAll.ForeColor = Color.White;

            }
            else if (chkReturnFull.policy == PolicyStatus.Work)
            {
                btnReturnAll.Enabled = true;
                btnReturnAll.BackgroundImage = BJCBCPOS.Properties.Resources.Rectangle_224;
                btnReturnAll.ForeColor = Color.White;
            }

            if (chkPartial == "Y")
            {
                Profile chkReturnnPartial = ProgramConfig.getProfile(FunctionID.Return_InputReturnItem_ByReceipt_InputAmountReturn_ReturnSomeItem);
                if (chkReturnnPartial.policy == PolicyStatus.Skip)
                {
                    btnReturnSomeItem.Enabled = false;
                    btnReturnSomeItem.BackgroundImage = BJCBCPOS.Properties.Resources.payment_btm_right_disable;
                    btnReturnSomeItem.ForeColor = Color.White;
                }
                else if (chkReturnnPartial.policy == PolicyStatus.Work)
                {
                    btnReturnSomeItem.Enabled = true;
                    btnReturnSomeItem.BackgroundImage = BJCBCPOS.Properties.Resources.txtboxWIC_enable;
                    btnReturnSomeItem.ForeColor = Color.ForestGreen;
                }
            }
            else if (chkPartial == "N")
            {
                btnReturnSomeItem.Enabled = false;
                btnReturnSomeItem.BackgroundImage = BJCBCPOS.Properties.Resources.payment_btm_right_disable;
                btnReturnSomeItem.ForeColor = Color.White;
                frmNotify dialog = new frmNotify(ResponseCode.Information, lbCantReturnPatial.Text);
                dialog.ShowDialog(this);
            }

            pn_receript_information.BringToFront();
            pn_input_type.BringToFront();
            pn_Item.BringToFront();
            pn_Detail.BringToFront();           
 
        }

        private void VisibleMember(bool flag)
        {
            if (flag)
            {
                ucHeader1.showMember = true;
                ucHeader1.btnMember.Visible = false;
                ucHeader1.nameText = memberName;
                ucHeader1.nameVisible = true;
                Label newFont = new Label();
                newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                int checkWidth = TextRenderer.MeasureText(memberName, newFont.Font).Width;
                ucHeader1.pnNameSize = new Size(50 + checkWidth, 45);
                panelMember.SendToBack();
            }
            else
            {
                ucHeader1.nameText = "";
                ucHeader1.nameVisible = false;
                ucHeader1.showMember = false;
                ucHeader1.btnMember.Visible = false;
            }
        }

        public void UCGridViewItemSellClick(object sender, EventArgs e)
        {            
            ucGV = (UCItemInvoice)sender;
            receiptNo = ucGV.lbUC_ReceiptNo.Text;
            showDetailInvoice();            
        }

        public void UCGridViewItemSellClick2(object sender, EventArgs e)
        {
            UCGridViewItemSell_Click2((UCItemInvoiceDetail)sender);
        }

        private void UCGridViewItemSell_Click2(UCItemInvoiceDetail ucGV2)
        {
            pn_qty_input.BringToFront();
            pnReturnPartial.BringToFront();
            panelKeyNumber.BringToFront();
            btnReturnPartial.Enabled = false;
            btnReturnPartial.BackgroundImage = Properties.Resources.confirm_disable;
            productRec = Convert.ToInt32(ucGV2.lbUC_ProductRec.Text);
            productDisPQ = ucGV2.lbUC_DisPQ.Text;
            productPrice = (double.Parse(ucGV2.lbUC_Price.Text).ToString());
            productPriceDis = (double.Parse(ucGV2.lbUC_Price.Text) - double.Parse(ucGV2.lbUC_DisPQ.Text)).ToString();
            productRTCPrice = ucGV2.lbUC_RTCPrice.Text;

            lbTxtProductCode.Text = ucGV2.lbUC_ProductCode.Text;
            lbTxtDesc.Text = ucGV2.lbUC_ProductName.Text;
            currentQty = double.Parse(ucGV2.lbUC_QtyMax.Text).ToString();
            ucTxtQty.Text = currentQty;
            //lbCurrentSell.Text = "จำนวนที่ซื้อไป " + currentQty + " ชิ้น";
            lbCurrentSell.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentSell"), currentQty);

            if (lastUCIS2 != null && lastUCIS2.lbNoText != ucGV2.lbNoText)
                UCItemInvoiceDetail.LostFocusItem(lastUCIS2);

            lastUCIS2 = ucGV2;
        }

        private void ucSearchType_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList2();
            }
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            Dispose();
            frmLoading.closeLoading();
        }

        private void btnReturnSomeItem_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            returnType = "P";            
            loadTempDLYPTransP();
            btnReturnPartial.Enabled = false;
            btnReturnPartial.BackgroundImage = Properties.Resources.confirm_disable;
            lbTxtDebtCustomer.Text = amtPriceP.ToString(amtFormat);
            pnReturnPartial.BringToFront();
            pn_select_product.BringToFront();
            panelKeyNumber.BringToFront();
            frmLoading.closeLoading();
        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            returnType = "F";
            loadTempDLYPTransF();
            ReturnFromInvoiceProcess();
            panelKeyNumber.SendToBack();
            frmLoading.closeLoading();
            
        }

        private void ucTxtQty_TextBoxKeydown(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            UpdateQtyAmt();
            frmLoading.closeLoading();
        }

        private void ucTxtQty_EnterFromButton(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            UpdateQtyAmt();
            frmLoading.closeLoading();
        }

        public void UpdateQtyAmt()
        {
            if (ucTxtQty.Text == "" || double.Parse(ucTxtQty.Text) < 0)
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowNullAndBelow0").message;
                string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowNullAndBelow0").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างหรือจำนวนน้อยกว่า 0 ได้");
                dialog.ShowDialog(this);
                return;
            }

            //btnReturnPartial.Enabled = true;
            //btnReturnPartial.BackgroundImage = Properties.Resources.confirm_enable;
            pn_select_product.BringToFront();
            panelKeyNumber.BringToFront();
            newQty = ucTxtQty.Text;
            if (double.Parse(productRTCPrice) != 0)
            {
                newAmt = (double.Parse(newQty) * double.Parse(productRTCPrice)).ToString();
            }
            else
            {
                newAmt = (double.Parse(newQty) * double.Parse(productPrice)).ToString();
            }            
            newDis = (double.Parse(newQty) * double.Parse(productDisPQ)).ToString();

            if (double.Parse(newQty) > double.Parse(currentQty))
            {
                string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowReturnQty").message;
                string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowReturnQty").help;
                frmNotify dialog = new frmNotify(ResponseCode.Error, string.Format(responseMessage, currentQty), helpMessage);

                //frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้คืนสินค้า รายการนี้เกิน " + currentQty + " หน่วย");
                dialog.ShowDialog(this);
                return;
            }

            foreach (DataRow dr in TEMPDLYPTRANSPARTIAL.Rows) // search whole table
            {
                if (dr["REC"].ToString() == productRec.ToString())
                {
                    dr["QNT"] = newQty;
                    dr["AMT"] = newAmt;
                    dr["FDS"] = newDis;
                }
            }

            //StoreResult updateQtyAmt = process.updateNewAmtQtyTempDlyptrans(ProgramConfig.returnRefNo, productRec, newAmt, newQty);
            
            loadTempDLYPTransP();

            if (a > 0)
            {
                btnReturnPartial.Enabled = true;
                btnReturnPartial.BackgroundImage = Properties.Resources.confirm_enable;
            }
 
        }

        public void loadTempDLYPTransF()
        {
            //Default
            amtPriceF = 0;
            disPriceF = 0;
            totalTax = 0;
            totalTaxDis = 0;
            pn_ItemPartial.Controls.Clear();

            DataTable loadTemp = TEMPDLYPTRANSFULL;
            if (loadTemp.Rows != null)
            {
                for (int i = 0; i < loadTemp.Rows.Count; i++)
                {
                    string stcode = loadTemp.Rows[i]["REF"].ToString();
                    int rec = Convert.ToInt32(loadTemp.Rows[i]["REC"].ToString());
                    string pcd = loadTemp.Rows[i]["PCD"].ToString();
                    double qnt = double.Parse(loadTemp.Rows[i]["QNT"].ToString());
                    string amt = loadTemp.Rows[i]["AMT"].ToString();
                    string upc = loadTemp.Rows[i]["UPC"].ToString();
                    string fds = loadTemp.Rows[i]["FDS"].ToString();
                    string vty = loadTemp.Rows[i]["VTY"].ToString();
                    string name = loadTemp.Rows[i]["PR_NAME"].ToString();
                    string disPQ = loadTemp.Rows[i]["DISPQ"].ToString();

                    if (qnt != 0)
                    {
                        disPriceF += double.Parse(fds);
                        if (vty == "1")
                        {
                            totalTax += (double.Parse(amt));
                            totalTaxDis += (double.Parse(fds));
                        }
                        totalQnt += qnt;
                    }

                    amtPriceF += double.Parse(amt);


                }
                totalResult = (amtPriceF).ToString(amtFormat);
                //lbTxtDebtCustomer.Text = totalResult;
            }

        }

        public void loadTempDLYPTransP()
        {
            //Default
            amtPriceP = 0;
            disPriceP = 0;
            totalTax = 0;
            totalTaxDis = 0;
            pn_ItemPartial.Controls.Clear();

            DataTable loadTemp = TEMPDLYPTRANSPARTIAL;
            if (loadTemp.Rows != null)
            {
                for (int i = 0; i < loadTemp.Rows.Count; i++)
                {
                    string stcode = loadTemp.Rows[i]["REF"].ToString();
                    int rec = Convert.ToInt32(loadTemp.Rows[i]["REC"].ToString());
                    string pcd = loadTemp.Rows[i]["PCD"].ToString();
                    double qnt = double.Parse(loadTemp.Rows[i]["QNT"].ToString());
                    string amt = loadTemp.Rows[i]["AMT"].ToString();
                    string upc = loadTemp.Rows[i]["UPC"].ToString();
                    string fds = loadTemp.Rows[i]["FDS"].ToString();
                    string vty = loadTemp.Rows[i]["VTY"].ToString();
                    string qntMax = loadTemp.Rows[i]["QNTMAX"].ToString();
                    string name = loadTemp.Rows[i]["PR_NAME"].ToString();
                    string disPQ = loadTemp.Rows[i]["DISPQ"].ToString();
                    string rtcP = loadTemp.Rows[i]["RTCPRICE"].ToString();

                    UCItemInvoiceDetail ucitmDetail = new UCItemInvoiceDetail();
                    ucitmDetail.UCGridViewItemSellClick += UCGridViewItemSellClick2;
                    ucitmDetail.lbUC_No.Text = rec.ToString();
                    ucitmDetail.lbUC_ProductRec.Text = rec.ToString();
                    ucitmDetail.lbUC_ProductCode.Text = pcd;
                    ucitmDetail.lbUC_Qty.Text = qnt.ToString();
                    ucitmDetail.lbUC_Price.Text = double.Parse(upc).ToString(amtFormat);
                    ucitmDetail.lbUC_QtyMax.Text = double.Parse(qntMax).ToString(amtFormat);
                    ucitmDetail.lbUC_Discount.Text = double.Parse(fds).ToString(amtFormat);
                    ucitmDetail.lbUC_DisPQ.Text = double.Parse(disPQ).ToString(amtFormat);
                    ucitmDetail.lbUC_TotalPrice.Text = (double.Parse(amt) - double.Parse(fds)).ToString(amtFormat);
                    ucitmDetail.lbUC_ProductName.Text = name;
                    ucitmDetail.lbUC_RTCPrice.Text = rtcP;
                    pn_ItemPartial.Controls.Add(ucitmDetail);
                    if(qnt != 0)
                    {
                        disPriceP += double.Parse(fds);
                        totalQnt += qnt; 

                        if (vty == "1")
                        {
                        totalTax += (double.Parse(amt));
                        totalTaxDis += (double.Parse(fds));
                        }
                            
                    }       
                    amtPriceP += double.Parse(amt);

                    
                }
                RefreshGridInfo2();
            }

            //if (double.Parse(totalQty) == a)
            //{
            //    totalResult = (amtPriceP - disPriceP + upPrice - downPrice).ToString(amtFormat);
            //}
            //else
            //{
            //    totalResult = (amtPriceP - disPriceP).ToString(amtFormat);
            //}
            totalResult = (amtPriceP - disPriceP).ToString(amtFormat);
            lbTxtDebtCustomer.Text = totalResult;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panelDetailInvoice.BringToFront();
            panelSearchReceipt.BringToFront();
            panelSearchInvoice.BringToFront();
            panelKeyNumber.BringToFront();
            panelSearchInvoice.Controls.Clear();
            ucTxtSearch.Text = "";
            ucTxtSearch.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pn_receript_information.BringToFront();
            pn_input_type.BringToFront();
        }

        private void picBack1_Click(object sender, EventArgs e)
        {
            if (returnType == "F")
            {
                pn_receript_information.BringToFront();
                pn_input_type.BringToFront();
            }
            else if (returnType == "P")
            {
                pnReturnPartial.BringToFront();
                pn_select_product.BringToFront();
                panelKeyNumber.BringToFront();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            btnReturnPartial.Enabled = true;
            btnReturnPartial.BackgroundImage = Properties.Resources.confirm_enable;
            pn_select_product.BringToFront();
            panelKeyNumber.BringToFront();
        }

        public void ReturnFromInvoiceProcess()
        {
            pn_Payment.BringToFront();
            totalAmt = "0";
            if (returnType == "F")
            {
                totalAmt = (amtPriceF - disPriceF + upPrice - downPrice).ToString(amtFormat);
            }
            else if(returnType == "P") 
            {
                if (double.Parse(totalQty) == a)
                {
                    totalAmt = (amtPriceP - disPriceP + upPrice - downPrice).ToString(amtFormat);
                }
                else
                {
                    totalAmt = (amtPriceP - disPriceP).ToString(amtFormat);
                }
                
            }

            double amtAuto = 0.0;

            var res = process.displayReturnPayment(returnType, receiptNo, double.Parse(saleAmt), Convert.ToDouble(totalAmt));
            if (res.response.next)
            {
                _dtDisplayPayment = res.otherData;
                pn_PaymentReturn.Controls.Clear();
                foreach (DataRow dr in res.otherData.Rows)
                {
                    if (dr["PaymentCode"].ToString() != "RETN" && dr["PaymentCode"].ToString() != "VISA" && dr["PaymentCode"].ToString() != "MAST")
                    {
                        UCItemVoid ucitmListP = new UCItemVoid();
                        ucitmListP.lbUC_No.Text = dr["Seq"].ToString();
                        ucitmListP.lbUC_Payment.Text = dr["PaymentCode"].ToString().Trim();
                        ucitmListP.lbUC_Price.Text = dr["PaymentAmt"].ToString().Trim();

                        amtAuto += Convert.ToDouble(dr["PaymentAmt"]);

                        pn_PaymentReturn.Controls.Add(ucitmListP);
                        pn_PaymentReturn.Controls.SetChildIndex(ucitmListP, 0);
                    }
                }
            }
            else
            {
                Utility.AlertMessage(res);
            }


            ucTxtTotalReceive.Text = (Convert.ToDouble(totalAmt) - amtAuto).ToString(amtFormat);
            lbDebtCash.Text = "ยอดเงินคงค้างลูกค้า"; //string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbDebtCash"), totalAmt, ProgramConfig.currencyDefault);
            label1.Text = totalAmt;
            //lbDebtCash.Text = "ยอดเงินคงค้างลูกค้า " + amtPrice.ToString(amtFormat) + " บาท";                     

        }

        public void frmAllDisplayReasonData(string reasonID)
        {
            _reasonId = reasonID;
        }

        private void btnSubmit1_Click(object sender, EventArgs e)
        {
            //Profile chk0Price = ProgramConfig.getProfile(FunctionID.);
            
            if (double.Parse(ucTxtTotalReceive.Text) == 0)
            {
                Profile chk0Price = ProgramConfig.getProfile(FunctionID.Return_CheckAmountZeroPrice);
                if (chk0Price.policy == PolicyStatus.Skip)
                {
                    string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ReturnPriceZero").message;
                    string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ReturnPriceZero").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Error, string.Format(responseMessage, ProgramConfig.currencyDefault), helpMessage);

                    //frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้รับคืนสินค้ามูลค่า 0 " +ProgramConfig.currencyDefault, "");
                    dialog.ShowDialog(this);
                    return;
                }
                else if (chk0Price.policy == PolicyStatus.Work)
                {
                    returnProcess();
                }
                //else
                //{
                //    string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ReturnPriceZero").message;
                //    string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "ReturnPriceZero").help;
                //    frmNotify dialog = new frmNotify(ResponseCode.Error, string.Format(responseMessage, ProgramConfig.currencyDefault), helpMessage);

                //    //frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้รับคืนสินค้ามูลค่า 0 " + ProgramConfig.currencyDefault, "");
                //    dialog.ShowDialog(this);
                //    return;
                //}
            }
            else
            {
                returnProcess();
            }
            
            
        }

        public void returnProcess()
        {
            frmLoading.showLoading();
            string eventName = "ReturnInvoice";
            Profile checkReturn = ProgramConfig.getProfile(FunctionID.Return_InputReturnReason);
            if (checkReturn.policy == PolicyStatus.Work)
            {                
                frmAllDisplayReason displayReason = new frmAllDisplayReason(eventName);
                DialogResult displayReason_res = displayReason.ShowDialog(this);

                this._reasonId = displayReason._reasonID;
                this._reasonTxt = displayReason._reasonTxt;

                if (displayReason_res != DialogResult.Yes)
                {
                    if (displayReason_res != System.Windows.Forms.DialogResult.Retry)
                    {
                        frmLoading.closeLoading();
                        return;
                    }
                    else
                    {
                        defaultPage();
                        ucFooterTran1.IsStandAlone = true;
                        return;
                    }
                }                
            }

            saveReturnTempProcess();
            frmLoading.closeLoading();
            frmReturnSuccess frmSuccess = new frmReturnSuccess(ProgramConfig.returnRefNo, totalAmt, _reasonId, _reasonTxt, receiptNo, returnType, ProgramConfig.tillNo, ProgramConfig.userId, memberName, saleDate, TEMPDLYPTRANSFULL, TEMPDLYPTRANSPARTIAL, _printType);
            DialogResult frmSuccess_res = frmSuccess.ShowDialog(this);
            if (frmSuccess_res != DialogResult.Yes)
            {
                if (frmSuccess_res != System.Windows.Forms.DialogResult.Retry)
                {
                    textLanguageChange();
                    if (returnType == "F")
                    {
                        TEMPDLYPTRANSFULL.AcceptChanges();
                        foreach (DataRow row in TEMPDLYPTRANSFULL.Rows)
                        {
                            if (row["VTY"].ToString() == "V" || row["VTY"].ToString() == "P" || row["VTY"].ToString() == "F" || row["VTY"].ToString() == "C")
                            {
                                row.Delete();
                            }

                        }
                        TEMPDLYPTRANSFULL.AcceptChanges();
                    }
                    else if (returnType == "P")
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
                    return;
                }
                else
                {
                    //defaultPage();
                    //ucFooterTran1.IsStandAlone = true;
                    this.Dispose();
                    Program.control.ShowForm(this.Name);
                    return;
                }
            }
        }

        public void saveReturnTempProcess()
        {

            if (returnType == "F")
            {
                TEMPDLYPTRANSFULL.AcceptChanges();
                foreach (DataRow row in TEMPDLYPTRANSFULL.Rows)
                {
                    if (row["QNT"].ToString() == "0")
                    {
                        row.Delete();
                    }

                }
                TEMPDLYPTRANSFULL.AcceptChanges();

                int i = TEMPDLYPTRANSFULL.Rows.Count;
                if (i == 0)
                {
                    maxRec = "0";
                }
                else
                {
                    maxRec = (TEMPDLYPTRANSFULL.Rows[i - 1]["REC"]).ToString();
                }
            }
            else if (returnType == "P")
            {
                TEMPDLYPTRANSPARTIAL.AcceptChanges();
                foreach (DataRow row in TEMPDLYPTRANSPARTIAL.Rows)
                {
                    if (row["QNT"].ToString() == "0")
                    {
                        row.Delete();
                    }

                }
                TEMPDLYPTRANSPARTIAL.AcceptChanges();

                int i = TEMPDLYPTRANSPARTIAL.Rows.Count;
                if (i == 0)
                {
                    maxRec = "0";
                }
                else
                {
                    maxRec = (TEMPDLYPTRANSPARTIAL.Rows[i - 1]["REC"]).ToString();
                }

            }

            string newInstuRec = (double.Parse(maxRec) + 10).ToString();
            string totalReceive = ucTxtTotalReceive.Text;
            string totalResult = "0";

            if (returnType == "F")
            {
                totalResult = (amtPriceF - disPriceF).ToString(amtFormat);
            }
            else if (returnType == "P")
            {
                totalResult = (amtPriceP - disPriceP).ToString(amtFormat);
            }

            //Member
            if (memberID != null && memberID.Trim() != "")
            {
                if (returnType == "F")
                {
                    TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, "0", "3", "C", memberID, "1.00", totalAmt, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                    , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
                }
                else if (returnType == "P")
                {
                    if (double.Parse(totalQty) == a)
                    {
                        TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, "0", "3", "C", memberID, "1.00", totalAmt, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                    , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
                    }
                    else
                    {
                        TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, "0", "3", "C", memberID, "1.00", totalAmt, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                    , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
                    }
                }
            }


  
            //ตราสาร ไม่ auto
            //if (ucDropDownList.LabelText.Replace(" ", "") == ("RETN"))
            //{
            //    newInstuRec = (Convert.ToInt32(newInstuRec) + 1).ToString();
            //    if (returnType == "F")
            //    {
            //        TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", "RETN", "1.00", ucTxtTotalReceive.Text, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
            //                                        , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
            //    }
            //    else if (returnType == "P")
            //    {
            //        if (double.Parse(totalQty) == a)
            //        {
            //            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", "RETN", "1.00", ucTxtTotalReceive.Text, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
            //                                        , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
            //        }
            //        else
            //        {
            //            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", "RETN", "1.00", ucTxtTotalReceive.Text, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
            //                                        , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
            //        }
            //    }
            //}



            double amtEDC = _dtDisplayPayment.AsEnumerable().Sum(s => Convert.ToDouble(s["EDC_AMOUNT"].ToString() == "" ? "0.00" : s["EDC_AMOUNT"].ToString()));
            foreach (DataRow dr in _dtDisplayPayment.Rows)
            {
                if (!pn_PaymentReturn.Controls.Cast<UCItemVoid>().ToList().Any(a => a.lbUC_Payment.Text == dr["PaymentCode"].ToString()))
                {
                    double amt = 0.0;
                    string isEDC = "";
                    if (dr["PaymentCode"].ToString() != "RETN")
                    {
                        amt = Convert.ToDouble(dr["PaymentAmt"].ToString());
                        isEDC = "Y";
                    }
                    else
                    {
                        amt = Convert.ToDouble(ucTxtTotalReceive.Text);
                    }

                    newInstuRec = (Convert.ToInt32(newInstuRec) + 1).ToString();
                    if (returnType == "F")
                    {
                        TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", dr["PaymentCode"].ToString(), "1.00", amt, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                        , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1", "", "", isEDC);
                    }
                    else if (returnType == "P")
                    {
                        if (double.Parse(totalQty) == a)
                        {
                            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", dr["PaymentCode"].ToString(), "1.00", amt, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                        , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1", "", "", isEDC);
                        }
                        else
                        {
                            TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", dr["PaymentCode"].ToString(), "1.00", amt, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                        , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1", "", "", isEDC);
                        }
                    }
                }
            }
    

            //ตราสาร auto
            foreach (UCItemVoid item in pn_PaymentReturn.Controls.Cast<UCItemVoid>().ToList())
            {
                newInstuRec = (Convert.ToInt32(newInstuRec) + 1).ToString();
                if (returnType == "F")
                {
                    TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", item.lbUC_Payment.Text, "1.00", item.lbUC_Price.Text, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                    , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
                }
                else if (returnType == "P")
                {
                    if (double.Parse(totalQty) == a)
                    {
                        TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", item.lbUC_Payment.Text, "1.00", item.lbUC_Price.Text, upPrice, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                    , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
                    }
                    else
                    {
                        TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newInstuRec, "3", "P", item.lbUC_Payment.Text, "1.00", item.lbUC_Price.Text, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                    , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
                    }
                }


            }

            //StoreResult saveInstrumentTemp = process.saveTempDlyptrans(ProgramConfig.returnRefNo, newInstuRec, "3", "P", "RETN", "1.00", totalResult, "0.00", ProgramConfig.userId
            //                        , "0.00", "", "0.00", "0", "0.00", "1", "0.00", "0", "");
            //if (saveInstrumentTemp.response == ResponseCode.Error)
            //{
            //    frmNotify dialog = new frmNotify(ResponseCode.Error, saveInstrumentTemp.responseMessage, saveInstrumentTemp.helpMessage);
            //    dialog.ShowDialog(this);
            //    return;
            //}


            //}
            //Vat
            string vatPercent = ProgramConfig.vatRate;
            newVatRec = (double.Parse(newInstuRec) + 1).ToString();
            double v = ((totalTax - totalTaxDis) * double.Parse(vatPercent)) / (100 + double.Parse(vatPercent));
            double mat = Math.Round(v, 2);

            if (returnType == "F")
            {
                TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newVatRec, "3", "V", "Vat Return", (totalTax - totalTaxDis).ToString(), mat, vatPercent, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
            }
            else if (returnType == "P")
            {
                TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, newVatRec, "3", "V", "Vat Return", (totalTax - totalTaxDis).ToString(), mat, vatPercent, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                , "0.00", "", "", "0", "0.00", "0", "0.00", "0.00", "1");
            }
            //StoreResult saveVatTemp = process.saveTempDlyptrans(ProgramConfig.returnRefNo, newVatRec, "3", "V", "Vat Return", totalTax.ToString(), vat, vatPercent, ProgramConfig.userId
            //                            , "0.00", "", "0.00", "0", "0.00", "1", "0.00", "0", "");
            //if (saveVatTemp.response == ResponseCode.Error)
            //{
            //    frmNotify dialog = new frmNotify(ResponseCode.Error, saveVatTemp.responseMessage, saveVatTemp.helpMessage);
            //    dialog.ShowDialog(this);
            //    return;
            //}

            //Final Record
            string finalRec = (double.Parse(newVatRec) + 1).ToString();
            string loc = receiptNo.Substring(0, 3);
            string runnningNum = receiptNo.Substring(3, 6);
            string newRunning = (double.Parse(runnningNum) + 100000000 + 100000000).ToString();
            string mode = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigTill_SaleModeType.parameterCode).ToString();
            string superId = "";
            string upc = "";

            if (mode == "1")
            {
                upc = "0.00";
            }
            else if (mode == "2")
            {
                upc = "1.00";

            }
            string dateForm = saleDate.Replace("/", "").Replace(" ", "").Replace(":", "");

            if (ProgramConfig.superUserId == "" || ProgramConfig.superUserId == null || ProgramConfig.superUserId == "N/A")
            {
                superId = "0";

            }
            else
            {
                superId = ProgramConfig.superUserId;
            }

            if (returnType == "F")
            {
                TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, finalRec, "3", "F", receiptNo + " Return", totalQnt, totalAmt, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                , loc, "", "", _reasonId, superId, "", dateForm, upc, "1");


            }
            else if (returnType == "P")
            {
                TEMPDLYPTRANSPARTIAL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, finalRec, "3", "F", receiptNo + " Return", a, totalAmt, "0.00", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId
                                                , loc, "", "", _reasonId, superId, "", dateForm, upc, "1");
            }


        }

        private void ucHeader1_MemberClick(object sender, EventArgs e)
        {
            panelMember.BringToFront();
        }

        private void ucTBWI_Member_IconClick(object sender, EventArgs e)
        {
            string eventName = "ReturnInvoice";
            Profile check = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember);
            if (check.policy == PolicyStatus.Work)
            {
                frmSearchMember frm = new frmSearchMember((UCTextBoxWithIcon)sender, eventName);
                frm.ShowDialog(this);
            }
            ucHeader1.nameText = ucTBWI_Member.Text;
            ucHeader1.nameVisible = true;
            Label newFont = new Label();
            newFont.Font = new Font("Microsoft Sans Serif", 14);
            int checkWidth = TextRenderer.MeasureText(ucTBWI_Member.Text, newFont.Font).Width;
            ucHeader1.pnNameSize = new Size(40 + checkWidth, 45);
            panelMember.SendToBack();

        }

        public void frmSearchMemberData(string memberData, string memberDataName)
        {
            this.memberID = memberData;
            ProgramConfig.memberId = memberID;
            this.memberName = memberDataName;
            ProgramConfig.memberName = memberName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string eventName = "ReturnInvoice";
            Profile check = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember);
            if (check.policy == PolicyStatus.Work)
            {
                frmSearchMemberAuto frm = new frmSearchMemberAuto(ucTBWI_Member.Text, eventName);
                frm.ShowDialog(this);
            }

            ucHeader1.nameText = memberName;
            ucHeader1.nameVisible = true;
            //e.Graphics.MeasureString(ucTBWI_Member.Text, SystemFonts.DefaultFont).Width);
            Label newFont = new Label();
            newFont.Font = new Font("Microsoft Sans Serif", 14);
            int checkWidth = TextRenderer.MeasureText(memberName, newFont.Font).Width;
            //base {System.MarshalByRefObject} = {Name = "Microsoft Sans Serif" Size=8.25}
            ucHeader1.pnNameSize = new Size(40 + checkWidth, 45);
            panelMember.SendToBack();
        }

        public void memberProcess()
        {
            StoreResult getMemberProfile = process.getMemberProfile(memberID);
            if (getMemberProfile.response.next)
            {
                if (getMemberProfile.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(getMemberProfile);
                    dialog.ShowDialog(this);
                }

                taxId = getMemberProfile.otherData.Rows[0]["IDCARD"].ToString();

            }
            else
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, getMemberProfile.responseMessage, getMemberProfile.helpMessage);
                dialog.ShowDialog(this);
                return;

            }

            StoreResult getCustomerFFTI = process.getCustomerFFTI(taxId);
            if (getCustomerFFTI.response.next)
            {
                if (getCustomerFFTI.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(getCustomerFFTI);
                    dialog.ShowDialog(this);
                }
                taxId = getMemberProfile.otherData.Rows[0]["IDCARD"].ToString();

            }
            else
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, getCustomerFFTI.responseMessage, getCustomerFFTI.helpMessage);
                dialog.ShowDialog(this);
                return;

            }

        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList()
        {

            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();

            Profile chkDisplayReturnPayment = ProgramConfig.getProfile(FunctionID.Return_SuggestReturnPaymentType);
            if (chkDisplayReturnPayment.policy == PolicyStatus.Work)
            {
                displayReturnPayment = process.displayReturnPayment(returnType, receiptNo, double.Parse(saleAmt), double.Parse(ucTxtTotalReceive.Text));
            }

            if (displayReturnPayment.otherData != null)
            {
                DataTable dt = displayReturnPayment.otherData;
                dt = dt.AsEnumerable().OrderByDescending(o => Convert.ToInt32(o["seq"])).CopyToDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    drItem.DisplayText = dr["PaymentDesc"].ToString();
                    //drItem.ValueText = dr["ReasonID"].ToString();
                    lstStr.Add(drItem);
                }
            }
            return lstStr;
        }

        private void ucHeader1_MainMenuClick_1(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            Dispose();
            frmLoading.closeLoading();
        }

        private void ucTxtSearch_EnterFromButton(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            searchReceiptProcess();
            frmLoading.closeLoading();
        }

        private void ucTxtSearch_TextBoxKeydown(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            searchReceiptProcess();
            frmLoading.closeLoading();
        }

        private void ucSearchType_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            ucTxtSearch.Focus();
        }

        private void ucDropDownList_UCDropDownListClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList();
            }
            frmLoading.closeLoading();
        }

        private void btnReturnPartial_Click(object sender, EventArgs e)
        {
            if (a <= 0)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, lbReturnQty0.Text);
                dialog.ShowDialog(this);
                return;
            }
            else
            {
                pn_Payment.BringToFront();
                ReturnFromInvoiceProcess();
                panelKeyNumber.SendToBack();
            }
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            textLanguageChange();
        }

        public void textLanguageChange()
        {
            if (currentQty != null || currentQty != "")
            {
                lbCurrentSell.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentSell"), currentQty);
            }
            if (totalAmt != null || totalAmt != "")
            {
                lbDebtCash.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbDebtCash"), totalAmt, ProgramConfig.currencyDefault);
            }
        }

        private void ucTBScanBarcode_EnterFromButton2(object sender, EventArgs e)
        {
            string strSearch = ((UCTextBoxWithIcon)sender).Text.Trim();
            ((UCTextBoxWithIcon)sender).Text = "";
            foreach (var item in pn_ItemPartial.Controls)
            {
                if (item is UCItemInvoiceDetail)
                {
                    UCItemInvoiceDetail uc = (UCItemInvoiceDetail)item;
                    if (uc.lbUC_ProductCode.Text.Trim() == strSearch)
                    {
                        UCGridViewItemSell_Click2(uc);
                        return;
                    }
                }
            }

            Utility.AlertMessage(ResponseCode.Error, "No item search");
        }
    }
}
