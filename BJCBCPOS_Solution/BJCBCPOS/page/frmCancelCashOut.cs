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
    public partial class frmCancelCashOut : Form
    {
        private CashOutProcess process = new CashOutProcess();
        private frmNotify notify;
        private UCItemCancelCashOut lastICO = new UCItemCancelCashOut();

        public frmCancelCashOut()
        {
            InitializeComponent();
        }

        private void frmCancelCashOut_Load(object sender, EventArgs e)
        {
            try
            {
                ClearBankNote();
                Profile check = ProgramConfig.getProfile(FunctionID.CancelCashOut_DisplayCashOut);
                if (check.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("CancelCashOut", check.diffUserStatus);
                    //auth.function = FunctionID.CancelCashOut_DisplayCashOut;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    this.Dispose();
                    //    return;
                    //}


                    if (!Utility.CheckAuthPass(this, check, "CancelCashOut"))
                    {
                        this.Dispose();
                        return;
                    }
                }

                StoreResult result = process.posDisplayContent();
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

                StoreResult res = process.displayCashOut();
                if(res.response.next)
                {
                    if (res.otherData != null && res.otherData.Rows[0]["ReferenceNo"] + "" != "N/A")
                    {
                        int cnt = res.otherData.Rows.Count;

                        foreach (DataRow dr in res.otherData.Rows)
                        {
                            UCItemCancelCashOut itm = new UCItemCancelCashOut();
                            if (cnt % 2 != 0)
                            {
                                itm.BackColor = Color.White;
                            }
                            else
                            {
                                itm.BackColor = Color.FromArgb(225, 225, 225);
                            }

                            itm.lbNoText = cnt--.ToString();
                            itm.lbEnvelopNumText = Convert.ToInt32(dr["BAGNO"]) + "";
                            itm.lbPriceText = Convert.ToDouble(dr["Amount"]).ToString(ProgramConfig.amountFormatString);
                            itm.lbRefNoText = dr["ReferenceNo"] + "";
                            itm.lbCurrencyText = (dr["PaymentCode"] + "").Trim();
                            itm.DeleteItem += DeleteItem_Click;
                            itm.ClickItem += ClickItem_Click;
                            pn_item.Controls.Add(itm);
                        }
                    }                    
                }
                else
                {                    
                    frmNotify dialog = new frmNotify(res);
                    dialog.ShowDialog(this);                    
                }
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    this.Dispose();
                    //frmCancelCashOut_Load(sender, e);
                }                
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        public void DeleteItem_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                UCItemCancelCashOut itm = (UCItemCancelCashOut)sender;
                frmConfirmCancelCashOut cfCancel = new frmConfirmCancelCashOut();
                cfCancel.envelopNumber = itm.lbEnvelopNumText;
                cfCancel.cashAmt = itm.lbPriceText;
                frmLoading.closeLoading();
                DialogResult resDialog = cfCancel.ShowDialog(this);
                frmLoading.showLoading();
                if (resDialog == System.Windows.Forms.DialogResult.Yes)
                {                   
                    process.newTransaction();
                    StoreResult res = process.updateStatusCancelCashOut(itm.lbRefNoText);
                    if (res.response != ResponseCode.Success)
                    {
                        process.rollback();
                        frmLoading.closeLoading();
                        notify = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                        notify.ShowDialog(this);
                        return;
                    }
                    process.commit();
                    frmLoading.closeLoading();
                    string responseMessage = ProgramConfig.message.get("frmCancelCashOut", "SaveCancelCashOutComplete").message;
                    string helpMessage = ProgramConfig.message.get("frmCancelCashOut", "SaveCancelCashOutComplete").help;
                    notify = new frmNotify(ResponseCode.Success, responseMessage, res.helpMessage);

                    //notify = new frmNotify(ResponseCode.Success, "บันทึกยกเลิกการส่งเงินตามรอบเรียบร้อยแล้ว", res.helpMessage);
                    notify.ShowDialog(this);
                    this.Dispose();
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                process.rollback();
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    DeleteItem_Click(sender, e);
                //}                
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void ClearBankNote()
        {
            for (int i = 1; i <= 10; i++)
            {
                Label lbAmount = pn_MainBankNote.Controls.Find("lbAmount" + i, true).FirstOrDefault() as Label;
                lbAmount.Text = "";
                lbAmount.Tag = "";

                Label lbNotBank = pn_MainBankNote.Controls.Find("lbBankNote" + i, true).FirstOrDefault() as Label;
                lbNotBank.Text = "";
                lbNotBank.Tag = "";
            }
            lb_CurrencyCode.Text = "";
        }

        public void ClickItem_Click(object sender, EventArgs e)
        {
            ClearBankNote();
            UCItemCancelCashOut ucGV = (UCItemCancelCashOut)sender;

            StoreResult res = process.displayCashOutDetail(ucGV.lbRefNoText, "LAK");

            if (res.otherData != null)
            {
                if (res.otherData.Rows.Count > 0)
                {
                    InitialPanelBankNote(res.otherData);
                }
            }

            if (lastICO != ucGV)
                UCItemCancelCashOut.LostFocusItem(lastICO);

            lastICO = ucGV;
        }

        private void InitialPanelBankNote(DataTable dt)
        {
            int cnt = 0;
            lb_CurrencyCode.Text = "LAK"; //TODO: Change data from stp
            foreach (DataRow dr in dt.Rows)
            {
                if (cnt++ <= 10)
                {
                    string amt = Convert.ToDouble(dr["Unit_Amt"]).ToString(ProgramConfig.amountFormatString);
                    string qnt = Convert.ToDouble(dr["CASHCNT"]).ToString(ProgramConfig.amountFormatString);

                    Label lbBankNote = pn_MainBankNote.Controls.Find("lbBankNote" + cnt, true).FirstOrDefault() as Label;
                    Label lbAmount = pn_MainBankNote.Controls.Find("lbAmount" + cnt, true).FirstOrDefault() as Label;

                    lbBankNote.Text = amt;
                    lbAmount.Text = qnt;                   
                }
            }
        }

        private void ucHeader2_MainMenuClick(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
