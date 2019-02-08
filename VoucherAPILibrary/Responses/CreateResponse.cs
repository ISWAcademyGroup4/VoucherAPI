using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class CreateResponse
    {
        public string Message { get; set; }
        public string Campaign { get; set; }
        public string VoucherType { get; set; }
        public int Count { get; set; }
        public string BatchNo { get; set; }

        public ServiceResponse ServiceResponse { get; set; }

        public CreateResponse(string message, string campaign, string voucherType, int count, string batchno, ServiceResponse serviceResponse)
        {
            Message = message;
            Campaign = campaign;
            VoucherType = voucherType;
            Count = count;
            BatchNo = batchno;
            ServiceResponse = serviceResponse;
        }

        public CreateResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse;
        }
    }
}
