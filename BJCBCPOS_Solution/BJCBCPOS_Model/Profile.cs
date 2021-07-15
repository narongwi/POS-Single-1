using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// utility class to keep data from pos_GetAuthority
    /// contains profile, policy, reason status and different user of each function id
    /// </summary>
    public class Profile
    {
        public FunctionID functionId { get; set; }
        public PolicyStatus policy { get; set; }
        public ProfileStatus profile { get; set; }
        public bool reasonStatus { get; set; }
        public bool diffUserStatus { get; set; }

        public Profile()
        {
            this.functionId = new FunctionID("");
            this.policy = PolicyStatus.NotFound;
            this.profile = ProfileStatus.NotFound;
            this.reasonStatus = false;
            this.diffUserStatus = false;
        }

        public Profile(string functionId, int policy, int profile, string reason, string diff)
        {
            this.functionId = new FunctionID(functionId);
            this.policy = (PolicyStatus)policy;
            this.profile = (ProfileStatus)profile;
            this.reasonStatus = reason.ToUpper().Equals("Y", StringComparison.OrdinalIgnoreCase);
            this.diffUserStatus = diff.ToUpper().Equals("Y", StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Profile other)
        {
            return this.functionId == other.functionId && 
                this.policy == other.policy && 
                this.profile == other.profile && 
                this.reasonStatus == other.reasonStatus && 
                this.diffUserStatus == other.diffUserStatus;
        }

        public bool found
        {
            get 
            {
                return (this.policy != PolicyStatus.NotFound) && (this.profile != ProfileStatus.NotFound);
            }
        }
    }

    /// <summary>
    /// collection class of Profile class
    /// to keep all profile data get from pos_GetAuthority
    /// </summary>
    public class ProfileCollection : ICollection<Profile>
    {
        private const string functionId_Name = "FunctionID";
        private const string policyStatus_Name = "PolicyStatus";
        private const string profileStatus_Name = "ProfileStatus";
        private const string diffUserStatus_Name = "DiffUserStatus";
        private const string reasonStatus_Name = "ReasonStatus";
        private Profile[] member = null;
        private int size = 0;

        public ProfileCollection()
        {
            member = new Profile[0];
            size = 0;
        }
        
        public ProfileCollection(DataTable data)
        {
            Profile item;
            int index, policy, profile;
            if (data != null)
            {
                if (data.Columns.Contains(functionId_Name) &&
                    data.Columns.Contains(policyStatus_Name) &&
                    data.Columns.Contains(profileStatus_Name) &&
                    data.Columns.Contains(diffUserStatus_Name) &&
                    data.Columns.Contains(reasonStatus_Name))
                {
                    size = data.Rows.Count;
                    member = new Profile[size];
                    index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        if (!int.TryParse(row[policyStatus_Name].ToString(), out policy)) policy = -1;
                        if (!int.TryParse(row[profileStatus_Name].ToString(), out profile)) profile = -1;

                        item = new Profile(row[functionId_Name].ToString().Trim().Replace("-", ""), policy, profile,
                            row[reasonStatus_Name].ToString().Trim(), row[diffUserStatus_Name].ToString().Trim());
                        member[index] = item;
                        index++;
                    }
                }
                else
                {
                    member = new Profile[0];
                    size = 0;
                }
            }
            else
            {
                member = new Profile[0];
                size = 0;
            }
        }
        
        public Profile getByFunctionId(FunctionID function)
        {
            for (int i = 0; i < size; i++)
            {
                if (member[i].functionId == function)
                {
                    return member[i];
                }
            }
            AppLog.writeLog(new Exception(string.Format("Can not find FunctionId {0} in profileCollections.", function.formatValue)));
            return new Profile();
        }

        public Profile getByFunctionId(string functionId)
        {
            for (int i = 0; i < size; i++)
            {
                if (member[i].functionId.value.Trim() == functionId)
                {
                    return member[i];
                }
            }
            AppLog.writeLog(new Exception(string.Format("Can not find FunctionId {0} in profileCollections.", functionId)));
            return new Profile();
        }

        #region ICollection<Profile> Members

        public void Add(Profile item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Profile item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Profile[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Profile item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Profile> Members

        public IEnumerator<Profile> GetEnumerator()
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
    }

    public enum PolicyStatus
    {
        NotFound = -1,
        NotCheck = 0,
        Skip = 1,
        Work = 2
    }

    public enum ProfileStatus
    {
        NotFound = -1,
        NotCheck = 0,
        NotAuthorize = 1,
        Authorize = 2
    }

    //public class ReasonStatus: IEquatable<ReasonStatus>
    //{
    //    public static ReasonStatus Display { get { return new ReasonStatus("Y"); } }
    //    public static ReasonStatus NotDisplay { get { return new ReasonStatus("N"); } }

    //    public string value { get; set; }

    //    public ReasonStatus(string value)
    //    {
    //        this.value = value;
    //    }

    //    #region IEquatable<ReasonStatus> Members

    //    public bool Equals(ReasonStatus other)
    //    {
    //        return this.value == other.value;
    //    }

    //    #endregion
    //}

    //public class DiffUserStatus: IEquatable<DiffUserStatus>
    //{
    //    public static DiffUserStatus Required { get { return new DiffUserStatus("Y"); } }
    //    public static DiffUserStatus NotRequired { get { return new DiffUserStatus("N"); } }

    //    public string value { get; set; }

    //    public DiffUserStatus(string value)
    //    {
    //        this.value = value;
    //    }

    //    #region IEquatable<DiffUserStatus> Members

    //    public bool Equals(DiffUserStatus other)
    //    {
    //        return this.value == other.value;
    //    }

    //    #endregion
    //}
}
