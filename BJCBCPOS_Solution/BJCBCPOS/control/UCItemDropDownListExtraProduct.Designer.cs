namespace BJCBCPOS
{
    partial class UCItemDropDownListExtraProduct
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
            this.lbText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.icon_Symbol = new System.Windows.Forms.PictureBox();
            this.lineShape1 = new System.Windows.Forms.PictureBox();
            this.lbValue = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon_Symbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineShape1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbText.ForeColor = System.Drawing.Color.Black;
            this.lbText.Location = new System.Drawing.Point(56, 3);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(242, 28);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "TEXT";
            this.lbText.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.icon_Symbol);
            this.panel1.Controls.Add(this.lineShape1);
            this.panel1.Controls.Add(this.lbValue);
            this.panel1.Controls.Add(this.lbText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 35);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // icon_Symbol
            // 
            this.icon_Symbol.Location = new System.Drawing.Point(17, 6);
            this.icon_Symbol.Name = "icon_Symbol";
            this.icon_Symbol.Size = new System.Drawing.Size(23, 23);
            this.icon_Symbol.TabIndex = 4;
            this.icon_Symbol.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BackColor = System.Drawing.Color.Gray;
            this.lineShape1.Location = new System.Drawing.Point(6, 33);
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.Size = new System.Drawing.Size(295, 2);
            this.lineShape1.TabIndex = 3;
            this.lineShape1.TabStop = false;
            // 
            // lbValue
            // 
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(246, 11);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(34, 13);
            this.lbValue.TabIndex = 2;
            this.lbValue.Text = "Value";
            this.lbValue.Visible = false;
            // 
            // UCItemDropDownListExtraProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "UCItemDropDownListExtraProduct";
            this.Size = new System.Drawing.Size(308, 35);
            this.Load += new System.EventHandler(this.UCItemDropDownList_Load);
            this.Click += new System.EventHandler(this.UCItemDropDownList_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon_Symbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineShape1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lbValue;
        public System.Windows.Forms.PictureBox lineShape1;
        private System.Windows.Forms.PictureBox icon_Symbol;

    }
}
