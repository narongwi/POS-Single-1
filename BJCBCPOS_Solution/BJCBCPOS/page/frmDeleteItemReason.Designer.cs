namespace BJCBCPOS
{
    partial class frmDeleteItemReason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteItemReason));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucDDReasonToDelete = new BJCBCPOS.UCDropDownList();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbConfirm = new System.Windows.Forms.Label();
            this.pn_DropDown = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ucDDReasonToDelete);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lbConfirm);
            this.panel1.Location = new System.Drawing.Point(223, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 305);
            this.panel1.TabIndex = 119;
            // 
            // ucDDReasonToDelete
            // 
            this.ucDDReasonToDelete.AutoScroll = true;
            this.ucDDReasonToDelete.BackColor = System.Drawing.Color.White;
            this.ucDDReasonToDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucDDReasonToDelete.BackgroundImage")));
            this.ucDDReasonToDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucDDReasonToDelete.DisplayText = null;
            this.ucDDReasonToDelete.DropdownExpandRightSide = false;
            this.ucDDReasonToDelete.IsLarge = false;
            this.ucDDReasonToDelete.LabelText = "กรุณาเลือกเหตุผล";
            this.ucDDReasonToDelete.Location = new System.Drawing.Point(72, 122);
            this.ucDDReasonToDelete.lstDDL = null;
            this.ucDDReasonToDelete.Name = "ucDDReasonToDelete";
            this.ucDDReasonToDelete.Size = new System.Drawing.Size(420, 42);
            this.ucDDReasonToDelete.TabIndex = 120;
            this.ucDDReasonToDelete.ValueText = null;
            this.ucDDReasonToDelete.UCDropDownListClick += new System.EventHandler(this.ucDDReasonToDelete_UCDropDownListClick);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BJCBCPOS.Properties.Resources.btn_Cancel_Reason;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnOk.Font = new System.Drawing.Font("Prompt", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(296, 211);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(210, 51);
            this.btnOk.TabIndex = 118;
            this.btnOk.Text = "ใช่";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lbConfirm
            // 
            this.lbConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirm.Location = new System.Drawing.Point(67, 14);
            this.lbConfirm.Name = "lbConfirm";
            this.lbConfirm.Size = new System.Drawing.Size(420, 82);
            this.lbConfirm.TabIndex = 117;
            this.lbConfirm.Text = "เหตุผลลบรายการสินค้า";
            this.lbConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pn_DropDown
            // 
            this.pn_DropDown.AutoScroll = true;
            this.pn_DropDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_DropDown.Location = new System.Drawing.Point(0, -102);
            this.pn_DropDown.Name = "pn_DropDown";
            this.pn_DropDown.Size = new System.Drawing.Size(200, 100);
            this.pn_DropDown.TabIndex = 121;
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
            // frmDeleteItemReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pn_DropDown);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDeleteItemReason";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDeleteItemReason";
            this.Load += new System.EventHandler(this.frmDeleteItemReason_Load);
            this.Shown += new System.EventHandler(this.frmDeleteItemReason_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UCDropDownList ucDDReasonToDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbConfirm;
        private System.Windows.Forms.Panel pn_DropDown;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}