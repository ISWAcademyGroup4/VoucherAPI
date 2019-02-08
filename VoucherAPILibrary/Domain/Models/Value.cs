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
    public class Value
    {
        [Int64Validation(ErrorMessage = "input value must be a number")]
        public long Amount { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Value voucher specification must be of Text Format")]
        public string ValueSpec {get;set;}

        public Value(long amount, string valueSpec)
        {
            Amount = amount;
            ValueSpec = valueSpec;
        }
    }
}
