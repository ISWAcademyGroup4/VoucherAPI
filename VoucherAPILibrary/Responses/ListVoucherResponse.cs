using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class ListVoucherResponse
    {
        public virtual string Campaign { get; set; }
        public virtual List<object> Vouchers { get; set; }      
        public virtual ServiceResponse ServiceResponse { get; set; }

        public ListVoucherResponse(string campaign, List<object> vouchers, ServiceResponse serviceResponse)
        {
            Campaign = campaign ?? throw new ArgumentNullException(nameof(campaign));
            Vouchers = vouchers ?? throw new ArgumentNullException(nameof(vouchers));
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
