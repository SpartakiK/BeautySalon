using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.DbConnection;

namespace Data.Repositories
{
   public  class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DbConnect _db;
        public OrderDetailRepository(DbConnect db)
        {
            _db = db;
        }

        public void Delete(int id)
        {
            var orderdetail = _db.OrderDetails.FirstOrDefault(a => a.Id == id);
            if (orderdetail != null)
            {
                _db.OrderDetails.Remove(orderdetail);
                _db.SaveChanges();
            }
        }

        public OrderDetailEntity GetById(int id)
        {
            return  _db.OrderDetails.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<OrderDetailEntity> GetAll()
        {
            return  _db.OrderDetails.ToList();
        }

        public OrderDetailEntity Insert(OrderDetailEntity entity)
        {
            _db.OrderDetails.Add(entity);
            return entity;
        }

        public OrderDetailEntity Edit(OrderDetailEntity entity)
        {
            var orderdetails =  _db.OrderDetails.FirstOrDefault(a => a.Id == entity.Id);
            orderdetails.ServiceId = entity.ServiceId;
            orderdetails.Price = entity.Price;
            orderdetails.Comission = entity.Comission;
            return orderdetails;
        }
        public void Save()      
        {
            _db.SaveChanges();      
        }

        public IEnumerable<OrderDetailEntity> GetListByOrderId(int id)
        {
            return _db.OrderDetails.Where(a => a.OrderId == id);
        }
    }
}