namespace BJCBCPOS
{
    partial class frmCashireMessage
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
            this.pnMainPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnMessagePanel = new System.Windows.Forms.Panel();
            this.pnMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMainPanel
            // 
            this.pnMainPanel.Controls.Add(this.label1);
            this.pnMainPanel.Controls.Add(this.btnOK);
            this.pnMainPanel.Controls.Add(this.pnMessagePanel);
            this.pnMainPanel.Location = new System.Drawing.Point(270, 80);
            this.pnMainPanel.Name = "pnMainPanel";
            this.pnMainPanel.Size = new System.Drawing.Size(535, 601);
            this.pnMainPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(520, 47);
            this.label1.TabIndex = 10;
            this.label1.Text = "Cashier Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(201, 540);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 45);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnMessagePanel
            // 
            this.pnMessagePanel.AutoScroll = true;
            this.pnMessagePanel.Location = new System.Drawing.Point(8, 83);
            this.pnMessagePanel.Name = "pnMessagePanel";
            this.pnMessagePanel.Size = new System.Drawing.Size(520, 440);
            this.pnMessagePanel.TabIndex = 0;
            // 
            // frmCashireMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pnMainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCashireMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCashireMessage";
            this.Load += new System.EventHandler(this.frmCashireMessage_Load);
            this.Shown += new System.EventHandler(this.frmCashireMessage_Shown);
            this.pnMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMainPanel;
        private System.Windows.Forms.Panel pnMessagePanel;
        public System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
    }
}