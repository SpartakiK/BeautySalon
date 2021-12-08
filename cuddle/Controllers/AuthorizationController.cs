using Domain.Models;
using Web.Models;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.AuthorizationInstruments;
using Services.Services.Abstract;

namespace Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IEmployeeServices _employeeservices;
        public AuthorizationController( IUserServices userServices, IEmployeeServices employeeServices)
        {
            _userServices = userServices;
            _employeeservices = employeeServices;
        }



        public IActionResult Registration()
        {

            return View();

        }


       [HttpPost]
        public IActionResult Registration(string name, string surname, string phonenumber, string password, string repassword)
        {
          var model = new ViewModel();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(phonenumber) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repassword))
            {
                model.Message = "Enter all values!";
                return View(model);
            }
            if (password != repassword)
            {
                model.Message = "Passwords are not the same!";
                return View(model);
            }
            if (_userServices.GetByPhoneNubmer(phonenumber) != null)
            {
                model.Message = "Phonenumber is already in use!";
                return View(model);
            }
            if (phonenumber.Contains("+995") == false)
            {
                phonenumber = "+995" + phonenumber;
            }
            var values = new UserModel
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phonenumber,
                Password = AuthorizationInstruments.CreateMD5(password)
                
            };
            
            _userServices.Create(values);
            model.Message = "You Have Been Registered!";
            return View(model);

        }


        
        public IActionResult Login()
        {           
            return View();      
        }

        [HttpPost]
        public IActionResult Login(string phonenumber, string password)
         {
            var model = new ViewModel();
            if (string.IsNullOrEmpty(phonenumber)|| string.IsNullOrEmpty(password))
            {
                model.Message = "Enter All Values!";
                return View(model);
            }
            if (_userServices.GetByPhoneNubmer(phonenumber) == null)
            {
                model.Message = "User Not Found";
                return View(model);
            }
            bool passwordcheck = _userServices.PasswordCheck(phonenumber, AuthorizationInstruments.CreateMD5(password));
            if (passwordcheck == false)
            {
                model.Message = "Password is incorrect";
                return View(model);
            }
            if (passwordcheck)
            {
                var user = _userServices.GetByPhoneNubmer(phonenumber);
                if (user.Status == "Employee" || user.Status == "Admin")
                {
                    var employee = _employeeservices.GetByUserId(user.Id);
                    HttpContext.Session.SetString("LogInfo", user.Id + " " + user.Status + " " + employee.Id);
                    return RedirectToAction("Cabinet", user.Status);
                }
                HttpContext.Session.SetString("LogInfo", user.Id + " " + user.Status);
                return RedirectToAction("Cabinet", user.Status);
            }

            return RedirectToAction("Index", "Home");
        }

       
        public ActionResult PermissionCheckForCabinet()
        {
           if(HttpContext.Session.GetString("LogInfo") == null)
           {
                return RedirectToAction("Index", "Home", new { message = "Login First!"});
           }               

            var userinfo = HttpContext.Session.GetString("LogInfo").Split(" ");
            return RedirectToAction("Cabinet", userinfo[1]);

        }

























    }
}
