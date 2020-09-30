using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTransaction.Application.Service.Product
{
   public interface IProductService
    {
         dynamic AddProduct(MvProduct productItem);
        dynamic GetAllProductDetails();
        dynamic UpdateProduct(MvProduct productItem);
       
    }
}
