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
    public partial class UCFooterTran : UserControl
    {
        private string _functionId;
        private string _mainContent;
        private string _fullContent;
        private bool _IsStandAlone;
        private frmDisplayContent contentForm = new frmDisplayContent();

        #region Properties

        [Category("Custom Property")]
        [Description("Set stand alone flag")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStandAlone
        {
            get
            {
                return ProgramConfig.IsStandAloneMode;
            }
            set
            {
                ProgramConfig.IsStandAloneMode = value;
                CheckModeStandAlone();
                this.Refresh();
            }
        }

        

        public string functionId
        {
            get { return this._functionId; }
            set
            {
                this._functionId = value;
                update();
            }
        }

        public string mainContent
        {
            get { return this._mainContent; }
            set
            {
                this._mainContent = value;
                update();
            }
        }

        public string fullContent
        {
            get { return this._fullContent; }
            set
            {
                this._fullContent = value;
                update();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;

                richTextBox1.ForeColor = value;
            }
        }


        #endregion
        
        public UCFooterTran()
        {
            InitializeComponent();
            update();
        }

        private void CheckModeStandAlone()
        {
            if (ProgramConfig.IsStandAloneMode)
            {
                lbStatus.Text = "STAND ALONE";
                lbStatus.BackColor = Color.Red;
                //ProgramConfig.saleMode = SaleMode.Standalone;
            }
            else
            {
                lbStatus.Text = "ON SERVER";
                lbStatus.BackColor = Color.FromArgb(100, 192, 100);
                //ProgramConfig.saleMode = SaleMode.Server;
            }
        }

        private void update()
        {
            CheckModeStandAlone();
            if (!string.IsNullOrEmpty(this._fullContent))
            {
                contentForm.content = this._fullContent;
            }

            string data;
            richTextBox1.Text = "";

            if (!string.IsNullOrEmpty(this._mainContent))
            {
                string[] boldSplit = this._mainContent.Split(new string[] { "<B>" }, StringSplitOptions.None);
                string[] normalSplit;

                for (int i = 0; i < boldSplit.Length; i++)
                {
                    data = boldSplit[i];

                    if (data.Length > 0)
                    {
                        normalSplit = data.Split(new string[] { "<N>" }, StringSplitOptions.None);

                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                        richTextBox1.AppendText(normalSplit[0]);

                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                        for (int j = 1; j < normalSplit.Length; j++)
                        {
                            richTextBox1.AppendText(normalSplit[j]);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(this._functionId))
            {
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText(", FunctionID: ");

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                richTextBox1.AppendText(this._functionId);
            }

            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            // display full data
            contentForm.ShowDialog(this);
            contentForm.BringToFront();
        }

        private void UCFooterTran_Load(object sender, EventArgs e)
        {
            lbStatus.Font = new Font("Prompt", lbStatus.Font.Size, lbStatus.Font.Style, lbStatus.Font.Unit, lbStatus.Font.GdiCharSet);
        }

        private void lb_Status_FontChanged(object sender, EventArgs e)
        {
            lbStatus.Font = new Font("Prompt", lbStatus.Font.Size, lbStatus.Font.Style, lbStatus.Font.Unit, lbStatus.Font.GdiCharSet);
            lbFunction.Font = new Font("Prompt", lbFunction.Font.Size, lbFunction.Font.Style, lbFunction.Font.Unit, lbFunction.Font.GdiCharSet);
            richTextBox1.Font = new Font("Prompt", lbFunction.Font.Size, lbFunction.Font.Style, lbFunction.Font.Unit, lbFunction.Font.GdiCharSet);
        }
    }
}
