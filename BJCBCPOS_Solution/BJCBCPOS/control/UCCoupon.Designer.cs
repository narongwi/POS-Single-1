namespace BJCBCPOS
{
    partial class UCCoupon
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
            this.lbNo = new System.Windows.Forms.Label();
            this.lbCouponNo = new System.Windows.Forms.Label();
            this.lbCouponValue = new System.Windows.Forms.Label();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbProductCode = new System.Windows.Forms.Label();
            this.picBin = new System.Windows.Forms.PictureBox();
            this.paymentCode = new System.Windows.Forms.Label();
            this.lbRow = new System.Windows.Forms.Label();
            this.UCC_lbcouponType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBin)).BeginInit();
            this.SuspendLayout();
            // 
            // lbNo
            // 
            this.lbNo.BackColor = System.Drawing.Color.Transparent;
            this.lbNo.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNo.ForeColor = System.Drawing.Color.Black;
            this.lbNo.Location = new System.Drawing.Point(2, 14);
            this.lbNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNo.Name = "lbNo";
            this.lbNo.Size = new System.Drawing.Size(34, 25);
            this.lbNo.TabIndex = 55;
            this.lbNo.Text = "99";
            this.lbNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNo.Click += new System.EventHandler(this.lbNo_Click);
            // 
            // lbCouponNo
            // 
            this.lbCouponNo.BackColor = System.Drawing.Color.Transparent;
            this.lbCouponNo.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCouponNo.ForeColor = System.Drawing.Color.Black;
            this.lbCouponNo.Location = new System.Drawing.Point(40, 14);
            this.lbCouponNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCouponNo.Name = "lbCouponNo";
            this.lbCouponNo.Size = new System.Drawing.Size(227, 25);
            this.lbCouponNo.TabIndex = 57;
            this.lbCouponNo.Text = "229732435005010030000165";
            this.lbCouponNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCouponNo.Click += new System.EventHandler(this.lbCouponNo_Click);
            // 
            // lbCouponValue
            // 
            this.lbCouponValue.BackColor = System.Drawing.Color.Transparent;
            this.lbCouponValue.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCouponValue.ForeColor = System.Drawing.Color.Black;
            this.lbCouponValue.Location = new System.Drawing.Point(272, 14);
            this.lbCouponValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCouponValue.Name = "lbCouponValue";
            this.lbCouponValue.Size = new System.Drawing.Size(106, 25);
            this.lbCouponValue.TabIndex = 58;
            this.lbCouponValue.Text = "8,888,888.00";
            this.lbCouponValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCouponValue.Click += new System.EventHandler(this.lbCouponValue_Click);
            // 
            // lbQty
            // 
            this.lbQty.BackColor = System.Drawing.Color.Transparent;
            this.lbQty.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQty.ForeColor = System.Drawing.Color.Black;
            this.lbQty.Location = new System.Drawing.Point(381, 14);
            this.lbQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(49, 25);
            this.lbQty.TabIndex = 59;
            this.lbQty.Text = "999";
            this.lbQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbQty.Click += new System.EventHandler(this.lbQty_Click);
            // 
            // lbProductCode
            // 
            this.lbProductCode.BackColor = System.Drawing.Color.Transparent;
            this.lbProductCode.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductCode.ForeColor = System.Drawing.Color.Black;
            this.lbProductCode.Location = new System.Drawing.Point(436, 14);
            this.lbProductCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProductCode.Name = "lbProductCode";
            this.lbProductCode.Size = new System.Drawing.Size(132, 25);
            this.lbProductCode.TabIndex = 60;
            this.lbProductCode.Text = "1234567890123";
            this.lbProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbProductCode.Click += new System.EventHandler(this.lbProductCode_Click);
            // 
            // picBin
            // 
            this.picBin.Image = global::BJCBCPOS.Properties.Resources.icons8_trash_can_100;
            this.picBin.Location = new System.Drawing.Point(574, 11);
            this.picBin.Name = "picBin";
            this.picBin.Size = new System.Drawing.Size(30, 31);
            this.picBin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBin.TabIndex = 61;
            this.picBin.TabStop = false;
            this.picBin.Click += new System.EventHandler(this.picBin_Click);
            // 
            // paymentCode
            // 
            this.paymentCode.AutoSize = true;
            this.paymentCode.Location = new System.Drawing.Point(367, 40);
            this.paymentCode.Name = "paymentCode";
            this.paymentCode.Size = new System.Drawing.Size(35, 13);
            this.paymentCode.TabIndex = 62;
            this.paymentCode.Text = "label1";
            this.paymentCode.Visible = false;
            // 
            // lbRow
            // 
            this.lbRow.AutoSize = true;
            this.lbRow.Location = new System.Drawing.Point(272, 34);
            this.lbRow.Name = "lbRow";
            this.lbRow.Size = new System.Drawing.Size(0, 13);
            this.lbRow.TabIndex = 63;
            this.lbRow.Visible = false;
            // 
            // UCC_lbcouponType
            // 
            this.UCC_lbcouponType.AutoSize = true;
            this.UCC_lbcouponType.Location = new System.Drawing.Point(409, 39);
            this.UCC_lbcouponType.Name = "UCC_lbcouponType";
            this.UCC_lbcouponType.Size = new System.Drawing.Size(27, 13);
            this.UCC_lbcouponType.TabIndex = 64;
            this.UCC_lbcouponType.Text = "type";
            this.UCC_lbcouponType.Visible = false;
            // 
            // UCCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.UCC_lbcouponType);
            this.Controls.Add(this.lbRow);
            this.Controls.Add(this.paymentCode);
            this.Controls.Add(this.picBin);
            this.Controls.Add(this.lbProductCode);
            this.Controls.Add(this.lbQty);
            this.Controls.Add(this.lbCouponValue);
            this.Controls.Add(this.lbCouponNo);
            this.Controls.Add(this.lbNo);
            this.DoubleBuffered = true;
            this.Name = "UCCoupon";
            this.Size = new System.Drawing.Size(620, 51);
            this.Load += new System.EventHandler(this.UCCoupon_Load);
            this.Click += new System.EventHandler(this.UCCoupon_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picBin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbNo;
        public System.Windows.Forms.Label lbCouponNo;
        public System.Windows.Forms.Label lbCouponValue;
        public System.Windows.Forms.Label lbQty;
        public System.Windows.Forms.Label lbProductCode;
        private System.Windows.Forms.PictureBox picBin;
        public System.Windows.Forms.Label paymentCode;
        public System.Windows.Forms.Label lbRow;
        public System.Windows.Forms.Label UCC_lbcouponType;
    }
}
