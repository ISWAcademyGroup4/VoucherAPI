using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class UpdateResponse
    {
        public virtual string Message { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }

        public UpdateResponse(string Message, ServiceResponse serviceResponse)
        {
            this.Message = Message;
            ServiceResponse = serviceResponse;
        }
    }
}
