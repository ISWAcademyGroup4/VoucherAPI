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
        public override string VoucherCode { get => base.VoucherCode; set => base.VoucherCode = value; }
        public virtual VoucherType VoucherType { get; set; }
        public override string Campaign { get => base.Campaign; set => base.Campaign = value; }
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

        public Voucher(string voucherCode, VoucherType voucherType, string campaign, Discount discount, Gift gift, Value value, DateTime startDate, DateTime expirationDate, bool active, Redemption redemption, Metadata metadata, string createdBy, DateTime creationDate, int voucherCount)
        {
            VoucherCode = voucherCode;
            VoucherType = voucherType;
            Campaign = campaign;
            Discount = discount ?? new Discount(0, 0, 0, 0, "");
            Gift = gift ?? new Gift(0, 0);
            Value = value ?? new Value(0, 0);
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Active = active;
            Redemption = redemption ?? new Redemption(null, 0, 0, 0, false);
            Metadata = metadata ?? new Metadata(0, 0, "", "", "");
            CreatedBy = createdBy;
            CreationDate = creationDate;
            VoucherCount = voucherCount;
        }
    }
}
