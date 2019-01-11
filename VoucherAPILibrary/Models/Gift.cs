using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Models
{
    public class Gift
    {
        public int Amount { get; set; }
        public int Balance { get; set; }

        public Gift(int amount, int balance)
        {
            Amount = amount;
            Balance = balance;
        }
    }
}
