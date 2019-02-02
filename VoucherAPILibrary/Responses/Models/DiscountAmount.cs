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
        public override string voucherCode { get => base.voucherCode; set => base.voucherCode = value; }
        public override string campaignName { get => base.campaignName; set => base.campaignName = value; }
        public override string type { get => base.type; set => base.type = value; }
        public virtual string DiscountType { get; set; }
        public virtual int value { get; set; }
        public override int RedemptionCount { get => base.RedemptionCount; set => base.RedemptionCount = value; }
        public override int RedeemedCount { get => base.RedeemedCount; set => base.RedeemedCount = value; }
        public string redemptionStatus { get; set; }
        public override decimal RedeemedAmount { get => base.RedeemedAmount; set => base.RedeemedAmount = value; }
        public string StartDate { get; set; }
        public string expiryDate { get; set; }
        public string status { get; set; }
        public string dateCreated { get; set; }

        public DiscountAmount(string code, string campaign, string voucherType, string discountType, int amountOff, int redemptionCount, int redeemedCount, bool isRedeemed, decimal redeemedAmount, DateTime startDate, DateTime expirationDate, bool active, DateTime creationDate)
        {
            voucherCode = code ?? throw new ArgumentNullException(nameof(code));
            campaignName = campaign ?? throw new ArgumentNullException(nameof(campaign));
            type = voucherType.ToString();
            DiscountType = discountType;
            value = amountOff;
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
            StartDate = String.Format("{0:d/M/yyyy}", startDate);
            expiryDate = String.Format("{0:d/M/yyyy}", expirationDate);
            
            switch (active)
            {
                case true:
                    status = "Active";
                    break;
                case false:
                    status = "Inactive";
                    break;
            }
        
            dateCreated = String.Format("{0:d/M/yyyy}",creationDate);
        }
    }
}
