using Domain.Mappers;
using Domain.Models;
using Data.Repositories.Abstract;
using Web.Models;
using Entities;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstract;
using Domain.Abstract.Mappers;
using Domain.Mappers.Abstract;
using Services.Services.Abstract;
using Web.Models.PermissionCheckings;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IEmployeeMapper _employeeMapper;
        private readonly IServiceMapper _serviceMapper;
        private readonly IEmployeeServices _employeeServices;
        private readonly IUserServices _userServices;
        private readonly IServiceServices _serviceServices;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IOrderStatusHistoryServices _orderDetailHistoryServices;
        private readonly IOrderStatusHistoryMapper _orderDetailHistoryMapper;
        private readonly IEmployeesAndServicesServices _employeesAndServicesServices;
        public OrderController
            (
            IOrderServices orderServices,
            IEmployeeMapper employeeMapper,
            IServiceMapper serviceMapper,
            IEmployeeServices employeeServices,
            IUserServices userServices,
            IServiceServices serviceServices,
            IOrderDetailServices orderDetailServices,
            IOrderStatusHistoryServices orderDetailHistoryServices,
            IOrderStatusHistoryMapper orderDetailHistoryMapper,
            IEmployeesAndServicesServices employeesAndServicesServices
            )
        {
            _orderServices = orderServices;
            _employeeMapper = employeeMapper;
            _serviceMapper = serviceMapper;
            _employeeServices = employeeServices;
            _userServices = userServices;
            _serviceServices = serviceServices;
            _orderDetailServices = orderDetailServices;
            _orderDetailHistoryServices = orderDetailHistoryServices;
            _orderDetailHistoryMapper = orderDetailHistoryMapper;
            _employeesAndServicesServices = employeesAndServicesServices;
        }
        public IActionResult OrdersManagement(string message, int orderid = 0, int customerid = 0, string customerphonenumber = null, int employeeid = 0)
        { 
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ViewModel();
            model.Message = message;
            if (orderid > 0)
            {
                var order = _orderServices.GetById(orderid);
                model.Order = order;
                return View(model);
            }
            if (customerid > 0)
            {
                model.Orders = _orderServices.GetListByCustomerId(customerid);
                return View(model); 
            }
            if(customerphonenumber != null)
            {
                if (customerphonenumber.Contains("+995") == false)
                {
                    customerphonenumber = "+995" + customerphonenumber;
                }
                var customer = _userServices.GetByPhoneNubmer(customerphonenumber);
                if (customer != null)
                {
                    model.Orders = _orderServices.GetListByCustomerId(customer.Id);
                    return View(model);
                }
            }
            if (employeeid > 0)
            {
                model.Orders = _orderServices.GetListByEmployeeId(employeeid);
                return View(model);
            }
            return View(model);
        }

        public IActionResult CreateOrder(int serviceid = 0, string message = null)         
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ViewModel();
            model.Message = message;
            model.Employees = _employeeServices.GetList();
            model.Services = _serviceServices.GetList();
            if (serviceid != 0)
            {
                model.EmployeesAndServices = _employeesAndServicesServices.GetEmployeesByServiceId(serviceid);
            }
            return View(model);

        }
        

        [HttpPost]
        public IActionResult CreateOrder(string customerphonenumber, int employeeid, DateTime makingdate, string additionalinfo)
        {
            var model = new ViewModel();
            if (string.IsNullOrEmpty(customerphonenumber) || employeeid <= 0 || makingdate == new DateTime())
            {
                return RedirectToAction("CreateOrder", new { message = "Enter All Values!" });
            }

            if (customerphonenumber.Contains("+995") == false)
            {
                customerphonenumber = "+995" + customerphonenumber;
            }
            var customer = _userServices.GetByPhoneNubmer(customerphonenumber);
            if (customer == null)
            {
                model.Message = "Customer not found!";
                return View(model);
            }
            _orderServices.Create(customer.Id,employeeid,makingdate,additionalinfo);
            return RedirectToAction("OrdersManagement", new {customerphonenumber = customerphonenumber}); 
        }


        public IActionResult AddOrderDetails(int orderid)
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var order = _orderServices.GetById(orderid);
            var model = new ViewModel();
            model.EmployeesAndServices = _employeesAndServicesServices.GetServicesByEmployeeId(order.Employee.Id);
            model.OrderId = orderid;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrderDetails(int orderid, int serviceid)
        {
            var service = _serviceServices.GetById(serviceid);
            var neworderdetail = new OrderDetailModel
            {
                OrderId = orderid,
                Service = _serviceServices.GetById(serviceid),
                Comission = service.Comission,
                Price = service.Price,
                CreationDate = DateTime.Now
                
            };
            var order = _orderServices.GetById(orderid);
            order.TotalPrice += service.Price;
            _orderServices.Edit(order);
            _orderDetailServices.Create(neworderdetail);
            return RedirectToAction("OrdersManagement", new { orderid = orderid });
        }
        public IActionResult DeleteOrderDetail(int orderdetailid)
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var orderdetail = _orderDetailServices.GetById(orderdetailid);
            var order = _orderServices.GetById(orderdetail.OrderId);
            order.TotalPrice -= orderdetail.Price;
            _orderServices.Edit(order);
            _orderDetailServices.Delete(orderdetailid);
            return RedirectToAction("OrderDetails", new { orderid = orderdetail.OrderId});
        }
        public IActionResult OrderDetails(int orderid)
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_orderDetailServices.GetListByOrderId(orderid));
        }

        
        public IActionResult DeleteOrder(int orderid)
        {
            if (PermissionCheckings.CheckIfAdmin(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("OrdersManagement", new { message = "Only admin can delete orders!" });
            }
            var order = _orderServices.GetById(orderid);
            _orderServices.Delete(order.Id);
            return RedirectToAction("OrdersManagement", new {customerid = order.Customer.Id});
        }



        public IActionResult EditOrder(int orderid)
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ViewModel();
            var order = _orderServices.GetById(orderid);
            return View(orderid);
        }
        [HttpPost]
        public IActionResult EditOrder(int orderid, string additionalinfo, string stage)
        {
            if (string.IsNullOrEmpty(additionalinfo))
            {
                
            }
            var order = _orderServices.GetById(orderid);
            order.Stage = stage;
            order.AdditionalInfo = additionalinfo;
            _orderServices.Edit(order);
            var employeeinfo = HttpContext.Session.GetString("LogInfo").Split(" ");

                var orderstagehistory = new OrderStatusHistoryModel
                {
                    OrderId = orderid,
                    EmployeeId = int.Parse(employeeinfo[2]),
                    Status = stage,
                    CreationDate = DateTime.Now
                };
                _orderDetailHistoryServices.Create(orderstagehistory);
            if (order.Employee.Id == int.Parse(employeeinfo[2]))
            {
                return RedirectToAction("MyServiceOrders");
            }
            return RedirectToAction("OrdersManagement", new { orderid = orderid});
        }

        public IActionResult MyServiceOrders()
        {
            if (PermissionCheckings.CheckIfAdminOrEmployee(HttpContext.Session.GetString("LogInfo")) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var loginfo = HttpContext.Session.GetString("LogInfo").Split(" ");            
            return View(_orderServices.GetListByEmployeeId(int.Parse(loginfo[2])));
        }

        public IActionResult MyOrders()
        {
            var loginfo = HttpContext.Session.GetString("LogInfo").Split(" ");
            if (loginfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var customerorders = _orderServices.GetListByCustomerId(int.Parse(loginfo[0]));
            return View(customerorders);
        }
        public IActionResult MyOrderDetails(int orderid)
        {
            return View(_orderDetailServices.GetListByOrderId(orderid));
        }

    }
}
