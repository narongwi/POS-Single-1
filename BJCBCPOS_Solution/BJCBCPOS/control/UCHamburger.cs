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
    public partial class UCHamburger : UserControl
    {
        [Category("Action")]
        [Description("Occurs when the menu item is clicked.")]
        [Browsable(true)]
        public event EventHandler HambergerItemClick;

        [Category("Action")]
        [Description("Occurs when the button back is clicked.")]
        [Browsable(true)]
        public event EventHandler BackClick;

        private bool IsPaint = true;
        private DataTable _dt = new DataTable();
        private string _subPanelMenuName = "PanelSubName";
        private string _subPanelMenuItenName = "PanelSubItemName";

        public UCHamburger()
        {
            InitializeComponent();
        }

        public UCHamburger(DataTable dt)
        {
            InitializeComponent();
            _dt = dt;
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
            if (BackClick != null)
            {
                BackClick(sender, e);
            }
        }

        private void UCHamberger_Load(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.Size = new Size(1024, 725);
                this.Location = new Point(0, 43);
            }

            IsPaint = false;

            foreach (DataRow dr in _dt.Select(" ReferMenuID = '0' "))
            {
                UCHamburgerItem ucHambItm = new UCHamburgerItem();

                if (_dt.Select(" ReferMenuID = '" + dr["MenuID"].ToString() + "'").Length > 0)                
                {
                    ucHambItm.label1.Text = dr["MenuName"].ToString();
                    ucHambItm.pictureBox1.Image = Properties.Resources.Sale_CancelReceipt;
                    pn_HambergerItem.Controls.Add(ucHambItm);
                    pn_HambergerItem.Controls.SetChildIndex(ucHambItm, 0);

                    DataRow[] drItemLv1 = _dt.Select(" ReferMenuID = '" + dr["MenuID"].ToString() + "'");
 
                    Panel panel = new Panel();
                    panel.Dock = System.Windows.Forms.DockStyle.Top;
                    panel.Location = new System.Drawing.Point(0, 0);
                    panel.Name = _subPanelMenuName + dr["MenuID"].ToString();
                    panel.Size = new System.Drawing.Size(344, 53 * drItemLv1.Length);
                    panel.Visible = false;
                    ucHambItm.HambergerItemClick += (s1, e1) => ExpandItemClick(s1, e1, panel);

                    foreach (DataRow dr2 in drItemLv1)
                    {
                        UCHamburgerItem ucHambItm2 = new UCHamburgerItem();
                        ucHambItm2.label1.Text = dr2["MenuName"].ToString();
                        ucHambItm2.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                        ucHambItm2.pictureBox1.Image = Properties.Resources.MenuHamberger_dot_subitem;
                        ucHambItm2.pictureBox3.Visible = false;
                        ucHambItm2.BackColor = Color.FromArgb(235, 248, 240);
                        ucHambItm2.Name = _subPanelMenuItenName + dr["MenuID"].ToString() + dr2["MenuID"].ToString();
                        ucHambItm2.MenuID = (MenuIdHamberger)Convert.ToInt32(dr2["MenuID"]);

                        DataRow[] drItemLv2 = _dt.Select(" ReferMenuID = '" + dr2["MenuID"].ToString() + "'");

                        if (drItemLv2.Length > 1)
                        {
                            string menuID = dr2["MenuID"].ToString();
                            string menuName = dr2["MenuName"].ToString();
                            ucHambItm2.HambergerItemClick += (s, e2) => ucHambItm_HambergerSubMenuItemClick(s, e2, menuID, menuName);
                        }
                        else
                        {

                            ucHambItm2.HambergerItemClick += ucHambItm_HambergerItemClick;
                        }
                        panel.Controls.Add(ucHambItm2);
                        panel.Controls.SetChildIndex(ucHambItm2, 0);
                    }

                    pn_HambergerItem.Controls.Add(panel);
                    pn_HambergerItem.Controls.SetChildIndex(panel, 0);
                }
                else
                {
                    ucHambItm.MenuID = (MenuIdHamberger)Convert.ToInt32(dr["MenuID"]);
                    ucHambItm.HambergerItemClick += ucHambItm_HambergerItemClick;
                    ucHambItm.label1.Text = dr["MenuName"].ToString();
                    ucHambItm.pictureBox3.Visible = false;
                    pn_HambergerItem.Controls.Add(ucHambItm);
                    pn_HambergerItem.Controls.SetChildIndex(ucHambItm, 0);
                } 
            }
        }

        private void ucHambItm_HambergerItemClick(object sender, EventArgs e)
        {
            if (HambergerItemClick != null)
            {
                HambergerItemClick(sender, e);
            }
        }

        private void ucHambItm_HambergerSubMenuItemClick(object sender, EventArgs e, string refMenuID, string menuName)
        {
            Form frm = this.FindForm();
            var ucSubMenu = new UCHamburgerSubMenu(_dt, refMenuID, menuName);
            ucSubMenu.HambergerSubItemClick += ucHambItm_HambergerItemClick;
            frm.Controls.Add(ucSubMenu);
            frm.Controls.SetChildIndex(ucSubMenu, 0);
            frm.Refresh();
        }

        private void ExpandItemClick(object sender, EventArgs e, Panel pn)
        {
            pn.Visible = !pn.Visible;
            UCHamburgerItem ucHambItm = (UCHamburgerItem)sender;
            if (pn.Visible)
            {
                ucHambItm.BackColor = Color.FromArgb(68, 186, 109);
                ucHambItm.label1.ForeColor = Color.White;
                ucHambItm.pictureBox3.Image = Properties.Resources.MenuHamberger_arrow_down;
            }
            else
            {
                ucHambItm.BackColor = Color.White;
                ucHambItm.label1.ForeColor = Color.DarkGray;
                ucHambItm.pictureBox3.Image = Properties.Resources.MenuHamberger_arrow_right;
            }
        }
    }
}
