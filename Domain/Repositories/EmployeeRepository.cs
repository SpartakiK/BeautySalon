using Entities;
using Data.Repositories.Abstract;
using EntityFrameworkCore.DbConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbConnect _db;
        public EmployeeRepository(DbConnect db)
        {
            _db = db;
        }
        public void Delete(int id)
        {
            var employee =  _db.Employees.FirstOrDefault(a => a.Id == id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
            }
        }

        public EmployeeEntity Edit(EmployeeEntity entity)
        {
            var employee =  _db.Employees.FirstOrDefault(a => a.Id == entity.Id);
            return employee;
        }

        public IEnumerable<EmployeeEntity> GetAll()
        {
            return  _db.Employees.ToList();
        }

        public EmployeeEntity GetById(int id)
        {
            var employeeentity = _db.Employees.FirstOrDefault(a => a.Id == id);
            return employeeentity;
        }

        public EmployeeEntity GetByUserId(int id)
        {
            return _db.Employees.FirstOrDefault(a => a.UserId == id);
        }


        public EmployeeEntity Insert(EmployeeEntity entity)
        {
            _db.Employees.Add(entity);
            return entity;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
