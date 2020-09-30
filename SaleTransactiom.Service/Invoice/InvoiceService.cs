using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaleTransaction.Application.DataAccess;
using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SaleTransaction.Application.Service.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly string _connectionString;
        private DataAccessHelper _dah;

        public InvoiceService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (_connectionString != null)
            {
                _dah = new DataAccessHelper(_connectionString);
            }
        }

        public dynamic GetInvoiceDetails()
        {

            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpInvoiceSelJson", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Invoice Detail");
                            return _dah.GetJson(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public dynamic GetSingleInvoiceDetails(String json) 
        {
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpInvoiceOneUser", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("invoice Detail");
                            return _dah.GetJson(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public dynamic ProduceInvoice(IEnumerable<MvInvoice> invoice)
        {
            var json = JsonConvert.SerializeObject(invoice);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpSaleInvoiceInsJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return invoice;
                }
            }
        }
    }
}
