using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class DiscountUnit : BaseVoucherEntity
    {
        public override string voucherCode { get => base.voucherCode; set => base.voucherCode = value; }
        public override string campaignName { get => base.campaignName; set => base.campaignName = value; }
        public override string type { get => base.type; set => base.type = value; }
        public virtual string DiscountType { get; set; }
        public virtual string UnitOff { get; set; }
        public override int RedemptionCount { get => base.RedemptionCount; set => base.RedemptionCount = value; }
        public override int RedeemedCount { get => base.RedeemedCount; set => base.RedeemedCount = value; }
        public override bool redemptionStatus { get => base.redemptionStatus; set => base.redemptionStatus = value; }
        public override decimal RedeemedAmount { get => base.RedeemedAmount; set => base.RedeemedAmount = value; }
        public override DateTime StartDate { get => base.StartDate; set => base.StartDate = value; }
        public override DateTime expiryDate { get => base.expiryDate; set => base.expiryDate = value; }
        public override bool status { get => base.status; set => base.status = value; }
        public override DateTime dateCreated { get => base.dateCreated; set => base.dateCreated = value; }

        public DiscountUnit(string voucherCode, string campaignName, string type, string discountType, string unitOff, int redemptionCount, int redeemedCount, bool redemptionStatus, decimal redeemedAmount, DateTime startDate, DateTime expiryDate, bool status, DateTime dateCreated)
        {
            voucherCode = voucherCode ?? throw new ArgumentNullException(nameof(voucherCode));
            campaignName = campaignName ?? throw new ArgumentNullException(nameof(campaignName));
            type = type ?? throw new ArgumentNullException(nameof(type));
            DiscountType = discountType ?? throw new ArgumentNullException(nameof(discountType));
            UnitOff = unitOff ?? throw new ArgumentNullException(nameof(unitOff));
            RedemptionCount = redemptionCount;
            RedeemedCount = redeemedCount;
            this.redemptionStatus = redemptionStatus;
            RedeemedAmount = redeemedAmount;
            StartDate = startDate;
            expiryDate = expiryDate;
            status = status;
            dateCreated = dateCreated;
        }
    }
}
