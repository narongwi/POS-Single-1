namespace BJCBCPOS
{
    partial class UCConfirmPaymentFXCU
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
            this.lbNameCurrency = new System.Windows.Forms.Label();
            this.lbExchangeRate = new System.Windows.Forms.Label();
            this.lbChangeCurrency = new System.Windows.Forms.Label();
            this.lbCurrency = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbNameCurrency
            // 
            this.lbNameCurrency.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbNameCurrency.Location = new System.Drawing.Point(5, 4);
            this.lbNameCurrency.Name = "lbNameCurrency";
            this.lbNameCurrency.Size = new System.Drawing.Size(229, 40);
            this.lbNameCurrency.TabIndex = 23;
            this.lbNameCurrency.Text = "US Dollar";
            this.lbNameCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbExchangeRate
            // 
            this.lbExchangeRate.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExchangeRate.ForeColor = System.Drawing.Color.Black;
            this.lbExchangeRate.Location = new System.Drawing.Point(5, 47);
            this.lbExchangeRate.Name = "lbExchangeRate";
            this.lbExchangeRate.Size = new System.Drawing.Size(229, 20);
            this.lbExchangeRate.TabIndex = 24;
            this.lbExchangeRate.Text = "Exchange Rate : 0.000234";
            this.lbExchangeRate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lbExchangeRate.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbChangeCurrency
            // 
            this.lbChangeCurrency.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChangeCurrency.ForeColor = System.Drawing.Color.Red;
            this.lbChangeCurrency.Location = new System.Drawing.Point(237, 1);
            this.lbChangeCurrency.Margin = new System.Windows.Forms.Padding(0);
            this.lbChangeCurrency.Name = "lbChangeCurrency";
            this.lbChangeCurrency.Size = new System.Drawing.Size(242, 73);
            this.lbChangeCurrency.TabIndex = 25;
            this.lbChangeCurrency.Text = "88,888,888.00";
            this.lbChangeCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbChangeCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbCurrency
            // 
            this.lbCurrency.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbCurrency.Location = new System.Drawing.Point(475, 0);
            this.lbCurrency.Margin = new System.Windows.Forms.Padding(0);
            this.lbCurrency.Name = "lbCurrency";
            this.lbCurrency.Size = new System.Drawing.Size(85, 75);
            this.lbCurrency.TabIndex = 26;
            this.lbCurrency.Text = "USD";
            this.lbCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // UCConfirmPaymentFXCU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbCurrency);
            this.Controls.Add(this.lbChangeCurrency);
            this.Controls.Add(this.lbExchangeRate);
            this.Controls.Add(this.lbNameCurrency);
            this.Name = "UCConfirmPaymentFXCU";
            this.Size = new System.Drawing.Size(560, 75);
            this.Load += new System.EventHandler(this.UCConfirmPaymentFXUC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbNameCurrency;
        public System.Windows.Forms.Label lbExchangeRate;
        public System.Windows.Forms.Label lbChangeCurrency;
        public System.Windows.Forms.Label lbCurrency;
    }
}
