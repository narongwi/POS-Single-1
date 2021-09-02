namespace BJCBCPOS.OtherServices {
    partial class frmParcel
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
            this.lbForm = new System.Windows.Forms.Label();
            this.lvKerryNormal = new System.Windows.Forms.ListView();
            this.lbMember = new System.Windows.Forms.Label();
            this.listView4 = new System.Windows.Forms.ListView();
            this.grpService = new System.Windows.Forms.GroupBox();
            this.btnBigService = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnBackMenu = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.listView5 = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textMenuID = new System.Windows.Forms.TextBox();
            this.textMenuName = new System.Windows.Forms.TextBox();
            this.listView7 = new System.Windows.Forms.ListView();
            this.label24 = new System.Windows.Forms.Label();
            this.lbMemberName = new System.Windows.Forms.Label();
            this.grpSender = new System.Windows.Forms.GroupBox();
            this.deftsoftTextbox5 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox4 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox3 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.btnReadIDCard = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbPostSender = new System.Windows.Forms.Label();
            this.lbTelSender = new System.Windows.Forms.Label();
            this.lbIDCard = new System.Windows.Forms.Label();
            this.lbCardType = new System.Windows.Forms.Label();
            this.grpReceiver = new System.Windows.Forms.GroupBox();
            this.deftsoftTextbox7 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox6 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.lbPostReceiver = new System.Windows.Forms.Label();
            this.lbTelReceiver = new System.Windows.Forms.Label();
            this.grpParcel = new System.Windows.Forms.GroupBox();
            this.deftsoftTextbox1 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbTrackingNo = new System.Windows.Forms.Label();
            this.lbParcelSize = new System.Windows.Forms.Label();
            this.btnCancel = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnConfirm = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.textMember = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.grpService.SuspendLayout();
            this.grpSender.SuspendLayout();
            this.grpReceiver.SuspendLayout();
            this.grpParcel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbForm
            // 
            this.lbForm.AutoSize = true;
            this.lbForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbForm.Location = new System.Drawing.Point(341, 78);
            this.lbForm.Name = "lbForm";
            this.lbForm.Size = new System.Drawing.Size(79, 25);
            this.lbForm.TabIndex = 151;
            this.lbForm.Text = "Parcel";
            // 
            // lvKerryNormal
            // 
            this.lvKerryNormal.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvKerryNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvKerryNormal.BackColor = System.Drawing.Color.White;
            this.lvKerryNormal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvKerryNormal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvKerryNormal.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lvKerryNormal.HideSelection = false;
            this.lvKerryNormal.Location = new System.Drawing.Point(329, 66);
            this.lvKerryNormal.Name = "lvKerryNormal";
            this.lvKerryNormal.Size = new System.Drawing.Size(683, 460);
            this.lvKerryNormal.TabIndex = 148;
            this.lvKerryNormal.UseCompatibleStateImageBehavior = false;
            // 
            // lbMember
            // 
            this.lbMember.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMember.AutoSize = true;
            this.lbMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMember.Location = new System.Drawing.Point(557, 85);
            this.lbMember.Name = "lbMember";
            this.lbMember.Size = new System.Drawing.Size(202, 18);
            this.lbMember.TabIndex = 156;
            this.lbMember.Text = "หมายเลขสมาชิก/เบอร์โทรศัพท์";
            // 
            // listView4
            // 
            this.listView4.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listView4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(329, 122);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(683, 42);
            this.listView4.TabIndex = 153;
            this.listView4.UseCompatibleStateImageBehavior = false;
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
            this.grpService.TabIndex = 157;
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
            this.btnBigService.TabIndex = 110;
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
            this.btnBackMenu.TabIndex = 109;
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
            this.listView5.Size = new System.Drawing.Size(299, 67);
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
            // listView7
            // 
            this.listView7.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listView7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView7.HideSelection = false;
            this.listView7.Location = new System.Drawing.Point(329, 113);
            this.listView7.Name = "listView7";
            this.listView7.Size = new System.Drawing.Size(683, 42);
            this.listView7.TabIndex = 162;
            this.listView7.UseCompatibleStateImageBehavior = false;
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(765, 122);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(141, 20);
            this.label24.TabIndex = 164;
            this.label24.Text = "นายบุดดี นามสมมุติ";
            // 
            // lbMemberName
            // 
            this.lbMemberName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMemberName.AutoSize = true;
            this.lbMemberName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMemberName.Location = new System.Drawing.Point(702, 122);
            this.lbMemberName.Name = "lbMemberName";
            this.lbMemberName.Size = new System.Drawing.Size(57, 20);
            this.lbMemberName.TabIndex = 163;
            this.lbMemberName.Text = "สมาชิก";
            // 
            // grpSender
            // 
            this.grpSender.Controls.Add(this.deftsoftTextbox5);
            this.grpSender.Controls.Add(this.deftsoftTextbox4);
            this.grpSender.Controls.Add(this.deftsoftTextbox3);
            this.grpSender.Controls.Add(this.btnReadIDCard);
            this.grpSender.Controls.Add(this.comboBox1);
            this.grpSender.Controls.Add(this.lbPostSender);
            this.grpSender.Controls.Add(this.lbTelSender);
            this.grpSender.Controls.Add(this.lbIDCard);
            this.grpSender.Controls.Add(this.lbCardType);
            this.grpSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSender.Location = new System.Drawing.Point(329, 161);
            this.grpSender.Name = "grpSender";
            this.grpSender.Size = new System.Drawing.Size(337, 444);
            this.grpSender.TabIndex = 168;
            this.grpSender.TabStop = false;
            this.grpSender.Text = "ข้อมูลผู้ส่ง";
            // 
            // deftsoftTextbox5
            // 
            this.deftsoftTextbox5.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox5.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox5.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox5.BorderRadius = 8;
            this.deftsoftTextbox5.BorderSize = 2;
            this.deftsoftTextbox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox5.Location = new System.Drawing.Point(31, 262);
            this.deftsoftTextbox5.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox5.Multiline = false;
            this.deftsoftTextbox5.Name = "deftsoftTextbox5";
            this.deftsoftTextbox5.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox5.PasswordChar = false;
            this.deftsoftTextbox5.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox5.PlaceholderText = "";
            this.deftsoftTextbox5.Size = new System.Drawing.Size(283, 34);
            this.deftsoftTextbox5.TabIndex = 185;
            this.deftsoftTextbox5.Texts = "";
            this.deftsoftTextbox5.UnderlinedStyle = false;
            // 
            // deftsoftTextbox4
            // 
            this.deftsoftTextbox4.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox4.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox4.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox4.BorderRadius = 8;
            this.deftsoftTextbox4.BorderSize = 2;
            this.deftsoftTextbox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox4.Location = new System.Drawing.Point(31, 194);
            this.deftsoftTextbox4.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox4.Multiline = false;
            this.deftsoftTextbox4.Name = "deftsoftTextbox4";
            this.deftsoftTextbox4.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox4.PasswordChar = false;
            this.deftsoftTextbox4.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox4.PlaceholderText = "";
            this.deftsoftTextbox4.Size = new System.Drawing.Size(283, 34);
            this.deftsoftTextbox4.TabIndex = 184;
            this.deftsoftTextbox4.Texts = "";
            this.deftsoftTextbox4.UnderlinedStyle = false;
            // 
            // deftsoftTextbox3
            // 
            this.deftsoftTextbox3.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox3.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox3.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox3.BorderRadius = 8;
            this.deftsoftTextbox3.BorderSize = 2;
            this.deftsoftTextbox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox3.Location = new System.Drawing.Point(31, 126);
            this.deftsoftTextbox3.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox3.Multiline = false;
            this.deftsoftTextbox3.Name = "deftsoftTextbox3";
            this.deftsoftTextbox3.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox3.PasswordChar = false;
            this.deftsoftTextbox3.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox3.PlaceholderText = "";
            this.deftsoftTextbox3.Size = new System.Drawing.Size(283, 34);
            this.deftsoftTextbox3.TabIndex = 183;
            this.deftsoftTextbox3.Texts = "";
            this.deftsoftTextbox3.UnderlinedStyle = false;
            // 
            // btnReadIDCard
            // 
            this.btnReadIDCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(192)))), ((int)(((byte)(100)))));
            this.btnReadIDCard.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(192)))), ((int)(((byte)(100)))));
            this.btnReadIDCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReadIDCard.BorderRadius = 8;
            this.btnReadIDCard.BorderSize = 1;
            this.btnReadIDCard.FlatAppearance.BorderSize = 0;
            this.btnReadIDCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadIDCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReadIDCard.ForeColor = System.Drawing.Color.White;
            this.btnReadIDCard.IconColor = System.Drawing.Color.White;
            this.btnReadIDCard.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.SearchWeb;
            this.btnReadIDCard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReadIDCard.Location = new System.Drawing.Point(108, 311);
            this.btnReadIDCard.Name = "btnReadIDCard";
            this.btnReadIDCard.Size = new System.Drawing.Size(206, 43);
            this.btnReadIDCard.TabIndex = 177;
            this.btnReadIDCard.Text = "อ่านบัตรประชาชน";
            this.btnReadIDCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReadIDCard.TextColor = System.Drawing.Color.White;
            this.btnReadIDCard.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(31, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(283, 32);
            this.comboBox1.TabIndex = 176;
            // 
            // lbPostSender
            // 
            this.lbPostSender.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbPostSender.AutoSize = true;
            this.lbPostSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPostSender.Location = new System.Drawing.Point(33, 238);
            this.lbPostSender.Name = "lbPostSender";
            this.lbPostSender.Size = new System.Drawing.Size(148, 20);
            this.lbPostSender.TabIndex = 174;
            this.lbPostSender.Text = "รหัสไปรษณีย์ของผู้ส่ง";
            // 
            // lbTelSender
            // 
            this.lbTelSender.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTelSender.AutoSize = true;
            this.lbTelSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTelSender.Location = new System.Drawing.Point(33, 168);
            this.lbTelSender.Name = "lbTelSender";
            this.lbTelSender.Size = new System.Drawing.Size(168, 20);
            this.lbTelSender.TabIndex = 172;
            this.lbTelSender.Text = "เบอร์โทรศัพท์ติดต่อผู้ส่ง";
            // 
            // lbIDCard
            // 
            this.lbIDCard.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbIDCard.AutoSize = true;
            this.lbIDCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIDCard.Location = new System.Drawing.Point(33, 102);
            this.lbIDCard.Name = "lbIDCard";
            this.lbIDCard.Size = new System.Drawing.Size(165, 20);
            this.lbIDCard.TabIndex = 170;
            this.lbIDCard.Text = "หมายเลขบัตรประชาชน";
            // 
            // lbCardType
            // 
            this.lbCardType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbCardType.AutoSize = true;
            this.lbCardType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardType.Location = new System.Drawing.Point(33, 38);
            this.lbCardType.Name = "lbCardType";
            this.lbCardType.Size = new System.Drawing.Size(91, 20);
            this.lbCardType.TabIndex = 169;
            this.lbCardType.Text = "ประเภทบัตร";
            // 
            // grpReceiver
            // 
            this.grpReceiver.Controls.Add(this.deftsoftTextbox7);
            this.grpReceiver.Controls.Add(this.deftsoftTextbox6);
            this.grpReceiver.Controls.Add(this.lbPostReceiver);
            this.grpReceiver.Controls.Add(this.lbTelReceiver);
            this.grpReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReceiver.Location = new System.Drawing.Point(672, 161);
            this.grpReceiver.Name = "grpReceiver";
            this.grpReceiver.Size = new System.Drawing.Size(340, 188);
            this.grpReceiver.TabIndex = 157;
            this.grpReceiver.TabStop = false;
            this.grpReceiver.Text = "ข้อมูลผู้รับ";
            // 
            // deftsoftTextbox7
            // 
            this.deftsoftTextbox7.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox7.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox7.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox7.BorderRadius = 8;
            this.deftsoftTextbox7.BorderSize = 2;
            this.deftsoftTextbox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox7.Location = new System.Drawing.Point(21, 62);
            this.deftsoftTextbox7.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox7.Multiline = false;
            this.deftsoftTextbox7.Name = "deftsoftTextbox7";
            this.deftsoftTextbox7.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox7.PasswordChar = false;
            this.deftsoftTextbox7.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox7.PlaceholderText = "";
            this.deftsoftTextbox7.Size = new System.Drawing.Size(283, 34);
            this.deftsoftTextbox7.TabIndex = 180;
            this.deftsoftTextbox7.Texts = "";
            this.deftsoftTextbox7.UnderlinedStyle = false;
            // 
            // deftsoftTextbox6
            // 
            this.deftsoftTextbox6.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox6.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox6.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox6.BorderRadius = 8;
            this.deftsoftTextbox6.BorderSize = 2;
            this.deftsoftTextbox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox6.Location = new System.Drawing.Point(21, 126);
            this.deftsoftTextbox6.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox6.Multiline = false;
            this.deftsoftTextbox6.Name = "deftsoftTextbox6";
            this.deftsoftTextbox6.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox6.PasswordChar = false;
            this.deftsoftTextbox6.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox6.PlaceholderText = "";
            this.deftsoftTextbox6.Size = new System.Drawing.Size(283, 34);
            this.deftsoftTextbox6.TabIndex = 179;
            this.deftsoftTextbox6.Texts = "";
            this.deftsoftTextbox6.UnderlinedStyle = false;
            // 
            // lbPostReceiver
            // 
            this.lbPostReceiver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbPostReceiver.AutoSize = true;
            this.lbPostReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPostReceiver.Location = new System.Drawing.Point(23, 102);
            this.lbPostReceiver.Name = "lbPostReceiver";
            this.lbPostReceiver.Size = new System.Drawing.Size(149, 20);
            this.lbPostReceiver.TabIndex = 178;
            this.lbPostReceiver.Text = "รหัสไปรษณีย์ของผู้รับ";
            // 
            // lbTelReceiver
            // 
            this.lbTelReceiver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTelReceiver.AutoSize = true;
            this.lbTelReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTelReceiver.Location = new System.Drawing.Point(23, 38);
            this.lbTelReceiver.Name = "lbTelReceiver";
            this.lbTelReceiver.Size = new System.Drawing.Size(169, 20);
            this.lbTelReceiver.TabIndex = 176;
            this.lbTelReceiver.Text = "เบอร์โทรศัพท์ติดต่อผู้รับ";
            // 
            // grpParcel
            // 
            this.grpParcel.Controls.Add(this.deftsoftTextbox1);
            this.grpParcel.Controls.Add(this.comboBox2);
            this.grpParcel.Controls.Add(this.lbTrackingNo);
            this.grpParcel.Controls.Add(this.lbParcelSize);
            this.grpParcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpParcel.Location = new System.Drawing.Point(672, 355);
            this.grpParcel.Name = "grpParcel";
            this.grpParcel.Size = new System.Drawing.Size(340, 250);
            this.grpParcel.TabIndex = 180;
            this.grpParcel.TabStop = false;
            this.grpParcel.Text = "ข้อมูลพัสดุ";
            // 
            // deftsoftTextbox1
            // 
            this.deftsoftTextbox1.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox1.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox1.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox1.BorderRadius = 8;
            this.deftsoftTextbox1.BorderSize = 2;
            this.deftsoftTextbox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox1.Location = new System.Drawing.Point(21, 126);
            this.deftsoftTextbox1.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox1.Multiline = false;
            this.deftsoftTextbox1.Name = "deftsoftTextbox1";
            this.deftsoftTextbox1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox1.PasswordChar = false;
            this.deftsoftTextbox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox1.PlaceholderText = "";
            this.deftsoftTextbox1.Size = new System.Drawing.Size(283, 34);
            this.deftsoftTextbox1.TabIndex = 178;
            this.deftsoftTextbox1.Texts = "";
            this.deftsoftTextbox1.UnderlinedStyle = false;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(21, 67);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(283, 32);
            this.comboBox2.TabIndex = 177;
            // 
            // lbTrackingNo
            // 
            this.lbTrackingNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTrackingNo.AutoSize = true;
            this.lbTrackingNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrackingNo.Location = new System.Drawing.Point(23, 102);
            this.lbTrackingNo.Name = "lbTrackingNo";
            this.lbTrackingNo.Size = new System.Drawing.Size(109, 20);
            this.lbTrackingNo.TabIndex = 178;
            this.lbTrackingNo.Text = "Tracking No.";
            // 
            // lbParcelSize
            // 
            this.lbParcelSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbParcelSize.AutoSize = true;
            this.lbParcelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbParcelSize.Location = new System.Drawing.Point(23, 44);
            this.lbParcelSize.Name = "lbParcelSize";
            this.lbParcelSize.Size = new System.Drawing.Size(113, 20);
            this.lbParcelSize.TabIndex = 176;
            this.lbParcelSize.Text = "ขนาดกล่องพัสดุ";
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
            this.btnCancel.Location = new System.Drawing.Point(816, 660);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 59);
            this.btnCancel.TabIndex = 182;
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
            this.btnConfirm.Location = new System.Drawing.Point(565, 660);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(245, 59);
            this.btnConfirm.TabIndex = 181;
            this.btnConfirm.Text = "ตกลง";
            this.btnConfirm.TextColor = System.Drawing.Color.White;
            this.btnConfirm.UseVisualStyleBackColor = false;
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
            this.textMember.Location = new System.Drawing.Point(766, 76);
            this.textMember.Margin = new System.Windows.Forms.Padding(4);
            this.textMember.Multiline = false;
            this.textMember.Name = "textMember";
            this.textMember.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.textMember.PasswordChar = false;
            this.textMember.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textMember.PlaceholderText = "";
            this.textMember.Size = new System.Drawing.Size(246, 34);
            this.textMember.TabIndex = 179;
            this.textMember.Texts = "";
            this.textMember.UnderlinedStyle = false;
            // 
            // Parcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.textMember);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.grpParcel);
            this.Controls.Add(this.grpReceiver);
            this.Controls.Add(this.grpSender);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lbMemberName);
            this.Controls.Add(this.listView7);
            this.Controls.Add(this.lbForm);
            this.Controls.Add(this.lbMember);
            this.Controls.Add(this.grpService);
            this.Controls.Add(this.lvKerryNormal);
            this.Controls.Add(this.listView4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Parcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parcel";
            this.Load += new System.EventHandler(this.Parcel_Load);
            this.grpService.ResumeLayout(false);
            this.grpService.PerformLayout();
            this.grpSender.ResumeLayout(false);
            this.grpSender.PerformLayout();
            this.grpReceiver.ResumeLayout(false);
            this.grpReceiver.PerformLayout();
            this.grpParcel.ResumeLayout(false);
            this.grpParcel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbForm;
        private System.Windows.Forms.ListView lvKerryNormal;
        private System.Windows.Forms.Label lbMember;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.GroupBox grpService;
        private System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textMenuID;
        private System.Windows.Forms.TextBox textMenuName;
        private System.Windows.Forms.ListView listView7;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lbMemberName;
        private System.Windows.Forms.GroupBox grpSender;
        private System.Windows.Forms.Label lbPostSender;
        private System.Windows.Forms.Label lbTelSender;
        private System.Windows.Forms.Label lbIDCard;
        private System.Windows.Forms.Label lbCardType;
        private System.Windows.Forms.GroupBox grpReceiver;
        private System.Windows.Forms.Label lbPostReceiver;
        private System.Windows.Forms.Label lbTelReceiver;
        private System.Windows.Forms.GroupBox grpParcel;
        private System.Windows.Forms.Label lbTrackingNo;
        private System.Windows.Forms.Label lbParcelSize;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnBigService;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnBackMenu;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnCancel;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnConfirm;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnReadIDCard;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox5;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox4;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox3;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox7;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox6;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox1;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox textMember;
    }
}