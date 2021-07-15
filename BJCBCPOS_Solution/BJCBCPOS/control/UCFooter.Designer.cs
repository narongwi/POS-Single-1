namespace BJCBCPOS
{
    partial class UCFooter
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
            this.lbFirstLine = new System.Windows.Forms.Label();
            this.lbSecondLine = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbFunction = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFirstLine
            // 
            this.lbFirstLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFirstLine.ForeColor = System.Drawing.Color.Black;
            this.lbFirstLine.Location = new System.Drawing.Point(163, 3);
            this.lbFirstLine.Name = "lbFirstLine";
            this.lbFirstLine.Size = new System.Drawing.Size(824, 15);
            this.lbFirstLine.TabIndex = 0;
            this.lbFirstLine.Text = "Store Code: XXXXXXXX | Store Name: XXXXXXXX | Tax ID: XXXXXXXX | Permission ID: X" +
                "XXXXXXX  | Till No: XXXXXXXX ";
            // 
            // lbSecondLine
            // 
            this.lbSecondLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSecondLine.ForeColor = System.Drawing.Color.Black;
            this.lbSecondLine.Location = new System.Drawing.Point(163, 20);
            this.lbSecondLine.Name = "lbSecondLine";
            this.lbSecondLine.Size = new System.Drawing.Size(827, 15);
            this.lbSecondLine.TabIndex = 1;
            this.lbSecondLine.Text = "Cashier Code and Name: XXXXXXXX | Datetime: XXXXXXXX | Server Name: XXXXXXXX | Da" +
                "tabase Name: XXXXXXXX | Safe Mode: XXXXXXXX | Version: XXXXXXXX\r\n";
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(192)))), ((int)(((byte)(100)))));
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbStatus.Font = new System.Drawing.Font("Prompt", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.White;
            this.lbStatus.Location = new System.Drawing.Point(0, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(157, 23);
            this.lbStatus.TabIndex = 8;
            this.lbStatus.Text = "ON SERVER";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbStatus.FontChanged += new System.EventHandler(this.lb_Status_FontChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.lbFunction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 40);
            this.panel1.TabIndex = 9;
            // 
            // lbFunction
            // 
            this.lbFunction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(192)))), ((int)(((byte)(100)))));
            this.lbFunction.Font = new System.Drawing.Font("Prompt", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbFunction.ForeColor = System.Drawing.Color.White;
            this.lbFunction.Location = new System.Drawing.Point(0, 23);
            this.lbFunction.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lbFunction.Name = "lbFunction";
            this.lbFunction.Size = new System.Drawing.Size(157, 17);
            this.lbFunction.TabIndex = 9;
            this.lbFunction.Text = "000-000-000-000-000";
            this.lbFunction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UCFooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbSecondLine);
            this.Controls.Add(this.lbFirstLine);
            this.DoubleBuffered = true;
            this.Name = "UCFooter";
            this.Size = new System.Drawing.Size(1024, 40);
            this.Load += new System.EventHandler(this.UCFooter_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbFirstLine;
        private System.Windows.Forms.Label lbSecondLine;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lbFunction;
    }
}
