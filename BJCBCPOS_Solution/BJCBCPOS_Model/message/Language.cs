using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace BJCBCPOS_Model
{
    public class Language
    {
        //public static Language ENGLISH { get { return new Language(1); } }
        //public static Language THAI { get { return new Language(2); } }
        //public static Language LAOS { get { return new Language(3); } }

        public int ID { get; set; }

        public Language(int ID)
        {
            this.ID = ID;
        }

        public Language(string code)
        {
            for (int i = 1; i <= ProgramConfig.dtActiveLanguage.AsEnumerable().Max(m => Convert.ToInt32(m["LANGUAGE_ID"])); i++)
            {
                if (new Language(i).Code == code.Trim().ToUpper())
                {
                    this.ID = i;
                    break;
                }
            }
        }

        public string Name
        {
            get
            {
                return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["LANGUAGE_DESC"].ToString()).FirstOrDefault();

                //switch (ID)
                //{
                //    case 1:
                //        return "English";
                //    case 2:
                //        return "Thai";
                //    case 3:
                //        return "Laos";
                //    default:
                //        return "";
                //}
            }
        }

        public string Code
        {
            get
            {
                return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["COUNTRY_CODE"].ToString()).FirstOrDefault();
 
                //switch (ID)
                //{
                //    case 1:
                //        return "ENG";
                //    case 2:
                //        return "THA";
                //    case 3:
                //        return "LAO";
                //    default:
                //        return "";
                //}
            }
        }

        public string LanguageFile
        {
            get
            {
                if (ProgramConfig.dtActiveLanguage.Rows.Count > 0)
                {
                    string langFile = ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["LANGUAGE_FILE"].ToString()).FirstOrDefault();
                    if (langFile == null)
                    {
                        return ProgramConfig.dtActiveLanguage.Rows[0]["LANGUAGE_FILE"].ToString();
                    }
                    else
                    {
                        return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["LANGUAGE_FILE"].ToString()).FirstOrDefault();
                    }
                }
                else
                {
                    return "English.txt";
                }

                //switch (ID)
                //{
                //    case 1:
                //        return "English.txt";
                //    case 2:
                //        return "Thai.txt";
                //    case 3:
                //        return "Laos.txt";
                //    default: 
                //        return "";
                //}
            }
        }

        public string FontName
        {
            get
            {
                return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["LANGUAGE_FONT"].ToString()).FirstOrDefault();
                //switch (ID)
                //{
                //    case 1:
                //        return "Prompt";
                //    case 2:
                //        return "Prompt";
                //    case 3:
                //        return "Phetsarath OT";
                //    default:
                //        return "";
                //}
            }
        }

        public string culture
        {
            get
            {
                if (ProgramConfig.dtActiveLanguage.Rows.Count > 0)
                {
                    return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["LANGUAGE_CULTURE"].ToString()).FirstOrDefault();
                }
                else
                {
                    return "en-US";
                }
                //switch (ID)
                //{
                //    case 1:
                //        return "en-US";
                //    case 2:
                //        return "th-TH";
                //    case 3:
                //        return "lo-LA";
                //    default:
                //        return "en-US";
                //}
            }
        }

        public string ImageName {
            get
            {
                return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["ImageName"].ToString()).FirstOrDefault();
            }
        }

        //public string AppDefaultMessage
        //{
        //    get
        //    {
        //        return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["LANGUAGE_ID"] == ID).Select(sdr => sdr["AppDefaultMessage"].ToString()).FirstOrDefault();
        //    }
        //}

        //public string AppDefaultHelpMessage
        //{
        //    get
        //    {
        //        return ProgramConfig.dtActiveLanguage.AsEnumerable().Where(wdr => (int)wdr["ID"] == ID).Select(sdr => sdr["AppDefaultHelpMessage"].ToString()).FirstOrDefault();
        //    }
        //}

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Language && this == (Language)obj;
        }

        public static bool operator ==(Language lhs, Language rhs)
        {
            return lhs.ID == rhs.ID;
        }

        public static bool operator !=(Language lhs, Language rhs)
        {
            return lhs.ID != rhs.ID;
        }
    }
}
