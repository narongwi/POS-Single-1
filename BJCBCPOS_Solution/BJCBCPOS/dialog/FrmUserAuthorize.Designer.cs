namespace BJCBCPOS
{
    partial class frmUserAuthorize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserAuthorize));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucTBWIEmpPass = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucTBWIEmpUser = new BJCBCPOS.UCTextBoxWithIcon();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbMainPassword = new System.Windows.Forms.Label();
            this.lbMainHeader = new System.Windows.Forms.Label();
            this.lbMainUser = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbSpecialProduct = new System.Windows.Forms.Label();
            this.lbPrintExport = new System.Windows.Forms.Label();
            this.lbRedeem = new System.Windows.Forms.Label();
            this.lbLogin = new System.Windows.Forms.Label();
            this.lbReport = new System.Windows.Forms.Label();
            this.lbCashOut = new System.Windows.Forms.Label();
            this.lbCancelSale = new System.Windows.Forms.Label();
            this.lbEditItem = new System.Windows.Forms.Label();
            this.lbDeleteItem = new System.Windows.Forms.Label();
            this.lbVoidSuccess = new System.Windows.Forms.Label();
            this.lbReturnSuccess = new System.Windows.Forms.Label();
            this.lbConfirmPayment = new System.Windows.Forms.Label();
            this.lbCancelCashOut = new System.Windows.Forms.Label();
            this.lbReportReceipt = new System.Windows.Forms.Label();
            this.lbReportDaySale = new System.Windows.Forms.Label();
            this.lbCheckProduct = new System.Windows.Forms.Label();
            this.lbChangePassword = new System.Windows.Forms.Label();
            this.lbEndofTill = new System.Windows.Forms.Label();
            this.lbEndofShift = new System.Windows.Forms.Label();
            this.lbVoid = new System.Windows.Forms.Label();
            this.lbReturnProduct = new System.Windows.Forms.Label();
            this.lbReturnReceipt = new System.Windows.Forms.Label();
            this.lbSale = new System.Windows.Forms.Label();
            this.lbCashIn = new System.Windows.Forms.Label();
            this.lbOpenDay = new System.Windows.Forms.Label();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.lbMessage = new System.Windows.Forms.Label();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucKeypad = new BJCBCPOS.UCKeypad();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            this.lbUserVal = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ucTBWIEmpPass);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.lbMainPassword);
            this.panel1.Controls.Add(this.lbMainHeader);
            this.panel1.Controls.Add(this.lbMainUser);
            this.panel1.Controls.Add(this.lbUser);
            this.panel1.Controls.Add(this.ucTBWIEmpUser);
            this.panel1.Controls.Add(this.lbUserVal);
            this.panel1.Location = new System.Drawing.Point(245, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 384);
            this.panel1.TabIndex = 7;
            // 
            // ucTBWIEmpPass
            // 
            this.ucTBWIEmpPass.BackColor = System.Drawing.Color.White;
            this.ucTBWIEmpPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTBWIEmpPass.BackgroundImage")));
            this.ucTBWIEmpPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTBWIEmpPass.EnabledUC = true;
            this.ucTBWIEmpPass.IsAmount = false;
            this.ucTBWIEmpPass.IsLarge = false;
            this.ucTBWIEmpPass.IsNumber = false;
            this.ucTBWIEmpPass.IsSetFormat = false;
            this.ucTBWIEmpPass.Location = new System.Drawing.Point(161, 190);
            this.ucTBWIEmpPass.MaxLength = 32767;
            this.ucTBWIEmpPass.Name = "ucTBWIEmpPass";
            this.ucTBWIEmpPass.PasswordChar = true;
            this.ucTBWIEmpPass.placeHolder = "Password";
            this.ucTBWIEmpPass.Readonly = false;
            this.ucTBWIEmpPass.ShortcutsEnabled = true;
            this.ucTBWIEmpPass.Size = new System.Drawing.Size(308, 42);
            this.ucTBWIEmpPass.TabIndex = 17;
            this.ucTBWIEmpPass.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTBWIEmpPass.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTBWIEmpPass.TextBoxKeydown += new System.EventHandler(this.ucTBWIEmpPass_TextBoxKeydown);
            this.ucTBWIEmpPass.Enter += new System.EventHandler(this.ucTBWIEmpPass_Enter);
            // 
            // ucTBWIEmpUser
            // 
            this.ucTBWIEmpUser.BackColor = System.Drawing.Color.White;
            this.ucTBWIEmpUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTBWIEmpUser.BackgroundImage")));
            this.ucTBWIEmpUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTBWIEmpUser.EnabledUC = true;
            this.ucTBWIEmpUser.IsAmount = false;
            this.ucTBWIEmpUser.IsLarge = false;
            this.ucTBWIEmpUser.IsNumber = false;
            this.ucTBWIEmpUser.IsSetFormat = false;
            this.ucTBWIEmpUser.Location = new System.Drawing.Point(161, 109);
            this.ucTBWIEmpUser.MaxLength = 32767;
            this.ucTBWIEmpUser.Name = "ucTBWIEmpUser";
            this.ucTBWIEmpUser.PasswordChar = false;
            this.ucTBWIEmpUser.placeHolder = "Username";
            this.ucTBWIEmpUser.Readonly = false;
            this.ucTBWIEmpUser.ShortcutsEnabled = true;
            this.ucTBWIEmpUser.Size = new System.Drawing.Size(308, 42);
            this.ucTBWIEmpUser.TabIndex = 16;
            this.ucTBWIEmpUser.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTBWIEmpUser.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucTBWIEmpUser.TextBoxKeydown += new System.EventHandler(this.ucTBWIEmpUser_TextBoxKeydown);
            this.ucTBWIEmpUser.Enter += new System.EventHandler(this.ucTBWIEmp_Enter);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel1;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(47, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 62);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(289, 283);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(180, 62);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lbMainPassword
            // 
            this.lbMainPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbMainPassword.Location = new System.Drawing.Point(8, 191);
            this.lbMainPassword.Name = "lbMainPassword";
            this.lbMainPassword.Size = new System.Drawing.Size(143, 41);
            this.lbMainPassword.TabIndex = 13;
            this.lbMainPassword.Text = "รหัสผ่าน";
            this.lbMainPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbMainHeader
            // 
            this.lbMainHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbMainHeader.Location = new System.Drawing.Point(12, 28);
            this.lbMainHeader.Name = "lbMainHeader";
            this.lbMainHeader.Size = new System.Drawing.Size(488, 51);
            this.lbMainHeader.TabIndex = 11;
            this.lbMainHeader.Text = "กรุณากรอกข้อมูลเพื่อยืนยัน";
            this.lbMainHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMainUser
            // 
            this.lbMainUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbMainUser.Location = new System.Drawing.Point(8, 109);
            this.lbMainUser.Name = "lbMainUser";
            this.lbMainUser.Size = new System.Drawing.Size(143, 41);
            this.lbMainUser.TabIndex = 12;
            this.lbMainUser.Text = "ผู้อนุมัติ";
            this.lbMainUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbUser
            // 
            this.lbUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbUser.Location = new System.Drawing.Point(8, 109);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(143, 41);
            this.lbUser.TabIndex = 18;
            this.lbUser.Text = "ผู้ใช้งาน";
            this.lbUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.splitContainer1.Panel1.Controls.Add(this.lbSpecialProduct);
            this.splitContainer1.Panel1.Controls.Add(this.lbPrintExport);
            this.splitContainer1.Panel1.Controls.Add(this.lbRedeem);
            this.splitContainer1.Panel1.Controls.Add(this.lbLogin);
            this.splitContainer1.Panel1.Controls.Add(this.lbReport);
            this.splitContainer1.Panel1.Controls.Add(this.lbCashOut);
            this.splitContainer1.Panel1.Controls.Add(this.lbCancelSale);
            this.splitContainer1.Panel1.Controls.Add(this.lbEditItem);
            this.splitContainer1.Panel1.Controls.Add(this.lbDeleteItem);
            this.splitContainer1.Panel1.Controls.Add(this.lbVoidSuccess);
            this.splitContainer1.Panel1.Controls.Add(this.lbReturnSuccess);
            this.splitContainer1.Panel1.Controls.Add(this.lbConfirmPayment);
            this.splitContainer1.Panel1.Controls.Add(this.lbCancelCashOut);
            this.splitContainer1.Panel1.Controls.Add(this.lbReportReceipt);
            this.splitContainer1.Panel1.Controls.Add(this.lbReportDaySale);
            this.splitContainer1.Panel1.Controls.Add(this.lbCheckProduct);
            this.splitContainer1.Panel1.Controls.Add(this.lbChangePassword);
            this.splitContainer1.Panel1.Controls.Add(this.lbEndofTill);
            this.splitContainer1.Panel1.Controls.Add(this.lbEndofShift);
            this.splitContainer1.Panel1.Controls.Add(this.lbVoid);
            this.splitContainer1.Panel1.Controls.Add(this.lbReturnProduct);
            this.splitContainer1.Panel1.Controls.Add(this.lbReturnReceipt);
            this.splitContainer1.Panel1.Controls.Add(this.lbSale);
            this.splitContainer1.Panel1.Controls.Add(this.lbCashIn);
            this.splitContainer1.Panel1.Controls.Add(this.lbOpenDay);
            this.splitContainer1.Panel1.Controls.Add(this.ucFooter1);
            this.splitContainer1.Panel1.Controls.Add(this.lbMessage);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeader1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucKeypad);
            this.splitContainer1.Panel2.Controls.Add(this.ucKeyboard1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Panel2MinSize = 10;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 8;
            // 
            // lbSpecialProduct
            // 
            this.lbSpecialProduct.AutoSize = true;
            this.lbSpecialProduct.Location = new System.Drawing.Point(2, 0);
            this.lbSpecialProduct.Name = "lbSpecialProduct";
            this.lbSpecialProduct.Size = new System.Drawing.Size(87, 13);
            this.lbSpecialProduct.TabIndex = 35;
            this.lbSpecialProduct.Text = "lbSpecialProduct";
            this.lbSpecialProduct.Visible = false;
            // 
            // lbPrintExport
            // 
            this.lbPrintExport.AutoSize = true;
            this.lbPrintExport.Location = new System.Drawing.Point(0, 0);
            this.lbPrintExport.Name = "lbPrintExport";
            this.lbPrintExport.Size = new System.Drawing.Size(66, 13);
            this.lbPrintExport.TabIndex = 34;
            this.lbPrintExport.Text = "lbPrintExport";
            this.lbPrintExport.Visible = false;
            // 
            // lbRedeem
            // 
            this.lbRedeem.AutoSize = true;
            this.lbRedeem.Location = new System.Drawing.Point(0, 0);
            this.lbRedeem.Name = "lbRedeem";
            this.lbRedeem.Size = new System.Drawing.Size(88, 13);
            this.lbRedeem.TabIndex = 33;
            this.lbRedeem.Text = "แลกคะแนนสะสม";
            this.lbRedeem.Visible = false;
            // 
            // lbLogin
            // 
            this.lbLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbLogin.AutoSize = true;
            this.lbLogin.Location = new System.Drawing.Point(0, 0);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(54, 13);
            this.lbLogin.TabIndex = 32;
            this.lbLogin.Text = "เข้าสู่ระบบ";
            this.lbLogin.Visible = false;
            // 
            // lbReport
            // 
            this.lbReport.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbReport.AutoSize = true;
            this.lbReport.Location = new System.Drawing.Point(0, 0);
            this.lbReport.Name = "lbReport";
            this.lbReport.Size = new System.Drawing.Size(43, 13);
            this.lbReport.TabIndex = 31;
            this.lbReport.Text = "รายงาน";
            this.lbReport.Visible = false;
            // 
            // lbCashOut
            // 
            this.lbCashOut.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbCashOut.AutoSize = true;
            this.lbCashOut.Location = new System.Drawing.Point(0, 0);
            this.lbCashOut.Name = "lbCashOut";
            this.lbCashOut.Size = new System.Drawing.Size(36, 13);
            this.lbCashOut.TabIndex = 30;
            this.lbCashOut.Text = "ส่งเงิน";
            this.lbCashOut.Visible = false;
            // 
            // lbCancelSale
            // 
            this.lbCancelSale.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbCancelSale.AutoSize = true;
            this.lbCancelSale.Location = new System.Drawing.Point(0, 0);
            this.lbCancelSale.Name = "lbCancelSale";
            this.lbCancelSale.Size = new System.Drawing.Size(75, 13);
            this.lbCancelSale.TabIndex = 29;
            this.lbCancelSale.Text = "ยกเลิกการขาย";
            this.lbCancelSale.Visible = false;
            // 
            // lbEditItem
            // 
            this.lbEditItem.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbEditItem.AutoSize = true;
            this.lbEditItem.Location = new System.Drawing.Point(0, 0);
            this.lbEditItem.Name = "lbEditItem";
            this.lbEditItem.Size = new System.Drawing.Size(90, 13);
            this.lbEditItem.TabIndex = 28;
            this.lbEditItem.Text = "แก้ไขราคา/หน่วย";
            this.lbEditItem.Visible = false;
            // 
            // lbDeleteItem
            // 
            this.lbDeleteItem.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbDeleteItem.AutoSize = true;
            this.lbDeleteItem.Location = new System.Drawing.Point(0, 0);
            this.lbDeleteItem.Name = "lbDeleteItem";
            this.lbDeleteItem.Size = new System.Drawing.Size(82, 13);
            this.lbDeleteItem.TabIndex = 27;
            this.lbDeleteItem.Text = "ลบรายการสินค้า";
            this.lbDeleteItem.Visible = false;
            // 
            // lbVoidSuccess
            // 
            this.lbVoidSuccess.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbVoidSuccess.AutoSize = true;
            this.lbVoidSuccess.Location = new System.Drawing.Point(0, 0);
            this.lbVoidSuccess.Name = "lbVoidSuccess";
            this.lbVoidSuccess.Size = new System.Drawing.Size(105, 13);
            this.lbVoidSuccess.TabIndex = 26;
            this.lbVoidSuccess.Text = "ยืนยันยกเลิกใบเสร็จ";
            this.lbVoidSuccess.Visible = false;
            // 
            // lbReturnSuccess
            // 
            this.lbReturnSuccess.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbReturnSuccess.AutoSize = true;
            this.lbReturnSuccess.Location = new System.Drawing.Point(0, 0);
            this.lbReturnSuccess.Name = "lbReturnSuccess";
            this.lbReturnSuccess.Size = new System.Drawing.Size(96, 13);
            this.lbReturnSuccess.TabIndex = 25;
            this.lbReturnSuccess.Text = "ยืนยันการคืนสินค้า";
            this.lbReturnSuccess.Visible = false;
            // 
            // lbConfirmPayment
            // 
            this.lbConfirmPayment.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbConfirmPayment.AutoSize = true;
            this.lbConfirmPayment.Location = new System.Drawing.Point(0, 0);
            this.lbConfirmPayment.Name = "lbConfirmPayment";
            this.lbConfirmPayment.Size = new System.Drawing.Size(98, 13);
            this.lbConfirmPayment.TabIndex = 24;
            this.lbConfirmPayment.Text = "ยืนยันยอดเงินชำระ";
            this.lbConfirmPayment.Visible = false;
            // 
            // lbCancelCashOut
            // 
            this.lbCancelCashOut.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbCancelCashOut.AutoSize = true;
            this.lbCancelCashOut.Location = new System.Drawing.Point(0, 0);
            this.lbCancelCashOut.Name = "lbCancelCashOut";
            this.lbCancelCashOut.Size = new System.Drawing.Size(119, 13);
            this.lbCancelCashOut.TabIndex = 23;
            this.lbCancelCashOut.Text = "ยกเลิกใบส่งเงินตามรอบ";
            this.lbCancelCashOut.Visible = false;
            // 
            // lbReportReceipt
            // 
            this.lbReportReceipt.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbReportReceipt.AutoSize = true;
            this.lbReportReceipt.Location = new System.Drawing.Point(0, 0);
            this.lbReportReceipt.Name = "lbReportReceipt";
            this.lbReportReceipt.Size = new System.Drawing.Size(68, 13);
            this.lbReportReceipt.TabIndex = 22;
            this.lbReportReceipt.Text = "ข้อมูลใบเสร็จ";
            this.lbReportReceipt.Visible = false;
            // 
            // lbReportDaySale
            // 
            this.lbReportDaySale.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbReportDaySale.AutoSize = true;
            this.lbReportDaySale.Location = new System.Drawing.Point(0, 0);
            this.lbReportDaySale.Name = "lbReportDaySale";
            this.lbReportDaySale.Size = new System.Drawing.Size(88, 13);
            this.lbReportDaySale.TabIndex = 21;
            this.lbReportDaySale.Text = "ยอดขายประจำวัน";
            this.lbReportDaySale.Visible = false;
            // 
            // lbCheckProduct
            // 
            this.lbCheckProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbCheckProduct.AutoSize = true;
            this.lbCheckProduct.Location = new System.Drawing.Point(0, 0);
            this.lbCheckProduct.Name = "lbCheckProduct";
            this.lbCheckProduct.Size = new System.Drawing.Size(77, 13);
            this.lbCheckProduct.TabIndex = 20;
            this.lbCheckProduct.Text = "ตรวจสอบสินค้า";
            this.lbCheckProduct.Visible = false;
            // 
            // lbChangePassword
            // 
            this.lbChangePassword.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbChangePassword.AutoSize = true;
            this.lbChangePassword.Location = new System.Drawing.Point(0, 0);
            this.lbChangePassword.Name = "lbChangePassword";
            this.lbChangePassword.Size = new System.Drawing.Size(79, 13);
            this.lbChangePassword.TabIndex = 19;
            this.lbChangePassword.Text = "เปลี่ยนรหัสผ่าน";
            this.lbChangePassword.Visible = false;
            // 
            // lbEndofTill
            // 
            this.lbEndofTill.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbEndofTill.AutoSize = true;
            this.lbEndofTill.Location = new System.Drawing.Point(0, 0);
            this.lbEndofTill.Name = "lbEndofTill";
            this.lbEndofTill.Size = new System.Drawing.Size(55, 13);
            this.lbEndofTill.TabIndex = 18;
            this.lbEndofTill.Text = "ปิดงาน Till";
            this.lbEndofTill.Visible = false;
            // 
            // lbEndofShift
            // 
            this.lbEndofShift.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbEndofShift.AutoSize = true;
            this.lbEndofShift.Location = new System.Drawing.Point(0, 0);
            this.lbEndofShift.Name = "lbEndofShift";
            this.lbEndofShift.Size = new System.Drawing.Size(86, 13);
            this.lbEndofShift.TabIndex = 17;
            this.lbEndofShift.Text = "ปิดงานแคชเชียร์";
            this.lbEndofShift.Visible = false;
            // 
            // lbVoid
            // 
            this.lbVoid.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbVoid.AutoSize = true;
            this.lbVoid.Location = new System.Drawing.Point(0, 0);
            this.lbVoid.Name = "lbVoid";
            this.lbVoid.Size = new System.Drawing.Size(39, 13);
            this.lbVoid.TabIndex = 16;
            this.lbVoid.Text = "ยกเลิก";
            this.lbVoid.Visible = false;
            // 
            // lbReturnProduct
            // 
            this.lbReturnProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbReturnProduct.AutoSize = true;
            this.lbReturnProduct.Location = new System.Drawing.Point(0, 0);
            this.lbReturnProduct.Name = "lbReturnProduct";
            this.lbReturnProduct.Size = new System.Drawing.Size(79, 13);
            this.lbReturnProduct.TabIndex = 15;
            this.lbReturnProduct.Text = "รับคืนจากสินค้า";
            this.lbReturnProduct.Visible = false;
            // 
            // lbReturnReceipt
            // 
            this.lbReturnReceipt.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbReturnReceipt.AutoSize = true;
            this.lbReturnReceipt.Location = new System.Drawing.Point(0, 0);
            this.lbReturnReceipt.Name = "lbReturnReceipt";
            this.lbReturnReceipt.Size = new System.Drawing.Size(89, 13);
            this.lbReturnReceipt.TabIndex = 14;
            this.lbReturnReceipt.Text = "รับคืนจากใบเสร็จ";
            this.lbReturnReceipt.Visible = false;
            // 
            // lbSale
            // 
            this.lbSale.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbSale.AutoSize = true;
            this.lbSale.Location = new System.Drawing.Point(0, 0);
            this.lbSale.Name = "lbSale";
            this.lbSale.Size = new System.Drawing.Size(25, 13);
            this.lbSale.TabIndex = 13;
            this.lbSale.Text = "ขาย";
            this.lbSale.Visible = false;
            // 
            // lbCashIn
            // 
            this.lbCashIn.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbCashIn.AutoSize = true;
            this.lbCashIn.Location = new System.Drawing.Point(0, 0);
            this.lbCashIn.Name = "lbCashIn";
            this.lbCashIn.Size = new System.Drawing.Size(59, 13);
            this.lbCashIn.TabIndex = 12;
            this.lbCashIn.Text = "รับเงินทอน";
            this.lbCashIn.Visible = false;
            // 
            // lbOpenDay
            // 
            this.lbOpenDay.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.lbOpenDay.AutoSize = true;
            this.lbOpenDay.Location = new System.Drawing.Point(0, 0);
            this.lbOpenDay.Name = "lbOpenDay";
            this.lbOpenDay.Size = new System.Drawing.Size(87, 13);
            this.lbOpenDay.TabIndex = 11;
            this.lbOpenDay.Text = "เปิดงานประจำวัน";
            this.lbOpenDay.Visible = false;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 9;
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage.Location = new System.Drawing.Point(12, 5);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(0, 33);
            this.lbMessage.TabIndex = 10;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "";
            this.ucHeader1.currentMenuTitle2 = "";
            this.ucHeader1.currentMenuTitle3 = "";
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = false;
            this.ucHeader1.showCurrentMenuText = false;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = false;
            this.ucHeader1.showLockScreen = true;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = false;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showScanner = false;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 8;
            this.ucHeader1.LanguageClick += new System.EventHandler(this.ucHeader1_LanguageClick);
            this.ucHeader1.Load += new System.EventHandler(this.ucHeader1_Load);
            // 
            // ucKeypad
            // 
            this.ucKeypad.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucKeypad.Location = new System.Drawing.Point(-186, 0);
            this.ucKeypad.Name = "ucKeypad";
            this.ucKeypad.Size = new System.Drawing.Size(336, 46);
            this.ucKeypad.TabIndex = 2;
            this.ucKeypad.ucTBS = null;
            this.ucKeypad.ucTBWI = null;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeyboard1.hideLanguageButton = false;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 0);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(150, 46);
            this.ucKeyboard1.TabIndex = 1;
            // 
            // lbUserVal
            // 
            this.lbUserVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbUserVal.Location = new System.Drawing.Point(161, 109);
            this.lbUserVal.Name = "lbUserVal";
            this.lbUserVal.Size = new System.Drawing.Size(308, 41);
            this.lbUserVal.TabIndex = 19;
            this.lbUserVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUserVal.Visible = false;
            // 
            // frmUserAuthorize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUserAuthorize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUserAuthorize_FormClosing);
            this.Load += new System.EventHandler(this.frmUserAuthorize_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbMainPassword;
        private System.Windows.Forms.Label lbMainUser;
        private System.Windows.Forms.Label lbMainHeader;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UCKeyboard ucKeyboard1;
        private UCKeypad ucKeypad;
        private UCTextBoxWithIcon ucTBWIEmpPass;
        private UCTextBoxWithIcon ucTBWIEmpUser;
        private UCFooter ucFooter1;
        private UCHeader ucHeader1;
        public System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label lbCancelCashOut;
        private System.Windows.Forms.Label lbReportReceipt;
        private System.Windows.Forms.Label lbReportDaySale;
        private System.Windows.Forms.Label lbCheckProduct;
        private System.Windows.Forms.Label lbChangePassword;
        private System.Windows.Forms.Label lbEndofTill;
        private System.Windows.Forms.Label lbEndofShift;
        private System.Windows.Forms.Label lbVoid;
        private System.Windows.Forms.Label lbReturnProduct;
        private System.Windows.Forms.Label lbReturnReceipt;
        private System.Windows.Forms.Label lbSale;
        private System.Windows.Forms.Label lbCashIn;
        private System.Windows.Forms.Label lbOpenDay;
        private System.Windows.Forms.Label lbVoidSuccess;
        private System.Windows.Forms.Label lbReturnSuccess;
        private System.Windows.Forms.Label lbConfirmPayment;
        private System.Windows.Forms.Label lbCancelSale;
        private System.Windows.Forms.Label lbEditItem;
        private System.Windows.Forms.Label lbDeleteItem;
        private System.Windows.Forms.Label lbCashOut;
        private System.Windows.Forms.Label lbReport;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.Label lbRedeem;
        private System.Windows.Forms.Label lbPrintExport;
        private System.Windows.Forms.Label lbSpecialProduct;
        private System.Windows.Forms.Label lbUserVal;

    }
}