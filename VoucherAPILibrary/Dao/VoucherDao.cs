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
                    parameters.Add("UserId", voucher.UserId);
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
                return new CreateVoucherResponse(null, voucher.VoucherType.ToString(), new ServiceResponse("500", "Something went wrong", new List<Error>
                {
                    new Error(ex.GetHashCode().ToString(),ex.Message)
                }));
            }
        }

    }
}
