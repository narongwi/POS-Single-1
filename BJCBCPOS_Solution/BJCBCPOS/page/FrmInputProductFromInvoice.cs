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
    public partial class frmInputProductFromInvoice : Form
    {
        private bool chkOpenDrawer = false;
        Point pnt = new Point();
        UCDropDownList currentUCDDL;
        public UCTextBoxWithIcon ucTBWI { get; set; }
        private ReturnProcess process = new ReturnProcess();
        public UCTextBoxSmall ucTBS { get; set; }


        public frmInputProductFromInvoice()
        {
            InitializeComponent();
        }

        public void DrawerStatus(string status)
        {
            //MessageBox.Show(status);
            chkOpenDrawer = true;
        }

        private void frmInputProductFromInvoice_Load(object sender, EventArgs e)
        {
            Utility.InitialTextBoxIcon(ucTxtReceiptCode, BJCBCPOS.Properties.Resources.icon_textbox_scan, UCTextBoxIconType.ScanAndDelete, IconType.Scan, "ຫຼືຕື່ມໃສ່ບັນຊີລາຍຊື່ສິນຄ້າ");
            Utility.InitialTextBoxIcon(ucTxtBarcode, BJCBCPOS.Properties.Resources.icon_textbox_scan, UCTextBoxIconType.ScanAndDelete, IconType.Scan, "ຫຼືຕື່ມໃສ່ບັນຊີລາຍຊື່ສິນຄ້າ");
            StoreResult result = null;
            panelSearchReceipt.BringToFront();
            Hardware.addDrawerListeners(DrawerStatus);
            Profile check = ProgramConfig.getProfile(FunctionID.Return_SelectReturnTypeMenu_ByReceipt);
            if (check.profile == ProfileStatus.NotAuthorize)
            {
                frmUserAuthorize auth = new frmUserAuthorize();
                DialogResult auth_res = auth.ShowDialog(this);
                if (auth_res != DialogResult.Yes)
                {
                    frmNotify dialog = new frmNotify(ResponseCode.Error, "No Authorize.");
                    dialog.ShowDialog(this);
                    return;
                }
            }

            result = process.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ReturnRef);
            string refNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
            ProgramConfig.returnRefNo = refNo;

            Profile openDay = ProgramConfig.getProfile(FunctionID.Return_CheckOpenDayofTillStatus);
            if (check.policy == PolicyStatus.Work)
            {
                StoreResult checkOpenDay = process.checkOpenDay(FunctionID.OpenDay_CheckOpenDayofTillStatus);
            }
            
            

            //DisplayContent อย่าลืม
            result = process.posDisplayContent();
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

        }

        private void ucDDInstrument_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList2();
            }
        }

        private List<BJCBCPOS.UCDropDownList.Dropdown> SetDataucDropDownList2()
        {
            //List<string> lstStr = new List<string>();
            //lstStr.Add("Yunnnnnnnb");
            //lstStr.Add("BBBBBB");
            //lstStr.Add("CCCCCC");
            //lstStr.Add("DDDDDD");

            List<BJCBCPOS.UCDropDownList.Dropdown> lstStr = new List<BJCBCPOS.UCDropDownList.Dropdown>();
            BJCBCPOS.UCDropDownList.Dropdown drItem = new UCDropDownList.Dropdown();
            drItem.DisplayText = "THB";
            drItem.ValueText = "THB";
            lstStr.Add(drItem);

            drItem.DisplayText = "USD";
            drItem.ValueText = "USD";
            lstStr.Add(drItem);

            return lstStr;
        }

        private void ucDDCause_UCDropDownListClick(object sender, EventArgs e)
        {
            if (sender is UCDropDownList)
            {
                var ucDDL = (UCDropDownList)sender;
                ucDDL.lstDDL = SetDataucDropDownList2();
            }
        }

        private void pn_receript_information_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReceive1_Click(object sender, EventArgs e)
        {

        }

        /*
        public void SearchFormTextBoxIcon(UCTextBoxWithIcon ucTBWI)
        {
            if (ucTBWI == ucTextBoxWithIcon1)
            {
                Form2 frm2 = new Form2(ucTBWI);

                // >>>>>>>>>>>>>>>>> Set BackGroundBrightness <<<<<<<<<<<<<<<<<<<
                Class1.SetBackGroundBrightness(panel1, pictureBox2, pictureBox3);
                Class1.SetFormAndArrow(pictureBox3, ucTextBoxWithIcon2, panel1, frm2);
                pictureBox3.Parent = pictureBox2;
                pictureBox3.BringToFront();
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>> <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                frm2.ShowDialog(this);
                pictureBox2.Visible = false;
            }
        }
         */
    }
}
