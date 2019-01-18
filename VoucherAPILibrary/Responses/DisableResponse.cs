using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class DisableResponse
    {
        public string Message = "Voucher was successfully disabled";
        public virtual ServiceResponse ServiceResponse { get; set; }

        public DisableResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
