using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleTransaction.Application.WebApi.Base;
using SaleTransaction.Application.Model;
using SaleTransaction.Application.Service.Account;




    public class AccountController : BaseController
    {
        private IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service; //defining depedency

        }

        [HttpGet]
        public string getURL()
        {
            return ("succedd");
        }

        [HttpGet]
        public IActionResult UserDetail(string json)
        {
            
        try
        {
            var data = _service.GetDetails(json);
            return Ok(data);
        }
        catch (Exception)
        {

            throw;
        }
    }

        [HttpPost]
        public IActionResult UserLogin([FromBody] MvUserLogin login)
        {
          
        try
        {
            var dataLogin = _service.GetLogin(login);
            return Ok(dataLogin);
        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpGet]
    public IActionResult AllUser()
    {
        try
        {
            var data = _service.AllDetails();
            return Ok(data);
        }
        catch(Exception ex)
        {
            throw;
        }
       
    }
    }

