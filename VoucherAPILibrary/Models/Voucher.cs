using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Models
{
    public class Voucher
    {
        public string UserId { get; set; }
        public VoucherType VoucherType { get; set; }
        public Discount Discount { get; set; }
        public Gift Gift { get; set; }
        public Value Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }
        public Redemption Redemption { get; set; }
        public Metadata Metadata { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int VoucherCount { get; set; }

    }
}
