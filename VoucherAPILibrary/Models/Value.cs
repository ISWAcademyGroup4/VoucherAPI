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
        public long VirtualPin { get; set; }

        [EnumDataType(typeof(Value_Type),ErrorMessage = "Must be a defned value type voucher")]
        public Value_Type ValueType {get;set;}

        public Value(long virtualPin, Value_Type valueType)
        {
            VirtualPin = virtualPin;
            ValueType = valueType;
        }
    }
}
