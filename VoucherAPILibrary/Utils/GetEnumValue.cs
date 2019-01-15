using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public static class GetEnumValue
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
        public static T GetEnumValueByInt<T>(int intValue) where T : struct, IConvertible
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

        public static T GetStatusResponse<T>(int ResponseCode) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration Type");
            }

            foreach (T Code in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(Code).Equals(ResponseCode))
                {
                    return Code;
                }                            
            }

            throw new Exception("Status code is not Registered");
        }

        public static string GetMessage<T>(this T enumValue) where T : struct, IConvertible
        {
            
            if (typeof(T).IsEnum)
            {
                Array values = Enum.GetValues(typeof(T));

                foreach (int value in values)
                {
                    if (value == enumValue.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var valueInfo = typeof(T).GetMember(typeof(T).GetEnumName(value));
                        var message = valueInfo[0]
                            .GetCustomAttributes(typeof(DefaultValueAttribute), false)
                            .FirstOrDefault() as DefaultValueAttribute;

                        return message.Value.ToString();
                    }                
                }
            }

            throw new Exception("Enum Value does not exist");
        }

        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {     
            if (typeof(T).IsEnum)
            {
                Array values = Enum.GetValues(typeof(T));

                foreach (int value in values)
                {
                    if (value == enumValue.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var valueInfo = typeof(T).GetMember(typeof(T).GetEnumName(value));
                        var description = valueInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        return description.Description;
                    }
                }              
            }

            throw new Exception("Enum Value does not exist");
        }


    }
}
