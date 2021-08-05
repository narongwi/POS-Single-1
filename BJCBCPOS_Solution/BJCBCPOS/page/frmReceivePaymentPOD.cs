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
    public partial class frmReceivePaymentPOD : Form
    {
        ProductAndServiceProcess process = new ProductAndServiceProcess();
        SaleProcess saleProcess = new SaleProcess();
        string displayAmt = ProgramConfig.amountFormatString;

        public frmReceivePaymentPOD()
        {
            InitializeComponent();
        }

        private void frmReceivePaymentPOD_Shown(object sender, EventArgs e)
        {
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.ReceivePOD;
            if (ProgramConfig.salePageState == 0)
            {
                FormLoad();
            }
        }

        private void frmReceivePaymentPOD_Activated(object sender, EventArgs e)
        {
            if (ProgramConfig.salePageState == 2)
            {
                ProgramConfig.salePageState = 3;
                ClearValue();
                FormLoad();
                GC.Collect();
                ucTBScanBarcode.FocusTxt();
                ProgramConfig.salePageState = 1;
            }
        }

        private void ClearValue()
        {
            double amtPrice = 0.0;
            lbTxtSubtotal.Text = amtPrice.ToString(displayAmt);
            lbTxtdiscount1.Text = amtPrice.ToString(displayAmt);
            lbTxtTotal.Text = amtPrice.ToString(displayAmt);
            panel3.Controls.Clear();
            ProgramConfig.podQRCode = "";
        }

        private void FormLoad()
        {
            ProgramConfig.printInvoiceType = PrintInvoiceType.RELATECUSTOMER;
            var result = process.getRunning(FunctionID.Deposit_GetRunning, RunningReceiptID.POD);
            if (result.response.next)
            {
                ProgramConfig.podRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.podRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                lbTxtRefNo.Text = ProgramConfig.podRefNo;
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

            btnPayment.Visible = false;
        }


        private void ucTBScanBarcode_EnterFromButton(object sender, EventArgs e)
        {
            //pos_PODGetOrder
            var res = process.PODGetOrder(ucTBScanBarcode.InpTxt);
            if (res.response.next)
            {
                ProgramConfig.podRefFFTI = ucTBScanBarcode.InpTxt.Trim();
                double amt = 0.0;
                panel3.Controls.Clear();
                foreach (DataRow dr in res.otherData.Rows)
                {
                    UCListItemPOD uc = new UCListItemPOD();
                    uc.UCPOD_lbPaymentDesc.Text = dr["PaymentDesc"].ToString();
                    uc.UCPOD_Qty.Text = Convert.ToDouble(dr["Qty"].ToString()).ToString("F3");
                    uc.UCPOD_lbAmount.Text = Convert.ToDouble(dr["Amount"].ToString()).ToString(displayAmt);
                    uc.UCPOD_lbChannel.Text = dr["Channel"].ToString();

                    if (dr["PaymentDesc"].ToString().Trim() == "CN")
                    {
                        amt -= Convert.ToDouble(dr["Amount"].ToString());
                    }
                    else
                    {
                        amt += Convert.ToDouble(dr["Amount"].ToString());
                    }
                    panel3.Controls.Add(uc);
                    panel3.Controls.SetChildIndex(uc, 0);
                }
                lbTxtTotal.Text = amt.ToString(displayAmt);
                lbTxtSubtotal.Text = amt.ToString(displayAmt);
                btnPayment.Visible = true;
            }
            else
            {
                Utility.AlertMessage(res);
            }

            Utility.SetGridColorAlternate<UCListItemPOD>(panel3.Controls.Cast<UCListItemPOD>().ToList(), Color.FromArgb(225, 225, 225));
            ucTBScanBarcode.Text = "";
            ucTBScanBarcode.FocusTxt();
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            this.Dispose();
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

            ProgramConfig.paymentOpenCashDrawer = FunctionID.ReceivePOD_OpenDrawerAndRecordTime;
            ProgramConfig.paymentCloseCashDrawer = FunctionID.ReceivePOD_CloseDrawerAndRecordTime;
            ProgramConfig.paymentFunction = FunctionID.ReceivePOD_Payment;
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.ReceivePOD;
            Program.control.CloseForm("frmPayment");
            Program.control.ShowForm("frmPayment");
        }

        private void btnPayment_VisibleChanged(object sender, EventArgs e)
        {
            btnConfirm.Visible = !btnPayment.Visible;
        }


    }
}
