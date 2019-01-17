using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class BaseVoucherEntity
    {
        public virtual string Code { get; set; }
        public virtual string Campaign { get; set; }
        public virtual string VoucherType { get; set; }
        public virtual int RedemptionCount { get; set; }
        public virtual int RedeemedCount { get; set; }
        public virtual bool isRedeemed { get; set; }
        public virtual decimal RedeemedAmount { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual bool Active { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual ServiceResponse ServiceResponse { get; set; }
        
    }
}
