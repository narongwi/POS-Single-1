using System;
using System.Windows.Forms;
using System.Threading;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public class PageUtil
    {
        public static void showForm(string frmName)
        {
            Program.control.ShowForm(frmName);
        }

        public static void closeForm(string frmName)
        {
            Program.control.CloseForm(frmName);
        }

        public static void executeFormMethod(string frmName, string methodName, object[] param = null)
        {
            Program.control.ExecuteFormMethod(frmName, methodName, param);
        }

        public static void exitProgram()
        {
            Program.control.ExitProgram();
        }

        public static void processWithLoadingScreen(MethodInvoker process)
        {
            new frmLoading(process).Show();
        }

        public static void processWithLoadingScreen(MethodInvoker process, Control owner)
        {
            new frmLoading(process, owner).Show();
        }

        public static void showAlertMessage(Form screen, ResponseCode responseCode, string messageKey)
        {
            AlertMessage message = ProgramConfig.message.get(screen.Name, messageKey);

            frmNotify dialog = new frmNotify(responseCode, message.message, message.help);
            dialog.ShowDialog(screen);
        }

        public static AlertMessage getAlertMessage(string screen, string messageKey)
        {
            return ProgramConfig.message.get(screen, messageKey);
        }
    }
}
