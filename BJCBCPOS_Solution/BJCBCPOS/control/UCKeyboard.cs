using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using BJCBCPOS_Model;
using System.Media;

namespace BJCBCPOS
{
    public partial class UCKeyboard : UserControl
    {

        [Category("Action")]
        [Description("Occurs when the hide keyboard button is clicked.")]
        [Browsable(true)]
        public event EventHandler HideKeyboardClick;

        public Control currentInput { get; set; }

        private TextBox testTextbox = new TextBox();

        private Bitmap[] language_flag = { null, Properties.Resources.usa_enable, Properties.Resources.Thai, Properties.Resources.Laos };
        private KeyboardApi keyboard = null;
        private key[] layout_key = {
                                      //ESC,    ~,          1,          2,          3,          4,          5,          6,          7,          8,          9,          0,          -,          =,          back space
                                      key.ESC,  key.Tilde,  key.One,    key.Two,    key.Three,  key.Four,   key.Five,   key.Six,    key.Seven,  key.Eight,  key.Nine,   key.Zero,   key.Hyphen, key.Equal,  key.BackSpace,
                                      //tab,    q,      w,      e,      r,      t,      y,      u,      i,      o,      p,      [,                  ],                  \,              del
                                      key.Tab,  key.q,  key.w,  key.e,  key.r,  key.t,  key.y,  key.u,  key.i,  key.o,  key.p,  key.OpenBracket,    key.CloseBracket,   key.BackSlash,  key.Delete,
                                      //caps,       a,      s,      d,      f,      g,      h,      j,      k,      l,      ;,              ',                  enter
                                      key.CapsLock, key.a,  key.s,  key.d,  key.f,  key.g,  key.h,  key.j,  key.k,  key.l,  key.SemiColon,  key.SingleQuote,    key.Enter,
                                      //lshift,         z,      x,      c,      v,      b,      n,      m,      ,,          .,          /,          up,     rshift
                                      key.LeftShift,    key.z,  key.x,  key.c,  key.v,  key.b,  key.n,  key.m,  key.Comma,  key.Dot,    key.Slash,  key.Up, key.RightShift,
                                      //fn -> change to switch language,    lctrl,          window -> change to Num Pad,    latl,           space bar,      ratl,           rctrl,          left,       down,       right,      switch language -> change to hide keyboard
                                      key.SwitchLanguage,                   key.Control,    key.NumPad,                     key.Alternate,  key.SpaceBar,   key.Alternate,  key.Control,    key.Left,   key.Down,   key.Right,  key.HideKeyboard,
                                      // NUM PAD
                                      // Clear,     Back space,    
                                      key.Delete,   key.BackSpace,
                                      // 7,         8,          9,          +,              -
                                      key.NUM_7,    key.NUM_8,  key.NUM_9,  key.NUM_PLUS,   key.NUM_MINUS,
                                      // 4,         5,          6,          *,                  /
                                      key.NUM_4,    key.NUM_5,  key.NUM_6,  key.NUM_MUTIPLY,    key.NUM_DIVIDE,
                                      // 1,         2,          3,          Enter
                                      key.NUM_1,    key.NUM_2,  key.NUM_3,  key.Enter,
                                      // 0,         .
                                      key.NUM_0,    key.NUM_DOT
                                 };

        private const int LSHIFT = 0;
        private const int RSHIFT = 1;
        private const int CTRL = 2;
        private const int ATL = 3;
        private const int CAP = 4;

        private Language keyboard_language = ProgramConfig.language;

        private bool[] hold = { false, false, false, false, false };

        public SoundPlayer sound = new SoundPlayer(Properties.Resources.beep_07);

        bool _hideLanguageButton = false;

        [Category("Action")]
        [Description("Occurs when hide keyboard language")]
        [Browsable(true)]
        public bool hideLanguageButton
        {
            get
            {
                return _hideLanguageButton;
            }
            set
            {
                _hideLanguageButton = value;
            }
        }

        public UCKeyboard()
        {
            InitializeComponent();
        }

        public void updateLanguage()
        {
            keyboard_language = ProgramConfig.language ?? new Language(1);
            keyboard = new KeyboardApi(CultureInfo.GetCultureInfo(keyboard_language.culture));
            updateKeyboard();
        }

        public void updateLanguage(Language lang)
        {
            keyboard_language = lang;
            keyboard = new KeyboardApi(CultureInfo.GetCultureInfo(keyboard_language.culture));
            updateKeyboard();
        }

        // change text and add event
        private void initialKeyboard()
        {
            key code;
            string keyValue;
            foreach (Control item in tableLayoutPanel1.Controls)
            {
                if (item is Button)
                {
                    code = layout_key[item.TabIndex];
                    if (code == key.ESC)
                    {
                        item.Text = "Esc";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.BackSpace)
                    {
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Tab)
                    {
                        item.Text = "Tab";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Delete)
                    {
                        item.Text = "C";
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.CapsLock)
                    {
                        item.Text = "CapsLock";
                        item.Click += new EventHandler(caplockClick);
                    }
                    else if (code == key.Enter)
                    {
                        item.Text = "Enter";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.LeftShift)
                    {
                        //item.Text = "Shift";
                        item.Click += new EventHandler(leftShiftClick);
                    }
                    else if (code == key.RightShift)
                    {
                        //item.Text = "Shift";
                        item.Click += new EventHandler(rightShiftClick);
                    }
                    else if (code == key.Control)
                    {
                        item.Text = "Ctrl";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Alternate)
                    {
                        item.Text = "Atl";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Up)
                    {
                        //item.Text = "Up";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Down)
                    {
                        //item.Text = "Down";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Left)
                    {
                        //item.Text = "Left";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.Right)
                    {
                        //item.Text = "Right";
                        item.Click += new EventHandler(sendInputScancode);
                    }
                    else if (code == key.SwitchLanguage)
                    {
                        if (_hideLanguageButton)
                        {
                            item.Visible = false;
                        }
                        else
                        {
                            item.Click += new EventHandler(changeLanguage);
                            ((Button)item).Image = Utility.CreateImageLanguage(keyboard_language);
                        }
                    }
                    else if (code == key.HideKeyboard)
                    {
                        //item.Text = "Hide";
                        item.Click += new EventHandler(hideKeyboard);
                    }
                    else if (code == key.NumPad)
                    {
                        //item.Text = "Num Pad";
                        //item.Click += new EventHandler(switchToNumPad);
                    }
                    else if (code == key.NUM_0)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_1)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_2)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_3)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_4)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_5)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_6)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_7)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_8)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_9)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code == key.NUM_DOT)
                    {
                        item.Click += new EventHandler(sendInputNumber);
                    }
                    else if (code.scancode != 0x00)
                    {
                        if (code.extended)
                        {
                            item.Click += new EventHandler(sendInputScancode);
                        }
                        else
                        {
                            keyValue = keyboard.GetCharacter(code.scancode, false);
                            if (keyValue != "")
                            {
                                item.Text = keyValue;
                            }
                            item.Click += new EventHandler(sendInputScancode);
                        }
                    }
                }
            }
        }

        // only change text
        private void updateKeyboard()
        {
            key code;
            string keyValue;
            Font old, update;
            foreach (Control item in tableLayoutPanel1.Controls)
            {
                if (item is Button)
                {
                    // change font
                    old = item.Font;
                    update = new Font("Segoe UI", old.Size, old.Style, old.Unit, old.GdiCharSet);
                    item.Font = update;

                    code = layout_key[item.TabIndex];

                    code = layout_key[item.TabIndex];
                    if (code == key.ESC)
                    {
                        item.Text = "Esc";
                    }
                    else if (code == key.BackSpace)
                    {
                    }
                    else if (code == key.Tab)
                    {
                        item.Text = "Tab";
                    }
                    else if (code == key.Delete)
                    {
                        item.Text = "C";
                    }
                    else if (code == key.CapsLock)
                    {
                        item.Text = "CapsLock";
                    }
                    else if (code == key.Enter)
                    {
                        item.Text = "Enter";
                    }
                    else if (code == key.LeftShift)
                    {
                    }
                    else if (code == key.RightShift)
                    {
                    }
                    else if (code == key.Control)
                    {
                        item.Text = "Ctrl";
                    }
                    else if (code == key.Alternate)
                    {
                        item.Text = "Atl";
                    }
                    else if (code == key.Up)
                    {
                    }
                    else if (code == key.Down)
                    {
                    }
                    else if (code == key.Left)
                    {
                    }
                    else if (code == key.Right)
                    {
                    }
                    else if (code == key.SwitchLanguage)
                    {
                        ((Button)item).Image = Utility.CreateImageLanguage(keyboard_language);
                    }
                    else if (code == key.HideKeyboard)
                    {
                    }
                    else if (code == key.NumPad)
                    {
                    }
                    else if (code.scancode != 0x00)
                    {
                        if (code.extended)
                        {
                        }
                        else
                        {
                            bool isCap = hold[CAP];
                            if (isCap)
                            {
                                if (hold[LSHIFT] || hold[RSHIFT])
                                {
                                    isCap = false;
                                }
                            }
                            else if (hold[LSHIFT] || hold[RSHIFT])
                            {
                                isCap = true;
                            }
                            keyValue = keyboard.GetCharacter(code.scancode, isCap);
                            if (keyValue != "")
                            {
                                item.Text = keyValue;
                            }
                        }
                    }
                }
            }
        }

        private void UCKeyboard_Resize(object sender, EventArgs e)
        {
            updateKeyboard();
        }

        private void caplockClick(object sender, EventArgs e)
        {
            if (hold[LSHIFT])
            {
                keyboard.SendInputUnHold(key.LeftShift.scancode);
                hold[LSHIFT] = false;
            }
            if (hold[RSHIFT])
            {
                keyboard.SendInputUnHold(key.RightShift.scancode);
                hold[RSHIFT] = false;
            }

            keyboard.SendInput(key.CapsLock.scancode);
            hold[CAP] = !hold[CAP];

            updateKeyboard();
            setInputFocus();
        }

        private void leftShiftClick(object sender, EventArgs e)
        {
            if (hold[LSHIFT])
            {
                keyboard.SendInputUnHold(key.LeftShift.scancode);
                hold[LSHIFT] = false;
            }
            else
            {
                keyboard.SendInputHold(key.LeftShift.scancode);
                hold[LSHIFT] = true;
            }
            updateKeyboard();
            setInputFocus();
        }

        private void rightShiftClick(object sender, EventArgs e)
        {
            if (hold[RSHIFT])
            {
                keyboard.SendInputUnHold(key.RightShift.scancode);
                hold[RSHIFT] = false;
            }
            else
            {
                keyboard.SendInputHold(key.RightShift.scancode);
                hold[RSHIFT] = true;
            }
            updateKeyboard();
            setInputFocus();
        }

        private void sendInputScancode(object sender, EventArgs e)
        {
            //Console.Beep();
            sound.Play();
            key current_key = layout_key[((Button)sender).TabIndex];
            if (current_key.extended)
            {
                keyboard.SendInputExtended(current_key.scancode);
                setInputFocus();
            }
            else
            {
                ​​//setInputFocus();
                keyboard.SendInput(current_key.scancode);
                
                if (hold[LSHIFT])
                {
                    keyboard.SendInputUnHold(key.LeftShift.scancode);
                    hold[LSHIFT] = false;
                }
                if (hold[RSHIFT])
                {
                    keyboard.SendInputUnHold(key.RightShift.scancode);
                    hold[RSHIFT] = false;
                }

                updateKeyboard();
                setInputFocus();
            }
        }

        private void sendInputNumber(object sender, EventArgs e)
        {
            sound.Play();
            if (hold[LSHIFT])
            {
                keyboard.SendInputUnHold(key.LeftShift.scancode);
                hold[LSHIFT] = false;
            }
            if (hold[RSHIFT])
            {
                keyboard.SendInputUnHold(key.RightShift.scancode);
                hold[RSHIFT] = false;
            }
            updateKeyboard();

            string value = "";
            key current_key = layout_key[((Button)sender).TabIndex];
            if (current_key == key.NUM_0)
            {
                value = "0";
            }
            else if (current_key == key.NUM_1)
            {
                value = "1";
            }
            else if (current_key == key.NUM_2)
            {
                value = "2";
            }
            else if (current_key == key.NUM_3)
            {
                value = "3";
            }
            else if (current_key == key.NUM_4)
            {
                value = "4";
            }
            else if (current_key == key.NUM_5)
            {
                value = "5";
            }
            else if (current_key == key.NUM_6)
            {
                value = "6";
            }
            else if (current_key == key.NUM_7)
            {
                value = "7";
            }
            else if (current_key == key.NUM_8)
            {
                value = "8";
            }
            else if (current_key == key.NUM_9)
            {
                value = "9";
            }
            else if (current_key == key.NUM_DOT)
            {
                value = ".";
            }
            else if (current_key == key.Delete)
            {
                value = "";
            }

            if (currentInput != null)
            {
                if (currentInput is TextBox)
                {
                    if (value == "")
                    {
                        currentInput.Text = "";
                    }
                    else
                    {
                        currentInput.Text += value;
                    }
                    ((TextBox)currentInput).SelectionStart = currentInput.Text.Length;
                    ((TextBox)currentInput).SelectionLength = 0;
                }
                else if (currentInput is UCTextBoxWithIcon)
                {
                    //if (((UCTextBoxWithIcon)currentInput).TextBox.TextLength)

                    if (((UCTextBoxWithIcon)currentInput).TextBox.TextLength != ((UCTextBoxWithIcon)currentInput).TextBox.MaxLength)
                    {
                        if (value == "")
                        {
                            currentInput.Text = "";
                        }
                        else
                        {
                            currentInput.Text += value;
                        }
                        TextBox tbox = ((UCTextBoxWithIcon)currentInput).TextBox;
                        tbox.SelectionStart = tbox.Text.Length;
                        tbox.SelectionLength = 0;
                    }
                }
            }
            setInputFocus();
        }

        private void changeLanguage(object sender, EventArgs e)
        {
            int new_lang = keyboard_language.ID + 1;
            new_lang = new_lang % (ProgramConfig.listActiveLanguage.Count + 1);
            if (new_lang == 0)
            {
                new_lang = 1;
            }
            keyboard_language = new Language(new_lang);
            //ProgramConfig.language = keyboard_language;
            keyboard = new KeyboardApi(CultureInfo.GetCultureInfo(keyboard_language.culture));
            updateKeyboard();
        }

        private void hideKeyboard(object sender, EventArgs e)
        {
            if (HideKeyboardClick != null)
            {
                HideKeyboardClick(this, e);
            }
        }

        private void switchToNumPad(object sender, EventArgs e)
        {
            //if (!keyboard.GetNumLockKeyIsActive())
            //{
            //    keyboard.SendInputExtended(key.NUMLOCK.scancode);
            //}
            setInputFocus();
        }

        private void setInputFocus()
        {
            if (currentInput != null)
            {
                if (currentInput is TextBox)
                {
                    currentInput.Focus();
                }
                else if (currentInput is UCTextBoxWithIcon)
                {
                    ((UCTextBoxWithIcon)currentInput).TextBox.Focus();
                }
            }
        }

        private int getCurrentInputTextLength()
        {
            if (currentInput != null)
            {
                if (currentInput is TextBox)
                {
                    return currentInput.Text.Length;
                }
                else if (currentInput is UCTextBoxWithIcon)
                {
                    return ((UCTextBoxWithIcon)currentInput).TextBox.Text.Length;
                }
            }
            return 0;
        }

        private void UCKeyboard_Load(object sender, EventArgs e)
        {
            updateLanguage();
            initialKeyboard();
        }
    }

}
