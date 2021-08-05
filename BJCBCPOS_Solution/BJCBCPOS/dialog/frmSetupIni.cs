using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class frmSetupIni : Form
    {
       
        private Point defaultConfigPanelLocation = new Point(200, 286);
        private Point newConfigPanelLocation = new Point(200, 50);
        private bool _hasConfigINI;
        private bool _hasRunningINI;

        private string storeCode;
        private string tillNo;
        private string ipServer;
        private string dbServer;
        private string ipServerBk;
        private string dbServerBk;
        private string ipServerTrain;
        private string dbServerTrain;
        private string printerName;
        private string comPort;

        public frmSetupIni(bool hasConfigINI, bool hasRunningINI)
        {
            InitializeComponent();
            _hasConfigINI = hasConfigINI;
            _hasRunningINI = hasRunningINI;
        }

        private void frmSetupIni_Load(object sender, EventArgs e)
        {
            ProgramConfig.language = new Language(0);

            if (!_hasConfigINI)
            {
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {

                if (_hasConfigINI || INIConfig.createConfigIni(storeCode, tillNo, ipServer, dbServer, ipServerBk, dbServerBk, ipServerTrain, dbServerTrain, printerName, comPort))
                {
                    ProgramConfig.config = new INIConfig(FixedData.config_name);
                    StartProcess process = new StartProcess();
                    process.getDatabaseLogin();
                    //TO DO
                    //process.getRunningINI
                    if (INIConfig.createRunningIni(storeCode, tillNo))
                    {
                        //string responseMessage = ProgramConfig.message.get("frmSetupIni", "BuildINIIncomplete").message;
                        //string helpMessage = ProgramConfig.message.get("frmSetupIni", "BuildINIIncomplete").help;
                        //frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                        //frmNotify dialog = new frmNotify(ResponseCode.Error, "ระบบผิดพลาด ไม่สามารถสร้าง .ini ได้");
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                    }
                }
                else
                {
                    frmNotify dialog = new frmNotify(ResponseCode.Error, "Cannot create file .ini");
                    dialog.ShowDialog(this);
                }
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            //string responseMessage = ProgramConfig.message.get("frmSetupIni", "CancelSetting").message;
            //string helpMessage = ProgramConfig.message.get("frmSetupIni", "CancelSetting").help;
            //frmNotify dialog = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);
            //frmNotify dialog = new frmNotify(ResponseCode.Warning, "ต้องการยกเลิกตั้งค่าก่อนใช้งานใช่หรือไม่");

            frmNotify dialog = new frmNotify(ResponseCode.Warning, "Do you want to cancel the configuration?");
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                string processName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
                Process[] list = Process.GetProcessesByName(processName);
                foreach (Process process in list)
                {
                    process.Kill();
                }
                this.Dispose();
            }
        }

        private bool valid()
        {
            frmNotify dialog;
            storeCode = uctwStoreCode.Text;
            if (string.IsNullOrEmpty(storeCode) || string.IsNullOrWhiteSpace(storeCode))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyStoreCode").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyStoreCode").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify Store Code");
                dialog.ShowDialog();
                uctwStoreCode.Focus();
                return false;
            }
            tillNo = uctwTillNo.Text;
            if (string.IsNullOrEmpty(tillNo) || string.IsNullOrWhiteSpace(tillNo))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyTillNo").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyTillNo").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify Till No.");
                dialog.ShowDialog();
                uctwTillNo.Focus();
                return false;
            }
            ipServer = uctwIPServer.Text;
            if (string.IsNullOrEmpty(ipServer) || string.IsNullOrWhiteSpace(ipServer))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyIPServer").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyIPServer").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify IP Server");
                dialog.ShowDialog();
                uctwIPServer.Focus();
                return false;
            }
            dbServer = uctwDBServer.Text;
            if (string.IsNullOrEmpty(dbServer) || string.IsNullOrWhiteSpace(dbServer))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyDatabaseServer").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyDatabaseServer").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify Database Server");
                dialog.ShowDialog();
                uctwDBServer.Focus();
                return false;
            }
            ipServerBk = uctwIPServerBackup.Text;
            if (string.IsNullOrEmpty(ipServerBk) || string.IsNullOrWhiteSpace(ipServerBk))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyIPServerBackup").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyIPServerBackup").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify IP Server Backup");
                dialog.ShowDialog();
                uctwIPServerBackup.Focus();
                return false;
            }
            dbServerBk = uctwDBServerBackup.Text;
            if (string.IsNullOrEmpty(dbServerBk) || string.IsNullOrWhiteSpace(dbServerBk))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyDatabaseServerBackup").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyDatabaseServerBackup").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify Database Server Backup");
                dialog.ShowDialog();
                uctwDBServerBackup.Focus();
                return false;
            }
            ipServerTrain = uctwIPServerTrainning.Text;
            if (string.IsNullOrEmpty(ipServerTrain) || string.IsNullOrWhiteSpace(ipServerTrain))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyIPServerTrainning").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyIPServerTrainning").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify IP Server Trainning");
                dialog.ShowDialog();
                uctwIPServerTrainning.Focus();
                return false;
            }
            dbServerTrain = uctwDBServerTrainning.Text;
            if (string.IsNullOrEmpty(dbServerTrain) || string.IsNullOrWhiteSpace(dbServerTrain))
            {
                //string responseMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyDatabaseServerTrainning").message;
                //string helpMessage = ProgramConfig.message.get("frmSetupIni", "SpecifyDatabaseServerTrainning").help;
                //dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                dialog = new frmNotify(ResponseCode.Error, "Please specify Database Server Trainning");
                dialog.ShowDialog();
                uctwDBServerTrainning.Focus();
                return false;
            }

            return true;
        }

        private void uctw_Enter(object sender, EventArgs e)
        {
            pnfrmConfig.Location = newConfigPanelLocation;
            splitContainer1.SplitterDistance = 500;

            splitContainer1.Panel2Collapsed = false;
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.currentInput = (UCTextBoxWithIcon)sender;
        }

        private void uctw_Leave(object sender, EventArgs e)
        {
            pnfrmConfig.Location = defaultConfigPanelLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeyboard1.Visible = false;
        }

        private void uctw_TextBoxKeydown(object sender, EventArgs e)
        {
            uctw_Leave(sender, e);
            this.Update();
            UCTextBoxWithIcon current = (UCTextBoxWithIcon)sender;
            if (current == uctwStoreCode)
            {
                uctwTillNo.Focus();
            }
            else if (current == uctwTillNo)
            {
                uctwIPServer.Focus();
            }
            else if (current == uctwIPServer)
            {
                uctwDBServer.Focus();
            }
            else if (current == uctwDBServer)
            {
                uctwIPServerBackup.Focus();
            }
            else if (current == uctwIPServerBackup)
            {
                uctwDBServerBackup.Focus();
            }
            else if (current == uctwDBServerBackup)
            {
                uctwIPServerTrainning.Focus();
            }
            else if (current == uctwIPServerTrainning)
            {
                uctwDBServerTrainning.Focus();
            }
            else if (current == uctwDBServerTrainning)
            {
                uctwPrinterName.Focus();
            }
            else if (current == uctwPrinterName)
            {
                uctwCOMPort.Focus();
            }
            else if (current == uctwCOMPort)
            {
                btnSave.PerformClick();
            }
        }
    }
}
