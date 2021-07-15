namespace BJCBCPOS
{
    partial class frmOtherPayment
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
            this.lbOtherPayment = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.lb_PageTotal = new System.Windows.Forms.Label();
            this.lb_PageNo = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOther_3 = new System.Windows.Forms.Button();
            this.btnOther_4 = new System.Windows.Forms.Button();
            this.btnOther_2 = new System.Windows.Forms.Button();
            this.btnOther_5 = new System.Windows.Forms.Button();
            this.btnOther_1 = new System.Windows.Forms.Button();
            this.btnOther_6 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbOtherPayment);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 75);
            this.panel1.TabIndex = 0;
            // 
            // lbOtherPayment
            // 
            this.lbOtherPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.lbOtherPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOtherPayment.ForeColor = System.Drawing.Color.White;
            this.lbOtherPayment.Location = new System.Drawing.Point(67, 10);
            this.lbOtherPayment.Name = "lbOtherPayment";
            this.lbOtherPayment.Size = new System.Drawing.Size(746, 56);
            this.lbOtherPayment.TabIndex = 2;
            this.lbOtherPayment.Text = "ตราสารอื่นๆ";
            this.lbOtherPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.button1.Location = new System.Drawing.Point(8, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 56);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.lb_PageTotal);
            this.panel2.Controls.Add(this.lb_PageNo);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.btn_Next);
            this.panel2.Controls.Add(this.btn_Previous);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(75, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(867, 593);
            this.panel2.TabIndex = 7;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(485, 539);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(20, 29);
            this.label27.TabIndex = 35;
            this.label27.Text = "/";
            // 
            // lb_PageTotal
            // 
            this.lb_PageTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_PageTotal.ForeColor = System.Drawing.Color.Black;
            this.lb_PageTotal.Location = new System.Drawing.Point(507, 539);
            this.lb_PageTotal.Name = "lb_PageTotal";
            this.lb_PageTotal.Size = new System.Drawing.Size(40, 29);
            this.lb_PageTotal.TabIndex = 36;
            this.lb_PageTotal.Text = "0";
            this.lb_PageTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_PageNo
            // 
            this.lb_PageNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_PageNo.ForeColor = System.Drawing.Color.Black;
            this.lb_PageNo.Location = new System.Drawing.Point(441, 539);
            this.lb_PageNo.Name = "lb_PageNo";
            this.lb_PageNo.Size = new System.Drawing.Size(40, 29);
            this.lb_PageNo.TabIndex = 34;
            this.lb_PageNo.Text = "1";
            this.lb_PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(319, 539);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(114, 29);
            this.label28.TabIndex = 33;
            this.label28.Text = "Page No.";
            // 
            // btn_Next
            // 
            this.btn_Next.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_next;
            this.btn_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Next.FlatAppearance.BorderSize = 0;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Location = new System.Drawing.Point(810, 524);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(49, 58);
            this.btn_Next.TabIndex = 32;
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_previous;
            this.btn_Previous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Previous.FlatAppearance.BorderSize = 0;
            this.btn_Previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Previous.Location = new System.Drawing.Point(7, 526);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(49, 56);
            this.btn_Previous.TabIndex = 31;
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnOther_3);
            this.panel3.Controls.Add(this.btnOther_4);
            this.panel3.Controls.Add(this.btnOther_2);
            this.panel3.Controls.Add(this.btnOther_5);
            this.panel3.Controls.Add(this.btnOther_1);
            this.panel3.Controls.Add(this.btnOther_6);
            this.panel3.Location = new System.Drawing.Point(4, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(860, 426);
            this.panel3.TabIndex = 7;
            // 
            // btnOther_3
            // 
            this.btnOther_3.BackColor = System.Drawing.Color.White;
            this.btnOther_3.BackgroundImage = global::BJCBCPOS.Properties.Resources.Alipay;
            this.btnOther_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOther_3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOther_3.FlatAppearance.BorderSize = 0;
            this.btnOther_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOther_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOther_3.ForeColor = System.Drawing.Color.Gray;
            this.btnOther_3.Location = new System.Drawing.Point(577, 8);
            this.btnOther_3.Name = "btnOther_3";
            this.btnOther_3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnOther_3.Size = new System.Drawing.Size(265, 200);
            this.btnOther_3.TabIndex = 3;
            this.btnOther_3.Text = "Alipay";
            this.btnOther_3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOther_3.UseVisualStyleBackColor = false;
            this.btnOther_3.Visible = false;
            // 
            // btnOther_4
            // 
            this.btnOther_4.BackColor = System.Drawing.Color.White;
            this.btnOther_4.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rabbit;
            this.btnOther_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOther_4.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOther_4.FlatAppearance.BorderSize = 0;
            this.btnOther_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOther_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOther_4.ForeColor = System.Drawing.Color.Gray;
            this.btnOther_4.Location = new System.Drawing.Point(24, 217);
            this.btnOther_4.Name = "btnOther_4";
            this.btnOther_4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnOther_4.Size = new System.Drawing.Size(265, 200);
            this.btnOther_4.TabIndex = 4;
            this.btnOther_4.Text = "Rabbit";
            this.btnOther_4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOther_4.UseVisualStyleBackColor = false;
            this.btnOther_4.Visible = false;
            // 
            // btnOther_2
            // 
            this.btnOther_2.BackColor = System.Drawing.Color.White;
            this.btnOther_2.BackgroundImage = global::BJCBCPOS.Properties.Resources.WeChat;
            this.btnOther_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOther_2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOther_2.FlatAppearance.BorderSize = 0;
            this.btnOther_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOther_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOther_2.ForeColor = System.Drawing.Color.Gray;
            this.btnOther_2.Location = new System.Drawing.Point(302, 8);
            this.btnOther_2.Name = "btnOther_2";
            this.btnOther_2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnOther_2.Size = new System.Drawing.Size(265, 200);
            this.btnOther_2.TabIndex = 2;
            this.btnOther_2.Text = "WeChat Pay";
            this.btnOther_2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOther_2.UseVisualStyleBackColor = false;
            this.btnOther_2.Visible = false;
            // 
            // btnOther_5
            // 
            this.btnOther_5.BackColor = System.Drawing.Color.White;
            this.btnOther_5.BackgroundImage = global::BJCBCPOS.Properties.Resources.BigWallet;
            this.btnOther_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOther_5.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOther_5.FlatAppearance.BorderSize = 0;
            this.btnOther_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOther_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOther_5.ForeColor = System.Drawing.Color.Gray;
            this.btnOther_5.Location = new System.Drawing.Point(302, 217);
            this.btnOther_5.Name = "btnOther_5";
            this.btnOther_5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnOther_5.Size = new System.Drawing.Size(265, 200);
            this.btnOther_5.TabIndex = 5;
            this.btnOther_5.Text = "BigC wallet";
            this.btnOther_5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOther_5.UseVisualStyleBackColor = false;
            this.btnOther_5.Visible = false;
            // 
            // btnOther_1
            // 
            this.btnOther_1.BackColor = System.Drawing.Color.White;
            this.btnOther_1.BackgroundImage = global::BJCBCPOS.Properties.Resources.QR_Code;
            this.btnOther_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOther_1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOther_1.FlatAppearance.BorderSize = 0;
            this.btnOther_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOther_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOther_1.ForeColor = System.Drawing.Color.Gray;
            this.btnOther_1.Location = new System.Drawing.Point(24, 8);
            this.btnOther_1.Name = "btnOther_1";
            this.btnOther_1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnOther_1.Size = new System.Drawing.Size(265, 200);
            this.btnOther_1.TabIndex = 1;
            this.btnOther_1.Text = "QR Code";
            this.btnOther_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOther_1.UseVisualStyleBackColor = false;
            this.btnOther_1.Visible = false;
            // 
            // btnOther_6
            // 
            this.btnOther_6.BackColor = System.Drawing.Color.White;
            this.btnOther_6.BackgroundImage = global::BJCBCPOS.Properties.Resources.GiftVoucher;
            this.btnOther_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOther_6.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOther_6.FlatAppearance.BorderSize = 0;
            this.btnOther_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOther_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOther_6.ForeColor = System.Drawing.Color.Gray;
            this.btnOther_6.Location = new System.Drawing.Point(577, 217);
            this.btnOther_6.Name = "btnOther_6";
            this.btnOther_6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.btnOther_6.Size = new System.Drawing.Size(265, 200);
            this.btnOther_6.TabIndex = 6;
            this.btnOther_6.Text = "Gift voucher";
            this.btnOther_6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOther_6.UseVisualStyleBackColor = false;
            this.btnOther_6.Visible = false;
            // 
            // frmOtherPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOtherPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmOtherPayment";
            this.Load += new System.EventHandler(this.frmOtherPayment_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOther_1;
        private System.Windows.Forms.Button btnOther_2;
        private System.Windows.Forms.Button btnOther_3;
        private System.Windows.Forms.Button btnOther_4;
        private System.Windows.Forms.Button btnOther_5;
        private System.Windows.Forms.Button btnOther_6;
        private System.Windows.Forms.Label lbOtherPayment;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lb_PageTotal;
        private System.Windows.Forms.Label lb_PageNo;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_Previous;
    }
}