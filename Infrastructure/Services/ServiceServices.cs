using Domain.Mappers;
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
    public class ServiceServices : IServiceServices
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceMapper _serviceMapper;
        public ServiceServices(IServiceRepository serviceRepository, IServiceMapper serviceMapper)
        {
            _serviceRepository = serviceRepository;
            _serviceMapper = serviceMapper;
        }



        public void Create(ServiceModel values)
        {
            values.CreationDate = DateTime.Now;
            _serviceRepository.Insert(_serviceMapper.ToEntity(values));
            _serviceRepository.Save();
        }

        public void Delete(int id)
        {
            _serviceRepository.Delete(id);
            _serviceRepository.Save();
        }

        public void Edit(ServiceModel values)
        {
            _serviceRepository.Edit(_serviceMapper.ToEntity(values));
            _serviceRepository.Save();
        }

        public ServiceModel GetById(int id)
        {
            return _serviceMapper.ToModel(_serviceRepository.GetById(id));
        }

        public IEnumerable<ServiceModel> GetList()
        { 
            return _serviceMapper.ToModelList(_serviceRepository.GetAll());
        }

        public void Save()
        {
            _serviceRepository.Save();
        }
    }
}
