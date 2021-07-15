using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS
{
    public partial class UCTextBoxSmall : UserControl
    {
        public UCTextBoxSmall()
        {
            InitializeComponent();
        }

        [Category("Custom Property")]
        [Description("Set when textbox edited focus.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTextEdit { get; set; }

        [Category("Custom Property")]
        [Description("Set when textbox lost focus.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTextChange { get; set; }

        public TextBox TextBox
        {
            get
            {
                return textBox1;
            }
            set
            {
                textBox1 = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return textBox1.Text.Trim() == "" ? "0" : textBox1.Text.Trim();
            }
            set
            {
                int num = 0;
                Int32.TryParse(value, out num);
                textBox1.Text = num.ToString();
            }
        }

        [Category("Action")]
        [Description("Occurs when enter from button.")]
        [Browsable(true)]
        public event EventHandler EnterFromButton;

        [Category("Action")]
        [Description("Occurs when lost focus from textbox.")]
        [Browsable(true)]
        public event EventHandler LostFocusTextBox;

        [Category("Action")]
        [Description("Occurs when textbox text change.")]
        [Browsable(true)]
        public event EventHandler TextBoxTextChange;


        public void Enter_FromButton(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
                EnterFromButton(this, e);
        }

        private void UCTextBoxSmall_Load(object sender, EventArgs e)
        {
            textBox1.LostFocus += textBox1_LostFocus;
            textBox1.GotFocus += textBox1_GotFocus;
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.txtSmall_disable;
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.txtSmall_enable;
            if (this.FindForm() != null)
            {
                var frm = this.FindForm().Controls.Find("ucKeypad", true).FirstOrDefault() as UCKeypad;
                if (frm != null)
                {
                    IsTextEdit = false;
                    frm.ucTBS = this;
                }
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                if (this.FindForm().ActiveControl is UCKeypad)
                {
                    this.IsTextChange = true;
                    IsTextEdit = true;
                }
                else
                {
                    if (LostFocusTextBox != null && (IsTextEdit || IsTextChange))
                    {
                        LostFocusTextBox(sender, e);
                    }

                    this.BackgroundImage = Properties.Resources.txtSmall_disable;
                    this.IsTextChange = false;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxTextChange != null)
            {
                TextBoxTextChange(sender, e);
            }

            IsTextEdit = true;
            int num = 0;
            string txt = textBox1.Text.Trim().Replace(",", "");
            if (!int.TryParse(txt, out num))
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = String.Format("{0:N0}", Convert.ToInt32(txt)).Replace(",", "");
            }
        }
    }
}
