using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class ValueResponse : BaseVoucherEntity
    {
        public override string voucherCode { get => base.voucherCode; set => base.voucherCode = value; }
        public override string campaignName { get => base.campaignName; set => base.campaignName = value; }
        public override string type { get => base.type; set => base.type = value; }
        public virtual string ValueType { get; set; }
        public virtual long VirtualPin { get; set; }
        public override int RedemptionCount { get => base.RedemptionCount; set => base.RedemptionCount = value; }
        public override int RedeemedCount { get => base.RedeemedCount; set => base.RedeemedCount = value; }
        public override string redemptionStatus { get => base.redemptionStatus; set => base.redemptionStatus = value; }
        public override decimal RedeemedAmount { get => base.RedeemedAmount; set => base.RedeemedAmount = value; }
        public override DateTime StartDate { get => base.StartDate; set => base.StartDate = value; }
        public override DateTime expiryDate { get => base.expiryDate; set => base.expiryDate = value; }
        public override bool status { get => base.status; set => base.status = value; }
        public override DateTime dateCreated { get => base.dateCreated; set => base.dateCreated = value; }
        
        public ValueResponse(string code, string campaign, string voucherType, string valueType, long virtualpin, int redemptionCount, 
            int redeemedCount, bool isRedeemed, decimal redeemedAmount, DateTime startDate, DateTime expirationDate, bool active, DateTime creationDate)
        {
            voucherCode = code ?? throw new ArgumentNullException(nameof(code));
            campaignName = campaign ?? throw new ArgumentNullException(nameof(campaign));
            type = voucherType ?? throw new ArgumentNullException(nameof(voucherType));
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
            VirtualPin = virtualpin;
            RedemptionCount = redemptionCount;
            RedeemedCount = redeemedCount;

            switch (isRedeemed)
            {
                case true:
                    redemptionStatus = "Redeemed";
                    break;
                case false:
                    redemptionStatus = "Not Redeemed";
                    break;
            }

            RedeemedAmount = redeemedAmount;
            StartDate = startDate;
            expiryDate = expirationDate;
            status = active;
            dateCreated = creationDate;
        }
    }
}
