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
    //[Authorize]
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

        [HttpGet]
        public string Voucher()
        {
            return "Voucher API Service is UP & Running";
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            return Accepted("",await _voucherService.Create(voucher));        
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code, [FromQuery] string Merchant)
        {
            return Ok(await _voucherService.Get(code, Merchant));
        }

        [HttpPut("{Code}")]
        public async Task<IActionResult> Update(string Code, [FromQuery] DateTime ExpirationDate)
        {
            return Ok(await _voucherService.Update(Code, ExpirationDate));
        }

        [HttpDelete("{Code}")]
        public async Task<IActionResult> Delete(string code, [FromQuery] string Merchant)
        {
            return Ok( await _voucherService.Delete(code));
        }

        [HttpGet("{Campaign}")]
        public async Task<IActionResult> List(string Campaign, [FromQuery] string Merchant)
        {      
            return Ok(await _voucherService.List(Campaign, Merchant));
        }

        [HttpPost("{Code}")]
        public async Task<IActionResult> Enable(string Code, [FromQuery] string e)
        {
            return Accepted("", await _voucherService.Enable(Code, e));
        }

        [HttpPost("{Code}")]
        public async Task<IActionResult> Disable(string Code, [FromQuery] string e)
        {
            return Accepted("", await _voucherService.Disable(Code, e));
        }

        [HttpPost("{Code}/balance")]
        public async Task<IActionResult> AddGiftBalance(string Code, [FromQuery] string e, [FromQuery] long a)
        {
            return Accepted(await _voucherService.AddGiftBalance(Code, e, a));
        }

        [HttpGet("{BatchNo}")]
        public async Task<IActionResult> GetBatchCount(string batchno)
        {
            return Ok(await _voucherService.GetBatchCount(batchno));
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
