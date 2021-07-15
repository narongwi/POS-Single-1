namespace BJCBCPOS
{
    partial class frmMonitorCustomerFooter
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
            this.components = new System.ComponentModel.Container();
            this.lbTxtDate = new System.Windows.Forms.Label();
            this.lbTxtTime = new System.Windows.Forms.Label();
            this.lbTxtCashier = new System.Windows.Forms.Label();
            this.lbCashier = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTxtDate
            // 
            this.lbTxtDate.AutoSize = true;
            this.lbTxtDate.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbTxtDate.Location = new System.Drawing.Point(326, 5);
            this.lbTxtDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtDate.Name = "lbTxtDate";
            this.lbTxtDate.Size = new System.Drawing.Size(80, 18);
            this.lbTxtDate.TabIndex = 61;
            this.lbTxtDate.Text = "01/01/2019";
            // 
            // lbTxtTime
            // 
            this.lbTxtTime.AutoSize = true;
            this.lbTxtTime.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbTxtTime.Location = new System.Drawing.Point(424, 5);
            this.lbTxtTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtTime.Name = "lbTxtTime";
            this.lbTxtTime.Size = new System.Drawing.Size(64, 18);
            this.lbTxtTime.TabIndex = 60;
            this.lbTxtTime.Text = "00:00:00";
            // 
            // lbTxtCashier
            // 
            this.lbTxtCashier.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbTxtCashier.Location = new System.Drawing.Point(82, 5);
            this.lbTxtCashier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtCashier.Name = "lbTxtCashier";
            this.lbTxtCashier.Size = new System.Drawing.Size(201, 18);
            this.lbTxtCashier.TabIndex = 59;
            // 
            // lbCashier
            // 
            this.lbCashier.BackColor = System.Drawing.Color.Transparent;
            this.lbCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbCashier.Location = new System.Drawing.Point(10, 5);
            this.lbCashier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCashier.Name = "lbCashier";
            this.lbCashier.Size = new System.Drawing.Size(67, 18);
            this.lbCashier.TabIndex = 58;
            this.lbCashier.Text = "Cashier : ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(495, 1);
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(0, 29);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(495, 1);
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // frmMonitorCustomerFooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(495, 30);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbTxtDate);
            this.Controls.Add(this.lbTxtTime);
            this.Controls.Add(this.lbTxtCashier);
            this.Controls.Add(this.lbCashier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(0, 30);
            this.Name = "frmMonitorCustomerFooter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmMonitorCustomerFooter";
            this.Load += new System.EventHandler(this.frmMonitorCustomerFooter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbTxtDate;
        public System.Windows.Forms.Label lbTxtTime;
        public System.Windows.Forms.Label lbTxtCashier;
        public System.Windows.Forms.Label lbCashier;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}