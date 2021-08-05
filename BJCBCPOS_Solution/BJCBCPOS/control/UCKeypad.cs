using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace BJCBCPOS
{
    public partial class UCKeypad : UserControl
    {
        public UCTextBoxWithIcon ucTBWI { get; set; }
        public UCTextBoxSmall ucTBS { get; set; }
        public UCKeypad()
        {
            InitializeComponent();
        }

        public SoundPlayer sound = new SoundPlayer(Properties.Resources.beep_07);

      /*  public bool Check_Validate()
        {
            if (ucTBWI != null)
            {
                if (ucTBWI.IsTextChange)
                {
                    return true;
                }
            }

            if (ucTBS != null)
            {
                if (ucTBS.IsTextChange)
                {
                    return true;
                }
            }

            return false;
        }*/

        private void NumberClick(string num)
        {            
            if (ucTBWI != null && ucTBWI.EnabledUC)
            {
                if (ucTBWI.IsTextChange)
                {
                    //ucTBWI.TextBox.SelectedText
                    if (!String.IsNullOrEmpty(ucTBWI.TextBox.SelectedText))
                    {
                        ucTBWI.TextBox.Text = ""; // ucTBWI.TextBox.Text.Remove(ucTBWI.TextBox.SelectionStart, ucTBWI.TextBox.SelectionLength);
                    }

                    if (ucTBWI.Text.Length != ucTBWI.TextBox.MaxLength)
                    {
                        if (ucTBWI.TextBox.SelectionStart != ucTBWI.Text.Length)
                        {
                            ucTBWI.TextBox.Paste(num);
                        }
                        else
                        {
                            ucTBWI.TextBox.Text += num;
                            ucTBWI.TextBox.SelectionStart = ucTBWI.Text.Length;
                        }                      
                    }
                    else
                    {
                        ucTBWI.TextBox.Focus();
                    }
                    
                }
            }

            if (ucTBS != null)
            {

                if (!String.IsNullOrEmpty(ucTBS.TextBox.SelectedText))
                {
                    ucTBS.TextBox.Text = ucTBS.TextBox.Text.Remove(ucTBS.TextBox.SelectionStart, ucTBS.TextBox.SelectionLength);
                }

                if (ucTBS.IsTextChange)
                {
                    if (ucTBS.TextBox.Text.Length < ucTBS.TextBox.MaxLength)
                    {
                        ucTBS.TextBox.Paste(num);
                        //ucTBS.TextBox.Text += num;
                    }
                    //ucTBS.TextBox.SelectionStart = ucTBS.Text.Length;
                    ucTBS.TextBox.Focus();
                }
            }

            //Console.Beep();
            sound.Play();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            NumberClick("7");
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (ucTBWI != null)
            {
                if (ucTBWI.IsTextChange)
                {
                    ucTBWI.Enter_FromButton(this, e);
                }
            }


            if (ucTBS != null)
            {
                if (ucTBS.IsTextChange)
                {
                    ucTBS.Enter_FromButton(this, e);
                }
            }

            if (ucTBWI != null && ucTBWI.EnabledUC)
            {
                ucTBWI.TextBox.Focus();
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
                NumberClick("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
                NumberClick("9");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
                NumberClick("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
                NumberClick("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
                NumberClick("6");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
                NumberClick("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
                NumberClick("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
                NumberClick("3");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
                NumberClick("0");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (ucTBWI != null && ucTBWI.EnabledUC)
            {
                if (ucTBWI.IsTextChange)
                {
                    ucTBWI.TextBox.Text = "";
                }
            }

            if (ucTBS != null)
            {
                if (ucTBS.IsTextChange)
                {
                    ucTBS.TextBox.Text = "";
                    ucTBS.TextBox.Focus();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ucTBWI != null && ucTBWI.EnabledUC)
            {
                if (ucTBWI.TextBox.Text != "")
                {
                    if (ucTBWI.IsTextChange)
                    {                     
                        ucTBWI.TextBox.Text = ucTBWI.TextBox.Text.Remove(ucTBWI.TextBox.Text.Length - 1, 1);
                        ucTBWI.TextBox.SelectionStart = ucTBWI.TextBox.Text.Length;
                    }
                }
            }

            if (ucTBS != null)
            {
                if (ucTBS.TextBox.Text != "")
                {
                    if (ucTBS.IsTextChange)
                    {
                        ucTBS.TextBox.Text = ucTBS.TextBox.Text.Remove(ucTBS.TextBox.Text.Length - 1, 1);
                        ucTBS.TextBox.SelectionStart = ucTBS.TextBox.Text.Length;
                    }
                }
            }
        }

        private void btndot_Click(object sender, EventArgs e)
        {
            //if (ucTBWI.IsAmount)
            //{
            //    NumberClick("00");
            //}
            //else
            //{
            NumberClick(".");
            //}
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            NumberClick("00");
        }
    }
}
