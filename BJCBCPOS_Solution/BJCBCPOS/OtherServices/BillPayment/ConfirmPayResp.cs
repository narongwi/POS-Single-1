using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ConfirmPay {
    [XmlRoot(ElementName = "result")]
    public class Result {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "msg")]
        public string Msg { get; set; }
        [XmlElement(ElementName = "msgEn")]
        public string MsgEn { get; set; }
    }

    [XmlRoot(ElementName = "confirmPayResp")]
    public class ConfirmPayResp {
        [XmlElement(ElementName = "result")]
        public Result Result { get; set; }
        [XmlElement(ElementName = "controlRef")]
        public string ControlRef { get; set; }
        [XmlElement(ElementName = "appServerSdate")]
        public string AppServerSdate { get; set; }
        [XmlElement(ElementName = "appServerEdate")]
        public string AppServerEdate { get; set; }
    }

}
