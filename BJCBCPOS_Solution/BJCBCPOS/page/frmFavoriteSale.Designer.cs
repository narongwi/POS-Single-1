namespace BJCBCPOS
{
    partial class frmFavoriteSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFavoriteSale));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lbHeader = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btn_Previousx = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Nextx = new System.Windows.Forms.Label();
            this.lb_PageNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_PageTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbStoreId = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon1 = new BJCBCPOS.UCTextBoxWithIcon();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.pn_Footer_Product_Search = new System.Windows.Forms.Panel();
            this.btn_Previous_Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Next_Search = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_PageNo_Search = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lb_PageTotal_Search = new System.Windows.Forms.Label();
            this.pn_Footer_Product = new System.Windows.Forms.Panel();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            this.panelHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pn_Footer_Product_Search.SuspendLayout();
            this.pn_Footer_Product.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.panelHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelHeader.Controls.Add(this.lbHeader);
            this.panelHeader.Controls.Add(this.btnBack);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(860, 64);
            this.panelHeader.TabIndex = 3;
            // 
            // lbHeader
            // 
            this.lbHeader.BackColor = System.Drawing.Color.Transparent;
            this.lbHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(70, 3);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(723, 58);
            this.lbHeader.TabIndex = 2;
            this.lbHeader.Text = "Favorite Sale";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.btnBack.Location = new System.Drawing.Point(5, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(53, 56);
            this.btnBack.TabIndex = 1;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Phetsarath OT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(20, 120);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(823, 568);
            this.tabControl1.TabIndex = 16;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // btn_Previousx
            // 
            this.btn_Previousx.AutoSize = true;
            this.btn_Previousx.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btn_Previousx.ForeColor = System.Drawing.Color.Blue;
            this.btn_Previousx.Location = new System.Drawing.Point(64, 16);
            this.btn_Previousx.Name = "btn_Previousx";
            this.btn_Previousx.Size = new System.Drawing.Size(83, 24);
            this.btn_Previousx.TabIndex = 17;
            this.btn_Previousx.Text = "Previous";
            this.btn_Previousx.Visible = false;
            this.btn_Previousx.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(339, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "Page No.";
            // 
            // btn_Nextx
            // 
            this.btn_Nextx.AutoSize = true;
            this.btn_Nextx.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btn_Nextx.ForeColor = System.Drawing.Color.Blue;
            this.btn_Nextx.Location = new System.Drawing.Point(740, 16);
            this.btn_Nextx.Name = "btn_Nextx";
            this.btn_Nextx.Size = new System.Drawing.Size(49, 24);
            this.btn_Nextx.TabIndex = 20;
            this.btn_Nextx.Text = "Next";
            this.btn_Nextx.Visible = false;
            this.btn_Nextx.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // lb_PageNo
            // 
            this.lb_PageNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_PageNo.Location = new System.Drawing.Point(430, 17);
            this.lb_PageNo.Name = "lb_PageNo";
            this.lb_PageNo.Size = new System.Drawing.Size(40, 24);
            this.lb_PageNo.TabIndex = 21;
            this.lb_PageNo.Text = "0";
            this.lb_PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(470, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "/";
            // 
            // lb_PageTotal
            // 
            this.lb_PageTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_PageTotal.Location = new System.Drawing.Point(481, 17);
            this.lb_PageTotal.Name = "lb_PageTotal";
            this.lb_PageTotal.Size = new System.Drawing.Size(40, 24);
            this.lb_PageTotal.TabIndex = 23;
            this.lb_PageTotal.Text = "1";
            this.lb_PageTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbStoreId);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon1);
            this.panel1.Controls.Add(this.panelHeader);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.tabControl2);
            this.panel1.Controls.Add(this.pn_Footer_Product_Search);
            this.panel1.Controls.Add(this.pn_Footer_Product);
            this.panel1.Location = new System.Drawing.Point(80, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 757);
            this.panel1.TabIndex = 16;
            // 
            // lbStoreId
            // 
            this.lbStoreId.AutoSize = true;
            this.lbStoreId.BackColor = System.Drawing.Color.White;
            this.lbStoreId.CausesValidation = false;
            this.lbStoreId.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStoreId.ForeColor = System.Drawing.Color.Black;
            this.lbStoreId.Location = new System.Drawing.Point(437, 76);
            this.lbStoreId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStoreId.Name = "lbStoreId";
            this.lbStoreId.Size = new System.Drawing.Size(96, 32);
            this.lbStoreId.TabIndex = 27;
            this.lbStoreId.Text = "Search :";
            // 
            // ucTextBoxWithIcon1
            // 
            this.ucTextBoxWithIcon1.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTextBoxWithIcon1.BackgroundImage")));
            this.ucTextBoxWithIcon1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon1.EnabledUC = true;
            this.ucTextBoxWithIcon1.IsAmount = false;
            this.ucTextBoxWithIcon1.IsLarge = false;
            this.ucTextBoxWithIcon1.IsNumber = false;
            this.ucTextBoxWithIcon1.IsSetFormat = false;

            this.ucTextBoxWithIcon1.Location = new System.Drawing.Point(547, 71);
            this.ucTextBoxWithIcon1.MaxLength = 32767;
            this.ucTextBoxWithIcon1.Name = "ucTextBoxWithIcon1";
            this.ucTextBoxWithIcon1.PasswordChar = false;
            this.ucTextBoxWithIcon1.placeHolder = "Input key search";
            this.ucTextBoxWithIcon1.Readonly = false;
            this.ucTextBoxWithIcon1.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon1.Size = new System.Drawing.Size(295, 42);
            this.ucTextBoxWithIcon1.TabIndex = 26;
            this.ucTextBoxWithIcon1.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon1.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTextBoxWithIcon1.TextBoxKeydown += new System.EventHandler(this.ucTextBoxWithIcon1_TextBoxKeydown);
            this.ucTextBoxWithIcon1.TextBoxLeave += new System.EventHandler(this.ucTextBoxWithIcon1_TextBoxLeave);
            this.ucTextBoxWithIcon1.TextBoxTextChange += new System.EventHandler(this.ucTextBoxWithIcon1_TextBoxTextChange);
            this.ucTextBoxWithIcon1.Enter += new System.EventHandler(this.ucTextBoxWithIcon1_Enter);
            // 
            // tabControl2
            // 
            this.tabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl2.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl2.Location = new System.Drawing.Point(20, 120);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(823, 568);
            this.tabControl2.TabIndex = 28;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);
            // 
            // pn_Footer_Product_Search
            // 
            this.pn_Footer_Product_Search.Controls.Add(this.btn_Previous_Search);
            this.pn_Footer_Product_Search.Controls.Add(this.label1);
            this.pn_Footer_Product_Search.Controls.Add(this.label4);
            this.pn_Footer_Product_Search.Controls.Add(this.btn_Next_Search);
            this.pn_Footer_Product_Search.Controls.Add(this.label5);
            this.pn_Footer_Product_Search.Controls.Add(this.lb_PageNo_Search);
            this.pn_Footer_Product_Search.Controls.Add(this.label7);
            this.pn_Footer_Product_Search.Controls.Add(this.lb_PageTotal_Search);
            this.pn_Footer_Product_Search.Location = new System.Drawing.Point(5, 694);
            this.pn_Footer_Product_Search.Name = "pn_Footer_Product_Search";
            this.pn_Footer_Product_Search.Size = new System.Drawing.Size(852, 61);
            this.pn_Footer_Product_Search.TabIndex = 30;
            // 
            // btn_Previous_Search
            // 
            this.btn_Previous_Search.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_previous;
            this.btn_Previous_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Previous_Search.FlatAppearance.BorderSize = 0;
            this.btn_Previous_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Previous_Search.Location = new System.Drawing.Point(6, 4);
            this.btn_Previous_Search.Name = "btn_Previous_Search";
            this.btn_Previous_Search.Size = new System.Drawing.Size(52, 53);
            this.btn_Previous_Search.TabIndex = 24;
            this.btn_Previous_Search.UseVisualStyleBackColor = true;
            this.btn_Previous_Search.Click += new System.EventHandler(this.btn_Previous_Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(64, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Previous";
            this.label1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(339, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 24);
            this.label4.TabIndex = 19;
            this.label4.Text = "Page No.";
            // 
            // btn_Next_Search
            // 
            this.btn_Next_Search.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_next;
            this.btn_Next_Search.FlatAppearance.BorderSize = 0;
            this.btn_Next_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next_Search.Location = new System.Drawing.Point(795, 4);
            this.btn_Next_Search.Name = "btn_Next_Search";
            this.btn_Next_Search.Size = new System.Drawing.Size(52, 53);
            this.btn_Next_Search.TabIndex = 25;
            this.btn_Next_Search.UseVisualStyleBackColor = true;
            this.btn_Next_Search.Click += new System.EventHandler(this.btn_Next_Search_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(740, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 24);
            this.label5.TabIndex = 20;
            this.label5.Text = "Next";
            this.label5.Visible = false;
            // 
            // lb_PageNo_Search
            // 
            this.lb_PageNo_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_PageNo_Search.Location = new System.Drawing.Point(430, 17);
            this.lb_PageNo_Search.Name = "lb_PageNo_Search";
            this.lb_PageNo_Search.Size = new System.Drawing.Size(40, 24);
            this.lb_PageNo_Search.TabIndex = 21;
            this.lb_PageNo_Search.Text = "0";
            this.lb_PageNo_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(470, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 24);
            this.label7.TabIndex = 22;
            this.label7.Text = "/";
            // 
            // lb_PageTotal_Search
            // 
            this.lb_PageTotal_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_PageTotal_Search.Location = new System.Drawing.Point(481, 17);
            this.lb_PageTotal_Search.Name = "lb_PageTotal_Search";
            this.lb_PageTotal_Search.Size = new System.Drawing.Size(40, 24);
            this.lb_PageTotal_Search.TabIndex = 23;
            this.lb_PageTotal_Search.Text = "1";
            this.lb_PageTotal_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pn_Footer_Product
            // 
            this.pn_Footer_Product.Controls.Add(this.btn_Previous);
            this.pn_Footer_Product.Controls.Add(this.btn_Previousx);
            this.pn_Footer_Product.Controls.Add(this.label3);
            this.pn_Footer_Product.Controls.Add(this.btn_Next);
            this.pn_Footer_Product.Controls.Add(this.btn_Nextx);
            this.pn_Footer_Product.Controls.Add(this.lb_PageNo);
            this.pn_Footer_Product.Controls.Add(this.label2);
            this.pn_Footer_Product.Controls.Add(this.lb_PageTotal);
            this.pn_Footer_Product.Location = new System.Drawing.Point(5, 694);
            this.pn_Footer_Product.Name = "pn_Footer_Product";
            this.pn_Footer_Product.Size = new System.Drawing.Size(852, 61);
            this.pn_Footer_Product.TabIndex = 29;
            // 
            // btn_Previous
            // 
            this.btn_Previous.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_previous;
            this.btn_Previous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Previous.FlatAppearance.BorderSize = 0;
            this.btn_Previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Previous.Location = new System.Drawing.Point(6, 4);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(52, 53);
            this.btn_Previous.TabIndex = 24;
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_next;
            this.btn_Next.FlatAppearance.BorderSize = 0;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Location = new System.Drawing.Point(795, 4);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(52, 53);
            this.btn_Next.TabIndex = 25;
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 462);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(1024, 298);
            this.ucKeyboard1.TabIndex = 31;
            this.ucKeyboard1.Visible = false;
            this.ucKeyboard1.HideKeyboardClick += new System.EventHandler(this.ucKeyboard1_HideKeyboardClick);
            // 
            // frmFavoriteSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1024, 760);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucKeyboard1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFavoriteSale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmGoodSales";
            this.Load += new System.EventHandler(this.frmFavoriteSale_Load);
            this.panelHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pn_Footer_Product_Search.ResumeLayout(false);
            this.pn_Footer_Product_Search.PerformLayout();
            this.pn_Footer_Product.ResumeLayout(false);
            this.pn_Footer_Product.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label btn_Previousx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label btn_Nextx;
        private System.Windows.Forms.Label lb_PageNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_PageTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button btn_Next;
        private UCTextBoxWithIcon ucTextBoxWithIcon1;
        private System.Windows.Forms.Label lbStoreId;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.Panel pn_Footer_Product;
        private System.Windows.Forms.Panel pn_Footer_Product_Search;
        private System.Windows.Forms.Button btn_Previous_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Next_Search;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_PageNo_Search;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lb_PageTotal_Search;
        private UCKeyboard ucKeyboard1;

    }
}