namespace BJCBCPOS
{
    partial class UCItem
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
            this.lb_ProductCode = new System.Windows.Forms.Label();
            this.lb_ProductName = new System.Windows.Forms.Label();
            this.lb_Amount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_ProductCode
            // 
            this.lb_ProductCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_ProductCode.BackColor = System.Drawing.Color.White;
            this.lb_ProductCode.Font = new System.Drawing.Font("Prompt", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ProductCode.ForeColor = System.Drawing.Color.Black;
            this.lb_ProductCode.Location = new System.Drawing.Point(3, 130);
            this.lb_ProductCode.Name = "lb_ProductCode";
            this.lb_ProductCode.Size = new System.Drawing.Size(217, 27);
            this.lb_ProductCode.TabIndex = 0;
            this.lb_ProductCode.Text = "12345678901234567890";
            this.lb_ProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_ProductCode.Click += new System.EventHandler(this.label1_Click);
            // 
            // lb_ProductName
            // 
            this.lb_ProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_ProductName.BackColor = System.Drawing.Color.White;
            this.lb_ProductName.Font = new System.Drawing.Font("Phetsarath OT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ProductName.ForeColor = System.Drawing.Color.Black;
            this.lb_ProductName.Location = new System.Drawing.Point(3, 1);
            this.lb_ProductName.Name = "lb_ProductName";
            this.lb_ProductName.Size = new System.Drawing.Size(217, 96);
            this.lb_ProductName.TabIndex = 1;
            this.lb_ProductName.Text = "​ເອັມ​ໂຟນ ​\r\nຮີ​ເຟວ​ຄ​ຣາດ\r\n10000ກີບ";
            this.lb_ProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_ProductName.Click += new System.EventHandler(this.lb_ProductName_Click);
            // 
            // lb_Amount
            // 
            this.lb_Amount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Amount.BackColor = System.Drawing.Color.White;
            this.lb_Amount.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Amount.ForeColor = System.Drawing.Color.Black;
            this.lb_Amount.Location = new System.Drawing.Point(3, 99);
            this.lb_Amount.Name = "lb_Amount";
            this.lb_Amount.Size = new System.Drawing.Size(217, 30);
            this.lb_Amount.TabIndex = 2;
            this.lb_Amount.Text = "88,888,888.00";
            this.lb_Amount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Amount.Click += new System.EventHandler(this.lb_Current_Click);
            // 
            // UCItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lb_Amount);
            this.Controls.Add(this.lb_ProductName);
            this.Controls.Add(this.lb_ProductCode);
            this.DoubleBuffered = true;
            this.Name = "UCItem";
            this.Size = new System.Drawing.Size(223, 159);
            this.Load += new System.EventHandler(this.UCItem_Load);
            this.Click += new System.EventHandler(this.UCItem_Click);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_ProductCode;
        public System.Windows.Forms.Label lb_ProductName;
        public System.Windows.Forms.Label lb_Amount;
    }
}
