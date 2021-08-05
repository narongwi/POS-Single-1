namespace BJCBCPOS
{
    partial class frmEditItemReason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditItemReason));
            this.pn_DropDown = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucDDReasonToEdit = new BJCBCPOS.UCDropDownList();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbMenuName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pn_DropDown
            // 
            this.pn_DropDown.AutoScroll = true;
            this.pn_DropDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_DropDown.Location = new System.Drawing.Point(13, -35);
            this.pn_DropDown.Name = "pn_DropDown";
            this.pn_DropDown.Size = new System.Drawing.Size(56, 27);
            this.pn_DropDown.TabIndex = 120;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ucDDReasonToEdit);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lbMenuName);
            this.panel1.Location = new System.Drawing.Point(223, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 305);
            this.panel1.TabIndex = 121;
            // 
            // ucDDReasonToEdit
            // 
            this.ucDDReasonToEdit.AutoScroll = true;
            this.ucDDReasonToEdit.BackColor = System.Drawing.Color.White;
            this.ucDDReasonToEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucDDReasonToEdit.BackgroundImage")));
            this.ucDDReasonToEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucDDReasonToEdit.DisplayText = null;
            this.ucDDReasonToEdit.DropdownExpandRightSide = false;
            this.ucDDReasonToEdit.IsLarge = false;
            this.ucDDReasonToEdit.LabelText = "กรุณาเลือกเหตุผล";
            this.ucDDReasonToEdit.Location = new System.Drawing.Point(67, 122);
            this.ucDDReasonToEdit.lstDDL = null;
            this.ucDDReasonToEdit.Name = "ucDDReasonToEdit";
            this.ucDDReasonToEdit.Size = new System.Drawing.Size(420, 42);
            this.ucDDReasonToEdit.TabIndex = 120;
            this.ucDDReasonToEdit.ValueText = null;
            this.ucDDReasonToEdit.UCDropDownListClick += new System.EventHandler(this.ucDDReasonToEdit_UCDropDownListClick);
            this.ucDDReasonToEdit.UCDropDownGetItemClick += new System.EventHandler(this.ucDDReasonToEdit_UCDropDownGetItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Cancel_Reason;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancel.Location = new System.Drawing.Point(50, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(210, 51);
            this.btnCancel.TabIndex = 119;
            this.btnCancel.Text = "ไม่ใช่";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_OK_Reason;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(296, 211);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(210, 51);
            this.btnOk.TabIndex = 118;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lbMenuName
            // 
            this.lbMenuName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMenuName.Location = new System.Drawing.Point(67, 14);
            this.lbMenuName.Name = "lbMenuName";
            this.lbMenuName.Size = new System.Drawing.Size(420, 82);
            this.lbMenuName.TabIndex = 117;
            this.lbMenuName.Text = "เหตุผลการปรับราคาสินค้า";
            this.lbMenuName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 768);
            this.pictureBox1.TabIndex = 122;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // frmEditItemReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pn_DropDown);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEditItemReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAdjustReason";
            this.Load += new System.EventHandler(this.frmEditReason_Load);
            this.Shown += new System.EventHandler(this.frmEditItemReason_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn_DropDown;
        private System.Windows.Forms.Panel panel1;
        private UCDropDownList ucDDReasonToEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbMenuName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}