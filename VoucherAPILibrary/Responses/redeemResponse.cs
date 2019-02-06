using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class redeemResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public ServiceResponse serviceResponse { get; set; }

        public redeemResponse(string code, ServiceResponse serviceResponse)
        {
            Code = code;
            Message = "Congratulations, your voucher: '" + Code + "' was successfully redeemed";
            this.serviceResponse = serviceResponse;
        }


    }
}
