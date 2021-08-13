namespace BJCBCPOS
{
    partial class frmProductAndService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductAndService));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucButtonPayment4 = new BJCBCPOS.UCButtonPayment();
            this.ucButtonPayment3 = new BJCBCPOS.UCButtonPayment();
            this.ucButtonPayment1 = new BJCBCPOS.UCButtonPayment();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lbProductAndServices = new System.Windows.Forms.Label();
            this.ucButtonPayment2 = new BJCBCPOS.UCButtonPayment();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox7);
            this.panel2.Controls.Add(this.ucButtonPayment2);
            this.panel2.Controls.Add(this.ucButtonPayment4);
            this.panel2.Controls.Add(this.ucButtonPayment3);
            this.panel2.Controls.Add(this.ucButtonPayment1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(55, 106);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(901, 540);
            this.panel2.TabIndex = 7;
            // 
            // ucButtonPayment4
            // 
            this.ucButtonPayment4.BackColor = System.Drawing.Color.White;
            this.ucButtonPayment4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPayment4.BackgroundImage")));
            this.ucButtonPayment4.Location = new System.Drawing.Point(611, 98);
            this.ucButtonPayment4.Name = "ucButtonPayment4";
            this.ucButtonPayment4.Size = new System.Drawing.Size(265, 200);
            this.ucButtonPayment4.TabIndex = 11;
            this.ucButtonPayment4.TextButton = "ชำระขายเชื่อ";
            this.ucButtonPayment4.ButtonClick += new System.EventHandler(this.ucButtonPayment4_ButtonClick);
            // 
            // ucButtonPayment3
            // 
            this.ucButtonPayment3.BackColor = System.Drawing.Color.White;
            this.ucButtonPayment3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPayment3.BackgroundImage")));
            this.ucButtonPayment3.Location = new System.Drawing.Point(320, 98);
            this.ucButtonPayment3.Name = "ucButtonPayment3";
            this.ucButtonPayment3.Size = new System.Drawing.Size(265, 200);
            this.ucButtonPayment3.TabIndex = 10;
            this.ucButtonPayment3.TextButton = "ชำระ POD";
            this.ucButtonPayment3.ButtonClick += new System.EventHandler(this.ucButtonPayment3_ButtonClick);
            // 
            // ucButtonPayment1
            // 
            this.ucButtonPayment1.BackColor = System.Drawing.Color.White;
            this.ucButtonPayment1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPayment1.BackgroundImage")));
            this.ucButtonPayment1.Location = new System.Drawing.Point(28, 98);
            this.ucButtonPayment1.Name = "ucButtonPayment1";
            this.ucButtonPayment1.Size = new System.Drawing.Size(265, 200);
            this.ucButtonPayment1.TabIndex = 8;
            this.ucButtonPayment1.TextButton = "รับเงินมัดจำ";
            this.ucButtonPayment1.ButtonClick += new System.EventHandler(this.ucButtonPayment1_ButtonClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_260_3x;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lbProductAndServices);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 75);
            this.panel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.btnBack.Location = new System.Drawing.Point(10, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(53, 56);
            this.btnBack.TabIndex = 3;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lbProductAndServices
            // 
            this.lbProductAndServices.BackColor = System.Drawing.Color.Transparent;
            this.lbProductAndServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductAndServices.ForeColor = System.Drawing.Color.White;
            this.lbProductAndServices.Location = new System.Drawing.Point(3, 5);
            this.lbProductAndServices.Name = "lbProductAndServices";
            this.lbProductAndServices.Size = new System.Drawing.Size(895, 65);
            this.lbProductAndServices.TabIndex = 2;
            this.lbProductAndServices.Text = "สินค้าและบริการอื่นๆ";
            this.lbProductAndServices.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucButtonPayment2
            // 
            this.ucButtonPayment2.BackColor = System.Drawing.Color.White;
            this.ucButtonPayment2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPayment2.BackgroundImage")));
            this.ucButtonPayment2.Location = new System.Drawing.Point(320, 321);
            this.ucButtonPayment2.Name = "ucButtonPayment2";
            this.ucButtonPayment2.Size = new System.Drawing.Size(265, 200);
            this.ucButtonPayment2.TabIndex = 12;
            this.ucButtonPayment2.TextButton = "Big Service";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::BJCBCPOS.Properties.Resources.BigService;
            this.pictureBox7.Location = new System.Drawing.Point(337, 334);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(230, 127);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 24;
            this.pictureBox7.TabStop = false;
            // 
            // frmProductAndService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProductAndService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmProductAndService";
            this.Load += new System.EventHandler(this.frmProductAndService_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbProductAndServices;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel2;
        private UCButtonPayment ucButtonPayment4;
        private UCButtonPayment ucButtonPayment3;
        private UCButtonPayment ucButtonPayment1;
        private UCButtonPayment ucButtonPayment2;
        private System.Windows.Forms.PictureBox pictureBox7;
    }
}