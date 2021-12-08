using Domain.Commands.Abstract;
using Domain.Mappers;
using Domain.Models;
using Entities;
using Data.Repositories;
using Data.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Mappers.Abstract;

namespace Services
{
    public class UserServices : IUserServices

    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCommands _userCommands;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserMapper _userMapper;
        public UserServices
            (
            IUserRepository userRepository,
            IUserCommands userCommands,
            IOrderRepository orderRepository,
            IUserMapper userMapper
            )
        {
            _userRepository = userRepository;
            _userCommands = userCommands;
            _orderRepository = orderRepository;
            _userMapper = userMapper;
        }

        public void Create(UserModel values)
        {
            values.Status = "Customer";
            values.CreationDate = DateTime.Now;
            _userRepository.Insert(_userMapper.ToEntity(values));
            _userRepository.Save();
        }

        public UserEntity EditUserAccount(UserModel newvalues)
        {
            UserEntity user = _userRepository.GetById(newvalues.Id);
            var isemployee =  _userCommands.CheckIfEmployee(newvalues.Id);

                if (newvalues.Status == "Customer")
                {
                     _userCommands.MakeCustomer(user.Id);
                    _userRepository.Edit(_userMapper.ToEntity(newvalues));
                _userRepository.Save();
                   return user;
                 }
                if (newvalues.Status == "Employee")
                {
                     _userCommands.MakeEmployee(user.Id);
                     _userRepository.Edit(_userMapper.ToEntity(newvalues));                
                     _userRepository.Save();
                return user;
            }
                if (newvalues.Status == "Admin")
                {
                     _userCommands.MakeAdmin(user.Id);
                     _userRepository.Edit(_userMapper.ToEntity(newvalues));
                _userRepository.Save();
                return user;
            }
                if (newvalues.Status == "Blocked")
                {
                    _userCommands.BlockUser(user.Id);
                _userRepository.Save();
                return user;
                }
            _userRepository.Save();
            return user;
        }



        public UserModel GetByPhoneNubmer(string phonenumber)
        {
            return _userMapper.ToModel(_userRepository.GetByPhoneNubmer(phonenumber));
        }

        public UserModel GetById(int id)
        {
            var userentity = _userRepository.GetById(id);
            if (userentity == null)
            {
                return null;
            }
            return _userMapper.ToModel(userentity);
        }

        public bool PasswordCheck(string phonenumber, string password)
        {
            var user = _userRepository.GetByPhoneNubmer(phonenumber);
            if (user == null)
            {
                return false;

            }
            if (user.Password == password)
            {
                return true;
            }
            return false;
        }

        public void Save()
        {
            _userRepository.Save();
        }
    }
}
