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
    public class Metadata
    {
        [Required]
        [Int32Validation]
        [Range(5,20,ErrorMessage="Length can only be between 5 & 20")]
        public int Length { get; set; }

        [Required]
        [EnumDataType(typeof(CharacterSet),ErrorMessage = "Character Set must be a system defined character set")]
        public CharacterSet CharSet { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "Prefix must be in text format")]
        [StringLength(6,ErrorMessage = "Prefix cannot be more than six (6) characters")]
        public string Prefix { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "Suffix must be in text format")]
        [StringLength(6, ErrorMessage = "Suffix cannot be more than six (6) characters")]
        public string Suffix { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "Prefix must be in text format")]
        [MaxLength(20)]
        public string Pattern { get; set; }

        public Metadata(int length, CharacterSet charSet, string prefix, string suffix, string pattern)
        {
            Length = length;
            CharSet = charSet;
            Prefix = prefix;
            Suffix = suffix;
            Pattern = pattern;
        }
    }
}
