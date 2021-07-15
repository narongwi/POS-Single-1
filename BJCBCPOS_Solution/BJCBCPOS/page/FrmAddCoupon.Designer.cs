namespace BJCBCPOS
{
    partial class frmAddCoupon
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
            this.lbCounponNo = new System.Windows.Forms.Label();
            this.lbCounponValue = new System.Windows.Forms.Label();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbProductCode = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCounponNo
            // 
            this.lbCounponNo.BackColor = System.Drawing.Color.Transparent;
            this.lbCounponNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCounponNo.ForeColor = System.Drawing.Color.Gray;
            this.lbCounponNo.Location = new System.Drawing.Point(14, 12);
            this.lbCounponNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCounponNo.Name = "lbCounponNo";
            this.lbCounponNo.Size = new System.Drawing.Size(108, 24);
            this.lbCounponNo.TabIndex = 97;
            this.lbCounponNo.Text = "เลขที่คูปอง";
            this.lbCounponNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCounponValue
            // 
            this.lbCounponValue.BackColor = System.Drawing.Color.Transparent;
            this.lbCounponValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCounponValue.ForeColor = System.Drawing.Color.Gray;
            this.lbCounponValue.Location = new System.Drawing.Point(183, 12);
            this.lbCounponValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCounponValue.Name = "lbCounponValue";
            this.lbCounponValue.Size = new System.Drawing.Size(97, 24);
            this.lbCounponValue.TabIndex = 98;
            this.lbCounponValue.Text = "มูลค่าคูปอง";
            this.lbCounponValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbQty
            // 
            this.lbQty.BackColor = System.Drawing.Color.Transparent;
            this.lbQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQty.ForeColor = System.Drawing.Color.Gray;
            this.lbQty.Location = new System.Drawing.Point(307, 12);
            this.lbQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(71, 24);
            this.lbQty.TabIndex = 99;
            this.lbQty.Text = "จำนวน";
            this.lbQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbProductCode
            // 
            this.lbProductCode.BackColor = System.Drawing.Color.Transparent;
            this.lbProductCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductCode.ForeColor = System.Drawing.Color.Gray;
            this.lbProductCode.Location = new System.Drawing.Point(426, 12);
            this.lbProductCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProductCode.Name = "lbProductCode";
            this.lbProductCode.Size = new System.Drawing.Size(108, 24);
            this.lbProductCode.TabIndex = 100;
            this.lbProductCode.Text = "รหัสสินค้า";
            this.lbProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 161);
            this.panel1.TabIndex = 102;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lbCounponNo);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.lbCounponValue);
            this.panel2.Controls.Add(this.lbProductCode);
            this.panel2.Controls.Add(this.lbQty);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 271);
            this.panel2.TabIndex = 103;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::BJCBCPOS.Properties.Resources.button_ok;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(165, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(253, 53);
            this.button1.TabIndex = 103;
            this.button1.Text = "ตกลง";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmAddCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(567, 271);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddCoupon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmAddCoupon";
            this.Load += new System.EventHandler(this.frmAddCoupon_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbCounponNo;
        private System.Windows.Forms.Label lbCounponValue;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbProductCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
    }
}