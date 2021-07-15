    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Process;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmFavoriteSale : Form
    {
        private bool IsPaint = false; 
        private List<int> lstLocationRow = new List<int>();
        private List<int> lstLocationCol = new List<int>();
        private string _pageNo = "1";
        private string _pageNoSearch = "1";
        private string _tabName = "1";
        private string _tabNameSearch = "1";
        private DataTable dtFavorit = new DataTable();
        private DataTable dtFavoritSearch = new DataTable();
        private List<TabColor> lstTC = new List<TabColor>();
        private frmSale fSale = null;
        private frmSaleProcess fSaleProcess = null;

        //private int[,] _rowcol = { { 1, 1 }, { 1, 2 }, { 1, 3 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 3, 1 }, { 3, 2 }, { 3, 3 } };

        private List<RowCount> _rowcount = new List<RowCount>() { new RowCount() { Row = 1, Column = 1 },
                                                                new RowCount() { Row = 1, Column = 2 } ,
                                                                new RowCount() { Row = 1, Column = 3 } ,
                                                                new RowCount() { Row = 2, Column = 1 } ,
                                                                new RowCount() { Row = 2, Column = 2 } ,
                                                                new RowCount() { Row = 2, Column = 3 } ,
                                                                new RowCount() { Row = 3, Column = 1 } ,
                                                                new RowCount() { Row = 3, Column = 2 } ,
                                                                new RowCount() { Row = 3, Column = 3 } };
        
        

        public struct RowCount
        {
            public int Row;
            public int Column;
        }

        public frmFavoriteSale()
        {
            InitializeComponent();
        }

        private void frmFavoriteSale_Load(object sender, EventArgs e)
        {
            try
            {
                AppMessage.fillForm(ProgramConfig.language, this);
                if (fSale == null && this.Owner is frmSale)
                {
                    fSale = (frmSale)this.Owner;
                }

                AddLocation();
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

                StoreResult res = fSale.fSaleProcess.getProductIcon();
                if (res.response.next)
                {
                    if (tabControl1.TabPages.Count > 0)
                        tabControl1.TabPages.Clear();

                    dtFavorit = res.otherData;
                    lb_PageNo.Text = _pageNo;
                    var lst = dtFavorit.AsEnumerable().Where(dr => dr["TabId"].ToString() == _tabName).OrderByDescending(o => Convert.ToInt32(o["PageNo"])).Select(s => Convert.ToInt32(s["PageNo"])).ToList();
                    if (lst.Count() > 0)
                    {
                        lb_PageTotal.Text = lst[0].ToString();
                    }
                    LoadTabPage(dtFavorit);
                }
                else
                {
                    this.Dispose();
                }

                tabControl1.BringToFront();
                pn_Footer_Product.BringToFront();
            }
            catch (NetworkConnectionException net)
            {
                AppLog.writeLog("connection to server lost at frmFavoriteSale.frmFavoriteSale_Load");
                fSale.CatchNetWorkConnectionException(net);
                frmLoading.closeLoading();
                this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                frmLoading.closeLoading();
                frmNotify dialog = new frmNotify(ResponseCode.Error, ex.Message, "");
                dialog.ShowDialog(this);
            }
            //AppMessage.fillForm(ProgramConfig.language, "frmFavoriteSale", this);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ItemClick(object sender, EventArgs e)
        {
            try
            {
                var ucItem = (UCItem)sender;

                frmSale fSales = this.Owner as frmSale;
                fSales.productCode = ucItem.Label1Text;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                Program.control.RetryConnection(net.errorType);
            }
            catch (Exception ex)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }

            
        }


        private void LoadTabPage(DataTable dt)
        {
            string tabid = "";
            string page = "";
            string tabName = "";
            string rowNo = "";
            string columnNo = "";
            string productCode = "";
            string productName = "";
            string price = "";
            string btnColor = "";
            string tabColor = "";
            int idx = 0;

            bool isClear = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tabid = dt.Rows[i]["TabId"].ToString();
                tabName = dt.Rows[i]["TabName"].ToString();
                btnColor = dt.Rows[i]["COLOR_CODE"].ToString();
                tabColor = dt.Rows[i]["TAB_COLOR_CODE"].ToString();

                if (!isClear)
                {
                    if (tabControl1.TabPages[_tabName] != null)
                    {
                        tabControl1.TabPages[_tabName].Controls.Clear();                     
                    }
                    isClear = true;
                }

                if (!tabControl1.TabPages.ContainsKey(tabid))
                {
                    TabPage tab = new TabPage();
                    tab.BackColor = Color.White;
                    tab.Name = tabid;
                    tab.Text = "  " + tabName.Trim() + "  "; //เว้นช่องว่างเพื่อให้ตัวหนังสือไม่ตกขอบ
                    tab.Tag = tabid;
       
                    tab.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
                    lstTC.Add(new TabColor { Index = idx, ColorCode = tabColor });
                    tabControl1.TabPages.Add(tab);
                    idx++;
                }

                page = dt.Rows[i]["PageNo"].ToString();

                if (page == _pageNo)
                {
                    rowNo = dt.Rows[i]["RowNo"].ToString();
                    columnNo = dt.Rows[i]["ColumnNo"].ToString();
                    productCode = dt.Rows[i]["ProductCode"].ToString();
                    productName = dt.Rows[i]["ProductName"].ToString();
                    price = Convert.ToDouble(dt.Rows[i]["PRICE_CURRENT"]).ToString(ProgramConfig.amountFormatString);

                    Color tabcolor = ColorTranslator.FromHtml(btnColor);
                    UCItem ucItem = new UCItem();
                    ucItem.lb_ProductCode.Text = productCode;
                    ucItem.lb_ProductCode.BackColor = tabcolor;

                    ucItem.lb_ProductName.Text = productName;
                    ucItem.lb_ProductName.BackColor = tabcolor;

                    ucItem.lb_Amount.Text = price;
                    ucItem.lb_Amount.BackColor = tabcolor;

                    ucItem.BackColor = tabcolor;

                    ucItem.ItemClick += ItemClick;
                    ucItem.Location = new Point(lstLocationCol[Convert.ToInt32(columnNo) - 1], lstLocationRow[Convert.ToInt32(rowNo) - 1]);

                    tabControl1.TabPages[tabid].Controls.Add(ucItem);
                }

            }

        }

        private void AddLocation()
        {
            //lstLocationRow.Add(20);   //row 1
            //lstLocationRow.Add(158);  //row 2
            //lstLocationRow.Add(296);  //row 3

            //lstLocationCol.Add(16); //Col 1
            //lstLocationCol.Add(297); //Col 2
            //lstLocationCol.Add(574); //Col 3

            lstLocationRow.Add(12);   //row 1
            lstLocationRow.Add(185);  //row 2
            lstLocationRow.Add(358);  //row 3

            lstLocationCol.Add(16); //Col 1
            lstLocationCol.Add(297); //Col 2
            lstLocationCol.Add(574); //Col 3
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            _tabName = tabControl1.SelectedTab.Name;
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage--;

            if (nextPage > 0)
            {
                _pageNo = nextPage.ToString();
                lb_PageNo.Text = nextPage.ToString();
                LoadTabPage(dtFavorit);
            }
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            _tabName = tabControl1.SelectedTab.Name;
            int nextPage = Convert.ToInt32(lb_PageNo.Text);
            nextPage++;
            if (nextPage <= Convert.ToInt32(lb_PageTotal.Text))
            {
                _pageNo = nextPage.ToString();
                lb_PageNo.Text = nextPage.ToString();
                LoadTabPage(dtFavorit);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                //var a = tabControl1.SelectedTab;
                if (tabControl1.TabPages.Count > 0)
                {
                    if (tabControl1.SelectedTab != null)
                    {
                        _tabName = tabControl1.SelectedTab.Name;
                        var lst = dtFavorit.AsEnumerable().Where(dr => dr["TabId"].ToString() == _tabName).OrderByDescending(o => Convert.ToInt32(o["PageNo"])).Select(s => Convert.ToInt32(s["PageNo"])).ToList();
                        if (lst.Count() > 0)
                        {
                            lb_PageTotal.Text = lst[0].ToString();
                            lb_PageNo.Text = "1";
                            _pageNo = "1";
                            LoadTabPage(dtFavorit);
                        }
                    }
                }
            }
            catch (NetworkConnectionException net)
            {
                //throw;
                Program.control.RetryConnection(net.errorType);
            }
            catch(Exception ex)
            {

            }
            //LoadTabPage(dtFavorit);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font f;
            Brush backBrush;
            Brush foreBrush;
            string color;
            TabPage tp = (TabPage)((TabControl)sender).TabPages[this.tabControl1.SelectedIndex];
            //TabPage te = tp.TabPages[this.tabControl1.SelectedIndex];

            if (e.Index == this.tabControl1.SelectedIndex)
            {
                //f = new Font("Microsoft Sans Serif", 20f, FontStyle.Regular);

                color = lstTC.Where(w => w.Index == e.Index).Select(s => s.ColorCode).FirstOrDefault();
                f = e.Font;
                f = new Font(e.Font, FontStyle.Regular);
                backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, ColorTranslator.FromHtml(color), ColorTranslator.FromHtml(color), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                foreBrush = Brushes.Black;
            }
            else
            {
                color = lstTC.Where(w => w.Index == e.Index).Select(s => s.ColorCode).FirstOrDefault();
                f = e.Font;            
//backBrush = new SolidBrush(ColorTranslator.FromHtml(color));
                backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, ColorTranslator.FromHtml(color), ColorTranslator.FromHtml(color), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                foreBrush = new SolidBrush(Color.Black);
            }
            string tabName = this.tabControl1.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            RectangleF r = new RectangleF(e.Bounds.X - 10, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4);
            e.Graphics.DrawString(tabName, f, foreBrush, r, sf);
            sf.Dispose();
            if (e.Index == this.tabControl1.SelectedIndex)
            {
                f.Dispose();
                backBrush.Dispose();
            }
            else
            {
                backBrush.Dispose();
                foreBrush.Dispose();
            }
        }

        private void ucTextBoxWithIcon1_TextBoxTextChange(object sender, EventArgs e)
        {
            try
            {
                string keySearch = EscapeLikeValue(ucTextBoxWithIcon1.InpTxt);
                if (keySearch == "")
                {
                    //lb_PageNo.Text = _pageNo;
                    //var lst = dtFavorit.AsEnumerable().Where(dr => dr["TabId"].ToString() == _tabName).OrderByDescending(o => Convert.ToInt32(o["PageNo"])).Select(s => Convert.ToInt32(s["PageNo"])).ToList();
                    //if (lst.Count() > 0)
                    //{
                    //    lb_PageTotal.Text = lst[0].ToString();
                    //}
                    pn_Footer_Product.BringToFront();
                    tabControl1.BringToFront();
                }
                else
                {
                    DataRow[] dr = dtFavorit.Select(String.Format(@" ProductCode like '%{0}%' OR ProductName like '%{0}%'", keySearch));
                    dtFavoritSearch = new DataTable();
                    _pageNoSearch = "1";
                    lb_PageNo_Search.Text = _pageNoSearch;
                    lb_PageTotal_Search.Text = _pageNoSearch;
                    if (dr.Length > 0)
                    {
                        lb_PageTotal_Search.Text = Math.Ceiling(Convert.ToDecimal(dr.Length) / 9).ToString();
                        dtFavoritSearch = dr.OrderBy(o => o["ProductName"]).CopyToDataTable();
                    }
                    LoadTabPageSearch(dtFavoritSearch);
                }
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex.Message);
            }
        }

        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private void LoadTabPageSearch(DataTable dt)
        {
            ucTextBoxWithIcon1.Focus();
            tabControl2.BringToFront();
            tabControl2.TabPages.Clear();
            pn_Footer_Product_Search.BringToFront();
            int tabid = 1;
            string page = "";
            //string tabName = "";
            string rowNo = "";
            string columnNo = "";
            string productCode = "";
            string productName = "";
            string price = "";
            string btnColor = "";
            string tabColor = "";
            int idx = 0;

            if (dt.Rows.Count > 9)
            {
                var dr = dt.AsEnumerable().Skip((Convert.ToInt32(_pageNoSearch) - 1) * 9);
                if (dr.Count() > 9)
                {
                    dt = dr.Take(9).CopyToDataTable();
                }
                else
                {
                    dt = dr.CopyToDataTable();
                }
            }

            TabPage tab = new TabPage();
            tab.BackColor = Color.White;
            tab.Name = tabid.ToString();
            tab.Text = String.Format("  Search  "); //เว้นช่องว่างเพื่อให้ตัวหนังสือไม่ตกขอบ
            tab.Tag = tabid;

            tab.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
            //lstTC.Add(new TabColor { Index = idx, ColorCode = tabColor });
            tabControl2.TabPages.Add(tab);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //tabid = (i + 1).ToString();//dt.Rows[i]["TabId"].ToString();
                //tabName = //dt.Rows[i]["TabName"].ToString();
                btnColor = dt.Rows[i]["COLOR_CODE"].ToString();
                tabColor = dt.Rows[i]["TAB_COLOR_CODE"].ToString();

                //if (!isClear)
                //{
                //    if (tabControl2.TabPages[_tabName] != null)
                //    {
                //        tabControl2.TabPages[_tabName].Controls.Clear();
                //    }
                //    isClear = true;
                //}

                //if (i % 9 == 0)
                //{
                //tabid++;
                //TabPage tab = new TabPage();
                //tab.BackColor = Color.White;
                //tab.Name = tabid.ToString();
                //tab.Text = String.Format("  Search  ", tabid); //เว้นช่องว่างเพื่อให้ตัวหนังสือไม่ตกขอบ
                //tab.Tag = tabid;

                //tab.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
                //lstTC.Add(new TabColor { Index = idx, ColorCode = tabColor });
                //tabControl2.TabPages.Add(tab);
                //idx++;
                //}

                //page = dt.Rows[i]["PageNo"].ToString();
                //if (page == _pageNoSearch)
                //{
                rowNo = _rowcount[i].Row.ToString();//dt.Rows[i]["RowNo"].ToString();
                columnNo = _rowcount[i].Column.ToString();//dt.Rows[i]["ColumnNo"].ToString();
                productCode = dt.Rows[i]["ProductCode"].ToString();
                productName = dt.Rows[i]["ProductName"].ToString();
                price = Convert.ToDouble(dt.Rows[i]["PRICE_CURRENT"]).ToString(ProgramConfig.amountFormatString);

                Color tabcolor = ColorTranslator.FromHtml(btnColor);
                UCItem ucItem = new UCItem();
                ucItem.lb_ProductCode.Text = productCode;
                ucItem.lb_ProductCode.BackColor = tabcolor;

                ucItem.lb_ProductName.Text = productName;
                ucItem.lb_ProductName.BackColor = tabcolor;

                ucItem.lb_Amount.Text = price;
                ucItem.lb_Amount.BackColor = tabcolor;

                ucItem.BackColor = tabcolor;

                ucItem.ItemClick += ItemClick;
                ucItem.Location = new Point(lstLocationCol[Convert.ToInt32(columnNo) - 1], lstLocationRow[Convert.ToInt32(rowNo) - 1]);

                tabControl2.TabPages[0].Controls.Add(ucItem);
                //}
            }
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font f;
            Brush backBrush;
            Brush foreBrush;
            string color;
            TabPage tp = (TabPage)((TabControl)sender).TabPages[this.tabControl2.SelectedIndex];
            //TabPage te = tp.TabPages[this.tabControl1.SelectedIndex];

            if (e.Index == this.tabControl2.SelectedIndex)
            {
                //f = new Font("Microsoft Sans Serif", 20f, FontStyle.Regular);

                //color = lstTC.Where(w => w.Index == e.Index).Select(s => s.ColorCode).FirstOrDefault();
                f = e.Font;
                f = new Font(e.Font, FontStyle.Regular);
                backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.CornflowerBlue, Color.CornflowerBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                foreBrush = Brushes.Black;
            }
            else
            {
                color = lstTC.Where(w => w.Index == e.Index).Select(s => s.ColorCode).FirstOrDefault();
                f = e.Font;
                //backBrush = new SolidBrush(ColorTranslator.FromHtml(color));
                backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.CornflowerBlue, Color.CornflowerBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                foreBrush = new SolidBrush(Color.Black);
            }
            string tabName = "Search"; //this.tabControl1.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            RectangleF r = new RectangleF(e.Bounds.X - 10, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4);
            e.Graphics.DrawString(tabName, f, foreBrush, r, sf);
            sf.Dispose();
            if (e.Index == this.tabControl2.SelectedIndex)
            {
                f.Dispose();
                backBrush.Dispose();
            }
            else
            {
                backBrush.Dispose();
                foreBrush.Dispose();
            }
        }

        private void btn_Next_Search_Click(object sender, EventArgs e)
        {
            _tabName = tabControl2.SelectedTab.Name;
            int nextPage = Convert.ToInt32(lb_PageNo_Search.Text);
            nextPage++;
            if (nextPage <= Convert.ToInt32(lb_PageTotal_Search.Text))
            {
                _pageNoSearch = nextPage.ToString();
                lb_PageNo_Search.Text = nextPage.ToString();
                LoadTabPageSearch(dtFavoritSearch);
            }
        }

        private void btn_Previous_Search_Click(object sender, EventArgs e)
        {
            _tabName = tabControl2.SelectedTab.Name;
            int nextPage = Convert.ToInt32(lb_PageNo_Search.Text);
            nextPage--;

            if (nextPage > 0)
            {
                _pageNoSearch = nextPage.ToString();
                lb_PageNo_Search.Text = nextPage.ToString();
                LoadTabPageSearch(dtFavoritSearch);
            }
        }

        private void ucTextBoxWithIcon1_Enter(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = true;
            this.ucKeyboard1.BringToFront();
            this.ucKeyboard1.currentInput = ucTextBoxWithIcon1;
        }

        private void ucKeyboard1_HideKeyboardClick(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
        }

        private void ucTextBoxWithIcon1_TextBoxLeave(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
        }

        private void ucTextBoxWithIcon1_TextBoxKeydown(object sender, EventArgs e)
        {
            this.ucKeyboard1.Visible = false;
        }

        //private void ucItem1_Click(object sender, EventArgs e)
        //{
        //    var fSales = this.Owner as frmSale;
        //    var fCus = fSales.frmMoCus;
        //    int cnt = fSales.cnt;

        //    fSales.goodSales(ucItem1.Label1Text);
        //    this.Dispose();
        //}

        //private void ucItem2_Click(object sender, EventArgs e)
        //{
        //    var fSales = this.Owner as frmSale;
        //    var fCus = fSales.frmMoCus;
        //    int cnt = fSales.cnt;

        //    fSales.goodSales(ucItem2.Label1Text);
        //    this.Dispose();
        //}

        //private void ucItem3_Click(object sender, EventArgs e)
        //{
        //    var fSales = this.Owner as frmSale;
        //    var fCus = fSales.frmMoCus;
        //    int cnt = fSales.cnt;

        //    fSales.goodSales(ucItem3.Label1Text);
        //    this.Dispose();
        //}
    }
}
