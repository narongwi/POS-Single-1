using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ServiceGroup {
	[XmlRoot(ElementName = "serviceGroupReq")]
	public class ServiceGroupReq {
		[XmlElement(ElementName = "storeCode")]
		public string StoreCode { get; set; }
		[XmlElement(ElementName = "locNo")]
		public string LocNo { get; set; }
		[XmlElement(ElementName = "controlRef")]
		public string ControlRef { get; set; }
		[XmlElement(ElementName = "cashierId")]
		public string CashierId { get; set; }
		[XmlElement(ElementName = "serviceType")]
		public string ServiceType { get; set; }
	}
}
