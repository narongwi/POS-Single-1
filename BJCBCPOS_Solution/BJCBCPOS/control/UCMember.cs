using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using System.Globalization;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class UCMember : UserControl
    {
        SaleProcess saleProcess = new SaleProcess();
        public delegate void MemberConnectionLostHandler(NetworkConnectionException net);
        public delegate bool CheckPolicyShowDetailHandler();
        public delegate StoreResult InsertTempCustomerFullTaxHandler(StoreResult res);

        UCHeader ucHeader;

        public string memberName = "";
        public string memberID = "";
        public string memberCardNo = "";
        public string eventName = "";
        public FunctionID functionID = FunctionID.NoFunctionID;
        public CheckPolicyShowDetailHandler CheckPolicyShowDetail;
        public InsertTempCustomerFullTaxHandler InsertTempCustomerFullTax;

        private bool _IsPaint = true;

        private bool IsPaint 
        {
            get
            {
                return _IsPaint;
            }
            set
            {
                _IsPaint = value;
            }
        }

        #region Event
              
        [Category("Action")]
        [Description("Occurs when the network lost.")]
        [Browsable(true)]
        public event MemberConnectionLostHandler MemberCatchNetWorkConnectionException;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler MemberButtonBackClick;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler MemberEnterFromButton;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler MemberIconClick;

        #endregion

        #region Properties

        [Category("Custom Property")]
        [Description("Set visible button back")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool VisibleBtnBack
        {
            get
            {
                return picBtnBack.Visible;
            }
            set
            {
                picBtnBack.Visible = value;
            }
        }


        [Category("Custom Property")]
        [Description("Set visible button back")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsSaveMember
        {
            get;
            set;
        }
      
        #endregion

        public UCMember()
        {
            InitializeComponent();
        }

        //public UCMember(bool IsPaint = true)
        //{
        //    InitializeComponent();
        //    _IsPaint = IsPaint;
        //}

        //public UCMember(bool IsShowAddress)
        //{
        //    InitializeComponent();
        //    _IsShowAddress = IsShowAddress;
        //}

        private void UCMember_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this);
            ucHeader = this.FindForm().Controls.Find("ucHeader1", true).FirstOrDefault() as UCHeader;
            ucTBWI_Member.TextBoxIconType = UCTextBoxIconType.SearchAndDelete;

            picBtnBack.Visible = VisibleBtnBack;

            


            //if (ucHeader.nameText.Trim() == "")
            //{
            //    ucTBWI_Member.Text = "";
            //}
        }

        private void picBtnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (MemberButtonBackClick != null)
            {
                MemberButtonBackClick(sender, e);
            }
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    if (!IsPaint)
        //    {
        //        using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
        //        {
        //            e.Graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), new Size(1024, 768)));
        //            IsPaint = true;
        //        }
        //    }
        //}

        private void ucTBWI_Member_EnterFromButton(object sender, EventArgs e)
        {
            btnOk_Click(sender, e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {               
                if (ucHeader != null)
                {
                    eventName = eventName == "" ? "UCMember" : eventName;
                    DialogResult resDialog = System.Windows.Forms.DialogResult.None;

                    Form form = Application.OpenForms["frmMonitor2Detail"];
                    frmMonitor2Detail mon = form as frmMonitor2Detail;

                    Form Owner = this.FindForm();

                    if (ucTBWI_Member.Text.Trim() != "")
                    {
                        string tmpMemberID = ProgramConfig.memberId;
                        string tmpMemberName = ProgramConfig.memberName;
                        string tmpMemberCardNo = ProgramConfig.memberCardNo;

                        frmSearchMemberAuto frm = null;
                        Profile check = ProgramConfig.getProfile(FunctionID.Sale_Member_Search);
                        if (check.policy == PolicyStatus.Work)
                        {
                            frm = new frmSearchMemberAuto(ucTBWI_Member.Text, eventName, this, functionID);
                            resDialog = frm.ShowDialog(this);
                        }
                        if (resDialog == System.Windows.Forms.DialogResult.Yes)
                        {
                            DialogResult res = DialogResult.None;
                            if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                            {
                                if (CheckPolicyShowDetail == null || CheckPolicyShowDetail())
                                {
                                    mon.panel2.Visible = true;
                                    mon.panel2.BringToFront();

                                    //TO DO Change Language
                                    mon.label5.Text = memberName;
                                    mon.label7.Text = "หมายเลขประจำตัวผู้เสียภาษี " + ProgramConfig.memberProfileMMFormat.Customer_IDCard;
                                    mon.label6.Text = ProgramConfig.memberProfileMMFormat.Address;

                                    frmShowInfoCustFullTax frmShowFFTI = new frmShowInfoCustFullTax(memberName);
                                    res = frmShowFFTI.ShowDialog(Owner);
                                }

                                if (frm != null)
                                {
                                    if (frm.resultSearch.response.next)
                                    {
                                        if (InsertTempCustomerFullTax != null)
                                        {
                                            InsertTempCustomerFullTax(frm.resultSearch);
                                        }                                       
                                    }
                                }
                                mon.panel2.Visible = false;
                            }
                          
                            if (res != DialogResult.No)
                            {
                                SaveMember();
                            }
                            else
                            {
                                this.memberID = tmpMemberID;
                                ProgramConfig.memberId = memberID;
                                this.memberName = tmpMemberName;
                                ProgramConfig.memberName = memberName;
                                this.memberCardNo = tmpMemberCardNo;
                                ProgramConfig.memberCardNo = memberCardNo;
                                //ClearMember();
                                ucTBWI_Member.Text = "";
                                ucTBWI_Member.Focus();
                                return;
                            }
                        }
                        else if (resDialog == System.Windows.Forms.DialogResult.No)
                        {
                            //ClearMember();
                            ucTBWI_Member.Text = "";
                            ucTBWI_Member.Focus();
                            return;
                        }
                        else if (resDialog == System.Windows.Forms.DialogResult.Abort)
                        {
                            throw new NetworkConnectionException();
                        }
                    }
                    else
                    {
                        //TO DO Change Language
                        Utility.AlertMessage(ResponseCode.Error, "กรุณาใส่สมาชิก");
                        //ClearMember();
                        ucTBWI_Member.Text = "";
                        ucTBWI_Member.Focus();
                    }

                    if (resDialog == System.Windows.Forms.DialogResult.Yes)
                    {                  
                        if (MemberEnterFromButton != null)
                        {
                            MemberEnterFromButton(this, e);
                        }   
                    }
                    mon.panel2.Visible = false;
                }
                else
                {
                    AppLog.writeLog("UCMember ucHeader = null");
                }               
            }
            catch (NetworkConnectionException net)
            {
                frmLoading.closeLoading();
                if (MemberCatchNetWorkConnectionException != null)
                {
                    MemberCatchNetWorkConnectionException(net);
                }
            }
        }       

        public void frmSearchMemberData(string memberData, string memberDataName, string memberCardNo)
        {
            this.memberID = memberData;
            ProgramConfig.memberId = memberID;
            this.memberName = memberDataName;
            ProgramConfig.memberName = memberName;
            this.memberCardNo = memberCardNo;
            ProgramConfig.memberCardNo = memberCardNo;
        }

        private void ClearMember()
        {
            ProgramConfig.memberId = "";
            ProgramConfig.memberName = "";
            ProgramConfig.memberCardNo = "";
            ProgramConfig.memberProfileMMFormat.Clear();
            ucTBWI_Member.Text = "";
            ucHeader.nameText = "";
            ucHeader.nameVisible = false;
            ucHeader.pnNameSize = new Size(50, 43);
        }

        private void ucTBWI_Member_IconClick(object sender, EventArgs e)
        {
            if (ucHeader != null)
            {
                if (MemberIconClick != null)
                {
                    MemberIconClick(this, e);
                }

                Form form = Application.OpenForms["frmMonitor2Detail"];
                frmMonitor2Detail mon = form as frmMonitor2Detail;

                string tmpMemberID = ProgramConfig.memberId;
                string tmpMemberName = ProgramConfig.memberName;
                string tmpMemberCardNo = ProgramConfig.memberCardNo;

                frmSearchMember frm = null;

                eventName = eventName == "" ? "UCMember" : eventName;
                Profile check = ProgramConfig.getProfile(FunctionID.Sale_Member_Search);
                if (check.policy == PolicyStatus.Work)
                {
                    frmLoading.showLoading();
                    frm = new frmSearchMember((UCTextBoxWithIcon)sender, eventName, this, functionID);
                    frm.ShowDialog(this);
                    frmLoading.closeLoading();
                }

                if (ucTBWI_Member.Text != "")
                {
                    DialogResult res = DialogResult.None;
                    if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                    {
                        if (CheckPolicyShowDetail == null || CheckPolicyShowDetail())
                        {
                            mon.panel2.Visible = true;
                            mon.panel2.BringToFront();

                            //TO DO Change Language
                            mon.label5.Text = memberName;
                            mon.label7.Text = "หมายเลขประจำตัวผู้เสียภาษี " + ProgramConfig.memberProfileMMFormat.Customer_IDCard;
                            mon.label6.Text = ProgramConfig.memberProfileMMFormat.Address;

                            frmShowInfoCustFullTax frmShowFFTI = new frmShowInfoCustFullTax(memberName);
                            res = frmShowFFTI.ShowDialog(this.FindForm());
                        }

                        if (frm != null)
                        {
                            if (frm.resultSearch.response.next)
                            {
                                if (InsertTempCustomerFullTax != null)
                                {
                                    InsertTempCustomerFullTax(frm.resultSearch);
                                }
                            }
                        }
                        mon.panel2.Visible = false;
                    }

                    if (res != DialogResult.No)
                    {
                        SaveMember();
                    }
                    else
                    {
                        this.memberID = tmpMemberID;
                        ProgramConfig.memberId = memberID;
                        this.memberName = tmpMemberName;
                        ProgramConfig.memberName = memberName;
                        this.memberCardNo = tmpMemberCardNo;
                        ProgramConfig.memberCardNo = memberCardNo;

                        ucTBWI_Member.Text = "";
                        ucTBWI_Member.FocusTxt();
                        return;
                    }               
                }
                else
                {
                    this.memberID = tmpMemberID;
                    ProgramConfig.memberId = memberID;
                    this.memberName = tmpMemberName;
                    ProgramConfig.memberName = memberName;
                    this.memberCardNo = tmpMemberCardNo;
                    ProgramConfig.memberCardNo = memberCardNo;

                    ucTBWI_Member.Text = "";
                    ucTBWI_Member.FocusTxt();
                    return;
                }
            }
            else
            {
                AppLog.writeLog("UCMember ucHeader = null");
            }

            //DisableControl();
            //panelScanBarcode.BringToFront();
            //currentPanel = CurrentPanel.PanelScanBarcode;  //currentPanel = "panelScanBarcode";
            //ucTBScanBarcode.Focus();
        }

        private void ucTBWI_Member_TextBoxFocus(object sender, EventArgs e)
        {
            KeyboardApi keyboard = new KeyboardApi(CultureInfo.GetCultureInfo(new Language(1).culture));
        }

        private void SaveMember()
        {
            ucHeader.nameText = memberName;
            ucHeader.nameVisible = true;
            Label newFont = new Label();
            newFont.Font = new Font(ProgramConfig.language.FontName, 14);
            int checkWidth = TextRenderer.MeasureText(memberName, newFont.Font).Width;
            ucHeader.pnNameSize = new Size(50 + checkWidth, 43);
            this.Visible = false;

            if (IsSaveMember)
            {
                saleProcess.SaveMember();
            }
        }
    }
}
