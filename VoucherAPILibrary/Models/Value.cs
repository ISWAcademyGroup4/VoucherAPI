using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Models
{
    public class Value
    {
        public long VirtualPin { get; set; }
        public Value_Type ValueType {get;set;}

        public Value(long virtualPin, Value_Type valueType)
        {
            VirtualPin = virtualPin;
            ValueType = valueType;
        }
    }
}
