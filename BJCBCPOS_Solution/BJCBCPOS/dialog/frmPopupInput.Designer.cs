namespace BJCBCPOS
{
    partial class frmPopupInput
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucTxtInput2 = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucTxtInput1 = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.btnCancel.Location = new System.Drawing.Point(36, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 45);
            this.btnCancel.TabIndex = 115;
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
            this.btnOK.Location = new System.Drawing.Point(235, 248);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(300, 45);
            this.btnOK.TabIndex = 114;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbHeader
            // 
            this.lbHeader.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.Location = new System.Drawing.Point(3, 3);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(554, 121);
            this.lbHeader.TabIndex = 117;
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ucTxtInput2);
            this.panel1.Controls.Add(this.lbHeader);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.ucTxtInput1);
            this.panel1.Location = new System.Drawing.Point(236, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 319);
            this.panel1.TabIndex = 119;
            // 
            // ucTxtInput2
            // 
            this.ucTxtInput2.BackColor = System.Drawing.Color.White;
            this.ucTxtInput2.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTxtInput2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxtInput2.EnabledUC = true;
            this.ucTxtInput2.IsAmount = false;
            this.ucTxtInput2.IsKeyBoardForScan = true;
            this.ucTxtInput2.IsLarge = true;
            this.ucTxtInput2.IsNumber = false;
            this.ucTxtInput2.IsSetFormat = false;
            this.ucTxtInput2.IsValidateNumberZero = false;
            this.ucTxtInput2.IsValidateTextEmpty = false;
            this.ucTxtInput2.Location = new System.Drawing.Point(36, 191);
            this.ucTxtInput2.MaxLength = 32767;
            this.ucTxtInput2.Name = "ucTxtInput2";
            this.ucTxtInput2.PasswordChar = false;
            this.ucTxtInput2.placeHolder = "";
            this.ucTxtInput2.Readonly = false;
            this.ucTxtInput2.ShortcutsEnabled = true;
            this.ucTxtInput2.Size = new System.Drawing.Size(499, 42);
            this.ucTxtInput2.TabIndex = 118;
            this.ucTxtInput2.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxtInput2.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTxtInput2.TextBoxKeydown += new System.EventHandler(this.ucTxtInput2_TextBoxKeydown);
            this.ucTxtInput2.Enter += new System.EventHandler(this.ucTxtInput2_Enter);
            // 
            // ucTxtInput1
            // 
            this.ucTxtInput1.BackColor = System.Drawing.Color.White;
            this.ucTxtInput1.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTxtInput1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxtInput1.EnabledUC = true;
            this.ucTxtInput1.IsAmount = false;
            this.ucTxtInput1.IsKeyBoardForScan = true;
            this.ucTxtInput1.IsLarge = true;
            this.ucTxtInput1.IsNumber = false;
            this.ucTxtInput1.IsSetFormat = false;
            this.ucTxtInput1.IsValidateNumberZero = false;
            this.ucTxtInput1.IsValidateTextEmpty = false;
            this.ucTxtInput1.Location = new System.Drawing.Point(36, 136);
            this.ucTxtInput1.MaxLength = 32767;
            this.ucTxtInput1.Name = "ucTxtInput1";
            this.ucTxtInput1.PasswordChar = false;
            this.ucTxtInput1.placeHolder = "";
            this.ucTxtInput1.Readonly = false;
            this.ucTxtInput1.ShortcutsEnabled = true;
            this.ucTxtInput1.Size = new System.Drawing.Size(499, 42);
            this.ucTxtInput1.TabIndex = 116;
            this.ucTxtInput1.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxtInput1.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTxtInput1.TextBoxKeydown += new System.EventHandler(this.ucTxtInput1_TextBoxKeydown);
            this.ucTxtInput1.EnterFromButton += new System.EventHandler(this.ucTxtInput1_EnterFromButton);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 474);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(1024, 294);
            this.ucKeyboard1.TabIndex = 118;
            this.ucKeyboard1.Visible = false;
            // 
            // frmPopupInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucKeyboard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPopupInput";
            this.Text = "frmPopupInput";
            this.Shown += new System.EventHandler(this.frmPopupInput_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public UCTextBoxWithIcon ucTxtInput1;
        public System.Windows.Forms.Label lbHeader;
        private UCKeyboard ucKeyboard1;
        private System.Windows.Forms.Panel panel1;
        public UCTextBoxWithIcon ucTxtInput2;

    }
}