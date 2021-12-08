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
    public class EmployeesAndServicesServices : IEmployeesAndServicesServices
    {
        private readonly IEmployeesAndServicesMapper _employeesAndServicesMapper;
        private readonly IEmployeesAndServicesRepository _employeesAndServicesRepository;
        public EmployeesAndServicesServices
            (
            IEmployeesAndServicesMapper employeesAndServicesMapper,
            IEmployeesAndServicesRepository employeesAndServicesRepository
            )
        {
            _employeesAndServicesMapper = employeesAndServicesMapper;
            _employeesAndServicesRepository = employeesAndServicesRepository;
        }
        public void Delete(int id)
        {
            _employeesAndServicesRepository.Delete(id);
            _employeesAndServicesRepository.Save();
        }

        public void Edit(EmployeesAndServicesModel values)
        {
            _employeesAndServicesRepository.Edit(_employeesAndServicesMapper.ToEntity(values));
            _employeesAndServicesRepository.Save();
        }

        public IEnumerable<EmployeesAndServicesModel> GetList()
        {
            return _employeesAndServicesMapper.ToModelList(_employeesAndServicesRepository.GetAll());
        }

        public EmployeesAndServicesModel GetById(int id)
        {
            return _employeesAndServicesMapper.ToModel(_employeesAndServicesRepository.GetById(id));
        }

        public IEnumerable<EmployeesAndServicesModel> GetEmployeesByServiceId(int id)
        {

            return _employeesAndServicesMapper.ToModelList(_employeesAndServicesRepository.GetEmployeesByServiceId(id));
        }

        public void Create(int serviceid, int employeeid)
        {
           _employeesAndServicesRepository.Insert(serviceid, employeeid);
            _employeesAndServicesRepository.Save();
        }

        public void Save()
        {
            _employeesAndServicesRepository.Save();
        }

        public IEnumerable<EmployeesAndServicesModel> GetServicesByEmployeeId(int id)
        {
            return _employeesAndServicesMapper.ToModelList(_employeesAndServicesRepository.GetServicesByEmployeeId(id));
        }
    }
}

