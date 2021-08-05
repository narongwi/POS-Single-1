using System;
//using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using BJCBCPOS_Model;
using System.Linq;

namespace BJCBCPOS
{
    static class Program
    {
        public static AppControl control = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void MainBackup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
                string processName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
                Process[] list = Process.GetProcessesByName(processName);
                if (list != null && list.Length > 1)
                {
                    foreach (Process process in list)
                    {
                        process.Kill();
                    }
                }
                control = new AppControl();
                Application.Run(control);
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("catch NetworkConnectionException in Program.Main");
                if (control != null)
                {
                    control.RetryConnection(net.errorType);
                }
            }
            catch (Exception ex)
            {
                LogResponse log = AppLog.writeLog(ex);
                frmNotify dialog = new frmNotify(log.respone, log.message, log.helpMessage);
                dialog.Show();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            frmLoading.closeLoading();
            AppLog.writeLog("catch exception in CurrentDomain_UnhandledException");
            if (e.ExceptionObject.GetType() == typeof(NetworkConnectionException))
            {
                if (control != null)
                {
                    //// recheck drawer is opened if not close notify form
                    //frmNotify notify = Application.OpenForms.OfType<frmNotify>().FirstOrDefault();
                    //if (notify != null)
                    //{
                    //    AppLog.writeLog("close form notify.");
                    //    notify.connectionLostClose();
                    //}
                    if (!(ProgramConfig.formGlobal is frmSale) && !(ProgramConfig.formGlobal is frmPayment) && !(ProgramConfig.formGlobal is frmConfirmPayment))
                    {
                        if (control.RetryConnection(((NetworkConnectionException)e.ExceptionObject).errorType))
                        {
                            Utility.CheckRunningNumber();
                            if (!(ProgramConfig.formGlobal is frmDeleteReason) && !(ProgramConfig.formGlobal is frmDeleteItemReason) && 
                                !(ProgramConfig.formGlobal is frmEditItemReason))
                            {                               
                                control.CloseForm(ProgramConfig.formGlobal.Name);
                                control.ShowForm(ProgramConfig.formGlobal.Name);
                            }
                        }
                    }
                    //else
                    //{
                    //    control.RetryConnection(((NetworkConnectionException)e.ExceptionObject).errorType);
                    //}
                }
            }
            else
            {
                LogResponse log = AppLog.writeLog((Exception)e.ExceptionObject);
                frmNotify dialog = new frmNotify(log.respone, log.message, log.helpMessage);
                dialog.ShowDialog();
                if (e.IsTerminating)
                {
                    string processName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
                    Process[] list = Process.GetProcessesByName(processName);
                    foreach (Process process in list)
                    {
                        process.Kill();
                    }
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            frmLoading.closeLoading();
            AppLog.writeLog("catch exception in Application_ThreadException");
            if (e.Exception.GetType() == typeof(NetworkConnectionException))
            {
                if (control != null)
                {
                    //// recheck drawer is opened if not close notify form
                    //frmNotify notify = Application.OpenForms.OfType<frmNotify>().FirstOrDefault();
                    //if (notify != null)
                    //{
                    //    AppLog.writeLog("close form notify.");
                    //    notify.connectionLostClose();
                    //}

                    if (!(ProgramConfig.formGlobal is frmSale) && !(ProgramConfig.formGlobal is frmPayment) && !(ProgramConfig.formGlobal is frmConfirmPayment))
                    {
                        if (control.RetryConnection(((NetworkConnectionException)e.Exception).errorType))
                        {
                            Utility.CheckRunningNumber();
                            if (!(ProgramConfig.formGlobal is frmDeleteReason) && !(ProgramConfig.formGlobal is frmDeleteItemReason) && 
                                !(ProgramConfig.formGlobal is frmEditItemReason))
                            {
                                control.CloseForm(ProgramConfig.formGlobal.Name);
                                control.ShowForm(ProgramConfig.formGlobal.Name);  
                            }           
                        }      
                    }
                    //else
                    //{
                    //    control.RetryConnection(((NetworkConnectionException)e.Exception).errorType);
                    //}
                }
            }
            else
            {
                LogResponse log = AppLog.writeLog(e.Exception);
                frmNotify dialog = new frmNotify(log.respone, log.message, log.helpMessage);
                dialog.ShowDialog();
                string processName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
                Process[] list = Process.GetProcessesByName(processName);
                foreach (Process process in list)
                {
                    process.Kill();
                }
            }
        }
    }
}
