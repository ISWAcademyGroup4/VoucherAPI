using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Utils;
using VoucherAPILibrary.Utils.Validation;

namespace VoucherAPILibrary.Models
{
    public class Redemption
    {
        [Required]
        [Int32Validation(ErrorMessage = "Redemption count must be a number")]
        [Range(1, 5, ErrorMessage = "Redemption count can only be between 1 & 5")]
        public int RedemptionCount { get; set; }
        
        public Redemption(int redemptionCount)
        {
            RedemptionCount = redemptionCount;
        }
    }
}
