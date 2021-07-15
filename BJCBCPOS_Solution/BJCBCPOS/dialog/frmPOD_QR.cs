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
    public partial class frmPOD_QR : Form
    {
        SaleProcess process = new SaleProcess();
        private bool IsPaint = false;
        List<string> _qrcode = new List<string>();
        private Point defalutLocation = new Point();
        private Point keyboardLocation = new Point();
        frmPayment fPayment;

        public string edcAmt;

        string pmType;
        string pmNumber;
        string refID;
        string approveCode;
        string traceNo;
        string terminalID;
        string merchantID;
        string edcDate;       
        string invoiceNo;
        string qrCodeFull;
        string total;
        string pmCode;

        public frmPOD_QR()
        {
            InitializeComponent();
        }

        public frmPOD_QR(string total, string pmCode)
        {
            InitializeComponent();
            this.total = total;
            this.pmCode = pmCode;
        }

        private void btn_edit_credit_VisibleChanged(object sender, EventArgs e)
        {
            btnCancelChange.Visible = !btn_edit_credit.Visible;
            btnConfirmChange.Visible = !btn_edit_credit.Visible;
            ucTextBoxWithIcon1.Visible = !btn_edit_credit.Visible;
        }

        private void frmPOD_QR_Load(object sender, EventArgs e)
        {
            fPayment = (frmPayment)this.Owner;

            btn_edit_credit.Visible = false;
            btnCancelChange.Visible = false;
            btnConfirmChange.Visible = false;
            ucTextBoxWithIcon1.Visible = false;

            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (pnMain.Size.Width / 2);
                int y = (this.Size.Height / 2) - (pnMain.Size.Height / 2);
                pnMain.Location = new Point(x, y - 20);
                defalutLocation = pnMain.Location;
                keyboardLocation = new Point(pnMain.Location.X, -127);
                this.Location = this.Owner.Location;
            }
            else
            {
                this.Size = new System.Drawing.Size(1024, 768);
                int x = (this.Size.Width / 2) - (pnMain.Size.Width / 2);
                int y = (this.Size.Height / 2) - (pnMain.Size.Height / 2);
                pnMain.Location = new Point(x, y - 20);
                defalutLocation = pnMain.Location;
                keyboardLocation = new Point(pnMain.Location.X, -127);
                this.Location = new Point(0, 0);
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

        private void ucTBScanBarcode_TextBoxKeydown(object sender, EventArgs e)
        {
            //ucTBScanBarcode.Text
            _qrcode.Add(ucTBScanBarcode.InpTxt.Trim());
            if (_qrcode.Count == 10)
            {
                pmType = _qrcode[0];
                pmNumber = _qrcode[1];
                refID = _qrcode[2];
                approveCode = _qrcode[3];
                traceNo = _qrcode[4];
                terminalID = _qrcode[5];
                merchantID = _qrcode[6];
                edcDate = _qrcode[7];
                edcAmt = _qrcode[8];
                invoiceNo = _qrcode[9];

                lbPaymentTypeVal.Text = pmType;
                lbPaymentNumberVal.Text = pmNumber;
                lbRefIDVal.Text = refID;
                lbApproveCodeVal.Text = approveCode;
                lbTraceNoVal.Text = traceNo;
                lbTerminalIDVal.Text = terminalID;
                lbMerchantIDVal.Text = merchantID;
                lbEDCDateVal.Text = edcDate;
                lbEDCAmtVal.Text = edcAmt;
                lbInvoiceNoVal.Text = invoiceNo;
                qrCodeFull = String.Join("|", _qrcode);

                if (lbInvoiceNoVal.Text == "")
                {
                    btnConfirmChange.Visible = true;
                    btnCancelChange.Visible = true;
                    ucTextBoxWithIcon1.Visible = true;
                    ucTextBoxWithIcon1.FocusTxt();
                }
                else
                {
                    btn_edit_credit.Visible = true;
                }

                _qrcode = new List<string>();
            }
            ucTBScanBarcode.InpTxt = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _qrcode = new List<string>();
            lbPaymentTypeVal.Text = "";
            lbPaymentNumberVal.Text = "";
            lbRefIDVal.Text = "";
            lbApproveCodeVal.Text = "";
            lbTraceNoVal.Text = "";
            lbTerminalIDVal.Text = "";
            lbMerchantIDVal.Text = "";
            lbEDCDateVal.Text = "";
            lbEDCAmtVal.Text = "";
            lbInvoiceNoVal.Text = "";
            btn_edit_credit.Visible = false;
            btnConfirmChange.Visible = false;
            btnCancelChange.Visible = false;

            ucTextBoxWithIcon1.Text = "";
            ucTextBoxWithIcon1.Visible = false;
           
            ucTBScanBarcode.InpTxt = "";
            ucTBScanBarcode.FocusTxt();
        }

        private void btn_edit_credit_Click(object sender, EventArgs e)
        {
            btn_edit_credit.Visible = false;
            pnMain.Location = keyboardLocation;
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.BringToFront();
            this.ucKeyboard1.currentInput = ucTextBoxWithIcon1;
            ucTextBoxWithIcon1.FocusTxt();
        }

        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
            pnMain.Location = defalutLocation;
            btn_edit_credit.Visible = true;
        }

        private void btnConfirmChange_Click(object sender, EventArgs e)
        {
            SubmitInvoice();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (lbPaymentTypeVal.Text == "" && lbPaymentNumberVal.Text == "" && lbRefIDVal.Text == "" && lbApproveCodeVal.Text == "" && lbTraceNoVal.Text == "" && lbTerminalIDVal.Text == "" &&
            lbMerchantIDVal.Text == "" && lbEDCDateVal.Text == "" && lbEDCAmtVal.Text == "" && lbInvoiceNoVal.Text == "")
            {
                Utility.AlertMessage(ResponseCode.Error, "กรุณาสแกน Barcode");
                return;
            }

            if (!fPayment.CheckChangeStatus(ucTBScanBarcode, edcAmt, true))
            {
                return;
            }

            string chg = "";
            if (Convert.ToDouble(total) > Convert.ToDouble(edcAmt))
            {
                chg = "";
            }
            else
            {
                chg = (Convert.ToDouble(edcAmt) - Convert.ToDouble(total)).ToString(ProgramConfig.amountFormatString);
            }

            var res = process.savePaymentPOD(pmCode, pmNumber, edcAmt, chg, "", refID, approveCode, traceNo, terminalID, merchantID, edcDate, invoiceNo, qrCodeFull);
            if (res.response.next)
            {
                ProgramConfig.podRefID = refID;
                ProgramConfig.podQRCode = qrCodeFull;
                if (Convert.ToDouble(total) > Convert.ToDouble(edcAmt))
                {
                    DialogResult = System.Windows.Forms.DialogResult.Retry;
                }
                else
                {
                    DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
            }
            else
            {
                Utility.AlertMessage(res);
            }
        }

        private void frmPOD_QR_Shown(object sender, EventArgs e)
        {
            Utility.CropFromScreen(this, pictureBox1);
        }

        private void ucTextBoxWithIcon1_Enter(object sender, EventArgs e)
        {
            pnMain.Location = keyboardLocation;
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.BringToFront();
            this.ucKeyboard1.currentInput = ucTextBoxWithIcon1;
            this.ucKeyboard1.updateLanguage(new Language(1));
        }

        private void ucTextBoxWithIcon1_TextBoxLeave(object sender, EventArgs e)
        {
            if (!(this.ActiveControl is Button))
            {
                this.ucKeyboard1.Visible = false;
                pnMain.Location = defalutLocation;
            }   
        }

        private void ucKeyboard1_HideKeyboardClick(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
        }

        private void SubmitInvoice()
        {
            this.ucKeyboard1.Visible = false;
            pnMain.Location = defalutLocation;
            btn_edit_credit.Visible = true;
            lbInvoiceNoVal.Text = ucTextBoxWithIcon1.Text;
            invoiceNo = ucTextBoxWithIcon1.Text;
            ucTextBoxWithIcon1.Text = "";
        }

        private void ucTextBoxWithIcon1_TextBoxKeydown(object sender, EventArgs e)
        {
            SubmitInvoice();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
