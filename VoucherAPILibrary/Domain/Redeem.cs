using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Domain
{
    public class Redeem
    {

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Value { get; set; }

        public Redeem(string code, string name, string emailAddress, string value)
        {
            Name = name;
            EmailAddress = emailAddress;
            Value = value;
        }
    }
}
