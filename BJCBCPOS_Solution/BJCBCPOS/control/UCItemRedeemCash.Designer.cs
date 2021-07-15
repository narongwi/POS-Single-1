namespace BJCBCPOS
{
    partial class UCItemRedeemCash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCItemRedeemCash));
            this.lbTypeName = new System.Windows.Forms.Label();
            this.lbCash_Discount = new System.Windows.Forms.Label();
            this.lbCash_UsePoint = new System.Windows.Forms.Label();
            this.lbCash_RedeemAmt = new System.Windows.Forms.Label();
            this.lb_Seq = new System.Windows.Forms.Label();
            this.lbCash_PointToRedeem = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.ucTextBoxSmall1 = new BJCBCPOS.UCTextBoxSmall();
            this.lbInputQty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTypeName
            // 
            this.lbTypeName.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTypeName.Location = new System.Drawing.Point(556, 2);
            this.lbTypeName.Name = "lbTypeName";
            this.lbTypeName.Size = new System.Drawing.Size(122, 73);
            this.lbTypeName.TabIndex = 23;
            this.lbTypeName.Text = "แลกคะแนนแทนเงินสด";
            this.lbTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCash_Discount
            // 
            this.lbCash_Discount.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCash_Discount.Location = new System.Drawing.Point(446, 2);
            this.lbCash_Discount.Name = "lbCash_Discount";
            this.lbCash_Discount.Size = new System.Drawing.Size(104, 73);
            this.lbCash_Discount.TabIndex = 22;
            this.lbCash_Discount.Text = "8,888,888.00";
            this.lbCash_Discount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCash_UsePoint
            // 
            this.lbCash_UsePoint.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCash_UsePoint.Location = new System.Drawing.Point(359, 2);
            this.lbCash_UsePoint.Name = "lbCash_UsePoint";
            this.lbCash_UsePoint.Size = new System.Drawing.Size(81, 73);
            this.lbCash_UsePoint.TabIndex = 21;
            this.lbCash_UsePoint.Text = "8,888,888";
            this.lbCash_UsePoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCash_RedeemAmt
            // 
            this.lbCash_RedeemAmt.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCash_RedeemAmt.Location = new System.Drawing.Point(129, 2);
            this.lbCash_RedeemAmt.Name = "lbCash_RedeemAmt";
            this.lbCash_RedeemAmt.Size = new System.Drawing.Size(87, 73);
            this.lbCash_RedeemAmt.TabIndex = 20;
            this.lbCash_RedeemAmt.Text = "8,888,888";
            this.lbCash_RedeemAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Seq
            // 
            this.lb_Seq.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Seq.Location = new System.Drawing.Point(1, 2);
            this.lb_Seq.Name = "lb_Seq";
            this.lb_Seq.Size = new System.Drawing.Size(37, 73);
            this.lb_Seq.TabIndex = 19;
            this.lb_Seq.Text = "999";
            this.lb_Seq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCash_PointToRedeem
            // 
            this.lbCash_PointToRedeem.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCash_PointToRedeem.Location = new System.Drawing.Point(42, 2);
            this.lbCash_PointToRedeem.Name = "lbCash_PointToRedeem";
            this.lbCash_PointToRedeem.Size = new System.Drawing.Size(81, 73);
            this.lbCash_PointToRedeem.TabIndex = 18;
            this.lbCash_PointToRedeem.Text = "8,888,888";
            this.lbCash_PointToRedeem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMinus
            // 
            this.btnMinus.BackColor = System.Drawing.Color.Transparent;
            this.btnMinus.BackgroundImage = global::BJCBCPOS.Properties.Resources.minus;
            this.btnMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinus.FlatAppearance.BorderSize = 0;
            this.btnMinus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Location = new System.Drawing.Point(214, 22);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(43, 33);
            this.btnMinus.TabIndex = 25;
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.Transparent;
            this.btnPlus.BackgroundImage = global::BJCBCPOS.Properties.Resources.plus;
            this.btnPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Location = new System.Drawing.Point(315, 21);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(43, 33);
            this.btnPlus.TabIndex = 24;
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // ucTextBoxSmall1
            // 
            this.ucTextBoxSmall1.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTextBoxSmall1.BackgroundImage")));
            this.ucTextBoxSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxSmall1.Location = new System.Drawing.Point(261, 19);
            this.ucTextBoxSmall1.Name = "ucTextBoxSmall1";
            this.ucTextBoxSmall1.Size = new System.Drawing.Size(50, 38);
            this.ucTextBoxSmall1.TabIndex = 26;
            this.ucTextBoxSmall1.Text = "0";
            this.ucTextBoxSmall1.EnterFromButton += new System.EventHandler(this.ucTextBoxSmall1_EnterFromButton);
            this.ucTextBoxSmall1.LostFocusTextBox += new System.EventHandler(this.ucTextBoxSmall1_LostFocusTextBox);
            this.ucTextBoxSmall1.TextBoxTextChange += new System.EventHandler(this.ucTextBoxSmall1_TextBoxTextChange);
            // 
            // lbInputQty
            // 
            this.lbInputQty.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbInputQty.Location = new System.Drawing.Point(261, 19);
            this.lbInputQty.Name = "lbInputQty";
            this.lbInputQty.Size = new System.Drawing.Size(50, 38);
            this.lbInputQty.TabIndex = 27;
            this.lbInputQty.Text = "0";
            this.lbInputQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCItemRedeemCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.lbTypeName);
            this.Controls.Add(this.lbCash_Discount);
            this.Controls.Add(this.lbCash_UsePoint);
            this.Controls.Add(this.lbCash_RedeemAmt);
            this.Controls.Add(this.lb_Seq);
            this.Controls.Add(this.lbCash_PointToRedeem);
            this.Controls.Add(this.lbInputQty);
            this.Controls.Add(this.ucTextBoxSmall1);
            this.Name = "UCItemRedeemCash";
            this.Size = new System.Drawing.Size(680, 75);
            this.Load += new System.EventHandler(this.UCItemRedeemCash_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnMinus;
        public System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Label lbTypeName;
        private System.Windows.Forms.Label lbCash_Discount;
        private System.Windows.Forms.Label lbCash_UsePoint;
        public System.Windows.Forms.Label lbCash_RedeemAmt;
        public System.Windows.Forms.Label lb_Seq;
        public System.Windows.Forms.Label lbCash_PointToRedeem;
        private UCTextBoxSmall ucTextBoxSmall1;
        public System.Windows.Forms.Label lbInputQty;
    }
}
