using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class GetAllDiscountResponse
    {
        public virtual string Message { get; set; }
        public virtual List<object> Vouchers { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }
    }
}
