using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BJCBCPRNFFTI.FFTIPrinting;
using CrystalDecisions.CrystalReports.Engine;

namespace BJCBCPOS
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            edcControl1.EnableLog = true;
            edcControl1.EDCKey = "D9t42q$Or5tHw6";
            edcControl1.EDCPort = "COM5";
            edcControl1.EDCPortSetting = "9600,N,8,1";
            edcControl1.EDCTimeout = 90000;

            if (edcControl1.EDCOpen())
            {
                if (edcControl1.SendSale_VerifoneSCO(Convert.ToDouble("1.00"), "", ""))
                {

                }
            }
            edcControl1.EDCClose();

            //Printing ffti = new Printing();
            //DataTable dt = ffti.StartPreview(textBox1.Text, "101");

            //ReportDocument cr = new ReportDocument();
            //cr.Load(@"D:\BIG_C\BJCBCPOS_Source\BJCBCPOS-V2.03.00\BJCBCPOS_Solution\BJCBCPOS\bin\Debug\Fulltax.rpt");
            //cr.SetDataSource(dt);
            //cr.PrintOptions.PrinterName = "PrimoPDF";
            //cr.PrintToPrinter(1, false, 0, 0);
        }
    }
}
