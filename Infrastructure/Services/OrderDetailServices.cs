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
    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderDetailMapper _orderDetailMapper;
        public OrderDetailServices(IOrderDetailRepository orderDetailRepository, IOrderDetailMapper orderDetailMapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderDetailMapper = orderDetailMapper;
        }
        public void Create(OrderDetailModel values)
        {
            values.CreationDate = DateTime.Now;
            _orderDetailRepository.Insert(_orderDetailMapper.ToEntity(values));
            _orderDetailRepository.Save();
        }

        public void Delete(int id)
        {
            _orderDetailRepository.Delete(id);
            _orderDetailRepository.Save();
        }

        public OrderDetailModel GetById(int id)
        {
            return _orderDetailMapper.ToModel(_orderDetailRepository.GetById(id));
        }

        public IEnumerable<OrderDetailModel> GetList()
        { 
            return _orderDetailMapper.ToModelList(_orderDetailRepository.GetAll());
        }

        public IEnumerable<OrderDetailModel> GetListByOrderId(int id)
        {
            return _orderDetailMapper.ToModelList(_orderDetailRepository.GetListByOrderId(id));
        }
    }
}
