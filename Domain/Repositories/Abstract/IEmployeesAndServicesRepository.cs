using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IEmployeesAndServicesRepository
    {
        IEnumerable<EmployeesAndServicesEntity> GetAll();
        IEnumerable<EmployeesAndServicesEntity> GetEmployeesByServiceId(int id);
        IEnumerable<EmployeesAndServicesEntity> GetServicesByEmployeeId(int id);
        EmployeesAndServicesEntity GetById(int id);
        void Insert(int serviceid, int employeeid);
        void Edit(EmployeesAndServicesEntity entity);
        void Delete(int id);
        void Save();
    }
}
