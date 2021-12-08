using Domain.Mappers;
using Domain.Models;
using Web.Models;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Mappers.Abstract;
using Services.Services.Abstract;
using Web.Models.PermissionCheckings;

namespace Web.Controllers
{


    public class EmployeeController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IUserMapper _userMapper;
        private readonly IEmployeeServices _employeeServices;
        private readonly IEmployeesAndServicesServices _employeesAndServicesServices;
        public EmployeeController
            (
            IUserServices userServices,
            IUserMapper userMapper,
            IEmployeeServices employeeServices,
           IEmployeesAndServicesServices employeesAndServicesServices
            )
        {
            _userServices = userServices;
            _userMapper = userMapper;
            _employeeServices = employeeServices;
            _employeesAndServicesServices = employeesAndServicesServices;
        }

        

        
        public IActionResult Cabinet()
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        
        public  IActionResult EditUserAccaunt(string phonenumber, string message = null)
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ViewModel();
            var user =  _userServices.GetByPhoneNubmer(phonenumber);
            var employee = _employeeServices.GetByUserId(user.Id);
            if (employee != null)
            {
                return RedirectToAction("ShowUserInfo", "User", new {phonenumber = phonenumber, message = "You dont't have a permission!" });
            }
            if (user != null)
            {
                model.User = user;
                model.Message = message;
            }          
            return View(model);
        }


    }
}
