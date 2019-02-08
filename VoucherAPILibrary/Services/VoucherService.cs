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
using VoucherAPILibrary.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace VoucherAPILibrary.Services
{
    public class VoucherService : DbConfigService, IVoucherService<object>
    {
        private readonly ILogger<VoucherService> _logger;
        private MessageBroker _messageBroker;
        

        public VoucherService(IConfiguration configuration, ILogger<VoucherService> logger, MessageBroker messageBroker) : base(configuration)
        {
            _logger = logger;
            _messageBroker = messageBroker;
        }

        public async Task<object> Create(Voucher voucher)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var batchno = IdGenerator.RandomGen(6);

                    List<string> voucherIdList = new List<string>();
                    List<string> voucherCodeList = new List<string>();

                    List<DynamicParameters> parameterList = new List<DynamicParameters>();

                    for (int i = 0; i < voucher.VoucherCount; i++)
                    {
                        voucherIdList.Add(IdGenerator.RandomGen(10));
                        voucherCodeList.Add(CodeGenerator.GetGeneratedCode(voucher.Metadata));

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
                        parameters.Add("ValueSpec", voucher.Value.ValueSpec);
                        parameters.Add("Amount", voucher.Value.Amount);
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
                        parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                        parameters.Add("VoucherId", voucherIdList.ElementAt(i));
                        parameters.Add("Code", voucherCodeList.ElementAt(i));

                        parameterList.Add(parameters);
                    }

                    //DynamicParameters parameters = new DynamicParameters();
                    //parameters.Add("VoucherType", voucher.VoucherType);
                    //parameters.Add("Campaign", voucher.Campaign);
                    //parameters.Add("DiscountType", voucher.Discount.DiscountType);
                    //parameters.Add("PercentOff", voucher.Discount.PercentOff);
                    //parameters.Add("AmountLimit", voucher.Discount.AmountLimit);
                    //parameters.Add("AmountOff", voucher.Discount.AmountOff);
                    //parameters.Add("UnitOff", voucher.Discount.UnitOff);
                    //parameters.Add("GiftAmount", voucher.Gift.Amount);
                    //parameters.Add("GiftBalance", voucher.Gift.Balance);
                    //parameters.Add("ValueSpec", voucher.Value.ValueSpec);
                    //parameters.Add("Amount", voucher.Value.Amount);
                    //parameters.Add("StartDate", voucher.StartDate);
                    //parameters.Add("ExpirationDate", voucher.ExpirationDate);
                    //parameters.Add("RedemptionCount", voucher.Redemption.RedemptionCount);
                    //parameters.Add("Length", voucher.Metadata.Length);
                    //parameters.Add("Charset", voucher.Metadata.CharSet);
                    //parameters.Add("Prefix", voucher.Metadata.Prefix);
                    //parameters.Add("Suffix", voucher.Metadata.Suffix);
                    //parameters.Add("Pattern", voucher.Metadata.Pattern);
                    //parameters.Add("CreatedBy", voucher.CreatedBy);
                    //parameters.Add("CreationDate", voucher.CreationDate);
                    //parameters.Add("BatchNo", batchno);
                    //parameters.Add("VoucherCount", voucher.VoucherCount);
                    //parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    Task.Run(() =>
                    {
                        Parallel.ForEach(parameterList, async (parameter) => 
                        {
                            using (var conn = Connection)
                            {
                                try
                                {
                                    int rowsAffected = await conn.ExecuteAsync("create", parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                                    int ReturnErrorCode = parameter.Get<int>("ReturnValue");

                                    switch (rowsAffected)
                                    {
                                        case 0:
                                            _logger.LogInformation("Request was not saved into db", new object[] { voucher });
                                            throw new Exception("Request was not saved into db");

                                        case int n when n <= 4:
                                            _logger.LogInformation("Request was not completely saved into db", new object[] { voucher });
                                            break;

                                        case int n when n > 4:
                                            Console.WriteLine("{0} was successfully created at {1}", parameter.Get<string>("Code"), DateTime.Now);
                                            _logger.LogInformation("" + parameter.Get<string>("Code") + " was created successfully");
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "Oops, an exception occurred");
                                }
                                finally
                                {
                                    if (conn.State == ConnectionState.Open)
                                        conn.Close();
                                }      
                            }
                        });
                    });
                  
                    Thread messageThread = new Thread(()=>_messageBroker.PublishMessage(new CustomMessage("USER "+voucher.CreatedBy+" created "+voucher.VoucherCount+" vouchers","USER","CREATE",String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                    messageThread.Start();

                    return new CreateResponse("Your request was successfully received and vouchers are being created",voucher.Campaign,voucher.VoucherType.ToString(),voucher.VoucherCount, batchno, HttpResponseHandler.GetServiceResponse(202)) as object;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> Get(string voucherCode, string MerchantId)
        {   
            return await Task.Run( async() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", voucherCode);
                        parameters.Add("MerchantId", MerchantId);
                    
                        IDataReader reader = await conn.ExecuteReaderAsync("findbycode", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        var responseObject = GetVoucherHandler.GetResponse(reader);
                        reader.Close();

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + MerchantId + " requested for VOUCHER: " + voucherCode + "", "USER", "GET", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

                        return new GetResponse("Your request was processed successfully", responseObject , HttpResponseHandler.GetServiceResponse(200)) as object;
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new GetResponse("Your request was processed successfully", null, HttpResponseHandler.GetServiceResponse(500)) as object;
                }
            });
        }

        public async Task<object> Update(string voucherCode, string Merchant, DateTime ExpirationDate)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("Merchant", Merchant);
                        parameters.Add("ExpirationDate", ExpirationDate);

                        

                        var task = Task.Factory.StartNew(async () =>
                        {
                            int rowCount = await conn.ExecuteAsync("update", parameters, commandType: CommandType.StoredProcedure);

                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, couldn't update your voucher: " + voucherCode + "");
                                case 1:
                                    _logger.LogInformation("" + voucherCode + " was updated with " + ExpirationDate.ToString() + " successfully");
                                    break;
                                default:
                                    _logger.LogInformation("Couldn't log update information for " + voucherCode + "");
                                    break;
                            }
                        });

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " updated VOUCHER: " + voucherCode + "", "USER", "UPDATE", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

                        return new UpdateResponse("Your Voucher was updated Successfully", HttpResponseHandler.GetServiceResponse(202)) as object;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new UpdateResponse(ex.Message, HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public async Task<object> Delete(string voucherCode, string Merchant)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("VoucherCode", voucherCode);
                        parameters.Add("Merchant", Merchant);
                        parameters.Add("DeletionDate", DateTime.Now);

                        var task = Task.Factory.StartNew(async () => {

                            int rowCount = await conn.ExecuteAsync("delete", parameters, commandType: System.Data.CommandType.StoredProcedure);

                            switch (rowCount)
                            {
                                case 0:
                                    throw new Exception("Something went wrong, we couldn't delete voucher: "+voucherCode+"");
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

                    Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " deleted VOUCHER: " + voucherCode + "", "USER", "DELETE", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                    messageThread.Start();

                    return new DeleteResponse("Your voucher was successfully deleted",HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new DeleteResponse(ex.Message,HttpResponseHandler.GetServiceResponse(500));
                }
            });
        }

        public async Task<object> List(string campaign, string Merchant)
        {          
            return await Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("campaign", campaign);
                        parameters.Add("MerchantId", Merchant);

                        IDataReader reader = await conn.ExecuteReaderAsync("findByCampaign",parameters,commandType: CommandType.StoredProcedure);
                        var responseObject = GetVoucherHandler.GetListResponse(reader) as object;
                        reader.Close();

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " requested for CAMPAIGN: " +campaign + "", "USER", "LIST CAMPAIGN", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();
                        
                        return responseObject;
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> Enable(string code, string Merchant)
        {
            return await Task.Run(()=> 
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", code);
                        parameters.Add("MerchantId", Merchant);

                        var task = Task.Factory.StartNew(async ()=> {
                            int rowCount = await conn.ExecuteAsync("enable", parameters, commandType: CommandType.StoredProcedure);

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

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " enabled VOUCHER: " + code + "", "USER", "ENABLE", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

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

        public async Task<object> Disable(string code, string Merchant)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Code", code);
                        parameters.Add("MerchantId", Merchant);

                        var task = Task.Factory.StartNew(async () => 
                        {
                            int rowCount = await conn.ExecuteAsync("disable", parameters, commandType: CommandType.StoredProcedure);

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

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " disabled VOUCHER: " + code + "", "USER", "DISABLE", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();
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

        public async Task<object> AddGiftBalance(string code, string Merchant, long amount)
        {
            return await Task.Run(() => 
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("code", code);
                        parameters.Add("MerchantId", Merchant);
                        parameters.Add("amount", amount);

                        var task = Task.Factory.StartNew(async () => {

                            int rowCount = await conn.ExecuteAsync("addGiftBalance", parameters, commandType: CommandType.StoredProcedure);

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

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " added gift balance with VOUCHER: " + code + "", "USER", "ADD GIFT BALANCE", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

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

                    Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("SERVER retrieved vouchers with BATCH NO: " + batchno + "", "SERVER", "GET", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                    messageThread.Start();

                    return new BatchResponse(percentage, HttpResponseHandler.GetServiceResponse(200)) as object;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> GetAll(string Merchant)
        {
            return await Task.Run( async ()=> 
            {
                try
                {
                    var responseObject = new List<object>();

                    var discountResponse = (List<object>) await GetAllDiscount(Merchant);
                    foreach (object obj in discountResponse)
                    {
                        responseObject.Add(obj);
                    }

                    var giftResponse = (List<object>) await GetAllGift(Merchant);
                    foreach (object obj in giftResponse)
                    {
                        responseObject.Add(obj);
                    }

                    var valueResponse = (List<object>) await GetAllValue(Merchant);
                    foreach (object obj in valueResponse)
                    {
                        responseObject.Add(obj);
                    }
                  
                    return responseObject as object;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> GetAllDiscount(string Merchant)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var responseObject = new List<object>();

                    var amountResponse = (List<object>)await GetAllDiscount(DiscountType.amount, Merchant);
                    foreach (object obj in amountResponse)
                    {
                        responseObject.Add(obj);
                    }

                    var percentageResponse = (List<object>)await GetAllDiscount(DiscountType.percentage, Merchant);
                    foreach (object obj in percentageResponse)
                    {
                        responseObject.Add(obj);
                    }

                    var unitResponse = (List<object>)await GetAllDiscount(DiscountType.unit, Merchant);
                    foreach (object obj in unitResponse)
                    {
                        responseObject.Add(obj);
                    }

                    _logger.LogInformation(new EventId(), "Successfully retrieved Discount all vouchers");

                    Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " retrieved all discount vouchers", "USER", "GET", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                    messageThread.Start();

                    return responseObject as object;


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> GetAllDiscount(DiscountType discountType, string Merchant)
        {
            return await Task.Run( async ()=> 
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("DiscountType",(Int32) discountType,DbType.Int32);
                        parameters.Add("Merchant", Merchant);

                        IDataReader reader = await conn.ExecuteReaderAsync("getAllDiscount", parameters, commandType: CommandType.StoredProcedure);

                        _logger.LogInformation(new EventId(), "Successfully retrieved Discount vouchers of Type " + discountType + "");
                        
                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " retrieved all discount "+discountType.ToString()+" vouchers", "USER", "GET", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

                        return GetVoucherHandler.GetListResponse(reader) as object;
                    }              
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,"Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> GetAllGift(string Merchant)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Merchant", Merchant);

                        IDataReader reader = await conn.ExecuteReaderAsync("getAllGift", parameters, commandType: CommandType.StoredProcedure);
                        var responseObject = GetVoucherHandler.GetListResponse(reader);
                        _logger.LogInformation(new EventId(), "Successfully retrieved Gift vouchers");

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " retrieved all gift vouchers", "USER", "GET", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

                        return responseObject as object;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public async Task<object> GetAllValue(string Merchant)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Merchant", Merchant);

                        IDataReader reader = await conn.ExecuteReaderAsync("getAllValue", parameters, commandType: CommandType.StoredProcedure);
                        var responseObject = GetVoucherHandler.GetListResponse(reader);

                        _logger.LogInformation(new EventId(), "Successfully retrieved value vouchers");

                        Thread messageThread = new Thread(() => _messageBroker.PublishMessage(new CustomMessage("USER " + Merchant + " retrieved all value vouchers", "USER", "GET", String.Format("{0:d/m/yyyy H:mm:ss}", DateTime.Now))));
                        messageThread.Start();

                        return responseObject as object;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return HttpResponseHandler.GetServiceResponse(500);
                }
            });
        }

        public Task<object> ImportVouchers()
        {
            throw new NotImplementedException();
        }

        public Task<object> ImportCSV()
        {
            throw new NotImplementedException();
        }

        public async Task<object> Redeem(string code, Redeem redeem)
        {
            return await Task.Run(async ()=> 
            {
                try
                {
                    object result = null;
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("code", code);
                        parameters.Add("name", redeem.Name);
                        parameters.Add("email", redeem.EmailAddress);
                        parameters.Add("value", redeem.Value);
                        parameters.Add("ReturnValue", DbType.Int16, direction: ParameterDirection.ReturnValue);

                        
                        int rowsAffected = await conn.ExecuteAsync("redeem", parameters, commandType: CommandType.StoredProcedure);
                        int ReturnErrorCode = parameters.Get<int>("ReturnValue");
                        if (ReturnErrorCode != 0)
                        {
                             result = HttpResponseHandler.GetServiceResponse(200, ReturnErrorCode) as object;
                        }
                        else
                        {
                            if (rowsAffected == 1)
                            {
                                result = new redeemResponse(code, HttpResponseHandler.GetServiceResponse(200)) as object;
                            }
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops, an exception occurred");
                    return new StatusCodeResult(500) as object;
                }
            });
        }
    }
}
