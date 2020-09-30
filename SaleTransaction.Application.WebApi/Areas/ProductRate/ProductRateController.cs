using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleTransaction.Application.Model;
using SaleTransaction.Application.Service.ProductRate;
using SaleTransaction.Application.WebApi.Base;

namespace SaleTransaction.Application.WebApi.Areas.ProductRate
{
   
    public class ProductRateController : BaseController
    {
        private IProductRateService _rateService;

        public ProductRateController(IProductRateService rateService)
        {
            this._rateService = rateService;
        
        
        }
        [HttpPost]
        public IActionResult rateAdd([FromBody] MvProductRate rate)
        {
            try
            {
                var data = _rateService.AddRate(rate);

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        [HttpGet]
        public IActionResult allRate()
        {
            try
            {
                var details = _rateService.GetAllRate();
                return Ok(details);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpPost]
        public IActionResult rateUpdate([FromBody] MvProductRate rate)
        {
            try
            {
                var data = _rateService.UpdateRate(rate );

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
