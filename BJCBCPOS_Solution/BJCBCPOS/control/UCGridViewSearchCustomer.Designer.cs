namespace BJCBCPOS
{
    public partial class UCGridViewSearchCustomer
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
            this.lb_MemberCard = new System.Windows.Forms.Label();
            this.lb_Name = new System.Windows.Forms.Label();
            this.lb_MemberID = new System.Windows.Forms.Label();
            this.lb_IDCard = new System.Windows.Forms.Label();
            this.lb_Tel = new System.Windows.Forms.Label();
            this.lb_TempName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_MemberCard
            // 
            this.lb_MemberCard.AutoSize = true;
            this.lb_MemberCard.BackColor = System.Drawing.Color.Transparent;
            this.lb_MemberCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_MemberCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lb_MemberCard.Location = new System.Drawing.Point(13, 4);
            this.lb_MemberCard.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_MemberCard.Name = "lb_MemberCard";
            this.lb_MemberCard.Size = new System.Drawing.Size(136, 18);
            this.lb_MemberCard.TabIndex = 49;
            this.lb_MemberCard.Text = "1234567890123456";
            this.lb_MemberCard.Click += new System.EventHandler(this.lb_MemberCard_Click);
            // 
            // lb_Name
            // 
            this.lb_Name.AutoSize = true;
            this.lb_Name.BackColor = System.Drawing.Color.Transparent;
            this.lb_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lb_Name.Location = new System.Drawing.Point(13, 28);
            this.lb_Name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(227, 20);
            this.lb_Name.TabIndex = 50;
            this.lb_Name.Text = "Member Name And Last Name";
            this.lb_Name.Click += new System.EventHandler(this.lb_Name_Click);
            // 
            // lb_MemberID
            // 
            this.lb_MemberID.AutoSize = true;
            this.lb_MemberID.BackColor = System.Drawing.Color.Transparent;
            this.lb_MemberID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_MemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lb_MemberID.Location = new System.Drawing.Point(173, 4);
            this.lb_MemberID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_MemberID.Name = "lb_MemberID";
            this.lb_MemberID.Size = new System.Drawing.Size(136, 18);
            this.lb_MemberID.TabIndex = 51;
            this.lb_MemberID.Text = "1234567890123456";
            this.lb_MemberID.Click += new System.EventHandler(this.lb_MemberID_Click);
            // 
            // lb_IDCard
            // 
            this.lb_IDCard.AutoSize = true;
            this.lb_IDCard.BackColor = System.Drawing.Color.Transparent;
            this.lb_IDCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_IDCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lb_IDCard.Location = new System.Drawing.Point(327, 4);
            this.lb_IDCard.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_IDCard.Name = "lb_IDCard";
            this.lb_IDCard.Size = new System.Drawing.Size(112, 18);
            this.lb_IDCard.TabIndex = 52;
            this.lb_IDCard.Text = "1234567890123";
            this.lb_IDCard.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_IDCard.Click += new System.EventHandler(this.lb_IDCard_Click);
            // 
            // lb_Tel
            // 
            this.lb_Tel.AutoSize = true;
            this.lb_Tel.BackColor = System.Drawing.Color.Transparent;
            this.lb_Tel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Tel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lb_Tel.Location = new System.Drawing.Point(477, 4);
            this.lb_Tel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Tel.Name = "lb_Tel";
            this.lb_Tel.Size = new System.Drawing.Size(88, 18);
            this.lb_Tel.TabIndex = 53;
            this.lb_Tel.Text = "1234567890";
            this.lb_Tel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_Tel.Click += new System.EventHandler(this.lb_Tel_Click);
            // 
            // lb_TempName
            // 
            this.lb_TempName.AutoSize = true;
            this.lb_TempName.Location = new System.Drawing.Point(480, 39);
            this.lb_TempName.Name = "lb_TempName";
            this.lb_TempName.Size = new System.Drawing.Size(35, 13);
            this.lb_TempName.TabIndex = 54;
            this.lb_TempName.Text = "label1";
            this.lb_TempName.Visible = false;
            // 
            // UCGridViewSearchCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lb_TempName);
            this.Controls.Add(this.lb_Tel);
            this.Controls.Add(this.lb_IDCard);
            this.Controls.Add(this.lb_MemberID);
            this.Controls.Add(this.lb_Name);
            this.Controls.Add(this.lb_MemberCard);
            this.DoubleBuffered = true;
            this.Name = "UCGridViewSearchCustomer";
            this.Size = new System.Drawing.Size(570, 55);
            this.Load += new System.EventHandler(this.UCGridViewSearchCustomer_Load);
            this.Click += new System.EventHandler(this.UCGridViewSearchCustomer_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_MemberCard;
        public System.Windows.Forms.Label lb_Name;
        public System.Windows.Forms.Label lb_MemberID;
        public System.Windows.Forms.Label lb_IDCard;
        public System.Windows.Forms.Label lb_Tel;
        public System.Windows.Forms.Label lb_TempName;

    }
}
