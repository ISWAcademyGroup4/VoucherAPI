using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class ErrorResponseHandler
    {
        public static Error GetErrorResponse(int Code)
        {
            var errorCode = GetEnumValue.GetStatusResponse<ErrorStatusCode>(Code);
            var message = GetEnumValue.GetMessage<ErrorStatusCode>(errorCode);

            return new Error(errorCode, message);         
        }
    }
}
