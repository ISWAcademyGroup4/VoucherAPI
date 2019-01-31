using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class GiftResponse : BaseVoucherEntity
    {
        public override string voucherCode { get => base.voucherCode; set => base.voucherCode = value; }
        public override string campaignName { get => base.campaignName; set => base.campaignName = value; }
        public override string type { get => base.type; set => base.type = value; }
        public virtual long Amount { get; set; }
        public virtual long Balance { get; set; }
        public override int RedemptionCount { get => base.RedemptionCount; set => base.RedemptionCount = value; }
        public override int RedeemedCount { get => base.RedeemedCount; set => base.RedeemedCount = value; }
        public override bool redemptionStatus { get => base.redemptionStatus; set => base.redemptionStatus = value; }
        public override decimal RedeemedAmount { get => base.RedeemedAmount; set => base.RedeemedAmount = value; }
        public override DateTime StartDate { get => base.StartDate; set => base.StartDate = value; }
        public override DateTime expiryDate { get => base.expiryDate; set => base.expiryDate = value; }
        public override bool status { get => base.status; set => base.status = value; }
        public override DateTime dateCreated { get => base.dateCreated; set => base.dateCreated = value; }

        public GiftResponse(string code, string campaign, string voucherType, long amount, long balance, int redemptionCount, int redeemedCount, bool isRedeemed, decimal redeemedAmount, DateTime startDate, DateTime expirationDate, bool active, DateTime creationDate)
        {
            voucherCode = code ?? throw new ArgumentNullException(nameof(code));
            campaignName = campaign ?? throw new ArgumentNullException(nameof(campaign));
            type = voucherType ?? throw new ArgumentNullException(nameof(voucherType));
            Amount = amount;
            Balance = balance;
            RedemptionCount = redemptionCount;
            RedeemedCount = redeemedCount;
            redemptionStatus = isRedeemed;
            RedeemedAmount = redeemedAmount;
            StartDate = startDate;
            expiryDate = expirationDate;
            status = active;
            dateCreated = creationDate;
        }
    }
}
