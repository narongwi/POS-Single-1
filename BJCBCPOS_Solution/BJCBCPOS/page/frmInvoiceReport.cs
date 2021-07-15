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
    public partial class frmInvoiceReport : Form
    {
        public frmMainMenu frmMainMenu;
        string searchType;
        public UCTextBoxWithIcon ucTBWI { get; set; }
        private ReportProcess process = new ReportProcess();
        public UCTextBoxSmall ucTBS { get; set; }
        public int cnt { get; set; }
        public UCItemInvoice ucGV;
        public UCItemSell ucGV2;
        double amtPrice = 0;
        double disPrice = 0;
        double changeValue = 0;
        double totalTax = 0;
        double vatValue = 0;
        double totalPrice = 0;
        string change;
        string receiptNo;
        string saleDate;
        string cashier;
        string totalQty;
        string totalAmt;
        string totalDisc;
        string dateString;
        string vat;
        string vatType;
        string dty;
        string cashType;
        string cashLabel;
        string creditLabel;
        string creditNumber;
        string value;
        string value1 = "";
        string value2 = "";
        double upPrice = 0;
        double downPrice = 0;
        string cashValue;
        string creditValue;
        string newAmt;
        string newQty;
        int productRec;
        string productPrice;
        string returnType;
        string paymentType;
        string saleAmt;
        string returnAmt;
        string saleRef;
        string reasonId;
        string memberID;
        string memberName;
        string taxId;
        string currentQty;
        StoreResult displayReturnPayment = null;
        string amtFormat = ProgramConfig.amountFormatString;
        string totalResult;
        string vty;
        string disType = "";
        string maxRec;
        string newVatRec;
        string chkPartial;
        string tillNo;
        string lockNo;
        StoreResult otherTillList = null;
        Profile chkOtherTill = null;
        public bool getAutherize = false;
        public bool needPrintAutherize = false;
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public frmInvoiceReport()
        {
            InitializeComponent();
            //TEST//
        }

        private void frmInvoiceReport_Load(object sender, EventArgs e)
        {
            try
            {
                AppMessage.fillForm(ProgramConfig.language, this);
                //Class1.InitialTextBoxIcon(ucTxtSearch, BJCBCPOS.Properties.Resources.icon_textbox_scan, Class1.UCTextBoxIconType.ScanAndDelete, Class1.IconType.Scan, AppMessage.getMessage(ProgramConfig.language, this.Name, "ucTxtSearch.placeHolder"));
                StoreResult result = null;
                chkOtherTill = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_ReportOtherTill);
                ucSearchTillType.LabelText = "Till " + ProgramConfig.tillNo;
                ucSearchTillType.ValueText = ProgramConfig.tillNo;
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "ReceiptNo").message;
                ucSearchType.LabelText = responseMessage;
                ucSearchType.ValueText = "1";

                if (chkOtherTill.policy == PolicyStatus.Work)
                {
                    ucSearchTillType.Enabled = true;

                }
                else if (chkOtherTill.policy == PolicyStatus.Skip)
                {
                    ucSearchTillType.Enabled = false;
                }

                if (chkOtherTill.profile == ProfileStatus.Authorize)
                {
                    getAutherize = true;
                }
                else
                {
                    getAutherize = false;
                }

                //DisplayContent อย่าลืม
                result = process.posDisplayContent();
                if (result.response.next)
                {
                    if (result.response == ResponseCode.Information)
                    {
                        frmNotify dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                    }

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
                else
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                }

                defaultSetting();                
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    defaultSetting();
                    //this.Dispose();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownTillList()
        {

            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();

            if (otherTillList.otherData != null)
            {
                DataTable dt = otherTillList.otherData;
                dt = dt.AsEnumerable().OrderByDescending(o => Convert.ToInt32(o["seq"])).CopyToDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    drItem.DisplayText = "Till "+ dr["TillNo"].ToString();
                    drItem.ValueText = dr["TillNo"].ToString();
                    lstStr.Add(drItem);
                }
                //drItem.DisplayText = "Till " + ProgramConfig.tillNo.ToString();
                //drItem.ValueText = ProgramConfig.tillNo.ToString();
                //lstStr.Add(drItem);
            }
            return lstStr;
        }

        private void ucSearchTillType_UCDropDownListClick(object sender, EventArgs e)
        {
            if (chkOtherTill.profile == ProfileStatus.Authorize)
            {
                otherTillList = process.getTillNo4DispReport(FunctionID.Report_CheckCurrentDayReceipt_ReportOtherTillList);
                if (!otherTillList.response.next)
                {
                    if (sender is UCDropDownList)
                    {
                        var ucDDL = (UCDropDownList)sender;
                        ucDDL.lstDDL = SetDataucDropDownListBlank();
                    }

                    frmNotify dialog = new frmNotify(otherTillList);
                    dialog.ShowDialog(this);
                    return;
                }
                else
                {
                    if (otherTillList.response == ResponseCode.Information)
                    {
                        frmNotify dialog = new frmNotify(otherTillList);
                        dialog.ShowDialog(this);
                    }

                    if (sender is UCDropDownList)
                    {
                        var ucDDL = (UCDropDownList)sender;
                        ucDDL.lstDDL = SetDataucDropDownTillList();
                    }
                }
            }
            else if (chkOtherTill.profile == ProfileStatus.NotAuthorize)
            {
                
                if (getAutherize == false)
                {
                    //frmLoading.showLoading();
                    //frmUserAuthorize auth = new frmUserAuthorize("ReportReceipt", chkOtherTill.diffUserStatus);
                    //auth.function = FunctionID.Report_CheckCurrentDayReceipt_ReportOtherTill;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    if (sender is UCDropDownList)
                    //    {
                    //        var ucDDL = (UCDropDownList)sender;
                    //        ucDDL.lstDDL = SetDataucDropDownListBlank();
                    //    }
                    //    frmLoading.closeLoading();
                    //    return;
                    //}
                    //else
                    //{
                    //    getAutherize = true;
                    //}

                    if (!Utility.CheckAuthPass(this, chkOtherTill, "ReportReceipt"))
                    {
                        if (sender is UCDropDownList)
                        {
                            var ucDDL = (UCDropDownList)sender;
                            ucDDL.lstDDL = SetDataucDropDownListBlank();
                        }
                        frmLoading.closeLoading();
                        return;
                    }
                    else
                    {
                        getAutherize = true;
                    }
                    frmLoading.closeLoading();
                }
                

                otherTillList = process.getTillNo4DispReport(FunctionID.Report_CheckCurrentDayReceipt_ReportOtherTillList);
                if (!otherTillList.response.next)
                {
                    if (sender is UCDropDownList)
                    {
                        var ucDDL = (UCDropDownList)sender;
                        ucDDL.lstDDL = SetDataucDropDownListBlank();
                    }

                    frmNotify dialog = new frmNotify(otherTillList);
                    dialog.ShowDialog(this);
                    return;
                }
                else
                {
                    if (otherTillList.response == ResponseCode.Information)
                    {
                        frmNotify dialog = new frmNotify(otherTillList);
                        dialog.ShowDialog(this);
                    }

                    if (sender is UCDropDownList)
                    {
                        var ucDDL = (UCDropDownList)sender;
                        ucDDL.lstDDL = SetDataucDropDownTillList();
                    }
                }
            }
        }

        public void defaultSetting()
        {
            
            pn_Item.Controls.Clear();
            pnTypePDetail.Controls.Clear();
            ucTxtSearch.Text = "";
            lbTxtDiscountTotal.Text = "0";
            lbTxtTotal.Text = "0";
            lbTxtTotalRes.Text = "0";
            panelDetailInvoice.BringToFront();
            panelKeyNumber.BringToFront();
            panelSearchReceipt.BringToFront();
            ucTxtSearch.Select();
            ucTxtSearch.Focus();

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

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            closeForm();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            defaultSetting();
        }

        private void ucSearchType_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList();
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            Profile check1 = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_Search_SearchByReceiptNo);
            Profile check2 = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_Search_SearchByProductCode);
            Profile check3 = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_Search_SearchByProductName);
            Profile check4 = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_Search_SearchByMemberID);
            Profile check5 = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_Search_SearchByMemberName);
            Profile check6 = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_Search_SearchByPaymentType);

            if (check6.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "InstrumentType").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "6" });
            }
            if (check5.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "MemberName").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "5" });
            }
            if (check4.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "MemberID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "4" });
            }
            if (check3.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "ProductName").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "3" });
            }
            if (check2.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "ProductCode").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "2" });
            }
            if (check1.policy == PolicyStatus.Work)
            {
                string responseMessage = ProgramConfig.message.get("frmInvoiceReport", "ReceiptNo").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = responseMessage, ValueText = "1" });
            }
            return lstStr;
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownListBlank()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = "Till " + ProgramConfig.tillNo, ValueText = ProgramConfig.tillNo });
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
            amtPrice = 0;
            disPrice = 0;
            changeValue = 0;
            totalTax = 0;
            vatValue = 0;
            totalPrice = 0;
        }

        public void searchReceiptProcess()
        {
            try
            {
                defaultValue();
                panelSearchInvoice.Controls.Clear();
                searchType = ucSearchType.ValueText;
                tillNo = ucSearchTillType.ValueText;
                StoreResult searchReceipt = process.searchReceipt(Convert.ToInt32(searchType), ucTxtSearch.Text, tillNo, getAutherize);
                if (searchReceipt.response.next)
                {
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
                            lockNo = receiptNo.Substring(0, 3);
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
                    ucTxtSearch.Text = "";
                    ucTxtSearch.Focus();
                    return;
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    defaultSetting();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void UCGridViewItemSellClick(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                ucGV = (UCItemInvoice)sender;
                receiptNo = ucGV.lbUC_ReceiptNo.Text;
                showDetailInvoice();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    defaultSetting();
                    //this.Dispose();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void showDetailInvoice()
        {            
            pnTypePDetail.Controls.Clear();
            pn_Item.Controls.Clear();

            StoreResult displayReceiptDetail = process.displayReceiptDetail(receiptNo, tillNo, getAutherize);

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
                        if (dataType == "I")
                        {
                            if (line == "1")
                            {
                                UCItemSell ucitmDetail = new UCItemSell();
                                ucitmDetail.lbNo.Text = rec.ToString();
                                ucitmDetail.lbUC_ProductCode.Text = codeOrName;
                                ucitmDetail.lbUC_Qty.Text = double.Parse(qty).ToString();
                                ucitmDetail.lbUC_Price.Text = double.Parse(price).ToString(amtFormat);
                                ucitmDetail.lbUC_Discount.Text = double.Parse(dis).ToString(amtFormat);
                                ucitmDetail.lbUC_TotalPrice.Text = double.Parse(amt).ToString(amtFormat);
                                StoreResult chkVatItem = process.getProductDesc(codeOrName);
                                vatType = chkVatItem.otherData.Rows[0]["PR_VAT"].ToString();
                                if (vatType == "V")
                                {
                                    vty = "1";
                                    totalTax += double.Parse(amt);
                                }
                                else
                                {
                                    vty = "0";
                                }
                                dty = chkVatItem.otherData.Rows[0]["PR_DTYPE"].ToString();
                                disPrice += double.Parse(dis);
                                amtPrice += double.Parse(amt);
                                if (double.Parse(qty) == 1 && double.Parse(price) != double.Parse(amt) && double.Parse(dis) == 0)
                                {
                                    totalPrice += double.Parse(ucitmDetail.lbUC_TotalPrice.Text);
                                }
                                else
                                {
                                    totalPrice += (double.Parse(ucitmDetail.lbUC_Price.Text) * double.Parse(ucitmDetail.lbUC_Qty.Text));
                                }
                                codeOrName = displayReceiptDetail.otherData.Rows[i + 1]["ProductDesc"].ToString();
                                ucitmDetail.lbProductName.Text = codeOrName;

                                //TEMPDLYPTRANSFULL.Rows.Add(ProgramConfig.storeCode, ProgramConfig.returnRefNo, rec, "3", vty, ucitmDetail.lbProductCode.Text
                                //                , ucitmDetail.lbQty.Text, ucitmDetail.lbTotalPrice.Text, dis, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), ProgramConfig.userId, vatValue
                                //                , "", "", "0", "0.00", "0", "0.00", ucitmDetail.lbPrice.Text, dty);
                                

                                pn_Item.Controls.Add(ucitmDetail);
                                i += 2;
                            }

                        }
                        else if (dataType == "P")
                        {
                            paymentType = displayReceiptDetail.otherData.Rows[i]["ProductDesc"].ToString().Replace(" ", "");
                            //change = displayReceiptDetail.otherData.Rows[i]["Discount"].ToString();
                            //value = displayReceiptDetail.otherData.Rows[i]["Amount"].ToString();
                            value1 = displayReceiptDetail.otherData.Rows[i]["Discount"].ToString();
                            value2 = displayReceiptDetail.otherData.Rows[i]["Amount"].ToString();
                            string fxcuAmt = displayReceiptDetail.otherData.Rows[i]["fxcu_Amt"].ToString();

                            //changeValue += double.Parse(change);
                            //UCItemVoid ucitmListP = new UCItemVoid();
                            //ucitmListP.lbNo.Text = rec.ToString();
                            //ucitmListP.lbPayment.Text = codeOrName;
                            if (paymentType == "CHGD")
                            {
                                upPrice = double.Parse(value1);
                                downPrice = double.Parse(value2);
                                UCItemVoid ucitmListP = new UCItemVoid();
                                ucitmListP.lbUC_No.Text = rec.ToString();
                                ucitmListP.lbUC_Payment.Text = codeOrName;
                                ucitmListP.lbUC_Price.Text = (upPrice + downPrice).ToString(amtFormat);
                                pnTypePDetail.Controls.Add(ucitmListP);
                                pnTypePDetail.Controls.SetChildIndex(ucitmListP, 0);
                                i++;
                            }
                            else if (paymentType.Length >= 4 && paymentType.Substring(0, 4) == "FXCU")
                            {
                                changeValue += double.Parse(value1);
                                UCItemVoid ucitmListP = new UCItemVoid();
                                ucitmListP.Size = new System.Drawing.Size(ucitmListP.Size.Width, 55);
                                ucitmListP.picUnderLine.Location = new Point(0, 54);
                                ucitmListP.lbUC_No.Text = rec.ToString();
                                ucitmListP.lbUC_Payment.Text = codeOrName;
                                ucitmListP.lbUC_Price.Text = double.Parse(amt).ToString(amtFormat);
                                ucitmListP.lbUC_PriceCurrency.Text = fxcuAmt;
                                pnTypePDetail.Controls.Add(ucitmListP);
                                pnTypePDetail.Controls.SetChildIndex(ucitmListP, 0);
                                i++;
                            }
                            else
                            {
                                changeValue += double.Parse(value1);
                                UCItemVoid ucitmListP = new UCItemVoid();
                                ucitmListP.lbUC_No.Text = rec.ToString();
                                ucitmListP.lbUC_Payment.Text = codeOrName;
                                ucitmListP.lbUC_Price.Text = double.Parse(amt).ToString(amtFormat);
                                pnTypePDetail.Controls.Add(ucitmListP);
                                pnTypePDetail.Controls.SetChildIndex(ucitmListP, 0);
                                i++;
                            }
                            

                        }
                        else if (dataType == "V")
                        {
                            vat = displayReceiptDetail.otherData.Rows[i]["Amount"].ToString();
                            i++;

                        }
                        else
                        {
                            i++;
                        }
                    }

                    for (int j = 0; j < displayReceiptDetail.otherData.Rows.Count; j++)
                    {
                        chkPartial = displayReceiptDetail.otherData.Rows[j]["ReturnPartial"].ToString();
                        if (chkPartial == "N")
                        {
                            break;
                        }
                    }

                    AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pnTypePDetail);
                    Utility.SetGridColorAlternate<UCItemVoid>(pnTypePDetail.Controls.Cast<UCItemVoid>().ToList(), Color.FromArgb(225, 225, 225));

                    RefreshGridInfo();
                    pn_DisplayDetail.BringToFront();
                    lbTxtRefNo.Text = receiptNo;
                    lbTxtDiscountTotal.Text = disPrice.ToString(amtFormat);
                    lbTxtTotal.Text = (totalPrice).ToString(amtFormat);
                    lbTxtTotalRes.Text = (amtPrice + upPrice - downPrice).ToString(amtFormat);
                    lbWorkOnResult.Text = ProgramConfig.serverName;
                    lbReceiptNoResult.Text = ucTxtSearch.Text;
                    lbLockNoResult.Text = lockNo;
                    lbUserIdResult.Text = cashier;
                    lbDateResult.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy HH:mm:ss");

                    DataTable UserNameLocal = process.SelectName(cashier);
                    if (UserNameLocal != null)
                    {
                        lbStaffName.Text = UserNameLocal.Rows[0]["UserNameLocal"].ToString();
                    }
                    else
                    {
                        lbStaffName.Text = "";
                    }
                    
                    lbCashBalanceResult.Text = changeValue.ToString(amtFormat);
                    lbTotalTaxResult.Text = (totalTax).ToString(amtFormat);
                    lbTaxResult.Text = double.Parse(vat).ToString(amtFormat);

                    Profile check = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_PrintReceiptCopy);
                    if (check.policy == PolicyStatus.Work)
                    {
                        btnPrint.Enabled = true;
                        btnPrint.BackgroundImage = BJCBCPOS.Properties.Resources.multi_enable;
                    }
                    else if (check.policy == PolicyStatus.Work)
                    {
                        btnPrint.Enabled = false;
                        btnPrint.BackgroundImage = BJCBCPOS.Properties.Resources.multi_disable;
                    }

                    pn_receript_information.BringToFront();
                    pn_Item.BringToFront();
                }
            }
            else
            {
                defaultPage();
                frmNotify dialog = new frmNotify(displayReceiptDetail);
                dialog.ShowDialog(this);

                if (displayReceiptDetail.response == ResponseCode.Error && ucTxtSearch.Text.Trim() != "")
                {
                    if (getAutherize == false)
                    {
                        //frmUserAuthorize auth = new frmUserAuthorize("ReportReceipt", chkOtherTill.diffUserStatus);
                        //auth.function = FunctionID.Report_CheckCurrentDayReceipt_ReportOtherTill;
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    return;
                        //}
                        //else
                        //{
                        //    getAutherize = true;
                        //    showDetailInvoice();                            
                        //}

                        if (!Utility.CheckAuthPass(this, chkOtherTill, "ReportReceipt"))
                        {
                            return;
                        }
                        else
                        {
                            getAutherize = true;
                            showDetailInvoice();   
                        }
                    }
                }

                ucTxtSearch.Text = "";
                ucTxtSearch.Focus();
                return;

            }
            ucTxtSearch.Text = "";
            ucTxtSearch.Focus();


        }

        public void defaultPage()
        {
            panelDetailInvoice.BringToFront();
            panelSearchReceipt.BringToFront();
            panelKeyNumber.BringToFront();
            panelSearchReceipt.BringToFront();
            ucTxtSearch.Select();
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
            List<UCItemSell> lstItemReturn = new List<UCItemSell>();
            lstItemReturn = pn_Item.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_Item.Controls.Clear();
            int num = lstItemReturn.Count;
            //double a = 0;

            foreach (UCItemSell item in lstItemReturn)
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
            ScrollToBottom(pnTypePDetail);
        }

        private void RefreshGrid()
        {
            List<UCItemInvoice> lstItemReturn = new List<UCItemInvoice>();
            lstItemReturn = panelSearchInvoice.Controls.Cast<UCItemInvoice>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panelSearchInvoice.Controls.Clear();
            int num = lstItemReturn.Count;

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
                panelSearchInvoice.Controls.Add(item);
                num--;
            }

            ScrollToBottom(panelSearchInvoice);
            
        }

        private void ucSearchTillType_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            ucTxtSearch.Focus();
        }

        private void ucSearchType_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            ucTxtSearch.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                Profile check = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt_PrintReceiptCopy);
                if (check.profile == ProfileStatus.NotAuthorize)
                {
                    needPrintAutherize = true;
                    //frmUserAuthorize auth = new frmUserAuthorize("ReportReceipt", check.diffUserStatus);
                    //auth.function = FunctionID.Report_CheckCurrentDayReceipt_PrintReceiptCopy;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    frmLoading.closeLoading();
                    //    return;
                    //}
                    if (!Utility.CheckAuthPass(this, chkOtherTill, "ReportReceipt"))
                    {
                        frmLoading.closeLoading();
                        return;
                    }
                    frmLoading.closeLoading();
                    printReport();

                }
                else if (check.profile == ProfileStatus.Authorize)
                {
                    needPrintAutherize = false;
                    frmLoading.closeLoading();
                    printReport();
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    defaultSetting();
                    //this.Dispose();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void printReport()
        {
            frmLoading.showLoading();
            StoreResult printReceipt = process.printReceiptReport(receiptNo, needPrintAutherize);
            if (!printReceipt.response.next)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(printReceipt);
                dialog.ShowDialog(this);
                return;
            }
            else
            {
                if (printReceipt.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(printReceipt);
                    dialog.ShowDialog(this);
                }

            }
            DataTable dt = printReceipt.otherData;
            Hardware.printTermal(dt);
            closeForm();
            frmLoading.closeLoading();        
        }

        public void closeForm()
        {
            ProgramConfig.superUserId = string.Empty;
            ProgramConfig.superPassword = string.Empty;
            ProgramConfig.superUserName = string.Empty;
            ProgramConfig.superUserAuthorizeResult = null;

            foreach (Form item in Application.OpenForms)
            {
                if (item is frmMainMenu)
                {
                    frmMainMenu = (frmMainMenu)item;
                    frmMainMenu.BringToFront();
                    break;
                }
            }
            Dispose();
        }
    }
}
