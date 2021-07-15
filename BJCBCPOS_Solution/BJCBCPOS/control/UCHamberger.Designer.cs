namespace BJCBCPOS
{
    partial class UCHamberger
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHamb_Back = new System.Windows.Forms.Button();
            this.pn_HambergerItem = new System.Windows.Forms.Panel();
            this.lbHamb_Menu = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.btnHamb_Back);
            this.panel1.Controls.Add(this.pn_HambergerItem);
            this.panel1.Controls.Add(this.lbHamb_Menu);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 725);
            this.panel1.TabIndex = 0;
            // 
            // btnHamb_Back
            // 
            this.btnHamb_Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(186)))), ((int)(((byte)(109)))));
            this.btnHamb_Back.FlatAppearance.BorderSize = 0;
            this.btnHamb_Back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnHamb_Back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnHamb_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHamb_Back.Image = global::BJCBCPOS.Properties.Resources.arrow_white_left;
            this.btnHamb_Back.Location = new System.Drawing.Point(302, 11);
            this.btnHamb_Back.Name = "btnHamb_Back";
            this.btnHamb_Back.Size = new System.Drawing.Size(53, 56);
            this.btnHamb_Back.TabIndex = 86;
            this.btnHamb_Back.UseVisualStyleBackColor = false;
            this.btnHamb_Back.Click += new System.EventHandler(this.picBack_Click);
            // 
            // pn_HambergerItem
            // 
            this.pn_HambergerItem.BackColor = System.Drawing.Color.White;
            this.pn_HambergerItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pn_HambergerItem.Location = new System.Drawing.Point(0, 81);
            this.pn_HambergerItem.Name = "pn_HambergerItem";
            this.pn_HambergerItem.Size = new System.Drawing.Size(362, 644);
            this.pn_HambergerItem.TabIndex = 85;
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
            this.lbHamb_Menu.Size = new System.Drawing.Size(362, 78);
            this.lbHamb_Menu.TabIndex = 84;
            this.lbHamb_Menu.Text = "Menu";
            this.lbHamb_Menu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCHamberger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCHamberger";
            this.Size = new System.Drawing.Size(362, 725);
            this.Load += new System.EventHandler(this.UCHamberger_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbHamb_Menu;
        public System.Windows.Forms.Panel pn_HambergerItem;
        private System.Windows.Forms.Button btnHamb_Back;
    }
}
