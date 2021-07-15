using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BJCBCPOS_Model;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class frmEDCProcess : Form
    {
        private string _sumTotal = "";
        private string _creditAmt = "";
        private string _gTotal = "";
        private bool IsPaint = false;
        private EventEDC _eventEDC;
        private string _invoiceNo = "";
        private string _approveCode = "";
        public EDCResult edcResult = new EDCResult();
        public SaleProcess saleProcess = new SaleProcess();


        //public string ApproveCode = "";

        public struct EDCResult
        {
            public DialogResult dialogRes;
            public string ApproveCode;
            public string creditCard;
            public string paymentCode;
            public double edcAmount;
            public StoreResult res;
        }

        public frmEDCProcess()
        {
            InitializeComponent();
        }

        public frmEDCProcess(EventEDC eventVoid, string creditAmt, string sumTotal, string gTotal, string invoiceNo = "", string approveCode = "")
        {
            InitializeComponent();
            _sumTotal = sumTotal == "" ? "0.00" : sumTotal;
            _creditAmt = creditAmt == "" ? "0.00" : creditAmt;
            _gTotal = gTotal == "" ? "0.00" : gTotal;
            _eventEDC = eventVoid;
            _invoiceNo = invoiceNo;
            _approveCode = approveCode;
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

        private void frmEDCProcess_Shown(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            try
            {
                lbTxtCash.Text = _gTotal;
                lbTxtPayment.Text = _sumTotal;
                lbTxtBalance.Text = (Convert.ToDouble(_gTotal) + Convert.ToDouble(_sumTotal)).ToString(ProgramConfig.amountFormatString);

                if (_eventEDC == EventEDC.NormalSale)
                {
                    worker.DoWork += (s, de) =>
                    {
                        de.Result = EDC_NormalSaleProcess();
                    };

                }
                else if (_eventEDC == EventEDC.Void)
                {
                    worker.DoWork += (s, de) =>
                    {
                        de.Result = EDC_VoidProcess();
                    };
                }
                else if (_eventEDC == EventEDC.Return)
                {
                    worker.DoWork += (s, de) =>
                    {
                        de.Result = EDC_ReturnProcess();
                    };
                }

                worker.RunWorkerCompleted += (s, re) =>
                {
                    worker.Dispose();
                    this.Dispose();
                    edcResult = (EDCResult)re.Result;
                };
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
            catch (Exception)
            {
                worker.Dispose();
                this.Dispose();
            }
        }

        private EDCResult EDC_ReturnProcess()
        {
            edcControl1.EnableLog = true;
            edcControl1.EDCKey = "D9t42q$Or5tHw6";
            edcControl1.EDCPort = ProgramConfig.comPort;
            edcControl1.EDCPortSetting = "9600,N,8,1";
            edcControl1.EDCTimeout = 90000;

            if (edcControl1.IsProcess)
            {
                edcControl1.EDCClose();
                return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(ResponseCode.Error, "EDC กำลังทำงานอยู่ กรุณาลองอีกครั้ง") };
            }

            if (edcControl1.EDCOpen())
            {

                if (edcControl1.SendRefund(Convert.ToDouble(_creditAmt)))
                {
                    if (edcControl1.EDCStatusCode == "900")
                    {
                        edcControl1.EDCClose();
                        return new EDCResult()
                        {
                            dialogRes = System.Windows.Forms.DialogResult.Yes
                        };
                    }
                }

                string errMsg = edcControl1.EDCStatus;
                edcControl1.EDCClose();
                return new EDCResult()
                {
                    dialogRes = System.Windows.Forms.DialogResult.No,
                    res = new StoreResult(ResponseCode.Error, errMsg)
                };
            }
            else
            {
                edcControl1.EDCClose();
                return new EDCResult()
                {
                    dialogRes = System.Windows.Forms.DialogResult.No,
                    res = new StoreResult(ResponseCode.Error, "ไม่สามารถเชื่อมต่อ EDC ได้")
                };
            }
        }

        private EDCResult EDC_NormalSaleProcess()
        {
            try
            {
                edcControl1.EnableLog = true;
                edcControl1.EDCKey = "D9t42q$Or5tHw6";
                edcControl1.EDCPort = ProgramConfig.comPort;
                edcControl1.EDCPortSetting = "9600,N,8,1";
                edcControl1.EDCTimeout = 90000;

                var res = saleProcess.SumSalesVat2EDC("", _creditAmt, "CASH", "", "SA");

                string vat_edc = "";
                if (res.response.next)
                {
                    vat_edc = res.otherData.Rows[0]["EDC_VATAMOUNT"].ToString();
                    //edcControl1.IVat_Amt_info = vat_edc;
                }

                if (edcControl1.IsProcess)
                {
                    edcControl1.EDCClose();
                    return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(ResponseCode.Error, "EDC กำลังทำงานอยู่ กรุณาลองอีกครั้ง") };
                }

                int ret = 0;
                string rcvMode = "";

                if (edcControl1.EDCOpen())
                {
                    //Recheck Cobrand
                    //If paymentType = CEnum.PaymentType.AEON_BIGC Then
                    //    EdcControl1.IPrimary_Account_Number = CoBrandCard
                    //    EdcControl1.ICardRangeSelector = "B"
                    //End If

                    if (edcControl1.SendSale_VerifoneSCO(Convert.ToDouble(_creditAmt), "", ""))
                    {
                        if (lbEDCMessage.InvokeRequired)
                        {
                            lbEDCMessage.BeginInvoke((MethodInvoker)delegate { lbEDCMessage.Text = "กรุณาเสียบบัตร"; this.Refresh(); });
                        }
                        else
                        {
                            lbEDCMessage.Text = "กรุณาเสียบบัตร";
                        }

                        do
                        {
                            ret = edcControl1.RcvVerifoneSCO(rcvMode);
                            switch (ret)
                            {
                                //Fix language
                                case 10:
                                    if (lbEDCMessage.InvokeRequired)
                                    {
                                        lbEDCMessage.BeginInvoke((MethodInvoker)delegate { lbEDCMessage.Text = "กรุณากด Pin code"; this.Refresh(); });
                                    }
                                    else
                                    {
                                        lbEDCMessage.Text = "กรุณากด Pin code";
                                    }                                    
                                    rcvMode = "PR";
                                    break;
                                case 11:
                                    if (lbEDCMessage.InvokeRequired)
                                    {
                                        lbEDCMessage.BeginInvoke((MethodInvoker)delegate { lbEDCMessage.Text = "กรุณาเซ็นลายเซ็นที่เครื่อง SignPad"; this.Refresh(); });
                                    }
                                    else
                                    {
                                        lbEDCMessage.Text = "กรุณาเซ็นลายเซ็นที่เครื่อง SignPad";
                                    }  
                                    rcvMode = "SR";
                                    break;
                                case 12:
                                    if (lbEDCMessage.InvokeRequired)
                                    {
                                        lbEDCMessage.BeginInvoke((MethodInvoker)delegate { lbEDCMessage.Text = "กรุณารับบัตรเครดิตคืน\nจากเครื่องรูดบัตร"; this.Refresh(); });
                                    }
                                    else
                                    {
                                        lbEDCMessage.Text = "กรุณารับบัตรเครดิตคืน\nจากเครื่องรูดบัตร";
                                    }  
                                    rcvMode = "RC";
                                    break;
                                default:
                                    rcvMode = "";
                                    break;
                            }
                        } while (ret >= 10);

                        if (edcControl1.EDCStatusCode == "900")
                        {
                            string approveCode = edcControl1.OApproval_Code;
                            string invoiceNumber = edcControl1.OInvoice_Number;
                            double edcAmount = Convert.ToDouble(edcControl1.OAmount_Transaction) / 100;

                            res = saleProcess.SaveEDCTrans(edcControl1.OPrimary_Account_Number, edcControl1.OTerminal_Identification_Number, "", edcControl1.OApproval_Code,
                                                                edcControl1.OInvoice_Number, edcControl1.OTransaction_Date, edcAmount,
                                                                edcControl1.OCard_Issuer_Name, vat_edc);
                            if (res.response.next)
                            {
                                //Fix Data
                                string creditCard = res.otherData.Rows[0][0].ToString().Replace(" ", "");//"VISACTB.438679XX9923";
                                string paymentCode = "";//res.otherData.Rows[0][0].ToString().Replace(" ", "");
                                if (creditCard.Length >= 4)
                                {
                                    paymentCode = creditCard.Substring(0, 4);
                                }

                                edcControl1.EDCClose();
                                return new EDCResult() { 
                                    dialogRes = System.Windows.Forms.DialogResult.Yes, 
                                    ApproveCode = edcControl1.OApproval_Code,
                                    creditCard = creditCard,
                                    paymentCode = paymentCode,
                                    edcAmount = edcAmount
                                    };
                            }
                            else
                            {
                                edcControl1.EDCClose();
                                return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(res.response, res.responseMessage) };
                            }
                        }
                    }

                    string errMsg = edcControl1.EDCStatus;
                    edcControl1.EDCClose();
                    return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(ResponseCode.Error, errMsg) };
                }
                else
                {
                    edcControl1.EDCClose();
                    return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(ResponseCode.Error, "ไม่สามารถเชื่อมต่อ EDC ได้") };
                }
            }
            catch (Exception ex)
            {
                edcControl1.EDCClose();
                return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(ResponseCode.Error, ex.Message) };
            }
        }

        private EDCResult EDC_VoidProcess()
        {
            edcControl1.EnableLog = true;
            edcControl1.EDCKey = "D9t42q$Or5tHw6";
            edcControl1.EDCPort = ProgramConfig.comPort;
            edcControl1.EDCPortSetting = "9600,N,8,1";
            edcControl1.EDCTimeout = 90000;

            if (edcControl1.IsProcess)
            {
                edcControl1.EDCClose();
                return new EDCResult() { dialogRes = System.Windows.Forms.DialogResult.No, ApproveCode = "", res = new StoreResult(ResponseCode.Error, "EDC กำลังทำงานอยู่ กรุณาลองอีกครั้ง") };
            }

            if (edcControl1.EDCOpen())
            {
                if (edcControl1.SendVoid_Verifone(_invoiceNo, _approveCode))
                {
                    if (edcControl1.EDCStatusCode == "900")
                    {
                        edcControl1.EDCClose();
                        return new EDCResult()
                        {
                            dialogRes = System.Windows.Forms.DialogResult.Yes
                        };
                    }
                }

                string errMsg = edcControl1.EDCStatus;
                edcControl1.EDCClose();
                return new EDCResult()
                {
                    dialogRes = System.Windows.Forms.DialogResult.No,
                    res = new StoreResult(ResponseCode.Error, errMsg) 
                };
            }
            else
            {
                edcControl1.EDCClose();
                return new EDCResult()
                {
                    dialogRes = System.Windows.Forms.DialogResult.No,
                    res = new StoreResult(ResponseCode.Error, "ไม่สามารถเชื่อมต่อ EDC ได้") 
                };
            }
        }
    }
}
