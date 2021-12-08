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
   public class OrderRepository : IOrderRepository
    {
        private readonly DbConnect _db;
        public OrderRepository(DbConnect db)
        {
            _db = db;
        }

        public void Delete(int id)
        {
            var order =  _db.Orders.FirstOrDefault(a => a.Id == id);
            if (order != null)
            {
                _db.Orders.Remove(order);
            }
        }

        public OrderEntity Edit(OrderEntity entity)
        {
            var order =  _db.Orders.FirstOrDefault(a => a.Id == entity.Id);
            order.CustomerId = entity.CustomerId;
            order.EmployeeId = entity.EmployeeId;       
            order.AdditionalInfo = entity.AdditionalInfo;
            order.TotalPrice = entity.TotalPrice;
            order.MakingDate = entity.MakingDate;
            order.Stage = entity.Stage;
            return order;
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            return  _db.Orders.ToList();
        }

        public OrderEntity GetById(int id)
        {
            return  _db.Orders.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<OrderEntity> GetByCustomerId(int id)
        {
            var orderlist =  _db.Orders.Where(a => a.CustomerId == id).ToList();
            return orderlist;

        }

        public OrderEntity Insert(OrderEntity entity)
        {
            _db.Orders.Add(entity);
            return entity;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<OrderEntity> GetListByEmployeeId(int id)
        {
            return _db.Orders.Where(a => a.EmployeeId == id);
        }
    }
}