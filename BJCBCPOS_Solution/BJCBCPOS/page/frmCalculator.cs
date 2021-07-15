using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;


namespace BJCBCPOS
{
    public partial class frmCalculator : Form
    {
        private bool IsPaint = false;

        private Control m_ResultControl;
        private Control m_AnchorControl;
        private string m_StartValue = String.Empty;
        private string m_Result = "0";
        private bool m_Restart = true;
        private Color m_BorderColor = Color.White;
        private char m_LastOp = '\0';
        private bool m_IsInEquals = false;
        private Stack<double> m_Stack = new Stack<double>();
        private EventHandler<CalculatorParseEventArgs> m_Parse;
        private EventHandler<CalculatorFormatEventArgs> m_Format;
        private bool m_Centered = false;
        private bool m_AutoEvaluatePercentKey = false;

        public frmCalculator()
        {
            InitializeComponent();
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    if (!IsPaint)
        //    {
        //        using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
        //        {
        //            e.Graphics.FillRectangle(brush, e.ClipRectangle);
        //            IsPaint = true;
        //        }
        //    }
        //}

        private void frmCalculator_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel1.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);

                this.Location = this.Owner.Location;
            }
            else
            {
                this.Size = new Size(1024, 768);
                int x = 512 - (panel1.Size.Width / 2);
                int y = 384 - (panel1.Size.Height / 2);
                panel1.Location = new Point(x, y);

                this.Location = new Point(0, 0);
            }
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbCal.Text != "")
            {
                lbCal.Text = lbCal.Text.Remove(lbCal.Text.Length - 1, 1);
                //lbMessage.Text = lbMessage.Text.Remove(lbMessage.Text.Length - 1, 1);
                if (lbCal.Text.Length == 0)
                {
                    lbCal.Text = "0";
                }
            }
            else
            {
                lbCal.Text = "0";
                lbMessage.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbCal.Text = "";              
            clear();                        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Program.control.CloseForm("frmCalculator");
        }

        private void AddChar(char c)
        {
            string s = lbCal.Text;
            if (c == GetSeparatorChar() &&
                s.Contains(GetSeparatorChar().ToString()) && !m_Restart)
                return;

            if (s == "0" || s == "00")
                s = c.ToString();
            else
                s += c;
            SetDisplay(s);
            if (m_Restart)
            {
                SetDisplay(c.ToString());
                m_Restart = false;
            }
        }

        private char GetSeparatorChar()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            string sep = culture.NumberFormat.NumberDecimalSeparator;
            if (String.IsNullOrEmpty(sep))
                return '.';

            return sep[0];
        }

        private void SetDisplay(string s)
        {
            m_Result = s;
            lbCal.Text = s;

            //if (!m_Restart)
            //{
            //    lbMessage.Text = lbMessage.Text + s;
            //}
        }

        private double DisplayValue
        {
            get
            {
                double d = 0;
                if (Double.TryParse(lbCal.Text, out d))
                    return d;

                return 0.0;
            }
        }

        private void DoLastOp()
        {
            m_Restart = true;
            if (m_LastOp == '\0' || m_Stack.Count == 1)
                return;

            double valTwo = m_Stack.Pop();
            double valOne = m_Stack.Pop();
            switch (m_LastOp)
            {
                case '+':
                    m_Stack.Push(valOne + valTwo);
                    break;
                case '-':
                    m_Stack.Push(valOne - valTwo);
                    break;
                case 'x':
                    m_Stack.Push(valOne * valTwo);
                    break;
                case '÷':
                    m_Stack.Push(valOne / valTwo);
                    break;
                default:
                    break;
            }
            SetDisplay(m_Stack.Peek().ToString());
            if (m_IsInEquals)
                m_Stack.Push(valTwo);
        }

        private void DoOpChar(char op)
        {
            if (m_IsInEquals)
            {
                m_Stack.Clear();
                m_IsInEquals = false;
            }
            m_Stack.Push(DisplayValue);
            DoLastOp();
            m_LastOp = op;
        }

        private void NumClick(object sender, EventArgs e)
        {
            AddChar(((Button)sender).Text[0]);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            DoOpChar(((Button)sender).Text[0]);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            DoOpChar(((Button)sender).Text[0]);
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            DoOpChar(((Button)sender).Text[0]);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            DoOpChar(((Button)sender).Text[0]);
        }

        public void clear()
        {
            SetDisplay("0");
            m_LastOp = '\0';
            m_Restart = true;
            m_Stack.Clear();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DoEqualsChar();
        }

        private void DoEqualsChar()
        {
            if (m_LastOp == '\0')
                return;

            if (!m_IsInEquals)
            {
                m_IsInEquals = true;
                m_Stack.Push(DisplayValue);
            }
            DoLastOp();
        }

        private void Num0Click(object sender, EventArgs e)
        {
            AddChar(((Button)sender).Text[0]);
            AddChar(((Button)sender).Text[0]);
        }
    }
}
