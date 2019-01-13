using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class GetVoucherResponse
    {
        public virtual string VoucherCode { get; set; }
        public virtual VoucherType VoucherType { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual Gift Gift { get; set; }
        public virtual Value Value { get; set; }
        public virtual DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Redemption Redemption { get; set; }
        public Metadata Metadata { get; set; }    
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public ServiceResponse ServiceResponse { get; set; }

        public GetVoucherResponse(string voucherCode, VoucherType voucherType, Discount discount, Gift gift, Value value, DateTime startDate, DateTime expirationDate, Redemption redemption, Metadata metadata, DateTime creationDate, bool active, ServiceResponse serviceResponse)
        {
            VoucherCode = voucherCode;
            VoucherType = voucherType;
            Discount = discount;
            Gift = gift;
            Value = value;
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Redemption = redemption;
            Metadata = metadata;
            CreationDate = creationDate;
            Active = active;
            ServiceResponse = serviceResponse;

            //switch (VoucherType)
            //{
            //    case VoucherType.DiscountVoucher:
                    
            //}
        }





        //Gets an Error Response
        public GetVoucherResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse;
        }
    }
}
