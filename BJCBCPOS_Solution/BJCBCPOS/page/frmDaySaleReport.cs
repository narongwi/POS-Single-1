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
    public partial class frmDaySaleReport : Form
    {
        Point pnt = new Point();
        public frmMainMenu frmMainMenu;
        UCDropDownList currentUCDDL;
        string searchType;
        public UCTextBoxWithIcon ucTBWI { get; set; }
        private ReportProcess process = new ReportProcess();
        public UCTextBoxSmall ucTBS { get; set; }
        public int cnt { get; set; }
        public UCItemInvoice ucGV;
        public UCItemInvoiceDetail ucGV2;
        public UCItemInvoice lastUCIS;
        public UCItemInvoiceDetail lastUCIS2;
        double amtPrice = 0;
        double disPrice = 0;
        double changeValue = 0;
        double totalTax = 0;
        double vatValue = 0;
        string change;
        string receiptNo;
        string saleDate;
        string cashier;
        string totalQty;
        string totalAmt;
        string totalDisc;
        string amtFormat = ProgramConfig.amountFormatString;
        string tillNo;
        string repNo;
        DataTable sumTransDetail = null;
        StoreResult otherTillList = null;
        Profile chkOtherTill = null;
        private bool getAutherize = false;
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public frmDaySaleReport()
        {
            InitializeComponent();
 
        }

        private void frmInvoiceReport_Load(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                StoreResult result = null;
                chkOtherTill = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDaySale_ReportOtherTill);
                ucSearchTillType.LabelText = "Till " + ProgramConfig.tillNo;
                ucSearchTillType.ValueText = ProgramConfig.tillNo;

                if (chkOtherTill.policy == PolicyStatus.Work)
                {
                    ucSearchTillType.Enabled = true;
                }
                else if (chkOtherTill.policy == PolicyStatus.Skip)
                {
                    ucSearchTillType.Enabled = false;
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
                daySalesReportProcess();
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

        public void daySalesReportProcess()
        {
            frmLoading.showLoading();
            StoreResult posRepNo = process.repSumTrans(ucSearchTillType.ValueText);
            frmLoading.closeLoading();
            if (!posRepNo.response.next)
            {
                frmNotify dialog = new frmNotify(posRepNo);
                dialog.ShowDialog(this);
                this.Dispose();
                return;
            }
            else
            {
                repNo = posRepNo.otherData.Rows[0]["PosRepNo"].ToString();
                ProgramConfig.posrepRefNo = posRepNo.otherData.Rows[0]["PosRepNo"].ToString();
                ProgramConfig.posrepRefNoIni = posRepNo.otherData.Rows[0]["PosRepNoINI"].ToString();
                if (posRepNo.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(posRepNo);
                    dialog.ShowDialog(this);
                }
            }

            lbNameRes.Text = "";
            lbStoreRes.Text = ProgramConfig.storeFullname;
            lbAddressRes.Text = ProgramConfig.address;
            lbVatRes.Text = ProgramConfig.vatRate + "%";

            lbDateRes.Text = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo(ProgramConfig.language.culture));

            lbPrintDateRes.Text = DateTime.Now.ToString();
            lbTaxIdRes.Text = ProgramConfig.taxId;
            lbStoreCodeRes.Text = ProgramConfig.storeCode;
            
            DataTable sumTransHeader = process.SumTRansHEader(repNo);

            if (sumTransHeader != null)
            {
                lbTotalSalesRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_sales"].ToString()).ToString(amtFormat);
                lbTotalVatableRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_vatable"].ToString()).ToString(amtFormat);
                lbTotalVatRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_vat"].ToString()).ToString(amtFormat);
                lbSaleexcLVatRes.Text = double.Parse(sumTransHeader.Rows[0]["Sales_Excl_Vat"].ToString()).ToString(amtFormat);
                lbTotalReceipReturnRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_ret"].ToString()).ToString(amtFormat);
                lbTotalReturnQuantRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_ret"].ToString()).ToString(amtFormat);

                lbAverangeSalesRes.Text = double.Parse(sumTransHeader.Rows[0]["avg_Sales"].ToString()).ToString(amtFormat);
                lbTotalDiscountRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_discount"].ToString()).ToString(amtFormat);
                lbOrderDeskRes.Text = double.Parse(sumTransHeader.Rows[0]["order_desk"].ToString()).ToString(amtFormat);
                lbTotalReceiptRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_rcv"].ToString()).ToString(amtFormat);
                lbTotalReturnAmountRes.Text = double.Parse(sumTransHeader.Rows[0]["ret_amt"].ToString()).ToString(amtFormat);
                lbTotalReturnAmountExVatRes.Text = double.Parse(sumTransHeader.Rows[0]["Ret_Excl_Vat"].ToString()).ToString(amtFormat);

                lbTotalQuantityRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_quant"].ToString()).ToString();
                lbTotalRecordRes.Text = double.Parse(sumTransHeader.Rows[0]["tot_Record"].ToString()).ToString(amtFormat);
                lbTotalReceipt2Res.Text = double.Parse(sumTransHeader.Rows[0]["tot_rcv"].ToString()).ToString(amtFormat);
                lbCashAmtRes.Text = double.Parse(sumTransHeader.Rows[0]["cash_amt"].ToString()).ToString(amtFormat);

                sumTransDetail = process.SumTRansDetail(repNo);
                itemAdd();
            }
            
        }

        public void itemAdd()
        {
            if (sumTransDetail != null)
            {
                if (sumTransDetail.Rows.Count > 1)
                {
                    for (int i = 0; i < (sumTransDetail.Rows.Count); i++)
                    {
                        if (double.Parse(sumTransDetail.Rows[i]["seq"].ToString()) < sumTransDetail.Rows.Count)
                        {
                            UCItemDaySales ucitmDaySales = new UCItemDaySales();
                            ucitmDaySales.lbNo.Text = sumTransDetail.Rows[i]["seq"].ToString();
                            ucitmDaySales.lbTill.Text = sumTransDetail.Rows[i]["Till"].ToString();
                            ucitmDaySales.lbSaleReceiptCnt.Text = double.Parse(sumTransDetail.Rows[i]["SaleReceiptCnt"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbSaleAmt.Text = double.Parse(sumTransDetail.Rows[i]["SaleAmt"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbDiscount.Text = double.Parse(sumTransDetail.Rows[i]["Discount"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbReturnAmt.Text = double.Parse(sumTransDetail.Rows[i]["ReturnAmt"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbNetSale.Text = double.Parse(sumTransDetail.Rows[i]["NetSale"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbCashInDrawer.Text = double.Parse(sumTransDetail.Rows[i]["CashInDrawer"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbStatus.Text = sumTransDetail.Rows[i]["Status"].ToString();
                            ucitmDaySales.lbCashier.Text = sumTransDetail.Rows[i]["Cashier"].ToString();
                            pn_Item.Controls.Add(ucitmDaySales);
                        }
                        else
                        {
                            UCItemDaySales ucitmDaySales = new UCItemDaySales();
                            ucitmDaySales.lbNo.Text = sumTransDetail.Rows[i]["seq"].ToString();
                            ucitmDaySales.lbNo.Visible = false;
                            ucitmDaySales.lbTill.Text = lbTotalGV.Text;
                            ucitmDaySales.lbSaleReceiptCnt.Text = double.Parse(sumTransDetail.Rows[i]["SaleReceiptCnt"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbSaleAmt.Text = double.Parse(sumTransDetail.Rows[i]["SaleAmt"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbDiscount.Text = double.Parse(sumTransDetail.Rows[i]["Discount"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbReturnAmt.Text = double.Parse(sumTransDetail.Rows[i]["ReturnAmt"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbNetSale.Text = double.Parse(sumTransDetail.Rows[i]["NetSale"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbCashInDrawer.Text = double.Parse(sumTransDetail.Rows[i]["CashInDrawer"].ToString()).ToString(amtFormat);
                            ucitmDaySales.lbStatus.Text = sumTransDetail.Rows[i]["Status"].ToString();
                            ucitmDaySales.lbCashier.Text = sumTransDetail.Rows[i]["Cashier"].ToString();
                            pn_Item.Controls.Add(ucitmDaySales);
                        }
                    }
                    RefreshGrid();
                }
            }
        }

        private void RefreshGrid()
        {
            List<UCItemDaySales> lstItemReport = new List<UCItemDaySales>();
            lstItemReport = pn_Item.Controls.Cast<UCItemDaySales>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_Item.Controls.Clear();
            int num = lstItemReport.Count;
            //double a = 0;

            foreach (UCItemDaySales item in lstItemReport)
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

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
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
            try
            {
                if (chkOtherTill.profile == ProfileStatus.Authorize)
                {
                    frmLoading.showLoading();
                    otherTillList = process.getTillNo4DispReport(FunctionID.Report_CheckCurrentDaySale_ReportOtherTillList);
                    frmLoading.closeLoading();
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
                        //frmUserAuthorize auth = new frmUserAuthorize("ReportDaySale", chkOtherTill.diffUserStatus);
                        //auth.function = FunctionID.Report_CheckCurrentDaySale_ReportOtherTill;
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    if (sender is UCDropDownList)
                        //    {
                        //        var ucDDL = (UCDropDownList)sender;
                        //        ucDDL.lstDDL = SetDataucDropDownListBlank();
                        //    }
                        //    return;
                        //}
                        //else
                        //{
                        //    getAutherize = true;
                        //}

                        if (!Utility.CheckAuthPass(this, chkOtherTill, "ReportDaySale"))
                        {
                            if (sender is UCDropDownList)
                            {
                                var ucDDL = (UCDropDownList)sender;
                                ucDDL.lstDDL = SetDataucDropDownListBlank();
                            }
                            return;
                        }
                        else
                        {
                            getAutherize = true;
                        }
                    }

                    otherTillList = process.getTillNo4DispReport(FunctionID.Report_CheckCurrentDaySale_ReportOtherTillList);
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

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownListBlank()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = "Till " + ProgramConfig.tillNo, ValueText = ProgramConfig.tillNo });
            return lstStr;
        }

        public void defaultSetting ()
        {
            pn_Item.Controls.Clear();
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            closeForm();
        }
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            defaultSetting();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                Profile check = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDaySale_PrintReportDocument);
                if (check.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("ReportDaySale", check.diffUserStatus);
                    //auth.function = FunctionID.Report_CheckCurrentDaySale_PrintReportDocument;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    frmLoading.closeLoading();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, check, "ReportDaySale"))
                    {
                        frmLoading.closeLoading();
                        return;
                    }
                    frmLoading.closeLoading();
                    printReport();

                }
                else if (check.profile == ProfileStatus.Authorize)
                {
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

        public void printReport()
        {
            frmLoading.showLoading();
            StoreResult printReceipt = process.printSumTransReport(repNo);
            if (!printReceipt.response.next)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(printReceipt);
                dialog.ShowDialog(this);
                return;
            }
            DataTable dt = printReceipt.otherData;

            //for (int i = 0; i < 2; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["seq"] = "9999";
            //    dt.Rows.Add(dr);
            //}
            
            Hardware.printTermal(dt);
            closeForm();
            frmLoading.closeLoading();        
        }

        public void closeForm()
        {
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

        private void ucSearchTillType_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            pn_Item.Controls.Clear();
            daySalesReportProcess();
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();

            lbDateRes.Text = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo(ProgramConfig.language.culture));

            pn_Item.Controls.Clear();
            itemAdd();
            frmLoading.closeLoading();
        }

    }
}
