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
        public virtual string Message { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }

        public UpdateVoucherResponse(string Message, ServiceResponse serviceResponse)
        {
            this.Message = Message;
            ServiceResponse = serviceResponse;
        }
    }
}
