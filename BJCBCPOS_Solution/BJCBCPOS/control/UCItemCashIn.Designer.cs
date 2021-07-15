namespace BJCBCPOS
{
    partial class UCItemCashIn
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
            this.lbUCNo = new System.Windows.Forms.Label();
            this.lbUCChange = new System.Windows.Forms.Label();
            this.lbUCChangeType = new System.Windows.Forms.Label();
            this.lbUCCurrency = new System.Windows.Forms.Label();
            this.lbExchangeRate = new System.Windows.Forms.Label();
            this.lbValue = new System.Windows.Forms.Label();
            this.UCItemInputAmt = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UCItemCashType = new System.Windows.Forms.Label();
            this.lbInputDisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUCNo
            // 
            this.lbUCNo.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbUCNo.Location = new System.Drawing.Point(2, 8);
            this.lbUCNo.Name = "lbUCNo";
            this.lbUCNo.Size = new System.Drawing.Size(25, 23);
            this.lbUCNo.TabIndex = 0;
            this.lbUCNo.Text = "99";
            this.lbUCNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbUCNo.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbUCChange
            // 
            this.lbUCChange.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUCChange.Location = new System.Drawing.Point(31, 8);
            this.lbUCChange.Name = "lbUCChange";
            this.lbUCChange.Size = new System.Drawing.Size(155, 23);
            this.lbUCChange.TabIndex = 1;
            this.lbUCChange.Text = "รับเงินทอนแบบปกติ";
            this.lbUCChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbUCChangeType
            // 
            this.lbUCChangeType.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUCChangeType.Location = new System.Drawing.Point(185, 8);
            this.lbUCChangeType.Name = "lbUCChangeType";
            this.lbUCChangeType.Size = new System.Drawing.Size(47, 23);
            this.lbUCChangeType.TabIndex = 2;
            this.lbUCChangeType.Text = "FXCU";
            this.lbUCChangeType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbUCChangeType.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbUCCurrency
            // 
            this.lbUCCurrency.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUCCurrency.Location = new System.Drawing.Point(255, 8);
            this.lbUCCurrency.Name = "lbUCCurrency";
            this.lbUCCurrency.Size = new System.Drawing.Size(45, 23);
            this.lbUCCurrency.TabIndex = 3;
            this.lbUCCurrency.Text = "THB";
            this.lbUCCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbUCCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbExchangeRate
            // 
            this.lbExchangeRate.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbExchangeRate.Location = new System.Drawing.Point(326, 8);
            this.lbExchangeRate.Name = "lbExchangeRate";
            this.lbExchangeRate.Size = new System.Drawing.Size(89, 23);
            this.lbExchangeRate.TabIndex = 4;
            this.lbExchangeRate.Text = "0.00000234";
            this.lbExchangeRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbExchangeRate.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbValue
            // 
            this.lbValue.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbValue.Location = new System.Drawing.Point(532, 8);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(92, 23);
            this.lbValue.TabIndex = 5;
            this.lbValue.Text = "800,000.00";
            this.lbValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbValue.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // UCItemInputAmt
            // 
            this.UCItemInputAmt.AutoSize = true;
            this.UCItemInputAmt.Location = new System.Drawing.Point(510, 24);
            this.UCItemInputAmt.Name = "UCItemInputAmt";
            this.UCItemInputAmt.Size = new System.Drawing.Size(84, 13);
            this.UCItemInputAmt.TabIndex = 8;
            this.UCItemInputAmt.Text = "UCItemInputAmt";
            this.UCItemInputAmt.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_trash_can_100;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(628, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.pictureBox1.Location = new System.Drawing.Point(0, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(675, 1);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // UCItemCashType
            // 
            this.UCItemCashType.AutoSize = true;
            this.UCItemCashType.Location = new System.Drawing.Point(157, 21);
            this.UCItemCashType.Name = "UCItemCashType";
            this.UCItemCashType.Size = new System.Drawing.Size(90, 13);
            this.UCItemCashType.TabIndex = 9;
            this.UCItemCashType.Text = "UCItemCashType";
            this.UCItemCashType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UCItemCashType.Visible = false;
            // 
            // lbInputDisplay
            // 
            this.lbInputDisplay.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbInputDisplay.Location = new System.Drawing.Point(420, 8);
            this.lbInputDisplay.Name = "lbInputDisplay";
            this.lbInputDisplay.Size = new System.Drawing.Size(108, 23);
            this.lbInputDisplay.TabIndex = 10;
            this.lbInputDisplay.Text = "80,800,000.00";
            this.lbInputDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbInputDisplay.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // UCItemCashIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lbInputDisplay);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.lbUCChangeType);
            this.Controls.Add(this.UCItemCashType);
            this.Controls.Add(this.UCItemInputAmt);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbExchangeRate);
            this.Controls.Add(this.lbUCCurrency);
            this.Controls.Add(this.lbUCChange);
            this.Controls.Add(this.lbUCNo);
            this.DoubleBuffered = true;
            this.Name = "UCItemCashIn";
            this.Size = new System.Drawing.Size(670, 37);
            this.Load += new System.EventHandler(this.UCItemCashIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUCNo;
        private System.Windows.Forms.Label lbUCChange;
        private System.Windows.Forms.Label lbUCChangeType;
        private System.Windows.Forms.Label lbUCCurrency;
        private System.Windows.Forms.Label lbExchangeRate;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label UCItemInputAmt;
        private System.Windows.Forms.Label UCItemCashType;
        private System.Windows.Forms.Label lbInputDisplay;
    }
}
