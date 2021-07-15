namespace BJCBCPOS
{
    partial class UCDropDownLanguage
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
            this.lbLanguageName = new System.Windows.Forms.Label();
            this.pic_Flag = new System.Windows.Forms.PictureBox();
            this.pic_Line = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Flag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Line)).BeginInit();
            this.SuspendLayout();
            // 
            // lbLanguageName
            // 
            this.lbLanguageName.AutoSize = true;
            this.lbLanguageName.Font = new System.Drawing.Font("Prompt SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbLanguageName.Location = new System.Drawing.Point(73, 7);
            this.lbLanguageName.Name = "lbLanguageName";
            this.lbLanguageName.Size = new System.Drawing.Size(53, 29);
            this.lbLanguageName.TabIndex = 1;
            this.lbLanguageName.Text = "THA";
            this.lbLanguageName.Click += new System.EventHandler(this.DropDownLanguage_Click);
            // 
            // pic_Flag
            // 
            this.pic_Flag.BackColor = System.Drawing.Color.White;
            this.pic_Flag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_Flag.Location = new System.Drawing.Point(18, 10);
            this.pic_Flag.Name = "pic_Flag";
            this.pic_Flag.Size = new System.Drawing.Size(38, 25);
            this.pic_Flag.TabIndex = 0;
            this.pic_Flag.TabStop = false;
            this.pic_Flag.Click += new System.EventHandler(this.DropDownLanguage_Click);
            // 
            // pic_Line
            // 
            this.pic_Line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pic_Line.Location = new System.Drawing.Point(10, 43);
            this.pic_Line.Name = "pic_Line";
            this.pic_Line.Size = new System.Drawing.Size(120, 2);
            this.pic_Line.TabIndex = 2;
            this.pic_Line.TabStop = false;
            this.pic_Line.Click += new System.EventHandler(this.DropDownLanguage_Click);
            // 
            // UCDropDownLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pic_Line);
            this.Controls.Add(this.lbLanguageName);
            this.Controls.Add(this.pic_Flag);
            this.Name = "UCDropDownLanguage";
            this.Size = new System.Drawing.Size(140, 45);
            this.Load += new System.EventHandler(this.UCDropDownLanguage_Load);
            this.Click += new System.EventHandler(this.DropDownLanguage_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Flag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Line)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Flag;
        private System.Windows.Forms.Label lbLanguageName;
        private System.Windows.Forms.PictureBox pic_Line;
    }
}
