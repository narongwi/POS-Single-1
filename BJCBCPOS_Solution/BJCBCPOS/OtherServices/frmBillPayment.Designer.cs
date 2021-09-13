namespace BJCBCPOS.OtherServices {
  partial class frmBillPayment{
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
            this.lbIDCard = new System.Windows.Forms.Label();
            this.lbTelNo = new System.Windows.Forms.Label();
            this.lbAmountReceive = new System.Windows.Forms.Label();
            this.lbAddress3 = new System.Windows.Forms.Label();
            this.lbAddress2 = new System.Windows.Forms.Label();
            this.lbAddress1 = new System.Windows.Forms.Label();
            this.lbVendor = new System.Windows.Forms.Label();
            this.lbRef4 = new System.Windows.Forms.Label();
            this.lbRef3 = new System.Windows.Forms.Label();
            this.lbRef2 = new System.Windows.Forms.Label();
            this.lbRef1 = new System.Windows.Forms.Label();
            this.listView6 = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textMenuID = new System.Windows.Forms.TextBox();
            this.textMenuName = new System.Windows.Forms.TextBox();
            this.grdBillDetail = new System.Windows.Forms.DataGridView();
            this.serviceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.servicename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fee_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpService = new System.Windows.Forms.GroupBox();
            this.btnBigService = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnBackMenu = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.listView5 = new System.Windows.Forms.ListView();
            this.lbMember = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbVat = new System.Windows.Forms.Label();
            this.lbServiceCharge = new System.Windows.Forms.Label();
            this.lbCurrency = new System.Windows.Forms.Label();
            this.lbTotalAmount = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.listView4 = new System.Windows.Forms.ListView();
            this.lbForm = new System.Windows.Forms.Label();
            this.listView3 = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lbMemberName = new System.Windows.Forms.Label();
            this.deftsoftTextbox1 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox2 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox3 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox4 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox5 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox6 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.textMember = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.btnAddItem = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnPay = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnCancel = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.btnReadIDCard = new BJCBCPOS.OtherServices.UserControls.DeftsoftButton();
            this.deftsoftTextbox8 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox9 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox10 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox11 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox12 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            this.deftsoftTextbox13 = new BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.grdBillDetail)).BeginInit();
            this.grpService.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbIDCard
            // 
            this.lbIDCard.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbIDCard.AutoSize = true;
            this.lbIDCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIDCard.Location = new System.Drawing.Point(674, 371);
            this.lbIDCard.Name = "lbIDCard";
            this.lbIDCard.Size = new System.Drawing.Size(73, 16);
            this.lbIDCard.TabIndex = 94;
            this.lbIDCard.Text = "บัตรประชาชน";
            // 
            // lbTelNo
            // 
            this.lbTelNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTelNo.AutoSize = true;
            this.lbTelNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTelNo.Location = new System.Drawing.Point(371, 410);
            this.lbTelNo.Name = "lbTelNo";
            this.lbTelNo.Size = new System.Drawing.Size(99, 16);
            this.lbTelNo.TabIndex = 91;
            this.lbTelNo.Text = "เบอร์โทรศัพท์ติดต่อ";
            // 
            // lbAmountReceive
            // 
            this.lbAmountReceive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbAmountReceive.AutoSize = true;
            this.lbAmountReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmountReceive.Location = new System.Drawing.Point(367, 370);
            this.lbAmountReceive.Name = "lbAmountReceive";
            this.lbAmountReceive.Size = new System.Drawing.Size(103, 16);
            this.lbAmountReceive.TabIndex = 89;
            this.lbAmountReceive.Text = " จำนวนเงินที่รับชำระ";
            // 
            // lbAddress3
            // 
            this.lbAddress3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbAddress3.AutoSize = true;
            this.lbAddress3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAddress3.Location = new System.Drawing.Point(709, 331);
            this.lbAddress3.Name = "lbAddress3";
            this.lbAddress3.Size = new System.Drawing.Size(38, 16);
            this.lbAddress3.TabIndex = 87;
            this.lbAddress3.Text = "ที่อยู่ 3";
            // 
            // lbAddress2
            // 
            this.lbAddress2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbAddress2.AutoSize = true;
            this.lbAddress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAddress2.Location = new System.Drawing.Point(709, 294);
            this.lbAddress2.Name = "lbAddress2";
            this.lbAddress2.Size = new System.Drawing.Size(38, 16);
            this.lbAddress2.TabIndex = 86;
            this.lbAddress2.Text = "ที่อยู่ 2";
            // 
            // lbAddress1
            // 
            this.lbAddress1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbAddress1.AutoSize = true;
            this.lbAddress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAddress1.Location = new System.Drawing.Point(709, 252);
            this.lbAddress1.Name = "lbAddress1";
            this.lbAddress1.Size = new System.Drawing.Size(38, 16);
            this.lbAddress1.TabIndex = 85;
            this.lbAddress1.Text = "ที่อยู่ 1";
            // 
            // lbVendor
            // 
            this.lbVendor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbVendor.AutoSize = true;
            this.lbVendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVendor.Location = new System.Drawing.Point(695, 216);
            this.lbVendor.Name = "lbVendor";
            this.lbVendor.Size = new System.Drawing.Size(52, 16);
            this.lbVendor.TabIndex = 84;
            this.lbVendor.Text = "ผู้รับชำระ";
            // 
            // lbRef4
            // 
            this.lbRef4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbRef4.AutoSize = true;
            this.lbRef4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRef4.Location = new System.Drawing.Point(338, 333);
            this.lbRef4.Name = "lbRef4";
            this.lbRef4.Size = new System.Drawing.Size(89, 16);
            this.lbRef4.TabIndex = 79;
            this.lbRef4.Text = "หมายเลขอ้างอิง 4";
            // 
            // lbRef3
            // 
            this.lbRef3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbRef3.AutoSize = true;
            this.lbRef3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRef3.Location = new System.Drawing.Point(338, 294);
            this.lbRef3.Name = "lbRef3";
            this.lbRef3.Size = new System.Drawing.Size(89, 16);
            this.lbRef3.TabIndex = 77;
            this.lbRef3.Text = "หมายเลขอ้างอิง 3";
            // 
            // lbRef2
            // 
            this.lbRef2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbRef2.AutoSize = true;
            this.lbRef2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRef2.Location = new System.Drawing.Point(338, 256);
            this.lbRef2.Name = "lbRef2";
            this.lbRef2.Size = new System.Drawing.Size(89, 16);
            this.lbRef2.TabIndex = 75;
            this.lbRef2.Text = "หมายเลขอ้างอิง 2";
            // 
            // lbRef1
            // 
            this.lbRef1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbRef1.AutoSize = true;
            this.lbRef1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRef1.Location = new System.Drawing.Point(338, 218);
            this.lbRef1.Name = "lbRef1";
            this.lbRef1.Size = new System.Drawing.Size(89, 16);
            this.lbRef1.TabIndex = 73;
            this.lbRef1.Text = "หมายเลขอ้างอิง 1";
            // 
            // listView6
            // 
            this.listView6.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView6.BackColor = System.Drawing.Color.White;
            this.listView6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.listView6.HideSelection = false;
            this.listView6.Location = new System.Drawing.Point(329, 193);
            this.listView6.Name = "listView6";
            this.listView6.Size = new System.Drawing.Size(683, 224);
            this.listView6.TabIndex = 71;
            this.listView6.UseCompatibleStateImageBehavior = false;
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
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
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
            // grdBillDetail
            // 
            this.grdBillDetail.AllowUserToAddRows = false;
            this.grdBillDetail.AllowUserToDeleteRows = false;
            this.grdBillDetail.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.grdBillDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBillDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serviceno,
            this.servicename,
            this.cust_name,
            this.fee_amount,
            this.amount});
            this.grdBillDetail.GridColor = System.Drawing.Color.WhiteSmoke;
            this.grdBillDetail.Location = new System.Drawing.Point(323, 565);
            this.grdBillDetail.Name = "grdBillDetail";
            this.grdBillDetail.RowHeadersVisible = false;
            this.grdBillDetail.Size = new System.Drawing.Size(689, 154);
            this.grdBillDetail.TabIndex = 70;
            // 
            // serviceno
            // 
            this.serviceno.HeaderText = "ลำดับที่";
            this.serviceno.Name = "serviceno";
            this.serviceno.Width = 80;
            // 
            // servicename
            // 
            this.servicename.HeaderText = "บริการ";
            this.servicename.Name = "servicename";
            this.servicename.Width = 200;
            // 
            // cust_name
            // 
            this.cust_name.HeaderText = "ชื่อ";
            this.cust_name.Name = "cust_name";
            this.cust_name.Width = 200;
            // 
            // fee_amount
            // 
            this.fee_amount.HeaderText = "ค่าบริการ";
            this.fee_amount.Name = "fee_amount";
            this.fee_amount.Width = 90;
            // 
            // amount
            // 
            this.amount.HeaderText = "ยอดชำระ";
            this.amount.Name = "amount";
            this.amount.Width = 110;
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
            this.grpService.TabIndex = 66;
            this.grpService.TabStop = false;
            this.grpService.Text = "รับฝากชำระ";
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
            this.btnBigService.Location = new System.Drawing.Point(41, 588);
            this.btnBigService.Name = "btnBigService";
            this.btnBigService.Size = new System.Drawing.Size(136, 59);
            this.btnBigService.TabIndex = 108;
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
            this.btnBackMenu.Location = new System.Drawing.Point(183, 588);
            this.btnBackMenu.Name = "btnBackMenu";
            this.btnBackMenu.Size = new System.Drawing.Size(122, 59);
            this.btnBackMenu.TabIndex = 107;
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
            // lbMember
            // 
            this.lbMember.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMember.AutoSize = true;
            this.lbMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMember.Location = new System.Drawing.Point(557, 85);
            this.lbMember.Name = "lbMember";
            this.lbMember.Size = new System.Drawing.Size(202, 18);
            this.lbMember.TabIndex = 65;
            this.lbMember.Text = "หมายเลขสมาชิก/เบอร์โทรศัพท์";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(490, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 18);
            this.label10.TabIndex = 64;
            this.label10.Text = "0.00";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(490, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 18);
            this.label9.TabIndex = 63;
            this.label9.Text = "5.00";
            // 
            // lbVat
            // 
            this.lbVat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbVat.AutoSize = true;
            this.lbVat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVat.Location = new System.Drawing.Point(357, 167);
            this.lbVat.Name = "lbVat";
            this.lbVat.Size = new System.Drawing.Size(130, 18);
            this.lbVat.TabIndex = 62;
            this.lbVat.Text = "ภาษีมูลค่าเพิ่ม(7%)";
            // 
            // lbServiceCharge
            // 
            this.lbServiceCharge.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbServiceCharge.AutoSize = true;
            this.lbServiceCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbServiceCharge.Location = new System.Drawing.Point(418, 131);
            this.lbServiceCharge.Name = "lbServiceCharge";
            this.lbServiceCharge.Size = new System.Drawing.Size(66, 18);
            this.lbServiceCharge.TabIndex = 61;
            this.lbServiceCharge.Text = "ค่าบริการ";
            // 
            // lbCurrency
            // 
            this.lbCurrency.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbCurrency.AutoSize = true;
            this.lbCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrency.Location = new System.Drawing.Point(919, 167);
            this.lbCurrency.Name = "lbCurrency";
            this.lbCurrency.Size = new System.Drawing.Size(35, 18);
            this.lbCurrency.TabIndex = 60;
            this.lbCurrency.Text = "บาท";
            // 
            // lbTotalAmount
            // 
            this.lbTotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTotalAmount.AutoSize = true;
            this.lbTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalAmount.ForeColor = System.Drawing.Color.Chartreuse;
            this.lbTotalAmount.Location = new System.Drawing.Point(766, 162);
            this.lbTotalAmount.Name = "lbTotalAmount";
            this.lbTotalAmount.Size = new System.Drawing.Size(104, 25);
            this.lbTotalAmount.TabIndex = 59;
            this.lbTotalAmount.Text = "2,000.00";
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(698, 167);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(61, 18);
            this.lbTotal.TabIndex = 58;
            this.lbTotal.Text = "ยอดรวม";
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
            this.listView4.Size = new System.Drawing.Size(683, 73);
            this.listView4.TabIndex = 55;
            this.listView4.UseCompatibleStateImageBehavior = false;
            // 
            // lbForm
            // 
            this.lbForm.AutoSize = true;
            this.lbForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbForm.Location = new System.Drawing.Point(341, 79);
            this.lbForm.Name = "lbForm";
            this.lbForm.Size = new System.Drawing.Size(143, 25);
            this.lbForm.TabIndex = 53;
            this.lbForm.Text = "Bill Payment";
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
            this.listView3.Size = new System.Drawing.Size(683, 428);
            this.listView3.TabIndex = 50;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "นายทองดี นามสมมุติ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "สมาชิก : ";
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(768, 131);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(132, 18);
            this.label24.TabIndex = 96;
            this.label24.Text = "นายบุดดี นามสมมุติ";
            // 
            // lbMemberName
            // 
            this.lbMemberName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMemberName.AutoSize = true;
            this.lbMemberName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMemberName.Location = new System.Drawing.Point(707, 131);
            this.lbMemberName.Name = "lbMemberName";
            this.lbMemberName.Size = new System.Drawing.Size(52, 18);
            this.lbMemberName.TabIndex = 95;
            this.lbMemberName.Text = "สมาชิก";
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
            this.deftsoftTextbox1.Location = new System.Drawing.Point(433, 206);
            this.deftsoftTextbox1.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox1.Multiline = false;
            this.deftsoftTextbox1.Name = "deftsoftTextbox1";
            this.deftsoftTextbox1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox1.PasswordChar = false;
            this.deftsoftTextbox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox1.PlaceholderText = "";
            this.deftsoftTextbox1.Size = new System.Drawing.Size(212, 34);
            this.deftsoftTextbox1.TabIndex = 97;
            this.deftsoftTextbox1.Texts = "";
            this.deftsoftTextbox1.UnderlinedStyle = false;
            // 
            // deftsoftTextbox2
            // 
            this.deftsoftTextbox2.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox2.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox2.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox2.BorderRadius = 8;
            this.deftsoftTextbox2.BorderSize = 2;
            this.deftsoftTextbox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox2.Location = new System.Drawing.Point(433, 245);
            this.deftsoftTextbox2.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox2.Multiline = false;
            this.deftsoftTextbox2.Name = "deftsoftTextbox2";
            this.deftsoftTextbox2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox2.PasswordChar = false;
            this.deftsoftTextbox2.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox2.PlaceholderText = "";
            this.deftsoftTextbox2.Size = new System.Drawing.Size(212, 34);
            this.deftsoftTextbox2.TabIndex = 98;
            this.deftsoftTextbox2.Texts = "";
            this.deftsoftTextbox2.UnderlinedStyle = false;
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
            this.deftsoftTextbox3.Location = new System.Drawing.Point(434, 284);
            this.deftsoftTextbox3.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox3.Multiline = false;
            this.deftsoftTextbox3.Name = "deftsoftTextbox3";
            this.deftsoftTextbox3.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox3.PasswordChar = false;
            this.deftsoftTextbox3.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox3.PlaceholderText = "";
            this.deftsoftTextbox3.Size = new System.Drawing.Size(212, 34);
            this.deftsoftTextbox3.TabIndex = 99;
            this.deftsoftTextbox3.Texts = "";
            this.deftsoftTextbox3.UnderlinedStyle = false;
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
            this.deftsoftTextbox4.Location = new System.Drawing.Point(434, 323);
            this.deftsoftTextbox4.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox4.Multiline = false;
            this.deftsoftTextbox4.Name = "deftsoftTextbox4";
            this.deftsoftTextbox4.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox4.PasswordChar = false;
            this.deftsoftTextbox4.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox4.PlaceholderText = "";
            this.deftsoftTextbox4.Size = new System.Drawing.Size(212, 34);
            this.deftsoftTextbox4.TabIndex = 100;
            this.deftsoftTextbox4.Texts = "";
            this.deftsoftTextbox4.UnderlinedStyle = false;
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
            this.deftsoftTextbox5.Location = new System.Drawing.Point(476, 361);
            this.deftsoftTextbox5.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox5.Multiline = false;
            this.deftsoftTextbox5.Name = "deftsoftTextbox5";
            this.deftsoftTextbox5.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox5.PasswordChar = false;
            this.deftsoftTextbox5.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox5.PlaceholderText = "";
            this.deftsoftTextbox5.Size = new System.Drawing.Size(169, 34);
            this.deftsoftTextbox5.TabIndex = 101;
            this.deftsoftTextbox5.Texts = "";
            this.deftsoftTextbox5.UnderlinedStyle = false;
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
            this.deftsoftTextbox6.Location = new System.Drawing.Point(477, 399);
            this.deftsoftTextbox6.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox6.Multiline = false;
            this.deftsoftTextbox6.Name = "deftsoftTextbox6";
            this.deftsoftTextbox6.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox6.PasswordChar = false;
            this.deftsoftTextbox6.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox6.PlaceholderText = "";
            this.deftsoftTextbox6.Size = new System.Drawing.Size(169, 34);
            this.deftsoftTextbox6.TabIndex = 102;
            this.deftsoftTextbox6.Texts = "";
            this.deftsoftTextbox6.UnderlinedStyle = false;
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
            this.textMember.Location = new System.Drawing.Point(766, 79);
            this.textMember.Margin = new System.Windows.Forms.Padding(4);
            this.textMember.Multiline = false;
            this.textMember.Name = "textMember";
            this.textMember.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.textMember.PasswordChar = false;
            this.textMember.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textMember.PlaceholderText = "";
            this.textMember.Size = new System.Drawing.Size(241, 34);
            this.textMember.TabIndex = 103;
            this.textMember.Texts = "";
            this.textMember.UnderlinedStyle = false;
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
            this.btnAddItem.Location = new System.Drawing.Point(329, 500);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(226, 59);
            this.btnAddItem.TabIndex = 104;
            this.btnAddItem.Text = "เพิ่มรายการ";
            this.btnAddItem.TextColor = System.Drawing.Color.White;
            this.btnAddItem.UseVisualStyleBackColor = false;
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnPay.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnPay.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPay.BorderRadius = 8;
            this.btnPay.BorderSize = 1;
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.IconColor = System.Drawing.Color.White;
            this.btnPay.IconType = BJCBCPOS.OtherServices.Fonts.MaterialDesignIcons.CheckCircleOutline;
            this.btnPay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPay.Location = new System.Drawing.Point(560, 500);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(245, 59);
            this.btnPay.TabIndex = 105;
            this.btnPay.Text = "ชำระเงิน";
            this.btnPay.TextColor = System.Drawing.Color.White;
            this.btnPay.UseVisualStyleBackColor = false;
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
            this.btnCancel.Location = new System.Drawing.Point(811, 500);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 59);
            this.btnCancel.TabIndex = 106;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextColor = System.Drawing.Color.White;
            this.btnCancel.UseVisualStyleBackColor = false;
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
            this.btnReadIDCard.Location = new System.Drawing.Point(801, 398);
            this.btnReadIDCard.Name = "btnReadIDCard";
            this.btnReadIDCard.Size = new System.Drawing.Size(206, 43);
            this.btnReadIDCard.TabIndex = 107;
            this.btnReadIDCard.Text = "อ่านบัตรประชาชน";
            this.btnReadIDCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReadIDCard.TextColor = System.Drawing.Color.White;
            this.btnReadIDCard.UseVisualStyleBackColor = false;
            // 
            // deftsoftTextbox8
            // 
            this.deftsoftTextbox8.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox8.BorderColor = System.Drawing.Color.LightGray;
            this.deftsoftTextbox8.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox8.BorderRadius = 8;
            this.deftsoftTextbox8.BorderSize = 2;
            this.deftsoftTextbox8.Enabled = false;
            this.deftsoftTextbox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox8.Location = new System.Drawing.Point(754, 206);
            this.deftsoftTextbox8.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox8.Multiline = false;
            this.deftsoftTextbox8.Name = "deftsoftTextbox8";
            this.deftsoftTextbox8.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox8.PasswordChar = false;
            this.deftsoftTextbox8.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox8.PlaceholderText = "";
            this.deftsoftTextbox8.Size = new System.Drawing.Size(75, 34);
            this.deftsoftTextbox8.TabIndex = 108;
            this.deftsoftTextbox8.Texts = "";
            this.deftsoftTextbox8.UnderlinedStyle = false;
            // 
            // deftsoftTextbox9
            // 
            this.deftsoftTextbox9.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox9.BorderColor = System.Drawing.Color.LightGray;
            this.deftsoftTextbox9.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox9.BorderRadius = 8;
            this.deftsoftTextbox9.BorderSize = 2;
            this.deftsoftTextbox9.Enabled = false;
            this.deftsoftTextbox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox9.Location = new System.Drawing.Point(829, 207);
            this.deftsoftTextbox9.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox9.Multiline = false;
            this.deftsoftTextbox9.Name = "deftsoftTextbox9";
            this.deftsoftTextbox9.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox9.PasswordChar = false;
            this.deftsoftTextbox9.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox9.PlaceholderText = "";
            this.deftsoftTextbox9.Size = new System.Drawing.Size(182, 34);
            this.deftsoftTextbox9.TabIndex = 109;
            this.deftsoftTextbox9.Texts = "";
            this.deftsoftTextbox9.UnderlinedStyle = false;
            // 
            // deftsoftTextbox10
            // 
            this.deftsoftTextbox10.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox10.BorderColor = System.Drawing.Color.LightGray;
            this.deftsoftTextbox10.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox10.BorderRadius = 8;
            this.deftsoftTextbox10.BorderSize = 2;
            this.deftsoftTextbox10.Enabled = false;
            this.deftsoftTextbox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox10.Location = new System.Drawing.Point(754, 245);
            this.deftsoftTextbox10.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox10.Multiline = false;
            this.deftsoftTextbox10.Name = "deftsoftTextbox10";
            this.deftsoftTextbox10.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox10.PasswordChar = false;
            this.deftsoftTextbox10.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox10.PlaceholderText = "";
            this.deftsoftTextbox10.Size = new System.Drawing.Size(257, 34);
            this.deftsoftTextbox10.TabIndex = 110;
            this.deftsoftTextbox10.Texts = "";
            this.deftsoftTextbox10.UnderlinedStyle = false;
            // 
            // deftsoftTextbox11
            // 
            this.deftsoftTextbox11.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox11.BorderColor = System.Drawing.Color.LightGray;
            this.deftsoftTextbox11.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox11.BorderRadius = 8;
            this.deftsoftTextbox11.BorderSize = 2;
            this.deftsoftTextbox11.Enabled = false;
            this.deftsoftTextbox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox11.Location = new System.Drawing.Point(755, 284);
            this.deftsoftTextbox11.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox11.Multiline = false;
            this.deftsoftTextbox11.Name = "deftsoftTextbox11";
            this.deftsoftTextbox11.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox11.PasswordChar = false;
            this.deftsoftTextbox11.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox11.PlaceholderText = "";
            this.deftsoftTextbox11.Size = new System.Drawing.Size(257, 34);
            this.deftsoftTextbox11.TabIndex = 111;
            this.deftsoftTextbox11.Texts = "";
            this.deftsoftTextbox11.UnderlinedStyle = false;
            // 
            // deftsoftTextbox12
            // 
            this.deftsoftTextbox12.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox12.BorderColor = System.Drawing.Color.LightGray;
            this.deftsoftTextbox12.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox12.BorderRadius = 8;
            this.deftsoftTextbox12.BorderSize = 2;
            this.deftsoftTextbox12.Enabled = false;
            this.deftsoftTextbox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox12.Location = new System.Drawing.Point(755, 323);
            this.deftsoftTextbox12.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox12.Multiline = false;
            this.deftsoftTextbox12.Name = "deftsoftTextbox12";
            this.deftsoftTextbox12.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox12.PasswordChar = false;
            this.deftsoftTextbox12.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox12.PlaceholderText = "";
            this.deftsoftTextbox12.Size = new System.Drawing.Size(257, 34);
            this.deftsoftTextbox12.TabIndex = 112;
            this.deftsoftTextbox12.Texts = "";
            this.deftsoftTextbox12.UnderlinedStyle = false;
            // 
            // deftsoftTextbox13
            // 
            this.deftsoftTextbox13.BackColor = System.Drawing.SystemColors.Window;
            this.deftsoftTextbox13.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.deftsoftTextbox13.BorderFocusColor = System.Drawing.Color.HotPink;
            this.deftsoftTextbox13.BorderRadius = 8;
            this.deftsoftTextbox13.BorderSize = 2;
            this.deftsoftTextbox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deftsoftTextbox13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deftsoftTextbox13.Location = new System.Drawing.Point(754, 361);
            this.deftsoftTextbox13.Margin = new System.Windows.Forms.Padding(4);
            this.deftsoftTextbox13.Multiline = false;
            this.deftsoftTextbox13.Name = "deftsoftTextbox13";
            this.deftsoftTextbox13.Padding = new System.Windows.Forms.Padding(10, 10, 10, 7);
            this.deftsoftTextbox13.PasswordChar = false;
            this.deftsoftTextbox13.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.deftsoftTextbox13.PlaceholderText = "";
            this.deftsoftTextbox13.Size = new System.Drawing.Size(257, 34);
            this.deftsoftTextbox13.TabIndex = 113;
            this.deftsoftTextbox13.Texts = "";
            this.deftsoftTextbox13.UnderlinedStyle = false;
            // 
            // BillPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.deftsoftTextbox13);
            this.Controls.Add(this.deftsoftTextbox12);
            this.Controls.Add(this.deftsoftTextbox11);
            this.Controls.Add(this.deftsoftTextbox10);
            this.Controls.Add(this.deftsoftTextbox9);
            this.Controls.Add(this.deftsoftTextbox8);
            this.Controls.Add(this.btnReadIDCard);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textMember);
            this.Controls.Add(this.deftsoftTextbox6);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.deftsoftTextbox5);
            this.Controls.Add(this.deftsoftTextbox4);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.deftsoftTextbox3);
            this.Controls.Add(this.deftsoftTextbox2);
            this.Controls.Add(this.deftsoftTextbox1);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lbMemberName);
            this.Controls.Add(this.lbIDCard);
            this.Controls.Add(this.lbTelNo);
            this.Controls.Add(this.lbAmountReceive);
            this.Controls.Add(this.lbAddress3);
            this.Controls.Add(this.lbAddress2);
            this.Controls.Add(this.lbAddress1);
            this.Controls.Add(this.lbVendor);
            this.Controls.Add(this.lbRef4);
            this.Controls.Add(this.lbRef3);
            this.Controls.Add(this.lbRef2);
            this.Controls.Add(this.lbRef1);
            this.Controls.Add(this.listView6);
            this.Controls.Add(this.grdBillDetail);
            this.Controls.Add(this.grpService);
            this.Controls.Add(this.lbMember);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbVat);
            this.Controls.Add(this.lbServiceCharge);
            this.Controls.Add(this.lbCurrency);
            this.Controls.Add(this.lbTotalAmount);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.listView4);
            this.Controls.Add(this.lbForm);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BillPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BillPayment";
            this.Load += new System.EventHandler(this.BillPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBillDetail)).EndInit();
            this.grpService.ResumeLayout(false);
            this.grpService.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbIDCard;
    private System.Windows.Forms.Label lbTelNo;
    private System.Windows.Forms.Label lbAmountReceive;
    private System.Windows.Forms.Label lbAddress3;
    private System.Windows.Forms.Label lbAddress2;
    private System.Windows.Forms.Label lbAddress1;
    private System.Windows.Forms.Label lbVendor;
    private System.Windows.Forms.Label lbRef4;
    private System.Windows.Forms.Label lbRef3;
    private System.Windows.Forms.Label lbRef2;
    private System.Windows.Forms.Label lbRef1;
    private System.Windows.Forms.ListView listView6;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.TextBox textMenuID;
    private System.Windows.Forms.TextBox textMenuName;
    private System.Windows.Forms.DataGridView grdBillDetail;
    private System.Windows.Forms.GroupBox grpService;
    private System.Windows.Forms.ListView listView5;
    private System.Windows.Forms.Label lbMember;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label lbVat;
    private System.Windows.Forms.Label lbServiceCharge;
    private System.Windows.Forms.Label lbCurrency;
    private System.Windows.Forms.Label lbTotalAmount;
    private System.Windows.Forms.Label lbTotal;
    private System.Windows.Forms.ListView listView4;
    private System.Windows.Forms.Label lbForm;
    private System.Windows.Forms.ListView listView3;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lbMemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn servicename;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn fee_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox1;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox2;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox3;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox4;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox5;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox6;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox textMember;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnAddItem;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnPay;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnCancel;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnBigService;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnBackMenu;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftButton btnReadIDCard;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox8;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox9;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox10;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox11;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox12;
        private BJCBCPOS.OtherServices.UserControls.DeftsoftTextbox deftsoftTextbox13;
    }
}
