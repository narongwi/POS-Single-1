using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace BJCBCPOS.OtherServices.BillPayment.ConfirmPay {
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

	[XmlRoot(ElementName = "confirmPayReq")]
	public class ConfirmPayReq {
		[XmlElement(ElementName = "storeCode")]
		public string StoreCode { get; set; }
		[XmlElement(ElementName = "locNo")]
		public string LocNo { get; set; }
		[XmlElement(ElementName = "controlRef")]
		public string ControlRef { get; set; }
		[XmlElement(ElementName = "txNo")]
		public string TxNo { get; set; }
		[XmlElement(ElementName = "operateDate")]
		public string OperateDate { get; set; }
		[XmlElement(ElementName = "paymentList")]
		public PaymentList PaymentList { get; set; }
	}
}
