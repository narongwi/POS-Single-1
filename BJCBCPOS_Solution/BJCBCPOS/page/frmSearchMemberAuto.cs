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
    public partial class frmSearchMemberAuto : Form
    {
        private bool IsPaint = false; 
        private SaleProcess process = new SaleProcess();
        private ReturnProcess processRe = new ReturnProcess();
        public string searchData;
        public StoreResult result = null;
        public StoreResult resultSearch = null;
        public StoreResult resultProfile = null;
        public int cnt { get; set; }
        int searchType;
        string membercard = "";
        string memberID = "";
        string idCard = "";
        string tel = "";
        string tName = "";
        string eName = "";
        string txtName = "";
        string eventName = "";
        FunctionID _functionPolicy;
        
        UserControl _uc;

        public frmSearchMemberAuto(string data, string evName)
        {
            InitializeComponent();
            //actiontype = action;
            //productBarcode = barCode; 
            searchData = data;
            eventName = evName;
        }

        public frmSearchMemberAuto(string data, string evName, UserControl uc, FunctionID functionPolicy = null)
        {
            InitializeComponent();
            searchData = data;
            eventName = evName;
            _uc = uc;
            _functionPolicy = functionPolicy;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    IsPaint = true;
                }
            }
        }

        private void frmDeleteItem_Load(object sender, EventArgs e)
        {
            try
            {               
                if (this.Owner != null)
                {
                    this.Size = this.Owner.Size;

                    int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                    int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                    panel1.Location = new Point(x, y);

                    this.Location = this.Owner.Location;
                }
                searchType = 0;
                frmLoading.showLoading();

                if (eventName == "Deposit")
                {
                    if (_functionPolicy == FunctionID.Deposit_SearchMember1)
                    {
                        result = process.searchMemberByType(searchType, searchData, SearchTypeCustomer.SearchCustomerFullTax);
                    }
                    else if (_functionPolicy == FunctionID.Deposit_SearchMember2)
                    {
                        result = process.searchMemberByType(searchType, searchData, SearchTypeCustomer.SearchMember);
                    }
                    else if (_functionPolicy == FunctionID.Deposit_SearchMember3)
                    {
                        result = process.searchMemberByType(searchType, searchData, SearchTypeCustomer.SearchCustomer);
                    }
                }
                else
                {
                    result = process.searchMember(searchType, searchData);
                }

                resultSearch = result;

                frmLoading.closeLoading();
                if (result.response == ResponseCode.Success)
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
                        pn_MemberList.Controls.Add(uc);
                        pn_MemberList.Controls.SetChildIndex(uc, 0);
                    }

                    Utility.SetGridColorAlternate<UCGridViewSearchCustomer>(pn_MemberList.Controls.Cast<UCGridViewSearchCustomer>().ToList(), Color.FromArgb(220, 220, 220));
                   
                    if (rowCount == 1)
                    {
                        SearchProcess();
                        //if (tName.Trim() != "")
                        //{
                        //    txtName = tName;
                        //}
                        //else
                        //{
                        //    txtName = eName;
                        //}

                        //if (eventName == "Sale")
                        //{
                        //    frmSale frmSale = (frmSale)this.Owner;
                        //    frmSale.frmSearchMemberData(memberID, txtName, membercard);
                        //}
                        //else if (eventName == "ReturnScan")
                        //{
                        //    frmReturnFromScanProduct frmReturnFromScanProduct = (frmReturnFromScanProduct)this.Owner;
                        //    frmReturnFromScanProduct.frmSearchMemberData(memberID, txtName);
                        //}
                        //else if (eventName == "ReturnInvoice")
                        //{
                        //    frmReturnFromInvoice frmReturnFromInvoice = (frmReturnFromInvoice)this.Owner;
                        //    frmReturnFromInvoice.frmSearchMemberData(memberID, txtName);
                        //}
                        //else if (eventName == "UCMember")
                        //{
                        //    UCMember ucMember = (UCMember)_uc;
                        //    ucMember.frmSearchMemberData(memberID, txtName, membercard);
                        //}

                        Dispose();
                        DialogResult = System.Windows.Forms.DialogResult.Yes;
                    }

                    AppMessage.fillForm(ProgramConfig.language, this);
                }
                else
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                    DialogResult = System.Windows.Forms.DialogResult.No;
                }
            }
            catch (NetworkConnectionException)
            {
                DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Dispose();
                //frmLoading.closeLoading();
                //bool result = Program.control.RetryConnection(net.errorType);
                //if (result)
                //{
                //    this.Dispose();
                //}
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
        }

        private void RefreshGrid()
        {
            List<UCItemSell> lstItemSell = new List<UCItemSell>();
            lstItemSell = pn_MemberList.Controls.Cast<UCItemSell>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            pn_MemberList.Controls.Clear();
            int num = lstItemSell.Count;

            foreach (UCItemSell item in lstItemSell)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num.ToString();
                pn_MemberList.Controls.Add(item);
                num--;
            }
            ScrollToBottom(pn_MemberList);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void UCGridViewSearchCustomerClick(object sender, EventArgs e)
        {
            UCGridViewSearchCustomer ucGV = (UCGridViewSearchCustomer)sender;
            memberID = ucGV.lb_MemberID.Text;
            txtName = ucGV.lb_TempName.Text;
            membercard = ucGV.lb_MemberCard.Text;
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //frmSale frmSale = new frmSale(productCode, productName, productQty, productPrice);
            //if (eventName == "Sale")
            //{
            //    frmSale frmSale = (frmSale)this.Owner;
            //    frmSale.frmSearchMemberDataAuto(memberID, txtName, membercard);
            //}
            //else if (eventName == "ReturnScan")
            //{
            //    frmReturnFromScanProduct frmReturnFromScanProduct = (frmReturnFromScanProduct)this.Owner;
            //    frmReturnFromScanProduct.frmSearchMemberDataAuto(memberID, txtName);
            //}
            //else if (eventName == "ReturnInvoice")
            //{
            //    frmReturnFromScanProduct frmReturnFromScanProduct = (frmReturnFromScanProduct)this.Owner;
            //    frmReturnFromScanProduct.frmSearchMemberDataAuto(memberID, txtName);
            //}
            //else if (eventName == "UCMember")
            //{
            //    UCMember ucMember = (UCMember)_uc;
            //    ucMember.frmSearchMemberData(memberID, txtName, membercard);
            //}
            SearchProcess();
            ProgramConfig.memberName = txtName;

            Dispose();
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dispose();
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

                if (_uc != null)
                {
                    if (_uc is UCMember)
                        ((UCMember)_uc).ucTBWI_Member.InpTxt = membercard;
                    else
                        _uc.Text = tName;
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
                else if (eventName == "UCMember")
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
                frmNotify dialog = new frmNotify(ResponseCode.Error, result.responseMessage, result.helpMessage);
                dialog.ShowDialog(this);
                return;
            }
        }
    }
}