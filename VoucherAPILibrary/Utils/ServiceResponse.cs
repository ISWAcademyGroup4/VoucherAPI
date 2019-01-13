using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class ServiceResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<Error> Errors { get; set; }

        

        public ServiceResponse(string responseCode, string responseMessage, List<Error> errors)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            Errors = errors;
        }
    }
}
