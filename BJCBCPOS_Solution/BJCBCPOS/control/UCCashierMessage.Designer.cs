namespace BJCBCPOS
{
    partial class UCCashierMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCashierMessage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbMessageDate = new System.Windows.Forms.Label();
            this.lbMessageText = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lbMessageDate);
            this.panel1.Controls.Add(this.lbMessageText);
            this.panel1.Location = new System.Drawing.Point(20, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 110);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BJCBCPOS.Properties.Resources.cashier_message;
            this.pictureBox1.Location = new System.Drawing.Point(9, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lbMessageDate
            // 
            this.lbMessageDate.BackColor = System.Drawing.Color.Transparent;
            this.lbMessageDate.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbMessageDate.ForeColor = System.Drawing.Color.DarkGray;
            this.lbMessageDate.Location = new System.Drawing.Point(53, 73);
            this.lbMessageDate.Name = "lbMessageDate";
            this.lbMessageDate.Padding = new System.Windows.Forms.Padding(5);
            this.lbMessageDate.Size = new System.Drawing.Size(420, 30);
            this.lbMessageDate.TabIndex = 0;
            this.lbMessageDate.Text = "label1";
            this.lbMessageDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMessageText
            // 
            this.lbMessageText.BackColor = System.Drawing.Color.Transparent;
            this.lbMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbMessageText.Location = new System.Drawing.Point(53, 7);
            this.lbMessageText.Name = "lbMessageText";
            this.lbMessageText.Padding = new System.Windows.Forms.Padding(5);
            this.lbMessageText.Size = new System.Drawing.Size(420, 60);
            this.lbMessageText.TabIndex = 1;
            this.lbMessageText.Text = "label1";
            // 
            // UCCashierMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "UCCashierMessage";
            this.Size = new System.Drawing.Size(520, 120);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbMessageDate;
        private System.Windows.Forms.Label lbMessageText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}
