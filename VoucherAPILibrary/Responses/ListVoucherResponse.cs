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
        public virtual List<Voucher> Vouchers { get; set; }
       
        public virtual ServiceResponse ServiceResponse { get; set; }


    }
}
