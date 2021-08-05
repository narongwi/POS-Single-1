using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using BJCBCPOS_Model;

namespace BJCBCPOS_Model.api.Request
{
    [DataContract]
    public class RequestPayInvoiceAR : APILog
    {
        [DataMember(EmitDefaultValue = false)]
        public string Trans_ID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Payment_Amount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Payment_Date { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Pay_Store_Code { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Created_By { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Receipt_No { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<Payment_List> Payment_list { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<Invoice_List> Invoice_list { get; set; }
        
    }
}
