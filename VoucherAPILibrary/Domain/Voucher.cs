using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Utils;
using VoucherAPILibrary.Utils.Validation;

namespace VoucherAPILibrary.Domain
{
    public class Voucher : BaseEntity<int>
    {
        [Required(ErrorMessage = "You must declare a VoucherType")]
        [EnumDataType(typeof(VoucherType), ErrorMessage = "Must be a defined VoucherType")]
        public virtual VoucherType VoucherType { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text, ErrorMessage = "Campaign name must be in text format")]
        public override string Campaign { get => base.Campaign; set => base.Campaign = value; }

        
        public virtual Discount Discount { get; set; }

        public virtual Gift Gift { get; set; }

        public virtual Value Value { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Start Date")]
        [MyDate(ErrorMessage = "Back date entry not allowed")]
        public virtual DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Expiry Date")]
        [MyDate(ErrorMessage = "Back date entry not allowed")]
        public virtual DateTime ExpirationDate { get; set; }

        public virtual Redemption Redemption { get; set; }  

        public virtual Metadata Metadata { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "Campaign name must be in text format")]
        public virtual string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Created")]
        //[MyDate(ErrorMessage = "Back date entry not allowed")]
        public virtual DateTime CreationDate { get; set; }



        [Required]
        [Range(1, 1000000, ErrorMessage = "Must be within 1 & 1000000")]
        public virtual int VoucherCount { get; set; }

        public Voucher(string voucherCode, VoucherType voucherType, string campaign, Discount discount, Gift gift, Value value, DateTime startDate, DateTime expirationDate, Redemption redemption, Metadata metadata, string createdBy, int voucherCount)
        {
            VoucherCode = voucherCode;
            VoucherType = voucherType;
            Campaign = campaign;
            Discount = discount ?? new Discount(0, 10, 10, 10, "");
            Gift = gift ?? new Gift(10, 10);
            Value = value ?? new Value(10, 0);
            StartDate = startDate;
            ExpirationDate = expirationDate;
            Redemption = redemption ?? new Redemption(0);
            Metadata = metadata;
            CreatedBy = createdBy;
            CreationDate = DateTime.Now;
            VoucherCount = voucherCount;
            
        }

        
    }
}
