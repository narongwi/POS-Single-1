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
    public partial class frmSearchMember : Form
    {
        private Control _ctrl;
        private int keyboardType = 0;
        private Point defaultLoginPanelLocation = new Point(139, 265);
        private Point newloginPanelLocation = new Point(139, 65);
        int searchType = 0;
        string eventName = "";
        string membercard = "";
        string memberID = "";
        string idCard = "";
        string tel = "";
        string tName = "";
        string eName = "";
        string txtName = "";
        FunctionID _functionPolicy;
        private SaleProcess process = new SaleProcess();
        private ReturnProcess processRe = new ReturnProcess();
        StoreResult result = null;
        UserControl _uc;
        public StoreResult resultSearch = null;
        public StoreResult resultProfile = null;

        private UCGridViewSearchCustomer lastUCgvsc = new UCGridViewSearchCustomer();

        public frmSearchMember()
        {
            InitializeComponent();
        }

        public frmSearchMember(Control ctrl, string evName)
        {
            InitializeComponent();
            _ctrl = ctrl;
            eventName = evName;
        }

        public frmSearchMember(Control ctrl, string evName, UserControl uc, FunctionID functionPolicy = null)
        {
            InitializeComponent();
            _ctrl = ctrl;
            eventName = evName;
            _uc = uc;
            _functionPolicy = functionPolicy;
        }

        public void btnEnable()
        {
            btnOk.Enabled = true;
            btnOk.BackgroundImage = Properties.Resources.button_ok;
        }

        public void btnDisable()
        {
            btnOk.Enabled = false;
            btnOk.BackgroundImage = Properties.Resources.confirm_disable;
        }

        private void frmSearchMember_Load(object sender, EventArgs e)
        {
            AppMessage.fillForm(ProgramConfig.language, this);
            Profile check1 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_ID);
            ucTBWI_Member.Enabled = false;
            ucTextBoxWithIcon2.Enabled = false;
            ucTextBoxWithIcon3.Enabled = false;
            if (check1.policy == PolicyStatus.Work)
            {
                searchType = 1;
            }
            // get keyboard type from pos config
            int.TryParse(ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_Login_KeyboradDisplay.parameterCode).ToString(), out keyboardType);
            ucTxtSearch.InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_none, UCTextBoxIconType.NoneAndDelete, IconType.None, "ກະລຸນາລະບຸຂໍ້ມູນຂອງສະມາຊິກ");
            string Message = ProgramConfig.message.get("frmSearchMember", "MemberID").message;
            ucDropDownList1.LabelText = Message;
            titleTextChange();
            btnDisable();
        }

        public void titleTextChange()
        {
            if (eventName == "Sale")
            {
                ucHeader1.currentMenuTitle1 = saleTitle1.Text;
                ucHeader1.currentMenuTitle2 = saleTitle2.Text;
                ucHeader1.currentMenuTitle3 = saleTitle3.Text;
            }
            else if (eventName == "ReturnScan")
            {
                ucHeader1.currentMenuTitle1 = reInvTitle1.Text;
                ucHeader1.currentMenuTitle2 = reInvTitle2.Text;
                ucHeader1.currentMenuTitle3 = "";
            }
            else if (eventName == "ReturnInvoice")
            {
                ucHeader1.currentMenuTitle1 = "";
                ucHeader1.currentMenuTitle2 = "";
                ucHeader1.currentMenuTitle3 = "";
            }
            else if (eventName == "Deposit")
            {
                ucHeader1.currentMenuTitle1 = saleTitle1.Text;
                ucHeader1.currentMenuTitle2 = saleTitle2.Text;
                ucHeader1.currentMenuTitle3 = saleTitle3.Text;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                //panel2.Location = newloginPanelLocation;
                splitContainer1.SplitterDistance = 768;
                splitContainer1.Panel2Collapsed = true;
                this.ucKeypad1.Visible = true;
                this.ucKeyboard1.Visible = false;
                panel3.Visible = true;
                btnDisable();

                pn_GridView.Controls.Clear();

                if (Convert.ToInt32(ucDropDownList1.ValueText) != 0)
                {
                    searchType = Convert.ToInt32(ucDropDownList1.ValueText);
                }
                else
                {
                    searchType = 1;
                }

                //if (eventName == "Sale")
                //{
                //    result = process.searchMember(searchType, ucTxtSearch.Text);
                //}
                //else if (eventName == "ReturnScan")
                //{
                //    result = processRe.searchMember(searchType, ucTxtSearch.Text);
                //}
                //else if (eventName == "ReturnInvoice")
                //{
                //    result = processRe.searchMember(searchType, ucTxtSearch.Text);
                //}
                //else if (eventName == "UCMember")
                //{
                //    result = process.searchMember(searchType, ucTxtSearch.Text);
                //}
                //else if (eventName == "Deposit")
                //{
                    
                //}

                if (eventName == "Deposit")
                {
                    if (_functionPolicy == FunctionID.Deposit_SearchMember1)
                    {
                        result = process.searchMemberByType(searchType, ucTxtSearch.Text, SearchTypeCustomer.SearchCustomerFullTax);
                    }
                    else if (_functionPolicy == FunctionID.Deposit_SearchMember2)
                    {
                        result = process.searchMemberByType(searchType, ucTxtSearch.Text, SearchTypeCustomer.SearchMember);
                    }
                    else if (_functionPolicy == FunctionID.Deposit_SearchMember3)
                    {
                        result = process.searchMemberByType(searchType, ucTxtSearch.Text, SearchTypeCustomer.SearchCustomer);
                    }
                }
                else
                {
                    result = process.searchMember(searchType, ucTxtSearch.Text, _functionPolicy);
                }

                resultSearch = result;

                if (result.response.next)
                {
                    int rowCount = result.otherData.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (ProgramConfig.memberFormat == MemberFormat.BigC)
                        {
                            membercard = result.otherData.Rows[i]["cardnumber"].ToString();
                            memberID = result.otherData.Rows[i]["MemberId"].ToString();
                            idCard = result.otherData.Rows[i]["idcard"].ToString();
                            tel = result.otherData.Rows[i]["TELEPHONE"].ToString();
                            tName = result.otherData.Rows[i]["TName"].ToString();
                            eName = result.otherData.Rows[i]["EName"].ToString();
                        }
                        else
                        {
                            membercard = result.otherData.Rows[i]["CardHolderNumber"].ToString();
                            memberID = result.otherData.Rows[i]["CustId"].ToString();
                            idCard = result.otherData.Rows[i]["IDCard_No"].ToString();
                            tel = result.otherData.Rows[i]["Mobile"].ToString();
                            tName = result.otherData.Rows[i]["CustName"].ToString();
                            eName = result.otherData.Rows[i]["CustName"].ToString();
                        }


                        UCGridViewSearchCustomer uc = new UCGridViewSearchCustomer();
                        uc.UCGridViewSearchCustomerClick += UCGridViewSearchCustomerClick;
                        uc.lb_MemberCard.Text = membercard;
                        uc.lb_MemberID.Text = memberID;
                        uc.lb_IDCard.Text = idCard;
                        uc.lb_Tel.Text = tel;
                        uc.lb_TempName.Text = tName;
                        uc.Name = uc.placeHolderName + i;

                        if (eName.Trim() == "")
                        {
                            uc.lb_Name.Text = tName;
                        }
                        else
                        {
                            uc.lb_Name.Text = tName + " [" + eName + "]";
                        }
                        //uc.lb_Name.Text = tName + " [" + eName + "]";
                        pn_GridView.Controls.Add(uc);
                        pn_GridView.Controls.SetChildIndex(uc, 0);

                    }

                    Utility.SetGridColorAlternate<UCGridViewSearchCustomer>(pn_GridView.Controls.Cast<UCGridViewSearchCustomer>().ToList(), Color.FromArgb(220, 220, 220));

                    if (rowCount == 1)
                    {
                        SearchProcess();
                    }
                }
                else
                {
                    frmNotify dialog = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                    dialog.ShowDialog(this);
                    return;
                }
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void ucDropDownList1_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                if (eventName == "Sale")
                {
                    ucDDL.lstDDL = SetDataucDropDownList();
                }
                else if (eventName == "ReturnScan")
                {
                    ucDDL.lstDDL = SetDataucDropDownList2();
                }
                else if (eventName == "ReturnInvoice")
                {
                    ucDDL.lstDDL = SetDataucDropDownList2();
                }
                else if (eventName == "UCMember")
                {
                    ucDDL.lstDDL = SetDataucDropDownList();
                }
                else if (eventName == "Deposit")
                {
                    ucDDL.lstDDL = SetDataucDropDownList3();
                }
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            Profile check1 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_ID);
            Profile check2 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_PhoneNo);
            Profile check3 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_CitizenID);
            Profile check4 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_TaxID);
            Profile check5 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_Firstname);
            Profile check6 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_Lastname);
            Profile check7 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_EnglishFirstname);
            Profile check8 = ProgramConfig.getProfile(FunctionID.Sale_Member_Search_EnglishLastname);

            if (check8.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberFamilyEN").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "8" });
            }
            if (check7.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberNameEN").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "7" });
            }
            if (check6.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberFamily").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "6" });
            }
            if (check5.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberName").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "5" });
            }
            if (check4.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "TaxID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "4" });
            }
            if (check3.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "IDCard").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "3" });
            }
            if (check2.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "Telephone").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "2" });
            }
            if (check1.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "1" });
            }
            return lstStr;
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            Profile check1 = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember_Search_MemberID);
            Profile check2 = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember_Search_PhoneNo);
            Profile check3 = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember_Search_CitizenID);
            Profile check4 = ProgramConfig.getProfile(FunctionID.Return_InputCustomer_ByMember_Search_TaxID);

            if (check4.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "TaxID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "4" });
            }
            if (check3.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "IDCard").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "3" });
            }
            if (check2.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "Telephone").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "2" });
            }
            if (check1.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "1" });
            }
            return lstStr;
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList3()
        {
            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            Profile check1 = ProgramConfig.getProfile(FunctionID.NoFunctionID);
            Profile check2 = ProgramConfig.getProfile(FunctionID.NoFunctionID);
            Profile check3 = ProgramConfig.getProfile(FunctionID.NoFunctionID);
            Profile check4 = ProgramConfig.getProfile(FunctionID.NoFunctionID);


            if (_functionPolicy == FunctionID.Deposit_SearchMember1)
            {
                check2 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember1_Search_PhoneNo);
                check4 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember1_Search_TaxID);
            }
            else if (_functionPolicy == FunctionID.Deposit_SearchMember2)
            {
                check1 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember2_Search_MemberID);
                check2 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember2_Search_PhoneNo);
                check3 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember2_Search_CitizenID);
            }
            else if (_functionPolicy == FunctionID.Deposit_SearchMember3)
            {
                check1 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember3_Search_MemberID);
                check2 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember3_Search_PhoneNo);
                check4 = ProgramConfig.getProfile(FunctionID.Deposit_SearchMember3_Search_TaxID);
            }


            if (check4.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "TaxID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "4" });
            }
            if (check3.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "IDCard").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "3" });
            }
            if (check2.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "Telephone").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "2" });
            }
            if (check1.policy == PolicyStatus.Work)
            {
                string Message = ProgramConfig.message.get("frmSearchMember", "MemberID").message;
                lstStr.Add(new BJCBCPOS.UCDropDownList.Dropdown() { DisplayText = Message, ValueText = "1" });
            }

            return lstStr;
        }

        private void UCGridViewSearchCustomerClick(object sender, EventArgs e)
        {
            var ucGV = (UCGridViewSearchCustomer)sender;
            memberID = ucGV.lb_MemberID.Text;
            tName = ucGV.lb_TempName.Text;
            membercard = ucGV.lb_MemberCard.Text;

            btnEnable();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                SearchProcess();
                frmLoading.closeLoading();
            }
            catch (NetworkConnectionException net)
            {
                CatchNetWorkException(net);
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void SearchProcess()
        {
            string memberInp = "";

            if (ProgramConfig.memberFormat == MemberFormat.MegaMaket)
            {
                memberInp = membercard;
            }
            else
            {
                memberInp = memberID;
            }

            if (eventName == "Sale")
            {
                result = process.getMemberProfile(memberInp);
            }
            else if (eventName == "ReturnScan")
            {
                result = processRe.getMemberProfile(memberInp);
            }
            else if (eventName == "ReturnInvoice")
            {
                result = processRe.getMemberProfile(memberInp);
            }
            else if (eventName == "UCMember")
            {
                result = process.getMemberProfile(memberInp);
            }
            else if (eventName == "Deposit")
            {
                if (_functionPolicy == FunctionID.Deposit_SearchMember1)
                {

                }
                else if (_functionPolicy == FunctionID.Deposit_SearchMember2)
                {
                    result = process.getMemberProfileByType(memberInp, SearchTypeCustomer.SearchMember);
                }
                else if (_functionPolicy == FunctionID.Deposit_SearchMember3)
                {
                    result = process.getMemberProfileByType(memberInp, SearchTypeCustomer.SearchCustomer);
                }
            }

            resultProfile = result;

            if (result.response.next)
            {
                if (tName.Trim() != "")
                {
                    txtName = tName;
                }
                else
                {
                    txtName = eName;
                }

                if (_ctrl != null)
                {
                    if (_ctrl is UCTextBoxWithIcon)
                        ((UCTextBoxWithIcon)_ctrl).TextBox.Text = membercard;
                    else
                        _ctrl.Text = tName;
                }

                if (eventName == "Sale")
                {
                    frmSale frmSale = (frmSale)this.Owner;
                    frmSale.frmSearchMemberData(memberID, txtName, membercard);
                }
                else if (eventName == "ReturnScan")
                {
                    frmReturnFromScanProduct frmReturnFromScanProduct = (frmReturnFromScanProduct)this.Owner;
                    frmReturnFromScanProduct.frmSearchMemberData(memberID, txtName);
                }
                else if (eventName == "ReturnInvoice")
                {
                    frmReturnFromInvoice frmReturnFromInvoice = (frmReturnFromInvoice)this.Owner;
                    frmReturnFromInvoice.frmSearchMemberData(memberID, txtName);
                }
                else if (eventName == "UCMember" || eventName == "CreditSale")
                {
                    UCMember ucMember = (UCMember)_uc;
                    ucMember.frmSearchMemberData(memberID, txtName, membercard);
                }
                else if (eventName == "Deposit")
                {
                    UCMember ucMember = (UCMember)_uc;
                    ucMember.frmSearchMemberData(memberID, txtName, membercard);
                }
                Dispose();
            }
            else
            {
                btnDisable();
                frmNotify dialog = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                dialog.ShowDialog(this);
                return;
            }
        }

        private void ucTxtSearch_Enter_1(object sender, EventArgs e)
        {
            switch (keyboardType)
            {
                case 1:
                    //picLogo.Visible = false;
                    //panel2.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad1.Visible = true;
                    this.ucKeyboard1.Visible = false;

                    break;
                case 2:
                    //picLogo.Visible = false;
                    //panel2.Location = newloginPanelLocation;
                    splitContainer1.SplitterDistance = 500;

                    splitContainer1.Panel2Collapsed = false;
                    this.ucKeypad1.Visible = false;
                    this.ucKeyboard1.Visible = true;
                    this.ucKeyboard1.currentInput = ucTxtSearch;
                    break;
                default:
                    //picLogo.Visible = true;
                    //panel2.Location = defaultLoginPanelLocation;
                    splitContainer1.SplitterDistance = 768;

                    splitContainer1.Panel2Collapsed = true;
                    this.ucKeypad1.Visible = false;
                    this.ucKeyboard1.Visible = false;
                    break;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void picBtBack6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            string Message = ProgramConfig.message.get("frmSearchMember", "MemberID").message;
            ucDropDownList1.LabelText = Message;

            titleTextChange();
        }

        private void CatchNetWorkException(NetworkConnectionException net)
        {
            frmLoading.closeLoading();
            if (eventName == "Sale")
            {
                if (this.Owner is frmSale)
                {
                    frmSale fmSale = this.Owner as frmSale;
                    if (fmSale.CatchNetWorkConnectionException(net))
                    {
                        btnDisable();
                        pn_GridView.Controls.Clear();
                        ProgramConfig.formGlobal = fmSale;
                        Program.control.CloseForm(this.Name);
                    }
                }
            }
            else if (eventName == "ReturnScan")
            {
                throw new NetworkConnectionException(net.Message, NetworkErrorType.ConnectionTimeout);
            }
            else if (eventName == "ReturnInvoice")
            {
                throw new NetworkConnectionException(net.Message, NetworkErrorType.ConnectionTimeout);
            }
        }

        private void ucKeyboard1_HideKeyboardClick(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            this.ucKeypad1.Visible = false;
            this.ucKeyboard1.Visible = false;
        }
     }
}

