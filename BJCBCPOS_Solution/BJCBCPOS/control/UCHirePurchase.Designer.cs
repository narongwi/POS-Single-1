namespace BJCBCPOS
{
    partial class UCHirePurchase
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCHirePurchase));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucInstallment = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbClear = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbDownPaymentAmt = new System.Windows.Forms.Label();
            this.lbDownPayment = new System.Windows.Forms.Label();
            this.lbPrPriceAmt = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ucContractNumber = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbContractNumber = new System.Windows.Forms.Label();
            this.lbPrPrice = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.lbInstallment = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ucInstallment);
            this.panel1.Controls.Add(this.lbClear);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.lbDownPaymentAmt);
            this.panel1.Controls.Add(this.lbDownPayment);
            this.panel1.Controls.Add(this.lbPrPriceAmt);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.ucContractNumber);
            this.panel1.Controls.Add(this.lbContractNumber);
            this.panel1.Controls.Add(this.lbPrPrice);
            this.panel1.Controls.Add(this.lbHeader);
            this.panel1.Controls.Add(this.lbInstallment);
            this.panel1.Location = new System.Drawing.Point(67, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 455);
            this.panel1.TabIndex = 1;
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
            this.ucInstallment.Location = new System.Drawing.Point(243, 136);
            this.ucInstallment.MaxLength = 32767;
            this.ucInstallment.Name = "ucInstallment";
            this.ucInstallment.PasswordChar = false;
            this.ucInstallment.placeHolder = "Input hire purchase amount";
            this.ucInstallment.Readonly = false;
            this.ucInstallment.ShortcutsEnabled = true;
            this.ucInstallment.Size = new System.Drawing.Size(279, 42);
            this.ucInstallment.TabIndex = 102;
            this.ucInstallment.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucInstallment.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucInstallment.EnterFromButton += new System.EventHandler(this.ucInstallment_EnterFromButton);
            this.ucInstallment.TextBoxLeave += new System.EventHandler(this.ucInstallment_TextBoxLeave);
            // 
            // lbClear
            // 
            this.lbClear.AutoSize = true;
            this.lbClear.Font = new System.Drawing.Font("Prompt", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbClear.ForeColor = System.Drawing.Color.Black;
            this.lbClear.Location = new System.Drawing.Point(504, 434);
            this.lbClear.Name = "lbClear";
            this.lbClear.Size = new System.Drawing.Size(39, 18);
            this.lbClear.TabIndex = 115;
            this.lbClear.Text = "Clear";
            this.lbClear.Click += new System.EventHandler(this.lbClear_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_clean;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(505, 399);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 35);
            this.btnClear.TabIndex = 114;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbDownPaymentAmt
            // 
            this.lbDownPaymentAmt.BackColor = System.Drawing.Color.White;
            this.lbDownPaymentAmt.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownPaymentAmt.ForeColor = System.Drawing.Color.Black;
            this.lbDownPaymentAmt.Location = new System.Drawing.Point(243, 284);
            this.lbDownPaymentAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDownPaymentAmt.Name = "lbDownPaymentAmt";
            this.lbDownPaymentAmt.Size = new System.Drawing.Size(279, 33);
            this.lbDownPaymentAmt.TabIndex = 111;
            this.lbDownPaymentAmt.Text = "0.00";
            this.lbDownPaymentAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDownPayment
            // 
            this.lbDownPayment.BackColor = System.Drawing.Color.White;
            this.lbDownPayment.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownPayment.ForeColor = System.Drawing.Color.Black;
            this.lbDownPayment.Location = new System.Drawing.Point(29, 284);
            this.lbDownPayment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDownPayment.Name = "lbDownPayment";
            this.lbDownPayment.Size = new System.Drawing.Size(204, 33);
            this.lbDownPayment.TabIndex = 110;
            this.lbDownPayment.Text = "Down payment :";
            this.lbDownPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPrPriceAmt
            // 
            this.lbPrPriceAmt.BackColor = System.Drawing.Color.White;
            this.lbPrPriceAmt.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrPriceAmt.ForeColor = System.Drawing.Color.Black;
            this.lbPrPriceAmt.Location = new System.Drawing.Point(241, 82);
            this.lbPrPriceAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPrPriceAmt.Name = "lbPrPriceAmt";
            this.lbPrPriceAmt.Size = new System.Drawing.Size(279, 33);
            this.lbPrPriceAmt.TabIndex = 109;
            this.lbPrPriceAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel1;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(71, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 62);
            this.btnCancel.TabIndex = 108;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(304, 346);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(180, 62);
            this.btnOK.TabIndex = 107;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.ucContractNumber.Location = new System.Drawing.Point(243, 209);
            this.ucContractNumber.MaxLength = 16;
            this.ucContractNumber.Name = "ucContractNumber";
            this.ucContractNumber.PasswordChar = false;
            this.ucContractNumber.placeHolder = "Input contract number";
            this.ucContractNumber.Readonly = false;
            this.ucContractNumber.ShortcutsEnabled = true;
            this.ucContractNumber.Size = new System.Drawing.Size(279, 42);
            this.ucContractNumber.TabIndex = 106;
            this.ucContractNumber.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucContractNumber.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ucContractNumber.TextBoxLeave += new System.EventHandler(this.ucContractNumber_TextBoxLeave);
            this.ucContractNumber.Enter += new System.EventHandler(this.ucContractNumber_Enter);
            // 
            // lbContractNumber
            // 
            this.lbContractNumber.BackColor = System.Drawing.Color.White;
            this.lbContractNumber.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContractNumber.ForeColor = System.Drawing.Color.Black;
            this.lbContractNumber.Location = new System.Drawing.Point(29, 213);
            this.lbContractNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbContractNumber.Name = "lbContractNumber";
            this.lbContractNumber.Size = new System.Drawing.Size(204, 33);
            this.lbContractNumber.TabIndex = 105;
            this.lbContractNumber.Text = "Contract No :";
            this.lbContractNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPrPrice
            // 
            this.lbPrPrice.BackColor = System.Drawing.Color.White;
            this.lbPrPrice.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrPrice.ForeColor = System.Drawing.Color.Black;
            this.lbPrPrice.Location = new System.Drawing.Point(29, 82);
            this.lbPrPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPrPrice.Name = "lbPrPrice";
            this.lbPrPrice.Size = new System.Drawing.Size(204, 33);
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
            this.lbInstallment.Location = new System.Drawing.Point(7, 140);
            this.lbInstallment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInstallment.Name = "lbInstallment";
            this.lbInstallment.Size = new System.Drawing.Size(226, 33);
            this.lbInstallment.TabIndex = 101;
            this.lbInstallment.Text = "Hire Purchase amount :";
            this.lbInstallment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCHirePurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCHirePurchase";
            this.Size = new System.Drawing.Size(688, 725);
            this.Load += new System.EventHandler(this.UCHirePurchase_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lbDownPaymentAmt;
        private System.Windows.Forms.Label lbDownPayment;
        public System.Windows.Forms.Label lbPrPriceAmt;
        public System.Windows.Forms.Button btnCancel;
        public UCTextBoxWithIcon ucContractNumber;
        private System.Windows.Forms.Label lbContractNumber;
        private System.Windows.Forms.Label lbPrPrice;
        public System.Windows.Forms.Label lbHeader;
        public UCTextBoxWithIcon ucInstallment;
        private System.Windows.Forms.Label lbInstallment;
        private System.Windows.Forms.Label lbClear;
        private System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnOK;
    }
}
