using BJCBCPOS.OtherServices.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJCBCPOS.OtherServices.Forms {
    public partial class UIForm : Form {
        public UIForm() {
            InitializeComponent();
            FontIconUtils.InitialiseFont();
        }

        private void SimCardForm_Load(object sender,EventArgs e) {

        }

        private void deftsoftButton1_Click(object sender,EventArgs e) {
            MessageBox.Show(deftsoftTextbox1.Texts,"Sample");
        }

        private void SimCardForm_Paint(object sender,PaintEventArgs e) {
            SetStyle(ControlStyles.UserPaint,true);
        }

        private void deftsoftIcon1_Click(object sender,EventArgs e) {
                    }
    }
}
