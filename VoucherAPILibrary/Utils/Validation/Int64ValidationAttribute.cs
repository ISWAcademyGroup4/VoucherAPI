using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils.Validation
{
    public class Int64ValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.GetType().Equals(typeof(Int64)))
            {
                return ValidationResult.Success;
            }
            else
                return new ValidationResult(ErrorMessage);
        }
    }
}
