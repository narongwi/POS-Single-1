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

namespace BJCBCPOS
{
    public partial class frmReturnSuccess : Form
    {
        public frmMainMenu frmMainMenu;
        private bool IsPaint = false;
        private bool tranSuccess = false;
        private bool chkSwOpen = false;
        private bool chkSwClose = false;
        private bool statusCloseDrawer = false;
        string returnRefNo;
        string returnPrice;
        string reasonId = "";
        string eventName = "CloseReturn";
        string openTime = "";
        string closeTime = "";
        string saleRef;
        string CNNo;
        string nextCNNo;
        string returnType;
        string reasonTxt;
        string lockNo;
        string userID;
        string member;
        DataTable FULL;
        DataTable PARTIAL;
        DataTable saveTempTable;
        public bool chk;
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        private ReturnProcess process = new ReturnProcess();
        private frmReturnFromInvoice dataTable = new frmReturnFromInvoice();
        private bool submitFromEDC;
        private PrintInvoiceType _printType;

        public frmReturnSuccess(string refNo, string price, string reasonData, string reasonTxt ,string saleRefData, string reType, string lockNo, string userID, string member, string saleTime, DataTable tFull, DataTable tPartial, PrintInvoiceType printType)
        {
            InitializeComponent();
            returnRefNo = refNo;
            returnPrice = price;
            reasonId = reasonData;
            saleRef = saleRefData;
            returnType = reType;
            this.reasonTxt = reasonTxt;
            this.lockNo = lockNo;
            this.userID = userID;
            this.member = member;
            FULL = tFull;
            PARTIAL = tPartial;
            _printType = printType;
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

        private void frmReturnSuccess_Load(object sender, EventArgs e)
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
            Hardware.addDrawerListeners(DrawerStatus);
            //lbReceiptNo.Text = "(ด้วยยอดเงิน " + returnPrice +" " +ProgramConfig.currencyDefault+ ")";
            lbRecepitNoVal.Text = returnRefNo;
            lbTotalVal.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbReceiptNo"), returnPrice, ProgramConfig.currencyDefault);

            lbLockNoResult.Text = lockNo;
            lbCashierIdVal.Text = userID;
            lbMemberName.Text = member.Trim() == "" ? "-" : member; //TO DO
            lbTotalVal.Text = returnPrice;
            lbReasonVal.Text = reasonTxt;
            lbReturnTimeVal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lbSaleTimeVal.Text = "";

            submitFromEDC = false;

            panel_button.BringToFront();
        }

        public void btnEnable()
        {
            panel_button.BringToFront();
            btnCancel.Enabled = true;
            btnOk.Enabled = true;
            submitFromEDC = false;
        }

        public void btnDisable()
        {
            btnCancel.Enabled = false;
            btnOk.Enabled = false;
        }

        public void returnSuccessProcess()
        {
            //saveTempReturn();
            //OpenCashDrawer();
            try
            {
                frmLoading.showLoading();
                btnDisable();
                ProgramConfig.seqOfProcess = 0;
                if (ProgramConfig.hasDrawer)
                {
                    if (saveTempReturn())
                    {
                        if (OpenCashDrawer())
                        {
                            StoreResult res = process.saveReturnTransaction(saleRef);
                            if (!res.response.next)
                            {
                                process.rollback();
                                btnEnable();
                                frmLoading.closeLoading();
                                frmNotify dialog = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                                dialog.ShowDialog(this);
                                return;
                            }
                            else if (res.response == ResponseCode.Success)
                            {
                                var dt = res.otherData;
                                if (dt != null)
                                {
                                    CNNo = dt.Rows[0]["CNNo"].ToString();
                                    Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                                    Match result = re.Match(CNNo);
                                    string alphaPart = result.Groups[1].ToString();
                                    string numberPart = result.Groups[2].ToString();
                                    nextCNNo = alphaPart + (Convert.ToInt32(numberPart) + 1).ToString("D7");
                                }

                                Profile checkPro = ProgramConfig.getProfile(FunctionID.Return_ProcessAfterReturnTransaction);
                                if (checkPro.policy == PolicyStatus.Work)
                                {
                                    StoreResult checkProcess = process.concludeReturn(ProgramConfig.cnNo);
                                    if (!checkProcess.response.next)
                                    {
                                        process.rollback();
                                        btnEnable();
                                        frmLoading.closeLoading();
                                        frmNotify dialog = new frmNotify(checkProcess);
                                        dialog.ShowDialog(this);
                                        return;
                                    }
                                    else
                                    {
                                        if (checkProcess.response == ResponseCode.Information)
                                        {
                                            frmNotify dialog = new frmNotify(checkProcess);
                                            dialog.ShowDialog(this);
                                        }
                                    }


                                }

                                if (!printReceipt(CNNo))
                                {
                                    return;
                                }

                                process.commit();
                                frmLoading.closeLoading();

                                if (Hardware.isDrawerOpen)
                                {
                                    panel_message.BringToFront();
                                    //frmNotify dialog = new frmNotify("ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ");
                                    //dialog.ShowDialog(this);
                                    //dialog.Refresh();
                                    //return;
                                }
                                tranSuccess = true;
                                closeForm();
                            }
                        }
                        frmLoading.closeLoading();
                    }
                }
                else
                {
                    if (saveTempReturn())
                    {
                        if (OpenCashDrawer())
                        {
                            StoreResult res = process.saveReturnTransaction(saleRef);
                            if (!res.response.next)
                            {
                                process.rollback();
                                btnEnable();
                                frmLoading.closeLoading();
                                frmNotify dialog = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                                dialog.ShowDialog(this);
                                return;
                            }
                            else if (res.response == ResponseCode.Success)
                            {
                                var dt = res.otherData;
                                if (dt != null)
                                {
                                    CNNo = dt.Rows[0]["CNNo"].ToString();
                                    Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                                    Match result = re.Match(CNNo);
                                    string alphaPart = result.Groups[1].ToString();
                                    string numberPart = result.Groups[2].ToString();
                                    nextCNNo = alphaPart + (Convert.ToInt32(numberPart) + 1).ToString("D7");
                                }

                                Profile checkPro = ProgramConfig.getProfile(FunctionID.Return_ProcessAfterReturnTransaction);
                                if (checkPro.policy == PolicyStatus.Work)
                                {
                                    StoreResult checkProcess = process.concludeReturn(ProgramConfig.cnNo);
                                    if (!checkProcess.response.next)
                                    {
                                        process.rollback();
                                        btnEnable();
                                        frmLoading.closeLoading();
                                        frmNotify dialog = new frmNotify(checkProcess);
                                        dialog.ShowDialog(this);
                                        return;
                                    }
                                    else
                                    {
                                        if (checkProcess.response == ResponseCode.Information)
                                        {
                                            frmNotify dialog = new frmNotify(checkProcess);
                                            dialog.ShowDialog(this);
                                        }
                                    }

                                }

                                if (!printReceipt(CNNo))
                                {
                                    return;
                                }

                                process.commit();
                                frmLoading.closeLoading();

                                if (Hardware.isDrawerOpen)
                                {
                                    panel_message.BringToFront();
                                    //frmNotify dialog = new frmNotify("ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ");
                                    //dialog.ShowDialog(this);
                                    //dialog.Refresh();
                                    //return;
                                }
                                else
                                {
                                    chkSwClose = true;
                                    tranSuccess = true;
                                    statusCloseDrawer = true;
                                    closeForm();
                                }
                            }
                        }
                        frmLoading.closeLoading();
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                btnEnable();
                frmLoading.closeLoading();
                if (Program.control.RetryConnection(net.errorType))
                {
                    this.Dispose();
                    DialogResult = System.Windows.Forms.DialogResult.Retry;
                }
            }
            catch (Exception ex)
            {
                process.rollback();
                btnEnable();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //DefaultStatus
            statusCloseDrawer = false;
            chkSwClose = false;
            tranSuccess = false;

            frmLoading.showLoading();
            returnSuccessProcess();
            frmLoading.closeLoading();
            //saveTempReturn();
            //OpenCashDrawer();
            //frmLoading.showLoading();
            //btnDisable();
            //process.newTransaction();            
            //try
            //{
            //    ProgramConfig.seqOfProcess = 0;
            //    if (ProgramConfig.hasDrawer)
            //    {
            //        if (saveTempReturn())
            //        {
            //            if (OpenCashDrawer())
            //            {
            //                StoreResult res = process.saveReturnTransaction(saleRef);
            //                if (!res.response.next)
            //                {
            //                    process.rollback();
            //                    btnEnable();
            //                    frmLoading.closeLoading();
            //                    frmNotify dialog = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
            //                    dialog.ShowDialog(this);
            //                    return;
            //                }
            //                else if (res.response == ResponseCode.Success)
            //                {
            //                    var dt = res.otherData;
            //                    if (dt != null)
            //                    {
            //                        CNNo = dt.Rows[0]["CNNo"].ToString();
            //                        Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            //                        Match result = re.Match(CNNo);
            //                        string alphaPart = result.Groups[1].ToString();
            //                        string numberPart = result.Groups[2].ToString();
            //                        nextCNNo = alphaPart + (Convert.ToInt32(numberPart) + 1).ToString("D7");
            //                    }

            //                    Profile checkPro = ProgramConfig.getProfile(FunctionID.Return_ProcessAfterReturnTransaction);
            //                    if (checkPro.policy == PolicyStatus.Work)
            //                    {
            //                        StoreResult checkProcess = process.concludeReturn(ProgramConfig.cnNo);
            //                        if (!checkProcess.response.next)
            //                        {
            //                            process.rollback();
            //                            btnEnable();
            //                            frmLoading.closeLoading();
            //                            frmNotify dialog = new frmNotify(checkProcess.response, checkProcess.responseMessage, checkProcess.helpMessage);
            //                            dialog.ShowDialog(this);
            //                            return;
            //                        }
            //                        else
            //                        {
            //                            if (checkProcess.response == ResponseCode.Information)
            //                            {
            //                                frmNotify dialog = new frmNotify(checkProcess.response, checkProcess.responseMessage, checkProcess.helpMessage);
            //                                dialog.ShowDialog(this);
            //                            }
            //                        }

                                    
            //                    }                                
            //                    printReceipt();
            //                    process.commit();
            //                    frmLoading.closeLoading();
            //                    ProgramConfig.seqOfProcess = 1;

            //                    if (Hardware.isDrawerOpen)
            //                    {
            //                        panel_message.BringToFront();
            //                        //frmNotify dialog = new frmNotify("ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ");
            //                        //dialog.ShowDialog(this);
            //                        //dialog.Refresh();
            //                        //return;
            //                    }
            //                    else
            //                    {
            //                        chkSwClose = false;
            //                        tranSuccess = true;
            //                        frmLoading.showLoading();
            //                        closeForm();
            //                        frmLoading.closeLoading();
            //                    }
            //                }
            //            }
            //            frmLoading.closeLoading();
            //        }
            //    }
            //    else
            //    {
            //        if (saveTempReturn())
            //        {
            //            if (OpenCashDrawer())
            //            {
            //                StoreResult res = process.saveReturnTransaction(saleRef);
            //                if (!res.response.next)
            //                {
            //                    process.rollback();
            //                    btnEnable();
            //                    frmLoading.closeLoading();
            //                    frmNotify dialog = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
            //                    dialog.ShowDialog(this);
            //                    return;
            //                }
            //                else if (res.response == ResponseCode.Success)
            //                {
            //                    var dt = res.otherData;
            //                    if (dt != null)
            //                    {
            //                        CNNo = dt.Rows[0]["CNNo"].ToString();
            //                        Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            //                        Match result = re.Match(CNNo);
            //                        string alphaPart = result.Groups[1].ToString();
            //                        string numberPart = result.Groups[2].ToString();
            //                        nextCNNo = alphaPart + (Convert.ToInt32(numberPart) + 1).ToString("D7");
            //                    }

            //                    Profile checkPro = ProgramConfig.getProfile(FunctionID.Return_ProcessAfterReturnTransaction);
            //                    if (checkPro.policy == PolicyStatus.Work)
            //                    {
            //                        StoreResult checkProcess = process.concludeReturn(ProgramConfig.cnNo);
            //                        if (!checkProcess.response.next)
            //                        {
            //                            process.rollback();
            //                            btnEnable();
            //                            frmLoading.closeLoading();
            //                            frmNotify dialog = new frmNotify(checkProcess.response, checkProcess.responseMessage, checkProcess.helpMessage);
            //                            dialog.ShowDialog(this);
            //                            return;
            //                        }
            //                        else
            //                        {
            //                            if (checkProcess.response == ResponseCode.Information)
            //                            {
            //                                frmNotify dialog = new frmNotify(checkProcess.response, checkProcess.responseMessage, checkProcess.helpMessage);
            //                                dialog.ShowDialog(this);
            //                            }
            //                        }
                                    
            //                    }
            //                    printReceipt();
            //                    process.commit();                                
            //                    frmLoading.closeLoading();                                

            //                    if (Hardware.isDrawerOpen)
            //                    {
            //                        panel_message.BringToFront();
            //                        //frmNotify dialog = new frmNotify("ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ");
            //                        //dialog.ShowDialog(this);
            //                        //dialog.Refresh();
            //                        //return;
            //                    }
            //                    else
            //                    {
            //                        chkSwClose = true;
            //                        tranSuccess = true;
            //                        frmLoading.showLoading();
            //                        closeForm();
            //                        frmLoading.closeLoading();
            //                    }
            //                }
            //            }
            //            frmLoading.closeLoading();
            //        }
            //    }
            //}
            //catch (NetworkConnectionException net)
            //{
            //    btnEnable();
            //    frmLoading.closeLoading();
            //    Program.control.RetryConnection(net.errorType);                
            //}
            //catch (Exception ex)
            //{
            //    process.rollback();
            //    btnEnable();
            //    frmLoading.closeLoading();
            //    frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
            //    dialog.ShowDialog(this);
            //}
        }

        private void closeForm()
        {
            frmLoading.showLoading();
            try
            {
                if (tranSuccess == true && chkSwClose == true && statusCloseDrawer == true)
                {
                    process.newTransaction();
                    if (ProgramConfig.hasDrawer)
                    {                        
                        Profile check = ProgramConfig.getProfile(FunctionID.Return_CloseDrawerAndRecordTime);
                        if (check.policy == PolicyStatus.Work)
                        {
                            StoreResult resClose = process.saveCloseDrawer(closeTime, ProgramConfig.cnNo);
                            if (!resClose.response.next)
                            {
                                frmNotify dialog = new frmNotify(resClose);
                                dialog.ShowDialog(this);
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
                    }

                    Profile checkDataT = ProgramConfig.getProfile(FunctionID.Return_SaveReturnTransaction_SynchSaleTransactiontoDataTank);
                    if (checkDataT.policy == PolicyStatus.Work)
                    {
                        string refNo = ProgramConfig.voidRefNo;
                        string rec = "1";

                        StoreResult checkDataTank = process.syncToDataTank(eventName, FunctionID.Return_SaveReturnTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.returnRefNo, rec, ProgramConfig.cnNo);
                        if (!checkDataTank.response.next)
                        {
                            frmNotify dialog = new frmNotify(checkDataTank);
                            dialog.ShowDialog(this);
                            process.commit();

                            if (nextCNNo != null || nextCNNo != "")
                            {
                                ProgramConfig.cnNo = nextCNNo;
                                ProgramConfig.running.updateValue();
                            }
                            chkSwOpen = false;
                            //Program.control.ShowForm("frmMainMenu");
                            Program.control.CloseForm("frmReturnSuccess");
                            Program.control.CloseForm("frmReturnFromInvoice");
                            Program.control.CloseForm("frmReturnFromScanProduct");
                            //Program.control.CloseForm("frmNotify");
                            foreach (Form item in Application.OpenForms)
                            {
                                if (item is frmMainMenu)
                                {
                                    frmMainMenu = (frmMainMenu)item;
                                    frmMainMenu.BringToFront();
                                    frmMainMenu.Activate();
                                    break;
                                }
                            }
                            this.DialogResult = DialogResult.Yes;
                            //Program.control.ShowForm("frmMainMenu");
                        }
                        else
                        {
                            if (checkDataTank.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(checkDataTank);
                                dialog.ShowDialog(this);
                            }
                            process.commit();

                            if (nextCNNo != null || nextCNNo != "")
                            {
                                ProgramConfig.cnNo = nextCNNo;
                                ProgramConfig.running.updateValue();
                            }
                            chkSwOpen = false;
                            //Program.control.ShowForm("frmMainMenu");
                            Program.control.CloseForm("frmReturnSuccess");
                            Program.control.CloseForm("frmReturnFromInvoice");
                            Program.control.CloseForm("frmReturnFromScanProduct");
                            //Program.control.CloseForm("frmNotify");
                            foreach (Form item in Application.OpenForms)
                            {
                                if (item is frmMainMenu)
                                {
                                    frmMainMenu = (frmMainMenu)item;
                                    frmMainMenu.BringToFront();
                                    frmMainMenu.Activate();
                                    break;
                                }
                            }
                            this.DialogResult = DialogResult.Yes;
                            //Program.control.ShowForm("frmMainMenu");
                        }
                    }
                    process.commit();
                    frmLoading.closeLoading();
                }
                else
                {
                    frmLoading.closeLoading();
                    return;
                }
                
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    closeForm();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private bool printReceipt(string cnNo)
        {
            try
            {
                Profile checkPrintCn = ProgramConfig.getProfile(FunctionID.Return_Document_CNCreditNote);
                if (checkPrintCn.policy == PolicyStatus.Work)
                {
                    if (_printType == PrintInvoiceType.FULLTAX)
                    {
                        process.commit();
                        Hardware.printCN(cnNo);
                    }
                    else
                    {
                        StoreResult PrintCn = process.printCN(ProgramConfig.cnNo);
                        if (!PrintCn.response.next)
                        {
                            process.rollback();
                            btnEnable();
                            frmNotify dialog = new frmNotify(PrintCn);
                            dialog.ShowDialog(this);
                            return false;
                        }
                        else
                        {
                            if (PrintCn.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(PrintCn);
                                dialog.ShowDialog(this);
                            }

                            DataTable dt = PrintCn.otherData;
                            Hardware.printTermal(dt);
                        }
                    }
                }

                Profile checkPrintCnCopy = ProgramConfig.getProfile(FunctionID.Return_Document_CopyOfCNCreditNote);
                if (checkPrintCnCopy.policy == PolicyStatus.Work)
                {
                    if (checkPrintCnCopy.profile == ProfileStatus.Authorize)
                    {
                        if (_printType == PrintInvoiceType.FULLTAX)
                        {
                            process.commit();
                            //Hardware.printCN(cnNo);
                        }
                        else
                        {
                            StoreResult printCNCopy = process.printCopyCN(ProgramConfig.cnNo);
                            if (!printCNCopy.response.next)
                            {
                                process.rollback();
                                btnEnable();
                                frmNotify dialog = new frmNotify(printCNCopy);
                                dialog.ShowDialog(this);
                                return false;
                            }
                            else
                            {
                                if (printCNCopy.response == ResponseCode.Information)
                                {
                                    frmNotify dialog = new frmNotify(printCNCopy);
                                    dialog.ShowDialog(this);
                                }
                                DataTable dt = printCNCopy.otherData;
                                Hardware.printTermal(dt);
                            }
                        }
                    }
                    else if (checkPrintCnCopy.profile == ProfileStatus.NotAuthorize)
                    {
                        if (Utility.CheckAuthPass(this, checkPrintCnCopy, "ReturnSuccess"))
                        {
                            if (ProgramConfig.superUserId != "" || ProgramConfig.superUserId != null || ProgramConfig.superUserId != "N/A")
                            {
                                StoreResult updateSuperuserTempF = process.updateSuperuserIdTempF();
                                if (!updateSuperuserTempF.response.next)
                                {
                                    process.rollback();
                                    btnEnable();
                                    string responseMessage = ProgramConfig.message.get("frmReturnFromInvoice", "CanNotReturn").message;
                                    string helpMessage = ProgramConfig.message.get("frmReturnFromInvoice", "CanNotReturn").help;
                                    frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                                    dialog.ShowDialog(this);
                                    return false;
                                }
                            }

                            if (_printType == PrintInvoiceType.FULLTAX)
                            {
                                process.commit();
                                Hardware.printCN(cnNo);
                            }
                            else
                            {
                                StoreResult printCNCopy = process.printCopyCN(ProgramConfig.cnNo);
                                if (!printCNCopy.response.next)
                                {
                                    process.rollback();
                                    btnEnable();
                                    frmNotify dialog = new frmNotify(printCNCopy);
                                    dialog.ShowDialog(this);
                                    return false;
                                }
                                else
                                {
                                    if (printCNCopy.response == ResponseCode.Information)
                                    {
                                        frmNotify dialog = new frmNotify(printCNCopy);
                                        dialog.ShowDialog(this);
                                    }
                                    DataTable dt = printCNCopy.otherData;
                                    Hardware.printTermal(dt);
                                }
                            }
                        }
                    }
                }

                StoreResult result = process.updatePrintTempDlyptrans();
                if (!result.response.next)
                {
                    process.rollback();
                    btnEnable();
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                    string helpMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                    dialog.ShowDialog(this);
                    return false;
                }
                return true;
            }
            catch (NetworkConnectionException net)
            {
                btnEnable();
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            return true;
        }

        public bool OpenCashDrawer()
        {
            //Open Cash Drawer
            AppLog.writeLog("1 chkSwOpen = " + chkSwOpen);
            if (ProgramConfig.hasDrawer)
            {
                chkSwOpen = Hardware.openDrawer();
                if (chkSwOpen == true)
                {
                    openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
                    Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Return_OpenDrawerAndRecordTime);
                    if (checkOpenDrawer.policy == PolicyStatus.Work)
                    {
                        StoreResult res = process.updateOpenCashDrawer(openTime);
                        if (!res.response.next)
                        {
                            process.rollback();
                            btnEnable();
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                            return false;
                        }
                        else
                        {
                            if (res.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }
                        }
                    }
                }
                else
                {
                    openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
                    Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Return_OpenDrawerAndRecordTime);
                    if (checkOpenDrawer.policy == PolicyStatus.Work)
                    {
                        StoreResult res = process.updateOpenCashDrawer(openTime);
                        if (!res.response.next)
                        {
                            process.rollback();
                            btnEnable();
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                            return false;
                        }
                        else
                        {
                            if (res.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }
                        }
                    }
                    chkSwClose = true;
                    statusCloseDrawer = true;
                }
            }
            #region For Test
            else
            {
                openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
                Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Return_OpenDrawerAndRecordTime);
                if (checkOpenDrawer.policy == PolicyStatus.Work)
                {
                    StoreResult res = process.updateOpenCashDrawer(openTime);
                    if (!res.response.next)
                    {
                        process.rollback();
                        btnEnable();
                        frmNotify dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        return false;
                    }
                    else
                    {
                        if (res.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                        }
                    }
                }
            }
            #endregion
            return true;

        }
        public bool saveTempReturn()
        {
            try
            {
                process.newTransaction();

                if (returnType == "F")
                {
                    saveTempTable = FULL;
                }
                else if (returnType == "P")
                {
                    saveTempTable = PARTIAL;
                }

                if (!submitFromEDC)
                {
                    DataRow[] dr2 = saveTempTable.Select(" VTY = 'P' AND ISEDC = 'Y'");
                    foreach (DataRow item in dr2)
                    {
                        saveTempTable.Rows.Remove(item);
                    }
                    saveTempTable.AcceptChanges();
                }

                StoreResult updateReturnFSlot = process.updateReturnFSlot(saleRef);
                if (!updateReturnFSlot.response.next)
                {
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                }

                for (int i = 0; i < saveTempTable.Rows.Count; i++)
                {
                    string refNo = saveTempTable.Rows[i]["REF"].ToString();
                    string rec = saveTempTable.Rows[i]["REC"].ToString();
                    string sty = saveTempTable.Rows[i]["STY"].ToString();
                    string vty = saveTempTable.Rows[i]["VTY"].ToString();
                    string pcd = saveTempTable.Rows[i]["PCD"].ToString();
                    string qnt = saveTempTable.Rows[i]["QNT"].ToString();
                    string amt = saveTempTable.Rows[i]["AMT"].ToString();
                    string fds = saveTempTable.Rows[i]["FDS"].ToString();
                    string usr = saveTempTable.Rows[i]["USR"].ToString();
                    string egp = saveTempTable.Rows[i]["EGP"].ToString();
                    string stt = saveTempTable.Rows[i]["STT"].ToString();
                    string stv = saveTempTable.Rows[i]["STV"].ToString();
                    string reason = saveTempTable.Rows[i]["REASON_ID"].ToString();
                    string pdisc = saveTempTable.Rows[i]["PDISC"].ToString();
                    string discid = saveTempTable.Rows[i]["DISCID"].ToString();
                    string discamt = saveTempTable.Rows[i]["DISCAMT"].ToString();
                    string upc = saveTempTable.Rows[i]["UPC"].ToString();
                    string dty = saveTempTable.Rows[i]["DTY"].ToString();

                    StoreResult saveTemp = process.saveTempReturn(refNo, rec, sty, vty, pcd, qnt, amt, fds
                    , usr, egp, stt, stv, reason, pdisc, discid, discamt, upc, dty);
                    if (!saveTemp.response.next)
                    {
                        frmLoading.closeLoading();
                        string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(responseMessage);
                    }
                }                
            }
            catch (NetworkConnectionException net)
            {
                throw;
                //btnEnable();
                //frmLoading.closeLoading();
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    this.Dispose();
                //    DialogResult = System.Windows.Forms.DialogResult.Retry;
                //}
            }
            catch (Exception ex)
            {                
                btnEnable();
                process.rollback();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
                return false;
            }
            return true;
        }

        //public void DrawerClose()
        //{
        //    frmLoading.showLoading();
        //    try
        //    {
        //        if (ProgramConfig.seqOfProcess == 2)
        //        {
        //            Profile check = ProgramConfig.getProfile(FunctionID.Return_CloseDrawerAndRecordTime);
        //            if (check.policy == PolicyStatus.Work)
        //            {
        //                StoreResult resClose = process.saveCloseDrawer(closeTime, ProgramConfig.cnNo);
        //                if (!resClose.response.next)
        //                {
        //                    btnEnable();
        //                    frmLoading.closeLoading();
        //                    frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
        //                    dialog.ShowDialog(this);
        //                    return;
        //                }
        //                else
        //                {
        //                    if (resClose.response == ResponseCode.Information)
        //                    {
        //                        frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
        //                        dialog.ShowDialog(this);
        //                    }

        //                    tranSuccess = true;
        //                }
        //            }
        //            else
        //            {
        //                AppLog.writeLog(check.policy.ToString());
        //                tranSuccess = true;
        //            }
        //            frmLoading.closeLoading();

        //            closeForm();
        //        }
        //    }
        //    catch (NetworkConnectionException net)
        //    {
        //        frmLoading.closeLoading();
        //        bool result = Program.control.RetryConnection(net.errorType);
        //        if (result)
        //        {
        //            DrawerClose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        frmLoading.closeLoading();
        //        frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
        //        dialog.ShowDialog(this);
        //    }
        //}

        public void DrawerStatus(string status)
        {
            AppLog.writeLog("2 chkSwOpen = " + chkSwOpen + " Status = " + status);
            if (chkSwOpen == true)
            {
                if (status.ToUpper() == "FALSE")
                {
                    //Save time Close Cash Drawer
                    chkSwClose = true;
                    statusCloseDrawer = true;
                    closeTime = DateTime.Now.AddMinutes(2).ToString("HHmmss", cultureinfo);
                    frmLoading.closeLoading();
                    closeForm();
                //    Profile check = ProgramConfig.getProfile(FunctionID.Return_CloseDrawerAndRecordTime);
                //    if (check.policy == PolicyStatus.Work)
                //    {
                //        StoreResult resClose = process.saveCloseDrawer(closeTime, ProgramConfig.cnNo);
                //        if (!resClose.response.next)
                //        {
                //            btnEnable();
                //            frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
                //            dialog.ShowDialog(this);
                //            return;
                //        }
                //        else
                //        {
                //            if (resClose.response == ResponseCode.Information)
                //            {
                //                frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
                //                dialog.ShowDialog(this);
                //            }

                //            tranSuccess = true;
                //        }
                //    }
                //    else
                //    {
                //        AppLog.writeLog(check.policy.ToString());
                //        tranSuccess = true;
                //    }

                //    closeForm();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            this.DialogResult = DialogResult.Ignore;
            this.Dispose();
            frmLoading.closeLoading();

        }

        private void frmReturnSuccess_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hardware.clearDrawerListeners();
        }

        private void btnEDCInterface_Click(object sender, EventArgs e)
        {
            if (returnType == "F")
            {
                saveTempTable = FULL;
            }
            else if (returnType == "P")
            {
                saveTempTable = PARTIAL;
            }

            var res = process.selectDLYPTRANS(saleRef, sty: "P", vty: "O");
            if (res.response.next)
            {
                var dr = saveTempTable.Select(" VTY = 'P' AND PCD = '" + res.otherData.Rows[0]["CARD_NO"].ToString().Substring(0, 4) + "'").FirstOrDefault();

                frmEDCProcess fEDC = new frmEDCProcess(EventEDC.Return, dr["AMT"].ToString(), "", "");
                fEDC.ShowDialog(this);

                var EDCResult = fEDC.edcResult;
                if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {                    
                        dr["PCD"] = res.otherData.Rows[0]["CARD_NO"].ToString();
                        saveTempTable.AcceptChanges();

                        var dr2 = saveTempTable.Select(" VTY = 'P' AND PCD = 'RETN'").FirstOrDefault();
                        if (dr2 != null)
                        {
                            double amt = Convert.ToDouble(dr2["AMT"]);
                            dr2["AMT"] = amt - Convert.ToDouble(dr["AMT"].ToString());
                            saveTempTable.AcceptChanges();
                        }

                        
                        //creditCard = EDCResult.creditCard;
                        //paymentCode = EDCResult.paymentCode;
                        //ucTxtAmount.Text = EDCResult.edcAmount.ToString();
                        //ucTxtApprove.Text = EDCResult.ApproveCode;
                        //ucTxtApprove_EnterFromButton(sender, e);
                        fEDC.Dispose();
                        submitFromEDC = true;
                        btnOk_Click(null, null);
                      
                    }
                    catch (Exception)
                    {
                        //TO DO Void EDC
                        saveTempTable.RejectChanges();
                        fEDC.Dispose();
                        process.rollback();
                        btnEnable();
                        frmLoading.closeLoading();
                        return;
                    }
                }
                else if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.No)
                {
                    Utility.AlertMessage(EDCResult.res);
                    process.rollback();
                    btnEnable();
                    frmLoading.closeLoading();
                    return;
                }
            }
            else
            {
                Utility.AlertMessage(ResponseCode.Error, "ไม่มีรายการที่จ่ายด้วย EDC");
            }
        }
    }
}
