using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaleTransaction.Application.DataAccess;
using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SaleTransaction.Application.Service.ProductRate
{
    public class ProductRateService : IProductRateService
    {
        private readonly string _connectionString;
        private DataAccessHelper _dah;

        public ProductRateService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (_connectionString != null)
            {
                _dah = new DataAccessHelper(_connectionString);
            }
        }
        public dynamic AddRate(MvProductRate rate)
        {
            var json = JsonConvert.SerializeObject(rate);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpProductRateInsJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return rate;
                }
            }
        }

        public dynamic GetAllRate()
        {
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpProductRateSelJson", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Rate Detail");
                            return _dah.GetJson(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public dynamic UpdateRate(MvProductRate rate)
        {
            var json = JsonConvert.SerializeObject(rate);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpProductRateUpdJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return rate;
                }
            }
        }
    }
}
