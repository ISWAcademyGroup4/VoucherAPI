using Microsoft.AspNetCore.Http;
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
    [Route("[action]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        public readonly IVoucherService<object> _voucherService;

        public VoucherController(IVoucherService<object> voucherService)
        {
            _voucherService = voucherService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateVoucher(Voucher voucher)
        {
            return Created("" ,await _voucherService.CreateVoucher(voucher));        
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetVoucher(string code, [FromQuery] string Merchant)
        {
            return Ok(await _voucherService.GetVoucher(code, Merchant));
        }

        [HttpPut("{Code}")]
        public async Task<IActionResult> UpdateVoucher(string Code, [FromQuery] DateTime ExpirationDate)
        {
            return Ok(await _voucherService.UpdateVoucher(Code, ExpirationDate));
        }

        [HttpDelete("{Code}")]
        public async Task<IActionResult> Deletevoucher(string code, [FromQuery] string Merchant)
        {
            return Ok( await _voucherService.DeleteVoucher(code));
        }

        [HttpGet("{Campaign}")]
        public async Task<IActionResult> ListVouchers(string Campaign, [FromQuery] string Merchant)
        {      
            return Ok(await _voucherService.ListVouchers(Campaign, Merchant));
        }

        [HttpPost("{Code}")]
        public async Task<IActionResult> EnableVoucher(string Code, [FromQuery] string e)
        {
            return Accepted("", await _voucherService.EnableVoucher(Code, e));
        }

        [HttpPost("{Code}")]
        public async Task<IActionResult> DisableVoucher(string Code, [FromQuery] string e)
        {
            return Accepted("", await _voucherService.DisableVoucher(Code, e));
        }

        [HttpPost("{Code}/balance")]
        public async Task<IActionResult> AddGiftBalance(string Code, [FromQuery] string e, [FromQuery] long a)
        {
            return Accepted(await _voucherService.AddGiftBalance(Code, e, a));
        }

        //[HttpPost]
        //public Task<object> Import(Voucher[] vouchers)
        //{
        //    return null;
        //}

        //[HttpPost]
        //public Task<object> ImportCSV(HttpPostedFile postedFile)
        //{
        //    return null;
        //}

    }
}
