namespace BJCBCPOS
{
    partial class UCListItemSaveTempSale
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
            this.lbRefTxt = new System.Windows.Forms.Label();
            this.lbRefVal = new System.Windows.Forms.Label();
            this.lbDateTime = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lbRefTxt
            // 
            this.lbRefTxt.AutoSize = true;
            this.lbRefTxt.BackColor = System.Drawing.Color.White;
            this.lbRefTxt.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbRefTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lbRefTxt.Location = new System.Drawing.Point(3, 3);
            this.lbRefTxt.Name = "lbRefTxt";
            this.lbRefTxt.Size = new System.Drawing.Size(113, 22);
            this.lbRefTxt.TabIndex = 0;
            this.lbRefTxt.Text = "Reference No.";
            this.lbRefTxt.Click += new System.EventHandler(this.Item_Click);
            // 
            // lbRefVal
            // 
            this.lbRefVal.BackColor = System.Drawing.Color.White;
            this.lbRefVal.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbRefVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lbRefVal.Location = new System.Drawing.Point(140, 3);
            this.lbRefVal.Name = "lbRefVal";
            this.lbRefVal.Size = new System.Drawing.Size(164, 44);
            this.lbRefVal.TabIndex = 1;
            this.lbRefVal.Text = "H00300002";
            this.lbRefVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbRefVal.Click += new System.EventHandler(this.Item_Click);
            // 
            // lbDateTime
            // 
            this.lbDateTime.AutoSize = true;
            this.lbDateTime.BackColor = System.Drawing.Color.White;
            this.lbDateTime.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDateTime.ForeColor = System.Drawing.Color.Gray;
            this.lbDateTime.Location = new System.Drawing.Point(3, 29);
            this.lbDateTime.Name = "lbDateTime";
            this.lbDateTime.Size = new System.Drawing.Size(131, 19);
            this.lbDateTime.TabIndex = 2;
            this.lbDateTime.Text = "24/5/2564 14:05:24";
            this.lbDateTime.Click += new System.EventHandler(this.Item_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox2.Location = new System.Drawing.Point(0, 49);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(308, 1);
            this.pictureBox2.TabIndex = 140;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.Item_Click);
            // 
            // UCListItemSaveTempSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbRefVal);
            this.Controls.Add(this.lbDateTime);
            this.Controls.Add(this.lbRefTxt);
            this.Name = "UCListItemSaveTempSale";
            this.Size = new System.Drawing.Size(308, 50);
            this.Load += new System.EventHandler(this.UCListItemSaveTempSale_Load);
            this.Click += new System.EventHandler(this.Item_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbRefTxt;
        public System.Windows.Forms.Label lbDateTime;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label lbRefVal;
    }
}
