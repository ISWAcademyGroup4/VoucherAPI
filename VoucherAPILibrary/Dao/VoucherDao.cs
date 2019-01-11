using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Services;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Dao
{
    public class VoucherDao : DbConfigService
    {
        public VoucherDao(IConfiguration configuration) : base(configuration)
        {
        }

        public static async Task<CreateVoucherResponse> CreateVoucher(Voucher voucher)
        {
            //Generate Random Voucher ID's & Voucher Codes
            List<string> voucherIdList = new List<string>();
            List<string> voucherCodeList = new List<string>(); 
            for(int i = 0; i < voucher.VoucherCount; i++)
            {
                voucherIdList.Add(IdGenerator.RandomGen(15));
                voucherCodeList.Add(CodeGenerator.GetGeneratedCode(voucher.Metadata));
            }

            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("VoucherType", voucher.VoucherType);
                    parameters.Add("DiscountType", voucher.Discount.DiscountType);
                    parameters.Add("PercentOff", voucher.Discount.PercentOff);
                    parameters.Add("AmountLimit", voucher.Discount.AmountLimit);
                    parameters.Add("AmountOff", voucher.Discount.AmountOff);
                    parameters.Add("UnitOff", voucher.Discount.UnitOff);
                    parameters.Add("GiftAmount", voucher.Gift.Amount);
                    parameters.Add("GiftBalance", voucher.Gift.Balance);
                    parameters.Add("ValueType", voucher.Value.ValueType);
                    parameters.Add("VirtualPin", voucher.Value.VirtualPin);
                    parameters.Add("StartDate", voucher.StartDate);
                    parameters.Add("ExpirationDate", voucher.ExpirationDate);
                    parameters.Add("RedemptionCount", voucher.Redemption.RedemptionCount);
                    parameters.Add("RedeemedCount", voucher.Redemption.RedeemedCount);
                    parameters.Add("RedeemedAmount", voucher.Redemption.RedeemedAmount);
                    parameters.Add("Length", voucher.Metadata.Length);
                    parameters.Add("Charset", voucher.Metadata.CharSet);
                    parameters.Add("Prefix", voucher.Metadata.Prefix);
                    parameters.Add("Suffix", voucher.Metadata.Suffix);
                    parameters.Add("Pattern", voucher.Metadata.Pattern);
                    parameters.Add("CreatedBy", voucher.CreatedBy);
                    parameters.Add("CreationDate", voucher.CreationDate);

                    for (int i = 0; i < voucher.VoucherCount; i++)
                    {
                        parameters.Add("VoucherId", voucherIdList.ElementAt<string>(i));
                        parameters.Add("Code", voucherCodeList.ElementAt<string>(i));
                        await conn.ExecuteAsync("CreateVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    }
                     
                    return new CreateVoucherResponse(voucherCodeList,voucher.VoucherType.ToString(),new ServiceResponse("200","Successful",null));
                }
            }
            catch (Exception ex)
            {
                return new CreateVoucherResponse(new ServiceResponse("500", "Something went wrong", new List<Error>
                {
                    new Error(ex.GetHashCode().ToString(),ex.Message)
                }));
            }
        }

        public static async Task<GetVoucherResponse> GetVoucher(string code,string username)
        {
            GetVoucherResponse getVoucherResponse = null;
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("code",code);
                    parameters.Add("username", username);
                    System.Data.IDataReader reader = await conn.ExecuteReaderAsync("GetVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    while (reader.Read())
                    {

                        switch ((VoucherType)Enum.Parse(typeof(VoucherType),reader["VoucherType"].ToString()))
                        {
                            case VoucherType.DiscountVoucher:
                                return getVoucherResponse = new GetVoucherResponse(reader["Code"].ToString(), VoucherType.DiscountVoucher.ToString(), new Discount((DiscountType)Enum.Parse(typeof(DiscountType),reader["DiscountType"].ToString()),Convert.ToInt32(reader["PercentOff"]), Convert.ToInt32(reader["AmountLimit"]), Convert.ToInt32(reader["AmountOff"]), reader["UnitOff"].ToString()), Convert.ToDateTime(reader["StartDate"].ToString()), Convert.ToDateTime(reader["ExpirationDate"].ToString()), new Redemption(null, Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), Convert.ToInt32(reader["RedeemedAmount"])), new Metadata(Convert.ToInt32(reader["Length"]), (CharacterSet)Enum.Parse(typeof(CharacterSet), reader["Charset"].ToString()), reader["Prefix"].ToString(), reader["Suffix"].ToString(), reader["Pattern"].ToString()), Convert.ToDateTime(reader["CreationDate"].ToString()), (bool)Enum.Parse(typeof(bool), reader["Active"].ToString()), new ServiceResponse("200", "Successful", null));
                            case VoucherType.GiftVoucher:
                                return getVoucherResponse = new GetVoucherResponse(reader["Code"].ToString(), VoucherType.GiftVoucher.ToString(), new Gift(Convert.ToInt32(reader["GiftAmount"]),Convert.ToInt32(reader["GiftBalance"])), Convert.ToDateTime(reader["StartDate"].ToString()), Convert.ToDateTime(reader["ExpirationDate"].ToString()), new Redemption(null, Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), Convert.ToInt32(reader["RedeemedAmount"])), new Metadata(Convert.ToInt32(reader["Length"]), (CharacterSet)Enum.Parse(typeof(CharacterSet), reader["Charset"].ToString()), reader["Prefix"].ToString(), reader["Suffix"].ToString(), reader["Pattern"].ToString()), Convert.ToDateTime(reader["CreationDate"].ToString()), (bool)Enum.Parse(typeof(bool), reader["Active"].ToString()), new ServiceResponse("200", "Successful", null));
                            case VoucherType.ValueVoucher:
                                return getVoucherResponse = new GetVoucherResponse(reader["Code"].ToString(), VoucherType.ValueVoucher.ToString(), new Value(Convert.ToInt64(reader["VirtualPin"]), (Value_Type)Enum.Parse(typeof(Value_Type), reader["ValueType"].ToString())), Convert.ToDateTime(reader["StartDate"].ToString()), Convert.ToDateTime(reader["ExpirationDate"].ToString()), new Redemption(null, Convert.ToInt32(reader["RedemptionCount"]), Convert.ToInt32(reader["RedeemedCount"]), Convert.ToInt32(reader["RedeemedAmount"])), new Metadata(Convert.ToInt32(reader["Length"]), (CharacterSet)Enum.Parse(typeof(CharacterSet), reader["Charset"].ToString()), reader["Prefix"].ToString(), reader["Suffix"].ToString(), reader["Pattern"].ToString()), Convert.ToDateTime(reader["CreationDate"].ToString()),(bool)Enum.Parse(typeof(bool),reader["Active"].ToString()), new ServiceResponse("200","Successful",null));
                        }
                    }
                    return getVoucherResponse;
                }
            }
            catch (Exception ex)
            {
                return new GetVoucherResponse(new ServiceResponse("500", "Something went wrong", new List<Error>
                {
                    new Error(ex.GetHashCode().ToString(),ex.Message)
                }));
            }
        }

    }
}
