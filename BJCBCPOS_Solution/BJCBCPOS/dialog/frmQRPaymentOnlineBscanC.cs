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
    public partial class frmQRPaymentOnlineBscanC : Form
    {
        SaleProcess saleProcess = new SaleProcess();
        frmPayment fPayment;
        private bool IsPaint = false;

        string _total;
        string _qrCode;
        string _requestTranID;
        string _bankCode;
        string _token;
        string _ota;
        int defaultDelay = 5000; //ms
        int defaultRetry = 5;
        int cnt = 0;
        bool offlineFlag = false;
        bool _isEndReceipt;

        public frmQRPaymentOnlineBscanC()
        {
            InitializeComponent();
        }

        public frmQRPaymentOnlineBscanC(string qrCode, string total, bool isEndReceipt)
        {
            InitializeComponent();
            _total = total;
            _qrCode = qrCode;
            _isEndReceipt = isEndReceipt;
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

        private void frmQRPaymentOnlineBscanC_Shown(object sender, EventArgs e)
        {
            fPayment = (frmPayment)this.Owner;

            var res = saleProcess.selectAPICONF_RETRY("pos_CheckCallback");
            if (res.response.next)
            {
                defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
            }

            timer1.Interval = defaultDelay;

            saleProcess.PaymentDiscount("QRPP", "");
            fPayment.loadDiscount();

            res = saleProcess.selectParameterForQR("", "BC", "PM");
            if (res.response.next)
            {
                DataRow dr = res.otherData.Rows[0];
                res = saleProcess.API_POS_BSC_PAYMENT(dr["TranID"].ToString(), _qrCode, _total, dr["Seq"].ToString());
                if (res.response.next)
                {
                    cnt = 0;
                    _requestTranID = res.otherData.Rows[0]["Prt_TxnUID"].ToString();
                    _bankCode = res.otherData.Rows[0]["BankCode"].ToString();
                    _token = res.otherData.Rows[0]["Token_ID"].ToString();
                    _ota = res.otherData.Rows[0]["OTA"].ToString();
                    offlineFlag = false;
                    timer1.Start();
                    return;
                }
            }

            Utility.AlertMessage(res);
            CloseForm();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool authFlag = false;
            cnt++;

            var res = saleProcess.CheckCallback(_requestTranID);
            DataRow dr = res.otherData.Rows[0];
            if (res.response.next)
            {
                timer1.Stop();
                res = saleProcess.updateQRPayTransBySet(" Ref2 = '" + dr["Ref2"].ToString() + "' ", " and ORG_TRANID = '" + _requestTranID + "' ");
                if (res.response.next)
                {
                    fPayment.DeleteTempPayment("QRPP");
                    res = saleProcess.savePaymentQROnline(_total.ToString(), "QRPP", "O");
                    if (res.response.next)
                    {
                        Program.control.CloseForm("frmQRPaymentOnlineBscanC");
                        if (_isEndReceipt)
                        {
                            fPayment.ShowConfirmPayment("QRPP", false);
                        }                        
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    }
                }
                CloseForm();
            }
            if (res.response == ResponseCode.Error && dr["TRX_STATUS"] + "" == "RJTC")
            {
                timer1.Stop();
                CloseForm(String.Format(" and ORG_TRANID = '{0}' and stt = '' ", _requestTranID));
                Utility.AlertMessage(res);
                return;
            }
            else if (res.response == ResponseCode.Error && dr["TRX_STATUS"] + "" == "")
            {
                if (cnt > defaultRetry)
                {
                    timer1.Stop();
                    frmNotify notify = new frmNotify("ยืนยันการชำระ QR Payment หรือ ชำระตราสารอื่น ?", "ยืนยันการชำระ QR Payment", "ชำระตราสารอื่น");
                    var resDialog = notify.ShowDialog(this);
                    if (resDialog == System.Windows.Forms.DialogResult.Yes)
                    {
                        //if (!offlineFlag)
                        //{
                        //    offlineFlag = true;
                        //    cnt = 0;
                        //    timer1.Start();
                        //    goto Retry;
                        //}
                        //else
                        //{
                            timer1.Stop();
                            res = saleProcess.selectParameterForQR("", "BC", "IP");
                            if (res.response.next)
                            {
                                DataRow dr2 = res.otherData.Rows[0];
                            Retry2:
                                res = saleProcess.API_POS_BSC_INQUIRY_PAYMENT_STATUS(dr2["TranID"].ToString(), _requestTranID, _qrCode, _bankCode, _token, _ota);
                                DataRow dr3 = res.otherData.Rows[0];
                                string resCodeInq = res.response.value;
                                string resMsgInq = res.responseMessage;
                                if (res.response == ResponseCode.Success)
                                {
                                    //DataRow dr3 = res.otherData.Rows[0];
                                    res = saleProcess.updateQRPayTrans(dr3["Prt_TxnUID"].ToString(), "", "", "", "O", "", "",
                                            channel: "BC", ref2: dr3["Ref2"].ToString(), whereCondition: " and action_Type = 'SA' ");

                                    if (res.response.next)
                                    {
                                        fPayment.DeleteTempPayment("QRPP");
                                        res = saleProcess.savePaymentQROnline(_total.ToString(), "QRPP", "O");
                                        if (res.response.next)
                                        {
                                            Program.control.CloseForm("frmQRPaymentOnlineBscanC");
                                            if (_isEndReceipt)
                                            {
                                                fPayment.ShowConfirmPayment("QRPP", false);
                                            }
                                            DialogResult = System.Windows.Forms.DialogResult.OK;
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                Retry3:
                                    if (!authFlag)
                                    {
                                        //Fix language
                                        frmPopupInput fPopup = new frmPopupInput("ระบุเลขที่รายการ/สแกน QR-Code จาก (E-Slip) บนมือถือลูกค้า เพื่อตรวจสอบการชำระเงิน", "กรุณากรอก E-Slip");
                                        fPopup.ucTxtInput2.Visible = false;
                                        resDialog = fPopup.ShowDialog();

                                        if (resDialog == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            res = saleProcess.selectParameterForQR("", "BC", "VR");
                                            if (res.response.next)
                                            {
                                                res = saleProcess.API_POS_BSC_VERIFY_SLIP(res.otherData.Rows[0]["TranID"].ToString(), _bankCode, _requestTranID,fPopup.result.input1);

                                                //TO DO update pos_runing
                                                saleProcess.updatePOS_Running("22");
                                                if (res.response.next)
                                                {
                                                    DataRow dr6 = res.otherData.Rows[0];
                                                    res = saleProcess.updateQRPayTrans(_requestTranID, "", "", "", "O", "", "",
                                                                                    channel: "BC",
                                                                                    ref2: dr6["Ref2"].ToString(),
                                                                                    transRef: dr6["TRANSREF"].ToString(),
                                                                                    whereCondition: " and action_type = 'SA' ");

                                                    //TO DO
                                                    if (res.response.next)
                                                    {
                                                        fPayment.DeleteTempPayment("QRPP");
                                                        res = saleProcess.savePaymentQROnline(_total.ToString(), "QRPP", "F");
                                                        if (res.response.next)
                                                        {
                                                            Program.control.CloseForm("frmQRPaymentOnlineBscanC");
                                                            if (_isEndReceipt)
                                                            {
                                                                fPayment.ShowConfirmPayment("QRPP", false);
                                                            }
                                                            DialogResult = System.Windows.Forms.DialogResult.OK;
                                                            return;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Utility.AlertMessage(res);
                                                    goto Retry3;
                                                }
                                                  
                                                #region Comment		 
                                                //else
                                                //{

                                                //    notify = new frmNotify(ResponseCode.Warning, res.responseMessage);
                                                //    resDialog = notify.ShowDialog(this);
                                                //    if (resDialog == System.Windows.Forms.DialogResult.Yes)
                                                //    {
                                                //        authFlag = false;
                                                //        goto Retry3;
                                                //    }
                                                //    else
                                                //    {
                                                //        notify = new frmNotify("ยืนยันการชำระ QR Payment หรือ ชำระตราสารอื่น ?", "ยืนยันการชำระ QR Payment", "ชำระตราสารอื่น");
                                                //        resDialog = notify.ShowDialog(this);

                                                //        if (resDialog == System.Windows.Forms.DialogResult.Yes)
                                                //        {
                                                //            var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentOffline);
                                                //            profile.profile = ProfileStatus.NotAuthorize;
                                                //            if (!Utility.CheckAuthPass(this, profile, "QR Payment"))
                                                //            {
                                                //                authFlag = false;
                                                //                goto Retry3;
                                                //            }

                                                //            res = saleProcess.updateQRPayTrans(dr3["Prt_TxnUID"].ToString(), resCodeInq, dr3["ERRORCODE"].ToString(),
                                                //                resMsgInq, "F", "Y", ProgramConfig.userId, whereCondition: " and action_type = 'SA' ");
                                                //            if (res.response.next)
                                                //            {
                                                //                res = saleProcess.savePaymentQROnline(_total.ToString(), "QRPP", "F");
                                                //                if (res.response.next)
                                                //                {
                                                //                    Program.control.CloseForm("frmQRPaymentOnlineBscanC");
                                                //                    fPayment.ShowConfirmPayment(false);
                                                //                    DialogResult = System.Windows.Forms.DialogResult.OK;
                                                //                    return;
                                                //                }
                                                //            }
                                                //        }


                                                //        else
                                                //        {
                                                //            res = saleProcess.selectParameterForQR("", "BC", "VP");
                                                //            if (res.response.next)
                                                //            {
                                                //                res = saleProcess.API_POS_BSC_VOID(res.otherData.Rows[0]["TranID"].ToString(), _requestTranID, _total);
                                                //                DataRow dr4 = res.otherData.Rows[0];
                                                //                if (res.response.next)
                                                //                {
                                                //                    saleProcess.saveQRPayTransOnline("VD", "", dr4["Prt_TxnUID"].ToString(), dr4["Account_name"].ToString(),
                                                //                                                     dr4["QR_Code"].ToString(), dr4["ResCode"].ToString(), dr4["ErrorCode"].ToString(), _total.ToString(),
                                                //                                                     "BC", dr4["BANKCODE"].ToString(), dr4["TOKEN_ID"].ToString(), dr4["OTA"].ToString(), dr4["BSD"].ToString(),
                                                //                                                     dr4["TEPA_CODE"].ToString(), dr4["SENDING_BANK"].ToString(), dr4["RECEVING"].ToString(), dr4["REF2"].ToString(),
                                                //                                                     dr4["TRANSREF"].ToString());
                                                //                    this.Dispose();
                                                //                }
                                                //                else if (res.response == ResponseCode.Error && dr4["StatusCOde"].ToString() != "F")
                                                //                {
                                                //                    Utility.AlertMessage(res);

                                                //                    res = saleProcess.updateVoidQRPayTrans(" and action_type = 'SA' ");
                                                //                    if (res.response.next)
                                                //                    {
                                                //                        res = saleProcess.savePaymentQROnline(_total.ToString(), "QRPP", "F");
                                                //                        if (res.response.next)
                                                //                        {
                                                //                            Program.control.CloseForm("frmQRPaymentOnlineBscanC");
                                                //                            fPayment.ShowConfirmPayment(false);
                                                //                            DialogResult = System.Windows.Forms.DialogResult.OK;
                                                //                            return;
                                                //                        }
                                                //                    }
                                                //                }
                                                //                else if (res.response == ResponseCode.Error && dr4["StatusCOde"].ToString() == "F")
                                                //                {
                                                //                    res = saleProcess.selectParameterForQR("", "BC", "IV");
                                                //                    if (res.response.next)
                                                //                    {
                                                //                        res = saleProcess.API_POS_BSC_INQUIRY_VOID_STATUS(dr2["TranID"].ToString(), _requestTranID, _total);
                                                //                        if (res.response.next)
                                                //                        {
                                                //                            DataRow dr5 = res.otherData.Rows[0];
                                                //                            saleProcess.saveQRPayTransOnline("VD", "", dr4["Prt_TxnUID"].ToString(), dr5["Account_name"].ToString(),
                                                //                                  dr5["QR_Code"].ToString(), dr5["ResCode"].ToString(), dr5["ErrorCode"].ToString(), _total.ToString(),
                                                //                                  "BC", dr5["BANKCODE"].ToString(), dr5["TOKEN_ID"].ToString(), dr5["OTA"].ToString(), dr5["BSD"].ToString(),
                                                //                                  dr5["TEPA_CODE"].ToString(), dr5["SENDING_BANK"].ToString(), dr5["RECEVING"].ToString(), dr5["REF2"].ToString(),
                                                //                                  "");

                                                //                            this.Dispose();
                                                //                        }
                                                //                        else
                                                //                        {
                                                //                            Utility.AlertMessage(res);
                                                //                            //TO DO Profile #101
                                                //                            var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentOffline);
                                                //                            if (!Utility.CheckAuthPass(this, profile, "QR Payment"))
                                                //                            {
                                                //                                authFlag = false;
                                                //                                goto Retry3;
                                                //                            }

                                                //                            //insert QRpaytrans_void
                                                //                            saleProcess.updateVoidQRPayTrans();
                                                //                            this.Dispose();
                                                //                        }
                                                //                    }
                                                //                }
                                                //            }
                                                //        }
                                                //    }
                                                //}
	                                            #endregion
                                            }
                                        }
                                        else
                                        {
                                            notify = new frmNotify("ยืนยันการชำระ QR Payment หรือ ชำระตราสารอื่น ?", "ยืนยันการชำระ QR Payment", "ชำระตราสารอื่น");
                                            resDialog = notify.ShowDialog(this);
                                            if (resDialog == System.Windows.Forms.DialogResult.Yes)
                                            {
                                                authFlag = true;
                                                goto Retry2;
                                            }
                                            else
                                            {
                                                CloseForm(String.Format(" and ORG_TRANID = '{0}' and stt = '' ", _requestTranID));
                                                return;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentOffline);
                                        profile.profile = ProfileStatus.NotAuthorize;
                                        if (!Utility.CheckAuthPass(this, profile, "QR Payment"))
                                        {
                                            authFlag = false;
                                            goto Retry3;
                                        }

                                        CloseForm(String.Format(" and ORG_TRANID = '{0}' and stt = '' ", _requestTranID));
                                        return;
                                    }
                                    // fPopup.result.input1
                                }

                                //var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentOffline);
                                //if (!Utility.CheckAuthPass(this, profile, "QR Payment"))
                                //{
                                //    CloseForm();
                                //    return;
                                //}

                                CloseForm();
                                Utility.AlertMessage(res);
                            }
                        //}
                    }
                    else
                    {
                        CloseForm();
                    }
                }
            }
            //else if (res.response == ResponseCode.Success && dr["TRX_STATUS"].ToString() == "PAID")
            //{
            //    timer1.Stop();
            //    dr = res.otherData.Rows[0];
            //    string resMsg = res.responseMessage;

            //    res = saleProcess.updateQRPayTrans(dr["TranID"].ToString(), dr["ResCODE"].ToString(), dr["ErrorCode"].ToString(), resMsg, "O", "", "");
            //    if (res.response.next)
            //    {
            //        res = saleProcess.savePaymentQROnline(_total.ToString(), "QRPP", "F");
            //        if (res.response.next)
            //        {
            //            Program.control.CloseForm("frmQRPaymentOnline");
            //            fPayment.ShowConfirmPayment(false);
            //            return;
            //        }
            //    }

            //    CloseForm();
            //}

            
        }

        private void CloseForm(string whereCondition = " and stt = '' and Action_Type = 'SA' ")
        {
            try
            {


                timer1.Stop();
                timer1.Dispose();

                var res = saleProcess.updateVoidQRPayTrans(whereCondition);
                if (!res.response.next)
                {
                    Utility.AlertMessage(res);
                }

                this.Dispose();
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
                this.Dispose();
            }
        }


    }
}
