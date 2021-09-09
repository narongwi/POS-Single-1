using System.Collections.Generic;
using System.Xml.Serialization;

namespace BJCBCPOS.OtherServices.BillPayment.NewBill {
    [XmlRoot(ElementName = "result")]
    public class Result {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "msg")]
        public string Msg { get; set; }
        [XmlElement(ElementName = "msgEn")]
        public string MsgEn { get; set; }
    }

    [XmlRoot(ElementName = "value")]
    public class Value {
        [XmlAttribute(AttributeName = "receiptDisplayFlag")]
        public string ReceiptDisplayFlag { get; set; }
        [XmlAttribute(AttributeName = "posDisplayFlag")]
        public string PosDisplayFlag { get; set; }
        [XmlAttribute(AttributeName = "isOverride")]
        public string IsOverride { get; set; }
        [XmlAttribute(AttributeName = "isRequired")]
        public string IsRequired { get; set; }
        [XmlAttribute(AttributeName = "val")]
        public string Val { get; set; }
        [XmlAttribute(AttributeName = "label")]
        public string Label { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "custIsRequired")]
        public string CustIsRequired { get; set; }
        [XmlAttribute(AttributeName = "custIsOverride")]
        public string CustIsOverride { get; set; }
        [XmlAttribute(AttributeName = "custPosDisplayFlag")]
        public string CustPosDisplayFlag { get; set; }
        [XmlAttribute(AttributeName = "custFormat")]
        public string CustFormat { get; set; }
        [XmlAttribute(AttributeName = "format")]
        public string Format { get; set; }
        [XmlAttribute(AttributeName = "max")]
        public string Max { get; set; }
        [XmlAttribute(AttributeName = "min")]
        public string Min { get; set; }
        [XmlAttribute(AttributeName = "regExpr")]
        public string RegExpr { get; set; }
    }

    [XmlRoot(ElementName = "entry")]
    public class Entry {
        [XmlElement(ElementName = "key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "value")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "billFields")]
    public class BillFields {
        [XmlElement(ElementName = "entry")]
        public List<Entry> Entry { get; set; }
    }

    [XmlRoot(ElementName = "newBillResp")]
    public class NewBillResp {
        [XmlElement(ElementName = "result")]
        public Result Result { get; set; }
        [XmlElement(ElementName = "txNo")]
        public string TxNo { get; set; }
        [XmlElement(ElementName = "billerId")]
        public string BillerId { get; set; }
        [XmlElement(ElementName = "billerName")]
        public string BillerName { get; set; }
        [XmlElement(ElementName = "billerShortName")]
        public string BillerShortName { get; set; }
        [XmlElement(ElementName = "needQuery")]
        public string NeedQuery { get; set; }
        [XmlElement(ElementName = "btype")]
        public string Btype { get; set; }
        [XmlElement(ElementName = "serviceId")]
        public string ServiceId { get; set; }
        [XmlElement(ElementName = "serviceName")]
        public string ServiceName { get; set; }
        [XmlElement(ElementName = "billerTaxId")]
        public string BillerTaxId { get; set; }
        [XmlElement(ElementName = "billerAddr1")]
        public string BillerAddr1 { get; set; }
        [XmlElement(ElementName = "billerAddr2")]
        public string BillerAddr2 { get; set; }
        [XmlElement(ElementName = "billerAddr3")]
        public string BillerAddr3 { get; set; }
        [XmlElement(ElementName = "billerPhoneNo")]
        public string BillerPhoneNo { get; set; }
        [XmlElement(ElementName = "isTopup")]
        public string IsTopup { get; set; }
        [XmlElement(ElementName = "topupSalePrice")]
        public string TopupSalePrice { get; set; }
        [XmlElement(ElementName = "formId")]
        public string FormId { get; set; }
        [XmlElement(ElementName = "subAccountCode")]
        public string SubAccountCode { get; set; }
        [XmlElement(ElementName = "accountCode")]
        public string AccountCode { get; set; }
        [XmlElement(ElementName = "subAccount")]
        public string SubAccount { get; set; }
        [XmlElement(ElementName = "interBranch")]
        public string InterBranch { get; set; }
        [XmlElement(ElementName = "vendorCode")]
        public string VendorCode { get; set; }
        [XmlElement(ElementName = "allowVoidBill")]
        public string AllowVoidBill { get; set; }
        [XmlElement(ElementName = "allowVoidFee")]
        public string AllowVoidFee { get; set; }
        [XmlElement(ElementName = "serviceGroupId")]
        public string ServiceGroupId { get; set; }
        [XmlElement(ElementName = "serviceGroupName")]
        public string ServiceGroupName { get; set; }
        [XmlElement(ElementName = "billFields")]
        public BillFields BillFields { get; set; }
        [XmlElement(ElementName = "appServerSdate")]
        public string AppServerSdate { get; set; }
        [XmlElement(ElementName = "appServerEdate")]
        public string AppServerEdate { get; set; }
        [XmlElement(ElementName = "projOwner")]
        public string ProjOwner { get; set; }
    }
}
