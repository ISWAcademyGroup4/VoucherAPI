using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class DeleteVoucherResponse
    {   
        public virtual ServiceResponse ServiceResponse { get; set; }

        public DeleteVoucherResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse;
        }
    }
}
