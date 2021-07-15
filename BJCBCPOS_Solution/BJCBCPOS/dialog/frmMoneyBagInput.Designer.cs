namespace BJCBCPOS
{
    partial class frmMoneyBagInput
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.ucTextBoxWithIcon1 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_257;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.splitContainer1.Panel1.Controls.Add(this.ucFooter1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeader1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_257;
            this.splitContainer1.Panel2.Controls.Add(this.ucKeyboard1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 739;
            this.splitContainer1.TabIndex = 118;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 120;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Location = new System.Drawing.Point(247, 254);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 251);
            this.panel1.TabIndex = 119;
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmIDcardCancel;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnBack.Location = new System.Drawing.Point(22, 186);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(180, 45);
            this.btnBack.TabIndex = 121;
            this.btnBack.Text = "ไม่ระบุ";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ucTextBoxWithIcon1
            // 
            this.ucTextBoxWithIcon1.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon1.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon1.EnabledUC = true;
            this.ucTextBoxWithIcon1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ucTextBoxWithIcon1.IsLarge = true;
            this.ucTextBoxWithIcon1.IsNumber = true;
            this.ucTextBoxWithIcon1.IsSetFormat = false;
            this.ucTextBoxWithIcon1.IsTextChange = true;
            this.ucTextBoxWithIcon1.Location = new System.Drawing.Point(23, 125);
            this.ucTextBoxWithIcon1.MaxLength = 4;
            this.ucTextBoxWithIcon1.Name = "ucTextBoxWithIcon1";
            this.ucTextBoxWithIcon1.PasswordChar = false;
            this.ucTextBoxWithIcon1.placeHolder = "";
            this.ucTextBoxWithIcon1.Readonly = false;
            this.ucTextBoxWithIcon1.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon1.Size = new System.Drawing.Size(500, 42);
            this.ucTextBoxWithIcon1.TabIndex = 120;
            this.ucTextBoxWithIcon1.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon1.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTextBoxWithIcon1.TextBoxKeydown += new System.EventHandler(this.ucTextBoxWithIcon1_TextBoxKeydown);
            this.ucTextBoxWithIcon1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(18, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(506, 32);
            this.label1.TabIndex = 118;
            this.label1.Text = "ส่งเงินจากการขาย";
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmIDCardOk;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(222, 186);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(300, 45);
            this.btnOk.TabIndex = 117;
            this.btnOk.Text = "ตกลง";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(19, 6);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(506, 63);
            this.lbTitle.TabIndex = 114;
            this.lbTitle.Text = "กรุณาระบุเลขที่ซองเงิน";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ucHeader1.TabIndex = 118;
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
            this.ucKeyboard1.TabIndex = 117;
            this.ucKeyboard1.Visible = false;
            // 
            // frmMoneyBagInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMoneyBagInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmMoneyBagInput";
            this.Load += new System.EventHandler(this.frmMoneyBagInput_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbTitle;
        private UCHeader ucHeader1;
        private UCKeyboard ucKeyboard1;
        private UCFooter ucFooter1;
        private UCTextBoxWithIcon ucTextBoxWithIcon1;
        private System.Windows.Forms.Button btnBack;

    }
}