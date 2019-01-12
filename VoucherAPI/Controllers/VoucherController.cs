using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoucherAPILibrary.Dao;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Models;
using VoucherAPILibrary.Responses;
using VoucherAPILibrary.Services;

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
        public async Task<ActionResult> GetVoucher(string code,[FromHeader] string username)
        {
            return Ok(await VoucherDao.GetVoucher(code,username));
        }

    }
}
