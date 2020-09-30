using System;
using System.Collections.Generic;
using System.Text;
using SaleTransaction.Application.Model;

namespace SaleTransaction.Application.Service.Account
{
    public interface IAccountService
    {

        dynamic GetLogin(MvUserLogin userLogin);
        dynamic GetDetails(string json);
        dynamic AllDetails();
    }
}
