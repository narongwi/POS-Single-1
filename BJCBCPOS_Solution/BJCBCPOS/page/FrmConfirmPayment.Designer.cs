namespace BJCBCPOS
{
    partial class frmConfirmPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfirmPayment));
            this.pn_PaymentNormal = new System.Windows.Forms.Panel();
            this.lbText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTxtChange = new System.Windows.Forms.Label();
            this.lbOldChange = new System.Windows.Forms.Label();
            this.lbChange = new System.Windows.Forms.Label();
            this.lbTxtPayment = new System.Windows.Forms.Label();
            this.lbPayment = new System.Windows.Forms.Label();
            this.lbTxtCash = new System.Windows.Forms.Label();
            this.lbCash = new System.Windows.Forms.Label();
            this.lbConfirm = new System.Windows.Forms.Label();
            this.panel_button = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel_message = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_message = new System.Windows.Forms.Label();
            this.btnEditChange = new System.Windows.Forms.Button();
            this.btnCancelChange = new System.Windows.Forms.Button();
            this.pn_PaymentCurrency = new System.Windows.Forms.Panel();
            this.btnEditChangeCurrency = new System.Windows.Forms.Button();
            this.lbTextCurrency = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbTxtPaymentCurrency = new System.Windows.Forms.Label();
            this.lbTxtCashCurrency = new System.Windows.Forms.Label();
            this.pn_Currency = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbTxtChangeCurrency = new System.Windows.Forms.Label();
            this.lbBalanceCurrency = new System.Windows.Forms.Label();
            this.lbPaymentCurrency = new System.Windows.Forms.Label();
            this.lbCashCurrency = new System.Windows.Forms.Label();
            this.lbConfirmCurrency = new System.Windows.Forms.Label();
            this.panel_button2 = new System.Windows.Forms.Panel();
            this.btnCancelCurrency = new System.Windows.Forms.Button();
            this.btnOkCurrency = new System.Windows.Forms.Button();
            this.panel_message2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lb_messageCurrency = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTxtChange = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucTxtChangeCurrency = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucKeypad1 = new BJCBCPOS.UCKeypad();
            this.pn_PaymentNormal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_button.SuspendLayout();
            this.panel_message.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pn_PaymentCurrency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel_button2.SuspendLayout();
            this.panel_message2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_PaymentNormal
            // 
            this.pn_PaymentNormal.BackColor = System.Drawing.Color.White;
            this.pn_PaymentNormal.Controls.Add(this.lbText);
            this.pn_PaymentNormal.Controls.Add(this.panel1);
            this.pn_PaymentNormal.Controls.Add(this.lbTxtPayment);
            this.pn_PaymentNormal.Controls.Add(this.lbPayment);
            this.pn_PaymentNormal.Controls.Add(this.lbTxtCash);
            this.pn_PaymentNormal.Controls.Add(this.lbCash);
            this.pn_PaymentNormal.Controls.Add(this.lbConfirm);
            this.pn_PaymentNormal.Controls.Add(this.panel_button);
            this.pn_PaymentNormal.Controls.Add(this.panel_message);
            this.pn_PaymentNormal.Controls.Add(this.btnEditChange);
            this.pn_PaymentNormal.Controls.Add(this.btnCancelChange);
            this.pn_PaymentNormal.Location = new System.Drawing.Point(39, 35);
            this.pn_PaymentNormal.Name = "pn_PaymentNormal";
            this.pn_PaymentNormal.Size = new System.Drawing.Size(644, 440);
            this.pn_PaymentNormal.TabIndex = 10;
            // 
            // lbText
            // 
            this.lbText.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(139)))), ((int)(((byte)(46)))));
            this.lbText.Location = new System.Drawing.Point(30, 74);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(579, 35);
            this.lbText.TabIndex = 141;
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbText.TextChanged += new System.EventHandler(this.lbText_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmPaymentChange;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbTxtChange);
            this.panel1.Controls.Add(this.lbChange);
            this.panel1.Controls.Add(this.ucTxtChange);
            this.panel1.Controls.Add(this.lbOldChange);
            this.panel1.Location = new System.Drawing.Point(52, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 77);
            this.panel1.TabIndex = 17;
            // 
            // lbTxtChange
            // 
            this.lbTxtChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbTxtChange.Font = new System.Drawing.Font("Prompt", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtChange.ForeColor = System.Drawing.Color.Red;
            this.lbTxtChange.Location = new System.Drawing.Point(251, 1);
            this.lbTxtChange.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbTxtChange.Name = "lbTxtChange";
            this.lbTxtChange.Size = new System.Drawing.Size(285, 73);
            this.lbTxtChange.TabIndex = 8;
            this.lbTxtChange.Text = "8,888,888.88";
            this.lbTxtChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbTxtChange.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbOldChange
            // 
            this.lbOldChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbOldChange.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOldChange.ForeColor = System.Drawing.Color.Red;
            this.lbOldChange.Location = new System.Drawing.Point(251, 6);
            this.lbOldChange.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbOldChange.Name = "lbOldChange";
            this.lbOldChange.Size = new System.Drawing.Size(283, 25);
            this.lbOldChange.TabIndex = 10;
            this.lbOldChange.Text = "เงินทอนเดิม 4,000.00";
            this.lbOldChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbOldChange.Visible = false;
            // 
            // lbChange
            // 
            this.lbChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbChange.Font = new System.Drawing.Font("Prompt", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChange.ForeColor = System.Drawing.Color.Black;
            this.lbChange.Location = new System.Drawing.Point(4, 1);
            this.lbChange.Name = "lbChange";
            this.lbChange.Size = new System.Drawing.Size(241, 73);
            this.lbChange.TabIndex = 7;
            this.lbChange.Text = "ยอดเงินทอน";
            this.lbChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbChange.TextChanged += new System.EventHandler(this.lbBalance_TextChanged);
            // 
            // lbTxtPayment
            // 
            this.lbTxtPayment.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtPayment.ForeColor = System.Drawing.Color.Black;
            this.lbTxtPayment.Location = new System.Drawing.Point(331, 166);
            this.lbTxtPayment.Name = "lbTxtPayment";
            this.lbTxtPayment.Size = new System.Drawing.Size(257, 50);
            this.lbTxtPayment.TabIndex = 14;
            this.lbTxtPayment.Text = "8,888,888.88";
            this.lbTxtPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbTxtPayment.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbPayment
            // 
            this.lbPayment.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPayment.ForeColor = System.Drawing.Color.Black;
            this.lbPayment.Location = new System.Drawing.Point(66, 173);
            this.lbPayment.Name = "lbPayment";
            this.lbPayment.Size = new System.Drawing.Size(257, 40);
            this.lbPayment.TabIndex = 13;
            this.lbPayment.Text = "ยอดชำระ";
            this.lbPayment.TextChanged += new System.EventHandler(this.lbPayment_TextChanged);
            // 
            // lbTxtCash
            // 
            this.lbTxtCash.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtCash.ForeColor = System.Drawing.Color.Black;
            this.lbTxtCash.Location = new System.Drawing.Point(331, 110);
            this.lbTxtCash.Name = "lbTxtCash";
            this.lbTxtCash.Size = new System.Drawing.Size(257, 50);
            this.lbTxtCash.TabIndex = 12;
            this.lbTxtCash.Text = "8,888,888.88";
            this.lbTxtCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbTxtCash.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbCash
            // 
            this.lbCash.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCash.ForeColor = System.Drawing.Color.Black;
            this.lbCash.Location = new System.Drawing.Point(66, 116);
            this.lbCash.Name = "lbCash";
            this.lbCash.Size = new System.Drawing.Size(257, 40);
            this.lbCash.TabIndex = 11;
            this.lbCash.Text = "ยอดเงิน";
            this.lbCash.TextChanged += new System.EventHandler(this.lbCash_TextChanged);
            // 
            // lbConfirm
            // 
            this.lbConfirm.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirm.ForeColor = System.Drawing.Color.Black;
            this.lbConfirm.Location = new System.Drawing.Point(3, 9);
            this.lbConfirm.Name = "lbConfirm";
            this.lbConfirm.Size = new System.Drawing.Size(638, 58);
            this.lbConfirm.TabIndex = 10;
            this.lbConfirm.Text = "ยืนยันยอดเงินชำระ";
            this.lbConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbConfirm.TextChanged += new System.EventHandler(this.lbConfirm_TextChanged);
            // 
            // panel_button
            // 
            this.panel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_button.BackColor = System.Drawing.Color.White;
            this.panel_button.Controls.Add(this.btnCancel);
            this.panel_button.Controls.Add(this.btnOk);
            this.panel_button.Location = new System.Drawing.Point(0, 330);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(644, 110);
            this.panel_button.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Cancel_Confirmpayment;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancel.Location = new System.Drawing.Point(40, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 62);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "ไม่ใช่";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.TextChanged += new System.EventHandler(this.btnCancel_TextChanged);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_OK_Confirmpayment;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(260, 24);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(345, 62);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.TextChanged += new System.EventHandler(this.btnOk_TextChanged);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel_message
            // 
            this.panel_message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_message.Controls.Add(this.pictureBox1);
            this.panel_message.Controls.Add(this.lb_message);
            this.panel_message.Location = new System.Drawing.Point(0, 330);
            this.panel_message.Name = "panel_message";
            this.panel_message.Size = new System.Drawing.Size(644, 110);
            this.panel_message.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_error_126px;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(290, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 60);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lb_message
            // 
            this.lb_message.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_message.Location = new System.Drawing.Point(2, 72);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(640, 35);
            this.lb_message.TabIndex = 0;
            this.lb_message.Text = "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ";
            this.lb_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_message.TextChanged += new System.EventHandler(this.lb_message_TextChanged);
            // 
            // btnEditChange
            // 
            this.btnEditChange.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_edit_creditcard;
            this.btnEditChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditChange.FlatAppearance.BorderSize = 0;
            this.btnEditChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditChange.ForeColor = System.Drawing.Color.White;
            this.btnEditChange.Location = new System.Drawing.Point(597, 246);
            this.btnEditChange.Name = "btnEditChange";
            this.btnEditChange.Size = new System.Drawing.Size(41, 54);
            this.btnEditChange.TabIndex = 142;
            this.btnEditChange.UseVisualStyleBackColor = true;
            this.btnEditChange.Visible = false;
            this.btnEditChange.Click += new System.EventHandler(this.btnEditChange_Click);
            // 
            // btnCancelChange
            // 
            this.btnCancelChange.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_cancel_126px_2;
            this.btnCancelChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancelChange.FlatAppearance.BorderSize = 0;
            this.btnCancelChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelChange.ForeColor = System.Drawing.Color.White;
            this.btnCancelChange.Location = new System.Drawing.Point(597, 246);
            this.btnCancelChange.Name = "btnCancelChange";
            this.btnCancelChange.Size = new System.Drawing.Size(41, 54);
            this.btnCancelChange.TabIndex = 143;
            this.btnCancelChange.UseVisualStyleBackColor = true;
            this.btnCancelChange.Visible = false;
            this.btnCancelChange.Click += new System.EventHandler(this.btnCancelChange_Click);
            // 
            // pn_PaymentCurrency
            // 
            this.pn_PaymentCurrency.BackColor = System.Drawing.Color.White;
            this.pn_PaymentCurrency.Controls.Add(this.btnEditChangeCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.lbTextCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.pictureBox3);
            this.pn_PaymentCurrency.Controls.Add(this.lbTxtPaymentCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.lbTxtCashCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.pn_Currency);
            this.pn_PaymentCurrency.Controls.Add(this.panel4);
            this.pn_PaymentCurrency.Controls.Add(this.lbPaymentCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.lbCashCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.lbConfirmCurrency);
            this.pn_PaymentCurrency.Controls.Add(this.panel_button2);
            this.pn_PaymentCurrency.Controls.Add(this.panel_message2);
            this.pn_PaymentCurrency.Location = new System.Drawing.Point(39, 35);
            this.pn_PaymentCurrency.Name = "pn_PaymentCurrency";
            this.pn_PaymentCurrency.Size = new System.Drawing.Size(741, 513);
            this.pn_PaymentCurrency.TabIndex = 11;
            // 
            // btnEditChangeCurrency
            // 
            this.btnEditChangeCurrency.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_edit_creditcard;
            this.btnEditChangeCurrency.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditChangeCurrency.FlatAppearance.BorderSize = 0;
            this.btnEditChangeCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditChangeCurrency.ForeColor = System.Drawing.Color.White;
            this.btnEditChangeCurrency.Location = new System.Drawing.Point(695, 233);
            this.btnEditChangeCurrency.Name = "btnEditChangeCurrency";
            this.btnEditChangeCurrency.Size = new System.Drawing.Size(41, 54);
            this.btnEditChangeCurrency.TabIndex = 143;
            this.btnEditChangeCurrency.UseVisualStyleBackColor = true;
            this.btnEditChangeCurrency.Visible = false;
            this.btnEditChangeCurrency.Click += new System.EventHandler(this.btnEditChangeCurrency_Click);
            // 
            // lbTextCurrency
            // 
            this.lbTextCurrency.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(139)))), ((int)(((byte)(46)))));
            this.lbTextCurrency.Location = new System.Drawing.Point(29, 69);
            this.lbTextCurrency.Name = "lbTextCurrency";
            this.lbTextCurrency.Size = new System.Drawing.Size(615, 41);
            this.lbTextCurrency.TabIndex = 140;
            this.lbTextCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox3.Location = new System.Drawing.Point(47, 325);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(640, 2);
            this.pictureBox3.TabIndex = 138;
            this.pictureBox3.TabStop = false;
            // 
            // lbTxtPaymentCurrency
            // 
            this.lbTxtPaymentCurrency.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbTxtPaymentCurrency.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtPaymentCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbTxtPaymentCurrency.Location = new System.Drawing.Point(388, 181);
            this.lbTxtPaymentCurrency.Name = "lbTxtPaymentCurrency";
            this.lbTxtPaymentCurrency.Size = new System.Drawing.Size(296, 50);
            this.lbTxtPaymentCurrency.TabIndex = 27;
            this.lbTxtPaymentCurrency.Text = "88,888,888.88";
            this.lbTxtPaymentCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbTxtPaymentCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbTxtCashCurrency
            // 
            this.lbTxtCashCurrency.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbTxtCashCurrency.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtCashCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbTxtCashCurrency.Location = new System.Drawing.Point(388, 125);
            this.lbTxtCashCurrency.Name = "lbTxtCashCurrency";
            this.lbTxtCashCurrency.Size = new System.Drawing.Size(296, 50);
            this.lbTxtCashCurrency.TabIndex = 26;
            this.lbTxtCashCurrency.Text = "888,888.88";
            this.lbTxtCashCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbTxtCashCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // pn_Currency
            // 
            this.pn_Currency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pn_Currency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_Currency.Location = new System.Drawing.Point(91, 332);
            this.pn_Currency.Name = "pn_Currency";
            this.pn_Currency.Size = new System.Drawing.Size(560, 75);
            this.pn_Currency.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.BackgroundImage = global::BJCBCPOS.Properties.Resources.confirmPaymentChange;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.lbTxtChangeCurrency);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.ucTxtChangeCurrency);
            this.panel4.Controls.Add(this.lbBalanceCurrency);
            this.panel4.Location = new System.Drawing.Point(50, 247);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(640, 72);
            this.panel4.TabIndex = 24;
            // 
            // lbTxtChangeCurrency
            // 
            this.lbTxtChangeCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbTxtChangeCurrency.Font = new System.Drawing.Font("Prompt", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtChangeCurrency.ForeColor = System.Drawing.Color.Red;
            this.lbTxtChangeCurrency.Location = new System.Drawing.Point(306, 2);
            this.lbTxtChangeCurrency.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbTxtChangeCurrency.Name = "lbTxtChangeCurrency";
            this.lbTxtChangeCurrency.Size = new System.Drawing.Size(332, 67);
            this.lbTxtChangeCurrency.TabIndex = 8;
            this.lbTxtChangeCurrency.Text = "8,888,888.88";
            this.lbTxtChangeCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbTxtChangeCurrency.FontChanged += new System.EventHandler(this.Control_FontChanged);
            // 
            // lbBalanceCurrency
            // 
            this.lbBalanceCurrency.AutoSize = true;
            this.lbBalanceCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.lbBalanceCurrency.Font = new System.Drawing.Font("Prompt", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBalanceCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbBalanceCurrency.Location = new System.Drawing.Point(4, 14);
            this.lbBalanceCurrency.Name = "lbBalanceCurrency";
            this.lbBalanceCurrency.Size = new System.Drawing.Size(175, 44);
            this.lbBalanceCurrency.TabIndex = 7;
            this.lbBalanceCurrency.Text = "ยอดเงินทอน";
            // 
            // lbPaymentCurrency
            // 
            this.lbPaymentCurrency.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPaymentCurrency.AutoSize = true;
            this.lbPaymentCurrency.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPaymentCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbPaymentCurrency.Location = new System.Drawing.Point(69, 189);
            this.lbPaymentCurrency.Name = "lbPaymentCurrency";
            this.lbPaymentCurrency.Size = new System.Drawing.Size(118, 40);
            this.lbPaymentCurrency.TabIndex = 23;
            this.lbPaymentCurrency.Text = "ยอดชำระ";
            // 
            // lbCashCurrency
            // 
            this.lbCashCurrency.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbCashCurrency.AutoSize = true;
            this.lbCashCurrency.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCashCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbCashCurrency.Location = new System.Drawing.Point(69, 133);
            this.lbCashCurrency.Name = "lbCashCurrency";
            this.lbCashCurrency.Size = new System.Drawing.Size(110, 40);
            this.lbCashCurrency.TabIndex = 22;
            this.lbCashCurrency.Text = "ยอดเงิน";
            // 
            // lbConfirmCurrency
            // 
            this.lbConfirmCurrency.Font = new System.Drawing.Font("Prompt", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirmCurrency.ForeColor = System.Drawing.Color.Black;
            this.lbConfirmCurrency.Location = new System.Drawing.Point(3, 5);
            this.lbConfirmCurrency.Name = "lbConfirmCurrency";
            this.lbConfirmCurrency.Size = new System.Drawing.Size(728, 60);
            this.lbConfirmCurrency.TabIndex = 21;
            this.lbConfirmCurrency.Text = "ยืนยันยอดเงินชำระ";
            this.lbConfirmCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_button2
            // 
            this.panel_button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_button2.BackColor = System.Drawing.Color.White;
            this.panel_button2.Controls.Add(this.btnCancelCurrency);
            this.panel_button2.Controls.Add(this.btnOkCurrency);
            this.panel_button2.Location = new System.Drawing.Point(0, 412);
            this.panel_button2.Name = "panel_button2";
            this.panel_button2.Size = new System.Drawing.Size(741, 100);
            this.panel_button2.TabIndex = 20;
            // 
            // btnCancelCurrency
            // 
            this.btnCancelCurrency.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Cancel_Confirmpayment;
            this.btnCancelCurrency.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelCurrency.FlatAppearance.BorderSize = 0;
            this.btnCancelCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelCurrency.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelCurrency.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancelCurrency.Location = new System.Drawing.Point(48, 13);
            this.btnCancelCurrency.Name = "btnCancelCurrency";
            this.btnCancelCurrency.Size = new System.Drawing.Size(180, 73);
            this.btnCancelCurrency.TabIndex = 15;
            this.btnCancelCurrency.Text = "ไม่ใช่";
            this.btnCancelCurrency.UseVisualStyleBackColor = true;
            this.btnCancelCurrency.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOkCurrency
            // 
            this.btnOkCurrency.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_OK_Confirmpayment;
            this.btnOkCurrency.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOkCurrency.FlatAppearance.BorderSize = 0;
            this.btnOkCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkCurrency.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkCurrency.ForeColor = System.Drawing.Color.White;
            this.btnOkCurrency.Location = new System.Drawing.Point(270, 13);
            this.btnOkCurrency.Name = "btnOkCurrency";
            this.btnOkCurrency.Size = new System.Drawing.Size(422, 73);
            this.btnOkCurrency.TabIndex = 16;
            this.btnOkCurrency.Text = "ใช่";
            this.btnOkCurrency.UseVisualStyleBackColor = true;
            this.btnOkCurrency.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel_message2
            // 
            this.panel_message2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_message2.Controls.Add(this.pictureBox2);
            this.panel_message2.Controls.Add(this.lb_messageCurrency);
            this.panel_message2.Location = new System.Drawing.Point(0, 412);
            this.panel_message2.Name = "panel_message2";
            this.panel_message2.Size = new System.Drawing.Size(741, 100);
            this.panel_message2.TabIndex = 139;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::BJCBCPOS.Properties.Resources.icons8_error_126px;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(335, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 60);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // lb_messageCurrency
            // 
            this.lb_messageCurrency.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_messageCurrency.Location = new System.Drawing.Point(2, 62);
            this.lb_messageCurrency.Name = "lb_messageCurrency";
            this.lb_messageCurrency.Size = new System.Drawing.Size(729, 35);
            this.lb_messageCurrency.TabIndex = 0;
            this.lb_messageCurrency.Text = "ลิ้นชักเปิดอยู่ ปิดเสียก่อนทำการขายต่อ";
            this.lb_messageCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel1.Controls.Add(this.pn_PaymentNormal);
            this.splitContainer1.Panel1.Controls.Add(this.pn_PaymentCurrency);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Panel2.Controls.Add(this.ucKeypad1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 465;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(219)))), ((int)(((byte)(179)))));
            this.label1.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(351, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "เงินทอนเดิม 4,000.00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // ucTxtChange
            // 
            this.ucTxtChange.BackColor = System.Drawing.Color.Transparent;
            this.ucTxtChange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTxtChange.BackgroundImage")));
            this.ucTxtChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxtChange.EnabledUC = true;
            this.ucTxtChange.IsAmount = true;
            this.ucTxtChange.IsLarge = false;
            this.ucTxtChange.IsNumber = false;
            this.ucTxtChange.IsSetFormat = true;
            this.ucTxtChange.IsValidateNumberZero = false;
            this.ucTxtChange.IsValidateTextEmpty = false;
            this.ucTxtChange.Location = new System.Drawing.Point(271, 31);
            this.ucTxtChange.MaxLength = 11;
            this.ucTxtChange.Name = "ucTxtChange";
            this.ucTxtChange.PasswordChar = false;
            this.ucTxtChange.placeHolder = "กรอกจำนวนเงิน";
            this.ucTxtChange.Readonly = false;
            this.ucTxtChange.ShortcutsEnabled = true;
            this.ucTxtChange.Size = new System.Drawing.Size(263, 42);
            this.ucTxtChange.TabIndex = 9;
            this.ucTxtChange.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxtChange.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ucTxtChange.Visible = false;
            this.ucTxtChange.EnterFromButton += new System.EventHandler(this.ucTxtChange_EnterFromButton);
            this.ucTxtChange.VisibleChanged += new System.EventHandler(this.ucTxtChange_VisibleChanged);
            // 
            // ucTxtChangeCurrency
            // 
            this.ucTxtChangeCurrency.BackColor = System.Drawing.Color.Transparent;
            this.ucTxtChangeCurrency.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucTxtChangeCurrency.BackgroundImage")));
            this.ucTxtChangeCurrency.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTxtChangeCurrency.EnabledUC = true;
            this.ucTxtChangeCurrency.IsAmount = true;
            this.ucTxtChangeCurrency.IsLarge = false;
            this.ucTxtChangeCurrency.IsNumber = false;
            this.ucTxtChangeCurrency.IsSetFormat = true;
            this.ucTxtChangeCurrency.IsValidateNumberZero = false;
            this.ucTxtChangeCurrency.IsValidateTextEmpty = false;
            this.ucTxtChangeCurrency.Location = new System.Drawing.Point(372, 26);
            this.ucTxtChangeCurrency.MaxLength = 11;
            this.ucTxtChangeCurrency.Name = "ucTxtChangeCurrency";
            this.ucTxtChangeCurrency.PasswordChar = false;
            this.ucTxtChangeCurrency.placeHolder = "กรอกจำนวนเงิน";
            this.ucTxtChangeCurrency.Readonly = false;
            this.ucTxtChangeCurrency.ShortcutsEnabled = true;
            this.ucTxtChangeCurrency.Size = new System.Drawing.Size(263, 42);
            this.ucTxtChangeCurrency.TabIndex = 10;
            this.ucTxtChangeCurrency.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTxtChangeCurrency.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ucTxtChangeCurrency.EnterFromButton += new System.EventHandler(this.ucTxtChange_EnterFromButton);
            // 
            // ucKeypad1
            // 
            this.ucKeypad1.Location = new System.Drawing.Point(332, 3);
            this.ucKeypad1.Name = "ucKeypad1";
            this.ucKeypad1.Size = new System.Drawing.Size(336, 296);
            this.ucKeypad1.TabIndex = 0;
            this.ucKeypad1.ucTBS = null;
            this.ucKeypad1.ucTBWI = null;
            // 
            // frmConfirmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConfirmPayment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConfirmPayment_FormClosed);
            this.Load += new System.EventHandler(this.frmConfirmPayment_Load);
            this.Shown += new System.EventHandler(this.frmConfirmPayment_Shown);
            this.pn_PaymentNormal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel_button.ResumeLayout(false);
            this.panel_message.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pn_PaymentCurrency.ResumeLayout(false);
            this.pn_PaymentCurrency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel_button2.ResumeLayout(false);
            this.panel_message2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn_PaymentNormal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTxtChange;
        private System.Windows.Forms.Label lbChange;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbTxtPayment;
        private System.Windows.Forms.Label lbPayment;
        private System.Windows.Forms.Label lbTxtCash;
        private System.Windows.Forms.Label lbCash;
        private System.Windows.Forms.Label lbConfirm;
        private System.Windows.Forms.Panel panel_message;
        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_button;
        private System.Windows.Forms.Panel pn_PaymentCurrency;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbTxtChangeCurrency;
        private System.Windows.Forms.Label lbBalanceCurrency;
        private System.Windows.Forms.Label lbPaymentCurrency;
        private System.Windows.Forms.Label lbCashCurrency;
        private System.Windows.Forms.Label lbConfirmCurrency;
        private System.Windows.Forms.Panel panel_button2;
        private System.Windows.Forms.Button btnCancelCurrency;
        private System.Windows.Forms.Button btnOkCurrency;
        private System.Windows.Forms.Panel pn_Currency;
        private System.Windows.Forms.Label lbTxtPaymentCurrency;
        private System.Windows.Forms.Label lbTxtCashCurrency;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel_message2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lb_messageCurrency;
        private System.Windows.Forms.Label lbTextCurrency;
        private System.Windows.Forms.Label lbText;
        public UCTextBoxWithIcon ucTxtChange;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UCKeypad ucKeypad1;
        private System.Windows.Forms.Button btnEditChangeCurrency;
        private System.Windows.Forms.Button btnEditChange;
        public UCTextBoxWithIcon ucTxtChangeCurrency;
        private System.Windows.Forms.Label lbOldChange;
        private System.Windows.Forms.Button btnCancelChange;
        private System.Windows.Forms.Label label1;

    }
}