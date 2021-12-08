using Data.Repositories.Abstract;
using Entities;
using EntityFrameworkCore.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class EmployeesAndServicesRepository : IEmployeesAndServicesRepository
    {
        private readonly DbConnect _db;
        public EmployeesAndServicesRepository(DbConnect db)
        {
            _db = db;
        }
        public void Delete(int id)
        {
            var entity = _db.EmployeesAndServices.FirstOrDefault(a => a.Id == id);
            _db.EmployeesAndServices.Remove(entity);
        }

        public void Edit(EmployeesAndServicesEntity entity)
        {
            var employeeandservice = _db.EmployeesAndServices.FirstOrDefault(a => a.Id == entity.Id);
            employeeandservice.EmployeeId = entity.EmployeeId;
            employeeandservice.ServiceId = entity.EmployeeId;
        }

        public IEnumerable<EmployeesAndServicesEntity> GetAll()
        {
            return _db.EmployeesAndServices.ToList();
        }

        public EmployeesAndServicesEntity GetById(int id)
        {
            return _db.EmployeesAndServices.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<EmployeesAndServicesEntity> GetEmployeesByServiceId(int id)
        {
            return _db.EmployeesAndServices.Where(a => a.ServiceId == id);
        }

        public IEnumerable<EmployeesAndServicesEntity> GetServicesByEmployeeId(int id)
        {
            return _db.EmployeesAndServices.Where(a => a.EmployeeId == id);
        }

        public void Insert(int serviceid, int employeeid)
        {
            var values = new EmployeesAndServicesEntity
            {
                ServiceId = serviceid,
                EmployeeId = employeeid,
                CreationDate = DateTime.Now
            };
            _db.EmployeesAndServices.Add(values);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
