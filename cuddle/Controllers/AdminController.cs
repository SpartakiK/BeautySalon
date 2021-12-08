using Domain.Mappers;
using Domain.Models;
using Web.Models;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.PermissionCheckings;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private IUserServices _userService;
        public AdminController( IUserServices userServices)
        {
            _userService = userServices;
        }

        public IActionResult Cabinet()
        {
            
            if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult EditUserAccaunt(string phonenumber, string message = null)
        {
            if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _userService.GetByPhoneNubmer(phonenumber);
            var model = new ViewModel();
            model.Message = message;
            model.User = user;
            return View(model);
        }
               
    }
    
}
