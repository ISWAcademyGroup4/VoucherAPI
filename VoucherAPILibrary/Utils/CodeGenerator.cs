using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;

namespace VoucherAPILibrary.Utils
{
    public class CodeGenerator
    {
        static Random random = new Random();
        static StringBuilder result;
        
        private static string GenerateAlphaNumeric(int length)
        {
            // Generate a random alphanumeric code
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
        private static string GenerateNumeric(int length)
        {
            // Generate a random number
            string characters = "0123456789";
            result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
        private static string GenerateAlphabetic(int length)
        {
            // Generate a random alphabetic code
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        public static string GetGeneratedCode(Metadata metadata)
        {
            int codeLength = metadata.Length - (metadata.Prefix.Length + metadata.Suffix.Length);
            string code = null;

            switch (metadata.CharSet)
            {
                case CharacterSet.Numeric:
                    return code = metadata.Prefix + GenerateNumeric(codeLength) + metadata.Suffix;
                case CharacterSet.Alphabetic:
                    return code = metadata.Prefix + GenerateAlphabetic(codeLength) + metadata.Suffix;
                case CharacterSet.Alphanumeric:
                    return code = metadata.Prefix + GenerateAlphaNumeric(codeLength) + metadata.Suffix;
            }

            return code;
        }

    }
}
