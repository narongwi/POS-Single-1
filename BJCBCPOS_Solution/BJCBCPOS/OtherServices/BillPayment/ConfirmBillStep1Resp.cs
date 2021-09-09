using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace BJCBCPOS.OtherServices.BillPayment.ConfirmStep1 {
	[XmlRoot(ElementName = "result")]
	public class Result {
		[XmlElement(ElementName = "code")]
		public string Code { get; set; }
		[XmlElement(ElementName = "msg")]
		public string Msg { get; set; }
		[XmlElement(ElementName = "msgEn")]
		public string MsgEn { get; set; }
	}

	[XmlRoot(ElementName = "billReceipt")]
	public class BillReceipt {
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "seq")]
		public string Seq { get; set; }
		[XmlAttribute(AttributeName = "label")]
		public string Label { get; set; }
	}

	[XmlRoot(ElementName = "billReceiptList")]
	public class ConfirmBillStep1Resp {
		[XmlElement(ElementName = "billReceipt")]
		public List<BillReceipt> BillReceipt { get; set; }
		[XmlAttribute(AttributeName = "receiptNo")]
		public string ReceiptNo { get; set; }
		[XmlAttribute(AttributeName = "vatAmt")]
		public string VatAmt { get; set; }
		[XmlAttribute(AttributeName = "receiptAmt")]
		public string ReceiptAmt { get; set; }
	}

	[XmlRoot(ElementName = "feeReceipt")]
	public class FeeReceipt {
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "seq")]
		public string Seq { get; set; }
		[XmlAttribute(AttributeName = "label")]
		public string Label { get; set; }
	}

	[XmlRoot(ElementName = "feeReceiptList")]
	public class FeeReceiptList {
		[XmlElement(ElementName = "feeReceipt")]
		public List<FeeReceipt> FeeReceipt { get; set; }
		[XmlAttribute(AttributeName = "receiptNo")]
		public string ReceiptNo { get; set; }
		[XmlAttribute(AttributeName = "vatAmt")]
		public string VatAmt { get; set; }
		[XmlAttribute(AttributeName = "receiptAmt")]
		public string ReceiptAmt { get; set; }
	}

	[XmlRoot(ElementName = "confirmBillResp")]
	public class ConfirmBillResp {
		[XmlElement(ElementName = "result")]
		public Result Result { get; set; }
		[XmlElement(ElementName = "txNo")]
		public string TxNo { get; set; }
		[XmlElement(ElementName = "billReceiptList")]
		public ConfirmBillStep1Resp BillReceiptList { get; set; }
		[XmlElement(ElementName = "feeReceiptList")]
		public FeeReceiptList FeeReceiptList { get; set; }
		[XmlElement(ElementName = "appServerSdate")]
		public string AppServerSdate { get; set; }
		[XmlElement(ElementName = "appServerEdate")]
		public string AppServerEdate { get; set; }
		[XmlElement(ElementName = "ref1")]
		public string Ref1 { get; set; }
		[XmlElement(ElementName = "ref2")]
		public string Ref2 { get; set; }
	}
}
