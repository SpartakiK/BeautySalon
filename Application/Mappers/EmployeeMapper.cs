using Domain.Abstract.Mappers;
using Domain.Mappers.Abstract;
using Domain.Models;
using Data.Repositories;
using Data.Repositories.Abstract;
using Entities;
using EntityFrameworkCore.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    public class EmployeeMapper : IEmployeeMapper
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;
        public EmployeeMapper(IUserRepository userRepository, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }
        public  EmployeeEntity ToEntity( EmployeeModel values)
        {
            var employeeentity = new EmployeeEntity
            {
                Id = values.Id,
                UserId = values.User.Id,     
                CreationDate = values.CreationDate
            };
            return employeeentity;
        }
        public  EmployeeModel ToModel(EmployeeEntity values)
        {
            if(values == null)
            {
                return null;
            }
            var employeeemodel = new EmployeeModel
            {
                Id = values.Id,
                User = _userMapper.ToModel(_userRepository.GetById(values.UserId)),
                CreationDate = values.CreationDate
            };
            return employeeemodel;
        }

        public  IEnumerable<EmployeeEntity> ToEntityList(IEnumerable<EmployeeModel> usermodel)
        {
            var entitylist = new List<EmployeeEntity>();
            foreach (var item in usermodel)
            {
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }

        public IEnumerable<EmployeeModel> ToModelList(IEnumerable<EmployeeEntity> userentity)
        {
            var modellist = new List<EmployeeModel>();
            foreach (var item in userentity)
            {
                modellist.Add(ToModel(item));
            }

            return modellist;
        }
    }
}
