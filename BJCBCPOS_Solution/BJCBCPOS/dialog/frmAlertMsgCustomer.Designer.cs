namespace BJCBCPOS
{
    partial class frmAlertMsgCustomer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbAlertMessage = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.panel_consent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbAlertMessage);
            this.panel1.Location = new System.Drawing.Point(51, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 412);
            this.panel1.TabIndex = 208;
            // 
            // lbAlertMessage
            // 
            this.lbAlertMessage.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lbAlertMessage.Location = new System.Drawing.Point(20, 19);
            this.lbAlertMessage.Name = "lbAlertMessage";
            this.lbAlertMessage.Size = new System.Drawing.Size(657, 369);
            this.lbAlertMessage.TabIndex = 5;
            this.lbAlertMessage.Text = "ขออภัยค่ะ\r\nงดขายนอกช่วงเวลาที่กฎหมายกำหนด";
            this.lbAlertMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picLogo.Image = global::BJCBCPOS.Properties.Resources.rsz_mm_food_service_logobo;
            this.picLogo.ImageLocation = "";
            this.picLogo.Location = new System.Drawing.Point(6, 6);
            this.picLogo.Margin = new System.Windows.Forms.Padding(2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(95, 87);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 212;
            this.picLogo.TabStop = false;
            // 
            // frmAlertMsgCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BJCBCPOS.Properties.Resources.background_consent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAlertMsgCustomer";
            this.Text = "frmNoSaleCustomer";
            this.Load += new System.EventHandler(this.frmAlertMsgCustomer_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        public System.Windows.Forms.Label lbAlertMessage;
    }
}