using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;
using VoucherAPILibrary.Utils.Validation;

namespace VoucherAPILibrary.Models
{
    public class Discount
    {
        
        [EnumDataType(typeof(DiscountType),ErrorMessage = "Must be a defined Discount Type")]
        public DiscountType DiscountType { get; set; }

        [Int32Validation]
        [Range(1, 100, ErrorMessage = "Percent Off must be defined as a pecentage")]
        public int PercentOff { get; set; }

        [Int32Validation]
        [Range(1, 100000, ErrorMessage = "Amount Limit cannot be more than 100000")]
        public int AmountLimit { get; set; }

        [Int32Validation]
        [Range(1, 100000, ErrorMessage = "Amount off cannot be more than 100000")]
        public int AmountOff { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Text, ErrorMessage = "Unit Off must be in text format" )]
        public string UnitOff { get; set; }

        public Discount(DiscountType discountType, int percentOff, int amountLimit, int amountOff, string unitOff)
        {
            DiscountType = discountType;
            PercentOff = percentOff;
            AmountLimit = amountLimit;
            AmountOff = amountOff;
            UnitOff = unitOff;
        }
    }
}
