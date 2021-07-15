using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;

namespace BJCBCPOS
{
    public partial class frmAddCoupon : Form
    {
        private bool IsPaint = false; 
        public string couponNo;
        public string couponQnt;
        public string payCode;
        public string couNo;
        public string couAmt;
        public string formatCash;

        private UCCoupon ucGV;

        private SaleProcess saleProcess = new SaleProcess();
        DataTable dt2Data = null;

        public frmAddCoupon()
        {
            InitializeComponent();
        }

        private void frmAddCoupon_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Size = this.Owner.Size;

                int x = (this.Size.Width / 2) - (panel2.Size.Width / 2);
                int y = (this.Size.Height / 2) - (panel2.Size.Height / 2);
                panel2.Location = new Point(x, y);

                this.Location = this.Owner.Location;
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

        public void loadAddCoupon()
        {
            StoreResult read = saleProcess.checkCoupon(couponNo, Convert.ToInt32(couponQnt), ProgramConfig.memberId);
            if (read.response == ResponseCode.Success)
            {
                DataTable dt = read.otherData;
                dt2Data = dt;
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    int num;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string amtTye = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_AmountType.parameterCode);
                        string displayAmt = (string)ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_AmountDisplay.parameterCode);

                        payCode = dt.Rows[i]["PaymentCode"].ToString();
                        couNo = dt.Rows[i]["CouponNo"].ToString();
                        couAmt = dt.Rows[i]["CouponAmt"].ToString();

                        double receiveCash = double.Parse(couAmt);

                        if (amtTye == "1") //ทศนิยม
                        {
                            if (displayAmt == "1")
                            {
                                formatCash = receiveCash.ToString("#,###,###.00");
                            }
                            else if (displayAmt == "2")
                            {
                                formatCash = receiveCash.ToString("#,###,###");
                            }
                            else if (displayAmt == "3")
                            {
                                formatCash = receiveCash.ToString("#.###.###,00");
                            }
                        }
                        else if (amtTye == "1") //ไม่มีทศนิยม
                        {
                            if (displayAmt == "2")
                            {
                                formatCash = receiveCash.ToString("#,###,###");
                            }
                            else
                            {
                                formatCash = receiveCash.ToString("#,###,###");
                            }
                        }

                        if (panel1.Controls.Count > 0)
                        {
                            num = panel1.Controls.Cast<UCCoupon>().Select(s => Convert.ToInt32(String.IsNullOrEmpty(s.lbNoText) ? "0" : s.lbNoText)).Max() + 1;
                        }
                        else
                        {
                            num = 1;
                        }

                        UCCoupon ucList = new UCCoupon();
                        ucList.lbNo.Text = num.ToString();
                        ucList.UCGridViewItemSellClick += UCGridViewItemSellClick;
                        ucList.DeleteClick += DeleteClick;
                        ucList.lbCouponNo.Text = couNo;
                        ucList.lbCouponValue.Text = formatCash;
                        ucList.lbQty.Text = "1";
                        ucList.lbProductCode.Text = "";
                        panel1.Controls.Add(ucList);
                    }
                }

                RefreshGrid();

            }
            else if (read.response == ResponseCode.Error)
            {
                frmNotify dialog = new frmNotify(ResponseCode.Error, read.responseMessage, read.helpMessage);
                dialog.ShowDialog(this);
                return;
            }
        }

        public void UCGridViewItemSellClick(object sender, EventArgs e)
        {
            ucGV = (UCCoupon)sender;

            couNo = ucGV.lbCouponNo.Text;
            couAmt = ucGV.lbCouponValue.Text;

            frmPayment frmPayment = (frmPayment)this.Owner;
            frmPayment.frmAddCouponData(payCode,couNo,couAmt);

            this.Dispose();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            List<UCCoupon> lstCoupon = new List<UCCoupon>();
            lstCoupon = panel1.Controls.Cast<UCCoupon>().OrderByDescending(o => Convert.ToInt32(o.lbNoText)).ToList();
            panel1.Controls.Clear();
            int num = lstCoupon.Count;

            foreach (UCCoupon item in lstCoupon)
            {
                if (num % 2 != 0)
                {
                    item.BackColor = Color.FromArgb(225, 225, 225);
                }
                else
                {
                    item.BackColor = Color.White;
                }
                item.lbNoText = num.ToString();
                panel1.Controls.Add(item);
                num--;
            }
            ScrollToBottom(panel1);
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
