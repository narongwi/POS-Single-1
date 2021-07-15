namespace BJCBCPOS
{
    partial class frmHirePurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHirePurchase));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbDownPaymentAmt = new System.Windows.Forms.Label();
            this.lbDownPayment = new System.Windows.Forms.Label();
            this.lbPrPriceAmt = new System.Windows.Forms.Label();
            this.lbContractNumber = new System.Windows.Forms.Label();
            this.lbPrPrice = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.lbInstallment = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.ucContractNumber = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucInstallment = new BJCBCPOS.UCTextBoxWithIcon();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lbDownPaymentAmt);
            this.panel1.Controls.Add(this.lbDownPayment);
            this.panel1.Controls.Add(this.lbPrPriceAmt);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnAccept);
            this.panel1.Controls.Add(this.ucContractNumber);
            this.panel1.Controls.Add(this.lbContractNumber);
            this.panel1.Controls.Add(this.lbPrPrice);
            this.panel1.Controls.Add(this.lbHeader);
            this.panel1.Controls.Add(this.ucInstallment);
            this.panel1.Controls.Add(this.lbInstallment);
            this.panel1.Location = new System.Drawing.Point(68, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 455);
            this.panel1.TabIndex = 0;
            // 
            // lbDownPaymentAmt
            // 
            this.lbDownPaymentAmt.BackColor = System.Drawing.Color.White;
            this.lbDownPaymentAmt.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownPaymentAmt.ForeColor = System.Drawing.Color.Black;
            this.lbDownPaymentAmt.Location = new System.Drawing.Point(194, 288);
            this.lbDownPaymentAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDownPaymentAmt.Name = "lbDownPaymentAmt";
            this.lbDownPaymentAmt.Size = new System.Drawing.Size(306, 33);
            this.lbDownPaymentAmt.TabIndex = 111;
            this.lbDownPaymentAmt.Text = "0.00";
            this.lbDownPaymentAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDownPayment
            // 
            this.lbDownPayment.BackColor = System.Drawing.Color.White;
            this.lbDownPayment.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownPayment.ForeColor = System.Drawing.Color.Black;
            this.lbDownPayment.Location = new System.Drawing.Point(26, 288);
            this.lbDownPayment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDownPayment.Name = "lbDownPayment";
            this.lbDownPayment.Size = new System.Drawing.Size(164, 33);
            this.lbDownPayment.TabIndex = 110;
            this.lbDownPayment.Text = "Down payment :";
            this.lbDownPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPrPriceAmt
            // 
            this.lbPrPriceAmt.BackColor = System.Drawing.Color.White;
            this.lbPrPriceAmt.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrPriceAmt.ForeColor = System.Drawing.Color.Black;
            this.lbPrPriceAmt.Location = new System.Drawing.Point(190, 86);
            this.lbPrPriceAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPrPriceAmt.Name = "lbPrPriceAmt";
            this.lbPrPriceAmt.Size = new System.Drawing.Size(278, 33);
            this.lbPrPriceAmt.TabIndex = 109;
            this.lbPrPriceAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbContractNumber
            // 
            this.lbContractNumber.BackColor = System.Drawing.Color.White;
            this.lbContractNumber.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContractNumber.ForeColor = System.Drawing.Color.Black;
            this.lbContractNumber.Location = new System.Drawing.Point(26, 217);
            this.lbContractNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbContractNumber.Name = "lbContractNumber";
            this.lbContractNumber.Size = new System.Drawing.Size(164, 33);
            this.lbContractNumber.TabIndex = 105;
            this.lbContractNumber.Text = "Contract No :";
            this.lbContractNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPrPrice
            // 
            this.lbPrPrice.BackColor = System.Drawing.Color.White;
            this.lbPrPrice.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrPrice.ForeColor = System.Drawing.Color.Black;
            this.lbPrPrice.Location = new System.Drawing.Point(26, 86);
            this.lbPrPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPrPrice.Name = "lbPrPrice";
            this.lbPrPrice.Size = new System.Drawing.Size(164, 33);
            this.lbPrPrice.TabIndex = 104;
            this.lbPrPrice.Text = "Product Price :";
            this.lbPrPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHeader
            // 
            this.lbHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.lbHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbHeader.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(0, 0);
            this.lbHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(547, 64);
            this.lbHeader.TabIndex = 103;
            this.lbHeader.Text = "Hire Purchaes";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInstallment
            // 
            this.lbInstallment.BackColor = System.Drawing.Color.White;
            this.lbInstallment.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInstallment.ForeColor = System.Drawing.Color.Black;
            this.lbInstallment.Location = new System.Drawing.Point(26, 144);
            this.lbInstallment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInstallment.Name = "lbInstallment";
            this.lbInstallment.Size = new System.Drawing.Size(164, 33);
            this.lbInstallment.TabIndex = 101;
            this.lbInstallment.Text = "Installment :";
            this.lbInstallment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_clean;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(499, 409);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(42, 40);
            this.btnClear.TabIndex = 114;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.btnBack.Location = new System.Drawing.Point(4, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(53, 56);
            this.btnBack.TabIndex = 113;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel1;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReset.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnReset.Location = new System.Drawing.Point(69, 350);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(180, 62);
            this.btnReset.TabIndex = 108;
            this.btnReset.Text = "Cancel";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.Location = new System.Drawing.Point(302, 350);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(180, 62);
            this.btnAccept.TabIndex = 107;
            this.btnAccept.Text = "OK";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // ucContractNumber
            // 
            this.ucContractNumber.BackColor = System.Drawing.Color.White;
            this.ucContractNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucContractNumber.BackgroundImage")));
            this.ucContractNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucContractNumber.EnabledUC = true;
            this.ucContractNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucContractNumber.IsAmount = false;
            this.ucContractNumber.IsLarge = false;
            this.ucContractNumber.IsNumber = false;
            this.ucContractNumber.IsSetFormat = false;
            this.ucContractNumber.IsValidate = false;
            this.ucContractNumber.Location = new System.Drawing.Point(195, 213);
            this.ucContractNumber.MaxLength = 16;
            this.ucContractNumber.Name = "ucContractNumber";
            this.ucContractNumber.PasswordChar = false;
            this.ucContractNumber.placeHolder = "Input contract number";
            this.ucContractNumber.Readonly = false;
            this.ucContractNumber.ShortcutsEnabled = true;
            this.ucContractNumber.Size = new System.Drawing.Size(305, 42);
            this.ucContractNumber.TabIndex = 106;
            this.ucContractNumber.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucContractNumber.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucContractNumber.EnterFromButton += new System.EventHandler(this.ucContractNumber_EnterFromButton);
            this.ucContractNumber.TextBoxLeave += new System.EventHandler(this.ucContractNumber_TextBoxLeave);
            this.ucContractNumber.Leave += new System.EventHandler(this.ucContractNumber_Leave);
            // 
            // ucInstallment
            // 
            this.ucInstallment.BackColor = System.Drawing.Color.White;
            this.ucInstallment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucInstallment.BackgroundImage")));
            this.ucInstallment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucInstallment.EnabledUC = true;
            this.ucInstallment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucInstallment.IsAmount = false;
            this.ucInstallment.IsLarge = false;
            this.ucInstallment.IsNumber = false;
            this.ucInstallment.IsSetFormat = false;
            this.ucInstallment.IsValidate = false;
            this.ucInstallment.Location = new System.Drawing.Point(195, 140);
            this.ucInstallment.MaxLength = 32767;
            this.ucInstallment.Name = "ucInstallment";
            this.ucInstallment.PasswordChar = false;
            this.ucInstallment.placeHolder = "Input installment amount";
            this.ucInstallment.Readonly = false;
            this.ucInstallment.ShortcutsEnabled = true;
            this.ucInstallment.Size = new System.Drawing.Size(305, 42);
            this.ucInstallment.TabIndex = 102;
            this.ucInstallment.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucInstallment.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucInstallment.EnterFromButton += new System.EventHandler(this.ucInstallment_EnterFromButton);
            this.ucInstallment.TextBoxLeave += new System.EventHandler(this.ucInstallment_TextBoxLeave);
            this.ucInstallment.Leave += new System.EventHandler(this.ucInstallment_Leave);
            // 
            // frmHirePurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 725);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHirePurchase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmHirePurchase";
            this.Load += new System.EventHandler(this.frmHirePurchase_Load);
            this.Leave += new System.EventHandler(this.frmHirePurchase_Leave);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbHeader;
        private UCTextBoxWithIcon ucInstallment;
        private System.Windows.Forms.Label lbInstallment;
        private System.Windows.Forms.Label lbContractNumber;
        private System.Windows.Forms.Label lbPrPrice;
        private UCTextBoxWithIcon ucContractNumber;
        public System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lbPrPriceAmt;
        public System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lbDownPaymentAmt;
        private System.Windows.Forms.Label lbDownPayment;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnClear;
    }
}