using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstract
{
    public interface IOrderStatusHistoryServices
    {
        IEnumerable<OrderStatusHistoryModel> GetList();
        OrderStatusHistoryModel GetById(int id);
        void Create(OrderStatusHistoryModel values);
        void Delete(int id);
    }
}
