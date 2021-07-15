namespace BJCBCPOS
{
    partial class UCItemRedeemDiscountCust
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
            this.lbName = new System.Windows.Forms.Label();
            this.lb_Seq = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lbPointUse = new System.Windows.Forms.Label();
            this.lbRateUse = new System.Windows.Forms.Label();
            this.lbRate = new System.Windows.Forms.Label();
            this.btnYesNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.Font = new System.Drawing.Font("Prompt", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbName.Location = new System.Drawing.Point(37, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(86, 90);
            this.lbName.TabIndex = 5;
            this.lbName.Text = "ใช้ 1,000 คะแนน + เงิน 99 บาท แลกซื้อ ฉัตรอรุณข้าว หอม 5กก เทส เทส เทส เทส";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Seq
            // 
            this.lb_Seq.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Seq.Location = new System.Drawing.Point(0, 1);
            this.lb_Seq.Name = "lb_Seq";
            this.lb_Seq.Size = new System.Drawing.Size(33, 89);
            this.lb_Seq.TabIndex = 6;
            this.lb_Seq.Text = "999";
            this.lb_Seq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPrice
            // 
            this.lbPrice.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPrice.Location = new System.Drawing.Point(124, 0);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(93, 90);
            this.lbPrice.TabIndex = 7;
            this.lbPrice.Text = "8,888,888.00";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPointUse
            // 
            this.lbPointUse.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPointUse.Location = new System.Drawing.Point(218, 0);
            this.lbPointUse.Name = "lbPointUse";
            this.lbPointUse.Size = new System.Drawing.Size(75, 90);
            this.lbPointUse.TabIndex = 8;
            this.lbPointUse.Text = "8,888,888";
            this.lbPointUse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbRateUse
            // 
            this.lbRateUse.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbRateUse.Location = new System.Drawing.Point(294, 0);
            this.lbRateUse.Name = "lbRateUse";
            this.lbRateUse.Size = new System.Drawing.Size(93, 90);
            this.lbRateUse.TabIndex = 9;
            this.lbRateUse.Text = "8,888,888.00";
            this.lbRateUse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbRate
            // 
            this.lbRate.Font = new System.Drawing.Font("Prompt", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbRate.Location = new System.Drawing.Point(388, 0);
            this.lbRate.Name = "lbRate";
            this.lbRate.Size = new System.Drawing.Size(39, 90);
            this.lbRate.TabIndex = 10;
            this.lbRate.Text = "100%";
            this.lbRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnYesNo
            // 
            this.btnYesNo.BackColor = System.Drawing.Color.Transparent;
            this.btnYesNo.BackgroundImage = global::BJCBCPOS.Properties.Resources.redeemDiscountDisable;
            this.btnYesNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnYesNo.FlatAppearance.BorderSize = 0;
            this.btnYesNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesNo.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnYesNo.Location = new System.Drawing.Point(429, 17);
            this.btnYesNo.Name = "btnYesNo";
            this.btnYesNo.Size = new System.Drawing.Size(60, 55);
            this.btnYesNo.TabIndex = 11;
            this.btnYesNo.Text = "N";
            this.btnYesNo.UseVisualStyleBackColor = false;
            this.btnYesNo.Click += new System.EventHandler(this.btnYesNo_Click);
            // 
            // UCItemRedeemDiscountCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnYesNo);
            this.Controls.Add(this.lbRate);
            this.Controls.Add(this.lbRateUse);
            this.Controls.Add(this.lbPointUse);
            this.Controls.Add(this.lbPrice);
            this.Controls.Add(this.lb_Seq);
            this.Controls.Add(this.lbName);
            this.Name = "UCItemRedeemDiscountCust";
            this.Size = new System.Drawing.Size(495, 90);
            this.Load += new System.EventHandler(this.UCItemRedeemDiscount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbName;
        public System.Windows.Forms.Label lb_Seq;
        public System.Windows.Forms.Label lbPrice;
        public System.Windows.Forms.Label lbPointUse;
        public System.Windows.Forms.Label lbRateUse;
        public System.Windows.Forms.Label lbRate;
        public System.Windows.Forms.Button btnYesNo;
    }
}
