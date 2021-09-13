
using BJCBCPOS.OtherServices.Helpers;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BJCBCPOS.OtherServices.Services {
    public class BillPaymentService {
        private readonly string basePath = @"D:\Repository\Deftsoft\POS-Single\BJCBCPOS_Solution\BJCBCPOS\OtherServices\BillPayment";
        public BillPayment.ServiceType.ServiceTypeResp GetServiceType() {
            try {
                // request
                var request = new BillPayment.ServiceType.ServiceTypeReq();
                request = request.DeserializeFromFile(Path.Combine(basePath,"1. Bill Payment List Menu Request.xml"));
                var xlmparam = request.SerializeObject();

                // response
                var serviceType = new BillPayment.ServiceType.ServiceTypeResp();
                return serviceType.DeserializeFromFile(Path.Combine(basePath,"2. Bill Payment List Menu Response.xml"));

            } catch(Exception ex) {

                throw ex;
            }
        }
    }
}
