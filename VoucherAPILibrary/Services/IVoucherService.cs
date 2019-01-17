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
    public interface IVoucherService
    {
        Task<CreateVoucherResponse> CreateVoucher(Voucher voucher);
        Task<Object> GetVoucher(string voucherCode, string MerchantId);
        Task<UpdateVoucherResponse> UpdateVoucher(string code, DateTime ExpirationDate);
        Task<DeleteVoucherResponse> DeleteVoucher(string voucherCode);
        Task<ListVoucherResponse> ListVouchers(string campaign);
    }
}
