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
        Task<GetVoucherResponse> GetVoucher(string voucherCode, string MerchantId);
        Task<UpdateVoucherResponse> UpdateVoucher(Voucher voucher);
        Task<DeleteVoucherResponse> DeleteVoucher(string voucherCode, string MerchantId);
        
    }
}
