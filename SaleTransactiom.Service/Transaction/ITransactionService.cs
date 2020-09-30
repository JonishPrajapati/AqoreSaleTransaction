using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTransaction.Application.Service.Transaction
{
    public interface ITransactionService
    {
        dynamic AddTransaction(MvTransaction transaction);
        dynamic GetTransaction();
        dynamic UpdateTransaction(MvTransaction transaction);
    }
}
