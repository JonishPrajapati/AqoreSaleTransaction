using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleTransaction.Application.Model;
using SaleTransaction.Application.Service.Transaction;
using SaleTransaction.Application.WebApi.Base;

namespace SaleTransaction.Application.WebApi.Areas.Transaction
{
  
    public class TransactionController : BaseController
    {
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService _service)
        {
            this._transactionService = _service;
        }
        [HttpPost]
        public IActionResult transactionAdd([FromBody] MvTransaction transaction)
        {
            try
            {
                var data = _transactionService.AddTransaction(transaction);

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        [HttpGet]
        public IActionResult transactionAll()
        {
            try
            {
                var details = _transactionService.GetTransaction();
                return Ok(details);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult transactionUpdate([FromBody] MvTransaction transaction)
        {
            try
            {
                var data = _transactionService.UpdateTransaction(transaction);

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
