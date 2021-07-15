using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmCustRedeem : Form
    {
        frmRedeem fRedeem = null;

        public frmCustRedeem()
        {
            InitializeComponent();
        }

        private void frmCustRedeem_Load(object sender, EventArgs e)
        {
            //this.Disposed += new EventHandler(frmCustRedeem_Disposed);
            fRedeem = this.Owner as frmRedeem;
            if (Screen.AllScreens.Length == 2)
            {
                Point screen_location = Screen.AllScreens[1].WorkingArea.Location;
                this.Location = new Point(screen_location.X, screen_location.Y);
            }
        }

        //public void ClearForm()
        //{
        //    this.pn_ItemCustRDCash.Controls.Clear();
        //    this.pn_ItemCustRDDiscount.Controls.Clear();
        //    this.pn_ItemCustRDProductAndCoupon.Controls.Clear();
        //    this.panel5.Controls.Clear();
        //    this.panel1.Controls.Clear();
        //    this.panel2.Controls.Clear();
        //    fRedeem = null;
        //    this.Controls.Clear();
        //    this.Close();
        //    this.Dispose();
        //}

        private void btn_Next_Click(object sender, EventArgs e)
        {
            List<DataRow> lstDR = new List<DataRow>();
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage++;
            if (nextPage <= Convert.ToInt32(lb_PageTotal.Text))
            {
                int skip = nextPage - 1;
                //fRedeem.SaveValueCustProductAndCoupon(); //Save Old Value Page
                if (fRedeem._page == RedeemPage.Product)
                {
                    fRedeem.RefreshPageProductAndCoupon<UCItemRedeemProduct>(nextPage, skip);
                }
                else if (fRedeem._page == RedeemPage.Coupon)
                {
                    fRedeem.RefreshPageProductAndCoupon<UCItemRedeemCoupon>(nextPage, skip);
                }
            }
        }

        public void VisibleItemRedeem()
        {
            ucItemCustRDProduct1.Visible = false;
            ucItemCustRDProduct2.Visible = false;
            ucItemCustRDProduct3.Visible = false;
            ucItemCustRDProduct4.Visible = false;
            ucItemCustRDProduct5.Visible = false;
            ucItemCustRDProduct6.Visible = false;
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            List<DataRow> lstDR = new List<DataRow>();
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage--;
            if (nextPage > 0)
            {
                int skip = nextPage - 1;
                //fRedeem.SaveValueCustProductAndCoupon(); //Save Old Value Page
                if (fRedeem._page == RedeemPage.Product)
                {
                    fRedeem.RefreshPageProductAndCoupon<UCItemRedeemProduct>(nextPage, skip);
                }
                else if (fRedeem._page == RedeemPage.Coupon)
                {
                    fRedeem.RefreshPageProductAndCoupon<UCItemRedeemCoupon>(nextPage, skip);
                }
            }
        }

        private void ucItemCustRDProduct_ButtonMinusClick(object sender, EventArgs e)
        {
            UCItemRedeemProductAndCouponCust ucItem = (UCItemRedeemProductAndCouponCust)sender;

            if (fRedeem._page == RedeemPage.Product)
            {
                ucItemCustBaseQTYClick<UCItemRedeemProductAndCouponCust, UCItemRedeemProduct>(ucItem, ucItem.lb_QTY.Text, ucItem.SEQInt, ucItem.RedeemCode);
                fRedeem.EnterRedeemProduct<UCItemRedeemProduct>(Convert.ToInt32(ucItem.lb_QTY.Text), Convert.ToInt32(ucItem.lbLimit.Text), ucItem.SEQInt, ucItem.RedeemCode);
            }
            else if (fRedeem._page == RedeemPage.Coupon)
            {
                ucItemCustBaseQTYClick<UCItemRedeemProductAndCouponCust, UCItemRedeemCoupon>(ucItem, ucItem.lb_QTY.Text, ucItem.SEQInt, ucItem.RedeemCode);
                fRedeem.EnterRedeemProduct<UCItemRedeemCoupon>(Convert.ToInt32(ucItem.lb_QTY.Text), Convert.ToInt32(ucItem.lbLimit.Text), ucItem.SEQInt, ucItem.RedeemCode);
            }
        }

        private void ucItemCustRDProduct_ButtonPlusClick(object sender, EventArgs e)
        {
            UCItemRedeemProductAndCouponCust ucItem = (UCItemRedeemProductAndCouponCust)sender;
            if (fRedeem._page == RedeemPage.Product)
            {
                ucItemCustBaseQTYClick<UCItemRedeemProductAndCouponCust, UCItemRedeemProduct>(ucItem, ucItem.lb_QTY.Text, ucItem.SEQInt, ucItem.RedeemCode);
                fRedeem.EnterRedeemProduct<UCItemRedeemProduct>(Convert.ToInt32(ucItem.lb_QTY.Text), Convert.ToInt32(ucItem.lbLimit.Text), ucItem.SEQInt, ucItem.RedeemCode);
            }
            else if (fRedeem._page == RedeemPage.Coupon)
            {
                ucItemCustBaseQTYClick<UCItemRedeemProductAndCouponCust, UCItemRedeemCoupon>(ucItem, ucItem.lb_QTY.Text, ucItem.SEQInt, ucItem.RedeemCode);
                fRedeem.EnterRedeemProduct<UCItemRedeemCoupon>(Convert.ToInt32(ucItem.lb_QTY.Text), Convert.ToInt32(ucItem.lbLimit.Text), ucItem.SEQInt, ucItem.RedeemCode);
            }  
        }

        public void ucItemCustBaseQTYClick<T, T2>(T ucItem, string qty, int seq, string rdCode) where T : UserControl 
                                                                        where T2 : UserControl, IRedeem
        {
            Utility.SetItemCashierCust<T2>(fRedeem.pn_ItemRD, seq.ToString(), qty);
            //fRedeem.SaveValueCustProductAndCoupon(rdCode, qty);
            //int skipPage = Convert.ToInt32(lb_PageNo.Text) - 1 <= 0 ? 0 : Convert.ToInt32(lb_PageNo.Text) - 1
            //string seq = ((Convert.ToInt32(lb_PageNo.Text) - 1) * 6) + Convert.ToInt32(ucItem.Name.Substring(ucItem.Name.Length - 1)) + "";
        }

        public void SetIgnoreLanguageNumber()
        {
            //AppMessage.fillControlsFont(ProgramConfig.language, pn_RDCash, new List<string>());
            //AppMessage.fillControlsFont(ProgramConfig.language, pn_RDDiscount, new List<string>());
            AppMessage.fillControlsFont(ProgramConfig.language, 
                                        pn_RDProductAndCoupon, 
                                        new List<string>()                                                                                         
                                        { ucItemCustRDProduct1.lbLimit.Name, ucItemCustRDProduct1.lb_Seq.Name, ucItemCustRDProduct1.lb_QTY.Name });
        }

    }
}
