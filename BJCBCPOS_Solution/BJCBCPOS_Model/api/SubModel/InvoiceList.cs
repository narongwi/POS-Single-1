using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BJCBCPOS_Model
{
    [DataContract]
    public class Invoice_List
    {
        [DataMember(EmitDefaultValue = false)]
        public string Invoice_No { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Apply_Amount { get; set; }
    }
}
