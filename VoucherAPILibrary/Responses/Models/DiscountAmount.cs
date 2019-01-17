using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class DiscountAmount : BaseVoucherEntity
    { 
        public override string Code { get => base.Code; set => base.Code = value; }
        public override string Campaign { get => base.Campaign; set => base.Campaign = value; }
        public override string VoucherType { get => base.VoucherType; set => base.VoucherType = value; }
        public virtual string DiscountType { get; set; }
        public virtual int AmountOff { get; set; }
        public override int RedemptionCount { get => base.RedemptionCount; set => base.RedemptionCount = value; }
        public override int RedeemedCount { get => base.RedeemedCount; set => base.RedeemedCount = value; }
        public override bool isRedeemed { get => base.isRedeemed; set => base.isRedeemed = value; }
        public override decimal RedeemedAmount { get => base.RedeemedAmount; set => base.RedeemedAmount = value; }
        public override DateTime StartDate { get => base.StartDate; set => base.StartDate = value; }
        public override DateTime ExpirationDate { get => base.ExpirationDate; set => base.ExpirationDate = value; }
        public override bool Active { get => base.Active; set => base.Active = value; }
        public override DateTime CreationDate { get => base.CreationDate; set => base.CreationDate = value; }
        public override ServiceResponse ServiceResponse { get => base.ServiceResponse; set => base.ServiceResponse = value; }

        public DiscountAmount(string code, string campaign, string voucherType, string discountType, int amountOff, int redemptionCount, int redeemedCount, bool isRedeemed, decimal redeemedAmount, DateTime startDate, DateTime expirationDate, bool active, DateTime creationDate, ServiceResponse serviceResponse)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Campaign = campaign ?? throw new ArgumentNullException(nameof(campaign));
            VoucherType = voucherType;
            DiscountType = discountType;
            AmountOff = amountOff;
            RedemptionCount = redemptionCount;
            RedeemedCount = redeemedCount;
            this.isRedeemed = isRedeemed;
            RedeemedAmount = redeemedAmount;
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Active = active;
            CreationDate = creationDate;
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
