using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Runtime;
using System.Data;

namespace BJCBCPOS {
    public class AppControl : ApplicationContext {
        private Screen main = null;
        private Timer timer;
        //private int retry = 3;

        public AppControl() {
            SubAppControl();
        }

        private void SubAppControl() {
            timer = new Timer();
            timer.Tick += new EventHandler(Timer_Tick);

            //int retry = 3;
            //while (retry > 0)
            //{
            try {
                // find main screen
                List<Screen> possible = new List<Screen>();
                foreach(Screen scr in Screen.AllScreens) {
                    if(scr.WorkingArea.Size.Width >= 1024 && scr.WorkingArea.Size.Height >= 768) {
                        possible.Add(scr);
                    }
                }

                if(possible.Count > 0) {
                    if(possible.Count == 1) {
                        main = possible[0];
                    } else {
                        foreach(Screen scr in possible) {
                            if(scr.Primary) {
                                main = scr;
                                break;
                            }
                        }

                        if(main == null) {
                            main = possible[0];
                        }
                    }
                } else {
                    main = Screen.AllScreens[0];
                }

                bool hasConfigINI = File.Exists(FixedData.config_name);
                bool hasRunningINI = File.Exists(FixedData.running_name);

                if(!hasConfigINI || !hasRunningINI) {
                    frmSetupIni ini = new frmSetupIni(hasConfigINI,hasRunningINI);
                    if(ini.ShowDialog() != DialogResult.OK) {
                        timer.Interval = 1000;
                        timer.Enabled = true;
                        ExitThread();
                        return;
                    }
                }

                frmLoading.showLoading();
                ClearLog();
                frmLoading.closeLoading();

                // get current app name and version
                ProgramConfig.appName = Assembly.GetExecutingAssembly().GetName().Name;
                ProgramConfig.version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                //ProgramConfig.version = "1.00.00";
                // get current computer name and ip
                ProgramConfig.computerName = Environment.MachineName;
                //ProgramConfig.ipAddress = System.Net.Dns.GetHostAddresses(ProgramConfig.computerName)[1].ToString();
                try {
                    using(Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,0)) {
                        socket.Connect("8.8.8.8",65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        ProgramConfig.ipAddress = endPoint.Address.ToString();
                    }
                } catch(Exception) {
                    ProgramConfig.ipAddress = "";
                }

                // read .ini config
                if(ProgramConfig.config == null) {
                    ProgramConfig.config = new INIConfig(FixedData.config_name);
                }
                ProgramConfig.running = new INIConfig(FixedData.running_name);

                // database connect
                StartProcess login = new StartProcess();

                // start with get db user password from db user LOWAPP
                frmLoading.showLoading();
                login.getDatabaseLogin();
                frmLoading.closeLoading();

                StoreResult result = login.SetLanguage();
                if(!result.response.next) {
                    //frmNotify dialog = new frmNotify(result.response, result.responseMessage, "pos_GetDisplayLanguage Fail!!");
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog();
                    frmLoading.closeLoading();
                    ExitThread();
                    return;
                }

                // read language file
                if(ProgramConfig.language.Name != null) {
                    AppMessage.readFromFile(ProgramConfig.language);
                } else {
                    frmNotify dialog = new frmNotify(ResponseCode.Error,"Default Language not 'Active' in config database.","");
                    dialog.ShowDialog();
                    frmLoading.closeLoading();
                    ExitThread();
                    return;
                }

                // start application process
                frmLoading.showLoading();
                result = login.getPOSConfigFromDB();
                frmLoading.closeLoading();
                if(result.response.next) {
                    //Check Local POS 
                    frmLoading.showLoading();
                    if(!login.CheckLocalPOS()) {
                        frmNotify dialog = new frmNotify(ResponseCode.Error,ProgramConfig.message.get("CannotAccessLOCALPOS","CannotAccessLOCALPOS").message,ProgramConfig.message.get("CannotAccessLOCALPOS","CannotAccessLOCALPOS").help);
                        frmLoading.closeLoading();
                        dialog.ShowDialog();
                        ExitThread();
                        ExitProgram();
                        return;
                    }
                    frmLoading.closeLoading();

                    if(result.response == ResponseCode.Information) {
                        frmNotify dialog = new frmNotify(result);
                        dialog.ShowDialog();
                    }
                    //show login form
                    //ShowForm("frmMonitorCustomer");
                    //ShowForm("frmMonitorCustomerFooter");
                    //ShowForm("frmMonitor2Detail");
                    //ShowForm("frmVDO");
                    //ShowForm("frmLogin");

                    // test form
                    //ShowForm("Services.Forms.frmBigService");
                    ShowForm("Services.Forms.frmBillPayment");

                    //ShowForm("OtherServices.frmBillPayments");
                } else {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog();
                    frmLoading.closeLoading();
                    ExitThread();
                }

                //// skip to test page
                //ShowForm("frmLogin");
                //ShowForm("TestHardware");
            } catch(NetworkConnectionException net) {
                //if (retry > 0)
                //{
                if(ProgramConfig.config.getValue("connection","standalone").Trim() == "Y") {
                    if(RetryConnection(net.errorType,true)) {
                        SubAppControl();
                    }
                } else if(ProgramConfig.config.getValue("connection","standalone").Trim() == "N") {
                    RetryConnection(net.errorType);
                }

                //    retry--;
                //}
                //else
                //{
                //    ExitProgram();
                //    ExitThread();
                //}
            }

            timer.Interval = 5000;
            timer.Enabled = true;
        }

        private void ClearLog() {
            try {
                DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath) + @"\log");
                foreach(FileInfo file in directory.GetFiles("*.log")) {
                    if((DateTime.Now - file.CreationTime).Days > 30 || (DateTime.Now - file.LastWriteTime).Days > 30) {
                        file.Delete();
                    }
                }

                DirectoryInfo directory2 = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath));
                foreach(FileInfo file in directory2.GetFiles()) {
                    if(file.Extension == ".vlg" || file.Extension == ".ulg") {
                        if((DateTime.Now - file.CreationTime).Days > 30 || (DateTime.Now - file.LastWriteTime).Days > 30) {
                            file.Delete();
                        }
                    }

                }
            } catch(Exception ex) {
                AppLog.writeLog("ClearLog() : " + ex.Message);
            }
        }

        private void OnFormClosed(object sender,EventArgs e) {
            try {
                Form current = (Form)sender;
                foreach(Control item in current.Controls) {
                    if(item is UserControl) {
                        item.Dispose();
                    }
                }
            } catch { }

            if(Application.OpenForms.Count == 0) {
                ExitThread();
            }
        }

        private void Timer_Tick(object sender,EventArgs e) {
            if(Application.OpenForms.Count == 0) {
                ExitThread();
            }
        }

        private void OnActivated(object sender,EventArgs e) {
            // check form if not contains keyboard reset layout to english

            //bool hasKeyboard = false;
            //List<Control> checkList = new List<Control>();
            //checkList.Add((Form)sender);
            //while (checkList.Count > 0)
            //{
            //    Control check = checkList.ElementAt(0);
            //    foreach (Control item in check.Controls)
            //    {
            //        if (item is UCKeyboard)
            //        {
            //            hasKeyboard = true;
            //            break;
            //        }
            //        else
            //        {
            //            checkList.Add(item);
            //        }
            //    }
            //    if (hasKeyboard) break;
            //    checkList.Remove(check);
            //}

            AppLog.writeLog(((Form)sender).Name + " is Activate.");

            if(sender is frmSale || sender is frmFavoriteSale || sender is frmPayment || sender is frmConfirmPayment || sender is frmVoid || sender is frmReturnFromInvoice
                || sender is frmVoidSuccess || sender is frmReturnSuccess || sender is frmCheckProduct || sender is frmMainMenu || sender is frmInvoiceReport) {
                KeyboardApi keyboard = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
            }

            //if (!hasKeyboard)
            //{
            //    KeyboardApi keyboard = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
            //}

            //if (!(sender is frmLogin) || !(sender is frmUserAuthorize) || !(sender is frmSearchMember) || !(sender is frmMoneyBagInput) || !(sender is frmChangePassword)
            //    || !(sender is frmSetupIni))
            //{
            //    KeyboardApi keyboard = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
            //}
        }

        public void ShowForm(string formName) {
            ShowForm(Application.OpenForms[formName],formName);
        }

        public void ShowForm(string formName,Form OwnerForm) {
            Form form = BaseShowForm(Application.OpenForms[formName],formName);
            if(form != null) {
                form.Show(OwnerForm);
                form.BringToFront();
            }
            form = null;
        }

        public void ShowForm(Form formIn,string formName) {
            Form form = BaseShowForm(formIn,formName);
            if(form != null) {
                form.Show();
                form.BringToFront();
            }
            form = null;
        }

        public void ShowForm(Form formIn,string formName,Form OwnerForm) {
            Form form = BaseShowForm(formIn,formName);
            if(form != null) {
                form.Show();
                form.BringToFront();
            }
            form = null;
        }

        public Form BaseShowForm(Form form,string formName) {
            //Form form = Application.OpenForms[formName];
            if(form == null) {
                try {
                    form = (Form)Activator.CreateInstance(Type.GetType("BJCBCPOS." + formName));
                } catch(TargetInvocationException ex) {
                    if(ex.InnerException != null && ex.InnerException.GetType() == typeof(NetworkConnectionException)) {
                        throw ex.InnerException;
                    } else {
                        throw ex;
                    }
                }
                // set form location to main screen
                if(main != null && (!form.Name.Equals("frmMonitorCustomer") && !form.Name.Equals("frmMonitorCustomerFooter")
                    && !form.Name.Equals("frmMonitor2Detail") && !form.Name.Equals("frmCustRedeem") && !form.Name.Equals("frmVDO") && !form.Name.Equals("frmRedeem"))) {
                    Point screen_location = main.WorkingArea.Location;
                    Point frm_location = form.Location;
                    form.Location = new Point(screen_location.X + frm_location.X,screen_location.Y + frm_location.Y);
                    ProgramConfig.formGlobal = form;
                }
            }

            if(main != null && (!form.Name.Equals("frmMonitorCustomer") && !form.Name.Equals("frmMonitorCustomerFooter")
                && !form.Name.Equals("frmMonitor2Detail") && !form.Name.Equals("frmCustRedeem") && !form.Name.Equals("frmVDO") && !form.Name.Equals("frmRedeem"))) {
                ProgramConfig.formGlobal = form;
            }

            AppMessage.fillForm(ProgramConfig.language,form);
            form.FormClosed += new FormClosedEventHandler(OnFormClosed);
            form.Activated += new EventHandler(OnActivated);

            //if (form is frmSale || form is frmFavoriteSale || form is frmPayment || form is frmConfirmPayment || form is frmVoid || form is frmReturnFromInvoice
            //    || form is frmVoidSuccess || form is frmReturnSuccess || form is frmCheckProduct || form is frmMainMenu || form is frmInvoiceReport)
            //{
            //    //recheck redeem page 
            //    KeyboardApi keyboard = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
            //}

            if(formName == "frmMainMenu") {
                ((frmMainMenu)form).needUpdate = true;
            } else if(formName == "frmSale") {
                AppLog.writeLog("Call afterNotify False");
                ((frmSale)form).afterNotify = false;
            }

            return form;
        }

        #region BackUp Old Code
        //public void ShowForm(string formName)
        //{
        //    Form form = Application.OpenForms[formName];

        //    if (form == null)
        //    {
        //        try
        //        {
        //            form = (Form)Activator.CreateInstance(Type.GetType("BJCBCPOS." + formName));
        //        }
        //        catch (TargetInvocationException ex)
        //        {
        //            if (ex.InnerException != null && ex.InnerException.GetType() == typeof(NetworkConnectionException))
        //            {
        //                throw ex.InnerException;
        //            }
        //            else
        //            {
        //                throw ex;
        //            }
        //        }

        //        // set form location to main screen
        //        if (main != null && (!form.Name.Equals("frmMonitorCustomer") && !form.Name.Equals("frmMonitorCustomerFooter") && !form.Name.Equals("frmMonitor2Detail")) && !form.Name.Equals("frmCustRedeem"))
        //        {
        //            Point screen_location = main.WorkingArea.Location;
        //            Point frm_location = form.Location;
        //            form.Location = new Point(screen_location.X + frm_location.X, screen_location.Y + frm_location.Y);
        //        }
        //    }

        //    AppMessage.fillForm(ProgramConfig.language, form);
        //    form.FormClosed += new FormClosedEventHandler(OnFormClosed);
        //    form.Activated += new EventHandler(OnActivated);

        //    // check form if not contains keyboard reset layout to english
        //    //bool hasKeyboard = false;
        //    //List<Control> checkList = new List<Control>();
        //    //checkList.Add(form);
        //    //while (checkList.Count > 0)
        //    //{
        //    //    Control check = checkList.ElementAt(0);
        //    //    foreach (Control item in check.Controls)
        //    //    {
        //    //        if (item is UCKeyboard)
        //    //        {
        //    //            hasKeyboard = true;
        //    //            break;
        //    //        }
        //    //        else
        //    //        {
        //    //            checkList.Add(item);
        //    //        }
        //    //    }
        //    //    if (hasKeyboard) break;
        //    //    checkList.Remove(check);
        //    //}

        //    //if (!hasKeyboard)
        //    //{
        //    //    KeyboardApi keyboard = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
        //    //}

        //    if (form is frmSale || form is frmFavoriteSale || form is frmPayment || form is frmConfirmPayment || form is frmVoid || form is frmReturnFromInvoice
        //        || form is frmVoidSuccess || form is frmReturnSuccess || form is frmCheckProduct || form is frmMainMenu || form is frmInvoiceReport)
        //    {
        //        KeyboardApi keyboard = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
        //    }

        //    if (formName == "frmMainMenu")
        //    {
        //        ((frmMainMenu)form).needUpdate = true;
        //    }
        //    else if (formName == "frmSale")
        //    {
        //        ((frmSale)form).afterNotify = false;
        //    }

        //    form.Show();
        //    form.BringToFront();
        //}
        #endregion

        public void CloseForm(string formName) {
            Form form = Application.OpenForms[formName];

            if(form != null) {
                form.Dispose();
            }
        }

        public void ExitProgram() {
            FormCollection openedForm = Application.OpenForms;
            while(openedForm.Count > 0) {
                foreach(Form item in openedForm.Cast<Form>().ToList()) {
                    try {
                        item.Dispose();
                    } catch(Exception ex) {
                        AppLog.writeLog(ex);
                    }
                }
                openedForm = Application.OpenForms;
            }

            if(Application.OpenForms.Count == 0) {
                ExitThread();
            }

            string processName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            Process[] list = Process.GetProcessesByName(processName);
            if(list != null && list.Length > 1) {
                foreach(Process process in list) {
                    process.Kill();
                }
            }
        }

        //public bool RetryConnection(NetworkErrorType type)
        //{
        //    AppLog.writeLog("call AppControl.RetryConnection.");
        //    frmConnectionLost frm = new frmConnectionLost(type);
        //    DialogResult res = frm.ShowDialog();
        //    if (res == DialogResult.Abort)
        //    {
        //        ExitProgram();
        //        ExitThread();
        //        return false;
        //    }
        //    else if (res == DialogResult.Retry)
        //    {
        //        return RetryConnection(type, true);
        //    }
        //    return true;
        //}

        public bool RetryConnection(NetworkErrorType type,bool IsStandAlone = false) {
            AppLog.writeLog("call AppControl.RetryConnection.");
            frmConnectionLost frm = new frmConnectionLost(type,IsStandAlone);
            DialogResult res = frm.ShowDialog();
            if(res == DialogResult.Abort) {
                ExitProgram();
                ExitThread();
                return false;
            } else if(res == DialogResult.Retry) {
                return RetryConnection(type,true);
            } else if(res == DialogResult.Ignore) {
                return RetryConnection(type);
            } else if(res == DialogResult.OK) {
                return false;
            }

            return true;
        }


    }
}
