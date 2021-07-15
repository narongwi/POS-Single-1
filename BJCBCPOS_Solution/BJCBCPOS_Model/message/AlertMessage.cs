using System;
using System.Collections.Generic;
using System.Data;

namespace BJCBCPOS_Model
{
    public struct AlertMessage
    {
        public string screen { get; set; }
        public string messageKey { get; set; }
        public Language language { get; set; }
        public string message { get; set; }
        public string help { get; set; }

        public AlertMessage(string screen, string messageKey, Language language, string message, string help)
            : this()
        {
            this.screen = screen;
            this.messageKey = messageKey;
            this.language = language;
            this.message = message;
            this.help = help;
        }

        public override int GetHashCode()
        {
            return screen.GetHashCode() + messageKey.GetHashCode() + language.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            return obj is AlertMessage && this == (AlertMessage)obj;
        }

        public static bool operator ==(AlertMessage x, AlertMessage y)
        {
            return x.screen == y.screen && x.messageKey == y.messageKey && x.language == y.language;
        }

        public static bool operator !=(AlertMessage x, AlertMessage y)
        {
            return x.screen != y.screen || x.messageKey != y.messageKey || x.language != y.language;
        }
    }

    public class AlertMessageCollection : ICollection<AlertMessage>
    {
        private AlertMessage[] member = null;
        private int size = 0;

        private const string screen_Name = "ScreenName";
        private const string messageKey_Name = "MessageKey";
        private const string language_Name = "LanguageId";
        private const string message_Name = "MessageError";
        private const string help_Name = "MessageHelp";

        public AlertMessageCollection()
        {
            member = new AlertMessage[0];
            size = 0;
        }

        public AlertMessageCollection(DataTable data)
        {
            AlertMessage item;
            int index, language_id;
            if (data != null)
            {
                if (data.Columns.Contains(screen_Name) &&
                    data.Columns.Contains(messageKey_Name) &&
                    data.Columns.Contains(language_Name) &&
                    data.Columns.Contains(message_Name) &&
                    data.Columns.Contains(help_Name))
                {
                    size = data.Rows.Count;
                    member = new AlertMessage[size];
                    index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        if (!int.TryParse(row[language_Name].ToString(), out language_id)) language_id = 0;
                        item = new AlertMessage(row[screen_Name].ToString().Trim(), row[messageKey_Name].ToString().Trim(), new Language(language_id),
                            row[message_Name].ToString().Trim(), row[help_Name].ToString().Trim());
                        member[index] = item;
                        index++;
                    }
                }
                else
                {
                    member = new AlertMessage[0];
                    size = 0;
                }
            }
            else
            {
                member = new AlertMessage[0];
                size = 0;
            }
        }

        #region ICollection<AlertMessage> Members

        public void Add(AlertMessage item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(AlertMessage item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(AlertMessage[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return this.size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AlertMessage item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<AlertMessage> Members

        public IEnumerator<AlertMessage> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        public AlertMessage get(string screen, string messageKey)
        {
            string OriScreen = screen;
            string OriMessageKey = messageKey;
            AlertMessage item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.screen.Equals(screen.Trim(), StringComparison.OrdinalIgnoreCase)
                    && item.messageKey.Equals(messageKey.Trim(), StringComparison.OrdinalIgnoreCase)
                    && item.language == ProgramConfig.language)
                {
                    item.message = item.message.Replace("@n", Environment.NewLine);
                    item.help = item.help.Replace("@n", Environment.NewLine);
                    return item;
                }
            }

            // not found show default message in list
            screen = FixedData.defaultMessageNotFound;
            messageKey = FixedData.defaultMessageNotFound;

            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.screen.Equals(screen.Trim(), StringComparison.OrdinalIgnoreCase)
                    && item.messageKey.Equals(messageKey.Trim(), StringComparison.OrdinalIgnoreCase)
                    && item.language == ProgramConfig.language)
                {
                    item.message = item.message.Replace("@n", Environment.NewLine);
                    item.help = item.help.Replace("@n", Environment.NewLine);

                    item.help = String.Format(item.help, OriScreen, OriMessageKey);

                    return item;
                }
            }
            
            // find default message from file 1st

            //string defMessage = AppMessage.getMessage(ProgramConfig.language, "ApplicationError", "DefaultMessage") == "" ? "Application Error."
            //    : AppMessage.getMessage(ProgramConfig.language, "ApplicationError", "DefaultMessage");
            //string defHelpMessage = AppMessage.getMessage(ProgramConfig.language, "ApplicationError", "DefaultHelpMessage") == "" ? "Please Contract IT Service Desk"
            //    : AppMessage.getMessage(ProgramConfig.language, "ApplicationError", "DefaultHelpMessage");

            string defMessage = "Application Error.";
            string defHelpMessage = "Please Contract IT Service Desk";

            // not found default message in list. Show default message from fixed data
            return new AlertMessage(FixedData.defaultMessageKey, FixedData.defaultMessageKey, ProgramConfig.language, defMessage, defHelpMessage);
        }

        public AlertMessage defaultMessage
        {
            get
            {
                return this.get(FixedData.defaultMessageKey, FixedData.defaultMessageKey);
            }
        }
    }
}
