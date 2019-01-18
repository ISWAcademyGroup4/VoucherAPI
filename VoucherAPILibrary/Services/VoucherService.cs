﻿using System;
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
using System.Data;
using System.Threading;

namespace VoucherAPILibrary.Services
{
    public class VoucherService : DbConfigService, IVoucherService
    {
        public VoucherService(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<object> CreateVoucher(Voucher voucher)
        {
            
            return Task.Run(() =>
            {
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


                    Task.Run(async () =>
                    {
                        using (var conn = Connection)
                        {

                            for (int i = 0; i < voucher.VoucherCount; i++)
                            {
                                parameters.Add("VoucherId", voucherIdList.ElementAt<string>(i));
                                parameters.Add("Code", voucherCodeList.ElementAt<string>(i));

                                
                                int count = await conn.ExecuteAsync("CreateVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                                Console.WriteLine(count);
                               

                            }

                        }
                    });
                  
                    return new CreateVoucherResponse(voucherCodeList, voucher.VoucherType.ToString(), HttpResponseHandler.GetServiceResponse(201)) as object;
                }
                catch (Exception ex)
                {

                    return new CreateVoucherResponse(HttpResponseHandler.GetServiceResponse(500)) as object;
                }



            });
        }

        public Task<object> GetVoucher(string voucherCode, string MerchantId)
        {
            var obj = new Object();
            List<Object> values = null;
            return Task.Run( async() =>
            {        
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();

                        parameters.Add("Code", voucherCode);
                        parameters.Add("MerchantId", MerchantId);

                        IDataReader reader = await conn.ExecuteReaderAsync("GetVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);

                        return GetVoucherResponse.GetResponse(reader);
                    }
                }
                catch (Exception ex)
                {
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public Task<object> UpdateVoucher(string voucherCode, DateTime ExpirationDate)
        {
            return Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();

                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("ExpirationDate", ExpirationDate);

                        await conn.ExecuteAsync("UpdateVoucherProcedure", parameters, commandType: CommandType.StoredProcedure);

                        return new UpdateVoucherResponse("Your Voucher was updated Successfully", HttpResponseHandler.GetServiceResponse(202)) as object;
                    }
                }
                catch (Exception ex)
                {
                    return new UpdateVoucherResponse("Sorry, we couldn't process your request at this time", HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public Task<object> DeleteVoucher(string voucherCode)
        {

            return Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("DeletionDate", DateTime.Today);

                        await conn.ExecuteAsync("DeleteVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    }

                    return new DeleteVoucherResponse(HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch (Exception ex)
                {
                    return new DeleteVoucherResponse(HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public Task<object> ListVouchers(string campaign,string MerchantId)
        {
           
            return Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("campaign", campaign);
                        parameters.Add("MerchantId", MerchantId);

                        IDataReader reader = await conn.ExecuteReaderAsync("ListVouchersProcedure",parameters,commandType: CommandType.StoredProcedure);
                       
                        return new ListVoucherResponse(campaign, GetVoucherResponse.GetListResponse(reader), HttpResponseHandler.GetServiceResponse(200)) as object;
                    }
                }
                catch(Exception ex)
                {
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public Task<object> EnableVoucher(string code,string MerchantId)
        {
            return Task.Run(async()=> 
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", code);
                        parameters.Add("MerchantId", MerchantId);

                        await conn.ExecuteAsync("EnableVoucherProcedure", parameters, commandType: CommandType.StoredProcedure);
                    }
                    return new EnableResponse(HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch 
                {
                    return HttpResponseHandler.GetServiceResponse(500) as object;
                }
            });
        }

        public Task<object> DisableVoucher(string code, string MerchantId)
        {
            return Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", code);
                        parameters.Add("MerchantId", MerchantId);

                        await conn.ExecuteAsync("DisableVoucherProcedure", parameters, commandType: CommandType.StoredProcedure);
                    }
                    return new DisableResponse(HttpResponseHandler.GetServiceResponse(200))as object;

                }
                catch (Exception ex)
                {
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }
    }
}
