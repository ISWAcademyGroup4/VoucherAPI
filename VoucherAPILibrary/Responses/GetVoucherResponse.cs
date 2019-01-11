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
        public string VoucherCode { get; set; }
        public string VoucherType { get; set; }
        public Discount Discount { get; set; }
        public Gift Gift { get; set; }
        public Value Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Redemption Redemption { get; set; }
        public Metadata Metadata { get; set; }    
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public ServiceResponse ServiceResponse { get; set; }

        //Gets a Discount Voucher
        public GetVoucherResponse(string voucherCode, string voucherType, Discount discount, DateTime startDate, DateTime expirationDate, Redemption redemption, Metadata metadata, DateTime creationDate, bool active, ServiceResponse serviceResponse)
        {
            VoucherCode = voucherCode;
            VoucherType = voucherType;
            Discount = discount;
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Redemption = redemption;
            Metadata = metadata;
            CreationDate = creationDate;
            Active = active;
            ServiceResponse = serviceResponse;
        }

        //Gets a gift voucher
        public GetVoucherResponse(string voucherCode, string voucherType, Gift gift, DateTime startDate, DateTime expirationDate, Redemption redemption, Metadata metadata, DateTime creationDate, bool active, ServiceResponse serviceResponse)
        {
            VoucherCode = voucherCode;
            VoucherType = voucherType;
            Gift = gift;
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Redemption = redemption;
            Metadata = metadata;
            CreationDate = creationDate;
            Active = active;
            ServiceResponse = serviceResponse;
        }

        //Gets a Value Voucher
        public GetVoucherResponse(string voucherCode, string voucherType, Value value, DateTime startDate, DateTime expirationDate, Redemption redemption, Metadata metadata, DateTime creationDate, bool active, ServiceResponse serviceResponse)
        {
            VoucherCode = voucherCode;
            VoucherType = voucherType;
            Value = value;
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Redemption = redemption;
            Metadata = metadata;
            CreationDate = creationDate;
            Active = active;
            ServiceResponse = serviceResponse;
        }

        //Gets an Error Response
        public GetVoucherResponse(ServiceResponse serviceResponse)
        {
            ServiceResponse = serviceResponse;
        }
    }
}
