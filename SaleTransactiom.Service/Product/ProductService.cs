using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaleTransaction.Application.DataAccess;
using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace SaleTransaction.Application.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString;
        private DataAccessHelper _dah;

        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (_connectionString != null)
            {
                _dah = new DataAccessHelper(_connectionString);
            }
        }
        public  dynamic AddProduct(MvProduct productItem)
        {

            var json = JsonConvert.SerializeObject(productItem);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpProductInsJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure);;
                    command.Parameters.Add(new SqlParameter("@json", json)); 
                    command.ExecuteNonQuery();
                    return productItem;
                }
            }
        }

        public dynamic GetAllProductDetails()
        {
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpProductSelJson", sql))
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

        public dynamic UpdateProduct(MvProduct productItem)
        {
            var json = JsonConvert.SerializeObject(productItem);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpProductUpdJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return productItem;
                }
            }
        }
    }
}
