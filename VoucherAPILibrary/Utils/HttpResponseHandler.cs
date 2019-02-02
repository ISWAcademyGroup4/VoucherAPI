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
       
        public static ServiceResponse GetServiceResponse(int ResponseCode)
        {
            var statusCode = GetEnumValue.GetStatusResponse<HttpStatusCode>(ResponseCode);
            var message = GetEnumValue.GetMessage<HttpStatusCode>(statusCode);
            var description = GetEnumValue.GetDescription<HttpStatusCode>(statusCode);

            switch (ResponseCode)
            {
                case int n when (n < 400):
                    return new ServiceResponse(statusCode, message, description, null );
                case int n when (n >= 400):
                    return new ServiceResponse(statusCode, message, description, null);
                //default:
                //    return new ServiceResponse(statusCode, message, description, null);
            }

            return null;
           
        }

    }
}
