namespace BJCBCPOS
{
    partial class UCItemCancelCashOut
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
            this.lb_Amt = new System.Windows.Forms.Label();
            this.lbEnvelopNum = new System.Windows.Forms.Label();
            this.lbNo = new System.Windows.Forms.Label();
            this.picBin = new System.Windows.Forms.PictureBox();
            this.lbRefNo = new System.Windows.Forms.Label();
            this.lb_Currency = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBin)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_Amt
            // 
            this.lb_Amt.BackColor = System.Drawing.Color.Transparent;
            this.lb_Amt.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Amt.Location = new System.Drawing.Point(410, 13);
            this.lb_Amt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Amt.Name = "lb_Amt";
            this.lb_Amt.Size = new System.Drawing.Size(153, 30);
            this.lb_Amt.TabIndex = 58;
            this.lb_Amt.Text = "1234567890";
            this.lb_Amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lb_Amt.Click += new System.EventHandler(this.Click_Item);
            // 
            // lbEnvelopNum
            // 
            this.lbEnvelopNum.BackColor = System.Drawing.Color.Transparent;
            this.lbEnvelopNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEnvelopNum.Location = new System.Drawing.Point(108, 13);
            this.lbEnvelopNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEnvelopNum.Name = "lbEnvelopNum";
            this.lbEnvelopNum.Size = new System.Drawing.Size(108, 30);
            this.lbEnvelopNum.TabIndex = 57;
            this.lbEnvelopNum.Text = "1234";
            this.lbEnvelopNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEnvelopNum.Click += new System.EventHandler(this.Click_Item);
            // 
            // lbNo
            // 
            this.lbNo.BackColor = System.Drawing.Color.Transparent;
            this.lbNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNo.Location = new System.Drawing.Point(25, 13);
            this.lbNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNo.Name = "lbNo";
            this.lbNo.Size = new System.Drawing.Size(31, 30);
            this.lbNo.TabIndex = 54;
            this.lbNo.Text = "-1";
            this.lbNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNo.Click += new System.EventHandler(this.Click_Item);
            // 
            // picBin
            // 
            this.picBin.Image = global::BJCBCPOS.Properties.Resources.icons8_trash_can_100;
            this.picBin.Location = new System.Drawing.Point(572, 7);
            this.picBin.Name = "picBin";
            this.picBin.Size = new System.Drawing.Size(42, 41);
            this.picBin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBin.TabIndex = 62;
            this.picBin.TabStop = false;
            this.picBin.Click += new System.EventHandler(this.picBin_Click);
            // 
            // lbRefNo
            // 
            this.lbRefNo.AutoSize = true;
            this.lbRefNo.Location = new System.Drawing.Point(3, 40);
            this.lbRefNo.Name = "lbRefNo";
            this.lbRefNo.Size = new System.Drawing.Size(38, 13);
            this.lbRefNo.TabIndex = 63;
            this.lbRefNo.Text = "RefNo";
            this.lbRefNo.Visible = false;
            // 
            // lb_Currency
            // 
            this.lb_Currency.BackColor = System.Drawing.Color.Transparent;
            this.lb_Currency.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Currency.Location = new System.Drawing.Point(271, 13);
            this.lb_Currency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Currency.Name = "lb_Currency";
            this.lb_Currency.Size = new System.Drawing.Size(136, 30);
            this.lb_Currency.TabIndex = 64;
            this.lb_Currency.Text = "LAK";
            this.lb_Currency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Currency.Click += new System.EventHandler(this.Click_Item);
            // 
            // UCItemCancelCashOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lb_Currency);
            this.Controls.Add(this.lbRefNo);
            this.Controls.Add(this.picBin);
            this.Controls.Add(this.lb_Amt);
            this.Controls.Add(this.lbEnvelopNum);
            this.Controls.Add(this.lbNo);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "UCItemCancelCashOut";
            this.Size = new System.Drawing.Size(620, 55);
            this.Load += new System.EventHandler(this.UCItemCancelCashOut_Load);
            this.Click += new System.EventHandler(this.Click_Item);
            ((System.ComponentModel.ISupportInitialize)(this.picBin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_Amt;
        private System.Windows.Forms.Label lbEnvelopNum;
        private System.Windows.Forms.Label lbNo;
        private System.Windows.Forms.PictureBox picBin;
        private System.Windows.Forms.Label lbRefNo;
        private System.Windows.Forms.Label lb_Currency;
    }
}
