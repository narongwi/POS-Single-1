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
using System.Text.RegularExpressions;
using System.Threading;
using System.Security.Permissions;


namespace BJCBCPOS
{
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public partial class frmSale : Form, IMessageFilter
    {

        private frmNotify notify;
        public frmMainMenu frmMain;
        private bool chkOpenDrawer = false;
        public SaleProcess process = new SaleProcess();
        public frmSaleProcess fSaleProcess = null;
        private UCItemSell lastUCIS = new UCItemSell();
        private string currentPanel = "";
        private int cntTimeoutSale = 0;
        private int timeOutSale = ProgramConfig.timeOutActionIdle;

        Sale_TypeCode saleTypeCode;

        public List<string> lstDDL { get; set; }

        private UCItemSell ucGV;
        string qty = "";
        double amtPrice = 0;
        public string code;
        public string name;
        public string price;
        public string quant = "";
        public string currentPrice;
        public string currentDis;
        public string seq;
        public string deleteType;
        public string editType;
        public string discID;
        public string _IsFFNRTC;
        public string _PrType;
        public string _UPCDB;
        public double _totalPriceChange = 0;

        string quntValue;

        string displayAmt = ProgramConfig.amountFormatString;

        public frmMonitorCustomer frmMoCus = null;
        public frmMonitorCustomerFooter frmMoFooter = null;
        public frmMonitor2Detail frm2Detail = null;
        public string memberID;
        public string memberName;
        public string memberCardNo;
        //public UCTextBoxWithIcon ucTBWI { get; set; }
        public UCTextBoxSmall ucTBS { get; set; }
        public bool afterNotify = false;
        public string productCode = "";
        public string recExport = "";

        private DataTable dtSaleOrderMenu;

        public frmSale()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(frmSale_Disposed);
            fSaleProcess = new frmSaleProcess(this);
        }

        //public void DrawerStatus(string status)
        //{
        //    chkOpenDrawer = true;
        //}

        private void frmSale_Load(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                Application.AddMessageFilter(this);
                ProgramConfig.pageBackFromPayment = PageBackFormPayment.NormalSale;
                ucHeader1.alertFunctionID = FunctionID.Sale_GetMessageCashier;
                afterNotify = false;
                if (ProgramConfig.salePageState == 0)
                {
                    if (!timer1.Enabled)
                    {
                        timer1.Start();
                    }
                    cntTimeoutSale = 0;
                    // initial page load from main menu
                    //ucTBScanBarcode.Focus();

                    Utility.GlobalClear();

                    ProgramConfig.employeeID = "";

                    StoreResult result = null;
                    //panelScanBarcode.BringToFront();
                    //currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
                    //Hardware.addDrawerListeners(DrawerStatus);

                    if (ProgramConfig.saleNeedAuthorize)
                    {
                        if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.Sale_SelectSaleMenu }, "Sale"))
                        {
                            textLanguageChange();
                            return;
                        }

                        textLanguageChange();
                    }

                    result = process.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.SaleRef);
                    if (result.response.next)
                    {
                        lbTxtRefNo.Text = ProgramConfig.saleRefNo;
                    }
                    else
                    {
                        notify = new frmNotify(result);
                        notify.ShowDialog();
                        this.Dispose();
                        return;
                    }

                    Profile chkMCashier = ProgramConfig.getProfile(FunctionID.Sale_GetMessageCashier);
                    if (chkMCashier.policy == PolicyStatus.Work)
                    {
                        ProcessResult res = process.cashireMessageStatus();
                        if (res.response.next)
                        {
                            if (result.response == ResponseCode.Information)
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

                    if (!ProgramConfig.skipNormalSale)
                    {
                        if (ProgramConfig.normalSaleNeedAuthorize)
                        {
                            if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.Sale_InputSaleItem_InputProduct_NormalSale }, "Sale"))
                            {
                                textLanguageChange();
                                return;
                            }
                            textLanguageChange();
                        }
                        panelScanBarcode.Enabled = true;
                        setVisibleButtonPayment();
                    }
                    else
                    {
                        //ปิดการขายปกติ
                        panelScanBarcode.Enabled = false;
                        setVisibleButtonPayment();
                    }

                    if (ProgramConfig.checkSaleCashIn)
                    {
                        frmLoading.showLoading();
                        result = process.posCheckCashInSaleAmt();
                        frmLoading.closeLoading();
                        if (!result.response.next || result.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(result);
                            DialogResult auth_res = dialog.ShowDialog(this);
                            if (auth_res != DialogResult.No)
                            {
                            }
                            else
                            {
                                this.Dispose();
                                return;
                            }
                        }
                    }

                    int getPdInputType = ProgramConfig.productInputType;
                    if (getPdInputType == 1)
                    {
                        //Scan
                        ucTBScanBarcode.Enabled = true;
                        //btnGoodSales.Enabled = false;
                        btnGoodSales.Enabled = true;
                    }
                    else if (getPdInputType == 2)
                    {
                        //Icon
                        ucTBScanBarcode.Enabled = false;
                        btnGoodSales.Enabled = true;
                    }
                    else if (getPdInputType == 3)
                    {
                        //Both
                        ucTBScanBarcode.Enabled = true;
                        btnGoodSales.Enabled = true;
                    }

                    int getConfig = ProgramConfig.defaultCursorPosition;

                    if (getConfig == 1) //Product
                    {
                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
                        ucTBScanBarcode.Focus();
                    }
                    else if (getConfig == 2) //Member
                    {
                        currentPanel = CurrentPanelSale.PanelMember; 
                        clickSearchMember(sender, e);                  
                    }
                    else
                    {
                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        ucTBScanBarcode.Focus();
                    }

                    //Program.control.ShowForm("frmMonitorCustomer");
                    //Program.control.ShowForm("frmMonitorCustomerFooter");
                    //Program.control.ShowForm("frmMonitor2Detail");

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

                    dtSaleOrderMenu = process.selectSALEORDER_MENU();

                    //loadTempDLYPTRANS();

                    CallCheckPrintInvoidType();

                    ProgramConfig.salePageState = 1;
                }
                else if (ProgramConfig.salePageState == 1)
                {
                    // current sale process not end
                    ucTBScanBarcode.Focus();

                    loadTempDLYPTRANS();
                }
                else if (ProgramConfig.salePageState == 2)
                {
                    // end current sale prepare for new receipt
                    frmSale_Activated(sender, e);
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
                //if (CatchNetWorkConnectionException(net))
                //{
                //    frmSale_Load(sender, e);
                //}
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void CallCheckPrintInvoidType()
        {
            //#778
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CheckPrinterInvoiceType);
            if (check.policy == PolicyStatus.Work)
            {
                var printType = (SelectPrintInvoiceType)ProgramConfig.getPosConfig("SelectPrintInvoiceType");
                frmNotify notify = new frmNotify(ResponseCode.Warning, "ต้องการพิมพ์ใบเสร็จ ABB หรือไม่?");
                if (printType == SelectPrintInvoiceType.PrintABB)
                {
                    var resDialog = notify.ShowDialog(this);
                    if (resDialog == System.Windows.Forms.DialogResult.Yes)
                    {
                        ProgramConfig.printInvoiceType = PrintInvoiceType.ABB;
                    }
                    else
                    {
                        ProgramConfig.printInvoiceType = (PrintInvoiceType)ProgramConfig.getPosConfig("DefaultPrintInvoiceType");
                    }
                }
                else if (printType == SelectPrintInvoiceType.PrintFullTax)
                {
                    notify = new frmNotify(ResponseCode.Warning, "ต้องการพิมพ์ใบเสร็จ FullTax หรือไม่?");
                    var resDialog = notify.ShowDialog(this);
                    if (resDialog == System.Windows.Forms.DialogResult.Yes)
                    {
                        ProgramConfig.printInvoiceType = PrintInvoiceType.FULLTAX;
                    }
                    else
                    {
                        ProgramConfig.printInvoiceType = (PrintInvoiceType)ProgramConfig.getPosConfig("DefaultPrintInvoiceType");
                    }
                }
                else
                {
                    ProgramConfig.printInvoiceType = (PrintInvoiceType)ProgramConfig.getPosConfig("DefaultPrintInvoiceType");
                }

            }
            else
            {
                ProgramConfig.printInvoiceType = (PrintInvoiceType)Convert.ToInt32(ProgramConfig.getPosConfig("DefaultPrintInvoiceType"));
            }
        }

        private void picBtBack5_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void picBtBack4_Click(object sender, EventArgs e)
        {
            picBtBack4.Focus();
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void picBtBack3_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void picBtBackToDelete_Click(object sender, EventArgs e)
        {
            panelDeleteItem.BringToFront();
            currentPanel = CurrentPanelSale.PanelDeleteItem;  //currentPanel = "panelDeleteItem";
            DisableControl();
        }

        private void picBtBack2_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void picBtBack1_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void picBtBack_Click(object sender, EventArgs e)
        {
            picBtBack.Focus();
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                double amountCash = double.Parse(lbTxtTotal.Text);
                if (amountCash <= 0)
                {
                    process.savePaymentCashBalance("0.00", "0.00", "CASH", "0.00", "", "", "");
                }
              
                ShowPayment(amountCash.ToString(displayAmt));

            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkConnectionException(net);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void ShowPayment(string total)
        {
            Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Main); //#137 #40 

            ProgramConfig.disValue = lbTxtdiscount1.Text;
            ProgramConfig.amtValue = lbTxtSubtotal.Text;
            ProgramConfig.totalValue = total;

            //process.SaveMember();

            frm2Detail.label1.Text = "";
            frm2Detail.label2.Text = "";
            frm2Detail.label1.BackColor = Color.White;
            frm2Detail.label2.BackColor = Color.White;
            frm2Detail.panel_list.Controls.Clear();
            frm2Detail.panel_payment.BringToFront();
            //frm2Detail.lbTxtTotalCash.Text = amountCash.ToString(displayAmt);
            frm2Detail.lbTxtTotalCash.Text = lbTxtTotal.Text;
            //Program.control.CloseForm("frmPayment");
            //Program.control.ShowForm("frmPayment");

            ProgramConfig.paymentOpenCashDrawer = FunctionID.Sale_OpenDrawerAndRecordTime;
            ProgramConfig.paymentCloseCashDrawer = FunctionID.Sale_CloseDrawerAndRecordTime;
            ProgramConfig.paymentFunction = FunctionID.Sale_DisplayPaymentMenu;
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.NormalSale;

            if (chkRedeem.policy == PolicyStatus.Work)
            {
                if (ProgramConfig.memberId.Trim() != "" && ProgramConfig.memberId != "N/A")
                {
                    if (!Utility.CheckAuthPass(this, chkRedeem, "Redeem"))
                    {
                        return;
                    }
                    frmRedeem frm = new frmRedeem(RedeemPage.Product);
                    Program.control.ShowForm(frm, "frmRedeem");
                    return;
                }
            }


            Program.control.CloseForm("frmPayment");
            Program.control.ShowForm("frmPayment");
        }

        private void btnProductAndOther_Click(object sender, EventArgs e)
        {
            Program.control.ShowForm("frmProductAndService", this);
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (pn_item_sell.Controls.Count > 0)
            {
                DisableButtons();
                btnMultiplyItem.Tag = "disable";
                btnDeleteItem.Tag = "disable";
                if (btnEditItem.Tag == null || (string)btnEditItem.Tag == "disable")
                {
                    btnEditItem.Tag = "enable";
                    btnEditItem.BackgroundImage = Properties.Resources.change_enable;
                    btnEditItem.ForeColor = Color.White;
                }
                else
                {
                    btnEditItem.Tag = "disable";
                }

                DisableBtnFavorit();
            }
            else
            {
                btnEditItem.Tag = "disable";
            }
            ucTBScanBarcode.Focus();
            //panelEditItem.BringToFront();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (pn_item_sell.Controls.Count > 0)
            {
                DisableButtons();
                btnMultiplyItem.Tag = "disable";
                btnEditItem.Tag = "disable";
                if (btnDeleteItem.Tag == null || (string)btnDeleteItem.Tag == "disable")
                {
                    btnDeleteItem.Tag = "enable";
                    btnDeleteItem.BackgroundImage = Properties.Resources.remove_enable;
                    btnDeleteItem.ForeColor = Color.White;
                }
                else
                {
                    btnDeleteItem.Tag = "disable";
                }

                DisableBtnFavorit();
            }
            else
            {
                btnDeleteItem.Tag = "disable";
            }
            ucTBScanBarcode.Focus();
            //panelDeleteItem.BringToFront();
        }

        private void ucHeader1_MemberClick(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
            //clickSearchMember();
        }

        public void clickSearchMember(object sender, EventArgs e)
        {
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_Member_Search);
            if (check.policy == PolicyStatus.Work)
            {
                //#747
                check = ProgramConfig.getProfile(FunctionID.Sale_BeforeInputProductItem_DefaultCursorPosition);
                if (check.policy == PolicyStatus.Work)
                {
                    ucHeader1.showMember_ButtonBack = false;
                }
                ucHeader1.btnMember_Click(sender, e);
                this.ActiveControl = ucKeypad.ucTBWI;
                //panelMember.BringToFront();
                //ucTBWI_Member.InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_search, UCTextBoxIconType.SearchAndDelete, IconType.Search, "ກະລຸນາລະບຸສະມາຊິກ");
                //ucTBWI_Member.Focus();
            }
            else
            {
                this.ActiveControl = ucTBScanBarcode;
            }
        }

        private void ucTBWI_Member_IconClick(object sender, EventArgs e)
        {
            string eventName = "Sale";
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_Member_Search);
            if (check.policy == PolicyStatus.Work)
            {
                frmLoading.showLoading();
                frmSearchMember frm = new frmSearchMember((UCTextBoxWithIcon)sender, eventName);
                frm.ShowDialog(this);
                frmLoading.closeLoading();
            }

            if (ucTBWI_Member.Text != "")
            {
                ucHeader1.nameText = memberName;
                ucHeader1.nameVisible = true;
                Label newFont = new Label();
                newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                int checkWidth = TextRenderer.MeasureText(ucHeader1.nameText, newFont.Font).Width;
                ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);
            }
            else
            {
                ClearMember();
            }
            DisableControl();
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
            ucTBScanBarcode.Focus();
        }

        private void ucHeader1_MemberEnterFromButton(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode;
            ucTBScanBarcode.Focus();
        }

        private void picBtBack6_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void DisableControl()
        {
            AppLog.writeLog("before Properties.Resources.multi_disable;");
            btnEditItem.Tag = "disable";
            btnDeleteItem.Tag = "disable";
            btnMultiplyItem.Tag = "disable";

            if (btnMultiplyItem.InvokeRequired)
            {
                btnMultiplyItem.BeginInvoke((MethodInvoker)delegate
                {
                    btnMultiplyItem.BackgroundImage = Properties.Resources.multi_disable;
                });
            }
            else
            {
                btnMultiplyItem.BackgroundImage = Properties.Resources.multi_disable;
            }
            AppLog.writeLog("after Properties.Resources.multi_disable;");

            if (btnEditItem.InvokeRequired)
            {
                btnEditItem.BeginInvoke((MethodInvoker)delegate
                {
                    btnEditItem.BackgroundImage = Properties.Resources.change_disable;
                });
            }
            else
            {
                btnEditItem.BackgroundImage = Properties.Resources.change_disable;
            }           
            AppLog.writeLog("after Properties.Resources.change_disable;");

            if (btnDeleteItem.InvokeRequired)
            {
                btnDeleteItem.BeginInvoke((MethodInvoker)delegate
                {
                    btnDeleteItem.BackgroundImage = Properties.Resources.remove_disable;
                });
            }
            else
            {
                btnDeleteItem.BackgroundImage = Properties.Resources.remove_disable;
            }                    
            AppLog.writeLog("after Properties.Resources.remove_disable;");


            btnMultiplyItem.ForeColor
                = btnEditItem.ForeColor
                = btnDeleteItem.ForeColor
                = Color.Gray;
            lbQty.Visible = false;
            ucTxtQty.Visible = false;
            if (pn_item_sell.Controls.Count == 0)
            {
                //CalDiscount
                setVisibleButtonPayment();
            }
            ucDDLMenuExtra.LabelText = AppMessage.getMessage(ProgramConfig.language, "frmSale", "ucDDLMenuExtra");
            lbDiscID.Text = "";
            lbIsFFNTRC.Text = "";
            lbPR_Type.Text = "";
            lbDeletedbPrice.Text = "";
            lbRecNo.Text = "";
            ucTxtQty.Text = "";
            setFocus();
        }

        private void DisableButtons()
        {
            btnMultiplyItem.BackgroundImage = Properties.Resources.multi_disable;
            btnEditItem.BackgroundImage = Properties.Resources.change_disable;
            btnDeleteItem.BackgroundImage = Properties.Resources.remove_disable;
            btnMultiplyItem.ForeColor
                = btnEditItem.ForeColor
                = btnDeleteItem.ForeColor
                = Color.Gray;
            lbQty.Visible = false;
            ucTxtQty.Visible = false;
            ucTBScanBarcode.Text = "";
            ucTBScanBarcode.Focus();
            ucTBWI_Price1.Text = "";
            ucTBWI_Price2.Text = "";
            ucTBWI_Member.Text = "";
            ucTxtQty.Text = "";
            setFocus();
        }

        private void setFocus()
        {
            //if (currentPanel == CurrentPanelSale.PanelScanBarcode) //if (currentPanel == "panelScanBarcode")
            //{
            //    if (ucTBScanBarcode.InvokeRequired)
            //    {
            //        ucTBScanBarcode.BeginInvoke((MethodInvoker)delegate
            //        {
            //            ucTBScanBarcode.Focus();
            //        });
            //    }
            //    else
            //    {
            //        ucTBScanBarcode.Focus();
            //    }
            //}
            if (currentPanel == CurrentPanelSale.PanelEditItem) //else if (currentPanel == "panelEditItem")
            {
                if (ucTBWI_Price1.InvokeRequired)
                {
                    ucTBWI_Price1.BeginInvoke((MethodInvoker)delegate
                    {
                        ucTBWI_Price1.Focus();
                    });
                }
                else
                {
                    ucTBWI_Price1.Focus();
                }
            }
            else if (currentPanel == CurrentPanelSale.PanelDeleteItem) //else if (currentPanel == "panelDeleteItem")
            {
                if (ucTBWI_Qty2.InvokeRequired)
                {
                    ucTBWI_Qty2.BeginInvoke((MethodInvoker)delegate
                    {
                        ucTBWI_Qty2.Focus();
                    });
                }
                else
                {
                    ucTBWI_Qty2.Focus();
                }
            }
            else if (currentPanel == CurrentPanelSale.PaneltemDetail) //else if (currentPanel == "paneltemDetail")
            {
                if (paneltemDetail.InvokeRequired)
                {
                    paneltemDetail.BeginInvoke((MethodInvoker)delegate
                    {
                        paneltemDetail.BringToFront();
                    });
                }
                else
                {
                    paneltemDetail.BringToFront();
                }

                if (ucTBPrice.InvokeRequired)
                {
                    ucTBPrice.BeginInvoke((MethodInvoker)delegate
                    {
                        ucTBPrice.Focus();
                    });
                }
                else
                {
                    ucTBPrice.Focus();
                }              
            }
            else if (currentPanel == CurrentPanelSale.PanelPrintExport)  //else if (currentPanel == "panelPrintExport")
            {
                if (pictureBox2.InvokeRequired)
                {
                    pictureBox2.BeginInvoke((MethodInvoker)delegate
                    {
                        pictureBox2.Focus();
                    });
                }
                else
                {
                    pictureBox2.Focus();
                }
                
            }
            else if (currentPanel == CurrentPanelSale.PanelAddProductSpecial)    //else if (currentPanel == "panelAddProductSpecial")
            {
                if (ucKeypad.InvokeRequired)
                {
                    ucKeypad.BeginInvoke((MethodInvoker)delegate
                    {
                        ucKeypad.ucTBWI = null;
                    });
                }
                else
                {
                    ucKeypad.ucTBWI = null;
                }

                if (ucDDCause.InvokeRequired)
                {
                    ucDDCause.BeginInvoke((MethodInvoker)delegate
                    {
                        ucDDCause.Focus();
                    });
                }
                else
                {
                    ucDDCause.Focus();
                }
            }
            else
            {
                var ucTxt = ucKeypad.ucTBWI;
                ucTxt.FocusTxt();
            }
        }

        private void btnMultiplyItem_Click(object sender, EventArgs e)
        {
            DisableButtons();
            btnEditItem.Tag = "disable";
            btnDeleteItem.Tag = "disable";
            if (btnMultiplyItem.Tag == null || (string)btnMultiplyItem.Tag == "disable")
            {
                lbQty.Visible = true;
                ucTxtQty.Visible = true;
                ucTxtQty.Focus();
                btnMultiplyItem.BackgroundImage = Properties.Resources.multi_enable;
                btnMultiplyItem.ForeColor = Color.White;
                btnMultiplyItem.Tag = "enable";

                DisableBtnFavorit();
            }
            else
            {
                btnMultiplyItem.Tag = "disable";
                ucTBScanBarcode.Focus();
            }
        }

        public void frmCancelEditData()
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            ucTBScanBarcode.Text = "";
            ucTBScanBarcode.Focus();
            this.ActiveControl = ucTBScanBarcode;
        }

        public void frmDeleteEditData(string productBarcode, string productName, string productQty, string productPrice, string mode, string discID, string IsFFNRTC, string pr_type, string upcDB)
        {
            this.code = productBarcode;
            this.name = productName;
            double dbpdQty = double.Parse(productQty);
            this.quant = dbpdQty.ToString();
            double dbpdPrice = double.Parse(productPrice);
            this.price = dbpdPrice.ToString(displayAmt);
            this.discID = discID;
            _IsFFNRTC = IsFFNRTC;
            _PrType = pr_type;
            _UPCDB = upcDB;

            if (mode == "edit")
            {
                panelEditItem.BringToFront();
                currentPanel = CurrentPanelSale.PanelEditItem; //currentPanel = "panelEditItem";
                ucTBWI_Price1.Focus();
            }
            else if (mode == "delete")
            {
                panelDeleteItem.BringToFront();
                currentPanel = CurrentPanelSale.PanelDeleteItem; //currentPanel = "panelDeleteItem";
                if (_IsFFNRTC == "Y")
                {
                    ucTBWI_Qty2.EnabledUC = false;
                }
                else
                {
                    ucTBWI_Qty2.EnabledUC = true;
                }

                ucTBWI_Qty2.Focus();
            }
        }

        public void frmSearchMemberData(string memberData, string memberDataName, string memberCardNo)
        {
            this.memberID = memberData;
            ProgramConfig.memberId = memberID;
            this.memberName = memberDataName;
            ProgramConfig.memberName = memberName;
            this.memberCardNo = memberCardNo;
            ProgramConfig.memberCardNo = memberCardNo;
        }

        public void frmSearchMemberDataAuto(string memberDataId, string memberDataName, string memberCardNo)
        {
            this.memberID = memberDataId;
            ProgramConfig.memberId = memberID;
            this.memberName = memberDataName;
            ProgramConfig.memberName = memberDataName;
            this.memberCardNo = memberCardNo;
            ProgramConfig.memberCardNo = memberCardNo;
        }

        public void goodSales(string productCode)
        {
            ucTxtQty.Text = quntValue;
            //ucTBScanBarcode.Text = productCode;
            keyProduct(false, productCode);
            quntValue = "";
        }

        public void keyProduct(bool IsNoScanBarcode, string barcode)     
        {
            try
            {
                AppLog.writeLog("start keyProduct()");  
                ucTBScanBarcode.EnabledUC = false;
                if (barcode.Length < 13 && barcode.Length != 0)
                {
                    barcode = barcode.PadLeft(13, '0');
                }

                #region Delete
                if ((string)btnDeleteItem.Tag == "enable")
                {
                    StoreResult result = process.searchItem(barcode, SearchItemAction.Delete);
                    if (result.response.next)
                    {
                        if (result.response == ResponseCode.Information)
                        {
                            notify = new frmNotify(result);
                            notify.ShowDialog(this);
                        }

                        ucTBScanBarcode.Text = "";
                        int rowCount = result.otherData.Rows.Count;
                        if (result.otherData != null)
                        {                            
                            if (rowCount == 1)
                            {
                                lbDiscID.Text = result.otherData.Rows[0]["DISCID"].ToString();
                                lbPR_Type.Text = result.otherData.Rows[0]["PRODUCT_TYPE"].ToString();
                                code = result.otherData.Rows[0]["ProductCode"].ToString();
                                name = result.otherData.Rows[0]["PRoductNAme"].ToString();
                                double dbprice = double.Parse(result.otherData.Rows[0]["Price"].ToString());
                                price = dbprice.ToString(displayAmt);
                                double dbQuant = double.Parse(result.otherData.Rows[0]["Quant"].ToString());
                                quant = dbQuant.ToString();
                                seq = result.otherData.Rows[0]["Seq"].ToString();
                                lbIsFFNTRC.Text = lbPR_Type.Text == "RTC" || lbPR_Type.Text == "FF" ? "Y" : "N";
                                lbDeletedbPrice.Text = dbprice.ToString();
                                panelDeleteItem.BringToFront();
                                currentPanel = CurrentPanelSale.PanelDeleteItem; //currentPanel = "panelDeleteItem";
                                lbTxtProductCode2.Text = code;
                                lbTxtDesc2.Text = name;

                                if (lbPR_Type.Text == "FF" || lbPR_Type.Text == "RTC")
                                {
                                    ucTBWI_Qty2.Enabled = false;
                                }
                                else
                                {
                                    ucTBWI_Qty2.Enabled = true;
                                }
                   
                                lbCurrentQty2.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentQty2"), quant); //"จำนวนเดิม " + quant + " ชิ้น";
                                ucTBWI_Price2.Text = price;
                                ucTBWI_Price2.Enabled = false;
                                lbCurrentPrice2.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice2"), price, ProgramConfig.currencyDefault); //"ราคาเดิม " + price + " " + ProgramConfig.currencyDefault;
                                ucTBWI_Qty2.Text = quant;

                                ucTBWI_Qty2.FocusWithDisable = true;
                            }
                            else
                            {
                                using (frmDeleteEditItem frm = new frmDeleteEditItem(result, "delete"))
                                {
                                    // passing this in ShowDialog will set the .Owner 
                                    // property of the child form
                                    frm.ShowDialog(this);
                                    lbDiscID.Text = discID;
                                    lbTxtProductCode2.Text = code;
                                    lbTxtDesc2.Text = name;
                                    lbIsFFNTRC.Text = _IsFFNRTC;
                                    lbPR_Type.Text = _PrType;
                                    lbDeletedbPrice.Text = _UPCDB;

                                    if (lbPR_Type.Text == "FF" || lbPR_Type.Text == "RTC")
                                    {
                                        ucTBWI_Qty2.Enabled = false;
                                    }
                                    else
                                    {
                                        ucTBWI_Qty2.Enabled = true;
                                    }
                                   
                                    lbCurrentQty2.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentQty2"), quant); //"จำนวนเดิม " + quant + " ชิ้น";
                                    ucTBWI_Price2.Text = price;
                                    ucTBWI_Price2.Enabled = false;
                                    lbCurrentPrice2.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice2"), price, ProgramConfig.currencyDefault); //"ราคาเดิม " + ucTBWI_Price2.Text + " " + ProgramConfig.currencyDefault; 
                                    
                                    ucTBWI_Qty2.Text = quant;
                                    ucTBWI_Qty2.FocusWithDisable = true;
                                }
                            }
                            ucTBWI_Qty2.Focus();
                        }
                    }
                    else
                    {
                        notify = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                        notify.ShowDialog(this);
                        ucTBScanBarcode.Text = "";
                        ucTxtQty.Text = "";
                        ucTBScanBarcode.EnabledUC = true;
                        ucTBScanBarcode.Focus();
                        return;
                    }
                    ucTBScanBarcode.EnabledUC = true;
                }

                #endregion

                #region Edit
                else if ((string)btnEditItem.Tag == "enable")
                {
                    StoreResult result = process.searchItem(barcode, SearchItemAction.PriceAdjust);
                    if (result.response.next)
                    {
                        if (result.response == ResponseCode.Information)
                        {
                            notify = new frmNotify(result);
                            notify.ShowDialog(this);
                        }

                        ucTBScanBarcode.Text = "";
                        int rowCount = result.otherData.Rows.Count;
                        if (result.otherData != null)
                        {
                            if (rowCount == 1)
                            {
                                lbDiscID.Text = result.otherData.Rows[0]["DISCID"].ToString();
                                code = result.otherData.Rows[0]["ProductCode"].ToString();
                                name = result.otherData.Rows[0]["ProductName"].ToString();
                                double dbprice = double.Parse(result.otherData.Rows[0]["Price"].ToString());
                                price = dbprice.ToString(displayAmt);
                                double dbQuant = double.Parse(result.otherData.Rows[0]["Quant"].ToString());
                                quant = dbQuant.ToString();
                                seq = result.otherData.Rows[0]["Seq"].ToString();
                                lbPR_Type.Text = result.otherData.Rows[0]["PRODUCT_TYPE"].ToString();
                                //lbEditAmtTotal.Text = result.otherData.Rows[0]["AMT"].ToString();
                                lbIsFFNTRC.Text = lbPR_Type.Text == "RTC" || lbPR_Type.Text == "FF" ? "Y" : "N";
                                lbDeletedbPrice.Text = dbprice.ToString();
                                lbRecNo.Text = seq;
                                //test 8851959143018//0081824690905
                                panelEditItem.BringToFront();
                                currentPanel = CurrentPanelSale.PanelEditItem; //currentPanel = "panelEditItem";
                                lbTxtProductCode1.Text = code;
                                lbTxtDesc1.Text = name;
                                
                                ucTBWI_Qty1.Text = quant;                           
                                lbCurrentQty1.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentQty1"), quant);
                                ucTBWI_Price1.Text = price;                            
                                lbCurrentPrice1.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice1"), price, ProgramConfig.currencyDefault);

                                if (lbIsFFNTRC.Text == "Y" && lbPR_Type.Text == "RTC")
                                {
                                    ucTBWI_Qty1.Enabled = false;
                                    ucTBWI_Price1.Enabled = false;
                                }
                                else if (lbIsFFNTRC.Text == "Y" && lbPR_Type.Text == "FF")
                                {
                                    ucTBWI_Qty1.Enabled = false;
                                    ucTBWI_Price1.Enabled = true;
                                }
                                else
                                {
                                    ucTBWI_Price1.Enabled = true;
                                    ucTBWI_Qty1.Enabled = true;
                                    ucTBWI_Price1.Focus();
                                }                         
                            }
                            else
                            {
                                using (frmDeleteEditItem frm = new frmDeleteEditItem(result, "edit"))
                                {
                                    frm.ShowDialog(this);
                                    lbTxtProductCode1.Text = code;
                                    lbTxtDesc1.Text = name;
                                    lbDiscID.Text = discID;

                                    ucTBWI_Qty1.Text = quant;
                                    lbCurrentQty1.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentQty1"), quant);
                                    ucTBWI_Price1.Text = price;                                  
                                    lbCurrentPrice1.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice1"), price, ProgramConfig.currencyDefault);
                                    lbIsFFNTRC.Text = _IsFFNRTC;
                                    lbPR_Type.Text = _PrType;
                                    lbDeletedbPrice.Text = _UPCDB;

                                    if (lbIsFFNTRC.Text == "Y" && lbPR_Type.Text == "RTC")
                                    {
                                        ucTBWI_Qty1.Enabled = false;
                                        ucTBWI_Price1.Enabled = false;
                                    }
                                    else if (lbIsFFNTRC.Text == "Y" && lbPR_Type.Text == "FF")
                                    {
                                        ucTBWI_Qty1.Enabled = false;
                                        ucTBWI_Price1.Enabled = true;
                                    }
                                    else
                                    {
                                        ucTBWI_Price1.Enabled = true;
                                        ucTBWI_Qty1.Enabled = true;
                                        ucTBWI_Price1.Focus();
                                    }                                   
                                }
                            }
                        }
                    }
                    else
                    {
                        notify = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                        notify.ShowDialog(this);
                        ucTBScanBarcode.Text = "";
                        ucTxtQty.Text = "";
                        ucTBScanBarcode.EnabledUC = true;
                        ucTBScanBarcode.Focus();
                        return;
                    }
                    ucTBScanBarcode.EnabledUC = true;
                }

                #endregion

                #region CalDiscount
                else if (pn_item_sell.Controls.Count > 0 && barcode == "")
                {
                    //ucTBScanBarcode.EnabledUC = true;
                    ////CalDiscount
                    //double ind = 0;
                    //string discountDesc, saleAmt, discountAmt;
                    ////btnPayment.Enabled = true;
                    ////btnPayment.BackgroundImage = Properties.Resources.payment_enable;

                    ////เช็ค FunctionId ของDiscountอีกทีด้วย
                    //frmLoading.showLoading();
                    //ProcessResult result = process.calculateDiscount();
                    //frmLoading.closeLoading();
                    //if (result.response.next)
                    //{
                    //    panel_list_discount.Controls.Clear();
                    //    DataRow[] data = (DataRow[])result.data;
                    //    foreach (DataRow row in data)
                    //    {
                    //        discountDesc = row["ProductName"].ToString();
                    //        saleAmt = (double.Parse(row["DisplayAmt"].ToString())).ToString(displayAmt);
                    //        discountAmt = (double.Parse(row["AMT"].ToString())).ToString(displayAmt);

                    //        UCListDiscount ucDis = new UCListDiscount((int)row["REC"]);
                    //        ucDis.lbTxtName.Text = discountDesc;
                    //        ucDis.lbTxtPrice.Text = saleAmt;
                    //        ucDis.lbTxtAmt.Text = discountAmt;
                    //        ucDis.lbTxtAmt.Visible = true;
                    //        panel_discount.BringToFront();
                    //        panel_list_discount.Controls.Add(ucDis);

                    //        ind += double.Parse(row["AMT"].ToString());
                    //    }
                    //    lbTxtdiscount1.Text = ind.ToString(displayAmt);
                    //    lbTxtTotal.Text = (double.Parse(lbTxtSubtotal.Text) - ind).ToString(displayAmt);

                    //    frmMoCus.lbTxtDiscount.Text = lbTxtdiscount1.Text;
                    //    frmMoCus.lbTxtTotalCash.Text = lbTxtTotal.Text;
                    //}
                    //else
                    //{
                    //    notify = new frmNotify(result.response, result.responseMessage, result.helpMessage);
                    //    notify.ShowDialog(this);
                    //}
                    //DisableControl();

                    //btnPayment.Enabled = true;
                    //btnPayment.BackgroundImage = Properties.Resources.payment_enable;

                    //DisplayExchangeRate();
                }
                #endregion  

                else
                {
                    if (ucTxtQty.Text == "")
                    {
                        ucTxtQty.Text = "1";
                    }
                    qty = double.Parse(ucTxtQty.Text).ToString(displayAmt);

                    int discountType = 0;
                    double discountValue = 0f;
                    int.TryParse(ucDDDiscount.ValueText, out discountType);
                    double.TryParse(ucTxtAmountDiscount.Text, out discountValue);
                    //showLoadingProcess("scanSaleProduct", new object[] { ucTBScanBarcode.Text, double.Parse(qty), discountType, discountValue, ucTxtCouponNo.Text });
                    frmLoading.showLoading();
                    ProcessResult result = process.scanSaleProduct(barcode, double.Parse(qty), IsNoScanBarcode, discountType, discountValue, ucTxtCouponNo.Text,
                                                                    checkIME_Serial: () => CallPopupInput(), 
                                                                    CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h), 
                                                                    AlertMessage: (resCode, resMsg, resHelpMsg) => Utility.AlertMessage(this, resCode, resMsg, resHelpMsg),
                                                                    ShowAlertNoSale: (resCode, resMsg) => ShowAlertNoSale(resCode, resMsg));
                    
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
                            notify = new frmNotify(result);
                            notify.ShowDialog(this);
                        }
                    }

                    //if (ProgramConfig.memberId == "")
                    //{
                    //    ProgramConfig.memberId = "N/A";
                    //}

                    int cnt = 1;
                    
                   

                    //panel_list_suggest.AutoScroll = false;
                    //panel_list_suggest.HorizontalScroll.Enabled = true;
                    //panel_list_suggest.HorizontalScroll.Visible = true;

                    //panel_list_suggest.AutoScroll = true;

                    frm2Detail.panel_list.BringToFront();
                    frm2Detail.panel_list.Controls.Clear();
                    StoreResult result1 = process.getPromotionProduct(barcode, ProgramConfig.memberId);
                    if (result1.otherData != null)
                    {
                        if (result1.response == ResponseCode.Success)
                        {
                            if (result1.otherData.Rows.Count > 1)
                            {
                                panel_suggest.BringToFront();
                                panel_list_suggest.Controls.Clear();

                                for (int i = 0; i < result1.otherData.Rows.Count; i++)
                                {
                                    string desTxt = result1.otherData.Rows[i]["DescriptionMsg"].ToString();
                                    string customerTxt = result1.otherData.Rows[i]["Customer"].ToString();
                                    string memberTxt = result1.otherData.Rows[i]["Member"].ToString();

                                    UCSuggest ucSug = new UCSuggest(cnt);
                                    ucSug.lbNo.Text = cnt.ToString();
                                    ucSug.label1.Text = desTxt;
                                    ucSug.label2.Text = customerTxt;
                                    ucSug.label3.Text = memberTxt;
                                    panel_list_suggest.Controls.Add(ucSug);

                                    UCSuggest ucSug1 = new UCSuggest(cnt);
                                    ucSug1.lbNo.Text = cnt.ToString();
                                    ucSug1.label1.Text = desTxt;
                                    ucSug1.label2.Text = customerTxt;
                                    ucSug1.label3.Text = memberTxt;
                                    frm2Detail.panel_list.Controls.Add(ucSug1);
                                    cnt++;
                                }
                                RefreshSuggest();
                            }
                            else
                            {
                                frm2Detail.panel_list.Controls.Clear();
                                panel_suggest.SendToBack();
                            }
                        }
                        else
                        {
                            notify = new frmNotify(result);
                            notify.ShowDialog(this);
                        }
                    }

                    AppLog.writeLog("before DisableControl();");
                    DisableControl();

                    if (btnPayment.InvokeRequired)
                    {
                        btnPayment.BeginInvoke((MethodInvoker)delegate
                        {
                            btnPayment.Enabled = false;
                            btnConfirm.BringToFront();
                            //btnPayment.BackgroundImage = Properties.Resources.btn_payment_disable;
                        });
                    }
                    else
                    {
                        btnPayment.Enabled = false;
                        btnConfirm.BringToFront();
                        //btnPayment.BackgroundImage = Properties.Resources.btn_payment_disable;
                    }
                  
                    ProgramConfig.flagDiscount = false;
                    CheckItemSell();

                    DisplayExchangeRate();

                    if (ucTBScanBarcode.InvokeRequired)
                    {
                        ucTBScanBarcode.BeginInvoke((MethodInvoker)delegate
                        {
                            ucTBScanBarcode.Text = "";
                            ucTBScanBarcode.Focus();
                            ucTBScanBarcode.EnabledUC = true;
                        });
                    }
                    else
                    {
                        ucTBScanBarcode.Text = "";
                        ucTBScanBarcode.Focus();
                        ucTBScanBarcode.EnabledUC = true;
                    }
                }
                setFocus();
                AppLog.writeLog("after setFocus();");
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                RefreshfrmSale();
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                RefreshfrmSale();
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

        private Result_frmPopupInput CallPopupInput()
        {
            //Fix language
            frmPopupInput frmInp = new frmPopupInput((serial) => process.CheckIME_Serial(serial), "กรุณากรอก IME/Serial", "กรุณากรอก Reference 1", "กรุณากรอก Reference 2");
            frmInp.ShowDialog(this);

            return frmInp.result;
        }

        private void ShowAlertNoSale(ResponseCode resCode, string resMsg)
        {
            frmLoading.closeLoading();
            frmAlertMsgCustomer frm = new frmAlertMsgCustomer(resMsg);
            frm.Show();
            Utility.AlertMessage(resCode, resMsg);
            frm.Dispose();
        }


        private void DisplayExchangeRate()
        {
            AppLog.writeLog("after Properties.Resources.btn_payment_disable; ProgramConfig.flagDiscount = false;");
            StoreResult res = process.getAmountExchangeRate(lbTxtTotal.Text, "1", ProgramConfig.payment.getMainCurrency(), ProgramConfig.payment.getPaymentCode(ProgramConfig.payment.getMainCurrency()));
            frmLoading.closeLoading();
            if (res.response.next)
            {
                AppLog.writeLog("after saleProcess.getAmountExchangeRate keyProduct res.response.next = true");

                DataTable loadExchange = res.otherData;

                for (int i = 0; i < loadExchange.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        //frmMoCus.lbCurrencyRate1.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString() + " (" + loadExchange.Rows[i]["Rate"].ToString() + ")";
                        frmMoCus.lbCurrencyRate1.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString();
                        frmMoCus.lbAmtCurrency1.Text = loadExchange.Rows[i]["Total"].ToString();
                    }

                    if (i == 1)
                    {
                        //frmMoCus.lbCurrencyRate2.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString() +" (" + loadExchange.Rows[i]["Rate"].ToString() + ")";
                        frmMoCus.lbCurrencyRate2.Text = loadExchange.Rows[i]["PaymentSubCode"].ToString();
                        frmMoCus.lbAmtCurrency2.Text = loadExchange.Rows[i]["Total"].ToString();
                    }
                }
            }
            else
            {
                AppLog.writeLog("after saleProcess.getAmountExchangeRate keyProduct res.response.next = false");
                //AppLog.writeLog("Response Code = " + res.response.value +" Response Message = " + res.responseMessage + "");
                notify = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                notify.ShowDialog(this);
            }
        }

        private void CheckItemSell()
        {
            if (pn_item_sell.Controls.Count > 0)
            {
                btnOtherService.Visible = false;
                setVisibleButtonConfirm(true);
            }
            else if (pn_item_sell.Controls.Count == 0)
            {
                btnOtherService.Visible = true;
                btnOtherService.BringToFront();
                setVisibleButtonConfirm(false);
            }
        }

        private void RefreshfrmSale()
        {
            DisableControl();

            btnPayment.Enabled = false;
            btnConfirm.BringToFront();

            //btnConfirm.BackColor = Color.White;
            //btnPayment.BackgroundImage = Properties.Resources.btn_payment_disable;

            ucTBScanBarcode.Text = "";
            panelScanBarcodeBringToFront();
            ucTxtQty.Text = "";
            ucTBScanBarcode.Focus();

            ProgramConfig.flagDiscount = false;
            ucTBScanBarcode.EnabledUC = true;
        }
        
        public bool CatchNetWorkConnectionException(NetworkConnectionException net)
        {
            frmLoading.closeLoading();
            bool result = Program.control.RetryConnection(net.errorType);
            if (result)
            {
                if (fSaleProcess.InsertTempDLYPTransLocalPOS().response.next)
                {
                    process.deleteAllPayment(CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h));
                    ClearMember();

                    fSaleProcess.CheckRunningNumber(ProgramConfig.saleRefNoIni, ProgramConfig.voidRefNoIni, ProgramConfig.returnRefNoIni, ProgramConfig.cashInRefNoIni
                        , ProgramConfig.endOfShiftRefNoIni, ProgramConfig.expermitRefNoIni, ProgramConfig.openDayRefNoIni, ProgramConfig.posrepRefNoIni, ProgramConfig.actionRefNoIni, ProgramConfig.holdOrderRefNoIni, ProgramConfig.tempFFTINo);

                }
            }
            else
            {
                RefreshfrmSale();
                return false;
            }
            RefreshfrmSale();
            return true;
        }

        private void RefreshSuggest()
        {
            List<UCSuggest> lstItemSell = new List<UCSuggest>();
            lstItemSell = panel_list_suggest.Controls.Cast<UCSuggest>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panel_list_suggest.Controls.Clear();
            int num = lstItemSell.Count;

            foreach (UCSuggest item in lstItemSell)
            {
                if (num == 1)
                {
                    item.BackColor = Color.FromArgb(231, 175, 175);
                    item.label1.ForeColor = Color.White;
                    item.label1.Font = new Font(item.label1.Font, FontStyle.Bold);

                    item.label2.ForeColor = Color.White;
                    item.label2.Font = new Font(item.label2.Font, FontStyle.Bold);

                    item.label3.ForeColor = Color.White;
                }
                else if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(244, 221, 221);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num.ToString();
                panel_list_suggest.Controls.Add(item);
                num--;
            }
            ScrollToBottom(panel_list_suggest);
            panel_list_suggest.Refresh();

            List<UCSuggest> lstItemSell1 = new List<UCSuggest>();
            lstItemSell1 = frm2Detail.panel_list.Controls.Cast<UCSuggest>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            frm2Detail.panel_list.Controls.Clear();
            int num1 = lstItemSell1.Count;

            foreach (UCSuggest item in lstItemSell1)
            {
                if (num1 == 1)
                {
                    item.BackColor = Color.FromArgb(231, 175, 175);
                    item.label1.ForeColor = Color.White;
                    item.label1.Font = new Font(item.label1.Font, FontStyle.Bold);

                    item.label2.ForeColor = Color.White;
                    item.label2.Font = new Font(item.label2.Font, FontStyle.Bold);

                    item.label3.ForeColor = Color.White;
                }
                else if (num1 % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(244, 221, 221);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num1.ToString();
                frm2Detail.panel_list.Controls.Add(item);
                num1--;
            }
            ScrollToBottom(frm2Detail.panel_list);
            frm2Detail.panel_list.Refresh();
        }

        private void RefreshGrid2()
        {
            List<UCItemSell> lstItemSell = new List<UCItemSell>();
            //lstItemSell = pn_item_sell.Controls.Cast<UCItemSell>().ToList();
            lstItemSell = pn_item_sell.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            int maxnum = lstItemSell.Count;
            int num = maxnum;
            double a = 0;

            foreach (UCItemSell item in lstItemSell)
            {
                //int.TryParse(item.lbNoText, out num);
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
                //item.lbNoText = num.ToString();
                a += double.Parse(item.lbQtyText);
                //pn_item_sell.Controls.Add(item);
                num--;
            }          
            ScrollToBottom(pn_item_sell);
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
                //monitor2Item.lbNoText = number.ToString();
                //frmMoCus.pn_Item.Controls.Add(monitor2Item);
                number--;
            }
            ScrollToBottom(frmMoCus.pn_Item);
            frmMoCus.pn_Item.Refresh();
        }

        private void RefreshGrid()
        {
            List<UCItemSell> lstItemSell = new List<UCItemSell>();
            lstItemSell = pn_item_sell.Controls.Cast<UCItemSell>().ToList();
            //lstItemSell = pn_item_sell.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            //pn_item_sell.Controls.Clear();
            int maxnum = lstItemSell.Count;
            int cnt = 0;
            int num = 0;
            double a = 0;

            foreach (UCItemSell item in lstItemSell)
            {
                int.TryParse(item.lbNoText, out num);
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
                //item.lbNoText = num.ToString();
                a += double.Parse(item.lbQtyText);
                //pn_item_sell.Controls.Add(item);
                //cnt++;
            }
            ScrollToBottom(pn_item_sell);
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
                //monitor2Item.lbNoText = number.ToString();
                //frmMoCus.pn_Item.Controls.Add(monitor2Item);
                number--;
            }
            ScrollToBottom(frmMoCus.pn_Item);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void ucTBScanBarcode_TextBoxKeydown(object sender, EventArgs e)
        {
            keyProduct(false, ucTBScanBarcode.Text);
        }

        public void UCGridViewItemSellClick(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                DisableControl();

                //if (currentPanel != "panelPrintExport")
                //{
                //    currentPanel = "paneltemDetail";
                //}      
                ucGV = (UCItemSell)sender;

                if (CheckProductRule(ucGV.STV, ucGV.lbDiscID.Text, ucGV.lbProductCodeText))
                {
                    CheckPolicy();

                    recExport = ucGV.lbRecDB.Text;
                    lbRecNo.Text = ucGV.lbRecDB.Text;
                    lbDiscID.Text = ucGV.lbDiscID.Text;
                    lbIsFFNTRC.Text = ucGV.IsFreshFoodNRTC;
                    lbPR_Type.Text = ucGV.PR_TYPE;
                    lbDeletedbPrice.Text = ucGV.UPCPriceDB;
                    lbNo.Text = ucGV.lbNo.Text;
                    lbTxtProductCode3.Text = ucGV.lbUC_ProductCode.Text;
                    lbTxtDesc3.Text = ucGV.lbProductName.Text;
                    ucTBQty.InpTxt = ucGV.lbUC_Qty.Text;
                    ucTBQty.EnabledUC = false;
                    quant = ucGV.lbUC_Qty.Text;
                    ucTBPrice.Text = ucGV.lbUC_Price.Text;
                    //lbCurrentPrice.Text = "ราคาเดิม " + ucGV.lbPrice.Text + " " + ProgramConfig.currencyDefault;
                    lbCurrentPrice.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice"), ucGV.lbUC_Price.Text, ProgramConfig.currencyDefault);
                    price = ucGV.lbUC_Price.Text;
                    currentPrice = ucGV.lbUC_Price.Text;
                    ucTBDiscount.Text = ucGV.lbUC_Discount.Text;
                    currentDis = ucGV.lbUC_Discount.Text;
                    ucTBDiscount.Enabled = false;
                    //lbCurrentDiscount.Text = "ส่วนลดเดิม" +ucGV.lbDiscount.Text+ " " + ProgramConfig.currencyDefault;
                    lbCurrentDiscount.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentDiscount"), ucGV.lbUC_Discount.Text, ProgramConfig.currencyDefault);
                    lbTxtTotalPrice.Text = ucGV.lbUC_TotalPrice.Text;

                    frm2Detail.label1.Text = lbTxtDesc3.Text;
                    frm2Detail.label2.Text = ucTBPrice.Text;

                    if (ucGV.lbPromo.Text != "")
                    {
                        panel_list_promotion.Controls.Clear();
                        UCListDiscount ucDis = new UCListDiscount();
                        ucDis.lbTxtName.Text = ucGV.lbPromo.Text;
                        ucDis.lbTxtPrice.Text = ucGV.lbPromoPrice.Text;
                        ucDis.lbTxtAmt.Visible = false;
                        panel_promotion.BringToFront();
                        panel_list_promotion.Controls.Add(ucDis);
                    }
                    if (lastUCIS != ucGV)
                        UCItemSell.LostFocusItem(lastUCIS);

                    lastUCIS = ucGV;

                    int cnt1 = 1;
                    panel_list_suggest.Controls.Clear();
                    frm2Detail.panel_list.Controls.Clear();
                    StoreResult resPromo = process.getPromotionProduct(ucGV.lbUC_ProductCode.Text, ProgramConfig.memberId);
                    if (resPromo.otherData != null)
                    {
                        if (resPromo.response.next)
                        {
                            if (resPromo.response == ResponseCode.Information)
                            {
                                notify = new frmNotify(resPromo);
                                notify.ShowDialog(this);
                            }

                            if (resPromo.otherData.Rows.Count > 1)
                            {
                                for (int i = 0; i < resPromo.otherData.Rows.Count; i++)
                                {
                                    string desTxt = resPromo.otherData.Rows[i]["DescriptionMsg"].ToString();
                                    string customerTxt = resPromo.otherData.Rows[i]["Customer"].ToString();
                                    string memberTxt = resPromo.otherData.Rows[i]["Member"].ToString();

                                    UCSuggest ucSug = new UCSuggest(cnt1);
                                    ucSug.lbNo.Text = cnt1.ToString();
                                    ucSug.label1.Text = desTxt;
                                    ucSug.label2.Text = customerTxt;
                                    ucSug.label3.Text = memberTxt;
                                    panel_suggest.BringToFront();
                                    panel_list_suggest.Controls.Add(ucSug);

                                    UCSuggest ucSug1 = new UCSuggest(cnt1);
                                    ucSug1.lbNo.Text = cnt1.ToString();
                                    ucSug1.label1.Text = desTxt;
                                    ucSug1.label2.Text = customerTxt;
                                    ucSug1.label3.Text = memberTxt;
                                    frm2Detail.panel_list.Controls.Add(ucSug1);
                                    cnt1++;
                                }
                                RefreshSuggest();
                            }
                            else
                            {
                                frm2Detail.panel_list.Controls.Clear();
                                panel_suggest.SendToBack();
                            }
                        }
                        else
                        {
                            notify = new frmNotify(resPromo);
                            notify.ShowDialog(this);
                        }
                    }

                    ReadDataClickItem();
                    if (currentPanel != CurrentPanelSale.PanelPrintExport)
                    {
                        currentPanel = CurrentPanelSale.PaneltemDetail;
                    }
                }
                else
                {
                    ucTBScanBarcode.FocusTxt();
                }
                setFocus();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkConnectionException(net);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private bool CheckProductRule(string stv, string discID, string productCode)
        {
            string barcodetype = "";
            if (stv == "Z" || (stv == "N" && discID != "10004"))
            {
                barcodetype = "NOR";
            }
            else if (stv == "F")
            {
                barcodetype = "FF";
            }
            else if (stv == "N" && discID == "10004")
            {
                barcodetype = "RTC";
            }

            var result = fSaleProcess.CheckProducRule("BLKPRICECHANGE", productCode, barcodetype);

            return result.response.next;
        }

        private void ReadDataClickItem()
        {

            if (currentPanel == CurrentPanelSale.PanelPrintExport)     //if (currentPanel == "panelPrintExport")
            {
                ucKeypad.ucTBWI = null;
                string flag = process.getDataDTSaleMain(lbTxtProductCode3.Text, lbRecNo.Text, "PrintExport");
                if (flag == "N")
                {
                    pictureBox2.Tag = "N";
                    pictureBox2.Image = Properties.Resources.btn_PrintNoExportProduct;
                    lbNotPrint.BringToFront();
                }
                else
                {
                    pictureBox2.Tag = "Y";
                    pictureBox2.Image = Properties.Resources.btn_PrintExportProduct;
                    lbPrint.BringToFront();
                }
            }
        }

        private void CheckPolicy()
        {
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem);
            if (check.policy == PolicyStatus.Work)
            {
                lbDelete.Visible = true;
            }
            else if (check.policy == PolicyStatus.Skip)
            {
                lbDelete.Visible = false;
            }

            Profile check2 = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_EditPrice);
            if (check2.policy == PolicyStatus.Work)
            {
                if (ucGV.IsFreshFoodNRTC == "Y" && ucGV.PR_TYPE == "RTC")
                {
                    ucTBPrice.EnabledUC = false;
                }
                else
                {
                    ucTBPrice.EnabledUC = true;
                }
            }
            else if (check2.policy == PolicyStatus.Skip)
            {
                ucTBPrice.Enabled = false;
            }

            Profile check3 = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_AdditionalInput_SpecialProduct); //#125
            Profile check4 = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_AdditionalInput_TakeoutDocument); //#127
            if (check3.policy != PolicyStatus.Work && check4.policy != PolicyStatus.Work)
            {
                ucDDLMenuExtra.Visible = false;
            }
            else
            {
                ucDDLMenuExtra.Visible = true;
            }

        }
        
        private void ucDropDownList4_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList1();
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList1()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            drItem.DisplayText = "";
            drItem.ValueText = "";
            lstStr.Add(drItem);

            drItem.DisplayText = "";
            drItem.ValueText = "";
            lstStr.Add(drItem);

            return lstStr;
        }



        private void ucTBScanBarcode_EnterFromButton(object sender, EventArgs e)
        {
            keyProduct(true, ucTBScanBarcode.Text);
        }

        private void ucHeader1_ScannerClick(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
            setFocus();
        }

        private void ucTxtQty_EnterFromButton(object sender, EventArgs e)
        {
            if (ucTxtQty.Text != "" && double.Parse(ucTxtQty.Text) != 0)
            {
                string check = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Quantity_Limit.parameterCode);
                if (double.Parse(ucTxtQty.Text) > double.Parse(check))
                {
                    string alertMsg = ProgramConfig.message.get("frmSale", "QuantityInvalid").message;
                    string helpMsg = ProgramConfig.message.get("frmSale", "QuantityInvalid").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Error, alertMsg, String.Format(helpMsg, check));
                    dialog.ShowDialog(this);
                    ucTxtQty.Text = "";
                    ucTxtQty.Focus();
                    return;

                    //if (ProgramConfig.language == Language.THAI)
                    //{
                    //    notify = new frmNotify(ResponseCode.Error, "จำนวนสินค้าไม่ถูกต้อง.", "จำกัดจำนวนขายสินค้าต่อรายการสูงสุด ไม่เกิน " + check + " หน่วย");
                    //    notify.ShowDialog(this);
                    //    ucTxtQty.Text = "";
                    //    ucTxtQty.Focus();
                    //    return;
                    //}
                    //else if (ProgramConfig.language == Language.ENGLISH)
                    //{
                    //    notify = new frmNotify(ResponseCode.Error, "Invalid quantity of products.", "Limit the number of products sold per item, up to " + check + " Unit");
                    //    notify.ShowDialog(this);
                    //    ucTxtQty.Text = "";
                    //    ucTxtQty.Focus();
                    //    return;
                    //}
                    //else if (ProgramConfig.language == Language.LAOS)
                    //{
                    //    notify = new frmNotify(ResponseCode.Error, "ປະລິມານສິນຄ້າບໍ່ຖືກຕ້ອງ.", "ຈຳ ກັດ ຈຳ ນວນຜະລິດຕະພັນທີ່ຂາຍຕໍ່ສິນຄ້າ, ຂື້ນໄປ " + check + " ໜ່ວຍ");
                    //    notify.ShowDialog(this);
                    //    ucTxtQty.Text = "";
                    //    ucTxtQty.Focus();
                    //    return;
                    //}

                }
                else
                {
                    ucTBScanBarcode.Focus();
                }
            }
            else
            {
                DisableControl();
                string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ใส่จำนวนเป็น 0");
                notify.ShowDialog(this);
                return;
            }
        }

        private void btnGoodSales_Click(object sender, EventArgs e)
        {
            quntValue = ucTxtQty.Text;
            DisableButtons();
            var frmGoodSales = new frmFavoriteSale();
            DialogResult ret = frmGoodSales.ShowDialog(this);
            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                goodSales(productCode);
            }
            else
            {
                productCode = "";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                
                string eventName = "Sale";
                DialogResult resDialog = System.Windows.Forms.DialogResult.None;

                if (ucTBWI_Member.Text.Trim() != "")
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.Sale_Member_Search);
                    if (check.policy == PolicyStatus.Work)
                    {
                        frmSearchMemberAuto frm = new frmSearchMemberAuto(ucTBWI_Member.Text, eventName);
                        resDialog = frm.ShowDialog(this);
                    }

                    if (resDialog == System.Windows.Forms.DialogResult.Yes)
                    {
                        ucHeader1.nameText = memberName;
                        ucHeader1.nameVisible = true;
                        //e.Graphics.MeasureString(ucTBWI_Member.Text, SystemFonts.DefaultFont).Width);
                        Label newFont = new Label();
                        newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                        int checkWidth = TextRenderer.MeasureText(memberName, newFont.Font).Width;
                        //base {System.MarshalByRefObject} = {Name = "Microsoft Sans Serif" Size=8.25}
                        ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);
                    }
                    else if (resDialog == System.Windows.Forms.DialogResult.No)
                    {
                        ClearMember();
                        ucTBWI_Member.Text = "";
                        ucTBWI_Member.Focus();
                    }
                    else if (resDialog == System.Windows.Forms.DialogResult.Abort)
                    {
                        throw new NetworkConnectionException();
                    }
                }
                else
                {
                    ClearMember();
                    resDialog = System.Windows.Forms.DialogResult.Yes;
                }

                if (resDialog == System.Windows.Forms.DialogResult.Yes)
                {
                    DisableControl();
                    panelScanBarcode.BringToFront();
                    currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                    ucTBScanBarcode.Focus();
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkConnectionException(net);
            }
        }

        private void ClearMember()
        {
            ProgramConfig.memberId = "";
            ProgramConfig.memberName = "";
            ProgramConfig.memberCardNo = "";
            ProgramConfig.memberProfileMMFormat.Clear();
            ucTBWI_Member.Text = "";
            ucHeader1.nameText = "";
            ucHeader1.nameVisible = false;
            ucHeader1.pnNameSize = new Size(50, 43);
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            UCHeader uchead = (UCHeader)sender;
            uchead.pageTest = PageBackFormPayment.NormalSale;
            //if (pn_item_sell.Controls.Count == 0)
            //{
            //    frm2Detail.panel_message.BringToFront();
            //    Program.control.ShowForm("frmMainMenu");
            //    Program.control.CloseForm("frmSale");
            //}           
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownMenu()
        {
            //List<string> lstStr = new List<string>();
            //lstStr.Add("ยกเลิกระหว่างขาย");


            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();

            Profile check = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DiscountByProductManual);
            if (check.policy == PolicyStatus.Work)
            {
                if (ProgramConfig.language.ID == 1) //Eng
                {
                    drItem.DisplayText = "Product Discount";
                }
                else if (ProgramConfig.language.ID == 2) //Tha
                {
                    drItem.DisplayText = "ส่วนลดรายสินค้า";
                }
                else if (ProgramConfig.language.ID == 3) // Lao
                {
                    drItem.DisplayText = "ສ່ວນລົດສິນຄ້າ";
                }
                else
                {
                    drItem.DisplayText = "Product Discount";
                }
                drItem.ValueText = "1";
                lstStr.Add(drItem);
            }

            if (pn_item_sell.Controls.Count > 0)
            {
                if (ProgramConfig.language.ID == 1)
                {
                    drItem.DisplayText = "Cancel sales";
                }
                else if (ProgramConfig.language.ID == 2)
                {
                    drItem.DisplayText = "ยกเลิกการขาย";
                }
                else if (ProgramConfig.language.ID == 3)
                {
                    drItem.DisplayText = "ຍົກເລີກການຂາຍ";
                }
                else
                {
                    drItem.DisplayText = "Cancel sales";
                }
                drItem.ValueText = "2";
                lstStr.Add(drItem);
            }
            else
            {
                frm2Detail.panel_message.BringToFront();


                Program.control.ShowForm("frmMainMenu");
                Program.control.CloseForm("frmSale");
                //if (ProgramConfig.language.ID == 1)
                //{
                //    drItem.DisplayText = "Main Menu";
                //}
                //else if (ProgramConfig.language.ID == 2)
                //{
                //    drItem.DisplayText = "หน้าหลัก";
                //}
                //else if (ProgramConfig.language.ID == 3)
                //{
                //    drItem.DisplayText = "ໜ້າຫຼັກ";
                //}
                //drItem.ValueText = "3";
                //lstStr.Add(drItem);
            }
            
            return lstStr;
        }

        private void UCItemDropDownListClick(object sender, EventArgs e)
        {
            CancelSale(sender);
        }

        public void CancelSale(object sender)
        {
            try
            {
                string valueReturn = "";
                pn_drop_menu.Visible = false;
                var ucIDDL = (UCHamburgerItem)sender;
                //Fix Test
                #region Cancel Receipt
                if (ucIDDL.MenuID == MenuIdHamberger.CancelReceipt)
                {
                    if (pn_item_sell.Controls.Count == 0)
                    {
                        frm2Detail.panel_message.BringToFront();
                        Program.control.ShowForm("frmMainMenu");
                        Program.control.CloseForm("frmSale");
                    }
                    else
                    {
                        Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder);
                        if (check.profile == ProfileStatus.NotAuthorize)
                        {
                            AuthResult authRes = Utility.CheckAuthPassRes(this, check, "CancelSale");
                            if (!authRes.Next)
                            {
                                textLanguageChange();
                                picBtBack_Click(this, null);
                                return;
                            }
                            textLanguageChange();
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
                                    notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, double.Parse(valueReturn).ToString(displayAmt)));

                                    //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(valueReturn).ToString(displayAmt));
                                    notify.ShowDialog(this);
                                    pn_drop_menu.Visible = false;
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
                                        picBtBack_Click(this, null);
                                        textLanguageChange();
                                        return;
                                    }

                                    textLanguageChange();
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
                                        notify = new frmNotify(ResponseCode.Error, message, string.Format(help, double.Parse(checkAgain).ToString(displayAmt)));

                                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ยกเลิกระหว่างขายได้", "คุณมีสิทธิ์ยกเลิกระหว่างขายได้ไม่เกิน " + double.Parse(checkAgain).ToString(displayAmt));
                                        notify.ShowDialog(this);
                                        pn_drop_menu.Visible = false;
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
                #endregion  
                //else if (ucIDDL.lbMenuID.Text == "3")
                //{
                //    //pn_drop_menu.Visible = false;
                //    Program.control.ShowForm("frmMainMenu");
                //    foreach (Form item in Application.OpenForms)
                //    {
                //        if (item is frmMainMenu)
                //        {
                //            frmMain = (frmMainMenu)item;
                //            frmMain.BringToFront();
                //            break;
                //        }
                //    }
                //}
                else if (ucIDDL.MenuID == MenuIdHamberger.DisCountProductManual)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DiscountByProductManual);
                    if (check.policy == PolicyStatus.Skip)
                    {
                        //Fix Auth (Don't Fix)
                        frmUserAuthorize auth = new frmUserAuthorize("CancelSale", check.diffUserStatus);
                        auth.function = FunctionID.Sale_CancelWhileSale_CancelOrder;
                        DialogResult auth_res = auth.ShowDialog(this);
                        if (auth_res != DialogResult.Yes)
                        {
                            textLanguageChange();
                            return;
                        }

                        //if (!Utility.CheckAuthPass(this, check, "CancelSale"))
                        //{
                        //    textLanguageChange();
                        //    return;
                        //}

                        textLanguageChange();
                        //pn_drop_menu.Visible = false;
                        panel_product_discount.BringToFront();
                    }
                    else if (check.policy == PolicyStatus.Work)
                    {
                        panel_product_discount.BringToFront();
                    }
                }
                else if (ucIDDL.MenuID == MenuIdHamberger.Member)
                {
                    currentPanel = "Member";
                }
                else if (ucIDDL.MenuID == MenuIdHamberger.Employee)
                {
                    currentPanel = "Employee";
                }
                else if (ucIDDL.MenuID == MenuIdHamberger.PC_Man)
                {
                    currentPanel = "PCMan";
                }
                else if (ucIDDL.MenuID == MenuIdHamberger.LoadHolder)
                {
                    if (pn_item_sell.Controls.Count > 0)
                    {
                        //Fix language
                        Utility.AlertMessage(ResponseCode.Error, "กรุณาทำรายการขายนี้ให้เสร็จก่อน");
                    }
                    else
                    {
                        Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CheckHolderOrder);
                        if (check.policy == PolicyStatus.Work)
                        {
                            //Fix language
                            //if (Utility.CheckAuthPass(this, check, "Load Holder Order"))
                            //{                  
                            var res = process.CheckOnhold();
                            if (res.response.next)
                            {
                                UCSaveTemp uc = new UCSaveTemp(res.otherData);
                                uc.Location = new System.Drawing.Point(689, 43);
                                uc.Name = "UCSaveTemp";
                                uc.Size = new System.Drawing.Size(334, 723);
                                uc.BringToFront();
                                uc.ItemClick += UCSaveTemp_ItemClick;
                                uc.ConfirmClick += UCSaveTemp_ConfirmClick;
                                uc.BackClick += (s, e) =>
                                {
                                    panelMainSell.Enabled = true;
                                    if (ProgramConfig.memberId == "")
                                    {
                                        ClearValue();
                                        clickSearchMember(s, e);                                       
                                    }
                                    else
                                    {
                                        ClearValue(false);
                                        panelScanBarcode.BringToFront();
                                        currentPanel = CurrentPanelSale.PanelScanBarcode;
                                        ucTBScanBarcode.FocusTxt();
                                    }
                                };
                                this.Controls.Add(uc);
                                this.Controls.SetChildIndex(uc, 0);
                            }
                            else
                            {
                                Utility.AlertMessage(ResponseCode.Error, "ไม่พบรายการฝากขาย");
                            }
                            // }
                        }
                        else
                        {
                            // Fix Language
                            Utility.AlertMessage(ResponseCode.Information, "ไม่มีสิทธิเข้าใช้งานเมนูนี้");
                        }
                    }
                }

                else if (ucIDDL.MenuID == MenuIdHamberger.HolderOrder)
                {
                    Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_HoldOrder);
                    if (check.policy == PolicyStatus.Work)
                    {
                        if (pn_item_sell.Controls.Count > 0)
                        {
                            //Fix language
                            //if (Utility.CheckAuthPass(this, check, "Save Holder Order"))
                            //{
                            var res = process.SaveHoldOrder();
                            if (res.response.next)
                            {
                                printCancel(FunctionID.Sale_CancelWhileSale_HoldOrder_PrintDocument, false);
                                check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_HoldOrder_SaveHoldTransaction_SynchSaleTransactiontoDataTank);
                                if (check.policy == PolicyStatus.Work) //2
                                {
                                    process.syncToDataTank("Cancel", FunctionID.Sale_CancelWhileSale_HoldOrder_SaveHoldTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.saleRefNo, "1");
                                }
                            }
                            else
                            {
                                Utility.AlertMessage(res);
                            }
                            //}
                        }
                        else
                        {
                            Utility.AlertMessage(ResponseCode.Information, "ไม่มีรายการบันทึก");
                        }
                    }
                    else
                    {
                        // Fix Language
                        Utility.AlertMessage(ResponseCode.Information, "ไม่มีสิทธิเข้าใช้งานเมนูนี้");
                    }
                }
                else if (ucIDDL.MenuID == MenuIdHamberger.OrderType)
                {
                    //To Do Check Policy
                    DataTable dt = process.selectSALEORDERTYPE_TRANS();
                    if (dt.Rows.Count > 0)
                    {
                        //Fix language
                        frmNotify notify = new frmNotify(ResponseCode.Warning, String.Format("ข้อมูลที่บันทึกไว้\n{0}\n{1}\n{2}ต้องการบันทึกข้อมูลใหม่หรือไม่"
                                , saleTypeCode.OrderTypeDesc != "" && saleTypeCode.OrderTypeDesc != null
                                                ? String.Format("วิธีการสั่งสินค้า {0}", saleTypeCode.OrderTypeDesc) : ""
                                , saleTypeCode.DeliveryTypeDesc != "" && saleTypeCode.DeliveryTypeDesc != null
                                                ? String.Format("วิธีการรับสินค้า {0}", saleTypeCode.DeliveryTypeDesc) : ""
                                , saleTypeCode.MediaTypeDesc != "" && saleTypeCode.MediaTypeDesc != null
                                                ? String.Format("Media {0}", saleTypeCode.MediaTypeDesc) : ""));
                        var dialogRes = notify.ShowDialog();
                        if (dialogRes != System.Windows.Forms.DialogResult.Yes)
                        {
                            return;
                        }

                        var res = fSaleProcess.deleteSALEORDERTYPE_TRANS();
                        if (!res.response.next)
                        {
                            return;
                        }
                    }

                    frmSaleOrderType fSaleOrderType = new frmSaleOrderType(dtSaleOrderMenu);
                    fSaleOrderType.ShowDialog(this);

                }
            }
            catch (NetworkConnectionException net)
            {
                //setFocus();
                frmLoading.closeLoading();
                //Program.control.RetryConnection(net.errorType);
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                setFocus();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void OrderTypeProcess(bool isClickfromHamberger)
        {
            //#642
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_SaleOrderType);
            if (check.policy == PolicyStatus.Work)
            {
                DataTable dt = process.selectSALEORDERTYPE_TRANS();
                if (dt.Rows.Count == 0)
                {
                    //Fix language
                    frmNotify notify = new frmNotify(ResponseCode.Warning, String.Format("ยังไม่ได้ป้อนข้อมูลประเภทการขาย\nต้องการบันทึกข้อมูลใหม่หรือไม่"));
                    var dialogRes = notify.ShowDialog();
                    if (dialogRes != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }

                    frmSaleOrderType fSaleOrderType = new frmSaleOrderType(dtSaleOrderMenu);
                    fSaleOrderType.ShowDialog(this);
                }
            }
        }

        private void UCSaveTemp_ItemClick(object sender, EventArgs e)
        {
            var itm = (UCListItemSaveTempSale)sender;
            BaseProcess.clearDataTable();
            DataRow [] row = process.GetDisplayOnhold(itm.lbRefVal.Text);
            loadTempDLYPTRANS(row);
            //pn_item_sell.Enabled = false;'
            panelMainSell.Enabled = false;
        }

        private void UCSaveTemp_ConfirmClick(object sender, EventArgs e)
        {
            var itm = (UCListItemSaveTempSale)sender; 
            //pn_item_sell.Enabled = true;
            panelMainSell.Enabled = true;
            
            BaseProcess.clearDataTable();
            amtPrice = 0;
            pn_item_sell.Controls.Clear();
            var res = process.GetOnhold(itm.lbRefVal.Text);
            if (res.response.next)
            {
                ProgramConfig.loadHoldOrderReceipt = itm.lbRefVal.Text;

                //Product
                frmLoading.showLoading();
                foreach (DataRow dr in res.otherData.Rows)
                {

                    ucTxtQty.Text = dr["Qnt"].ToString();

                    Regex re = new Regex(@"(\d+)\s*([a-zA-Z]+)*");
                    Match result = re.Match(dr["ProductCode"].ToString().Trim());
                    string alphaPart = result.Groups[1].ToString();
                    string numberPart = result.Groups[2].ToString();

                    keyProduct(false, alphaPart);

                    if (!String.IsNullOrEmpty(numberPart))
                    {
                        int rec = process.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo);
                        StoreResult resUD = process.updatePCDSymbolTempDlyptrans(alphaPart, rec.ToString(), numberPart);
                    }
                    

                }

                loadTempDLYPTRANS();
                //Member
                string memID = res.otherData.Rows[0]["CustID"].ToString();
                if (memID != "N/A" && memID.Trim() != "")
                {
                    var resMem = process.getMemberProfile(memID);
                    if (resMem.response.next)
                    {

                        string memName = ProgramConfig.memberFormat == MemberFormat.MegaMaket ? 
                                                    resMem.otherData.Rows[0]["CardHolderName"].ToString() : resMem.otherData.Rows[0]["TNAME"].ToString();
                        string memCardNo = ProgramConfig.memberFormat == MemberFormat.MegaMaket ?
                                                    resMem.otherData.Rows[0]["CardHolder_No"].ToString() : resMem.otherData.Rows[0]["CardNumber"].ToString();
                        string memInpID = ProgramConfig.memberFormat == MemberFormat.MegaMaket ?
                                                    resMem.otherData.Rows[0]["CustID"].ToString() : memID;
                        frmSearchMemberData(memInpID, memName, memCardNo);

                        ucHeader1.nameText = memName;
                        ucHeader1.nameVisible = true;
                        Label newFont = new Label();
                        newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                        int checkWidth = TextRenderer.MeasureText(memName, newFont.Font).Width;
                        ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);

                        ucHeader1.VisiblePanelMember = false;
                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        ucTBScanBarcode.FocusTxt();
                    }
                }
              
                //Emp
                string empID = res.otherData.Rows[0]["EmpID"].ToString();
                if (String.IsNullOrEmpty(empID))
                {
                    var resEmp = process.CheckEmployee(empID);
                    if (resEmp.response.next)
                    {
                        ProgramConfig.employeeID = empID;
                    }
                }
            }
            frmLoading.closeLoading();
        }

        public void SetSaleTypeCode(Sale_TypeCode saleTypeCode)
        {
            this.saleTypeCode = saleTypeCode;
        }

        public void reasonToCancel()
        {
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_CheckLimit);
            if (check.policy == PolicyStatus.Skip) //1
            {
                StoreResult resSaveCancelTran = process.saveCancelSaleTransaction(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction, "0");
                Profile checkPro = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank);

                if (resSaveCancelTran.response.next)
                {
                    printCancel(FunctionID.Sale_CancelWhileSale_CancelOrder_PrintCancelDocument, true);
                    if (checkPro.policy == PolicyStatus.Work) //2
                    {
                        process.syncToDataTank("Cancel", FunctionID.Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.saleRefNo, "1");
                    }
                } 
                else 
                {                  
                    notify = new frmNotify(ResponseCode.Error, resSaveCancelTran.responseMessage, resSaveCancelTran.helpMessage);
                    notify.ShowDialog(this);
                    return;
                }
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                frmDeleteReason dialog = new frmDeleteReason();
                DialogResult auth_res = dialog.ShowDialog(this);
                if (auth_res != DialogResult.Yes)
                {
                    picBtBack_Click(this, null);
                    return;
                }
                ucTBScanBarcode.Focus();
            }
        }

        private void printCancel(FunctionID function, bool IsExit)
        {
            Profile check = ProgramConfig.getProfile(function);
            if (check.policy == PolicyStatus.Skip) //1
            {
                if (IsExit)
                {
                    closeForm();
                }
                else
                {
                    ClearFormCancel();
                }
            }
            else if (check.policy == PolicyStatus.Work) //2
            {
                StoreResult result = process.printCancel(function);
                if (!result.response.next)
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                    return;
                }
                else if (result.response == ResponseCode.Information)
                {
                    notify = new frmNotify(result);
                    notify.ShowDialog(this);
                }

                DataTable dt = result.otherData;
                Hardware.printTermal(dt);

                if (IsExit)
                {
                    closeForm();
                }
                else
                {
                    ClearFormCancel();
                }
            }
        }

        private void ClearFormCancel()
        {
            AppLog.writeLog("ClearFormCancel");
            Program.control.CloseForm("frmPayment");
            Program.control.CloseForm("frmDeleteReason");

            Form form = Application.OpenForms["frmMonitorCustomer"];
            frmMonitorCustomer mon = form as frmMonitorCustomer;
            mon.clearForm();

            Form form2 = Application.OpenForms["frmMonitor2Detail"];
            frmMonitor2Detail mon2 = form2 as frmMonitor2Detail;
            mon2.clearForm();

            ProgramConfig.salePageState = 2;
            this.afterNotify = false;
            frmSale_Activated(null, null);
        }

        private void closeForm()
        {
            this.Dispose();
            Program.control.ShowForm("frmMainMenu");
        }

        private void lbDelete_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                deleteType = "Single";
                string check = ProgramConfig.cashireAuthorizeResult.otherData.Rows[0]["MaxDeleteItemAmt"].ToString();

                if (ucTBPrice.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    return;
                }

                if ((double.Parse(ucTBPrice.Text) * double.Parse(ucTBQty.Text)) <= double.Parse(check))
                {
                    Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                    if (checkReason.policy == PolicyStatus.Skip)
                    {
                        frmLoading.showLoading();
                        ProcessResult res = process.saveDeleteItem(lbTxtProductCode3.Text, ucTBQty.Text, lbTxtTotalPrice.Text);
                        frmLoading.closeLoading();
                        if (res.response.next)
                        {
                            pn_item_sell.Controls.Remove(ucGV);
                            amtPrice -= double.Parse(lbTxtTotalPrice.Text);
                            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                            lbTxtTotal.Text = amtPrice.ToString(displayAmt);
                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            DisableControl();
                            RefreshGrid();
                            ucTBScanBarcode.Focus();
                        }
                        else
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                    else if (checkReason.policy == PolicyStatus.Work)
                    {
                        StoreResult result = process.displayDeleteItemReason();
                        //string resPrice = lbDiscID.Text.Trim() == "10004" ? ucTBPrice.Text.Trim() : (Convert.ToDouble(ucTBPrice.Text.Trim()) * Convert.ToDouble(ucTBQty.Text)).ToString();
                        string resPrice = lbIsFFNTRC.Text == "Y" ? lbTxtTotalPrice.Text.Trim() : (Convert.ToDouble(ucTBPrice.Text.Trim()) * Convert.ToDouble(ucTBQty.Text)).ToString();
                        frmDeleteItemReason delete = new frmDeleteItemReason(lbTxtProductCode3.Text, ucTBQty.Text, ucTBQty.Text, resPrice, deleteType, lbDiscID.Text);
                        delete.ShowDialog(this);

                        pn_item_sell.Controls.Remove(ucGV);
                        amtPrice -= double.Parse(lbTxtTotalPrice.Text);
                        lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                        lbTxtTotal.Text = amtPrice.ToString(displayAmt);
                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        loadTempDLYPTRANS();
                        //DisableControl();
                        //RefreshGrid();
                        ucTBScanBarcode.Focus();
                    }
                }
                else
                {
                    Profile checkLimit = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem);
                    if (checkLimit.policy == PolicyStatus.Skip)
                    {
                        string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").message;
                        string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").help;
                        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage + " " + double.Parse(check).ToString(displayAmt));

                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ลบรายการสินค้านี้!!!", "คุณสามารถลบรายการสินค้าไม่เกิน " + double.Parse(check).ToString(displayAmt));
                        notify.ShowDialog(this);
                        DisableControl();
                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        ucTBScanBarcode.Focus();
                    }
                    else if (checkLimit.policy == PolicyStatus.Work)
                    {
                        string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").message;
                        string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").help;
                        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage + " " + double.Parse(check).ToString(displayAmt));

                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ลบรายการสินค้านี้!!!", "คุณสามารถลบรายการสินค้าไม่เกิน " + double.Parse(check).ToString(displayAmt));
                        notify.ShowDialog(this);

                        //Fix Auth (Done)
                        //frmUserAuthorize auth = new frmUserAuthorize("DeleteItem", checkLimit.diffUserStatus);
                        //auth.function = FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem;
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    textLanguageChange();
                        //    setFocus();
                        //    return;
                        //}

                        //Fix Profile
                        //checkLimit.profile = ProfileStatus.NotAuthorize;
                        if (!Utility.CheckAuthPass(this, checkLimit, "DeleteItem"))
                        {
                            textLanguageChange();
                            setFocus();
                            return;
                        }

                        textLanguageChange();

                        string checkAgain = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxDeleteItemAmt"].ToString();
                        if ((double.Parse(ucTBPrice.Text) * double.Parse(ucTBQty.Text)) <= double.Parse(checkAgain))
                        {
                            Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                            if (checkReason.policy == PolicyStatus.Skip)
                            {
                                frmLoading.showLoading();
                                ProcessResult res = process.saveDeleteItem(lbTxtProductCode3.Text, ucTBQty.Text, ucTBPrice.Text);
                                frmLoading.closeLoading();
                                if (res.response.next)
                                {

                                    pn_item_sell.Controls.Remove(ucGV);
                                    amtPrice -= double.Parse(lbTxtTotalPrice.Text);
                                    lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                                    lbTxtTotal.Text = amtPrice.ToString(displayAmt);
                                    panelScanBarcode.BringToFront();
                                    currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                                    DisableControl();
                                    RefreshGrid();
                                    ucTBScanBarcode.Focus();
                                }
                                else
                                {
                                    notify = new frmNotify(res);
                                    notify.ShowDialog(this);
                                    return;
                                }
                            }
                            else if (checkReason.policy == PolicyStatus.Work)
                            {
                                StoreResult result = process.displayDeleteItemReason();
                                string resPrice = lbIsFFNTRC.Text == "Y" ? lbTxtTotalPrice.Text.Trim() : (Convert.ToDouble(ucTBPrice.Text.Trim()) * Convert.ToDouble(ucTBQty.Text)).ToString();
                                frmDeleteItemReason delete = new frmDeleteItemReason(lbTxtProductCode3.Text, ucTBQty.Text, ucTBQty.Text, resPrice, deleteType, lbDiscID.Text);
                                delete.ShowDialog(this);

                                pn_item_sell.Controls.Remove(ucGV);
                                amtPrice -= double.Parse(lbTxtTotalPrice.Text);
                                lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                                lbTxtTotal.Text = amtPrice.ToString(displayAmt);
                                panelScanBarcode.BringToFront();
                                currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                                loadTempDLYPTRANS();
                                //DisableControl();
                                //RefreshGrid();
                                ucTBScanBarcode.Focus();
                            }
                        }
                        else
                        {
                            string message = ProgramConfig.message.get("frmSale", "NotAllowToDelete").message;
                            string help = ProgramConfig.message.get("frmSale", "NotAllowToDelete").help;
                            notify = new frmNotify(ResponseCode.Error, message, help + " " + double.Parse(checkAgain).ToString(displayAmt));

                            //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ลบรายการสินค้านี้!!!", "คุณสามารถลบรายการสินค้าไม่เกิน " + double.Parse(checkAgain).ToString(displayAmt));
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                }
                frmLoading.closeLoading();
                setFocus();
            }
            catch (NetworkConnectionException net)
            {
                //setFocus();
                frmLoading.closeLoading();
                //Program.control.RetryConnection(net.errorType);
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                setFocus();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void scanDeleteItem()
        {
            frmLoading.showLoading();
            try
            {
                deleteType = "All";
                string check = ProgramConfig.cashireAuthorizeResult.otherData.Rows[0]["MaxDeleteItemAmt"].ToString();
                //string check = "999999";

                if (ucTBWI_Qty2.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    return;
                }

                if (ucTBWI_Price2.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    return;
                }

                if (double.Parse(ucTBWI_Qty2.Text) > double.Parse(quant))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "QtyMoreThan").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "QtyMoreThan").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "จำนวนมากกว่าที่มีอยู่.");
                    notify.ShowDialog(this);
                    return;
                }
                else if (double.Parse(ucTBWI_Qty2.Text) <= 0)
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ใส่จำนวนเป็น 0");
                    notify.ShowDialog(this);
                    return;
                }

                if ((double.Parse(ucTBWI_Price2.Text) * double.Parse(ucTBWI_Qty2.Text)) <= double.Parse(check))
                {
                    Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                    if (checkReason.policy == PolicyStatus.Skip)
                    {
                        frmLoading.showLoading();
                        ProcessResult res = process.saveDeleteItem(lbTxtProductCode2.Text, ucTBWI_Qty2.Text, ucTBWI_Price2.Text);
                        if (res.response.next)
                        {
                            amtPrice -= double.Parse(price);
                            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                            lbTxtTotal.Text = amtPrice.ToString(displayAmt);
                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            //DisableControl();
                            loadTempDLYPTRANS();
                            ucTBScanBarcode.Focus();
                            //RefreshGrid();
                        }
                        else
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                    else if (checkReason.policy == PolicyStatus.Work)
                    {
                        StoreResult result = process.displayDeleteItemReason();
                        //string resPrice = lbIsFFNTRC.Text == "Y" ? lbDeleteAMT.Text : ucTBWI_Price2.Text.Trim();
                        frmDeleteItemReason delete = new frmDeleteItemReason(lbTxtProductCode2.Text, quant, ucTBWI_Qty2.Text, lbDeletedbPrice.Text.Trim(), deleteType, lbDiscID.Text);
                        delete.ShowDialog(this);

                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        //DisableControl();
                        loadTempDLYPTRANS();
                        ucTBScanBarcode.Focus();
                        //RefreshGrid(); ;

                    }
                }
                else if ((double.Parse(ucTBWI_Price2.Text) * double.Parse(ucTBWI_Qty2.Text)) > double.Parse(check))
                {
                    Profile checkLimit = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem);
                    if (checkLimit.policy == PolicyStatus.Skip)
                    {
                        string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").message;
                        string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").help;
                        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage + " " + double.Parse(check).ToString(displayAmt));

                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ลบรายการสินค้านี้!!!", "คุณสามารถลบรายการสินค้าไม่เกิน " + double.Parse(check).ToString(displayAmt));
                        notify.ShowDialog(this);
                        DisableControl();
                        panelScanBarcode.BringToFront();
                        //currentPanel = "panelScanBarcode";
                        currentPanel = CurrentPanelSale.PanelScanBarcode;
                        ucTBScanBarcode.Focus();
                    }
                    else if (checkLimit.policy == PolicyStatus.Work)
                    {
                        string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").message;
                        string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowToDelete").help;
                        notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage + " " + double.Parse(check).ToString(displayAmt));

                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ลบรายการสินค้านี้!!!", "คุณสามารถลบรายการสินค้าไม่เกิน " + double.Parse(check).ToString(displayAmt));
                        notify.ShowDialog(this);

                        //Fix Auth (Done)
                        //frmUserAuthorize auth = new frmUserAuthorize("DeleteItem", checkLimit.diffUserStatus);
                        //auth.function = FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem;
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    textLanguageChange();
                        //    setFocus();
                        //    return;
                        //}

                        if (!Utility.CheckAuthPass(this, checkLimit, "DeleteItem"))
                        {
                            textLanguageChange();
                            setFocus();
                            return;
                        }

                        textLanguageChange();

                        string checkAgain = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxDeleteItemAmt"].ToString();
                        if ((double.Parse(ucTBWI_Price2.Text) * double.Parse(ucTBWI_Qty2.Text)) <= double.Parse(checkAgain))
                        {
                            Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                            if (checkReason.policy == PolicyStatus.Skip)
                            {
                                DisableControl();
                                panelScanBarcode.BringToFront();
                                currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
                                RefreshGrid();
                                ucTBScanBarcode.Focus();

                            }
                            else if (checkReason.policy == PolicyStatus.Work)
                            {
                                StoreResult result = process.displayDeleteItemReason();
                                //string resPrice = lbDiscID.Text.Trim() == "10004" ? ucTBWI_Price2.Text.Trim() : (Convert.ToDouble(ucTBWI_Price2.Text.Trim()) * Convert.ToDouble(quant)).ToString();
                                frmDeleteItemReason delete = new frmDeleteItemReason(lbTxtProductCode2.Text, quant, ucTBWI_Qty2.Text, lbDeletedbPrice.Text.Trim(), deleteType, lbDiscID.Text);
                                delete.ShowDialog(this);
                                panelScanBarcode.BringToFront();
                                currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                                //DisableControl();
                                loadTempDLYPTRANS();
                                //RefreshGrid();
                                ucTBScanBarcode.Focus();
                            }
                        }
                        else if ((double.Parse(ucTBWI_Price2.Text) * double.Parse(ucTBWI_Qty2.Text)) > double.Parse(checkAgain))
                        {
                            string message = ProgramConfig.message.get("frmSale", "NotAllowToDelete").message;
                            string help = ProgramConfig.message.get("frmSale", "NotAllowToDelete").help;
                            notify = new frmNotify(ResponseCode.Error, message, help + " " + double.Parse(checkAgain).ToString(displayAmt));

                            //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้ลบรายการสินค้านี้!!!", "คุณสามารถลบรายการสินค้าไม่เกิน " + double.Parse(checkAgain).ToString(displayAmt));
                            notify.ShowDialog(this);
                            return;

                            //frmUserAuthorize auth2 = new frmUserAuthorize();
                            //auth2.function = FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem;
                            //DialogResult auth2_res = auth.ShowDialog(this);
                            //if (auth_res != DialogResult.Yes)
                            //{
                            //    frmNotify dialog = new frmNotify(ResponseCode.Error, "No Authorize.");
                            //    dialog.ShowDialog(this);
                            //    return;
                            //}
                            //string checkAgain2 = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxDeleteItemAmt"].ToString();
                            //if (double.Parse(ucTBWI_Price2.Text) <= double.Parse(checkAgain2))
                            //{
                            //    Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                            //    if (checkReason.policy == PolicyStatus.Skip)
                            //    {
                            //        DisableControl();
                            //        ucTBScanBarcode.Focus();
                            //        panelScanBarcode.BringToFront();
                            //        RefreshGrid();

                            //    }
                            //    else if (checkReason.policy == PolicyStatus.Work)
                            //    {
                            //        StoreResult result = process.displayDeleteItemReason();
                            //        frmDeleteItemReason delete = new frmDeleteItemReason(lbTxtProductCode2.Text, quant, ucTBWI_Qty2.Text, ucTBWI_Price2.Text, deleteType);
                            //        delete.ShowDialog(this);

                            //        panelScanBarcode.BringToFront();
                            //        ucTBScanBarcode.Focus();
                            //        //DisableControl();
                            //        loadTempDLYPTRANS();
                            //        //RefreshGrid(); ;
                            //        cnt++;
                            //    }
                            //}
                        }
                    }
                }
                frmLoading.closeLoading();
                setFocus();
                //ucTBScanBarcode.Text = "";
            }
            catch (NetworkConnectionException net)
            {
                //setFocus();
                frmLoading.closeLoading();
                //Program.control.RetryConnection(net.errorType);
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                setFocus();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void clickerEditItem()
        {
            frmLoading.showLoading();
            try
            {
                editType = "Single";

                if (ucTBPrice.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    return;
                }

                if (double.Parse(ucTBPrice.Text) <= 0)
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    return;
                }

                if (double.Parse(ucTBPrice.Text) == double.Parse(price))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NoPriceChange").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NoPriceChange").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่มีการเปลี่ยนแปลงราคา.");
                    notify.ShowDialog(this);
                    return;
                }

                if (ucTBQty.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    return;
                }

                if (double.Parse(ucTBQty.Text) > double.Parse(quant))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "QtyMoreThan").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "QtyMoreThan").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "จำนวนมากกว่าที่มีอยู่.");
                    notify.ShowDialog(this);
                    return;
                }
                else if (double.Parse(ucTBQty.Text) <= 0)
                {
                    string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowNullAndBelow0").message;
                    string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowNullAndBelow0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "จำนวนน้อยกว่าที่มีอยู่.");
                    notify.ShowDialog(this);
                    return;
                }

                //Check Adjuct Price item Max
                
                string chkAdjust = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_AdjustPrice_ItemMax.parameterCode);
                if ((Math.Abs((double.Parse(ucTBPrice.Text) - double.Parse(price))) * 100) / Convert.ToDouble(price) > Convert.ToDouble(chkAdjust))
                {
                    string responseMessage = String.Format(ProgramConfig.message.get("frmSale", "NotAllowAdjustLessThanPercent").message, 100 - Convert.ToDouble(chkAdjust));
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowAdjustLessThanPercent").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                    notify.ShowDialog(this);
                    return;
                }

                string check = ProgramConfig.cashireAuthorizeResult.otherData.Rows[0]["MaxPriceChange"].ToString();
                double diff = Math.Abs((double.Parse(ucTBPrice.Text) - double.Parse(price)) * double.Parse(quant));
                if (diff <= double.Parse(check))
                {
                    Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                    if (checkReason.policy == PolicyStatus.Skip)
                    {
                        frmLoading.showLoading();
                        StoreResult res = process.saveEditItem(lbTxtProductCode3.Text, quant, ucTBQty.Text, currentPrice, ucTBPrice.Text, lbRecNo.Text);
                        frmLoading.closeLoading();
                        if (res.response.next)
                        {
                            _totalPriceChange = _totalPriceChange + (Math.Abs((double.Parse(ucTBPrice.Text) - double.Parse(price)) * double.Parse(quant)));
                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            loadTempDLYPTRANS();
                            ucTBScanBarcode.Focus();
                        }
                        else
                        {
                            notify = new frmNotify(res);
                            notify.ShowDialog(this);
                            return;
                        }
                    }
                    else if (checkReason.policy == PolicyStatus.Work)
                    {
                        StoreResult result = process.displayDeleteItemReason();
                        frmEditItemReason delete = new frmEditItemReason(lbTxtProductCode3.Text, ucTBQty.Text, ucTBQty.Text, ucTBPrice.Text, currentPrice, editType, lbDiscID.Text, lbRecNo.Text, lbIsFFNTRC.Text, lbPR_Type.Text, lbTxtTotalPrice.Text);
                        delete.ShowDialog(this);

                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        loadTempDLYPTRANS();
                        ucTBScanBarcode.Focus();
                    }
                    //ucTBScanBarcode.Focus();
                }
                else
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowToEdit").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowToEdit").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage + " " + double.Parse(check).ToString(displayAmt));

                    //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้แก้ไขรายการสินค้านี้!!!", "คุณสามารถแก้ไขราคาสินค้าไม่เกิน " + double.Parse(check).ToString(displayAmt));
                    notify.ShowDialog(this);

                    //Fix Auth (Done)
                    //frmUserAuthorize auth = new frmUserAuthorize("EditItem", "2");
                    //auth.function = FunctionID.NoFunctionID;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    textLanguageChange();
                    //    setFocus();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.NoFunctionID }, "EditItem"))
                    {
                        textLanguageChange();
                        setFocus();
                        return;
                    }

                    textLanguageChange();

                    string checkAgian = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxPriceChange"].ToString();
                    if (diff <= double.Parse(checkAgian))
                    {
                        Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                        if (checkReason.policy == PolicyStatus.Skip)
                        {
                            frmLoading.showLoading();
                            StoreResult res = process.saveEditItem(lbTxtProductCode3.Text, quant, ucTBQty.Text, currentPrice, ucTBPrice.Text, lbRecNo.Text);
                            frmLoading.closeLoading();
                            if (res.response.next)
                            {
                                _totalPriceChange = _totalPriceChange + (Math.Abs((double.Parse(ucTBPrice.Text) - double.Parse(price)) * double.Parse(quant)));
                                panelScanBarcode.BringToFront();
                                currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                                loadTempDLYPTRANS();
                                ucTBScanBarcode.Focus();
                                //DisableControl();
                                //RefreshGrid();
                            }
                            else
                            {
                                notify = new frmNotify(res);
                                notify.ShowDialog(this);
                                return;
                            }
                        }
                        else if (checkReason.policy == PolicyStatus.Work)
                        {
                            frmLoading.showLoading();
                            StoreResult result = process.displayDeleteItemReason();
                            frmLoading.closeLoading();
                            frmEditItemReason delete = new frmEditItemReason(lbTxtProductCode3.Text, ucTBQty.Text, ucTBQty.Text, ucTBPrice.Text, currentPrice, editType, lbDiscID.Text, lbRecNo.Text, lbIsFFNTRC.Text, lbPR_Type.Text, lbDeletedbPrice.Text);
                            delete.ShowDialog(this);

                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            loadTempDLYPTRANS();
                            ucTBScanBarcode.Focus();
                            //DisableControl();
                            //RefreshGrid();
                        }
                    }
                    else if (diff > double.Parse(checkAgian))
                    {
                        string message = ProgramConfig.message.get("frmSale", "NotAllowToEdit").message;
                        string help = ProgramConfig.message.get("frmSale", "NotAllowToEdit").help;
                        notify = new frmNotify(ResponseCode.Error, message, help + " " + double.Parse(checkAgian).ToString(displayAmt));

                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้แก้ไขรายการสินค้านี้!!!", "คุณสามารถแก้ไขราคาสินค้าไม่เกิน " + double.Parse(checkAgian).ToString(displayAmt));
                        notify.ShowDialog(this);
                        return;
                    }
                }
                setFocus();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                //setFocus();
                frmLoading.closeLoading();
                //Program.control.RetryConnection(net.errorType);
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                setFocus();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void scanEditItem()
        {
            frmLoading.showLoading();
            try
            {
                editType = "All";
                string check = ProgramConfig.cashireAuthorizeResult.otherData.Rows[0]["MaxPriceChange"].ToString();
                //string check = "999999";

                if (ucTBWI_Qty1.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    ucTBWI_Qty1.Focus();
                    return;
                }

                if (ucTBWI_Price1.Text == "")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่สามารถใส่ค่าว่างได้.");
                    notify.ShowDialog(this);
                    ucTBWI_Price1.Focus();
                    return;
                }

                if (ucTBWI_Price1.Text == "0")
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    notify.ShowDialog(this);
                    ucTBWI_Price1.Focus();
                    return;
                }

                if (double.Parse(ucTBWI_Qty1.Text) > double.Parse(quant))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "QtyMoreThan").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "QtyMoreThan").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "จำนวนมากกว่าที่มีอยู่.");
                    notify.ShowDialog(this);
                    ucTBWI_Qty1.Focus();
                    return;
                }
                else if (double.Parse(ucTBWI_Qty1.Text) <= 0)
                {
                    string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowNullAndBelow0").message;
                    string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "NotAllowNullAndBelow0").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "จำนวนน้อยกว่าที่มีอยู่.");
                    notify.ShowDialog(this);
                    ucTBWI_Qty1.Focus();
                    return;
                }
                else if (double.Parse(ucTBWI_Price1.Text) == double.Parse(price))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NoPriceChange").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NoPriceChange").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //notify = new frmNotify(ResponseCode.Error, "ไม่มีการเปลี่ยนแปลงราคา.");
                    notify.ShowDialog(this);
                    ucTBWI_Price1.Focus();
                    return;
                }

                //Check Adjuct Price item Max
                string chkAdjust = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_AdjustPrice_ItemMax.parameterCode);
                if ((Math.Abs((double.Parse(ucTBWI_Price1.Text) - double.Parse(price))) * 100) / Convert.ToDouble(price) > Convert.ToDouble(chkAdjust))
                {
                    string responseMessage = String.Format(ProgramConfig.message.get("frmSale", "NotAllowAdjustLessThanPercent").message, 100 - Convert.ToDouble(chkAdjust));
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowAdjustLessThanPercent").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                    notify.ShowDialog(this);
                    return;
                }

                double diff = Math.Abs((double.Parse(ucTBWI_Price1.Text) - double.Parse(price)) * double.Parse(ucTBWI_Qty1.Text));
                if (diff <= double.Parse(check))
                {
                    Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                    if (checkReason.policy == PolicyStatus.Skip)
                    {
                        DisableControl();
                        ucTBScanBarcode.Focus();
                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        RefreshGrid();

                    }
                    else if (checkReason.policy == PolicyStatus.Work)
                    {
                        StoreResult result = process.displayEditItemReason();
                        frmEditItemReason edit = new frmEditItemReason(lbTxtProductCode1.Text, ucTBWI_Qty1.Text, quant, ucTBWI_Price1.Text, price, editType, lbDiscID.Text, lbRecNo.Text, lbIsFFNTRC.Text, lbPR_Type.Text, lbDeletedbPrice.Text);
                        edit.ShowDialog(this);

                        panelScanBarcode.BringToFront();
                        currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        //DisableControl();
                        loadTempDLYPTRANS();
                        //RefreshGrid(); ;
                        ucTBScanBarcode.Focus();
                    }

                }
                else if (diff > double.Parse(check))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowToEdit").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowToEdit").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, helpMessage + " " + double.Parse(check).ToString(displayAmt));

                    //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้แก้ไขรายการสินค้านี้!!!", "คุณสามารถแก้ไขราคาสินค้าไม่เกิน " + double.Parse(check).ToString(displayAmt));
                    notify.ShowDialog(this);

                    //Fix Auth (Done)
                    //frmUserAuthorize auth = new frmUserAuthorize("EditItem", "2");
                    //auth.function = FunctionID.NoFunctionID;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    textLanguageChange();
                    //    setFocus();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.NoFunctionID }, "EditItem"))
                    {
                        textLanguageChange();
                        setFocus();
                        return;
                    }

                    textLanguageChange();

                    string checkAgain = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxPriceChange"].ToString();
                    if (diff <= double.Parse(checkAgain))
                    {
                        Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                        if (checkReason.policy == PolicyStatus.Skip)
                        {
                            DisableControl();
                            ucTBScanBarcode.Focus();
                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            RefreshGrid();

                        }
                        else if (checkReason.policy == PolicyStatus.Work)
                        {
                            StoreResult result = process.displayEditItemReason();
                            frmEditItemReason edit = new frmEditItemReason(lbTxtProductCode1.Text, ucTBWI_Qty1.Text, quant, ucTBWI_Price1.Text, price, editType, lbDiscID.Text, lbRecNo.Text, lbIsFFNTRC.Text, lbPR_Type.Text, lbDeletedbPrice.Text);
                            edit.ShowDialog(this);

                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            //DisableControl(); 
                            loadTempDLYPTRANS();
                            //RefreshGrid(); ;
                            ucTBScanBarcode.Focus();
                        }

                    }
                    else if (diff > double.Parse(checkAgain))
                    {
                        string message = ProgramConfig.message.get("frmSale", "NotAllowToEdit").message;
                        string help = ProgramConfig.message.get("frmSale", "NotAllowToEdit").help;
                        notify = new frmNotify(ResponseCode.Error, message, help + " " + double.Parse(checkAgain).ToString(displayAmt));

                        //notify = new frmNotify(ResponseCode.Error, "ไม่อนุญาตให้แก้ไขรายการสินค้านี้!!!", "คุณสามารถแก้ไขราคาสินค้าไม่เกิน " + double.Parse(checkAgian).ToString(displayAmt));
                        notify.ShowDialog(this);
                        return;

                        //frmUserAuthorize auth2 = new frmUserAuthorize();
                        //auth.function = FunctionID.NoFunctionID;
                        //DialogResult auth2_res = auth.ShowDialog(this);
                        //if (auth2_res != DialogResult.Yes)
                        //{
                        //    frmNotify dialog = new frmNotify(ResponseCode.Error, "No Authorize.");
                        //    dialog.ShowDialog(this);
                        //    return;
                        //}
                        //string checkAgian2 = ProgramConfig.superUserAuthorizeResult.otherData.Rows[0]["MaxPriceChange"].ToString();
                        //if (diff <= double.Parse(checkAgian2))
                        //{
                        //    Profile checkReason = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
                        //    if (checkReason.policy == PolicyStatus.Skip)
                        //    {
                        //        DisableControl();
                        //        ucTBScanBarcode.Focus();
                        //        panelScanBarcode.BringToFront();
                        //        RefreshGrid();

                        //    }
                        //    else if (checkReason.policy == PolicyStatus.Work)
                        //    {
                        //        StoreResult result = process.displayEditItemReason();
                        //        frmEditItemReason edit = new frmEditItemReason(lbTxtProductCode1.Text, ucTBWI_Qty1.Text, quant, ucTBWI_Price1.Text, price, editType);
                        //        edit.ShowDialog(this);

                        //        panelScanBarcode.BringToFront();
                        //        ucTBScanBarcode.Focus();
                        //        //DisableControl();
                        //        loadTempDLYPTRANS();
                        //        //RefreshGrid(); ;
                        //        cnt++;
                        //    }

                        //}
                    }
                }
                //ucTBScanBarcode.Text = "";
                frmLoading.closeLoading();
                setFocus();
            }
            catch (NetworkConnectionException net)
            {
                //setFocus();
                frmLoading.closeLoading();
               // Program.control.RetryConnection(net.errorType);
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                setFocus();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void loadTempDLYPTRANS(DataRow[] drInp = null)
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
                panel_list_promotion.Controls.Clear();

                DataRow[] data;
                if (drInp == null)
                {
                    data = process.getTempSaleItem();
                }
                else
                {
                    data = drInp;
                }
                
                 
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
                //AppMessage.fillControlsFont(ProgramConfig.language, pn_item_sell, GetListIgnoreFont_pn_item_sell());
                //AppMessage.fillControlsFont(ProgramConfig.language, frmMoCus.pn_Item, GetListIgnoreFont_frmMoCus_pn_Item());

                lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
                lbTxtTotal.Text = amtPrice.ToString(displayAmt);

                frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString(displayAmt);
                frmMoCus.lbTxtDiscount.Text = "0";
                frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString(displayAmt);

                frmLoading.closeLoading();
                RefreshGrid2();
                DisableControl();
                setVisibleButtonPayment();
                CheckItemSell();
                setFocus();

        }

        private void ucTBPrice_EnterFromButton(object sender, EventArgs e)
        {
            clickerEditItem();
        }

        private void ucTBWI_Qty1_EnterFromButton(object sender, EventArgs e)
        {
            scanEditItem();
        }

        private void ucTBWI_Price1_EnterFromButton(object sender, EventArgs e)
        {
            scanEditItem();
        }

        private void ucTBWI_Qty2_EnterFromButton(object sender, EventArgs e)
        {
            scanDeleteItem();
        }

        private void ucDDDiscount_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownDiscountList();
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownDiscountList()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            StoreResult result = process.getDisplayDiscountManual();
            if (result.otherData != null)
            {
                DataTable dt = result.otherData;
                dt = dt.AsEnumerable().OrderByDescending(o => Convert.ToInt32(o["Seq"])).CopyToDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    drItem.DisplayText = dr["DiscountName"].ToString();
                    drItem.ValueText = dr["DiscountID"].ToString();
                    lstStr.Add(drItem);
                }
            }

            return lstStr;
        }

        private void ucDDDiscount_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            var ddl = (UCDropDownList)sender;

            if (ddl.ValueText == "1") // %
            {
                lbType.Text = "%";
                ucTxtCouponNo.Visible = false;
                ucTxtCouponNo.Text = "N/A";
            }
            else if (ddl.ValueText == "2") // Bath
            {
                lbType.Text = "" + ProgramConfig.currencyDefault;
                ucTxtCouponNo.Visible = false;
                ucTxtCouponNo.Text = "N/A";
            }
            else if (ddl.ValueText == "3") // Coupon
            {
                lbType.Text = "" + ProgramConfig.currencyDefault;
                ucTxtCouponNo.Visible = true;
                ucTxtCouponNo.Text = "";
            }

            ucTxtAmountDiscount.Focus();
        }

        private void ucTxtAmountDiscount_TextBoxKeydown(object sender, EventArgs e)
        {
            checkDiscount();
        }

        private void ucTxtAmountDiscount_EnterFromButton(object sender, EventArgs e)
        {
            checkDiscount();
        }

        private void checkDiscount()
        {
            string discountLimit = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_DiscountManual_LimitItemDiscount.parameterCode).ToString();

            if (ucDDDiscount.ValueText == "1")
            {
                if (double.Parse(ucTxtAmountDiscount.Text) > double.Parse(discountLimit))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "Message Box").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "Message Box").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, discountLimit));

                    //notify = new frmNotify(ResponseCode.Error, "มูลค่าส่วนลดสูงกว่าส่วนลดที่กำหนด", "มูลค่าส่วนลดต้องไม่เกิน " + discountLimit + "% ของยอดขายสินค้า");
                    notify.ShowDialog(this);
                    return;
                }
                else
                {
                    ProgramConfig.flagDiscount = true;
                    panelScanBarcode.BringToFront();
                    currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                    ucTBScanBarcode.Focus();
                }
            }
            else if (ucDDDiscount.ValueText == "2")
            {
                if (double.Parse(ucTxtAmountDiscount.Text) > double.Parse(discountLimit))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "DiscountExceedLimit").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "DiscountExceedLimit").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, discountLimit, ProgramConfig.currencyDefault));

                    //notify = new frmNotify(ResponseCode.Error, "มูลค่าส่วนลดสูงกว่าส่วนลดที่กำหนด", "มูลค่าส่วนลดต้องไม่เกิน " + discountLimit + " " + ProgramConfig.currencyDefault + " ของยอดขายสินค้า");
                    notify.ShowDialog(this);
                    return;
                }
                else
                {
                    ProgramConfig.flagDiscount = true;
                    panelScanBarcode.BringToFront();
                    currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                    ucTBScanBarcode.Focus();
                }
            }
            else if (ucDDDiscount.ValueText == "3")
            {
                if (double.Parse(ucTxtAmountDiscount.Text) > double.Parse(discountLimit))
                {
                    string responseMessage = ProgramConfig.message.get("frmSale", "DiscountExceedLimit").message;
                    string helpMessage = ProgramConfig.message.get("frmSale", "DiscountExceedLimit").help;
                    notify = new frmNotify(ResponseCode.Error, responseMessage, string.Format(helpMessage, discountLimit, ProgramConfig.currencyDefault));

                    //notify = new frmNotify(ResponseCode.Error, "มูลค่าส่วนลดสูงกว่าส่วนลดที่กำหนด", "มูลค่าส่วนลดต้องไม่เกิน " + discountLimit + " " + ProgramConfig.currencyDefault + " ของยอดขายสินค้า");
                    notify.ShowDialog(this);
                    return;
                }
                else
                {
                    ProgramConfig.flagDiscount = true;
                    panelScanBarcode.BringToFront();
                    currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                    ucTBScanBarcode.Focus();
                }
            }
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            loadTempDLYPTRANS();
            //AppMessage.fillControlsFont(ProgramConfig.language, pn_item_sell, GetListIgnoreFont_pn_item_sell());
            //AppMessage.fillControlsFont(ProgramConfig.language, frmMoCus.pn_Item, GetListIgnoreFont_frmMoCus_pn_Item());
            textLanguageChange();
            setFocus();

            if (ProgramConfig.memberName != "")
            {
                ucHeader1.nameVisible = true;
                Label newFont = new Label();
                newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                int checkWidth = TextRenderer.MeasureText(ProgramConfig.memberName, newFont.Font).Width;
                ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);
            }
            else
            {
                ucHeader1.nameVisible = false;
            }
        }

        private List<string> GetListIgnoreFont_frmMoCus_pn_Item()
        {
            List<string> lst = new List<string>();
            lst.Add("lb_AMT");
            lst.Add("lb_QTY");
            return lst;
        }

        private List<string> GetListIgnoreFont_pn_item_sell()
        {
            List<string> lst = new List<string>();
            lst.Add("lbNo");
            lst.Add("lbUC_ProductCode");
            lst.Add("lbUC_Qty");
            lst.Add("lbUC_Price");
            lst.Add("lbUC_Discount");
            lst.Add("lbUC_TotalPrice");
            return lst;
        }
       
        public void textLanguageChange()
        {
            if (price != null || price != "")
            {
                lbCurrentPrice.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice"), price, ProgramConfig.currencyDefault);
            }

            if (currentDis != null || currentDis != "")
            {
                lbCurrentDiscount.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentDiscount"), currentDis, ProgramConfig.currencyDefault);
            }

            if (quant != null || quant != "")
            {
                lbCurrentQty1.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentQty1"), quant);
                lbCurrentQty2.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentQty2"), quant);
            }
            if (price != null || price != "")
            {
                lbCurrentPrice1.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice1"), price, ProgramConfig.currencyDefault);
                lbCurrentPrice2.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbCurrentPrice2"), price, ProgramConfig.currencyDefault);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
            DisableControl();
        }

        private void ucTBWI_Qty1_TextBoxKeydown(object sender, EventArgs e)
        {
            scanEditItem();
        }

        private void ucTBWI_Price1_TextBoxKeydown(object sender, EventArgs e)
        {
            scanEditItem();
        }

        private void ucTBWI_Qty2_TextBoxKeydown(object sender, EventArgs e)
        {
            scanDeleteItem();
        }

        private void btnMultiplyItem_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

        private void btnEditItem_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

        private void btnDeleteItem_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

        private void ucTxtQty_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

        private void btnGoodSales_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

        private void setVisibleButtonPayment()
        {
            btnPayment.Enabled = false;
            btnConfirm.BringToFront();
            //btnPayment.BackgroundImage = Properties.Resources.btn_payment_disable;
            pn_drop_menu.Visible = false;
        }

        private void setVisibleButtonConfirm(bool val)
        {      
            if (val)
            {
                if(!btnConfirm.Enabled)
                {
                    btnConfirm.Enabled = true;
                    btnConfirm.BackgroundImage = Properties.Resources.Sale_btnConfirm;
                //btnConfirm.BackColor = Color.White;
                }
            }
            else
            {
                if (btnConfirm.Enabled)
                {
                    btnConfirm.Enabled = false;
                    btnConfirm.BackgroundImage = Properties.Resources.payment_disable;
                    //btnConfirm.BackColor = Color.Silver;
                }

            }
        }

        private void ucTBScanBarcode_Enter(object sender, EventArgs e)
        {
            setVisibleButtonPayment();
        }

        private void pn_drop_menu_Leave(object sender, EventArgs e)
        {
            pn_drop_menu.Visible = false;
        }

        private void ucTBPrice_TextBoxKeydown(object sender, EventArgs e)
        {
            clickerEditItem();
        }

        private void ucTBQty_EnterFromButton(object sender, EventArgs e)
        {
            clickerEditItem();
        }

        private void ucTBQty_TextBoxKeydown(object sender, EventArgs e)
        {
            clickerEditItem();
        }

        private void switchToStandalone(string step)
        {
            //PageResult res = process.serverConnectionLost();
            //if (res.response.next)
            //{
            //    updateSaleMode();
            //    notify = new frmNotify(ResponseCode.Information, "POS เปลี่ยน Mode การขายเป็น Stand Alone กรุณาทำรายการล่าสุดใหม่อีกครั้ง");
            //    notify.ShowDialog(this);
            //    if (step == "loadSalePage")
            //    {
            //        Program.control.ShowForm("frmMainMenu");
            //        this.Dispose();
            //    }
            //    else if (step == "beforePaymentProcess")
            //    {
            //    }
            //    else if (step == "calculateDiscount")
            //    {
            //    }
            //    else if (step == "editSaleItem")
            //    {
            //    }
            //    else if (step == "scanSaleProduct")
            //    {
            //    }
            //    else if (step == "saveCancelItem")
            //    {
            //    }
            //    else if (step == "deleteSaleItem")
            //    {
            //    }
            //}
            //else
            //{
            //    notify = new frmNotify(res.response, res.message, res.help);
            //    notify.ShowDialog(this);
            //    return;
            //}
        }

        public void frmSale_Activated(object sender, EventArgs e)
        {
            try
            {
                cntTimeoutSale = 0;
                AppLog.writeLog("frmSale_Activated afterNotify = " + afterNotify + "");
                if (!afterNotify)
                {
                    AppLog.writeLog("frmSale_Activated salePageState = " + ProgramConfig.salePageState.ToString());
                    if (ProgramConfig.salePageState == 2)
                    {
                        if (!timer1.Enabled)
                        {
                            timer1.Start();
                        }
                        cntTimeoutSale = 0;
                        ProgramConfig.pageBackFromPayment = PageBackFormPayment.NormalSale;
                        ProgramConfig.salePageState = 3;
                        // end current sale prepare for new receipt
                        AppLog.writeLog("frmSale_Activated pn_item_sell.Controls.Clear();");
                        pn_item_sell.Controls.Clear();
                        //ucTBScanBarcode.Focus();

                        StoreResult result = null;
                        //panelScanBarcode.BringToFront();
                        //currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                        //Hardware.addDrawerListeners(DrawerStatus);

                        if (ProgramConfig.saleNeedAuthorize)
                        {
                            afterNotify = true;
                            if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.Sale_SelectSaleMenu }, "Sale"))
                            {
                                textLanguageChange();
                                return;
                            }

                            textLanguageChange();
                        }

                        frmLoading.showLoading();
                        result = process.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.SaleRef);
                        frmLoading.closeLoading();
                        if (result.response.next)
                        {
                            lbTxtRefNo.Text = ProgramConfig.saleRefNo;
                        }
                        else
                        {
                            afterNotify = true;
                            notify = new frmNotify(result);
                            notify.ShowDialog();
                            this.Dispose();
                            return;
                        }

                        Profile chkMCashier = ProgramConfig.getProfile(FunctionID.Sale_GetMessageCashier);
                        if (chkMCashier.policy == PolicyStatus.Work)
                        {
                            ProcessResult res = process.cashireMessageStatus();
                            if (res.response.next)
                            {
                                if (result.response == ResponseCode.Information)
                                {
                                    frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
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
                                frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                                dialog.ShowDialog(this);
                                this.Dispose();
                                return;
                            }
                        }

                        if (!ProgramConfig.skipNormalSale)
                        {
                            if (ProgramConfig.normalSaleNeedAuthorize)
                            {
                                afterNotify = true;
                                if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.Sale_InputSaleItem_InputProduct_NormalSale }, "Sale"))
                                {
                                    textLanguageChange();
                                    return;
                                }

                                textLanguageChange();
                            }
                            panelScanBarcode.Enabled = true;
                            setVisibleButtonPayment();
                        }
                        else
                        {
                            //ปิดการขายปกติ
                            panelScanBarcode.Enabled = false;
                            setVisibleButtonPayment();
                        }

                        if (ProgramConfig.checkSaleCashIn)
                        {

                            result = process.posCheckCashInSaleAmt();
                            if (!result.response.next || result.response == ResponseCode.Information)
                            {
                                afterNotify = true;
                                frmNotify dialog = new frmNotify(result.response, result.responseMessage, result.helpMessage);
                                DialogResult auth_res = dialog.ShowDialog(this);
                                if (auth_res != DialogResult.No)
                                {

                                }
                                else
                                {
                                    this.Dispose();
                                    return;
                                }
                            }
                        }

                        int getPdInputType = ProgramConfig.productInputType;
                        if (getPdInputType == 1)
                        {
                            //Scan
                            ucTBScanBarcode.Enabled = true;
                            //btnGoodSales.Enabled = false;
                            btnGoodSales.Enabled = true;
                        }
                        else if (getPdInputType == 2)
                        {
                            //Icon
                            ucTBScanBarcode.Enabled = false;
                            btnGoodSales.Enabled = true;
                        }
                        else if (getPdInputType == 3)
                        {
                            //Both
                            ucTBScanBarcode.Enabled = true;
                            btnGoodSales.Enabled = true;
                        }

                        // clear value
                        ClearValue();

                        int getConfig = ProgramConfig.defaultCursorPosition;

                        if (getConfig == 1) //Product
                        {
                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
                            ucTBScanBarcode.Focus();
                        }
                        else if (getConfig == 2) //Member
                        {
                            currentPanel = CurrentPanelSale.PanelMember;
                            clickSearchMember(sender, e);
                        }
                        else
                        {
                            panelScanBarcode.BringToFront();
                            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
                            ucTBScanBarcode.Focus();
                        }

                        CallCheckPrintInvoidType();

                        GC.Collect();
                        ProgramConfig.salePageState = 1;
                    }
                    //ucTBScanBarcode.Focus();
                    setFocus();
                    frmLoading.closeLoading();
                }

                if (ProgramConfig.IsStandAloneMode)
                {
                    ucFooterTran1.IsStandAlone = true;
                }
                else
                {
                    ucFooterTran1.IsStandAlone = false;
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkConnectionException(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void ClearValue(bool IsClearMember = true)
        {
            _totalPriceChange = 0;
            amtPrice = 0;
            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
            lbTxtdiscount1.Text = amtPrice.ToString(displayAmt);
            lbTxtTotal.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtSubTotalCash.Text = amtPrice.ToString(displayAmt);
            frmMoCus.lbTxtDiscount.Text = "0";
            frmMoCus.lbTxtTotalCash.Text = amtPrice.ToString(displayAmt);
            pn_item_sell.Controls.Clear();
            frmMoCus.pn_Item.Controls.Clear();
            panel_list_promotion.Controls.Clear();
            panel_list_suggest.Controls.Clear();
            panel_suggest.BringToFront();
            panel_list_discount.Controls.Clear();
            btnOtherService.Visible = true;
            btnOtherService.BringToFront();
            RefreshGrid();
            DisableControl();
            setVisibleButtonPayment();
            setVisibleButtonConfirm(false);
            ProgramConfig.employeeID = "";
            ProgramConfig.loadHoldOrderReceipt = "";

            if (IsClearMember)
                    ClearMember();
        }

        private void ucTxtQty_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTxtQty_EnterFromButton(this, null);
        }

        private void ucHeader1_AlertClick(object sender, EventArgs e)
        {
            picBtBack_Click(this, null);
        }

        private void btnEditItem_BackgroundImageChanged(object sender, EventArgs e)
        {
            DisableBtnFavorit();
        }

        private void btnDeleteItem_BackgroundImageChanged(object sender, EventArgs e)
        {
            DisableBtnFavorit();
        }

        private void DisableBtnFavorit()
        {
            if ((string)btnEditItem.Tag == "enable" || (string)btnDeleteItem.Tag == "enable")
            {
                btnGoodSales.Enabled = false;
                btnGoodSales.BackColor = Color.LightGray;
            }
            else
            {
                btnGoodSales.Enabled = true;
                btnGoodSales.BackColor = Color.White;
            }
        }

        private void frmSale_Disposed(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
            Hardware.clearDrawerListeners();
        }

        private void ucTBPrice_Enter(object sender, EventArgs e)
        {
            ucTBPrice.Focus();
        }

        private void panelScanBarcodeBringToFront()
        {
            panelScanBarcode.BringToFront();
            currentPanel = CurrentPanelSale.PanelScanBarcode; //currentPanel = "panelScanBarcode";
        }

        private void ucDDLMenuExtra_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownListMenuExtra();
            }

        }

        private List<UCDropDownList.Dropdown> SetDataucDropDownListMenuExtra()
        {
            List<UCDropDownList.Dropdown> lstStr = new List<UCDropDownList.Dropdown>();

            Profile check = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_AdditionalInput_SpecialProduct); //#125
            if (check.policy == PolicyStatus.Work)
            {
                // "เพิ่มสินค้าขายของพิเศษ"
                lstStr.Add(new UCDropDownList.Dropdown() { DisplayText = ProgramConfig.message.get("frmSale", "AddSpecialProduct").message, ValueText = "1" });
            }

            Profile check2 = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_AdditionalInput_TakeoutDocument); //#127
            if (check2.policy == PolicyStatus.Work)
            {
                //"พิมพ์ใบนำออก"
                lstStr.Add(new UCDropDownList.Dropdown() { DisplayText = ProgramConfig.message.get("frmSale", "PrintExportPermit").message, ValueText = "2" }); 
            }

            return lstStr;
        }

        private void ucDDLMenuExtra_UCDropDownGetItemClick(object sender, EventArgs e)
        {
            var ddl = (UCDropDownList)sender;
            if (ddl.ValueText == "1")
            {
                Profile check = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_AdditionalInput_SpecialProduct); //#30
                if (Utility.CheckAuthPass(this, check, "SpecialProduct"))
                {
                    panelAddProductSpecial.BringToFront();
                    currentPanel = CurrentPanelSale.PanelAddProductSpecial; //currentPanel = "panelAddProductSpecial";
                    ucDDCause.LabelText = AppMessage.getMessage(ProgramConfig.language, "frmSale", "ucDDCause");
                    ucDDCause.ValueText = "";
                    setFocus();
                }
            }
            else if (ddl.ValueText == "2")
            {
                Profile check2 = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_EditProduct_AdditionalInput_TakeoutDocument); //#32
                if (Utility.CheckAuthPass(this, check2, "PrintExport"))
                {
                    panelPrintExport.BringToFront();
                    currentPanel = CurrentPanelSale.PanelPrintExport; //currentPanel = "panelPrintExport";
                    ReadDataClickItem();
                    setFocus();
                }
            }
        }

        private void ucDDCause_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownListCustom)
            {
                var ucDDL = (UCDropDownListCustom)sender;
                ucDDL.lstDDLC = SetDataucDropDownListExtraProduct();
            }
        }

        private IEnumerable<IDropdownListItem> SetDataucDropDownListExtraProduct()
        {
            List<UCItemDropDownListExtraProduct> lstStr = new List<UCItemDropDownListExtraProduct>();

            //lstStr.Add(new UCItemDropDownListExtraProduct() { DisplayText = "( / ) สินค้าไม่มี Barcode", ValueText = "L", picIcon = Properties.Resources.icon_NoBarcode });
            //lstStr.Add(new UCItemDropDownListExtraProduct() { DisplayText = "( + ) สินค้าขายพิเศษ", ValueText = "A", picIcon = Properties.Resources.icon_star_special });
            //lstStr.Add(new UCItemDropDownListExtraProduct() { DisplayText = "( - ) สินค้าไม่สามารถ Scan", ValueText = "K", picIcon = Properties.Resources.icon_CannotScan });

            string noBar = AppMessage.getMessage(ProgramConfig.language, "frmSale", "DDExtraProductNoBarcode");
            string special = AppMessage.getMessage(ProgramConfig.language, "frmSale", "DDExtraProductSpecialProd");
            string noScan = AppMessage.getMessage(ProgramConfig.language, "frmSale", "DDExtraProductProdCntScan");

            lstStr.Add(new UCItemDropDownListExtraProduct() { DisplayText = noBar, ValueText = "L", picIcon = Properties.Resources.icon_NoBarcode });
            lstStr.Add(new UCItemDropDownListExtraProduct() { DisplayText = special, ValueText = "A", picIcon = Properties.Resources.icon_star_special });
            lstStr.Add(new UCItemDropDownListExtraProduct() { DisplayText = noScan, ValueText = "K", picIcon = Properties.Resources.icon_CannotScan });

            return lstStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add To TempDLYPTrans
            string symbol = ucDDCause.ValueText;
            if (symbol != "")
            {
                StoreResult res = process.updatePCDSymbolTempDlyptrans(lbTxtProductCode3.Text, lbRecNo.Text, symbol);
                panelScanBarcode.BringToFront();
                currentPanel = CurrentPanelSale.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
                loadTempDLYPTRANS();
                ucTBScanBarcode.Focus();
            }
            else
            {
                string responseMessage = ProgramConfig.message.get("ChooseDropDown", "ChooseDropDown").message;
                string helpMessage = ProgramConfig.message.get("ChooseDropDown", "ChooseDropDown").help;
                frmNotify notify = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);
                notify.ShowDialog();
                return;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Tag.ToString() == "N")
            {
                pictureBox2.Tag = "Y";
                pictureBox2.Image = Properties.Resources.btn_PrintExportProduct;
                lbPrint.BringToFront();
                process.updatePrintExportTempDlyptrans(lbTxtProductCode3.Text, recExport, "Y");
            }
            else
            {
                pictureBox2.Tag = "N";
                pictureBox2.Image = Properties.Resources.btn_PrintNoExportProduct;
                lbNotPrint.BringToFront();
                process.updatePrintExportTempDlyptrans(lbTxtProductCode3.Text, recExport, "N");
             
            }
            loadTempDLYPTRANS();
            selectedSellItem();
        }

        private void selectedSellItem()
        {
           UCItemSell ucItm = pn_item_sell.Controls.Cast<UCItemSell>().Where(w => w.lbNoText == lbNo.Text).FirstOrDefault();          
           ucItm = ucItm ?? new UCItemSell() { lbNoText = "" };
           if (ucItm.lbNoText != "")
           {
               lastUCIS = ucItm;
               ucItm.BackColor = Color.FromArgb(184, 251, 207);
           }
        }

        private void lbTxtProductCode3_TextChanged(object sender, EventArgs e)
        {
            lbAddProductSpecial_ProductCode.Text = lbTxtProductCode3.Text;
        }

        private void lbTxtDesc3_TextChanged(object sender, EventArgs e)
        {
            lbAddProductSpecial_ProductDesc.Text = lbTxtDesc3.Text;
        }

        private void lbTxtRefNo_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbTxtRefNo);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmProductAndService fPAS = new frmProductAndService();
            fPAS.ShowDialog(this);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ucTBScanBarcode.EnabledUC = true;
            //CalDiscount
            double ind = 0;
            string discountDesc, saleAmt, discountAmt;

            frmLoading.showLoading();
            ProcessResult result = process.calculateDiscount();
            frmLoading.closeLoading();
            if (result.response.next)
            {
                panel_list_discount.Controls.Clear();
                DataRow[] data = (DataRow[])result.data;
                foreach (DataRow row in data)
                {
                    discountDesc = row["ProductName"].ToString();
                    saleAmt = (double.Parse(row["DisplayAmt"].ToString())).ToString(displayAmt);
                    discountAmt = (double.Parse(row["AMT"].ToString())).ToString(displayAmt);

                    UCListDiscount ucDis = new UCListDiscount((int)row["REC"]);
                    ucDis.lbTxtName.Text = discountDesc;
                    ucDis.lbTxtPrice.Text = saleAmt;
                    ucDis.lbTxtAmt.Text = discountAmt;
                    ucDis.lbTxtAmt.Visible = true;
                    panel_discount.BringToFront();
                    panel_list_discount.Controls.Add(ucDis);

                    ind += double.Parse(row["AMT"].ToString());
                }
                lbTxtdiscount1.Text = ind.ToString(displayAmt);
                lbTxtTotal.Text = (double.Parse(lbTxtSubtotal.Text) - ind).ToString(displayAmt);

                frmMoCus.lbTxtDiscount.Text = lbTxtdiscount1.Text;
                frmMoCus.lbTxtTotalCash.Text = lbTxtTotal.Text;
            }
            else
            {
                notify = new frmNotify(result);
                notify.ShowDialog(this);
            }

            OrderTypeProcess(false);

            DisableControl();

            btnPayment.Enabled = true;
            btnPayment.BringToFront();
            panelScanBarcode.BringToFront();
        }

        private void ucHeader1_HambergerItemClick(object sender, EventArgs e)
        {          
            CancelSale(sender);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cntTimeoutSale++;
            label1.Text = cntTimeoutSale.ToString();
            if (cntTimeoutSale == timeOutSale)
            {
                if (pn_item_sell.Controls.Count == 0)
                {
                    frm2Detail.panel_message.BringToFront();
                    Program.control.ShowForm("frmMainMenu");
                    Program.control.CloseForm("frmSale");
                }
                else
                {
                    cntTimeoutSale = 0;
                }
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg >= 513 && m.Msg <= 515)
            {
                cntTimeoutSale = 0;
                return false;
            }
            return false;
        }

    }
}
