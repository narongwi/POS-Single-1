namespace BJCBCPOS
{
    partial class frmMainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lbTextHeaderMain = new System.Windows.Forms.Label();
            this.pnMainMenu = new System.Windows.Forms.Panel();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnTool = new System.Windows.Forms.Button();
            this.btnCloseTransaction = new System.Windows.Forms.Button();
            this.btnCloseCashier = new System.Windows.Forms.Button();
            this.btnCashOut = new System.Windows.Forms.Button();
            this.btnVoid = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnCashBalance = new System.Windows.Forms.Button();
            this.btnOpenTransaction = new System.Windows.Forms.Button();
            this.lbHeadMenu4 = new System.Windows.Forms.Label();
            this.lbHeadMenu3 = new System.Windows.Forms.Label();
            this.lbHeadMenu2 = new System.Windows.Forms.Label();
            this.lbHeadMenu1 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.picGroupMenu4 = new System.Windows.Forms.PictureBox();
            this.picGroupMenu3 = new System.Windows.Forms.PictureBox();
            this.picGroupMenu2 = new System.Windows.Forms.PictureBox();
            this.picGroupMenu1 = new System.Windows.Forms.PictureBox();
            this.panelOpenTransaction = new System.Windows.Forms.Panel();
            this.btnOpenTran = new System.Windows.Forms.Button();
            this.btnCancelOpenTran = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbConfirmClose = new System.Windows.Forms.Label();
            this.lbCloseCash = new System.Windows.Forms.Label();
            this.lbConfirmOpen = new System.Windows.Forms.Label();
            this.lbConfirmOpenTran = new System.Windows.Forms.Label();
            this.lbCloseCashtxt = new System.Windows.Forms.Label();
            this.panelOpenTranSuccess = new System.Windows.Forms.Panel();
            this.btnOpenTranSuccess = new System.Windows.Forms.Button();
            this.lbTxtCloseTranSuccess = new System.Windows.Forms.Label();
            this.lbTxtTranSuccess = new System.Windows.Forms.Label();
            this.lblSuccessCloseCash = new System.Windows.Forms.Label();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.lbOpentran = new System.Windows.Forms.Label();
            this.lbCloseTran = new System.Windows.Forms.Label();
            this.lbCloseCashier = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnMainMenu.SuspendLayout();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu1)).BeginInit();
            this.panelOpenTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelOpenTranSuccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLogo.Image = global::BJCBCPOS.Properties.Resources.NoPath1;
            this.picLogo.Location = new System.Drawing.Point(2, 2);
            this.picLogo.Margin = new System.Windows.Forms.Padding(2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(106, 120);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lbTextHeaderMain
            // 
            this.lbTextHeaderMain.BackColor = System.Drawing.Color.Transparent;
            this.lbTextHeaderMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextHeaderMain.ForeColor = System.Drawing.Color.Black;
            this.lbTextHeaderMain.Location = new System.Drawing.Point(110, 2);
            this.lbTextHeaderMain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTextHeaderMain.Name = "lbTextHeaderMain";
            this.lbTextHeaderMain.Size = new System.Drawing.Size(458, 120);
            this.lbTextHeaderMain.TabIndex = 2;
            this.lbTextHeaderMain.Text = "Main menu";
            this.lbTextHeaderMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnMainMenu
            // 
            this.pnMainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.pnMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnMainMenu.Controls.Add(this.ucFooter1);
            this.pnMainMenu.Controls.Add(this.picLogo);
            this.pnMainMenu.Controls.Add(this.panelMenu);
            this.pnMainMenu.Controls.Add(this.panelOpenTransaction);
            this.pnMainMenu.Controls.Add(this.panelOpenTranSuccess);
            this.pnMainMenu.Controls.Add(this.lbTextHeaderMain);
            this.pnMainMenu.Controls.Add(this.ucHeader1);
            this.pnMainMenu.Controls.Add(this.lbOpentran);
            this.pnMainMenu.Controls.Add(this.lbCloseTran);
            this.pnMainMenu.Controls.Add(this.lbCloseCashier);
            this.pnMainMenu.Location = new System.Drawing.Point(0, 0);
            this.pnMainMenu.Margin = new System.Windows.Forms.Padding(2);
            this.pnMainMenu.Name = "pnMainMenu";
            this.pnMainMenu.Size = new System.Drawing.Size(1024, 768);
            this.pnMainMenu.TabIndex = 12;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 20;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelMenu.Controls.Add(this.btnReport);
            this.panelMenu.Controls.Add(this.btnTool);
            this.panelMenu.Controls.Add(this.btnCloseTransaction);
            this.panelMenu.Controls.Add(this.btnCloseCashier);
            this.panelMenu.Controls.Add(this.btnCashOut);
            this.panelMenu.Controls.Add(this.btnVoid);
            this.panelMenu.Controls.Add(this.btnReturn);
            this.panelMenu.Controls.Add(this.btnSell);
            this.panelMenu.Controls.Add(this.btnCashBalance);
            this.panelMenu.Controls.Add(this.btnOpenTransaction);
            this.panelMenu.Controls.Add(this.lbHeadMenu4);
            this.panelMenu.Controls.Add(this.lbHeadMenu3);
            this.panelMenu.Controls.Add(this.lbHeadMenu2);
            this.panelMenu.Controls.Add(this.lbHeadMenu1);
            this.panelMenu.Controls.Add(this.pictureBox8);
            this.panelMenu.Controls.Add(this.pictureBox7);
            this.panelMenu.Controls.Add(this.pictureBox6);
            this.panelMenu.Controls.Add(this.picGroupMenu4);
            this.panelMenu.Controls.Add(this.picGroupMenu3);
            this.panelMenu.Controls.Add(this.picGroupMenu2);
            this.panelMenu.Controls.Add(this.picGroupMenu1);
            this.panelMenu.Location = new System.Drawing.Point(0, 124);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1024, 582);
            this.panelMenu.TabIndex = 12;
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReport.BackgroundImage")));
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.Location = new System.Drawing.Point(783, 336);
            this.btnReport.Margin = new System.Windows.Forms.Padding(5);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(230, 100);
            this.btnReport.TabIndex = 20;
            this.btnReport.Text = "รายงาน";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnTool
            // 
            this.btnTool.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTool.BackgroundImage")));
            this.btnTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTool.FlatAppearance.BorderSize = 0;
            this.btnTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTool.ForeColor = System.Drawing.Color.Black;
            this.btnTool.Location = new System.Drawing.Point(783, 214);
            this.btnTool.Margin = new System.Windows.Forms.Padding(5);
            this.btnTool.Name = "btnTool";
            this.btnTool.Size = new System.Drawing.Size(230, 100);
            this.btnTool.TabIndex = 19;
            this.btnTool.Text = "เครื่องมือ";
            this.btnTool.UseVisualStyleBackColor = true;
            this.btnTool.Click += new System.EventHandler(this.btnTool_Click);
            // 
            // btnCloseTransaction
            // 
            this.btnCloseTransaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCloseTransaction.BackgroundImage")));
            this.btnCloseTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseTransaction.FlatAppearance.BorderSize = 0;
            this.btnCloseTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseTransaction.ForeColor = System.Drawing.Color.Black;
            this.btnCloseTransaction.Location = new System.Drawing.Point(526, 456);
            this.btnCloseTransaction.Margin = new System.Windows.Forms.Padding(5);
            this.btnCloseTransaction.Name = "btnCloseTransaction";
            this.btnCloseTransaction.Size = new System.Drawing.Size(230, 100);
            this.btnCloseTransaction.TabIndex = 18;
            this.btnCloseTransaction.Text = "ปิดงานประจำวัน";
            this.btnCloseTransaction.UseVisualStyleBackColor = true;
            this.btnCloseTransaction.Click += new System.EventHandler(this.btnCloseTransaction_Click);
            // 
            // btnCloseCashier
            // 
            this.btnCloseCashier.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCloseCashier.BackgroundImage")));
            this.btnCloseCashier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseCashier.FlatAppearance.BorderSize = 0;
            this.btnCloseCashier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseCashier.ForeColor = System.Drawing.Color.Black;
            this.btnCloseCashier.Location = new System.Drawing.Point(525, 336);
            this.btnCloseCashier.Margin = new System.Windows.Forms.Padding(5);
            this.btnCloseCashier.Name = "btnCloseCashier";
            this.btnCloseCashier.Size = new System.Drawing.Size(230, 100);
            this.btnCloseCashier.TabIndex = 17;
            this.btnCloseCashier.Text = "ปิดงานแคชเชียร์";
            this.btnCloseCashier.UseVisualStyleBackColor = true;
            this.btnCloseCashier.Click += new System.EventHandler(this.btnCloseCashier_Click);
            // 
            // btnCashOut
            // 
            this.btnCashOut.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_260;
            this.btnCashOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCashOut.FlatAppearance.BorderSize = 0;
            this.btnCashOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashOut.ForeColor = System.Drawing.Color.Black;
            this.btnCashOut.Location = new System.Drawing.Point(525, 214);
            this.btnCashOut.Margin = new System.Windows.Forms.Padding(5);
            this.btnCashOut.Name = "btnCashOut";
            this.btnCashOut.Size = new System.Drawing.Size(230, 100);
            this.btnCashOut.TabIndex = 16;
            this.btnCashOut.Text = "ส่งเงินตามรอบ";
            this.btnCashOut.UseVisualStyleBackColor = true;
            this.btnCashOut.Click += new System.EventHandler(this.btnMenu3_1_Click);
            // 
            // btnVoid
            // 
            this.btnVoid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVoid.BackgroundImage")));
            this.btnVoid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVoid.FlatAppearance.BorderSize = 0;
            this.btnVoid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoid.ForeColor = System.Drawing.Color.Black;
            this.btnVoid.Location = new System.Drawing.Point(270, 456);
            this.btnVoid.Margin = new System.Windows.Forms.Padding(5);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(230, 100);
            this.btnVoid.TabIndex = 15;
            this.btnVoid.Text = "ยกเลิกใบเสร็จ";
            this.btnVoid.UseVisualStyleBackColor = true;
            this.btnVoid.Click += new System.EventHandler(this.btnMenu2_3_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReturn.BackgroundImage")));
            this.btnReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.Black;
            this.btnReturn.Location = new System.Drawing.Point(270, 336);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(5);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(230, 100);
            this.btnReturn.TabIndex = 14;
            this.btnReturn.Text = "รับคืน";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnMenu2_2_Click);
            // 
            // btnSell
            // 
            this.btnSell.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSell.BackgroundImage")));
            this.btnSell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSell.FlatAppearance.BorderSize = 0;
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSell.ForeColor = System.Drawing.Color.Black;
            this.btnSell.Location = new System.Drawing.Point(270, 214);
            this.btnSell.Margin = new System.Windows.Forms.Padding(5);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(230, 100);
            this.btnSell.TabIndex = 13;
            this.btnSell.Text = "การขาย";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnCashBalance
            // 
            this.btnCashBalance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCashBalance.BackgroundImage")));
            this.btnCashBalance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCashBalance.FlatAppearance.BorderSize = 0;
            this.btnCashBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashBalance.ForeColor = System.Drawing.Color.Black;
            this.btnCashBalance.Location = new System.Drawing.Point(12, 336);
            this.btnCashBalance.Margin = new System.Windows.Forms.Padding(5);
            this.btnCashBalance.Name = "btnCashBalance";
            this.btnCashBalance.Size = new System.Drawing.Size(230, 100);
            this.btnCashBalance.TabIndex = 12;
            this.btnCashBalance.Text = "เงินทอน";
            this.btnCashBalance.UseVisualStyleBackColor = true;
            this.btnCashBalance.Click += new System.EventHandler(this.btnCashBalance_Click);
            // 
            // btnOpenTransaction
            // 
            this.btnOpenTransaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenTransaction.BackgroundImage")));
            this.btnOpenTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenTransaction.FlatAppearance.BorderSize = 0;
            this.btnOpenTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTransaction.ForeColor = System.Drawing.Color.Black;
            this.btnOpenTransaction.Location = new System.Drawing.Point(12, 214);
            this.btnOpenTransaction.Margin = new System.Windows.Forms.Padding(5);
            this.btnOpenTransaction.Name = "btnOpenTransaction";
            this.btnOpenTransaction.Size = new System.Drawing.Size(230, 100);
            this.btnOpenTransaction.TabIndex = 11;
            this.btnOpenTransaction.Text = "เปิดงานประจำวัน";
            this.btnOpenTransaction.UseVisualStyleBackColor = true;
            this.btnOpenTransaction.Click += new System.EventHandler(this.btnOpenTransaction_Click);
            // 
            // lbHeadMenu4
            // 
            this.lbHeadMenu4.BackColor = System.Drawing.Color.Transparent;
            this.lbHeadMenu4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeadMenu4.ForeColor = System.Drawing.Color.YellowGreen;
            this.lbHeadMenu4.Location = new System.Drawing.Point(774, 153);
            this.lbHeadMenu4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeadMenu4.Name = "lbHeadMenu4";
            this.lbHeadMenu4.Size = new System.Drawing.Size(248, 49);
            this.lbHeadMenu4.TabIndex = 10;
            this.lbHeadMenu4.Text = "อื่นๆ";
            this.lbHeadMenu4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHeadMenu3
            // 
            this.lbHeadMenu3.BackColor = System.Drawing.Color.Transparent;
            this.lbHeadMenu3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeadMenu3.ForeColor = System.Drawing.Color.YellowGreen;
            this.lbHeadMenu3.Location = new System.Drawing.Point(518, 153);
            this.lbHeadMenu3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeadMenu3.Name = "lbHeadMenu3";
            this.lbHeadMenu3.Size = new System.Drawing.Size(246, 49);
            this.lbHeadMenu3.TabIndex = 9;
            this.lbHeadMenu3.Text = "ปิดงาน";
            this.lbHeadMenu3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHeadMenu2
            // 
            this.lbHeadMenu2.BackColor = System.Drawing.Color.Transparent;
            this.lbHeadMenu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeadMenu2.ForeColor = System.Drawing.Color.YellowGreen;
            this.lbHeadMenu2.Location = new System.Drawing.Point(262, 153);
            this.lbHeadMenu2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeadMenu2.Name = "lbHeadMenu2";
            this.lbHeadMenu2.Size = new System.Drawing.Size(246, 49);
            this.lbHeadMenu2.TabIndex = 8;
            this.lbHeadMenu2.Text = "ระหว่างวัน";
            this.lbHeadMenu2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHeadMenu1
            // 
            this.lbHeadMenu1.BackColor = System.Drawing.Color.Transparent;
            this.lbHeadMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeadMenu1.ForeColor = System.Drawing.Color.YellowGreen;
            this.lbHeadMenu1.Location = new System.Drawing.Point(3, 153);
            this.lbHeadMenu1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeadMenu1.Name = "lbHeadMenu1";
            this.lbHeadMenu1.Size = new System.Drawing.Size(249, 49);
            this.lbHeadMenu1.TabIndex = 7;
            this.lbHeadMenu1.Text = "เริ่มงาน";
            this.lbHeadMenu1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Image = global::BJCBCPOS.Properties.Resources.Line_117_3x;
            this.pictureBox8.Location = new System.Drawing.Point(768, 24);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(2, 530);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 6;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Image = global::BJCBCPOS.Properties.Resources.Line_117_3x;
            this.pictureBox7.Location = new System.Drawing.Point(512, 24);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(2, 530);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 5;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = global::BJCBCPOS.Properties.Resources.Line_117_3x;
            this.pictureBox6.Location = new System.Drawing.Point(256, 24);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(2, 530);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // picGroupMenu4
            // 
            this.picGroupMenu4.BackColor = System.Drawing.Color.White;
            this.picGroupMenu4.Image = global::BJCBCPOS.Properties.Resources.icons8_services_100;
            this.picGroupMenu4.Location = new System.Drawing.Point(850, 33);
            this.picGroupMenu4.Margin = new System.Windows.Forms.Padding(2);
            this.picGroupMenu4.Name = "picGroupMenu4";
            this.picGroupMenu4.Size = new System.Drawing.Size(100, 100);
            this.picGroupMenu4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picGroupMenu4.TabIndex = 3;
            this.picGroupMenu4.TabStop = false;
            // 
            // picGroupMenu3
            // 
            this.picGroupMenu3.BackColor = System.Drawing.Color.White;
            this.picGroupMenu3.Image = global::BJCBCPOS.Properties.Resources.icons8_external_100;
            this.picGroupMenu3.Location = new System.Drawing.Point(595, 33);
            this.picGroupMenu3.Margin = new System.Windows.Forms.Padding(2);
            this.picGroupMenu3.Name = "picGroupMenu3";
            this.picGroupMenu3.Size = new System.Drawing.Size(100, 100);
            this.picGroupMenu3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picGroupMenu3.TabIndex = 2;
            this.picGroupMenu3.TabStop = false;
            // 
            // picGroupMenu2
            // 
            this.picGroupMenu2.BackColor = System.Drawing.Color.White;
            this.picGroupMenu2.Image = global::BJCBCPOS.Properties.Resources.icons8_cash_register_100;
            this.picGroupMenu2.Location = new System.Drawing.Point(341, 33);
            this.picGroupMenu2.Margin = new System.Windows.Forms.Padding(2);
            this.picGroupMenu2.Name = "picGroupMenu2";
            this.picGroupMenu2.Size = new System.Drawing.Size(100, 100);
            this.picGroupMenu2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picGroupMenu2.TabIndex = 1;
            this.picGroupMenu2.TabStop = false;
            // 
            // picGroupMenu1
            // 
            this.picGroupMenu1.BackColor = System.Drawing.Color.White;
            this.picGroupMenu1.Image = global::BJCBCPOS.Properties.Resources.icons8_internal_100;
            this.picGroupMenu1.Location = new System.Drawing.Point(82, 33);
            this.picGroupMenu1.Margin = new System.Windows.Forms.Padding(2);
            this.picGroupMenu1.Name = "picGroupMenu1";
            this.picGroupMenu1.Size = new System.Drawing.Size(100, 100);
            this.picGroupMenu1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picGroupMenu1.TabIndex = 0;
            this.picGroupMenu1.TabStop = false;
            // 
            // panelOpenTransaction
            // 
            this.panelOpenTransaction.BackColor = System.Drawing.Color.White;
            this.panelOpenTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelOpenTransaction.Controls.Add(this.btnOpenTran);
            this.panelOpenTransaction.Controls.Add(this.btnCancelOpenTran);
            this.panelOpenTransaction.Controls.Add(this.pictureBox1);
            this.panelOpenTransaction.Controls.Add(this.lbConfirmClose);
            this.panelOpenTransaction.Controls.Add(this.lbCloseCash);
            this.panelOpenTransaction.Controls.Add(this.lbConfirmOpen);
            this.panelOpenTransaction.Controls.Add(this.lbConfirmOpenTran);
            this.panelOpenTransaction.Controls.Add(this.lbCloseCashtxt);
            this.panelOpenTransaction.Location = new System.Drawing.Point(0, 124);
            this.panelOpenTransaction.Margin = new System.Windows.Forms.Padding(2);
            this.panelOpenTransaction.Name = "panelOpenTransaction";
            this.panelOpenTransaction.Size = new System.Drawing.Size(1024, 582);
            this.panelOpenTransaction.TabIndex = 14;
            // 
            // btnOpenTran
            // 
            this.btnOpenTran.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenTran.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_260_3x;
            this.btnOpenTran.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenTran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTran.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTran.ForeColor = System.Drawing.Color.White;
            this.btnOpenTran.Location = new System.Drawing.Point(563, 351);
            this.btnOpenTran.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenTran.Name = "btnOpenTran";
            this.btnOpenTran.Size = new System.Drawing.Size(183, 66);
            this.btnOpenTran.TabIndex = 4;
            this.btnOpenTran.Text = "ตกลง";
            this.btnOpenTran.UseCompatibleTextRendering = true;
            this.btnOpenTran.UseVisualStyleBackColor = false;
            this.btnOpenTran.Click += new System.EventHandler(this.btnOpenTran_Click);
            // 
            // btnCancelOpenTran
            // 
            this.btnCancelOpenTran.BackColor = System.Drawing.Color.White;
            this.btnCancelOpenTran.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelOpenTran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelOpenTran.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelOpenTran.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCancelOpenTran.Location = new System.Drawing.Point(348, 351);
            this.btnCancelOpenTran.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelOpenTran.Name = "btnCancelOpenTran";
            this.btnCancelOpenTran.Size = new System.Drawing.Size(183, 66);
            this.btnCancelOpenTran.TabIndex = 3;
            this.btnCancelOpenTran.Text = "ยกเลิก";
            this.btnCancelOpenTran.UseCompatibleTextRendering = true;
            this.btnCancelOpenTran.UseVisualStyleBackColor = false;
            this.btnCancelOpenTran.Click += new System.EventHandler(this.btnCancelOpenTran_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BJCBCPOS.Properties.Resources.icons8_checked_100;
            this.pictureBox1.Location = new System.Drawing.Point(476, 38);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 134);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lbConfirmClose
            // 
            this.lbConfirmClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirmClose.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbConfirmClose.Location = new System.Drawing.Point(123, 198);
            this.lbConfirmClose.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConfirmClose.Name = "lbConfirmClose";
            this.lbConfirmClose.Size = new System.Drawing.Size(827, 58);
            this.lbConfirmClose.TabIndex = 5;
            this.lbConfirmClose.Text = "ยืนยันการปิด";
            this.lbConfirmClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCloseCash
            // 
            this.lbCloseCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCloseCash.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbCloseCash.Location = new System.Drawing.Point(123, 198);
            this.lbCloseCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCloseCash.Name = "lbCloseCash";
            this.lbCloseCash.Size = new System.Drawing.Size(827, 58);
            this.lbCloseCash.TabIndex = 6;
            this.lbCloseCash.Text = "ยืนยัน";
            this.lbCloseCash.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbConfirmOpen
            // 
            this.lbConfirmOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirmOpen.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbConfirmOpen.Location = new System.Drawing.Point(123, 198);
            this.lbConfirmOpen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConfirmOpen.Name = "lbConfirmOpen";
            this.lbConfirmOpen.Size = new System.Drawing.Size(827, 58);
            this.lbConfirmOpen.TabIndex = 1;
            this.lbConfirmOpen.Text = "ยืนยันการเปิด";
            this.lbConfirmOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbConfirmOpenTran
            // 
            this.lbConfirmOpenTran.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfirmOpenTran.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbConfirmOpenTran.Location = new System.Drawing.Point(107, 274);
            this.lbConfirmOpenTran.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConfirmOpenTran.Name = "lbConfirmOpenTran";
            this.lbConfirmOpenTran.Size = new System.Drawing.Size(843, 57);
            this.lbConfirmOpenTran.TabIndex = 2;
            this.lbConfirmOpenTran.Text = "งานการขายประจำวัน?";
            this.lbConfirmOpenTran.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCloseCashtxt
            // 
            this.lbCloseCashtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCloseCashtxt.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbCloseCashtxt.Location = new System.Drawing.Point(107, 274);
            this.lbCloseCashtxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCloseCashtxt.Name = "lbCloseCashtxt";
            this.lbCloseCashtxt.Size = new System.Drawing.Size(843, 57);
            this.lbCloseCashtxt.TabIndex = 7;
            this.lbCloseCashtxt.Text = "การปิดงานแคชเชียร์?";
            this.lbCloseCashtxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelOpenTranSuccess
            // 
            this.panelOpenTranSuccess.BackColor = System.Drawing.Color.White;
            this.panelOpenTranSuccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelOpenTranSuccess.Controls.Add(this.btnOpenTranSuccess);
            this.panelOpenTranSuccess.Controls.Add(this.lbTxtCloseTranSuccess);
            this.panelOpenTranSuccess.Controls.Add(this.lbTxtTranSuccess);
            this.panelOpenTranSuccess.Controls.Add(this.lblSuccessCloseCash);
            this.panelOpenTranSuccess.Location = new System.Drawing.Point(0, 124);
            this.panelOpenTranSuccess.Margin = new System.Windows.Forms.Padding(2);
            this.panelOpenTranSuccess.Name = "panelOpenTranSuccess";
            this.panelOpenTranSuccess.Size = new System.Drawing.Size(1024, 582);
            this.panelOpenTranSuccess.TabIndex = 14;
            // 
            // btnOpenTranSuccess
            // 
            this.btnOpenTranSuccess.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenTranSuccess.BackgroundImage = global::BJCBCPOS.Properties.Resources.button_search;
            this.btnOpenTranSuccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenTranSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTranSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTranSuccess.ForeColor = System.Drawing.Color.White;
            this.btnOpenTranSuccess.Location = new System.Drawing.Point(440, 258);
            this.btnOpenTranSuccess.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenTranSuccess.Name = "btnOpenTranSuccess";
            this.btnOpenTranSuccess.Size = new System.Drawing.Size(170, 81);
            this.btnOpenTranSuccess.TabIndex = 4;
            this.btnOpenTranSuccess.Text = "ตกลง";
            this.btnOpenTranSuccess.UseVisualStyleBackColor = false;
            this.btnOpenTranSuccess.Click += new System.EventHandler(this.btnOpenTranSuccess_Click);
            // 
            // lbTxtCloseTranSuccess
            // 
            this.lbTxtCloseTranSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtCloseTranSuccess.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbTxtCloseTranSuccess.Location = new System.Drawing.Point(48, 104);
            this.lbTxtCloseTranSuccess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtCloseTranSuccess.Name = "lbTxtCloseTranSuccess";
            this.lbTxtCloseTranSuccess.Size = new System.Drawing.Size(902, 57);
            this.lbTxtCloseTranSuccess.TabIndex = 5;
            this.lbTxtCloseTranSuccess.Text = "ปิดงานประจำวันเรียบร้อยแล้ว";
            this.lbTxtCloseTranSuccess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbTxtTranSuccess
            // 
            this.lbTxtTranSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtTranSuccess.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbTxtTranSuccess.Location = new System.Drawing.Point(48, 104);
            this.lbTxtTranSuccess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTxtTranSuccess.Name = "lbTxtTranSuccess";
            this.lbTxtTranSuccess.Size = new System.Drawing.Size(902, 57);
            this.lbTxtTranSuccess.TabIndex = 2;
            this.lbTxtTranSuccess.Text = "เปิดงานประจำวันเรียบร้อยแล้ว";
            this.lbTxtTranSuccess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSuccessCloseCash
            // 
            this.lblSuccessCloseCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuccessCloseCash.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblSuccessCloseCash.Location = new System.Drawing.Point(48, 104);
            this.lblSuccessCloseCash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSuccessCloseCash.Name = "lblSuccessCloseCash";
            this.lblSuccessCloseCash.Size = new System.Drawing.Size(902, 57);
            this.lblSuccessCloseCash.TabIndex = 6;
            this.lblSuccessCloseCash.Text = "บันทึกปิดงานแคชเชียร์เรียบร้อยแล้ว";
            this.lblSuccessCloseCash.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "Title1";
            this.ucHeader1.currentMenuTitle2 = "Title2";
            this.ucHeader1.currentMenuTitle3 = "Title3";
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.ForeColor = System.Drawing.Color.White;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = true;
            this.ucHeader1.showCalculator = true;
            this.ucHeader1.showCurrentMenuText = false;
            this.ucHeader1.showHamberGetItm = true;
            this.ucHeader1.showLanguage = true;
            this.ucHeader1.showLine = false;
            this.ucHeader1.showLockScreen = true;
            this.ucHeader1.showLogout = true;
            this.ucHeader1.showMainMenu = false;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showMember_ButtonBack = true;
            this.ucHeader1.showMember_IsSaveMember = true;
            this.ucHeader1.showScanner = true;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 19;
            this.ucHeader1.LanguageClick += new System.EventHandler(this.ucHeader1_LanguageClick);
            // 
            // lbOpentran
            // 
            this.lbOpentran.BackColor = System.Drawing.Color.Transparent;
            this.lbOpentran.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOpentran.ForeColor = System.Drawing.Color.Black;
            this.lbOpentran.Location = new System.Drawing.Point(110, 2);
            this.lbOpentran.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOpentran.Name = "lbOpentran";
            this.lbOpentran.Size = new System.Drawing.Size(458, 120);
            this.lbOpentran.TabIndex = 22;
            this.lbOpentran.Text = "เปิดงานประจำวัน";
            this.lbOpentran.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCloseTran
            // 
            this.lbCloseTran.BackColor = System.Drawing.Color.Transparent;
            this.lbCloseTran.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCloseTran.ForeColor = System.Drawing.Color.Black;
            this.lbCloseTran.Location = new System.Drawing.Point(110, 2);
            this.lbCloseTran.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCloseTran.Name = "lbCloseTran";
            this.lbCloseTran.Size = new System.Drawing.Size(458, 120);
            this.lbCloseTran.TabIndex = 21;
            this.lbCloseTran.Text = "ปิดงานประจำวัน";
            this.lbCloseTran.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCloseCashier
            // 
            this.lbCloseCashier.BackColor = System.Drawing.Color.Transparent;
            this.lbCloseCashier.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCloseCashier.ForeColor = System.Drawing.Color.Black;
            this.lbCloseCashier.Location = new System.Drawing.Point(110, 2);
            this.lbCloseCashier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCloseCashier.Name = "lbCloseCashier";
            this.lbCloseCashier.Size = new System.Drawing.Size(458, 120);
            this.lbCloseCashier.TabIndex = 23;
            this.lbCloseCashier.Text = "ปิดงานแคชเชียร์";
            this.lbCloseCashier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pnMainMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.frmMainMenu_Activated);
            this.Shown += new System.EventHandler(this.frmMainMenu_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnMainMenu.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMenu1)).EndInit();
            this.panelOpenTransaction.ResumeLayout(false);
            this.panelOpenTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelOpenTranSuccess.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        public System.Windows.Forms.Label lbTextHeaderMain;
        private System.Windows.Forms.Panel pnMainMenu;
        private System.Windows.Forms.Panel panelMenu;
        public System.Windows.Forms.Button btnReport;
        public System.Windows.Forms.Button btnTool;
        public System.Windows.Forms.Button btnCloseTransaction;
        public System.Windows.Forms.Button btnCloseCashier;
        public System.Windows.Forms.Button btnCashOut;
        public System.Windows.Forms.Button btnVoid;
        public System.Windows.Forms.Button btnReturn;
        public System.Windows.Forms.Button btnSell;
        public System.Windows.Forms.Button btnCashBalance;
        public System.Windows.Forms.Button btnOpenTransaction;
        private System.Windows.Forms.Label lbHeadMenu4;
        private System.Windows.Forms.Label lbHeadMenu3;
        private System.Windows.Forms.Label lbHeadMenu2;
        private System.Windows.Forms.Label lbHeadMenu1;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox picGroupMenu4;
        private System.Windows.Forms.PictureBox picGroupMenu3;
        private System.Windows.Forms.PictureBox picGroupMenu2;
        private System.Windows.Forms.PictureBox picGroupMenu1;
        public System.Windows.Forms.Panel panelOpenTransaction;
        private System.Windows.Forms.Button btnOpenTran;
        public System.Windows.Forms.Button btnCancelOpenTran;
        public System.Windows.Forms.Label lbConfirmOpenTran;
        public System.Windows.Forms.Label lbConfirmOpen;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelOpenTranSuccess;
        private System.Windows.Forms.Button btnOpenTranSuccess;
        private System.Windows.Forms.Label lbTxtTranSuccess;
        private UCHeader ucHeader1;
        private UCFooter ucFooter1;
        public System.Windows.Forms.Label lbConfirmClose;
        private System.Windows.Forms.Label lbTxtCloseTranSuccess;
        public System.Windows.Forms.Label lbCloseTran;
        public System.Windows.Forms.Label lbOpentran;
        public System.Windows.Forms.Label lbCloseCashier;
        public System.Windows.Forms.Label lbCloseCashtxt;
        public System.Windows.Forms.Label lbCloseCash;
        public System.Windows.Forms.Label lblSuccessCloseCash;
    }
}