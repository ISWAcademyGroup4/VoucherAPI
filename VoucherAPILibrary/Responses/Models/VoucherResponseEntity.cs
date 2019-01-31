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
        public virtual string voucherCode { get; set; }
        public virtual string campaignName { get; set; }
        public virtual string type { get; set; }
        public virtual int RedemptionCount { get; set; }
        public virtual int RedeemedCount { get; set; }
        public virtual bool redemptionStatus { get; set; }
        public virtual decimal RedeemedAmount { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime expiryDate { get; set; }
        public virtual bool status { get; set; }
        public virtual DateTime dateCreated { get; set; }
        
    }
}
