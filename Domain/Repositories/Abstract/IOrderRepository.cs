using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IOrderRepository
    {
        IEnumerable<OrderEntity> GetByCustomerId(int id);

        IEnumerable<OrderEntity> GetListByEmployeeId(int id);
        IEnumerable<OrderEntity> GetAll();
        OrderEntity GetById(int id);
        OrderEntity Insert(OrderEntity entity);
        OrderEntity Edit(OrderEntity entity);
        void Delete(int id);
        void Save();
    }
}
