using Domain.Mappers;
using Web.Models;
using Data.Repositories.Abstract;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Services.Services.Abstract;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceServices _serviceServices;
        public HomeController( IServiceServices serviceRepository)
        {
            _serviceServices = serviceRepository;
        }
        public IActionResult Index(string message = null)
        {
            if (message != null)
            {
                return View(message);
            }

            return View();

        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}
