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
    public partial class frmVoid : Form
    {
        public frmMainMenu frmMainMenu;
        
        double amtPrice = 0;
        double disPrice = 0;
        double changeValue = 0;
        double totalTax = 0;
        double totalPrice = 0;
        string openTime = "";
        string closeTime = "";
        private VoidProcess process = new VoidProcess();
        string receiptNo = "";
        string vat = "";
        string value1 = "";
        string value2 = "";
        double upPrice = 0;
        double downPrice = 0;
        public int cnt = 1;
        public string _reasonId = "";
        public string _reasonTxt = "";
        public string upc = "";
        public bool chk;
        string _saleTime;
        string _saleType;
     
        string amtFormat = ProgramConfig.amountFormatString;
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");


        public frmVoid()
        {
            InitializeComponent();
        }

        private void frmVoid_Load(object sender, EventArgs e)
        {
            try
            {
                //Class1.InitialTextBoxIcon(ucTBScanBarcode, BJCBCPOS.Properties.Resources.icon_textbox_scan, Class1.UCTextBoxIconType.ScanAndDelete, Class1.IconType.Scan);
                StoreResult result = null;
                ucHeader1.alertFunctionID = FunctionID.Void_GetMessageCashier;

                Utility.GlobalClear();

                Profile check = ProgramConfig.getProfile(FunctionID.Void_SelectVoidMenu);
                if (check.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("Void", check.diffUserStatus);
                    //auth.function = FunctionID.Void_SelectVoidMenu;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    this.Dispose();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, check, "Void"))
                    {
                        this.Dispose();
                        return;
                    }
                }

                result = process.getRunning(FunctionID.Return_GetRunningNo, RunningReceiptID.VoidRef);
                string refNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.voidRefNo = refNo;
                ProgramConfig.voidRefNoIni = refNo;
                lbTxtRefNo.Text = refNo;

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

                Profile chkMCashier = ProgramConfig.getProfile(FunctionID.Void_GetMessageCashier);
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

                defaultSetting();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    Utility.CheckRunningNumber();
                    frmVoid_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void defaultValue()
        {
            amtPrice = 0;
            disPrice = 0;
            changeValue = 0;
            totalTax = 0;
            totalPrice = 0;
            upPrice = 0;
            downPrice = 0;
        
        }

        public void keyReceipt()
        {
            try
            {
                //if (ucTBScanBarcode.Text.Length < 13 && ucTBScanBarcode.Text.Length != 0)
                //{
                //    ucTBScanBarcode.Text = ucTBScanBarcode.Text.PadLeft(13, '0');
                //}
                string memberName = "-";
                defaultValue();
                pn_Item.Controls.Clear();
                pnTypePDetail.Controls.Clear();

                receiptNo = ucTBScanBarcode.Text;
                StoreResult checkReceipt = process.checkReceipt(receiptNo);

                if (checkReceipt.response.next)
                {
                    if (checkReceipt.otherData != null)
                    {
                        _saleType = checkReceipt.otherData.Rows[0]["SaleType"].ToString();
                        StoreResult displayReceipt = process.displayReceiptDetail(FunctionID.Void_InputReceiptNo, ucTBScanBarcode.Text, _saleType);
                        if (!displayReceipt.response.next)
                        {
                            frmNotify dialog = new frmNotify(displayReceipt.response, displayReceipt.responseMessage, displayReceipt.helpMessage);
                            dialog.ShowDialog(this);
                            return;
                        }
                        else
                        {
                            if (displayReceipt.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(displayReceipt);
                                dialog.ShowDialog(this);
                            }

                            //UCItemSell ucitmVoid = new UCItemSell();
                            for (int i = 0; i < displayReceipt.otherData.Rows.Count; )
                            {
                                string codeOrName = displayReceipt.otherData.Rows[i]["ProductDesc"].ToString();
                                string qty = displayReceipt.otherData.Rows[i]["QuantSale"].ToString();
                                string price = displayReceipt.otherData.Rows[i]["Price"].ToString();
                                string dis = displayReceipt.otherData.Rows[i]["Discount"].ToString();
                                string amt = displayReceipt.otherData.Rows[i]["Amount"].ToString();
                                string line = displayReceipt.otherData.Rows[i]["LineNo"].ToString();
                                int rec = Convert.ToInt32(displayReceipt.otherData.Rows[i]["rec"].ToString());
                                string dataType = displayReceipt.otherData.Rows[i]["DataType"].ToString();
                                string vatType = displayReceipt.otherData.Rows[i]["Vat_Status"].ToString();
                                //string currency = displayReceipt.otherData.Rows[i]["Currency"].ToString();
                                string fxcuAmt = displayReceipt.otherData.Rows[i]["fxcu_Amt"].ToString();

                                if (dataType == "I")
                                {
                                    if (line == "1")
                                    {
                                        UCItemSell ucitmVoid = new UCItemSell();
                                        ucitmVoid.lbNo.Text = rec.ToString();
                                        ucitmVoid.lbUC_ProductCode.Text = codeOrName;
                                        ucitmVoid.lbUC_Qty.Text = double.Parse(qty).ToString();
                                        ucitmVoid.lbUC_Price.Text = double.Parse(price).ToString(amtFormat);
                                        ucitmVoid.lbUC_Discount.Text = double.Parse(dis).ToString(amtFormat);
                                        ucitmVoid.lbUC_TotalPrice.Text = (double.Parse(amt)).ToString(amtFormat);
                                        if (vatType == "1")
                                        {
                                            totalTax += double.Parse(amt);
                                        }
                                        codeOrName = displayReceipt.otherData.Rows[i + 1]["ProductDesc"].ToString();
                                        ucitmVoid.lbProductName.Text = codeOrName;
                                        disPrice += double.Parse(ucitmVoid.lbUC_Discount.Text);
                                        amtPrice += double.Parse(ucitmVoid.lbUC_TotalPrice.Text);
                                        if (double.Parse(qty) == 1 && double.Parse(price) != double.Parse(amt) && double.Parse(dis) == 0)
                                        {
                                            totalPrice += double.Parse(ucitmVoid.lbUC_TotalPrice.Text);
                                        }
                                        else
                                        {
                                            totalPrice += double.Parse(amt);//(double.Parse(ucitmVoid.lbUC_Price.Text) * double.Parse(ucitmVoid.lbUC_Qty.Text));
                                        }


                                        pn_Item.Controls.Add(ucitmVoid);
                                        i += 2;
                                    }

                                }
                                else if (dataType == "P")
                                {
                                    string paymentType = displayReceipt.otherData.Rows[i]["ProductDesc"].ToString().Replace(" ", "");
                                    value1 = displayReceipt.otherData.Rows[i]["Discount"].ToString();
                                    value2 = displayReceipt.otherData.Rows[i]["Amount"].ToString();


                                    //changeValue += double.Parse(change);
                                    //UCItemVoid ucitmListP = new UCItemVoid();
                                    //ucitmListP.lbNo.Text = rec.ToString();
                                    //ucitmListP.lbPayment.Text = codeOrName;
                                    if (paymentType.Trim() == "CHGD")
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
                                    else if (paymentType.Trim() == "CASH")
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
                                    else if (receiptNo.Substring(0, 2) == "CN")
                                    {
                                        amtPrice = double.Parse(value2);
                                        UCItemVoid ucitmListP = new UCItemVoid();
                                        ucitmListP.lbUC_No.Text = rec.ToString();
                                        ucitmListP.lbUC_Payment.Text = codeOrName;
                                        ucitmListP.lbUC_Price.Text = double.Parse(amt).ToString(amtFormat);
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
                                        UCItemVoid ucitmListP = new UCItemVoid();
                                        ucitmListP.lbUC_No.Text = rec.ToString();
                                        ucitmListP.lbUC_Payment.Text = codeOrName;
                                        ucitmListP.lbUC_Price.Text = double.Parse(amt).ToString(amtFormat);
                                        pnTypePDetail.Controls.Add(ucitmListP);
                                        pnTypePDetail.Controls.SetChildIndex(ucitmListP, 0);
                                        i++;
                                    }                  //ucitmListP.lbPrice.Text = double.Parse(amt).ToString(amtFormat);
                                    //pnTypePDetail.Controls.Add(ucitmListP);
                                    //i++;

                                    
                                }
                                else if (dataType == "V")
                                {
                                    //totalVat = displayReceipt.otherData.Rows[i]["Amount"].ToString();
                                    vat = displayReceipt.otherData.Rows[i]["Amount"].ToString();
                                    i++;
                                    //string vatType = displayReceipt.otherData.Rows[i]["ProductDesc"].ToString();
                                    //if (vatType == "VATABLE")
                                    //{
                                    //    totalVat = displayReceipt.otherData.Rows[i]["Amount"].ToString();
                                    //    vat = displayReceipt.otherData.Rows[i + 1]["Amount"].ToString();
                                    //    i+= 2;
                                    //}
                                    //else if (vatType == "Vat Value")
                                    //{
                                    //    vat = displayReceipt.otherData.Rows[i]["Amount"].ToString();
                                    //    i++;
                                    //}

                                }
                                else if (dataType == "D")
                                {
                                    i++;
                                }
                                else if (dataType == "C")
                                {
                                    if (line == "2")
                                    {
                                        memberName = displayReceipt.otherData.Rows[i]["ProductDesc"].ToString();
                                    }
                                    i++;
                                }
                                else if (dataType == "T")
                                {
                                    _saleTime = Convert.ToDateTime(displayReceipt.otherData.Rows[i]["ProductDesc"].ToString(), cultureinfo).ToString("dd/MM/yyyy HH:mm:ss", cultureinfo);
                                    i++;
                                }
                                else
                                {
                                    i++;
                                }

                            }

                            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pnTypePDetail);
                            Utility.SetGridColorAlternate<UCItemVoid>(pnTypePDetail.Controls.Cast<UCItemVoid>().ToList(), Color.FromArgb(225, 225, 225));
                            //AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pnTypePDetail);

                            RefreshGrid();
                            pn_DisplayDetail.BringToFront();
                            lbTxtDiscountTotal.Text = disPrice.ToString(amtFormat);
                            lbTxtTotal.Text = (totalPrice).ToString(amtFormat);
                            lbTxtTotalRes.Text = (amtPrice + upPrice - downPrice).ToString(amtFormat);
                            lbWorkOnResult.Text = ProgramConfig.serverName;
                            lbReceiptNoResult.Text = ucTBScanBarcode.Text;
                            lbLockNoResult.Text = ProgramConfig.tillNo;
                            lbUserIdResult.Text = ProgramConfig.userId;
                            lbDateResult.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cultureinfo);
                            lbMemberName.Text = memberName;//ProgramConfig.cashireAuthorizeResult.otherData.Rows[0]["UserNameLocal"].ToString();
                            lbCashBalanceResult.Text = changeValue.ToString(amtFormat);
                            lbTotalTaxResult.Text = (totalTax).ToString(amtFormat);
                            lbTaxResult.Text = double.Parse(vat).ToString(amtFormat);

                            ucKeyboard1.Visible = false;
                        }
                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(ResponseCode.Error, checkReceipt.responseMessage, checkReceipt.helpMessage);
                        dialog.ShowDialog(this);
                        ucTBScanBarcode.Text = "";
                        ucTBScanBarcode.Focus();
                        return;
                    }
                }
                else
                {
                    frmNotify dialog = new frmNotify(checkReceipt);
                    dialog.ShowDialog(this);
                    ucTBScanBarcode.Text = "";
                    ucTBScanBarcode.Focus();
                    return;
                }

                //ucTBScanBarcode.Text = "";
                this.ActiveControl = btnCancelReceipt;
                //ucTBScanBarcode.Focus();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    defaultSetting();
                    ucFooterTran1.IsStandAlone = true;
                    Utility.CheckRunningNumber();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }

        }

        private void RefreshGrid()
        {
            List<UCItemSell> lstItemVoid = new List<UCItemSell>();
            lstItemVoid = pn_Item.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_Item.Controls.Clear();
            int num = lstItemVoid.Count;

            foreach (UCItemSell item in lstItemVoid)
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
                pn_Item.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_Item);
            ScrollToBottom(pnTypePDetail);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void btnCancelReceipt_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            string eventName = MappingEventVoid();
            Profile checkReason = ProgramConfig.getProfile(FunctionID.Void_InputReason);
            if (checkReason.policy == PolicyStatus.Work)
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
                        defaultSetting();
                        ucFooterTran1.IsStandAlone = true;
                        return;
                    }
                }
            }

            frmLoading.closeLoading();
            runningProcess();
                                       
        }

        private string MappingEventVoid()
        {
            if (_saleType == VoidSaleType.CreditSale)
            {
                return "CreditSale";
            }
            if (_saleType == VoidSaleType.POD)
            {
                return "POD";
            }
            if (_saleType == VoidSaleType.Deposit)
            {
                return "Deposit";
            }
            else
            {
                return "Void";
            }

        }

        public void runningProcess()
        {
            //OpenCashDrawer();

            frmVoidSuccess voidSuccess = new frmVoidSuccess(receiptNo, _reasonId, _reasonTxt, lbLockNoResult.Text, lbUserIdResult.Text, lbMemberName.Text, _saleTime, lbTxtTotalRes.Text, _saleType);
            var res = voidSuccess.ShowDialog(this);
            if (res == System.Windows.Forms.DialogResult.Retry)
            {
                defaultSetting();
                ucFooterTran1.IsStandAlone = true;
                return;
            }

            //completeSetting();            

        }

        private void closeForm()
        {
            //Program.control.CloseForm("frmNotify");

            Program.control.CloseForm("frmVoid");
            
            foreach (Form item in Application.OpenForms)
            {
                if (item is frmMainMenu)
                {
                    frmMainMenu = (frmMainMenu)item;
                    frmMainMenu.BringToFront();
                    break;
                }

            }
            
        }

        private void closeNotify()
        {
            Program.control.CloseForm("frmNotify");
        }

        public void defaultSetting ()
        {
            pn_Item.Controls.Clear();
            pnTypePDetail.Controls.Clear();
            lbCancelReceipt.Visible = true;
            lbConfirmCancelReceipt.Visible = false;
            btnCancelReceipt.Visible = true;
            btnClose.Visible = false;
            ucTBScanBarcode.Text = "";
            lbTxtDiscountTotal.Text = "0";
            lbTxtTotal.Text = "0";
            lbTxtTotalRes.Text = "0";
            
            panelScanBarcode.BringToFront();
            panelKeyNumber.BringToFront();
            ucTBScanBarcode.Select();
            ucTBScanBarcode.Focus();

        }

        public void completeSetting ()
        {
            lbCancelReceipt.Visible = false;
            lbConfirmCancelReceipt.Visible = true;
            btnCancelReceipt.Visible = false;
            btnClose.Visible = true;

        }

        private void ucTBScanBarcode_EnterFromButton(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            keyReceipt();
            frmLoading.closeLoading();  
        }

        private void ucTBScanBarcode_TextBoxKeydown(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            keyReceipt();
            frmLoading.closeLoading();
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            closeForm();
            frmLoading.closeLoading();
        }

        //public void OpenCashDrawer()
        //{
        //    //Open Cash Drawer
        //    openTime = DateTime.Now.ToString("HH:mm:ss", cultureinfo);
        //    Hardware.addDrawerListeners(DrawerStatus);
        //    chk = Hardware.openDrawer();
        //    //chkOpenDrawer = true;
        //    //Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Void_OpenDrawerAndRecordTime);
        //    //if (checkOpenDrawer.policy == PolicyStatus.Work)
        //    //{
        //        StoreResult res = process.saveVoidTransaction(receiptNo, openTime, _reasonId);
        //        if (!res.response.next)
        //        {
        //            frmNotify dialog = new frmNotify(res);
        //            dialog.ShowDialog(this);
        //            return;
        //        }
        //        else
        //        {
        //            if (res.response == ResponseCode.Information)
        //            {
        //                frmNotify dialog = new frmNotify(res);
        //                dialog.ShowDialog(this);
        //            }
        //        }
        //    //}

        //}

        public void DrawerStatus(string status)
        {
            if (status.ToUpper() == "FALSE")
            {
                //Save time Close Cash Drawer
                string closeTime = DateTime.Now.AddMinutes(2).ToString("HHmmss", cultureinfo);

                Profile check = ProgramConfig.getProfile(FunctionID.Void_CloseDrawerAndRecordTime);
                if (check.policy == PolicyStatus.Work)
                {
                    StoreResult resClose = process.saveCloseDrawer(FunctionID.Void_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
                    if (!resClose.response.next)
                    {
                        frmNotify dialog = new frmNotify(resClose);
                        dialog.ShowDialog(this);
                        return;
                    }
                    else
                    {
                        if (resClose.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(resClose);
                            dialog.ShowDialog(this);
                        }
                    }

                }
                closeNotify();

            }
        }

        public void frmAllDisplayReasonData(string reasonID)
        {
            _reasonId = reasonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            closeForm();
            frmLoading.closeLoading();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            defaultSetting();
        }

        private void ucTBScanBarcode_Enter(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.BringToFront();
            this.ucKeyboard1.currentInput = ucTBScanBarcode;
        }       
    }
}
