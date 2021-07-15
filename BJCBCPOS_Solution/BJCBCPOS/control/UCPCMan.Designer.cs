namespace BJCBCPOS
{
    partial class UCPCMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPCMan));
            this.btnOk = new System.Windows.Forms.Button();
            this.picBtnBack = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbPCMan = new System.Windows.Forms.Label();
            this.lbMessage1 = new System.Windows.Forms.Label();
            this.lbNamePCMan = new System.Windows.Forms.Label();
            this.ucTxt_PCMan = new BJCBCPOS.UCTextBoxWithIcon();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.White;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(12, 373);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(310, 43);
            this.btnOk.TabIndex = 94;
            this.btnOk.Text = "ตกลง";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // picBtnBack
            // 
            this.picBtnBack.BackColor = System.Drawing.Color.White;
            this.picBtnBack.Image = ((System.Drawing.Image)(resources.GetObject("picBtnBack.Image")));
            this.picBtnBack.Location = new System.Drawing.Point(8, 7);
            this.picBtnBack.Name = "picBtnBack";
            this.picBtnBack.Size = new System.Drawing.Size(40, 36);
            this.picBtnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBtnBack.TabIndex = 97;
            this.picBtnBack.TabStop = false;
            this.picBtnBack.Click += new System.EventHandler(this.picBtnBack_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(127, 69);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(86, 85);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 93;
            this.pictureBox2.TabStop = false;
            // 
            // lbPCMan
            // 
            this.lbPCMan.BackColor = System.Drawing.Color.White;
            this.lbPCMan.Font = new System.Drawing.Font("Prompt", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPCMan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(186)))), ((int)(((byte)(109)))));
            this.lbPCMan.Location = new System.Drawing.Point(42, 18);
            this.lbPCMan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPCMan.Name = "lbPCMan";
            this.lbPCMan.Size = new System.Drawing.Size(259, 45);
            this.lbPCMan.TabIndex = 92;
            this.lbPCMan.Text = "PC-Man";
            this.lbPCMan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMessage1
            // 
            this.lbMessage1.BackColor = System.Drawing.Color.White;
            this.lbMessage1.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbMessage1.Location = new System.Drawing.Point(8, 163);
            this.lbMessage1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMessage1.Name = "lbMessage1";
            this.lbMessage1.Size = new System.Drawing.Size(319, 66);
            this.lbMessage1.TabIndex = 95;
            this.lbMessage1.Text = "กรอกหมายเลข PC-Man";
            this.lbMessage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNamePCMan
            // 
            this.lbNamePCMan.BackColor = System.Drawing.Color.White;
            this.lbNamePCMan.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNamePCMan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbNamePCMan.Location = new System.Drawing.Point(15, 288);
            this.lbNamePCMan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNamePCMan.Name = "lbNamePCMan";
            this.lbNamePCMan.Size = new System.Drawing.Size(260, 31);
            this.lbNamePCMan.TabIndex = 99;
            this.lbNamePCMan.Text = "ชื่อ : ";
            this.lbNamePCMan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucTxt_PCMan
            // 
            this.ucTxt_PCMan.BackColor = System.Drawing.Color.White;
            this.ucTxt_PCMan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTxt_PCMan.BackgroundImage")));
            this.ucTxt_PCMan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxt_PCMan.EnabledUC = true;
            this.ucTxt_PCMan.IsAmount = false;
            this.ucTxt_PCMan.IsLarge = false;
            this.ucTxt_PCMan.IsNumber = false;
            this.ucTxt_PCMan.IsSetFormat = false;
            this.ucTxt_PCMan.IsValidateNumberZero = false;
            this.ucTxt_PCMan.IsValidateTextEmpty = false;
            this.ucTxt_PCMan.Location = new System.Drawing.Point(15, 236);
            this.ucTxt_PCMan.MaxLength = 32767;
            this.ucTxt_PCMan.Name = "ucTxt_PCMan";
            this.ucTxt_PCMan.PasswordChar = false;
            this.ucTxt_PCMan.placeHolder = "ระบุหมายเลข PC-Man";
            this.ucTxt_PCMan.Readonly = false;
            this.ucTxt_PCMan.ShortcutsEnabled = true;
            this.ucTxt_PCMan.Size = new System.Drawing.Size(307, 42);
            this.ucTxt_PCMan.TabIndex = 98;
            this.ucTxt_PCMan.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxt_PCMan.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTxt_PCMan.EnterFromButton += new System.EventHandler(this.ucTxt_PCMan_EnterFromButton);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_trash_can_100;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(284, 287);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(34, 31);
            this.btnDelete.TabIndex = 100;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            // 
            // UCPCMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lbNamePCMan);
            this.Controls.Add(this.picBtnBack);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ucTxt_PCMan);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbPCMan);
            this.Controls.Add(this.lbMessage1);
            this.Name = "UCPCMan";
            this.Size = new System.Drawing.Size(334, 427);
            this.Load += new System.EventHandler(this.UCPCMan_Load);
            this.VisibleChanged += new System.EventHandler(this.UCPCMan_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        public UCTextBoxWithIcon ucTxt_PCMan;
        private System.Windows.Forms.PictureBox picBtnBack;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbPCMan;
        private System.Windows.Forms.Label lbMessage1;
        private System.Windows.Forms.Label lbNamePCMan;
        public System.Windows.Forms.Button btnDelete;
    }
}
