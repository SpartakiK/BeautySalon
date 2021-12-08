using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IOrderServices
    {
        IEnumerable<OrderModel> GetList();
        IEnumerable<OrderModel> GetListByEmployeeId(int id);
        IEnumerable<OrderModel> GetListByUserEmployeeId(int id);
        IEnumerable<OrderModel> GetListByCustomerId(int id);
        OrderModel GetById(int id);
        void Edit(OrderModel values);
        void Create(int customerid, int employeeid, DateTime makingdate, string additionalinfo);
        void Delete(int id);
        void Save();
    }

}
