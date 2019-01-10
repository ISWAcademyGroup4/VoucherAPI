using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Models
{
    public class Discount
    {
        public DiscountType DiscountType { get; set; }

        //Properties of a Percentage Discount Voucher
        public int PercentOff { get; set; }
        public int AmountLimit { get; set; }

        //Properties of an Amount Discunt Voucher
        public int AmountOff { get; set; }

        //Properties of an Unit Voucher
        public string UnitOff { get; set; }
    }
}
