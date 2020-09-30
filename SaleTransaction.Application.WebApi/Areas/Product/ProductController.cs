using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleTransaction.Application.Model;
using SaleTransaction.Application.Service.Product;
using SaleTransaction.Application.WebApi.Base;

namespace SaleTransaction.Application.WebApi.Areas.Product
{
    public class ProductController : BaseController {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public IActionResult productAdd([FromBody] MvProduct product)
        {
            try
            {
               var data =  _productService.AddProduct(product);
               
                return Ok(data);
            }
            catch(Exception ex)
            {
                    throw;
            }
            
        }


        [HttpGet]
        public IActionResult productAll()
        {
            try
            {
                var details = _productService.GetAllProductDetails();
                return Ok(details);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult productUpdate([FromBody] MvProduct product)
        {
            try
            {
                var data = _productService.UpdateProduct(product);

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
