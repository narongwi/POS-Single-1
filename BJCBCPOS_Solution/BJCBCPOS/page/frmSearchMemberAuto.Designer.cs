namespace BJCBCPOS
{
    partial class frmSearchMemberAuto
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
            this.pn_MemberList = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbSearch = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbCardNo = new System.Windows.Forms.Label();
            this.lbCusNo = new System.Windows.Forms.Label();
            this.lbCardID = new System.Windows.Forms.Label();
            this.lbTel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_MemberList
            // 
            this.pn_MemberList.AutoScroll = true;
            this.pn_MemberList.BackColor = System.Drawing.Color.White;
            this.pn_MemberList.Location = new System.Drawing.Point(0, 90);
            this.pn_MemberList.Name = "pn_MemberList";
            this.pn_MemberList.Size = new System.Drawing.Size(580, 300);
            this.pn_MemberList.TabIndex = 72;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.button_ok;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(454, 396);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(118, 46);
            this.btnOk.TabIndex = 73;
            this.btnOk.Text = "ตกลง";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.BackColor = System.Drawing.Color.Transparent;
            this.lbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSearch.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbSearch.Location = new System.Drawing.Point(11, 15);
            this.lbSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(156, 33);
            this.lbSearch.TabIndex = 74;
            this.lbSearch.Text = "ค้นหาสมาชิก";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BJCBCPOS.Properties.Resources.icon_textbox_delete;
            this.pictureBox2.Location = new System.Drawing.Point(525, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 88;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lbCardNo
            // 
            this.lbCardNo.BackColor = System.Drawing.Color.Transparent;
            this.lbCardNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardNo.ForeColor = System.Drawing.Color.Gray;
            this.lbCardNo.Location = new System.Drawing.Point(5, 66);
            this.lbCardNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCardNo.Name = "lbCardNo";
            this.lbCardNo.Size = new System.Drawing.Size(162, 20);
            this.lbCardNo.TabIndex = 89;
            this.lbCardNo.Text = "หมายเลขบัตร";
            // 
            // lbCusNo
            // 
            this.lbCusNo.BackColor = System.Drawing.Color.Transparent;
            this.lbCusNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCusNo.ForeColor = System.Drawing.Color.Gray;
            this.lbCusNo.Location = new System.Drawing.Point(202, 66);
            this.lbCusNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCusNo.Name = "lbCusNo";
            this.lbCusNo.Size = new System.Drawing.Size(101, 20);
            this.lbCusNo.TabIndex = 90;
            this.lbCusNo.Text = "รหัสสมาชิก";
            this.lbCusNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCardID
            // 
            this.lbCardID.BackColor = System.Drawing.Color.Transparent;
            this.lbCardID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardID.ForeColor = System.Drawing.Color.Gray;
            this.lbCardID.Location = new System.Drawing.Point(311, 66);
            this.lbCardID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCardID.Name = "lbCardID";
            this.lbCardID.Size = new System.Drawing.Size(157, 20);
            this.lbCardID.TabIndex = 91;
            this.lbCardID.Text = "เลขที่บัตรประชาชน";
            this.lbCardID.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbTel
            // 
            this.lbTel.BackColor = System.Drawing.Color.Transparent;
            this.lbTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTel.ForeColor = System.Drawing.Color.Gray;
            this.lbTel.Location = new System.Drawing.Point(476, 66);
            this.lbTel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTel.Name = "lbTel";
            this.lbTel.Size = new System.Drawing.Size(95, 20);
            this.lbTel.TabIndex = 92;
            this.lbTel.Text = "เบอร์โทร";
            this.lbTel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lbSearch);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.lbCardNo);
            this.panel1.Controls.Add(this.lbCusNo);
            this.panel1.Controls.Add(this.lbCardID);
            this.panel1.Controls.Add(this.lbTel);
            this.panel1.Controls.Add(this.pn_MemberList);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 450);
            this.panel1.TabIndex = 93;
            // 
            // frmSearchMemberAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 450);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSearchMemberAuto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDeleteItem";
            this.Load += new System.EventHandler(this.frmDeleteItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn_MemberList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbSearch;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbCardNo;
        private System.Windows.Forms.Label lbCusNo;
        private System.Windows.Forms.Label lbCardID;
        private System.Windows.Forms.Label lbTel;
        private System.Windows.Forms.Panel panel1;
    }
}