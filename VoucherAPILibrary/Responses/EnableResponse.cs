using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class EnableResponse
    {
        public string Message = "Voucher was updated successfully";
        public virtual ServiceResponse ServiceResponse { get; set; }

        public EnableResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
