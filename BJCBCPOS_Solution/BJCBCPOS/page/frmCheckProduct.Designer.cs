namespace BJCBCPOS
{
    partial class frmCheckProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckProduct));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_list_product = new System.Windows.Forms.Panel();
            this.ucFooterTran1 = new BJCBCPOS.UCFooterTran();
            this.lbPromo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_list_suggest = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbScanProduct = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.ucTBScanProduct = new BJCBCPOS.UCTextBoxWithIcon();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucKeypad = new BJCBCPOS.UCKeypad();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel_list_product);
            this.panel1.Controls.Add(this.ucFooterTran1);
            this.panel1.Controls.Add(this.lbPromo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 725);
            this.panel1.TabIndex = 103;
            // 
            // panel_list_product
            // 
            this.panel_list_product.AutoScroll = true;
            this.panel_list_product.Location = new System.Drawing.Point(20, 11);
            this.panel_list_product.Name = "panel_list_product";
            this.panel_list_product.Size = new System.Drawing.Size(650, 435);
            this.panel_list_product.TabIndex = 183;
            // 
            // ucFooterTran1
            // 
            this.ucFooterTran1.BackColor = System.Drawing.Color.White;
            this.ucFooterTran1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooterTran1.fullContent = null;
            this.ucFooterTran1.functionId = null;
            this.ucFooterTran1.Location = new System.Drawing.Point(0, 685);
            this.ucFooterTran1.mainContent = null;
            this.ucFooterTran1.Name = "ucFooterTran1";
            this.ucFooterTran1.Size = new System.Drawing.Size(688, 40);
            this.ucFooterTran1.TabIndex = 182;
            // 
            // lbPromo
            // 
            this.lbPromo.BackColor = System.Drawing.Color.Transparent;
            this.lbPromo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPromo.ForeColor = System.Drawing.Color.Gray;
            this.lbPromo.Location = new System.Drawing.Point(11, 449);
            this.lbPromo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPromo.Name = "lbPromo";
            this.lbPromo.Size = new System.Drawing.Size(296, 33);
            this.lbPromo.TabIndex = 181;
            this.lbPromo.Text = "โปรโมชั่น";
            this.lbPromo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_list_suggest);
            this.panel2.Location = new System.Drawing.Point(15, 489);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(656, 181);
            this.panel2.TabIndex = 180;
            // 
            // panel_list_suggest
            // 
            this.panel_list_suggest.AutoScroll = true;
            this.panel_list_suggest.Location = new System.Drawing.Point(3, 3);
            this.panel_list_suggest.Name = "panel_list_suggest";
            this.panel_list_suggest.Size = new System.Drawing.Size(650, 175);
            this.panel_list_suggest.TabIndex = 179;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BJCBCPOS.Properties.Resources.icon_textbox_delete;
            this.pictureBox2.Location = new System.Drawing.Point(287, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 167;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lbScanProduct
            // 
            this.lbScanProduct.BackColor = System.Drawing.Color.Transparent;
            this.lbScanProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScanProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(186)))), ((int)(((byte)(109)))));
            this.lbScanProduct.Location = new System.Drawing.Point(48, 25);
            this.lbScanProduct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbScanProduct.Name = "lbScanProduct";
            this.lbScanProduct.Size = new System.Drawing.Size(236, 53);
            this.lbScanProduct.TabIndex = 110;
            this.lbScanProduct.Text = "สแกนสินค้า";
            this.lbScanProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.pictureBox9);
            this.panel3.Controls.Add(this.ucTBScanProduct);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.lbScanProduct);
            this.panel3.Location = new System.Drawing.Point(690, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(334, 427);
            this.panel3.TabIndex = 175;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox9.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox9.ErrorImage")));
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(125, 91);
            this.pictureBox9.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(84, 84);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox9.TabIndex = 170;
            this.pictureBox9.TabStop = false;
            // 
            // ucTBScanProduct
            // 
            this.ucTBScanProduct.BackColor = System.Drawing.Color.White;
            this.ucTBScanProduct.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTBScanProduct.BackgroundImage")));
            this.ucTBScanProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTBScanProduct.EnabledUC = true;
            this.ucTBScanProduct.IsAmount = false;
            this.ucTBScanProduct.IsLarge = false;
            this.ucTBScanProduct.IsNumber = true;
            this.ucTBScanProduct.IsSetFormat = false;
            this.ucTBScanProduct.IsTextChange = true;
            this.ucTBScanProduct.Location = new System.Drawing.Point(16, 197);
            this.ucTBScanProduct.MaxLength = 32767;
            this.ucTBScanProduct.Name = "ucTBScanProduct";
            this.ucTBScanProduct.PasswordChar = false;
            this.ucTBScanProduct.placeHolder = "กรอกรหัสสินค้า";
            this.ucTBScanProduct.Readonly = false;
            this.ucTBScanProduct.ShortcutsEnabled = true;
            this.ucTBScanProduct.Size = new System.Drawing.Size(306, 42);
            this.ucTBScanProduct.TabIndex = 169;
            this.ucTBScanProduct.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTBScanProduct.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTBScanProduct.TextBoxKeydown += new System.EventHandler(this.ucTBScanProduct_TextBoxKeydown);
            this.ucTBScanProduct.EnterFromButton += new System.EventHandler(this.ucTBScanProduct_EnterFromButton);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Location = new System.Drawing.Point(688, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 725);
            this.pictureBox1.TabIndex = 170;
            this.pictureBox1.TabStop = false;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "เครื่องมือ";
            this.ucHeader1.currentMenuTitle2 = "สแกนสินค้า";
            this.ucHeader1.currentMenuTitle3 = "";
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = true;
            this.ucHeader1.showCurrentMenuText = true;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = true;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = true;
            this.ucHeader1.showMember = true;
            this.ucHeader1.showScanner = true;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 174;
            this.ucHeader1.MainMenuClick += new System.EventHandler(this.ucHeader1_MainMenuClick);
            this.ucHeader1.CalculatorClick += new System.EventHandler(this.ucHeader1_CalculatorClick);
            this.ucHeader1.ScannerClick += new System.EventHandler(this.ucHeader1_ScannerClick);
            // 
            // ucKeypad
            // 
            this.ucKeypad.Location = new System.Drawing.Point(688, 471);
            this.ucKeypad.Name = "ucKeypad";
            this.ucKeypad.Size = new System.Drawing.Size(336, 296);
            this.ucKeypad.TabIndex = 173;
            this.ucKeypad.ucTBS = null;
            this.ucKeypad.ucTBWI = null;
            // 
            // frmCheckProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ucHeader1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucKeypad);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCheckProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmCheckProduct_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbScanProduct;
        private System.Windows.Forms.PictureBox pictureBox2;
        private UCKeypad ucKeypad;
        private System.Windows.Forms.Panel panel_list_suggest;
        private System.Windows.Forms.Panel panel2;
        private UCHeader ucHeader1;
        private System.Windows.Forms.Panel panel3;
        private UCTextBoxWithIcon ucTBScanProduct;
        private System.Windows.Forms.Label lbPromo;
        private UCFooterTran ucFooterTran1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_list_product;
        private System.Windows.Forms.PictureBox pictureBox9;
    }
}