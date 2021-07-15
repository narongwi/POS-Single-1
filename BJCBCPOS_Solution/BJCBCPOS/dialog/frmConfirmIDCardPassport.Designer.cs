namespace BJCBCPOS
{
    partial class frmConfirmIDCardPassport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbConfirm = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ucTxtIDCard = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            this.ucKeypad1 = new BJCBCPOS.UCKeypad();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucKeyboard1);
            this.splitContainer1.Panel2.Controls.Add(this.ucKeypad1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 470;
            this.splitContainer1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmIDCardPanel;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbConfirm);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.ucTxtIDCard);
            this.panel1.Location = new System.Drawing.Point(235, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 275);
            this.panel1.TabIndex = 1;
            // 
            // lbConfirm
            // 
            this.lbConfirm.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirm.Location = new System.Drawing.Point(3, 5);
            this.lbConfirm.Name = "lbConfirm";
            this.lbConfirm.Size = new System.Drawing.Size(554, 121);
            this.lbConfirm.TabIndex = 115;
            this.lbConfirm.Text = "กรุณากรอกเลขบัตรประชาชนหรือพาสปอร์ต";
            this.lbConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmIDcardCancel;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(32, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 45);
            this.btnCancel.TabIndex = 113;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmIDCardOk;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(231, 205);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(300, 45);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 470);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ucTxtIDCard
            // 
            this.ucTxtIDCard.BackColor = System.Drawing.Color.White;
            this.ucTxtIDCard.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTxtIDCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxtIDCard.EnabledUC = true;
            this.ucTxtIDCard.IsAmount = false;
            this.ucTxtIDCard.IsLarge = true;
            this.ucTxtIDCard.IsNumber = false;
            this.ucTxtIDCard.IsSetFormat = false;
            this.ucTxtIDCard.IsTextChange = false;
            this.ucTxtIDCard.Location = new System.Drawing.Point(32, 141);
            this.ucTxtIDCard.MaxLength = 32767;
            this.ucTxtIDCard.Name = "ucTxtIDCard";
            this.ucTxtIDCard.PasswordChar = false;
            this.ucTxtIDCard.placeHolder = "กรุณากรอกเลขบัตรประชาชนหรือพาสปอร์ต";
            this.ucTxtIDCard.Readonly = false;
            this.ucTxtIDCard.ShortcutsEnabled = true;
            this.ucTxtIDCard.Size = new System.Drawing.Size(500, 42);
            this.ucTxtIDCard.TabIndex = 0;
            this.ucTxtIDCard.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxtIDCard.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTxtIDCard.TextBoxKeydown += new System.EventHandler(this.ucTextBoxWithIcon1_TextBoxKeydown);
            this.ucTxtIDCard.Enter += new System.EventHandler(this.ucTextBoxWithIcon1_Enter);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 0);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(1024, 294);
            this.ucKeyboard1.TabIndex = 0;
            // 
            // ucKeypad1
            // 
            this.ucKeypad1.Location = new System.Drawing.Point(688, 84);
            this.ucKeypad1.Name = "ucKeypad1";
            this.ucKeypad1.Size = new System.Drawing.Size(336, 296);
            this.ucKeypad1.TabIndex = 3;
            this.ucKeypad1.ucTBS = null;
            this.ucKeypad1.ucTBWI = null;
            // 
            // frmConfirmIDCardPassport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmIDCardPassport";
            this.Text = "frmConfirmIDCardPassport";
            this.Load += new System.EventHandler(this.frmConfirmIDCardPassport_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCKeyboard ucKeyboard1;
        private System.Windows.Forms.Panel panel1;
        private UCTextBoxWithIcon ucTxtIDCard;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbConfirm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UCKeypad ucKeypad1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}