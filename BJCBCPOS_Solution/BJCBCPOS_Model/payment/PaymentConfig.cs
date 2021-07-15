using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BJCBCPOS_Model
{
    public class PaymentConfig
    {
        public int saleType { get; set; }
        public int paymentTypeSeq { get; set; }
        public int paymentTypeId { get; set; }
        public string paymentTypeName { get; set; }
        public string paymentCode { get; set; }
        public string currencyCode { get; set; }
        public string paymentName { get; set; }
        public float exchangeRate { get; set; }
        public double maxCashOut { get; set; }
        public int? length { get; set; }
        public bool changeStatus { get; set; }
        public bool excessChangeStatus { get; set; }

        public PaymentConfig(int saleType, int paymentTypeSeq, int paymentTypeId, string paymentTypeName, string paymentCode, string currencyCode, string paymentName, float exchangeRate, int? length, bool changeStatus, float maxCashOut, bool excessChangeStatus)
        {
            this.saleType = saleType;
            this.paymentTypeSeq = paymentTypeSeq;
            this.paymentTypeId = paymentTypeId;
            this.paymentTypeName = paymentTypeName;
            this.paymentCode = paymentCode;
            this.currencyCode = currencyCode;
            this.paymentName = paymentName;
            this.exchangeRate = exchangeRate;
            this.length = length;
            this.changeStatus = changeStatus;
            this.maxCashOut = maxCashOut;
            this.excessChangeStatus = excessChangeStatus;
        }
    }

    public class PaymentConfigCollections: ICollection<PaymentConfig>
    {
        private PaymentConfig[] member = null;
        private int size = 0;

        private const string saleType_Name = "SaleType";
        private const string paymentTypeSeq_Name = "PaymentTypeSeq";
        private const string paymentTypeId_Name = "PaymentType";
        private const string paymentTypeName_Name = "PaymentTypeName";
        private const string paymentCode_Name = "PaymentCode";
        private const string currencyCode_Name = "CurrencyCode";
        private const string paymentName_Name = "PaymentName";
        private const string exchangeRate_Name = "ExchageRate";
        private const string length_Name = "NumberLength";
        private const string changeStatus_Name = "ChageStatus";
        private const string exchangeRate_paymentCode = "FXCU";
        private const string maxCashOut_paymentCode = "CASH";
        private const string MaxCashOut_Name = "MaxCashOut";
        private const string excessChangeStatus_Name = "ExcessChange";
        
        public PaymentConfigCollections()
        {
            member = new PaymentConfig[0];
            size = 0;
        }
        
        public PaymentConfigCollections(int size)
        {
            member = new PaymentConfig[size];
            this.size = size;
        }
        
        public PaymentConfigCollections(DataTable data)
        {
            PaymentConfig item;
            int index, saleType, paymentTypeSeq, paymentTypeId, length;
            float exchangeRate;
            float maxCashOut;
            bool changeStatus;
            bool excessChangeStatus;
            if (data != null)
            {
                if (data.Columns.Contains(saleType_Name) && 
                    data.Columns.Contains(paymentTypeSeq_Name) &&
                    data.Columns.Contains(paymentTypeId_Name) && 
                    data.Columns.Contains(paymentTypeName_Name) && 
                    data.Columns.Contains(paymentCode_Name) &&
                    data.Columns.Contains(currencyCode_Name) &&
                    data.Columns.Contains(paymentName_Name) &&
                    data.Columns.Contains(exchangeRate_Name) &&
                    data.Columns.Contains(length_Name) &&
                    data.Columns.Contains(changeStatus_Name) &&
                    data.Columns.Contains(excessChangeStatus_Name))
                    
                {
                    size = data.Rows.Count;
                    member = new PaymentConfig[size];
                    index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        if (!int.TryParse(row[saleType_Name].ToString(), out saleType)) saleType = 0;
                        if (!int.TryParse(row[paymentTypeSeq_Name].ToString(), out paymentTypeSeq)) paymentTypeSeq = 0;
                        if (!int.TryParse(row[paymentTypeId_Name].ToString(), out paymentTypeId)) paymentTypeId = 0;
                        if (!float.TryParse(row[exchangeRate_Name].ToString(), out exchangeRate)) exchangeRate = 0;
                        if (!int.TryParse(row[length_Name].ToString(), out length)) length = 0;
                        changeStatus = row[changeStatus_Name].ToString().Trim().ToUpper().Equals("Y");
                        excessChangeStatus = row[excessChangeStatus_Name].ToString().Trim().ToUpper().Equals("Y");
                        if (!float.TryParse(row[MaxCashOut_Name].ToString(), out maxCashOut)) exchangeRate = 0;

                        item = new PaymentConfig(saleType, paymentTypeSeq, paymentTypeId,
                            row[paymentTypeName_Name].ToString(), row[paymentCode_Name].ToString(), row[currencyCode_Name].ToString(), row[paymentName_Name].ToString(), exchangeRate, length == 0 ? null : (int?)length, changeStatus, maxCashOut, excessChangeStatus);
                        member[index] = item;
                        index++;
                    }
                }
                else
                {
                    member = new PaymentConfig[0];
                    size = 0;
                }
            }
            else
            {
                member = new PaymentConfig[0];
                size = 0;
            }
        }

        #region ICollection<PaymentConfig> Members
        
        public void Add(PaymentConfig item)
        {
            PaymentConfig[] temp = new PaymentConfig[size + 1];
            this.CopyTo(temp, 0);
            temp[size] = item;
            member = temp;
            size = size + 1;
        }

        public void Clear()
        {
            member = new PaymentConfig[0];
            size = 0;
        }

        public bool Contains(PaymentConfig item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(PaymentConfig[] array, int arrayIndex)
        {
            foreach (PaymentConfig item in member)
            {
                array.SetValue(item, arrayIndex);
                arrayIndex = arrayIndex + 1;
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

        public bool Remove(PaymentConfig item)
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

            PaymentConfig[] temp = new PaymentConfig[size - 1];
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

        #region IEnumerable<PaymentConfig> Members

        public IEnumerator<PaymentConfig> GetEnumerator()
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

        public float getExchangeRate(string currencyCode)
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals(exchangeRate_paymentCode) && item.currencyCode.Trim().Equals(currencyCode))
                {
                    return item.exchangeRate;
                }
            }
            return 0;
        }

        public double getMaxCashout(string paymentCode)
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals(paymentCode))
                {
                    return item.maxCashOut;
                }
            }
            return 0;
        }

        public string getPaymentCode(string currencyCode)
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.currencyCode.Equals(currencyCode))
                {
                    return item.paymentCode;
                }
            }
            return "";
        }

        public string getMainCurrency()
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals("CASH"))
                {
                    return item.currencyCode;
                }
            }
            return "";
        }

        //public string getPCD(string paymentCode)
        //{
        //    if (paymentCode.Trim() != "FXCU")
        //    {
        //        return paymentCode;
        //    }

        //    PaymentConfig item;
        //    for (int i = 0; i < size; i++)
        //    {
        //        item = member[i];
        //        if (item.paymentCode.Equals(paymentCode))
        //        {
        //            return item.paymentCode + " " + item.currencyCode  + " " + item.exchangeRate;
        //        }
        //    }
        //    return "";
        //}

        public string getPCD(string paymentCode, string currencyCode)
        {
            if (paymentCode.Trim() != "FXCU")
            {
                return paymentCode;
            }

            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals(paymentCode) && item.currencyCode.Equals(currencyCode))
                {
                    return item.paymentCode + " " + item.currencyCode + " " + item.exchangeRate;
                }
            }
            return "";
        }

        public PaymentConfig[] data()
        {
            return member;
        }

        public bool getChangeStatus(string pm_code)
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals(pm_code))
                {
                    return item.changeStatus;
                }
            }
            return false;
        }

        public bool getExcessChange(string pm_code)
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals(pm_code))
                {
                    return item.excessChangeStatus;
                }
            }
            return false;
        }

        public int getPaymentTypeID(string pmCode)
        {
            PaymentConfig item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.paymentCode.Equals(pmCode))
                {
                    return item.paymentTypeId;
                }
            }
            return -1;
        }
    }
}
