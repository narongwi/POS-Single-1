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
            this.pnfrmConfig = new System.Windows.Forms.Panel();
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
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.ucFooter1 = new BJCBCPOS.UCFooter();
            this.ucHeader1 = new BJCBCPOS.UCHeader();
            this.ucKeyboard1 = new BJCBCPOS.UCKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnfrmConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.pnfrmConfig);
            this.splitContainer1.Panel1.Controls.Add(this.picLogo);
            this.splitContainer1.Panel1.Controls.Add(this.ucFooter1);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeader1);
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
            // pnfrmConfig
            // 
            this.pnfrmConfig.BackgroundImage = global::BJCBCPOS.Properties.Resources.Rectangle_258_3x;
            this.pnfrmConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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
            this.pnfrmConfig.Location = new System.Drawing.Point(200, 286);
            this.pnfrmConfig.Margin = new System.Windows.Forms.Padding(2);
            this.pnfrmConfig.Name = "pnfrmConfig";
            this.pnfrmConfig.Size = new System.Drawing.Size(650, 375);
            this.pnfrmConfig.TabIndex = 11;
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
            this.btnNo.Location = new System.Drawing.Point(28, 316);
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
            this.btnSave.Location = new System.Drawing.Point(231, 316);
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
            this.lbDBServerTrainning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            this.lbIPServerTrainning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            this.lbDBServerBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            this.lbIPServerBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            this.uctwDBServerTrainning.IsLarge = false;
            this.uctwDBServerTrainning.IsNumber = false;
            this.uctwDBServerTrainning.IsSetFormat = false;
            this.uctwDBServerTrainning.IsTextChange = true;
            this.uctwDBServerTrainning.Location = new System.Drawing.Point(231, 262);
            this.uctwDBServerTrainning.MaxLength = 32767;
            this.uctwDBServerTrainning.Name = "uctwDBServerTrainning";
            this.uctwDBServerTrainning.PasswordChar = false;
            this.uctwDBServerTrainning.placeHolder = "Database Server Trainning";
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
            this.uctwIPServerTrainning.IsLarge = false;
            this.uctwIPServerTrainning.IsNumber = false;
            this.uctwIPServerTrainning.IsSetFormat = false;
            this.uctwIPServerTrainning.IsTextChange = true;
            this.uctwIPServerTrainning.Location = new System.Drawing.Point(231, 221);
            this.uctwIPServerTrainning.MaxLength = 32767;
            this.uctwIPServerTrainning.Name = "uctwIPServerTrainning";
            this.uctwIPServerTrainning.PasswordChar = false;
            this.uctwIPServerTrainning.placeHolder = "IP Server Trainning";
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
            this.uctwDBServerBackup.IsLarge = false;
            this.uctwDBServerBackup.IsNumber = false;
            this.uctwDBServerBackup.IsSetFormat = false;
            this.uctwDBServerBackup.IsTextChange = true;
            this.uctwDBServerBackup.Location = new System.Drawing.Point(231, 180);
            this.uctwDBServerBackup.MaxLength = 32767;
            this.uctwDBServerBackup.Name = "uctwDBServerBackup";
            this.uctwDBServerBackup.PasswordChar = false;
            this.uctwDBServerBackup.placeHolder = "Database Server Backup";
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
            this.uctwIPServerBackup.IsLarge = false;
            this.uctwIPServerBackup.IsNumber = false;
            this.uctwIPServerBackup.IsSetFormat = false;
            this.uctwIPServerBackup.IsTextChange = true;
            this.uctwIPServerBackup.Location = new System.Drawing.Point(231, 139);
            this.uctwIPServerBackup.MaxLength = 32767;
            this.uctwIPServerBackup.Name = "uctwIPServerBackup";
            this.uctwIPServerBackup.PasswordChar = false;
            this.uctwIPServerBackup.placeHolder = "IP Server Backup";
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
            this.lbDBServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            this.uctwDBServer.IsLarge = false;
            this.uctwDBServer.IsNumber = false;
            this.uctwDBServer.IsSetFormat = false;
            this.uctwDBServer.IsTextChange = true;
            this.uctwDBServer.Location = new System.Drawing.Point(231, 98);
            this.uctwDBServer.MaxLength = 32767;
            this.uctwDBServer.Name = "uctwDBServer";
            this.uctwDBServer.PasswordChar = false;
            this.uctwDBServer.placeHolder = "Database Server";
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
            this.lbIPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
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
            this.uctwIPServer.IsLarge = false;
            this.uctwIPServer.IsNumber = false;
            this.uctwIPServer.IsSetFormat = false;
            this.uctwIPServer.IsTextChange = true;
            this.uctwIPServer.Location = new System.Drawing.Point(231, 57);
            this.uctwIPServer.MaxLength = 32767;
            this.uctwIPServer.Name = "uctwIPServer";
            this.uctwIPServer.PasswordChar = false;
            this.uctwIPServer.placeHolder = "IP Server";
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
            this.lbTillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTillNo.Location = new System.Drawing.Point(400, 19);
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
            this.uctwStoreCode.IsLarge = false;
            this.uctwStoreCode.IsNumber = false;
            this.uctwStoreCode.IsSetFormat = false;
            this.uctwStoreCode.IsTextChange = true;
            this.uctwStoreCode.Location = new System.Drawing.Point(231, 19);
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
            this.lbStoreCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbStoreCode.Location = new System.Drawing.Point(25, 19);
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
            this.uctwTillNo.IsLarge = false;
            this.uctwTillNo.IsNumber = false;
            this.uctwTillNo.IsSetFormat = false;
            this.uctwTillNo.IsTextChange = false;
            this.uctwTillNo.Location = new System.Drawing.Point(481, 19);
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
            // picLogo
            // 
            this.picLogo.Image = global::BJCBCPOS.Properties.Resources.NoPath_3x;
            this.picLogo.Location = new System.Drawing.Point(451, 80);
            this.picLogo.Margin = new System.Windows.Forms.Padding(2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(141, 187);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 10;
            this.picLogo.TabStop = false;
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
            this.ucHeader1.showLanguage = false;
            this.ucHeader1.showLine = false;
            this.ucHeader1.showLogout = false;
            this.ucHeader1.showMainMenu = false;
            this.ucHeader1.showMember = false;
            this.ucHeader1.showScanner = false;
            this.ucHeader1.Size = new System.Drawing.Size(1024, 43);
            this.ucHeader1.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private UCKeyboard ucKeyboard1;
        private UCFooter ucFooter1;
        private UCHeader ucHeader1;
        private System.Windows.Forms.Panel pnfrmConfig;
        private UCTextBoxWithIcon uctwTillNo;
        private System.Windows.Forms.PictureBox picLogo;
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
    }
}