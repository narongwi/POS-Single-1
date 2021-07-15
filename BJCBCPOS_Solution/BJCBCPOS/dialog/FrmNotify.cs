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
    public partial class frmNotify : Form
    {
        private delegate DialogResult InvokeDelegate(Form parent);

        private bool IsPaint = false;  
        private ResponseCode _response = ResponseCode.Success;
        private string _helpMessage = "";
        private string _message = "";
        private bool _isShowHelpMsg;
        private Image _picboxImage;
        private string _functionID = "";
        private Panel pn_Active;

        public bool IsShowHelpMsg
        {
            get { return _isShowHelpMsg; }
            set
            {
                _isShowHelpMsg = value;
                if (_isShowHelpMsg)
                {
                    lbMessage.Text = _helpMessage;
                    this.helpIcon.Image = Properties.Resources.icons8_info_enable;
                    this.pictureBox1.Image = Properties.Resources.icons8_info_126px;
                }
                else
                {
                    lbMessage.Text = _message;
                    this.helpIcon.Image = Properties.Resources.icons8_info_disable;
                    this.pictureBox1.Image = _picboxImage;
                }
            }
        }

        public string message
        {
            get { return _message; }
            set { _message = lbMessage.Text = value; }
        }

        public string functionID
        {
            get { return _functionID; }
            set {
                if (value.Trim() == "")
                {
                    lbFunction.Visible = false;
                }
                else
                {
                    lbFunction.Visible = true;
                    lbFunction.Text = "FunctionID : " + value; 
                }
                _functionID = value;
            }
        }

        public string helpMessage
        {
            get { return _helpMessage; }
            set
            {
                _helpMessage = value;
                if (string.IsNullOrEmpty(_helpMessage))
                {
                    helpIcon.Visible = false;
                    helpIcon.Enabled = false;
                }
                else 
                {
                    helpIcon.Visible = true;
                    helpIcon.Enabled = true;
                }
            }
        }

        public ResponseCode response
        {
            get { return _response; }
            set 
            {
                _response = value;
                if (_response == ResponseCode.Success)
                {
                    pictureBox1.Image = Properties.Resources.icons8_checked_126px_1;

                    btnOK.Visible = true;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
                else if (_response == ResponseCode.Information)
                {
                    pictureBox1.Image = Properties.Resources.icons8_info_126px;

                    btnOK.Visible = true;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
                else if (_response == ResponseCode.Warning)
                {
                    pictureBox1.Image = Properties.Resources.icons8_box_important_126px_3;

                    btnOK.Visible = false;
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                }
                else if (_response == ResponseCode.Error)
                {
                    pictureBox1.Image = Properties.Resources.icons8_cancel_126px_2;

                    btnOK.Visible = true;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
                //else if (_response == ResponseCode.Ignore)
                //{
                    
                //}
                else if (_response == ResponseCode.PasswordExpired)
                {
                    pictureBox1.Image = Properties.Resources.icons8_info_126px;

                    btnOK.Visible = true;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
                else if (_response == ResponseCode.Exit)
                {
                    pictureBox1.Image = Properties.Resources.icons8_cancel_126px_2;

                    btnOK.Visible = true;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
                else if (_response == ResponseCode.CloseDrawer)
                {
                    pictureBox1.Image = Properties.Resources.icons8_error_126px;
                    
                    btnOK.Visible = false;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
                _picboxImage = pictureBox1.Image;
            }
        }

        public frmNotify()
        {
            InitializeComponent();
            
            lbMessage.Text = "message";

            pictureBox1.Image = Properties.Resources.icons8_checked_126px_1;

            btnOK.Visible = true;
            btnYes.Visible = false;
            btnNo.Visible = false;
        }

        public frmNotify(string message, string msgBtnLeft, string msgBtnRight)
        {
            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = true;
            lbMessage2.Text = message;
            btnLeft.Text = msgBtnLeft;
            btnRight.Text = msgBtnRight;
        }

        public frmNotify(ResponseCode response, string message, string helpMessage = "")
        {
            InitializeComponent();
            this.message = message;
            this.response = response;
            this.helpMessage = helpMessage;           
        }

        public frmNotify(StoreResult res)
        {
            InitializeComponent();
            this.message = res.responseMessage;
            this.response = res.response;
            this.helpMessage = res.helpMessage;
            this.functionID = res.functionID;
        }

        public frmNotify(ProcessResult res)
        {
            InitializeComponent();
            this.message = res.responseMessage;
            this.response = res.response;
            this.helpMessage = res.helpMessage;
            this.functionID = res.functionID;
        }

        public frmNotify(ResponseCode response, AlertMessage alert)
        {
            InitializeComponent();
            this.message = alert.message;
            this.response = response;
            this.helpMessage = alert.help;
        }

        private void helpIcon_Click(object sender, EventArgs e)
        {
            IsShowHelpMsg = !IsShowHelpMsg;
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

        private void frmNotify_Load(object sender, EventArgs e)        
        {
            pn_Active = panel2.Visible ? panel2 : panel1;
            AppMessage.fillForm(ProgramConfig.language, this);
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (pn_Active.Size.Width / 2);
                int y = (this.Size.Height / 2) - (pn_Active.Size.Height / 2);
                pn_Active.Location = new Point(x, y);
                this.Location = this.Owner.Location;
            }
            else
            {
                this.Size = new System.Drawing.Size(1024, 768);
                int x = (this.Size.Width / 2) - (pn_Active.Size.Width / 2);
                int y = (this.Size.Height / 2) - (pn_Active.Size.Height / 2);
                pn_Active.Location = new Point(x, y);
                this.Location = new Point(0, 0);
            }

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (response == ResponseCode.Exit)
            {
                Program.control.ExitProgram();
            }
            DialogResult = DialogResult.OK;
        }

        //public DialogResult ShowDialog(Form parent)
        //{
        //    if (parent.InvokeRequired)
        //    {
        //        InvokeDelegate d = new InvokeDelegate(ShowDialog);
        //        object[] o = new object[] { parent };
        //        return (DialogResult)parent.Invoke(d, o);
        //    }
        //    else
        //    {
        //        return base.ShowDialog(parent);
        //    }
        //}

        public void connectionLostClose()
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void lbFunction_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbFunction);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
