using Domain.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ViewModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public IEnumerable<ServiceModel> Services { get; set; }
        public IEnumerable<EmployeesAndServicesModel> EmployeesAndServices { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; }
        public IEnumerable<OrderDetailModel> OrderDetails { get; set; }
        public ServiceModel Service { get; set; }
        public OrderModel Order { get; set; }
        public UserModel User { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public string Message { get; set; }

    }
}
