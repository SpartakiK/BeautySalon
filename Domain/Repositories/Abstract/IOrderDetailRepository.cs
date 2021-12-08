using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailEntity> GetAll();
        IEnumerable<OrderDetailEntity> GetListByOrderId(int id);
        OrderDetailEntity GetById(int id);
        OrderDetailEntity Insert(OrderDetailEntity entity);
        OrderDetailEntity Edit(OrderDetailEntity entity);
        void Delete(int id);
        void Save();
    }
}
