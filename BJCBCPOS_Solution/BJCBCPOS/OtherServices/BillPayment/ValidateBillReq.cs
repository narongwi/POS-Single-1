using System.Collections.Generic;
using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ValidateBill {
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

    [XmlRoot(ElementName = "validateBillReq")]
    public class ValidateBillReq {
        [XmlElement(ElementName = "txNo")]
        public string TxNo { get; set; }
        [XmlElement(ElementName = "needQuery")]
        public string NeedQuery { get; set; }
        [XmlElement(ElementName = "billFields")]
        public BillFields BillFields { get; set; }
        [XmlElement(ElementName = "projOwner")]
        public string ProjOwner { get; set; }
    }
}
