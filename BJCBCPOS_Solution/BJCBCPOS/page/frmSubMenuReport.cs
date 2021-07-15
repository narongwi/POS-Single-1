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
    public partial class frmSubMenuReport : Form
    {
        private bool IsPaint = false;
        private MenuProcess process = new MenuProcess();

        public frmSubMenuReport()
        {
            InitializeComponent();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDaySaleReport_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                Profile chkDaySale = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDaySale);
                if (chkDaySale.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("ReportDaySale", chkDaySale.diffUserStatus);
                    //auth.function = FunctionID.Report_CheckCurrentDaySale;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    this.Dispose();
                    //    frmLoading.closeLoading();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, chkDaySale, "ReportDaySale"))
                    {
                        this.Dispose();
                        frmLoading.closeLoading();
                        return;
                    }

                    Program.control.ShowForm("frmDaySaleReport");
                }
                else if (chkDaySale.profile == ProfileStatus.Authorize)
                {
                    Program.control.ShowForm("frmDaySaleReport");
                }
                Dispose();
                frmLoading.closeLoading();

                //else
                //{
                //    Program.control.ShowForm("frmDaySaleReport");
                //    Dispose();
                //}
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

        private void btnInvoiceReport_Click(object sender, EventArgs e)
        {
            try
            {
                frmLoading.showLoading();
                Profile chkInvReport = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt);
                if (chkInvReport.profile == ProfileStatus.NotAuthorize)
                {
                    //frmUserAuthorize auth = new frmUserAuthorize("ReportReceipt", chkInvReport.diffUserStatus);
                    //auth.function = FunctionID.Report_CheckCurrentDayReceipt;
                    //DialogResult auth_res = auth.ShowDialog(this);
                    //if (auth_res != DialogResult.Yes)
                    //{
                    //    this.Dispose();
                    //    frmLoading.closeLoading();
                    //    return;
                    //}

                    if (!Utility.CheckAuthPass(this, chkInvReport, "ReportReceipt"))
                    {
                        this.Dispose();
                        frmLoading.closeLoading();
                        return;
                    }

                    ProgramConfig.superUserId = string.Empty;
                    ProgramConfig.superUserName = string.Empty;
                    ProgramConfig.superPassword = string.Empty;
                    ProgramConfig.superUserAuthorizeResult = null;
                    Program.control.ShowForm("frmInvoiceReport");
                }
                else if (chkInvReport.profile == ProfileStatus.Authorize)
                {
                    Program.control.ShowForm("frmInvoiceReport");
                }
                Dispose();
                frmLoading.closeLoading();

                //else
                //{
                //    Program.control.ShowForm("frmInvoiceReport");
                //    Dispose(); 
                //}
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

        private void frmSubMenuReport_Load(object sender, EventArgs e)
        {
            try
            {
                StoreResult result = updateMainMenu();
                if (!result.response.next)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog();
                    this.Dispose();
                    return;
                }

                Profile chkSumReport = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDaySale);
                if (chkSumReport.policy == PolicyStatus.Work)
                {
                    btnDaySaleReport.Enabled = true;
                    btnDaySaleReport.BackgroundImage = BJCBCPOS.Properties.Resources.multi_enable;
                    btnDaySaleReport.ForeColor = Color.Black;

                }
                else if (chkSumReport.policy == PolicyStatus.Skip)
                {
                    btnDaySaleReport.Enabled = false;
                    btnDaySaleReport.BackgroundImage = BJCBCPOS.Properties.Resources.multi_disable;
                    btnDaySaleReport.ForeColor = Color.White;

                }

                Profile chkInvReport = ProgramConfig.getProfile(FunctionID.Report_CheckCurrentDayReceipt);
                if (chkInvReport.policy == PolicyStatus.Work)
                {
                    btnInvoiceReport.Enabled = true;
                    btnInvoiceReport.BackgroundImage = BJCBCPOS.Properties.Resources.multi_enable;
                    btnInvoiceReport.ForeColor = Color.Black;
                }
                else if (chkInvReport.policy == PolicyStatus.Skip)
                {
                    btnInvoiceReport.Enabled = false;
                    btnInvoiceReport.BackgroundImage = BJCBCPOS.Properties.Resources.multi_disable;
                    btnInvoiceReport.ForeColor = Color.White;

                }

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

        private StoreResult updateMainMenu()
        {
            // get menu from DB
            StoreResult result = process.generateMenu();
            if (result.response.next)
            {
                if (result.response == ResponseCode.Information)
                {
                    frmNotify dialog = new frmNotify(result);
                    dialog.ShowDialog();
                }

                if (result.otherData != null && result.otherData.Rows != null)
                {
                    foreach (DataRow dr in result.otherData.Rows)
                    {
                        Button currentBtn = null;
                        if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Report_CheckDaySale.formatValue)
                        {
                            currentBtn = btnDaySaleReport;
                        }
                        else if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Report_CheckDayReceipt.formatValue)
                        {
                            currentBtn = btnInvoiceReport;
                        }

                        if (currentBtn != null)
                        {
                            switch (dr["SubMenuSeq"] + "")
                            {
                                case "1":
                                    currentBtn.Location = new Point(20, 92);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Enabled = true;
                                    }
                                    else
                                    {
                                        currentBtn.Enabled = false;
                                    }
                                    break;
                                case "2":
                                    currentBtn.Location = new Point(299, 92);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Enabled = true;
                                    }
                                    else
                                    {
                                        currentBtn.Enabled = false;
                                    }
                                    break;
                            }
                            currentBtn.Text = dr["SubMenuName"] + "";
                        }
                    }
                }
            }

            return result;
        }
    }
}
