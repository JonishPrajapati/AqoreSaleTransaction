using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTransaction.Application.Service.ProductRate
{
   public interface IProductRateService
    {
        dynamic AddRate(MvProductRate rate);
        dynamic GetAllRate();
        dynamic UpdateRate(MvProductRate rate);
    }
}
