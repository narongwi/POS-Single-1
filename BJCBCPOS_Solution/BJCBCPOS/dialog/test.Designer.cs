namespace BJCBCPOS
{
    partial class test
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.edcControl1 = new EDCControl.EDCControl();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(171, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // edcControl1
            // 
            this.edcControl1.BackColor = System.Drawing.Color.Silver;
            this.edcControl1.EDCKey = "";
            this.edcControl1.EDCPort = "";
            this.edcControl1.EDCPortSetting = "";
            this.edcControl1.EDCTimeout = 0;
            this.edcControl1.EnableLog = false;
            this.edcControl1.isBreak = false;
            this.edcControl1.Location = new System.Drawing.Point(11, 111);
            this.edcControl1.Margin = new System.Windows.Forms.Padding(2);
            this.edcControl1.Name = "edcControl1";
            this.edcControl1.Size = new System.Drawing.Size(176, 102);
            this.edcControl1.TabIndex = 4;
            this.edcControl1.Tag = "";
            this.edcControl1.Visible = false;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 224);
            this.Controls.Add(this.edcControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "test";
            this.Text = "test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private EDCControl.EDCControl edcControl1;
    }
}