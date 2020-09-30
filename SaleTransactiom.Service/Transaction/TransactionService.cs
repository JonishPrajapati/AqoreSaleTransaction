using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaleTransaction.Application.DataAccess;
using SaleTransaction.Application.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SaleTransaction.Application.Service.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly string _connectionString;
        private DataAccessHelper _dah;

        public TransactionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (_connectionString != null)
            {
                _dah = new DataAccessHelper(_connectionString);
            }
        }
        public dynamic AddTransaction(MvTransaction transaction)
        {
            var json = JsonConvert.SerializeObject(transaction);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpSaleTransactionInsJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return transaction;
                }
            }
        }

        public dynamic GetTransaction()
        {
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpTransactionSelJson", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("trabsaction Detail");
                            return _dah.GetJson(reader);
                        }
                        return null;
                    }

                }
            }

        }

        public dynamic UpdateTransaction(MvTransaction transaction)
        {
            var json = JsonConvert.SerializeObject(transaction);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpTransactionUpdJson", sql))
                {
                    command.CommandType = (System.Data.CommandType.StoredProcedure); ;
                    command.Parameters.Add(new SqlParameter("@json", json));
                    command.ExecuteNonQuery();
                    return transaction;
                }
            }
        }
    }
}

       
    
