namespace BJCBCPOS.dialog
{
    partial class frmConsentCashier
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
            this.VScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.lb_Argee1 = new System.Windows.Forms.Label();
            this.lb_Argee2 = new System.Windows.Forms.Label();
            this.ConsentRTB = new System.Windows.Forms.RichTextBox();
            this.chkb_Agree = new System.Windows.Forms.CheckBox();
            this.ButtonUp = new System.Windows.Forms.Button();
            this.ButtonDown = new System.Windows.Forms.Button();
            this.pic_UnCheck = new System.Windows.Forms.PictureBox();
            this.pic_Check = new System.Windows.Forms.PictureBox();
            this.ButtonEnd = new System.Windows.Forms.Button();
            this.lb_Header = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelCurrency = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_UnCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Check)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // VScrollBar1
            // 
            this.VScrollBar1.LargeChange = 500;
            this.VScrollBar1.Location = new System.Drawing.Point(885, 176);
            this.VScrollBar1.Maximum = 1000;
            this.VScrollBar1.Name = "VScrollBar1";
            this.VScrollBar1.Size = new System.Drawing.Size(25, 320);
            this.VScrollBar1.SmallChange = 500;
            this.VScrollBar1.TabIndex = 205;
            // 
            // lb_Argee1
            // 
            this.lb_Argee1.AutoSize = true;
            this.lb_Argee1.BackColor = System.Drawing.Color.White;
            this.lb_Argee1.Font = new System.Drawing.Font("DB Helvethaica X", 15.75F);
            this.lb_Argee1.Location = new System.Drawing.Point(141, 521);
            this.lb_Argee1.Name = "lb_Argee1";
            this.lb_Argee1.Size = new System.Drawing.Size(54, 24);
            this.lb_Argee1.TabIndex = 204;
            this.lb_Argee1.Text = "Label1";
            // 
            // lb_Argee2
            // 
            this.lb_Argee2.BackColor = System.Drawing.Color.White;
            this.lb_Argee2.Font = new System.Drawing.Font("DB Helvethaica X", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Argee2.Location = new System.Drawing.Point(127, 551);
            this.lb_Argee2.Name = "lb_Argee2";
            this.lb_Argee2.Size = new System.Drawing.Size(790, 50);
            this.lb_Argee2.TabIndex = 201;
            this.lb_Argee2.Text = "Label2";
            // 
            // ConsentRTB
            // 
            this.ConsentRTB.BackColor = System.Drawing.SystemColors.Window;
            this.ConsentRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsentRTB.Cursor = System.Windows.Forms.Cursors.No;
            this.ConsentRTB.Font = new System.Drawing.Font("DB Helvethaica X", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsentRTB.Location = new System.Drawing.Point(112, 177);
            this.ConsentRTB.Name = "ConsentRTB";
            this.ConsentRTB.ReadOnly = true;
            this.ConsentRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ConsentRTB.Size = new System.Drawing.Size(794, 322);
            this.ConsentRTB.TabIndex = 196;
            this.ConsentRTB.Text = "";
            this.ConsentRTB.VScroll += new System.EventHandler(this.ConsentRTB_VScroll);
            // 
            // chkb_Agree
            // 
            this.chkb_Agree.AutoSize = true;
            this.chkb_Agree.Font = new System.Drawing.Font("DB Helvethaica X", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkb_Agree.Location = new System.Drawing.Point(116, 524);
            this.chkb_Agree.Name = "chkb_Agree";
            this.chkb_Agree.Size = new System.Drawing.Size(15, 14);
            this.chkb_Agree.TabIndex = 200;
            this.chkb_Agree.UseVisualStyleBackColor = true;
            // 
            // ButtonUp
            // 
            this.ButtonUp.BackColor = System.Drawing.Color.White;
            this.ButtonUp.BackgroundImage = global::BJCBCPOS.Properties.Resources.scroll_1;
            this.ButtonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ButtonUp.FlatAppearance.BorderSize = 0;
            this.ButtonUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.ButtonUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.ButtonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonUp.Location = new System.Drawing.Point(885, 168);
            this.ButtonUp.Name = "ButtonUp";
            this.ButtonUp.Size = new System.Drawing.Size(26, 26);
            this.ButtonUp.TabIndex = 197;
            this.ButtonUp.UseVisualStyleBackColor = false;
            this.ButtonUp.Click += new System.EventHandler(this.ButtonUp_Click);
            // 
            // ButtonDown
            // 
            this.ButtonDown.BackColor = System.Drawing.Color.White;
            this.ButtonDown.BackgroundImage = global::BJCBCPOS.Properties.Resources.scroll_2;
            this.ButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ButtonDown.FlatAppearance.BorderSize = 0;
            this.ButtonDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.ButtonDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.ButtonDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDown.Location = new System.Drawing.Point(885, 480);
            this.ButtonDown.Name = "ButtonDown";
            this.ButtonDown.Size = new System.Drawing.Size(26, 26);
            this.ButtonDown.TabIndex = 198;
            this.ButtonDown.UseVisualStyleBackColor = false;
            this.ButtonDown.Click += new System.EventHandler(this.ButtonDown_Click);
            // 
            // pic_UnCheck
            // 
            this.pic_UnCheck.BackColor = System.Drawing.Color.White;
            this.pic_UnCheck.Image = global::BJCBCPOS.Properties.Resources.non_check_box;
            this.pic_UnCheck.Location = new System.Drawing.Point(108, 517);
            this.pic_UnCheck.Name = "pic_UnCheck";
            this.pic_UnCheck.Size = new System.Drawing.Size(30, 30);
            this.pic_UnCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic_UnCheck.TabIndex = 202;
            this.pic_UnCheck.TabStop = false;
            this.pic_UnCheck.Tag = "NonCheck";
            this.pic_UnCheck.Click += new System.EventHandler(this.pic_UnCheck_Click);
            // 
            // pic_Check
            // 
            this.pic_Check.Image = global::BJCBCPOS.Properties.Resources.check_box;
            this.pic_Check.Location = new System.Drawing.Point(108, 517);
            this.pic_Check.Name = "pic_Check";
            this.pic_Check.Size = new System.Drawing.Size(30, 30);
            this.pic_Check.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic_Check.TabIndex = 203;
            this.pic_Check.TabStop = false;
            this.pic_Check.Tag = "Check";
            // 
            // ButtonEnd
            // 
            this.ButtonEnd.BackColor = System.Drawing.Color.White;
            this.ButtonEnd.BackgroundImage = global::BJCBCPOS.Properties.Resources.scroll_3;
            this.ButtonEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ButtonEnd.FlatAppearance.BorderSize = 0;
            this.ButtonEnd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.ButtonEnd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.ButtonEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonEnd.Location = new System.Drawing.Point(885, 508);
            this.ButtonEnd.Name = "ButtonEnd";
            this.ButtonEnd.Size = new System.Drawing.Size(26, 26);
            this.ButtonEnd.TabIndex = 199;
            this.ButtonEnd.UseVisualStyleBackColor = false;
            this.ButtonEnd.Click += new System.EventHandler(this.ButtonEnd_Click);
            // 
            // lb_Header
            // 
            this.lb_Header.Font = new System.Drawing.Font("DB Helvethaica X", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lb_Header.Location = new System.Drawing.Point(173, 61);
            this.lb_Header.Name = "lb_Header";
            this.lb_Header.Size = new System.Drawing.Size(685, 74);
            this.lb_Header.TabIndex = 206;
            this.lb_Header.Text = "Label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.panel_consent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(62, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 479);
            this.panel1.TabIndex = 207;
            // 
            // btnCancelCurrency
            // 
            this.btnCancelCurrency.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelCurrency.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Cancel_Confirmpayment;
            this.btnCancelCurrency.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelCurrency.FlatAppearance.BorderSize = 0;
            this.btnCancelCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelCurrency.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelCurrency.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancelCurrency.Location = new System.Drawing.Point(759, 661);
            this.btnCancelCurrency.Name = "btnCancelCurrency";
            this.btnCancelCurrency.Size = new System.Drawing.Size(180, 62);
            this.btnCancelCurrency.TabIndex = 208;
            this.btnCancelCurrency.Text = "ไม่ใช่";
            this.btnCancelCurrency.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.payment_disable;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Gray;
            this.btnOk.Location = new System.Drawing.Point(552, 661);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(180, 62);
            this.btnOk.TabIndex = 209;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::BJCBCPOS.Properties.Resources.NoPath_3x;
            this.picLogo.Location = new System.Drawing.Point(0, 9);
            this.picLogo.Margin = new System.Windows.Forms.Padding(2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(110, 131);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 210;
            this.picLogo.TabStop = false;
            // 
            // frmConsentCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BJCBCPOS.Properties.Resources.background_consent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.pic_UnCheck);
            this.Controls.Add(this.pic_Check);
            this.Controls.Add(this.ButtonDown);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancelCurrency);
            this.Controls.Add(this.lb_Header);
            this.Controls.Add(this.ButtonUp);
            this.Controls.Add(this.VScrollBar1);
            this.Controls.Add(this.lb_Argee1);
            this.Controls.Add(this.lb_Argee2);
            this.Controls.Add(this.ConsentRTB);
            this.Controls.Add(this.chkb_Agree);
            this.Controls.Add(this.ButtonEnd);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConsentCashier";
            this.Text = "frmConsent";
            this.Shown += new System.EventHandler(this.frmConsentCashier_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pic_UnCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Check)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button ButtonUp;
        internal System.Windows.Forms.Button ButtonDown;
        internal System.Windows.Forms.VScrollBar VScrollBar1;
        internal System.Windows.Forms.Label lb_Argee1;
        internal System.Windows.Forms.PictureBox pic_UnCheck;
        internal System.Windows.Forms.PictureBox pic_Check;
        internal System.Windows.Forms.Label lb_Argee2;
        internal System.Windows.Forms.RichTextBox ConsentRTB;
        internal System.Windows.Forms.CheckBox chkb_Agree;
        internal System.Windows.Forms.Button ButtonEnd;
        internal System.Windows.Forms.Label lb_Header;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelCurrency;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox picLogo;
    }
}