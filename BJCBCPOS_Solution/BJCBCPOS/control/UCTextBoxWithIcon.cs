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
    public partial class UCTextBoxWithIcon : UserControl
    {
        public UCTextBoxWithIcon currentUC = null;
        public UCTextBoxIconType _textBoxItonType = UCTextBoxIconType.None;

        public UCTextBoxWithIcon()
        {
            InitializeComponent();
        }

        #region Properties
        [Category("Custom Property")]
        [Description("Set validate amount empty")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsValidateTextEmpty { get; set; }

        [Category("Custom Property")]
        [Description("Set validate amount empty")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsValidateNumberZero { get; set; }

        [Category("Custom Property")]
        [Description("Set amount dot replace zero")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsAmount { get; set; }

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsLarge { get; set; }

        [Category("Custom Property")]
        [Description("Set text align.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public HorizontalAlignment TextBoxAlign
        {
            get { return textBox1.TextAlign; }
            set { textBox1.TextAlign = value; }
        }

        [Category("Custom Property")]
        [Description("Set text placeholder.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string placeHolder
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        [Category("Custom Property")]
        [Description("Set text Readonly.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool Readonly
        {
            get
            {
                return textBox1.ReadOnly;
            }
            set
            {
                textBox1.ReadOnly = value;
            }
        }

        [Category("Custom Property")]
        [Description("Set text Readonly.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShortcutsEnabled
        {
            get
            {
                return textBox1.ShortcutsEnabled;
            }
            set
            {
                textBox1.ShortcutsEnabled = value;
            }
        }

        [Category("Custom Property")]
        [Description("Set input max length")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int MaxLength
        {
            get
            {
                return textBox1.MaxLength;
            }
            set
            {
                textBox1.MaxLength = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value.ToString();
                this.IsTextChange = true;
                if (this.FindForm() != null)
                {
                    var frm = this.FindForm().Controls.Find("ucKeypad", true).FirstOrDefault() as UCKeypad;
                    if (frm != null)
                    {
                        frm.ucTBWI = this;
                    }
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InpTxt
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value.ToString();
                this.IsTextChange = true;
                if (this.FindForm() != null)
                {
                    var frm = this.FindForm().Controls.Find("ucKeypad", true).FirstOrDefault() as UCKeypad;
                    if (frm != null)
                    {
                        frm.ucTBWI = this;
                    }
                }
            }
        }

        public bool PasswordChar
        {
            get
            {
                return textBox1.UseSystemPasswordChar;
            }
            set
            {
                textBox1.UseSystemPasswordChar = value;
            }
        }

        [Browsable(false)]
        public bool EnabledUC
        {
            get
            {
                return this.Enabled;
            }
            set
            {
                if (value)
                {                    
                    this.Enabled = true;
                    if (this.textBox1.Text.Length == 0)
                    {
                        label1.Visible = true;
                        panel2.Visible = true;
                    }
                }
                else
                {
                    label1.Visible = false;
                    this.Enabled = false;
                    panel2.Visible = false;
                }
            }
        }
        
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTextChange { get; set; }

        [Browsable(false)]
        public bool SetSelection
        {
            set
            {
                if (value)
                {
                    textBox1.Focus();
                    this.textBox1.SelectionStart = 0;
                    this.textBox1.SelectionLength = textBox1.Text.Length;                   
                }
            }
        }

        [Category("Custom Property")]
        [Description("Set format number.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsSetFormat { get; set; }

        [Category("Custom Property")]
        [Description("Set format number.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsNumber { get; set; }

        [Category("Custom Property")]
        [Description("Set focus when textbox disable.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FocusWithDisable
        {
            set
            {
                FocusTxt();
            }
        }

        [Category("Custom Property")]
        [Description("Set focus when textbox disable.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UCTextBoxIconType TextBoxIconType
        {
            get
            {
                if (_textBoxItonType != UCTextBoxIconType.None)
                {
                    return _textBoxItonType;
                }
                else
                {
                    return UCTextBoxIconType.NoneAndDelete;
                }
            }

            set
            {
                _textBoxItonType = value;
                if (_textBoxItonType == UCTextBoxIconType.SearchAndDelete)
                {
                    InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_search, UCTextBoxIconType.SearchAndDelete, IconType.Search, "");
                }
                else if (_textBoxItonType == UCTextBoxIconType.ScanAndDelete)
                {
                    InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_scan, UCTextBoxIconType.ScanAndDelete, IconType.Scan, "");
                }
                else
                {
                    InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_none, UCTextBoxIconType.NoneAndDelete, IconType.None, "");
                }
            }
        }

        [Category("Custom Property")]
        [Description("Set sequence textbox.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SeqTextBox { get; set; }

        [Category("Custom Property")]
        [Description("Set sequence textbox.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaymentStepDetail_StepID StepID { get; set; }

        [Category("Custom Property")]
        [Description("Set sequence textbox.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DataType { get; set; }

        [Category("Custom Property")]
        [Description("Set sequence textbox.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PaymentStepGroupID { get; set; }

        [Category("Custom Property")]
        [Description("Set sequence textbox.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InputType { get; set; }

        [Category("Custom Property")]
        [Description("Set Keyboard for scan")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsKeyBoardForScan { get; set; }

        #endregion

        #region EventHandler

        [Category("Action")]
        [Description("Occurs when the icon is clicked.")]
        [Browsable(true)]
        public event EventHandler IconClick;

        [Category("Action")]
        [Description("Occurs when keydown.")]
        [Browsable(true)]
        public event EventHandler TextBoxKeydown;

        [Category("Action")]
        [Description("Occurs when enter from button. (send text)")]
        [Browsable(true)]
        public event EventHandler EnterFromButton;

        //[Category("Action")]
        //[Description("Occurs when enter from button. (send object)")]
        //[Browsable(true)]
        //public event EventHandler EnterFromButton2;

        [Category("Action")]
        [Description("Occurs when textbox leave.")]
        [Browsable(true)]
        public event EventHandler TextBoxLeave;

        [Category("Action")]
        [Description("Occurs when textbox focus.")]
        [Browsable(true)]
        public event EventHandler TextBoxFocus;

        [Category("Action")]
        [Description("Occurs when textbox text change.")]
        [Browsable(true)]
        public event EventHandler TextBoxTextChange;

        #endregion

        private void UCTextBoxWithIcon_Load(object sender, EventArgs e)
        {
            textBox1.LostFocus += textBox1_LostFocus;
            textBox1.GotFocus += textBox1_GotFocus;
            InitialTextBoxIcon(BJCBCPOS.Properties.Resources.icon_textbox_none, UCTextBoxIconType.NoneAndDelete, IconType.None, "");
        }

        public void Enter_FromButton(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                if (IsValidateTextEmpty)
                {
                    if (this.textBox1.Text.Trim() == "")
                    {
                        string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").message;
                        string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowNull").help;
                        Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);
                        return;
                    }
                    
                }

                if (IsValidateNumberZero)
                {
                    double num = 0;
                    Double.TryParse(this.textBox1.Text.Trim(), out num);
                    if (num == 0)
                    {
                        string responseMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").message;
                        string helpMessage = ProgramConfig.message.get("frmSale", "NotAllowQty0").help;
                        Utility.AlertMessage(ResponseCode.Error, responseMessage, helpMessage);
                        return;
                    }
                }

                EnterFromButton(this, e);
                return;
            }
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            FocusTxt();
        }

        public void FocusTxt()
        {
            if (IsLarge)
            {
                this.BackgroundImage = Properties.Resources.textboxLarge_enable;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.txtboxWIC_enable;
            }

            if (textBox1.TextAlign != HorizontalAlignment.Right)
            {
                panel2.Visible = true;
            }

            if (this.FindForm() != null)
            {
                var keypad = this.FindForm().Controls.Find("ucKeypad", true).FirstOrDefault() as UCKeypad;
                if (keypad != null)
                {
                    keypad.ucTBWI = this;
                }
                else
                {
                    //TO DO Change to ucTxt.ucTBS = this;
                    Form owner = this.FindForm().Owner;
                    if (owner != null)
                    {
                        keypad = owner.Controls.Find("ucKeypad", true).FirstOrDefault() as UCKeypad;
                        if (keypad != null)
                        {
                            keypad.ucTBWI = this;
                        }
                    }
                }

                if (IsKeyBoardForScan)
                {
                    var keyboard = this.FindForm().Controls.Find("ucKeyboard1", true).FirstOrDefault() as UCKeyboard;
                    if (keyboard != null)
                    {
                        keyboard.updateLanguage(new Language(1));
                    }
                    KeyboardApi keyboardLang = new KeyboardApi(new System.Globalization.CultureInfo("en-US"));
                }

                if (!(this.FindForm().ActiveControl is UCKeyboard) && !(this.FindForm().ActiveControl is UCKeypad))
                {
                    if (TextBoxFocus != null)
                    {
                        TextBoxFocus(this, null);
                    }
                }
            }

            textBox1.Focus();





        }


        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                if (this.EnabledUC)
                {
                    label1.Visible = true;
                }
            }

            if (IsLarge)
            {
                this.BackgroundImage = Properties.Resources.textboxLarge_disable;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.txtboxWIC_disable;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.FindForm().ActiveControl is UCKeypad)
            {
                this.IsTextChange = true;
            }
            else if (this.ActiveControl != null)
            {
                if (this.ActiveControl.Name.ToUpper() == "ICON")
                {
                    if (IsLarge)
                    {
                        this.BackgroundImage = Properties.Resources.textboxLarge_disable;
                    }
                    else
                    {
                        this.BackgroundImage = Properties.Resources.txtboxWIC_disable;
                    }
                }
            }
            else
            {
                // check in split panel
                bool found = false;
                ContainerControl[] container = this.FindForm().Controls.OfType<ContainerControl>().ToArray();
                foreach (ContainerControl item in container)
                {
                    if (item.ActiveControl is UCKeypad)
                    {
                        this.IsTextChange = true;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (IsLarge)
                    {
                        this.BackgroundImage = Properties.Resources.textboxLarge_disable;
                    }
                    else
                    {
                        this.BackgroundImage = Properties.Resources.txtboxWIC_disable;
                    }

                    panel2.Visible = false;
                    this.IsTextChange = false;
                }

                if (!(this.FindForm().ActiveControl is UCKeyboard))
                {
                    if (TextBoxLeave != null)
                    {
                        TextBoxLeave(this, e);
                    }
                }


                //if (currentUC != null)
                //{
                //    currentUC.BackgroundImage = Properties.Resources.txtboxWIC_enable;
                //    currentUC.Focus();
                //}
            }
        }

        private void iCon_Click(object sender, EventArgs e)
        {
            UCTextBoxIconType txtType = (UCTextBoxIconType)this.Tag;
            if (txtType == UCTextBoxIconType.NoneAndDelete)
            {
                this.textBox1.Text = "";
                this.textBox1.Focus();
                InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_none, IconType.None);

                if (IconClick != null)
                {
                    IconClick(this, e);
                }
            }
            else if (txtType == UCTextBoxIconType.SearchAndDelete)
            {
                if ((IconType)this.iCon.Tag == IconType.Search)
                {
                    if (IconClick != null)
                    {
                        IconClick(this, e);
                    }
                }
                else if ((IconType)this.iCon.Tag == IconType.Delete)
                {
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_search, IconType.Search);
                }
            }
            else if (txtType == UCTextBoxIconType.ScanAndDelete)
            {
                if ((IconType)this.iCon.Tag == IconType.Delete)
                {
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_scan, IconType.Scan);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxTextChange != null)
            {
                TextBoxTextChange(sender, e);
            }

            if (textBox1.Text.Length > 0)
            {
                //if (textBox1.TextAlign == HorizontalAlignment.Right)
                //{
                //    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_delete, IconType.Delete, true);
                //}
                //else
                //{
                    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_delete, IconType.Delete, true);
                //}

                // For QTY
                if (IsNumber)
                {
                    //TO DO Set format QTY
                    int select = textBox1.SelectionStart;
                    //textBox1.Text = textBox1.Text.Replace(".0000", "").Replace(".000", "").Replace(".00", "").Replace(".0","").Replace(".","");
                    textBox1.TextChanged -= textBox1_TextChanged;
                    textBox1.Text = textBox1.Text.Replace(".0000", "").Replace(".000", "").Replace(".00", "").Replace(".0", "").Replace(".", "");
                    textBox1.TextChanged += textBox1_TextChanged;
                    textBox1.SelectionStart = select;
                }
                
                if (IsSetFormat)
                {                  
                    if (textBox1.Text.Trim() != "")
                    {
                        double temp = 0.0;
                        string amt = textBox1.Text.Replace(",", "");
                        if (!double.TryParse(amt, out temp))
                        {
                            textBox1.Text = "";
                        }
                    }
                }
            }
            else
            {
                if ((UCTextBoxIconType)this.Tag == UCTextBoxIconType.SearchAndDelete)
                    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_search, IconType.Search);
                else if ((UCTextBoxIconType)this.Tag == UCTextBoxIconType.NoneAndDelete)
                    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_none, IconType.None);
                else if ((UCTextBoxIconType)this.Tag == UCTextBoxIconType.ScanAndDelete)
                    InitialTextBoxIcon(this, BJCBCPOS.Properties.Resources.icon_textbox_scan, IconType.Scan);

                if (this.EnabledUC)
                {
                    label1.Visible = true;
                }       
            }
            textBox1.Focus();
        }

        public void InitialTextBoxIcon(Image pic, UCTextBoxIconType textBoxType, IconType iconType, string placeHolder = "", bool NoCustomShowLine = true, bool isEnabelBtn = true)
        {
            this.Tag = textBoxType;
            InitialTextBoxIcon(this, pic, iconType, NoCustomShowLine, isEnabelBtn);
        }

        public void InitialTextBoxIcon(UCTextBoxWithIcon UCtbwi, Image pic, IconType iconType, bool NoCustomShowLine = true, bool isEnabelBtn = true)
        {
            if (NoCustomShowLine)
            {
                if (iconType == IconType.None || iconType == IconType.Scan)
                {
                    isEnabelBtn = false;
                    UCtbwi.lineShape1.Visible = false;
                }
            }
            else
            {
                UCtbwi.lineShape1.Visible = true;
            }

            if (textBox1.TextAlign == HorizontalAlignment.Right)
            {
                panel3.Dock = DockStyle.Fill;
                panel2.Visible = false;
            }

            UCtbwi.iCon.Tag = iconType;
            UCtbwi.iCon.BackgroundImage = pic;
            UCtbwi.iCon.Enabled = isEnabelBtn;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            if (textBox1.TextAlign == HorizontalAlignment.Right)
                panel2.Visible = false;

            textBox1.Focus();
        }

        private void iCon_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (textBox1.TextAlign != HorizontalAlignment.Right)
                {
                    panel2.Visible = true;
                }
                label1.Visible = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.EnabledUC)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (TextBoxKeydown != null)
                    {
                        //if (IsAmount)
                        //{
                        //    if (textBox1.Text.IndexOf(".") <= 0)
                        //    {
                        //        this.textBox1.Text = (Convert.ToDouble(textBox1.Text) / 100).ToString();
                        //    }
                        //}
                        TextBoxKeydown(this, e);
                        return;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public void switchLanguage(string text, string fontName)
        {
            this.label1.Text = text;
            this.label1.Font = new Font(fontName, this.label1.Font.Size, this.label1.Font.Style, this.label1.Font.Unit, this.label1.Font.GdiCharSet);
            this.textBox1.Font = new Font(fontName, this.textBox1.Font.Size, this.textBox1.Font.Style, this.textBox1.Font.Unit, this.textBox1.Font.GdiCharSet); ;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.EnabledUC)
            {
                if (IsNumber)
                {
                    if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                    {
                        e.Handled = true;
                    }
                    if (e.KeyChar != (char)Keys.Enter)
                    {
                        textBox1.Focus();
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox1_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(textBox1);
        }
    }
}
