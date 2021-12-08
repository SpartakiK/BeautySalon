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
    public class OrderDetailMapper : IOrderDetailMapper
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceMapper _serviceMapper;
        public OrderDetailMapper(IServiceRepository serviceRepository, IServiceMapper serviceMapper)
        {
            _serviceRepository = serviceRepository;
            _serviceMapper = serviceMapper;
        }

        public OrderDetailEntity ToEntity(OrderDetailModel values)
        {
            var orderdetailentity = new OrderDetailEntity
            {
                Id = values.Id,
                Comission = values.Comission,
                ServiceId = values.Service.Id,
                CreationDate = values.CreationDate,
                OrderId = values.OrderId,
                Price = values.Price
            };
            return orderdetailentity;
        }

        public OrderDetailModel ToModel(OrderDetailEntity values)
        {
            var orderdetailmodel = new OrderDetailModel
            {
                Id = values.Id,
                Comission = values.Comission,
                Service = _serviceMapper.ToModel(_serviceRepository.GetById(values.ServiceId)),
                CreationDate = values.CreationDate,
                OrderId = values.OrderId,
                Price = values.Price
            };
            return orderdetailmodel;
        }

        public IEnumerable<OrderDetailEntity> ToEntityList(IEnumerable<OrderDetailModel> modellist)
        {
            var entitylist = new List<OrderDetailEntity>();
            foreach (var item in modellist)
            {
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }

        public IEnumerable<OrderDetailModel> ToModelList(IEnumerable<OrderDetailEntity> entitylist)
        {
            var modellist = new List<OrderDetailModel>();
            foreach (var item in entitylist)
            {
                modellist.Add(ToModel(item));
            }

            return modellist;
        }

    }

}
