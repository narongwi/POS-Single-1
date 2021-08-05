using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmDisplayContent : Form
    {
        private bool IsPaint = false; 
        private string _content;
        //public Language currentLangDisplay = null;

        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value; 
                update();
            }
        }

        public frmDisplayContent()
        {
            InitializeComponent();
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

        private void frmDisplayContent_Load(object sender, EventArgs e)
        {
            AppMessage.fillControlsFont(ProgramConfig.language, this, new List<string>());
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
            //currentLangDisplay = ProgramConfig.language;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            IsPaint = false;
        }

        private void update()
        {
            string data;
            richTextBox1.Text = "";

            string[] boldSplit = this._content.Replace("|", Environment.NewLine).Split(new string[] { "<B>" }, StringSplitOptions.None);
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

            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

    }
}
