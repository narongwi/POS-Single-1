namespace BJCBCPOS
{
    partial class frmSaleOrderType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaleOrderType));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbHamb_Menu = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.btnSkip = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(25, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 503);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Prompt", 15.75F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(24, 701);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(433, 43);
            this.btnCancel.TabIndex = 133;
            this.btnCancel.Text = "ออก";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbHamb_Menu
            // 
            this.lbHamb_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(186)))), ((int)(((byte)(109)))));
            this.lbHamb_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbHamb_Menu.Font = new System.Drawing.Font("Prompt", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHamb_Menu.ForeColor = System.Drawing.Color.White;
            this.lbHamb_Menu.Location = new System.Drawing.Point(0, 0);
            this.lbHamb_Menu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHamb_Menu.Name = "lbHamb_Menu";
            this.lbHamb_Menu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lbHamb_Menu.Size = new System.Drawing.Size(483, 75);
            this.lbHamb_Menu.TabIndex = 135;
            this.lbHamb_Menu.Text = "ประเภทการขาย";
            this.lbHamb_Menu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHeader
            // 
            this.lbHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(207)))), ((int)(((byte)(155)))));
            this.lbHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbHeader.Font = new System.Drawing.Font("Prompt", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(0, 75);
            this.lbHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lbHeader.Size = new System.Drawing.Size(483, 44);
            this.lbHeader.TabIndex = 136;
            this.lbHeader.Text = "วิธีรับสินค้า";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSkip
            // 
            this.btnSkip.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_224;
            this.btnSkip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSkip.FlatAppearance.BorderSize = 0;
            this.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkip.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSkip.ForeColor = System.Drawing.Color.White;
            this.btnSkip.Location = new System.Drawing.Point(24, 642);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(433, 43);
            this.btnSkip.TabIndex = 134;
            this.btnSkip.Text = "ข้าม";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Visible = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(207)))), ((int)(((byte)(155)))));
            this.panel2.Controls.Add(this.lbHeader);
            this.panel2.Controls.Add(this.lbHamb_Menu);
            this.panel2.Controls.Add(this.btnSkip);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 768);
            this.panel2.TabIndex = 137;
            // 
            // frmSaleOrderType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(742, 768);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSaleOrderType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSaleOrderType";
            this.Load += new System.EventHandler(this.frmSaleOrderType_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbHamb_Menu;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Panel panel2;
    }
}