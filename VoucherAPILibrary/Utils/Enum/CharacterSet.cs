using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public enum CharacterSet
    {
        [DefaultValue("Numeric")]
        Numeric,

        [DefaultValue("Alphabetic")]
        Alphabetic,

        [DefaultValue("Alpha-Numeric")]
        Alphanumeric
    }
}
