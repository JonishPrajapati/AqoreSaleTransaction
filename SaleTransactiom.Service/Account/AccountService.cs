using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaleTransaction.Application.DataAccess;
using SaleTransaction.Application.Model;

namespace SaleTransaction.Application.Service.Account
{
    public class AccountService : IAccountService
    {
        private readonly string _connectionString;
        private DataAccessHelper _dah;

        public AccountService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (_connectionString !=null )
            {
                _dah = new DataAccessHelper(_connectionString);
            }
        }

        public dynamic GetLogin(MvUserLogin userLogin)
        {

        
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpUserLoginCheck", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserName", userLogin.Username));
                    command.Parameters.Add(new SqlParameter("@Password", userLogin.Password));


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("user Exist");
                            return _dah.GetJson(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public dynamic GetDetails(string json)
        {
            var jsonNew = JsonConvert.DeserializeObject(json);
            using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpUserLoginSelByUser", sql))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@json", jsonNew.ToString()));
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

        public dynamic AllDetails()
        {
             using (var sql = _dah.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SpUserLoginSelAllUser", sql))
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
    }
}
