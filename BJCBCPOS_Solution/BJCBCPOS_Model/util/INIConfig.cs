using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BJCBCPOS_Model
{

    /// <summary>
    /// store pos config read from .ini file
    /// </summary>
    public class INIConfig
    {
        private string fileName;
        private Dictionary<ConfigKey, string> config;

        public INIConfig(string fileName)
        {
            this.fileName = fileName;
            readFromFile();
        }

        private bool readFromFile()
        {
            string section = "", key, value, read_str;
            string[] key_split;
            try
            {
                config = new Dictionary<ConfigKey, string>(new ConfigKeyComparer());
                StreamReader read = new StreamReader(this.fileName);
                while (!read.EndOfStream)
                {
                    read_str = read.ReadLine();
                    if (read_str.StartsWith("[") && read_str.EndsWith("]"))
                    {
                        section = read_str.Replace("[", "").Replace("]", "").ToLower();
                    }
                    else if (read_str.Contains("="))
                    {
                        key_split = read_str.Split('=');
                        if (key_split.Length >= 2)
                        {
                            key = key_split[0].Trim().ToLower();
                            value = "";
                            for (int i = 1; i < key_split.Length; i++)
                            {
                                value += key_split[i].Trim();
                            }
                            config.Add(new ConfigKey(section, key), value);
                        }
                    }
                }
                read.Close();
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public string getValue(string section, string key)
        {
            return this.getValue(new ConfigKey(section.ToLower(), key.ToLower()));
        }

        public string getValue(ConfigKey key)
        {
            try
            {
                return this.config[key];
            }
            catch (Exception ex)
            {
                AppLog.writeLog("key section = " + key.section + ", name = " + key.key);
                AppLog.writeLog(ex);
                return "";
            }
        }

        public void setValue(string section, string key, string value)
        {
            this.setValue(new ConfigKey(section.ToLower(), key.ToLower()), value);
        }

        public void setValue(ConfigKey key, string value)
        {
            try
            {
                this.config[key] = value;
            }
            catch (Exception ex)
            {
                AppLog.writeLog("key section = " + key.section + ", name = " + key.key);
                AppLog.writeLog(ex);
            }
        }

        public bool updateValue()
        {
            string current_section = "", current_key, current_value, read_str;
            string[] key_split;
            string value;
            List<string> write = new List<string>();
            try
            {
                StreamReader read = new StreamReader(this.fileName);
                while (!read.EndOfStream)
                {
                    read_str = read.ReadLine();
                    if (read_str.StartsWith("[") && read_str.EndsWith("]"))
                    {
                        current_section = read_str.Replace("[", "").Replace("]", "").ToLower();
                        write.Add(read_str);
                    }
                    else if (read_str.Contains("="))
                    {
                        key_split = read_str.Split('=');
                        if (key_split.Length >= 2)
                        {
                            current_key = key_split[0].Trim().ToLower();
                            current_value = "";
                            for (int i = 1; i < key_split.Length; i++)
                            {
                                current_value += key_split[i].Trim();
                            }

                            value = getValue(current_section, current_key);
                            if (!current_value.Equals(value))
                            {
                                write.Add(read_str.Replace(current_value, value));
                            }
                            else
                            {
                                write.Add(read_str);
                            }
                        }
                        else
                        {
                            write.Add(read_str);
                        }
                    }
                    else
                    {
                        write.Add(read_str);
                    }
                }
                read.Close();

                File.WriteAllLines(this.fileName, write.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool updateValue(string section, string key, string value)
        {
            string current_section = "", current_key, current_value, read_str;
            string[] key_split;
            List<string> write = new List<string>();
            try
            {
                StreamReader read = new StreamReader(this.fileName);
                while (!read.EndOfStream)
                {
                    read_str = read.ReadLine();
                    if (read_str.StartsWith("[") && read_str.EndsWith("]"))
                    {
                        current_section = read_str.Replace("[", "").Replace("]", "").ToLower();
                        write.Add(read_str);
                    }
                    else if (read_str.Contains("="))
                    {
                        key_split = read_str.Split('=');
                        if (key_split.Length >= 2)
                        {
                            current_key = key_split[0].Trim().ToLower();
                            current_value = "";
                            for (int i = 1; i < key_split.Length; i++)
                            {
                                current_value += key_split[i].Trim();
                            }

                            if (section == current_section && key == current_key)
                            {
                                write.Add(read_str.Replace(current_value, value));
                            }
                            else
                            {
                                write.Add(read_str);
                            }
                        }
                        else
                        {
                            write.Add(read_str);
                        }
                    }
                    else
                    {
                        write.Add(read_str);
                    }
                }
                read.Close();

                File.WriteAllLines(this.fileName, write.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public bool updateValue(ConfigKey key, string value)
        {
            return updateValue(key.section, key.key, value);
        }

        public static bool createConfigIni(string stroeCode, string tillNo, string ipServer, string dbServer, string ipServerBk, string dbServerBk, string ipServerTrain, string dbServerTrain)
        {
            try
            {
                string fileName = FixedData.config_name;
                string config =
@"[connection]
server = {0}
database = {1}
serverbackup = {2}
databasebackup = {3}
servertraining = {4}
databasetraining = {5}
connecttimeout = 30
commandtimeout = 60
standalone = N

[setting]
store = {6}
till = {7}
language = 1
printpreview = 0
printwatermark = 0
shutdown = 0
userlog = 0

[device]
cashdrawer=10
handheldscanner=1
tablescanner=1
customerdisplay=
printerthermal=
printera4=
edc=
magneticstripe=

[macro_key]
";
                File.AppendAllText(fileName, string.Format(config, ipServer, dbServer, ipServerBk, dbServerBk, ipServerTrain, dbServerTrain, stroeCode, tillNo), Encoding.Unicode);
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public static bool createRunningIni(string storeCode, string tillNo)
        {
            try
            {
                string fileName = FixedData.running_name;
                string config =
@"abbno={0}
cnno={1}
fftino={2}
";
                string abbNo = tillNo + "00000" + 1;
                string cnno = "CN" + tillNo + "000" + 1;
                string fftino = storeCode + tillNo + "00000" + 1;

                File.AppendAllText(fileName, string.Format(config, abbNo, cnno, fftino), Encoding.Unicode);
                return true;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }
    }

    /// <summary>
    /// class contain key from .ini file.
    /// </summary>
    public struct ConfigKey
    {
        public string section;
        public string key;

        public ConfigKey(string section, string key)
        {
            this.section = section;
            this.key = key;
        }

    }

    /// <summary>
    /// implement IEqualityComparer of ConfigKey class for equals compare.
    /// </summary>
    public class ConfigKeyComparer : IEqualityComparer<ConfigKey>
    {
        public bool Equals(ConfigKey obj1, ConfigKey obj2)
        {
            return (obj1.section.Equals(obj2.section, StringComparison.OrdinalIgnoreCase)) && (obj1.key.Equals(obj2.key, StringComparison.OrdinalIgnoreCase));
        }


        public int GetHashCode(ConfigKey x)
        {
            return x.section.GetHashCode() + x.key.GetHashCode();
        }
    }

}
