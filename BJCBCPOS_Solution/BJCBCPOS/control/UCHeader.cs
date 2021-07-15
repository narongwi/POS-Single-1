using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using BJCBCPOS_Process;
using System.IO;

namespace BJCBCPOS
{
    public partial class UCHeader : UserControl
    {
        private Timer tm = new Timer();
        private bool _showMainMenu = true;
        private bool _showCurrentText = true;
        private bool _showMember = true;
        private bool _showScanner = true;
        private bool _showCalculator = true;
        private bool _showLogout = false;
        private bool _showLine = true;
        private bool _showAlert = true;
        private bool _showLanguage = true;
        private bool _showLockScreen = true;
        private bool _alertStatus = false;
        private bool _nameVisible = false;
        private bool _showHamberGetItem = true;
        private bool _isMemberShowButtonBack = true;
        private bool memberActivate = false;
        private FunctionID _alertFunctionId;
        private MenuProcess process;
        private Language curLang;
        private frmMonitorCustomer frmMon;
        private string _hambergerName = "MenuHamberger";
        private UCHamberger _ucHamb = null;
        private UCMember _ucMember = null;
        private UCPCMan _ucPCMan = null;
        private UCEmployeeDiscount _ucEmployee = null;

        public PageBackFormPayment pageTest;

        public UCHeader()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(UCHeader_Disposed);

            //if (ProgramConfig.language == Language.ENGLISH)
            //{
            //    btnLanguage.BackgroundImage = Properties.Resources.usa_enable;
            //}
            //else if (ProgramConfig.language == Language.LAOS)
            //{
            //    btnLanguage.BackgroundImage = Properties.Resources.Laos;
            //}
            //else if (ProgramConfig.language == Language.THAI)
            //{
            //    btnLanguage.BackgroundImage = Properties.Resources.Thai;
            //}

            memberActivate = FixedData.member_function_allow;
            if (memberActivate)
            {
                btnMember.BackgroundImage = Properties.Resources.member_activate;
            }
            else
            {
                btnMember.BackgroundImage = Properties.Resources.member_non_activate;
            }

            if (ProgramConfig.langaugeType == "Single")
            {
                btnLanguage.Enabled = false;
            }
            else
            {
                btnLanguage.Enabled = true;
            }
        }

        #region Property

        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                lbDateTimeHeader.ForeColor = value;
                pipe1.ForeColor = value;
                pipe2.ForeColor = value;
                pipe3.ForeColor = value;
                pipe4.ForeColor = value;
                pipe5.ForeColor = value;
                pipe6.ForeColor = value;
                pipe7.ForeColor = value;
                lbLogout.ForeColor = value;
            }
        }

        //[Category("Custom Property")]
        //[Description("Text display in member button.")]
        //[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        //public string memberText
        //{
        //    get { return lbMember.Text; }
        //    set
        //    {
        //        lbMember.Text = value;
        //    }
        //}

        [Category("Custom Property")]
        [Description("Text display in member button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string nameText
        {
            get { return lbName.Text; }
            set
            {
                if (value == "")
                {
                    pnNameSize = new Size(50, 43);
                }

                lbName.Text = value;

                if (frmMon != null)
                {
                    frmMon.lbTxtMember.Text = lbName.Text;
                } 
            }
        }

        [Category("Custom Property")]
        [Description("Text display in member button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool nameVisible
        {
            get { return this._nameVisible; }
            set
            {
                this._nameVisible = value;
                lbName.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("Text display in member button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size pnNameSize
        {
            get { return pnMember.Size; }
            set
            {
                pnMember.Size = value;
            }
        }

        [Category("Custom Property")]
        [Description("Text display in logout button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string logoutText
        {
            get { return lbLogout.Text; }
            set
            {
                lbLogout.Text = value;
            }
        }

        [Category("Custom Property")]
        [Description("display main menu button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showMainMenu
        {
            get { return this._showMainMenu; }
            set
            {
                this._showMainMenu = value;
                btnMainMenu.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display current menu text.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showCurrentMenuText
        {
            get { return this._showCurrentText; }
            set
            {
                this._showCurrentText = value;
                pnCurrentText.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("text to display in current menu text title 1.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string currentMenuTitle1
        {
            get { return lbTitle1.Text; }
            set
            {
                this.lbTitle1.Text = value;
                updateCurrentMenuText();
            }
        }

        [Category("Custom Property")]
        [Description("text to display in current menu text title 2.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string currentMenuTitle2
        {
            get { return lbTitle2.Text; }
            set
            {
                this.lbTitle2.Text = value;
                updateCurrentMenuText();
            }
        }

        [Category("Custom Property")]
        [Description("text to display in current menu text title 3.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string currentMenuTitle3
        {
            get { return lbTitle3.Text; }
            set
            {
                this.lbTitle3.Text = value;
                updateCurrentMenuText();
            }
        }

        [Category("Custom Property")]
        [Description("display member button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showMember
        {
            get { return this._showMember; }
            set
            {
                this._showMember = value;
                pnMember.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display calculator button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showCalculator
        {
            get { return this._showCalculator; }
            set
            {
                this._showCalculator = value;
                pnCalculator.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display scanner button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showScanner
        {
            get { return this._showScanner; }
            set
            {
                this._showScanner = value;
                pnScanner.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display logout button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showLogout
        {
            get { return this._showLogout; }
            set
            {
                this._showLogout = value;
                pnLogout.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display language button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showLanguage
        {
            get { return this._showLanguage; }
            set
            {
                this._showLanguage = value;
                btnLanguage.Visible = value;
                pipe5.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display underline of header.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showLine
        {
            get { return this._showLine; }
            set
            {
                this._showLine = value;
                picLine.Visible = value;
            }
        }

        [Category("Cashier Message")]
        [Description("display alert button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showAlert
        {
            get { return this._showAlert; }
            set
            {
                this._showAlert = value;
                pnAlert.Visible = value;
            }
        }

        [Category("Custom Property")]
        [Description("display lock screen button.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showLockScreen
        {
            get { return this._showLockScreen; }
            set
            {
                this._showLockScreen = value;
                pn_LockScreen.Visible = value;
            }
        }

        [Category("Cashier Message")]
        [Description("alert message enabled.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool alertEnabled
        {
            get { return btnAlert.Enabled; }
            set
            {
                btnAlert.Enabled = value;
            }
        }

        [Category("Cashier Message")]
        [Description("alert message status.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool alertStatus
        {
            get { return this._alertStatus; }
            set
            {
                this._alertStatus = value;
                updateAlertStatus();
            }
        }

        public FunctionID alertFunctionID
        {
            get
            {
                return this._alertFunctionId;
            }
            set
            {
                this._alertFunctionId = value;
            }
        }

        [Category("Custom Property")]
        [Description("display current menu text.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showHamberGetItm
        {
            get { return this._showHamberGetItem; }
            set
            {
                this._showHamberGetItem = value;
            }
        }

        [Category("Custom Property")]
        [Description("display current menu text.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool showMember_ButtonBack
        {
            get { return this._isMemberShowButtonBack; }
            set
            {
                this._isMemberShowButtonBack = value;
            }
        }

        #endregion

        #region Event

        [Category("Action")]
        [Description("Occurs when the main menu button is clicked.")]
        [Browsable(true)]
        public event EventHandler MainMenuClick;

        [Category("Action")]
        [Description("Occurs when the logout button is clicked.")]
        [Browsable(true)]
        public event EventHandler LogoutClick;

        [Category("Action")]
        [Description("Occurs when the language button is clicked.")]
        [Browsable(true)]
        public event EventHandler LanguageClick;

        [Category("Action")]
        [Description("Occurs when the calculator button is clicked.")]
        [Browsable(true)]
        public event EventHandler CalculatorClick;

        [Category("Action")]
        [Description("Occurs when the scanner button is clicked.")]
        [Browsable(true)]
        public event EventHandler ScannerClick;

        [Category("Action")]
        [Description("Occurs when the title 1 button is clicked.")]
        [Browsable(true)]
        public event EventHandler Title1Click;

        [Category("Action")]
        [Description("Occurs when the title 2 button is clicked.")]
        [Browsable(true)]
        public event EventHandler Title2Click;

        [Category("Action")]
        [Description("Occurs when the alert button is clicked.")]
        [Browsable(true)]
        public event EventHandler AlertClick;

        [Category("Action")]
        [Description("Occurs when the menu hamberger item is clicked.")]
        [Browsable(true)]
        public event EventHandler HambergerItemClick;

        #region Member
        [Category("Member")]
        [Description("Occurs when the member button is clicked.")]
        [Browsable(true)]
        public event EventHandler MemberClick;

        [Category("Member")]
        [Description("Occurs when the network lost.")]
        [Browsable(true)]
        public event UCMember.MemberConnectionLostHandler MemberCatchNetWorkConnectionException;

        [Category("Member")]
        [Description("Occurs when the member button is clicked.")]
        [Browsable(true)]
        public event EventHandler MemberButtonBackClick;

        [Category("Member")]
        [Description("Occurs when the click button back.")]
        [Browsable(true)]
        public event EventHandler MemberEnterFromButton;

        [Category("Member")]
        [Description("Occurs when the member button is enter")]
        [Browsable(true)]
        public event EventHandler MemberIconClick;
        #endregion

        #region PCMan

        [Category("PC-Man")]
        [Description("Occurs when the network lost.")]
        [Browsable(true)]
        public event UCPCMan.PCManConnectionLostHandler PCManCatchNetWorkConnectionException;

        [Category("PC-Man")]
        [Description("Occurs when the click button back.")]
        [Browsable(true)]
        public event EventHandler PCManButtonBackClick;

        [Category("PC-Man")]
        [Description("Occurs when the click button enter")]
        [Browsable(true)]
        public event EventHandler PCManEnterFromButton;

        #endregion

        #region Employee

        [Category("Action")]
        [Description("Occurs when the network lost.")]
        [Browsable(true)]
        public event UCEmployeeDiscount.EmployeeConnectionLostHandler EmployeeCatchNetWorkConnectionException;

        [Category("Action")]
        [Description("Occurs when the click button back")]
        [Browsable(true)]
        public event EventHandler EmployeeButtonBackClick;

        [Category("Action")]
        [Description("Occurs when the click button enter")]
        [Browsable(true)]
        public event EventHandler EmployeeEnterFromButton;

        #endregion

        #endregion

        private void UCHeader_Load(object sender, EventArgs e)
        {
            if (_showLanguage)
            {
                tm.Tick += new EventHandler(timer1_Tick);
                tm.Interval = 500;
                tm.Enabled = true;
                CheckCurrentLanguage();
            }

            if (frmMon == null)
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item is frmMonitorCustomer)
                    {
                        frmMon = (frmMonitorCustomer)item;
                        break;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var a = this.FindForm();
            lbDateTimeHeader.Text = DateTime.Now.ToString("dd/MM/yyyy")
                + " " + DateTime.Now.ToString("HH:mm:ss");
            if (a != null && Form.ActiveForm == a && ProgramConfig.langaugeType != "Single")
            {

                curLang = curLang ?? ProgramConfig.language;
                if (curLang != ProgramConfig.language) 
                {
                    CheckCurrentLanguage();
                    curLang = ProgramConfig.language;
                }
               
    
                //int idx = Array.IndexOf<Language>(ProgramConfig.listActiveLanguage.ToArray(), new Language(ProgramConfig.language.ID));

                //string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                //string imageName = ProgramConfig.listActiveLanguage[idx].ImageName;

                //if (btnLanguage.BackgroundImage != Image.FromFile(appPath + pathIconLanguage + imageName))
                //{
                //    btnLanguage.BackgroundImage = Image.FromFile(appPath + pathIconLanguage + imageName);
                //    //CheckCurrentLanguage();
                //}
                


                //if (ProgramConfig.language == Language.ENGLISH && btnLanguage.BackgroundImage != Properties.Resources.usa_enable)
                //{
                //    btnLanguage.BackgroundImage = Properties.Resources.usa_enable;
                //    //curLang = ProgramConfig.language;
                //    //AppMessage.fillForm(curLang, a.Name, a);
                //}
                //else if (ProgramConfig.language == Language.LAOS && btnLanguage.BackgroundImage != Properties.Resources.Laos)
                //{
                //    btnLanguage.BackgroundImage = Properties.Resources.Laos;
                //    //curLang = ProgramConfig.language;
                //    //AppMessage.fillForm(curLang, a.Name, a);
                //}
                //else if (ProgramConfig.language == Language.THAI && btnLanguage.BackgroundImage != Properties.Resources.Thai)
                //{
                //    btnLanguage.BackgroundImage = Properties.Resources.Thai;
                //    //curLang = ProgramConfig.language;
                //    //AppMessage.fillForm(curLang, a.Name, a);
                //}
            }
        }

        public void btnMainMenu_Click(object sender, EventArgs e)
        {
            if (_showHamberGetItem)
            {
                if (this._showMainMenu)
                {
                    if (MainMenuClick != null)
                    {
                        if (_ucHamb == null)
                        {
                            MainMenuClick(this, e);
                            InitialMenu();
                        }
                        else
                        {
                            _ucHamb.Dispose();
                            _ucHamb = null;
                        }
                    }

                    //Program.control.ShowForm("frmMainMenu");

                    //this.FindForm().Dispose();
                }
            }
        }

        private void InitialMenu()
        {
            Form frm = this.FindForm();

            if (frm != null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("MenuId");
                dt.Columns.Add("ReferMenuId");
                dt.Columns.Add("MenuName");
                if (pageTest == PageBackFormPayment.NormalSale)
                {
                    DataRow dr = dt.NewRow();
                    dr["MenuId"] = "1";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "PERSONAL";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "7";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "SAVE HOLD ORDER";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "8";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "LOAD HOLD ORDER";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "6";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "ORDER TYPE";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "2";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "CANCEL";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "3";
                    dr["ReferMenuId"] = "1";
                    dr["MenuName"] = "MEMBER";
                    dt.Rows.Add(dr);

                    //dr = dt.NewRow();
                    //dr["MenuId"] = "4";
                    //dr["ReferMenuId"] = "1";
                    //dr["MenuName"] = "PC MAN";
                    //dt.Rows.Add(dr);

                    //dr = dt.NewRow();
                    //dr["MenuId"] = "5";
                    //dr["ReferMenuId"] = "1";
                    //dr["MenuName"] = "EMPLOYEE";
                    //dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "7";
                    dr["ReferMenuId"] = "20";
                    dr["MenuName"] = "SAVE ORDER";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "8";
                    dr["ReferMenuId"] = "20";
                    dr["MenuName"] = "ORDER SHOPPING";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "9";
                    dr["ReferMenuId"] = "20";
                    dr["MenuName"] = "FAST SCAN";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "10";
                    dr["ReferMenuId"] = "88";
                    dr["MenuName"] = "TEST 1";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "11";
                    dr["ReferMenuId"] = "88";
                    dr["MenuName"] = "TEST 11";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "12";
                    dr["ReferMenuId"] = "11";
                    dr["MenuName"] = "TEST 2";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "13";
                    dr["ReferMenuId"] = "11";
                    dr["MenuName"] = "TEST 22";
                    dt.Rows.Add(dr);
                }
                else if (pageTest == PageBackFormPayment.Deposit)
                {
                    DataRow dr = dt.NewRow();
                    dr["MenuId"] = "1";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "PERSONAL";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "3";
                    dr["ReferMenuId"] = "1";
                    dr["MenuName"] = "MEMBER";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["MenuId"] = "2";
                    dr["ReferMenuId"] = "0";
                    dr["MenuName"] = "CANCEL";
                    dt.Rows.Add(dr);

                }





                _ucHamb = new UCHamberger(dt);
                _ucHamb.Name = _hambergerName;
                _ucHamb.Leave += (s, e) =>
                    {
                        if (this.FindForm().ActiveControl is UCHeader)
                        {
                            ((UCHamberger)s).Dispose();
                        }
                    };
                _ucHamb.BackClick += (s, e) =>
                    {
                        _ucHamb = null;
                    };
                _ucHamb.HambergerItemClick += (s, e) => ucHamb_HambergerItemClick(s, e, _ucHamb);

                frm.Controls.Add(_ucHamb);
                frm.Controls.SetChildIndex(_ucHamb, 0);
                frm.Refresh();
                _ucHamb.Focus();
            }
        }

        private void ucHamb_HambergerItemClick(object sender, EventArgs e, UCHamberger pUcHamb)
        {
            UCHambergerItem hamItm = (UCHambergerItem)sender;
            if (hamItm.MenuID == MenuIdHamberger.Member)
            {
                DisableMember_Employee_PCMan();
                Member_Click(sender, e);
            }

            if (hamItm.MenuID == MenuIdHamberger.PC_Man)
            {
                DisableMember_Employee_PCMan();
                if (_ucPCMan == null)
                {
                    _ucPCMan = InitialUCPCMan();
                    AddToForm(_ucPCMan);
                }
                else
                {
                    _ucPCMan.ucTxt_PCMan.InpTxt = ProgramConfig.pcManID;
                    _ucPCMan.Visible = true;
                    _ucPCMan.BringToFront();
                }
                _ucPCMan.ucTxt_PCMan.FocusTxt();
            }

            if (hamItm.MenuID == MenuIdHamberger.Employee)
            {
                DisableMember_Employee_PCMan();
                if (_ucEmployee == null)
                {
                    _ucEmployee = InitialUCEmployee();
                    AddToForm(_ucEmployee);
                }
                else
                {
                    _ucEmployee.ucTxt_EmpID.InpTxt = ProgramConfig.employeeID;
                    _ucEmployee.Visible = true;
                    _ucEmployee.BringToFront();
                }
                _ucEmployee.ucTxt_EmpID.FocusTxt();
            }

            if (HambergerItemClick != null)
            {
                pUcHamb.Dispose();
                _ucHamb.Dispose();
                _ucHamb = null;
                this.FindForm().Refresh();
                HambergerItemClick(sender, e);
            }
        }

        private void DisableMember_Employee_PCMan()
        {
            if (_ucMember != null)
            {
                _ucMember.Dispose();
            }

            if (_ucPCMan != null)
            {
                if (String.IsNullOrEmpty(ProgramConfig.pcManID))
                {
                    _ucPCMan.Dispose();
                }
                else
                {
                    _ucPCMan.Visible = false;
                }
            }

            if (_ucEmployee != null)
            {
                if (String.IsNullOrEmpty(ProgramConfig.employeeID))
                {
                    _ucEmployee.Dispose();
                }
                else
                {
                    _ucEmployee.Visible = false;
                }
            }
        }

        private void ucHamb_Leave(object sender, EventArgs e)
        {
            if (this.FindForm().ActiveControl is UCHeader)
            {
                ((UCHambergerSubMenu)sender).Dispose();
            }
        }

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            if (ProgramConfig.listActiveLanguage.Count > 0)
            {
                Form frm = this.FindForm();
                //Control con = this.Parent;
                if (ProgramConfig.pnLanguage == null)
                {
                    InitialPanel(frm);
                }
                else
                {
                    //Check ถ้า form นี้มี dropdown แล้วไม่ต้อง add เพิ่ม
                    if (!frm.Controls.Contains(ProgramConfig.pnLanguage))
                    {
                        InitialPanel(frm);
                    }
                    else
                    {
                        ProgramConfig.pnLanguage.Visible = true;
                    }
                    ProgramConfig.pnLanguage.Focus();
                    //ProgramConfig.pnLanguage.Focus();
                }
            }

        }

        private void InitialPanel(Form frm)
        {
            int sizeHeight = (ProgramConfig.listActiveLanguage.Count * 45) + 6; // +6 for padding

            Panel pn = new Panel();
            pn.Parent = frm;
            pn.BackColor = Color.Transparent;
            pn.Location = new Point(836, 44);
            pn.Name = "pn_DropDownLanguage";
            pn.Size = new System.Drawing.Size(140, sizeHeight);           
            pn.BackgroundImageLayout = ImageLayout.Stretch;
            pn.AutoScroll = true;
            pn.Padding = new Padding(0, 3, 0, 0);
            

            switch (ProgramConfig.listActiveLanguage.Count)
            {
                case 1:
                    pn.BackgroundImage = Properties.Resources.pn_Language_1R;
                    break;
                case 2:
                    pn.BackgroundImage = Properties.Resources.pn_Language_2R;
                    break;
                case 3:
                    pn.BackgroundImage = Properties.Resources.pn_Language_3R;
                    break;
                default:
                    pn.BackgroundImage = Properties.Resources.pn_Language_4R;
                    break;
            }

            pn.Leave += new EventHandler(delegate(Object o, EventArgs a)
            {
                ((Panel)o).Visible = false;
            });

            pn.BringToFront();

            foreach (Language item in ProgramConfig.listActiveLanguage)
            {
                UCDropDownLanguage ucDDL = new UCDropDownLanguage();
                ucDDL.ClickUCDropDownLanguage -= UCDropDownLanguageClick;
                ucDDL.ClickUCDropDownLanguage += UCDropDownLanguageClick;
                ucDDL.TextLanguage = item.Code;
                ucDDL.ImageFlag = Utility.CreateImageLanguage(item);
                ucDDL.LanguageID = item.ID;       

                if (item == ProgramConfig.listActiveLanguage.Last())
                {
                    ucDDL.VisibleLine = false;
                }

                pn.Controls.Add(ucDDL);
                pn.Controls.SetChildIndex(ucDDL, 0);
            }

            ProgramConfig.pnLanguage = pn;
            frm.Controls.Add(pn);
            frm.Controls.SetChildIndex(pn, 0);
            pn.Focus();
        }

        private void UCDropDownLanguageClick(object sender, EventArgs e)
        {
            CheckCurrentLanguage();
            Form form = this.FindForm();
            if (form != null)
            {
                foreach (Form item in Application.OpenForms)
                {
                    AppMessage.fillForm(ProgramConfig.language, item);
                }

                updateKeyboard(form);

                if (LanguageClick != null)
                {
                    LanguageClick(this, e);
                }
            }

            Panel panel = (Panel)sender;
            panel.Visible = false;
        }

        private void CheckCurrentLanguage()
        {        
            btnLanguage.BackgroundImage = Utility.CreateImageLanguage();
        }

        private void ChangeNextLanguage()
        {
            int idx = Array.IndexOf<Language>(ProgramConfig.listActiveLanguage.ToArray(), new Language(ProgramConfig.language.ID));
            idx++;
            if (idx > ProgramConfig.listActiveLanguage.Count - 1)
            {
                idx = 0;
            }
            btnLanguage.BackgroundImage = Utility.CreateImageLanguage(idx);  //Image.FromFile(appPath + pathIconLanguage + imageName);
            ProgramConfig.language = ProgramConfig.listActiveLanguage[idx];
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            if (CalculatorClick != null)
            {
                CalculatorClick(this, e);
            }
            Program.control.ShowForm("frmCalculator");
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            if (ScannerClick != null)
            {
                ScannerClick(this, e);
            }
            Program.control.ShowForm("frmCheckProduct");
        }

        private void lbLogout_Click(object sender, EventArgs e)
        {
            if (this._showLogout)
            {
                if (this.LogoutClick != null)
                {
                    LogoutClick(this, e);
                    return;
                }

                // TODO: log out process
                Form currentForm = this.FindForm();
                if (currentForm.Name != "frmLogin")
                {
                    string responseMessage = ProgramConfig.message.get("frmMainMenu", "ConfirmLogout").message;
                    string helpMessage = ProgramConfig.message.get("frmMainMenu", "ConfirmLogout").help;
                    frmNotify dialog = new frmNotify(ResponseCode.Warning, responseMessage, helpMessage);

                    //frmNotify dialog = new frmNotify(ResponseCode.Warning, "ยืนยันออกจากระบบใช่หรือไม่");
                    if (dialog.ShowDialog(currentForm) == DialogResult.Yes)
                    {
                        if (process == null)
                        {
                            process = new MenuProcess();
                        }
                        StoreResult res = process.saveLogout();
                        if (res.response.next)
                        {
                            ProgramConfig.userId = string.Empty;
                            ProgramConfig.cashierName = string.Empty;
                            ProgramConfig.cashireAuthorizeResult = null;
                            ProgramConfig.superUserId = string.Empty;
                            ProgramConfig.superUserAuthorizeResult = null;
                            ProgramConfig.profile = null;
                            ProgramConfig.clearSaleConfig();
                            
                            Program.control.ShowForm("frmLogin");
                            currentForm.Dispose();
                        }
                        else
                        {
                            dialog = new frmNotify(res);
                            dialog.ShowDialog();
                        }

                        List<Form> opened = Application.OpenForms.Cast<Form>().ToList();
                        while (opened.Count > 5)
                        {
                            foreach (Form item in opened)
                            {
                                if (!item.Name.Equals("frmLogin") && !item.Name.Equals("frmMonitorCustomer") && !item.Name.Equals("frmMonitorCustomerFooter") && !item.Name.Equals("frmMonitor2Detail") && !item.Name.Equals("frmVDO"))
                                {
                                    try
                                    {
                                        Program.control.CloseForm(item.Name);
                                    }
                                    catch (Exception ex)
                                    {
                                        AppLog.writeLog(ex);
                                    }
                                }
                            }
                            opened = Application.OpenForms.Cast<Form>().ToList();
                        }
                    }
                }
            }
        }

        public void btnMember_Click(object sender, EventArgs e)
        {
            Member_Click(sender, e);
        }

        private void Member_Click(object sender, EventArgs e)
        {
            if (_ucMember == null)
            {
                _ucMember = InitialUCMember();
                AddToForm(_ucMember);         
            }
            else
            {
                _ucMember.ucTBWI_Member.InpTxt = ProgramConfig.memberCardNo;
                _ucMember.Visible = true;
                _ucMember.BringToFront();
            }
            _ucMember.ucTBWI_Member.FocusTxt();

            if (this._showMember && this.memberActivate && MemberClick != null)
            {
                MemberClick(_ucMember, e);
            }
        }

        private void AddToForm(UserControl uc)
        {
            Form frm = this.FindForm();
            frm.Controls.Add(uc);
            frm.Controls.SetChildIndex(uc, 1);
        }

        private UCMember InitialUCMember()
        {
            UCMember ucMember = BaseInitialUCRightSide<UCMember>();
            ucMember.IsShowButtonBack = showMember_ButtonBack;
            ucMember.Disposed += new EventHandler(delegate(Object o, EventArgs a)
            {
                _ucMember = null;
            });

            if (MemberCatchNetWorkConnectionException != null)
            {
                ucMember.MemberCatchNetWorkConnectionException += MemberCatchNetWorkConnectionException;
            }

            if (MemberButtonBackClick != null)
            {
                ucMember.MemberButtonBackClick += MemberButtonBackClick;
            }

            if (MemberEnterFromButton != null)
            {
                ucMember.MemberEnterFromButton += MemberEnterFromButton;
            }

            if (MemberIconClick != null)
            {
                ucMember.MemberIconClick += MemberIconClick;
            }

            return ucMember;
        }

        private T BaseInitialUCRightSide<T>() where T : UserControl, new()
        {
            T ucRSide = new T();
            ucRSide.BackColor = System.Drawing.Color.White;
            ucRSide.Location = new System.Drawing.Point(690, 44);
            ucRSide.Name = typeof(T).Name;
            ucRSide.Size = new System.Drawing.Size(334, 427);

            return ucRSide;
        }

        private UCPCMan InitialUCPCMan()
        {
            UCPCMan ucPCMan = BaseInitialUCRightSide<UCPCMan>();
            ucPCMan.Disposed += new EventHandler(delegate(Object o, EventArgs a)
            {
                _ucPCMan = null;
            });

            if (PCManCatchNetWorkConnectionException != null)
            {
                ucPCMan.PCManCatchNetWorkConnectionException += PCManCatchNetWorkConnectionException;
            }

            if (PCManButtonBackClick != null)
            {
                ucPCMan.PCManButtonBackClick += PCManButtonBackClick;
            }

            if (PCManEnterFromButton != null)
            {
                ucPCMan.PCManEnterFromButton += PCManEnterFromButton;
            }

            return ucPCMan;
        }

        private UCEmployeeDiscount InitialUCEmployee()
        {
            UCEmployeeDiscount ucEmp = BaseInitialUCRightSide<UCEmployeeDiscount>();
            ucEmp.Disposed += new EventHandler(delegate(Object o, EventArgs a)
            {
                _ucEmployee = null;
            });

            if (EmployeeCatchNetWorkConnectionException != null)
            {
                ucEmp.EmployeeCatchNetWorkConnectionException += EmployeeCatchNetWorkConnectionException;
            }

            if (EmployeeButtonBackClick != null)
            {
                ucEmp.EmployeeButtonBackClick += EmployeeButtonBackClick;
            }

            if (EmployeeEnterFromButton != null)
            {
                ucEmp.EmployeeEnterFromButton += EmployeeEnterFromButton;
            }

            return ucEmp;
        }

        private void updateCurrentMenuText()
        {
            if (this._showCurrentText)
            {
                if (string.IsNullOrEmpty(this.lbTitle3.Text))
                {
                    lbTitle3.Visible = false;
                    lbTitleSep2.Visible = false;
                    if (string.IsNullOrEmpty(this.lbTitle2.Text))
                    {
                        lbTitle2.Visible = false;
                        lbTitleSep1.Visible = false;

                        lbTitle1.ForeColor = Color.Gray;
                    }
                    else
                    {
                        lbTitle2.Visible = true;
                        lbTitleSep1.Visible = true;
                        lbTitle2.ForeColor = Color.Gray;
                        lbTitle1.ForeColor = Color.ForestGreen;
                    }
                }
                else
                {
                    lbTitle3.Visible = true;
                    lbTitleSep2.Visible = true;
                    lbTitle2.Visible = true;
                    if (Title2Click != null)
                    {
                        lbTitle2.Font = new Font(lbTitle2.Font, FontStyle.Underline);
                    }
                    lbTitleSep1.Visible = true;
                    lbTitle1.Visible = true;

                    lbTitle3.ForeColor = Color.Gray;
                    lbTitle2.ForeColor = Color.ForestGreen;
                    lbTitle1.ForeColor = Color.ForestGreen;
                }
            }
        }

        private void lbTitle1_Click(object sender, EventArgs e)
        {
            if (this._showCurrentText)
            {
                if (Title1Click != null)
                {
                    Title1Click(this, e);
                }

                // close current page
                //this.FindForm().Dispose();
            }
        }

        private void lbTitle2_Click(object sender, EventArgs e)
        {
            if (this._showCurrentText)
            {
                if (Title2Click != null && (Color)lbTitle2.ForeColor == Color.ForestGreen)
                {
                    Title2Click(this, e);
                }

                // close current page
                //if ((Color)lbTitle2.ForeColor == Color.ForestGreen)
                //    this.FindForm().Dispose();
            }
        }

        private void updateKeyboard(Control control)
        {
            if (control is UCKeyboard)
            {
                ((UCKeyboard)control).updateLanguage();
            }
            else
            {
                foreach (Control item in control.Controls)
                {
                    updateKeyboard(item);
                }
            }
        }

        private void btnAlert_Click(object sender, EventArgs e)
        {
            if (alertEnabled)
            {
                frmCashireMessage mes = new frmCashireMessage();
                mes.function = this._alertFunctionId;
                mes.ShowDialog();

                if (_alertStatus)
                {
                    _alertStatus = false;
                }
                updateAlertStatus();
            }

            if (AlertClick != null)
            {
                AlertClick(this, e);
            }
        }

        private void updateAlertStatus()
        {
            if (_alertStatus)
            {
                btnAlert.BackgroundImage = Properties.Resources.alarm__1_;
            }
            else
            {
                btnAlert.BackgroundImage = Properties.Resources.alarm__2_;
            }
        }

        private void UCHeader_Disposed(object sender, EventArgs e)
        {
            tm.Stop();
            tm.Dispose();
        }

        private void pipe1_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe1);            
        }

        private void pipe2_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe2);   
        }

        private void pipe3_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe3); 
        }

        private void pipe4_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe4); 
        }

        private void pipe5_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe5); 
        }

        private void pipe6_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe6); 
        }

        private void pipe7_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(pipe7); 
        }

        private void lbDateTimeHeader_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbDateTimeHeader); 
        }

        private void lbTitleSep2_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbTitleSep2); 
        }

        private void lbTitleSep1_FontChanged(object sender, EventArgs e)
        {
            Utility.SetStandardFont(lbTitleSep1); 
        }

        private void btnLock_Screen_Click(object sender, EventArgs e)
        {
            Program.control.ShowForm("frmLockScreen");
        }


    }
}
