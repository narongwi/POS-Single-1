using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class UCPCMan : UserControl
    {
        frmSaleProcess saleProcess;
        frmSale _frmSale;
        string _pcManID = "";

        public delegate void PCManConnectionLostHandler(NetworkConnectionException net);

        #region Event

        [Category("Action")]
        [Description("Occurs when the network lost.")]
        [Browsable(true)]
        public event PCManConnectionLostHandler PCManCatchNetWorkConnectionException;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler PCManButtonBackClick;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler PCManEnterFromButton;

        #endregion

        public UCPCMan()
        {
            InitializeComponent();
        }

        private void UCPCMan_Load(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            if (frm is frmSale)
            {
                _frmSale = (frmSale)frm;
                saleProcess = new frmSaleProcess(_frmSale);
            }

            CheckPCMan();
        }

        private void picBtnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (PCManButtonBackClick != null)
            {
                PCManButtonBackClick(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_pcManID != "")
            {
                saleProcess.SavePCMan(_pcManID);
                ProgramConfig.pcManID = _pcManID;
            }
            this.Visible = false;
        }

        private void ucTxt_PCMan_EnterFromButton(object sender, EventArgs e)
        {
            string pcManID = ucTxt_PCMan.InpTxt.Trim();
            StoreResult res = saleProcess.CheckPCMan(pcManID);
            if (res.response.next)
            {
                _pcManID = pcManID;
                lbNamePCMan.Text = "ชื่อ : " + res.otherData.Rows[0]["PcName"].ToString();
                lbNamePCMan.Visible = true;
                btnOk.Visible = true;
                if (PCManEnterFromButton != null)
                {
                    PCManEnterFromButton(this, e);
                }
            }
            ucTxt_PCMan.FocusTxt();
        }

        private void CheckPCMan()
        {
            if (String.IsNullOrEmpty(ProgramConfig.pcManID))
            {
                lbNamePCMan.Visible = false;
                btnOk.Visible = false;
                //btnDelete.Visible = false;
            }
            else
            {
                lbNamePCMan.Visible = true;
                btnOk.Visible = true;
                //btnDelete.Visible = true;
            }
        }

        private void UCPCMan_VisibleChanged(object sender, EventArgs e)
        {
            CheckPCMan();
        }

    }


}
