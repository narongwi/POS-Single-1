using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BJCBCPOS_Model;
using System.Diagnostics;
using System.Threading;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class frmQRPaymentOnline : Form
    {
        SaleProcess saleProcess = new SaleProcess();
        frmPayment fPayment;

        string filePNG;
        string _requestTranID;

        int defaultDelay = 5000; //ms
        int defaultRetry = 5;
        int cnt = 0;

        string _total;
        string _trainID;
        string _transTime;
        string _seq;

        public frmQRPaymentOnline()
        {
            InitializeComponent();
        }

        public frmQRPaymentOnline(string total)
        {
            InitializeComponent();
            _total = total;
        }

        private void frmQRPaymentOnline_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            Screen second = Screen.AllScreens[0];
            if (Screen.AllScreens.Length > 1 && second.Primary)
            {
                second = Screen.AllScreens[1];
                Point screen_location = second.WorkingArea.Location;
                this.Location = new Point(screen_location.X - 375, screen_location.Y);
            }

            pictureBox5.Image = Utility.GetLogoImage();
            picLogo.Image = Utility.GetLogoImage();
            lb_Amt.Text = _total;
            label5.Text = _total;
            //GenQRCode("00020101021230810016A00000067701011201150107536000315080214KB0000002538580320API1622129319175404531690016A00000067701011301030040214KB0000002538580420API1622129319175404553037645402505802TH630477E0");
            //return;

            fPayment = (frmPayment)this.Owner;

            var res = saleProcess.selectAPICONF_RETRY("pos_QR_InquirePayment");
            if (res.response.next)
            {
                defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
            }

            timer1.Interval = defaultDelay;

            saleProcess.PaymentDiscount("QRPP", "");
            fPayment.loadDiscount();

            res = saleProcess.selectParameterForQR("RQ", "CB", "");
            if (res.response.next)
            {
                DataRow dr = res.otherData.Rows[0];

                res = saleProcess.QRRequest(dr["TranID"].ToString(), dr["TranTime"].ToString(), ProgramConfig.qrPaymentMID, _total.ToString());
                if (res.response.next)
                {                
                    DataRow dr2 = res.otherData.Rows[0];
                    label7.Text = label2.Text = "บัญชี : " + dr2["ACCOUNT_NAME"].ToString();

                    _requestTranID = dr2["TranID"].ToString();

                    res = saleProcess.saveQRPayTransOnline("SA", dr["Seq"].ToString(), dr2["TranID"].ToString(), dr2["Account_name"].ToString(), dr2["QR_Code"].ToString(), dr2["STATUS_CODE"].ToString(),
                        dr2["ErrorCode"].ToString(), _total, "CB", "", "", "", "", "", "", "", "", "");
                    if (res.response.next)
                    {
                        this.Refresh();
                        if (GenQRCode(dr2["QR_Code"].ToString()))
                        {
                            cnt = 0;
                            timer1.Start();
                            res = saleProcess.selectParameterForQR("IQ", "CB", "");
                            if (res.response.next)
                            {
                                DataRow drx = res.otherData.Rows[0];
                                _seq = drx["Seq"].ToString();
                                _trainID = drx["TranID"].ToString();
                                _transTime = drx["TranTime"].ToString();
                            }
                            else
                            {
                                CloseForm();
                                Utility.AlertMessage(res);
                            }
                            return;
                        }
                    }
                }
            }
            Utility.AlertMessage(res);
            CloseForm();
        }

        private bool GenQRCode(string data)
        {
            Process proc = null;
            try
            {
                AppLog.writeLog("Start GenQR");
                int retry = 0;
                int timeout = 0;

            Retry:
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string qrPath = String.Format(@"{0}\qrcode.exe", appPath);

                filePNG = String.Format(@"{0}\qr{1}.png", appPath, ProgramConfig.tillNo);

                if (File.Exists(filePNG))
                {
                    File.Delete(filePNG);
                }

                if (!File.Exists(qrPath))
                {
                    //Alert not found QRCode.exe
                    //Change language
                    Utility.AlertMessage(ResponseCode.Error, "ไม่พบ path qrcode.exe");
                }

                string argument = String.Format(@"-o {0} -s 5 -l H ""{1}""", filePNG, data);

                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = qrPath,
                        Arguments = argument,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();

                do
                {
                    Thread.Sleep(500);
                    if (File.Exists(filePNG))
                    {
                        using (FileStream stm = new FileStream(filePNG, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(stm);
                            panel1.BackgroundImage = img;
                            panel4.BackgroundImage = img;
                            stm.Dispose();
                            proc.Dispose();
                            return true;
                        }
                    }

                    timeout++;
                    if (timeout > 50)
                    {
                        if (retry < 1)
                        {
                            retry++;
                            timeout = 0;
                            goto Retry;
                        }
                        //Alert Message genQRCode Time out" & vbNewLine & "กรุณาลองใหม่อีกครั้ง
                        if (proc != null)
                        {
                            proc.Close();
                        }
                        break;
                    }
                } while (true);

                AppLog.writeLog("End GenQR");
                return false;
            }
            catch(Exception ex)
            {
                if (proc != null)
                {
                    proc.Close();
                }
                AppLog.writeLog("[Error] : " + ex.Message);
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
Retry:
            cnt++;
            //var res = saleProcess.selectParameterForQR("IQ", "CB", "");
            //if (res.response.next)
            //{
                //DataRow dr = res.otherData.Rows[0];

                var res = saleProcess.QRInquirePayment(_trainID, _transTime, ProgramConfig.qrPaymentMID, _requestTranID);
                DataRow dr = res.otherData.Rows[0];

                if (res.response == ResponseCode.Error && (dr["STATUS_CODE"].ToString() == "E" || dr["STATUS_CODE"].ToString() == "F"))
                {
                    if (cnt > defaultRetry)
                    {
                        timer1.Stop();
                        Utility.AlertMessage(ResponseCode.Information, "โทร KBANK : 02-888-8822 กด 818 แจ้ง \"ขอตรวจสอบ transaction รายการ API ร้านค้า Big C พร้อม Transaction ID ที่แสดงใน E-Slip\" ");
                        frmNotify notify = new frmNotify(ResponseCode.Warning, "ต้องการอนุมัติให้รับชำระ QR Payment แบบ Offline หรือไม่ ?");
                        var resDialog = notify.ShowDialog();
                        if (resDialog == System.Windows.Forms.DialogResult.Yes)
                        {
                        RetryAuth:
                            var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentCSB);
                            if (!Utility.CheckAuthPass(fPayment, profile, "QR Payment"))
                            {
                                //CloseForm();
                                goto RetryAuth;
                            }

                            res = saleProcess.updateQRPayTrans(dr["TranID"].ToString(), "", "", "", "F", "Y", profile.profile == ProfileStatus.Authorize ? ProgramConfig.userId : ProgramConfig.superUserId, channel: "CB");
                            if (res.response.next)
                            {
                                res = saleProcess.savePaymentOffline(_total, "QRPP" + _seq.PadLeft(5, '0') + ProgramConfig.saleRefNo, new List<PaymentStepDet>());
                                if (res.response.next)
                                {
                                    Program.control.CloseForm("frmQRPaymentOnline");
                                    fPayment.ShowConfirmPayment("QRPP", false);
                                    this.Dispose();
                                    return;
                                }
                            }

                            CloseForm();
                            Utility.AlertMessage(res);
                        }
                        else
                        {
                            CloseForm();
                        }
                    }
                }
                else if (res.response == ResponseCode.Error && dr["TXN_STATUS"].ToString() != "PAID")
                {
                    if (cnt > defaultRetry)
                    {
                        timer1.Stop();
                    //Retry2:
                        frmNotify notify = new frmNotify("ยืนยันการชำระ QR Payment หรือ ชำระตราสารอื่น ?", "ยืนยันการชำระ QR Payment", "ชำระตราสารอื่น");
                        var resDialog = notify.ShowDialog();
                        if (resDialog == System.Windows.Forms.DialogResult.Yes)
                        {
                            cnt = defaultRetry;
                            //timer1.Start();
                            goto Retry;

                            //if (!offlineFlag)
                            //{
                            //    offlineFlag = true;
                            //    cnt = defaultRetry;
                            //    goto Retry;
                            //}
                            //else
                            //{
                            //    var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentCSB);
                            //    if (!Utility.CheckAuthPass(fPayment, profile, "QR Payment"))
                            //    {
                            //        goto Retry2;
                            //    }

                            //    res = saleProcess.updateQRPayTrans("", "", "", "", "F", "Y", profile.profile == ProfileStatus.Authorize ? ProgramConfig.userId : ProgramConfig.superUserId);
                            //    if (res.response.next)
                            //    {
                            //        res = saleProcess.savePaymentOffline(_total, "QRPP" + _seq.PadLeft(5, '0') + ProgramConfig.saleRefNo, new List<PaymentStepDet>());
                            //        if (res.response.next)
                            //        {
                            //            Program.control.CloseForm("frmQRPaymentOnline");
                            //            fPayment.ShowConfirmPayment(false);
                            //            this.Dispose();
                            //            return;
                            //        }
                            //    }

                            //    CloseForm();
                            //    Utility.AlertMessage(res);
                            //}
                        }
                        else
                        {
                        RetryAuth:
                            var profile = ProgramConfig.getProfile(FunctionID.Sale_AuthQRPaymentCSB2);
                            if (!Utility.CheckAuthPass(fPayment, profile, "QR Payment"))
                            {
                                goto RetryAuth;
                            }

                            CloseForm();
                        }
                    }

                }

                else if (res.response == ResponseCode.Success && dr["TXN_STATUS"].ToString() == "PAID")
                {
                    timer1.Stop();
                    dr = res.otherData.Rows[0];
                    string resMsg = res.responseMessage;

                    res = saleProcess.updateQRPayTrans(dr["TranID"].ToString(), dr["STATUS_CODE"].ToString(), dr["ErrorCode"].ToString(), resMsg, "O", "", "", channel : "CB");
                    if (res.response.next)
                    {
                        res = saleProcess.savePaymentQROnline(_total, "QRPP" + _seq.PadLeft(5, '0') + ProgramConfig.saleRefNo, "O");
                        if (res.response.next)
                        {
                            Program.control.CloseForm("frmQRPaymentOnline");
                            fPayment.ShowConfirmPayment("QRPP", false);
                        }
                    }
                }
            //}
            //else
            //{
            //    CloseForm();
            //    Utility.AlertMessage(res);
            //}
        }

        private void CloseForm()
        {
            saleProcess.updateVoidQRPayTrans();
            timer1.Stop();
            timer1.Dispose();
            this.Dispose();
        }
    }
}
