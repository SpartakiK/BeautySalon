using Domain.Mappers.Abstract;
using Domain.Models;
using Data.Repositories.Abstract;
using Entities;
using Services.Abstract;
using Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderMapper _orderMapper;
        private readonly IEmployeeServices _employeeServices;
        private readonly IUserRepository _userRepository;
        public OrderServices(IOrderRepository orderRepository, IOrderMapper orderMapper, IEmployeeServices employeeServices, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderMapper = orderMapper;
            _employeeServices = employeeServices;
            _userRepository = userRepository;
        }

        public void Create(int customerid, int employeeid, DateTime makingdate, string additionalinfo)
        {
            var values = new OrderEntity
            {
                CustomerId = customerid,
                EmployeeId = employeeid,
                AdditionalInfo = additionalinfo,
                CreationDate = DateTime.Now,
                Stage = "booked",
                MakingDate = makingdate
            };
            _orderRepository.Insert(values);
            _orderRepository.Save();
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
            _orderRepository.Save();
        }

        public void Edit(OrderModel values)
        {
            _orderRepository.Edit(_orderMapper.ToEntity(values));
            _orderRepository.Save();
        }

        public OrderModel GetById(int id)
        {
            return _orderMapper.ToModel(_orderRepository.GetById(id));
        }

        public IEnumerable<OrderModel> GetList()
        {
            return _orderMapper.ToModelList(_orderRepository.GetAll());
        }

        public IEnumerable<OrderModel> GetListByCustomerId(int id)
        {
            
            return _orderMapper.ToModelList(_orderRepository.GetByCustomerId(id));
        }

        public IEnumerable<OrderModel> GetListByEmployeeId(int id)
        {
           return _orderMapper.ToModelList(_orderRepository.GetListByEmployeeId(id));
        }

        public IEnumerable<OrderModel> GetListByUserEmployeeId(int id)
        {
            var employee = _employeeServices.GetByUserId(id);
            return GetListByEmployeeId(employee.Id);
        }

        public void Save()
        {
            _orderRepository.Save();
        }
    }
}
