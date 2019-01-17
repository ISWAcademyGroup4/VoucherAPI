using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Services;
using VoucherAPILibrary.Utils;

namespace VoucherAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        public readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            this._voucherService = voucherService;      
        }

        
        [HttpPost]
        public Task<CreateVoucherResponse> CreateVoucher(Voucher voucher)
        {
            return _voucherService.CreateVoucher(voucher);
        }

        [HttpGet("{code}")]
        public Task<object> GetVoucher(string code, [FromQuery] string Merchant)
        {
            return _voucherService.GetVoucher(code, Merchant);
        }

        [HttpPut("{Code}")]
        public Task<UpdateVoucherResponse> UpdateVoucher(string Code, [FromQuery] DateTime ExpirationDate)
        {
            return _voucherService.UpdateVoucher(Code, ExpirationDate);
        }

        [HttpDelete("{Code}")]
        public Task<DeleteVoucherResponse> Deletevoucher(string code, [FromQuery] string Merchant)
        {
            return _voucherService.DeleteVoucher(code);
        }

        [HttpGet("{Campaign}")]
        public Task<ListVoucherResponse> ListVouchers(string Campaign, [FromQuery] string Merchant)
        {
            return null;
        }

        //[HttpPost("{Code}")]
        //public Task<EnableVoucherResponse> EnableVoucher(string Code, [FromQuery] string Merchant)
        //{
        //    return null;
        //}

        [HttpPost("{Code}")]
        public Task<DisableVoucherResponse> DisableVoucher(string Code, [FromQuery] string Merchant)
        {
            return null;
        }

        //[HttpPost("{Code}/balance")]
        //public Task<AddGiftBalanceResponse> AddGiftBalance(string Code, [FromQuery] string Merchant)
        //{
        //    return null;
        //}

        //[HttpPost]
        //public Task<ImportVouchersResponse> Import(Voucher voucher)
        //{
        //    return null;
        //}

        //[HttpPost]
        //public Task<ImportVouchersCSVResponse> ImportCSV(string CSV)
        //{
        //    return null;
        //}

    }
}
