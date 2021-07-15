using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmOtherPayment : Form
    {
        public struct LocationPoint
        {
            public string row;
            public string col;
            public Point point;
        }

        List<PaymentMenuIcon> lstPaymentMenuIcon;
        int[] xPoint = { 0, 24, 302, 577 };
        int[] yPoint = { 0, 8, 217 };

        //List<LocationPoint> lstLoPoint = new List<LocationPoint>() { 
        //                                                   { new LocationPoint() { row="1", col="1", point = new Point(26, 93) } }, 
        //                                                   { new LocationPoint() { row="1", col="2", point = new Point(304, 93) } }, 
        //                                                   { new LocationPoint() { row="1", col="3", point = new Point(579, 93) } }, 
        //                                                   { new LocationPoint() { row="2", col="1", point = new Point(26, 302) } }, 
        //                                                   { new LocationPoint() { row="2", col="2", point = new Point(304, 302) } }, 
        //                                                   { new LocationPoint() { row="2", col="3", point = new Point(579, 302) } } 
        //                                                 };

        int _refID;
        string _header = "";
        bool _IsOtherPaymentClick;
        int lastMenuID;

        private bool IsPaint = false;
        frmPayment _fPayment = null;
        //DataTable _data;
        List<PaymentMenuIcon> _dataFilter;
        
        public frmOtherPayment()
        {
            InitializeComponent();
        }

        public frmOtherPayment(List<PaymentMenuIcon> dataFilter, string header, int refID, bool IsOtherPaymentClick = false)
        {
            InitializeComponent();
            //_data = data;
            _dataFilter = dataFilter;
            _header = header;
            _refID = refID;
            _IsOtherPaymentClick = IsOtherPaymentClick;
        }

        private void frmOtherPayment_Load(object sender, EventArgs e)
        {
            _fPayment = (frmPayment)this.Owner;

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

            lb_PageNo.Text = "1";
            GenerateMenu(_refID, 1);
            AppMessage.fillForm(ProgramConfig.language, this);
        }

        private void SetPage(string menuID)
        {
           // List<PaymentMenuIcon> lstPmMenuIcon = _paymentMenuIcon.GetDataByReferMenuID(menuID); //dtData.Select(" ReferMenuID = '" + menuID + "' ");
            if (lstPaymentMenuIcon != null)
            {
                var lst = _dataFilter.AsEnumerable().Where(itm => itm.ReferMenuID.ToString() == menuID).OrderByDescending(o => o.PageID).Select(s => s.PageID).ToList();
                if (lst.Count > 0)
                {
                    lb_PageTotal.Text = lst[0].ToString();
                }
            }
        }

        private void GenerateMenu(int refID, int pageID)
        {    
            lbOtherPayment.Text = _header;//_dataFilter.Rows[0]["PaymentMainCode"].ToString();
            if (refID >= 0)
            {
                lstPaymentMenuIcon = _dataFilter.Where(w => w.ReferMenuID == refID && w.PageID == pageID).ToList();
            }

            if (refID != lastMenuID)
            {
                SetPage(refID.ToString());
                lastMenuID = refID;
            }


            //List<PaymentMenuIcon> lst = _dataFilter.Select(" RefMenuID = '" + refID + "' ").ToList();
            panel3.Controls.Clear();
            foreach (PaymentMenuIcon itm in lstPaymentMenuIcon)
            {
                string template = itm.PaymentStepGroupID;
                int row = itm.Row;
                int col = itm.Comlumn;
                int meduID = itm.MenuID;
                int page_ID = itm.PageID;
                string subMenuID = itm.SubMenuID;

                UCButtonPayment ucbpm = new UCButtonPayment();
                ucbpm.label1.Text = itm.LabelName;
                ucbpm.PaymentMainCode = itm.PaymentMainCode;


                if (itm.MenuID == 9)
                {
                    ucbpm.ButtonClick += (s, ev) => _fPayment.btnPayment_HirePurchase_Click(s, ev);
                }
                else
                {
                    if (_IsOtherPaymentClick)
                    {
                        ucbpm.ButtonClick += _fPayment.GetButtonTemplate(template, itm.MenuID.ToString(), _fPayment._paymentMenuIcon, itm.PaymentMainCode, itm.LabelName, itm.SubMenuID, true);
                    }
                    else
                    {
                        ucbpm.ButtonClick += (s, ev) => ButtonItemClick(s, ev, template, meduID, subMenuID, page_ID);
                    }

                    ////if (itm.SubMenuID == "Y" && itm.PageID != 0)
                    //if (_IsFixTemp)
                    //{
                    //    ucbpm.ButtonClick += (s, ev) => ButtonItemClick(s, ev, template, meduID, subMenuID);
                    //}
                    ////else if (itm.SubMenuID == "N")
                    //else
                    //{
                    //    //if (itm.SubMenuID == "Y")
                    //    //{
                    //        ucbpm.ButtonClick += (s, ev) => ButtonItemClick(s, ev, template, meduID, subMenuID);
                    //    //}
                    //    //else
                    //    //{
                    //    //    ucbpm.ButtonClick += _fPayment.GetButtonTemplate(template, itm.MenuID.ToString(), _fPayment._paymentMenuIcon, itm.PaymentMainCode, itm.LabelName, itm.SubMenuID, true);
                    //    //}                      
                    //}
                }

                if (itm.Picture.Length > 0)
                {                    
                    ucbpm.pictureBox1.Image = Utility.ByteToImage(itm.Picture);
                }
                ucbpm.Location = new Point(xPoint[col], yPoint[row]);
                panel3.Controls.Add(ucbpm);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            _fPayment.dialogFromOther = System.Windows.Forms.DialogResult.No;
            this.Dispose();
        }

        private void ButtonItemClick(object sender, EventArgs e, string template, int menuID, string subMenuID, int pageID)
        {
            lstPaymentMenuIcon = _dataFilter.Where(w => w.ReferMenuID == menuID && w.PageID == pageID).ToList();
            if (subMenuID == "Y" && lstPaymentMenuIcon.Count > 0)
            {
                GenerateMenu(menuID, pageID);
            }
            else
            {
                UCButtonPayment uc = (UCButtonPayment)sender;
                _fPayment._OPpaymentCode = uc.PaymentMainCode;
                _fPayment._OPpaymentName = uc.label1.Text;
                _fPayment._OPtemplate = template;
                DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
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

        private void btn_Next_Click(object sender, EventArgs e)
        {
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage++;
            if (nextPage <= Convert.ToInt32(lb_PageTotal.Text))
            {
                //pageNo = nextPage.ToString();
                lb_PageNo.Text = nextPage.ToString();
                GenerateMenu(lastMenuID, nextPage);
            }
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage--;
            if (nextPage > 0)
            {
                lb_PageNo.Text = nextPage.ToString();
                GenerateMenu(lastMenuID, nextPage);
            }
        }
    }
}
