using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Domain
{
    public class Voucher : BaseEntity<int>
    {
        //public override string Id { get => base.Id; set => base.Id = IdGenerator.RandomGen(15); }
        public override string VoucherCode { get => base.VoucherCode; set => base.VoucherCode = value; }
        public virtual VoucherType VoucherType { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual Gift Gift { get; set; }
        public virtual Value Value { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual bool Active { get; set; }
        public virtual Redemption Redemption { get; set; }  
        public virtual Metadata Metadata { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual int VoucherCount { get; set; }
        
    }
}
