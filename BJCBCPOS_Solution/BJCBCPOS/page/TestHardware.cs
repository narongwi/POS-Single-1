using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPOS_Model;
using System.Collections;

namespace BJCBCPOS
{
    public partial class TestHardware : Form
    {
        public TestHardware()
        {
            InitializeComponent();
            ucKeyboard1.currentInput = textBox3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Hardware.initialHardware())
            {
                Hardware.addDrawerListeners(drawerStatus);
                textBox3.Text += "Initial printer and cash drawer success." + Environment.NewLine;
            }
            else
            {
                textBox3.Text += "Initial printer and cash drawer error, Please check log file." + Environment.NewLine;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList message = new ArrayList();
            message.Add("Test Termal Printer");
            message.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            if (Hardware.printTermal(message))
            {
                textBox3.Text += "Print success." + Environment.NewLine;
            }
            else
            {
                textBox3.Text += "Print error, Please check log file." + Environment.NewLine;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Hardware.openDrawer())
            {
                textBox3.Text += "Open cash drawer success." + Environment.NewLine;
            }
            else
            {
                textBox3.Text += "Open cash drawer error, Please check log file." + Environment.NewLine;
            }
        }

        private void drawerStatus(string status)
        {
            textBox3.Text += "drawer status return = " + status + " at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine;
        }
    }
}
