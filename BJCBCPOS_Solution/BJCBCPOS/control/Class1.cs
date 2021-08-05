using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using BJCBCPOS_Model;
using System.IO;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    static class Utility
    {
        private static string pathIconLanguage = @"\iconLanguage\";
        private delegate bool ProcessCheckNetWorkLost(NetworkConnectionException net);

        public static void InitialTextBoxIcon(UCTextBoxWithIcon UCtbwi, Image pic, UCTextBoxIconType textBoxType, IconType iconType, string placeHolder = "", bool isShowLine = true, bool isEnabelBtn = true, int maxlength = 32767)
        {
            UCtbwi.Tag = textBoxType;
            UCtbwi.label1.Text = placeHolder;
            InitialTextBoxIcon(UCtbwi, pic, iconType, isShowLine, isEnabelBtn, maxlength);
        }

        public static void InitialTextBoxIcon(UCTextBoxWithIcon UCtbwi, Image pic, IconType iconType, bool isShowLine = true, bool isEnabelBtn = true, int maxlength = 32767)
        {
            if (iconType == IconType.None || iconType == IconType.Scan)
            {
                isEnabelBtn = false;
                isShowLine = false;
            }
            UCtbwi.iCon.Tag = iconType;
            UCtbwi.iCon.BackgroundImage = pic;
            UCtbwi.iCon.Enabled = isEnabelBtn;
            UCtbwi.lineShape1.Visible = isShowLine;
            UCtbwi.TextBox.MaxLength = maxlength;
        }

        //>>>>>>>>>>>>>>>>> Set BackGroundBrightness <<<<<<<<<<<<<<<<<<

        public static Bitmap TrackBarBrightness(Bitmap Img, float val)
        {
            float value = val * 0.01F;
            float[][] colorMatrixElements = new[] { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 1, 0 }, new float[] { value, value, value, 0, 1 } };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Image _img = Img;
            Graphics _g = null;
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));
            _g = Graphics.FromImage(bm_dest);
            _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);
            return bm_dest;
        }

        public static void CenterForm(Form frm, Control parent = null)
        {
            Rectangle r;
            if (parent != null)
                r = parent.RectangleToScreen(parent.ClientRectangle);
            else
                r = Screen.FromPoint(frm.Location).WorkingArea;

            var x = r.Left + (r.Width - frm.Width) / 2;       
            var y = r.Top + (r.Height - frm.Height) / 2;

            frm.Location = new Point(x, y);
        }

        public static void SetFormDialog(Panel frm, PictureBox pic, Control parent = null)
        {
            Rectangle r;
            if (parent != null)
                r = parent.RectangleToScreen(parent.ClientRectangle);
            else
                r = Screen.FromPoint(frm.Location).WorkingArea;

            var x = r.Left + pic.Location.X - (frm.Width + 15); // 15 = ระยะห่างระหว่างลูกศรกับ form
            var y = r.Top + pic.Location.Y - ((frm.Height / 2) - (pic.Height / 2));
            frm.Location = new Point(x, y);
        }

        public static void SetArrow(PictureBox picBox, Control ctrl, Control parent = null)
        {
            Rectangle r;
            if (parent != null)
                r = parent.RectangleToScreen(parent.ClientRectangle);
            else
                r = Screen.FromPoint(picBox.Location).WorkingArea;

            var x = (r.Width - picBox.Width + 2);
            var y = ctrl.Location.Y - ((picBox.Height / 2) - (ctrl.Height / 2)) - r.Top;
            picBox.Location = new Point(x, y);
        }

        public static void SetFormAndArrow(PictureBox pic, Control ctrl_source, Control parent_form, Panel frm_ShowDialog)
        {
            //pic = control picturebox ของรูปลูกศร
            //ctrl_source = control ที่ใช้ action เพื่อให้มีลูกศร
            //parent_form = control หรือ form parent ที่ใช้ในการเปิด form 
            //frm_ShowDialog = form ที่จะเปิด

            Utility.SetArrow(pic, ctrl_source, parent_form);
            Utility.SetFormDialog(frm_ShowDialog, pic, parent_form);
        }

        public static void SetBackGroundBrightness(Control Owner, PictureBox picArea,PictureBox picArrow)
        {
            var bmp = new Bitmap(Owner.Width, Owner.Height);
            Owner.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            picArea.Image = Utility.TrackBarBrightness(bmp, -60);
            picArea.Visible = true;
            picArea.BringToFront();
            picArrow.Parent = picArea;
            picArrow.BringToFront();
        }

        public static void SetBackGroundBrightness(Control Owner, PictureBox picArea, float val = -60)
        {
            var bmp = new Bitmap(Owner.Width, Owner.Height);
            Owner.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            picArea.Image = Utility.TrackBarBrightness(bmp, val);
            picArea.Visible = true;
            picArea.SendToBack();
        }

        public static void SetBackGroundBrightness(Control Owner, Control ctrl, float val = -60)
        {
            var bmp = new Bitmap(Owner.Width, Owner.Height);
            Owner.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            ctrl.BackgroundImage = Utility.TrackBarBrightness(bmp, val);
            ctrl.Visible = true;
            ctrl.BringToFront();
        }

        public static void SetBackGroundBrightness(Control Owner, UserControl UserCtrl, float val = -50)
        {
            var bmp = new Bitmap(Owner.Width, Owner.Height);
            Owner.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            UserCtrl.BackgroundImage = Utility.TrackBarBrightness(bmp, val);
            UserCtrl.Visible = true;
            UserCtrl.BringToFront();
        }

        public static void SetBackGroundWhiteBrightness(Control Owner, PictureBox picArea, PictureBox picArrow)
        {
            var bmp = new Bitmap(Owner.Width, Owner.Height);
            Owner.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            picArea.Image = Utility.TrackBarBrightness(bmp, 25);
            picArea.Visible = true;
            picArea.BringToFront();
            picArrow.Parent = picArea;
            picArrow.BringToFront();
        }

        public static void CropFromScreen(Control Owner, PictureBox picArea)
        {
            Rectangle rect = new Rectangle(0, 0, Owner.Width, Owner.Height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(Owner.Location.X, Owner.Location.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            picArea.Image = bmp;
            picArea.Visible = true;
        }

        public static Bitmap CropFromScreen(int width, int height, int pointX, int pointY)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(pointX, pointY, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            return bmp;
        }

        public static void SetGridColorAlternate<T>(List<T> lstItem, Color color, Color? color2 = null) where T : UserControl
        {          
            int maxnum = lstItem.Count;
            int num = maxnum;
            foreach (T item in lstItem)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = color; //Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = color2.GetValueOrDefault(Color.White);
                }
                num--;
            }
        }

        //public static void SumBottomLine<T>(List<T> lstItem, Label sumAmt, Label sumDiscount, Label sumPayAmt, 
        //    Label sumCntRedeem, Label balPoint,Label sumPoint, Label remainPoint) where T : ITextBox
       

        public static void SetItemCashierCust<T>(Panel pnItem, string seq, string qty) where T : UserControl, IRedeem
        {
            var item = pnItem.Controls.Cast<T>().Where(w => w.SEQText == seq).FirstOrDefault();
            if (item != null)
            {
                item.QTYText = qty;
            }
        }

        public static Image CreateImageLanguage()
        {
            int idx = 0;
            if (ProgramConfig.listActiveLanguage.Count > 0)
            {
                idx = Array.IndexOf<Language>(ProgramConfig.listActiveLanguage.ToArray(), new Language(ProgramConfig.language.ID));
                return CreateImageLanguage(idx);
            }
            else
            {
                return Properties.Resources.usa_enable;
            } 
        }

        public static Image CreateImageLanguage(int idx)
        {
            try
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string imageName = ProgramConfig.listActiveLanguage[idx].ImageName;
                return Image.FromFile(appPath + pathIconLanguage + imageName);
            }
            catch (Exception ex)
            {
                return Properties.Resources.usa_enable;
            }
        }

        public static Image CreateImageLanguage(Language lang)
        {
            try
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string imageName = lang.ImageName;
                return Image.FromFile(appPath + pathIconLanguage + imageName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Image GetLogoImage()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
            string imageName = "LogoApp.bmp";//ProgramConfig.businessLogo;
            return Image.FromFile(appPath + imageName);
        }

        //public static StoreResult CheckNotifyNext(StoreResult res, bool isCheckMessage = false)
        //{
        //    if (res.response == ResponseCode.Information || res.response == ResponseCode.Warning || (res.response == ResponseCode.Error && (isCheckMessage ? res.responseMessage != "" : true)))
        //    {
        //        frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
        //        dialog.ShowDialog();
        //    }
        //    return res;
        //}

        public static StoreResult CheckNotifyNext(StoreResult res)
        {
            if (res.response == ResponseCode.Information || res.response == ResponseCode.Error)
            {
                frmNotify dialog = new frmNotify(res);
                res.dialogRes = dialog.ShowDialog();
            }
            return res;
        }

        public static ProcessResult CheckNotifyNext(ProcessResult res)
        {
            if (res.response == ResponseCode.Information || res.response == ResponseCode.Error || res.response == ResponseCode.Warning)
            {
                frmNotify dialog = new frmNotify(res.response, res.responseMessage, res.helpMessage);
                dialog.ShowDialog();
            }
            return res;
        }

        public static void SetStatusModeFooter()
        {
            if (ProgramConfig.formGlobal != null)
            {
                var ucfootertrans = ProgramConfig.formGlobal.Controls.Find("ucFooterTran1", true).FirstOrDefault();
                if (ucfootertrans != null && ucfootertrans is UCFooterTran)
                {
                    ((UCFooterTran)ucfootertrans).IsStandAlone = true;
                }

                var ucfooter = ProgramConfig.formGlobal.Controls.Find("ucFooter1", true).FirstOrDefault();
                if (ucfooter != null && ucfooter is UCFooter)
                {
                    ((UCFooter)ucfooter).IsStandAlone = true;
                }
            }
        }

        public static bool CheckAuthPass(Form owner, Profile profile, string headerEvent)
        {
            if (profile.profile == ProfileStatus.NotAuthorize)
            {
                frmUserAuthorize auth = new frmUserAuthorize(headerEvent, profile.diffUserStatus);
                auth.function = profile.functionId;
                DialogResult auth_res = auth.ShowDialog(owner);
                if (auth_res != DialogResult.Yes)
                {
                    if (auth_res == DialogResult.Abort)
                    {
                        throw new NetworkConnectionException();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static AuthResult CheckAuthPassRes(Form owner, Profile profile, string headerEvent)
        {
            AuthResult authRes = new AuthResult() { Next = true, maxCancelReceiptAmt = "0", maxDeleteItemAmt = "0", maxPriceChange = "0", Skip = false };
            if (profile.profile == ProfileStatus.NotAuthorize)
            {
                frmUserAuthorize auth = new frmUserAuthorize(headerEvent, profile.diffUserStatus);
                auth.function = profile.functionId;
                DialogResult auth_res = auth.ShowDialog(owner);

                authRes.maxCancelReceiptAmt = auth.maxCancelReceiptAmt;
                authRes.maxDeleteItemAmt = auth.maxDeleteItemAmt;
                authRes.maxPriceChange = auth.maxPriceChange;

                if (auth_res != DialogResult.Yes)
                {
                    if (auth_res == DialogResult.Abort)
                    {
                        throw new NetworkConnectionException();
                    }
                    else
                    {
                        frmLoading.closeLoading();
                        authRes.Next = false;
                        return authRes;
                    }
                }
            }
            else if (profile.profile == ProfileStatus.Authorize)
            {
                authRes.Skip = true;
                authRes.Next = true;
            }
            else
            {
                authRes.Next = false;
            }
            
            return authRes;
        }

        public static void SetStandardFont(Control ctrl)
        {
            Font font = ctrl.Font;
            ctrl.Font = new Font("MM Mega Market Regular", font.Size, font.Style, font.Unit, font.GdiCharSet);
        }

        public static void AlertMessage(ResponseCode responseCode, string message, string help = "")
        {
            frmNotify notify = new frmNotify(responseCode, message, help);
            notify.ShowDialog();
        }

        public static bool AlertMessage(Form Owner, ResponseCode responseCode, string message, string help = "")
        {
            frmNotify notify = new frmNotify(responseCode, message, help);
            notify.ShowDialog(Owner);

            if (notify.DialogResult == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AlertMessage(StoreResult res)
        {
            frmNotify notify = new frmNotify(res);
            notify.ShowDialog();
        }

        public static void AlertMessage(ProcessResult pRes)
        {
            //frmNotify notify = new frmNotify(res);
            //notify.ShowDialog();
        }

        public static void CheckRunningNumber()
        {
            StartProcess process = new StartProcess();
            StoreResult res = process.CheckRunningNumber(ProgramConfig.saleRefNoIni, ProgramConfig.voidRefNoIni, ProgramConfig.returnRefNoIni, ProgramConfig.cashInRefNoIni
                        , ProgramConfig.endOfShiftRefNoIni, ProgramConfig.expermitRefNoIni, ProgramConfig.openDayRefNoIni, ProgramConfig.posrepRefNoIni, ProgramConfig.actionRefNoIni, ProgramConfig.holdOrderRefNoIni, ProgramConfig.tempFFTINo);
        }

        public static void ClearMember(UCTextBoxWithIcon ucTBWI_Member, UCHeader ucHeader)
        {
            ProgramConfig.memberId = "";
            ProgramConfig.memberName = "";
            ProgramConfig.memberCardNo = "";
            ucTBWI_Member.Text = "";
            ucHeader.nameText = "";
            ucHeader.nameVisible = false;
            ucHeader.pnNameSize = new Size(50, 43);
        }

        public static void ShowPayment(string total, Form owner)
        {
            Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Main); //#137 #40 

            //ProgramConfig.disValue = lbTxtdiscount1.Text;
            //ProgramConfig.amtValue = lbTxtSubtotal.Text;
            ProgramConfig.totalValue = total;


            Form form2 = Application.OpenForms["frmMonitor2Detail"];
            frmMonitor2Detail frm2Detail = form2 as frmMonitor2Detail;

            frm2Detail.label1.Text = "";
            frm2Detail.label2.Text = "";
            frm2Detail.label1.BackColor = Color.White;
            frm2Detail.label2.BackColor = Color.White;
            frm2Detail.panel_list.Controls.Clear();
            frm2Detail.panel_payment.BringToFront();
            //frm2Detail.lbTxtTotalCash.Text = amountCash.ToString(displayAmt);
            frm2Detail.lbTxtTotalCash.Text = total;
            //Program.control.CloseForm("frmPayment");
            //Program.control.ShowForm("frmPayment");

            //if (chkRedeem.policy == PolicyStatus.Work)
            //{
            //    if (ProgramConfig.memberId.Trim() != "" && ProgramConfig.memberId != "N/A")
            //    {
            //        if (!Utility.CheckAuthPass(this, chkRedeem, "Redeem"))
            //        {
            //            return;
            //        }
            //        frmRedeem frm = new frmRedeem(RedeemPage.Product);
            //        Program.control.ShowForm(frm, "frmRedeem");
            //        return;
            //    }
            //}

            Program.control.CloseForm("frmPayment");
            Program.control.ShowForm("frmPayment", owner);
        }

        public static Image ByteToImage(byte[] byteAry)
        {
            if (byteAry.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(byteAry))
                {
                    return Image.FromStream(ms, false, true);
                }
            }
            else
            {
                return null;
            }
        }

        public static ProcessResult ProcessCashierMessage(UCHeader ucHeader, FunctionID functionID, Func<FunctionID, ProcessResult> cashireMessageStatus)
        {
            Profile chkMCashier = ProgramConfig.getProfile(functionID);
            if (chkMCashier.policy == PolicyStatus.Work)
            {
                ProcessResult res = cashireMessageStatus(functionID);
                if (res.response.next)
                {
                    if (res.response == ResponseCode.Information)
                    {
                        return res;
                    }

                    if (res.needNextProcess)
                    {
                        ucHeader.alertStatus = true;
                    }
                    else
                    {
                        ucHeader.alertStatus = false;
                    }
                }
                return res;
            }
            return new ProcessResult(new StoreResult(ResponseCode.Ignore, ""), needNextProcess: true);
        }

        public static void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        public static void GlobalClear()
        {
            ProgramConfig.qntValue = "";
            ProgramConfig.amtValue = "";
            ProgramConfig.disValue = "";
            ProgramConfig.redeemLTYD = "";
            ProgramConfig.memberId = "";
            ProgramConfig.memberName = "";
            ProgramConfig.memberCardNo = "";
            ProgramConfig.memberProfileMMFormat.Clear();
        }


        public static void AutoVoidEDC(Form Owner, Func<string, string , StoreResult> SelectDlypTrans)
        {
            int cnt = 0;
        Retry:
            try
            {
                var res = SelectDlypTrans("P", "O");//process.selectDLYPTRANS(ProgramConfig.saleRefNo, vty: "P", dty: "O");
                if (res.response.next)
                {
                    frmLoading.closeLoading();
                    frmEDCProcess fEDC = new frmEDCProcess(EventEDC.Void, "", "", ""
                                            , invoiceNo: res.otherData.Rows[0]["TRACE_NO"].ToString()
                                            , approveCode: res.otherData.Rows[0]["APPROVE_CODE"].ToString());
                    fEDC.ShowDialog(Owner);

                    var EDCResult = fEDC.edcResult;
                    if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.Yes)
                    {
                        fEDC.Dispose();
                    }
                    else if (EDCResult.dialogRes == System.Windows.Forms.DialogResult.No)
                    {
                        if (cnt > 2)
                        {
                            return;
                        }
                        cnt++;
                        Utility.AlertMessage(EDCResult.res);
                        frmLoading.showLoading();
                        goto Retry;
                    }
                }
            }
            catch (Exception ex)
            {
                if (cnt > 2)
                {
                    return;
                }
                cnt++;
                Utility.AlertMessage(ResponseCode.Error, ex.Message);
                goto Retry;
            }
        }

        //public static T ToEnum<T>(this string value, T defaultValue)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        return defaultValue;
        //    }

        //    T result;
        //    return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        //}

        //public static void BackWorker(Action action)
        //{
        //    frmLoading fLoad = new frmLoading();
        //    BackgroundWorker worker = new BackgroundWorker();
        //    worker.DoWork += (s, de) =>
        //    {
        //        action();
        //    };
        //    worker.RunWorkerCompleted += (s, re) =>
        //    {
        //        if (fLoad.InvokeRequired)
        //        {
        //            fLoad.BeginInvoke((MethodInvoker)delegate
        //            {
        //                fLoad.Close();
        //                fLoad.Dispose();
        //            });

        //        }
        //        else
        //        {
        //            fLoad.Close();
        //            fLoad.Dispose();
        //        }
        //    };
        //    worker.WorkerSupportsCancellation = true;
        //    worker.RunWorkerAsync();
        //    worker.Dispose();

        //    fLoad.Show();
        //    fLoad.Refresh();
        //    fLoad.Update();
        //}


        //delegate void MyDelegate();

        //public static void BackWorker(Action actions)
        //{
        //    frmLoading fLoad = new frmLoading();
        //    MyDelegate del = new MyDelegate(actions);
        //    //del();
        //    fLoad.Show();
        //    fLoad.Refresh();
        //    fLoad.Update();

        //    Timer time = new Timer();
        //    time.Interval = 1000;
        //    time.Tick += (s, e) =>
        //        {
        //            time.Stop();

        //            //if (fLoad.InvokeRequired)
        //            //{
        //                fLoad.BeginInvoke((MethodInvoker)delegate
        //                   {
        //                       fLoad.Refresh();
        //                       fLoad.Update();
        //                   });
        //            //}
        //            //else
        //            //{
        //            //    fLoad.Refresh();
        //            //    fLoad.Update();
        //            //}
        //            del();
        //        };
        //    time.Start();
        //}

        //Keep Code
        //public static void BackWorker(Action actions)
        //{
        //    frmLoading fLoad = new frmLoading();
        //    fLoad.Shown += async (s, ee) =>
        //    {
        //        await Task.Run(() =>
        //        {
        //            actions();
        //        }).ConfigureAwait(true);
        //        fLoad.Close();
        //    };
        //    fLoad.Show();
        //}

        //public static void BackWorker(Action actions)
        //{
        //    frmLoading fLoad = new frmLoading();
        //    fLoad.Show();

        //    Application.DoEvents();
        //    actions();
        //}

    }
}
