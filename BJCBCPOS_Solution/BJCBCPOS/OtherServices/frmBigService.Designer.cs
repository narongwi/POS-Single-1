namespace BJCBCPOS.OtherServices {
  partial class frmBigService {
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
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.ForeColor = System.Drawing.Color.Coral;
            this.panel1.Location = new System.Drawing.Point(-2, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 685);
            this.panel1.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.label1.Location = new System.Drawing.Point(435, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 20);
            this.label1.TabIndex = 89;
            this.label1.Text = "Select a service item";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(140)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(383, 38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(255, 51);
            this.lblTitle.TabIndex = 80;
            this.lblTitle.Text = "Big Services";
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(141)))), ((int)(((byte)(71)))));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(271, 146);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(539, 514);
            this.listView1.TabIndex = 79;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "เมนูหลัก";
            this.ucHeader1.currentMenuTitle2 = "Big Service";
            this.ucHeader1.currentMenuTitle3 = "";
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = true;
            this.ucHeader1.showCurrentMenuText = true;
            this.ucHeader1.showHamberGetItm = true;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = true;
            this.ucHeader1.showLockScreen = true;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = true;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showMember_ButtonBack = true;
            this.ucHeader1.showMember_IsSaveMember = true;
            this.ucHeader1.showScanner = true;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 73;
            // 
            // frmBigService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucFooter1);
            this.Controls.Add(this.ucHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBigService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BigService";
            this.Load += new System.EventHandler(this.BigService_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
        private UCFooter ucFooter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private UCHeader ucHeader1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
    }
}
