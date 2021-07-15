using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// utility class for create application error log file
    /// </summary>
    public static class AppLog
    {
        private const string fileNameFormat = "log\\BJCBCPOS_log_{0}{1}.log";
        private const string SqlfileNameFormat = "log\\SqlCommand_log_{0}{1}.log";
        private const string drawerFileNameFormat = "log\\DrawerEvent_log_{0}{1}.log";
        private static object log_lock = new object();
        private static object sql_lock = new object();
        private static object drawer_lock = new object();

        static AppLog()
        {
            if (!Directory.Exists("log"))
            {
                Directory.CreateDirectory("log");
            }
            if (!Directory.Exists("log\\Timer"))
            {
                Directory.CreateDirectory("log\\Timer");
            }
        }

        /// <summary>
        /// write application error to log file
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static LogResponse writeLog(Exception ex)
        {
            int number = 0;
            string log = string.Format(fileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "");
            while (File.Exists(log))
            {
                if (new FileInfo(log).Length >= 1048576000)
                {
                    number++;
                    log = string.Format(fileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "_" + number);
                }
                else
                {
                    break;
                }
            }
            string log_msg = DateTime.Now.ToString("HH:mm:ss.ffff") + "  " + ex.Message + Environment.NewLine;
            if (ex.StackTrace != null)
            {
                log_msg += ex.StackTrace + Environment.NewLine;
            }
            log_msg += Environment.NewLine;

            lock (log_lock)
            {
                File.AppendAllText(log, log_msg);

                if (ex.InnerException != null)
                {
                    writeInnerException(ex.InnerException, log);
                }
            }

            LogResponse res = new LogResponse();
            res.respone = ResponseCode.Error;
            res.message = ProgramConfig.message.defaultMessage.message;
            res.helpMessage = ProgramConfig.message.defaultMessage.help;
            return res;
        }

        private static void writeInnerException(Exception ex, string filename)
        {
            string log_msg = "\tInnerException : " + ex.Message + Environment.NewLine;

            if (ex.StackTrace != null)
            {
                log_msg += ex.StackTrace + Environment.NewLine;
            }
            log_msg += Environment.NewLine;

            File.AppendAllText(filename, log_msg);

            if (ex.InnerException != null)
            {
                writeInnerException(ex.InnerException, filename);
            }
        }

        public static void writeLog(string message)
        {
            int number = 0;
            string log = string.Format(fileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "");
            while (File.Exists(log))
            {
                if (new FileInfo(log).Length >= 1048576000)
                {
                    number++;
                    log = string.Format(fileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "_" + number);
                }
                else
                {
                    break;
                }
            }
            string log_msg = DateTime.Now.ToString("HH:mm:ss.ffff") + "  " + message + Environment.NewLine;

            lock (log_lock)
            {
                File.AppendAllText(log, log_msg);
            }

            //LogResponse res = new LogResponse();
            //res.respone = ResponseCode.Error;
            //res.message = ProgramConfig.message.defaultMessage.message;
            //res.helpMessage = ProgramConfig.message.defaultMessage.help;
            //return res;
        }

        public static void writeSqlCommand(string command)
        {
            int number = 0;
            string log = string.Format(SqlfileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "");
            while (File.Exists(log))
            {
                if (new FileInfo(log).Length >= 1048576000)
                {
                    number++;
                    log = string.Format(SqlfileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "_" + number);
                }
                else
                {
                    break;
                }
            }
            string log_msg = DateTime.Now.ToString("HH:mm:ss.ffff") + "  " + command + Environment.NewLine + Environment.NewLine;

            lock (sql_lock)
            {
                File.AppendAllText(log, log_msg);
            }
        }

        public static void writeDrawerEvent(string message)
        {
            int number = 0;
            string log = string.Format(drawerFileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "");
            while (File.Exists(log))
            {
                if (new FileInfo(log).Length >= 1048576000)
                {
                    number++;
                    log = string.Format(drawerFileNameFormat, DateTime.Now.Date.ToString("yyyy_MM_dd"), "_" + number);
                }
                else
                {
                    break;
                }
            }
            string log_msg = DateTime.Now.ToString("HH:mm:ss.ffff") + "  " + message + Environment.NewLine;

            lock (drawer_lock)
            {
                File.AppendAllText(log, log_msg);
            }
        }

    }

}
