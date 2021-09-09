using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ConfirmStep2 {
    [XmlRoot(ElementName = "payment")]
    public class Payment {
        [XmlAttribute(AttributeName = "paymentId")]
        public string PaymentId { get; set; }
        [XmlAttribute(AttributeName = "pmCode")]
        public string PmCode { get; set; }
        [XmlAttribute(AttributeName = "payAmt")]
        public string PayAmt { get; set; }
        [XmlAttribute(AttributeName = "billAmt")]
        public string BillAmt { get; set; }
        [XmlAttribute(AttributeName = "feeAmt")]
        public string FeeAmt { get; set; }
        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }
    }

    [XmlRoot(ElementName = "paymentList")]
    public class PaymentList {
        [XmlElement(ElementName = "payment")]
        public Payment Payment { get; set; }
    }

    [XmlRoot(ElementName = "confirmBillReq")]
    public class ConfirmBillReq {
        [XmlElement(ElementName = "txNo")]
        public string TxNo { get; set; }
        [XmlElement(ElementName = "projOwner")]
        public string ProjOwner { get; set; }
        [XmlElement(ElementName = "change")]
        public string Change { get; set; }
        [XmlElement(ElementName = "paymentList")]
        public PaymentList PaymentList { get; set; }
    }

}
