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
    public partial class UCDropDownListCustom : UserControl
    {
        Point pnt = new Point();
        UCDropDownListCustom currentUCDDL;
        public IEnumerable<IDropdownListItem> lstDDLC;

        public UCDropDownListCustom()
        {
            InitializeComponent();
        }

        [Category("Custom Property")]
        [Description("Set picture large size")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsLarge { get; set; }

        [Category("Custom Property")]
        [Description("Set dropdown right side")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool DropdownExpandRightSide { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DisplayText { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ValueText { get; set; }

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
            if (UCDropDownListClick != null)
            {
                var ucDDL = this;
                lstDDLC = new List<IDropdownListItem>();

                //int sizeHeight = (ProgramConfig.listActiveLanguage.Count * 45) + 6; // +6 for padding

                Panel pn = new Panel();
                pn.Parent = this.FindForm();
                pn.AutoScroll = true;
                pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;


                //var pn_DropDown = this.FindForm().Controls.Find("pn_DropDown", true).FirstOrDefault() as Panel;
                pn.Leave += LeavePanel;
                var ucDDL_Point = ucDDL.FindForm().PointToClient(ucDDL.Parent.PointToScreen(ucDDL.Location));
                
                //if (pn_DropDown.Visible && pnt == ucDDL_Point)
                //{
                //    pn_DropDown.Visible = !pn_DropDown.Visible;
                //}
                //else
                //{
                    pnt = ucDDL_Point;
                    currentUCDDL = ucDDL;
                    pn.Width = ucDDL.Width;
                    pn.Location = new Point(ucDDL_Point.X, ucDDL.Height + ucDDL_Point.Y + 1);

                    pn.Controls.Clear();
                    pn.Visible = true;

                    UCDropDownListClick(this, e);

                    int ucH = 0;
                    int cnt = 1;
                    int temp = 0;
                    int widthDD = pn.Width;
                    int lineWidth = 0;
                    int widthLabel = 0;

                    if (lstDDLC != null)
                    {
                        IDropdownListItem maxStr = lstDDLC.Select(s => s).OrderByDescending(o => o.DisplayTextLength > 0 ? o.DisplayTextLength : o.DisplayText.Length).FirstOrDefault();

                        UCItemDropDownList _ucdd = new UCItemDropDownList();
                        Font font = new System.Drawing.Font(ProgramConfig.language.FontName, maxStr.LabelFont.Size);
                        lineWidth = maxStr.LineWidth - maxStr.LineLocation.X;
                        widthLabel = ((UserControl)maxStr).Width;

                        if (maxStr.DisplayTextLength > 0)
                        {
                            temp = maxStr.DisplayTextLength;
                        }
                        else
                        {
                            temp = TextRenderer.MeasureText(maxStr.DisplayText, font).Width;
                        }

                        if (temp + 13 >= widthDD)
                        {
                            widthDD = temp + 13; // 13 คือส่วนต่างของ Size form width กับ Size label width >>>>> Form UCItemDropDownList
                            widthLabel = temp;
                        }

                        if (temp >= lineWidth)
                        {
                            lineWidth = temp + _ucdd.lineShape1.Location.X;
                        }
                        else
                        {
                            lineWidth = widthDD - 13;
                        }

                        foreach (IDropdownListItem str in lstDDLC)
                        {
                            ucH += 35; // ความสูงของ item >>>>> UCItemDropDownList
                            str.UCItemDropDownListClick += (s2, e2) => UCItemDropDownListClick(s2, e2, pn);

                            ((UserControl)maxStr).Width = widthLabel;
                            str.LineWidth = lineWidth;
                            str.LabelFont = font;

                            if (cnt == 1)
                            {
                                str.LineVisible = false;
                            }

                            cnt++;
                            pn.Controls.Add((UserControl)str);
                        }

                        if (ucH > 210) // check ให้ item dropdown มีได้แค่ 6 ถ้ามากกว่านั้น จะมี scroll bar 
                        //if (ucH >= 99)
                        {
                            ucH = 210; // 198 คือ ส่วนสูงของ panel เมื่อมี item 6 ชิ้น
                            //ucH = 99;
                            widthDD = widthDD + (widthDD == pn.Width ? 0 : 10); // + scorll bar ที่เพิ่มเข้ามา
                        }

                        if (widthDD > pn.Width && !DropdownExpandRightSide)
                        {
                            // set location ไปทางซ้าย
                            pn.Location = new Point(pn.Location.X - (widthDD - pn.Width), pn.Location.Y);
                        }

                        pn.Height = ucH + 3;
                        pn.Width = widthDD;
                        pn.BringToFront();
                        pn.Focus();

                        if (IsLarge)
                        {
                            this.BackgroundImage = Properties.Resources.DropDownListLarge_enable;
                        }
                        else
                        {
                            this.BackgroundImage = Properties.Resources.txtboxWIC_enable;
                        }
                    }
                //}
            }
        }

        private void UCItemDropDownListClick(object sender, EventArgs e, Panel pn_DropDown)
        {
            var ucIDDL = (IDropdownListItem)sender;
            this.UCddlLbText.Text = ucIDDL.DisplayText;
            this.DisplayText = ucIDDL.DisplayText;
            this.ValueText = ucIDDL.ValueText;
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
