using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaleTransaction.Application.DataAccess;
using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SaleTransaction.Application.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly string _connectionString;
        private DataAccessHelper _dah;

        public CustomerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (_connectionString != null)
            {
                _dah = new DataAccessHelper(_connectionString);
            }
        }

        public dynamic addCustomer(MvCustomer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpCustomerInsJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return customer;
                }
            }
        }

        public dynamic GetCustomerDetails()
        {
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpCustomerSelJson", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("user Detail");
                            return _dah.GetJson(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public dynamic updateCustomer(MvCustomer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpCustomerUpdJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return customer;
                }
            }
        }
    }
}
