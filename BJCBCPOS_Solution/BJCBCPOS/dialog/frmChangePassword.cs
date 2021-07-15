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
    public partial class frmChangePassword : Form
    {
        private delegate DialogResult InvokeDelegate(Form parent);

        private MenuProcess process;
        private Point newLocation = new Point(247, 5);
        private Point defaultLocation = new Point(247, 140);
        bool alert401;
        StoreResult res = null;


        public frmChangePassword()
        {
            InitializeComponent();
            alert401 = false;
            lbUserID.Text = ProgramConfig.userId;
            lbCashierName.Text = ProgramConfig.cashierName;
            process = new MenuProcess();
        }

        public frmChangePassword(string page)
        {
            InitializeComponent();
            alert401 = true;
            lbUserID.Text = ProgramConfig.superUserId;
            lbCashierName.Text = ProgramConfig.superUserName;
            process = new MenuProcess();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                string oldPass = uctwOldPassword.Text;
                string newPass = uctwNewPassword.Text;
                string confirmPass = uctwConfirmPassword.Text;

                if (isValid(oldPass, newPass, confirmPass))
                {
                    if (alert401 == true)
                    {
                        res = process.saveChangePassword(ProgramConfig.superUserId, newPass);
                    }
                    else
                    {
                        res = process.saveChangePassword(ProgramConfig.userId, newPass);
                    }

                    if (res.response.next)
                    {
                        string responseMessage = ProgramConfig.message.get("frmChangePassword", "ChangePasswordComplete").message;
                        string helpMessage = ProgramConfig.message.get("frmChangePassword", "ChangePasswordComplete").help;
                        frmNotify dialog = new frmNotify(ResponseCode.Success, responseMessage, helpMessage);

                        //frmNotify dialog = new frmNotify(ResponseCode.Success, "เปลี่ยนรหัสผ่านเรียบร้อยแล้ว กรุณาเข้าสู่ระบบด้วยรหัสผ่านใหม่");
                        dialog.ShowDialog(this);

                        res = process.saveChangePasswordAutoLogout();
                        if (!res.response.next)
                        {
                            dialog = new frmNotify(res);
                            dialog.ShowDialog(this);
                        }
                        this.DialogResult = DialogResult.OK;

                        if (alert401 == true)
                        {
                            this.Dispose();
                        }
                        else
                        {
                            ProgramConfig.userId = string.Empty;
                            ProgramConfig.cashierName = string.Empty;
                            ProgramConfig.cashireAuthorizeResult = null;
                            ProgramConfig.superUserId = string.Empty;
                            ProgramConfig.superUserAuthorizeResult = null;
                            Program.control.ShowForm("frmLogin");
                            Form form = Application.OpenForms["frmLogin"];
                            frmLogin mon = form as frmLogin;
                            mon.ucTextBoxWithIcon1.Focus();

                            List<Form> opened = Application.OpenForms.Cast<Form>().ToList();
                            while (opened.Count > 7)
                            {
                                foreach (Form item in opened)
                                {
                                    if (!item.Name.Equals("frmLoading") && !item.Name.Equals("frmLogin") && !item.Name.Equals("frmMonitorCustomer") && !item.Name.Equals("frmMonitorCustomerFooter") && !item.Name.Equals("frmMonitor2Detail") && !item.Name.Equals("frmVDO") && !item.Name.Equals("frmChangePassword"))
                                    {
                                        try
                                        {
                                            Program.control.CloseForm(item.Name);
                                        }
                                        catch (Exception ex)
                                        {
                                            AppLog.writeLog(ex);
                                        }
                                    }
                                }
                                opened = Application.OpenForms.Cast<Form>().ToList();
                            }
                            Program.control.CloseForm("frmChangePassword");
                        }

                    }
                    else
                    {
                        frmNotify dialog = new frmNotify(res);
                        dialog.ShowDialog(this);
                    }
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                bool result = Program.control.RetryConnection(net.errorType);
                if (result)
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            frmLoading.closeLoading();
        }

        private bool isValid(string oldPass, string newPass, string confirmPass)
        {
            frmNotify dialog;
            bool pErr;
            if (string.IsNullOrEmpty(oldPass))
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "EmptyOldPassword").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "EmptyOldPassword").help;
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //dialog = new frmNotify(ResponseCode.Warning, "กรุณาระบุรหัสผ่านเดิม");
                dialog.ShowDialog(this);
                uctwOldPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(newPass))
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "EmptyNewPassword").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "EmptyNewPassword").help;
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //dialog = new frmNotify(ResponseCode.Warning, "กรุณาระบุรหัสผ่านใหม่");
                dialog.ShowDialog(this);
                uctwNewPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(confirmPass))
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "EmptyConfirmPassword").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "EmptyConfirmPassword").help;
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //dialog = new frmNotify(ResponseCode.Warning, "กรุณาระบุช่องยืนยันรหัสผ่าน");
                dialog.ShowDialog(this);
                uctwConfirmPassword.Focus();
                return false;
            }
            if (newPass.Length != 6)
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "PasswordOnly6Digit").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "PasswordOnly6Digit").help;
                //string responseMessage = "กรุณาใส่รหัสผ่านใหม่ ให้ครบ 6 หลัก";
                //string helpMessage = "";
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                dialog.ShowDialog(this);
                uctwNewPassword.Focus();
                return false;
            }
            if (newPass == ProgramConfig.userId)
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "NotAllowPasswordMatchUserID").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "NotAllowPasswordMatchUserID").help;
                //string responseMessage = "รหัสผ่านใหม่ ต้องไม่ตรงกับ รหัสผู้ใช้";
                //string helpMessage = "";
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                dialog.ShowDialog(this);
                uctwNewPassword.Focus();
                return false;
            }
            pErr = true;
            var ch = newPass.Substring(0, 1);
            for (int i = 1; i < 6; i++)
            {
                if (ch != newPass.Substring(i, 1))
                {
                    pErr = false;
                }
            }
            if (!pErr)
            {
                pErr = true;
                ch = newPass.Substring(0, 1);
                for (int i = 1; i < 6; i++)
                {
                    if (char.Parse(ch) + 1 != char.Parse(newPass.Substring(i, 1)))
                    {
                        pErr = false;
                    }
                    else
                    {
                        ch = newPass.Substring(i, 1);
                    }
                }
            }
            if (!pErr)
            {
                pErr = true;
                ch = newPass.Substring(0, 1);
                for (int i = 1; i < 6; i++)
                {
                    if (char.Parse(ch) - 1 != char.Parse(newPass.Substring(i, 1)))
                    {
                        pErr = false;
                    }
                    else
                    {
                        ch = newPass.Substring(i, 1);
                    }
                }
            }
            if (pErr)
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "NewPasswordIncorrect").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "NewPasswordIncorrect").help;
                //string responseMessage = "รหัสผ่านใหม่ ไม่ถูกต้อง";
                //string helpMessage = "";
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                dialog.ShowDialog(this);
                uctwNewPassword.Focus();
                return false;
            }

            if (alert401 == true)
            {
                if (!oldPass.Equals(ProgramConfig.superPassword))
                {
                    string responseMessage = ProgramConfig.message.get("frmChangePassword", "OldPasswordMismatch").message;
                    string helpMessage = ProgramConfig.message.get("frmChangePassword", "OldPasswordMismatch").help;
                    dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //dialog = new frmNotify(ResponseCode.Warning, "รหัสผ่านเดิมไม่ถูกต้อง");
                    dialog.ShowDialog(this);
                    return false;
                }
            }
            else
            {
                if (!oldPass.Equals(ProgramConfig.password))
                {
                    string responseMessage = ProgramConfig.message.get("frmChangePassword", "OldPasswordMismatch").message;
                    string helpMessage = ProgramConfig.message.get("frmChangePassword", "OldPasswordMismatch").help;
                    dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //dialog = new frmNotify(ResponseCode.Warning, "รหัสผ่านเดิมไม่ถูกต้อง");
                    dialog.ShowDialog(this);
                    return false;
                }
            }

            if (!newPass.Equals(confirmPass))
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "NewPasswordConfirmMismatch").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "NewPasswordConfirmMismatch").help;
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                //dialog = new frmNotify(ResponseCode.Warning, "ข้อมูลที่ระบุในช่องยืนยันไม่ตรงกับรหัสผ่านใหม่");
                dialog.ShowDialog(this);
                uctwNewPassword.Focus();
                return false;
            }

            DataTable dt = process.checkPassword(newPass);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Passw"].ToString().Trim() == dt.Rows[i]["NPassw"].ToString().Trim())
                {
                    pErr = true;
                }
            }

            if (pErr)
            {
                string responseMessage = ProgramConfig.message.get("frmChangePassword", "NotAllowPreviousPassword").message;
                string helpMessage = ProgramConfig.message.get("frmChangePassword", "NotAllowPreviousPassword").help;
                //string responseMessage = "ไม่อนุญาตให้ใช้ รหัสครั้งก่อน";
                //string helpMessage = "";
                dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);
                dialog.ShowDialog(this);
                uctwNewPassword.Text = "";
                uctwConfirmPassword.Text = "";
                uctwNewPassword.Focus();
                return false;
            }            
            return true;
        }

        public DialogResult ShowDialog(Form parent)
        {
            //if (parent.InvokeRequired)
            //{
            //    InvokeDelegate d = new InvokeDelegate(ShowDialog);
            //    object[] o = new object[] { parent };
            //    return (DialogResult)parent.Invoke(d, o);
            //}
            //else
            //{
            //    return base.ShowDialog(parent);
            //}
            this.Location = new Point(0, 0); 
            return base.ShowDialog(parent);
        }

        private void uctwOldPassword_Enter(object sender, EventArgs e)
        {
            pnChangePassword.Location = newLocation;
            splitContainer1.SplitterDistance = 500;

            splitContainer1.Panel2Collapsed = false;
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.currentInput = uctwOldPassword;
        }

        private void uctwOldPassword_Leave(object sender, EventArgs e)
        {
            pnChangePassword.Location = defaultLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeyboard1.Visible = false;
        }

        private void uctwOldPassword_TextBoxKeydown(object sender, EventArgs e)
        {
            uctwOldPassword_Leave(sender, e);
            this.Update();
            this.uctwNewPassword.Focus();
        }

        private void uctwNewPassword_Enter(object sender, EventArgs e)
        {
            pnChangePassword.Location = newLocation;
            splitContainer1.SplitterDistance = 500;

            splitContainer1.Panel2Collapsed = false;
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.currentInput = uctwNewPassword;
        }

        private void uctwNewPassword_Leave(object sender, EventArgs e)
        {
            pnChangePassword.Location = defaultLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeyboard1.Visible = false;
        }

        private void uctwNewPassword_TextBoxKeydown(object sender, EventArgs e)
        {
            uctwNewPassword_Leave(sender, e);
            this.Update();
            this.uctwConfirmPassword.Focus();
        }

        private void uctwConfirmPassword_Enter(object sender, EventArgs e)
        {
            pnChangePassword.Location = newLocation;
            splitContainer1.SplitterDistance = 500;

            splitContainer1.Panel2Collapsed = false;
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.currentInput = uctwConfirmPassword;
        }

        private void uctwConfirmPassword_Leave(object sender, EventArgs e)
        {
            pnChangePassword.Location = defaultLocation;
            splitContainer1.SplitterDistance = 768;

            splitContainer1.Panel2Collapsed = true;
            this.ucKeyboard1.Visible = false;
        }

        private void uctwConfirmPassword_TextBoxKeydown(object sender, EventArgs e)
        {
            uctwConfirmPassword_Leave(sender, e);
            this.Update();
            this.btnOK.PerformClick();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this);
            uctwOldPassword.Select();
        }

    }
}
