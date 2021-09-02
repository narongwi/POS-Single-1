namespace BJCBCPOS.OtherServices {
    partial class frmTopup
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.service_provider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.topup_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.topup_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpService = new System.Windows.Forms.GroupBox();
            this.btnBigService = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnBackMenu = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.listView5 = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textMenuID = new System.Windows.Forms.TextBox();
            this.textMenuName = new System.Windows.Forms.TextBox();
            this.lbMember = new System.Windows.Forms.Label();
            this.lbForm = new System.Windows.Forms.Label();
            this.listView3 = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listView6 = new System.Windows.Forms.ListView();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.btnOnline = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnPIN = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.grpAmount = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listView4 = new System.Windows.Forms.ListView();
            this.lbMemberName = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lbTotalAmount = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.btnCancel = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnConfirm = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnAddItem = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.textMember = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpService.SuspendLayout();
            this.grpType.SuspendLayout();
            this.grpAmount.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.service_provider,
            this.topup_type,
            this.product_code,
            this.topup_amount});
            this.dataGridView1.Location = new System.Drawing.Point(329, 598);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(683, 121);
            this.dataGridView1.TabIndex = 116;
            // 
            // service_provider
            // 
            this.service_provider.HeaderText = "ผู้ให้บริการ";
            this.service_provider.Name = "service_provider";
            this.service_provider.Width = 210;
            // 
            // topup_type
            // 
            this.topup_type.HeaderText = "ประเภทการเติมเงิน";
            this.topup_type.Name = "topup_type";
            this.topup_type.Width = 150;
            // 
            // product_code
            // 
            this.product_code.HeaderText = "เบอร์โทรศัพท์/รหัสสินค้า";
            this.product_code.Name = "product_code";
            this.product_code.Width = 220;
            // 
            // topup_amount
            // 
            this.topup_amount.HeaderText = "ยอดเติมเงิน";
            this.topup_amount.Name = "topup_amount";
            // 
            // grpService
            // 
            this.grpService.Controls.Add(this.btnBigService);
            this.grpService.Controls.Add(this.btnBackMenu);
            this.grpService.Controls.Add(this.listView5);
            this.grpService.Controls.Add(this.listView1);
            this.grpService.Controls.Add(this.textMenuID);
            this.grpService.Controls.Add(this.textMenuName);
            this.grpService.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpService.Location = new System.Drawing.Point(12, 66);
            this.grpService.Name = "grpService";
            this.grpService.Size = new System.Drawing.Size(311, 653);
            this.grpService.TabIndex = 112;
            this.grpService.TabStop = false;
            this.grpService.Text = "ผู้ให้บริการ";
            // 
            // btnBigService
            // 
            this.btnBigService.BackColor = System.Drawing.Color.YellowGreen;
            this.btnBigService.BackgroundColor = System.Drawing.Color.YellowGreen;
            this.btnBigService.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBigService.BorderRadius = 8;
            this.btnBigService.BorderSize = 1;
            this.btnBigService.FlatAppearance.BorderSize = 0;
            this.btnBigService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBigService.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBigService.ForeColor = System.Drawing.Color.White;
            this.btnBigService.IconColor = System.Drawing.Color.White;
            this.btnBigService.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.BankOutline;
            this.btnBigService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBigService.Location = new System.Drawing.Point(41, 584);
            this.btnBigService.Name = "btnBigService";
            this.btnBigService.Size = new System.Drawing.Size(136, 59);
            this.btnBigService.TabIndex = 152;
            this.btnBigService.Text = "บิ๊กเซอร์วิส";
            this.btnBigService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBigService.TextColor = System.Drawing.Color.White;
            this.btnBigService.UseVisualStyleBackColor = false;
            this.btnBigService.Click += new System.EventHandler(this.btnBigService_Click);
            // 
            // btnBackMenu
            // 
            this.btnBackMenu.BackColor = System.Drawing.Color.DarkOrange;
            this.btnBackMenu.BackgroundColor = System.Drawing.Color.DarkOrange;
            this.btnBackMenu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBackMenu.BorderRadius = 8;
            this.btnBackMenu.BorderSize = 1;
            this.btnBackMenu.FlatAppearance.BorderSize = 0;
            this.btnBackMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBackMenu.ForeColor = System.Drawing.Color.White;
            this.btnBackMenu.IconColor = System.Drawing.Color.White;
            this.btnBackMenu.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.ArrowLeftCircleOutline;
            this.btnBackMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackMenu.Location = new System.Drawing.Point(183, 584);
            this.btnBackMenu.Name = "btnBackMenu";
            this.btnBackMenu.Size = new System.Drawing.Size(122, 59);
            this.btnBackMenu.TabIndex = 151;
            this.btnBackMenu.Text = "ย้อนกลับ";
            this.btnBackMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackMenu.TextColor = System.Drawing.Color.White;
            this.btnBackMenu.UseVisualStyleBackColor = false;
            this.btnBackMenu.Click += new System.EventHandler(this.btnBackMenu_Click);
            // 
            // listView5
            // 
            this.listView5.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView5.BackColor = System.Drawing.Color.White;
            this.listView5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView5.HideSelection = false;
            this.listView5.Location = new System.Drawing.Point(6, 579);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(299, 68);
            this.listView5.TabIndex = 23;
            this.listView5.UseCompatibleStateImageBehavior = false;
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(299, 514);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // textMenuID
            // 
            this.textMenuID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textMenuID.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textMenuID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textMenuID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMenuID.Location = new System.Drawing.Point(6, 554);
            this.textMenuID.Name = "textMenuID";
            this.textMenuID.Size = new System.Drawing.Size(61, 19);
            this.textMenuID.TabIndex = 1;
            // 
            // textMenuName
            // 
            this.textMenuName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textMenuName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textMenuName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textMenuName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMenuName.Location = new System.Drawing.Point(73, 554);
            this.textMenuName.Name = "textMenuName";
            this.textMenuName.Size = new System.Drawing.Size(232, 19);
            this.textMenuName.TabIndex = 3;
            // 
            // lbMember
            // 
            this.lbMember.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMember.AutoSize = true;
            this.lbMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMember.Location = new System.Drawing.Point(540, 84);
            this.lbMember.Name = "lbMember";
            this.lbMember.Size = new System.Drawing.Size(219, 20);
            this.lbMember.TabIndex = 111;
            this.lbMember.Text = "หมายเลขสมาชิก/เบอร์โทรศัพท์";
            // 
            // lbForm
            // 
            this.lbForm.AutoSize = true;
            this.lbForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbForm.Location = new System.Drawing.Point(341, 79);
            this.lbForm.Name = "lbForm";
            this.lbForm.Size = new System.Drawing.Size(85, 25);
            this.lbForm.TabIndex = 99;
            this.lbForm.Text = "Top up";
            // 
            // listView3
            // 
            this.listView3.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView3.BackColor = System.Drawing.Color.White;
            this.listView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(329, 66);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(683, 526);
            this.listView3.TabIndex = 96;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 103;
            this.label3.Text = "นายทองดี นามสมมุติ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "สมาชิก : ";
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(658, 331);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(78, 13);
            this.label23.TabIndex = 140;
            this.label23.Text = "บัตรประชาชน :";
            // 
            // textBox13
            // 
            this.textBox13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox13.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox13.Location = new System.Drawing.Point(742, 322);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(265, 22);
            this.textBox13.TabIndex = 139;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(831, 354);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(176, 42);
            this.button6.TabIndex = 138;
            this.button6.Text = "อ่านบัตรประชนชน";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(343, 378);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(105, 13);
            this.label22.TabIndex = 137;
            this.label22.Text = "เบอร์โทรศัพท์ติดต่อ :";
            // 
            // textBox12
            // 
            this.textBox12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.Location = new System.Drawing.Point(454, 370);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(169, 26);
            this.textBox12.TabIndex = 136;
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(338, 346);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 13);
            this.label21.TabIndex = 135;
            this.label21.Text = " จำนวนเงินที่รับชำระ :";
            // 
            // textBox11
            // 
            this.textBox11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.Location = new System.Drawing.Point(454, 338);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(169, 26);
            this.textBox11.TabIndex = 134;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(694, 302);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(42, 13);
            this.label20.TabIndex = 133;
            this.label20.Text = "ที่อยู่ 3 :";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(694, 274);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(42, 13);
            this.label19.TabIndex = 132;
            this.label19.Text = "ที่อยู่ 2 :";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(694, 246);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 13);
            this.label18.TabIndex = 131;
            this.label18.Text = "ที่อยู่ 1 :";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(689, 218);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 130;
            this.label17.Text = "Vendor :";
            // 
            // textBox10
            // 
            this.textBox10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox10.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Enabled = false;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(742, 210);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(52, 22);
            this.textBox10.TabIndex = 129;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox9.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Enabled = false;
            this.textBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.Location = new System.Drawing.Point(742, 294);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(265, 22);
            this.textBox9.TabIndex = 128;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox8.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Enabled = false;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(742, 266);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(265, 22);
            this.textBox8.TabIndex = 127;
            // 
            // textBox7
            // 
            this.textBox7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox7.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Enabled = false;
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(742, 238);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(265, 22);
            this.textBox7.TabIndex = 126;
            // 
            // textBox6
            // 
            this.textBox6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox6.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Enabled = false;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(800, 210);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(207, 22);
            this.textBox6.TabIndex = 115;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(343, 314);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 125;
            this.label16.Text = "Ref. 4 :";
            // 
            // textBox5
            // 
            this.textBox5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(391, 306);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(232, 26);
            this.textBox5.TabIndex = 124;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(343, 282);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 123;
            this.label15.Text = "Ref. 3 :";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(391, 274);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(232, 26);
            this.textBox4.TabIndex = 122;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(343, 250);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 121;
            this.label14.Text = "Ref. 2 :";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(391, 242);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(232, 26);
            this.textBox3.TabIndex = 120;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(343, 218);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 119;
            this.label13.Text = "Ref. 1 :";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(391, 210);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(232, 26);
            this.textBox2.TabIndex = 118;
            // 
            // listView6
            // 
            this.listView6.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView6.BackColor = System.Drawing.Color.Gainsboro;
            this.listView6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView6.HideSelection = false;
            this.listView6.Location = new System.Drawing.Point(329, 201);
            this.listView6.Name = "listView6";
            this.listView6.Size = new System.Drawing.Size(683, 198);
            this.listView6.TabIndex = 117;
            this.listView6.UseCompatibleStateImageBehavior = false;
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.btnOnline);
            this.grpType.Controls.Add(this.btnPIN);
            this.grpType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpType.Location = new System.Drawing.Point(329, 189);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(226, 329);
            this.grpType.TabIndex = 141;
            this.grpType.TabStop = false;
            this.grpType.Text = "ประเภทการเติมเงิน";
            // 
            // btnOnline
            // 
            this.btnOnline.BackColor = System.Drawing.Color.Thistle;
            this.btnOnline.BackgroundColor = System.Drawing.Color.Thistle;
            this.btnOnline.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOnline.BorderRadius = 8;
            this.btnOnline.BorderSize = 1;
            this.btnOnline.FlatAppearance.BorderSize = 0;
            this.btnOnline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOnline.ForeColor = System.Drawing.Color.White;
            this.btnOnline.IconColor = System.Drawing.Color.White;
            this.btnOnline.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.AccessPointNetwork;
            this.btnOnline.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOnline.Location = new System.Drawing.Point(27, 173);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(173, 90);
            this.btnOnline.TabIndex = 153;
            this.btnOnline.Text = "Online";
            this.btnOnline.TextColor = System.Drawing.Color.White;
            this.btnOnline.UseVisualStyleBackColor = false;
            // 
            // btnPIN
            // 
            this.btnPIN.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnPIN.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnPIN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPIN.BorderRadius = 8;
            this.btnPIN.BorderSize = 1;
            this.btnPIN.FlatAppearance.BorderSize = 0;
            this.btnPIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPIN.ForeColor = System.Drawing.Color.White;
            this.btnPIN.IconColor = System.Drawing.Color.White;
            this.btnPIN.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.PinOffOutline;
            this.btnPIN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPIN.Location = new System.Drawing.Point(27, 50);
            this.btnPIN.Name = "btnPIN";
            this.btnPIN.Size = new System.Drawing.Size(173, 90);
            this.btnPIN.TabIndex = 152;
            this.btnPIN.Text = "PIN";
            this.btnPIN.TextColor = System.Drawing.Color.White;
            this.btnPIN.UseVisualStyleBackColor = false;
            // 
            // grpAmount
            // 
            this.grpAmount.Controls.Add(this.label10);
            this.grpAmount.Controls.Add(this.label26);
            this.grpAmount.Controls.Add(this.label27);
            this.grpAmount.Controls.Add(this.label9);
            this.grpAmount.Controls.Add(this.label7);
            this.grpAmount.Controls.Add(this.label8);
            this.grpAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAmount.Location = new System.Drawing.Point(556, 189);
            this.grpAmount.Name = "grpAmount";
            this.grpAmount.Size = new System.Drawing.Size(456, 329);
            this.grpAmount.TabIndex = 142;
            this.grpAmount.TabStop = false;
            this.grpAmount.Text = "ยอดการเติมเงิน";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(314, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 65);
            this.label10.TabIndex = 154;
            this.label10.Text = "500";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Gainsboro;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(160, 133);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(133, 65);
            this.label26.TabIndex = 153;
            this.label26.Text = "300";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Gainsboro;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(7, 133);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(133, 65);
            this.label27.TabIndex = 152;
            this.label27.Text = "200";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(314, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 65);
            this.label9.TabIndex = 151;
            this.label9.Text = "100";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(160, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 65);
            this.label7.TabIndex = 150;
            this.label7.Text = "50";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 65);
            this.label8.TabIndex = 149;
            this.label8.Text = "20";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView4
            // 
            this.listView4.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView4.BackColor = System.Drawing.Color.MistyRose;
            this.listView4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(329, 113);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(683, 56);
            this.listView4.TabIndex = 155;
            this.listView4.UseCompatibleStateImageBehavior = false;
            // 
            // lbMemberName
            // 
            this.lbMemberName.AutoSize = true;
            this.lbMemberName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMemberName.Location = new System.Drawing.Point(388, 131);
            this.lbMemberName.Name = "lbMemberName";
            this.lbMemberName.Size = new System.Drawing.Size(70, 18);
            this.lbMemberName.TabIndex = 156;
            this.lbMemberName.Text = "ชื่อสมาชิก";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(465, 131);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(132, 18);
            this.label29.TabIndex = 157;
            this.label29.Text = "นายบุดดี นามสมมุติ";
            // 
            // lbTotalAmount
            // 
            this.lbTotalAmount.AutoSize = true;
            this.lbTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalAmount.Location = new System.Drawing.Point(762, 122);
            this.lbTotalAmount.Name = "lbTotalAmount";
            this.lbTotalAmount.Size = new System.Drawing.Size(61, 18);
            this.lbTotalAmount.TabIndex = 158;
            this.lbTotalAmount.Text = "ยอดรวม";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(963, 146);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 18);
            this.label25.TabIndex = 159;
            this.label25.Text = "บาท";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.GreenYellow;
            this.label30.Location = new System.Drawing.Point(850, 131);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(104, 25);
            this.label30.TabIndex = 160;
            this.label30.Text = "2,000.00";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.IconColor = System.Drawing.Color.White;
            this.btnCancel.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.MinusBoxMultipleOutline;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(816, 529);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 59);
            this.btnCancel.TabIndex = 186;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextColor = System.Drawing.Color.White;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnConfirm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnConfirm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfirm.BorderRadius = 8;
            this.btnConfirm.BorderSize = 1;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.IconColor = System.Drawing.Color.White;
            this.btnConfirm.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.CheckCircleOutline;
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(565, 529);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(245, 59);
            this.btnConfirm.TabIndex = 185;
            this.btnConfirm.Text = "เติมเงิน";
            this.btnConfirm.TextColor = System.Drawing.Color.White;
            this.btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAddItem.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAddItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddItem.BorderRadius = 8;
            this.btnAddItem.BorderSize = 1;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.IconColor = System.Drawing.Color.White;
            this.btnAddItem.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.PlusBoxMultipleOutline;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.Location = new System.Drawing.Point(333, 529);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(226, 59);
            this.btnAddItem.TabIndex = 187;
            this.btnAddItem.Text = "เพิ่มรายการ";
            this.btnAddItem.TextColor = System.Drawing.Color.White;
            this.btnAddItem.UseVisualStyleBackColor = false;
            // 
            // textMember
            // 
            this.textMember.BackColor = System.Drawing.SystemColors.Window;
            this.textMember.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.textMember.BorderFocusColor = System.Drawing.Color.HotPink;
            this.textMember.BorderRadius = 8;
            this.textMember.BorderSize = 2;
            this.textMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textMember.Location = new System.Drawing.Point(766, 72);
            this.textMember.Margin = new System.Windows.Forms.Padding(4);
            this.textMember.Multiline = false;
            this.textMember.Name = "textMember";
            this.textMember.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.textMember.PasswordChar = false;
            this.textMember.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textMember.PlaceholderText = "";
            this.textMember.Size = new System.Drawing.Size(246, 34);
            this.textMember.TabIndex = 188;
            this.textMember.Texts = "";
            this.textMember.UnderlinedStyle = false;
            // 
            // Topup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.textMember);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.lbTotalAmount);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.lbMemberName);
            this.Controls.Add(this.listView4);
            this.Controls.Add(this.grpAmount);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grpService);
            this.Controls.Add(this.lbMember);
            this.Controls.Add(this.lbForm);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.listView6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Topup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Topup";
            this.Load += new System.EventHandler(this.Topup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpService.ResumeLayout(false);
            this.grpService.PerformLayout();
            this.grpType.ResumeLayout(false);
            this.grpAmount.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grpService;
        private System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textMenuID;
        private System.Windows.Forms.TextBox textMenuName;
        private System.Windows.Forms.Label lbMember;
        private System.Windows.Forms.Label lbForm;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListView listView6;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.GroupBox grpAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.Label lbMemberName;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lbTotalAmount;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.DataGridViewTextBoxColumn service_provider;
        private System.Windows.Forms.DataGridViewTextBoxColumn topup_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn topup_amount;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnBigService;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnBackMenu;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnCancel;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnConfirm;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnAddItem;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnOnline;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnPIN;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox textMember;
    }
}