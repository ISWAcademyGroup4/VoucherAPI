using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Globalization;

namespace VoucherAPILibrary.Utils
{
    public class HttpResponseHandler
    {
       
        public static ServiceResponse GetServiceResponse(int Code)
        {
            var statusCode = GetEnumValue.GetStatusResponse<HttpStatusCode>(Code);
            var message = GetEnumValue.GetMessage<HttpStatusCode>(statusCode);
            var description = GetEnumValue.GetDescription<HttpStatusCode>(statusCode);

            switch (Code)
            {
                case int n when (n < 400):
                    return new ServiceResponse(statusCode, message, description, null );

                case int n when (n >= 400):
                    return new ServiceResponse(statusCode, message, description, null);

                default:
                    break;
            }

            return null;
           
        }

        public static ServiceResponse GetServiceResponse(int Code, string message, string description)
        {
            var statusCode = GetEnumValue.GetEnumValueByInt<HttpStatusCode>(Code);

            switch (Code)
            {
                case int n when (n < 400):
                    return new ServiceResponse(statusCode, message, description, null);

                case int n when (n > 400):
                    return new ServiceResponse(statusCode, message, description, null);

                default:
                    break;
            }

            return null;
        }

        public static ServiceResponse GetServiceResponse(int Code, int errorCode)
        {
            var statusCode = GetEnumValue.GetEnumValueByInt<HttpStatusCode>(Code);
            var message = GetEnumValue.GetMessage<HttpStatusCode>(statusCode);
            var description = GetEnumValue.GetDescription<HttpStatusCode>(statusCode);
            var errorStatusCode = GetEnumValue.GetEnumValueByInt<ErrorStatusCode>(errorCode);
            var errorMessage = GetEnumValue.GetMessage<ErrorStatusCode>(errorStatusCode);

            return new ServiceResponse(statusCode, message, description,
                ErrorResponseHandler.GetErrorResponse(errorCode)
            );
        }



    }
}
