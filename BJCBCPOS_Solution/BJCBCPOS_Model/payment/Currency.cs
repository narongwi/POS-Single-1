using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BJCBCPOS_Model
{
    public struct Currency
    {
        public string code { get; set; }
        public string pmCode { get; set; }
        public bool isDefault { get; set; }

        public Currency(string code, string pmCode,bool isDefault)
            : this()
        {
            this.code = code;
            this.isDefault = isDefault;
            this.pmCode = pmCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Currency && this == (Currency)obj;
        }

        public bool Equals(Currency obj)
        {
            return this.code.ToUpper().Equals(obj.code.ToUpper());
        }

        public override int GetHashCode()
        {
            return code.GetHashCode();
        }

        public static bool operator ==(Currency lhs, Currency rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Currency lhs, Currency rhs)
        {
            return !(lhs.Equals(rhs));
        }
    }

    public class CurrencyCollections : ICollection<Currency>
    {
        private const string paymentTypeName_Value = "CASH";
        private const string paymentCode_Value = "FXCU";
        private const string paymentTypeName_Name = "PaymentTypeName";
        private const string paymentSeq_Name = "PaymentSeq";
        private const string paymentCode_Name = "PaymentCode";
        private const string currencyCode_Name = "CurrencyCode";

        private Currency[] member = null;
        private int size = 0;

        public CurrencyCollections()
        {
            member = new Currency[0];
            size = 0;
        }

        public CurrencyCollections(int size)
        {
            member = new Currency[size];
            this.size = size;
        }

        public CurrencyCollections(DataTable data)
        {
            Currency item;
            int index, paymentSeq;
            string paymentType, paymentCode;
            if (data != null)
            {
                if (data.Columns.Contains(paymentTypeName_Name) &&
                    data.Columns.Contains(paymentSeq_Name) &&
                    data.Columns.Contains(paymentCode_Name) &&
                    data.Columns.Contains(currencyCode_Name))
                {
                    DataView view = data.DefaultView;
                    view.Sort = paymentCode_Name + ", " + paymentSeq_Name;
                    data = view.ToTable();

                    Currency[] temp = new Currency[data.Rows.Count];
                    index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        paymentType = row[paymentTypeName_Name].ToString().Trim();
                        paymentCode = row[paymentCode_Name].ToString().Trim();
                        if (!int.TryParse(row[paymentSeq_Name].ToString(), out paymentSeq)) paymentSeq = 0;

                        if (paymentType.ToUpper().Equals(paymentTypeName_Value) && paymentCode.ToUpper().Equals(paymentTypeName_Value))
                        {
                            item = new Currency(row[currencyCode_Name].ToString().Trim(), paymentCode, true);
                            if (!this.Contains(item))
                            {
                                temp[index] = item;
                                index++;
                            }
                        }
                        else if (paymentType.ToUpper().Equals(paymentTypeName_Value) && paymentCode.ToUpper().Equals(paymentCode_Value))
                        {
                            item = new Currency(row[currencyCode_Name].ToString().Trim(), paymentCode, false);
                            if (!this.Contains(item))
                            {
                                temp[index] = item;
                                index++;
                            }
                        }
                    }

                    size = index;
                    member = new Currency[size];
                    for (int i = 0; i < size; i++)
                    {
                        member[i] = temp[i];
                    }
                }
                else
                {
                    member = new Currency[0];
                    size = 0;
                }
            }
            else
            {
                member = new Currency[0];
                size = 0;
            }
        }

        #region ICollection<Currency> Members

        public void Add(Currency item)
        {
            Currency[] temp = new Currency[size + 1];
            this.CopyTo(temp, 0);
            temp[size] = item;
            member = temp;
            size = size + 1;
        }

        public void Clear()
        {
            member = new Currency[0];
            size = 0;
        }

        public bool Contains(Currency item)
        {
            if (member != null)
            {
                foreach (Currency data in member)
                {
                    if (item.Equals(data))
                        return true;
                }
            }
            return false;
        }

        public void CopyTo(Currency[] array, int arrayIndex)
        {
            if (member != null)
            {
                foreach (Currency item in member)
                {
                    array.SetValue(item, arrayIndex);
                    arrayIndex = arrayIndex + 1;
                }
            }
        }

        public int Count
        {
            get { return this.size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Currency item)
        {
            int index = -1;
            for (int i = 0; i < size; i++)
            {
                if (member[i] == item)
                {
                    index = -1;
                }
            }
            if (index == -1)
            {
                return false;
            }

            Currency[] temp = new Currency[size - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = member[i];
            }
            for (int j = index + 1; j < size; j++)
            {
                temp[j - 1] = member[j];
            }
            member = temp;
            size = size - 1;
            return true;
        }

        #endregion

        #region IEnumerable<Currency> Members

        public IEnumerator<Currency> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return member.GetEnumerator();
        }

        #endregion
        
        public Currency getCurrency(string code)
        {
            foreach (Currency item in member)
            {
                if (item.code == code)
                {
                    return item;
                }
            }
            return new Currency();
        }

        public List<Currency> list()
        {
            return member.ToList();
        }

        public List<string> codeList()
        {
            List<string> data = null;
            if (size > 0)
            {
                data = new List<string>();
                foreach (Currency item in member)
                {
                    data.Add(item.code);
                }
            }
            return data;
        }
    }
}
