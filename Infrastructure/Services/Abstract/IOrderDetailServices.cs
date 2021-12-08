using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstract
{
    public interface IOrderDetailServices
    {
        IEnumerable<OrderDetailModel> GetList();
        IEnumerable<OrderDetailModel> GetListByOrderId(int id);
        OrderDetailModel GetById(int id);
        void Create(OrderDetailModel values);
        void Delete(int id);
    }
}
