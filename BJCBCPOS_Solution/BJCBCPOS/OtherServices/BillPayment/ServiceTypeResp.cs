using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace BJCBCPOS.OtherServices.BillPayment.ServiceType {
	[XmlRoot(ElementName = "result")]
	public class Result {
		[XmlElement(ElementName = "code")]
		public string Code { get; set; }
		[XmlElement(ElementName = "msg")]
		public string Msg { get; set; }
		[XmlElement(ElementName = "msgEn")]
		public string MsgEn { get; set; }
	}

	[XmlRoot(ElementName = "serviceType")]
	public class ServiceType {
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "seq")]
		public string Seq { get; set; }
	}

	[XmlRoot(ElementName = "serviceTypeList")]
	public class ServiceTypeList {
		[XmlElement(ElementName = "serviceType")]
		public List<ServiceType> ServiceType { get; set; }
	}

	[XmlRoot(ElementName = "serviceTypeResp")]
	public class ServiceTypeResp {
		[XmlElement(ElementName = "result")]
		public Result Result { get; set; }
		[XmlElement(ElementName = "serviceTypeList")]
		public ServiceTypeList ServiceTypeList { get; set; }
	}
}
