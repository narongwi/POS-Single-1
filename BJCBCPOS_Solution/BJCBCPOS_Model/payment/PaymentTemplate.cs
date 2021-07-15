using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BJCBCPOS_Model
{
    public class PaymentTemplateConfig
    {
        public string Payment { get; set; }
        public int Seq { get; set; }
        public int StepID { get; set; }
        public string LabelTxt { get; set; }
        public string DataType { get; set; }
        public string DataLenght { get; set; }
        public PaymentStepDetail_ModuleID Module { get; set; }

        public PaymentTemplateConfig(string payment, int seq, int stepId, string labeltxt, string dataType, string dataLength, PaymentStepDetail_ModuleID module)
        {
            this.Payment = payment;
            this.Seq = seq;
            this.StepID = stepId;
            this.LabelTxt = labeltxt;
            this.DataType = dataType;
            this.DataLenght = dataLength;
            this.Module = Enum.IsDefined(typeof(PaymentStepDetail_ModuleID), module) ? module : PaymentStepDetail_ModuleID.None;
        }
    }

    public class PaymentTemplateConfigCollections : ICollection<PaymentTemplateConfig>
    {
        private PaymentTemplateConfig[] member = null;
        private int size = 0;

        private const string Payment_Name = "Payment";
        private const string Seq_Name = "Seq";
        private const string StepID_Name = "StepID";
        private const string LabelTxt_Name = "LabelTxt";
        private const string DataType_Name = "DataType";
        private const string DataLenght_Name = "DataLenght";
        private const string Module = "Module";

        public PaymentTemplateConfigCollections()
        {

        }

        public PaymentTemplateConfigCollections(DataTable data)
        {
            PaymentTemplateConfig item;
            int index;
            int iSeq, iStepID;

            if (data != null)
            {
                if (data.Columns.Contains(Payment_Name) &&
                    data.Columns.Contains(Seq_Name) &&
                    data.Columns.Contains(StepID_Name) &&
                    data.Columns.Contains(LabelTxt_Name) &&
                    data.Columns.Contains(DataType_Name) &&
                    data.Columns.Contains(DataLenght_Name) &&
                    data.Columns.Contains(Module))
                {
                    size = data.Rows.Count;
                    member = new PaymentTemplateConfig[size];
                    index = 0;

                    foreach (DataRow row in data.Rows)
                    {
                        if (!int.TryParse(row[Seq_Name].ToString(), out iSeq)) iSeq = 0;
                        if (!int.TryParse(row[StepID_Name].ToString(), out iStepID)) iStepID = 0;

                        item = new PaymentTemplateConfig(row[Payment_Name].ToString(), iSeq, iStepID, row[LabelTxt_Name].ToString(), row[DataType_Name].ToString()
                            , row[DataLenght_Name].ToString(), (PaymentStepDetail_ModuleID)row[Module]);

                        member[index] = item;
                        index++;
                    }
                }
                else
                {
                    member = new PaymentTemplateConfig[0];
                    size = 0;
                }
            }
            else
            {
                member = new PaymentTemplateConfig[0];
                size = 0;
            }
        }

        public PaymentTemplateConfig[] getModuleFromLabel(string payment, string label)
        {
            List<PaymentTemplateConfig> res = new List<PaymentTemplateConfig>();
            PaymentTemplateConfig item;

            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.LabelTxt.Equals(label) && item.Payment.Trim().Equals(payment))
                {
                    res.Add(item);
                }
            }
            return res.ToArray();
        }

        #region ICollection<PaymentConfig> Members

        public void Add(PaymentTemplateConfig item)
        {
            PaymentTemplateConfig[] temp = new PaymentTemplateConfig[size + 1];
            this.CopyTo(temp, 0);
            temp[size] = item;
            member = temp;
            size = size + 1;
        }

        public void Clear()
        {
            member = new PaymentTemplateConfig[0];
            size = 0;
        }

        public bool Contains(PaymentTemplateConfig item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(PaymentTemplateConfig[] array, int arrayIndex)
        {
            foreach (PaymentTemplateConfig item in member)
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

        public bool Remove(PaymentTemplateConfig item)
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

            PaymentTemplateConfig[] temp = new PaymentTemplateConfig[size - 1];
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

        public IEnumerator<PaymentTemplateConfig> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
