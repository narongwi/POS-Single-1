namespace BJCBCPOS
{
    partial class frmMonitorCustomer
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
            this.panel_exchange = new System.Windows.Forms.Panel();
            this.lbAmtCurrency2 = new System.Windows.Forms.Label();
            this.lbAmtCurrency1 = new System.Windows.Forms.Label();
            this.lbCurrencyRate2 = new System.Windows.Forms.Label();
            this.lbCurrencyRate1 = new System.Windows.Forms.Label();
            this.lbTotalcurrency1 = new System.Windows.Forms.Label();
            this.lbTotalcurrency2 = new System.Windows.Forms.Label();
            this.lbTxtSubTotalCash = new System.Windows.Forms.Label();
            this.lbTxtDiscount = new System.Windows.Forms.Label();
            this.lbDiscount = new System.Windows.Forms.Label();
            this.lbSubTotalCash = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTotalCash = new System.Windows.Forms.Label();
            this.lbTxtTotalCash = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pn_Item = new System.Windows.Forms.Panel();
            this.pn_Header = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbCash = new System.Windows.Forms.Label();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbListItem = new System.Windows.Forms.Label();
            this.pn_Top = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbTxtMember = new System.Windows.Forms.Label();
            this.lbMember = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel_exchange.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pn_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pn_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel_exchange);
            this.panel1.Controls.Add(this.lbTxtSubTotalCash);
            this.panel1.Controls.Add(this.lbTxtDiscount);
            this.panel1.Controls.Add(this.lbDiscount);
            this.panel1.Controls.Add(this.lbSubTotalCash);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pn_Item);
            this.panel1.Controls.Add(this.pn_Header);
            this.panel1.Controls.Add(this.pn_Top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 600);
            this.panel1.TabIndex = 57;
            // 
            // panel_exchange
            // 
            this.panel_exchange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(188)))));
            this.panel_exchange.Controls.Add(this.lbCurrencyRate2);
            this.panel_exchange.Controls.Add(this.lbCurrencyRate1);
            this.panel_exchange.Controls.Add(this.lbAmtCurrency2);
            this.panel_exchange.Controls.Add(this.lbAmtCurrency1);
            this.panel_exchange.Controls.Add(this.lbTotalcurrency1);
            this.panel_exchange.Controls.Add(this.lbTotalcurrency2);
            this.panel_exchange.Location = new System.Drawing.Point(0, 542);
            this.panel_exchange.Name = "panel_exchange";
            this.panel_exchange.Size = new System.Drawing.Size(302, 56);
            this.panel_exchange.TabIndex = 130;
            // 
            // lbAmtCurrency2
            // 
            this.lbAmtCurrency2.BackColor = System.Drawing.Color.Transparent;
            this.lbAmtCurrency2.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmtCurrency2.ForeColor = System.Drawing.Color.Black;
            this.lbAmtCurrency2.Location = new System.Drawing.Point(172, 33);
            this.lbAmtCurrency2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAmtCurrency2.Name = "lbAmtCurrency2";
            this.lbAmtCurrency2.Size = new System.Drawing.Size(126, 19);
            this.lbAmtCurrency2.TabIndex = 129;
            this.lbAmtCurrency2.Text = "-";
            this.lbAmtCurrency2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAmtCurrency1
            // 
            this.lbAmtCurrency1.BackColor = System.Drawing.Color.Transparent;
            this.lbAmtCurrency1.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmtCurrency1.ForeColor = System.Drawing.Color.Black;
            this.lbAmtCurrency1.Location = new System.Drawing.Point(172, 6);
            this.lbAmtCurrency1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAmtCurrency1.Name = "lbAmtCurrency1";
            this.lbAmtCurrency1.Size = new System.Drawing.Size(126, 19);
            this.lbAmtCurrency1.TabIndex = 128;
            this.lbAmtCurrency1.Text = "-";
            this.lbAmtCurrency1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbCurrencyRate2
            // 
            this.lbCurrencyRate2.BackColor = System.Drawing.Color.Transparent;
            this.lbCurrencyRate2.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrencyRate2.ForeColor = System.Drawing.Color.Black;
            this.lbCurrencyRate2.Location = new System.Drawing.Point(83, 33);
            this.lbCurrencyRate2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCurrencyRate2.Name = "lbCurrencyRate2";
            this.lbCurrencyRate2.Size = new System.Drawing.Size(80, 19);
            this.lbCurrencyRate2.TabIndex = 127;
            this.lbCurrencyRate2.Text = "-";
            this.lbCurrencyRate2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurrencyRate1
            // 
            this.lbCurrencyRate1.BackColor = System.Drawing.Color.Transparent;
            this.lbCurrencyRate1.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrencyRate1.ForeColor = System.Drawing.Color.Black;
            this.lbCurrencyRate1.Location = new System.Drawing.Point(83, 7);
            this.lbCurrencyRate1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCurrencyRate1.Name = "lbCurrencyRate1";
            this.lbCurrencyRate1.Size = new System.Drawing.Size(80, 19);
            this.lbCurrencyRate1.TabIndex = 126;
            this.lbCurrencyRate1.Text = "-";
            this.lbCurrencyRate1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotalcurrency1
            // 
            this.lbTotalcurrency1.AutoSize = true;
            this.lbTotalcurrency1.BackColor = System.Drawing.Color.Transparent;
            this.lbTotalcurrency1.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalcurrency1.ForeColor = System.Drawing.Color.Black;
            this.lbTotalcurrency1.Location = new System.Drawing.Point(4, 4);
            this.lbTotalcurrency1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTotalcurrency1.Name = "lbTotalcurrency1";
            this.lbTotalcurrency1.Size = new System.Drawing.Size(66, 22);
            this.lbTotalcurrency1.TabIndex = 124;
            this.lbTotalcurrency1.Text = "ราคารวม";
            // 
            // lbTotalcurrency2
            // 
            this.lbTotalcurrency2.AutoSize = true;
            this.lbTotalcurrency2.BackColor = System.Drawing.Color.Transparent;
            this.lbTotalcurrency2.Font = new System.Drawing.Font("Prompt", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalcurrency2.ForeColor = System.Drawing.Color.Black;
            this.lbTotalcurrency2.Location = new System.Drawing.Point(3, 30);
            this.lbTotalcurrency2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTotalcurrency2.Name = "lbTotalcurrency2";
            this.lbTotalcurrency2.Size = new System.Drawing.Size(66, 22);
            this.lbTotalcurrency2.TabIndex = 125;
            this.lbTotalcurrency2.Text = "ราคารวม";
            // 
            // lbTxtSubTotalCash
            // 
            this.lbTxtSubTotalCash.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtSubTotalCash.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtSubTotalCash.ForeColor = System.Drawing.Color.Black;
            this.lbTxtSubTotalCash.Location = new System.Drawing.Point(139, 427);
            this.lbTxtSubTotalCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtSubTotalCash.Name = "lbTxtSubTotalCash";
            this.lbTxtSubTotalCash.Size = new System.Drawing.Size(158, 24);
            this.lbTxtSubTotalCash.TabIndex = 129;
            this.lbTxtSubTotalCash.Text = "8,888,888.00";
            this.lbTxtSubTotalCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTxtDiscount
            // 
            this.lbTxtDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtDiscount.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtDiscount.ForeColor = System.Drawing.Color.Black;
            this.lbTxtDiscount.Location = new System.Drawing.Point(139, 455);
            this.lbTxtDiscount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtDiscount.Name = "lbTxtDiscount";
            this.lbTxtDiscount.Size = new System.Drawing.Size(158, 24);
            this.lbTxtDiscount.TabIndex = 128;
            this.lbTxtDiscount.Text = "8,888,888.00";
            this.lbTxtDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDiscount
            // 
            this.lbDiscount.AutoSize = true;
            this.lbDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lbDiscount.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDiscount.ForeColor = System.Drawing.Color.Black;
            this.lbDiscount.Location = new System.Drawing.Point(7, 454);
            this.lbDiscount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDiscount.Name = "lbDiscount";
            this.lbDiscount.Size = new System.Drawing.Size(60, 24);
            this.lbDiscount.TabIndex = 127;
            this.lbDiscount.Text = "ส่วนลด";
            // 
            // lbSubTotalCash
            // 
            this.lbSubTotalCash.AutoSize = true;
            this.lbSubTotalCash.BackColor = System.Drawing.Color.Transparent;
            this.lbSubTotalCash.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubTotalCash.ForeColor = System.Drawing.Color.Black;
            this.lbSubTotalCash.Location = new System.Drawing.Point(6, 427);
            this.lbSubTotalCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSubTotalCash.Name = "lbSubTotalCash";
            this.lbSubTotalCash.Size = new System.Drawing.Size(69, 24);
            this.lbSubTotalCash.TabIndex = 126;
            this.lbSubTotalCash.Text = "ราคารวม";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(188)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.lbTotalCash);
            this.panel2.Controls.Add(this.lbTxtTotalCash);
            this.panel2.Location = new System.Drawing.Point(0, 479);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 64);
            this.panel2.TabIndex = 125;
            // 
            // lbTotalCash
            // 
            this.lbTotalCash.AutoSize = true;
            this.lbTotalCash.BackColor = System.Drawing.Color.Transparent;
            this.lbTotalCash.Font = new System.Drawing.Font("Prompt", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalCash.ForeColor = System.Drawing.Color.Black;
            this.lbTotalCash.Location = new System.Drawing.Point(2, 3);
            this.lbTotalCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTotalCash.Name = "lbTotalCash";
            this.lbTotalCash.Size = new System.Drawing.Size(175, 26);
            this.lbTotalCash.TabIndex = 56;
            this.lbTotalCash.Text = "ราคาที่ต้องชำระทั้งหมด";
            // 
            // lbTxtTotalCash
            // 
            this.lbTxtTotalCash.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtTotalCash.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtTotalCash.ForeColor = System.Drawing.Color.Black;
            this.lbTxtTotalCash.Location = new System.Drawing.Point(11, 30);
            this.lbTxtTotalCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtTotalCash.Name = "lbTxtTotalCash";
            this.lbTxtTotalCash.Size = new System.Drawing.Size(287, 33);
            this.lbTxtTotalCash.TabIndex = 57;
            this.lbTxtTotalCash.Text = "888,888,888.00";
            this.lbTxtTotalCash.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.Location = new System.Drawing.Point(4, 423);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(292, 2);
            this.pictureBox1.TabIndex = 61;
            this.pictureBox1.TabStop = false;
            // 
            // pn_Item
            // 
            this.pn_Item.AutoScroll = true;
            this.pn_Item.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_Item.Location = new System.Drawing.Point(0, 83);
            this.pn_Item.Name = "pn_Item";
            this.pn_Item.Size = new System.Drawing.Size(302, 338);
            this.pn_Item.TabIndex = 3;
            // 
            // pn_Header
            // 
            this.pn_Header.Controls.Add(this.pictureBox3);
            this.pn_Header.Controls.Add(this.lbCash);
            this.pn_Header.Controls.Add(this.lbQty);
            this.pn_Header.Controls.Add(this.lbListItem);
            this.pn_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_Header.Location = new System.Drawing.Point(0, 39);
            this.pn_Header.Name = "pn_Header";
            this.pn_Header.Size = new System.Drawing.Size(302, 44);
            this.pn_Header.TabIndex = 2;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Gray;
            this.pictureBox3.Location = new System.Drawing.Point(5, 42);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(292, 2);
            this.pictureBox3.TabIndex = 62;
            this.pictureBox3.TabStop = false;
            // 
            // lbCash
            // 
            this.lbCash.AutoSize = true;
            this.lbCash.BackColor = System.Drawing.Color.Transparent;
            this.lbCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCash.ForeColor = System.Drawing.Color.Black;
            this.lbCash.Location = new System.Drawing.Point(227, 18);
            this.lbCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCash.Name = "lbCash";
            this.lbCash.Size = new System.Drawing.Size(65, 18);
            this.lbCash.TabIndex = 54;
            this.lbCash.Text = "จำนวนเงิน";
            // 
            // lbQty
            // 
            this.lbQty.AutoSize = true;
            this.lbQty.BackColor = System.Drawing.Color.Transparent;
            this.lbQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQty.ForeColor = System.Drawing.Color.Black;
            this.lbQty.Location = new System.Drawing.Point(143, 17);
            this.lbQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(46, 18);
            this.lbQty.TabIndex = 55;
            this.lbQty.Text = "จำนวน";
            // 
            // lbListItem
            // 
            this.lbListItem.AutoSize = true;
            this.lbListItem.BackColor = System.Drawing.Color.Transparent;
            this.lbListItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbListItem.ForeColor = System.Drawing.Color.Black;
            this.lbListItem.Location = new System.Drawing.Point(5, 17);
            this.lbListItem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbListItem.Name = "lbListItem";
            this.lbListItem.Size = new System.Drawing.Size(81, 18);
            this.lbListItem.TabIndex = 53;
            this.lbListItem.Text = "รายการสินค้า";
            // 
            // pn_Top
            // 
            this.pn_Top.Controls.Add(this.pictureBox2);
            this.pn_Top.Controls.Add(this.lbTxtMember);
            this.pn_Top.Controls.Add(this.lbMember);
            this.pn_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_Top.Location = new System.Drawing.Point(0, 0);
            this.pn_Top.Name = "pn_Top";
            this.pn_Top.Size = new System.Drawing.Size(302, 39);
            this.pn_Top.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Gray;
            this.pictureBox2.Location = new System.Drawing.Point(5, 37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(292, 2);
            this.pictureBox2.TabIndex = 62;
            this.pictureBox2.TabStop = false;
            // 
            // lbTxtMember
            // 
            this.lbTxtMember.BackColor = System.Drawing.Color.Transparent;
            this.lbTxtMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtMember.ForeColor = System.Drawing.Color.Black;
            this.lbTxtMember.Location = new System.Drawing.Point(89, 6);
            this.lbTxtMember.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtMember.Name = "lbTxtMember";
            this.lbTxtMember.Size = new System.Drawing.Size(206, 24);
            this.lbTxtMember.TabIndex = 55;
            this.lbTxtMember.Text = "คุณสมฤดี มีเงิน";
            // 
            // lbMember
            // 
            this.lbMember.BackColor = System.Drawing.Color.Transparent;
            this.lbMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMember.ForeColor = System.Drawing.Color.Green;
            this.lbMember.Location = new System.Drawing.Point(9, 7);
            this.lbMember.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMember.Name = "lbMember";
            this.lbMember.Size = new System.Drawing.Size(76, 20);
            this.lbMember.TabIndex = 54;
            this.lbMember.Text = "สมาชิก";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmMonitorCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 600);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMonitorCustomer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmMonitorCustomer";
            this.Load += new System.EventHandler(this.frmMonitorCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_exchange.ResumeLayout(false);
            this.panel_exchange.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pn_Header.ResumeLayout(false);
            this.pn_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pn_Top.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel pn_Item;
        private System.Windows.Forms.Panel pn_Header;
        private System.Windows.Forms.Label lbCash;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbListItem;
        private System.Windows.Forms.Panel pn_Top;
        public System.Windows.Forms.Label lbTxtMember;
        private System.Windows.Forms.Label lbMember;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTotalCash;
        public System.Windows.Forms.Label lbTxtTotalCash;
        public System.Windows.Forms.Panel panel_exchange;
        public System.Windows.Forms.Label lbAmtCurrency2;
        public System.Windows.Forms.Label lbAmtCurrency1;
        public System.Windows.Forms.Label lbCurrencyRate2;
        public System.Windows.Forms.Label lbCurrencyRate1;
        private System.Windows.Forms.Label lbTotalcurrency1;
        private System.Windows.Forms.Label lbTotalcurrency2;
        public System.Windows.Forms.Label lbTxtSubTotalCash;
        public System.Windows.Forms.Label lbTxtDiscount;
        private System.Windows.Forms.Label lbDiscount;
        private System.Windows.Forms.Label lbSubTotalCash;
    }
}