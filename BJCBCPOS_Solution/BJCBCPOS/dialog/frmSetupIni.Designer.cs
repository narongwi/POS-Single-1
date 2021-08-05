namespace BJCBCPOS
{
    partial class frmSetupIni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupIni));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.pnfrmConfig = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.uctwCOMPort = new BJCBCPOS.UCTextBoxWithIcon();
            this.label8 = new System.Windows.Forms.Label();
            this.uctwPrinterName = new BJCBCPOS.UCTextBoxWithIcon();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbDBServerTrainning = new System.Windows.Forms.Label();
            this.lbIPServerTrainning = new System.Windows.Forms.Label();
            this.lbDBServerBackup = new System.Windows.Forms.Label();
            this.lbIPServerBackup = new System.Windows.Forms.Label();
            this.uctwDBServerTrainning = new BJCBCPOS.UCTextBoxWithIcon();
            this.uctwIPServerTrainning = new BJCBCPOS.UCTextBoxWithIcon();
            this.uctwDBServerBackup = new BJCBCPOS.UCTextBoxWithIcon();
            this.uctwIPServerBackup = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbDBServer = new System.Windows.Forms.Label();
            this.uctwDBServer = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbIPServer = new System.Windows.Forms.Label();
            this.uctwIPServer = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbTillNo = new System.Windows.Forms.Label();
            this.uctwStoreCode = new BJCBCPOS.UCTextBoxWithIcon();
            this.lbStoreCode = new System.Windows.Forms.Label();
            this.uctwTillNo = new BJCBCPOS.UCTextBoxWithIcon();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon7 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label6 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon6 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label5 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon5 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label4 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon4 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label3 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon3 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label2 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon2 = new BJCBCPOS.UCTextBoxWithIcon();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTextBoxWithIcon1 = new BJCBCPOS.UCTextBoxWithIcon();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnfrmConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(53)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_257;
            this.splitContainer1.Panel1.Controls.Add(this.ucFooter1);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeader1);
            this.splitContainer1.Panel1.Controls.Add(this.pnfrmConfig);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_257;
            this.splitContainer1.Panel2.Controls.Add(this.ucKeyboard1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 743;
            this.splitContainer1.TabIndex = 0;
            // 
            // ucFooter1
            // 
            this.ucFooter1.BackColor = System.Drawing.Color.Transparent;
            this.ucFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucFooter1.Location = new System.Drawing.Point(0, 728);
            this.ucFooter1.Name = "ucFooter1";
            this.ucFooter1.Size = new System.Drawing.Size(1024, 40);
            this.ucFooter1.TabIndex = 1;
            // 
            // ucHeader1
            // 
            this.ucHeader1.alertEnabled = true;
            this.ucHeader1.alertFunctionID = null;
            this.ucHeader1.alertStatus = false;
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.currentMenuTitle1 = "Title1";
            this.ucHeader1.currentMenuTitle2 = "Title2";
            this.ucHeader1.currentMenuTitle3 = "Title3";
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.logoutText = "ออกจากระบบ";
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.nameText = "ชื่อสมาชิก";
            this.ucHeader1.nameVisible = false;
            this.ucHeader1.showAlert = false;
            this.ucHeader1.showCalculator = false;
            this.ucHeader1.showCurrentMenuText = false;
            this.ucHeader1.showHamberGetItm = true;
            this.ucHeader1.showLanguage = false;
            this.ucHeader1.showLine = false;
            this.ucHeader1.showLockScreen = false;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = false;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showMember_ButtonBack = true;
            this.ucHeader1.showMember_IsSaveMember = true;
            this.ucHeader1.showScanner = false;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 0;
            // 
            // pnfrmConfig
            // 
            this.pnfrmConfig.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_258_3x;
            this.pnfrmConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnfrmConfig.Controls.Add(this.label9);
            this.pnfrmConfig.Controls.Add(this.uctwCOMPort);
            this.pnfrmConfig.Controls.Add(this.label8);
            this.pnfrmConfig.Controls.Add(this.uctwPrinterName);
            this.pnfrmConfig.Controls.Add(this.btnNo);
            this.pnfrmConfig.Controls.Add(this.btnSave);
            this.pnfrmConfig.Controls.Add(this.lbDBServerTrainning);
            this.pnfrmConfig.Controls.Add(this.lbIPServerTrainning);
            this.pnfrmConfig.Controls.Add(this.lbDBServerBackup);
            this.pnfrmConfig.Controls.Add(this.lbIPServerBackup);
            this.pnfrmConfig.Controls.Add(this.uctwDBServerTrainning);
            this.pnfrmConfig.Controls.Add(this.uctwIPServerTrainning);
            this.pnfrmConfig.Controls.Add(this.uctwDBServerBackup);
            this.pnfrmConfig.Controls.Add(this.uctwIPServerBackup);
            this.pnfrmConfig.Controls.Add(this.lbDBServer);
            this.pnfrmConfig.Controls.Add(this.uctwDBServer);
            this.pnfrmConfig.Controls.Add(this.lbIPServer);
            this.pnfrmConfig.Controls.Add(this.uctwIPServer);
            this.pnfrmConfig.Controls.Add(this.lbTillNo);
            this.pnfrmConfig.Controls.Add(this.uctwStoreCode);
            this.pnfrmConfig.Controls.Add(this.lbStoreCode);
            this.pnfrmConfig.Controls.Add(this.uctwTillNo);
            this.pnfrmConfig.Location = new System.Drawing.Point(189, 152);
            this.pnfrmConfig.Margin = new System.Windows.Forms.Padding(2);
            this.pnfrmConfig.Name = "pnfrmConfig";
            this.pnfrmConfig.Size = new System.Drawing.Size(650, 445);
            this.pnfrmConfig.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(24, 345);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(200, 35);
            this.label9.TabIndex = 33;
            this.label9.Text = "COM Port (EDC) :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwCOMPort
            // 
            this.uctwCOMPort.BackColor = System.Drawing.Color.White;
            this.uctwCOMPort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwCOMPort.BackgroundImage")));
            this.uctwCOMPort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwCOMPort.EnabledUC = true;
            this.uctwCOMPort.IsAmount = false;
            this.uctwCOMPort.IsKeyBoardForScan = false;
            this.uctwCOMPort.IsLarge = false;
            this.uctwCOMPort.IsNumber = false;
            this.uctwCOMPort.IsSetFormat = false;
            this.uctwCOMPort.IsValidateNumberZero = false;
            this.uctwCOMPort.IsValidateTextEmpty = false;
            this.uctwCOMPort.Location = new System.Drawing.Point(230, 345);
            this.uctwCOMPort.MaxLength = 32767;
            this.uctwCOMPort.Name = "uctwCOMPort";
            this.uctwCOMPort.PasswordChar = false;
            this.uctwCOMPort.placeHolder = "Input COM Port for EDC";
            this.uctwCOMPort.Readonly = false;
            this.uctwCOMPort.ShortcutsEnabled = true;
            this.uctwCOMPort.Size = new System.Drawing.Size(400, 35);
            this.uctwCOMPort.TabIndex = 32;
            this.uctwCOMPort.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwCOMPort.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(25, 304);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 35);
            this.label8.TabIndex = 31;
            this.label8.Text = "Printer Name :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwPrinterName
            // 
            this.uctwPrinterName.BackColor = System.Drawing.Color.White;
            this.uctwPrinterName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwPrinterName.BackgroundImage")));
            this.uctwPrinterName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwPrinterName.EnabledUC = true;
            this.uctwPrinterName.IsAmount = false;
            this.uctwPrinterName.IsKeyBoardForScan = false;
            this.uctwPrinterName.IsLarge = false;
            this.uctwPrinterName.IsNumber = false;
            this.uctwPrinterName.IsSetFormat = false;
            this.uctwPrinterName.IsValidateNumberZero = false;
            this.uctwPrinterName.IsValidateTextEmpty = false;
            this.uctwPrinterName.Location = new System.Drawing.Point(231, 304);
            this.uctwPrinterName.MaxLength = 32767;
            this.uctwPrinterName.Name = "uctwPrinterName";
            this.uctwPrinterName.PasswordChar = false;
            this.uctwPrinterName.placeHolder = "Input Printer Name";
            this.uctwPrinterName.Readonly = false;
            this.uctwPrinterName.ShortcutsEnabled = true;
            this.uctwPrinterName.Size = new System.Drawing.Size(400, 35);
            this.uctwPrinterName.TabIndex = 30;
            this.uctwPrinterName.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwPrinterName.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.White;
            this.btnNo.BackgroundImage = global::BJCBCPOS.Properties.Resources.icon_alert_cancel1;
            this.btnNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNo.FlatAppearance.BorderSize = 0;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(184)))), ((int)(((byte)(105)))));
            this.btnNo.Location = new System.Drawing.Point(28, 390);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(198, 44);
            this.btnNo.TabIndex = 10;
            this.btnNo.Text = "Cancel";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_224;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(231, 390);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(400, 44);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbDBServerTrainning
            // 
            this.lbDBServerTrainning.BackColor = System.Drawing.Color.Transparent;
            this.lbDBServerTrainning.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDBServerTrainning.Location = new System.Drawing.Point(25, 262);
            this.lbDBServerTrainning.Name = "lbDBServerTrainning";
            this.lbDBServerTrainning.Size = new System.Drawing.Size(200, 35);
            this.lbDBServerTrainning.TabIndex = 29;
            this.lbDBServerTrainning.Text = "Database Server Trainning :";
            this.lbDBServerTrainning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbIPServerTrainning
            // 
            this.lbIPServerTrainning.BackColor = System.Drawing.Color.Transparent;
            this.lbIPServerTrainning.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbIPServerTrainning.Location = new System.Drawing.Point(25, 221);
            this.lbIPServerTrainning.Name = "lbIPServerTrainning";
            this.lbIPServerTrainning.Size = new System.Drawing.Size(200, 35);
            this.lbIPServerTrainning.TabIndex = 28;
            this.lbIPServerTrainning.Text = "IP Server Trainning :";
            this.lbIPServerTrainning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDBServerBackup
            // 
            this.lbDBServerBackup.BackColor = System.Drawing.Color.Transparent;
            this.lbDBServerBackup.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDBServerBackup.Location = new System.Drawing.Point(25, 180);
            this.lbDBServerBackup.Name = "lbDBServerBackup";
            this.lbDBServerBackup.Size = new System.Drawing.Size(200, 35);
            this.lbDBServerBackup.TabIndex = 27;
            this.lbDBServerBackup.Text = "Database Server Backup :";
            this.lbDBServerBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbIPServerBackup
            // 
            this.lbIPServerBackup.BackColor = System.Drawing.Color.Transparent;
            this.lbIPServerBackup.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbIPServerBackup.Location = new System.Drawing.Point(25, 139);
            this.lbIPServerBackup.Name = "lbIPServerBackup";
            this.lbIPServerBackup.Size = new System.Drawing.Size(200, 35);
            this.lbIPServerBackup.TabIndex = 26;
            this.lbIPServerBackup.Text = "IP Server Backup :";
            this.lbIPServerBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwDBServerTrainning
            // 
            this.uctwDBServerTrainning.BackColor = System.Drawing.Color.White;
            this.uctwDBServerTrainning.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwDBServerTrainning.BackgroundImage")));
            this.uctwDBServerTrainning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwDBServerTrainning.EnabledUC = true;
            this.uctwDBServerTrainning.IsAmount = false;
            this.uctwDBServerTrainning.IsKeyBoardForScan = false;
            this.uctwDBServerTrainning.IsLarge = false;
            this.uctwDBServerTrainning.IsNumber = false;
            this.uctwDBServerTrainning.IsSetFormat = false;
            this.uctwDBServerTrainning.IsValidateNumberZero = false;
            this.uctwDBServerTrainning.IsValidateTextEmpty = false;
            this.uctwDBServerTrainning.Location = new System.Drawing.Point(231, 262);
            this.uctwDBServerTrainning.MaxLength = 32767;
            this.uctwDBServerTrainning.Name = "uctwDBServerTrainning";
            this.uctwDBServerTrainning.PasswordChar = false;
            this.uctwDBServerTrainning.placeHolder = "Input Database Server Trainning";
            this.uctwDBServerTrainning.Readonly = false;
            this.uctwDBServerTrainning.ShortcutsEnabled = true;
            this.uctwDBServerTrainning.Size = new System.Drawing.Size(400, 35);
            this.uctwDBServerTrainning.TabIndex = 8;
            this.uctwDBServerTrainning.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwDBServerTrainning.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwDBServerTrainning.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwDBServerTrainning.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // uctwIPServerTrainning
            // 
            this.uctwIPServerTrainning.BackColor = System.Drawing.Color.White;
            this.uctwIPServerTrainning.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwIPServerTrainning.BackgroundImage")));
            this.uctwIPServerTrainning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwIPServerTrainning.EnabledUC = true;
            this.uctwIPServerTrainning.IsAmount = false;
            this.uctwIPServerTrainning.IsKeyBoardForScan = false;
            this.uctwIPServerTrainning.IsLarge = false;
            this.uctwIPServerTrainning.IsNumber = false;
            this.uctwIPServerTrainning.IsSetFormat = false;
            this.uctwIPServerTrainning.IsValidateNumberZero = false;
            this.uctwIPServerTrainning.IsValidateTextEmpty = false;
            this.uctwIPServerTrainning.Location = new System.Drawing.Point(231, 221);
            this.uctwIPServerTrainning.MaxLength = 32767;
            this.uctwIPServerTrainning.Name = "uctwIPServerTrainning";
            this.uctwIPServerTrainning.PasswordChar = false;
            this.uctwIPServerTrainning.placeHolder = "Input IP Server Trainning";
            this.uctwIPServerTrainning.Readonly = false;
            this.uctwIPServerTrainning.ShortcutsEnabled = true;
            this.uctwIPServerTrainning.Size = new System.Drawing.Size(400, 35);
            this.uctwIPServerTrainning.TabIndex = 7;
            this.uctwIPServerTrainning.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwIPServerTrainning.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwIPServerTrainning.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwIPServerTrainning.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // uctwDBServerBackup
            // 
            this.uctwDBServerBackup.BackColor = System.Drawing.Color.White;
            this.uctwDBServerBackup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwDBServerBackup.BackgroundImage")));
            this.uctwDBServerBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwDBServerBackup.EnabledUC = true;
            this.uctwDBServerBackup.IsAmount = false;
            this.uctwDBServerBackup.IsKeyBoardForScan = false;
            this.uctwDBServerBackup.IsLarge = false;
            this.uctwDBServerBackup.IsNumber = false;
            this.uctwDBServerBackup.IsSetFormat = false;
            this.uctwDBServerBackup.IsValidateNumberZero = false;
            this.uctwDBServerBackup.IsValidateTextEmpty = false;
            this.uctwDBServerBackup.Location = new System.Drawing.Point(231, 180);
            this.uctwDBServerBackup.MaxLength = 32767;
            this.uctwDBServerBackup.Name = "uctwDBServerBackup";
            this.uctwDBServerBackup.PasswordChar = false;
            this.uctwDBServerBackup.placeHolder = "Input Database Server Backup";
            this.uctwDBServerBackup.Readonly = false;
            this.uctwDBServerBackup.ShortcutsEnabled = true;
            this.uctwDBServerBackup.Size = new System.Drawing.Size(400, 35);
            this.uctwDBServerBackup.TabIndex = 6;
            this.uctwDBServerBackup.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwDBServerBackup.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwDBServerBackup.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwDBServerBackup.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // uctwIPServerBackup
            // 
            this.uctwIPServerBackup.BackColor = System.Drawing.Color.White;
            this.uctwIPServerBackup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwIPServerBackup.BackgroundImage")));
            this.uctwIPServerBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwIPServerBackup.EnabledUC = true;
            this.uctwIPServerBackup.IsAmount = false;
            this.uctwIPServerBackup.IsKeyBoardForScan = false;
            this.uctwIPServerBackup.IsLarge = false;
            this.uctwIPServerBackup.IsNumber = false;
            this.uctwIPServerBackup.IsSetFormat = false;
            this.uctwIPServerBackup.IsValidateNumberZero = false;
            this.uctwIPServerBackup.IsValidateTextEmpty = false;
            this.uctwIPServerBackup.Location = new System.Drawing.Point(231, 139);
            this.uctwIPServerBackup.MaxLength = 32767;
            this.uctwIPServerBackup.Name = "uctwIPServerBackup";
            this.uctwIPServerBackup.PasswordChar = false;
            this.uctwIPServerBackup.placeHolder = "Input IP Server Backup";
            this.uctwIPServerBackup.Readonly = false;
            this.uctwIPServerBackup.ShortcutsEnabled = true;
            this.uctwIPServerBackup.Size = new System.Drawing.Size(400, 35);
            this.uctwIPServerBackup.TabIndex = 5;
            this.uctwIPServerBackup.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwIPServerBackup.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwIPServerBackup.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwIPServerBackup.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // lbDBServer
            // 
            this.lbDBServer.BackColor = System.Drawing.Color.Transparent;
            this.lbDBServer.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDBServer.Location = new System.Drawing.Point(25, 98);
            this.lbDBServer.Name = "lbDBServer";
            this.lbDBServer.Size = new System.Drawing.Size(200, 35);
            this.lbDBServer.TabIndex = 21;
            this.lbDBServer.Text = "Database Server :";
            this.lbDBServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwDBServer
            // 
            this.uctwDBServer.BackColor = System.Drawing.Color.White;
            this.uctwDBServer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwDBServer.BackgroundImage")));
            this.uctwDBServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwDBServer.EnabledUC = true;
            this.uctwDBServer.IsAmount = false;
            this.uctwDBServer.IsKeyBoardForScan = false;
            this.uctwDBServer.IsLarge = false;
            this.uctwDBServer.IsNumber = false;
            this.uctwDBServer.IsSetFormat = false;
            this.uctwDBServer.IsValidateNumberZero = false;
            this.uctwDBServer.IsValidateTextEmpty = false;
            this.uctwDBServer.Location = new System.Drawing.Point(231, 98);
            this.uctwDBServer.MaxLength = 32767;
            this.uctwDBServer.Name = "uctwDBServer";
            this.uctwDBServer.PasswordChar = false;
            this.uctwDBServer.placeHolder = "Input Database Server";
            this.uctwDBServer.Readonly = false;
            this.uctwDBServer.ShortcutsEnabled = true;
            this.uctwDBServer.Size = new System.Drawing.Size(400, 35);
            this.uctwDBServer.TabIndex = 4;
            this.uctwDBServer.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwDBServer.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwDBServer.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwDBServer.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // lbIPServer
            // 
            this.lbIPServer.BackColor = System.Drawing.Color.Transparent;
            this.lbIPServer.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbIPServer.Location = new System.Drawing.Point(25, 57);
            this.lbIPServer.Name = "lbIPServer";
            this.lbIPServer.Size = new System.Drawing.Size(200, 35);
            this.lbIPServer.TabIndex = 19;
            this.lbIPServer.Text = "IP Server :";
            this.lbIPServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwIPServer
            // 
            this.uctwIPServer.BackColor = System.Drawing.Color.White;
            this.uctwIPServer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwIPServer.BackgroundImage")));
            this.uctwIPServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwIPServer.EnabledUC = true;
            this.uctwIPServer.IsAmount = false;
            this.uctwIPServer.IsKeyBoardForScan = false;
            this.uctwIPServer.IsLarge = false;
            this.uctwIPServer.IsNumber = false;
            this.uctwIPServer.IsSetFormat = false;
            this.uctwIPServer.IsValidateNumberZero = false;
            this.uctwIPServer.IsValidateTextEmpty = false;
            this.uctwIPServer.Location = new System.Drawing.Point(231, 57);
            this.uctwIPServer.MaxLength = 32767;
            this.uctwIPServer.Name = "uctwIPServer";
            this.uctwIPServer.PasswordChar = false;
            this.uctwIPServer.placeHolder = "Input IP Server";
            this.uctwIPServer.Readonly = false;
            this.uctwIPServer.ShortcutsEnabled = true;
            this.uctwIPServer.Size = new System.Drawing.Size(400, 35);
            this.uctwIPServer.TabIndex = 3;
            this.uctwIPServer.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwIPServer.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwIPServer.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwIPServer.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // lbTillNo
            // 
            this.lbTillNo.BackColor = System.Drawing.Color.Transparent;
            this.lbTillNo.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTillNo.Location = new System.Drawing.Point(400, 16);
            this.lbTillNo.Name = "lbTillNo";
            this.lbTillNo.Size = new System.Drawing.Size(75, 35);
            this.lbTillNo.TabIndex = 17;
            this.lbTillNo.Text = "Till No :";
            this.lbTillNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwStoreCode
            // 
            this.uctwStoreCode.BackColor = System.Drawing.Color.White;
            this.uctwStoreCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwStoreCode.BackgroundImage")));
            this.uctwStoreCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwStoreCode.EnabledUC = true;
            this.uctwStoreCode.IsAmount = false;
            this.uctwStoreCode.IsKeyBoardForScan = false;
            this.uctwStoreCode.IsLarge = false;
            this.uctwStoreCode.IsNumber = false;
            this.uctwStoreCode.IsSetFormat = false;
            this.uctwStoreCode.IsValidateNumberZero = false;
            this.uctwStoreCode.IsValidateTextEmpty = false;
            this.uctwStoreCode.Location = new System.Drawing.Point(231, 16);
            this.uctwStoreCode.MaxLength = 32767;
            this.uctwStoreCode.Name = "uctwStoreCode";
            this.uctwStoreCode.PasswordChar = false;
            this.uctwStoreCode.placeHolder = "Store Code";
            this.uctwStoreCode.Readonly = false;
            this.uctwStoreCode.ShortcutsEnabled = true;
            this.uctwStoreCode.Size = new System.Drawing.Size(150, 35);
            this.uctwStoreCode.TabIndex = 1;
            this.uctwStoreCode.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwStoreCode.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwStoreCode.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwStoreCode.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // lbStoreCode
            // 
            this.lbStoreCode.BackColor = System.Drawing.Color.Transparent;
            this.lbStoreCode.Font = new System.Drawing.Font("Prompt", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbStoreCode.Location = new System.Drawing.Point(25, 16);
            this.lbStoreCode.Name = "lbStoreCode";
            this.lbStoreCode.Size = new System.Drawing.Size(200, 35);
            this.lbStoreCode.TabIndex = 15;
            this.lbStoreCode.Text = "Store Code :";
            this.lbStoreCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uctwTillNo
            // 
            this.uctwTillNo.BackColor = System.Drawing.Color.White;
            this.uctwTillNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uctwTillNo.BackgroundImage")));
            this.uctwTillNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uctwTillNo.EnabledUC = true;
            this.uctwTillNo.IsAmount = false;
            this.uctwTillNo.IsKeyBoardForScan = false;
            this.uctwTillNo.IsLarge = false;
            this.uctwTillNo.IsNumber = false;
            this.uctwTillNo.IsSetFormat = false;
            this.uctwTillNo.IsValidateNumberZero = false;
            this.uctwTillNo.IsValidateTextEmpty = false;
            this.uctwTillNo.Location = new System.Drawing.Point(481, 16);
            this.uctwTillNo.MaxLength = 32767;
            this.uctwTillNo.Name = "uctwTillNo";
            this.uctwTillNo.PasswordChar = false;
            this.uctwTillNo.placeHolder = "Till No";
            this.uctwTillNo.Readonly = false;
            this.uctwTillNo.ShortcutsEnabled = true;
            this.uctwTillNo.Size = new System.Drawing.Size(150, 35);
            this.uctwTillNo.TabIndex = 2;
            this.uctwTillNo.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.uctwTillNo.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uctwTillNo.TextBoxKeydown += new System.EventHandler(this.uctw_TextBoxKeydown);
            this.uctwTillNo.Enter += new System.EventHandler(this.uctw_Enter);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ucTextBoxWithIcon1);
            this.panel1.Location = new System.Drawing.Point(117, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 575);
            this.panel1.TabIndex = 12;
            this.panel1.Visible = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(34, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 35);
            this.label7.TabIndex = 33;
            this.label7.Text = "IP Server :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon7
            // 
            this.ucTextBoxWithIcon7.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon7.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon7.EnabledUC = true;
            this.ucTextBoxWithIcon7.IsAmount = false;
            this.ucTextBoxWithIcon7.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon7.IsLarge = true;
            this.ucTextBoxWithIcon7.IsNumber = false;
            this.ucTextBoxWithIcon7.IsSetFormat = false;
            this.ucTextBoxWithIcon7.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon7.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon7.Location = new System.Drawing.Point(240, 335);
            this.ucTextBoxWithIcon7.MaxLength = 32767;
            this.ucTextBoxWithIcon7.Name = "ucTextBoxWithIcon7";
            this.ucTextBoxWithIcon7.PasswordChar = false;
            this.ucTextBoxWithIcon7.placeHolder = "IP Server";
            this.ucTextBoxWithIcon7.Readonly = false;
            this.ucTextBoxWithIcon7.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon7.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon7.TabIndex = 32;
            this.ucTextBoxWithIcon7.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon7.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(34, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 35);
            this.label6.TabIndex = 31;
            this.label6.Text = "IP Server :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon6
            // 
            this.ucTextBoxWithIcon6.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon6.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon6.EnabledUC = true;
            this.ucTextBoxWithIcon6.IsAmount = false;
            this.ucTextBoxWithIcon6.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon6.IsLarge = true;
            this.ucTextBoxWithIcon6.IsNumber = false;
            this.ucTextBoxWithIcon6.IsSetFormat = false;
            this.ucTextBoxWithIcon6.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon6.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon6.Location = new System.Drawing.Point(240, 285);
            this.ucTextBoxWithIcon6.MaxLength = 32767;
            this.ucTextBoxWithIcon6.Name = "ucTextBoxWithIcon6";
            this.ucTextBoxWithIcon6.PasswordChar = false;
            this.ucTextBoxWithIcon6.placeHolder = "IP Server";
            this.ucTextBoxWithIcon6.Readonly = false;
            this.ucTextBoxWithIcon6.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon6.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon6.TabIndex = 30;
            this.ucTextBoxWithIcon6.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon6.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(34, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 35);
            this.label5.TabIndex = 29;
            this.label5.Text = "IP Server :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon5
            // 
            this.ucTextBoxWithIcon5.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon5.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon5.EnabledUC = true;
            this.ucTextBoxWithIcon5.IsAmount = false;
            this.ucTextBoxWithIcon5.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon5.IsLarge = true;
            this.ucTextBoxWithIcon5.IsNumber = false;
            this.ucTextBoxWithIcon5.IsSetFormat = false;
            this.ucTextBoxWithIcon5.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon5.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon5.Location = new System.Drawing.Point(240, 238);
            this.ucTextBoxWithIcon5.MaxLength = 32767;
            this.ucTextBoxWithIcon5.Name = "ucTextBoxWithIcon5";
            this.ucTextBoxWithIcon5.PasswordChar = false;
            this.ucTextBoxWithIcon5.placeHolder = "IP Server";
            this.ucTextBoxWithIcon5.Readonly = false;
            this.ucTextBoxWithIcon5.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon5.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon5.TabIndex = 28;
            this.ucTextBoxWithIcon5.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon5.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(34, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 35);
            this.label4.TabIndex = 27;
            this.label4.Text = "IP Server :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon4
            // 
            this.ucTextBoxWithIcon4.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon4.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon4.EnabledUC = true;
            this.ucTextBoxWithIcon4.IsAmount = false;
            this.ucTextBoxWithIcon4.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon4.IsLarge = true;
            this.ucTextBoxWithIcon4.IsNumber = false;
            this.ucTextBoxWithIcon4.IsSetFormat = false;
            this.ucTextBoxWithIcon4.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon4.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon4.Location = new System.Drawing.Point(240, 191);
            this.ucTextBoxWithIcon4.MaxLength = 32767;
            this.ucTextBoxWithIcon4.Name = "ucTextBoxWithIcon4";
            this.ucTextBoxWithIcon4.PasswordChar = false;
            this.ucTextBoxWithIcon4.placeHolder = "IP Server";
            this.ucTextBoxWithIcon4.Readonly = false;
            this.ucTextBoxWithIcon4.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon4.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon4.TabIndex = 26;
            this.ucTextBoxWithIcon4.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon4.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(34, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 35);
            this.label3.TabIndex = 25;
            this.label3.Text = "IP Server :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon3
            // 
            this.ucTextBoxWithIcon3.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon3.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon3.EnabledUC = true;
            this.ucTextBoxWithIcon3.IsAmount = false;
            this.ucTextBoxWithIcon3.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon3.IsLarge = true;
            this.ucTextBoxWithIcon3.IsNumber = false;
            this.ucTextBoxWithIcon3.IsSetFormat = false;
            this.ucTextBoxWithIcon3.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon3.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon3.Location = new System.Drawing.Point(240, 144);
            this.ucTextBoxWithIcon3.MaxLength = 32767;
            this.ucTextBoxWithIcon3.Name = "ucTextBoxWithIcon3";
            this.ucTextBoxWithIcon3.PasswordChar = false;
            this.ucTextBoxWithIcon3.placeHolder = "IP Server";
            this.ucTextBoxWithIcon3.Readonly = false;
            this.ucTextBoxWithIcon3.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon3.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon3.TabIndex = 24;
            this.ucTextBoxWithIcon3.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon3.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(34, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 35);
            this.label2.TabIndex = 23;
            this.label2.Text = "IP Server :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon2
            // 
            this.ucTextBoxWithIcon2.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon2.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon2.EnabledUC = true;
            this.ucTextBoxWithIcon2.IsAmount = false;
            this.ucTextBoxWithIcon2.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon2.IsLarge = true;
            this.ucTextBoxWithIcon2.IsNumber = false;
            this.ucTextBoxWithIcon2.IsSetFormat = false;
            this.ucTextBoxWithIcon2.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon2.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon2.Location = new System.Drawing.Point(240, 99);
            this.ucTextBoxWithIcon2.MaxLength = 32767;
            this.ucTextBoxWithIcon2.Name = "ucTextBoxWithIcon2";
            this.ucTextBoxWithIcon2.PasswordChar = false;
            this.ucTextBoxWithIcon2.placeHolder = "IP Server";
            this.ucTextBoxWithIcon2.Readonly = false;
            this.ucTextBoxWithIcon2.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon2.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon2.TabIndex = 22;
            this.ucTextBoxWithIcon2.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon2.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(34, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 35);
            this.label1.TabIndex = 21;
            this.label1.Text = "IP Server :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucTextBoxWithIcon1
            // 
            this.ucTextBoxWithIcon1.BackColor = System.Drawing.Color.White;
            this.ucTextBoxWithIcon1.BackgroundImage = global::BJCBCPOS.Properties.Resources.textboxLarge_disable;
            this.ucTextBoxWithIcon1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTextBoxWithIcon1.EnabledUC = true;
            this.ucTextBoxWithIcon1.IsAmount = false;
            this.ucTextBoxWithIcon1.IsKeyBoardForScan = false;
            this.ucTextBoxWithIcon1.IsLarge = true;
            this.ucTextBoxWithIcon1.IsNumber = false;
            this.ucTextBoxWithIcon1.IsSetFormat = false;
            this.ucTextBoxWithIcon1.IsValidateNumberZero = false;
            this.ucTextBoxWithIcon1.IsValidateTextEmpty = false;
            this.ucTextBoxWithIcon1.Location = new System.Drawing.Point(240, 52);
            this.ucTextBoxWithIcon1.MaxLength = 32767;
            this.ucTextBoxWithIcon1.Name = "ucTextBoxWithIcon1";
            this.ucTextBoxWithIcon1.PasswordChar = false;
            this.ucTextBoxWithIcon1.placeHolder = "IP Server";
            this.ucTextBoxWithIcon1.Readonly = false;
            this.ucTextBoxWithIcon1.ShortcutsEnabled = true;
            this.ucTextBoxWithIcon1.Size = new System.Drawing.Size(400, 35);
            this.ucTextBoxWithIcon1.TabIndex = 20;
            this.ucTextBoxWithIcon1.Tag = BJCBCPOS_Model.UCTextBoxIconType.NoneAndDelete;
            this.ucTextBoxWithIcon1.TextBoxAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.BackColor = System.Drawing.Color.White;
            this.ucKeyboard1.currentInput = null;
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeyboard1.hideLanguageButton = true;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 0);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(150, 46);
            this.ucKeyboard1.TabIndex = 0;
            // 
            // frmSetupIni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSetupIni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmSetupIni";
            this.Load += new System.EventHandler(this.frmSetupIni_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnfrmConfig.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private UCKeyboard ucKeyboard1;
        private UCFooter ucFooter1;
        private UCHeader ucHeader1;
        private System.Windows.Forms.Panel pnfrmConfig;
        private UCTextBoxWithIcon uctwTillNo;
        private System.Windows.Forms.Label lbStoreCode;
        private UCTextBoxWithIcon uctwIPServer;
        private System.Windows.Forms.Label lbTillNo;
        private UCTextBoxWithIcon uctwStoreCode;
        private System.Windows.Forms.Label lbDBServerTrainning;
        private System.Windows.Forms.Label lbIPServerTrainning;
        private System.Windows.Forms.Label lbDBServerBackup;
        private System.Windows.Forms.Label lbIPServerBackup;
        private UCTextBoxWithIcon uctwDBServerTrainning;
        private UCTextBoxWithIcon uctwIPServerTrainning;
        private UCTextBoxWithIcon uctwDBServerBackup;
        private UCTextBoxWithIcon uctwIPServerBackup;
        private System.Windows.Forms.Label lbDBServer;
        private UCTextBoxWithIcon uctwDBServer;
        private System.Windows.Forms.Label lbIPServer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private UCTextBoxWithIcon ucTextBoxWithIcon1;
        private System.Windows.Forms.Label label7;
        private UCTextBoxWithIcon ucTextBoxWithIcon7;
        private System.Windows.Forms.Label label6;
        private UCTextBoxWithIcon ucTextBoxWithIcon6;
        private System.Windows.Forms.Label label5;
        private UCTextBoxWithIcon ucTextBoxWithIcon5;
        private System.Windows.Forms.Label label4;
        private UCTextBoxWithIcon ucTextBoxWithIcon4;
        private System.Windows.Forms.Label label3;
        private UCTextBoxWithIcon ucTextBoxWithIcon3;
        private System.Windows.Forms.Label label2;
        private UCTextBoxWithIcon ucTextBoxWithIcon2;
        private System.Windows.Forms.Label label9;
        private UCTextBoxWithIcon uctwCOMPort;
        private System.Windows.Forms.Label label8;
        private UCTextBoxWithIcon uctwPrinterName;
    }
}