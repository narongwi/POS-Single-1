using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BJCBCPOS_Model;

namespace BJCBCPOS
{
    public partial class frmVDO : Form
    {
        //WMPLib.WindowsMediaPlayer Player;

        public frmVDO()
        {
            InitializeComponent();
        }

        //private void PlayFile(String url)
        //{
        //    try
        //    {
        //        Player = new WMPLib.WindowsMediaPlayer();
        //        Player.PlayStateChange +=
        //            new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
        //        Player.MediaError +=
        //            new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
        //        Player.URL = url;
        //        Player.controls.play();
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog("error at frmVDO.PlayFile");
        //        AppLog.writeLog(ex);
        //    }
        //}

        //private void Player_PlayStateChange(int NewState)
        //{
        //    try
        //    {
        //        if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
        //        {
        //            this.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog("error at frmVDO.Player_PlayStateChange");
        //        AppLog.writeLog(ex);
        //    }
        //}

        //private void Player_MediaError(object pMediaObject)
        //{
        //    try
        //    {
        //        MessageBox.Show("Cannot play media file.");
        //        this.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog("error at frmVDO.Player_MediaError");
        //        AppLog.writeLog(ex);
        //    }
        //}

        private void frmVDO_Load(object sender, EventArgs e)
        {
            try
            {
                if (Screen.AllScreens.Length == 2)
                {
                    Point screen_location = Screen.AllScreens[1].WorkingArea.Location;
                    this.Location = new Point(screen_location.X, screen_location.Y);
                }
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                axWindowsMediaPlayer1.URL = appPath + @"\\video\\pos_video.mp4";
                axWindowsMediaPlayer1.settings.autoStart = true;
                axWindowsMediaPlayer1.settings.setMode("loop", true);
                axWindowsMediaPlayer1.uiMode = "None";
            }
            catch (Exception ex)
            {
                AppLog.writeLog("error at frmVDO.frmVDO_Load");
                AppLog.writeLog(ex);
            }
        }
    }
}
