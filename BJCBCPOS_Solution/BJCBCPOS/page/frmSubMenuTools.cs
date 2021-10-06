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
    public partial class frmSubMenuTools : Form
    {
        public frmCancelCashOut frmCCO;
        public frmChangePassword frmCPW;
        public frmCheckProduct frmCPD;
        private bool IsPaint = false; 
        private MenuProcess process = new MenuProcess();

        private int[] x_location = { 0, 12, 270, 525, 783 };
        private int[] y_location = { 0, 214, 336, 456 };

        public frmSubMenuTools()
        {
            InitializeComponent();
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmSubMenuTools_Load(object sender, EventArgs e)
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
                if (result.otherData != null && result.otherData.Rows != null)
                {
                    foreach (DataRow dr in result.otherData.Rows)
                    {
                        Button currentBtn = null;
                        if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Tool_ChangePassword.formatValue)
                        {
                            currentBtn = button6;
                        }
                        else if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Tool_CheckProduct.formatValue)
                        {
                            currentBtn = button1;
                        }
                        else if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Tool_CancelCashOut.formatValue)
                        {
                            Profile chkPf = ProgramConfig.getProfile(FunctionID.CancelCashOut);
                            if (chkPf.policy == PolicyStatus.Work)
                            {
                                currentBtn = button7;
                            }                           
                        }
                        else if (dr["functionid"] + "" == FunctionID.Login_DisplayMainMenu_Tool_FullTax.formatValue)
                        {
                            currentBtn = button2;
                        }


                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;

                        if (currentBtn != null)
                        {
                            switch (dr["SubMenuSeq"] + "")
                            {
                                case "1":
                                    currentBtn.Location = new Point(19, 74);
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
                                    currentBtn.Location = new Point(215, 74);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                                case "3":
                                    currentBtn.Location = new Point(413, 74);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                                case "4":
                                    currentBtn.Location = new Point(19, 226);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                                case "5":
                                    currentBtn.Location = new Point(215, 226);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                                case "6":
                                    currentBtn.Location = new Point(413, 226);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                                case "7":
                                    currentBtn.Location = new Point(413, 226);
                                    if (dr["MenuStatus"] + "" == "2")
                                    {
                                        currentBtn.Visible = true;
                                    }
                                    else
                                    {
                                        currentBtn.Visible = false;
                                    }
                                    break;
                            }
                            currentBtn.Text = dr["SubMenuName"] + "";
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmLoading.showLoading();
            Program.control.ShowForm("frmCheckProduct");
            foreach (Form item in Application.OpenForms)
            {
                if (item is frmCheckProduct)
                {
                    frmCPD = (frmCheckProduct)item;
                    frmCPD.BringToFront();
                    break;
                }
            }
            frmLoading.closeLoading();
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ProgramConfig.IsStandAloneMode)
            {
                Utility.AlertMessage(ResponseCode.Information, ProgramConfig.message.get("frmSubMenuTools", "NotAllowChangePassword").message, ProgramConfig.message.get("frmSubMenuTools", "NotAllowChangePassword").help);
                return;
            }
            else
            {
                frmLoading.showLoading();
                Program.control.ShowForm("frmChangePassword");
                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmChangePassword)
                    {
                        frmCPW = (frmChangePassword)item;
                        frmCPW.BringToFront();
                        break;
                    }
                }
                frmLoading.closeLoading();
                this.Dispose();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Profile chkPf = ProgramConfig.getProfile(FunctionID.CancelCashOut);
            if (chkPf.profile == ProfileStatus.Authorize)
            {
                frmLoading.showLoading();
                Program.control.ShowForm("frmCancelCashOut");
                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmCancelCashOut)
                    {
                        frmCCO = (frmCancelCashOut)item;
                        frmCCO.BringToFront();
                        break;
                    }
                }
            }
            //else
            //{
            //    frmNotify notify = new frmNotify(ResponseCode.Error, ProgramConfig.message.get("frmSubMenuTools", "CancelCashOut").message
            //                                                       , ProgramConfig.message.get("frmSubMenuTools", "CancelCashOut").help);
            //    notify.ShowDialog(this);
            //}

            frmLoading.closeLoading();
            this.Dispose();
            
        }

        private void btnReceivePayment_Click(object sender, EventArgs e)
        {
            //Profile check = ProgramConfig.getProfile(FunctionID.ReceivePayment_MenuReceivePOD);
            ////if()
            //Program.control.ShowForm("frmSubMenu");
        }

        private void button8_Click(object sender,EventArgs e) {
            Profile chkPf = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_InputProduct_BillPayment);
            if(chkPf.profile == ProfileStatus.Authorize) {
                frmLoading.showLoading();
                Program.control.ShowForm("BJCBCPOS.Services.Forms.frmBigService");
            }
         
            frmLoading.closeLoading();
            this.Dispose();
        }
    }
}
