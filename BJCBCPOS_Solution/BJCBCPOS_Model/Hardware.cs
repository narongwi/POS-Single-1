using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOPOS;
using System.Collections;
using System.Data;

namespace BJCBCPOS_Model
{
    public static class Hardware
    {
        public delegate void DrawerListener(string status);

        private static clsOPOS opos = null;
        private static clsOPOS.DrawerStatusEventHandler drawerCurrentEvent = null;
        private const string SeqCol = "Seq";
        private const string MsgTextCol = "Msg_text";
        private const string MsgAmtCol = "Msg_amt";
        private const string MsgPosCol = "Msg_text_Position";

        static Hardware()
        {
            try
            {  
                if (opos == null)
                {
                    opos = new clsOPOS("BIGC15dfnil23$498d#fdsfk25dcxi");
                }
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
            }
        }

        public static bool checkPrinter()
        {
            try
            {
                string res = opos.OpenPrinter();
                if (res.StartsWith("error"))
                {
                    throw new Exception(res);
                }
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public static bool checkCashDrawer()
        {
            try
            {
                string res = opos.OpenCD();
                if (res.StartsWith("error"))
                {
                    throw new Exception(res);
                }
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public static bool printTermal(ArrayList data)
        {
            try
            {
                string res = opos.PrintAsText(data);
                if (res.StartsWith("error"))
                {
                    throw new Exception(res);
                }
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public static bool printTermal(DataTable data)
        {
            try
            {
                if (data != null && data.Rows.Count > 0)
                {
                    if (data.Columns.Contains(MsgTextCol) && data.Columns.Contains(MsgAmtCol))
                    {
                        DataView view = data.DefaultView;
                        // sort by seq
                        if (data.Columns.Contains(SeqCol))
                        {
                            view.Sort = SeqCol + " ASC";
                        }

                        string MsgAmt;
                        ArrayList print = new ArrayList();
                        if (data.Columns.Contains(MsgPosCol))
                        {
                            string posText = "";
                            foreach (DataRowView row in view)
                            {
                                MsgAmt = row[MsgAmtCol].ToString();
                                if (!string.IsNullOrEmpty(MsgAmt) && !string.IsNullOrWhiteSpace(MsgAmt))
                                {
                                    print.Add(row[MsgTextCol] + "|" + MsgAmt);
                                }
                                else
                                {
                                    posText = row[MsgPosCol].ToString();
                                    if (posText.Trim().ToUpper() == "C")
                                    {
                                        posText = "<C>";
                                    }
                                    else if (posText.Trim().ToUpper() == "R")
                                    {
                                        posText = "<R>";
                                    }
                                    else
                                    {
                                        posText = "";
                                    }
                                    print.Add(posText + row[MsgTextCol]);
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRowView row in view)
                            {
                                MsgAmt = row[MsgAmtCol].ToString();
                                if (!string.IsNullOrEmpty(MsgAmt) && !string.IsNullOrWhiteSpace(MsgAmt))
                                {
                                    print.Add(row[MsgTextCol] + "|" + MsgAmt);
                                }
                                else
                                {
                                    print.Add(row[MsgTextCol]);
                                }
                            }
                        }

                        string res = opos.PrintAsText(print);
                        if (res.StartsWith("error"))
                        {
                            throw new Exception(res);
                        }
                        return true;
                    }
                    else
                    {
                        throw new Exception("DataTable does not contains required column name.");
                    }
                }
                else
                {
                    AppLog.writeLog(new Exception("No Data To Print."));
                    return true;
                }
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public static bool openDrawer()
        {
            try
            {
                return opos.KickDrawer();
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public static void addDrawerListeners(DrawerListener listener)
        {
            try
            {
                // clear current event listener
                if (drawerCurrentEvent != null)
                {
                    opos.DrawerStatus -= drawerCurrentEvent;
                }

                // add new event
                drawerCurrentEvent = new clsOPOS.DrawerStatusEventHandler(listener);
                opos.DrawerStatus += drawerCurrentEvent;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
            }
        }

        public static void clearDrawerListeners()
        {
            try
            {
                // clear current event listener
                if (drawerCurrentEvent != null)
                {
                    opos.DrawerStatus -= drawerCurrentEvent;
                }
                drawerCurrentEvent = null;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
            }
        }
    }
}
