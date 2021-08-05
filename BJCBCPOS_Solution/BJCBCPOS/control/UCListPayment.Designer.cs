namespace BJCBCPOS
{
    partial class UCListPayment
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
            this.UCLP_lbNo = new System.Windows.Forms.Label();
            this.UCLP_lbPaymentType = new System.Windows.Forms.Label();
            this.UCLP_lbAmount = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.UCLP_label1 = new System.Windows.Forms.Label();
            this.UCLP_lbRec = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // UCLP_lbNo
            // 
            this.UCLP_lbNo.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCLP_lbNo.ForeColor = System.Drawing.Color.Gray;
            this.UCLP_lbNo.Location = new System.Drawing.Point(5, 7);
            this.UCLP_lbNo.Name = "UCLP_lbNo";
            this.UCLP_lbNo.Size = new System.Drawing.Size(35, 28);
            this.UCLP_lbNo.TabIndex = 0;
            this.UCLP_lbNo.Text = "10";
            this.UCLP_lbNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UCLP_lbNo.FontChanged += new System.EventHandler(this.UCLP_lbNo_FontChanged);
            // 
            // UCLP_lbPaymentType
            // 
            this.UCLP_lbPaymentType.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCLP_lbPaymentType.ForeColor = System.Drawing.Color.Gray;
            this.UCLP_lbPaymentType.Location = new System.Drawing.Point(45, 7);
            this.UCLP_lbPaymentType.Name = "UCLP_lbPaymentType";
            this.UCLP_lbPaymentType.Size = new System.Drawing.Size(379, 28);
            this.UCLP_lbPaymentType.TabIndex = 1;
            this.UCLP_lbPaymentType.Text = "เงินสด";
            this.UCLP_lbPaymentType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UCLP_lbPaymentType.FontChanged += new System.EventHandler(this.UCLP_lbPaymentType_FontChanged);
            // 
            // UCLP_lbAmount
            // 
            this.UCLP_lbAmount.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UCLP_lbAmount.ForeColor = System.Drawing.Color.Gray;
            this.UCLP_lbAmount.Location = new System.Drawing.Point(419, 7);
            this.UCLP_lbAmount.Name = "UCLP_lbAmount";
            this.UCLP_lbAmount.Size = new System.Drawing.Size(184, 28);
            this.UCLP_lbAmount.TabIndex = 2;
            this.UCLP_lbAmount.Text = "999,999,999.00";
            this.UCLP_lbAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.UCLP_lbAmount.FontChanged += new System.EventHandler(this.UCLP_lbAmount_FontChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_trash_can_100;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(605, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(34, 31);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UCLP_label1
            // 
            this.UCLP_label1.AutoSize = true;
            this.UCLP_label1.Location = new System.Drawing.Point(414, 29);
            this.UCLP_label1.Name = "UCLP_label1";
            this.UCLP_label1.Size = new System.Drawing.Size(35, 13);
            this.UCLP_label1.TabIndex = 9;
            this.UCLP_label1.Text = "label1";
            this.UCLP_label1.Visible = false;
            // 
            // UCLP_lbRec
            // 
            this.UCLP_lbRec.AutoSize = true;
            this.UCLP_lbRec.Location = new System.Drawing.Point(358, 29);
            this.UCLP_lbRec.Name = "UCLP_lbRec";
            this.UCLP_lbRec.Size = new System.Drawing.Size(13, 13);
            this.UCLP_lbRec.TabIndex = 10;
            this.UCLP_lbRec.Text = "0";
            this.UCLP_lbRec.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox2.Location = new System.Drawing.Point(0, 41);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(642, 1);
            this.pictureBox2.TabIndex = 69;
            this.pictureBox2.TabStop = false;
            // 
            // UCListPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.UCLP_lbRec);
            this.Controls.Add(this.UCLP_lbAmount);
            this.Controls.Add(this.UCLP_label1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.UCLP_lbPaymentType);
            this.Controls.Add(this.UCLP_lbNo);
            this.DoubleBuffered = true;
            this.Name = "UCListPayment";
            this.Size = new System.Drawing.Size(642, 42);
            this.Load += new System.EventHandler(this.UCListPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label UCLP_lbNo;
        public System.Windows.Forms.Label UCLP_lbPaymentType;
        public System.Windows.Forms.Label UCLP_lbAmount;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Label UCLP_label1;
        public System.Windows.Forms.Label UCLP_lbRec;
        public System.Windows.Forms.PictureBox pictureBox2;
    }
}
