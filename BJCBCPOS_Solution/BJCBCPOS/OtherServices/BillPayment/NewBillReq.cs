using System.Xml.Serialization;
namespace BJCBCPOS.OtherServices.BillPayment.NewBill {
    [XmlRoot(ElementName = "newBillReq")]
    public class NewBillReq {
        [XmlElement(ElementName = "storeCode")]
        public string StoreCode { get; set; }
        [XmlElement(ElementName = "locNo")]
        public string LocNo { get; set; }
        [XmlElement(ElementName = "controlRef")]
        public string ControlRef { get; set; }
        [XmlElement(ElementName = "posControlRef")]
        public string PosControlRef { get; set; }
        [XmlElement(ElementName = "cashierId")]
        public string CashierId { get; set; }
        [XmlElement(ElementName = "barcodeMode")]
        public string BarcodeMode { get; set; }
        [XmlElement(ElementName = "barcode1")]
        public string Barcode1 { get; set; }
        [XmlElement(ElementName = "barcode2")]
        public string Barcode2 { get; set; }
        [XmlElement(ElementName = "barcode3")]
        public string Barcode3 { get; set; }
        [XmlElement(ElementName = "barcode4")]
        public string Barcode4 { get; set; }
    }
}
