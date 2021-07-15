namespace BJCBCPOS
{
    partial class frmConfirmCashout
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbTxtAmount = new System.Windows.Forms.Label();
            this.lbConfirm = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.lbConfirm);
            this.panel1.Location = new System.Drawing.Point(252, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 260);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(16, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(506, 32);
            this.label1.TabIndex = 118;
            this.label1.Text = "label1";
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.button_search;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(205, 196);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(314, 50);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancel.Location = new System.Drawing.Point(14, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 50);
            this.btnCancel.TabIndex = 116;
            this.btnCancel.Text = "ไม่ใช่";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::BJCBCPOS.Properties.Resources.green_total;
            this.panel3.Controls.Add(this.lbTxtAmount);
            this.panel3.Location = new System.Drawing.Point(14, 116);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(506, 68);
            this.panel3.TabIndex = 115;
            // 
            // lbTxtAmount
            // 
            this.lbTxtAmount.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtAmount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbTxtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtAmount.ForeColor = System.Drawing.Color.Black;
            this.lbTxtAmount.Location = new System.Drawing.Point(71, 5);
            this.lbTxtAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtAmount.Name = "lbTxtAmount";
            this.lbTxtAmount.Size = new System.Drawing.Size(430, 58);
            this.lbTxtAmount.TabIndex = 111;
            this.lbTxtAmount.Text = "20,000.00";
            this.lbTxtAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbConfirm
            // 
            this.lbConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirm.Location = new System.Drawing.Point(12, 9);
            this.lbConfirm.Name = "lbConfirm";
            this.lbConfirm.Size = new System.Drawing.Size(506, 63);
            this.lbConfirm.TabIndex = 114;
            this.lbConfirm.Text = "ยืนยันส่งเงินตามรอบ";
            this.lbConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmConfirmCashout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmCashout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConfirmCashout";
            this.Load += new System.EventHandler(this.frmConfirmCashout_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbTxtAmount;
        private System.Windows.Forms.Label lbConfirm;
        private System.Windows.Forms.Label label1;
    }
}