using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmLoading : Form
    {
        //private bool IsPaint = false; 

        public frmLoading()
        {
            InitializeComponent();
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static frmLoading splashForm;

        //public void showLoad()
        //{
        //    this.Show();
        //    this.Refresh();
        //    this.Update();
        //    //ProgramConfig.loadingStatus = LoadingStatus.Show;
        //}

        //public void closeLoad()
        //{
        //    ProgramConfig.loadingStatus = LoadingStatus.Close;
        //}

        public static void showLoading()
        {
            //ProgramConfig.loadingStatus = LoadingStatus.Show;
            //if (Program.control != null)
            //{
            //    Program.control.ShowForm("frmLoading");
            //}

            //splashForm = new frmLoading();
            //splashForm.timer1 = new System.Windows.Forms.Timer();
            //splashForm.timer1.Interval = 100;
            //splashForm.timer1.Tick += (s, e) =>
            //    {
            //        splashForm.timer1.Stop();
            //        splashForm.Show();                   
            //    };
            //splashForm.Show();

            closeLoading();
            // Make sure it is only launched once.    
            if (splashForm != null) return;
            splashForm = new frmLoading();
            Thread thread = new Thread(new ThreadStart(frmLoading.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ShowForm()
        {
            if (splashForm != null) Application.Run(splashForm);
        }

        public static void closeLoading()
        {
            try 
            {
                //Program.control.CloseForm("frmLoading");
                //ProgramConfig.loadingStatus = LoadingStatus.Close;
                //ProgramConfig.IsShowLoading = false;
                //if (Program.control != null)
                //    Program.control.CloseForm("frmLoading");

                //splashForm.Dispose();
                //splashForm = null;
                //splashForm.timer1.Stop();
                //splashForm.timer1.Dispose();
                //splashForm.timer1 = null;
                if (splashForm != null)
                {
                    while (!splashForm.IsHandleCreated) { }
                    splashForm.Invoke(new CloseDelegate(frmLoading.CloseFormInternal));
                }
            }
            catch (Exception ex)
            {
                AppLog.writeLog("catch error from closeLoading.");
                AppLog.writeLog(ex);
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    //if (ProgramConfig.loadingStatus == LoadingStatus.Show)
        //    //{
        //    //    this.Hide();
        //    //    this.Show();
        //    //    ProgramConfig.loadingStatus = LoadingStatus.None;
        //    //}
        //    //else if (ProgramConfig.loadingStatus == LoadingStatus.Close)
        //    //{
        //    //    this.Hide();
        //    //    ProgramConfig.loadingStatus = LoadingStatus.None;
        //    //}

        //    //timer1.Stop();

        //        //Thread.Sleep(1000);
        //        if (cnt == 0)
	       //     {
        //            label1.Text = "Loading";
        //        }
        //        else if (cnt == 1)
        //        {
        //            label1.Text = "Loading .";
        //        }
        //        else if (cnt == 2)
        //        {
        //            label1.Text = "Loading . .";
        //        }
        //        else if (cnt == 3)
        //        {
        //            label1.Text = "Loading . . .";
        //        }

        //        if (cnt == 3)
        //        {
        //            cnt = 0;
        //        }

        //        this.Refresh();
        //        this.Update();
        //   // }
            
        //}

        //private void TimerLoading_Tick(object sender, EventArgs e)
        //{
        //    if (ProgramConfig.loadingStatus == LoadingStatus.Show)
        //    {
        //        this.Hide();
        //        this.Show();
        //        ProgramConfig.loadingStatus = LoadingStatus.None;
        //    }
        //    else if (ProgramConfig.loadingStatus == LoadingStatus.Close)
        //    {
        //        this.Hide();
        //        ProgramConfig.loadingStatus = LoadingStatus.None;
        //    }
        //}

        private static void CloseFormInternal()
        {
            try
            {
                if (splashForm != null)
                {
                    while (!splashForm.IsHandleCreated) { }
                    splashForm.Dispose();
                    splashForm = null;
                }
            }
            catch (Exception ex)
            {
                AppLog.writeLog("catch error from CloseFormInternal.");
                AppLog.writeLog(ex);
            }
        }
    
    }
}
