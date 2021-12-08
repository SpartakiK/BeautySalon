using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IOrderStatusHistoryRepository
    {
        IEnumerable<OrderStatusHistoryEntity> GetAll();
        OrderStatusHistoryEntity GetById(int id);
        void Insert(OrderStatusHistoryEntity entity);
        void Edit(OrderStatusHistoryEntity entity);
        void Delete(int id);
        void Save();
    }
}
