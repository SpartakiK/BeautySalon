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
    public class OrderStatusHistoryRepository : IOrderStatusHistoryRepository
    {
        private readonly DbConnect _db;
        public OrderStatusHistoryRepository(DbConnect db)
        {
            _db = db;
        }
        
        public void Delete(int id)
        {
            var orderstatushistories = _db.OrderStatusHistories.FirstOrDefault(a => a.Id == id);
            if (orderstatushistories != null)
            {
                _db.OrderStatusHistories.Remove(orderstatushistories);
            }
        }

        public void Edit(OrderStatusHistoryEntity entity)
        {
            var order = _db.OrderStatusHistories.FirstOrDefault(a => a.Id == entity.Id);
            order.EmployeeId = entity.EmployeeId;
            order.OrderId = entity.OrderId;
            order.Status = entity.Status;
        }

        public IEnumerable<OrderStatusHistoryEntity> GetAll()
        {
            return _db.OrderStatusHistories.ToList();
        }

        public OrderStatusHistoryEntity GetById(int id)
        {
            return _db.OrderStatusHistories.FirstOrDefault(a => a.Id == id);
        }

        public void Insert(OrderStatusHistoryEntity entity)
        {
            _db.OrderStatusHistories.Add(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
