using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BJCBCPOS_Model
{
    [DataContract]
    public class ResponsePayInvoicePOD : APILog
    {
        [DataMember(EmitDefaultValue = false)]
        public string Result_Code { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Result_Msg_EN { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Result_Msg_TH { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Api_Type { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Payment_ID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Invoice_No { get; set; }

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
    }
}
