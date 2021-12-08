using Domain.Commands;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Repositories;
using EntityFrameworkCore.DbConnection;
using Data.Repositories.Abstract;
using Domain.Commands.Abstract;
using Domain.Models;
using Domain.Mappers;
using Domain.Mappers.Abstract;

namespace Domain.Commands
{ 
    public class UserCommands : IUserCommands
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserMapper _userMapper;
        public UserCommands
            (
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository,
            IOrderRepository orderRepository,
            IUserMapper userMapper
            )
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _orderRepository = orderRepository;
            _userMapper = userMapper;

        }

        public UserModel BlockUser(int id)
        {
            UserEntity user =  _userRepository.GetById(id);
            UserEntity blockeduser = user;
            blockeduser.Status = "Blocked";
            _userRepository.Edit(blockeduser);
            return _userMapper.ToModel(user);
        }

        public bool CheckIfEmployee(int id)
        {
            var employee =  _userRepository.GetById(id);
            if (employee == null)
            {
                return (bool)false;
            }
            return (bool)true;
        }

        public  bool CheckIfExist(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<OrderEntity> GetUserOrders(int userid)
        {
            var orders = _orderRepository.GetByCustomerId(userid);
            return orders;
        }

        public UserModel MakeAdmin(int id)
        {
            var user = _userRepository.GetById(id);
            var employee = _employeeRepository.GetByUserId(id);
            if (employee == null)
            {
                var newemployee = new EmployeeEntity
                {
                    UserId = user.Id,
                };
                _employeeRepository.Insert(newemployee);
                user.Status = "Admin";
                return _userMapper.ToModel(user);
            }
            else if (employee != null)
            {
                user.Status = "Admin";
                return _userMapper.ToModel(user);
            }
            return _userMapper.ToModel(user);
        }

        public UserModel MakeCustomer(int id)
        {
            var user = _userRepository.GetById(id);
            user.Status = "Customer";           
            return _userMapper.ToModel(user);
        }

        public UserModel MakeEmployee(int id)
        {
            var user =  _userRepository.GetById(id);
            var employee = _employeeRepository.GetByUserId(id);
            if (employee == null)
            {
                var newemployee = new EmployeeEntity
                {
                    UserId = user.Id,
                };
                user.Status = "Employee";
                _employeeRepository.Insert(newemployee);
                return _userMapper.ToModel(user);
            }
            else if (employee != null)
            {
                user.Status = "Employee";
            }
            return _userMapper.ToModel(user);
        }


    }
}

