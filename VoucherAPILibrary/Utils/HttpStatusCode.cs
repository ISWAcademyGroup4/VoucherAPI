using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public enum HttpStatusCode
    {
        [Description("Continue")]
        Continue = 100,
        Switching_Protocols = 101
    }
}
