using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace BJCBCPOS.OtherServices.BillPayment.MessageGuide {
	[XmlRoot(ElementName = "messageGuideReq")]
	public class MessageGuideReq {
		[XmlElement(ElementName = "storeCode")]
		public string StoreCode { get; set; }
		[XmlElement(ElementName = "locNo")]
		public string LocNo { get; set; }
		[XmlElement(ElementName = "controlRef")]
		public string ControlRef { get; set; }
		[XmlElement(ElementName = "cashierId")]
		public string CashierId { get; set; }
		[XmlElement(ElementName = "formId")]
		public List<string> FormId { get; set; }
	}

}
