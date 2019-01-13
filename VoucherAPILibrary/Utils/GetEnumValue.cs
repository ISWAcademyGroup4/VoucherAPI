using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class GetEnumValue
    {
        //get an enum value using a string input
        public static T GetEnumValueByString<T>(string str) where T : struct, IConvertible
        {
            if (!(typeof(T).IsEnum))
            {
                throw new Exception("T must be an Enumeration Type");
            }

            T val = ((T[])Enum.GetValues(typeof(T)))[0];
            
            if (!string.IsNullOrEmpty(str))
            {
                foreach (T enumValue in Enum.GetValues(typeof(T)))
                {
                    if (enumValue.ToString().Equals(str))
                    {
                        val = enumValue;
                        break;
                    }
                }
            }

            return val;
        }

        //get an anum value using a numeric input
        public T GetEnumValueByInt<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }
    }
}
