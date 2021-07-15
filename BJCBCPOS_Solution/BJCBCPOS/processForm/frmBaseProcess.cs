using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Model;
using System.Windows.Forms;

namespace BJCBCPOS
{
    public class frmBaseProcess
    {
        private Form _currentForm = null;

        public Form CurrentForm
        {
            get
            {
                return this._currentForm;
            }
            set
            {
                _currentForm = value;
            }
        }

        public frmBaseProcess()
        {
            //frmCurrent = fCurrnect;
        }

        public frmBaseProcess(Form currnectForm)
        {
            _currentForm = currnectForm;
        }

        public StoreResult ReturnResult(Func<StoreResult> action)
        {
            try
            {
                frmLoading.showLoading();
                return Utility.CheckNotifyNext(action());
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at " + action.Method.Name);
                if (this.CheckException(net))
                {
                    return ReturnResult(() => action());
                }

                if (_currentForm != null)
                {
                    _currentForm.Dispose();
                    _currentForm = null;
                }

                return new StoreResult(AppLog.writeLog(net));
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        public virtual bool CheckException(NetworkConnectionException net)
        {
            if (Program.control.RetryConnection(net.errorType))
            {
                if (_currentForm != null)
                {
                    _currentForm.Dispose();
                    _currentForm = null;
                }
                return false;
            }
            return true;
        }

        public ProcessResult ReturnResult(Func<ProcessResult> action)
        {
            try
            {
                frmLoading.showLoading();
                return Utility.CheckNotifyNext(action());
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at " + action.Method.Name);
                if (this.CheckException(net))
                {
                    return ReturnResult(() => action());
                    //return Utility.CheckNotifyNext(action());
                }

                if (_currentForm != null)
                {
                    _currentForm.Dispose();
                    _currentForm = null;
                }
                return new ProcessResult(AppLog.writeLog(net));
            }
            finally
            {
                frmLoading.closeLoading();
            }
        }

        //public T ReturnResult<T>(Func<T> action)
        //{
        //    try
        //    {
        //        return action();
        //    }
        //    catch (NetworkConnectionException net)
        //    {
        //        AppLog.writeLog("connection to server lost at " + action.ToString());
        //        throw;
        //    }
        //}
    }
}
