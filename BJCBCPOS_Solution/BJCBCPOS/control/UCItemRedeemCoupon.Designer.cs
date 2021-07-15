namespace BJCBCPOS
{
    partial class UCItemRedeemCoupon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCItemRedeemCoupon));
            this.lb_Seq = new System.Windows.Forms.Label();
            this.lbCoupon_SumCouponVal = new System.Windows.Forms.Label();
            this.lbCoupon_SumPoint = new System.Windows.Forms.Label();
            this.lbCoupon_UsePoint = new System.Windows.Forms.Label();
            this.lbCoupon_CouponVal = new System.Windows.Forms.Label();
            this.lbCoupon_ItemName = new System.Windows.Forms.Label();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.ucTextBoxSmall1 = new BJCBCPOS.UCTextBoxSmall();
            this.lbLimit = new System.Windows.Forms.Label();
            this.lbInputQty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_Seq
            // 
            this.lb_Seq.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Seq.Location = new System.Drawing.Point(2, 3);
            this.lb_Seq.Name = "lb_Seq";
            this.lb_Seq.Size = new System.Drawing.Size(33, 70);
            this.lb_Seq.TabIndex = 1;
            this.lb_Seq.Text = "999";
            this.lb_Seq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCoupon_SumCouponVal
            // 
            this.lbCoupon_SumCouponVal.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCoupon_SumCouponVal.Location = new System.Drawing.Point(573, 3);
            this.lbCoupon_SumCouponVal.Name = "lbCoupon_SumCouponVal";
            this.lbCoupon_SumCouponVal.Size = new System.Drawing.Size(93, 70);
            this.lbCoupon_SumCouponVal.TabIndex = 11;
            this.lbCoupon_SumCouponVal.Text = "8,888,888.00";
            this.lbCoupon_SumCouponVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCoupon_SumPoint
            // 
            this.lbCoupon_SumPoint.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCoupon_SumPoint.Location = new System.Drawing.Point(495, 3);
            this.lbCoupon_SumPoint.Name = "lbCoupon_SumPoint";
            this.lbCoupon_SumPoint.Size = new System.Drawing.Size(73, 70);
            this.lbCoupon_SumPoint.TabIndex = 10;
            this.lbCoupon_SumPoint.Text = "8,888,888";
            this.lbCoupon_SumPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCoupon_UsePoint
            // 
            this.lbCoupon_UsePoint.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCoupon_UsePoint.Location = new System.Drawing.Point(290, 2);
            this.lbCoupon_UsePoint.Name = "lbCoupon_UsePoint";
            this.lbCoupon_UsePoint.Size = new System.Drawing.Size(73, 70);
            this.lbCoupon_UsePoint.TabIndex = 9;
            this.lbCoupon_UsePoint.Text = "8,888,888";
            this.lbCoupon_UsePoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCoupon_CouponVal
            // 
            this.lbCoupon_CouponVal.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCoupon_CouponVal.Location = new System.Drawing.Point(191, 2);
            this.lbCoupon_CouponVal.Name = "lbCoupon_CouponVal";
            this.lbCoupon_CouponVal.Size = new System.Drawing.Size(93, 70);
            this.lbCoupon_CouponVal.TabIndex = 8;
            this.lbCoupon_CouponVal.Text = "8,888,888.00";
            this.lbCoupon_CouponVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCoupon_ItemName
            // 
            this.lbCoupon_ItemName.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCoupon_ItemName.Location = new System.Drawing.Point(41, 0);
            this.lbCoupon_ItemName.Name = "lbCoupon_ItemName";
            this.lbCoupon_ItemName.Size = new System.Drawing.Size(144, 75);
            this.lbCoupon_ItemName.TabIndex = 7;
            this.lbCoupon_ItemName.Text = "ใช้ 1,000 คะแนน + เงิน 99 บาท แลกซื้อ ฉัตรอรุณข้าว หอม 5กก";
            this.lbCoupon_ItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.Transparent;
            this.btnPlus.BackgroundImage = global::BJCBCPOS.Properties.Resources.plus;
            this.btnPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Location = new System.Drawing.Point(455, 21);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(38, 34);
            this.btnPlus.TabIndex = 18;
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.BackColor = System.Drawing.Color.Transparent;
            this.btnMinus.BackgroundImage = global::BJCBCPOS.Properties.Resources.minus;
            this.btnMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinus.FlatAppearance.BorderSize = 0;
            this.btnMinus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Location = new System.Drawing.Point(361, 22);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 33);
            this.btnMinus.TabIndex = 19;
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // ucTextBoxSmall1
            // 
            this.ucTextBoxSmall1.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTextBoxSmall1.BackgroundImage")));
            this.ucTextBoxSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxSmall1.Location = new System.Drawing.Point(402, 21);
            this.ucTextBoxSmall1.Name = "ucTextBoxSmall1";
            this.ucTextBoxSmall1.Size = new System.Drawing.Size(49, 36);
            this.ucTextBoxSmall1.TabIndex = 20;
            this.ucTextBoxSmall1.Text = "0";
            this.ucTextBoxSmall1.EnterFromButton += new System.EventHandler(this.ucTextBoxSmall1_EnterFromButton);
            this.ucTextBoxSmall1.TextBoxTextChange += new System.EventHandler(this.ucTextBoxSmall1_TextBoxTextChange);
            // 
            // lbLimit
            // 
            this.lbLimit.AutoSize = true;
            this.lbLimit.Location = new System.Drawing.Point(407, 61);
            this.lbLimit.Name = "lbLimit";
            this.lbLimit.Size = new System.Drawing.Size(24, 13);
            this.lbLimit.TabIndex = 21;
            this.lbLimit.Text = "limit";
            this.lbLimit.Visible = false;
            // 
            // lbInputQty
            // 
            this.lbInputQty.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbInputQty.Location = new System.Drawing.Point(402, 21);
            this.lbInputQty.Name = "lbInputQty";
            this.lbInputQty.Size = new System.Drawing.Size(49, 36);
            this.lbInputQty.TabIndex = 28;
            this.lbInputQty.Text = "0";
            this.lbInputQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCItemRedeemCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbLimit);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.lbCoupon_SumCouponVal);
            this.Controls.Add(this.lbCoupon_SumPoint);
            this.Controls.Add(this.lbCoupon_UsePoint);
            this.Controls.Add(this.lbCoupon_CouponVal);
            this.Controls.Add(this.lbCoupon_ItemName);
            this.Controls.Add(this.lb_Seq);
            this.Controls.Add(this.ucTextBoxSmall1);
            this.Controls.Add(this.lbInputQty);
            this.Name = "UCItemRedeemCoupon";
            this.Size = new System.Drawing.Size(680, 75);
            this.Load += new System.EventHandler(this.UCItemRedeemCoupon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_Seq;
        private System.Windows.Forms.Label lbCoupon_SumCouponVal;
        private System.Windows.Forms.Label lbCoupon_SumPoint;
        private System.Windows.Forms.Label lbCoupon_UsePoint;
        private System.Windows.Forms.Label lbCoupon_CouponVal;
        private System.Windows.Forms.Label lbCoupon_ItemName;
        public System.Windows.Forms.Button btnPlus;
        public System.Windows.Forms.Button btnMinus;
        public UCTextBoxSmall ucTextBoxSmall1;
        private System.Windows.Forms.Label lbLimit;
        public System.Windows.Forms.Label lbInputQty;
    }
}
