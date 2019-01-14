using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class UpdateVoucherResponse
    {
        public virtual string UpdateResponseMessage { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }

        public UpdateVoucherResponse(string updateResponseMessage, ServiceResponse serviceResponse)
        {
            UpdateResponseMessage = updateResponseMessage;
            ServiceResponse = serviceResponse;
        }
    }
}
