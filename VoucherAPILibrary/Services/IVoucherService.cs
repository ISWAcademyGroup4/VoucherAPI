using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Services
{
    public interface IVoucherService<T> 
    {

        Task<T> Create(Voucher voucher);

        Task<T> Get(string voucherCode, string MerchantId);

        Task<T> Update(string code, string Mercahnt, DateTime ExpirationDate);

        Task<T> Delete(string voucherCode, string Merchant);

        Task<T> List(string campaign, string MerchantId);

        Task<T> Enable (string code, string MerchantId);

        Task<T> Disable(string code, string MerchantId);

        Task<T> AddGiftBalance(string code, string Merchant, long amount);

        Task<T> GetBatchCount(string batchno);

        Task<T> GetAll(string Merchant);

        Task<T> GetAllDiscount(string merchant);

        Task<T> GetAllDiscount(DiscountType discountType, string merchant);

        Task<T> GetAllGift(string Merchant);

        Task<T> GetAllValue(string Merchant);

        Task<T> ImportVouchers();

        Task<T> ImportCSV();

        Task<T> Redeem(string code, Redeem redeem);
        
    }
}
