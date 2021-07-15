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
    public partial class UCItemDropDownListExtraProduct : UserControl, IDropdownListItem
    {
        private int _txtLength = 0;

        public UCItemDropDownListExtraProduct()
        {
            InitializeComponent();
        }

        [Category("Action")]
        [Description("Occurs when the dropdown is clicked.")]
        [Browsable(true)]
        public event EventHandler UCItemDropDownListClick;

        private void UCItemDropDownList_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        private void UCItemDropDownList_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        private void lineShape1_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        public void DropDownClick(object sender, EventArgs e)
        {
            if (UCItemDropDownListClick != null)
            {
                UCItemDropDownListClick(this, e);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            DropDownClick(sender, e);
        }

        public int LineWidth
        {
            get
            {
                return lineShape1.Width;
            }
            set
            {
                lineShape1.Width = value;
            }
        }

        public Point LineLocation
        {
            get
            {
                return lineShape1.Location;
            }
        }


        public Image picIcon
        {
            get
            {
                return icon_Symbol.Image;
            }
            set
            {
                icon_Symbol.Image = value;
            }
        }


        public Font LabelFont
        {
            get { return lbText.Font; }
            set { lbText.Font = value; }
        }

        public string DisplayText
        {
            get
            {
                return lbText.Text;
            }
            set
            {
                lbText.Text = value;
            }
        }
        public string ValueText
        {
            get
            {
                return lbValue.Text;
            }
            set
            {
                lbValue.Text = value;
            }
        }
        public int DisplayTextLength
        {
            get
            {
                if (_txtLength == 0)
                {
                    return lbText.Text.Length;
                }
                else
                {
                    return _txtLength;
                }
            }
            set
            {
                _txtLength = value;
            }
        }


        public bool LineVisible
        {
            get
            {
                return lineShape1.Visible;
            }
            set
            {
                lineShape1.Visible = value;
            }
        }
    }
}
