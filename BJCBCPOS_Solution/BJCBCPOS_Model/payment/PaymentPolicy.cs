using System;
using System.Collections.Generic;
using System.Data;

namespace BJCBCPOS_Model
{
    public class PaymentPolicy
    {
        public string paymentCode { get; set; }
        public FunctionID functionId { get; set; }
        public string functionDesc { get; set; }
        public PolicyStatus policy { get; set; }


        public PaymentPolicy()
        {
        }

        public PaymentPolicy(string paymentCode, FunctionID functionId, string functionDesc, PolicyStatus policy)
        {
            this.paymentCode = paymentCode;
            this.functionId = functionId;
            this.functionDesc = functionDesc;
            this.policy = policy;
        }

        public override int GetHashCode()
        {
            return paymentCode.GetHashCode() + functionId.GetHashCode() + functionDesc.GetHashCode() + policy.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            return obj is PaymentPolicy && this == (PaymentPolicy)obj;
        }

        public static bool operator ==(PaymentPolicy x, PaymentPolicy y)
        {
            return x.paymentCode == y.paymentCode && x.functionId == y.functionId && x.functionDesc == y.functionDesc && x.policy == y.policy;
        }

        public static bool operator !=(PaymentPolicy x, PaymentPolicy y)
        {
            return x.paymentCode != y.paymentCode || x.functionId != y.functionId || x.functionDesc != y.functionDesc || x.policy != y.policy;
        }
    }

    public class PaymentPolicyCollections : ICollection<PaymentPolicy>
    {
        
        private PaymentPolicy[] member = null;
        private int size = 0;

        private const string paymentCode_Name = "PaymentCode";
        private const string functionId_Name = "FunctionID";
        private const string functionDesc_Name = "FunctionDesc";
        private const string policy_Name = "PolicyStatus";

        public PaymentPolicyCollections()
        {
            member = new PaymentPolicy[0];
            size = 0;
        }

        public PaymentPolicyCollections(int size)
        {
            member = new PaymentPolicy[size];
            this.size = size;
        }

        public PaymentPolicyCollections(DataTable data)
        {
            PaymentPolicy item;
            int index, policy_value;
            if (data != null)
            {
                if (data.Columns.Contains(paymentCode_Name) &&
                    data.Columns.Contains(functionId_Name) &&
                    data.Columns.Contains(functionDesc_Name) &&
                    data.Columns.Contains(policy_Name))
                {
                    size = data.Rows.Count;
                    member = new PaymentPolicy[size];
                    index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        if (!int.TryParse(row[policy_Name].ToString(), out policy_value)) policy_value = 0;

                        item = new PaymentPolicy(row[paymentCode_Name].ToString().Trim(), new FunctionID(row[functionId_Name].ToString().Trim()),
                            row[functionDesc_Name].ToString().Trim(), (PolicyStatus)policy_value);
                        member[index] = item;
                        index++;
                    }
                }
                else
                {
                    member = new PaymentPolicy[0];
                    size = 0;
                }
            }
            else
            {
                member = new PaymentPolicy[0];
                size = 0;
            }
        }

        public List<PaymentPolicy> GetPaymentPolicyByFunction(FunctionID functionID)
        {
            PaymentPolicy item;
            List<PaymentPolicy> lstAdd = new List<PaymentPolicy>();
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.functionId.formatValue.Equals(functionID.formatValue) && item.policy == PolicyStatus.Work)
                {
                    lstAdd.Add(item);
                }
            }

            return lstAdd;
        }

        public PaymentPolicy GetPaymentPolicyByFunctionPaymentCode(FunctionID functionID, string pmCode)
        {
            PaymentPolicy item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.functionId.formatValue.Equals(functionID.formatValue) && item.paymentCode == pmCode)
                {
                    return item;
                }
            }

            return new PaymentPolicy();
        }

        #region ICollection<PaymentPolicy> Members

        public void Add(PaymentPolicy item)
        {
            PaymentPolicy[] temp = new PaymentPolicy[size + 1];
            this.CopyTo(temp, 0);
            temp[size] = item;
            member = temp;
            size = size + 1;
        }

        public void Clear()
        {
            member = new PaymentPolicy[0];
            size = 0;
        }

        public bool Contains(PaymentPolicy item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(PaymentPolicy[] array, int arrayIndex)
        {
            foreach (PaymentPolicy item in member)
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

        public bool Remove(PaymentPolicy item)
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

            PaymentPolicy[] temp = new PaymentPolicy[size - 1];
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

        #region IEnumerable<PaymentPolicy> Members

        public IEnumerator<PaymentPolicy> GetEnumerator()
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

    }
}
