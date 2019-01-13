using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using VoucherAPILibrary.Dao;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Utils;
using VoucherAPILibrary.Models;

namespace VoucherAPILibrary.Services
{
    public class VoucherService : DbConfigService, IVoucherService
    {
        public VoucherService(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<CreateVoucherResponse> CreateVoucher(Voucher voucher)
        {
            return Task.Run(async()=> {
                //Generate Random Voucher ID's & Voucher Codes
                List<string> voucherIdList = new List<string>();
                List<string> voucherCodeList = new List<string>();
                for (int i = 0; i < voucher.VoucherCount; i++)
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

                        return new CreateVoucherResponse(voucherCodeList, voucher.VoucherType.ToString(), new ServiceResponse("200", "Successful", null));
                    }
                }
                catch (Exception ex)
                {
                    return new CreateVoucherResponse(new ServiceResponse("500", "Something went wrong", new List<Error>
                    {
                        new Error(ex.GetHashCode().ToString(),ex.Message)
                    }));
                }
            });
        }
        public Task<GetVoucherResponse> GetVoucher(string voucherCode, string MerchantId)
        {
            return Task.Run(async () =>
            {
                GetVoucherResponse getVoucherResponse = null;
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("code", voucherCode);
                        parameters.Add("username", MerchantId);
                        System.Data.IDataReader reader = await conn.ExecuteReaderAsync("GetVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        while (reader.Read())
                        {
                            getVoucherResponse = new GetVoucherResponse(
                                reader["Code"].ToString(), 
                                GetEnumValue.GetEnumValueByString<VoucherType>(reader["VoucherType"].ToString()), 
                                new Discount(
                                    GetEnumValue.GetEnumValueByString<DiscountType>(reader["DiscountType"].ToString()), 
                                    Convert.ToInt32(reader["PercentOff"]), 
                                    Convert.ToInt32(reader["AmountLimit"]), 
                                    Convert.ToInt32(reader["AmountOff"]), 
                                    reader["UnitOff"].ToString()), 
                                new Gift(
                                    Convert.ToInt32(reader["GiftAmount"]), 
                                    Convert.ToInt32(reader["GiftBalance"])), 
                                new Value(
                                    Convert.ToInt64(reader["VirtualPin"]), 
                                    GetEnumValue.GetEnumValueByString<Value_Type>(reader["ValueType"].ToString())), 
                                Convert.ToDateTime(reader["StartDate"].ToString()), 
                                Convert.ToDateTime(reader["ExpirationDate"].ToString()), 
                                new Redemption(
                                    null, 
                                    Convert.ToInt32(reader["RedemptionCount"]), 
                                    Convert.ToInt32(reader["RedeemedCount"]), 
                                    Convert.ToInt32(reader["RedeemedAmount"])), 
                                new Metadata(
                                    Convert.ToInt32(reader["Length"]), 
                                    GetEnumValue.GetEnumValueByString<CharacterSet>(reader["Charset"].ToString()), 
                                    reader["Prefix"].ToString(), 
                                    reader["Suffix"].ToString(), 
                                    reader["Pattern"].ToString()), 
                                    Convert.ToDateTime(reader["CreationDate"].ToString()), 
                                    Convert.ToBoolean(reader["Active"]), 
                                new ServiceResponse("200", "Successfull", null));
                            Console.WriteLine(getVoucherResponse);

                            switch (getVoucherResponse.VoucherType)
                            {
                                case VoucherType.DiscountVoucher:
                                    getVoucherResponse.Gift = null;
                                    getVoucherResponse.Value = null;
                                    return getVoucherResponse;
                                case VoucherType.GiftVoucher:
                                    getVoucherResponse.Discount = null;
                                    getVoucherResponse.Value = null;
                                    return getVoucherResponse;
                                case VoucherType.ValueVoucher:
                                    getVoucherResponse.Discount = null;
                                    getVoucherResponse.Gift = null;
                                    return getVoucherResponse;
                                    
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
            });
        }
        public Task<UpdateVoucherResponse> UpdateVoucher(string voucherCode, Voucher voucher)
        {
            
        }
        public Task<DeleteVoucherResponse> DeleteVoucher(string voucherCode, string MerchantId)
        {
            return Task.Run(async () =>
{
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("voucherCode", voucherCode);
                        parameters.Add("MerchantId", MerchantId);
                        parameters.Add("DeletionDate", new DateTime());

                        await conn.ExecuteAsync("DeleteVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    }

                    return new DeleteVoucherResponse(new ServiceResponse("200","Voucher was Successfully Deleted",null));
                }
                catch (Exception ex)
                {
                    return new DeleteVoucherResponse(new ServiceResponse("404", "Something went wrong", new List<Error> { new Error(ex.GetHashCode().ToString(), ex.Message) }));
                }
            });
            
        }
    }
}
