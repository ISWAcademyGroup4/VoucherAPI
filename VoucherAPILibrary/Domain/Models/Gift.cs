using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils.Validation;

namespace VoucherAPILibrary.Models
{
    public class Gift
    {
        [Int32Validation(ErrorMessage = "Must be a number")]
        [Range(10, 100000,ErrorMessage = "Gift amount must be within 10 & 100000")]
        public int Amount { get; set; }

        [Int32Validation(ErrorMessage = "Must be a number")]
        [Range(10, 100000, ErrorMessage = "Gift balance must be within 10 & 100000")]
        public int Balance { get; set; }

        public Gift(int amount, int balance)
        {
            Amount = amount;
            Balance = balance;
        }
    }
}
