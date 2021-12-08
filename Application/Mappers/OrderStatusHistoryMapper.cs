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
    public class OrderStatusHistoryMapper : IOrderStatusHistoryMapper
    {
        private readonly IEmployeeMapper _employeeMapper;
        private readonly IEmployeeRepository _employeeRepository;
        public OrderStatusHistoryMapper(IEmployeeMapper employeeMapper, IEmployeeRepository employeeRepository)
        {
            _employeeMapper = employeeMapper;
            _employeeRepository = employeeRepository;
            
        }
        public OrderStatusHistoryEntity ToEntity(OrderStatusHistoryModel values)
        {
            var orderstatushistoryentity = new OrderStatusHistoryEntity
            {
                EmployeeId = values.EmployeeId,
                Id = values.Id,
                CreationDate = values.CreationDate,
                Status = values.Status,
                OrderId = values.OrderId
            };
            return orderstatushistoryentity;
        }

        public OrderStatusHistoryModel ToModel(OrderStatusHistoryEntity values)
        {
            var orderstatushistorymodel = new OrderStatusHistoryModel
            {
                EmployeeId = values.EmployeeId,
                Id = values.Id,
                CreationDate = values.CreationDate,
                OrderId = values.OrderId,
                Status = values.Status
            };
            return orderstatushistorymodel;
        }
        public IEnumerable<OrderStatusHistoryEntity> ToEntityList(IEnumerable<OrderStatusHistoryModel> modellist)
        {
            var entitylist = new List<OrderStatusHistoryEntity>();
            foreach (var item in modellist)
            {
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }

        public IEnumerable<OrderStatusHistoryModel> ToModelList(IEnumerable<OrderStatusHistoryEntity> entitylist)
        {
            var modellist = new List<OrderStatusHistoryModel>();
            foreach (var item in entitylist)
            {
                modellist.Add(ToModel(item));
            }
            return modellist;
        }
    }
}
