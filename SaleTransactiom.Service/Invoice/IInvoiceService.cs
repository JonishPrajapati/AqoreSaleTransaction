using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTransaction.Application.Service.Invoice
{
    public interface IInvoiceService
    {
        dynamic ProduceInvoice(IEnumerable<MvInvoice>  invoice);
        dynamic GetSingleInvoiceDetails(String json);
        dynamic GetInvoiceDetails();
        
    }
}
