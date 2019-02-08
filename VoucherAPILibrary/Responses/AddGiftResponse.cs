using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class AddGiftResponse
    {

        public virtual string Message { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }

        public AddGiftResponse(string message, ServiceResponse serviceResponse)
        {
            Message = message ?? "No message from Server";
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
