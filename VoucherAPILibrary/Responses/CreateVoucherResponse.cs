using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class CreateVoucherResponse
    {
        public List<string> Vouchers { get; set; }
        public string VoucherType { get; set; }
        public ServiceResponse ServiceResponse { get; set; }

        public CreateVoucherResponse(List<string> vouchers, string voucherType, ServiceResponse serviceResponse)
        {
            Vouchers = vouchers;
            VoucherType = voucherType;      
            ServiceResponse = serviceResponse;
        }
    }
}
