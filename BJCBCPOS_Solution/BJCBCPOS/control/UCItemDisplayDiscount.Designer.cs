namespace BJCBCPOS
{
    partial class UCItemDisplayDiscount
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
            this.lbItemName = new System.Windows.Forms.Label();
            this.lbDisAmt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbItemName
            // 
            this.lbItemName.BackColor = System.Drawing.Color.Transparent;
            this.lbItemName.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbItemName.ForeColor = System.Drawing.Color.Black;
            this.lbItemName.Location = new System.Drawing.Point(1, 2);
            this.lbItemName.Name = "lbItemName";
            this.lbItemName.Size = new System.Drawing.Size(229, 25);
            this.lbItemName.TabIndex = 0;
            this.lbItemName.Text = "รายการ";
            this.lbItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDisAmt
            // 
            this.lbDisAmt.BackColor = System.Drawing.Color.Transparent;
            this.lbDisAmt.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDisAmt.ForeColor = System.Drawing.Color.Black;
            this.lbDisAmt.Location = new System.Drawing.Point(232, 2);
            this.lbDisAmt.Name = "lbDisAmt";
            this.lbDisAmt.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lbDisAmt.Size = new System.Drawing.Size(127, 25);
            this.lbDisAmt.TabIndex = 1;
            this.lbDisAmt.Text = "ส่วนลด";
            this.lbDisAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCItemDisplayDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbDisAmt);
            this.Controls.Add(this.lbItemName);
            this.Name = "UCItemDisplayDiscount";
            this.Size = new System.Drawing.Size(360, 30);
            this.Load += new System.EventHandler(this.UCItemDisplayDiscount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbItemName;
        public System.Windows.Forms.Label lbDisAmt;
    }
}
