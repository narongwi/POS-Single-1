using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Process;
using BJCBCPOS_Model;
using System.Collections;

namespace BJCBCPOS
{
    public partial class frmMainMenu : Form
    {
        public frmSale frmSale;
        public bool needUpdate = false;

        private MenuProcess process = new MenuProcess();
        private SaleProcess saleProcess = new SaleProcess();
        private CashInProcess cashinprocess = new CashInProcess();
        private CashOutProcess cashoutProcess = new CashOutProcess();
        private ReportProcess reportProcess = new ReportProcess();
        private ReturnProcess returnProcess = new ReturnProcess();
        private VoidProcess voidProcess = new VoidProcess();
        public int cashierMode = 0;
        public int btnReturnCount = 0;
        private int[] x_location = { 0, 12, 270, 525, 783 };
        private int[] y_location = { 0, 214, 336, 456 };
        private frmNotify dialog;
        private Language currentMenuLanguage = new Language(0);

        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void updateMainMenu()
        {
            frmLoading.showLoading();
            // get menu from DB
            AppMessage.fillForm(ProgramConfig.language, this);
            StoreResult result = process.generateMenu();
            frmLoading.closeLoading();
            if (result.response.next)
            {
                try
                {
                    currentMenuLanguage = ProgramConfig.language;
                    if (result.otherData != null && result.otherData.Rows != null)
                    {
                        DataTable allMenu = result.otherData;
                        int menuStatus;

                        DataTable MainGroup = allMenu.AsEnumerable()
                            .GroupBy(x => new { MenuNo = x["MenuNo"], HeadMenu = x["HeadMenu"] })
                            .Select(x => x.First())
                            .CopyToDataTable();

                        foreach (DataRow main in MainGroup.Rows)
                        {
                            DataTable menuGroup = allMenu.AsEnumerable()
                                .Where(x => x["MenuNo"].ToString() == main["MenuNo"].ToString() && x["HeadMenu"].ToString() == main["HeadMenu"].ToString())
                                .GroupBy(x => new { MenuSeq = x["MenuSeq"], MenuName = x["MenuName"] })
                                .Select(x => x.First())
                                .CopyToDataTable();

                            if (menuGroup != null)
                            {
                                foreach (DataRow menu in menuGroup.Rows)
                                {
                                    menuStatus = 1;
                                    DataTable subGroup = allMenu.AsEnumerable()
                                    .Where(x => x["MenuNo"].ToString() == main["MenuNo"].ToString() && x["HeadMenu"].ToString() == main["HeadMenu"].ToString() && x["MenuSeq"].ToString() == menu["MenuSeq"].ToString() && x["MenuName"].ToString() == menu["MenuName"].ToString())
                                    .CopyToDataTable();

                                    if (subGroup.Rows.Count > 1)
                                    {
                                        foreach (DataRow sub in subGroup.Rows)
                                        {
                                            //  change sub menu display text
                                            if (Convert.ToInt32(sub["MenuStatus"].ToString()) == 2)
                                            {
                                                // enable sub menu
                                                menuStatus = 2;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int.TryParse(subGroup.Rows[0]["MenuStatus"].ToString(), out menuStatus);
                                    }

                                    // change menu display text map with function Id
                                    Button currentBtn = null;
                                    string functionId = subGroup.Rows[0]["FunctionID"].ToString().Replace("-", "");
                                    if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_OpenDay.value.Substring(0, 9))
                                    {
                                        currentBtn = btnOpenTransaction;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_CashIn.value.Substring(0, 9))
                                    {
                                        currentBtn = btnCashBalance;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_Sale.value.Substring(0, 9))
                                    {
                                        currentBtn = btnSell;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_Return_ByReceipt.value.Substring(0, 9))
                                    {
                                        currentBtn = btnReturn;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_Void.value.Substring(0, 9))
                                    {
                                        currentBtn = btnVoid;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_CashOut.value.Substring(0, 9))
                                    {
                                        currentBtn = btnCashOut;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_EndOfShift.value.Substring(0, 9))
                                    {
                                        currentBtn = btnCloseCashier;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_EndOfDay.value.Substring(0, 9))
                                    {
                                        currentBtn = btnCloseTransaction;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_Tool_ChangePassword.value.Substring(0, 9))
                                    {
                                        currentBtn = btnTool;
                                    }
                                    else if (functionId.Substring(0, 9) == FunctionID.Login_DisplayMainMenu_Report_CheckDaySale.value.Substring(0, 9))
                                    {
                                        currentBtn = btnReport;
                                    }

                                    if (currentBtn != null)
                                    {
                                        if (currentBtn.InvokeRequired)
                                        {
                                            currentBtn.BeginInvoke((MethodInvoker)delegate
                                            {
                                                currentBtn.Location = new Point(x_location[(int)main["MenuNo"]], y_location[(int)menu["MenuSeq"]]);
                                                currentBtn.Text = subGroup.Rows[0]["MenuName"].ToString();
                                                currentBtn.Enabled = (menuStatus == 2);
                                                if (currentBtn.Enabled == true)
                                                {
                                                    currentBtn.BackgroundImage = Properties.Resources.payment_enable;
                                                }
                                                else
                                                {
                                                    currentBtn.BackgroundImage = Properties.Resources.payment_disable;
                                                }
                                                currentBtn.Refresh();
                                            });
                                        }
                                        else
                                        {
                                            currentBtn.Location = new Point(x_location[(int)main["MenuNo"]], y_location[(int)menu["MenuSeq"]]);
                                            currentBtn.Text = subGroup.Rows[0]["MenuName"].ToString();
                                            currentBtn.Enabled = (menuStatus == 2);
                                            if (currentBtn.Enabled == true)
                                            {
                                                currentBtn.BackgroundImage = Properties.Resources.payment_enable;
                                            }
                                            else
                                            {
                                                currentBtn.BackgroundImage = Properties.Resources.payment_disable;
                                            }
                                            currentBtn.Refresh();
                                        }
                                    }
                                }
                            }

                            // change head menu display text
                            Control[] head = this.Controls.Find("lbHeadMenu" + main["MenuNo"], true);
                            if (head != null && head.Length == 1)
                            {
                                if (head[0].InvokeRequired)
                                {
                                    head[0].BeginInvoke((MethodInvoker)delegate
                                    {
                                        head[0].Text = main["HeadMenu"].ToString();
                                        head[0].Refresh();
                                    });
                                }
                                else
                                {
                                    head[0].Text = main["HeadMenu"].ToString();
                                    head[0].Refresh();
                                }
                            }
                        }
                    }
                }
                catch (NetworkConnectionException net)
                {
                    //throw;
                    CatchNetWorkException(net);
                }
                catch (Exception ex)
                {
                    dialog = new frmNotify(ResponseCode.Error, ex.Message);
                    dialog.ShowDialog(this);
                }
            }
        }

        private void updateCashierMessage()
        {
            frmLoading.showLoading();
            ProcessResult res = process.cashireMessageStatus();
            frmLoading.closeLoading();
            ucHeader1.alertFunctionID = FunctionID.Login_CashierMessage_Status;
            ucHeader1.alertEnabled = true;
            if (res.response.next)
            {
                if (ProgramConfig.enableCashierMessage)
                {
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
                    ucHeader1.alertEnabled = false;
                }
            }
            else
            {
                dialog = new frmNotify(res);
                dialog.ShowDialog(this);
            }
        }

        private void btnOpenTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                ProcessResult res = process.checkOpenDayAuthorize();
                if (res.response.next)
                {
                    if (res.needNextProcess)
                    {
                        //frmUserAuthorize auth = new frmUserAuthorize("OpenDay", "2");
                        //auth.function = FunctionID.OpenDay_SelectOpenDayMenu;
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    frmLoading.closeLoading();
                        //    return;
                        //}

                        if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.OpenDay_SelectOpenDayMenu }, "OpenDay"))
                        {
                            frmLoading.closeLoading();
                            return;
                        }
                    }
                    StoreResult result = process.checkOpenDayofTill();
                    if (result.response.next)
                    {
                        //if (panelOpenTransaction.InvokeRequired)
                        //{
                        //    panelOpenTransaction.BeginInvoke((MethodInvoker)delegate { panelOpenTransaction.BringToFront(); });
                        //}
                        //else
                        //{
                        //    panelOpenTransaction.BringToFront();
                        //}
                        //if (lbTextHeaderMain.InvokeRequired)
                        //{
                        //    lbTextHeaderMain.BeginInvoke((MethodInvoker)delegate { lbTextHeaderMain.Text = "เปิดงานประจำวัน"; });
                        //}
                        //else
                        //{
                        //    lbTextHeaderMain.Text = "เปิดงานประจำวัน";
                        //}
                        //if (lbConfirmOpen.InvokeRequired)
                        //{
                        //    lbConfirmOpen.BeginInvoke((MethodInvoker)delegate { lbConfirmOpen.Text = "ยืนยันการเปิด"; });
                        //}
                        //else
                        //{
                        //    lbConfirmOpen.Text = "ยืนยันการเปิด";
                        //}
                        //if (lbConfirmOpenTran.InvokeRequired)
                        //{
                        //    lbConfirmOpenTran.BeginInvoke((MethodInvoker)delegate { lbConfirmOpenTran.Text = "งานการขายประจำวัน?"; });
                        //}
                        //else
                        //{
                        //    lbConfirmOpenTran.Text = "งานการขายประจำวัน?";
                        //}
                        if (panelOpenTransaction.InvokeRequired)
                        {
                            panelOpenTransaction.BeginInvoke((MethodInvoker)delegate { panelOpenTransaction.BringToFront(); });
                        }
                        else
                        {
                            panelOpenTransaction.BringToFront();
                        }

                        if (lbOpentran.InvokeRequired)
                        {
                            lbOpentran.BeginInvoke((MethodInvoker)delegate { lbOpentran.BringToFront(); });
                        }
                        else
                        {
                            lbOpentran.BringToFront();
                        }

                        if (lbConfirmOpen.InvokeRequired)
                        {
                            lbConfirmOpen.BeginInvoke((MethodInvoker)delegate { lbConfirmOpen.BringToFront(); });
                        }
                        else
                        {
                            lbConfirmOpen.BringToFront();
                        }

                        if (lbConfirmOpenTran.InvokeRequired)
                        {
                            lbConfirmOpenTran.BeginInvoke((MethodInvoker)delegate { lbConfirmOpenTran.BringToFront(); });
                        }
                        else
                        {
                            lbConfirmOpenTran.BringToFront();
                        }

                        if (lbTxtTranSuccess.InvokeRequired)
                        {
                            lbTxtTranSuccess.BeginInvoke((MethodInvoker)delegate { lbTxtTranSuccess.BringToFront(); });
                        }
                        else
                        {
                            lbTxtTranSuccess.BringToFront();
                        }
                        cashierMode = 1;
                    }
                    else
                    {
                        dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                    }
                }
                else
                {
                    dialog = new frmNotify(res);
                    dialog.ShowDialog(this);
                }

                btnCancelOpenTran.Select();

                Profile chkMCashier = ProgramConfig.getProfile(FunctionID.OpenDay_GetMessageCashier);
                if (chkMCashier.policy == PolicyStatus.Work)
                {
                    ProcessResult result = process.cashireMessageStatusOpenDayMenu();
                    if (result.response.next)
                    {
                        if (result.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                        }

                        if (result.needNextProcess)
                        {
                            ucHeader1.alertFunctionID = FunctionID.OpenDay_GetMessageCashier;
                            ucHeader1.alertStatus = true;
                        }
                        else
                        {
                            ucHeader1.alertStatus = false;
                        }
                    }
                    else
                    {
                        frmLoading.closeLoading();
                        frmNotify dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                        panelMenu.BringToFront();
                        ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                        return;
                    }
                }
                frmLoading.closeLoading();
                ucFooter1.lbFunction.Text = FunctionID.OpenDay_ConfirmOpenDayProcess.formatValue;
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void btnCancelOpenTran_Click(object sender, EventArgs e)
        {
            panelMenu.BringToFront();
            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
            lbTextHeaderMain.BringToFront();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            //loading = new frmLoading();
            //loading.Show();
            //worker = new BackgroundWorker();
            //worker.DoWork += backgroundUpdate_DoWork;
            //worker.RunWorkerCompleted += backgroundUpdate_RunWorkerCompleted;
            //worker.RunWorkerAsync();
            //pnMainMenu.BringToFront();
            //panelMenu.BringToFront();

            //if (Screen.AllScreens.Length == 2)
            //{
            //    Program.control.ShowForm("frmMonitorCustomer");
            //    Program.control.ShowForm("frmMonitorCustomerFooter");
            //    Program.control.ShowForm("frmMonitor2Detail");
            //}
        }

        private void btnOpenTran_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                StoreResult result;
                if (cashierMode == 1) //OpenDay
                {
                    //OpenTran
                    ProcessResult res = process.openDay();
                    if (res.response.next)
                    {
                        if (res.needNextProcess)
                        {
                            result = process.printOpenDay();
                            if (result.otherData != null &&
                                result.otherData.Columns.Contains("Msg_text") &&
                                result.otherData.Columns.Contains("Msg_amt"))
                            {
                                // print open day
                                if (!Hardware.printTermal(result.otherData))
                                {
                                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "NotAllowPrintOpenDay").message;
                                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "NotAllowPrintOpenDay").help;
                                    dialog = new frmNotify(ResponseCode.Information, responseMessage, helpMessage);

                                    //dialog = new frmNotify(ResponseCode.Warning, "ไม่สามารถพิมพ์ใบเปิดงานประจำวันได้.");
                                    dialog.ShowDialog(this);
                                }
                                else
                                {
                                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "PrintOpenDayComplete").message;
                                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "PrintOpenDayComplete").help;
                                    dialog = new frmNotify(ResponseCode.Success, responseMessage, helpMessage);

                                    //dialog = new frmNotify(ResponseCode.Success, "พิมพ์ใบเปิดงานประจำวันสำเร็จ.");
                                    dialog.ShowDialog(this);
                                }
                            }
                            else
                            {
                                dialog = new frmNotify(result);
                                dialog.ShowDialog(this);
                            }
                        }
                        //lbTxtTranSuccess.BringToFront();
                        ////lbTxtTranSuccess.Text = "บันทึกเปิดงานประจำวันเรียบร้อยแล้ว";
                        //panelOpenTranSuccess.BringToFront();

                        res = process.saveToDataTankOpenDay();
                        if (res.response.next)
                        {
                            lbTxtTranSuccess.BringToFront();
                            panelOpenTranSuccess.BringToFront();
                        }
                        else
                        {
                            dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                            panelMenu.BringToFront();
                            lbTextHeaderMain.Text = "Main menu";
                            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                        }
                    }
                    else
                    {
                        dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        panelMenu.BringToFront();
                        lbTextHeaderMain.Text = "Main menu";
                        ucFooter1.lbFunction.Text = FunctionID.OpenDay_ConfirmOpenDayProcess.formatValue;
                    }

                }
                else if (cashierMode == 2)
                {
                    //CloseCashier
                    ProcessResult res = process.endOfShift();
                    if (res.response.next)
                    {
                        if (res.needNextProcess)
                        {
                            result = process.printEndOfShift((int)res.data);
                            if (result.otherData != null &&
                                result.otherData.Columns.Contains("Msg_text") &&
                                result.otherData.Columns.Contains("Msg_amt"))
                            {
                                // print open day
                                if (!Hardware.printTermal(result.otherData))
                                {
                                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "NotAllowPrintCashier").message;
                                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "NotAllowPrintCashier").help;
                                    dialog = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);

                                    //dialog = new frmNotify(ResponseCode.Warning, "ไม่สามารถพิมพ์ใบปิดงานแคชเชียร์ได้");
                                    dialog.ShowDialog(this);
                                }
                                else
                                {
                                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "PrintCashierComplete").message;
                                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "PrintCashierComplete").help;
                                    dialog = new frmNotify(ResponseCode.Success, responseMessage, helpMessage);

                                    //dialog = new frmNotify(ResponseCode.Success, "พิมพ์ใบปิดงานแคชเชียร์สำเร็จ");
                                    dialog.ShowDialog(this);
                                }
                            }
                            else
                            {
                                dialog = new frmNotify(result);
                                dialog.ShowDialog(this);
                            }
                        }

                        string rec = (string)res.data2;
                        process.syncToDataTank("CloseCashier", FunctionID.EndOfShift_SaveEndOfShiftTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.endOfShiftRefNo, rec);

                        //lbTxtTranSuccess.Text = "บันทึกปิดงานแคชเชียร์เรียบร้อยแล้ว";
                        lblSuccessCloseCash.BringToFront();
                        panelOpenTranSuccess.BringToFront();
                    }
                    else
                    {
                        dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        panelMenu.BringToFront();
                        lbTextHeaderMain.Text = "Main menu";
                        ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                    }
                }
                else if (cashierMode == 3)
                {
                    //CloseTran
                    ProcessResult res = process.endOfTill();
                    if (res.response.next)
                    {
                        if (res.needNextProcess)
                        {
                            result = process.printEndOfTill();
                            if (result.otherData != null &&
                                result.otherData.Columns.Contains("Msg_text") &&
                                result.otherData.Columns.Contains("Msg_amt"))
                            {

                                //Profile check = ProgramConfig.getProfile(FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank);
                                //if (check.policy == PolicyStatus.Work)
                                //{
                                //    string ref_no = result.otherData.Rows[0]["ReferenceNo"].ToString();
                                //    result = command.syncToDataTank("CloseDay", FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.endOfTillRefNo, ref_no, ProgramConfig.abbNo);
                                //}

                                // print open day
                                if (!Hardware.printTermal(result.otherData))
                                {
                                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "NotAllowPrintCloseDay").message;
                                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "NotAllowPrintCloseDay").help;
                                    dialog = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);

                                    //dialog = new frmNotify(ResponseCode.Warning, "ไม่สามารถพิมพ์ใบปิดงานประจำวันได้");
                                    dialog.ShowDialog(this);
                                }
                                else
                                {
                                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "PrintCloseDayComplete").message;
                                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "PrintCloseDayComplete").help;
                                    dialog = new frmNotify(ResponseCode.Success, responseMessage, helpMessage);

                                    //dialog = new frmNotify(ResponseCode.Success, "พิมพ์ใบปิดงานประจำวันสำเร็จ");
                                    dialog.ShowDialog(this);
                                }
                            }
                            else
                            {
                                dialog = new frmNotify(result);
                                dialog.ShowDialog(this);
                            }
                        }

                        string rec = (string)res.data;
                        process.syncToDataTank("CloseDay", FunctionID.OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.endOfTillRefNo, rec);

                        lbTxtCloseTranSuccess.BringToFront();
                        //lbTxtTranSuccess.Text = "บันทึกปิดงานประจำวันเรียบร้อยแล้ว";
                        panelOpenTranSuccess.BringToFront();
                    }
                    else
                    {
                        dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                        panelMenu.BringToFront();
                        lbTextHeaderMain.Text = "Main menu";
                        ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                    }
                }
                //panelOpenTranSuccess.BringToFront();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void btnOpenTranSuccess_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            if (cashierMode == 1)
            {
                panelMenu.BringToFront();
                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                //lbTextHeaderMain.Text = "Main menu";
                lbTextHeaderMain.BringToFront();
                frmLoading.closeLoading();
            }
            else if (cashierMode == 2)
            {
                StoreResult res = process.endOfShiftLogout();
                if (!res.response.next)
                {
                    frmLoading.closeLoading();
                    dialog = new frmNotify(res);
                    dialog.ShowDialog(this);
                    return;
                }
                panelMenu.BringToFront();
                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                //lbTextHeaderMain.Text = "Main menu";
                lbTextHeaderMain.BringToFront();
                Program.control.ShowForm("frmLogin");
                Program.control.CloseForm(this.Name);
                frmLoading.closeLoading();
            }
            else if (cashierMode == 3)
            {
                StoreResult res = process.endOfTillLogout();
                if (!res.response.next)
                {
                    frmLoading.closeLoading();
                    dialog = new frmNotify(res);
                    dialog.ShowDialog(this);
                    return;
                }
                panelMenu.BringToFront();
                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                //lbTextHeaderMain.Text = "Main menu";
                lbTextHeaderMain.BringToFront();
                frmLoading.closeLoading();
                Program.control.ExitProgram();
            }
        }

        private void btnCashBalance_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                if (checkBeforeCashin())
                {
                    Program.control.ShowForm("frmCashbalance");
                }
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {              
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private bool checkBeforeCashin()
        {
            StoreResult result = null;

            Profile check = ProgramConfig.getProfile(FunctionID.CashIn_CheckOpenDayofTillStatus);
            if (check.policy == PolicyStatus.Work)
            {
                result = cashinprocess.checkOpenDay();
                if (!result.response.next)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    return false;
                }
            }

            check = ProgramConfig.getProfile(FunctionID.CashIn_SelectCashInMenu);
            if (check.profile == ProfileStatus.NotAuthorize)
            {
                //frmUserAuthorize auth = new frmUserAuthorize("CashIn", check.diffUserStatus);
                //auth.function = FunctionID.CashIn_SelectCashInMenu;
                //DialogResult auth_res = auth.ShowDialog(this);
                //if (auth_res != DialogResult.Yes)
                //{
                //    return false;
                //}

                if (!Utility.CheckAuthPass(this, check, "CashIn"))
                {
                    return false;
                }
            }

            result = cashinprocess.getRunning(FunctionID.CashIn_GetRunningNo);
            if (!result.response.next)
            {
                frmNotify dialog = new frmNotify(result);
                dialog.ShowDialog(this);
                return false;
            }
            ProgramConfig.cashInRefNo = result.otherData.Rows[0]["ReferenceNo"] + "";
            ProgramConfig.cashInRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"] + "";

            check = ProgramConfig.getProfile(FunctionID.CashIn_CheckUserIDPasswordCurrentCashier);
            if (check.policy == PolicyStatus.Work)
            {
                //frmUserAuthorize auth = new frmUserAuthorize("CashIn", check.diffUserStatus);
                //auth.function = FunctionID.CashIn_CheckUserIDPasswordCurrentCashier;
                //DialogResult auth_res = auth.ShowDialog(this);
                //if (auth_res != DialogResult.Yes)
                //{
                //    return false;
                //}
                //check.profile = ProfileStatus.NotAuthorize;
                if (!Utility.CheckAuthPass(this, check, "CashIn"))
                {
                    return false;
                }
            }

            return true;
        }

        private void btnCancelCashBalance_Click(object sender, EventArgs e)
        {
            panelMenu.BringToFront();
            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
            lbTextHeaderMain.Text = "Main menu";
        }

        private void btnConfirmRecriveSubmit_Click(object sender, EventArgs e)
        {
            // call process submit cash in
            StoreResult result = process.cashIn();
            if (result.response == ResponseCode.Success)
            {
                // success submit cash in
                panelMenu.BringToFront();
                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                lbTextHeaderMain.Text = "Main menu";
            }
            else
            {
                dialog = new frmNotify(result);
                dialog.ShowDialog(this);
            }
        }

        private void btnCancelCashIn_Click(object sender, EventArgs e)
        {
            panelMenu.BringToFront();
            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
            lbTextHeaderMain.Text = "Main menu";
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                if (checkOpenDayFormSell())
                {
                    ProgramConfig.salePageState = 0;
                    //Program.control.ShowForm("frmMonitorCustomer");
                    //Program.control.ShowForm("frmMonitorCustomerFooter");
                    //Program.control.ShowForm("frmMonitor2Detail");
                    Program.control.ShowForm("frmSale");
                    foreach (Form item in Application.OpenForms)
                    {
                        if (item is frmSale)
                        {
                            frmSale = (frmSale)item;
                            frmSale.BringToFront();
                            //frmSale.panelScanBarcode.BringToFront();
                            //frmSale.ucTBScanBarcode.Select();
                            //frmSale.ucTBScanBarcode.Focus();
                            break;
                        }
                    }
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                dialog = new frmNotify(ResponseCode.Error, ex.Message);
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private bool checkOpenDayFormSell()
        {
            frmLoading.showLoading();
            ProcessResult checkOpenDay = saleProcess.checkOpenDaySuccess(FunctionID.Sale_CheckOpenDayofTillStatus);
            frmLoading.closeLoading();
            if (checkOpenDay.needNextProcess)
            {
                // check OpenDay error show alert
                frmNotify notify = new frmNotify(checkOpenDay);
                notify.ShowDialog(this);
                return false;
            }

            return true;
        }

        public void btnCloseCashier_Click(object sender, EventArgs e)
        {
            try
            {
                //panelOpenTransaction.BringToFront();
                //lbTextHeaderMain.Text = "ปิดงานแคชเชียร์";
                //lbConfirmOpen.Text = "ยืนยัน";
                //lbConfirmOpenTran.Text = "การปิดงานแคชเชียร์?";
                //cashierMode = 2;
                frmLoading.showLoading();
                StoreResult result = process.beforeEndOfShift();
                if (result.response.next)
                {
                    Profile chkMCashier = ProgramConfig.getProfile(FunctionID.EndOfShift_GetMessageCashier);
                    if (chkMCashier.policy == PolicyStatus.Work)
                    {
                        ProcessResult res = process.cashireMessageStatusOpenDayMenu();
                        if (res.response.next)
                        {
                            if (res.response == ResponseCode.Information)
                            {
                                frmNotify dialog = new frmNotify(res);
                                dialog.ShowDialog(this);
                            }

                            if (res.needNextProcess)
                            {
                                ucHeader1.alertFunctionID = FunctionID.EndOfShift_GetMessageCashier;
                                ucHeader1.alertStatus = true;
                            }
                            else
                            {
                                ucHeader1.alertStatus = false;
                            }
                        }
                        else
                        {
                            frmLoading.closeLoading();
                            frmNotify dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                            panelMenu.BringToFront();
                            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                            return;
                        }
                    }

                    Profile chkAuth = ProgramConfig.getProfile(FunctionID.EndOfShift_ConfirmEndOfShiftProcess_UserAuthorize);
                    if (chkAuth.policy == PolicyStatus.Work)
                    {
                        //frmUserAuthorize auth = new frmUserAuthorize("EndOfShift", chkAuth.diffUserStatus);
                        //auth.function = FunctionID.EndOfShift_ConfirmEndOfShiftProcess_UserAuthorize;
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    panelMenu.BringToFront();
                        //    frmLoading.closeLoading();
                        //    return;
                        //}
                        chkAuth.profile = ProfileStatus.NotAuthorize;
                        if (!Utility.CheckAuthPass(this, chkAuth, "EndOfShift"))
                        {
                            panelMenu.BringToFront();
                            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                            frmLoading.closeLoading();
                            return;
                        }
                    }

                    lbCloseCashier.BringToFront();
                    panelOpenTransaction.BringToFront();
                    ucFooter1.lbFunction.Text = FunctionID.EndOfShift_ConfirmEndOfShiftProcess.formatValue;
                    lbCloseCash.BringToFront();
                    lbCloseCashtxt.BringToFront();
                    lblSuccessCloseCash.BringToFront();
                    cashierMode = 2;
                }
                else
                {
                    dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                }
                btnCancelOpenTran.Select();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                dialog = new frmNotify(ResponseCode.Error, ex.Message);
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void btnCloseTransaction_Click(object sender, EventArgs e)
        {
            //panelOpenTransaction.BringToFront();
            //lbTextHeaderMain.Text = "ปิดงานประจำวัน";
            //lbConfirmOpen.Text = "ยืนยันการปิด";
            //lbConfirmOpenTran.Text = "งานการขายประจำวัน?";
            //cashierMode = 3;
            try
            {
                frmLoading.showLoading();
                Profile check = ProgramConfig.getProfile(FunctionID.EndOfTill_CheckOpenDayofTillStatus);
                if (check.policy == PolicyStatus.Work)
                {
                    StoreResult result = process.endOfTillOpenDay();
                    if (result.response.next)
                    {
                        if (result.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(result);
                            dialog.ShowDialog(this);
                        }

                    }
                    else
                    {
                        frmLoading.closeLoading();
                        frmNotify dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                        panelMenu.BringToFront();
                        ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                        return;
                    }

                }

                ProcessResult res = process.checkEndOfTillAuthorize();
                if (res.response.next)
                {
                    if (res.needNextProcess)
                    {
                        //frmUserAuthorize auth = new frmUserAuthorize("EndOfTill", "2");
                        //DialogResult auth_res = auth.ShowDialog(this);
                        //if (auth_res != DialogResult.Yes)
                        //{
                        //    frmLoading.closeLoading();
                        //    return;
                        //}
                        //Ness to fix สามารถใส่ได้ทั้ง user approve และ user ปกติ
                        if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.NoFunctionID }, "EndOfTill"))
                        {
                            frmLoading.closeLoading();
                            return;
                        }
                    }
                    StoreResult result = process.endOfTillGetRunning();
                    if (result.response.next)
                    {
                        Profile chkMCashier = ProgramConfig.getProfile(FunctionID.EndOfTill_GetMessageCashier);
                        if (chkMCashier.policy == PolicyStatus.Work)
                        {
                            ProcessResult result2 = process.cashireMessageStatusOpenDayMenu();
                            if (result2.response.next)
                            {
                                if (result2.response == ResponseCode.Information)
                                {
                                    frmNotify dialog = new frmNotify(result2);
                                    dialog.ShowDialog(this);
                                }

                                if (result2.needNextProcess)
                                {
                                    ucHeader1.alertFunctionID = FunctionID.EndOfTill_GetMessageCashier;
                                    ucHeader1.alertStatus = true;
                                }
                                else
                                {
                                    ucHeader1.alertStatus = false;
                                }
                            }
                            else
                            {
                                frmLoading.closeLoading();
                                frmNotify dialog = new frmNotify(result2);
                                dialog.ShowDialog(this);
                                panelMenu.BringToFront();
                                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                                return;
                            }
                        }

                        Profile chkAuth = ProgramConfig.getProfile(FunctionID.EndOfTill_ConfirmEndOfTillProcess_UserAuthorize);
                        if (chkAuth.policy == PolicyStatus.Work)
                        {
                            //frmUserAuthorize auth = new frmUserAuthorize("EndOfTill", chkAuth.diffUserStatus);
                            //auth.function = FunctionID.EndOfTill_ConfirmEndOfTillProcess_UserAuthorize;
                            //DialogResult auth_res = auth.ShowDialog(this);
                            //if (auth_res != DialogResult.Yes)
                            //{
                            //    panelMenu.BringToFront();
                            //    frmLoading.closeLoading();
                            //    return;
                            //}
                            chkAuth.profile = ProfileStatus.NotAuthorize;
                            if (!Utility.CheckAuthPass(this, chkAuth, "EndOfTill"))
                            {
                                panelMenu.BringToFront();
                                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                                frmLoading.closeLoading();
                                return;
                            }
                        }

                        lbCloseTran.BringToFront();
                        panelOpenTransaction.BringToFront();
                        ucFooter1.lbFunction.Text = FunctionID.EndOfTill_ConfirmEndOfTillProcess.formatValue;
                        lbConfirmClose.BringToFront();
                        lbConfirmOpenTran.BringToFront();
                        lbTxtCloseTranSuccess.BringToFront();
                        cashierMode = 3;
                    }
                    else
                    {
                        dialog = new frmNotify(result);
                        dialog.ShowDialog(this);
                    }
                }
                else
                {
                    dialog = new frmNotify(res);
                    dialog.ShowDialog(this);
                }

                btnCancelOpenTran.Select();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                dialog = new frmNotify(ResponseCode.Error, ex.Message);
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void btnMenu3_1_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                if (checkBeforeCashout())
                {
                    Program.control.ShowForm("frmCashout");
                }
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private bool checkBeforeCashout()
        {
            StoreResult result = null;
            Profile check = ProgramConfig.getProfile(FunctionID.CashOut_CheckOpenDayofTillStatus);
            if (check.policy == PolicyStatus.Work)
            {
                result = cashoutProcess.checkOpenDay();
                if (!result.response.next)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    return false;
                }
            }

            ProcessResult res = cashoutProcess.checkAuthorize();
            if (res.response.next)
            {
                if (res.needNextProcess)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("CashOut", "");
                    //auth.function = FunctionID.CashOut_SelectCashOutMenu;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    return false;
                    //}

                    if (!Utility.CheckAuthPass(this, new Profile() { profile = ProfileStatus.NotAuthorize, diffUserStatus = true, functionId = FunctionID.CashOut_SelectCashOutMenu }, "CashOut"))
                    {
                        return false;
                    }
                }
            }
            else
            {
                dialog = new frmNotify(res);
                dialog.ShowDialog(this);
                return false;
            }

            res = cashoutProcess.getRunning();
            if (res.response.next)
            {
                if (res.needNextProcess)
                {
                    check = ProgramConfig.getProfile(FunctionID.CashOut_CheckUserIDPasswordCurrentCashier);
                    //frmUserAuthorize auth = new frmUserAuthorize("CashOut", check.diffUserStatus);
                    //auth.function = FunctionID.CashOut_CheckUserIDPasswordCurrentCashier;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    return false;
                    //}

                    if (!Utility.CheckAuthPass(this, check, "CashOut"))
                    {
                        return false;
                    }
                }
            }
            else
            {
                dialog = new frmNotify(res);
                dialog.ShowDialog(this);
                return false;
            }
            return true;
        }

        private void btnMenu2_2_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                Profile openDay = ProgramConfig.getProfile(FunctionID.Return_CheckOpenDayofTillStatus);
                if (openDay.policy == PolicyStatus.Work)
                {
                    StoreResult checkOpenDay = returnProcess.checkOpenDay();
                    if (checkOpenDay.response.next)
                    {
                        if (checkOpenDay.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(checkOpenDay);
                            dialog.ShowDialog(this);
                        }
                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(checkOpenDay);
                        dialog.ShowDialog(this);
                        return;
                    }
                }
                //Program.control.ShowForm("frmReturnFromInvoice");
                Program.control.ShowForm("frmSubMenuReturn");
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkException(net);
                
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void btnMenu2_3_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                Profile openDay = ProgramConfig.getProfile(FunctionID.Void_CheckOpenDayofTillStatus);
                if (openDay.policy == PolicyStatus.Work)
                {
                    StoreResult checkOpenDay = voidProcess.checkOpenDay();
                    if (!checkOpenDay.response.next)
                    {
                        frmNotify dialog = new frmNotify(checkOpenDay);
                        dialog.ShowDialog(this);
                        return;
                    }
                    else
                    {
                        if (checkOpenDay.response == ResponseCode.Information)
                        {
                            frmNotify dialog = new frmNotify(checkOpenDay);
                            dialog.ShowDialog(this);
                        }
                    }
                }
                Program.control.ShowForm("frmVoid");
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkException(net);

            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                updateMainMenu();
                updateCashierMessage();
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        private void CatchNetWorkException(NetworkConnectionException net)
        {
            frmLoading.closeLoading();
            if (Program.control.RetryConnection(net.errorType))
            {
                Utility.CheckRunningNumber();
                panelMenu.BringToFront();
                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                lbTextHeaderMain.BringToFront();
            } 
        }

        private void btnTool_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                //Program.control.ShowForm("frmSubMenuTools");
                if (checkNetwork())
                {
                    Program.control.ShowForm("frmSubMenuTools");
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private bool checkNetwork()
        {
            StoreResult result = null;

            Profile check = ProgramConfig.getProfile(FunctionID.CashIn_CheckOpenDayofTillStatus);
            if (check.policy == PolicyStatus.Work)
            {
                result = cashinprocess.checkOpenDay();
                if (!result.response.next)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    return false;
                }
            }

            return true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            try
            {
                Profile openDay = ProgramConfig.getProfile(FunctionID.Report_CheckOpenDayofTillStatus);
                if (openDay.policy == PolicyStatus.Work)
                {
                    StoreResult checkOpenDay = reportProcess.checkOpenDay();
                    if (!checkOpenDay.response.next)
                    {
                        dialog = new frmNotify(checkOpenDay);
                        dialog.ShowDialog(this);
                        return;
                    }
                    else
                    {
                        if (checkOpenDay.response == ResponseCode.Information)
                        {
                            dialog = new frmNotify(checkOpenDay);
                            dialog.ShowDialog(this);
                        }
                    }
                }

                Profile check = ProgramConfig.getProfile(FunctionID.Report_SelectReportMenu);
                if (check.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("Report", check.diffUserStatus);
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    frmLoading.closeLoading();
                    //    return;
                    //}

                    //check.functionId = FunctionID.NoFunctionID;
                    if (!Utility.CheckAuthPass(this, check, "Report"))
                    {
                        frmLoading.closeLoading();
                        return;
                    }
                }

                Program.control.ShowForm("frmSubMenuReport");
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void frmMainMenu_Shown(object sender, EventArgs e)
        {
            pnMainMenu.BringToFront();
            panelMenu.BringToFront();
            ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
            picLogo.Image = Utility.GetLogoImage();
        }

        private void frmMainMenu_Activated(object sender, EventArgs e)
        {
            if (ProgramConfig.IsStandAloneMode)
            {
                ucFooter1.IsStandAlone = true;
            }
            else
            {
                ucFooter1.IsStandAlone = false;
            }

            if (this == Form.ActiveForm && currentMenuLanguage != ProgramConfig.language)
            {
                ucFooter1.lbFunction.Text = FunctionID.Login_DisplayMainMenu.formatValue;
                frmLoading.showLoading();
                updateMainMenu();
                updateCashierMessage();
            }
        }
    }
}
