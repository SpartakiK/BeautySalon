using Domain.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Abstract
{
    public interface IUserCommands
    {
        UserModel BlockUser(int id);
        bool CheckIfEmployee(int id);
        bool CheckIfExist(int id);
        UserModel MakeAdmin(int id);
        UserModel MakeCustomer(int id);
        UserModel MakeEmployee(int id);
        IEnumerable<OrderEntity> GetUserOrders(int userid);

    }
}
