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
using System.Data;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace VoucherAPILibrary.Services
{
    public class VoucherService : DbConfigService, IVoucherService<object>
    {
        private readonly ILogger<VoucherService> _logger;

        public VoucherService(IConfiguration configuration, ILogger<VoucherService> logger) : base(configuration)
        {
            _logger = logger;
        }

        public Task<object> Create(Voucher voucher)
        {
            return Task.Run(() =>
            {
                try
                {
                    var batchno = IdGenerator.RandomGen(6);

                    List<string> voucherIdList = new List<string>();
                    List<string> voucherCodeList = new List<string>();

                    for (int i = 0; i < voucher.VoucherCount; i++)
                    {
                        voucherIdList.Add(IdGenerator.RandomGen(10));
                        voucherCodeList.Add(CodeGenerator.GetGeneratedCode(voucher.Metadata));
                    }

                    Task.Run(async () =>
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
                            parameters.Add("BatchNo", batchno);
                            parameters.Add("VoucherCount", voucher.VoucherCount);

                            for (int i = 0; i < voucher.VoucherCount; i++)
                            {
                                parameters.Add("VoucherId", voucherIdList.ElementAt(i));
                                parameters.Add("Code", voucherCodeList.ElementAt(i));

                                try
                                {
                                    int count = await conn.ExecuteAsync("CreateVoucherProcedure", parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                                    switch (count)
                                    {
                                        case 0:
                                            _logger.LogInformation("Request was not saved into db", new object[] { voucher });
                                            throw new Exception("Request was not saved into db");
                                        case 1:
                                        case 2:
                                        case 3:
                                            _logger.LogInformation("Request was not completely saved into db", new object[] { voucher });
                                            break;
                                        case 4:
                                        case 5:
                                        case 6:
                                            _logger.LogInformation("" + voucherCodeList.ElementAt<string>(i) + " was created successfully");
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    _logger.LogError(ex, "Oops, an exception occurred");
                                    
                                }
                            }
                        }
                    });


                    return new CreateResponse("Your request was successfully received and vouchers are being created",voucher.Campaign,voucher.VoucherType.ToString(),voucher.VoucherCount, batchno, HttpResponseHandler.GetServiceResponse(202)) as object;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> Get(string voucherCode, string MerchantId)
        {   
            return await Task.Run( async() =>
            {
                int count = 0;
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", voucherCode);
                        parameters.Add("MerchantId", MerchantId);

                        IDataReader reader = await conn.ExecuteReaderAsync("findbycode", parameters, commandType: System.Data.CommandType.StoredProcedure);

                        while (reader.Read())
                        {
                            count++;
                        }

                        if (count < 1)  
                            return new GetResponse("This voucher has been deleted or does not exist", null, HttpResponseHandler.GetServiceResponse(204));
                        else
                            return new GetResponse("Your request was processed successfully", Utils.GetVoucherHandler.GetResponse(reader), HttpResponseHandler.GetServiceResponse(200)) as object;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new GetResponse("Your request was processed successfully", null, HttpResponseHandler.GetServiceResponse(500)) as object;
                }
            });
        }

        public async Task<object> Update(string voucherCode, DateTime ExpirationDate)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("ExpirationDate", ExpirationDate);

                        var task = Task.Factory.StartNew(async () => {
                            int rowCount = await conn.ExecuteAsync("UpdateVoucherProcedure", parameters, commandType: CommandType.StoredProcedure);
                        
                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, we couldn't update your voucher");
                                case 1:
                                    _logger.LogInformation("" + voucherCode + " was updated with " + ExpirationDate.ToString() + " successfully");
                                    break;
                                default:
                                    _logger.LogInformation("Couldn't log update information for " + voucherCode + "");
                                    break;
                            }
                        });

                        Task.WaitAll(task);
                        return new UpdateVoucherResponse("Your Voucher was updated Successfully", HttpResponseHandler.GetServiceResponse(202)) as object;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new UpdateVoucherResponse(ex.Message, HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public async Task<object> Delete(string voucherCode)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("DeletionDate", DateTime.Today);

                        var task = Task.Factory.StartNew(async () => {
                            int rowCount = await conn.ExecuteAsync("DeleteVoucherProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);

                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, we couldn't delete your voucher");
                                case 1:
                                    _logger.LogInformation("" + voucherCode + " was deleted successfully");
                                    break;
                                default:
                                    _logger.LogInformation("Couldn't log delete information for " + voucherCode + "");
                                    break;
                            }
                        });

                        Task.WaitAll(task);
                    }

                    return new DeleteResponse("Your voucher was successfully deleted",HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new DeleteResponse(ex.Message,HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public async Task<object> List(string campaign,string MerchantId)
        {          
            return await Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("campaign", campaign);
                        parameters.Add("MerchantId", MerchantId);

                        IDataReader reader = await conn.ExecuteReaderAsync("findByCampaign",parameters,commandType: CommandType.StoredProcedure);

                        //return new ListVoucherResponse("Retrieved successfully",campaign, GetVoucherHandler.GetListResponse(reader), HttpResponseHandler.GetServiceResponse(200)) as object;
                        return GetVoucherHandler.GetListResponse(reader) as object;
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> Enable(string code, string MerchantId)
        {
            return await Task.Run(()=> 
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", code);
                        parameters.Add("MerchantId", MerchantId);

                        var task = Task.Factory.StartNew(async ()=> {
                            int rowCount = await conn.ExecuteAsync("EnableVoucherProcedure", parameters, commandType: CommandType.StoredProcedure);

                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, we couldn't enable your voucher");
                                case 1:
                                    _logger.LogInformation("" + code + " was enable successfully");
                                    break;
                                default:
                                    _logger.LogInformation("Couldn't log enable information for " + code + "");
                                    break;
                            }
                        });

                        Task.WaitAll(task);
                    }
                    return new EnableResponse("Voucher was enabled successfully", HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new EnableResponse("Something went wrong", HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public async Task<object> Disable(string code, string MerchantId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", code);
                        parameters.Add("MerchantId", MerchantId);

                        var task = Task.Factory.StartNew(async () => 
                        {
                            int rowCount = await conn.ExecuteAsync("DisableVoucherProcedure", parameters, commandType: CommandType.StoredProcedure);

                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, we couldn't enable your voucher");
                                case 1:
                                    _logger.LogInformation("" + code + " was enable successfully");
                                    break;
                                default:
                                    _logger.LogInformation("Couldn't log enable information for " + code + "");
                                    break;
                            }
                        });

                        Task.WaitAll(task);
                        
                    }
                    return new DisableResponse(HttpResponseHandler.GetServiceResponse(200))as object;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> AddGiftBalance(string code, string MerchantId, long amount)
        {
            return await Task.Run(() => 
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("code", code);
                        parameters.Add("MerchantId", MerchantId);
                        parameters.Add("amount", amount);

                        var task = Task.Factory.StartNew(async () => {

                            int rowCount = await conn.ExecuteAsync("AddGiftBalanceProcedure", parameters, commandType: CommandType.StoredProcedure);

                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, we couldn't Add your Gift Balance");
                                case 1:
                                    _logger.LogInformation("" + code + " was enable successfully");
                                    break;
                                default:
                                    _logger.LogInformation("Couldn't log enable information for " + code + "");
                                    break;
                            }
                        });

                        return new AddGiftResponse("Your request was processed successfully", HttpResponseHandler.GetServiceResponse(201)) as object;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new AddGiftResponse("Something went wrong", HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public async Task<object> GetBatchCount(string batchno)
        {
            return await Task.Run(async() => {
                try
                {
                    int percentage = 0;
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("BatchNo", batchno);
                        parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                        await conn.ExecuteScalarAsync("getBatchCount", parameters, commandType: CommandType.StoredProcedure);

                        percentage = parameters.Get<int>("ReturnValue");                    
                    }
                    _logger.LogInformation("Batch Count was retrieved successfully");
                    return new BatchResponse(percentage, HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }
    }
}
