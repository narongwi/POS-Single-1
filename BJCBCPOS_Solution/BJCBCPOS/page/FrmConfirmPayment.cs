
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
using System.Collections;
using System.Text.RegularExpressions;
using MMFSAPI;

namespace BJCBCPOS
{
    public partial class frmConfirmPayment : Form
    {
        private SaleProcess process = new SaleProcess();
        public frmPayment frmPay;
        public frmSale frmSale;
        public frmMonitorCustomer frmMonitor;
        public frmMonitorCustomerFooter frmMoniterFooter;
        public frmMonitor2Detail frm2Detail;
        public string lastPMCode;
        private bool IsPaint = false;
        private bool tranSuccess = false;
        private bool chkSwOpen = false;
        private bool chkSwClose = false;
        private bool drawerOpenStatus = false;
        private bool statusDrawer = false;
        private bool finishReceipt = false;
        private string statusConnect = "0";
        private bool statusCloseDrawer = false;
        public DataTable dtChange = new DataTable();
        private string _lbConfirmCash;
        private string _lbConfirmPayment;
        private string _lbConfirmBalance;
        private string _lbConfirmChange;
        private string _oldChange;
        private bool _isOffline;
        private Point OriLocationBtnOK = new Point(260, 24);
        private Point MiddleLocationBtnOK = new Point(196, 23);

        Timer t1 = null;

        string timeout = "";
        string openTime = "";
        string closeTime = "";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");


        #region Property

        [Category("Custom Property")]
        [Description("Set label text cash.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbConfirmCash
        {
            get
            {
                return _lbConfirmCash;
            }
            set
            {
                _lbConfirmCash = value;
                lbTxtCash.Text = _lbConfirmCash;
                lbTxtCashCurrency.Text = _lbConfirmCash;
            }
        }

        [Category("Custom Property")]
        [Description("Set label text payment.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbConfirmPayment
        {
            get
            {
                return _lbConfirmPayment;
            }
            set
            {
                _lbConfirmPayment = value;
                lbTxtPayment.Text = _lbConfirmPayment;
                lbTxtPaymentCurrency.Text = _lbConfirmPayment;
            }
        }

        [Category("Custom Property")]
        [Description("Set label text balance.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string lbConfirmBalance
        {
            get
            {
                return _lbConfirmBalance;
            }
            set
            {
                _lbConfirmBalance = value;
                lbTxtChange.Text = _lbConfirmBalance;
                lbTxtChangeCurrency.Text = _lbConfirmBalance;
            }
        }

        [Category("Custom Property")]
        [Description("Set label text change.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string txtConfirmChange
        {
            get
            {
                return _lbConfirmChange;
            }
            set
            {
                _lbConfirmChange = value;
                lbTxtChange.Text = _lbConfirmChange;
                lbTxtChangeCurrency.Text = _lbConfirmChange; 
                //ucTxtChange.Text = _lbConfirmChange;
                //ucTxtChangeCurrency.Text = _lbConfirmChange;
            }
        }


        #endregion

        public frmConfirmPayment()
        {
            InitializeComponent();
        }

        public frmConfirmPayment(bool isOffline)
        {
            InitializeComponent();
            _isOffline = isOffline;
        }

        private void frmConfirmPayment_Load(object sender, EventArgs e)
        {
            frmPay = (frmPayment)this.Owner;
            try
            {
                ucTxtChange.Text = "";
                ucTxtChangeCurrency.Text = "";

                if (frmPay._hpContract.Trim() != "")
                {
                    lbText.Text = AppMessage.getMessage(ProgramConfig.language, "FrmConfirmPayment", "Contract") + " : " + frmPay._hpContract;
                }
                int height = 0;
                int cnt = 0;
                pn_PaymentNormal.Visible = false;
                pn_PaymentCurrency.Visible = false;
                if (dtChange.Rows.Count > 0 && dtChange.AsEnumerable().Any(a => a["ChangeStatus"].ToString() == "Y") && dtChange.AsEnumerable().Any(a => a["FXCU_CODE"].ToString() != "N/A"))
                {
                    pn_PaymentCurrency.Visible = true;
                    pn_PaymentCurrency.BringToFront();

                    for (int i = 0; i < dtChange.Rows.Count; i++)
                    {
                        if (dtChange.Rows[i]["ChangeStatus"].ToString() == "Y" && dtChange.Rows[i]["FXCU_CODE"].ToString() != "N/A")
                        {
                            cnt++;
                            UCConfirmPaymentFXCU ucpm = new UCConfirmPaymentFXCU();
                            ucpm.lbExchangeRate.Text = "Exchange Rate : " + dtChange.Rows[i]["EXCG_RATE"].ToString();
                            ucpm.lbNameCurrency.Text = dtChange.Rows[i]["FULLNAME"].ToString();
                            ucpm.lbChangeCurrency.Text = Convert.ToDouble(dtChange.Rows[i]["CHG_DISP"]).ToString(ProgramConfig.amountFormatString);
                            ucpm.lbCurrency.Text = dtChange.Rows[i]["FXCU_CODE"].ToString();

                            pn_Currency.Controls.Add(ucpm);
                            pn_Currency.Controls.SetChildIndex(ucpm, 0);

                            if (i == 0)
                            {
                                frmPay.moni2.lbCurrencyRate1.Text = dtChange.Rows[i]["FXCU_CODE"].ToString() + " " + dtChange.Rows[i]["EXCG_RATE"].ToString();
                                frmPay.moni2.lbChangeCurrency1.Text = ucpm.lbChangeCurrency.Text;
                            }
                            else if (i == 1)
                            {
                                frmPay.moni2.lbCurrencyRate2.Text = dtChange.Rows[i]["FXCU_CODE"].ToString() + " " + dtChange.Rows[i]["EXCG_RATE"].ToString();
                                frmPay.moni2.lbChangeCurrency2.Text = ucpm.lbChangeCurrency.Text;
                            }
                            else if (i == 2)
                            {
                                frmPay.moni2.lbCurrencyRate3.Text = dtChange.Rows[i]["FXCU_CODE"].ToString() + " " + dtChange.Rows[i]["EXCG_RATE"].ToString();
                                frmPay.moni2.lbChangeCurrency3.Text = ucpm.lbChangeCurrency.Text;
                            }
                        }
                    }

                    height = (cnt - 1) * 75;

                    Utility.SetGridColorAlternate<UCConfirmPaymentFXCU>(pn_Currency.Controls.Cast<UCConfirmPaymentFXCU>().ToList(), Color.FromArgb(200, 255, 202));
                    pn_PaymentCurrency.Height = pn_PaymentCurrency.Height + height;

                }
                else
                {
                    pn_PaymentNormal.Visible = true;
                    pn_PaymentNormal.BringToFront();
                }

                lbConfirmBalance = dtChange.AsEnumerable().Sum(s => Convert.ToDouble(s["EXCG_AMT_DISP"])).ToString(ProgramConfig.amountFormatString);
                _oldChange = lbConfirmBalance;

                if (this.Owner != null)
                {
                    this.Size = this.Owner.Size;

                    int x = (this.Size.Width / 2) - (pn_PaymentNormal.Size.Width / 2);
                    int y = (this.Size.Height / 2) - (pn_PaymentNormal.Size.Height / 2);

                    int x2 = (this.Size.Width / 2) - (pn_PaymentCurrency.Size.Width / 2);
                    int y2 = (this.Size.Height / 2) - (pn_PaymentCurrency.Size.Height / 2);

                    pn_PaymentNormal.Location = new Point(x, y - 50);
                    pn_PaymentCurrency.Location = new Point(x2, y2 - 50);
                    this.Location = this.Owner.Location;
                }
                else
                {
                    this.Size = new Size(1024, 768);
                    int x = 512 - (pn_PaymentNormal.Size.Width / 2);
                    int y = 384 - (pn_PaymentNormal.Size.Height / 2);

                    int x2 = 512 - (pn_PaymentNormal.Size.Width / 2);
                    int y2 = 384 - (pn_PaymentNormal.Size.Height / 2);

                    pn_PaymentNormal.Location = new Point(x, y - 50);
                    pn_PaymentCurrency.Location = new Point(x2, y2 - 50);
                    this.Location = new Point(0, 0);
                }

                //if (_isOffline)
                //{
                //    btnCancel.Visible = true;
                //    btnCancelCurrency.Visible = true;
                //    btnOk.Location = OriLocationBtnOK;
                //}
                //else
                //{
                //    btnCancel.Visible = false;
                //    btnCancelCurrency.Visible = false;
                //    btnOk.Location = MiddleLocationBtnOK;
                //}

                Profile check = ProgramConfig.getProfile(FunctionID.Sale_Change_EditChange); //#141
                if (check.policy == PolicyStatus.Work)
                {
                    //Fix Language
                    //if (Utility.CheckAuthPass(this, check, "Edit Change")) //#44
                    //{
                        btnEditChange.Visible = true;
                        btnEditChangeCurrency.Visible = true;
                    //}
                }

                Hardware.addDrawerListeners(DrawerStatus);
                panel_button.BringToFront();
                panel_button2.BringToFront();
                btnOk.Select();
                btnOkCurrency.Select();
                
                chkSwOpen = Hardware.openDrawer();
                process.SaveDrawerTrans(ProgramConfig.paymentOpenCashDrawer);
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        public void btnOk_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtConfirmChange != null && Convert.ToDouble(txtConfirmChange) < Convert.ToDouble(lbConfirmBalance))
                {
                    Utility.AlertMessage(ResponseCode.Error, "ไม่สามารถแก้ไขเงินทอนน้อยกว่าเงินทอนปัจจุบันได้");
                    return;
                }
                StoreResult chkMinCash = process.checkMinCashUnitAmount("CASH", lbTxtChange.Text, ProgramConfig.currencyDefault, "2");
                if (chkMinCash.response.next)
                {
                    statusConnect = "0";
                    //statusCloseDrawer = false;
                    drawerOpenStatus = false;
                    //chkSwClose = false;
                    tranSuccess = false;
                    finishReceipt = false;

                    if (ProgramConfig.hasDrawer)
                    {
                        //chkSwOpen = Hardware.openDrawer();
                        AppLog.writeLog("frmConfirmPayment btnOk_Click chkSwOpen" + chkSwOpen);
                        if (chkSwOpen == true)
                        {
                            openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
                            frmLoading.showLoading();

                            DataTable dt = new DataTable();
                            dt = BaseProcess.dtSaleMain.Copy();

                            StoreResult result = process.saveConfirmPayment(openTime, Convert.ToDouble(lbConfirmBalance), Convert.ToDouble(txtConfirmChange),
                                                                            AlertMessage: (resCode, resMsg, resHelpMsg) => Utility.AlertMessage(this, resCode, resMsg, resHelpMsg));
                            frmLoading.closeLoading();
                            if (result.response == ResponseCode.Success)
                            {
                                try
                                {
                                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale)
                                    {
                                        result = process.PrintExport(dt);
                                        if (result.response.next && result.otherData != null && result.otherData.Rows.Count > 0)
                                        {
                                            DataTable dt1 = result.otherData;
                                            Hardware.printTermal(dt1);
                                        }
                                        else
                                        {
                                            if (!result.response.next)
                                            {
                                                frmNotify dialog = new frmNotify(result);
                                                dialog.ShowDialog(this);
                                            }
                                        }
                                    }

                                    AppLog.writeLog("frmConfirmPayment btnOk_Click Hardware.isDrawerOpen" + Hardware.isDrawerOpen);
                                    if (Hardware.isDrawerOpen)
                                    {
                                        panel_message.BringToFront();
                                        panel_message2.BringToFront();
                                        TimerProcess();
                                    }
                                    tranSuccess = true;
                                    AppLog.writeLog("btnOk1 tranSuccess = " + tranSuccess + " chkSwClose = " + chkSwClose + " statusClose = " + statusCloseDrawer);
                                    closeForm();

                                }
                                catch (NetworkConnectionException net)
                                {
                                    Utility.CheckRunningNumber();
                                    Program.control.RetryConnection(net.errorType);
                                    //frmNotify dialog = new frmNotify(ResponseCode.Error, net.Message, "");
                                    //dialog.ShowDialog(this);
                                }
                                catch (Exception ex)
                                {
                                    Utility.AlertMessage(ResponseCode.Error, ex.Message);
                                    Utility.AutoVoidEDC(this, (vty, dty) => process.selectDLYPTRANS(ProgramConfig.abbNo, vty, dty));
                                    return;
                                }
                            }
                            else
                            {
                                tranSuccess = false;
                                if (result.response == ResponseCode.Ignore)
                                {
                                    return;
                                }
                                frmNotify dialog = new frmNotify(result);
                                dialog.ShowDialog(this);
                                return;
                            }
                        }
                        else
                        {
                            openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
                            ProcessCashDrawerClose();
                        }

                        #region Code Deleted

                        //if (!Hardware.isDrawerOpen)
                        //{
                        //    chkSwOpen = Hardware.openDrawer();
                        //    if (chkSwOpen == true)
                        //    {
                        //        openTime = DateTime.Now.ToString("HHmmss", cultureinfo);

                        //        frmLoading.showLoading();
                        //        StoreResult result = process.saveConfirmPayment(openTime);
                        //        frmLoading.closeLoading();
                        //        if (result.response.next && seqOfProcess == 1)
                        //        {
                        //            printReceipt();

                        //            ProgramConfig.memberId = "";
                        //            ProgramConfig.memberName = "";

                        //            //OpenCashDrawer();

                        //            if (Hardware.isDrawerOpen)
                        //            {
                        //                panel_message.BringToFront();
                        //            }
                        //            else
                        //            {
                        //                chkSwClose = false;
                        //                tranSuccess = true;
                        //                closeForm();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            frmNotify dialog = new frmNotify(result.response, result.responseMessage, result.helpMessage);
                        //            dialog.ShowDialog(this);
                        //            return;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        openTime = DateTime.Now.ToString("HHmmss", cultureinfo);

                        //        frmLoading.showLoading();
                        //        StoreResult result = process.saveConfirmPayment(openTime);
                        //        frmLoading.closeLoading();
                        //        if (result.response.next && seqOfProcess == 1)
                        //        {
                        //            printReceipt();

                        //            ProgramConfig.memberId = "";
                        //            ProgramConfig.memberName = "";

                        //            //OpenCashDrawer();

                        //            if (Hardware.isDrawerOpen)
                        //            {
                        //                panel_message.BringToFront();
                        //            }
                        //            else
                        //            {
                        //                chkSwClose = true;
                        //                tranSuccess = true;
                        //                closeForm();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            frmNotify dialog = new frmNotify(result.response, result.responseMessage, result.helpMessage);
                        //            dialog.ShowDialog(this);
                        //            return;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    string responseMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").message;
                        //    string helpMessage = ProgramConfig.message.get("frmCashOut", "CloseDrawer").help;
                        //    frmNotify notify = new frmNotify(ResponseCode.CloseDrawer, responseMessage, helpMessage);

                        //    //notify = new frmNotify(ResponseCode.Success, "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำรายการต่อ");
                        //    notify.btnOK.Visible = true;
                        //    notify.ShowDialog(this);

                        //}
                        #endregion
                    }
                    else
                    {
                        openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
                        ProcessCashDrawerClose();
                    }
                }
                else
                {
                    Utility.AlertMessage(chkMinCash);
                    return;
                }
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
            catch (Exception ex)
            {
                Utility.AlertMessage(ResponseCode.Error, ex.Message);
                Utility.AutoVoidEDC(this, (vty, dty) => process.selectDLYPTRANS(ProgramConfig.saleRefNo, vty, dty));
            }
        }

        private void ProcessCashDrawerClose()
        {
            frmLoading.showLoading();
            DataTable dt = new DataTable();
            dt = BaseProcess.dtSaleMain.Copy();

            StoreResult result = process.saveConfirmPayment(openTime, Convert.ToDouble(lbConfirmBalance), Convert.ToDouble(txtConfirmChange),
                                                            AlertMessage: (resCode, resMsg, resHelpMsg) => Utility.AlertMessage(this, resCode, resMsg, resHelpMsg));
            frmLoading.closeLoading();
            if (result.response == ResponseCode.Success)
            {
                try
                {
                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale)
                    {
                        result = process.PrintExport(dt);
                        if (result.response.next && result.otherData != null && result.otherData.Rows.Count > 0)
                        {
                            DataTable dt1 = result.otherData;
                            Hardware.printTermal(dt1);
                        }
                        else
                        {
                            if (!result.response.next)
                            {
                                frmNotify dialog = new frmNotify(result);
                                dialog.ShowDialog(this);
                            }
                        }
                    }

                    AppLog.writeLog("frmConfirmPayment ProcessCashDrawerClose Hardware.isDrawerOpen" + Hardware.isDrawerOpen);

                    if (Hardware.isDrawerOpen)
                    {
                        panel_message.BringToFront();
                        panel_message2.BringToFront();
                        TimerProcess();
                    }
                    chkSwClose = true;
                    tranSuccess = true;
                    statusCloseDrawer = true;
                    closeForm();
                }
                catch (NetworkConnectionException net)
                {
                    Utility.CheckRunningNumber();
                    Program.control.RetryConnection(net.errorType);
                    //frmNotify dialog = new frmNotify(ResponseCode.Error, net.Message, "");
                    //dialog.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    Utility.AlertMessage(ResponseCode.Error, ex.Message);
                    Utility.AutoVoidEDC(this, (vty, dty) => process.selectDLYPTRANS(ProgramConfig.abbNo, vty, dty));
                    return;
                }
            }
            else
            {
                tranSuccess = false;
                if (result.response == ResponseCode.Ignore)
                {
                    return;
                }
                frmNotify dialog = new frmNotify(result);
                dialog.ShowDialog(this);
                return;
            }
        }

        private void RedeemCoupon()
        {
            if (ProgramConfig.memberId.Trim() != "" && ProgramConfig.memberId != "N/A")
            {
                frmRedeem frm = new frmRedeem(RedeemPage.Coupon);
                frm.ShowDialog(this);
                frm = null;
                this.BringToFront();
                this.Refresh();
            }
        }

        //public void kickDrawer()
        //{
        //    openTime = DateTime.Now.ToString("HHmmss", cultureinfo);
        //    chkSwOpen = Hardware.openDrawer();
        //    StoreResult res = process.updateOpenCashDrawer(openTime);
        //    if (res.response == ResponseCode.Error)
        //    {
        //        frmNotify dialog = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
        //        dialog.ShowDialog(this);
        //        return;
        //    }

        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancelClick();
        }

        private void btnCancelClick()
        {
            try
            {
                #region Old Code
                //#region Remove CHGD
                //string maxRecCHGD = process.selectMaxRecTempDlyptransForTypeP_FormPMCODE("CHGD").ToString();
                //DataTable paymentTypeCHGD = process.selectTypeFromMaxRec(maxRecCHGD);
                //if (paymentTypeCHGD.Rows.Count > 0)
                //{
                //    ProgramConfig.paymentType = paymentTypeCHGD.Rows[0]["PCD"].ToString();
                //    ProgramConfig.paymentAmt = paymentTypeCHGD.Rows[0]["AMT"].ToString();
                //}
                //else
                //{
                //    ProgramConfig.paymentType = "";
                //    ProgramConfig.paymentAmt = "";
                //}

                //StoreResult resCHGD = process.deletePaymentType(maxRecCHGD);
                //if (!resCHGD.response.next)
                //{
                //    frmNotify dialog = new frmNotify(resCHGD.response, resCHGD.responseMessage, resCHGD.helpMessage);
                //    dialog.ShowDialog(this);
                //    return;
                //}

                //#endregion

                //#region Remove FXCU Diff

                //string maxRecFXCUDiff = process.selectMaxRecTempDlyptransForTypeP_FXCU_Diff().ToString();
                //DataTable paymentTypeFXCUDiff = process.selectTypeFromMaxRec(maxRecFXCUDiff);
                //if (paymentTypeFXCUDiff.Rows.Count > 0)
                //{
                //    ProgramConfig.paymentType = paymentTypeFXCUDiff.Rows[0]["PCD"].ToString();
                //    ProgramConfig.paymentAmt = paymentTypeFXCUDiff.Rows[0]["AMT"].ToString();
                //}
                //else
                //{
                //    ProgramConfig.paymentType = "";
                //    ProgramConfig.paymentAmt = "";
                //}

                //StoreResult resFXCUDiff = process.deletePaymentType(maxRecFXCUDiff);
                //if (!resFXCUDiff.response.next)
                //{
                //    frmNotify dialog = new frmNotify(resFXCUDiff.response, resFXCUDiff.responseMessage, resFXCUDiff.helpMessage);
                //    dialog.ShowDialog(this);
                //    return;
                //}

                //#endregion


                //string maxRec = process.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo).ToString();
                //DataTable paymentType = process.selectTypeFromMaxRec(maxRec);
                //if (paymentType.Rows.Count > 0)
                //{
                //    ProgramConfig.paymentType = paymentType.Rows[0]["PCD"].ToString();
                //}
                //else
                //{
                //    ProgramConfig.paymentType = "";
                //}

                //DataTable paymentAmt = process.selectTypeFromMaxRec(maxRec);
                //if (paymentAmt.Rows.Count > 0)
                //{
                //    ProgramConfig.paymentAmt = paymentAmt.Rows[0]["AMT"].ToString();
                //}
                //else
                //{
                //    ProgramConfig.paymentAmt = "";
                //}

                //Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                //Match result = re.Match(ProgramConfig.paymentType);
                //string alphaPart = result.Groups[1].ToString();
                //string numberPart = result.Groups[2].ToString();
                //string payCode = alphaPart;
                //string payType = "";



                //StoreResult res = process.deletePaymentType(maxRec);
                //if (!res.response.next)
                //{
                //    frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                //    dialog.ShowDialog(this);
                //    return;
                //}

                ////payCode.Length == 4 ดักกรณีเป็น credit
                //if (payCode != "" && payCode.Length == 4)
                //{
                //    if (payCode == "CPN")
                //    {
                //        payCode = "CPN1";
                //    }
                //    payType = process.selectPaymentType(payCode).Rows[0]["PaymentTypeId"].ToString();
                //}

                //if (payType == "6")
                //{
                //    string GFSLNO = "";
                //    DataTable resSelectGFSL = process.selectDataSCANGFSL();
                //    if (resSelectGFSL.Rows.Count != 0 || resSelectGFSL != null)
                //    {
                //        for (int i = 0; i < resSelectGFSL.Rows.Count; i++)
                //        {
                //            GFSLNO = resSelectGFSL.Rows[i]["GFSL_NO"].ToString();
                //            StoreResult resDelete = process.delGFSL(GFSLNO);
                //        }
                //    }
                //}

                //if (payType == "7")
                //{
                //    StoreResult resDisCupon = process.displayCoupon();
                //    if (resDisCupon.response.next)
                //    {
                //        DataRow dr = resDisCupon.otherData.Select().Last();
                //        StoreResult resDel = process.delCoupon(dr["CouponNo"].ToString(), Convert.ToInt32(dr["CouponQnt"].ToString()), Convert.ToInt32(dr["ROW"].ToString()));
                //    }
                //}
                #endregion

                Utility.AutoVoidEDC(this, (vty, dty) => process.selectDLYPTRANS(ProgramConfig.saleRefNo, vty, dty));
                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmPayment)
                    {
                        frmPay = (frmPayment)item;
                        frmPay.BackProcess();
                        this.Dispose();
                        break;
                    }
                }

                frm2Detail.lbChangeCurrency1.Text = "";
                frm2Detail.lbChangeCurrency2.Text = "";
                frm2Detail.lbChangeCurrency3.Text = "";
                frm2Detail.lbCurrencyRate1.Text = "";
                frm2Detail.lbCurrencyRate2.Text = "";
                frm2Detail.lbCurrencyRate3.Text = "";
            }
            catch (NetworkConnectionException net)
            {
                ProcessCheckNetWorkLost(net);
            }
        }

        private void ProcessCheckNetWorkLost(NetworkConnectionException net)
        {
            if (!frmPay.ProcessCheckNetWorkLost(net))
            {
                btnCancelClick();
            }
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

        //private void DrawerClose()
        //{
        //    try
        //    {
        //        AppLog.writeLog("DrawerClose seqOfProcess = " + seqOfProcess);
        //        if (seqOfProcess > 1)
        //        {
        //            AppLog.writeLog("If : seqOfProcess = " + seqOfProcess);
        //            if (t1 != null)
        //            {
        //                AppLog.writeLog("close drawer timer stop");
        //                t1.Stop();
        //            }
        //            //t1.Enabled = false;
        //            //drawerOpenStatus = false;
        //            //seqOfProcess = 2;
        //            chkSwClose = true;

        //            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CloseDrawerAndRecordTime);
        //            if (check.policy == PolicyStatus.Work)
        //            {
        //                var resClose = process.saveCloseDrawer(FunctionID.Sale_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
        //                if (!resClose.response.next)
        //                {
        //                    tranSuccess = false;
        //                    frmNotify dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
        //                    dialog.ShowDialog(this);
        //                    return;
        //                }
        //                else
        //                {
        //                    statusCloseDrawer = true;
        //                    tranSuccess = true;
        //                }
        //            }
        //            else
        //            {
        //                statusCloseDrawer = true;
        //                tranSuccess = true;
        //            }

        //            AppLog.writeLog("DrawerClose chkSwClose = " + chkSwClose + " tranSuccess = " + tranSuccess + " seqOfProcess = " + seqOfProcess + " statusCloseDrawer = " + statusCloseDrawer);
        //            //closeForm();
        //        }
        //        else
        //        {
        //            AppLog.writeLog("Else : seqOfProcess = " + seqOfProcess + " statusConnect = " + statusConnect);
        //            setDrawer();
        //            //BackgroundWorker bg = new BackgroundWorker();
        //            //bg.DoWork += new DoWorkEventHandler(backgroundSetDrawer_Work);
        //            //bg.RunWorkerAsync();
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

        //private void setDrawer()
        //{
        //    if (statusConnect == "0")
        //    {
        //        while (seqOfProcess == 0)
        //        {
        //            System.Threading.Thread.Sleep(100);
        //        }
        //        //seqOfProcess = 1;
        //        DrawerClose();
        //    }
        //    else
        //    {
        //        statusConnect = "2";
        //        if (seqOfProcess > 0)
        //        {
        //            DrawerClose();
        //        }
        //    }
        //}

        public void DrawerStatus(string status)
        {
            AppLog.writeLog("DrawerStatus chkSwOpen = " + chkSwOpen + " ,tranSuccess = " + tranSuccess + " status " + status.ToUpper());
            if (chkSwOpen == true)
            {
                if (status.ToUpper() == "FALSE")
                {
                    //Save time Close Cash Drawer
                    closeTime = DateTime.Now.ToString("HHmmss", cultureinfo);

                    chkSwClose = true;
                    statusCloseDrawer = true;
                    closeForm();
                }
                else
                {
                    drawerOpenStatus = true;
                }
            }
        }

        private void TimerProcess()
        {
            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CheckCashdrawer);
            if (check.policy == PolicyStatus.Work)
            {
                timeout = (string)ProgramConfig.getPosConfig(FunctionID.Sale_CheckCashdrawer.parameterCode);
            }

            t1 = new Timer
            {
                Interval = 1000 * Convert.ToInt32(timeout)
            };
            t1.Tick += new System.EventHandler(OnTimerEvent);
            t1.Start();

        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            if (t1 != null)
            {
                AppLog.writeLog("timer stop");
                t1.Stop();
            }

            string responseMessage = ProgramConfig.message.get("frmConfirmPayment", "PressOkToContinue").message;
            frmNotify dialog = new frmNotify(ResponseCode.Information, responseMessage);
            DialogResult diaRes = dialog.ShowDialog();
            if (diaRes == DialogResult.OK)
            {

                drawerOpenStatus = false;
                chkSwClose = true;
                //Save time Close Cash Drawer
                closeTime = DateTime.Now.ToString("HHmmss", cultureinfo);

                //Profile check = ProgramConfig.getProfile(FunctionID.Sale_CloseDrawerAndRecordTime);
                //if (check.policy == PolicyStatus.Work)
                //{
                //    var resClose = process.saveCloseDrawer(FunctionID.Sale_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
                //    if (!resClose.response.next)
                //    {
                //        tranSuccess = false;
                //        dialog = new frmNotify(resClose.response, resClose.responseMessage, resClose.helpMessage);
                //        dialog.ShowDialog(this);
                //        return;
                //    }
                //    else
                //    {
                //        statusCloseDrawer = true;
                //        tranSuccess = true;
                //    }
                //}
                //else
                //{
                //    statusCloseDrawer = true;
                //    tranSuccess = true;
                //}

                statusCloseDrawer = true;
                tranSuccess = true;
                closeForm();
            }
        }

        private void closeForm()
        {
            string tempOldAbb = ProgramConfig.abbNo;
            try
            {
                frmLoading.showLoading();
                AppLog.writeLog("frmConfirmPayment: call method closeForm >> tranSuccess : " + tranSuccess + ", chkSwClose : " + chkSwClose + ", statusCloseDrawer : " + statusCloseDrawer + ", finishReceipt " + finishReceipt);

                if (tranSuccess == true && chkSwClose == true && statusCloseDrawer == true && finishReceipt == false)
                {
                    BaseProcess.clearDataTable();
                    finishReceipt = true;
                    if (t1 != null)
                    {
                        AppLog.writeLog("timer stop");
                        t1.Stop();
                    }


                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                    {
                        //TO DO Synctodatatank

                        //API payInvoicePOD
                        ClosePayment("");
                    }
                    else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                    {
                        ClosePayment("");
                    }
                    else
                    {
                        #region Normal Sale

                        process.newTransaction();
                        if (ProgramConfig.hasDrawer)
                        {
                            AppLog.writeLog("frmConfirmPayment: call method closeForm >> if (ProgramConfig.hasDrawer)");
                            Profile check = ProgramConfig.getProfile(FunctionID.Sale_CloseDrawerAndRecordTime);
                            if (check.policy == PolicyStatus.Work)
                            {
                                var resClose = process.saveCloseDrawer(FunctionID.Sale_CloseDrawerAndRecordTime, closeTime, ProgramConfig.abbNo);
                                if (!resClose.response.next)
                                {
                                    frmNotify dialog = new frmNotify(resClose);
                                    dialog.ShowDialog(this);
                                }
                                else
                                {
                                    if (resClose.response == ResponseCode.Information)
                                    {
                                        frmNotify notify = new frmNotify(resClose);
                                        notify.ShowDialog(this);
                                    }
                                }
                            }

                            process.SaveDrawerTrans(ProgramConfig.paymentCloseCashDrawer);
                        }



                        StoreResult result = process.saveToDataTank();
                        ProcessResult res = null;
                        if (result.response.next)
                        {
                            //ProgramConfig.abbNo = nextAbb;
                            //updateAbbValue
                            string oldAbb = ProgramConfig.abbNo;
                            string strRunning = oldAbb.Substring(3, 6);

                            //if (int)
                            //{

                            //}

                            //string nextAbb = (Convert.ToInt32(ProgramConfig.abbNo) + 1).ToString("D9");
                            string nextAbb = ProgramConfig.tillNo + (Convert.ToInt32(strRunning) + 1).ToString().PadLeft(6, '0') + (oldAbb.IndexOf("F") > 0 ? "F" : "");
                            int running = (Convert.ToInt32(strRunning) + 1);
                            //int running = 1000000;
                            if (running > 999999)
                            {
                                nextAbb = ProgramConfig.tillNo + "1".PadLeft(6, '0') + (oldAbb.IndexOf("F") > 0 ? "F" : "");
                            }

                            res = process.saleAutoVoid(ProgramConfig.saleRefNo, oldAbb, nextAbb, openTime, 
                                                        CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h),
                                                        AutoVoidEDC: () => Utility.AutoVoidEDC(this, (vty, dty) => process.selectDLYPTRANS(ProgramConfig.abbNo, vty, dty)));
                            if (!res.response.next)
                            {
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }
                            else
                            {
                                if (res.response == ResponseCode.Information)
                                {
                                    frmNotify notify = new frmNotify(res);
                                    notify.ShowDialog(this);
                                }
                            }
                            process.commit();                       

                            try
                            {
                                Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Main2); //#300 #72
                                if (chkRedeem.policy == PolicyStatus.Work && ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale)
                                {
                                    RedeemCoupon();
                                }
                            }
                            catch (NetworkConnectionException net)
                            {
                                Utility.CheckRunningNumber();
                                Program.control.RetryConnection(net.errorType);
                            }
                            ClosePayment(nextAbb);
                        }
                        else
                        {

                            string oldAbb = ProgramConfig.abbNo;
                            string strRunning = oldAbb.Substring(3, 6);

                            //string nextAbb = (Convert.ToInt32(ProgramConfig.abbNo) + 1).ToString("D9");
                            string nextAbb = ProgramConfig.tillNo + (Convert.ToInt32(strRunning) + 1).ToString().PadLeft(6, '0') + (oldAbb.IndexOf("F") > 0 ? "F" : "");
                            int running = (Convert.ToInt32(strRunning) + 1);

                            if (running > 999999)
                            {
                                nextAbb = ProgramConfig.tillNo + "1".PadLeft(6, '0');
                            }

                            res = process.saleAutoVoid(ProgramConfig.saleRefNo, oldAbb, nextAbb, openTime,
                                                        CheckAuth: (p, h) => Utility.CheckAuthPass(this, p, h),
                                                        AutoVoidEDC : () => Utility.AutoVoidEDC(this, (vty, dty) => process.selectDLYPTRANS(ProgramConfig.saleRefNo, vty, dty)));
                            if (!res.response.next)
                            {
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }
                            else
                            {
                                if (res.response == ResponseCode.Information)
                                {
                                    frmNotify notify = new frmNotify(res);
                                    notify.ShowDialog(this);
                                }
                            }
                            process.commit();

                            try
                            {
                                Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Main2); //#300 #72
                                if (chkRedeem.policy == PolicyStatus.Work && ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale)
                                {
                                    RedeemCoupon();
                                }
                            }
                            catch (NetworkConnectionException net)
                            {
                                Utility.CheckRunningNumber();
                                Program.control.RetryConnection(net.errorType);
                            }

                            ClosePayment(nextAbb);
                        }
                        process.commit();

                        #endregion
                    }
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
                process.rollback();
                if (Program.control.RetryConnection(net.errorType))
                {
                    AppLog.writeLog("NetworkConnectionException closeForm");

                    if (tempOldAbb == ProgramConfig.abbNo)
                    {
                        string strRunning = tempOldAbb.Substring(3, 6);

                        string nextAbb = (Convert.ToInt32(ProgramConfig.abbNo) + 1).ToString("D9");
                        int running = (Convert.ToInt32(strRunning) + 1);

                        if (running > 999999)
                        {
                            nextAbb = ProgramConfig.tillNo + "1".PadLeft(6, '0');
                        }

                        ClosePayment(nextAbb);
                    }

                  
                    //finishReceipt = false;
                    //closeForm();
                } 
                //ProcessCheckNetWorkLost(net);
               
            }
            //catch (Exception ex)
            //{
            //    frmLoading.closeLoading();
            //    frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
            //    dialog.ShowDialog(this);
            //}
        }

        private void ClosePayment(string nextAbb)
        {
            if (nextAbb != "")
            {
                ProgramConfig.abbNo = nextAbb;
                ProgramConfig.running.updateValue();
            }

            Utility.GlobalClear();
            ProgramConfig.salePageState = 2;

            if (frmPay == null)
            {
                if (this.Owner is frmPayment)
                {
                    frmPay = (frmPayment)this.Owner;
                    frmPay.ucFooterTran1.mainContent = "";
                    frmPay.ucFooterTran1.fullContent = "";
                    frmPay.ucFooterTran1.functionId = "";
                }
            }
            else
            {
                frmPay.ucFooterTran1.mainContent = "";
                frmPay.ucFooterTran1.fullContent = "";
                frmPay.ucFooterTran1.functionId = "";
            }

            AppLog.writeLog("frmConfirmPayment: salePageState in closeFormPayment = " + ProgramConfig.salePageState);

            chkSwOpen = false;

            Form form = Application.OpenForms["frmMonitorCustomer"];
            frmMonitorCustomer mon = form as frmMonitorCustomer;
            mon.clearForm();

            Form form2 = Application.OpenForms["frmMonitor2Detail"];
            frmMonitor2Detail mon2 = form2 as frmMonitor2Detail;
            mon2.clearForm();

            if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.NormalSale)
            {
                Program.control.ShowForm("frmSale");
                Form form3 = Application.OpenForms["frmSale"];
                form3.Activate();
            }
            else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
            {
                Program.control.ShowForm("frmDeposit");
                Form form3 = Application.OpenForms["frmDeposit"];
                form3.Activate();
            }
            else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
            {
                Program.control.ShowForm("frmReceivePaymentPOD");
                Form form3 = Application.OpenForms["frmReceivePaymentPOD"];
                form3.Activate();
            }
            else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
            {
                Program.control.ShowForm("SubOtherService1");
                Form form3 = Application.OpenForms["SubOtherService1"];
                form3.Activate();
            }


            Program.control.CloseForm("frmPayment");
            Program.control.CloseForm("frmConfirmPayment");
            frmLoading.closeLoading();
        }

        private void frmConfirmPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hardware.clearDrawerListeners();
        }

        private void frmConfirmPayment_Shown(object sender, EventArgs e)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item is frmMonitor2Detail)
                {
                    frm2Detail = (frmMonitor2Detail)item;
                    frm2Detail.lbTxtReceive.Text = lbConfirmPayment;
                    frm2Detail.lbTxtbalance.Text = lbConfirmBalance;
                }
            }

            btnOk.Select();
        }

        private void lbConfirm_TextChanged(object sender, EventArgs e)
        {
            lbConfirmCurrency.Text = lbConfirm.Text;
        }

        private void lbCash_TextChanged(object sender, EventArgs e)
        {
            lbCashCurrency.Text = lbCash.Text;
        }

        private void lbPayment_TextChanged(object sender, EventArgs e)
        {
            lbPaymentCurrency.Text = lbPayment.Text;
        }

        private void lbBalance_TextChanged(object sender, EventArgs e)
        {
            lbBalanceCurrency.Text = lbChange.Text;
        }

        private void btnCancel_TextChanged(object sender, EventArgs e)
        {
            btnCancelCurrency.Text = btnCancel.Text;
        }

        private void btnOk_TextChanged(object sender, EventArgs e)
        {
            btnOkCurrency.Text = btnOk.Text;
        }

        private void lb_message_TextChanged(object sender, EventArgs e)
        {
            lb_messageCurrency.Text = lb_message.Text;
        }

        private void Control_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont((Control)sender);
        }

        private void lbText_TextChanged(object sender, EventArgs e)
        {
            lbTextCurrency.Text = lbText.Text;
        }

        private void btnEditChange_Click(object sender, EventArgs e)
        {
            btnEditChange.Visible = false;
            btnCancelChange.Visible = true;
            lbTxtChange.Visible = false;
            ucTxtChange.Visible = true;
            splitContainer1.Panel2Collapsed = false;
            ucKeypad1.ucTBWI = ucTxtChange;
            lbOldChange.Text = "เงินทอนเดิม " + _oldChange;
            ucTxtChange.FocusTxt();
        }

        private void ucTxtChange_EnterFromButton(object sender, EventArgs e)
        {
            var uc = (UCTextBoxWithIcon)sender;

            if (uc.Text != "" && Convert.ToDouble(uc.Text) < Convert.ToDouble(_oldChange))
            {
                Utility.AlertMessage(ResponseCode.Error, "ไม่สามารถแก้ไขเงินทอนน้อยกว่าเงินทอนปัจจุบันได้");
                return;
            }

            double limChange = ProgramConfig.payment.getUserChangeLimit(lastPMCode);
            if (uc.Text != "" && (Convert.ToDouble(uc.Text) - Convert.ToDouble(_oldChange)) > limChange)
            {
                Utility.AlertMessage(ResponseCode.Error, "ไม่สามารถแก้ไขเงินทอนมากกว่า " + limChange.ToString(ProgramConfig.amountFormatString) + " " + ProgramConfig.currencyDefault + " \nจากเงินทอนปัจุบัน");
                return;
            }

            StoreResult chkMinCash = process.checkMinCashUnitAmount("CASH", uc.Text.Trim(), ProgramConfig.currencyDefault, "2");
            if (chkMinCash.response.next)
            {
                double inpChange = 0;
                double.TryParse(uc.Text, out inpChange);
                splitContainer1.Panel2Collapsed = true;
                txtConfirmChange = inpChange.ToString(ProgramConfig.amountFormatString);
                uc.Visible = false;
                lbTxtChange.Visible = true;
                lbTxtChangeCurrency.Visible = true;

                btnEditChange.Visible = true;
                btnCancelChange.Visible = false;
            }
            else
            {
                Utility.AlertMessage(chkMinCash);
                return;
            }
        }

        private void btnEditChangeCurrency_Click(object sender, EventArgs e)
        {
            lbTxtChangeCurrency.Visible = false;
            ucTxtChangeCurrency.Visible = true;
            ucTxtChangeCurrency.FocusTxt();
            splitContainer1.Panel2Collapsed = false;
            ucKeypad1.ucTBWI = ucTxtChangeCurrency;
        }

        private void ucTxtChange_VisibleChanged(object sender, EventArgs e)
        {
            lbOldChange.Visible = ucTxtChange.Visible;
            btnCancelChange.Visible = ucTxtChange.Visible;
        }

        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            btnEditChange.Visible = true;
            btnCancelChange.Visible = false;
            splitContainer1.Panel2Collapsed = true;
            lbTxtChange.Visible = true;
            lbTxtChangeCurrency.Visible = true;
            ucTxtChange.Visible = false;
        }

        


        //private void backgroundSetDrawer_Work(object sender, DoWorkEventArgs e)
        //{
        //    setDrawer();
        //}

        //private void backgroundConfirm_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    PageResult res = process.saveConfirmPayment();
        //}

        //private void backgroundConfirm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (e.Error == null && !e.Cancelled)
        //    {
        //        PageResult res = (PageResult)e.Result;
        //        if (res.response.next)
        //        {
        //            kickDrawer();

        //            printReceipt();

        //            ProgramConfig.memberId = "";
        //            ProgramConfig.memberName = "";

        //            AppLog.writeLog("[2] Time : " + DateTime.Now.ToString("hh:mm:ss.fff") + "");
        //            OpenCashDrawer();
        //        }
        //        else
        //        {
        //            frmNotify dialog = new frmNotify(res.response, res.message, res.help);
        //            dialog.ShowDialog(this);
        //        }
        //    }
        //}
    }
}
