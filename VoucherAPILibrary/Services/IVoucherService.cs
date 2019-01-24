using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Responses;

namespace VoucherAPILibrary.Services
{
    public interface IVoucherService<T> 
    {

        Task<object> CreateVoucher(Voucher voucher);

        Task<T> GetVoucher(string voucherCode, string MerchantId);

        Task<T> UpdateVoucher(string code, DateTime ExpirationDate);

        Task<T> DeleteVoucher(string voucherCode);

        Task<T> ListVouchers(string campaign, string MerchantId);

        Task<T> EnableVoucher (string code, string MerchantId);

        Task<T> DisableVoucher(string code, string MerchantId);

        Task<T> AddGiftBalance(string code, string MerchantId, long amount);

    }
}
