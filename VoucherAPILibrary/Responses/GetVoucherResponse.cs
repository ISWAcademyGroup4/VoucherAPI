using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class GetVoucherResponse
    {
        public static object GetResponse(IDataReader reader)
        {
            object obj = null;
            while (reader.Read())
            {

                switch ((VoucherType)Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()))
                {
                    case VoucherType.DiscountVoucher:
                        switch ((DiscountType)Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()))
                        {
                            case DiscountType.amount:
                                obj = new DiscountAmount((string)reader["Code"], (string)reader["Campaign"], Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()).ToString(), Convert.ToInt32(reader["AmountOff"]), Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), (bool)reader["isRedeemed"], (decimal)reader["RedeemedAmount"], (DateTime)reader["StartDate"], (DateTime)reader["ExpirationDate"], (bool)reader["Active"], (DateTime)reader["CreationDate"], HttpResponseHandler.GetServiceResponse(200));
                                break;
                            case DiscountType.percentage:
                                obj = new DiscountPercent((string)reader["Code"], (string)reader["Campaign"], Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()).ToString(), Convert.ToInt32(reader["PercentOff"]),Convert.ToInt32(reader["AmountLimit"]), Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), (bool)reader["isRedeemed"], (decimal)reader["RedeemedAmount"], (DateTime)reader["StartDate"], (DateTime)reader["ExpirationDate"], (bool)reader["Active"], (DateTime)reader["CreationDate"], HttpResponseHandler.GetServiceResponse(200));
                                break;
                            case DiscountType.unit:
                                obj = new DiscountUnit((string)reader["Code"], (string)reader["Campaign"], Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), Enum.Parse(typeof(DiscountType), reader["DiscountType"].ToString()).ToString(),reader["UnitOff"].ToString(), Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), (bool)reader["isRedeemed"], (decimal)reader["RedeemedAmount"], (DateTime)reader["StartDate"], (DateTime)reader["ExpirationDate"], (bool)reader["Active"], (DateTime)reader["CreationDate"], HttpResponseHandler.GetServiceResponse(200));
                                break;
                        }
                        break;
                    case VoucherType.GiftVoucher:
                        obj = new GiftResponse((string)reader["Code"], (string)reader["Campaign"], Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(),Convert.ToInt64(reader["Amount"]), Convert.ToInt64(reader["Balance"]), Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), (bool)reader["isRedeemed"], (decimal)reader["RedeemedAmount"], (DateTime)reader["StartDate"], (DateTime)reader["ExpirationDate"], (bool)reader["Active"], (DateTime)reader["CreationDate"], HttpResponseHandler.GetServiceResponse(200));
                        break;
                    case VoucherType.ValueVoucher:
                        obj = new ValueResponse((string)reader["Code"], (string)reader["Campaign"], Enum.Parse(typeof(VoucherType), reader["VoucherType"].ToString()).ToString(), Enum.Parse(typeof(Value_Type),reader["ValueType"].ToString()).ToString(), Convert.ToInt64(reader["VirtualPin"]), Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), (bool)reader["isRedeemed"], (decimal)reader["RedeemedAmount"], (DateTime)reader["StartDate"], (DateTime)reader["ExpirationDate"], (bool)reader["Active"], (DateTime)reader["CreationDate"], HttpResponseHandler.GetServiceResponse(200));
                        break;
                }
                
            }
            return obj;
        }
    }
}
