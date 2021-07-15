namespace BJCBCPOS
{
    partial class UCListDiscount
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
            this.lbTxtName = new System.Windows.Forms.Label();
            this.lbTxtPrice = new System.Windows.Forms.Label();
            this.lbTxtAmt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTxtName
            // 
            this.lbTxtName.AutoSize = true;
            this.lbTxtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtName.ForeColor = System.Drawing.Color.Gray;
            this.lbTxtName.Location = new System.Drawing.Point(3, 5);
            this.lbTxtName.Name = "lbTxtName";
            this.lbTxtName.Size = new System.Drawing.Size(84, 20);
            this.lbTxtName.TabIndex = 0;
            this.lbTxtName.Text = "lbTxtName";
            // 
            // lbTxtPrice
            // 
            this.lbTxtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtPrice.ForeColor = System.Drawing.Color.Gray;
            this.lbTxtPrice.Location = new System.Drawing.Point(162, 5);
            this.lbTxtPrice.Name = "lbTxtPrice";
            this.lbTxtPrice.Size = new System.Drawing.Size(147, 20);
            this.lbTxtPrice.TabIndex = 1;
            this.lbTxtPrice.Text = "lbTxtPrice";
            this.lbTxtPrice.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbTxtAmt
            // 
            this.lbTxtAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtAmt.ForeColor = System.Drawing.Color.Gray;
            this.lbTxtAmt.Location = new System.Drawing.Point(341, 5);
            this.lbTxtAmt.Name = "lbTxtAmt";
            this.lbTxtAmt.Size = new System.Drawing.Size(100, 20);
            this.lbTxtAmt.TabIndex = 2;
            this.lbTxtAmt.Text = "lbTxtAmt";
            this.lbTxtAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UCListDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lbTxtAmt);
            this.Controls.Add(this.lbTxtPrice);
            this.Controls.Add(this.lbTxtName);
            this.DoubleBuffered = true;
            this.Name = "UCListDiscount";
            this.Size = new System.Drawing.Size(444, 30);
            this.Load += new System.EventHandler(this.UCListDiscount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbTxtName;
        public System.Windows.Forms.Label lbTxtPrice;
        public System.Windows.Forms.Label lbTxtAmt;

    }
}
