namespace BJCBCPOS
{
    partial class frmSubMenuReport
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDaySaleReport = new System.Windows.Forms.Button();
            this.btnInvoiceReport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbOtherPayment = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.BackgroundImage = global::BJCBCPOS.Properties.Resources.payment_disable;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.btnBack.Location = new System.Drawing.Point(5, 7);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(53, 45);
            this.btnBack.TabIndex = 19;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnDaySaleReport
            // 
            this.btnDaySaleReport.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Search_ReturnFromInvoice;
            this.btnDaySaleReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDaySaleReport.FlatAppearance.BorderSize = 0;
            this.btnDaySaleReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDaySaleReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDaySaleReport.ForeColor = System.Drawing.Color.Black;
            this.btnDaySaleReport.Location = new System.Drawing.Point(23, 92);
            this.btnDaySaleReport.Name = "btnDaySaleReport";
            this.btnDaySaleReport.Size = new System.Drawing.Size(265, 200);
            this.btnDaySaleReport.TabIndex = 20;
            this.btnDaySaleReport.Text = "ยอดขายประจำวัน";
            this.btnDaySaleReport.UseVisualStyleBackColor = true;
            this.btnDaySaleReport.Click += new System.EventHandler(this.btnDaySaleReport_Click);
            // 
            // btnInvoiceReport
            // 
            this.btnInvoiceReport.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Search_ReturnFromInvoice;
            this.btnInvoiceReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInvoiceReport.FlatAppearance.BorderSize = 0;
            this.btnInvoiceReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoiceReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoiceReport.ForeColor = System.Drawing.Color.Black;
            this.btnInvoiceReport.Location = new System.Drawing.Point(318, 92);
            this.btnInvoiceReport.Name = "btnInvoiceReport";
            this.btnInvoiceReport.Size = new System.Drawing.Size(265, 200);
            this.btnInvoiceReport.TabIndex = 21;
            this.btnInvoiceReport.Text = "ข้อมูลใบเสร็จ";
            this.btnInvoiceReport.UseVisualStyleBackColor = true;
            this.btnInvoiceReport.Click += new System.EventHandler(this.btnInvoiceReport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lbOtherPayment);
            this.panel1.Controls.Add(this.btnDaySaleReport);
            this.panel1.Controls.Add(this.btnInvoiceReport);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 312);
            this.panel1.TabIndex = 22;
            // 
            // lbOtherPayment
            // 
            this.lbOtherPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.lbOtherPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbOtherPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOtherPayment.ForeColor = System.Drawing.Color.White;
            this.lbOtherPayment.Location = new System.Drawing.Point(0, 0);
            this.lbOtherPayment.Name = "lbOtherPayment";
            this.lbOtherPayment.Size = new System.Drawing.Size(605, 59);
            this.lbOtherPayment.TabIndex = 22;
            this.lbOtherPayment.Text = "รายงาน";
            this.lbOtherPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSubMenuReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(605, 312);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSubMenuReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmGoodSales";
            this.Load += new System.EventHandler(this.frmSubMenuReport_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDaySaleReport;
        private System.Windows.Forms.Button btnInvoiceReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbOtherPayment;
    }
}