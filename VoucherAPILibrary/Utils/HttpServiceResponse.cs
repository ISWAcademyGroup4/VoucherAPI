using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class HttpServiceResponse
    {
        public virtual HttpStatusCode StatusCode { get; set; }
        public virtual string Message { get; set; }
        public virtual string Description { get; set; }

        public HttpServiceResponse(HttpStatusCode statusCode, string message, string description)
        {
            StatusCode = statusCode;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
