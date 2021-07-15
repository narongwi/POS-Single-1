namespace BJCBCPOS
{
    partial class UCItemCurrency
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
            this.lbCurrency = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbRate = new System.Windows.Forms.Label();
            this.lbNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbCurrency
            // 
            this.lbCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lbCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbCurrency.Location = new System.Drawing.Point(0, 0);
            this.lbCurrency.Name = "lbCurrency";
            this.lbCurrency.Size = new System.Drawing.Size(105, 24);
            this.lbCurrency.TabIndex = 114;
            this.lbCurrency.Text = "Currency";
            this.lbCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotal
            // 
            this.lbTotal.BackColor = System.Drawing.Color.Transparent;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.Black;
            this.lbTotal.Location = new System.Drawing.Point(205, 0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(100, 24);
            this.lbTotal.TabIndex = 117;
            this.lbTotal.Text = "Total";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRate
            // 
            this.lbRate.BackColor = System.Drawing.Color.Transparent;
            this.lbRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRate.ForeColor = System.Drawing.Color.Black;
            this.lbRate.Location = new System.Drawing.Point(105, 0);
            this.lbRate.Name = "lbRate";
            this.lbRate.Size = new System.Drawing.Size(100, 24);
            this.lbRate.TabIndex = 116;
            this.lbRate.Text = "Rate";
            this.lbRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbNo
            // 
            this.lbNo.AutoSize = true;
            this.lbNo.BackColor = System.Drawing.Color.Transparent;
            this.lbNo.Location = new System.Drawing.Point(212, 10);
            this.lbNo.Name = "lbNo";
            this.lbNo.Size = new System.Drawing.Size(35, 13);
            this.lbNo.TabIndex = 118;
            this.lbNo.Text = "label1";
            this.lbNo.Visible = false;
            // 
            // UCItemCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbNo);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.lbRate);
            this.Controls.Add(this.lbCurrency);
            this.Name = "UCItemCurrency";
            this.Size = new System.Drawing.Size(306, 25);
            this.Load += new System.EventHandler(this.UCItemCurrency_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbCurrency;
        public System.Windows.Forms.Label lbTotal;
        public System.Windows.Forms.Label lbRate;
        private System.Windows.Forms.Label lbNo;
    }
}
