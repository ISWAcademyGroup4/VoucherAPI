using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Utils
{
    public class GetVoucherHandler
    {
        public static object GetResponse(IDataReader reader)
        {
            object obj = null;

            while (reader.Read())
            {
                var type = (VoucherType)Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString());

                switch (type)
                {
                    case VoucherType.Discount:

                        switch ((DiscountType)Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()))
                        {
                            case DiscountType.amount:
                                obj = GetDiscountAmount(reader);
                                break;

                            case DiscountType.percentage:
                                obj = GetDiscountPercent(reader);
                                break;

                            case DiscountType.unit:
                                obj = GetDiscountUnit(reader);
                                break;

                            default:
                                break;
                        }

                        break;

                    case VoucherType.Gift:
                        obj = GetGift(reader);
                        break;

                    case VoucherType.Value:
                        obj = GetValue(reader);
                        break;

                    default:
                        break;
                }

            }

            return obj;
        }
        public static List<object> GetListResponse(IDataReader reader)
        {
            object obj = null;
            List<object> list = new List<object>();

            while (reader.Read())
            {
                var type = (VoucherType)Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString());
                switch (type)
                {
                    case VoucherType.Discount:

                        var discountype = (DiscountType)Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString());

                        switch (discountype)
                        {
                            case DiscountType.amount:
                                obj = GetDiscountAmount(reader);
                                break;

                            case DiscountType.percentage:
                                obj = GetDiscountPercent(reader);
                                break;

                            case DiscountType.unit:
                                obj = GetDiscountUnit(reader);
                                break;

                            default:
                                break;
                        }

                        break;

                    case VoucherType.Gift:
                        obj = GetGift(reader);
                        break;

                    case VoucherType.Value:
                        obj = GetValue(reader);
                        break;

                    default:
                        break;
                }

                list.Add(obj);
            }

            return list;
        }

        private static DiscountAmount GetDiscountAmount(IDataReader reader)
        {
            return new DiscountAmount(
                (string)reader["Code"], 
                (string)reader["Campaign"], 
                Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), 
                Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()).ToString(), 
                Convert.ToInt32(reader["AmountOff"]), 
                Convert.ToInt32(reader["RedemptionCount"]), 
                Convert.ToInt32(reader["RedeemedCount"]), 
                (bool)reader["isRedeemed"], 
                (decimal)reader["RedeemedAmount"], 
                (DateTime)reader["StartDate"], 
                (DateTime)reader["ExpirationDate"], 
                (bool)reader["Active"], 
                (DateTime)reader["CreationDate"]);
        }

        private static DiscountPercent GetDiscountPercent(IDataReader reader)
        {
            return new DiscountPercent(
                (string)reader["Code"], 
                (string)reader["Campaign"], 
                Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), 
                Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()).ToString(),
                Convert.ToInt32(reader["PercentOff"]), 
                Convert.ToInt32(reader["AmountLimit"]), 
                Convert.ToInt32(reader["RedemptionCount"]), 
                Convert.ToInt32(reader["RedeemedCount"]), 
                (bool)reader["isRedeemed"], 
                (decimal)reader["RedeemedAmount"], 
                (DateTime)reader["StartDate"], 
                (DateTime)reader["ExpirationDate"], 
                (bool)reader["Active"], 
                (DateTime)reader["CreationDate"]);
        }

        private static DiscountUnit GetDiscountUnit(IDataReader reader)
        {
            return new DiscountUnit(
                (string)reader["Code"], 
                (string)reader["Campaign"], 
                Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), 
                Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()).ToString(), 
                reader["UnitOff"].ToString(), 
                Convert.ToInt32(reader["RedemptionCount"]), 
                Convert.ToInt32(reader["RedeemedCount"]), 
                (bool)reader["isRedeemed"], 
                (decimal)reader["RedeemedAmount"], 
                (DateTime)reader["StartDate"], 
                (DateTime)reader["ExpirationDate"], 
                (bool)reader["Active"], 
                (DateTime)reader["CreationDate"]);
        }

        private static Gift GetGift(IDataReader reader)
        {
            return new Gift(
                (string)reader["Code"], 
                (string)reader["Campaign"], 
                Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), 
                Convert.ToInt64(reader["Amount"]), 
                Convert.ToInt64(reader["Balance"]), 
                Convert.ToInt32(reader["RedemptionCount"]), 
                Convert.ToInt32(reader["RedeemedCount"]), 
                (bool)reader["isRedeemed"], 
                (decimal)reader["RedeemedAmount"], 
                (DateTime)reader["StartDate"], 
                (DateTime)reader["ExpirationDate"], 
                (bool)reader["Active"], 
                (DateTime)reader["CreationDate"]);
        }

        private static Value GetValue(IDataReader reader)
        {
            return new Value(
                (string)reader["Code"], 
                (string)reader["Campaign"], 
                Enum.Parse(typeof(VoucherType), 
                reader["VoucherType"].ToString()).ToString(), 
                reader["ValueSpec"].ToString(),
                (long)reader["Amount"], 
                Convert.ToInt32(reader["RedemptionCount"]), 
                Convert.ToInt32(reader["RedeemedCount"]), 
                (bool)reader["isRedeemed"], 
                (decimal)reader["RedeemedAmount"], 
                (DateTime)reader["StartDate"], 
                (DateTime)reader["ExpirationDate"], 
                (bool)reader["Active"], 
                (DateTime)reader["CreationDate"]);
        }

    }
}
