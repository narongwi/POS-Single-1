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
using MMFSAPI;
using ApiServices;

namespace BJCBCPOS
{
    public partial class SubOtherService1 : Form
    {
        SaleProcess saleProcess = new SaleProcess();
        ProductAndServiceProcess process = new ProductAndServiceProcess();
        string displayAmt = ProgramConfig.amountFormatString;
        string strState = "";

        private struct StateAction
        {
            public const string SelectInvoice = "SelectInvoice";
            public const string DeleteInvoice = "DeleteInvoice";
        }

        public SubOtherService1()
        {
            InitializeComponent();
        }

        private void SubOtherService1_Load(object sender, EventArgs e)
        {
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.ReceivePOD;
            if (ProgramConfig.salePageState == 0)
            {
                ClearValue();
                FormLoad();
            }
        }

        private void SubOtherService1_Activated(object sender, EventArgs e)
        {
            if (ProgramConfig.salePageState == 2)
            {
                ProgramConfig.salePageState = 3;
                ClearValue();
                FormLoad();
                GC.Collect();
                ProgramConfig.salePageState = 1;
            }
        }

        private void FormLoad()
        {
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.CreditSale;

            var result = process.getRunning(FunctionID.CreditSale_GetRunning, RunningReceiptID.CreditSale);
            if (result.response.next)
            {
                ProgramConfig.creditSaleNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.creditSaleRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                lbTxtRefNo.Text = ProgramConfig.creditSaleNo;
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

            btnPay.BackgroundImage = Properties.Resources.btn_payment_disable;
            btnPay.Enabled = false;

            pn_Pay.Enabled = false;
            pn_Order.Enabled = false;
            ucHeader1.btnMember_Click(null, null);
        }

        private void ClearValue()
        {
            panel1.Controls.Clear();
            panel5.Controls.Clear();
            lbSumAmt.Text = "0.00";
            lbSumPay.Text = "0.00";
            pn_Pay.BackgroundImage = Properties.Resources.panel_item_credisale_disable;
            pn_Order.BackgroundImage = Properties.Resources.panel_item_credisale_disable;
            pn_Pay.Enabled = false;
            pn_Order.Enabled = false;
        }

        private void ItemLeftClick(object sender, EventArgs e)
        {
            UCListOtherService ucLstClk = (UCListOtherService)sender;
            ucLstClk.ItemClick -= ItemLeftClick;
            panel5.Controls.Remove(ucLstClk);

            ucLstClk.ItemClick += ItemRightClick;
            panel1.Controls.Add(ucLstClk);

            double amt = Convert.ToDouble(ucLstClk.label1.Text);

            lbSumPay.Text = (Convert.ToDouble(lbSumPay.Text) + amt).ToString(displayAmt);
            lbSumAmt.Text = (Convert.ToDouble(lbSumAmt.Text) - amt).ToString(displayAmt);
            label4.Text = ucLstClk.label3.Text;

            FocusPanalOrder();
        }

        private void ItemRightClick(object sender, EventArgs e)
        {
            UCListOtherService ucLstClk = (UCListOtherService)sender;

            ucLstClk.ItemClick -= ItemRightClick;
            panel1.Controls.Remove(ucLstClk);

            ucLstClk.ItemClick += ItemLeftClick;
            panel5.Controls.Add(ucLstClk);

            double amt = Convert.ToDouble(ucLstClk.label1.Text);

            lbSumPay.Text = (Convert.ToDouble(lbSumPay.Text) - amt).ToString(displayAmt);
            lbSumAmt.Text = (Convert.ToDouble(lbSumAmt.Text) + amt).ToString(displayAmt);
            label4.Text = ucLstClk.label3.Text;

            FocusPanalPay();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClearValue();
            this.Dispose();
        }

        private void ucTBScanBarcode_EnterFromButton(object sender, EventArgs e)
        {
            if (strState == StateAction.SelectInvoice)
            {
               var list = panel5.Controls.Cast<UCListOtherService>().ToList();
               foreach (UCListOtherService item in list)
               {
                   if (item.label3.Text == ucTBScanBarcode.Text.Trim())
                   {
                       item.ItemClick -= ItemLeftClick;
                       panel5.Controls.Remove(item);

                       item.ItemClick += ItemRightClick;
                       panel1.Controls.Add(item);

                       double amt = Convert.ToDouble(item.label1.Text);

                       lbSumPay.Text = (Convert.ToDouble(lbSumPay.Text) + amt).ToString(displayAmt);
                       lbSumAmt.Text = (Convert.ToDouble(lbSumAmt.Text) - amt).ToString(displayAmt);
                       return;
                   }
               }

               Utility.AlertMessage(ResponseCode.Error, "ไม่พบรายการ กรุณาลงอีกครั้ง");
            }
            else if (strState == StateAction.DeleteInvoice)
            {
                var list = panel1.Controls.Cast<UCListOtherService>().ToList();
                foreach (UCListOtherService item in list)
                {
                    if (item.label3.Text == ucTBScanBarcode.Text.Trim())
                    {
                        item.ItemClick -= ItemRightClick;
                        panel1.Controls.Remove(item);

                        item.ItemClick += ItemLeftClick;
                        panel5.Controls.Add(item);

                        double amt = Convert.ToDouble(item.label1.Text);

                        lbSumPay.Text = (Convert.ToDouble(lbSumPay.Text) - amt).ToString(displayAmt);
                        lbSumAmt.Text = (Convert.ToDouble(lbSumAmt.Text) + amt).ToString(displayAmt);

                        return;
                    }
                }

                Utility.AlertMessage(ResponseCode.Error, "ไม่พบรายการ กรุณาลงอีกครั้ง");
            }

            ucTBScanBarcode.InpTxt = "";
            ucTBScanBarcode.FocusTxt();
        }

        private void ucHeader1_MemberClick(object sender, EventArgs e)
        {
            var ucMem = (UCMember)sender;
            ucMem.eventName = "CreditSale";
            ucMem.functionID = FunctionID.CreditSale_SearchMember;
        }

        private void ucMember1_EnterFromButton(object sender, EventArgs e)
        {
            ClearValue();
           
            clsMMFSAPI sv = new clsMMFSAPI();
            CcosServices service = new CcosServices();
            ResponseListInvoiceAR resp = new ResponseListInvoiceAR();

            string resCode = "";
            string resMsg = "";
            string resMsgTH = "";
            clsMMFSAPI.invoice_list[] list = null;

            string custNo = ProgramConfig.memberProfileMMFormat.Customer_No;
            string custNo2 = ProgramConfig.memberCardNo;
            string paySTCode = ProgramConfig.storeCode;
            string createby = ProgramConfig.userId;

            AppLog.writeLog("Before ListInvoiceAR");

            //resp = service.ListInvoiceAR(custNo, paySTCode, createby);

            sv.listInvoiceAR(ref resCode, ref resMsg, ref resMsgTH, ref list, ref custNo, ref paySTCode, ref createby);

            double amt = 0.0;
            foreach (var item in list)
            {
                UCListOtherService ucLst = new UCListOtherService();
                ucLst.label3.Text = item.invoice_no;
                ucLst.label2.Text = item.Invoice_date;
                
                ucLst.ItemClick += ItemLeftClick;
                ucLst.StoreCode = item.store_code;
                ucLst.InvoiceNo = item.invoice_no;
                ucLst.InvoiceAmountAPI = item.invoice_amount.ToString(displayAmt);
                ucLst.InvoiceDate = item.Invoice_date;
                ucLst.TransDate = item.due_date;

                if (item.trans_type == "CN" && item.invoice_amount > 0)
                {
                    double amtCN = item.invoice_amount * -1;
                    ucLst.label1.Text = amtCN.ToString(displayAmt);
                    ucLst.InvoiceAmount = amtCN.ToString(displayAmt);
                    amt += amtCN;
                }
                else
                {
                    ucLst.label1.Text = item.invoice_amount.ToString(displayAmt);
                    ucLst.InvoiceAmount = item.invoice_amount.ToString(displayAmt);
                    amt += item.invoice_amount;
                }

                if (item.trans_type == "CN")
                {
                    ucLst.panel2.BackColor = Color.Red;
                }
                else
                {
                    ucLst.panel2.BackColor = Color.FromArgb(63, 184, 105);
                }

                panel5.Controls.Add(ucLst);
                panel5.Controls.SetChildIndex(ucLst, 0);
            }

            //if (resp.invoice_list != null)
            //{
            //    foreach (var item in resp.invoice_list)
            //    {
            //        UCListOtherService ucLst = new UCListOtherService();
            //        ucLst.label3.Text = item.invoice_no;
            //        ucLst.label2.Text = item.invoice_date;
            //        ucLst.label1.Text = item.invoice_amount;
            //        ucLst.ItemClick += ItemLeftClick;
            //        ucLst.StoreCode = item.store_code;
            //        ucLst.InvoiceNo = item.invoice_no;
            //        ucLst.InvoiceDate = item.invoice_date;
            //        ucLst.InvoiceAmount = item.invoice_amount;
            //        ucLst.TransDate = item.due_date;

            //        amt += Convert.ToDouble(item.invoice_amount);

            //        panel5.Controls.Add(ucLst);
            //        panel5.Controls.SetChildIndex(ucLst, 0);
            //    }
            //}
            //else
            //{
            //    Utility.AlertMessage(ResponseCode.Information, "ไม่พบรายการชำระคงค้าง");
            //    return;
            //}

            if (list.Length == 0)
            {
                Utility.AlertMessage(ResponseCode.Error, "ไม่พบรายการชำระคงค้าง");
                return;
            }

            pn_Order.Enabled = true;
            pn_Pay.Enabled = true;

            CheckEnableButtonPay();

            panelScanBarcode.BringToFront();
            lbSumAmt.Text = amt.ToString(displayAmt);
            //CheckEnableButtonPay();
            ucTBScanBarcode.FocusTxt();
        }

        private void CheckEnableButtonPay()
        {
            if (panel1.Controls.Count > 0 && Convert.ToDouble(lbSumPay.Text) >= 0)
            {
                btnPay.BackgroundImage = Properties.Resources.btn_Search_ReturnFromInvoice;
                btnPay.Enabled = true;
            }
            else
            {
                btnPay.BackgroundImage = Properties.Resources.btn_payment_disable;
                btnPay.Enabled = false;
            }
        }

        private void ucMember1_ButtonBackClick(object sender, EventArgs e)
        {
            ClearValue();
            this.Dispose();
        }

        private void ucMember1_IconClick(object sender, EventArgs e)
        {
            panelScanBarcode.BringToFront();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {  
            CreditSaleData credit = new CreditSaleData();
            credit.RefCreditPay = ProgramConfig.creditSaleNo;
            credit.CustomerID = ProgramConfig.memberProfileMMFormat.CustomerID;
            credit.CustomerNo = ProgramConfig.memberProfileMMFormat.Customer_No;
            credit.CustomerCardNo = ProgramConfig.memberCardNo;
            credit.Amount = Convert.ToDouble(lbSumPay.Text).ToString(displayAmt);
            credit.Status = "A";
            credit.ListCreditSaleDetail = new List<CreditSaleDataDetail>();
            var list = panel1.Controls.Cast<UCListOtherService>().ToList();
            int cnt = 1;
            foreach (var item in list)
            {
               var detail = new CreditSaleDataDetail();
               detail.StoreCode = item.StoreCode;
               detail.RefCreditPay = ProgramConfig.creditSaleNo;
               detail.Seq = cnt.ToString();
               detail.Credit_InvoiceNo = item.InvoiceNo;
               detail.Credit_InvoiceDate = item.InvoiceDate;
               detail.Credit_Amount = item.InvoiceAmount;
               detail.Credit_AmountAPI = item.InvoiceAmountAPI;
               detail.Status = "A";
               detail.TransDate = item.TransDate;
               credit.ListCreditSaleDetail.Add(detail);
               cnt++;
            }

            var res = process.saveCreditSaleData(credit);
            if (!res.response.next)
            {
                Utility.AlertMessage(res);
                return;
            }

            ShowPayment();
        }

        private void ShowPayment()
        {
            ProgramConfig.disValue = "0.00";
            ProgramConfig.totalValue = lbSumPay.Text;

            ProgramConfig.paymentOpenCashDrawer = FunctionID.CreditSale_OpenDrawerAndRecordTime;
            ProgramConfig.paymentCloseCashDrawer = FunctionID.CreditSale_CloseDrawerAndRecordTime;
            ProgramConfig.paymentFunction = FunctionID.CreditSale_Payment;
            ProgramConfig.pageBackFromPayment = PageBackFormPayment.CreditSale;


            if (Convert.ToDouble(lbSumPay.Text) == 0)
            {
                saleProcess.savePaymentCashBalance("0.00", "0.00", "CASH", "0.00", "", "", "");
                Program.control.CloseForm("frmPayment");
                Program.control.ShowForm("frmPayment");
            }
            else
            {
                Program.control.CloseForm("frmPayment");
                Program.control.ShowForm("frmPayment");
            }
        }

        public void FocusPanalOrder()
        {
            lbAction.Text = "Select Invice";
            pn_Order.BackgroundImage = Properties.Resources.panel_item_credisale_enable;
            pn_Pay.BackgroundImage = Properties.Resources.panel_item_credisale_disable;
            strState = StateAction.SelectInvoice;
            CheckEnableButtonPay();
        }

        public void FocusPanalPay()
        {
            lbAction.Text = "Delete Invice";
            pn_Order.BackgroundImage = Properties.Resources.panel_item_credisale_disable;
            pn_Pay.BackgroundImage = Properties.Resources.panel_item_credisale_enable;
            strState = StateAction.DeleteInvoice;
            CheckEnableButtonPay();
        }

        private void pn_Order_Click(object sender, EventArgs e)
        {
            FocusPanalOrder();
        }

        private void pn_Pay_Click(object sender, EventArgs e)
        {
            FocusPanalPay();
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }


}
