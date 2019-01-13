using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Domain
{
    public class BaseEntity<T> where T: struct
    {
        public virtual T Id { get; set; }
        public virtual string VoucherCode { get; set; }
        
    }
}
