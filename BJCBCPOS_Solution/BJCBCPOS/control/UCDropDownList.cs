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
    public partial class UCDropDownList : UserControl
    {
        public Form _pr;
        Point pnt = new Point();
        UCDropDownList currentUCDDL;

        public struct Dropdown
        {
            public string DisplayText;
            public string ValueText;
        }

        [Browsable(false)]
        public List<Dropdown> lstDDL { get; set; }

        public List<IDropdownListItem> lstCustom = new List<IDropdownListItem>();

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsLarge { get; set; }

        [Category("Custom Property")]
        [Description("Set dropdown right side")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool DropdownExpandRightSide { get; set; }
       
        public string DisplayText { get; set; }
        public string ValueText { get; set; }

        public UCDropDownList()
        {
            InitializeComponent();
        }

        public void SetType<T>() where T : IDropdownListItem, new()
        {
            List<T> a = new List<T>();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (this.Enabled)
            {
                panel1.BackColor = Color.White;
            }
            else
            {
                panel1.BackColor = Color.Gray;
            }

            base.OnEnabledChanged(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UCClick(sender, e);
        }

        private void UCDropDownList_Click(object sender, EventArgs e)
        {
            UCClick(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            UCClick(sender, e);
        }

        [Category("Action")]
        [Description("Occurs when the dropdown is clicked.")]
        [Browsable(true)]
        public event EventHandler UCDropDownListClick;

        [Category("Action")]
        [Description("Occurs when the dropdown item is clicked.")]
        [Browsable(true)]
        public event EventHandler UCDropDownGetItemClick;

        [Category("Custom Property")]
        [Description("Text display label in dropdown")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string LabelText
        {
            get { return UCddlLbText.Text; }
            set
            {
                UCddlLbText.Text = value;
            }
        }

        public void UCClick(object sender, EventArgs e)
        {
            var ucDDL = this;
            lstDDL = new List<Dropdown>();
            var pn_DropDown = this.FindForm().Controls.Find("pn_DropDown", true).FirstOrDefault() as Panel;
            pn_DropDown.Leave += LeavePanel;
            var ucDDL_Point = ucDDL.FindForm().PointToClient(ucDDL.Parent.PointToScreen(ucDDL.Location));
            if (pn_DropDown.Visible && pnt == ucDDL_Point)
            {
                pn_DropDown.Visible = !pn_DropDown.Visible;
            }
            else
            {
                pnt = ucDDL_Point;
                currentUCDDL = ucDDL;
                pn_DropDown.Width = ucDDL.Width;
                pn_DropDown.Location = new Point(ucDDL_Point.X, ucDDL.Height + ucDDL_Point.Y + 1);

                pn_DropDown.Controls.Clear();
                pn_DropDown.Visible = true;

                UCDropDownListClick(this, e);

                int ucH = 0;
                int cnt = 1;
                int temp = 0;
                int widthDD = pn_DropDown.Width;
                int widthLine = 0;
                int widthLabel = 0;

                if (lstDDL != null)
                {
                    Dropdown maxStr = lstDDL.Select(s => s).OrderByDescending(o => o.DisplayText.Length).FirstOrDefault();

                    UCItemDropDownList _ucdd = new UCItemDropDownList();
                    Font font = _ucdd.label1.Font;
                    widthLine = _ucdd.lineShape1.Width - _ucdd.lineShape1.Location.X;
                    widthLabel = _ucdd.Width;
                    temp = TextRenderer.MeasureText(maxStr.DisplayText, font).Width;

                    if (temp + 13 >= widthDD)
                    {
                        widthDD = temp + 13; // 13 คือส่วนต่างของ Size form width กับ Size label width >>>>> Form UCItemDropDownList
                        widthLabel = temp;
                    }

                    if (temp >= widthLine)
                    {
                        widthLine = temp + _ucdd.lineShape1.Location.X;
                    }
                    else
                    {
                        widthLine = widthDD - 13;
                    }

                    foreach (Dropdown str in lstDDL)
                    {
                        ucH += 35; // ความสูงของ item >>>>> UCItemDropDownList

                        UCItemDropDownList ucdd = new UCItemDropDownList();

                        ucdd.UCItemDropDownListClick += (s2, e2) => UCItemDropDownListClick(s2, e2, pn_DropDown);

                        ucdd.label1.Text = str.DisplayText;
                        ucdd.label2.Text = str.ValueText;

                        ucdd.label1.Width = widthLabel;
                        ucdd.lineShape1.Width = widthLine;

                        if (cnt == 1)
                        {
                            ucdd.lineShape1.Visible = false;
                        }

                        cnt++;
                        pn_DropDown.Controls.Add(ucdd);
                    }

                    if (ucH > 210) // check ให้ item dropdown มีได้แค่ 6 ถ้ามากกว่านั้น จะมี scroll bar 
                    //if (ucH >= 99)
                    {
                        ucH = 210; // 198 คือ ส่วนสูงของ panel เมื่อมี item 6 ชิ้น
                        //ucH = 99;
                        widthDD = widthDD + (widthDD == pn_DropDown.Width ? 0 : 10); // + scorll bar ที่เพิ่มเข้ามา
                    }

                    if (widthDD > pn_DropDown.Width && !DropdownExpandRightSide)
                    {
                        // set location ไปทางซ้าย
                        pn_DropDown.Location = new Point(pn_DropDown.Location.X - (widthDD - pn_DropDown.Width), pn_DropDown.Location.Y);
                    }

                    pn_DropDown.Height = ucH + 3;
                    pn_DropDown.Width = widthDD;
                    pn_DropDown.BringToFront();
                    pn_DropDown.Focus();
                    if (IsLarge)
                    {
                        this.BackgroundImage = Properties.Resources.DropDownListLarge_enable;
                    }
                    else
                    {
                        this.BackgroundImage = Properties.Resources.txtboxWIC_enable;
                    }
                }
            }
        }

        private void UCItemDropDownListClick(object sender, EventArgs e, Panel pn_DropDown)
        {
            var ucIDDL = (UCItemDropDownList)sender;
            this.UCddlLbText.Text = ucIDDL.label1.Text;
            this.DisplayText = ucIDDL.label1.Text;
            this.ValueText = ucIDDL.label2.Text;
            pn_DropDown.Visible = false;

            if (IsLarge)
            {
                this.BackgroundImage = Properties.Resources.DropDownListLarge_disable;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.txtboxWIC_disable;
            }

            if(UCDropDownGetItemClick != null)
            {
                UCDropDownGetItemClick(this, e);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            UCClick(sender, e);
        }

        private void UCDropDownList_Load(object sender, EventArgs e)
        {

        }

        private void LeavePanel(object sender, EventArgs e)
        {
            ((Panel)sender).Visible = false;

            if (IsLarge)
            {
                this.BackgroundImage = Properties.Resources.DropDownListLarge_disable;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.txtboxWIC_disable;
            }

        }

    }
}
