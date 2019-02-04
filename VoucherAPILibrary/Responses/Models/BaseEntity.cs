using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class BaseEntity
    {
        public virtual string VoucherCode { get; set; }
        public virtual string CampaignName { get; set; }
        public virtual string Type { get; set; }
        public virtual string Value { get; set; }
        public virtual int RedemptionCount { get; set; }
        public virtual int RedeemedCount { get; set; }
        public virtual string RedemptionStatus { get; set; }
        public virtual decimal RedeemedAmount { get; set; }
        public virtual string StartDate { get; set; }
        public virtual string ExpiryDate { get; set; }
        public virtual string Status { get; set; }
        public virtual string DateCreated { get; set; }
        
    }
}
