namespace BJCBCPOS
{
    partial class frmLockScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLockScreen));
            this.pnfrmLogin = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.ucTxtPassword = new BJCBCPOS.UCTextBoxWithIcon();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lbHeader = new System.Windows.Forms.Label();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            this.pnfrmLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnfrmLogin
            // 
            this.pnfrmLogin.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_258_3x;
            this.pnfrmLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnfrmLogin.Controls.Add(this.label2);
            this.pnfrmLogin.Controls.Add(this.label1);
            this.pnfrmLogin.Controls.Add(this.lbUser);
            this.pnfrmLogin.Controls.Add(this.ucTxtPassword);
            this.pnfrmLogin.Controls.Add(this.btnLogin);
            this.pnfrmLogin.Controls.Add(this.lbHeader);
            this.pnfrmLogin.Location = new System.Drawing.Point(288, 114);
            this.pnfrmLogin.Margin = new System.Windows.Forms.Padding(2);
            this.pnfrmLogin.Name = "pnfrmLogin";
            this.pnfrmLogin.Size = new System.Drawing.Size(404, 265);
            this.pnfrmLogin.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Prompt", 15.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 41);
            this.label2.TabIndex = 17;
            this.label2.Text = "User :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Prompt", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 140);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 41);
            this.label1.TabIndex = 16;
            this.label1.Text = "Password :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbUser
            // 
            this.lbUser.BackColor = System.Drawing.Color.White;
            this.lbUser.Font = new System.Drawing.Font("Prompt", 15.75F);
            this.lbUser.ForeColor = System.Drawing.Color.Black;
            this.lbUser.Location = new System.Drawing.Point(153, 85);
            this.lbUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(235, 41);
            this.lbUser.TabIndex = 15;
            this.lbUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucTxtPassword
            // 
            this.ucTxtPassword.BackColor = System.Drawing.Color.White;
            this.ucTxtPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTxtPassword.BackgroundImage")));
            this.ucTxtPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxtPassword.EnabledUC = true;
            this.ucTxtPassword.IsAmount = false;
            this.ucTxtPassword.IsLarge = false;
            this.ucTxtPassword.IsNumber = false;
            this.ucTxtPassword.IsSetFormat = false;
            this.ucTxtPassword.Location = new System.Drawing.Point(153, 139);
            this.ucTxtPassword.MaxLength = 32767;
            this.ucTxtPassword.Name = "ucTxtPassword";
            this.ucTxtPassword.PasswordChar = true;
            this.ucTxtPassword.placeHolder = "Password";
            this.ucTxtPassword.Readonly = false;
            this.ucTxtPassword.ShortcutsEnabled = true;
            this.ucTxtPassword.Size = new System.Drawing.Size(235, 42);
            this.ucTxtPassword.TabIndex = 14;
            this.ucTxtPassword.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxtPassword.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTxtPassword.TextBoxKeydown += new System.EventHandler(this.ucTxtPassword_TextBoxKeydown);
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_224;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(15, 207);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(374, 44);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "UnLock";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lbHeader
            // 
            this.lbHeader.BackColor = System.Drawing.Color.White;
            this.lbHeader.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbHeader.Location = new System.Drawing.Point(16, 11);
            this.lbHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(373, 52);
            this.lbHeader.TabIndex = 2;
            this.lbHeader.Text = "Enter Password";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 470);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(1024, 298);
            this.ucKeyboard1.TabIndex = 11;
            // 
            // frmLockScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.ucKeyboard1);
            this.Controls.Add(this.pnfrmLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLockScreen";
            this.Text = "frmLockScreen";
            this.Load += new System.EventHandler(this.frmLockScreen_Load);
            this.pnfrmLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnfrmLogin;
        private UCTextBoxWithIcon ucTxtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UCKeyboard ucKeyboard1;
    }
}