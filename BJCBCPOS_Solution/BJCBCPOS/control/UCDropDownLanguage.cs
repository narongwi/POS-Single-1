using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class UCDropDownLanguage : UserControl
    {

        #region Event

        [Category("Action")]
        [Description("Occurs when click from user control.")]
        [Browsable(true)]
        public event EventHandler ClickUCDropDownLanguage;

        #endregion

        #region Properties

        public Image ImageFlag {
            set
            {
                pic_Flag.BackgroundImage = value;
            }
        }

        public bool VisibleLine
        {
            get
            {
                return pic_Line.Visible;
            }
            set
            {
                pic_Line.Visible = value;
            }
        }

        public string TextLanguage
        {
            get
            {
               return lbLanguageName.Text;
            }
            set
            {
                lbLanguageName.Text = value;
            }
        }

        public int LanguageID { get; set; }
        
        #endregion

        public UCDropDownLanguage()
        {
            InitializeComponent();
        }

        private void UCDropDownLanguage_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Top;
            lbLanguageName.ForeColor = Color.Black;
        }

        private void DropDownLanguage_Click(object sender, EventArgs e)
        {
            if (ClickUCDropDownLanguage != null)
            {
                Panel ctrl = (Panel)this.Parent;
                ProgramConfig.language = new Language(this.LanguageID);
                ClickUCDropDownLanguage(ctrl, e);
            }
        }
    }
}
