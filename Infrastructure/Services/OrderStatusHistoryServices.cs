using Domain.Mappers.Abstract;
using Domain.Models;
using Data.Repositories.Abstract;
using Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OrderStatusHistoryServices : IOrderStatusHistoryServices
    {
        private readonly IOrderStatusHistoryRepository _orderStatusHistoryRepository;
        private readonly IOrderStatusHistoryMapper _orderStatusHistoryMapper;
        public OrderStatusHistoryServices
            (
            IOrderStatusHistoryRepository orderStatusHistoryRepository,
            IOrderStatusHistoryMapper orderStatusHistoryMapper
            )
        {
            _orderStatusHistoryRepository = orderStatusHistoryRepository;
            _orderStatusHistoryMapper = orderStatusHistoryMapper;
        }
        public void Create(OrderStatusHistoryModel values)
        {
            values.CreationDate = DateTime.Now;
            _orderStatusHistoryRepository.Insert(_orderStatusHistoryMapper.ToEntity(values));
            _orderStatusHistoryRepository.Save();
        }

        public void Delete(int id)
        {
            _orderStatusHistoryRepository.Delete(id);
            _orderStatusHistoryRepository.Save();
        }

        public OrderStatusHistoryModel GetById(int id)
        {
            return _orderStatusHistoryMapper.ToModel(_orderStatusHistoryRepository.GetById(id));
        }

        public IEnumerable<OrderStatusHistoryModel> GetList()
        {
            return _orderStatusHistoryMapper.ToModelList(_orderStatusHistoryRepository.GetAll());
        }
    }
}
