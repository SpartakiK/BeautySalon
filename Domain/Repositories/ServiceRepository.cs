using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using EntityFrameworkCore.DbConnection;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{

    public class ServiceRepository : IServiceRepository
    {
        private readonly DbConnect _db;
        public ServiceRepository(DbConnect db)
        {
            _db = db;
        }
        public void Delete(int id)
        {
            var service = _db.Services.FirstOrDefault(a => a.Id == id);
            if (service != null)
            {
                _db.Services.Remove(service);
            }

        }

        public IEnumerable<ServiceEntity> GetAll()
        {
            return _db.Services.ToList();
        }

        public ServiceEntity GetById(int id)
        {
            return  _db.Services.FirstOrDefault(a => a.Id == id);        
        }
     

        public ServiceEntity Insert(ServiceEntity entity)
        {
            _db.Services.AddAsync(entity);
            return entity;
        }

        public ServiceEntity Edit(ServiceEntity entity)
        {
            var service = _db.Services.FirstOrDefault(a => a.Id == entity.Id);
            service.Name = entity.Name;
            service.Price = entity.Price;
            service.Description = entity.Description;
            service.CreationDate = entity.CreationDate;
            return service;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
