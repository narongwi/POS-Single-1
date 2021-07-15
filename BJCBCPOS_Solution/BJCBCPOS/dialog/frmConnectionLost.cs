using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using BJCBCPOS_Process;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmConnectionLost : Form
    {
        private BackgroundWorker worker = null;
        private bool IsPaint = false;
        private NetworkErrorType errorType = NetworkErrorType.NotSpecify;
        private bool _isStandAlone = false;

        public frmConnectionLost(NetworkErrorType type)
        {
            AppLog.writeLog("call frmConnectionLost.");
            InitializeComponent();
            errorType = type;
            try
            {
                //AppMessage.fillForm(ProgramConfig.language, this.Name, this);
                AppMessage.fillForm(ProgramConfig.language, this);
            }
            catch { }
        }

        public frmConnectionLost(NetworkErrorType type, bool isStandAlone)
        {
            AppLog.writeLog("call frmConnectionLost.");
            InitializeComponent();
            _isStandAlone = isStandAlone;
            errorType = type;
            try
            {
                //AppMessage.fillForm(ProgramConfig.language, this.Name, this);
                AppMessage.fillForm(ProgramConfig.language, this);
            }
            catch { }
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
        
        private void btnSuccess_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnEnterStanAlone_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Abort;
            //this.Close();

            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
            //this.DialogResult = DialogResult.Retry;
            //this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSuccessStandAlone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void frmConnectionLost_Shown(object sender, EventArgs e)
        {
            AppLog.writeLog("call frmConnectionLost_Shown.");
            pnLoading.BringToFront();
            pnLoading.Visible = true;
            pnConnectionLostStandAlone.Visible = false;
            pnConnectionLostNotStandAlone.Visible = false;
            pnStandAlone.Visible = false;
            pnReconnect.Visible = false;
            pnCommandTimeout.Visible = false;
            this.Update();
            frmLoading.closeLoading();

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        private StartProcess retryInitialProcess(int retryTime)
        {
            try
            {
                return new StartProcess();
            }
            catch (NetworkConnectionException)
            {
                retryTime++;
                if (retryTime > ProgramConfig.connectionRetry)
                //if (retryTime > 1)
                {
                    return null;
                }
                Thread.Sleep(1000);
                return retryInitialProcess(retryTime);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StartProcess process = retryInitialProcess(1);
                if (process != null)
                {
                    if (_isStandAlone)
                    {
                        BaseProcess.changeConnectionString(ProgramConfig.connectionStringLocal);
                    }
                    e.Result = process.retryConnection(1);
                }
                else
                {
                    e.Result = false;
                }
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                throw;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnLoading.Visible = false;

            if (_isStandAlone && (bool)e.Result && e.Error == null && !e.Cancelled)
            {
                ProgramConfig.IsStandAloneMode = true;
                Utility.SetStatusModeFooter();
                pnStandAlone.BringToFront();
                pnStandAlone.Visible = true;
                btnSuccessStandAlone.Focus();                
            }
            else
            {
                if (e.Error == null && !e.Cancelled && (bool)e.Result)
                {
                    if (errorType == NetworkErrorType.ConnectionTimeout)
                    {
                        pnReconnect.BringToFront();
                        pnReconnect.Visible = true;
                        btnSuccess.Focus();
                    }
                    else if (errorType == NetworkErrorType.CommandTimeout)
                    {
                        pnCommandTimeout.BringToFront();
                        pnCommandTimeout.Visible = true;
                        btnContinue.Focus();
                    }
                    else
                    {
                        pnReconnect.BringToFront();
                        pnReconnect.Visible = true;
                        btnSuccess.Focus();
                    }
                }
                else
                {
                    //เคสกรณีที่เข้า mode stand alone ต้องแต่เข้าโปรแกรม (connect db server ไม่ได้)
                    //(ProgramConfig.saleModePopUp == 0 && ProgramConfig.config.getValue("connection", "standalone").Trim() == "Y") 
                    
                    //เคสกรณีที่ connection lost แล้วขึ้น popup เข้าสู่โหมด stand alone
                    //ProgramConfig.saleModePopUp == SaleMode.Standalone

                    //เคสกรณีที่ db local ไม่สามารถ connect ได้่ จะต้องไปตก case else เพื่อที่จะ re try connect
                    //&& _isStandAlone

                    if (((ProgramConfig.saleModePopUp == SaleMode.Standalone) && !ProgramConfig.IsStandAloneMode) || (ProgramConfig.saleModePopUp == 0 && ProgramConfig.config.getValue("connection", "standalone").Trim() == "Y"))
                    {
                        pnConnectionLostStandAlone.BringToFront();
                        pnConnectionLostStandAlone.Visible = true;
                        btnExit.Focus();
                    }
                    else
                    {
                        pnConnectionLostNotStandAlone.BringToFront();
                        pnConnectionLostNotStandAlone.Visible = true;
                        btnExit.Focus();
                    }
                }
            }
            this.Update();
        }

        private void btnTryToConnect_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }



    }
}
