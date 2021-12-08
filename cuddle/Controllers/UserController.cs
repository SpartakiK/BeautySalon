using Domain.Mappers;
using Web.Models;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Mappers.Abstract;
using Web.Models.PermissionCheckings;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly DbConnect db;
        private readonly IUserServices _userServices;
        private readonly IUserMapper _userMapper;
        public UserController(DbConnect _db, IUserServices userServices, IUserMapper userMapper)
        {
            db = _db;
            _userServices = userServices;
            _userMapper = userMapper;
        }

        public IActionResult EditUserPermissionCheck(string phonenumber)
        {
            var loginfo = HttpContext.Session.GetString("LogInfo");
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var employeeinfo = loginfo.Split(" ");
            if (phonenumber.Contains("+995") == false)
            {
                phonenumber = "+995" + phonenumber;
            }
            return RedirectToAction("EditUserAccaunt", employeeinfo[1], new {phonenumber = phonenumber });
        }

    
        public IActionResult FindUser(string message = null) 
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(message);
        }


        public IActionResult ShowUserInfo(string phonenumber = null, int userid = -1, string message = null)
        {
            var loginfo = HttpContext.Session.GetString("LogInfo");
            if (PermissionCheckings.CheckIfAdminOrEmployee(loginfo) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ViewModel();
            if (phonenumber != null)
            {
                if (phonenumber.Contains("+995") == false)
                {
                    phonenumber = "+995" + phonenumber;
                }
                var user = _userServices.GetByPhoneNubmer(phonenumber);                
                model.Message = message;
                if (user == null)
                {
                    return RedirectToAction("FindUser");
                }
                model.User = user;
                return View(model);
            }
            if (userid > 0)
            {
                var user = _userServices.GetById(userid);
                if (user == null)
                {
                    return RedirectToAction("FindUser");
                }
                model.User = user;
                return View(model);
            }
            return FindUser();
        }       

        public IActionResult Profile()
        {
            var loginfo = HttpContext.Session.GetString("LogInfo");
            if (string.IsNullOrEmpty(loginfo))
            {
                return RedirectToAction("Index", "Home");
            }
            loginfo.Split(" ");
            var user = _userServices.GetById(int.Parse(loginfo[0].ToString()));
            
            return View(user);
        }


        public IActionResult EditProfile()
        {
            var loginfo = HttpContext.Session.GetString("LogInfo");
            if (string.IsNullOrEmpty(loginfo))
            {
                return RedirectToAction("Index", "Home");
            }
            loginfo.Split(" ");
            var user = _userServices.GetById(int.Parse(loginfo[0].ToString()));          
            return View(user);
        }


        [HttpPost]
        public IActionResult EditProfile(string name, string surname, string phonenumber)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(phonenumber))
            {
                return RedirectToAction("EditProfile");
            }
            if (phonenumber.Contains("+995") == false)
            {
                phonenumber = "+995" + phonenumber;
            }
            var userinfo = HttpContext.Session.GetString("LogInfo").Split();
            var user = _userServices.GetById(int.Parse(userinfo[0]));           
            user.Name = name;
            user.Surname = surname;
            user.PhoneNumber = phonenumber;
            _userServices.EditUserAccount(user);
            _userServices.Save();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult EditUserAccaunt(UserModel values)
        {
            var user = _userServices.GetByPhoneNubmer(values.PhoneNumber);
            user.Name = values.Name;
            user.Surname = values.Surname;
            user.PhoneNumber = values.PhoneNumber;
            user.Status = values.Status;
            _userServices.EditUserAccount(user);
            ViewBag.Text = "Valuse have been changed!";
            db.SaveChanges();
            return RedirectToAction("ShowUserInfo", "User", new { phonenumber = user.PhoneNumber });
        }
    }
}
