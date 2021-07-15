namespace BJCBCPOS
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnChangePassword = new System.Windows.Forms.Panel();
            this.lbCashierName = new System.Windows.Forms.Label();
            this.lbUserID = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbConfirmPassword = new System.Windows.Forms.Label();
            this.lbNewPassword = new System.Windows.Forms.Label();
            this.lbOldPassword = new System.Windows.Forms.Label();
            this.uctwConfirmPassword = new BJCBCPOS.UCTextBoxWithIcon();
            this.uctwNewPassword = new BJCBCPOS.UCTextBoxWithIcon();
            this.uctwOldPassword = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbLabelCashierName = new System.Windows.Forms.Label();
            this.lbLabelUserID = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnChangePassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.splitContainer1.Panel1.Controls.Add(this.pnChangePassword);
            this.splitContainer1.Panel1.Controls.Add(this.ucFooter1);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeader1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucKeyboard1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Panel2MinSize = 10;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnChangePassword
            // 
            this.pnChangePassword.BackColor = System.Drawing.Color.White;
            this.pnChangePassword.Controls.Add(this.lbCashierName);
            this.pnChangePassword.Controls.Add(this.lbUserID);
            this.pnChangePassword.Controls.Add(this.btnCancel);
            this.pnChangePassword.Controls.Add(this.btnOK);
            this.pnChangePassword.Controls.Add(this.lbConfirmPassword);
            this.pnChangePassword.Controls.Add(this.lbNewPassword);
            this.pnChangePassword.Controls.Add(this.lbOldPassword);
            this.pnChangePassword.Controls.Add(this.uctwConfirmPassword);
            this.pnChangePassword.Controls.Add(this.uctwNewPassword);
            this.pnChangePassword.Controls.Add(this.uctwOldPassword);
            this.pnChangePassword.Controls.Add(this.lbLabelCashierName);
            this.pnChangePassword.Controls.Add(this.lbLabelUserID);
            this.pnChangePassword.Controls.Add(this.lbTitle);
            this.pnChangePassword.Location = new System.Drawing.Point(247, 140);
            this.pnChangePassword.Name = "pnChangePassword";
            this.pnChangePassword.Size = new System.Drawing.Size(530, 440);
            this.pnChangePassword.TabIndex = 2;
            // 
            // lbCashierName
            // 
            this.lbCashierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCashierName.Location = new System.Drawing.Point(196, 121);
            this.lbCashierName.Name = "lbCashierName";
            this.lbCashierName.Size = new System.Drawing.Size(308, 41);
            this.lbCashierName.TabIndex = 31;
            this.lbCashierName.Text = "CashierName";
            this.lbCashierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbUserID
            // 
            this.lbUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbUserID.Location = new System.Drawing.Point(196, 80);
            this.lbUserID.Name = "lbUserID";
            this.lbUserID.Size = new System.Drawing.Size(308, 41);
            this.lbUserID.TabIndex = 30;
            this.lbUserID.Text = "UserID";
            this.lbUserID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel1;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(53, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 62);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(303, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(180, 62);
            this.btnOK.TabIndex = 28;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbConfirmPassword
            // 
            this.lbConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbConfirmPassword.Location = new System.Drawing.Point(4, 262);
            this.lbConfirmPassword.Name = "lbConfirmPassword";
            this.lbConfirmPassword.Size = new System.Drawing.Size(185, 41);
            this.lbConfirmPassword.TabIndex = 27;
            this.lbConfirmPassword.Text = "ยืนยันรหัสผ่าน";
            this.lbConfirmPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbNewPassword
            // 
            this.lbNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbNewPassword.Location = new System.Drawing.Point(4, 214);
            this.lbNewPassword.Name = "lbNewPassword";
            this.lbNewPassword.Size = new System.Drawing.Size(185, 41);
            this.lbNewPassword.TabIndex = 26;
            this.lbNewPassword.Text = "รหัสผ่านใหม่";
            this.lbNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbOldPassword
            // 
            this.lbOldPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbOldPassword.Location = new System.Drawing.Point(4, 166);
            this.lbOldPassword.Name = "lbOldPassword";
            this.lbOldPassword.Size = new System.Drawing.Size(185, 41);
            this.lbOldPassword.TabIndex = 25;
            this.lbOldPassword.Text = "รหัสผ่านเดิม";
            this.lbOldPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwConfirmPassword
            // 
            this.uctwConfirmPassword.BackColor = System.Drawing.Color.White;
            this.uctwConfirmPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwConfirmPassword.BackgroundImage")));
            this.uctwConfirmPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwConfirmPassword.EnabledUC = true;
            this.uctwConfirmPassword.IsAmount = false;
            this.uctwConfirmPassword.IsLarge = false;
            this.uctwConfirmPassword.IsNumber = true;
            this.uctwConfirmPassword.IsSetFormat = false;
            this.uctwConfirmPassword.IsTextChange = false;
            this.uctwConfirmPassword.Location = new System.Drawing.Point(196, 261);
            this.uctwConfirmPassword.MaxLength = 6;
            this.uctwConfirmPassword.Name = "uctwConfirmPassword";
            this.uctwConfirmPassword.PasswordChar = true;
            this.uctwConfirmPassword.placeHolder = "Password";
            this.uctwConfirmPassword.Readonly = false;
            this.uctwConfirmPassword.ShortcutsEnabled = true;
            this.uctwConfirmPassword.Size = new System.Drawing.Size(308, 42);
            this.uctwConfirmPassword.TabIndex = 24;
            this.uctwConfirmPassword.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwConfirmPassword.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwConfirmPassword.TextBoxKeydown += new System.EventHandler(this.uctwConfirmPassword_TextBoxKeydown);
            this.uctwConfirmPassword.Enter += new System.EventHandler(this.uctwConfirmPassword_Enter);
            // 
            // uctwNewPassword
            // 
            this.uctwNewPassword.BackColor = System.Drawing.Color.White;
            this.uctwNewPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwNewPassword.BackgroundImage")));
            this.uctwNewPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwNewPassword.EnabledUC = true;
            this.uctwNewPassword.IsAmount = false;
            this.uctwNewPassword.IsLarge = false;
            this.uctwNewPassword.IsNumber = true;
            this.uctwNewPassword.IsSetFormat = false;
            this.uctwNewPassword.IsTextChange = false;
            this.uctwNewPassword.Location = new System.Drawing.Point(196, 213);
            this.uctwNewPassword.MaxLength = 6;
            this.uctwNewPassword.Name = "uctwNewPassword";
            this.uctwNewPassword.PasswordChar = true;
            this.uctwNewPassword.placeHolder = "Password";
            this.uctwNewPassword.Readonly = false;
            this.uctwNewPassword.ShortcutsEnabled = true;
            this.uctwNewPassword.Size = new System.Drawing.Size(308, 42);
            this.uctwNewPassword.TabIndex = 23;
            this.uctwNewPassword.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwNewPassword.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwNewPassword.TextBoxKeydown += new System.EventHandler(this.uctwNewPassword_TextBoxKeydown);
            this.uctwNewPassword.Enter += new System.EventHandler(this.uctwNewPassword_Enter);
            // 
            // uctwOldPassword
            // 
            this.uctwOldPassword.BackColor = System.Drawing.Color.White;
            this.uctwOldPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwOldPassword.BackgroundImage")));
            this.uctwOldPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwOldPassword.EnabledUC = true;
            this.uctwOldPassword.IsAmount = false;
            this.uctwOldPassword.IsLarge = false;
            this.uctwOldPassword.IsNumber = true;
            this.uctwOldPassword.IsSetFormat = false;
            this.uctwOldPassword.IsTextChange = false;
            this.uctwOldPassword.Location = new System.Drawing.Point(196, 165);
            this.uctwOldPassword.MaxLength = 6;
            this.uctwOldPassword.Name = "uctwOldPassword";
            this.uctwOldPassword.PasswordChar = true;
            this.uctwOldPassword.placeHolder = "Password";
            this.uctwOldPassword.Readonly = false;
            this.uctwOldPassword.ShortcutsEnabled = true;
            this.uctwOldPassword.Size = new System.Drawing.Size(308, 42);
            this.uctwOldPassword.TabIndex = 22;
            this.uctwOldPassword.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwOldPassword.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwOldPassword.TextBoxKeydown += new System.EventHandler(this.uctwOldPassword_TextBoxKeydown);
            this.uctwOldPassword.Enter += new System.EventHandler(this.uctwOldPassword_Enter);
            // 
            // lbLabelCashierName
            // 
            this.lbLabelCashierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbLabelCashierName.Location = new System.Drawing.Point(4, 121);
            this.lbLabelCashierName.Name = "lbLabelCashierName";
            this.lbLabelCashierName.Size = new System.Drawing.Size(185, 41);
            this.lbLabelCashierName.TabIndex = 19;
            this.lbLabelCashierName.Text = "ชื่อผู้ใช้";
            this.lbLabelCashierName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLabelUserID
            // 
            this.lbLabelUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbLabelUserID.Location = new System.Drawing.Point(4, 80);
            this.lbLabelUserID.Name = "lbLabelUserID";
            this.lbLabelUserID.Size = new System.Drawing.Size(185, 41);
            this.lbLabelUserID.TabIndex = 18;
            this.lbLabelUserID.Text = "รหัสผู้ใช้";
            this.lbLabelUserID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTitle.Location = new System.Drawing.Point(23, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(488, 51);
            this.lbTitle.TabIndex = 12;
            this.lbTitle.Text = "กรุณาระบุข้อมูลเพื่อเปลี่ยนรหัสผ่าน";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 1;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "Title1";
            this.ucHeader1.currentMenuTitle2 = "Title2";
            this.ucHeader1.currentMenuTitle3 = "Title3";
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = false;
            this.ucHeader1.showCurrentMenuText = false;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = false;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = false;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showScanner = false;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 0;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 0);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(1024, 739);
            this.ucKeyboard1.TabIndex = 0;
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChangePassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmChangePassword";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnChangePassword.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnChangePassword;
        private UCFooter ucFooter1;
        private UCHeader ucHeader1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbConfirmPassword;
        private System.Windows.Forms.Label lbNewPassword;
        private System.Windows.Forms.Label lbOldPassword;
        private UCTextBoxWithIcon uctwConfirmPassword;
        private UCTextBoxWithIcon uctwNewPassword;
        private UCTextBoxWithIcon uctwOldPassword;
        private System.Windows.Forms.Label lbLabelCashierName;
        private System.Windows.Forms.Label lbLabelUserID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbCashierName;
        private System.Windows.Forms.Label lbUserID;
        private UCKeyboard ucKeyboard1;
    }
}