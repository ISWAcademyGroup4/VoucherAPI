using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Domain
{
    public class RedemptionUser : BaseEntity<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public override string VoucherCode { get => base.VoucherCode; set => base.VoucherCode = value; }
    }
}
