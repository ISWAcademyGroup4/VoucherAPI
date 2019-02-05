using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class Error
    {
        public ErrorStatusCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public Error(ErrorStatusCode errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
