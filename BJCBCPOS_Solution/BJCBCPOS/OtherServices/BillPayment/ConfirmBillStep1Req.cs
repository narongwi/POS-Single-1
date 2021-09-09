using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ConfirmStep1 {
    [XmlRoot(ElementName = "paymentList")]
    public class PaymentList {
        [XmlAttribute(AttributeName = "billAmt")]
        public string BillAmt { get; set; }
        [XmlAttribute(AttributeName = "billDiscAmt")]
        public string BillDiscAmt { get; set; }
        [XmlAttribute(AttributeName = "discRoundDown")]
        public string DiscRoundDown { get; set; }
        [XmlAttribute(AttributeName = "feeAmt")]
        public string FeeAmt { get; set; }
        [XmlAttribute(AttributeName = "feeVatAmt")]
        public string FeeVatAmt { get; set; }
        [XmlAttribute(AttributeName = "payAmt")]
        public string PayAmt { get; set; }
        [XmlAttribute(AttributeName = "billReceiptNo")]
        public string BillReceiptNo { get; set; }
        [XmlAttribute(AttributeName = "feeReceiptNo")]
        public string FeeReceiptNo { get; set; }
        [XmlAttribute(AttributeName = "posReference")]
        public string PosReference { get; set; }
        [XmlAttribute(AttributeName = "billRollingNo")]
        public string BillRollingNo { get; set; }
        [XmlAttribute(AttributeName = "posControlRef")]
        public string PosControlRef { get; set; }
    }

    [XmlRoot(ElementName = "confirmBillReq")]
    public class ConfirmBillStep1Req {
        [XmlElement(ElementName = "txNo")]
        public string TxNo { get; set; }
        [XmlElement(ElementName = "operateDate")]
        public string OperateDate { get; set; }
        [XmlElement(ElementName = "transactionDate")]
        public string TransactionDate { get; set; }
        [XmlElement(ElementName = "projOwner")]
        public string ProjOwner { get; set; }
        [XmlElement(ElementName = "paymentList")]
        public PaymentList PaymentList { get; set; }
    }
}
