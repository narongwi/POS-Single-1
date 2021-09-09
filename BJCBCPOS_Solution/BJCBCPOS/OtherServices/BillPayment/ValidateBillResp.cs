using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace BJCBCPOS.OtherServices.BillPayment.ValidateBill {
	[XmlRoot(ElementName = "result")]
	public class Result {
		[XmlElement(ElementName = "code")]
		public string Code { get; set; }
		[XmlElement(ElementName = "msg")]
		public string Msg { get; set; }
		[XmlElement(ElementName = "msgEn")]
		public string MsgEn { get; set; }
	}

	[XmlRoot(ElementName = "payment")]
	public class Payment {
		[XmlAttribute(AttributeName = "paymentId")]
		public string PaymentId { get; set; }
		[XmlAttribute(AttributeName = "pmCode")]
		public string PmCode { get; set; }
	}

	[XmlRoot(ElementName = "paymentList")]
	public class PaymentList {
		[XmlElement(ElementName = "payment")]
		public List<Payment> Payment { get; set; }
	}

	[XmlRoot(ElementName = "validateBillResp")]
	public class ValidateBillResp {
		[XmlElement(ElementName = "result")]
		public Result Result { get; set; }
		[XmlElement(ElementName = "txNo")]
		public string TxNo { get; set; }
		[XmlElement(ElementName = "broker")]
		public string Broker { get; set; }
		[XmlElement(ElementName = "barCode")]
		public string BarCode { get; set; }
		[XmlElement(ElementName = "articleCode")]
		public string ArticleCode { get; set; }
		[XmlElement(ElementName = "receiptFlag")]
		public string ReceiptFlag { get; set; }
		[XmlElement(ElementName = "templateId")]
		public string TemplateId { get; set; }
		[XmlElement(ElementName = "hasVat")]
		public string HasVat { get; set; }
		[XmlElement(ElementName = "feeVatRate")]
		public string FeeVatRate { get; set; }
		[XmlElement(ElementName = "billVatRate")]
		public string BillVatRate { get; set; }
		[XmlElement(ElementName = "custFeeAmt")]
		public string CustFeeAmt { get; set; }
		[XmlElement(ElementName = "brokerFeeAmt")]
		public string BrokerFeeAmt { get; set; }
		[XmlElement(ElementName = "billAmt")]
		public string BillAmt { get; set; }
		[XmlElement(ElementName = "paymentList")]
		public PaymentList PaymentList { get; set; }
		[XmlElement(ElementName = "needQuery")]
		public string NeedQuery { get; set; }
		[XmlElement(ElementName = "ref1")]
		public string Ref1 { get; set; }
		[XmlElement(ElementName = "appServerSdate")]
		public string AppServerSdate { get; set; }
		[XmlElement(ElementName = "appServerEdate")]
		public string AppServerEdate { get; set; }
		[XmlElement(ElementName = "partnerFeeFlag")]
		public string PartnerFeeFlag { get; set; }
		[XmlElement(ElementName = "partnerFeeAmt")]
		public string PartnerFeeAmt { get; set; }
		[XmlElement(ElementName = "custConfirmFlag")]
		public string CustConfirmFlag { get; set; }
		[XmlElement(ElementName = "authenCashier2Flag")]
		public string AuthenCashier2Flag { get; set; }
	}
}
