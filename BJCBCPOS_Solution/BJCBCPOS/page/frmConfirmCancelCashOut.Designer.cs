namespace BJCBCPOS
{
    partial class frmConfirmCancelCashOut
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
            this.lbMoneyBagNo = new System.Windows.Forms.Label();
            this.lbEnvelop = new System.Windows.Forms.Label();
            this.lbAmount = new System.Windows.Forms.Label();
            this.lbTxtAmount = new System.Windows.Forms.Label();
            this.lbConfirm = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbMoneyBagNo);
            this.panel1.Controls.Add(this.lbEnvelop);
            this.panel1.Controls.Add(this.lbAmount);
            this.panel1.Controls.Add(this.lbTxtAmount);
            this.panel1.Controls.Add(this.lbConfirm);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Location = new System.Drawing.Point(218, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 293);
            this.panel1.TabIndex = 116;
            // 
            // lbMoneyBagNo
            // 
            this.lbMoneyBagNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMoneyBagNo.Location = new System.Drawing.Point(15, 95);
            this.lbMoneyBagNo.Name = "lbMoneyBagNo";
            this.lbMoneyBagNo.Size = new System.Drawing.Size(242, 44);
            this.lbMoneyBagNo.TabIndex = 117;
            this.lbMoneyBagNo.Text = "เลขที่ซองเงิน";
            this.lbMoneyBagNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbEnvelop
            // 
            this.lbEnvelop.BackColor = System.Drawing.Color.Transparent;
            this.lbEnvelop.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEnvelop.ForeColor = System.Drawing.Color.Black;
            this.lbEnvelop.Location = new System.Drawing.Point(262, 95);
            this.lbEnvelop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEnvelop.Name = "lbEnvelop";
            this.lbEnvelop.Size = new System.Drawing.Size(312, 44);
            this.lbEnvelop.TabIndex = 116;
            this.lbEnvelop.Text = "9,999,000,000.00";
            this.lbEnvelop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAmount
            // 
            this.lbAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmount.Location = new System.Drawing.Point(16, 153);
            this.lbAmount.Name = "lbAmount";
            this.lbAmount.Size = new System.Drawing.Size(241, 44);
            this.lbAmount.TabIndex = 115;
            this.lbAmount.Text = "จำนวนเงิน";
            this.lbAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTxtAmount
            // 
            this.lbTxtAmount.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtAmount.ForeColor = System.Drawing.Color.Black;
            this.lbTxtAmount.Location = new System.Drawing.Point(262, 153);
            this.lbTxtAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtAmount.Name = "lbTxtAmount";
            this.lbTxtAmount.Size = new System.Drawing.Size(312, 44);
            this.lbTxtAmount.TabIndex = 111;
            this.lbTxtAmount.Text = "20,000.00";
            this.lbTxtAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbConfirm
            // 
            this.lbConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirm.Location = new System.Drawing.Point(13, 4);
            this.lbConfirm.Name = "lbConfirm";
            this.lbConfirm.Size = new System.Drawing.Size(556, 63);
            this.lbConfirm.TabIndex = 0;
            this.lbConfirm.Text = "ยืนยันยกเลิกส่งเงินตามรอบ";
            this.lbConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(18, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(193, 50);
            this.btnCancel.TabIndex = 112;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.popup_ok;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(236, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(329, 50);
            this.btnOk.TabIndex = 113;
            this.btnOk.Text = "ตกลง";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmConfirmCancelCashOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmCancelCashOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConfirmCancelCashOut";
            this.Load += new System.EventHandler(this.frmConfirmCancelCashOut_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbTxtAmount;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbMoneyBagNo;
        private System.Windows.Forms.Label lbEnvelop;
        private System.Windows.Forms.Label lbAmount;
    }
}