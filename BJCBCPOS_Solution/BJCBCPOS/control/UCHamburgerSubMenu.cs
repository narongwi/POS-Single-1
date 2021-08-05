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
    public partial class UCHamburgerSubMenu : UserControl
    {
        [Category("Action")]
        [Description("Occurs when the menu item is clicked.")]
        [Browsable(true)]
        public event EventHandler HambergerSubItemClick;

        [Category("Action")]
        [Description("Occurs when the button back is clicked.")]
        [Browsable(true)]
        public event EventHandler BackClick;

        private bool IsPaint = true;
        private DataTable _dt = new DataTable();
        private float _size;
        private string _menuName = "";
        private string _refMenuID = "";
        private string _subPanelMenuName = "PanelSubName";
        private string _subPanelMenuItenName = "PanelSubItemName";

        public UCHamburgerSubMenu()
        {
            InitializeComponent();
        }

        public UCHamburgerSubMenu(DataTable dt, string refMenuID, string menuName)
        {
            InitializeComponent();
            _dt = dt;
            _refMenuID = refMenuID;
            _menuName = menuName;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsPaint)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), new Size(1024, 725)));
                    IsPaint = true;
                }
            }
        }

        private void picBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //if (BackClick != null)
            //{
            //    BackClick(sender, e);
            //}
        }

        private void UCHamberger_Load(object sender, EventArgs e)
        {
            _size = lbHamb_Menu.Font.Size;
            if (this.FindForm() != null)
            {
                this.Size = new Size(1024, 725);
                this.Location = new Point(0, 43);
            }

            InitialSubMenu(_refMenuID, _menuName);        
        }

        private void InitialSubMenu(string refMenuID, string menuName)
        {
            pn_HambergerItem.Controls.Clear();
            foreach (DataRow dr in _dt.Select(" ReferMenuID = '" + refMenuID + "'"))
            {
                UCHamburgerItem ucHambItm2 = new UCHamburgerItem();
                ucHambItm2.label1.Text = dr["MenuName"].ToString();
                ucHambItm2.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                ucHambItm2.pictureBox1.Image = Properties.Resources.MenuHamberger_dot_subitem;
                ucHambItm2.pictureBox3.Visible = false;
                //ucHambItm2.BackColor = Color.FromArgb(235, 248, 240);
                ucHambItm2.Name = _subPanelMenuItenName + dr["MenuID"].ToString();
                ucHambItm2.MenuID = (MenuIdHamberger)Convert.ToInt32(dr["MenuID"]);

                DataRow[] drItemLv2 = _dt.Select(" ReferMenuID = '" + dr["MenuID"].ToString() + "'");

                if (drItemLv2.Length > 0)
                {
                    string menuId = dr["MenuID"].ToString();
                    string menuSubName = dr["MenuName"].ToString();
                    ucHambItm2.HambergerItemClick += (s, e) =>
                        {
                            InitialSubMenu(menuId, menuSubName);
                        };
                }
                else
                {
                    ucHambItm2.HambergerItemClick += ucHambItm_HambergerSubItemClick;
                }

                lbHamb_Menu.Text = menuName;
                pn_HambergerItem.Controls.Add(ucHambItm2);
                pn_HambergerItem.Controls.SetChildIndex(ucHambItm2, 0);
            }
        }

        private void ucHambItm_HambergerSubItemClick(object sender, EventArgs e)
        {
            if (HambergerSubItemClick != null)
            {
                this.Dispose();
                HambergerSubItemClick(sender, e);
            }
        }

        private void lbHamb_Menu_TextChanged(object sender, EventArgs e)
        {
            float size = _size;
            while (size > 0)
            {
                if (lbHamb_Menu.Width > System.Windows.Forms.TextRenderer.MeasureText(lbHamb_Menu.Text,
                     new Font(lbHamb_Menu.Font.Name, size, lbHamb_Menu.Font.Style)).Width)
                {
                    lbHamb_Menu.Font = new Font(lbHamb_Menu.Font.Name, size, lbHamb_Menu.Font.Style);
                    break;
                }
                size -= 3f;
            }
        }
    }
}
