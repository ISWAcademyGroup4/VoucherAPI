using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class CreateResponse
    {
        public string Message { get; set; }
        public ServiceResponse ServiceResponse { get; set; }

        public CreateResponse(string message, ServiceResponse serviceResponse)
        {
            Message = message;      
            ServiceResponse = serviceResponse;
        }

        public CreateResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse;
        }
    }
}
