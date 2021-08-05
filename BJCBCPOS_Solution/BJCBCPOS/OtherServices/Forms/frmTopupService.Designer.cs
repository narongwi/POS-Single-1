namespace BJCBCPOS.OtherServices.Forms {
    partial class frmTopupService
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
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referenceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.feeamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.GreenYellow;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(351, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(176, 94);
            this.button3.TabIndex = 45;
            this.button3.Text = "ตกลง (F2)";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vendor,
            this.referenceno,
            this.payamount,
            this.feeamount});
            this.dataGridView1.Location = new System.Drawing.Point(6, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1001, 141);
            this.dataGridView1.TabIndex = 0;
            // 
            // vendor
            // 
            this.vendor.HeaderText = "Vendor";
            this.vendor.Name = "vendor";
            this.vendor.Width = 300;
            // 
            // referenceno
            // 
            this.referenceno.HeaderText = "เลขที่อ้างอิง";
            this.referenceno.Name = "referenceno";
            this.referenceno.Width = 300;
            // 
            // payamount
            // 
            this.payamount.HeaderText = "จำนวนเงิน";
            this.payamount.Name = "payamount";
            this.payamount.Width = 195;
            // 
            // feeamount
            // 
            this.feeamount.HeaderText = "ค่าบริการ";
            this.feeamount.Name = "feeamount";
            this.feeamount.Width = 195;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(10, 284);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1013, 184);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "รายละเอียดการชำระเงิน";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightCoral;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(456, 82);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(176, 94);
            this.button4.TabIndex = 45;
            this.button4.Text = "ชำระเงิน (F3)";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.textBox15);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBox14);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBox9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.textBox11);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox12);
            this.groupBox3.Controls.Add(this.textBox13);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(10, 472);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1013, 209);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "มูลค่าสินค้า";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "เงินสด",
            "บัตรเครดิต"});
            this.comboBox1.Location = new System.Drawing.Point(160, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(270, 33);
            this.comboBox1.TabIndex = 37;
            // 
            // textBox15
            // 
            this.textBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.ForeColor = System.Drawing.Color.Green;
            this.textBox15.Location = new System.Drawing.Point(160, 139);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(270, 38);
            this.textBox15.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(715, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 24);
            this.label5.TabIndex = 29;
            this.label5.Text = "มูลค่าสินค้ารวม";
            // 
            // textBox14
            // 
            this.textBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.DarkRed;
            this.textBox14.Location = new System.Drawing.Point(160, 83);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(270, 38);
            this.textBox14.TabIndex = 40;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(849, 34);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(146, 24);
            this.textBox6.TabIndex = 29;
            this.textBox6.Text = "499.00";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(79, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 24);
            this.label12.TabIndex = 39;
            this.label12.Text = "เงินทอน";
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.Location = new System.Drawing.Point(849, 62);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(146, 22);
            this.textBox9.TabIndex = 30;
            this.textBox9.Text = "0.00";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(95, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 24);
            this.label8.TabIndex = 38;
            this.label8.Text = "รับเงิน";
            // 
            // textBox10
            // 
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(849, 90);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(146, 22);
            this.textBox10.TabIndex = 31;
            this.textBox10.Text = "0.00";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox11
            // 
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.Location = new System.Drawing.Point(849, 118);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(146, 22);
            this.textBox11.TabIndex = 32;
            this.textBox11.Text = "0.00";
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(7, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 24);
            this.label6.TabIndex = 36;
            this.label6.Text = "ชำระค่าสินค้าโดย";
            // 
            // textBox12
            // 
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.Location = new System.Drawing.Point(849, 146);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(146, 22);
            this.textBox12.TabIndex = 33;
            this.textBox12.Text = "0.00";
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox13
            // 
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox13.Location = new System.Drawing.Point(849, 179);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(146, 24);
            this.textBox13.TabIndex = 35;
            this.textBox13.Text = "499.00";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(723, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 24);
            this.label7.TabIndex = 34;
            this.label7.Text = "ยอดเงินคงค้าง";
            // 
            // textBox16
            // 
            this.textBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox16.Location = new System.Drawing.Point(440, 55);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(503, 29);
            this.textBox16.TabIndex = 42;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(148, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 29);
            this.textBox1.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 24);
            this.label3.TabIndex = 41;
            this.label3.Text = "หมายเลขสมาชิก";
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 689);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1037, 40);
            this.ucFooter1.TabIndex = 39;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "เติมเงิน";
            this.ucHeader1.currentMenuTitle2 = "";
            this.ucHeader1.currentMenuTitle3 = "";
            this.ucHeader1.Location = new System.Drawing.Point(1, 1);
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
            this.ucHeader1.TabIndex = 38;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1013, 184);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "เลือกประเภทการเติมเงิน";
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.amount});
            this.dataGridView2.Location = new System.Drawing.Point(548, 19);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(459, 159);
            this.dataGridView2.TabIndex = 1;
            // 
            // amount
            // 
            this.amount.HeaderText = "จำนวนเงินที่ต้องการเติม";
            this.amount.Name = "amount";
            this.amount.Width = 450;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "เงินสด",
            "บัตรเครดิต"});
            this.comboBox2.Location = new System.Drawing.Point(11, 30);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(516, 47);
            this.comboBox2.TabIndex = 46;
            // 
            // frmTopupService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1037, 729);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucFooter1);
            this.Controls.Add(this.ucHeader1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTopupService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmTopupService";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private UCFooter ucFooter1;
        private UCHeader ucHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn referenceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn payamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn feeamount;
    }
}