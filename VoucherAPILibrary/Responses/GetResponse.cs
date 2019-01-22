using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class GetResponse
    {
        public string Message { get; set; }
        public object Voucher { get; set; }
        public ServiceResponse serviceResponse { get; set;}

        public GetResponse(string message, object voucher, ServiceResponse serviceResponse)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Voucher = voucher ?? throw new ArgumentNullException(nameof(voucher));
            this.serviceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
