using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class UCAdjustCash : UserControl
    {
        public UCAdjustCash()
        {
            InitializeComponent();
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return btnContinue.Text;
            }
            set
            {
                btnContinue.Text = value;
            }
        }

        [Category("Action")]
        [Description("Occurs when enter from button.")]
        [Browsable(true)]
        public event EventHandler EnterFromButton;

        [Category("Action")]
        [Description("Occurs when enter contiune button.")]
        [Browsable(true)]
        public event EventHandler EnterContinueButton;

        public void InitialBankNote(DataTable dt)
        {
            ClearBankNote();
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr["Seq"].ToString())
                {
                    case "1":
                        ChangeColor(coin1, bankNote1, lbSeq1, pnBankNote1, ucTextBoxSmall1, dr);
                        break;
                    case "2":
                        ChangeColor(coin2, bankNote2, lbSeq2, pnBankNote2, ucTextBoxSmall2, dr);
                        break;
                    case "3":
                        ChangeColor(coin3, bankNote3, lbSeq3, pnBankNote3, ucTextBoxSmall3, dr);
                        break;
                    case "4":
                        ChangeColor(coin4, bankNote4, lbSeq4, pnBankNote4, ucTextBoxSmall4, dr);
                        break;
                    case "5":
                        ChangeColor(coin5, bankNote5, lbSeq5, pnBankNote5, ucTextBoxSmall5, dr);
                        break;
                    case "6":
                        ChangeColor(coin6, bankNote6, lbSeq6, pnBankNote6, ucTextBoxSmall6, dr);
                        break;
                    case "7":
                        ChangeColor(coin7, bankNote7, lbSeq7, pnBankNote7, ucTextBoxSmall7, dr);
                        break;
                    case "8":
                        ChangeColor(coin8, bankNote8, lbSeq8, pnBankNote8, ucTextBoxSmall8, dr);
                        break;
                    case "9":
                        ChangeColor(coin9, bankNote9, lbSeq9, pnBankNote9, ucTextBoxSmall9, dr);
                        break;
                    case "10":
                        ChangeColor(coin10, bankNote10, lbSeq10, pnBankNote10, ucTextBoxSmall10, dr);
                        break;
                    case "11":
                        ChangeColor(coin11, bankNote11, lbSeq11, pnBankNote11, ucTextBoxSmall11, dr);
                        break;
                    case "12":
                        ChangeColor(coin12, bankNote11, lbSeq12, pnBankNote12, ucTextBoxSmall12, dr);
                        break;
                    default:
                        break;
                }           
            }
        }
        private void ClearBankNote()
        {
            pnBankNote1.Visible = false;
            pnBankNote2.Visible = false;
            pnBankNote3.Visible = false;
            pnBankNote4.Visible = false;
            pnBankNote5.Visible = false;
            pnBankNote6.Visible = false;
            pnBankNote7.Visible = false;
            pnBankNote8.Visible = false;
            pnBankNote9.Visible = false;
            pnBankNote10.Visible = false;
            pnBankNote11.Visible = false;
            pnBankNote12.Visible = false;
        }
        private void ChangeColor(OvalShape coin, RectangleShape bankNote, Label lbSeq, Panel pnBankNote, UCTextBoxSmall ucTextBoxSmall, DataRow dr)
        {
            if (dr["UnitType"].ToString() == "N")
            {         
                coin.Visible = false;
                bankNote.Visible = true;
                bankNote.BorderColor = ColorTranslator.FromHtml(dr["UnitColor"].ToString());                
            }
            else
            {
                bankNote.Visible = false;
                coin.Visible = true;
                coin.BorderColor = ColorTranslator.FromHtml(dr["UnitColor"].ToString());
            }
            ucTextBoxSmall.Tag = Convert.ToDouble(dr["UnitAmt"]).ToString();
            lbSeq.Text = Convert.ToDouble(dr["UnitAmt"]).ToString();
            pnBankNote.Visible = true;
        }
        private void ucTextBoxSmall_EnterFromButton(object sender, EventArgs e)
        {
            if (EnterFromButton != null)
            {
                EnterFromButton(this, e);
            }
        }
        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (EnterFromButton != null && EnterContinueButton != null)
            {
                EnterFromButton(this, e);
                //Clear textbox
                for (int i = 1; i <= 12; i++)
                {
                    UCTextBoxSmall txtbox = this.Controls.Find("ucTextBoxSmall" + i, true).FirstOrDefault() as UCTextBoxSmall;
                    txtbox.Text = "";
                }             
                EnterContinueButton(sender, e);
            }
        }
        public void SendToBackLabel()
        {
            lbSeq1.SendToBack();
            lbSeq2.SendToBack();
            lbSeq3.SendToBack();
            lbSeq4.SendToBack();
            lbSeq5.SendToBack();
            lbSeq6.SendToBack();
            lbSeq7.SendToBack();
            lbSeq8.SendToBack();
            lbSeq9.SendToBack();
            lbSeq10.SendToBack();
            lbSeq11.SendToBack();
            lbSeq12.SendToBack();
            this.Refresh();
        }

        private void BankNoteClinkCount(UCTextBoxSmall ucTBS)
        {
            int num = Convert.ToInt32(ucTBS.Text.Trim() == "" ? "0" : ucTBS.Text.Trim());
            num++;
            ucTBS.Text = num.ToString();
            ucTBS.TextBox.Focus();
        }

        private void coin1_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall1);
        }

        private void bankNote1_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall1);
        }

        private void coin2_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall2);
        }

        private void bankNote2_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall2);
        }

        private void coin3_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall3);
        }

        private void bankNote3_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall3);
        }

        private void coin4_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall4);
        }

        private void bankNote4_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall4);
        }

        private void coin5_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall5);
        }

        private void bankNote5_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall5);
        }

        private void coin6_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall6);
        }

        private void bankNote6_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall6);
        }

        private void coin7_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall7);
        }

        private void bankNote7_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall7);
        }

        private void coin8_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall8);
        }

        private void bankNote8_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall8);
        }

        private void coin9_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall9);
        }

        private void bankNote9_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall9);
        }

        private void coin10_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall10);
        }

        private void bankNote10_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall10);
        }

        private void coin11_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall11);
        }

        private void bankNote11_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall11);
        }

        private void coin12_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall12);
        }

        private void bankNote12_Click(object sender, EventArgs e)
        {
            BankNoteClinkCount(ucTextBoxSmall12);
        }

        public void switchLanguage(string text, string fontName)
        {
            this.btnContinue.Text = text;
            this.btnContinue.Font = new Font(fontName, this.btnContinue.Font.Size, this.btnContinue.Font.Style, this.btnContinue.Font.Unit, this.btnContinue.Font.GdiCharSet);
            //this.textBox1.Font = new Font(fontName, this.textBox1.Font.Size, this.textBox1.Font.Style, this.textBox1.Font.Unit, this.textBox1.Font.GdiCharSet); ;
        }
    }
}
