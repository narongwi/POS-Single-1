using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.ServiceType {
    [XmlRoot(ElementName = "serviceTypeReq")]
    public class ServiceTypeReq {
        [XmlElement(ElementName = "storeCode")]
        public string StoreCode { get; set; }
        [XmlElement(ElementName = "locNo")]
        public string LocNo { get; set; }
        [XmlElement(ElementName = "controlRef")]
        public string ControlRef { get; set; }
        [XmlElement(ElementName = "cashierId")]
        public string CashierId { get; set; }
    }

}
