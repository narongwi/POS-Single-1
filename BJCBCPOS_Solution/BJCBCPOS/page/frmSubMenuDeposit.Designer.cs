namespace BJCBCPOS.page
{
    partial class frmSubMenuDeposit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubMenuDeposit));
            this.button1 = new System.Windows.Forms.Button();
            this.btnReturnFromProduct = new System.Windows.Forms.Button();
            this.btnReturnFromReceipt = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(493, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 100);
            this.button1.TabIndex = 124;
            this.button1.Text = "รับชำระขายเชื่อ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnReturnFromProduct
            // 
            this.btnReturnFromProduct.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReturnFromProduct.BackgroundImage")));
            this.btnReturnFromProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReturnFromProduct.FlatAppearance.BorderSize = 0;
            this.btnReturnFromProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnFromProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnFromProduct.ForeColor = System.Drawing.Color.Black;
            this.btnReturnFromProduct.Location = new System.Drawing.Point(253, 57);
            this.btnReturnFromProduct.Margin = new System.Windows.Forms.Padding(5);
            this.btnReturnFromProduct.Name = "btnReturnFromProduct";
            this.btnReturnFromProduct.Size = new System.Drawing.Size(230, 100);
            this.btnReturnFromProduct.TabIndex = 123;
            this.btnReturnFromProduct.Text = "รับชำระ POD";
            this.btnReturnFromProduct.UseVisualStyleBackColor = true;
            this.btnReturnFromProduct.Click += new System.EventHandler(this.btnReturnFromProduct_Click);
            // 
            // btnReturnFromReceipt
            // 
            this.btnReturnFromReceipt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReturnFromReceipt.BackgroundImage")));
            this.btnReturnFromReceipt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReturnFromReceipt.FlatAppearance.BorderSize = 0;
            this.btnReturnFromReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnFromReceipt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnFromReceipt.ForeColor = System.Drawing.Color.Black;
            this.btnReturnFromReceipt.Location = new System.Drawing.Point(13, 57);
            this.btnReturnFromReceipt.Margin = new System.Windows.Forms.Padding(5);
            this.btnReturnFromReceipt.Name = "btnReturnFromReceipt";
            this.btnReturnFromReceipt.Size = new System.Drawing.Size(230, 100);
            this.btnReturnFromReceipt.TabIndex = 122;
            this.btnReturnFromReceipt.Text = "รับเงินมัดจำ";
            this.btnReturnFromReceipt.UseVisualStyleBackColor = true;
            this.btnReturnFromReceipt.Click += new System.EventHandler(this.btnReturnFromReceipt_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BJCBCPOS.Properties.Resources.icon_textbox_delete;
            this.pictureBox2.Location = new System.Drawing.Point(693, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 125;
            this.pictureBox2.TabStop = false;
            // 
            // frmSubMenuDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(738, 170);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReturnFromProduct);
            this.Controls.Add(this.btnReturnFromReceipt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSubMenuDeposit";
            this.Text = "frmSubMenuDeposit";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReturnFromProduct;
        private System.Windows.Forms.Button btnReturnFromReceipt;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}