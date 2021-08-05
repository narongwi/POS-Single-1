using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Process;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmDeposit : Form
    {
        private UCItemSell lastUCIS = new UCItemSell();

        public frmMonitorCustomer frmMoCus = null;
        public frmMonitorCustomerFooter frmMoFooter = null;
        public frmMonitor2Detail frm2Detail = null;
        string displayAmt = ProgramConfig.amountFormatString;
        double amtPrice = 0;
        public string name;
        public string price;
        ProductAndServiceProcess process = new ProductAndServiceProcess();
        SaleProcess saleProcess = new SaleProcess();
        FunctionID functionSearch;


        public frmDeposit()
        {
            InitializeComponent();
        }

        private void frmDeposit_Shown(object sender, EventArgs e)
        {
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.Deposit;
            if (frmMoCus == null || frmMoFooter == null || frm2Detail == null)
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmMonitorCustomerFooter)
                    {
                        frmMoFooter = (frmMonitorCustomerFooter)item;
                    }
                    else if (item is frmMonitorCustomer)
                    {
                        frmMoCus = (frmMonitorCustomer)item;
                    }
                    else if (item is frmMonitor2Detail)
                    {
                        frm2Detail = (frmMonitor2Detail)item;
                        frm2Detail.panel_message.BringToFront();
                    }

                    if (frmMoCus != null && frmMoFooter != null && frm2Detail != null)
                    {
                        break;
                    }
                }
            }

            if (ProgramConfig.salePageState == 0)
            {
                FromLoad();
            }

        }

        private void FromLoad()
        {
            btnConfirm.Visible = true;
            btnConfirm.BringToFront();
            btnPayment.Visible = false;
            var result = process.getRunning(FunctionID.Deposit_GetRunning, RunningReceiptID.SaleRef);
            if (result.response.next)
            {
                ProgramConfig.saleRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.saleRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                lbTxtRefNo.Text = ProgramConfig.saleRefNo;
            }
            else
            {
                Utility.AlertMessage(result.response, result.responseMessage);
                this.Dispose();
                return;
            }

            ProcessResult res = Utility.ProcessCashierMessage(ucHeader1, FunctionID.Deposit_GetMessageCashier,
                                           (function) => process.cashireMessageStatus(function));
            if (!res.needNextProcess)
            {
                Utility.AlertMessage(res.response, res.responseMessage);
                this.Dispose();
                return;
            }

            result = process.GetDepositCustomerType();
            if (!result.response.next)
            {
                Utility.AlertMessage(result.response, result.responseMessage);
                this.Dispose();
                return;
            }



            int cnt = 1;
            frmPopUpSelectList frmLst = new frmPopUpSelectList();
            FunctionID fn = FunctionID.NoFunctionID;
            foreach (DataRow dr in result.otherData.Rows)
            {
                UCItemDepositCustomerType ucDC = new UCItemDepositCustomerType();
                ucDC.UCItemDepositCustomerTypeClick += (s, e) => UCItemDepositCustomerTypeClick(s, e, frmLst.btnOK);
                ucDC.lbName.Text = dr["ACTION3_DESC"].ToString();
                ucDC.Seq = dr["Column1"].ToString();
                ucDC.FunctionID = dr["Function_ID"].ToString();
                ucDC.Name = ucDC.placeHolderName + cnt;
                frmLst.lstUC.Add(ucDC);
                cnt++;

                fn = new FunctionID(dr["Function_ID"].ToString());
            }

            if (frmLst.lstUC.Count > 1)
            {
                frmLst.ShowDialog(this);
            }
            else
            {
                functionSearch = fn;
            }

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

            ucHeader1.showMember_ButtonBack = false;
            ucHeader1.btnMember_Click(null, null);
        }

        public void UCItemDepositCustomerTypeClick(object sender, EventArgs e, Button btn)
        {
            var ucDC = (UCItemDepositCustomerType)sender;
            functionSearch = new FunctionID(ucDC.FunctionID);
            btn.Enabled = true;
            btn.BackColor = Color.White;
        }

        private void ucHeader1_MemberIconClick(object sender, EventArgs e)
        {
            var ucMem = (UCMember)sender;
            ucMem.functionID = functionSearch;
        }

        private void ucHeader1_MemberEnterFromButton(object sender, EventArgs e)
        {
            ucTextBoxWithIcon1.FocusTxt();
        }

        public bool CheckPolicyShowMemberDetail()
        {
            //#755
            Profile check = ProgramConfig.getProfile(FunctionID.Deposit_ShowDetailCustomer);
            return check.policy == PolicyStatus.Work;
        }

        public StoreResult InsertTempCustomerFullTax(StoreResult res)
        {
            return new StoreResult(ResponseCode.Success, "");
            //process.In
        }

        private void ucHeader1_MemberClick(object sender, EventArgs e)
        {
            var ucMem = (UCMember)sender;
            ucMem.CheckPolicyShowDetail -= CheckPolicyShowMemberDetail;
            ucMem.CheckPolicyShowDetail += CheckPolicyShowMemberDetail;
            ucMem.InsertTempCustomerFullTax -= InsertTempCustomerFullTax;
            ucMem.InsertTempCustomerFullTax += InsertTempCustomerFullTax;
            ucMem.eventName = "Deposit";
            ucMem.functionID = functionSearch;
        }

        public void keyProduct(bool IsNoScanBarcode, string barcode)
        {
            try
            {

                AppLog.writeLog("start keyProduct()");
                ucTextBoxWithIcon1.EnabledUC = false;
                if (barcode.Length < 13 && barcode.Length != 0)
                {
                    barcode = barcode.PadLeft(13, '0');
                }

                string qty = "1";

                int discountType = 0;
                double discountValue = 0f;
                //showLoadingProcess("scanSaleProduct", new object[] { ucTBScanBarcode.Text, double.Parse(qty), discountType, discountValue, ucTxtCouponNo.Text });
                frmLoading.showLoading();
                ProcessResult result = saleProcess.scanSaleProduct(barcode, double.Parse(qty), IsNoScanBarcode, discountType, discountValue, "", priceInp: ucTextBoxWithIcon1.InpTxt,
                                                                CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h),
                                                                AlertMessage: (resCode, resMsg, resHelpMsg) => Utility.AlertMessage(this, resCode, resMsg, resHelpMsg));

                frmLoading.closeLoading();
                if (result.response.next)
                {
                    if (result.data != null)
                    {
                        DataRow[] data = (DataRow[])result.data;
                        foreach (DataRow row in data)
                        {
                            UCMonitor2Item ucitm = new UCMonitor2Item((int)row["REC"]);
                            ucitm.lbNo.Text = row["REC"].ToString();

                            if (row["ProductName"].ToString().Length > 15)
                            {
                                ucitm.lb_ITEM.Text = row["ProductName"].ToString().Substring(0, 15);
                            }
                            else
                            {
                                ucitm.lb_ITEM.Text = row["ProductName"].ToString();
                            }

                            ucitm.lb_AMT.Text = (double.Parse(row["DisplayAmt"].ToString())).ToString(displayAmt);
                            ucitm.lb_QTY.Text = row["QNT"].ToString();

                            //AppMessage.fillControlsFont(ProgramConfig.language, ucitm, GetListIgnoreFont_frmMoCus_pn_Item());

                            frmMoCus.pn_Item.Controls.Add(ucitm);
                            frmMoCus.pn_Item.Controls.SetChildIndex(ucitm, 0);
                            frmMoCus.pn_Item.Refresh();

                            UCItemSell ucitmSell = new UCItemSell((int)row["REC"]);
                            ucitmSell.UCGridViewItemSellClick += UCGridViewItemSellClick;
                            ucitmSell.lbNo.Text = row["REC"].ToString();
                            ucitmSell.lbRecDB.Text = row["REC"].ToString();
                            ucitmSell.lbDiscID.Text = row["DISCID"].ToString();
                            ucitmSell.IsFreshFoodNRTC = row["IsFFNRTC"].ToString();
                            ucitmSell.PR_TYPE = row["PRODUCT_TYPE"].ToString();
                            ucitmSell.UPCPriceDB = row["UPC"].ToString();
                            ucitmSell.lbUC_ProductCode.Text = row["PCD"].ToString();
                            ucitmSell.STV = row["STV"].ToString();

                            string symbol = "";
                            if (row["PCD"].ToString().Length == 20)
                            {
                                string chr = row["PCD"].ToString().Substring(19, 1);
                                switch (chr)
                                {
                                    case "A":
                                        symbol = "+";
                                        break;
                                    case "K":
                                        symbol = "-";
                                        break;
                                    case "L":
                                        symbol = "/";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            ucitmSell.lbSymbolText = symbol;

                            ucitmSell.lbUC_Qty.Text = (double.Parse(row["QNT"].ToString())).ToString();
                            ucitmSell.lbUC_Price.Text = (double.Parse(row["UPC"].ToString())).ToString(displayAmt);
                            ucitmSell.lbUC_Discount.Text = (double.Parse(row["DISCAMT"].ToString())).ToString(displayAmt);
                            ucitmSell.lbUC_TotalPrice.Text = (double.Parse(row["totalPrice"].ToString())).ToString(displayAmt);
                            price = (double.Parse(row["totalPrice"].ToString())).ToString(displayAmt);
                            ucitmSell.lbProductName.Text = row["ProductName"].ToString();
                            name = row["ProductName"].ToString();
                            ucitmSell.lbPromo.Text = row["PromotionName"].ToString();
                            ucitmSell.lbPromoPrice.Text = row["PromotionPrice"].ToString();

                            //AppMessage.fillControlsFont(ProgramConfig.language, ucitmSell, GetListIgnoreFont_pn_item_sell());

                            pn_item_sell.Controls.Add(ucitmSell);
                            pn_item_sell.Controls.SetChildIndex(ucitmSell, 0);
                            pn_item_sell.Refresh();

                            amtPrice += (double.Parse(row["totalPrice"].ToString()));
                        }

                        lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                        lbTxtTotal.Text = amtPrice.ToString(displayAmt);

                        frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString(displayAmt);
                        frmMoCus.lbTxtDiscount.Text = "0";
                        frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString(displayAmt);

                        if (frm2Detail.panel_list.InvokeRequired)
                        {
                            frm2Detail.panel_list.BeginInvoke((MethodInvoker)delegate
                            {
                                frm2Detail.panel_list.BringToFront();
                                frm2Detail.panel_product.BringToFront();
                            });
                        }
                        else
                        {
                            frm2Detail.panel_list.BringToFront();
                            frm2Detail.panel_product.BringToFront();
                        }

                        if (frm2Detail.label1.InvokeRequired)
                        {
                            frm2Detail.label1.BeginInvoke((MethodInvoker)delegate
                            {
                                frm2Detail.label1.Text = name;
                                frm2Detail.label1.BackColor = Color.FromArgb(143, 255, 182);
                            });
                        }
                        else
                        {
                            frm2Detail.label1.Text = name;
                            frm2Detail.label1.BackColor = Color.FromArgb(143, 255, 182);
                        }

                        if (frm2Detail.label2.InvokeRequired)
                        {
                            frm2Detail.label2.BeginInvoke((MethodInvoker)delegate
                            {
                                frm2Detail.label2.Text = price;
                                frm2Detail.label2.BackColor = Color.FromArgb(143, 255, 182);
                            });
                        }
                        else
                        {
                            frm2Detail.label2.Text = price;
                            frm2Detail.label2.BackColor = Color.FromArgb(143, 255, 182);
                        }
                    }
                    RefreshGrid2();
                }
                else
                {
                    if (result.response != ResponseCode.Ignore)
                    {
                        Utility.AlertMessage(result.response, result.responseMessage);
                    }
                }

                int cnt = 1;

                ProgramConfig.flagDiscount = false;
                DisplayExchangeRate();

                CheckItemSell();
                //if (ucTextBoxWithIcon1.InvokeRequired)
                //{
                //    ucTextBoxWithIcon1.BeginInvoke((MethodInvoker)delegate
                //    {
                //        ucTextBoxWithIcon1.Text = "";
                //        ucTextBoxWithIcon1.Focus();
                //        ucTextBoxWithIcon1.EnabledUC = true;
                //    });
                //}
                //else
                //{
                //    ucTextBoxWithIcon1.Text = "";
                //    ucTextBoxWithIcon1.Focus();
                //    ucTextBoxWithIcon1.EnabledUC = true;
                //}
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
            }
            catch (Exception ex)
            {
                AppLog.writeLog("cast [Method] keyProduct :" + ex.Message);
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        public void UCGridViewItemSellClick(object sender, EventArgs e)
        {
            btnEdit.Visible = true;
            ucTextBoxWithIcon1.EnabledUC = true;
            UCItemSell ucGV = (UCItemSell)sender;
            if (lastUCIS != ucGV)
                UCItemSell.LostFocusItem(lastUCIS);

            lastUCIS = ucGV;


            ucTextBoxWithIcon1.InpTxt = ucGV.lbTotalPriceText;
            ucTextBoxWithIcon1.FocusTxt();
            ucTextBoxWithIcon1.SetSelection = true;
        }

        private void CheckItemSell()
        {
            if (pn_item_sell.Controls.Count > 0)
            {
                ucTextBoxWithIcon1.Text = "";
                ucTextBoxWithIcon1.EnabledUC = false;
                ucKeypad.ucTBWI = null;
                btnPayment.Visible = true;
                btnPayment.BringToFront();
                btnPayment.Focus();
            }
            else if (pn_item_sell.Controls.Count == 0)
            {
                btnConfirm.Visible = true;
                btnConfirm.BringToFront();
                ucTextBoxWithIcon1.Text = "";
                ucTextBoxWithIcon1.Focus();
                ucTextBoxWithIcon1.EnabledUC = true;
            }
        }
        //private void setVisibleButtonConfirm(bool val)
        //{
        //    if (val)
        //    {
        //        if (!btnConfirm.Enabled)
        //        {
        //            btnConfirm.Enabled = true;
        //            btnConfirm.BackgroundImage = Properties.Resources.Sale_btnConfirm;
        //        }
        //    }
        //    else
        //    {
        //        if (btnConfirm.Enabled)
        //        {
        //            btnConfirm.Enabled = false;
        //            btnConfirm.BackgroundImage = Properties.Resources.payment_disable;
        //        }

        //    }
        //}


        private void RefreshGrid2()
        {
            List<UCItemSell> lstItemSell = new List<UCItemSell>();
            lstItemSell = pn_item_sell.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            int maxnum = lstItemSell.Count;
            int num = maxnum;
            double a = 0;

            foreach (UCItemSell item in lstItemSell)
            {
                if (maxnum == num)
                {
                    item.BackColor = Color.FromArgb(255, 255, 172);
                }
                else if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                a += double.Parse(item.lbQtyText);
                num--;
            }
            Utility.ScrollToBottom(pn_item_sell);
            pn_item_sell.Refresh();
            ProgramConfig.qntValue = a.ToString();

            List<UCMonitor2Item> lstMonitor2ItemSell = new List<UCMonitor2Item>();
            lstMonitor2ItemSell = frmMoCus.pn_Item.Controls.Cast<UCMonitor2Item>().ToList();
            //lstMonitor2ItemSell = frmMoCus.pn_Item.Controls.Cast<UCMonitor2Item>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            //frmMoCus.pn_Item.Controls.Clear();
            int number = lstMonitor2ItemSell.Count;

            foreach (UCMonitor2Item monitor2Item in lstMonitor2ItemSell)
            {
                if (number % 2 != 0)
                {
                    monitor2Item.BackColor = Color.FromArgb(143, 255, 182);
                }
                else
                {
                    monitor2Item.BackColor = Color.White;
                }
                number--;
            }
            Utility.ScrollToBottom(frmMoCus.pn_Item);
            frmMoCus.pn_Item.Refresh();
        }

        private void DisplayExchangeRate()
        {
            StoreResult res = saleProcess.getAmountExchangeRate(lbTxtTotal.Text, "1", ProgramConfig.payment.getMainCurrency(), ProgramConfig.payment.getPaymentCode(ProgramConfig.payment.getMainCurrency()));
            frmLoading.closeLoading();
            if (res.response.next)
            {
                DataTable loadExchange = res.otherData;
                for (int i = 0; i < loadExchange.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        frmMoCus.lbCurrencyRate1.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString();
                        frmMoCus.lbAmtCurrency1.Text = loadExchange.Rows[i]["Total"].ToString();
                    }

                    if (i == 1)
                    {
                        frmMoCus.lbCurrencyRate2.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString();
                        frmMoCus.lbAmtCurrency2.Text = loadExchange.Rows[i]["Total"].ToString();
                    }
                }
            }
            else
            {
                Utility.AlertMessage(res);
            }
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            UCHeader uchead = (UCHeader)sender;
            uchead.pageTest = PageBackFormPayment.Deposit;
            //this.Dispose();
        }

        private void ucTextBoxWithIcon1_EnterFromButton(object sender, EventArgs e)
        {
            if (!btnEdit.Visible)
            {
                keyProduct(false, ProgramConfig.getPosConfig("DepositProductCode").ToString());
            }
            else
            {
                btnEdit_Click(null, null);
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ShowPayment();
        }

        private void ShowPayment()
        {
            ProgramConfig.disValue = lbTxtdiscount1.Text;
            ProgramConfig.amtValue = lbTxtSubtotal.Text;
            ProgramConfig.totalValue = lbTxtTotal.Text;

            //saleProcess.SaveMember();

            frm2Detail.label1.Text = "";
            frm2Detail.label2.Text = "";
            frm2Detail.label1.BackColor = Color.White;
            frm2Detail.label2.BackColor = Color.White;
            frm2Detail.panel_list.Controls.Clear();
            frm2Detail.panel_payment.BringToFront();
            frm2Detail.lbTxtTotalCash.Text = lbTxtTotal.Text;

            ProgramConfig.paymentOpenCashDrawer = FunctionID.Deposit_OpenDrawerAndRecordTime;
            ProgramConfig.paymentCloseCashDrawer = FunctionID.Deposit_CloseDrawerAndRecordTime;
            ProgramConfig.paymentFunction = FunctionID.Deposit_Payment;
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.Deposit;
            Program.control.CloseForm("frmPayment");
            Program.control.ShowForm("frmPayment");
        }

        public void frmDeposit_Activated(object sender, EventArgs e)
        {
            if (ProgramConfig.salePageState == 2)
            {
                ProgramConfig.salePageState = 3;
                ClearValue();
                FromLoad();
                GC.Collect();
                ProgramConfig.salePageState = 1;
            }

            if (ucKeypad.ucTBWI != null)
            {
                ucKeypad.ucTBWI.FocusTxt();
            }

        }

        private void ClearValue()
        {
            amtPrice = 0;
            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
            lbTxtdiscount1.Text = amtPrice.ToString(displayAmt);
            lbTxtTotal.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtDiscount.Text = "0";
            frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString(displayAmt);
            pn_item_sell.Controls.Clear();
            frmMoCus.pn_Item.Controls.Clear();
            //setVisibleButtonConfirm(false);
            ClearMember();
            ucTextBoxWithIcon1.InpTxt = "";
            ucTextBoxWithIcon1.EnabledUC = true;
            btnEdit.Visible = false;
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            double inp = 0.0;
            double.TryParse(ucTextBoxWithIcon1.InpTxt, out inp);
            if (!(ucTextBoxWithIcon1.InpTxt != "" || inp > 0))
            {
                Utility.AlertMessage(ResponseCode.Error, "กรุณาระบุจำนวนเงิน");
                return;
            }

            btnEdit.Visible = false;

            string amt = Convert.ToDouble(ucTextBoxWithIcon1.InpTxt).ToString(displayAmt);
            var res = process.Deposit_EditPrice(lastUCIS.lbProductCodeText, lastUCIS.lbQtyText, amt, lastUCIS.lbRecDB.Text);
            if (res.response.next)
            {
                loadTempDLYPTRANS();
            }
            else
            {
                Utility.AlertMessage(res);
            }
        }

        public void loadTempDLYPTRANS()
        {
            frmLoading.showLoading();
            amtPrice = 0;
            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
            lbTxtdiscount1.Text = amtPrice.ToString(displayAmt);
            lbTxtTotal.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtDiscount.Text = "0";
            frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString(displayAmt);
            pn_item_sell.Controls.Clear();
            frmMoCus.pn_Item.Controls.Clear();

            var data = saleProcess.getTempSaleItem();

            int cnt = 0;
            foreach (DataRow row in data)
            {
                UCMonitor2Item ucitm = new UCMonitor2Item(cnt);
                ucitm.lbNo.Text = row["DisplayRec"].ToString();

                if (row["ProductName"].ToString().Length > 15)
                {
                    ucitm.lb_ITEM.Text = row["ProductName"].ToString().Substring(0, 15);
                }
                else
                {
                    ucitm.lb_ITEM.Text = row["ProductName"].ToString();
                }

                ucitm.lb_AMT.Text = (double.Parse(row["DisplayAmt"].ToString())).ToString(displayAmt);
                ucitm.lb_QTY.Text = (double.Parse(row["QNT"].ToString())).ToString();
                frmMoCus.pn_Item.Controls.Add(ucitm);
                frmMoCus.pn_Item.Controls.SetChildIndex(ucitm, 0);
                frmMoCus.pn_Item.Refresh();

                UCItemSell ucitmSell = new UCItemSell();
                ucitmSell.UCGridViewItemSellClick += UCGridViewItemSellClick;
                ucitmSell.lbNo.Text = row["DisplayRec"].ToString();
                ucitmSell.lbRecDB.Text = row["REC"].ToString();
                ucitmSell.lbDiscID.Text = row["DISCID"].ToString();
                ucitmSell.IsFreshFoodNRTC = row["IsFFNRTC"].ToString();
                ucitmSell.PR_TYPE = row["PRODUCT_TYPE"].ToString();
                ucitmSell.UPCPriceDB = row["UPC"].ToString();
                ucitmSell.STV = row["STV"].ToString();

                string symbol = "";
                if (row["PCD"].ToString().Length == 20)
                {
                    string chr = row["PCD"].ToString().Substring(19, 1);
                    switch (chr)
                    {
                        case "A":
                            symbol = "+";
                            break;
                        case "K":
                            symbol = "-";
                            break;
                        case "L":
                            symbol = "/";
                            break;
                        default:
                            break;
                    }
                }

                ucitmSell.lbUC_ProductCode.Text = row["PCD"].ToString();
                ucitmSell.lbSymbolText = symbol;
                ucitmSell.lbUC_Qty.Text = (double.Parse(row["QNT"].ToString())).ToString();
                ucitmSell.lbUC_Price.Text = (double.Parse(row["DisplayPrice"].ToString())).ToString(displayAmt);
                ucitmSell.lbUC_Discount.Text = (double.Parse(row["DISCAMT"].ToString())).ToString(displayAmt);
                ucitmSell.lbUC_TotalPrice.Text = (double.Parse(row["TotalPrice"].ToString())).ToString(displayAmt);
                ucitmSell.lbProductName.Text = row["ProductName"].ToString();
                ucitmSell.lbPromo.Text = row["PromotionName"].ToString();
                ucitmSell.lbPromoPrice.Text = (double.Parse(row["PromotionPrice"].ToString())).ToString(displayAmt);

                if (row["PrintExport"].ToString() == "Y")
                {
                    ucitmSell.iconPrintExport.Visible = true;
                }
                else
                {
                    ucitmSell.iconPrintExport.Visible = false;
                }

                pn_item_sell.Controls.Add(ucitmSell);
                pn_item_sell.Controls.SetChildIndex(ucitmSell, 0);
                amtPrice += double.Parse(row["TotalPrice"].ToString());
                cnt++;
            }
            pn_item_sell.Refresh();
            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
            lbTxtTotal.Text = amtPrice.ToString(displayAmt);

            frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtDiscount.Text = "0";
            frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString(displayAmt);

            frmLoading.closeLoading();
            RefreshGrid2();
            CheckItemSell();
        }

        private void ucHeader1_HambergerItemClick(object sender, EventArgs e)
        {
            var hamItem = (UCHamburgerItem)sender;
            if (hamItem.MenuID == MenuIdHamberger.CancelReceipt)
            {
                if (pn_item_sell.Controls.Count == 0)
                {
                    frm2Detail.panel_message.BringToFront();
                    Program.control.ShowForm("frmMainMenu");
                    Program.control.CloseForm("frmDeposit");
                }
                else
                {
                    string valueReturn = "";
                    Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder);
                    if (check.profile == ProfileStatus.NotAuthorize)
                    {
                        AuthResult authRes = Utility.CheckAuthPassRes(this, check, "CancelSale");
                        if (!authRes.Next)
                        {
                            //picBtBack_Click(this, null);
                            return;
                        }
                        valueReturn = authRes.maxCancelReceiptAmt;
                    }

                    check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_CheckLimit);
                    if (check.policy == PolicyStatus.Work) //2
                    {
                        //string valueReturn = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_CancelOrder_Limit.parameterCode);
                        if (valueReturn == "" || valueReturn == null)
                        {
                            valueReturn = "0";
                        }
                        if (double.Parse(lbTxtTotal.Text) > double.Parse(valueReturn))
                        {
                            //ไป Step4
                            check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_OverLimit);
                            if (check.policy == PolicyStatus.Skip) //1
                            {
                                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowCancelSale").message;
                                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowCancelSale").help;
                                Utility.AlertMessage(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(valueReturn).ToString(displayAmt)));

                                //notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(valueReturn).ToString(displayAmt)));
                                //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(valueReturn).ToString(displayAmt));

                                return;
                            }
                            else if (check.policy == PolicyStatus.Work) //2
                            {
                                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowCancelSale").message;
                                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowCancelSale").help;
                                frmNotify dialog_check = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(valueReturn).ToString(displayAmt)));
                                //frmNotify dialog_check = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(valueReturn).ToString(displayAmt));
                                dialog_check.ShowDialog(this);

                                AuthResult authRes = Utility.CheckAuthPassRes(this, check, "CancelSale");
                                if (!authRes.Next)
                                {
                                    //picBtBack_Click(this, null);
                                    return;
                                }
                                string checkAgain = authRes.maxCancelReceiptAmt;

                                //string checkAgain = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxCancelReceiptAmt"].ToString();

                                //เช็ควงเงิน User Authorize 
                                if (double.Parse(checkAgain) >= double.Parse(lbTxtTotal.Text))
                                {
                                    //Step 5
                                    reasonToCancel();
                                }
                                else
                                {
                                    string message = ProgramConfig.message.get("frmSale", "NotAllowCancelSale").message;
                                    string help = ProgramConfig.message.get("frmSale", "NotAllowCancelSale").help;
                                    //notify = new frmNotify(ResponseCode.Error, message, string.Format(help, double.Parse(checkAgain).ToString(displayAmt)));

                                    Utility.AlertMessage(ResponseCode.Error, message, string.Format(help, double.Parse(valueReturn).ToString(displayAmt)));

                                    //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(checkAgain).ToString(displayAmt));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            reasonToCancel();
                        }
                    }
                    else if (check.policy == PolicyStatus.Skip) //1
                    {
                        //ข้ามไป Step 5
                        reasonToCancel();
                    }
                }
            }
        }

        public void reasonToCancel()
        {
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_CheckLimit);
            if (check.policy == PolicyStatus.Skip) //1
            {
                StoreResult resSaveCancelTran = saleProcess.saveCancelSaleTransaction(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction, "0");
                Profile checkPro = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank);

                if (resSaveCancelTran.response.next)
                {
                    printCancel(FunctionID.Sale_CancelWhileSale_CancelOrder_PrintCancelDocument, true);
                    if (checkPro.policy == PolicyStatus.Work) //2
                    {
                        saleProcess.syncToDataTank("Cancel", FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.saleRefNo, "1");
                    }
                }
                else
                {
                    Utility.AlertMessage(ResponseCode.Error, resSaveCancelTran.responseMessage, resSaveCancelTran.helpMessage);                 
                    return;
                }
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                frmDeleteReason dialog = new frmDeleteReason();
                DialogResult auth_res = dialog.ShowDialog(this);
                if (auth_res != DialogResult.Yes)
                {
                    //picBtBack_Click(this, null);
                    return;
                }
                ucTextBoxWithIcon1.FocusTxt();
            }
        }

        private void printCancel(FunctionID function, bool IsExit)
        {
            Profile check = ProgramConfig.getProfile(function);
            if (check.policy == PolicyStatus.Skip) //1
            {
                if (IsExit)
                {
                    this.Dispose();
                }
                else
                {
                    pn_item_sell.Controls.Clear();
                    ProgramConfig.salePageState = 2;
                    frmDeposit_Activated(null, null);
                }
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                StoreResult result = saleProcess.printCancel(function);
                if (!result.response.next)
                {
                    Utility.AlertMessage(result);
                    return;
                }
                else if (result.response == ResponseCode.Information)
                {
                    Utility.AlertMessage(result);
                }

                DataTable dt = result.otherData;
                Hardware.printTermal(dt);

                if (IsExit)
                {
                    this.Dispose();
                }
                else
                {
                    pn_item_sell.Controls.Clear();
                    ProgramConfig.salePageState = 2;
                    frmDeposit_Activated(null, null);
                }
            }
        }

    }
}
