using System.Xml.Serialization;

namespace BJCBCPOS.OtherServices.BillPayment.MessageGuide {
    [XmlRoot(ElementName = "result")]
    public class Result {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "msg")]
        public string Msg { get; set; }
        [XmlElement(ElementName = "msgEn")]
        public string MsgEn { get; set; }
        [XmlElement(ElementName = "desc")]
        public string Desc { get; set; }
    }

    [XmlRoot(ElementName = "messageGuideResp")]
    public class MessageGuideResp {
        [XmlElement(ElementName = "result")]
        public Result Result { get; set; }
    }
}
