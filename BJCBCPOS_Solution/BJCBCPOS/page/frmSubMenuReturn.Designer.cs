namespace BJCBCPOS
{
    partial class frmSubMenuReturn
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
            this.btnReturnInvoice = new System.Windows.Forms.Button();
            this.btnReturnScanProduct = new System.Windows.Forms.Button();
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
            this.btnBack.Location = new System.Drawing.Point(6, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(53, 44);
            this.btnBack.TabIndex = 19;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnReturnInvoice
            // 
            this.btnReturnInvoice.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Search_ReturnFromInvoice;
            this.btnReturnInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReturnInvoice.FlatAppearance.BorderSize = 0;
            this.btnReturnInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnInvoice.ForeColor = System.Drawing.Color.Black;
            this.btnReturnInvoice.Location = new System.Drawing.Point(21, 89);
            this.btnReturnInvoice.Name = "btnReturnInvoice";
            this.btnReturnInvoice.Size = new System.Drawing.Size(265, 161);
            this.btnReturnInvoice.TabIndex = 20;
            this.btnReturnInvoice.Text = "รับคืนจากใบเสร็จ";
            this.btnReturnInvoice.UseVisualStyleBackColor = true;
            this.btnReturnInvoice.Click += new System.EventHandler(this.btnDaySaleReport_Click);
            // 
            // btnReturnScanProduct
            // 
            this.btnReturnScanProduct.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Search_ReturnFromInvoice;
            this.btnReturnScanProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReturnScanProduct.FlatAppearance.BorderSize = 0;
            this.btnReturnScanProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnScanProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnScanProduct.ForeColor = System.Drawing.Color.Black;
            this.btnReturnScanProduct.Location = new System.Drawing.Point(305, 89);
            this.btnReturnScanProduct.Name = "btnReturnScanProduct";
            this.btnReturnScanProduct.Size = new System.Drawing.Size(265, 161);
            this.btnReturnScanProduct.TabIndex = 21;
            this.btnReturnScanProduct.Text = "รับคืนจากสินค้า";
            this.btnReturnScanProduct.UseVisualStyleBackColor = true;
            this.btnReturnScanProduct.Click += new System.EventHandler(this.btnInvoiceReport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lbOtherPayment);
            this.panel1.Controls.Add(this.btnReturnInvoice);
            this.panel1.Controls.Add(this.btnReturnScanProduct);
            this.panel1.Location = new System.Drawing.Point(9, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 272);
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
            this.lbOtherPayment.Size = new System.Drawing.Size(586, 59);
            this.lbOtherPayment.TabIndex = 24;
            this.lbOtherPayment.Text = "รับคืน";
            this.lbOtherPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSubMenuReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(634, 312);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSubMenuReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmGoodSales";
            this.Load += new System.EventHandler(this.frmSubMenuReturn_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnReturnInvoice;
        private System.Windows.Forms.Button btnReturnScanProduct;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbOtherPayment;
    }
}