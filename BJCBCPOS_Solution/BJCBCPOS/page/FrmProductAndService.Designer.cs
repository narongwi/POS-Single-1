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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lbProductAndServices = new System.Windows.Forms.Label();
            this.btnService_1 = new System.Windows.Forms.Button();
            this.btnService_5 = new System.Windows.Forms.Button();
            this.btnService_2 = new System.Windows.Forms.Button();
            this.btnService_4 = new System.Windows.Forms.Button();
            this.btnService_3 = new System.Windows.Forms.Button();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucButtonPayment4);
            this.panel2.Controls.Add(this.ucButtonPayment3);
            this.panel2.Controls.Add(this.ucButtonPayment1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnService_1);
            this.panel2.Controls.Add(this.btnService_5);
            this.panel2.Controls.Add(this.btnService_2);
            this.panel2.Controls.Add(this.btnService_4);
            this.panel2.Controls.Add(this.btnService_3);
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = global::BJCBCPOS.Properties.Resources.Flight_Ticket;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Gray;
            this.button1.Location = new System.Drawing.Point(320, 318);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.button1.Size = new System.Drawing.Size(265, 200);
            this.button1.TabIndex = 7;
            this.button1.Text = "ตั๋วเครื่องบิน";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
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
            // btnService_1
            // 
            this.btnService_1.BackColor = System.Drawing.Color.White;
            this.btnService_1.BackgroundImage = global::BJCBCPOS.Properties.Resources.Group_668;
            this.btnService_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnService_1.FlatAppearance.BorderSize = 0;
            this.btnService_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService_1.ForeColor = System.Drawing.Color.Gray;
            this.btnService_1.Location = new System.Drawing.Point(28, 318);
            this.btnService_1.Name = "btnService_1";
            this.btnService_1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnService_1.Size = new System.Drawing.Size(265, 200);
            this.btnService_1.TabIndex = 1;
            this.btnService_1.Text = "Credit Sale";
            this.btnService_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnService_1.UseVisualStyleBackColor = false;
            this.btnService_1.Visible = false;
            this.btnService_1.Click += new System.EventHandler(this.btnService_1_Click);
            // 
            // btnService_5
            // 
            this.btnService_5.BackColor = System.Drawing.Color.White;
            this.btnService_5.BackgroundImage = global::BJCBCPOS.Properties.Resources.Receive_Supplies;
            this.btnService_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnService_5.FlatAppearance.BorderSize = 0;
            this.btnService_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService_5.ForeColor = System.Drawing.Color.Gray;
            this.btnService_5.Location = new System.Drawing.Point(611, 318);
            this.btnService_5.Name = "btnService_5";
            this.btnService_5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnService_5.Size = new System.Drawing.Size(265, 200);
            this.btnService_5.TabIndex = 5;
            this.btnService_5.Text = "พัสดุ";
            this.btnService_5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnService_5.UseVisualStyleBackColor = false;
            this.btnService_5.Visible = false;
            this.btnService_5.Click += new System.EventHandler(this.btnService_5_Click);
            // 
            // btnService_2
            // 
            this.btnService_2.BackColor = System.Drawing.Color.White;
            this.btnService_2.BackgroundImage = global::BJCBCPOS.Properties.Resources.TopUp_Service;
            this.btnService_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnService_2.FlatAppearance.BorderSize = 0;
            this.btnService_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService_2.ForeColor = System.Drawing.Color.Gray;
            this.btnService_2.Location = new System.Drawing.Point(611, 318);
            this.btnService_2.Name = "btnService_2";
            this.btnService_2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnService_2.Size = new System.Drawing.Size(265, 200);
            this.btnService_2.TabIndex = 2;
            this.btnService_2.Text = "Top Up Service";
            this.btnService_2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnService_2.UseVisualStyleBackColor = false;
            this.btnService_2.Visible = false;
            // 
            // btnService_4
            // 
            this.btnService_4.BackColor = System.Drawing.Color.White;
            this.btnService_4.BackgroundImage = global::BJCBCPOS.Properties.Resources.Deposit_Money;
            this.btnService_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnService_4.FlatAppearance.BorderSize = 0;
            this.btnService_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService_4.ForeColor = System.Drawing.Color.Gray;
            this.btnService_4.Location = new System.Drawing.Point(611, 318);
            this.btnService_4.Name = "btnService_4";
            this.btnService_4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnService_4.Size = new System.Drawing.Size(265, 200);
            this.btnService_4.TabIndex = 4;
            this.btnService_4.Text = "ตู้เงินฝาก";
            this.btnService_4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnService_4.UseVisualStyleBackColor = false;
            this.btnService_4.Visible = false;
            // 
            // btnService_3
            // 
            this.btnService_3.BackColor = System.Drawing.Color.White;
            this.btnService_3.BackgroundImage = global::BJCBCPOS.Properties.Resources.SimCard;
            this.btnService_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnService_3.FlatAppearance.BorderSize = 0;
            this.btnService_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService_3.ForeColor = System.Drawing.Color.Gray;
            this.btnService_3.Location = new System.Drawing.Point(611, 318);
            this.btnService_3.Name = "btnService_3";
            this.btnService_3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnService_3.Size = new System.Drawing.Size(265, 200);
            this.btnService_3.TabIndex = 3;
            this.btnService_3.Text = "Sim Card";
            this.btnService_3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnService_3.UseVisualStyleBackColor = false;
            this.btnService_3.Visible = false;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "สินค้าและบริการอื่นๆ";
            this.ucHeader1.currentMenuTitle2 = "";
            this.ucHeader1.currentMenuTitle3 = "";
            this.ucHeader1.Location = new System.Drawing.Point(0, 3);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = true;
            this.ucHeader1.showCurrentMenuText = true;
            this.ucHeader1.showHamberGetItm = false;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = true;
            this.ucHeader1.showLockScreen = true;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = true;
            this.ucHeader1.showMember = true;
            this.ucHeader1.showMember_ButtonBack = false;
            this.ucHeader1.showScanner = true;
            this.ucHeader1.Size = new System.Drawing.Size(1038, 43);
            this.ucHeader1.TabIndex = 10;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 689);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1037, 40);
            this.ucFooter1.TabIndex = 11;
            // 
            // frmProductAndService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1037, 729);
            this.Controls.Add(this.ucFooter1);
            this.Controls.Add(this.ucHeader1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProductAndService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmProductAndService";
            this.Load += new System.EventHandler(this.frmProductAndService_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnService_1;
        private System.Windows.Forms.Button btnService_2;
        private System.Windows.Forms.Button btnService_3;
        private System.Windows.Forms.Button btnService_4;
        private System.Windows.Forms.Button btnService_5;
        private System.Windows.Forms.Label lbProductAndServices;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private UCButtonPayment ucButtonPayment4;
        private UCButtonPayment ucButtonPayment3;
        private UCButtonPayment ucButtonPayment1;
        private UCHeader ucHeader1;
        private UCFooter ucFooter1;
    }
}