namespace BJCBCPOS
{
    partial class frmConnectionLost
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
            this.pnLoading = new System.Windows.Forms.Panel();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lbLoading = new System.Windows.Forms.Label();
            this.pnConnectionLostStandAlone = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnEnterStanAlone = new System.Windows.Forms.Button();
            this.lbConnectionLostStandAlone = new System.Windows.Forms.Label();
            this.pnReconnect = new System.Windows.Forms.Panel();
            this.btnSuccess = new System.Windows.Forms.Button();
            this.lbReconnect = new System.Windows.Forms.Label();
            this.pnCommandTimeout = new System.Windows.Forms.Panel();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lbCommandTimeout = new System.Windows.Forms.Label();
            this.pnStandAlone = new System.Windows.Forms.Panel();
            this.btnSuccessStandAlone = new System.Windows.Forms.Button();
            this.lbStandAlone = new System.Windows.Forms.Label();
            this.pnConnectionLostNotStandAlone = new System.Windows.Forms.Panel();
            this.btnTryToConnect = new System.Windows.Forms.Button();
            this.btnExit2 = new System.Windows.Forms.Button();
            this.lbConnectionLostNotStandAlone = new System.Windows.Forms.Label();
            this.pnLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.pnConnectionLostStandAlone.SuspendLayout();
            this.pnReconnect.SuspendLayout();
            this.pnCommandTimeout.SuspendLayout();
            this.pnStandAlone.SuspendLayout();
            this.pnConnectionLostNotStandAlone.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnLoading
            // 
            this.pnLoading.Controls.Add(this.picLoading);
            this.pnLoading.Controls.Add(this.lbLoading);
            this.pnLoading.Location = new System.Drawing.Point(256, 192);
            this.pnLoading.Name = "pnLoading";
            this.pnLoading.Size = new System.Drawing.Size(512, 331);
            this.pnLoading.TabIndex = 1;
            // 
            // picLoading
            // 
            this.picLoading.Image = global::BJCBCPOS.Properties.Resources.loading;
            this.picLoading.Location = new System.Drawing.Point(186, 135);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(150, 150);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 1;
            this.picLoading.TabStop = false;
            // 
            // lbLoading
            // 
            this.lbLoading.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbLoading.Location = new System.Drawing.Point(18, 21);
            this.lbLoading.Name = "lbLoading";
            this.lbLoading.Size = new System.Drawing.Size(477, 97);
            this.lbLoading.TabIndex = 4;
            this.lbLoading.Text = "Connecting to Server...\r\nPlease wait a moment.";
            this.lbLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnConnectionLostStandAlone
            // 
            this.pnConnectionLostStandAlone.Controls.Add(this.btnExit);
            this.pnConnectionLostStandAlone.Controls.Add(this.btnEnterStanAlone);
            this.pnConnectionLostStandAlone.Controls.Add(this.lbConnectionLostStandAlone);
            this.pnConnectionLostStandAlone.Location = new System.Drawing.Point(256, 192);
            this.pnConnectionLostStandAlone.Name = "pnConnectionLostStandAlone";
            this.pnConnectionLostStandAlone.Size = new System.Drawing.Size(512, 331);
            this.pnConnectionLostStandAlone.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnExit.Location = new System.Drawing.Point(47, 238);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(180, 62);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "No";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnEnterStanAlone
            // 
            this.btnEnterStanAlone.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnEnterStanAlone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnterStanAlone.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnEnterStanAlone.FlatAppearance.BorderSize = 0;
            this.btnEnterStanAlone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnterStanAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterStanAlone.ForeColor = System.Drawing.Color.White;
            this.btnEnterStanAlone.Location = new System.Drawing.Point(287, 239);
            this.btnEnterStanAlone.Name = "btnEnterStanAlone";
            this.btnEnterStanAlone.Size = new System.Drawing.Size(180, 62);
            this.btnEnterStanAlone.TabIndex = 9;
            this.btnEnterStanAlone.Text = "Yes";
            this.btnEnterStanAlone.UseVisualStyleBackColor = true;
            this.btnEnterStanAlone.Click += new System.EventHandler(this.btnEnterStanAlone_Click);
            // 
            // lbConnectionLostStandAlone
            // 
            this.lbConnectionLostStandAlone.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConnectionLostStandAlone.Location = new System.Drawing.Point(15, 25);
            this.lbConnectionLostStandAlone.Name = "lbConnectionLostStandAlone";
            this.lbConnectionLostStandAlone.Size = new System.Drawing.Size(482, 190);
            this.lbConnectionLostStandAlone.TabIndex = 7;
            this.lbConnectionLostStandAlone.Text = "Unable to connect to Server or POS Server.\r\nDo you want to enter \"Stand Alone\" mo" +
                "de?\r\n\r\nPress Yes to enter Stand Alone mode.\r\nPress No to exit the program.";
            this.lbConnectionLostStandAlone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnReconnect
            // 
            this.pnReconnect.Controls.Add(this.btnSuccess);
            this.pnReconnect.Controls.Add(this.lbReconnect);
            this.pnReconnect.Location = new System.Drawing.Point(256, 192);
            this.pnReconnect.Name = "pnReconnect";
            this.pnReconnect.Size = new System.Drawing.Size(512, 331);
            this.pnReconnect.TabIndex = 2;
            // 
            // btnSuccess
            // 
            this.btnSuccess.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnSuccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSuccess.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSuccess.FlatAppearance.BorderSize = 0;
            this.btnSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuccess.ForeColor = System.Drawing.Color.White;
            this.btnSuccess.Location = new System.Drawing.Point(166, 233);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Size = new System.Drawing.Size(180, 62);
            this.btnSuccess.TabIndex = 6;
            this.btnSuccess.Text = "OK";
            this.btnSuccess.UseVisualStyleBackColor = true;
            this.btnSuccess.Click += new System.EventHandler(this.btnSuccess_Click);
            // 
            // lbReconnect
            // 
            this.lbReconnect.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReconnect.Location = new System.Drawing.Point(15, 25);
            this.lbReconnect.Name = "lbReconnect";
            this.lbReconnect.Size = new System.Drawing.Size(482, 190);
            this.lbReconnect.TabIndex = 5;
            this.lbReconnect.Text = "Successfully connected to the server.\r\nPlease try again.";
            this.lbReconnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnCommandTimeout
            // 
            this.pnCommandTimeout.Controls.Add(this.btnContinue);
            this.pnCommandTimeout.Controls.Add(this.lbCommandTimeout);
            this.pnCommandTimeout.Location = new System.Drawing.Point(256, 192);
            this.pnCommandTimeout.Name = "pnCommandTimeout";
            this.pnCommandTimeout.Size = new System.Drawing.Size(512, 331);
            this.pnCommandTimeout.TabIndex = 4;
            // 
            // btnContinue
            // 
            this.btnContinue.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(166, 233);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(180, 62);
            this.btnContinue.TabIndex = 6;
            this.btnContinue.Text = "OK";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lbCommandTimeout
            // 
            this.lbCommandTimeout.Font = new System.Drawing.Font("Prompt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCommandTimeout.Location = new System.Drawing.Point(15, 25);
            this.lbCommandTimeout.Name = "lbCommandTimeout";
            this.lbCommandTimeout.Size = new System.Drawing.Size(482, 190);
            this.lbCommandTimeout.TabIndex = 5;
            this.lbCommandTimeout.Text = "An error has occurred in the transaction. \r\nPlease try again.";
            this.lbCommandTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnStandAlone
            // 
            this.pnStandAlone.Controls.Add(this.btnSuccessStandAlone);
            this.pnStandAlone.Controls.Add(this.lbStandAlone);
            this.pnStandAlone.Location = new System.Drawing.Point(256, 192);
            this.pnStandAlone.Name = "pnStandAlone";
            this.pnStandAlone.Size = new System.Drawing.Size(512, 331);
            this.pnStandAlone.TabIndex = 10;
            // 
            // btnSuccessStandAlone
            // 
            this.btnSuccessStandAlone.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnSuccessStandAlone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSuccessStandAlone.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSuccessStandAlone.FlatAppearance.BorderSize = 0;
            this.btnSuccessStandAlone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccessStandAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuccessStandAlone.ForeColor = System.Drawing.Color.White;
            this.btnSuccessStandAlone.Location = new System.Drawing.Point(166, 233);
            this.btnSuccessStandAlone.Name = "btnSuccessStandAlone";
            this.btnSuccessStandAlone.Size = new System.Drawing.Size(180, 62);
            this.btnSuccessStandAlone.TabIndex = 9;
            this.btnSuccessStandAlone.Text = "OK";
            this.btnSuccessStandAlone.UseVisualStyleBackColor = true;
            this.btnSuccessStandAlone.Click += new System.EventHandler(this.btnSuccessStandAlone_Click);
            // 
            // lbStandAlone
            // 
            this.lbStandAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbStandAlone.Location = new System.Drawing.Point(15, 25);
            this.lbStandAlone.Name = "lbStandAlone";
            this.lbStandAlone.Size = new System.Drawing.Size(482, 190);
            this.lbStandAlone.TabIndex = 8;
            this.lbStandAlone.Text = "Entered \"Stand Alone\" mode successfully.";
            this.lbStandAlone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnConnectionLostNotStandAlone
            // 
            this.pnConnectionLostNotStandAlone.Controls.Add(this.btnTryToConnect);
            this.pnConnectionLostNotStandAlone.Controls.Add(this.btnExit2);
            this.pnConnectionLostNotStandAlone.Controls.Add(this.lbConnectionLostNotStandAlone);
            this.pnConnectionLostNotStandAlone.Location = new System.Drawing.Point(256, 192);
            this.pnConnectionLostNotStandAlone.Name = "pnConnectionLostNotStandAlone";
            this.pnConnectionLostNotStandAlone.Size = new System.Drawing.Size(512, 331);
            this.pnConnectionLostNotStandAlone.TabIndex = 10;
            // 
            // btnTryToConnect
            // 
            this.btnTryToConnect.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel;
            this.btnTryToConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTryToConnect.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnTryToConnect.FlatAppearance.BorderSize = 0;
            this.btnTryToConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTryToConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTryToConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnTryToConnect.Location = new System.Drawing.Point(47, 238);
            this.btnTryToConnect.Name = "btnTryToConnect";
            this.btnTryToConnect.Size = new System.Drawing.Size(180, 62);
            this.btnTryToConnect.TabIndex = 8;
            this.btnTryToConnect.Text = "No";
            this.btnTryToConnect.UseVisualStyleBackColor = true;
            this.btnTryToConnect.Click += new System.EventHandler(this.btnTryToConnect_Click);
            // 
            // btnExit2
            // 
            this.btnExit2.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_ok1;
            this.btnExit2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit2.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnExit2.FlatAppearance.BorderSize = 0;
            this.btnExit2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit2.ForeColor = System.Drawing.Color.White;
            this.btnExit2.Location = new System.Drawing.Point(287, 239);
            this.btnExit2.Name = "btnExit2";
            this.btnExit2.Size = new System.Drawing.Size(180, 62);
            this.btnExit2.TabIndex = 9;
            this.btnExit2.Text = "Yes";
            this.btnExit2.UseVisualStyleBackColor = true;
            this.btnExit2.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbConnectionLostNotStandAlone
            // 
            this.lbConnectionLostNotStandAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbConnectionLostNotStandAlone.Location = new System.Drawing.Point(15, 25);
            this.lbConnectionLostNotStandAlone.Name = "lbConnectionLostNotStandAlone";
            this.lbConnectionLostNotStandAlone.Size = new System.Drawing.Size(482, 190);
            this.lbConnectionLostNotStandAlone.TabIndex = 7;
            this.lbConnectionLostNotStandAlone.Text = "Unable to connect to the Server or POS Server.\r\nPlease inform IT to check.\r\nPress" +
                " Yes to exit the program.\r\nPress No try to connect again.";
            this.lbConnectionLostNotStandAlone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmConnectionLost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pnLoading);
            this.Controls.Add(this.pnConnectionLostStandAlone);
            this.Controls.Add(this.pnStandAlone);
            this.Controls.Add(this.pnConnectionLostNotStandAlone);
            this.Controls.Add(this.pnCommandTimeout);
            this.Controls.Add(this.pnReconnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConnectionLost";
            this.Text = "frmConnectionLost";
            this.Shown += new System.EventHandler(this.frmConnectionLost_Shown);
            this.pnLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.pnConnectionLostStandAlone.ResumeLayout(false);
            this.pnReconnect.ResumeLayout(false);
            this.pnCommandTimeout.ResumeLayout(false);
            this.pnStandAlone.ResumeLayout(false);
            this.pnConnectionLostNotStandAlone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLoading;
        private System.Windows.Forms.Label lbLoading;
        private System.Windows.Forms.Panel pnConnectionLostStandAlone;
        private System.Windows.Forms.Label lbConnectionLostStandAlone;
        private System.Windows.Forms.Panel pnReconnect;
        private System.Windows.Forms.Label lbReconnect;
        public System.Windows.Forms.Button btnEnterStanAlone;
        public System.Windows.Forms.Button btnSuccess;
        public System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Panel pnCommandTimeout;
        public System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label lbCommandTimeout;
        private System.Windows.Forms.Panel pnStandAlone;
        private System.Windows.Forms.Label lbStandAlone;
        public System.Windows.Forms.Button btnSuccessStandAlone;
        private System.Windows.Forms.Panel pnConnectionLostNotStandAlone;
        public System.Windows.Forms.Button btnTryToConnect;
        public System.Windows.Forms.Button btnExit2;
        private System.Windows.Forms.Label lbConnectionLostNotStandAlone;
    }
}