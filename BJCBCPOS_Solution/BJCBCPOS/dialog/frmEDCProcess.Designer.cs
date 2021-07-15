namespace BJCBCPOS
{
    partial class frmEDCProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDCProcess));
            this.pnLoading = new System.Windows.Forms.Panel();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lbEDCMessage = new System.Windows.Forms.Label();
            this.edcControl1 = new EDCControl.EDCControl();
            this.panel_message = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_message = new System.Windows.Forms.Label();
            this.panel_button = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbConfirm = new System.Windows.Forms.Label();
            this.lbCash = new System.Windows.Forms.Label();
            this.lbTxtCash = new System.Windows.Forms.Label();
            this.lbPayment = new System.Windows.Forms.Label();
            this.lbTxtPayment = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTxtBalance = new System.Windows.Forms.Label();
            this.lbBalance = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.pn_PaymentNormal = new System.Windows.Forms.Panel();
            this.pnLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.panel_message.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_button.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pn_PaymentNormal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnLoading
            // 
            this.pnLoading.BackColor = System.Drawing.Color.White;
            this.pnLoading.Controls.Add(this.picLoading);
            this.pnLoading.Controls.Add(this.lbEDCMessage);
            this.pnLoading.Location = new System.Drawing.Point(226, 192);
            this.pnLoading.Name = "pnLoading";
            this.pnLoading.Size = new System.Drawing.Size(573, 331);
            this.pnLoading.TabIndex = 2;
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
            this.lbEDCMessage.Text = "EDC Process....";
            this.lbEDCMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edcControl1
            // 
            this.edcControl1.BackColor = System.Drawing.Color.Silver;
            this.edcControl1.EDCKey = "";
            this.edcControl1.EDCPort = "";
            this.edcControl1.EDCPortSetting = "";
            this.edcControl1.EDCTimeout = 0;
            this.edcControl1.EnableLog = false;
            this.edcControl1.isBreak = false;
            this.edcControl1.Location = new System.Drawing.Point(620, 419);
            this.edcControl1.Margin = new System.Windows.Forms.Padding(2);
            this.edcControl1.Name = "edcControl1";
            this.edcControl1.Size = new System.Drawing.Size(176, 102);
            this.edcControl1.TabIndex = 3;
            this.edcControl1.Tag = "";
            this.edcControl1.Visible = false;
            // 
            // panel_message
            // 
            this.panel_message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_message.Controls.Add(this.pictureBox1);
            this.panel_message.Controls.Add(this.lb_message);
            this.panel_message.Location = new System.Drawing.Point(0, 311);
            this.panel_message.Name = "panel_message";
            this.panel_message.Size = new System.Drawing.Size(573, 110);
            this.panel_message.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_error_126px;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(253, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 60);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lb_message
            // 
            this.lb_message.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_message.Location = new System.Drawing.Point(3, 71);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(567, 35);
            this.lb_message.TabIndex = 0;
            this.lb_message.Text = "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ";
            this.lb_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_button
            // 
            this.panel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_button.Controls.Add(this.btnCancel);
            this.panel_button.Controls.Add(this.btnOk);
            this.panel_button.Location = new System.Drawing.Point(0, 311);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(573, 110);
            this.panel_button.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancel.Location = new System.Drawing.Point(73, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 62);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "ไม่ใช่";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(322, 24);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(180, 62);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lbConfirm
            // 
            this.lbConfirm.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirm.ForeColor = System.Drawing.Color.Black;
            this.lbConfirm.Location = new System.Drawing.Point(3, 5);
            this.lbConfirm.Name = "lbConfirm";
            this.lbConfirm.Size = new System.Drawing.Size(567, 58);
            this.lbConfirm.TabIndex = 10;
            this.lbConfirm.Text = "ยอดเงินชำระ";
            this.lbConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCash
            // 
            this.lbCash.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCash.ForeColor = System.Drawing.Color.Black;
            this.lbCash.Location = new System.Drawing.Point(31, 116);
            this.lbCash.Name = "lbCash";
            this.lbCash.Size = new System.Drawing.Size(257, 40);
            this.lbCash.TabIndex = 11;
            this.lbCash.Text = "ยอดเงิน";
            // 
            // lbTxtCash
            // 
            this.lbTxtCash.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtCash.ForeColor = System.Drawing.Color.Black;
            this.lbTxtCash.Location = new System.Drawing.Point(296, 110);
            this.lbTxtCash.Name = "lbTxtCash";
            this.lbTxtCash.Size = new System.Drawing.Size(257, 50);
            this.lbTxtCash.TabIndex = 12;
            this.lbTxtCash.Text = "8,888,888.88";
            this.lbTxtCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPayment
            // 
            this.lbPayment.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPayment.ForeColor = System.Drawing.Color.Black;
            this.lbPayment.Location = new System.Drawing.Point(31, 173);
            this.lbPayment.Name = "lbPayment";
            this.lbPayment.Size = new System.Drawing.Size(257, 40);
            this.lbPayment.TabIndex = 13;
            this.lbPayment.Text = "ยอดชำระ";
            // 
            // lbTxtPayment
            // 
            this.lbTxtPayment.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtPayment.ForeColor = System.Drawing.Color.Black;
            this.lbTxtPayment.Location = new System.Drawing.Point(296, 166);
            this.lbTxtPayment.Name = "lbTxtPayment";
            this.lbTxtPayment.Size = new System.Drawing.Size(257, 50);
            this.lbTxtPayment.TabIndex = 14;
            this.lbTxtPayment.Text = "8,888,888.88";
            this.lbTxtPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmPaymentChange;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbTxtBalance);
            this.panel1.Controls.Add(this.lbBalance);
            this.panel1.Location = new System.Drawing.Point(17, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 60);
            this.panel1.TabIndex = 17;
            // 
            // lbTxtBalance
            // 
            this.lbTxtBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbTxtBalance.Font = new System.Drawing.Font("Prompt", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtBalance.ForeColor = System.Drawing.Color.Red;
            this.lbTxtBalance.Location = new System.Drawing.Point(251, 1);
            this.lbTxtBalance.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbTxtBalance.Name = "lbTxtBalance";
            this.lbTxtBalance.Size = new System.Drawing.Size(285, 58);
            this.lbTxtBalance.TabIndex = 8;
            this.lbTxtBalance.Text = "8,888,888.88";
            this.lbTxtBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbBalance
            // 
            this.lbBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbBalance.Font = new System.Drawing.Font("Prompt", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBalance.ForeColor = System.Drawing.Color.Black;
            this.lbBalance.Location = new System.Drawing.Point(4, 9);
            this.lbBalance.Name = "lbBalance";
            this.lbBalance.Size = new System.Drawing.Size(250, 44);
            this.lbBalance.TabIndex = 7;
            this.lbBalance.Text = "ยอดเงินทอน";
            // 
            // lbText
            // 
            this.lbText.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(139)))), ((int)(((byte)(46)))));
            this.lbText.Location = new System.Drawing.Point(30, 69);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(512, 35);
            this.lbText.TabIndex = 141;
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn_PaymentNormal
            // 
            this.pn_PaymentNormal.Controls.Add(this.lbText);
            this.pn_PaymentNormal.Controls.Add(this.panel1);
            this.pn_PaymentNormal.Controls.Add(this.lbTxtPayment);
            this.pn_PaymentNormal.Controls.Add(this.lbPayment);
            this.pn_PaymentNormal.Controls.Add(this.lbTxtCash);
            this.pn_PaymentNormal.Controls.Add(this.lbCash);
            this.pn_PaymentNormal.Controls.Add(this.lbConfirm);
            this.pn_PaymentNormal.Controls.Add(this.panel_button);
            this.pn_PaymentNormal.Controls.Add(this.panel_message);
            this.pn_PaymentNormal.Location = new System.Drawing.Point(226, 29);
            this.pn_PaymentNormal.Name = "pn_PaymentNormal";
            this.pn_PaymentNormal.Size = new System.Drawing.Size(573, 421);
            this.pn_PaymentNormal.TabIndex = 11;
            this.pn_PaymentNormal.Visible = false;
            // 
            // frmEDCProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pnLoading);
            this.Controls.Add(this.pn_PaymentNormal);
            this.Controls.Add(this.edcControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEDCProcess";
            this.Text = "frmEDCProcess";
            this.Shown += new System.EventHandler(this.frmEDCProcess_Shown);
            this.pnLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.panel_message.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_button.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pn_PaymentNormal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLoading;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lbEDCMessage;
        private EDCControl.EDCControl edcControl1;
        private System.Windows.Forms.Panel panel_message;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.Panel panel_button;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbConfirm;
        private System.Windows.Forms.Label lbCash;
        private System.Windows.Forms.Label lbTxtCash;
        private System.Windows.Forms.Label lbPayment;
        private System.Windows.Forms.Label lbTxtPayment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTxtBalance;
        private System.Windows.Forms.Label lbBalance;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Panel pn_PaymentNormal;
    }
}