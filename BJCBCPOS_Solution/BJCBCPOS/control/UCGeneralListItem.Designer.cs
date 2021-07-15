namespace BJCBCPOS  
{
    partial class UCGeneralListItem
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
            this.lbTxtDesc = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTxtDesc
            // 
            this.lbTxtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTxtDesc.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtDesc.ForeColor = System.Drawing.Color.DarkGray;
            this.lbTxtDesc.Location = new System.Drawing.Point(2, 2);
            this.lbTxtDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtDesc.Name = "lbTxtDesc";
            this.lbTxtDesc.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lbTxtDesc.Size = new System.Drawing.Size(322, 37);
            this.lbTxtDesc.TabIndex = 137;
            this.lbTxtDesc.Text = "วิธีรับสินค้า";
            this.lbTxtDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbTxtDesc.Click += new System.EventHandler(this.lbTxtDesc_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.Location = new System.Drawing.Point(0, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(326, 1);
            this.pictureBox1.TabIndex = 138;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // UCGeneralListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbTxtDesc);
            this.Name = "UCGeneralListItem";
            this.Size = new System.Drawing.Size(326, 42);
            this.Load += new System.EventHandler(this.UCGeneralListItem_Load);
            this.Click += new System.EventHandler(this.UCGeneralListItem_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbTxtDesc;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
