using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BJCBCPOS_Model
{
    public class PaymentMenuIcon
    {
        public int MenuID { get; set; }
        public int ReferMenuID { get; set; }
        public int PageID { get; set; }
        public int Row { get; set; }
        public int Comlumn{ get; set; }
        public byte[] Picture { get; set; }
        public string LabelName { get; set; }
        public string PaymentMainCode { get; set; }
        public string SubPaymentCode { get; set; }
        public string SubMenuID { get; set; }
        public string PaymentStepGroupID { get; set; }
        public int LanguageID { get; set; }

        public PaymentMenuIcon(int menuid, int refermenuid, int pageid, int row, int comlumn, byte[] picture, string labelname, string paymentmaincode
            , string subpaymentcode, string submenuid, string paymentstepgroupid, int langID)
        {
            this.MenuID = menuid;
            this.ReferMenuID = refermenuid;
            this.PageID = pageid;
            this.Row = row;
            this.Comlumn = comlumn;
            this.Picture = picture;
            this.LabelName = labelname;
            this.PaymentMainCode = paymentmaincode;
            this.SubPaymentCode = subpaymentcode;
            this.SubMenuID = submenuid;
            this.PaymentStepGroupID = paymentstepgroupid;
            this.LanguageID = langID;
        }
    }

    public class PaymentMenuIconCollections : ICollection<PaymentMenuIcon>
    {
        private PaymentMenuIcon[] member = null;
        private int size = 0;

        private const string MenuID_Name = "MenuID";
        private const string ReferMenuID_Name = "ReferMenuID";
        private const string PageID_Name = "PageID";
        private const string Row_Name = "RowIcon";
        private const string Comlumn_Name = "ColumnIcon";
        private const string Picture_Name = "PictureName";
        private const string Label_Name = "Label_Language";
        private const string PaymentMainCode_Name = "PaymentMainCode";
        private const string SubPaymentCode_Name = "SubPaymentCode";
        private const string SubMenuID_Name = "SubMenuID";
        private const string PaymentStepGroupID_Name = "PaymentStepGroupID";
        private const string Language_ID = "LanguageID";

        public PaymentMenuIconCollections()
        {

        }

        public PaymentMenuIconCollections(DataTable data)
        {
            PaymentMenuIcon item;
            int index;
            int iMenuID, iRefMID, iPageID, iRow, iColumn, iLang;

            if (data != null)
            {
                if (data.Columns.Contains(MenuID_Name) &&
                    data.Columns.Contains(ReferMenuID_Name) &&
                    data.Columns.Contains(PageID_Name) &&
                    data.Columns.Contains(Row_Name) &&
                    data.Columns.Contains(Comlumn_Name) &&
                    data.Columns.Contains(Picture_Name) &&
                    data.Columns.Contains(Label_Name) &&                   
                    data.Columns.Contains(PaymentMainCode_Name) &&
                    data.Columns.Contains(SubPaymentCode_Name) &&
                    data.Columns.Contains(SubMenuID_Name) &&
                    data.Columns.Contains(PaymentStepGroupID_Name) &&
                    data.Columns.Contains(Language_ID))
                {
                    size = data.Rows.Count;
                    member = new PaymentMenuIcon[size];
                    index = 0;

                    foreach (DataRow row in data.Rows)
                    {
                        if (!int.TryParse(row[MenuID_Name].ToString(), out iMenuID)) iMenuID = 0;
                        if (!int.TryParse(row[ReferMenuID_Name].ToString(), out iRefMID)) iRefMID = 0;
                        if (!int.TryParse(row[PageID_Name].ToString(), out iPageID)) iPageID = 0;
                        if (!int.TryParse(row[Row_Name].ToString(), out iRow)) iRow = 0;
                        if (!int.TryParse(row[Comlumn_Name].ToString(), out iColumn)) iColumn = 0;
                        if (!int.TryParse(row[Language_ID].ToString(), out iLang)) iLang = 0;

                        item = new PaymentMenuIcon(iMenuID, iRefMID, iPageID, iRow, iColumn, (byte[])row[Picture_Name], row[Label_Name].ToString()
                            , row[PaymentMainCode_Name].ToString(), row[SubPaymentCode_Name].ToString(), row[SubMenuID_Name].ToString(), row[PaymentStepGroupID_Name].ToString(), iLang);

                        member[index] = item;
                        index++;
                    }
                }
                else
                {
                    member = new PaymentMenuIcon[0];
                    size = 0;
                }
            }
            else
            {
                member = new PaymentMenuIcon[0];
                size = 0;
            }
        }

        public PaymentMenuIconCollections(PaymentMenuIcon[] pmMenuArry, List<PaymentPolicy> lst)
        {
            int cntCredit = 0;
            int cntVoucher = 0;
            PaymentMenuIcon itmCredit = null;
            PaymentMenuIcon itmVoucher = null;
            PaymentMenuIcon item;
            List<PaymentMenuIcon> lstAdd = new List<PaymentMenuIcon>();
            for (int i = 0; i < pmMenuArry.Length; i++)
            {
                foreach (var itmlst in lst)
                {
                    item = pmMenuArry[i];
                    if (item.LanguageID == ProgramConfig.language.ID || item.LanguageID == 0)
                    {
                        if (item.MenuID == 3)
                        {
                            itmCredit = item;
                        }
                        else if (item.MenuID == 4)
                        {
                            itmVoucher = item;
                        }
                        else
                        {
                            if (item.PaymentMainCode.Equals(itmlst.paymentCode) && itmlst.policy == PolicyStatus.Work)
                            {
                                if (ProgramConfig.payment.getPaymentTypeID(item.PaymentMainCode) == 1)
                                {
                                    cntCredit++;
                                }
                                else if (ProgramConfig.payment.getPaymentTypeID(item.PaymentMainCode) == 6)
                                {
                                    cntVoucher++;
                                }

                                lstAdd.Add(item);
                                break;
                            }
                        }
                    }
                }
            }

            if (cntCredit > 0)
            {
                lstAdd.Add(itmCredit);
            }
            if (cntVoucher > 0)
            {
                lstAdd.Add(itmVoucher);
            }

            AddItem(lstAdd);
        }

        public PaymentMenuIconCollections(PaymentMenuIcon[] pmMenuArry, List<PaymentConfig> lst)
        {
            int cntCredit = 0;
            int cntVoucher = 0;
            PaymentMenuIcon itmCredit = null;
            PaymentMenuIcon itmVoucher = null;
            PaymentMenuIcon item;
            List<PaymentMenuIcon> lstAdd = new List<PaymentMenuIcon>();
            for (int i = 0; i < pmMenuArry.Length; i++)
            {
                foreach (var itmlst in lst)
                {
                    item = pmMenuArry[i];
                    if (item.LanguageID == ProgramConfig.language.ID || item.LanguageID == 0)
                    {
                        //if (item.ReferMenuID == 0)
                        //{
                        //    if (pmMenuArry.Any(a => a.ReferMenuID == item.MenuID))
                        //    {
                        //        lstAdd.Add(item);
                        //        break;
                        //    }
                        //}

                        //if (item.PaymentMainCode.Equals(itmlst.paymentCode))
                        //{
                        //    lstAdd.Add(item);
                        //    break;
                        //}

                        if (item.PaymentMainCode.Equals(itmlst.paymentCode))
                        {
                            lstAdd.Add(item);

                            var mainMenu = pmMenuArry.Where(w => w.MenuID == item.ReferMenuID && (w.LanguageID == ProgramConfig.language.ID || w.LanguageID == 0)).Select(s => s).FirstOrDefault();
                            if (mainMenu != null && mainMenu.ReferMenuID == 0)
                            {
                                if (!lstAdd.Contains(mainMenu))
                                {
                                    lstAdd.Add(mainMenu);
                                }
                            }
                            break;
                        }
                        
                    }
                }
            }

            AddItem(lstAdd);
        }

        private void AddItem(List<PaymentMenuIcon> lstAdd)
        {
            PaymentMenuIcon item;
            int index;
            int iMenuID, iRefMID, iPageID, iRow, iColumn, iLang;
            size = lstAdd.Count;
            member = new PaymentMenuIcon[size];
            index = 0;
            foreach (PaymentMenuIcon row in lstAdd)
            {

                if (!int.TryParse(row.MenuID.ToString(), out iMenuID)) iMenuID = 0;
                if (!int.TryParse(row.ReferMenuID.ToString(), out iRefMID)) iRefMID = 0;
                if (!int.TryParse(row.PageID.ToString(), out iPageID)) iPageID = 0;
                if (!int.TryParse(row.Row.ToString(), out iRow)) iRow = 0;
                if (!int.TryParse(row.Comlumn.ToString(), out iColumn)) iColumn = 0;
                if (!int.TryParse(row.LanguageID.ToString(), out iLang)) iLang = 0;

                item = new PaymentMenuIcon(iMenuID, iRefMID, iPageID, iRow, iColumn, row.Picture, row.LabelName.ToString()
                    , row.PaymentMainCode.ToString(), row.SubPaymentCode.ToString(), row.SubMenuID.ToString(), row.PaymentStepGroupID.ToString(), iLang);

                member[index] = item;
                index++;
            }
        }

        public PaymentMenuIcon[] data()
        {
            return member;
        }

        public List<PaymentMenuIcon> GetList()
        {
            return member.ToList();
        }

        #region Function

        public List<PaymentMenuIcon> GetDataByReferMenuID(string referMID, int pageID = -1)
        {
            //var B = member.Select(a => a.ReferMenuID.Equals("0")).Cast<DataRow>();
            List<PaymentMenuIcon> lstItem = new List<PaymentMenuIcon>();
            for (int i = 0; i < size; i++)
            {
                if (pageID >= 0)
                {
                    if (member[i].PageID.Equals(pageID) && member[i].ReferMenuID.ToString().Equals(referMID) && (member[i].LanguageID == ProgramConfig.language.ID || member[i].LanguageID == 0))
                    {
                        lstItem.Add(member[i]);
                    }
                }
                else if (member[i].ReferMenuID.ToString().Equals(referMID) && (member[i].LanguageID == ProgramConfig.language.ID || member[i].LanguageID == 0))
                {
                    lstItem.Add(member[i]);
                }
            }
            return lstItem;
        }

        public bool CheckPMCode(string pm_code)
        {
            PaymentMenuIcon item;
            for (int i = 0; i < size; i++)
            {
                item = member[i];
                if (item.PaymentMainCode.Equals(pm_code))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ICollection<PaymentMenuIcon> Members

        public void Add(PaymentMenuIcon item)
        {
            PaymentMenuIcon[] temp = new PaymentMenuIcon[size + 1];
            this.CopyTo(temp, 0);
            temp[size] = item;
            member = temp;
            size = size + 1;
        }

        public void Clear()
        {
            member = new PaymentMenuIcon[0];
            size = 0;
        }

        public bool Contains(PaymentMenuIcon item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(PaymentMenuIcon[] array, int arrayIndex)
        {
            foreach (PaymentMenuIcon item in member)
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

        public bool Remove(PaymentMenuIcon item)
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

            PaymentMenuIcon[] temp = new PaymentMenuIcon[size - 1];
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

        public IEnumerator<PaymentMenuIcon> GetEnumerator()
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
