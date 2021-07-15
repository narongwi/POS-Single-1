namespace BJCBCPOS
{
    partial class UCListItemPOD
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
            this.UCPOD_lbPaymentDesc = new System.Windows.Forms.Label();
            this.UCPOD_Qty = new System.Windows.Forms.Label();
            this.UCPOD_lbAmount = new System.Windows.Forms.Label();
            this.UCPOD_lbChannel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // UCPOD_lbPaymentDesc
            // 
            this.UCPOD_lbPaymentDesc.BackColor = System.Drawing.Color.Transparent;
            this.UCPOD_lbPaymentDesc.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCPOD_lbPaymentDesc.Location = new System.Drawing.Point(27, 8);
            this.UCPOD_lbPaymentDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UCPOD_lbPaymentDesc.Name = "UCPOD_lbPaymentDesc";
            this.UCPOD_lbPaymentDesc.Size = new System.Drawing.Size(160, 18);
            this.UCPOD_lbPaymentDesc.TabIndex = 57;
            this.UCPOD_lbPaymentDesc.Text = "12";
            // 
            // UCPOD_Qty
            // 
            this.UCPOD_Qty.BackColor = System.Drawing.Color.Transparent;
            this.UCPOD_Qty.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCPOD_Qty.Location = new System.Drawing.Point(215, 8);
            this.UCPOD_Qty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UCPOD_Qty.Name = "UCPOD_Qty";
            this.UCPOD_Qty.Size = new System.Drawing.Size(123, 18);
            this.UCPOD_Qty.TabIndex = 58;
            this.UCPOD_Qty.Text = "12345";
            // 
            // UCPOD_lbAmount
            // 
            this.UCPOD_lbAmount.BackColor = System.Drawing.Color.Transparent;
            this.UCPOD_lbAmount.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCPOD_lbAmount.Location = new System.Drawing.Point(376, 8);
            this.UCPOD_lbAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UCPOD_lbAmount.Name = "UCPOD_lbAmount";
            this.UCPOD_lbAmount.Size = new System.Drawing.Size(125, 18);
            this.UCPOD_lbAmount.TabIndex = 59;
            this.UCPOD_lbAmount.Text = "1234567890123456";
            // 
            // UCPOD_lbChannel
            // 
            this.UCPOD_lbChannel.BackColor = System.Drawing.Color.Transparent;
            this.UCPOD_lbChannel.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCPOD_lbChannel.Location = new System.Drawing.Point(505, 8);
            this.UCPOD_lbChannel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UCPOD_lbChannel.Name = "UCPOD_lbChannel";
            this.UCPOD_lbChannel.Size = new System.Drawing.Size(101, 18);
            this.UCPOD_lbChannel.TabIndex = 60;
            this.UCPOD_lbChannel.Text = "CH";
            this.UCPOD_lbChannel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.pictureBox1.Location = new System.Drawing.Point(0, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 1);
            this.pictureBox1.TabIndex = 61;
            this.pictureBox1.TabStop = false;
            // 
            // UCListItemPOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.UCPOD_lbChannel);
            this.Controls.Add(this.UCPOD_lbAmount);
            this.Controls.Add(this.UCPOD_Qty);
            this.Controls.Add(this.UCPOD_lbPaymentDesc);
            this.Name = "UCListItemPOD";
            this.Size = new System.Drawing.Size(633, 35);
            this.Load += new System.EventHandler(this.UCListItemPOD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label UCPOD_lbPaymentDesc;
        public System.Windows.Forms.Label UCPOD_Qty;
        public System.Windows.Forms.Label UCPOD_lbAmount;
        public System.Windows.Forms.Label UCPOD_lbChannel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
