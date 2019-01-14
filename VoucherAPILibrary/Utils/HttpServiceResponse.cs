using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class HttpServiceResponse
    {
        public virtual HttpStatusCode StatusCode { get; set; }
        public virtual DescriptionAttribute Description { get; set; }
    }
}
