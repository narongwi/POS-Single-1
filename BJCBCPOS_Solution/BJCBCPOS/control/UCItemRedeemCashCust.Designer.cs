namespace BJCBCPOS
{
    partial class UCItemRedeemCashCust
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
            this.lbCashCust_PointToRedeem = new System.Windows.Forms.Label();
            this.lb_Seq = new System.Windows.Forms.Label();
            this.lbCashCust_RedeemAmt = new System.Windows.Forms.Label();
            this.lbCashCust_UsePoint = new System.Windows.Forms.Label();
            this.lbCashCust_Discount = new System.Windows.Forms.Label();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.lb_QTY = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbCashCust_PointToRedeem
            // 
            this.lbCashCust_PointToRedeem.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCashCust_PointToRedeem.Location = new System.Drawing.Point(41, 1);
            this.lbCashCust_PointToRedeem.Name = "lbCashCust_PointToRedeem";
            this.lbCashCust_PointToRedeem.Size = new System.Drawing.Size(63, 69);
            this.lbCashCust_PointToRedeem.TabIndex = 9;
            this.lbCashCust_PointToRedeem.Text = "888,888";
            this.lbCashCust_PointToRedeem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Seq
            // 
            this.lb_Seq.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Seq.Location = new System.Drawing.Point(2, 1);
            this.lb_Seq.Name = "lb_Seq";
            this.lb_Seq.Size = new System.Drawing.Size(33, 69);
            this.lb_Seq.TabIndex = 10;
            this.lb_Seq.Text = "999";
            this.lb_Seq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCashCust_RedeemAmt
            // 
            this.lbCashCust_RedeemAmt.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCashCust_RedeemAmt.Location = new System.Drawing.Point(107, 1);
            this.lbCashCust_RedeemAmt.Name = "lbCashCust_RedeemAmt";
            this.lbCashCust_RedeemAmt.Size = new System.Drawing.Size(73, 69);
            this.lbCashCust_RedeemAmt.TabIndex = 11;
            this.lbCashCust_RedeemAmt.Text = "8,888,888";
            this.lbCashCust_RedeemAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCashCust_UsePoint
            // 
            this.lbCashCust_UsePoint.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCashCust_UsePoint.Location = new System.Drawing.Point(321, 1);
            this.lbCashCust_UsePoint.Name = "lbCashCust_UsePoint";
            this.lbCashCust_UsePoint.Size = new System.Drawing.Size(63, 69);
            this.lbCashCust_UsePoint.TabIndex = 12;
            this.lbCashCust_UsePoint.Text = "888,888";
            this.lbCashCust_UsePoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCashCust_Discount
            // 
            this.lbCashCust_Discount.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCashCust_Discount.Location = new System.Drawing.Point(387, 1);
            this.lbCashCust_Discount.Name = "lbCashCust_Discount";
            this.lbCashCust_Discount.Size = new System.Drawing.Size(95, 69);
            this.lbCashCust_Discount.TabIndex = 13;
            this.lbCashCust_Discount.Text = "8,888,888.00";
            this.lbCashCust_Discount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.Transparent;
            this.btnPlus.BackgroundImage = global::BJCBCPOS.Properties.Resources.plus;
            this.btnPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Location = new System.Drawing.Point(283, 23);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(30, 25);
            this.btnPlus.TabIndex = 15;
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
            this.btnMinus.Location = new System.Drawing.Point(184, 23);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(35, 25);
            this.btnMinus.TabIndex = 16;
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // lb_QTY
            // 
            this.lb_QTY.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_QTY.Location = new System.Drawing.Point(222, 17);
            this.lb_QTY.Margin = new System.Windows.Forms.Padding(0);
            this.lb_QTY.Name = "lb_QTY";
            this.lb_QTY.Size = new System.Drawing.Size(57, 40);
            this.lb_QTY.TabIndex = 17;
            this.lb_QTY.Text = "0";
            this.lb_QTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCItemRedeemCashCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.lbCashCust_Discount);
            this.Controls.Add(this.lbCashCust_UsePoint);
            this.Controls.Add(this.lbCashCust_RedeemAmt);
            this.Controls.Add(this.lb_Seq);
            this.Controls.Add(this.lbCashCust_PointToRedeem);
            this.Controls.Add(this.lb_QTY);
            this.Name = "UCItemRedeemCashCust";
            this.Size = new System.Drawing.Size(495, 71);
            this.Load += new System.EventHandler(this.UCItemRedeemCashCust_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbCashCust_PointToRedeem;
        public System.Windows.Forms.Label lb_Seq;
        public System.Windows.Forms.Label lbCashCust_RedeemAmt;
        public System.Windows.Forms.Label lbCashCust_UsePoint;
        public System.Windows.Forms.Label lbCashCust_Discount;
        public System.Windows.Forms.Button btnPlus;
        public System.Windows.Forms.Button btnMinus;
        public System.Windows.Forms.Label lb_QTY;
    }
}
