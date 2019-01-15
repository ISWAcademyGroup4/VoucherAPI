using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class ServiceResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string Description { get; set; }
        public List<Error> Errors { get; set; }

        public ServiceResponse(HttpStatusCode responseCode, string responseMessage, string description, List<Error> errors)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage ?? throw new ArgumentNullException(nameof(responseMessage));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Errors = errors;
        }
    }
}
