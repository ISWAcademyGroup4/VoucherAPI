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
        public virtual string Message { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }

        public EnableResponse(string message, ServiceResponse serviceResponse)
        {
            Message = message ?? "No message from server";
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
