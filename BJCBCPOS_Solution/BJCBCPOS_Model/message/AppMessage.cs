using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Data;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// utility class support for application language change.
    /// </summary>
    public static class AppMessage
    {
        private static bool[] contains = new bool[(ProgramConfig.listActiveLanguage.Count > 0 ? ProgramConfig.listActiveLanguage.Max(lst => lst.ID) : 0) + 1];
        //private static Dictionary<AppMessageKey, string> values = null;
        private static DataTable values = null;
      
        /// <summary>
        /// read specific language from config file
        /// </summary>
        /// <param name="lang">language to load</param>
        /// <returns>read file success or not</returns>
        public static bool readFromFile(Language lang)
        {
            if(contains.Length != ((ProgramConfig.listActiveLanguage.Count > 0 ? ProgramConfig.listActiveLanguage.Max(lst => lst.ID) : 0) + 1)) //ใช้สำหรับจังหวะที่เปิดโปรแกรมมาครั้งแรกแล้วไม่ได้ต่อ network แต่ connect สำเร็จก่อนเข้า mode stand alone
            {
                contains = new bool[(ProgramConfig.listActiveLanguage.Count > 0 ? ProgramConfig.listActiveLanguage.Max(lst => lst.ID) : 0) + 1];
            }

            string formName, controlName, value, read_str;
            string[] key_split;
            DataRow row;
            try
            {
                if (values == null)
                {
                    //values = new Dictionary<AppMessageKey, string>(new AppMessageKeyComparer());
                    values = new DataTable();

                    values.Columns.Add("LanguageId", typeof(int));
                    values.Columns.Add("FormName", typeof(string));
                    values.Columns.Add("ControlName", typeof(string));
                    values.Columns.Add("Message", typeof(string));
                }
                StreamReader read = new StreamReader(lang.LanguageFile, Encoding.Unicode);
                while (!read.EndOfStream)
                {
                    //read_str = read.ReadLine();
                    //if (read_str.Contains('\u0009'))
                    //{
                    //    key_split = read_str.Split('\u0009');
                    //    if (key_split.Length >= 3)
                    //    {
                    //        formName = key_split[0].Trim().Normalize();
                    //        controlName = key_split[1].Trim().Normalize();
                    //        value = key_split[2].Trim();

                    //        if(!values.ContainsKey(new AppMessageKey(lang, formName, controlName)))
                    //            values.Add(new AppMessageKey(lang, formName, controlName), value);
                    //    }
                    //}
                    read_str = read.ReadLine();
                    if (read_str.Contains('\u0009'))
                    {
                        key_split = read_str.Split('\u0009');
                        if (key_split.Length >= 3)
                        {
                            formName = key_split[0].Trim().Normalize();
                            controlName = key_split[1].Trim().Normalize();
                            value = key_split[2].Trim().Replace("|", Environment.NewLine);

                            row = values.NewRow();
                            row[0] = lang.ID;
                            row[1] = formName;
                            row[2] = controlName;
                            row[3] = value;
                            values.Rows.Add(row);
                        }
                    }
                }
                read.Close();
                contains[lang.ID] = true;
                return true;
            }
            catch (Exception ex)
            {

                AppLog.writeLog(ex);
                contains[lang.ID] = false;
                return false;
            }
        }

        /// <summary>
        /// get application display message for specific location
        /// </summary>
        /// <param name="lang">language to display message</param>
        /// <param name="formName">form name that display message</param>
        /// <param name="controlName">control name to display message</param>
        /// <returns>display message of specific location</returns>
        //public static string getMessage(Language lang, string formName, string controlName)
        //{
        //    try
        //    {
        //        if (!contains[lang.ID])
        //        {
        //            readFromFile(lang);
        //        }
        //        AppMessageKey check_key = new AppMessageKey(lang, formName, controlName);
        //        if (values.ContainsKey(check_key))
        //        {
        //            return values[new AppMessageKey(lang, formName, controlName)];
        //        }
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog(ex);
        //        return "";
        //    }
        //}

        /// <summary>
        /// update application display message for all child control of specific control
        /// </summary>
        /// <param name="lang">language to display message</param>
        /// <param name="formName">name of form that contain specific control</param>
        /// <param name="control">specific control to change display message</param>
        //public static void fillForm(Language lang, string formName, Control control, bool fontOnly = false)
        //{
        //    AppMessageKey check_key;
        //    Font old, update = null;
        //    if (!contains[lang.ID])
        //    {
        //        readFromFile(lang);
        //    }
        //    foreach (Control item in control.Controls)
        //    {
        //        // change font
        //        old = item.Font;
        //        update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
        //        check_key = new AppMessageKey(lang, formName, item.Name);
        //        if (item is TextBox || item is Label || item is Button)
        //        {
        //            item.Font = update;
        //        }
        //        if (!fontOnly)
        //        {
        //            if (values.ContainsKey(check_key))
        //            {
        //                if (item is TextBox || item is Label || item is Button)
        //                {
        //                    item.Text = values[check_key];
        //                    //item.Font = update;
        //                }
        //                else
        //                {
        //                    MethodInfo method = item.GetType().GetMethod("switchLanguage");
        //                    if (method != null)
        //                    {
        //                        method.Invoke(item, new object[] { values[check_key], lang.FontName });
        //                    }
        //                }
        //            }
        //            //else if (item is TextBox || item is Label || item is Button)  //Case for button Main Menu
        //            //{
        //            //    item.Font = update;
        //            //}
        //            else if (item is UserControl)
        //            {
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".currentMenuTitle1");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("currentMenuTitle1", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".currentMenuTitle2");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("currentMenuTitle2", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".currentMenuTitle3");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("currentMenuTitle3", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".logoutText");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("logoutText", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".LabelText");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("LabelText", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".Text");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("Text", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".placeHolder");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("placeHolder", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //                check_key = new AppMessageKey(lang, formName, item.Name + ".memberText");
        //                if (values.ContainsKey(check_key))
        //                {
        //                    PropertyInfo prop = item.GetType().GetProperty("memberText", BindingFlags.Public | BindingFlags.Instance);
        //                    if (null != prop && prop.CanWrite)
        //                    {
        //                        prop.SetValue(item, values[check_key], null);
        //                    }
        //                }
        //            }

        //            if (item.Controls != null && item.Controls.Count > 0)
        //            {
        //                if (item is UserControl)
        //                {
        //                    fillForm(lang, formName, item, true);
        //                }
        //                else
        //                {
        //                    fillForm(lang, formName, item, false);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (item.Controls != null && item.Controls.Count > 0)
        //            {
        //                fillForm(lang, formName, item, true);
        //            }
        //        }

        //    }

        //    //if (ProgramConfig.languageId == POS_LANG.ENGLISH)
        //    //{
        //    //    btnLanguage.BackgroundImage = Properties.Resources.Laos;
        //    //    ProgramConfig.languageId = POS_LANG.LAOS;
        //    //}
        //    //else if (ProgramConfig.languageId == POS_LANG.LAOS)
        //    //{
        //    //    btnLanguage.BackgroundImage = Properties.Resources.Thai;
        //    //    ProgramConfig.languageId = POS_LANG.THAI;
        //    //}
        //    //else if (ProgramConfig.languageId == POS_LANG.THAI)
        //    //{
        //    //    btnLanguage.BackgroundImage = Properties.Resources.usa_enable;
        //    //    ProgramConfig.languageId = POS_LANG.ENGLISH;
        //    //}
        //}

        public static string getMessage(Language lang, string formName, string controlName)
        {
            try
            {
                if (contains.Length > lang.ID)
                {
                    if (!contains[lang.ID])
                    {
                        readFromFile(lang);
                    }

                    DataRow[] changeList = values.Select("LanguageId = " + lang.ID + " and FormName = '" + formName + "' and ControlName = '" + controlName + "'");
                    if (changeList != null && changeList.Length > 0)
                    {
                        return changeList[0]["Message"].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return "";
            }
        }

        public static string getMessage(Language lang, string formName, string controlName, string message)
        {
            try
            {
                string strQry = "FormName='" + formName + "'";

                if (!contains[lang.ID])
                {
                    readFromFile(lang);
                }

                if (!String.IsNullOrEmpty(controlName.Trim()))
                {
                    strQry += " and ControlName = '" + controlName + "'";
                }

                if (!String.IsNullOrEmpty(message.Trim()))
                {
                    strQry += " and Message = '" + message + "'";
                }

                DataRow[] findList = values.Select(strQry);

                if (findList != null && findList.Length > 0)
                {
                    DataRow[] changeList = values.Select("LanguageId = " + lang.ID + " and FormName = '" + findList[0]["FormName"] + "' and ControlName = '" + findList[0]["ControlName"] + "'");
                    if (changeList != null && changeList.Length > 0)
                    {
                        return changeList[0]["Message"].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return "";
            }
        }

        public static void fillForm(Language lang, Control form)
        {
            if (!lang.Equals(null) && contains.Length > (lang == null?0: lang.ID))
            {
                string control_str, control_name, control_prop;
                string[] spl;
                if (!contains[lang.ID])
                {
                    readFromFile(lang);
                }

                DataRow[] changeList = values.Select("LanguageId = " + lang.ID + " and FormName = '" + form.Name + "'");
                foreach (DataRow row in changeList)
                {
                    control_str = row["ControlName"].ToString();
                    control_name = control_str;
                    control_prop = "";
                    if (control_str.Contains("."))
                    {
                        spl = control_str.Split('.');
                        control_name = spl[0];
                        control_prop = spl[1];
                    }

                    Control[] itemList = form.Controls.Find(control_name, true);
                    foreach (Control item in itemList)
                    {
                        if (item is TextBox || item is Label || item is Button)
                        {
                            item.Text = row["Message"].ToString();
                            //item.Font = update;
                        }
                        else if (item is UserControl)
                        {
                            PropertyInfo prop = item.GetType().GetProperty(control_prop, BindingFlags.Public | BindingFlags.Instance);
                            if (null != prop && prop.CanWrite)
                            {
                                prop.SetValue(item, row["Message"].ToString(), null);
                            }
                        }
                        else
                        {
                            MethodInfo method = item.GetType().GetMethod("switchLanguage");
                            if (method != null)
                            {
                                method.Invoke(item, new object[] { row["Message"].ToString(), lang.FontName });
                            }
                        }
                    }
                }

                Font old, update;
                var allControl = GetAllControls(form);

                foreach (Control theControl in allControl.OfType<Button>().ToList())
                {
                    if (theControl.Name != "btnPlus" && theControl.Name != "btnMinus" && theControl.Name != "btnMultiply" && theControl.Name != "btnDivide")
                    {
                        old = theControl.Font;
                        update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                        theControl.Font = update;
                    }
                }
                foreach (Control theControl in allControl.OfType<TextBox>().ToList())
                {
                    old = theControl.Font;
                    update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
                foreach (Control theControl in allControl.OfType<Label>().ToList())
                {
                    old = theControl.Font;
                    update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
                foreach (TabControl theControl in allControl.OfType<TabControl>().ToList())
                {
                    old = theControl.Font;
                    update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
            }
        }

        public static void fillControlsFont(Language lang, Control control, List<string> lstIgnore)
        {
            Font old, update;
            var allControl = GetAllControls(control);

            foreach (Control theControl in allControl.OfType<Button>().ToList())
            {
                old = theControl.Font;
                update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                theControl.Font = update;
            }
            foreach (Control theControl in allControl.OfType<TextBox>().ToList())
            {
                old = theControl.Font;
                update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                theControl.Font = update;
            }
            foreach (Control theControl in allControl.OfType<Label>().ToList())
            {
                if (!lstIgnore.Any(str => str == theControl.Name))
                {
                    old = theControl.Font;
                    update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
                else
                {
                    old = theControl.Font;
                    update = new Font("Prompt", old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
            }
            foreach (TabControl theControl in allControl.OfType<TabControl>().ToList())
            {
                old = theControl.Font;
                update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                theControl.Font = update;
            }
            foreach (RichTextBox theControl in allControl.OfType<RichTextBox>().ToList())
            {
                theControl.Font = new Font(lang.FontName, theControl.Font.Size, theControl.Font.Style, theControl.Font.Unit, theControl.Font.GdiCharSet);
            }
        }

        public static void fillAllControlsFontIgnoreNumber(Language lang, Control control)
        {
            Font old, update;
            var allControl = GetAllControls(control);

            foreach (Control theControl in allControl.OfType<Button>().ToList())
            {
                old = theControl.Font;
                update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                theControl.Font = update;
            }
            foreach (Control theControl in allControl.OfType<TextBox>().ToList())
            {
                double chk = 0;
                if (!Double.TryParse(theControl.Text, out chk))
                {
                    old = theControl.Font;
                    update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
                else
                {
                    theControl.Font = new Font("Prompt", theControl.Font.Size, theControl.Font.Style, theControl.Font.Unit, theControl.Font.GdiCharSet);
                }      
            }
            foreach (Control theControl in allControl.OfType<Label>().ToList())
            {
                double chk = 0;
                if (!Double.TryParse(theControl.Text, out chk))
                {
                    old = theControl.Font;
                    update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                    theControl.Font = update;
                }
                else
                {
                    theControl.Font = new Font("Prompt", theControl.Font.Size, theControl.Font.Style, theControl.Font.Unit, theControl.Font.GdiCharSet);
                }       
            }
            foreach (TabControl theControl in allControl.OfType<TabControl>().ToList())
            {
                old = theControl.Font;
                update = new Font(lang.FontName, old.Size, old.Style, old.Unit, old.GdiCharSet);
                theControl.Font = update;
            }
            foreach (RichTextBox theControl in allControl.OfType<RichTextBox>().ToList())
            {
                theControl.Font = new Font(lang.FontName, theControl.Font.Size, theControl.Font.Style, theControl.Font.Unit, theControl.Font.GdiCharSet);
            }
        }

        public static IEnumerable<Control> GetAllControls(Control aControl)
        {
            Stack<Control> stack = new Stack<Control>();
            stack.Push(aControl);

            while (stack.Any())
            {
                var nextControl = stack.Pop();

                foreach (Control childControl in nextControl.Controls)
                {
                    stack.Push(childControl);
                }

                yield return nextControl;
            }
        }
    }

    /// <summary>
    /// class contain key for get message to display in application
    /// </summary>
    //public struct AppMessageKey
    //{
    //    public Language language;
    //    public string formName;
    //    public string controlName;

    //    public AppMessageKey(Language language, string formName, string controlName)
    //    {
    //        this.language = language;
    //        this.formName = formName;
    //        this.controlName = controlName;
    //    }
    //}

    ///// <summary>
    ///// implement IEqualityComparer of AppMessageKey class for equals compare.
    ///// </summary>
    //public class AppMessageKeyComparer : IEqualityComparer<AppMessageKey>
    //{
    //    public bool Equals(AppMessageKey obj1, AppMessageKey obj2)
    //    {
    //        return (obj1.language == obj2.language) && (obj1.formName == obj2.formName) && (obj1.controlName == obj2.controlName);
    //    }


    //    public int GetHashCode(AppMessageKey x)
    //    {
    //        return x.language.GetHashCode() + x.formName.GetHashCode() + x.controlName.GetHashCode();
    //    }
    //}

}
