namespace BJCBCPOS.dialog
{
    partial class frmSignature
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lb_Signature = new System.Windows.Forms.Label();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.panel_consent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(50, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 412);
            this.panel1.TabIndex = 208;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BackgroundImage = global::BJCBCPOS.Properties.Resources.consent_btn_enableconfirm;
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(360, 535);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(176, 52);
            this.btnConfirm.TabIndex = 211;
            this.btnConfirm.Text = "ใช่";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImage = global::BJCBCPOS.Properties.Resources.consent_btn_cancel;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnClear.Location = new System.Drawing.Point(558, 535);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(176, 50);
            this.btnClear.TabIndex = 210;
            this.btnClear.Text = "ไม่ใช่";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::BJCBCPOS.Properties.Resources.NoPath_3x;
            this.picLogo.Location = new System.Drawing.Point(5, 5);
            this.picLogo.Margin = new System.Windows.Forms.Padding(2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(72, 87);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 212;
            this.picLogo.TabStop = false;
            // 
            // lb_Signature
            // 
            this.lb_Signature.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_Signature.AutoSize = true;
            this.lb_Signature.Font = new System.Drawing.Font("DB Helvethaica X Bd", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Signature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.lb_Signature.Location = new System.Drawing.Point(213, 272);
            this.lb_Signature.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Signature.Name = "lb_Signature";
            this.lb_Signature.Size = new System.Drawing.Size(381, 53);
            this.lb_Signature.TabIndex = 190;
            this.lb_Signature.Text = "กรุณาลงลายมือชื่อคุณที่นี่";
            this.lb_Signature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Signature.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_Signature_MouseDown);
            // 
            // PictureBox3
            // 
            this.PictureBox3.BackColor = System.Drawing.Color.White;
            this.PictureBox3.Location = new System.Drawing.Point(64, 116);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(670, 388);
            this.PictureBox3.TabIndex = 189;
            this.PictureBox3.TabStop = false;
            this.PictureBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox3_Paint);
            this.PictureBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox3_MouseDown);
            this.PictureBox3.MouseLeave += new System.EventHandler(this.PictureBox3_MouseLeave);
            this.PictureBox3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox3_MouseMove);
            this.PictureBox3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox3_MouseUp);
            // 
            // frmSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BJCBCPOS.Properties.Resources.background_consent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lb_Signature);
            this.Controls.Add(this.PictureBox3);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSignature";
            this.Text = "frmSignature";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox picLogo;
        internal System.Windows.Forms.Label lb_Signature;
        internal System.Windows.Forms.PictureBox PictureBox3;
    }
}