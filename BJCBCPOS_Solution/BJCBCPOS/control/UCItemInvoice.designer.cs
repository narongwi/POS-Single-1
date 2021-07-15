namespace BJCBCPOS
{
    partial class UCItemInvoice
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUC_Cashier = new System.Windows.Forms.Label();
            this.lbUC_Qty = new System.Windows.Forms.Label();
            this.lbUC_ReceiptNo = new System.Windows.Forms.Label();
            this.lbUC_No = new System.Windows.Forms.Label();
            this.lbUC_ReturnPrice = new System.Windows.Forms.Label();
            this.lbUC_TotalDisc = new System.Windows.Forms.Label();
            this.lbUC_Date = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbUC_Cashier
            // 
            this.lbUC_Cashier.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_Cashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_Cashier.ForeColor = System.Drawing.Color.Black;
            this.lbUC_Cashier.Location = new System.Drawing.Point(268, 16);
            this.lbUC_Cashier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_Cashier.Name = "lbUC_Cashier";
            this.lbUC_Cashier.Size = new System.Drawing.Size(72, 18);
            this.lbUC_Cashier.TabIndex = 58;
            this.lbUC_Cashier.Text = "12345678";
            this.lbUC_Cashier.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbUC_Cashier.Click += new System.EventHandler(this.lbPrice_Click);
            // 
            // lbUC_Qty
            // 
            this.lbUC_Qty.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_Qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_Qty.ForeColor = System.Drawing.Color.Black;
            this.lbUC_Qty.Location = new System.Drawing.Point(364, 16);
            this.lbUC_Qty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_Qty.Name = "lbUC_Qty";
            this.lbUC_Qty.Size = new System.Drawing.Size(81, 18);
            this.lbUC_Qty.TabIndex = 57;
            this.lbUC_Qty.Text = "2323232";
            this.lbUC_Qty.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbUC_Qty.Click += new System.EventHandler(this.lbQty_Click);
            // 
            // lbUC_ReceiptNo
            // 
            this.lbUC_ReceiptNo.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_ReceiptNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_ReceiptNo.ForeColor = System.Drawing.Color.Black;
            this.lbUC_ReceiptNo.Location = new System.Drawing.Point(36, 16);
            this.lbUC_ReceiptNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_ReceiptNo.Name = "lbUC_ReceiptNo";
            this.lbUC_ReceiptNo.Size = new System.Drawing.Size(90, 18);
            this.lbUC_ReceiptNo.TabIndex = 56;
            this.lbUC_ReceiptNo.Text = "1234567890";
            // 
            // lbUC_No
            // 
            this.lbUC_No.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_No.ForeColor = System.Drawing.Color.Black;
            this.lbUC_No.Location = new System.Drawing.Point(2, 16);
            this.lbUC_No.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_No.Name = "lbUC_No";
            this.lbUC_No.Size = new System.Drawing.Size(34, 18);
            this.lbUC_No.TabIndex = 54;
            this.lbUC_No.Text = "999";
            this.lbUC_No.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbUC_No.Click += new System.EventHandler(this.lbNo_Click);
            // 
            // lbUC_ReturnPrice
            // 
            this.lbUC_ReturnPrice.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_ReturnPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_ReturnPrice.ForeColor = System.Drawing.Color.Black;
            this.lbUC_ReturnPrice.Location = new System.Drawing.Point(460, 16);
            this.lbUC_ReturnPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_ReturnPrice.Name = "lbUC_ReturnPrice";
            this.lbUC_ReturnPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbUC_ReturnPrice.Size = new System.Drawing.Size(90, 18);
            this.lbUC_ReturnPrice.TabIndex = 59;
            this.lbUC_ReturnPrice.Text = "1234567890";
            this.lbUC_ReturnPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbUC_ReturnPrice.Click += new System.EventHandler(this.lbDiscount_Click);
            // 
            // lbUC_TotalDisc
            // 
            this.lbUC_TotalDisc.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_TotalDisc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_TotalDisc.ForeColor = System.Drawing.Color.Black;
            this.lbUC_TotalDisc.Location = new System.Drawing.Point(554, 16);
            this.lbUC_TotalDisc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_TotalDisc.Name = "lbUC_TotalDisc";
            this.lbUC_TotalDisc.Size = new System.Drawing.Size(91, 18);
            this.lbUC_TotalDisc.TabIndex = 60;
            this.lbUC_TotalDisc.Text = "1234567890";
            this.lbUC_TotalDisc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbUC_TotalDisc.Click += new System.EventHandler(this.lbTotalDisc_Click_1);
            // 
            // lbUC_Date
            // 
            this.lbUC_Date.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_Date.ForeColor = System.Drawing.Color.Black;
            this.lbUC_Date.Location = new System.Drawing.Point(124, 16);
            this.lbUC_Date.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_Date.Name = "lbUC_Date";
            this.lbUC_Date.Size = new System.Drawing.Size(140, 18);
            this.lbUC_Date.TabIndex = 61;
            this.lbUC_Date.Text = "12345678901234567";
            this.lbUC_Date.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbUC_Date.Click += new System.EventHandler(this.lbDate_Click);
            // 
            // UCItemInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbUC_Date);
            this.Controls.Add(this.lbUC_TotalDisc);
            this.Controls.Add(this.lbUC_ReturnPrice);
            this.Controls.Add(this.lbUC_Cashier);
            this.Controls.Add(this.lbUC_Qty);
            this.Controls.Add(this.lbUC_ReceiptNo);
            this.Controls.Add(this.lbUC_No);
            this.DoubleBuffered = true;
            this.Name = "UCItemInvoice";
            this.Size = new System.Drawing.Size(647, 51);
            this.Load += new System.EventHandler(this.UCItemSell_Load);
            this.Click += new System.EventHandler(this.UCItemSell_Click);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbUC_Cashier;
        public System.Windows.Forms.Label lbUC_Qty;
        public System.Windows.Forms.Label lbUC_ReceiptNo;
        public System.Windows.Forms.Label lbUC_No;
        public System.Windows.Forms.Label lbUC_ReturnPrice;
        public System.Windows.Forms.Label lbUC_TotalDisc;
        public System.Windows.Forms.Label lbUC_Date;
    }
}
