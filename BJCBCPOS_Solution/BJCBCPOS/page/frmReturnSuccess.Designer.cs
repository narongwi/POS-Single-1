namespace BJCBCPOS
{
    partial class frmReturnSuccess
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
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbEDCInterFace = new System.Windows.Forms.Label();
            this.btnEDCInterface = new System.Windows.Forms.Button();
            this.lbSaleTimeVal = new System.Windows.Forms.Label();
            this.lbSaleTime = new System.Windows.Forms.Label();
            this.lbReturnTimeVal = new System.Windows.Forms.Label();
            this.lbReturnTime = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbReasonVal = new System.Windows.Forms.Label();
            this.lbRecepitNoVal = new System.Windows.Forms.Label();
            this.lbReceiptNoTxt = new System.Windows.Forms.Label();
            this.lbTotalVal = new System.Windows.Forms.Label();
            this.lbMemberName = new System.Windows.Forms.Label();
            this.lbCashierIdVal = new System.Windows.Forms.Label();
            this.lbLockNoResult = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbMember = new System.Windows.Forms.Label();
            this.lbCashierId = new System.Windows.Forms.Label();
            this.lbLockNo = new System.Windows.Forms.Label();
            this.lbReason = new System.Windows.Forms.Label();
            this.panel_button = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel_message = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lb_message = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel_button.SuspendLayout();
            this.panel_message.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.Font = new System.Drawing.Font("Prompt", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage.Location = new System.Drawing.Point(3, 1);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(550, 68);
            this.lbMessage.TabIndex = 0;
            this.lbMessage.Text = "ยืนยันการคืนสินค้า";
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_OK_ConfirmVoid;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(220, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(307, 67);
            this.btnOk.TabIndex = 88;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BJCBCPOS.Properties.Resources.icons8_checked_126px_1;
            this.pictureBox1.Location = new System.Drawing.Point(219, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbEDCInterFace);
            this.panel1.Controls.Add(this.btnEDCInterface);
            this.panel1.Controls.Add(this.lbSaleTimeVal);
            this.panel1.Controls.Add(this.lbSaleTime);
            this.panel1.Controls.Add(this.lbReturnTimeVal);
            this.panel1.Controls.Add(this.lbReturnTime);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbRecepitNoVal);
            this.panel1.Controls.Add(this.lbReceiptNoTxt);
            this.panel1.Controls.Add(this.lbTotalVal);
            this.panel1.Controls.Add(this.lbMemberName);
            this.panel1.Controls.Add(this.lbCashierIdVal);
            this.panel1.Controls.Add(this.lbLockNoResult);
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.lbMember);
            this.panel1.Controls.Add(this.lbCashierId);
            this.panel1.Controls.Add(this.lbLockNo);
            this.panel1.Controls.Add(this.lbReason);
            this.panel1.Controls.Add(this.lbMessage);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel_button);
            this.panel1.Controls.Add(this.panel_message);
            this.panel1.Location = new System.Drawing.Point(235, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 700);
            this.panel1.TabIndex = 89;
            // 
            // lbEDCInterFace
            // 
            this.lbEDCInterFace.BackColor = System.Drawing.Color.Transparent;
            this.lbEDCInterFace.Font = new System.Drawing.Font("Prompt", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbEDCInterFace.ForeColor = System.Drawing.Color.Gray;
            this.lbEDCInterFace.Location = new System.Drawing.Point(47, 549);
            this.lbEDCInterFace.Name = "lbEDCInterFace";
            this.lbEDCInterFace.Size = new System.Drawing.Size(58, 34);
            this.lbEDCInterFace.TabIndex = 134;
            this.lbEDCInterFace.Text = "EDC Interface";
            this.lbEDCInterFace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEDCInterFace.Click += new System.EventHandler(this.btnEDCInterface_Click);
            // 
            // btnEDCInterface
            // 
            this.btnEDCInterface.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEDCInterface.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.btnEDCInterface.FlatAppearance.BorderSize = 2;
            this.btnEDCInterface.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEDCInterface.ForeColor = System.Drawing.Color.Black;
            this.btnEDCInterface.Image = global::BJCBCPOS.Properties.Resources.icon_debit_credit;
            this.btnEDCInterface.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEDCInterface.Location = new System.Drawing.Point(35, 546);
            this.btnEDCInterface.Name = "btnEDCInterface";
            this.btnEDCInterface.Size = new System.Drawing.Size(111, 41);
            this.btnEDCInterface.TabIndex = 135;
            this.btnEDCInterface.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEDCInterface.UseVisualStyleBackColor = true;
            this.btnEDCInterface.Click += new System.EventHandler(this.btnEDCInterface_Click);
            // 
            // lbSaleTimeVal
            // 
            this.lbSaleTimeVal.BackColor = System.Drawing.Color.White;
            this.lbSaleTimeVal.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaleTimeVal.ForeColor = System.Drawing.Color.Gray;
            this.lbSaleTimeVal.Location = new System.Drawing.Point(279, 359);
            this.lbSaleTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSaleTimeVal.Name = "lbSaleTimeVal";
            this.lbSaleTimeVal.Size = new System.Drawing.Size(227, 32);
            this.lbSaleTimeVal.TabIndex = 133;
            this.lbSaleTimeVal.Text = "10/10/21";
            this.lbSaleTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbSaleTime
            // 
            this.lbSaleTime.AutoSize = true;
            this.lbSaleTime.BackColor = System.Drawing.Color.White;
            this.lbSaleTime.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaleTime.ForeColor = System.Drawing.Color.Black;
            this.lbSaleTime.Location = new System.Drawing.Point(55, 359);
            this.lbSaleTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSaleTime.Name = "lbSaleTime";
            this.lbSaleTime.Size = new System.Drawing.Size(112, 32);
            this.lbSaleTime.TabIndex = 132;
            this.lbSaleTime.Text = "Sale Time";
            this.lbSaleTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbReturnTimeVal
            // 
            this.lbReturnTimeVal.BackColor = System.Drawing.Color.White;
            this.lbReturnTimeVal.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReturnTimeVal.ForeColor = System.Drawing.Color.Gray;
            this.lbReturnTimeVal.Location = new System.Drawing.Point(304, 413);
            this.lbReturnTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbReturnTimeVal.Name = "lbReturnTimeVal";
            this.lbReturnTimeVal.Size = new System.Drawing.Size(236, 36);
            this.lbReturnTimeVal.TabIndex = 131;
            this.lbReturnTimeVal.Text = "10/10/21";
            this.lbReturnTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbReturnTime
            // 
            this.lbReturnTime.AutoSize = true;
            this.lbReturnTime.BackColor = System.Drawing.Color.White;
            this.lbReturnTime.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReturnTime.ForeColor = System.Drawing.Color.Black;
            this.lbReturnTime.Location = new System.Drawing.Point(18, 413);
            this.lbReturnTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReturnTime.Name = "lbReturnTime";
            this.lbReturnTime.Size = new System.Drawing.Size(155, 36);
            this.lbReturnTime.TabIndex = 130;
            this.lbReturnTime.Text = "Return Time";
            this.lbReturnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbReasonVal);
            this.panel2.Location = new System.Drawing.Point(226, 496);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 100);
            this.panel2.TabIndex = 127;
            // 
            // lbReasonVal
            // 
            this.lbReasonVal.AutoSize = true;
            this.lbReasonVal.BackColor = System.Drawing.Color.White;
            this.lbReasonVal.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbReasonVal.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReasonVal.ForeColor = System.Drawing.Color.Red;
            this.lbReasonVal.Location = new System.Drawing.Point(306, 0);
            this.lbReasonVal.MaximumSize = new System.Drawing.Size(291, 96);
            this.lbReasonVal.Name = "lbReasonVal";
            this.lbReasonVal.Size = new System.Drawing.Size(0, 32);
            this.lbReasonVal.TabIndex = 95;
            this.lbReasonVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbRecepitNoVal
            // 
            this.lbRecepitNoVal.BackColor = System.Drawing.Color.White;
            this.lbRecepitNoVal.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecepitNoVal.ForeColor = System.Drawing.Color.Gray;
            this.lbRecepitNoVal.Location = new System.Drawing.Point(279, 211);
            this.lbRecepitNoVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRecepitNoVal.Name = "lbRecepitNoVal";
            this.lbRecepitNoVal.Size = new System.Drawing.Size(227, 32);
            this.lbRecepitNoVal.TabIndex = 129;
            this.lbRecepitNoVal.Text = "V0000123";
            this.lbRecepitNoVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbReceiptNoTxt
            // 
            this.lbReceiptNoTxt.AutoSize = true;
            this.lbReceiptNoTxt.BackColor = System.Drawing.Color.White;
            this.lbReceiptNoTxt.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReceiptNoTxt.ForeColor = System.Drawing.Color.Black;
            this.lbReceiptNoTxt.Location = new System.Drawing.Point(55, 211);
            this.lbReceiptNoTxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReceiptNoTxt.Name = "lbReceiptNoTxt";
            this.lbReceiptNoTxt.Size = new System.Drawing.Size(129, 32);
            this.lbReceiptNoTxt.TabIndex = 128;
            this.lbReceiptNoTxt.Text = "Receipt No.";
            this.lbReceiptNoTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotalVal
            // 
            this.lbTotalVal.BackColor = System.Drawing.Color.White;
            this.lbTotalVal.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalVal.ForeColor = System.Drawing.Color.Gray;
            this.lbTotalVal.Location = new System.Drawing.Point(327, 453);
            this.lbTotalVal.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbTotalVal.Name = "lbTotalVal";
            this.lbTotalVal.Size = new System.Drawing.Size(213, 36);
            this.lbTotalVal.TabIndex = 125;
            this.lbTotalVal.Text = "8,888,888.00";
            this.lbTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbMemberName
            // 
            this.lbMemberName.BackColor = System.Drawing.Color.White;
            this.lbMemberName.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMemberName.ForeColor = System.Drawing.Color.Gray;
            this.lbMemberName.Location = new System.Drawing.Point(279, 322);
            this.lbMemberName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMemberName.Name = "lbMemberName";
            this.lbMemberName.Size = new System.Drawing.Size(227, 32);
            this.lbMemberName.TabIndex = 123;
            this.lbMemberName.Text = "Staff Name";
            this.lbMemberName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbCashierIdVal
            // 
            this.lbCashierIdVal.BackColor = System.Drawing.Color.White;
            this.lbCashierIdVal.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCashierIdVal.ForeColor = System.Drawing.Color.Gray;
            this.lbCashierIdVal.Location = new System.Drawing.Point(279, 285);
            this.lbCashierIdVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCashierIdVal.Name = "lbCashierIdVal";
            this.lbCashierIdVal.Size = new System.Drawing.Size(227, 32);
            this.lbCashierIdVal.TabIndex = 122;
            this.lbCashierIdVal.Text = "300004";
            this.lbCashierIdVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLockNoResult
            // 
            this.lbLockNoResult.BackColor = System.Drawing.Color.White;
            this.lbLockNoResult.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLockNoResult.ForeColor = System.Drawing.Color.Gray;
            this.lbLockNoResult.Location = new System.Drawing.Point(279, 248);
            this.lbLockNoResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLockNoResult.Name = "lbLockNoResult";
            this.lbLockNoResult.Size = new System.Drawing.Size(227, 32);
            this.lbLockNoResult.TabIndex = 121;
            this.lbLockNoResult.Text = "006";
            this.lbLockNoResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(23, 401);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(508, 2);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 126;
            this.pictureBox5.TabStop = false;
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.BackColor = System.Drawing.Color.White;
            this.lbTotal.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.Black;
            this.lbTotal.Location = new System.Drawing.Point(18, 453);
            this.lbTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(178, 36);
            this.lbTotal.TabIndex = 124;
            this.lbTotal.Text = "ยอดรวมทั้งหมด";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMember
            // 
            this.lbMember.AutoSize = true;
            this.lbMember.BackColor = System.Drawing.Color.White;
            this.lbMember.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMember.ForeColor = System.Drawing.Color.Black;
            this.lbMember.Location = new System.Drawing.Point(55, 322);
            this.lbMember.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMember.Name = "lbMember";
            this.lbMember.Size = new System.Drawing.Size(98, 32);
            this.lbMember.TabIndex = 120;
            this.lbMember.Text = "Member";
            this.lbMember.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCashierId
            // 
            this.lbCashierId.AutoSize = true;
            this.lbCashierId.BackColor = System.Drawing.Color.White;
            this.lbCashierId.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCashierId.ForeColor = System.Drawing.Color.Black;
            this.lbCashierId.Location = new System.Drawing.Point(55, 285);
            this.lbCashierId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCashierId.Name = "lbCashierId";
            this.lbCashierId.Size = new System.Drawing.Size(119, 32);
            this.lbCashierId.TabIndex = 119;
            this.lbCashierId.Text = "Cashier ID";
            this.lbCashierId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbLockNo
            // 
            this.lbLockNo.AutoSize = true;
            this.lbLockNo.BackColor = System.Drawing.Color.White;
            this.lbLockNo.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLockNo.ForeColor = System.Drawing.Color.Black;
            this.lbLockNo.Location = new System.Drawing.Point(55, 248);
            this.lbLockNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLockNo.Name = "lbLockNo";
            this.lbLockNo.Size = new System.Drawing.Size(88, 32);
            this.lbLockNo.TabIndex = 118;
            this.lbLockNo.Text = "Loc No.";
            this.lbLockNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.BackColor = System.Drawing.Color.White;
            this.lbReason.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReason.Location = new System.Drawing.Point(18, 493);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(83, 36);
            this.lbReason.TabIndex = 117;
            this.lbReason.Text = "เหตุผล";
            this.lbReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_button
            // 
            this.panel_button.Controls.Add(this.btnCancel);
            this.panel_button.Controls.Add(this.btnOk);
            this.panel_button.Location = new System.Drawing.Point(0, 602);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(556, 98);
            this.panel_button.TabIndex = 90;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Cancel_ConfirmVoid;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancel.Location = new System.Drawing.Point(29, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 67);
            this.btnCancel.TabIndex = 89;
            this.btnCancel.Text = "ไม่ใช่";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel_message
            // 
            this.panel_message.Controls.Add(this.pictureBox2);
            this.panel_message.Controls.Add(this.lb_message);
            this.panel_message.Location = new System.Drawing.Point(0, 602);
            this.panel_message.Name = "panel_message";
            this.panel_message.Size = new System.Drawing.Size(556, 98);
            this.panel_message.TabIndex = 91;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_error_126px;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(246, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 60);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // lb_message
            // 
            this.lb_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_message.Location = new System.Drawing.Point(2, 62);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(551, 35);
            this.lb_message.TabIndex = 0;
            this.lb_message.Text = "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ";
            this.lb_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmReturnSuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReturnSuccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmReturnSuccess";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReturnSuccess_FormClosed);
            this.Load += new System.EventHandler(this.frmReturnSuccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel_button.ResumeLayout(false);
            this.panel_message.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel_button;
        private System.Windows.Forms.Panel panel_message;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.Label lbSaleTimeVal;
        private System.Windows.Forms.Label lbSaleTime;
        private System.Windows.Forms.Label lbReturnTimeVal;
        private System.Windows.Forms.Label lbReturnTime;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbReasonVal;
        private System.Windows.Forms.Label lbRecepitNoVal;
        private System.Windows.Forms.Label lbReceiptNoTxt;
        private System.Windows.Forms.Label lbTotalVal;
        private System.Windows.Forms.Label lbMemberName;
        private System.Windows.Forms.Label lbCashierIdVal;
        private System.Windows.Forms.Label lbLockNoResult;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbMember;
        private System.Windows.Forms.Label lbCashierId;
        private System.Windows.Forms.Label lbLockNo;
        private System.Windows.Forms.Label lbReason;
        private System.Windows.Forms.Label lbEDCInterFace;
        private System.Windows.Forms.Button btnEDCInterface;
    }
}