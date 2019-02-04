using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class Value : BaseEntity
    {
        public override string VoucherCode { get => base.VoucherCode; set => base.VoucherCode = value; }

        public override string CampaignName { get => base.CampaignName; set => base.CampaignName = value; }

        public override string Type { get => base.Type; set => base.Type = value; }

        public virtual string ValueType { get; set; }

        public virtual long VirtualPin { get; set; }

        public override int RedemptionCount { get => base.RedemptionCount; set => base.RedemptionCount = value; }

        public override int RedeemedCount { get => base.RedeemedCount; set => base.RedeemedCount = value; }

        public override string RedemptionStatus { get => base.RedemptionStatus; set => base.RedemptionStatus = value; }

        public override decimal RedeemedAmount { get => base.RedeemedAmount; set => base.RedeemedAmount = value; }

        public override string StartDate { get => base.StartDate; set => base.StartDate = value; }

        public override string ExpiryDate { get => base.ExpiryDate; set => base.ExpiryDate = value; }

        public override string Status { get => base.Status; set => base.Status = value; }

        public override string DateCreated { get => base.DateCreated; set => base.DateCreated = value; }
        
        public Value(
            string code, 
            string campaign, 
            string voucherType, 
            string valueType, 
            long virtualpin, 
            int redemptionCount, 
            int redeemedCount, 
            bool isRedeemed, 
            decimal redeemedAmount, 
            DateTime startDate, 
            DateTime expirationDate, 
            bool active, 
            DateTime creationDate)
        {
            VoucherCode = code ?? throw new ArgumentNullException(nameof(code));
            CampaignName = campaign ?? throw new ArgumentNullException(nameof(campaign));
            Type = voucherType ?? throw new ArgumentNullException(nameof(voucherType));
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
            VirtualPin = virtualpin;
            RedemptionCount = redemptionCount;
            RedeemedCount = redeemedCount;

            switch (isRedeemed)
            {
                case true:
                    RedemptionStatus = "Redeemed";
                    break;
                case false:
                    RedemptionStatus = "Not Redeemed";
                    break;
            }

            RedeemedAmount = redeemedAmount;
            StartDate = String.Format("{0:d/M/yyyy}", startDate);
            ExpiryDate = String.Format("{0:d/M/yyyy}", expirationDate);

            switch (active)
            {
                case true:
                    Status = "Active";
                    break;
                case false:
                    Status = "Inactive";
                    break;
            }

            DateCreated = String.Format("{0:d/M/yyyy}", creationDate);
        }
    }
}
