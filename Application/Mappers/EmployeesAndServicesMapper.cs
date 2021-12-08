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
    public class EmployeesAndServicesMapper : IEmployeesAndServicesMapper
    {
        private readonly IServiceMapper _serviceMapper;
        private readonly IServiceRepository _serviceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeMapper _employeeMapper;
        public EmployeesAndServicesMapper
            (
            IServiceMapper serviceMapper,
            IServiceRepository serviceRepository,
            IEmployeeRepository employeeRepository,
            IEmployeeMapper employeeMapper
            )
        {
            _serviceMapper = serviceMapper;
            _serviceRepository = serviceRepository;
            _employeeMapper = employeeMapper;
            _employeeRepository = employeeRepository;
        }
        public EmployeesAndServicesEntity ToEntity(EmployeesAndServicesModel values)
        {
            var entity = new EmployeesAndServicesEntity 
            {
                Id = values.Id,
                EmployeeId = values.Employee.Id,
                ServiceId = values.Service.Id,
                CreationDate = values.CreationDate
            };
            return entity;
        }

        public EmployeesAndServicesModel ToModel(EmployeesAndServicesEntity values)
        {
            var model = new EmployeesAndServicesModel
            {
                Id = values.Id,
                Employee = _employeeMapper.ToModel(_employeeRepository.GetById(values.EmployeeId)),
                Service = _serviceMapper.ToModel(_serviceRepository.GetById(values.ServiceId)),
                CreationDate = values.CreationDate
            };
            return model;
        }

        public IEnumerable<EmployeesAndServicesEntity> ToEntityList(IEnumerable<EmployeesAndServicesModel> modellist)
        {
            var entitylist = new List<EmployeesAndServicesEntity>();
            foreach (var item in modellist)
            {
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }
        public IEnumerable<EmployeesAndServicesModel> ToModelList(IEnumerable<EmployeesAndServicesEntity> entitylist)
        {
            var modellist = new List<EmployeesAndServicesModel>();
            foreach (var item in entitylist)
            {
                modellist.Add(ToModel(item));
            }
            return modellist;
        }
    }
}
