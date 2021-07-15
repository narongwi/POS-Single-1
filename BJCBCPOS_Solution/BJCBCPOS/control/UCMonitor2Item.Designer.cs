namespace BJCBCPOS
{
    partial class UCMonitor2Item
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
            this.lb_AMT = new System.Windows.Forms.Label();
            this.lb_QTY = new System.Windows.Forms.Label();
            this.lb_ITEM = new System.Windows.Forms.Label();
            this.lbNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_AMT
            // 
            this.lb_AMT.BackColor = System.Drawing.Color.Transparent;
            this.lb_AMT.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_AMT.ForeColor = System.Drawing.Color.Black;
            this.lb_AMT.Location = new System.Drawing.Point(202, 0);
            this.lb_AMT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_AMT.Name = "lb_AMT";
            this.lb_AMT.Size = new System.Drawing.Size(95, 30);
            this.lb_AMT.TabIndex = 62;
            this.lb_AMT.Text = "8,888,888.00";
            this.lb_AMT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_QTY
            // 
            this.lb_QTY.BackColor = System.Drawing.Color.Transparent;
            this.lb_QTY.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_QTY.ForeColor = System.Drawing.Color.Black;
            this.lb_QTY.Location = new System.Drawing.Point(136, 0);
            this.lb_QTY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_QTY.Name = "lb_QTY";
            this.lb_QTY.Size = new System.Drawing.Size(63, 30);
            this.lb_QTY.TabIndex = 61;
            this.lb_QTY.Text = "999.999";
            this.lb_QTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_ITEM
            // 
            this.lb_ITEM.BackColor = System.Drawing.Color.Transparent;
            this.lb_ITEM.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ITEM.ForeColor = System.Drawing.Color.Black;
            this.lb_ITEM.Location = new System.Drawing.Point(-2, 0);
            this.lb_ITEM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_ITEM.Name = "lb_ITEM";
            this.lb_ITEM.Size = new System.Drawing.Size(134, 30);
            this.lb_ITEM.TabIndex = 60;
            this.lb_ITEM.Text = "โค้กกระป๋อง รสอริจินัล";
            this.lb_ITEM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbNo
            // 
            this.lbNo.BackColor = System.Drawing.Color.Transparent;
            this.lbNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
            this.lbNo.Location = new System.Drawing.Point(7, 1);
            this.lbNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNo.Name = "lbNo";
            this.lbNo.Size = new System.Drawing.Size(47, 16);
            this.lbNo.TabIndex = 63;
            this.lbNo.Text = "1";
            this.lbNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNo.Visible = false;
            // 
            // UCMonitor2Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lb_AMT);
            this.Controls.Add(this.lb_QTY);
            this.Controls.Add(this.lb_ITEM);
            this.Controls.Add(this.lbNo);
            this.DoubleBuffered = true;
            this.Name = "UCMonitor2Item";
            this.Size = new System.Drawing.Size(300, 30);
            this.Load += new System.EventHandler(this.UCMonitor2Item_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_AMT;
        public System.Windows.Forms.Label lb_QTY;
        public System.Windows.Forms.Label lb_ITEM;
        public System.Windows.Forms.Label lbNo;
    }
}
