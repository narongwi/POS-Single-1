using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Process;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmCheckProduct : Form
    {
        private SaleProcess process = new SaleProcess();

        public frmCheckProduct()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFrm();
        }

        private void clearFrm()
        {
            //lb1.Text = "";
            //lb2.Text = "";
            //lb3.Text = "";
            //lb4.Text = "";
            //lbTxt1.Text = "";
            //lbTxt2.Text = "";
            //lbTxt3.Text = "";
            //lbTxt4.Text = "";
            panel_list_suggest.Controls.Clear();
            ucTBScanProduct.Text = "";
            ucTBScanProduct.Select();
            ucTBScanProduct.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ucTBScanProduct_EnterFromButton(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            checkProduct();
            frmLoading.closeLoading();
        }

        private void ucTBScanProduct_TextBoxKeydown(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            checkProduct();
            frmLoading.closeLoading();
        }

        private void checkProduct()
        {
            if (ucTBScanProduct.Text.Length < 13 && ucTBScanProduct.Text.Length != 0)
            {
                ucTBScanProduct.Text = ucTBScanProduct.Text.PadLeft(13, '0');
            }

            if (ucTBScanProduct.Text != "")
            {
                StoreResult result = process.getCheckProduct(ucTBScanProduct.Text);

                if (result.response == ResponseCode.Success)
                {
                    int count = 1;
                    panel_list_product.Controls.Clear();
                    if (result.otherData != null)
                    {
                        for (int i = 0; i < result.otherData.Rows.Count; i++)
                        {
                            string label = result.otherData.Rows[i]["Label"].ToString();
                            string value = result.otherData.Rows[i]["Value"].ToString();

                            UCListCheckProduct ucCheck = new UCListCheckProduct(count);
                            ucCheck.lbNo.Text = count.ToString();
                            ucCheck.label1.Text = label;
                            ucCheck.label2.Text = value;
                            panel_list_product.Controls.Add(ucCheck);
                            count++;
                        }
                    }

                    //lb1.Text = result.otherData.Rows[1]["Label"].ToString();
                    //lbTxt1.Text = result.otherData.Rows[0]["Value"].ToString() + " - " + result.otherData.Rows[1]["Value"].ToString();
                    //lb2.Text = result.otherData.Rows[2]["Label"].ToString();
                    //double dbPrice = double.Parse(result.otherData.Rows[2]["Value"].ToString());
                    //lbTxt2.Text = dbPrice.ToString(ProgramConfig.amountFormatString);
                    //lb3.Text = result.otherData.Rows[3]["Label"].ToString();
                    //lbTxt3.Text = result.otherData.Rows[3]["Value"].ToString();
                    //lb4.Text = result.otherData.Rows[4]["Label"].ToString();
                    //lbTxt4.Text = result.otherData.Rows[4]["Value"].ToString();

                    if (ProgramConfig.memberId == "")
                    {
                        ProgramConfig.memberId = "N/A";
                    }

                    int cnt = 1;
                    panel_list_suggest.Controls.Clear();
                    result = process.getPromotionProduct(ucTBScanProduct.Text, ProgramConfig.memberId);
                    if (result.otherData != null)
                    {
                        if (result.response == ResponseCode.Success)
                        {
                            if (result.otherData.Rows.Count > 1)
                            {
                                for (int i = 0; i < result.otherData.Rows.Count; i++)
                                {
                                    string desTxt = result.otherData.Rows[i]["DescriptionMsg"].ToString();
                                    string customerTxt = result.otherData.Rows[i]["Customer"].ToString();
                                    string memberTxt = result.otherData.Rows[i]["Member"].ToString();

                                    UCListSuggest ucSug = new UCListSuggest(cnt);
                                    ucSug.lbNo.Text = cnt.ToString();
                                    ucSug.label1.Text = desTxt;
                                    ucSug.label2.Text = customerTxt;
                                    ucSug.label3.Text = memberTxt;
                                    panel_list_suggest.Controls.Add(ucSug);
                                    cnt++;
                                }
                            }
                            
                        }
                    }

                    RefreshSuggest();
                    ucTBScanProduct.Text = "";
                    ucTBScanProduct.Select();
                    ucTBScanProduct.Focus();
                }
                else if (result.response == ResponseCode.Error)
                {
                    string responseMessage = ProgramConfig.message.get("frmCheckProduct", "ProductNotFound").message;
                    string helpMessage = ProgramConfig.message.get("frmCheckProduct", "ProductNotFound").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Error, responseMessage, helpMessage);

                    //frmNotify dialog = new frmNotify(ResponseCode.Error, "ไม่พบสินค้า.");
                    dialog.ShowDialog(this);
                    clearFrm();
                }
            }
            else
            {
                clearFrm();
            }
        }

        private void RefreshSuggest()
        {
            List<UCListCheckProduct> lstItemProduct = new List<UCListCheckProduct>();
            lstItemProduct = panel_list_product.Controls.Cast<UCListCheckProduct>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panel_list_product.Controls.Clear();
            int num1 = lstItemProduct.Count;

            foreach (UCListCheckProduct item in lstItemProduct)
            {
                if (num1 == 1)
                {
                    item.BackColor = Color.Gray;
                    item.label1.ForeColor = Color.White;
                }
                else if (num1 % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num1.ToString();
                panel_list_product.Controls.Add(item);
                num1--;
            }
            ScrollToBottom(panel_list_product);

            List<UCListSuggest> lstItemSell = new List<UCListSuggest>();
            lstItemSell = panel_list_suggest.Controls.Cast<UCListSuggest>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panel_list_suggest.Controls.Clear();
            int num = lstItemSell.Count;

            foreach (UCListSuggest item in lstItemSell)
            {
                if (num == 1)
                {
                    item.BackColor = Color.Gray;
                    item.label1.ForeColor = Color.White;
                }
                else if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num.ToString();
                panel_list_suggest.Controls.Add(item);
                num--;
            }
            ScrollToBottom(panel_list_suggest);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void frmCheckProduct_Load(object sender, EventArgs e)
        {
            try
            {
                Profile check = ProgramConfig.getProfile(FunctionID.Tool_CheckProduct);
                if (check.profile == ProfileStatus.NotAuthorize) //1
                {
                    //frmUserAuthorize auth_cashier = new frmUserAuthorize("CheckProduct", check.diffUserStatus);
                    //auth_cashier.function = FunctionID.NoFunctionID;
                    //DialogResult auth_res_cash = auth_cashier.ShowDialog(this);
                    //if (auth_res_cash != DialogResult.Yes)
                    //{
                    //    frmNotify dialog = new frmNotify(ResponseCode.Error, "No Authorize.");
                    //    dialog.ShowDialog(this);
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, new Profile() { diffUserStatus = check.diffUserStatus, functionId = FunctionID.NoFunctionID, policy = check.policy, profile = check.profile }, "CheckProduct"))
                    {
                        frmNotify dialog = new frmNotify(ResponseCode.Error, "No Authorize.");
                        dialog.ShowDialog(this);
                        return;
                    }
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

                ucTBScanProduct.Select();
                ucTBScanProduct.Focus();
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

        private void ucHeader1_ScannerClick(object sender, EventArgs e)
        {

        }

        private void ucHeader1_MainMenuClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            this.Dispose();
            frmLoading.closeLoading();
        }

        private void ucHeader1_CalculatorClick(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            Program.control.ShowForm("frmCalculator");
            frmLoading.closeLoading();
        }
    }
}
