using Domain.Abstract.Mappers;
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
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeMapper _employeeMapper;
        public EmployeeServices(IEmployeeRepository employeeRepository, IEmployeeMapper employeeMapper)
        {
            _employeeRepository = employeeRepository;
            _employeeMapper = employeeMapper;
        }
        public void Create(EmployeeModel values)
        {
            values.CreationDate = DateTime.Now;
            _employeeRepository.Insert(_employeeMapper.ToEntity(values));
           _employeeRepository.Save();
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
            _employeeRepository.Save();
        }

        public EmployeeModel GetById(int id)
        {
            var employeeentitty = _employeeRepository.GetById(id);
            return _employeeMapper.ToModel(employeeentitty);
        }

        public EmployeeModel GetByUserId(int id)
        {
            return _employeeMapper.ToModel(_employeeRepository.GetByUserId(id));
        }

        public IEnumerable<EmployeeModel> GetList()
        {
            return _employeeMapper.ToModelList(_employeeRepository.GetAll());
        }

        public void Save()
        {
            _employeeRepository.Save();
        }
    }
}
