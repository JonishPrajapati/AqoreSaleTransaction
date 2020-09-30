using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleTransaction.Application.Model;
using SaleTransaction.Application.Service.Invoice;
using SaleTransaction.Application.WebApi.Base;

namespace SaleTransaction.Application.WebApi.Areas.Invoice
{

    public class InvoiceController : BaseController
    {
        private IInvoiceService _invoiceService;
        public InvoiceController([FromBody] IInvoiceService invoiceService)
        {
            this._invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult GetInvoiceDetail()
       {
            try
            {
                var details = _invoiceService.GetInvoiceDetails();
                return Ok(details);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult generateInvoice([FromBody]IEnumerable<MvInvoice> invoice)
        {
            try
            {
                var data = _invoiceService.ProduceInvoice(invoice);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpGet]
        public IActionResult generateSingleInvoice(string json)
        {

            try
            {
                var data = _invoiceService.GetSingleInvoiceDetails(json);
                return Ok(data);
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
