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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBigService));
            this.grpService = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textMenuID = new System.Windows.Forms.TextBox();
            this.textMenuName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpService.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpService
            // 
            this.grpService.Controls.Add(this.listView1);
            this.grpService.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpService.Location = new System.Drawing.Point(212, 73);
            this.grpService.Name = "grpService";
            this.grpService.Size = new System.Drawing.Size(587, 634);
            this.grpService.TabIndex = 67;
            this.grpService.TabStop = false;
            this.grpService.Text = "บิ๊กเซอร์วิส / Big Service";
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
            this.listView1.Location = new System.Drawing.Point(6, 34);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(575, 594);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // textMenuID
            // 
            this.textMenuID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textMenuID.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textMenuID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textMenuID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMenuID.Location = new System.Drawing.Point(500, 713);
            this.textMenuID.Name = "textMenuID";
            this.textMenuID.Size = new System.Drawing.Size(61, 19);
            this.textMenuID.TabIndex = 68;
            // 
            // textMenuName
            // 
            this.textMenuName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textMenuName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textMenuName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textMenuName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMenuName.Location = new System.Drawing.Point(567, 713);
            this.textMenuName.Name = "textMenuName";
            this.textMenuName.Size = new System.Drawing.Size(232, 19);
            this.textMenuName.TabIndex = 69;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(212, 713);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 33);
            this.button1.TabIndex = 70;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BigService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textMenuID);
            this.Controls.Add(this.textMenuName);
            this.Controls.Add(this.grpService);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BigService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BigService";
            this.Load += new System.EventHandler(this.BigService_Load);
            this.grpService.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox grpService;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.TextBox textMenuID;
    private System.Windows.Forms.TextBox textMenuName;
        private System.Windows.Forms.Button button1;
    }
}
