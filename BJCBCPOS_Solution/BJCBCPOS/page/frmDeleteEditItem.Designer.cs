namespace BJCBCPOS
{
    partial class frmDeleteEditItem
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
            this.pn_item_sell = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbHeaderDelete = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbListItem = new System.Windows.Forms.Label();
            this.lbQty1 = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lbTotalPrice = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbHeaderEdit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_item_sell
            // 
            this.pn_item_sell.AutoScroll = true;
            this.pn_item_sell.BackColor = System.Drawing.Color.White;
            this.pn_item_sell.Location = new System.Drawing.Point(0, 90);
            this.pn_item_sell.Name = "pn_item_sell";
            this.pn_item_sell.Size = new System.Drawing.Size(525, 300);
            this.pn_item_sell.TabIndex = 72;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.button_ok;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(395, 396);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(118, 46);
            this.btnOk.TabIndex = 73;
            this.btnOk.Text = "ตกลง";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lbHeaderDelete
            // 
            this.lbHeaderDelete.AutoSize = true;
            this.lbHeaderDelete.BackColor = System.Drawing.Color.Transparent;
            this.lbHeaderDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeaderDelete.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbHeaderDelete.Location = new System.Drawing.Point(11, 18);
            this.lbHeaderDelete.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeaderDelete.Name = "lbHeaderDelete";
            this.lbHeaderDelete.Size = new System.Drawing.Size(345, 33);
            this.lbHeaderDelete.TabIndex = 74;
            this.lbHeaderDelete.Text = "เลือกรายการสินค้าที่ต้องการลบ";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BJCBCPOS.Properties.Resources.icon_textbox_delete;
            this.pictureBox2.Location = new System.Drawing.Point(480, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 88;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lbListItem
            // 
            this.lbListItem.BackColor = System.Drawing.Color.Transparent;
            this.lbListItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbListItem.ForeColor = System.Drawing.Color.Gray;
            this.lbListItem.Location = new System.Drawing.Point(2, 67);
            this.lbListItem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbListItem.Name = "lbListItem";
            this.lbListItem.Size = new System.Drawing.Size(161, 20);
            this.lbListItem.TabIndex = 89;
            this.lbListItem.Text = "รายการสินค้า";
            this.lbListItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbQty1
            // 
            this.lbQty1.BackColor = System.Drawing.Color.Transparent;
            this.lbQty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQty1.ForeColor = System.Drawing.Color.Gray;
            this.lbQty1.Location = new System.Drawing.Point(179, 67);
            this.lbQty1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbQty1.Name = "lbQty1";
            this.lbQty1.Size = new System.Drawing.Size(100, 20);
            this.lbQty1.TabIndex = 90;
            this.lbQty1.Text = "จำนวน";
            this.lbQty1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPrice
            // 
            this.lbPrice.BackColor = System.Drawing.Color.Transparent;
            this.lbPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrice.ForeColor = System.Drawing.Color.Gray;
            this.lbPrice.Location = new System.Drawing.Point(280, 67);
            this.lbPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(129, 20);
            this.lbPrice.TabIndex = 91;
            this.lbPrice.Text = "ราคา/หน่วย";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTotalPrice
            // 
            this.lbTotalPrice.BackColor = System.Drawing.Color.Transparent;
            this.lbTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPrice.ForeColor = System.Drawing.Color.Gray;
            this.lbTotalPrice.Location = new System.Drawing.Point(395, 67);
            this.lbTotalPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTotalPrice.Name = "lbTotalPrice";
            this.lbTotalPrice.Size = new System.Drawing.Size(116, 20);
            this.lbTotalPrice.TabIndex = 92;
            this.lbTotalPrice.Text = "จำนวนเงิน";
            this.lbTotalPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbHeaderEdit);
            this.panel1.Controls.Add(this.lbHeaderDelete);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.lbListItem);
            this.panel1.Controls.Add(this.lbQty1);
            this.panel1.Controls.Add(this.lbPrice);
            this.panel1.Controls.Add(this.lbTotalPrice);
            this.panel1.Controls.Add(this.pn_item_sell);
            this.panel1.Location = new System.Drawing.Point(237, 172);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 450);
            this.panel1.TabIndex = 93;
            // 
            // lbHeaderEdit
            // 
            this.lbHeaderEdit.AutoSize = true;
            this.lbHeaderEdit.BackColor = System.Drawing.Color.Transparent;
            this.lbHeaderEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeaderEdit.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbHeaderEdit.Location = new System.Drawing.Point(11, 18);
            this.lbHeaderEdit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeaderEdit.Name = "lbHeaderEdit";
            this.lbHeaderEdit.Size = new System.Drawing.Size(416, 33);
            this.lbHeaderEdit.TabIndex = 93;
            this.lbHeaderEdit.Text = "เลือกรายการสินค้าที่ต้องการปรับราคา";
            // 
            // frmDeleteEditItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDeleteEditItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDeleteItem";
            this.Load += new System.EventHandler(this.frmDeleteItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn_item_sell;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbHeaderDelete;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbListItem;
        private System.Windows.Forms.Label lbQty1;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label lbTotalPrice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbHeaderEdit;
    }
}