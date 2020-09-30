using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTransaction.Application.Service.Customer
{
   public interface ICustomerService
    {
        dynamic GetCustomerDetails();
        dynamic addCustomer(MvCustomer customer);
        dynamic updateCustomer(MvCustomer customer);
    }
}
