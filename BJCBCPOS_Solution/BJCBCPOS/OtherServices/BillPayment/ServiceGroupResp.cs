using System.Collections.Generic;
using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ServiceGroup {

    [XmlRoot(ElementName = "result")]
    public class Result {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "msg")]
        public string Msg { get; set; }
        [XmlElement(ElementName = "msgEn")]
        public string MsgEn { get; set; }
    }

    [XmlRoot(ElementName = "serviceGroup")]
    public class ServiceGroup {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "seq")]
        public string Seq { get; set; }
        [XmlAttribute(AttributeName = "custPosDisplayFlag")]
        public string CustPosDisplayFlag { get; set; }
    }

    [XmlRoot(ElementName = "serviceGroupList")]
    public class ServiceGroupList {
        [XmlElement(ElementName = "serviceGroup")]
        public List<ServiceGroup> ServiceGroup { get; set; }
    }

    [XmlRoot(ElementName = "serviceGroupResp")]
    public class ServiceGroupResp {
        [XmlElement(ElementName = "result")]
        public Result Result { get; set; }
        [XmlElement(ElementName = "serviceGroupList")]
        public ServiceGroupList ServiceGroupList { get; set; }
    }

}
