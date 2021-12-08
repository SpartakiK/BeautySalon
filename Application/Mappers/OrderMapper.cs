using Domain.Abstract.Mappers;
using Domain.Mappers.Abstract;
using Domain.Models;
using Data.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
   public class OrderMapper : IOrderMapper
    {
        private readonly IEmployeeMapper _employeeMapper;
        private readonly IUserMapper _userMapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        public OrderMapper
            (
            IEmployeeMapper employeeMapper,
            IEmployeeRepository employeeRepository,
            IUserRepository userRepository,
            IUserMapper userMapper
            )
        {
            _employeeMapper = employeeMapper;
            _userMapper = userMapper;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            
        }

        public OrderEntity ToEntity(OrderModel values)
        {
            var entity = new OrderEntity
            {
                Id = values.Id,
                CustomerId = values.Customer.Id,
                EmployeeId = values.Employee.Id,
                AdditionalInfo = values.AdditionalInfo,
                MakingDate = values.MakingDate,
                Stage = values.Stage,
                CreationDate = values.CreationDate,
                TotalPrice = values.TotalPrice

            };
            return entity;
        }
        public OrderModel ToModel(OrderEntity values)
        {
            if (values == null)
            {
                return null;
            }
            var model = new OrderModel
            {
                Id = values.Id,
                Customer = _userMapper.ToModel(_userRepository.GetById(values.CustomerId)),
                Employee = _employeeMapper.ToModel(_employeeRepository.GetById(values.EmployeeId)),
                AdditionalInfo = values.AdditionalInfo,
                MakingDate = values.MakingDate,
                Stage = values.Stage,
                CreationDate = values.CreationDate,
                TotalPrice = values.TotalPrice
            };
            return model; 
        }

        public IEnumerable<OrderEntity> ToEntityList(IEnumerable<OrderModel> modellist)
        {
            var entitylist = new List<OrderEntity>();
            foreach (var item in modellist)
            {
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }

        public IEnumerable<OrderModel> ToModelList(IEnumerable<OrderEntity> entitylist)
        {
            var modellist = new List<OrderModel>();
            foreach (var item in entitylist)
            {
                modellist.Add(ToModel(item));
            }
            return modellist;
        }
    }

}
