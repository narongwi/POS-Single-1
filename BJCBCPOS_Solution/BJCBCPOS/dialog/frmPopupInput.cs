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
    public partial class frmPopupInput : Form
    {
        public Result_frmPopupInput result = new Result_frmPopupInput();
        string _header = "";
        string _placeHolderTxtBox1 = "";
        string _placeHolderTxtBox2 = "";
        Func<string, StoreResult> _action = null;
        Func<string, string, StoreResult> _action2 = null;
        private bool IsPaint = false;

        public frmPopupInput()
        {
            InitializeComponent();
        }

        public frmPopupInput(string header, string placeHolderTxtBox1, string placeHolderTxtBox2 = "")
        {
            InitializeComponent();
            _header = header;
            _placeHolderTxtBox1 = placeHolderTxtBox1;
            _placeHolderTxtBox2 = placeHolderTxtBox2;
        }

        public frmPopupInput(Func<string, StoreResult> action, string header, string placeHolderTxtBox1, string placeHolderTxtBox2)
        {
            InitializeComponent();
            _action = action;
            _header = header;
            _placeHolderTxtBox1 = placeHolderTxtBox1;
            _placeHolderTxtBox2 = placeHolderTxtBox2;
        }

        public frmPopupInput(Func<string, string, StoreResult> action, string header, string placeHolderTxtBox1, string placeHolderTxtBox2)
        {
            InitializeComponent();
            _action2 = action;
            _header = header;
            _placeHolderTxtBox1 = placeHolderTxtBox1;
            _placeHolderTxtBox2 = placeHolderTxtBox2;
        }



        private void frmPopupInput_Shown(object sender, EventArgs e)
        {
            lbHeader.Text = _header;
            ucTxtInput1.placeHolder = _placeHolderTxtBox1;
            ucTxtInput2.placeHolder = _placeHolderTxtBox2;
            ucTxtInput1.FocusTxt();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_action != null)
            {
                StoreResult res = _action(ucTxtInput1.Text);
                if (res.response.next)
                {
                    result.input1 = ucTxtInput1.Text;
                    result.input2 = ucTxtInput2.Text;
                    result.resultAction = res;
                    result.IsSuccess = true;
                    DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
                DialogResult = System.Windows.Forms.DialogResult.No;
            }
            else if (_action2 != null)
            {
                StoreResult res = _action2(ucTxtInput1.Text, ucTxtInput2.Text);
                if (res.response.next)
                {
                    result.input1 = ucTxtInput1.Text;
                    result.input2 = ucTxtInput2.Text;
                    result.resultAction = res;
                    result.IsSuccess = true;
                    DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
                DialogResult = System.Windows.Forms.DialogResult.No;
            }
            else
            {
                result.input1 = ucTxtInput1.Text;
                result.input2 = ucTxtInput2.Text;
                result.IsSuccess = true;
                DialogResult = System.Windows.Forms.DialogResult.Yes;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            result.IsSuccess = false;
            DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void ucTxtInput1_TextBoxKeydown(object sender, EventArgs e)
        {
            ucTxtInput2.FocusTxt();
        }

        private void ucTxtInput2_Enter(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.currentInput = ucTxtInput2;
        }

        private void ucTxtInput2_TextBoxKeydown(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        private void ucTxtInput1_EnterFromButton(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.currentInput = ucTxtInput1;
        }
     
    }
}
