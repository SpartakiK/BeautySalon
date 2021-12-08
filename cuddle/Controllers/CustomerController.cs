using Web.Models;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstract;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IOrderServices _orderServices;
        public CustomerController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public IActionResult Cabinet()
        {
            if (HttpContext.Session.GetString("LogInfo") == null)
            {
                    return RedirectToAction("Index", "Home");                
            }

            return View();
        }

    }
}
