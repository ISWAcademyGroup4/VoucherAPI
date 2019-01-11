using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Models
{
    public class Metadata
    {
        public int Length { get; set; }
        public CharacterSet CharSet { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
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
