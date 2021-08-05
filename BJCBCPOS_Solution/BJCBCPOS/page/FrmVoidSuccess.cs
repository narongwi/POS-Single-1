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
using MMFSAPI;

namespace BJCBCPOS
{
    public partial class frmVoidSuccess : Form
    {
        public frmMainMenu frmMainMenu;
        private bool IsPaint = false;
        private bool tranSuccess = false;
        private bool chkSwOpen = false;
        private bool chkSwClose = false;
        private bool statusCloseDrawer = false;
        string voidReceiptNo;
        string eventName = "VoidReceipt";
        string openTime;
        string closeTime;
        public bool chk;
        string reasonID;
        string reasonTxt;
        string lockNo;
        string userID;
        string member;
        string total;
        string saleType;
        string saleTime;
        Profile chkProfile191;
        DataTable dtVoidDepo = new DataTable();
        string _flagCallAPI = "";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        private VoidProcess process = new VoidProcess();

        public frmVoidSuccess(string receiptNo, string reasonID, string reasonTxt, string lockNo, string userID, string member, string saleTime, string total, string saleType)
        {
            InitializeComponent();
            this.voidReceiptNo = receiptNo;
            this.reasonID = reasonID;
            this.reasonTxt = reasonTxt;
            this.lockNo = lockNo;
            this.userID = userID;
            this.member = member;
            this.total = total;
            this.saleTime = saleTime;
            this.saleType = saleType;
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

        private void frmVoidSuccess_Load(object sender, EventArgs e)
        {
            frmLoading.showLoading();
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
            //lbReceiptNo.Text = "(ใบเสร็จเลขที่ " + voidReceiptNo + " )";
            //lbReceiptNo.Text = string.Format(AppMessage.getMessage(ProgramConfig.language, this.Name, "lbReceiptNo"), voidReceiptNo);
            lbRecepitNoVal.Text = voidReceiptNo;
            lbLockNoResult.Text = lockNo;
            lbCashierIdVal.Text = userID;
            lbMemberName.Text = member; //TO DO
            lbTotalVal.Text = total;
            lbReasonVal.Text = reasonTxt;
            lbVoidVal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cultureinfo);
            lbSaleTimeVal.Text = saleTime;
            chkProfile191 = ProgramConfig.getProfile(FunctionID.Void_OpenDrawerAndRecordTime);
            frmLoading.closeLoading();                  
        }

        public void btnEnable()
        {
            panel_button.BringToFront();
            btnCancel.Enabled = true;
            btnOk.Enabled = true;
        }

        public void btnDisable()
        {
            btnCancel.Enabled = false;
            btnOk.Enabled = false;
        }

        private bool printReceipt()
        {
            try
            {
                if (saleType == VoidSaleType.CreditSale)
                {
                    process.commit();
                    //CancelPayInvoiceAR
                    var res = process.CallAPICancelPayInvoiceAR(voidReceiptNo);
                    if (!res.response.next)
                    {
                        frmNotify dialog = new frmNotify(res);
                        dialog.ShowDialog(this);

                        res = process.clearUp(FunctionID.CreditSale_CancelVoid, voidReceiptNo);
                        if (!res.response.next)
                        {
                            dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                        }

                        process.rollback();
                        btnEnable();
                        return false;
                    }

                    res = process.CredPayPrintVoid(voidReceiptNo);
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

                        DataTable dt = res.otherData;
                        Hardware.printTermal(dt);
                    }
                }
                else if (saleType == VoidSaleType.POD)
                {
                    
                    //CancelPayInvoicePOD

                    if (_flagCallAPI == "Y")
                    {
                        process.commit();
                        var res = process.CallAPICancelPayInvoicePOD(voidReceiptNo);
                        if (!res.response.next)
                        {
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);

                            res = process.clearUp(FunctionID.ReceivePOD_CancelVoid, voidReceiptNo);
                            if (!res.response.next)
                            {
                                dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }

                            process.rollback();
                            btnEnable();
                            return false;
                        }
                    }
                    
                    //pos_PODPrintVoid
                    var res2 = process.PODPrintVoid(voidReceiptNo);
                    if (!res2.response.next)
                    {
                        process.rollback();
                        btnEnable();
                        frmNotify dialog = new frmNotify(res2);
                        dialog.ShowDialog(this);
                        return false;
                    }
                    else
                    {
                        if (res2.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(res2);
                            dialog.ShowDialog(this);
                        }

                        DataTable dt = res2.otherData;
                        Hardware.printTermal(dt);
                    }
                }
                else if (saleType == VoidSaleType.Deposit)
                {
                    string printType = ProgramConfig.getPosConfig("PrintDepositCreditNote").ToString();
                    if (printType == "1")
                    {
                        process.commit();
                        ProgramConfig.cnNo = dtVoidDepo.Rows[0]["CNNUM_INI"].ToString();
                        ProgramConfig.running.updateValue();
                        Hardware.printCN(dtVoidDepo.Rows[0]["CNNO"].ToString());
                    }
                    else
                    {
                        var res = process.printCN(dtVoidDepo.Rows[0]["CNNO"].ToString());
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

                            Hardware.printTermal(res.otherData);
                        }
                    }
                }
                else
                {
                    Profile checkPrint = ProgramConfig.getProfile(FunctionID.Void_PrintVoidDocument);
                    if (checkPrint.policy == PolicyStatus.Work)
                    {
                        StoreResult result = process.PrintVoidReceipt(voidReceiptNo);
                        if (!result.response.next)
                        {
                            process.rollback();
                            btnEnable();
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                            return false;
                        }
                        else
                        {
                            if (result.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(result);
                                dialog.ShowDialog(this);
                            }

                            DataTable dt = result.otherData;
                            Hardware.printTermal(dt);
                        }
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                btnEnable();
                process.rollback();
                frmLoading.closeLoading();
                throw;
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    return false;
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            btnDisable();
            try
            {                
                Profile checkAuth = ProgramConfig.getProfile(FunctionID.Void_InputUserApproveVoid);
                if (checkAuth.policy == PolicyStatus.Work)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("VoidSuccess", checkAuth.diffUserStatus);
                    //auth.function = FunctionID.Void_InputUserApproveVoid;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    btnEnable();
                    //    frmLoading.closeLoading();
                    //    return;
                    //}


                    if (!Utility.CheckAuthPass(this, checkAuth, "VoidSuccess"))
                    {
                        btnEnable();
                        frmLoading.closeLoading();
                        return;
                    }
                    frmLoading.closeLoading();
                    runningProcess();

                }
                else
                {
                    frmLoading.closeLoading();
                    runningProcess();
                }                
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                frmLoading.closeLoading();
                btnEnable();
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
            frmLoading.closeLoading();
        }

        public void runningProcess()
        {
            try
            {
            
                var res = process.selectDLYPTRANS(voidReceiptNo, vty: "P", dty: "O");
                if (res.response.next)
                {
                    frmEDCProcess fEDC = new frmEDCProcess(EventEDC.Void, "", "", ""
                                            , invoiceNo: res.otherData.Rows[0]["TRACE_NO"].ToString()
                                            , approveCode: res.otherData.Rows[0]["APPROVE_CODE"].ToString());

                    fEDC.ShowDialog(this);

                    var EDCResult = fEDC.edcResult;
                    if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            //creditCard = EDCResult.creditCard;
                            //paymentCode = EDCResult.paymentCode;
                            //ucTxtAmount.Text = EDCResult.edcAmount.ToString();
                            //ucTxtApprove.Text = EDCResult.ApproveCode;
                            //ucTxtApprove_EnterFromButton(sender, e);
                            fEDC.Dispose();
                        }
                        catch (Exception)
                        {
                            //TO DO Void EDC
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



                res = process.processCheckQRPayment(voidReceiptNo, AlertMessage: (resCode, resMsg, resHelpMsg) => 
                                                            Utility.AlertMessage(this, resCode, resMsg, resHelpMsg));
                if (!res.response.next || res.response == ResponseCode.Ignore)
                {
                    process.rollback();
                    btnEnable();
                    frmLoading.closeLoading();
                    return;
                }

                process.newTransaction();
                //DefaultStatus
                statusCloseDrawer = false;
                chkSwClose = false;
                tranSuccess = false;
                frmLoading.showLoading();
                if (ProgramConfig.hasDrawer && chkProfile191.policy == PolicyStatus.Work)
                {
                    if (OpenCashDrawer())
                    {
                        if (saleType == VoidSaleType.NormalSale || saleType == VoidSaleType.Deposit)
                        {
                            //TO DO #686
                            Profile checkVpod = ProgramConfig.getProfile(FunctionID.Void_ProcessAfterVoidTransaction);
                            if (checkVpod.policy == PolicyStatus.Work)
                            {
                                StoreResult checkProcess = process.concludeVoid(voidReceiptNo);
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
                        }

                        if (!printReceipt())
                        {
                            process.rollback();
                            btnEnable();
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
                else
                {
                    if (OpenCashDrawer())
                    {
                        frmLoading.showLoading();
                        Profile checkVpod = ProgramConfig.getProfile(FunctionID.Void_ProcessAfterVoidTransaction);
                        if (checkVpod.policy == PolicyStatus.Work)
                        {
                            StoreResult checkProcess = process.concludeVoid(voidReceiptNo);
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

                        Profile checkPrint = ProgramConfig.getProfile(FunctionID.Void_PrintVoidDocument);
                        if (checkPrint.policy == PolicyStatus.Work)
                        {
                            if (!printReceipt())
                            {
                                process.rollback();
                                btnEnable();
                                return;
                            }
                        } 
                        process.commit();
                        frmLoading.closeLoading();

                        //if (Hardware.isDrawerOpen)
                        //{
                        //    panel_message.BringToFront();
                        //    //frmNotify dialog = new frmNotify("ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ");
                        //    //dialog.ShowDialog(this);
                        //    //dialog.Refresh();
                        //    //return;
                        //}
                        //else
                        //{
                            chkSwClose = true;
                            statusCloseDrawer = true;
                            tranSuccess = true;
                            closeForm();
                        //}
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                throw;
                //btnEnable();
                //frmLoading.closeLoading();
                //Program.control.RetryConnection(net.errorType);                
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

        private void closeForm()
        {
            frmLoading.showLoading();
            try
            {
                if (tranSuccess == true && chkSwClose == true && statusCloseDrawer == true)
                {
                    process.newTransaction();
                    if (ProgramConfig.hasDrawer && chkProfile191.policy == PolicyStatus.Work)
                    {
                        Profile check = ProgramConfig.getProfile(FunctionID.Void_CloseDrawerAndRecordTime);
                        if (check.policy == PolicyStatus.Work)
                        {
                            StoreResult resClose = process.saveCloseDrawer(FunctionID.Void_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
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
                        SaveDraweTrans(false);
                    }
    
                    Profile checkDataT = ProgramConfig.getProfile(FunctionID.Void_SaveVoidTransaction_SynchSaleTransactiontoDataTank);
                    if (checkDataT.policy == PolicyStatus.Work)
                    {
                        string refNo = ProgramConfig.voidRefNo;
                        string rec = "1";

                        StoreResult checkDataTank = process.syncToDataTank(eventName, FunctionID.Void_SaveVoidTransaction_SynchSaleTransactiontoDataTank, refNo, rec, voidReceiptNo);
                        if (!checkDataTank.response.next)
                        {
                            frmNotify dialog = new frmNotify(checkDataTank);
                            dialog.ShowDialog(this);
                            process.commit();

                            //Program.control.ShowForm("frmMainMenu");
                            Program.control.CloseForm("frmVoid");
                            Program.control.CloseForm("frmVoidSuccess");
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
                        }
                        else
                        {
                            if (checkDataTank.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(checkDataTank);
                                dialog.ShowDialog(this);
                            }
                            process.commit();

                            //Program.control.ShowForm("frmMainMenu");
                            Program.control.CloseForm("frmVoid");
                            Program.control.CloseForm("frmVoidSuccess");
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
                throw;
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    closeForm();
                //}
            }
            catch (Exception ex)
            {
                process.rollback();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public bool OpenCashDrawer()
        {
            //Open Cash Drawer
            try
            {
                AppLog.writeLog("1 chkSwOpen = " + chkSwOpen);
                if (ProgramConfig.hasDrawer && chkProfile191.policy == PolicyStatus.Work)
                {
                    chkSwOpen = Hardware.openDrawer();
                    SaveDraweTrans(true);
                    if (chkSwOpen == true)
                    {
                        openTime = DateTime.Now.ToString("HH:mm:ss", cultureinfo);
                        //Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Void_OpenDrawerAndRecordTime);
                        //if (checkOpenDrawer.policy == PolicyStatus.Work)
                        //{
                            StoreResult res = process.saveVoidTransaction(voidReceiptNo, openTime, reasonID, saleType);
                            if (!res.response.next)
                            {
                                process.rollback();
                                btnEnable();
                                frmLoading.closeLoading();
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                                return false;
                            }
                            else
                            {
                                if (saleType == VoidSaleType.Deposit)
                                {
                                    dtVoidDepo = res.otherData;
                                }

                                if (saleType == VoidSaleType.POD)
                                {
                                    _flagCallAPI = res.otherData.Rows[0]["Flag_CallAPI"].ToString();
                                }

                                if (res.response == ResponseCode.Information)
                                {
                                    frmNotify dialog = new frmNotify(res);
                                    dialog.ShowDialog(this);
                                }
                            }
                        //}
                    }
                    else
                    {
                        openTime = DateTime.Now.ToString("HH:mm:ss", cultureinfo);
                        //Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Void_OpenDrawerAndRecordTime);
                        //if (checkOpenDrawer.policy == PolicyStatus.Work)
                        //{
                            StoreResult res = process.saveVoidTransaction(voidReceiptNo, openTime, reasonID, saleType);
                            if (!res.response.next)
                            {
                                process.rollback();
                                btnEnable();
                                frmLoading.closeLoading();
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                                return false;
                            }
                            else
                            {
                                if (saleType == VoidSaleType.Deposit)
                                {
                                    dtVoidDepo = res.otherData;
                                }

                                if (saleType == VoidSaleType.POD)
                                {
                                    _flagCallAPI = res.otherData.Rows[0]["Flag_CallAPI"].ToString();
                                }

                                if (res.response == ResponseCode.Information)
                                {
                                    frmNotify dialog = new frmNotify(res);
                                    dialog.ShowDialog(this);
                                }
                            }
                        //}
                        chkSwClose = true;
                        statusCloseDrawer = true;
                    }
                }
                else
                {
                    openTime = DateTime.Now.ToString("HH:mm:ss", cultureinfo);
                    //Profile checkOpenDrawer = ProgramConfig.getProfile(FunctionID.Void_OpenDrawerAndRecordTime);
                    //if (checkOpenDrawer.policy == PolicyStatus.Work)
                    //{
                    StoreResult res = process.saveVoidTransaction(voidReceiptNo, openTime, reasonID, saleType);
                        if (!res.response.next)
                        {
                            process.rollback();
                            btnEnable();
                            frmLoading.closeLoading();
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                            return false;
                        }
                        else
                        {
                            if (saleType == VoidSaleType.Deposit)
                            {
                                dtVoidDepo = res.otherData;
                            }

                            if (saleType == VoidSaleType.POD)
                            {
                                _flagCallAPI = res.otherData.Rows[0]["Flag_CallAPI"].ToString();
                            }

                            if (res.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }
                        }
                    //}
                }
            }
            catch (NetworkConnectionException net)
            {
                btnEnable();
                frmLoading.closeLoading();
                throw;
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                process.rollback();
                btnEnable();
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            return true;
        }

        private void SaveDraweTrans(bool isOpen)
        {
            FunctionID fn = FunctionID.NoFunctionID;

            if (isOpen)
            {
                if (saleType == VoidSaleType.NormalSale)
                {
                    fn = FunctionID.Void_OpenDrawerAndRecordTime;
                }
                else if (saleType == VoidSaleType.Deposit)
                {
                    fn = FunctionID.Void_Deposit_OpenDrawerAndRecordTime;
                }
                else if (saleType == VoidSaleType.POD)
                {
                    fn = FunctionID.Void_POD_OpenDrawerAndRecordTime;
                }
                else if (saleType == VoidSaleType.CreditSale)
                {
                    fn = FunctionID.Void_Credit_OpenDrawerAndRecordTime;
                }
            }
            else
            {
                if (saleType == VoidSaleType.NormalSale)
                {
                    fn = FunctionID.Void_CloseDrawerAndRecordTime;
                }
                else if (saleType == VoidSaleType.Deposit)
                {
                    fn = FunctionID.Void_Deposit_CloseDrawerAndRecordTime;
                }
                else if (saleType == VoidSaleType.POD)
                {
                    fn = FunctionID.Void_POD_CloseDrawerAndRecordTime;
                }
                else if (saleType == VoidSaleType.CreditSale)
                {
                    fn = FunctionID.Void_Credit_CloseDrawerAndRecordTime;
                }
            }

            process.SaveDrawerTrans(fn);
        }

        public void DrawerClose()
        {
            frmLoading.showLoading();
            try
            {
                Profile check = ProgramConfig.getProfile(FunctionID.Void_CloseDrawerAndRecordTime);
                if (check.policy == PolicyStatus.Work)
                {
                    StoreResult resClose = process.saveCloseDrawer(FunctionID.Void_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
                    if (!resClose.response.next)
                    {
                        btnEnable();
                        frmLoading.closeLoading();
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

                        tranSuccess = true;
                    }
                }
                else
                {
                    AppLog.writeLog(check.policy.ToString());
                    tranSuccess = true;
                }
                frmLoading.closeLoading();
                closeForm();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                throw;
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    DrawerClose();
                //}
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void DrawerStatus(string status)
        {
            AppLog.writeLog("2 chkSwOpen = " + chkSwOpen + " Status = " + status);
            if (chkSwOpen == true)
            {
                if (status.ToUpper() == "FALSE")
                {
                    chkSwClose = true;
                    statusCloseDrawer = true;
                    //Save time Close Cash Drawer
                    closeTime = DateTime.Now.AddMinutes(2).ToString("HHmmss", cultureinfo);
                    frmLoading.closeLoading();
                    //DrawerClose();
                    closeForm();
                    //Profile check = ProgramConfig.getProfile(FunctionID.Void_CloseDrawerAndRecordTime);
                    //if (check.policy == PolicyStatus.Work)
                    //{
                    //    StoreResult resClose = process.saveCloseDrawer(FunctionID.Void_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
                    //    if (!resClose.response.next)
                    //    {
                    //        btnEnable();
                    //        frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
                    //        dialog.ShowDialog(this);
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        if (resClose.response == ResponseCode.Information)
                    //        {
                    //            frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
                    //            dialog.ShowDialog(this);
                    //        }

                    //        tranSuccess = true;
                    //    }
                    //}
                    //else
                    //{
                    //    AppLog.writeLog(check.policy.ToString());
                    //    tranSuccess = true;
                    //}

                    //closeForm();
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Dispose();
        }

        private void frmVoidSuccess_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hardware.clearDrawerListeners();
        }
    }
}
