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
using System.Globalization;

namespace BJCBCPOS
{
    public partial class frmRedeem : Form
    {
        private frmCustRedeem frmCustRD = null;
        public frmSale fSale = null;
        public frmPayment fPayment = null;
        public RedeemPage _page;
        public string displayAmt = ProgramConfig.amountFormatString;
        public string pH_ucItemCustRDProduct = "ucItemCustRDProduct";
        private bool IsRedeemStepBack;
        private bool IsRedeemProduct;
        private bool IsRedeemDiscount;
        private bool IsRedeemCash;
        private bool ChkSkipProduct;
        private bool ChkSkipDiscount;
        private bool ChkSkipCash;
        private int pageSize = 6;
        private SaleProcess process = new SaleProcess();

        public DataTable dtProduct;
        public DataTable dtDiscount;
        public DataTable dtCash;
        public DataTable dtCoupon;

        public DataTable dtCurrentPage;

        string lbHeader_Product = "";
        string lbHeader_Discount = "";
        string lbHeader_Cash = "";
        string lbHeader_Coupon = "";

        public frmRedeem()
        {
            InitializeComponent();
        }

        public frmRedeem(RedeemPage page)
        {
            _page = page;
            InitializeComponent(); //mem 2700
        }

        public void CloseFormRedeemAndCustRedeem()
        {
            //Program.control.CloseForm("frmCustRedeem");         

            //this.pn_ItemRD.Controls.Clear();
            //this.pn_RedeemInfo.Controls.Clear();
            //this.panel1.Controls.Clear();
            //this.Controls.Clear();

            //frmCustRD.pn_ItemCustRDCash.Controls.Clear();
            //frmCustRD.pn_ItemCustRDDiscount.Controls.Clear();
            //frmCustRD.pn_ItemCustRDProductAndCoupon.Controls.Clear();

            ucFooterTran1.mainContent = "";
            ucFooterTran1.fullContent = "";
            ucFooterTran1.functionId = "";
            Program.control.CloseForm("frmRedeem");
            this.Dispose();

            Program.control.CloseForm("frmCustRedeem");
            frmCustRD.Dispose();
            if (_page != RedeemPage.Coupon)
            {
                Program.control.CloseForm("frmPayment");
                Program.control.ShowForm("frmPayment");
            }
        }

        private void frmRedeem_Load(object sender, EventArgs e)
        {
            IsRedeemStepBack = false;
            //this.Disposed += new EventHandler(frmRedeem_Disposed);
            Program.control.ShowForm("frmCustRedeem", this); //mem 2600
            if (frmCustRD == null || fSale == null)
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmCustRedeem)
                    {
                        frmCustRD = (frmCustRedeem)item;                
                    }

                    if (item is frmSale)
                    {
                        fSale = (frmSale)item;
                        //fSale.fSaleProcess.CurrentForm = this;
                    }

                    if (item is frmPayment)
                    {
                        fPayment = (frmPayment)item;
                    }

                    if (frmCustRD != null && fSale != null && fPayment != null)
                    {
                        break;
                    }
                }
            }

            AppMessage.fillForm(ProgramConfig.language, this);

            lbHeader_Product = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Product");
            lbHeader_Discount = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Discount");
            lbHeader_Cash = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Cash");
            lbHeader_Coupon = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Coupon");

            ClearControlData();
            ucHeader1.nameText = ProgramConfig.memberName;
            if (ProgramConfig.memberName != "")
            {
                ucHeader1.nameVisible = true;
                Label newFont = new Label();
                newFont.Font = new Font(ProgramConfig.language.FontName, 14);
                int checkWidth = TextRenderer.MeasureText(ProgramConfig.memberName, newFont.Font).Width;
                ucHeader1.pnNameSize = new Size(50 + checkWidth, 43);
            }
            else
            {
                ucHeader1.nameVisible = false;
            }

            StoreResult result = process.posDisplayContent();
            if (result.response.next)
            {
                if (result.otherData.Rows.Count > 0)
                {
                    if (result.otherData.Columns.Contains("Content_Default"))
                    {
                        ucFooterTran1.mainContent = result.otherData.Rows[0]["Content_Default"].ToString();
                    }
                    if (result.otherData.Columns.Contains("Content_Detail"))
                    {
                        ucFooterTran1.fullContent = result.otherData.Rows[0]["Content_Detail"].ToString();
                    }
                    ucFooterTran1.functionId = FunctionID.Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeCode.formatValue;
                }
            }

            pn_ItemRD.Controls.Clear();

            lbSumAmtVal.Text = ProgramConfig.totalValue;
            if (_page == RedeemPage.Product)
            {
                if (!GetRedeemProduct(lbSumAmtVal.Text, "", "0.00"))
                {
                    ChkSkipProduct = true;
                    _page = RedeemPage.Product;
                    OKClick();
                    //btnOK.PerformClick();
                    //return;
                }
            }
            else if (_page == RedeemPage.Coupon)
            {
                if (!GetRedeemCoupon())
                {
                    CloseFormRedeemAndCustRedeem();
                }
            }
            else
            {
                CloseFormRedeemAndCustRedeem();
            }
        }

        private void ClearControlData()
        {
            lbSumAmtVal.Text = "0.00";
            lbSumDiscountVal.Text = "0.00";
            lbSumPayAmtVal.Text = "0.00";
            lbSumPointUseVal.Text = "0";
            lbSumRemainPointVal.Text = "0";
            lbSumRDCount.Text = "0";
        }

        #region GetRedeem
        public bool GetRedeemProduct(string saleAmt, string rdCode, string qty)
        {
            Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Product); //#138
            if (chkRedeem.policy == PolicyStatus.Work)
            {
                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Product_DisplayCashier); //#290
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    pn_ItemRD.Enabled = true;                
                }
                else
                {
                    pn_ItemRD.Enabled = false;
                }

                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Product_DisplayCustomer); //#641
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    frmCustRD.Show();                   
                }
                else
                {
                    frmCustRD.Hide();
                }

                StoreResult res = fSale.fSaleProcess.CheckRedeemPoint_Free_CPN(saleAmt, rdCode, qty);
                if (res.response.next)
                {
                    if (res.response == ResponseCode.Ignore)
                    {
                        return false;
                    }
                    //AddDataTableProduct();
                    dtCurrentPage = new DataTable();
                    if (rdCode != "")
                    {
                        DataRow dr = dtProduct.AsEnumerable().Where(w => w["redeemcode"].ToString() == rdCode).FirstOrDefault();
                        if (res.otherData.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtProduct.Columns.Count; i++)
                            {
                                dr[i] = res.otherData.Rows[0][i];
                            }
                            dtCurrentPage = dtProduct;
                        }
                    }
                    else
                    {
                        dtCurrentPage = dtProduct = res.otherData;
                    }

                    if (dtProduct.Rows.Count > 0)
                    {
                        BeforeInitialRedeemProductCust();
                        InitialRedeemProductAndCouponCust<UCItemRedeemProduct>(dtProduct);
                        return true;
                    }
                }
            }
            return false;

        }

        private bool GetRedeemDiscount(string saleAmt, string rdCode, string isRedeem)
        {
            Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Discount); //#292

            if (chkRedeem.policy == PolicyStatus.Work)
            {
                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Discount_DisplayCashier); //#293
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    pn_ItemRD.Enabled = true;
                }
                else
                {
                    pn_ItemRD.Enabled = false;
                }

                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Discount_DisplayCustomer); //#643
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    frmCustRD.Show();
                }
                else
                {
                    frmCustRD.Hide();
                }

                return SubGetRedeemDiscount(saleAmt, rdCode, isRedeem);
            }
            return false;
        }

        private bool SubGetRedeemDiscount(string saleAmt, string rdCode, string isRedeem)
        {
            StoreResult res = fSale.fSaleProcess.CheckRedeemPointPercentDiscount(saleAmt, rdCode, isRedeem);
            if (res.response.next)
            {
                if (res.response == ResponseCode.Ignore)
                {
                    return false;
                }
                //AddDataTableDiscount();
                dtDiscount = res.otherData;
                if (dtDiscount.Rows.Count > 0)
                {
                    if (rdCode == "")
                    {
                        BeforeInitialRedeemDiscount();
                        InitialRedeemDiscount();
                    }
                    else
                    {
                        SumBottomLine<UCItemRedeemDiscount>(pn_ItemRD.Controls.Cast<UCItemRedeemDiscount>().ToList());
                    }

                    return true;
                }
            }
            return false;
        }

        private bool GetRedeemCash()
        {
            Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Cash); //#295


            if (chkRedeem.policy == PolicyStatus.Work)
            {
                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Cash_DisplayCashier); //#296
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    pn_ItemRD.Enabled = true;
                }
                else
                {
                    pn_ItemRD.Enabled = false;
                }


                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Cash_DisplayCustomer); //#645
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    frmCustRD.Show();                 
                }
                else
                {
                    frmCustRD.Hide();
                }

                return SubGetRedeemCash();
            }
            return false;
        }

        private bool SubGetRedeemCash()
        {
            StoreResult res = fSale.fSaleProcess.CheckRedeemPoint(lbSumAmtVal.Text); //mem 253
            if (res.response.next)
            {
                if (res.response == ResponseCode.Ignore)
                {
                    return false;
                }

                dtCash = res.otherData;
                //AddDataTableCash();
                if (dtCash.Rows.Count > 0)
                {
                    _page = RedeemPage.Cash;
                    lb_HeaderRedeemPoint.Text = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Cash", lbHeader_Cash);
                    InitialRedeemCash();
                    frmCustRD.pn_RDCash.BringToFront();
                    pn_HeaderRDCash.BringToFront();

                    return true;
                }
            }

            return false;
        }

        public bool GetRedeemCoupon()
        {
            ProgramConfig.redeemRefNo = "";

            Profile chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Main2); //#300 #72

            //if (!Utility.CheckAuthPass(this, chkRedeem, "Redeem"))
            //{
            //    return false;
            //}

            chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon); //#601

            if (chkRedeem.policy == PolicyStatus.Work)
            {
                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon_DisplayCustomer); //#602
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    pn_ItemRD.Enabled = false;
                }
                else
                {
                    pn_ItemRD.Enabled = true;
                }


                chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon_DisplayCustomer); //#650
                if (chkRedeem.policy == PolicyStatus.Work)
                {
                    frmCustRD.Show();                    
                }
                else
                {
                    frmCustRD.Hide();
                }

                StoreResult res = fSale.fSaleProcess.CheckRedeemPoint_Coupon(fPayment.lbTxtBalanceDiff.Text);
                if (res.response.next)
                {
                    if (res.response == ResponseCode.Ignore)
                    {
                        return false;
                    }

                    dtCurrentPage = dtCoupon = res.otherData;
                    if (dtCoupon.Rows.Count > 0)
                    {
                        _page = RedeemPage.Coupon;
                        lb_HeaderRedeemPoint.Text = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Coupon", lbHeader_Coupon);
                        InitialRedeemProductAndCouponCust<UCItemRedeemCoupon>(dtCoupon);
                        frmCustRD.pn_RDProductAndCoupon.BringToFront();
                        pn_HeaderRDCoupon.BringToFront();
                    }
                    return dtCoupon.Rows.Count > 0;
                }

            }

            return false;
        }
        #endregion

        #region AddData

        private void AddDataTableProduct()
        {
            dtProduct = new DataTable();
            dtProduct.Columns.Add("SEQ");
            dtProduct.Columns.Add("B");
            dtProduct.Columns.Add("Values");

            dtProduct.Rows.Add(1, 2, 0);
            dtProduct.Rows.Add(2, 2, 0);
            dtProduct.Rows.Add(3, 2, 0);
            dtProduct.Rows.Add(4, 2, 0);
            dtProduct.Rows.Add(5, 2, 0);
            dtProduct.Rows.Add(6, 2, 0);
            dtProduct.Rows.Add(7, 2, 0);
            dtProduct.Rows.Add(8, 2, 0);
            //dtProduct.Rows.Add(9, 2, 0);
            //dtProduct.Rows.Add(10, 2, 0);
            //dtProduct.Rows.Add(11, 2, 0);
            //dtProduct.Rows.Add(12, 2, 0);
            //dtProduct.Rows.Add(13, 2, 0);
            //dtProduct.Rows.Add(14, 2, 0);
            //dtProduct.Rows.Add(15, 2, 0);
            //dtProduct.Rows.Add(16, 2, 0);

            dtCurrentPage = new DataTable();
            dtCurrentPage = dtProduct;
        }

        private void AddDataTableDiscount()
        {
            dtDiscount = new DataTable();
            dtDiscount.Columns.Add("SEQ");
            dtDiscount.Columns.Add("B");
            dtDiscount.Columns.Add("Values");

            dtDiscount.Rows.Add(1, 2, 0);
            dtDiscount.Rows.Add(2, 2, 0);
            dtDiscount.Rows.Add(3, 2, 0);
            dtDiscount.Rows.Add(4, 2, 0);
            //dtDiscount.Rows.Add(5, 2, 0);
            //dtDiscount.Rows.Add(6, 2, 0);
            //dtDiscount.Rows.Add(7, 2, 0);
            //dtDiscount.Rows.Add(8, 2, 0);
            //dtDiscount.Rows.Add(9, 2, 0);
            //dtDiscount.Rows.Add(10, 2, 0);
            //dtDiscount.Rows.Add(11, 2, 0);
            //dtDiscount.Rows.Add(12, 2, 0);
            //dtDiscount.Rows.Add(13, 2, 0);
            //dtDiscount.Rows.Add(14, 2, 0);
            //dtDiscount.Rows.Add(15, 2, 0);
            //dtDiscount.Rows.Add(16, 2, 0);
        }

        private void AddDataTableCash()
        {
            dtCash = new DataTable();
            dtCash.Columns.Add("SEQ");
            dtCash.Columns.Add("B");
            dtCash.Columns.Add("Values");

            dtCash.Rows.Add(1, 2, 0);
            dtCash.Rows.Add(2, 2, 0);
            dtCash.Rows.Add(3, 2, 0);
            dtCash.Rows.Add(4, 2, 0);
            dtCash.Rows.Add(5, 2, 0);
            //dtCash.Rows.Add(6, 2, 0);
            //dtCash.Rows.Add(7, 2, 0);
            //dtCash.Rows.Add(8, 2, 0);
            //dtDiscount.Rows.Add(9, 2, 0);
            //dtDiscount.Rows.Add(10, 2, 0);
            //dtDiscount.Rows.Add(11, 2, 0);
            //dtDiscount.Rows.Add(12, 2, 0);
            //dtDiscount.Rows.Add(13, 2, 0);
            //dtDiscount.Rows.Add(14, 2, 0);
            //dtDiscount.Rows.Add(15, 2, 0);
            //dtDiscount.Rows.Add(16, 2, 0);
        }

        private void AddDataTableCoupon()
        {
            dtCoupon = new DataTable();
            dtCoupon.Columns.Add("SEQ");
            dtCoupon.Columns.Add("B");
            dtCoupon.Columns.Add("Values");

            dtCoupon.Rows.Add(1, 2, 0);
            dtCoupon.Rows.Add(2, 2, 0);
            dtCoupon.Rows.Add(3, 2, 0);
            dtCoupon.Rows.Add(4, 2, 0);
            //dtCoupon.Rows.Add(5, 2, 0);
            //dtCoupon.Rows.Add(6, 2, 0);
            //dtCoupon.Rows.Add(7, 2, 0);
            //dtCoupon.Rows.Add(8, 2, 0);
            //dtCoupon.Rows.Add(9, 2, 0);
            //dtCoupon.Rows.Add(10, 2, 0);
            //dtCoupon.Rows.Add(11, 2, 0);
            //dtCoupon.Rows.Add(12, 2, 0);
            //dtCoupon.Rows.Add(13, 2, 0);
            //dtCoupon.Rows.Add(14, 2, 0);
            //dtCoupon.Rows.Add(15, 2, 0);
            //dtCoupon.Rows.Add(16, 2, 0);

            dtCurrentPage = new DataTable();
            dtCurrentPage = dtCoupon;
        }

        #endregion

        #region InitialData

        private void BeforeInitialRedeemDiscount()
        {
            _page = RedeemPage.Discount;
            lb_HeaderRedeemPoint.Text = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Discount", lbHeader_Discount);
            frmCustRD.pn_RDDiscount.BringToFront();
            pn_HeaderRDDiscount.BringToFront();
            this.Refresh();
        }

        private void BeforeInitialRedeemProductCust()
        {
            _page = RedeemPage.Product;
            lb_HeaderRedeemPoint.Text = AppMessage.getMessage(ProgramConfig.language, this.Name, "HeaderRedeemPoint_Product", lbHeader_Product);
            pn_HeaderRDProduct.BringToFront();
            frmCustRD.pn_RDProductAndCoupon.BringToFront();
            this.Refresh();
        }

        private void InitialRedeemProductAndCouponCust<T>(DataTable dt) where T : UserControl, IRedeem, new ()
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                pn_ItemRD.Controls.Clear();
                int i = 0;
                int cnt = dt.Rows.Count;
                float cnt2 = dt.Rows.Count;
                string displayCust = "";
                string memid = "";
                string memname = "";
                Profile chkRedeemCust = new Profile();
                Profile chkRedeemCashier = new Profile();

                frmCustRD.VisibleItemRedeem();

                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    T itm = new T();
                    itm.SEQText = i.ToString();

                    if (_page == RedeemPage.Product)
                    {
                        chkRedeemCust = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Product_DisplayCustomer_ButtonPlusMinus); //#291
                        chkRedeemCashier = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Product_DisplayCashier_ButtonPlusMinus); //#642
                        
                        displayCust = String.Format("{0} Point + Cash {1}{2}{3}", dr["redeempoint"], dr["disp_rateuse"], Environment.NewLine, dr["point_redeem_name"]);
                        itm.ItemNameTxt = dr["point_redeem_name"].ToString();
                        itm.UsePointTxt = dr["redeempoint"].ToString();
                        itm.UseCashTxt = dr["disp_rateuse"].ToString();
                        itm.QTYText = dr["cntredeem"].ToString();
                        itm.SumPointTxt = dr["pointuse"].ToString();
                        itm.SumCashTxt = dr["rate"].ToString();
                        itm.DiscountTxt = dr["rateuse"].ToString();
                        itm.LimitTxt = dr["redeemlimit"].ToString();

                        memid = dt.Rows[0]["memberid"].ToString();
                        memname = dt.Rows[0]["memname"].ToString();
                    }
                    else if (_page == RedeemPage.Coupon)
                    {
                        chkRedeemCust = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon_DisplayCustomer_ButtonPlusMinus); //#603
                        chkRedeemCashier = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon_DisplayCashier_ButtonPlusMinus); //#651
                        displayCust = String.Format("{0} Point + Cash {1}{2}{3}", dr["S_REDEEMPOINT"], dr["rateuse"], Environment.NewLine, dr["E_REDEEMPOINT"]);
                        itm.ItemNameTxt = dr["E_REDEEMPOINT"].ToString();
                        itm.UsePointTxt = dr["S_REDEEMPOINT"].ToString();
                        itm.UseCashTxt = dr["RATE"].ToString();
                        itm.QTYText = dr["cntredeem"].ToString();
                        itm.LimitTxt = dr["limitredeem"].ToString();
                        memid = dt.Rows[0]["member_id"].ToString();
                        memname = dt.Rows[0]["member_name"].ToString();

                        itm.SumPointTxt = "0";//dr["pointuse"].ToString();
                        itm.SumCashTxt = "0.00";//dr["rate"].ToString();
                    }

                    itm.VisibleBtnPlusMinus = chkRedeemCashier.policy == PolicyStatus.Work;

                    itm.RedeemCode = dr["redeemcode"].ToString();
                    itm.RuleID = dr["ruleid"].ToString();

                    itm.EnterFromButton -= EnterFromTextBoxSmallRedeemProduct;
                    itm.EnterFromButton += EnterFromTextBoxSmallRedeemProduct;
                    pn_ItemRD.Controls.Add(itm);
                    pn_ItemRD.Controls.SetChildIndex(itm, 0);

                    if (cnt > 6)
                    {
                        cnt = 6;
                    }

                    if (i <= 6)
                    {
                        var ucItmRDP = frmCustRD.pn_ItemCustRDProductAndCoupon.Controls.Find(pH_ucItemCustRDProduct + i, true).Cast<UCItemRedeemProductAndCouponCust>().FirstOrDefault();
                        if (ucItmRDP != null)
                        {
                            ucItmRDP.SEQInt = i;
                            ucItmRDP.lbLimit.Text = itm.LimitTxt;
                            ucItmRDP.QTY = dr["cntredeem"].ToString();
                            ucItmRDP.RedeemCode = dr["redeemcode"].ToString();
                            ucItmRDP.Visible = true;
                            ucItmRDP.lbName.Text = displayCust;

                            if (chkRedeemCust.policy == PolicyStatus.Work)
                            {
                                ucItmRDP.btnMinus.Visible = true;
                                ucItmRDP.btnPlus.Visible = true;
                            }
                            else
                            {
                                ucItmRDP.btnMinus.Visible = false;
                                ucItmRDP.btnPlus.Visible = false;
                            }
                        }
                    }
                }

                lbMemberIDVal.Text = memid; //ProgramConfig.memberId;
                lbNameVal.Text = memname;//ProgramConfig.memberName;      
                lbExpireDateVal.Text = dt.Rows[0]["expdate"].ToString();

                lbPointVal.Text = dt.Rows[0]["currentpoint"].ToString();
                lbSumAmtVal.Text = Convert.ToDouble(dt.Rows[0]["purchaseamt"].ToString()).ToString(displayAmt);//  purchaseamt
                SumBottomLine<T>(pn_ItemRD.Controls.Cast<T>().ToList());

                AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pn_ItemRD);

                Utility.SetGridColorAlternate<T>(pn_ItemRD.Controls.Cast<T>().ToList(), Color.FromArgb(220, 220, 220), Color.FromArgb(240, 240, 240));

                var val = cnt2 / 6;

                frmCustRD.lb_PageNo.Text = "1";
                frmCustRD.lb_PageTotal.Text = Math.Ceiling(val).ToString();
                pn_ItemRD.Refresh();

            }
        }

        private void InitialRedeemCash()
        {
            pn_ItemRD.Controls.Clear();
            frmCustRD.pn_ItemCustRDCash.Controls.Clear();

            
            int i = 0;

            foreach (DataRow dr in dtCash.Rows) //mem 3000
            {
                i++;
                UCItemRedeemCash itm = new UCItemRedeemCash();
                itm.SEQText = i.ToString();
                itm.ItemNameTxt = dr["point_redeem_name"].ToString();
                itm.lbCash_PointToRedeem.Text = dr["S_REDEEMPOINT"].ToString();
                itm.lbCash_RedeemAmt.Text = dr["RATE"].ToString();
                itm.QTYText = dr["CNTREDEEM"].ToString();
                itm.SumPointTxt = dr["POINTUSE"].ToString();
                itm.DiscountTxt = dr["RATEUSE"].ToString();
                itm.RedeemCode = dr["redeemcode"].ToString();
                itm.RuleID = dr["ruleid"].ToString();
                itm.TypeNameTxt = dr["POINT_REDEEM_NAME"].ToString();

                itm.EnterFromButton -= UCItemRedeemCash_EnterFromButton;
                itm.EnterFromButton += UCItemRedeemCash_EnterFromButton;
                pn_ItemRD.Controls.Add(itm);
                pn_ItemRD.Controls.SetChildIndex(itm, 0);

                Profile chkRedeemCashier = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Cash_DisplayCashier_ButtonPlusMinus); //#646
                itm.VisibleBtnPlusMinus = chkRedeemCashier.policy == PolicyStatus.Work;

                UCItemRedeemCashCust itmCust = new UCItemRedeemCashCust();
                itmCust.SEQText = i.ToString();
                itmCust.ItemNameTxt = dr["point_redeem_name"].ToString();
                itmCust.lbCashCust_PointToRedeem.Text = dr["S_REDEEMPOINT"].ToString();
                itmCust.lbCashCust_RedeemAmt.Text = dr["RATE"].ToString();
                itmCust.QTYText = dr["CNTREDEEM"].ToString();
                itmCust.SumPointTxt = dr["POINTUSE"].ToString();
                itmCust.DiscountTxt = dr["RATEUSE"].ToString();
                itm.RedeemCode = dr["redeemcode"].ToString();
                itm.RuleID = dr["ruleid"].ToString();

                itmCust.ButtonPlusMinusClick -= UCItemRedeemCash_ButtonPlusMinusClick;
                itmCust.ButtonPlusMinusClick += UCItemRedeemCash_ButtonPlusMinusClick;

                Profile chkRedeemCust = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Cash_DisplayCustomer_ButtonPlusMinus); //#297

                if (chkRedeemCust.policy == PolicyStatus.Work)
                {
                    itmCust.btnMinus.Visible = true;
                    itmCust.btnPlus.Visible = true;
                }
                else
                {
                    itmCust.btnMinus.Visible = false;
                    itmCust.btnPlus.Visible = false;
                }

                frmCustRD.pn_ItemCustRDCash.Controls.Add(itmCust);
                frmCustRD.pn_ItemCustRDCash.Controls.SetChildIndex(itmCust, 0);
            }

            lbMemberIDVal.Text = dtCash.Rows[0]["member_id"].ToString(); //ProgramConfig.memberId;
            lbNameVal.Text = dtCash.Rows[0]["member_name"].ToString(); //ProgramConfig.memberName;
            lbExpireDateVal.Text = dtCash.Rows[0]["expdate"].ToString();

            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pn_ItemRD);
            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, frmCustRD);

            Utility.SetGridColorAlternate<UCItemRedeemCash>(pn_ItemRD.Controls.Cast<UCItemRedeemCash>().ToList(), Color.FromArgb(220, 220, 220));
            Utility.SetGridColorAlternate<UCItemRedeemCashCust>(frmCustRD.pn_ItemCustRDCash.Controls.Cast<UCItemRedeemCashCust>().ToList(), Color.FromArgb(255, 188, 150));
            pn_ItemRD.Refresh();

            lbSumAmtVal.Text = Convert.ToDouble(dtCash.Rows[0]["purchaseamt"]).ToString(displayAmt);
            lbPointVal.Text = Convert.ToDouble(dtCash.Rows[0]["currentpoint"]).ToString("N0");
            SumBottomLine<UCItemRedeemCash>(pn_ItemRD.Controls.Cast<UCItemRedeemCash>().ToList());
        }

        private void InitialRedeemDiscount()
        {
            Profile chkRedeemCust = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Discount_DisplayCustomer_ButtonYesNo); //#294
            Profile chkRedeemCashier = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Discount_DisplayCashier_ButtonYesNo); //#644

            pn_ItemRD.Controls.Clear();
            frmCustRD.pn_ItemCustRDDiscount.Controls.Clear();

            int i = 0;

            foreach (DataRow dr in dtDiscount.Rows)
            {
                i++;
                UCItemRedeemDiscount itm = new UCItemRedeemDiscount();
                itm.SEQText = i.ToString();
                itm.ItemNameTxt = dr["point_redeem_name"].ToString();
                itm.lbDisc_CouponVal.Text = dr["redeempurchaseamt"].ToString();
                itm.UsePointTxt = dr["pointuse"].ToString();
                itm.DiscountTxt = dr["rateuse"].ToString();
                itm.lbDisc_Rate.Text = dr["discount"].ToString();
                itm.btnYesNo.Text = dr["isredeem"].ToString();
                itm.RedeemCode = dr["redeemcode"].ToString();

                itm.ClickYesNoButton -= ClickYesNoButtonDiscount;
                if (chkRedeemCashier.policy == PolicyStatus.Work)
                {
                    itm.btnYesNo.Enabled = true;
                    itm.btnYesNo.Visible = true;
                    itm.ClickYesNoButton += ClickYesNoButtonDiscount;
                }
                else
                {
                    itm.btnYesNo.Visible = false;
                    itm.btnYesNo.Enabled = false;
                }

                pn_ItemRD.Controls.Add(itm);
                pn_ItemRD.Controls.SetChildIndex(itm, 0);

                UCItemRedeemDiscountCust itmCust = new UCItemRedeemDiscountCust();
                itmCust.lb_Seq.Text = i.ToString();
                itmCust.lbName.Text = dr["point_redeem_name"].ToString();
                itmCust.lbPrice.Text = dr["redeempurchaseamt"].ToString();
                itmCust.lbPointUse.Text = dr["pointuse"].ToString();
                itmCust.lbRateUse.Text = dr["rateuse"].ToString();
                itmCust.lbRate.Text = dr["rate"].ToString();
                itmCust.btnYesNo.Text = dr["isredeem"].ToString();
                itmCust.RedeemCode = dr["redeemcode"].ToString();

                itmCust.ClickYesNoButton -= ClickYesNoButtonDiscount;
                if (chkRedeemCust.policy == PolicyStatus.Work)
                {
                    itmCust.btnYesNo.Enabled = true;
                    itmCust.ClickYesNoButton += ClickYesNoButtonDiscount;
                }
                else
                {
                    itmCust.btnYesNo.Enabled = false;
                }

                if (dr["isredeem"].ToString() == "Y")
                {
                    itmCust.btnYesNo.BackgroundImage = itm.btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountEnable;
                    itmCust.btnYesNo.ForeColor = itm.btnYesNo.ForeColor = Color.White;
                }
                else
                {
                    itmCust.btnYesNo.BackgroundImage = itm.btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountDisable;
                    itmCust.btnYesNo.ForeColor = itm.btnYesNo.ForeColor = Color.Black;
                }

                frmCustRD.pn_ItemCustRDDiscount.Controls.Add(itmCust);
                frmCustRD.pn_ItemCustRDDiscount.Controls.SetChildIndex(itmCust, 0);
                
            }

            lbMemberIDVal.Text = dtDiscount.Rows[0]["memberid"].ToString(); //ProgramConfig.memberId;
            lbNameVal.Text = dtDiscount.Rows[0]["memname"].ToString(); //ProgramConfig.memberName;
            lbExpireDateVal.Text = dtDiscount.Rows[0]["expdate"].ToString();


            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pn_ItemRD);
            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, frmCustRD);

            Utility.SetGridColorAlternate<UCItemRedeemDiscount>(pn_ItemRD.Controls.Cast<UCItemRedeemDiscount>().ToList(), Color.FromArgb(220, 220, 220));
            Utility.SetGridColorAlternate<UCItemRedeemDiscountCust>(frmCustRD.pn_ItemCustRDDiscount.Controls.Cast<UCItemRedeemDiscountCust>().ToList(), Color.FromArgb(255, 188, 150));
            pn_ItemRD.Refresh();

            lbSumAmtVal.Text = Convert.ToDouble(dtDiscount.Rows[0]["purchaseamt"]).ToString(displayAmt);
            lbPointVal.Text = Convert.ToDouble(dtDiscount.Rows[0]["currentpoint"]).ToString("N0");
            SumBottomLine<UCItemRedeemDiscount>(pn_ItemRD.Controls.Cast<UCItemRedeemDiscount>().ToList());
        }

        private void SumBottomLine()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void UCItemRedeemCash_EnterFromButton(object sender, EventArgs e)
        {
            UCItemRedeemCash ucItem = (UCItemRedeemCash)sender;
            UCItemRedeemCashCust ucItemCust = frmCustRD.pn_ItemCustRDCash.Controls.Cast<UCItemRedeemCashCust>().Where(w => w.SEQText == ucItem.lb_Seq.Text).FirstOrDefault();
            string qty = ucItem.QTYText;
            Utility.SetItemCashierCust<UCItemRedeemCashCust>(frmCustRD.pn_ItemCustRDCash, ucItem.lb_Seq.Text, qty);
            ucItem.SumPointTxt = ucItemCust.SumPointTxt = (Convert.ToDouble(ucItem.lbCash_PointToRedeem.Text) * Convert.ToDouble(ucItem.QTYText)).ToString("N0");
            ucItem.DiscountTxt = ucItemCust.DiscountTxt = (Convert.ToDouble(ucItem.lbCash_RedeemAmt.Text) * Convert.ToDouble(ucItem.QTYText)).ToString(displayAmt);
            SumBottomLine<UCItemRedeemCash>(pn_ItemRD.Controls.Cast<UCItemRedeemCash>().ToList());
            if (Convert.ToDouble(lbSumPayAmtVal.Text) < 0 || Convert.ToDouble(lbSumRemainPointVal.Text) < 0)
            {
                //frmNotify notify = new frmNotify(ResponseCode.Error, String.Format("ไม่อนุญาติให้ Redeem ได้มากกว่ายอดซื้อและคะแนนคงเหลือ"));
                frmNotify notify = new frmNotify(ResponseCode.Error,
                                                        ProgramConfig.message.get("frmRedeem", "NotAllowRDMoreThanPriceNPoint").message,
                                                        ProgramConfig.message.get("frmRedeem", "NotAllowRDMoreThanPriceNPoint").help);     
                notify.ShowDialog(this);
                ucItem.QTYText = "0"; // (Convert.ToDouble(qty) - 1).ToString();
                qty = "0";
                Utility.SetItemCashierCust<UCItemRedeemCashCust>(frmCustRD.pn_ItemCustRDCash, ucItem.lb_Seq.Text, qty);
                ucItem.SumPointTxt = ucItemCust.SumPointTxt = (Convert.ToDouble(ucItem.lbCash_PointToRedeem.Text) * Convert.ToDouble(qty)).ToString("N0");
                ucItem.DiscountTxt = ucItemCust.DiscountTxt = (Convert.ToDouble(ucItem.lbCash_RedeemAmt.Text) * Convert.ToDouble(qty)).ToString(displayAmt);
                SumBottomLine<UCItemRedeemCash>(pn_ItemRD.Controls.Cast<UCItemRedeemCash>().ToList());
            }
        }

        private void UCItemRedeemCash_ButtonPlusMinusClick(object sender, EventArgs e)
        {
            if (sender is UCItemRedeemCashCust)
            {
                UCItemRedeemCashCust ucItemCust = (UCItemRedeemCashCust)sender;
                UCItemRedeemCash ucItem = pn_ItemRD.Controls.Cast<UCItemRedeemCash>().Where(w => w.SEQText == ucItemCust.lb_Seq.Text).FirstOrDefault();

                string qty = ucItem.QTYText;
                Utility.SetItemCashierCust<UCItemRedeemCash>(pn_ItemRD, ucItemCust.lb_Seq.Text, ucItemCust.QTYText);
                ucItemCust.SumPointTxt = ucItem.SumPointTxt = (Convert.ToDouble(ucItem.lbCash_PointToRedeem.Text) * Convert.ToDouble(ucItem.QTYText)).ToString("N0");
                ucItemCust.DiscountTxt = ucItem.DiscountTxt = (Convert.ToDouble(ucItem.lbCash_RedeemAmt.Text) * Convert.ToDouble(ucItem.QTYText)).ToString(displayAmt);
                SumBottomLine<UCItemRedeemCash>(pn_ItemRD.Controls.Cast<UCItemRedeemCash>().ToList());

                if (Convert.ToDouble(lbSumPayAmtVal.Text) < 0 || Convert.ToDouble(lbSumRemainPointVal.Text) < 0)
                {
                    //frmNotify notify = new frmNotify(ResponseCode.Error, String.Format("ไม่อนุญาติให้ Redeem ได้มากกว่ายอดซื้อและคะแนนคงเหลือ"));
                    frmNotify notify = new frmNotify(ResponseCode.Error, 
                                                        ProgramConfig.message.get("frmRedeem", "NotAllowRDMoreThanPriceNPoint").message,
                                                        ProgramConfig.message.get("frmRedeem", "NotAllowRDMoreThanPriceNPoint").help);       
                    notify.ShowDialog(this);
                    ucItemCust.QTYText = qty;
                    Utility.SetItemCashierCust<UCItemRedeemCash>(pn_ItemRD, ucItemCust.lb_Seq.Text, qty.ToString());
                    ucItemCust.SumPointTxt = ucItem.SumPointTxt = (Convert.ToDouble(ucItem.lbCash_PointToRedeem.Text) * Convert.ToDouble(qty)).ToString("N0");
                    ucItemCust.DiscountTxt = ucItem.DiscountTxt = (Convert.ToDouble(ucItem.lbCash_RedeemAmt.Text) * Convert.ToDouble(qty)).ToString(displayAmt);
                    SumBottomLine<UCItemRedeemCash>(pn_ItemRD.Controls.Cast<UCItemRedeemCash>().ToList());
                }
            }
        }

        private void SumCash()
        {

        }

        #region BackUp Old Code
        //private void EnterFromTextBoxSmall(object sender, EventArgs e)
        //{
        //    UCItemRedeemProduct ucItmRD = (UCItemRedeemProduct)sender;
        //    int qty = Convert.ToInt32(ucItmRD.ucTextBoxSmall1.Text);
        //    int seq = Convert.ToInt32(ucItmRD.lb_Seq.Text);
        //    //string currentPage = frmCustRD.lb_PageNo.Text;
        //    int totalPage = Convert.ToInt32(frmCustRD.lb_PageTotal.Text);
        //    int currentPage = Convert.ToInt32(frmCustRD.lb_PageNo.Text);
        //    bool condition = false;

        //    SaveValueCashier();

        //    int skip;
        //    int gotoPage;

        //    if ((seq % 6) != 0)
        //    {
        //        gotoPage = (seq / 6) + 1;
        //    }
        //    else
        //    {
        //        gotoPage = (seq / 6);
        //    }
               

        //    if (gotoPage > currentPage)
        //    {
        //        skip = currentPage;
        //        gotoPage = currentPage + 1;
        //        condition = gotoPage <= Convert.ToInt32(frmCustRD.lb_PageTotal.Text);
        //    }
        //    else if (gotoPage < currentPage)
        //    {
        //        skip = gotoPage - 1;
        //        gotoPage = currentPage - 1;
        //        condition = gotoPage > 0;
        //    }
        //    else
        //    {
        //        skip = gotoPage;
        //    }

           
        //    GoToPage(gotoPage, condition, skip);
        //    int numItm = 0;
        //    if ((gotoPage * 6) >= seq)
        //    {
        //        numItm = seq - ((gotoPage - 1) * 6);
        //    }
        //    else
        //    {

        //    }

        //    if (!condition)
        //    {
        //        skip = (currentPage * 6) - 6;
        //        List<DataRow> lstDR = dtProduct.AsEnumerable().Skip(skip).Take(6).ToList();

        //        foreach (var dr in lstDR)
        //        {
        //            int num = Convert.ToInt32(dr["SEQ"]) - skip;
        //            UCItemRedeemProductCust ucItmRDP = frmCustRD.pn_ItemCustRDProduct.Controls.Find(pH_ucItemCustRDProduct + num, true).Cast<UCItemRedeemProductCust>().FirstOrDefault();
        //            if (ucItmRDP != null)
        //            {
        //                ucItmRDP.lb_QTY.Text = dr["Values"] + "";
        //            }
        //        }
        //    }

        //    SaveOldValueCust();
        //    frmCustRD.Refresh();
        //}
        #endregion

        private void ClickYesNoButtonDiscount(object sender, EventArgs e)
        {
            if (sender is UCItemRedeemDiscount)
            {
                var itm = (UCItemRedeemDiscount)sender;

                var ucItm = frmCustRD.pn_ItemCustRDDiscount.Controls.Cast<UCItemRedeemDiscountCust>().Where(w => w.lb_Seq.Text == itm.lb_Seq.Text).FirstOrDefault();

                if (itm.btnYesNo.Text == "Y")
                {
                    ucItm.btnYesNo.Text = "Y";
                    ucItm.btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountEnable;
                    ucItm.btnYesNo.ForeColor = Color.White;

                }
                else
                {
                    ucItm.btnYesNo.Text = "N";
                    ucItm.btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountDisable;
                    ucItm.btnYesNo.ForeColor = Color.Black;
                }

                SubGetRedeemDiscount(lbSumAmtVal.Text, ucItm.RedeemCode, ucItm.btnYesNo.Text);
            }
            else if (sender is UCItemRedeemDiscountCust)
            {
                var itm = (UCItemRedeemDiscountCust)sender;
                var ucItm = pn_ItemRD.Controls.Cast<UCItemRedeemDiscount>().Where(w => w.lb_Seq.Text == itm.lb_Seq.Text).FirstOrDefault();

                if (itm.btnYesNo.Text == "Y")
                {
                    ucItm.btnYesNo.Text = "Y";
                    ucItm.btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountEnable;
                    ucItm.btnYesNo.ForeColor = Color.White;
                }
                else
                {
                    ucItm.btnYesNo.Text = "N";
                    ucItm.btnYesNo.BackgroundImage = Properties.Resources.redeemDiscountDisable;
                    ucItm.btnYesNo.ForeColor = Color.Black;
                }

                SubGetRedeemDiscount(lbSumAmtVal.Text, ucItm.RedeemCode, ucItm.btnYesNo.Text);
            }
        }

        private void EnterFromTextBoxSmallRedeemProduct(object sender, EventArgs e)
        {
            int qty = 0;
            int seq = 0;
            int limit = 0;
            int point = 0; // for test
            int cash = 0; // for test
            string rdCode = "";
            string name = "";

            if (sender is UCItemRedeemProduct)
            {
                UCItemRedeemProduct ucItmRD = (UCItemRedeemProduct)sender;
                qty = Convert.ToInt32(ucItmRD.ucTextBoxSmall1.Text);
                seq = Convert.ToInt32(ucItmRD.lb_Seq.Text);
                limit = Convert.ToInt32(ucItmRD.LimitTxt);
                name = ucItmRD.ItemNameTxt;
                rdCode = ucItmRD.RedeemCode;
                if (!EnterRedeemProduct<UCItemRedeemProduct>(qty, limit, seq, rdCode, name))
                {
                    ucItmRD.ucTextBoxSmall1.Text = "0";
                    //SaveValueCashier();
                    //SumBottomLine<UCItemRedeemProduct>(pn_ItemRD.Controls.Cast<UCItemRedeemProduct>().ToList());
                    EnterRedeemProduct<UCItemRedeemProduct>(0, limit, seq, rdCode, name);
                }
            }
            else if (sender is UCItemRedeemCoupon)
            {
                UCItemRedeemCoupon ucItmRD = (UCItemRedeemCoupon)sender;
                qty = Convert.ToInt32(ucItmRD.ucTextBoxSmall1.Text);
                seq = Convert.ToInt32(ucItmRD.lb_Seq.Text);
                limit = Convert.ToInt32(ucItmRD.LimitTxt);
                name = ucItmRD.ItemNameTxt;
                if (!EnterRedeemProduct<UCItemRedeemCoupon>(qty, limit, seq, rdCode, name))
                {
                    ucItmRD.ucTextBoxSmall1.Text = "0";
                    //SaveValueCashier();
                    //SumBottomLine<UCItemRedeemCoupon>(pn_ItemRD.Controls.Cast<UCItemRedeemCoupon>().ToList());
                    EnterRedeemProduct<UCItemRedeemProduct>(0, limit, seq, rdCode, name);
                }
            }          
        }

        public bool EnterRedeemProduct<T>(int qty, int limit, int seq, string rdCode, string name = "") where T : IRedeem
        {
            if (qty <= limit)
            {
                if (_page == RedeemPage.Product)
                {
                    GetRedeemProduct(lbSumAmtVal.Text, rdCode, qty.ToString());
                }

                if (dtCurrentPage.Rows.Count > 0)
                {
                    int totalPage = Convert.ToInt32(frmCustRD.lb_PageTotal.Text);
                    int currentPage = Convert.ToInt32(frmCustRD.lb_PageNo.Text);

                    SaveValueCashier();

                    int skip;
                    int gotoPage;

                    gotoPage = (int)Math.Ceiling(seq / (float)pageSize);
                    gotoPage = gotoPage <= 0 ? 1 : gotoPage;
                    skip = gotoPage - 1;

                    RefreshPageProductAndCoupon<T>(gotoPage, skip);
                }
                return true;
            }
            else
            {
                //frmNotify notify = new frmNotify(ResponseCode.Error, String.Format(name + Environment.NewLine + "ไม่อนุญาติให้ Redeem ได้มากกว่า {0} สิทธิ์", limit));
                frmNotify notify = new frmNotify(ResponseCode.Error, String.Format(name + Environment.NewLine + ProgramConfig.message.get("frmRedeem", "NotAllowMoreThan_N_Privilege").message, limit));
                notify.ShowDialog(this);
                return false;
            }
            //frmCustRD.Refresh();
        }

        private void SaveValueCashier()
        {
            double disAmt = 0;
            double rdPoint = 0;
            int rdCnt = 0;

            foreach (var item in pn_ItemRD.Controls)
            {
                if (item is UCItemRedeemProduct)
                {
                    UCItemRedeemProduct ucItemRD = item as UCItemRedeemProduct;
                    int qty = Convert.ToInt32(ucItemRD.ucTextBoxSmall1.Text);
                    double point = Convert.ToDouble(ucItemRD.lbProduct_UsePoint.Text); // for test
                    double disc = Convert.ToDouble(ucItemRD.lbProduct_Discount.Text); // for test

                    //ucItemRD.lbProduct_SumPoint.Text = (point * qty).ToString("N0");
                    //ucItemRD.lbProduct_SumCash.Text = (cash * qty).ToString(ProgramConfig.amountFormatString);

                    rdCnt += qty;
                    rdPoint += point * qty;
                    disAmt += disc;

                    //SaveDataTable(Convert.ToInt32(ucItemRD.lb_Seq.Text), ucItemRD.RedeemCode ,Convert.ToInt32(ucItemRD.QTYText));
                }
                else if (item is UCItemRedeemCoupon)
                {
                    UCItemRedeemCoupon ucItemRD = item as UCItemRedeemCoupon;
                    int qty = Convert.ToInt32(ucItemRD.ucTextBoxSmall1.Text);
                    double point = Convert.ToDouble(ucItemRD.UsePointTxt); // for test
                    double cash = Convert.ToDouble(ucItemRD.UseCashTxt); // for test

                    ucItemRD.SumPointTxt = (point * qty).ToString("N0");
                    ucItemRD.SumCashTxt = (cash * qty).ToString(ProgramConfig.amountFormatString);

                    rdCnt += qty;
                    rdPoint += point * qty;
                    //SaveDataTable(Convert.ToInt32(ucItemRD.lb_Seq.Text), ucItemRD.RedeemCode ,Convert.ToInt32(ucItemRD.QTYText));
                }
            }

            lbSumRDCount.Text = rdCnt.ToString();
            lbSumPointUseVal.Text = rdPoint.ToString("N0");
            lbSumDiscountVal.Text = disAmt.ToString(displayAmt);

            lbSumPayAmtVal.Text = (Convert.ToDouble(lbSumAmtVal.Text) - disAmt).ToString(displayAmt);
            lbSumRemainPointVal.Text = (Convert.ToDouble(lbPointVal.Text) - rdPoint).ToString("N0");

        }

        public void RefreshPageProductAndCoupon<T>(int gotoPage, int skipPage) where T : IRedeem
        {
            List<DataRow> lstDR = new List<DataRow>();
            int skip = skipPage * pageSize;
            int totalOfpage = gotoPage * pageSize;

            lstDR = dtCurrentPage.AsEnumerable().Skip(skip).Take(pageSize).ToList();
            frmCustRD.VisibleItemRedeem();

            for (int i = 1; i <= lstDR.Count; i++)
            {
                UCItemRedeemProductAndCouponCust ucItmRDPC = frmCustRD.pn_ItemCustRDProductAndCoupon.Controls.Find(pH_ucItemCustRDProduct + i, true).Cast<UCItemRedeemProductAndCouponCust>().FirstOrDefault();
                var itm = pn_ItemRD.Controls.Cast<T>().Where(w => Convert.ToInt32(w.SEQText) == skip + ucItmRDPC.SEQInt).FirstOrDefault();
                if (itm != null && ucItmRDPC != null)
                {
                    ucItmRDPC.Visible = true;
                    ucItmRDPC.lb_Seq.Text = itm.SEQText;
                    ucItmRDPC.QTY = itm.QTYText;
                }
            }

            //SaveValueCustProductAndCoupon();
            frmCustRD.lb_PageNo.Text = gotoPage.ToString();
        }

        public void SaveValueCustProductAndCoupon(string rdCode, string qty)
        {
            //GetRedeemProduct(lbSumPayAmtVal.Text, rdCode, qty);
            //for (int i = 1; i <= pageSize; i++)
            //{
            //    UCItemRedeemProductAndCouponCust ucItmRDP = frmCustRD.pn_ItemCustRDProductAndCoupon.Controls.Find(pH_ucItemCustRDProduct + i, true).Cast<UCItemRedeemProductAndCouponCust>().FirstOrDefault();
            //    if (ucItmRDP != null)
            //    {
            //        int val = Convert.ToInt32(ucItmRDP.QTY);
            //        SaveDataTable(ucItmRDP.SEQInt, ucItmRDP.RedeemCode, val);
            //    }
            //}
        }

        private void SaveDataTable(int seq, string rdCode,int qty)
        {
            //GetRedeemProduct(lbSumAmtVal.Text, rdCode, qty.ToString());

            //StoreResult res = fSale.fSaleProcess.CheckRedeemPoint_Free_CPN(lbSumAmtVal.Text, rdCode, qty.ToString());
            //if (res.response.next)
            //{
            //    dtCurrentPage = res.otherData;
            //}
            //var dr = dtCurrentPage.AsEnumerable().Where(w => Convert.ToInt32(w["SEQ"]) == seq).FirstOrDefault();
            //if (dr != null)
            //{
            //    dr["Values"] = val;
            //    dtCurrentPage.AcceptChanges();
            //}
        }

        #region BackUpOldCode
        //private void btn_Next_Click(object sender, EventArgs e)
        //{
        //    //List<DataRow> lstDR = new List<DataRow>();
        //    //int nextPage = Convert.ToInt32(lb_PageNo.Text);
        //    //nextPage++;
        //    //if (nextPage <= Convert.ToInt32(lb_PageTotal.Text))
        //    //{
        //    //    SaveOldValue();
        //    //    int skip = Convert.ToInt32(lb_PageNo.Text) * 6;
        //    //    int totalOfpage = nextPage * 6;

        //    //    if (totalOfpage <= dt.Rows.Count)
        //    //    {
        //    //        lstDR = dt.AsEnumerable().Skip(skip).Take(6).ToList();
        //    //    }
        //    //    else
        //    //    {
        //    //        lstDR = dt.AsEnumerable().Skip(skip).Take(totalOfpage - skip).ToList();
        //    //    }

        //    //    VisibleItemRedeem();
        //    //    for (int i = 1; i <= lstDR.Count; i++)
        //    //    {
        //    //        UCItemRedeemProductCust ucItmRDP = pn_ItemRDProduct.Controls.Find("ucItemRDProduct" + i, true).Cast<UCItemRedeemProductCust>().FirstOrDefault();
        //    //        var dr = dt.AsEnumerable().Where(w => Convert.ToInt32(w["SEQ"]) == skip + i).FirstOrDefault();
        //    //        if (dr != null)
        //    //        {
        //    //            ucItmRDP.Visible = true;
        //    //            ucItmRDP.ucTextBoxSmall1.Text = dr["Values"] + "";
        //    //        }
        //    //    }

        //    //    lb_PageNo.Text = nextPage.ToString();
        //    //}
        //}

        //private void btn_Previous_Click(object sender, EventArgs e)
        //{
        //    //List<DataRow> lstDR = new List<DataRow>();
        //    //int nextPage = Convert.ToInt32(lb_PageNo.Text);
        //    //nextPage--;
        //    //if (nextPage > 0)
        //    //{
        //    //    SaveOldValue();
        //    //    int skip = (nextPage - 1) * 6;
        //    //    int totalOfpage = nextPage * 6;

        //    //    if (totalOfpage <= dt.Rows.Count)
        //    //    {
        //    //        lstDR = dt.AsEnumerable().Skip(skip).Take(6).ToList();
        //    //    }
        //    //    else
        //    //    {
        //    //        lstDR = dt.AsEnumerable().Skip(skip).Take(totalOfpage - skip).ToList();
        //    //    }

        //    //    VisibleItemRedeem();
        //    //    for (int i = 1; i <= lstDR.Count; i++)
        //    //    {
        //    //        UCItemRedeemProductCust ucItmRDP = pn_ItemRDProduct.Controls.Find("ucItemRDProduct" + i, true).Cast<UCItemRedeemProductCust>().FirstOrDefault();
        //    //        var dr = dt.AsEnumerable().Where(w => Convert.ToInt32(w["SEQ"]) == skip + i).FirstOrDefault();
        //    //        if (dr != null)
        //    //        {
        //    //            ucItmRDP.Visible = true;
        //    //            ucItmRDP.ucTextBoxSmall1.Text = dr["Values"] + "";
        //    //        }
        //    //    }
        //    //    lb_PageNo.Text = nextPage.ToString();
        //    //}
        //}
        #endregion

        private void lb_HeaderRedeemPoint_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lb_Hearder.Text = lb_HeaderRedeemPoint.Text;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            CloseFormRedeemAndCustRedeem();
        }

        private void ucHeader1_LanguageClick(object sender, EventArgs e)
        {
            SetIgnoreLanguageNumber();
            frmCustRD.SetIgnoreLanguageNumber();

            string tempStr = lb_HeaderRedeemPoint.Text;
            lb_HeaderRedeemPoint.Text = AppMessage.getMessage(ProgramConfig.language, this.Name, "", lb_HeaderRedeemPoint.Text);
            if (String.IsNullOrEmpty(lb_HeaderRedeemPoint.Text.Trim()))
            {
                lb_HeaderRedeemPoint.Text = tempStr;
            }
        }

        public void SetIgnoreLanguageNumber()
        {
            //UCItemRedeemProduct rdPD = new UCItemRedeemProduct();

            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, pn_ItemRD);
            AppMessage.fillAllControlsFontIgnoreNumber(ProgramConfig.language, panel1);
            //AppMessage.fillControlsFont(ProgramConfig.language, 
            //                            panel1, 
            //                            new List<string>() { lbSumReceiptVal.Name, 
            //                                                 lbSumDiscountVal.Name, 
            //                                                 lbSumAmtVal.Name,
            //                                                 lbSumPointUseVal.Name,
            //                                                 lbRemainPointVal.Name});
            //AppMessage.fillControlsFont(ProgramConfig.language,
            //                            pn_ItemRD,
            //                            new List<string>() { rdPD.lb_Seq.Name,
            //                                                 rdPD.lbProduct_Discount.Name,
            //                                                 rdPD.lbProduct_SumCash.Name,
            //                                                 rdPD.lbProduct_SumPoint.Name,
            //                                                 rdPD.lbProduct_UseCash.Name,
            //                                                 rdPD.lbProduct_UsePoint.Name
            //                                               });
            //rdPD.Dispose();
        }

        private void RedeemSum()
        {
            StoreResult res = fSale.fSaleProcess.RedeemPoint_sum();
            if (res.response.next)
            {
                foreach (DataRow dr in res.otherData.Rows)
                {
                    if (dr["LTYPCODE"].ToString() == "LTYP")
                    {
                        fSale.fSaleProcess.savePaymentRedeemBalance(dr["LTYP"].ToString(), "0", "LTYP");
                    }

                    if (dr["LTYDCODE"].ToString() == "LTYD")
                    {
                        fSale.fSaleProcess.saveLTYD(res.otherData);
                               
                        // fSale.fSaleProcess.savePaymentRedeemBalance(dr["LTYD"].ToString(), "0", "LTYD");
                        //ProgramConfig.redeemLTYD = dr["LTYD"].ToString();
                    }
                }
                //ProgramConfig.totalValue = lbSumPayAmtVal.Text;
                CloseFormRedeemAndCustRedeem();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ucKeypad.ucTBS = null;
            OKClick();
        }

        private void OKClick()
        {
            try
            {
                if (_page == RedeemPage.Product)
                {
                    if (IsRedeemStepBack)
                    {
                        BeforeInitialRedeemProductCust();
                        InitialRedeemProductAndCouponCust<UCItemRedeemProduct>(dtProduct);
                    }

                    //EnterFromTextBoxSmallRedeemProduct(sender, e);
                    if (!ChkSkipProduct)
                    {
                        IsRedeemProduct = CheckGridIsRedeem<UCItemRedeemProduct>(); // For test
                    }

                    if (IsRedeemProduct || ChkSkipProduct)
                    {
                        if (IsRedeemStepBack)
                        {
                            DialogResult res = ShowFormIDCard();
                            if (res == System.Windows.Forms.DialogResult.Yes)
                            {
                                RedeemSum();
                            }
                            else if (res == System.Windows.Forms.DialogResult.Abort)
                            {
                                fSale.CatchNetWorkConnectionException(new NetworkConnectionException());
                            }
                            else if (res == System.Windows.Forms.DialogResult.Cancel)
                            {
                                fSale.ucTBScanBarcode.Focus();
                            }
                        }
                        else
                        {
                            if (IsRedeemProduct)
                                Update_PMS_REDEEM_POINT_Product();

                            if (!GetRedeemDiscount(lbSumAmtVal.Text, "", ""))
                            {
                                ChkSkipDiscount = true;
                                _page = RedeemPage.Discount;
                                OKClick();
                                return;
                            }
                        }
                    }
                    else
                    {
                        //"กรุณาป้อนจำนวนสิทธิ์ที่แลก"
                        frmNotify notify = new frmNotify(ResponseCode.Error
                            , ProgramConfig.message.get("frmRedeem", "ValidateRedeemQty").message
                            , ProgramConfig.message.get("frmRedeem", "ValidateRedeemQty").help);
                        notify.ShowDialog(this);
                        return;
                    }
                }
                else if (_page == RedeemPage.Discount)
                {
                    if (IsRedeemStepBack)
                    {
                        BeforeInitialRedeemDiscount();
                        InitialRedeemDiscount();
                    }

                    if (!ChkSkipDiscount)
                    {
                        IsRedeemDiscount = pn_ItemRD.Controls.Cast<UCItemRedeemDiscount>().Any(itm => itm.btnYesNo.Text == "Y");
                    }

                    if (IsRedeemDiscount || ChkSkipDiscount)
                    {
                        if (IsRedeemStepBack)
                        {
                            //DialogResult res = System.Windows.Forms.DialogResult.No;
                            DialogResult res = ShowFormIDCard();

                            if (res == System.Windows.Forms.DialogResult.Yes)
                            {
                                RedeemSum();
                            }
                            else if (res == System.Windows.Forms.DialogResult.Abort)
                            {
                                fSale.CatchNetWorkConnectionException(new NetworkConnectionException());
                            }
                            else if (res == System.Windows.Forms.DialogResult.Cancel)
                            {
                                fSale.ucTBScanBarcode.Focus();
                            }
                        }
                        else
                        {
                            if (!GetRedeemCash())
                            {
                                ChkSkipCash = true;
                                _page = RedeemPage.Cash;
                                OKClick();
                                return;
                            }
                        }
                    }
                    else
                    {
                        //"กรุณาป้อนจำนวนสิทธิ์ที่แลก"
                        frmNotify notify = new frmNotify(ResponseCode.Error
                            , ProgramConfig.message.get("frmRedeem", "ValidateRedeemQty").message
                            , ProgramConfig.message.get("frmRedeem", "ValidateRedeemQty").help);
                        notify.ShowDialog(this);
                        return;
                    }
                }
                else if (_page == RedeemPage.Cash)
                {
                    IsRedeemStepBack = true;

                    //if (dtCash.Rows.Count == 0)
                    //{
                    //    ShowFormIDCard();
                    //    return;
                    //}
                    if (!ChkSkipCash)
                    {
                        IsRedeemCash = CheckGridIsRedeem<UCItemRedeemCash>();
                    }

                    if (IsRedeemCash || ChkSkipCash)
                    {

                        if (IsRedeemProduct || IsRedeemDiscount || IsRedeemCash)
                        {
                            DialogResult dRes = ShowFormIDCard();
                            if (dRes == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (IsRedeemCash)
                                {
                                    foreach (UCItemRedeemCash item in pn_ItemRD.Controls)
                                    {
                                        fSale.fSaleProcess.Update_PMS_REDEEM_POINT_Cash(item.QTYText, item.DiscountTxt,
                                            item.SumPointTxt, item.RuleID, item.RedeemCode, item.lbCash_PointToRedeem.Text);
                                    }
                                }
                                RedeemSum();
                            }
                            else if (dRes == System.Windows.Forms.DialogResult.Abort)
                            {
                                fSale.CatchNetWorkConnectionException(new NetworkConnectionException());
                            }
                            else if (dRes == System.Windows.Forms.DialogResult.Cancel)
                            {
                                fSale.ucTBScanBarcode.Focus();
                            }
                        }
                        else
                        {
                            CloseFormRedeemAndCustRedeem();
                        }
                    }
                    else
                    {
                        //"กรุณาป้อนจำนวนสิทธิ์ที่แลก"
                        frmNotify notify = new frmNotify(ResponseCode.Error
                            , ProgramConfig.message.get("frmRedeem", "ValidateRedeemQty").message
                            , ProgramConfig.message.get("frmRedeem", "ValidateRedeemQty").help);
                        notify.ShowDialog(this);
                        return;
                    }
                }
                else if (_page == RedeemPage.Coupon)
                {
                    if (dtCoupon.Rows.Count > 0)
                    {
                        DialogResult dRes = ShowFormIDCard();
                        if (dRes == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                foreach (UCItemRedeemCoupon item in pn_ItemRD.Controls)
                                {
                                    fSale.process.Update_PMS_REDEEM_POINT_Coupon(item.QTYText, item.UsePointTxt, item.RuleID, item.RedeemCode);
                                }

                                StoreResult res = fSale.fSaleProcess.RedeemPoint_Coupon_Sum();
                                if (res.response.next)
                                {

                                    res = fSale.fSaleProcess.PrintRedeemPoint_Coupon();
                                    if (res.response.next)
                                    {
                                        DataTable dt1 = res.otherData;
                                        Hardware.printTermal(dt1);
                                        CloseFormRedeemAndCustRedeem();
                                        this.Close();
                                    }
                                    else
                                    {
                                        frmNotify notify = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                                        notify.ShowDialog(this);
                                        return;
                                    }
                                }
                                else
                                {
                                    frmNotify notify = new frmNotify(ResponseCode.Error, res.responseMessage, res.helpMessage);
                                    notify.ShowDialog(this);
                                    return;
                                }
                            }
                            catch (NetworkConnectionException net)
                            {
                                if (Program.control.RetryConnection(net.errorType))
                                {
                                    Utility.CheckRunningNumber();
                                    this.Dispose();
                                    this.Close();
                                }
                            }
                        }
                        else if (dRes == System.Windows.Forms.DialogResult.Abort)
                        {
                            if (Program.control.RetryConnection(NetworkErrorType.ConnectionTimeout))
                            {
                                this.Dispose();
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (NetworkConnectionException)
            {
                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucKeypad.ucTBS = null;
            CancelClick();
        }

        private void CancelClick()
        {
            bool chk = false;
            if (_page == RedeemPage.Product)
            {
                UpdateCancelRedeemProduct();
                if (IsRedeemStepBack)
                {
                    //if (IsRedeemProduct)
                    //{
                    //    _page = RedeemPage.Product;
                    //    btnOK.PerformClick();
                    //    return;
                    //}
                    //else
                    //{
                    CloseFormRedeemAndCustRedeem();
                    //}
                }
                else
                {
                    if (!GetRedeemDiscount(lbSumAmtVal.Text, "", ""))
                    //if (chk)
                    {
                        _page = RedeemPage.Discount;
                        CancelClick();
                        return;
                    }
                }
            }
            else if (_page == RedeemPage.Discount)
            {
                UpdateCancelRedeemDiscount();
                if (IsRedeemStepBack)
                {
                    //if (!GetRedeemProduct(ProgramConfig.totalValue, "", "0.00"))
                    //if (chk)
                    if (IsRedeemProduct)
                    {
                        _page = RedeemPage.Product;
                        OKClick();
                        return;
                    }
                    else
                    {
                        CloseFormRedeemAndCustRedeem();
                    }
                }
                else
                {
                    if (!GetRedeemCash())
                    //if (chk)
                    {
                        _page = RedeemPage.Cash;
                        CancelClick();
                        return;
                    }
                }
            }
            else if (_page == RedeemPage.Cash)
            {
                IsRedeemStepBack = true;

                if (dtCash != null && dtCash.Rows.Count > 0)
                {
                    fSale.fSaleProcess.Clear_PMS_REDEEM_POINT_Cash(dtCash.Rows[0]["ruleid"].ToString());
                }

                if (IsRedeemDiscount || IsRedeemProduct)
                {
                    if (IsRedeemDiscount)
                    {
                        _page = RedeemPage.Discount;
                        OKClick();
                        return;
                    }
                    else if (IsRedeemProduct)
                    {
                        _page = RedeemPage.Product;
                        OKClick();
                        return;
                    }
                }
                else
                {
                    CloseFormRedeemAndCustRedeem();
                }



                //if (!GetRedeemDiscount(lbSumPayAmtVal.Text, "", ""))
                //{
                //    _page = RedeemPage.Discount;
                //    btnCancel.PerformClick();
                //    return;
                //}
            }
            else if (_page == RedeemPage.Coupon)
            {
                CloseFormRedeemAndCustRedeem();
            }
        }

        private void UpdateCancelRedeemProduct()
        {
            if (fSale.fSaleProcess.DeleteTempRedeemFreePointCash().response.next)
            {
                fSale.fSaleProcess.UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL();
            }
        }

        private bool CheckGridIsRedeem<T>() where T : IRedeem
        {
            return pn_ItemRD.Controls.Cast<T>().Sum(itm => Convert.ToInt32(itm.QTYText)) > 0;
        }

        private void Update_PMS_REDEEM_POINT_Product()
        {
            foreach (var item in pn_ItemRD.Controls)
            {
                if (item is UCItemRedeemProduct)
                {
                    UCItemRedeemProduct ucItemRD = item as UCItemRedeemProduct;
                    StoreResult res = fSale.fSaleProcess.Update_PMS_REDEEM_POINT_Product(ucItemRD.QTYText, ucItemRD.DiscountTxt, ucItemRD.UsePointTxt, ucItemRD.RuleID, ucItemRD.RedeemCode);                 
                }
            }
        }

        private DialogResult ShowFormIDCard()
        {
            try
            {
                frmNotify dialog = new frmNotify(ResponseCode.Warning, ProgramConfig.message.get("frmRedeem", "ConfirmRedeem").message);
                var res = dialog.ShowDialog(this);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    if (Convert.ToDouble(lbSumDiscountVal.Text) > Convert.ToDouble(lbSumAmtVal.Text))
                    {
                        //"ตรวจสอบการแลกส่วยลดมากว่าราคาสินค้า" + Environment.NewLine + "กด Yes เพื่อยืนยันการแลกยอดซื้อสะสม" + Environment.NewLine + "กด No เพื่อตรวจสอบการแลกซื้อสะสม"
                        dialog = new frmNotify(ResponseCode.Warning, String.Format(ProgramConfig.message.get("frmRedeem", "CheckDiscount").message, Environment.NewLine));
                        res = dialog.ShowDialog(this);
                        if (res != System.Windows.Forms.DialogResult.Yes)
                        {
                            return DialogResult.No;
                        }
                    }
                    Profile chkRedeem = new Profile();
                    Profile chkpfKeyBoard = new Profile();
                    if (_page != RedeemPage.Coupon)
                    {
                        chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_VerifyID); //#298 //#41
                        chkpfKeyBoard = ProgramConfig.getProfile(FunctionID.Sale_Redeem_KeyBoard); //#299 #71
                    }
                    else
                    {
                        chkRedeem = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon_VerifyID); //#604 #73
                        chkpfKeyBoard = ProgramConfig.getProfile(FunctionID.Sale_Redeem_Coupon_KeyBoard); //#605 #74
                    }


                    if (chkRedeem.policy == PolicyStatus.Work)
                    {
                        //if (!Utility.CheckAuthPass(this, chkRedeem, "Redeem"))
                        //{
                        //    return System.Windows.Forms.DialogResult.No;
                        //}

                        frmConfirmIDCardPassport frm = new frmConfirmIDCardPassport(chkpfKeyBoard);
                        DialogResult dRes = frm.ShowDialog(this);
                        if (dRes == System.Windows.Forms.DialogResult.Abort)
                        {
                            return System.Windows.Forms.DialogResult.Abort;
                        }
                        if (dRes == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return System.Windows.Forms.DialogResult.Cancel;
                        }
                        return dRes;
                        //Program.control.ShowForm("frmConfirmIDCardPassport", this);
                    }
                    else
                    {
                        return System.Windows.Forms.DialogResult.Yes;
                    }
                }
                return System.Windows.Forms.DialogResult.No;
            }
            catch (NetworkConnectionException)
            {
                return System.Windows.Forms.DialogResult.Abort;
                //bool a = fSale.CatchNetWorkConnectionException(net);
            }
            
        }

        private void UpdateCancelRedeemDiscount()
        {
            //Delete temp_redeem_percent_discount
            if (fSale.fSaleProcess.Delete_Temp_Redeem_Percent_Discount().response.next)
            {
                fSale.fSaleProcess.Update_PMS_REDEEM_POINT_Percent_Discount();
            }
        }

        //private void frmRedeem_Disposed(object sender, EventArgs e)
        //{
        //    base.Dispose();
        //    this.Dispose();
        //}

        private void frmRedeem_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void lbMemberIDVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbMemberIDVal.Text = lbMemberIDVal.Text;
        }

        private void lbNameVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbNameVal.Text = lbNameVal.Text;
        }

        private void lbPointVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbPointVal.Text = lbPointVal.Text;
        }

        private void lbExpireDateVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbExpireDateVal.Text = lbExpireDateVal.Text;
        }

        private void lbSumPayAmtVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbSumPayAmtVal.Text = lbSumPayAmtVal.Text;
        }

        private void lbSumDiscountVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbSumDiscountVal.Text = lbSumDiscountVal.Text;
        }

        private void lbSumAmtVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbSumAmtVal.Text = lbSumAmtVal.Text;
        }

        private void lbSumRemainPointVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbSumRemainPointVal.Text = lbSumRemainPointVal.Text;
        }

        private void lbSumPointUseVal_TextChanged(object sender, EventArgs e)
        {
            frmCustRD.lbSumPointUseVal.Text = lbSumPointUseVal.Text;
        }

        public void SumBottomLine<T>(List<T> lstItem) where T : IRedeem
        {
            double _sumDiscount = 0;
            int _sumCntRedeem = 0;
            int _sumPoint = 0;

            foreach (T item in lstItem)
            {       
                if (typeof(T) == typeof(UCItemRedeemDiscount))
                {
                    UCItemRedeemDiscount itm = (UCItemRedeemDiscount)(object)item;
                    if (itm.btnYesNo.Text == "Y")
                    {
                        _sumCntRedeem++;
                        _sumPoint += Convert.ToInt32(item.UsePointTxt);
                        _sumDiscount += Convert.ToDouble(item.DiscountTxt);
                    }
                }
                else
                {
                    _sumCntRedeem += Convert.ToInt32(item.QTYText);
                    _sumPoint += Convert.ToInt32(Convert.ToDouble(item.SumPointTxt));
                    _sumDiscount += Convert.ToDouble(item.DiscountTxt);
                }
                
            }

            lbSumDiscountVal.Text = _sumDiscount.ToString(displayAmt);
            lbSumRDCount.Text = _sumCntRedeem.ToString("N0");
            lbSumPointUseVal.Text = _sumPoint.ToString("N0");

            lbSumPayAmtVal.Text = (Convert.ToDouble(lbSumAmtVal.Text) - _sumDiscount).ToString(displayAmt);
            lbSumRemainPointVal.Text = (Convert.ToDouble(Convert.ToDouble(lbPointVal.Text)) - _sumPoint).ToString("N0");

        }
    }
}
