using Microsoft.AspNetCore.Mvc;
using System;
using VoucherAPI.Controllers;
using VoucherAPILibrary.Domain;
using VoucherAPILibrary.Services;
using VoucherAPILibrary.Utils;
using Xunit;

namespace APITests
{
    public class UnitTest1
    {
        VoucherController _controller;
        VoucherService _service;
        public UnitTest1(VoucherController controller,VoucherService service)
        {
            _controller = controller;
            _service = service;
        }

        [Fact]
        public async void GetAllTest()
        {
            var okResult = await _controller.GetAll("Enunwah");

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async void GetAllDiscountTest()
        {
            var okResult = await _controller.GetAllDiscount("Enunwah");

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async void GetDiscountTest()
        {
            var okResult = await _controller.GetDiscount(DiscountType.amount,"Enunwah");

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async void GetAllGiftTest()
        {
            var okResult = await _controller.GetAllGift("Enunwah");

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async void GetAllValueTest()
        {
            var okResult = await _controller.GetAllValue("Enunwah");

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        
    }
}
