using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstract
{
    public interface IEmployeesAndServicesServices
    {
        IEnumerable<EmployeesAndServicesModel> GetList();
        IEnumerable<EmployeesAndServicesModel> GetEmployeesByServiceId(int id);
        IEnumerable<EmployeesAndServicesModel> GetServicesByEmployeeId(int id);
        EmployeesAndServicesModel GetById(int id);
        void Create(int serviceid, int employeeid);
        void Edit(EmployeesAndServicesModel values);
        void Delete(int id);
        void Save();
    }

}

