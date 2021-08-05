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
using System.Reflection;
using System.Collections;

namespace BJCBCPOS
{
    public partial class frmSubMenuReturn : Form
    {
        private bool IsPaint = false;
        private MenuProcess process = new MenuProcess();

        public frmSubMenuReturn()
        {
            InitializeComponent();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDaySaleReport_Click(object sender, EventArgs e)
        {

            Profile chkDaySale = ProgramConfig.getProfile(FunctionID.Return_SelectReturnTypeMenu_ByReceipt);
            if (chkDaySale.profile == ProfileStatus.NotAuthorize)
            {
                frmUserAuthorize auth = new frmUserAuthorize("ReturnReceipt", chkDaySale.diffUserStatus);
                auth.function = FunctionID.Return_SelectReturnTypeMenu_ByReceipt;
                DialogResult auth_res = auth.ShowDialog(this);
                if (auth_res != DialogResult.Yes)
                {
                    this.Dispose();
                    return;
                }
                Program.control.ShowForm("frmReturnFromInvoice");
                Dispose();
            }
            else if (chkDaySale.profile == ProfileStatus.Authorize)
            {
                Program.control.ShowForm("frmReturnFromInvoice");
                Dispose();
            }
            //else
            //{
            //    Program.control.ShowForm("frmDaySaleReport");
            //    Dispose();
            //}
        }

        private void btnInvoiceReport_Click(object sender, EventArgs e)
        {
            Profile chkInvReport = ProgramConfig.getProfile(FunctionID.Return_SelectReturnTypeMenu_ByProduct);
            if (chkInvReport.profile == ProfileStatus.NotAuthorize)
            {
                frmUserAuthorize auth = new frmUserAuthorize("ReturnProduct", chkInvReport.diffUserStatus);
                auth.function = FunctionID.Return_SelectReturnTypeMenu_ByProduct;
                DialogResult auth_res = auth.ShowDialog(this);
                if (auth_res != DialogResult.Yes)
                {
                    this.Dispose();
                    return;
                }
                Dispose();
                Program.control.ShowForm("frmReturnFromScanProduct");
                
            }
            else if (chkInvReport.profile == ProfileStatus.Authorize)
            {
                Dispose();
                Program.control.ShowForm("frmReturnFromScanProduct");
                
            }
            //else
            //{
            //    Program.control.ShowForm("frmInvoiceReport");
            //    Dispose(); 
            //}

            
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

        private void frmSubMenuReturn_Load(object sender, EventArgs e)
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
                else
                {
                    this.Size = new Size(1024, 768);
                    int x = 512 - (panel1.Size.Width / 2);
                    int y = 384 - (panel1.Size.Height / 2);
                    panel1.Location = new Point(x, y);

                    this.Location = new Point(0, 0);
                }

                updateMainMenu();
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
        private void updateMainMenu()
        {
            // get menu from DB
            StoreResult result = process.generateMenu();
            if (result.response.next)
            {
                if (result.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog(this);
                }

                if (result.otherData != null && result.otherData.Rows != null)
                {
                    foreach (DataRow dr in result.otherData.Rows)
                    {
                        Button currentBtn = null;
                        if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Return_ByReceipt.formatValue)
                        {
                            currentBtn = btnReturnInvoice;
                        }
                        else if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Return_ByItem.formatValue)
                        {
                            currentBtn = btnReturnScanProduct;
                        }

                        if (currentBtn != null)
                        {
                            switch (dr["SubMenuSeq"] + "")
                            {
                                case "1":
                                    currentBtn.Location = new Point(20, 92);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                                case "2":
                                    currentBtn.Location = new Point(299, 92);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    //currentBtn.Visible = true; //Fix data
                                    break;
                            }
                            currentBtn.Text = dr["SubMenuName"] + "";
                        }
                    }
                }
            }
        }
    }
}
