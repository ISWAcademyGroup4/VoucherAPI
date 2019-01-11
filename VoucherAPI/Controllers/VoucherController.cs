using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoucherAPILibrary.Dao;
using VoucherAPILibrary.Models;

namespace VoucherAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateVoucher(Voucher voucher)
        {
            return Ok(await VoucherDao.CreateVoucher(voucher));
        }

    }
}
