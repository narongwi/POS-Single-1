namespace BJCBCPOS
{
    partial class frmPopupQRPaymentSubMenu
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbOtherPayment = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnQR_CscanB = new System.Windows.Forms.Button();
            this.btnQR_BscanC = new System.Windows.Forms.Button();
            this.lbAmountCash = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbAmountCash);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnQR_CscanB);
            this.panel2.Controls.Add(this.btnQR_BscanC);
            this.panel2.Location = new System.Drawing.Point(39, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(688, 490);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lbOtherPayment);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 64);
            this.panel1.TabIndex = 2;
            // 
            // lbOtherPayment
            // 
            this.lbOtherPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.lbOtherPayment.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOtherPayment.ForeColor = System.Drawing.Color.White;
            this.lbOtherPayment.Location = new System.Drawing.Point(12, 4);
            this.lbOtherPayment.Name = "lbOtherPayment";
            this.lbOtherPayment.Size = new System.Drawing.Size(664, 56);
            this.lbOtherPayment.TabIndex = 2;
            this.lbOtherPayment.Text = "QR Payment";
            this.lbOtherPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.button1.Location = new System.Drawing.Point(7, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 51);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnQR_CscanB
            // 
            this.btnQR_CscanB.BackgroundImage = global::BJCBCPOS.Properties.Resources.QRPayment_CscanB;
            this.btnQR_CscanB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQR_CscanB.FlatAppearance.BorderSize = 0;
            this.btnQR_CscanB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQR_CscanB.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQR_CscanB.ForeColor = System.Drawing.Color.Black;
            this.btnQR_CscanB.Location = new System.Drawing.Point(352, 72);
            this.btnQR_CscanB.Margin = new System.Windows.Forms.Padding(5);
            this.btnQR_CscanB.Name = "btnQR_CscanB";
            this.btnQR_CscanB.Size = new System.Drawing.Size(324, 345);
            this.btnQR_CscanB.TabIndex = 17;
            this.btnQR_CscanB.UseVisualStyleBackColor = true;
            this.btnQR_CscanB.Click += new System.EventHandler(this.btnQR_CscanB_Click);
            // 
            // btnQR_BscanC
            // 
            this.btnQR_BscanC.BackgroundImage = global::BJCBCPOS.Properties.Resources.QRPayment_BscanC;
            this.btnQR_BscanC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQR_BscanC.FlatAppearance.BorderSize = 0;
            this.btnQR_BscanC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQR_BscanC.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQR_BscanC.ForeColor = System.Drawing.Color.Black;
            this.btnQR_BscanC.Location = new System.Drawing.Point(12, 72);
            this.btnQR_BscanC.Margin = new System.Windows.Forms.Padding(5);
            this.btnQR_BscanC.Name = "btnQR_BscanC";
            this.btnQR_BscanC.Size = new System.Drawing.Size(324, 345);
            this.btnQR_BscanC.TabIndex = 16;
            this.btnQR_BscanC.UseVisualStyleBackColor = true;
            this.btnQR_BscanC.Click += new System.EventHandler(this.btnQR_BscanC_Click);
            // 
            // lbAmountCash
            // 
            this.lbAmountCash.BackColor = System.Drawing.Color.Transparent;
            this.lbAmountCash.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmountCash.ForeColor = System.Drawing.Color.Gray;
            this.lbAmountCash.Location = new System.Drawing.Point(12, 418);
            this.lbAmountCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAmountCash.Name = "lbAmountCash";
            this.lbAmountCash.Size = new System.Drawing.Size(324, 65);
            this.lbAmountCash.TabIndex = 97;
            this.lbAmountCash.Text = "พนักงานสแกนคิวอาร์จากมือถือลูกค้า";
            this.lbAmountCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbAmountCash.Click += new System.EventHandler(this.btnQR_BscanC_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Prompt", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(352, 418);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 65);
            this.label1.TabIndex = 98;
            this.label1.Text = "ลูกค้าสแกนคิวอาร์จากมือถือแคชเชียร์";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.btnQR_CscanB_Click);
            // 
            // frmPopupQRPaymentSubMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(963, 556);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPopupQRPaymentSubMenu";
            this.Text = "frmPopupQRPayment";
            this.Load += new System.EventHandler(this.frmPopupQRPaymentSubMenu_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQR_BscanC;
        private System.Windows.Forms.Button btnQR_CscanB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbOtherPayment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbAmountCash;
    }
}