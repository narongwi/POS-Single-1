namespace BJCBCPOS
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnfrmLogin = new System.Windows.Forms.Panel();
            this.ucTextBoxWithIcon2 = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucTextBoxWithIcon1 = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbTxtTillNo = new System.Windows.Forms.Label();
            this.lbTxtServerName = new System.Windows.Forms.Label();
            this.lbTxtStoreId = new System.Windows.Forms.Label();
            this.lbTillNo = new System.Windows.Forms.Label();
            this.lbServerName = new System.Windows.Forms.Label();
            this.lbStoreId = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lbLogin = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucKeypad1 = new BJCBCPOS.UCKeypad();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnfrmLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
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
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel1.Controls.Add(this.pnfrmLogin);
            this.splitContainer1.Panel1.Controls.Add(this.picLogo);
            this.splitContainer1.Panel1.Controls.Add(this.ucFooter1);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeader1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel2.Controls.Add(this.ucKeypad1);
            this.splitContainer1.Panel2.Controls.Add(this.ucKeyboard1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.TabIndex = 7;
            // 
            // pnfrmLogin
            // 
            this.pnfrmLogin.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_258_3x;
            this.pnfrmLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnfrmLogin.Controls.Add(this.ucTextBoxWithIcon2);
            this.pnfrmLogin.Controls.Add(this.ucTextBoxWithIcon1);
            this.pnfrmLogin.Controls.Add(this.lbTxtTillNo);
            this.pnfrmLogin.Controls.Add(this.lbTxtServerName);
            this.pnfrmLogin.Controls.Add(this.lbTxtStoreId);
            this.pnfrmLogin.Controls.Add(this.lbTillNo);
            this.pnfrmLogin.Controls.Add(this.lbServerName);
            this.pnfrmLogin.Controls.Add(this.lbStoreId);
            this.pnfrmLogin.Controls.Add(this.btnLogin);
            this.pnfrmLogin.Controls.Add(this.lbLogin);
            this.pnfrmLogin.Location = new System.Drawing.Point(346, 274);
            this.pnfrmLogin.Margin = new System.Windows.Forms.Padding(2);
            this.pnfrmLogin.Name = "pnfrmLogin";
            this.pnfrmLogin.Size = new System.Drawing.Size(337, 366);
            this.pnfrmLogin.TabIndex = 9;
            // 
            // ucTextBoxWithIcon2
            // 
            this.ucTextBoxWithIcon2.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTextBoxWithIcon2.BackgroundImage")));
            this.ucTextBoxWithIcon2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon2.EnabledUC = true;
            this.ucTextBoxWithIcon2.IsAmount = false;
            this.ucTextBoxWithIcon2.IsLarge = false;
            this.ucTextBoxWithIcon2.IsNumber = false;
            this.ucTextBoxWithIcon2.IsSetFormat = false;
            this.ucTextBoxWithIcon2.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon2.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon2.Location = new System.Drawing.Point(15, 134);
            this.ucTextBoxWithIcon2.MaxLength = 32767;
            this.ucTextBoxWithIcon2.Name = "ucTextBoxWithIcon2";
            this.ucTextBoxWithIcon2.PasswordChar = true;
            this.ucTextBoxWithIcon2.placeHolder = "Password";
            this.ucTextBoxWithIcon2.Readonly = false;
            this.ucTextBoxWithIcon2.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon2.Size = new System.Drawing.Size(308, 42);
            this.ucTextBoxWithIcon2.TabIndex = 14;
            this.ucTextBoxWithIcon2.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon2.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTextBoxWithIcon2.TextBoxKeydown += new System.EventHandler(this.ucTextBoxWithIcon2_TextBoxKeydown);
            this.ucTextBoxWithIcon2.Enter += new System.EventHandler(this.ucTextBoxWithIcon2_Enter);
            // 
            // ucTextBoxWithIcon1
            // 
            this.ucTextBoxWithIcon1.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTextBoxWithIcon1.BackgroundImage")));
            this.ucTextBoxWithIcon1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon1.EnabledUC = true;
            this.ucTextBoxWithIcon1.IsAmount = false;
            this.ucTextBoxWithIcon1.IsLarge = false;
            this.ucTextBoxWithIcon1.IsNumber = false;
            this.ucTextBoxWithIcon1.IsSetFormat = false;
            this.ucTextBoxWithIcon1.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon1.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon1.Location = new System.Drawing.Point(15, 76);
            this.ucTextBoxWithIcon1.MaxLength = 32767;
            this.ucTextBoxWithIcon1.Name = "ucTextBoxWithIcon1";
            this.ucTextBoxWithIcon1.PasswordChar = false;
            this.ucTextBoxWithIcon1.placeHolder = "Username";
            this.ucTextBoxWithIcon1.Readonly = false;
            this.ucTextBoxWithIcon1.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon1.Size = new System.Drawing.Size(308, 42);
            this.ucTextBoxWithIcon1.TabIndex = 13;
            this.ucTextBoxWithIcon1.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon1.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTextBoxWithIcon1.TextBoxKeydown += new System.EventHandler(this.ucTextBoxWithIcon1_TextBoxKeydown);
            this.ucTextBoxWithIcon1.Enter += new System.EventHandler(this.ucTextBoxWithIcon1_Enter);
            // 
            // lbTxtTillNo
            // 
            this.lbTxtTillNo.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtTillNo.CausesValidation = false;
            this.lbTxtTillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtTillNo.ForeColor = System.Drawing.Color.Black;
            this.lbTxtTillNo.Location = new System.Drawing.Point(195, 331);
            this.lbTxtTillNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtTillNo.Name = "lbTxtTillNo";
            this.lbTxtTillNo.Size = new System.Drawing.Size(125, 20);
            this.lbTxtTillNo.TabIndex = 10;
            this.lbTxtTillNo.Text = "No. 103";
            this.lbTxtTillNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbTxtServerName
            // 
            this.lbTxtServerName.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtServerName.CausesValidation = false;
            this.lbTxtServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtServerName.ForeColor = System.Drawing.Color.Black;
            this.lbTxtServerName.Location = new System.Drawing.Point(190, 296);
            this.lbTxtServerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtServerName.Name = "lbTxtServerName";
            this.lbTxtServerName.Size = new System.Drawing.Size(133, 21);
            this.lbTxtServerName.TabIndex = 9;
            this.lbTxtServerName.Text = "XXXXXXXXXX";
            this.lbTxtServerName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbTxtStoreId
            // 
            this.lbTxtStoreId.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtStoreId.CausesValidation = false;
            this.lbTxtStoreId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtStoreId.ForeColor = System.Drawing.Color.Black;
            this.lbTxtStoreId.Location = new System.Drawing.Point(78, 261);
            this.lbTxtStoreId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtStoreId.Name = "lbTxtStoreId";
            this.lbTxtStoreId.Size = new System.Drawing.Size(245, 28);
            this.lbTxtStoreId.TabIndex = 8;
            this.lbTxtStoreId.Text = "000-ราชดำริ";
            this.lbTxtStoreId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbTillNo
            // 
            this.lbTillNo.AutoSize = true;
            this.lbTillNo.BackColor = System.Drawing.Color.Transparent;
            this.lbTillNo.CausesValidation = false;
            this.lbTillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTillNo.ForeColor = System.Drawing.Color.Black;
            this.lbTillNo.Location = new System.Drawing.Point(9, 331);
            this.lbTillNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTillNo.Name = "lbTillNo";
            this.lbTillNo.Size = new System.Drawing.Size(51, 20);
            this.lbTillNo.TabIndex = 7;
            this.lbTillNo.Text = "Till No";
            // 
            // lbServerName
            // 
            this.lbServerName.AutoSize = true;
            this.lbServerName.BackColor = System.Drawing.Color.Transparent;
            this.lbServerName.CausesValidation = false;
            this.lbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbServerName.ForeColor = System.Drawing.Color.Black;
            this.lbServerName.Location = new System.Drawing.Point(8, 296);
            this.lbServerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbServerName.Name = "lbServerName";
            this.lbServerName.Size = new System.Drawing.Size(101, 20);
            this.lbServerName.TabIndex = 6;
            this.lbServerName.Text = "Server Name";
            // 
            // lbStoreId
            // 
            this.lbStoreId.AutoSize = true;
            this.lbStoreId.BackColor = System.Drawing.Color.Transparent;
            this.lbStoreId.CausesValidation = false;
            this.lbStoreId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStoreId.ForeColor = System.Drawing.Color.Black;
            this.lbStoreId.Location = new System.Drawing.Point(9, 261);
            this.lbStoreId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStoreId.Name = "lbStoreId";
            this.lbStoreId.Size = new System.Drawing.Size(69, 20);
            this.lbStoreId.TabIndex = 2;
            this.lbStoreId.Text = "Store ID";
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_224;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(14, 191);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(308, 44);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Log in";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lbLogin
            // 
            this.lbLogin.BackColor = System.Drawing.Color.Transparent;
            this.lbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbLogin.Location = new System.Drawing.Point(15, 7);
            this.lbLogin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(308, 52);
            this.lbLogin.TabIndex = 2;
            this.lbLogin.Text = "ເຂົ້າ​ສູ່​ລະ​ບົບ";
            this.lbLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::BJCBCPOS.Properties.Resources.NoPath_3x;
            this.picLogo.Location = new System.Drawing.Point(449, 62);
            this.picLogo.Margin = new System.Windows.Forms.Padding(2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(141, 187);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 8;
            this.picLogo.TabStop = false;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 7;
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
            this.ucHeader1.ForeColor = System.Drawing.Color.White;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ปิดโปรแกรม";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = false;
            this.ucHeader1.showCurrentMenuText = false;
            this.ucHeader1.showHamberGetItm = true;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = false;
            this.ucHeader1.showLockScreen = false;
            this.ucHeader1.showLogout = true;
            this.ucHeader1.showMainMenu = false;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showMember_ButtonBack = true;
            this.ucHeader1.showScanner = false;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 6;
            this.ucHeader1.LogoutClick += new System.EventHandler(this.ucHeader1_LogoutClick);
            this.ucHeader1.LanguageClick += new System.EventHandler(this.ucHeader1_LanguageClick);
            // 
            // ucKeypad1
            // 
            this.ucKeypad1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucKeypad1.Location = new System.Drawing.Point(-186, 0);
            this.ucKeypad1.Name = "ucKeypad1";
            this.ucKeypad1.Size = new System.Drawing.Size(336, 46);
            this.ucKeypad1.TabIndex = 1;
            this.ucKeypad1.ucTBS = null;
            this.ucKeypad1.ucTBWI = null;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 0);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(150, 46);
            this.ucKeyboard1.TabIndex = 0;
            this.ucKeyboard1.HideKeyboardClick += new System.EventHandler(this.ucKeyboard1_HideKeyboardClick);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnfrmLogin.ResumeLayout(false);
            this.pnfrmLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnfrmLogin;
        private UCTextBoxWithIcon ucTextBoxWithIcon2;
        public UCTextBoxWithIcon ucTextBoxWithIcon1;
        private System.Windows.Forms.Label lbTxtTillNo;
        private System.Windows.Forms.Label lbTxtServerName;
        private System.Windows.Forms.Label lbTxtStoreId;
        private System.Windows.Forms.Label lbTillNo;
        private System.Windows.Forms.Label lbServerName;
        private System.Windows.Forms.Label lbStoreId;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.PictureBox picLogo;
        private UCFooter ucFooter1;
        private UCHeader ucHeader1;
        private UCKeyboard ucKeyboard1;
        private UCKeypad ucKeypad1;

    }
}