using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Utils;
using VoucherAPILibrary.Models;
using System.Data.SqlClient;

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
                        parameters.Add("Campaign", voucher.Campaign);
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

                        return new CreateVoucherResponse(voucherCodeList, voucher.VoucherType.ToString(), HttpResponseHandler.GetServiceResponse(201));
                    }
                }
                catch (Exception ex)
                {
                    return new CreateVoucherResponse(HttpResponseHandler.GetServiceResponse(500));
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

                        parameters.Add("Code", voucherCode);
                        parameters.Add("MerchantId", MerchantId);

                        System.Data.IDataReader reader = await conn.ExecuteReaderAsync("GetVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);

                        while (reader.Read())
                        {

                            getVoucherResponse = new GetVoucherResponse(
                                reader["Code"].ToString(),
                                GetEnumValue.GetEnumValueByString<VoucherType>(reader["VoucherType"].ToString()),
                                reader["Campaign"].ToString(),
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
                                Convert.ToDateTime(reader["StartDate"]),
                                Convert.ToDateTime(reader["ExpirationDate"]),
                                new Redemption(
                                    null,
                                    Convert.ToInt32(reader["RedemptionCount"]),
                                    Convert.ToInt32(reader["RedeemedCount"]),
                                    Convert.ToInt32(reader["RedeemedAmount"]),
                                    Convert.ToBoolean(reader["isRedeemed"])),
                                new Metadata(
                                    Convert.ToInt32(reader["Length"]),
                                    GetEnumValue.GetEnumValueByString<CharacterSet>(reader["Charset"].ToString()),
                                    reader["Prefix"].ToString(),
                                    reader["Suffix"].ToString(),
                                    reader["Pattern"].ToString()),
                                    Convert.ToDateTime(reader["CreationDate"].ToString()),
                                    Convert.ToBoolean(reader["Active"]),
                                HttpResponseHandler.GetServiceResponse(200));

                            //Customise Response according to Voucher Type
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

                            reader.Close(); 
                        }                     
                    }
                    return getVoucherResponse;
                }
                catch (Exception ex)
                {

                    return new GetVoucherResponse(HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }
        public Task<UpdateVoucherResponse> UpdateVoucher(string voucherCode, DateTime ExpirationDate)
        {
            return Task.Run(async () =>
            {
                try
                {
                    using(var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();

                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("ExpirationDate", ExpirationDate);

                        await conn.ExecuteAsync("UpdateVoucherProcedure",parameters,commandType: System.Data.CommandType.StoredProcedure);

                        return new UpdateVoucherResponse("Your Voucher was updated Successfully", HttpResponseHandler.GetServiceResponse(202));
                    }
                }
                catch (Exception ex)
                {
                    return new UpdateVoucherResponse("Sorry, we couldn't process your request at this time", HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }
        public Task<DeleteVoucherResponse> DeleteVoucher(string voucherCode)
        {
            
            return Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("DeletionDate", DateTime.Today );

                        await conn.ExecuteAsync("DeleteVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    }

                    return new DeleteVoucherResponse(HttpResponseHandler.GetServiceResponse(200));
                }
                catch (Exception ex)
                {
                    return new DeleteVoucherResponse(HttpResponseHandler.GetServiceResponse(500));
                }
            }); 
        }
        public Task<ListVoucherResponse> ListVoucher(string campaign)
        {
            throw new NotImplementedException();
        }
    }
}
