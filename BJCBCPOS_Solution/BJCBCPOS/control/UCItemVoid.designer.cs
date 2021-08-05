namespace BJCBCPOS
{
    partial class UCItemVoid
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
            this.lbUC_Payment = new System.Windows.Forms.Label();
            this.lbUC_No = new System.Windows.Forms.Label();
            this.lbUC_Price = new System.Windows.Forms.Label();
            this.lbUC_PriceCurrency = new System.Windows.Forms.Label();
            this.picUnderLine = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picUnderLine)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUC_Payment
            // 
            this.lbUC_Payment.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_Payment.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_Payment.ForeColor = System.Drawing.Color.Black;
            this.lbUC_Payment.Location = new System.Drawing.Point(2, 4);
            this.lbUC_Payment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_Payment.Name = "lbUC_Payment";
            this.lbUC_Payment.Size = new System.Drawing.Size(210, 20);
            this.lbUC_Payment.TabIndex = 56;
            this.lbUC_Payment.Text = "HPAEHPAE000000XX99987";
            this.lbUC_Payment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUC_Payment.Click += new System.EventHandler(this.lbProductCode_Click);
            // 
            // lbUC_No
            // 
            this.lbUC_No.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_No.ForeColor = System.Drawing.Color.Black;
            this.lbUC_No.Location = new System.Drawing.Point(124, 6);
            this.lbUC_No.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_No.Name = "lbUC_No";
            this.lbUC_No.Size = new System.Drawing.Size(24, 20);
            this.lbUC_No.TabIndex = 61;
            this.lbUC_No.Text = "1";
            this.lbUC_No.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbUC_No.Visible = false;
            // 
            // lbUC_Price
            // 
            this.lbUC_Price.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbUC_Price.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_Price.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_Price.ForeColor = System.Drawing.Color.Black;
            this.lbUC_Price.Location = new System.Drawing.Point(218, 4);
            this.lbUC_Price.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_Price.Name = "lbUC_Price";
            this.lbUC_Price.Size = new System.Drawing.Size(92, 20);
            this.lbUC_Price.TabIndex = 58;
            this.lbUC_Price.Text = "998,888.00";
            this.lbUC_Price.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbUC_Price.Click += new System.EventHandler(this.lbPrice_Click);
            // 
            // lbUC_PriceCurrency
            // 
            this.lbUC_PriceCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lbUC_PriceCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUC_PriceCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbUC_PriceCurrency.Location = new System.Drawing.Point(10, 30);
            this.lbUC_PriceCurrency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUC_PriceCurrency.Name = "lbUC_PriceCurrency";
            this.lbUC_PriceCurrency.Size = new System.Drawing.Size(289, 20);
            this.lbUC_PriceCurrency.TabIndex = 62;
            this.lbUC_PriceCurrency.Text = "1234567890";
            this.lbUC_PriceCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picUnderLine
            // 
            this.picUnderLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.picUnderLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.picUnderLine.Location = new System.Drawing.Point(0, 29);
            this.picUnderLine.Name = "picUnderLine";
            this.picUnderLine.Size = new System.Drawing.Size(315, 1);
            this.picUnderLine.TabIndex = 63;
            this.picUnderLine.TabStop = false;
            // 
            // UCItemVoid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbUC_Payment);
            this.Controls.Add(this.lbUC_Price);
            this.Controls.Add(this.picUnderLine);
            this.Controls.Add(this.lbUC_PriceCurrency);
            this.Controls.Add(this.lbUC_No);
            this.DoubleBuffered = true;
            this.Name = "UCItemVoid";
            this.Size = new System.Drawing.Size(315, 30);
            this.Load += new System.EventHandler(this.UCItemVoid_Load);
            this.Click += new System.EventHandler(this.UCItemVoid_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picUnderLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbUC_Payment;
        public System.Windows.Forms.Label lbUC_No;
        public System.Windows.Forms.Label lbUC_Price;
        public System.Windows.Forms.Label lbUC_PriceCurrency;
        public System.Windows.Forms.PictureBox picUnderLine;
    }
}
