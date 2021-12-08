using Domain.Mappers;
using Web.Models;
using Entities;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services.Abstract;
using Domain.Models;
using Domain.Mappers.Abstract;
using Web.Models.PermissionCheckings;

namespace Web.Controllers
{
     public class ServiceController : Controller
    {
        private readonly IServiceServices _serviceServices;
        private readonly IServiceMapper _serviceMapper;
        IEmployeesAndServicesServices _employeesAndServicesServices;
        public ServiceController
            ( 
            IServiceServices serviceServices,
            IServiceMapper serviceMapper,
            IEmployeesAndServicesServices employeesAndServicesServices
            )
        {
            _serviceServices = serviceServices;
            _serviceMapper = serviceMapper;
            _employeesAndServicesServices = employeesAndServicesServices;
        }

        public IActionResult ServiceManagement()
        {
           if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var services = _serviceServices.GetList();
            return View(services);
        }
        public IActionResult OurServices()
        {
            var services = _serviceServices.GetList();
            var model = new ViewModel
            {
                Services = services
            };

            return View(model);
        }
        public IActionResult MyServices()
        {
            var loginfo = HttpContext.Session.GetString("LogInfo");
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string[] employeeinfo = loginfo.Split(" ");
            var myservices = _employeesAndServicesServices.GetServicesByEmployeeId(int.Parse(employeeinfo[2]));
            return View(myservices);
        
        }
        public IActionResult DeleteEmployeeService(int id)
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            _employeesAndServicesServices.Delete(id);
            return RedirectToAction("MyServices");
        }
        public IActionResult CreateEmployeeService() 
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_serviceServices.GetList());
        }
        [HttpPost]
        public IActionResult CreateEmployeeService(int serviceid)
        {
            var loginfo = HttpContext.Session.GetString("LogInfo").Split(" ");
            foreach (var item in _employeesAndServicesServices.GetServicesByEmployeeId(int.Parse(loginfo[2])))
            {
                if (item.Service.Id == serviceid)
                {
                    return RedirectToAction("MyServices");
                }
            }
            _employeesAndServicesServices.Create(serviceid, int.Parse(loginfo[2]));
            return RedirectToAction("MyServices");
        }

        [HttpGet]
        public IActionResult CreateService(string message = null)
        {
            if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(message);
        }

        [HttpPost]
        public IActionResult CreateService(ServiceModel values)
        {
            var model = new ViewModel();
            if (string.IsNullOrEmpty(values.Name) || string.IsNullOrEmpty(values.Description) || values.Price <= 0 || values.Comission < 0)
            {
                return RedirectToAction("CreateService");
            }
            values.Price += (values.Price / 100) * values.Comission;
            _serviceServices.Create(values);
            return RedirectToAction("ServiceManagement");
        }


        public IActionResult DeleteService(int serviceid)
        {
            if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            _serviceServices.Delete(serviceid);
            return RedirectToAction("ServiceManagement");
        }

        public IActionResult EditService(int serviceid, string message = null)
        {
            if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ViewModel();
            model.Service = _serviceServices.GetById(serviceid);
            if (message != null)
            {
                model.Message = message;
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditService(int serviceid, string name , string description, int price, int comission)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || price <= 0 || comission < 0)
            {
                return RedirectToAction("EditService", new { serviceid = serviceid, message = "Enter All Values!" });
            }

            var service = _serviceServices.GetById(serviceid);
            service.Name = name;
            service.Description = description;
            service.Price = price;
            service.Comission = comission;
            _serviceServices.Edit(service);
            return RedirectToAction("ServiceManagement");
        }


    }





}
