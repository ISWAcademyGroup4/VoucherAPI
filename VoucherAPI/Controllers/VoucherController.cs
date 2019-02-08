using Microsoft.AspNetCore.Authorization;
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
    //[AllowAnonymous]
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
            return Created("", await _voucherService.Create(voucher));
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code, [FromQuery] string Merchant)
        {
            return Ok(await _voucherService.Get(code, Merchant));
        }

        [HttpPut("{Code}")]
        public async Task<IActionResult> Update(string Code, [FromQuery] string Merchant, [FromQuery] DateTime ExpirationDate)
        {
            return Ok(await _voucherService.Update(Code, Merchant, ExpirationDate));
        }

        [HttpDelete("{Code}")]
        public async Task<IActionResult> Delete(string code, [FromQuery] string Merchant)
        {
            return Ok(await _voucherService.Delete(code,Merchant));
        }

        [HttpGet("{Campaign}")]
        public async Task<IActionResult> List(string Campaign, [FromQuery] string Merchant)
        {
            return Ok(await _voucherService.List(Campaign, Merchant));
        }

        [HttpPost("{Code}")]
        public async Task<IActionResult> Enable(string Code, [FromQuery] string Merchant)
        {
            return Accepted("", await _voucherService.Enable(Code, Merchant));
        }

        [HttpPost("{Code}")]
        public async Task<IActionResult> Disable(string Code, [FromQuery] string Merchant)
        {
            return Accepted("", await _voucherService.Disable(Code, Merchant));
        }

        [HttpPost("{Code}/balance")]
        public async Task<IActionResult> AddGiftBalance(string Code, [FromQuery] string merchant, [FromQuery] long amount)
        {
            return Accepted(await _voucherService.AddGiftBalance(Code, merchant, amount));
        }

        [HttpGet("{BatchNo}")]
        public async Task<IActionResult> GetBatchCount(string batchno)
        {
            return Ok(await _voucherService.GetBatchCount(batchno));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string Merchant)
        {
            return Ok(await _voucherService.GetAll(Merchant));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscount([FromQuery] string Merchant)
        {
            return Ok(await _voucherService.GetAllDiscount(Merchant));
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscount([FromQuery] DiscountType discountType, [FromQuery] string merchant)
        {
            return Ok(await _voucherService.GetAllDiscount(discountType, merchant));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGift([FromQuery] string Merchant)
        {
            return Ok(await _voucherService.GetAllGift(Merchant));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllValue([FromQuery] string Merchant)
        {
            
            return Ok(await _voucherService.GetAllValue(Merchant));
        }

        [HttpPost]
        public Task<IActionResult> Import(Voucher[] vouchers)
        {
            return null;
        }

        [HttpPost]
        public Task<IActionResult> ImportCSV()
        {
            return null;
        }

        [HttpPost("{code}/redeem")]
        public async Task<IActionResult> Redeem(string code, Redeem redeem)
        {
            return Ok(await _voucherService.Redeem(code, redeem));
        }
    }
}
