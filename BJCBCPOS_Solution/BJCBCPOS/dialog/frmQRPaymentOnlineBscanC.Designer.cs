namespace BJCBCPOS
{
    partial class frmQRPaymentOnlineBscanC
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnLoading = new System.Windows.Forms.Panel();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lbEDCMessage = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pnLoading
            // 
            this.pnLoading.BackColor = System.Drawing.Color.White;
            this.pnLoading.Controls.Add(this.picLoading);
            this.pnLoading.Controls.Add(this.lbEDCMessage);
            this.pnLoading.Location = new System.Drawing.Point(226, 219);
            this.pnLoading.Name = "pnLoading";
            this.pnLoading.Size = new System.Drawing.Size(573, 331);
            this.pnLoading.TabIndex = 3;
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.White;
            this.picLoading.Image = global::BJCBCPOS.Properties.Resources.loading;
            this.picLoading.Location = new System.Drawing.Point(213, 135);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(150, 150);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 1;
            this.picLoading.TabStop = false;
            // 
            // lbEDCMessage
            // 
            this.lbEDCMessage.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbEDCMessage.Location = new System.Drawing.Point(18, 16);
            this.lbEDCMessage.Name = "lbEDCMessage";
            this.lbEDCMessage.Size = new System.Drawing.Size(538, 97);
            this.lbEDCMessage.TabIndex = 4;
            this.lbEDCMessage.Text = "กำลังดำเนินการ กรุณารอสักครู่";
            this.lbEDCMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmQRPaymentOnlineBscanC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pnLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQRPaymentOnlineBscanC";
            this.Text = "frmQRPaymentOnlineBscanC";
            this.Shown += new System.EventHandler(this.frmQRPaymentOnlineBscanC_Shown);
            this.pnLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLoading;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lbEDCMessage;
        private System.Windows.Forms.Timer timer1;
    }
}