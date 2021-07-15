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
    public partial class UCCashierMessage : UserControl
    {
        private string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        private string _text = "Message";
        private Color _bg = Color.White;

        public string date
        {
            get { return this._date; }
            set { this._date = value; }
        }

        public string text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public Color background
        {
            get { return this._bg; }
            set { this._bg = value; }
        }

        public UCCashierMessage()
        {
            InitializeComponent();
        }

        public void update()
        {
            lbMessageDate.Text = this._date;
            lbMessageText.Text = this._text;
            this.BackColor = this._bg;
        }
    }
}
